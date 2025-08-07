using System;

// Token: 0x020000F3 RID: 243
public class HiveGrowthMonitor : GameStateMachine<HiveGrowthMonitor, HiveGrowthMonitor.Instance, IStateMachineTarget, HiveGrowthMonitor.Def>
{
	// Token: 0x06000463 RID: 1123 RVA: 0x000244D3 File Offset: 0x000226D3
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.Behaviours.GrowUpBehaviour, new StateMachine<HiveGrowthMonitor, HiveGrowthMonitor.Instance, IStateMachineTarget, HiveGrowthMonitor.Def>.Transition.ConditionCallback(HiveGrowthMonitor.IsGrowing), null);
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x000244FB File Offset: 0x000226FB
	public static bool IsGrowing(HiveGrowthMonitor.Instance smi)
	{
		return !smi.GetSMI<BeeHive.StatesInstance>().IsFullyGrown();
	}

	// Token: 0x02001100 RID: 4352
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001101 RID: 4353
	public new class Instance : GameStateMachine<HiveGrowthMonitor, HiveGrowthMonitor.Instance, IStateMachineTarget, HiveGrowthMonitor.Def>.GameInstance
	{
		// Token: 0x0600813A RID: 33082 RVA: 0x0032EA84 File Offset: 0x0032CC84
		public Instance(IStateMachineTarget master, HiveGrowthMonitor.Def def)
			: base(master, def)
		{
		}
	}
}
