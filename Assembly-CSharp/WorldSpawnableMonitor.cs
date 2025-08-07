using System;

// Token: 0x020005A7 RID: 1447
public class WorldSpawnableMonitor : GameStateMachine<WorldSpawnableMonitor, WorldSpawnableMonitor.Instance, IStateMachineTarget, WorldSpawnableMonitor.Def>
{
	// Token: 0x0600210E RID: 8462 RVA: 0x000BEC04 File Offset: 0x000BCE04
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
	}

	// Token: 0x0200143A RID: 5178
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006BF0 RID: 27632
		public Func<int, int> adjustSpawnLocationCb;
	}

	// Token: 0x0200143B RID: 5179
	public new class Instance : GameStateMachine<WorldSpawnableMonitor, WorldSpawnableMonitor.Instance, IStateMachineTarget, WorldSpawnableMonitor.Def>.GameInstance
	{
		// Token: 0x06008CE8 RID: 36072 RVA: 0x0035752A File Offset: 0x0035572A
		public Instance(IStateMachineTarget master, WorldSpawnableMonitor.Def def)
			: base(master, def)
		{
		}
	}
}
