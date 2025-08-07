using System;
using KSerialization;
using UnityEngine;

// Token: 0x0200095F RID: 2399
[SerializationConfig(MemberSerialization.OptIn)]
public class IceMachine : StateMachineComponent<IceMachine.StatesInstance>, FewOptionSideScreen.IFewOptionSideScreen
{
	// Token: 0x060044D6 RID: 17622 RVA: 0x0018AEA7 File Offset: 0x001890A7
	public void SetStorages(Storage waterStorage, Storage iceStorage)
	{
		this.waterStorage = waterStorage;
		this.iceStorage = iceStorage;
	}

	// Token: 0x060044D7 RID: 17623 RVA: 0x0018AEB8 File Offset: 0x001890B8
	private bool CanMakeIce()
	{
		bool flag = this.waterStorage != null && this.waterStorage.GetMassAvailable(SimHashes.Water) >= 0.1f;
		bool flag2 = this.iceStorage != null && this.iceStorage.IsFull();
		return flag && !flag2;
	}

	// Token: 0x060044D8 RID: 17624 RVA: 0x0018AF18 File Offset: 0x00189118
	private void MakeIce(IceMachine.StatesInstance smi, float dt)
	{
		float num = this.heatRemovalRate * dt / (float)this.waterStorage.items.Count;
		foreach (GameObject gameObject in this.waterStorage.items)
		{
			GameUtil.DeltaThermalEnergy(gameObject.GetComponent<PrimaryElement>(), -num, smi.master.targetTemperature);
		}
		for (int i = this.waterStorage.items.Count; i > 0; i--)
		{
			GameObject gameObject2 = this.waterStorage.items[i - 1];
			if (gameObject2 && gameObject2.GetComponent<PrimaryElement>().Temperature < gameObject2.GetComponent<PrimaryElement>().Element.lowTemp)
			{
				PrimaryElement component = gameObject2.GetComponent<PrimaryElement>();
				this.waterStorage.AddOre(this.targetProductionElement, component.Mass, component.Temperature, component.DiseaseIdx, component.DiseaseCount, false, true);
				this.waterStorage.ConsumeIgnoringDisease(gameObject2);
			}
		}
		smi.UpdateIceState();
	}

	// Token: 0x060044D9 RID: 17625 RVA: 0x0018B040 File Offset: 0x00189240
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x060044DA RID: 17626 RVA: 0x0018B054 File Offset: 0x00189254
	public FewOptionSideScreen.IFewOptionSideScreen.Option[] GetOptions()
	{
		FewOptionSideScreen.IFewOptionSideScreen.Option[] array = new FewOptionSideScreen.IFewOptionSideScreen.Option[IceMachineConfig.ELEMENT_OPTIONS.Length];
		for (int i = 0; i < array.Length; i++)
		{
			string text = Strings.Get("STRINGS.BUILDINGS.PREFABS.ICEMACHINE.OPTION_TOOLTIPS." + IceMachineConfig.ELEMENT_OPTIONS[i].ToString().ToUpper());
			array[i] = new FewOptionSideScreen.IFewOptionSideScreen.Option(IceMachineConfig.ELEMENT_OPTIONS[i], ElementLoader.GetElement(IceMachineConfig.ELEMENT_OPTIONS[i]).name, Def.GetUISprite(IceMachineConfig.ELEMENT_OPTIONS[i], "ui", false), text);
		}
		return array;
	}

	// Token: 0x060044DB RID: 17627 RVA: 0x0018B0F8 File Offset: 0x001892F8
	public void OnOptionSelected(FewOptionSideScreen.IFewOptionSideScreen.Option option)
	{
		this.targetProductionElement = ElementLoader.GetElementID(option.tag);
	}

	// Token: 0x060044DC RID: 17628 RVA: 0x0018B10B File Offset: 0x0018930B
	public Tag GetSelectedOption()
	{
		return this.targetProductionElement.CreateTag();
	}

	// Token: 0x04002E0D RID: 11789
	[MyCmpGet]
	private Operational operational;

	// Token: 0x04002E0E RID: 11790
	public Storage waterStorage;

	// Token: 0x04002E0F RID: 11791
	public Storage iceStorage;

	// Token: 0x04002E10 RID: 11792
	public float targetTemperature;

	// Token: 0x04002E11 RID: 11793
	public float heatRemovalRate;

	// Token: 0x04002E12 RID: 11794
	private static StatusItem iceStorageFullStatusItem;

	// Token: 0x04002E13 RID: 11795
	[Serialize]
	public SimHashes targetProductionElement = SimHashes.Ice;

