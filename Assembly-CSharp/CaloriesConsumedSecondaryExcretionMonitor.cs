using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020009DC RID: 2524
public class CaloriesConsumedSecondaryExcretionMonitor : GameStateMachine<CaloriesConsumedSecondaryExcretionMonitor, CaloriesConsumedSecondaryExcretionMonitor.Instance>, IGameObjectEffectDescriptor
{
	// Token: 0x060049F3 RID: 18931 RVA: 0x001AC64C File Offset: 0x001AA84C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		base.serializable = StateMachine.SerializeType.Both_DEPRECATED;
		this.idle.PlayAnim("idle_loop", KAnim.PlayMode.Loop).Enter(delegate(CaloriesConsumedSecondaryExcretionMonitor.Instance smi)
		{
			this.handle = smi.gameObject.Subscribe(-2038961714, new Action<object>(smi.OnCaloriesConsumed));
		}).Exit(delegate(CaloriesConsumedSecondaryExcretionMonitor.Instance smi)
		{
			smi.gameObject.Unsubscribe(this.handle);
		});
		this.schedule_fart.ScheduleGoTo((CaloriesConsumedSecondaryExcretionMonitor.Instance smi) => global::UnityEngine.Random.Range(3f, 6f), this.needs_to_fart);
		this.needs_to_fart.Enter(new StateMachine<CaloriesConsumedSecondaryExcretionMonitor, CaloriesConsumedSecondaryExcretionMonitor.Instance, IStateMachineTarget, object>.State.Callback(CaloriesConsumedSecondaryExcretionMonitor.CreateChore)).ToggleUrge(Db.Get().Urges.Fart).EventHandler(GameHashes.BeginChore, delegate(CaloriesConsumedSecondaryExcretionMonitor.Instance smi, object o)
		{
			smi.OnStartChore(o);
		});
	}

	// Token: 0x060049F4 RID: 18932 RVA: 0x001AC724 File Offset: 0x001AA924
	public static void CreateChore(CaloriesConsumedSecondaryExcretionMonitor.Instance smi)
	{
		CreatureCalorieMonitor.CaloriesConsumedEvent consumptionData = smi.consumptionData;
		new FartChore(smi.GetComponent<ChoreProvider>(), Db.Get().ChoreTypes.Fart, consumptionData.calories * 0.001f * smi.sm.kgProducedPerKcalConsumed, smi.sm.producedElement, byte.MaxValue, 0, smi.sm.overpressureThreshold);
	}

	// Token: 0x060049F5 RID: 18933 RVA: 0x001AC788 File Offset: 0x001AA988
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(UI.BUILDINGEFFECTS.DIET_ADDITIONAL_PRODUCED.Replace("{Items}", ElementLoader.GetElement(this.producedElement.CreateTag()).name), UI.BUILDINGEFFECTS.TOOLTIPS.DIET_ADDITIONAL_PRODUCED.Replace("{Items}", ElementLoader.GetElement(this.producedElement.CreateTag()).name), Descriptor.DescriptorType.Effect, false)
		};
	}

	// Token: 0x040030BE RID: 12478
	public GameStateMachine<CaloriesConsumedSecondaryExcretionMonitor, CaloriesConsumedSecondaryExcretionMonitor.Instance, IStateMachineTarget, object>.State idle;

	// Token: 0x040030BF RID: 12479
	public GameStateMachine<CaloriesConsumedSecondaryExcretionMonitor, CaloriesConsumedSecondaryExcretionMonitor.Instance, IStateMachineTarget, object>.State schedule_fart;

	// Token: 0x040030C0 RID: 12480
	public GameStateMachine<CaloriesConsumedSecondaryExcretionMonitor, CaloriesConsumedSecondaryExcretionMonitor.Instance, IStateMachineTarget, object>.State needs_to_fart;

	// Token: 0x040030C1 RID: 12481
	public SimHashes producedElement;

	// Token: 0x040030C2 RID: 12482
	public float kgProducedPerKcalConsumed = 1f;

	// Token: 0x040030C3 RID: 12483
	private float overpressureThreshold = 2f;

	// Token: 0x040030C4 RID: 12484
	private int handle;

	// Token: 0x02001A1D RID: 6685
	public new class Instance : GameStateMachine<CaloriesConsumedSecondaryExcretionMonitor, CaloriesConsumedSecondaryExcretionMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A245 RID: 41541 RVA: 0x003A0C99 File Offset: 0x0039EE99
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x0600A246 RID: 41542 RVA: 0x003A0CA2 File Offset: 0x0039EEA2
		public void OnStartChore(object o)
		{
			if (((Chore)o).SatisfiesUrge(Db.Get().Urges.Fart))
			{
				this.GoTo(base.sm.idle);
			}
		}

		// Token: 0x0600A247 RID: 41543 RVA: 0x003A0CD1 File Offset: 0x0039EED1
		public void OnCaloriesConsumed(object data)
		{
			base.smi.consumptionData = (CreatureCalorieMonitor.CaloriesConsumedEvent)data;
			base.smi.GoTo(base.smi.sm.schedule_fart);
		}

		// Token: 0x04007EBF RID: 32447
		public CreatureCalorieMonitor.CaloriesConsumedEvent consumptionData;
	}
}
