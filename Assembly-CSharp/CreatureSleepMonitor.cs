using System;

// Token: 0x0200085B RID: 2139
public class CreatureSleepMonitor : GameStateMachine<CreatureSleepMonitor, CreatureSleepMonitor.Instance, IStateMachineTarget, CreatureSleepMonitor.Def>
{
	// Token: 0x06003ABE RID: 15038 RVA: 0x0014693E File Offset: 0x00144B3E
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.Behaviours.SleepBehaviour, new StateMachine<CreatureSleepMonitor, CreatureSleepMonitor.Instance, IStateMachineTarget, CreatureSleepMonitor.Def>.Transition.ConditionCallback(CreatureSleepMonitor.ShouldSleep), null);
	}

	// Token: 0x06003ABF RID: 15039 RVA: 0x00146966 File Offset: 0x00144B66
	public static bool ShouldSleep(CreatureSleepMonitor.Instance smi)
	{
		return GameClock.Instance.IsNighttime();
	}

	// Token: 0x020017E5 RID: 6117
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020017E6 RID: 6118
	public new class Instance : GameStateMachine<CreatureSleepMonitor, CreatureSleepMonitor.Instance, IStateMachineTarget, CreatureSleepMonitor.Def>.GameInstance
	{
		// Token: 0x06009ABF RID: 39615 RVA: 0x0038B1FE File Offset: 0x003893FE
		public Instance(IStateMachineTarget master, CreatureSleepMonitor.Def def)
			: base(master, def)
		{
		}
	}
}