	// Token: 0x02001971 RID: 6513
	public class StatesInstance : GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.GameInstance
	{
		// Token: 0x06009F33 RID: 40755 RVA: 0x00398E60 File Offset: 0x00397060
		public StatesInstance(IceMachine smi)
			: base(smi)
		{
			this.meter = new MeterController(base.gameObject.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_OL", "meter_frame", "meter_fill" });
			this.UpdateMeter();
			base.Subscribe(-1697596308, new Action<object>(this.OnStorageChange));
		}

		// Token: 0x06009F34 RID: 40756 RVA: 0x00398ED2 File Offset: 0x003970D2
		private void OnStorageChange(object data)
		{
			this.UpdateMeter();
		}

		// Token: 0x06009F35 RID: 40757 RVA: 0x00398EDA File Offset: 0x003970DA
		public void UpdateMeter()
		{
			this.meter.SetPositionPercent(Mathf.Clamp01(base.smi.master.iceStorage.MassStored() / base.smi.master.iceStorage.Capacity()));
		}

		// Token: 0x06009F36 RID: 40758 RVA: 0x00398F18 File Offset: 0x00397118
		public void UpdateIceState()
		{
			bool flag = false;
			for (int i = base.smi.master.waterStorage.items.Count; i > 0; i--)
			{
				GameObject gameObject = base.smi.master.waterStorage.items[i - 1];
				if (gameObject && gameObject.GetComponent<PrimaryElement>().Temperature <= base.smi.master.targetTemperature)
				{
					flag = true;
				}
			}
			base.sm.doneFreezingIce.Set(flag, this, false);
		}

		// Token: 0x04007C1E RID: 31774
		private MeterController meter;

		// Token: 0x04007C1F RID: 31775
		public Chore emptyChore;
	}

	// Token: 0x02001972 RID: 6514
	public class States : GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine>
	{
		// Token: 0x06009F37 RID: 40759 RVA: 0x00398FA8 File Offset: 0x003971A8
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
			this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on, (IceMachine.StatesInstance smi) => smi.master.operational.IsOperational);
			this.on.PlayAnim("on").EventTransition(GameHashes.OperationalChanged, this.off, (IceMachine.StatesInstance smi) => !smi.master.operational.IsOperational).DefaultState(this.on.waiting);
			this.on.waiting.EventTransition(GameHashes.OnStorageChange, this.on.working_pre, (IceMachine.StatesInstance smi) => smi.master.CanMakeIce());
			this.on.working_pre.Enter(delegate(IceMachine.StatesInstance smi)
			{
				smi.UpdateIceState();
			}).PlayAnim("working_pre").OnAnimQueueComplete(this.on.working);
			this.on.working.QueueAnim("working_loop", true, null).Update("UpdateWorking", delegate(IceMachine.StatesInstance smi, float dt)
			{
				smi.master.MakeIce(smi, dt);
			}, UpdateRate.SIM_200ms, false).ParamTransition<bool>(this.doneFreezingIce, this.on.working_pst, GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.IsTrue)
				.Enter(delegate(IceMachine.StatesInstance smi)
				{
					smi.master.operational.SetActive(true, false);
					smi.master.gameObject.GetComponent<ManualDeliveryKG>().Pause(true, "Working");
				})
				.Exit(delegate(IceMachine.StatesInstance smi)
				{
					smi.master.operational.SetActive(false, false);
					smi.master.gameObject.GetComponent<ManualDeliveryKG>().Pause(false, "Done Working");
				})
				.ToggleStatusItem(Db.Get().BuildingStatusItems.CoolingWater, null);
			this.on.working_pst.Exit(new StateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.State.Callback(this.DoTransfer)).PlayAnim("working_pst").OnAnimQueueComplete(this.on);
		}

		// Token: 0x06009F38 RID: 40760 RVA: 0x003991CC File Offset: 0x003973CC
		private void DoTransfer(IceMachine.StatesInstance smi)
		{
			for (int i = smi.master.waterStorage.items.Count - 1; i >= 0; i--)
			{
				GameObject gameObject = smi.master.waterStorage.items[i];
				if (gameObject && gameObject.GetComponent<PrimaryElement>().Temperature <= smi.master.targetTemperature)
				{
					smi.master.waterStorage.Transfer(gameObject, smi.master.iceStorage, false, true);
				}
			}
			smi.UpdateMeter();
		}

		// Token: 0x04007C20 RID: 31776
		public StateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.BoolParameter doneFreezingIce;

		// Token: 0x04007C21 RID: 31777
		public GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.State off;

		// Token: 0x04007C22 RID: 31778
		public IceMachine.States.OnStates on;

		// Token: 0x02002853 RID: 10323
		public class OnStates : GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.State
		{
			// Token: 0x0400B2E9 RID: 45801
			public GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.State waiting;

			// Token: 0x0400B2EA RID: 45802
			public GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.State working_pre;

			// Token: 0x0400B2EB RID: 45803
			public GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.State working;

			// Token: 0x0400B2EC RID: 45804
			public GameStateMachine<IceMachine.States, IceMachine.StatesInstance, IceMachine, object>.State working_pst;
		}
	}
}
