using System;

// Token: 0x020000F1 RID: 241
public class HiveEatingMonitor : GameStateMachine<HiveEatingMonitor, HiveEatingMonitor.Instance, IStateMachineTarget, HiveEatingMonitor.Def>
{
	// Token: 0x0600045D RID: 1117 RVA: 0x00024345 File Offset: 0x00022545
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.WantsToEat, new StateMachine<HiveEatingMonitor, HiveEatingMonitor.Instance, IStateMachineTarget, HiveEatingMonitor.Def>.Transition.ConditionCallback(HiveEatingMonitor.ShouldEat), null);
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x0002436D File Offset: 0x0002256D
	public static bool ShouldEat(HiveEatingMonitor.Instance smi)
	{
		return smi.storage.FindFirst(smi.def.consumedOre) != null;
	}

	// Token: 0x020010FA RID: 4346
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x040061B3 RID: 25011
		public Tag consumedOre;
	}

	// Token: 0x020010FB RID: 4347
	public new class Instance : GameStateMachine<HiveEatingMonitor, HiveEatingMonitor.Instance, IStateMachineTarget, HiveEatingMonitor.Def>.GameInstance
	{
		// Token: 0x06008130 RID: 33072 RVA: 0x0032E9F3 File Offset: 0x0032CBF3
		public Instance(IStateMachineTarget master, HiveEatingMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x040061B4 RID: 25012
		[MyCmpReq]
		public Storage storage;
	}
}
