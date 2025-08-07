using System;
using UnityEngine;

// Token: 0x02000A22 RID: 2594
public class EntityElementExchanger : StateMachineComponent<EntityElementExchanger.StatesInstance>
{
	// Token: 0x06004B4C RID: 19276 RVA: 0x001B4FAC File Offset: 0x001B31AC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06004B4D RID: 19277 RVA: 0x001B4FB4 File Offset: 0x001B31B4
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06004B4E RID: 19278 RVA: 0x001B4FC7 File Offset: 0x001B31C7
	public void SetConsumptionRate(float consumptionRate)
	{
		this.consumeRate = consumptionRate;
	}

	// Token: 0x06004B4F RID: 19279 RVA: 0x001B4FD0 File Offset: 0x001B31D0
	private static void OnSimConsumeCallback(Sim.MassConsumedCallback mass_cb_info, object data)
	{
		EntityElementExchanger entityElementExchanger = (EntityElementExchanger)data;
		if (entityElementExchanger != null)
		{
			entityElementExchanger.OnSimConsume(mass_cb_info);
		}
	}

	// Token: 0x06004B50 RID: 19280 RVA: 0x001B4FF4 File Offset: 0x001B31F4
	private void OnSimConsume(Sim.MassConsumedCallback mass_cb_info)
	{
		float num = mass_cb_info.mass * base.smi.master.exchangeRatio;
		if (this.reportExchange && base.smi.master.emittedElement == SimHashes.Oxygen)
		{
			string text = base.gameObject.GetProperName();
			ReceptacleMonitor component = base.GetComponent<ReceptacleMonitor>();
			if (component != null && component.GetReceptacle() != null)
			{
				text = text + " (" + component.GetReceptacle().gameObject.GetProperName() + ")";
			}
			ReportManager.Instance.ReportValue(ReportManager.ReportType.OxygenCreated, num, text, null);
		}
		SimMessages.EmitMass(Grid.PosToCell(base.smi.master.transform.GetPosition() + this.outputOffset), ElementLoader.FindElementByHash(base.smi.master.emittedElement).idx, num, ElementLoader.FindElementByHash(base.smi.master.emittedElement).defaultValues.temperature, byte.MaxValue, 0, -1);
	}

	// Token: 0x04003203 RID: 12803
	public Vector3 outputOffset = Vector3.zero;

	// Token: 0x04003204 RID: 12804
	public bool reportExchange;

	// Token: 0x04003205 RID: 12805
	[MyCmpReq]
	private KSelectable selectable;

	// Token: 0x04003206 RID: 12806
	public SimHashes consumedElement;

	// Token: 0x04003207 RID: 12807
	public SimHashes emittedElement;

	// Token: 0x04003208 RID: 12808
	public float consumeRate;

	// Token: 0x04003209 RID: 12809
	public float exchangeRatio;

	// Token: 0x02001AD7 RID: 6871
	public class StatesInstance : GameStateMachine<EntityElementExchanger.States, EntityElementExchanger.StatesInstance, EntityElementExchanger, object>.GameInstance
	{
		// Token: 0x0600A562 RID: 42338 RVA: 0x003A8D2E File Offset: 0x003A6F2E
		public StatesInstance(EntityElementExchanger master)
			: base(master)
		{
		}
	}

	// Token: 0x02001AD8 RID: 6872
	public class States : GameStateMachine<EntityElementExchanger.States, EntityElementExchanger.StatesInstance, EntityElementExchanger>
	{
		// Token: 0x0600A563 RID: 42339 RVA: 0x003A8D38 File Offset: 0x003A6F38
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.exchanging;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.exchanging.Enter(delegate(EntityElementExchanger.StatesInstance smi)
			{
				WiltCondition component = smi.master.gameObject.GetComponent<WiltCondition>();
				if (component != null && component.IsWilting())
				{
					smi.GoTo(smi.sm.paused);
				}
			}).EventTransition(GameHashes.Wilt, this.paused, null).ToggleStatusItem(Db.Get().CreatureStatusItems.ExchangingElementConsume, null)
				.ToggleStatusItem(Db.Get().CreatureStatusItems.ExchangingElementOutput, null)
				.Update("EntityElementExchanger", delegate(EntityElementExchanger.StatesInstance smi, float dt)
				{
					HandleVector<Game.ComplexCallbackInfo<Sim.MassConsumedCallback>>.Handle handle = Game.Instance.massConsumedCallbackManager.Add(new Action<Sim.MassConsumedCallback, object>(EntityElementExchanger.OnSimConsumeCallback), smi.master, "EntityElementExchanger");
					SimMessages.ConsumeMass(Grid.PosToCell(smi.master.gameObject), smi.master.consumedElement, smi.master.consumeRate * dt, 3, handle.index);
				}, UpdateRate.SIM_1000ms, false);
			this.paused.EventTransition(GameHashes.WiltRecover, this.exchanging, null);
		}

		// Token: 0x04008123 RID: 33059
		public GameStateMachine<EntityElementExchanger.States, EntityElementExchanger.StatesInstance, EntityElementExchanger, object>.State exchanging;

		// Token: 0x04008124 RID: 33060
		public GameStateMachine<EntityElementExchanger.States, EntityElementExchanger.StatesInstance, EntityElementExchanger, object>.State paused;
	}
}
