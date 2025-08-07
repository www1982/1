using System;

// Token: 0x02000881 RID: 2177
public class RocketSelfDestructMonitor : GameStateMachine<RocketSelfDestructMonitor, RocketSelfDestructMonitor.Instance>
{
	// Token: 0x06003BE3 RID: 15331 RVA: 0x0014C0E0 File Offset: 0x0014A2E0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		this.idle.EventTransition(GameHashes.RocketSelfDestructRequested, this.exploding, null);
		this.exploding.Update(delegate(RocketSelfDestructMonitor.Instance smi, float dt)
		{
			if (smi.timeinstate >= 3f)
			{
				smi.master.Trigger(-1311384361, null);
				smi.GoTo(this.idle);
			}
		}, UpdateRate.SIM_200ms, false);
	}

	// Token: 0x040024BB RID: 9403
	public GameStateMachine<RocketSelfDestructMonitor, RocketSelfDestructMonitor.Instance, IStateMachineTarget, object>.State idle;

	// Token: 0x040024BC RID: 9404
	public GameStateMachine<RocketSelfDestructMonitor, RocketSelfDestructMonitor.Instance, IStateMachineTarget, object>.State exploding;

	// Token: 0x0200183D RID: 6205
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200183E RID: 6206
	public new class Instance : GameStateMachine<RocketSelfDestructMonitor, RocketSelfDestructMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06009BF6 RID: 39926 RVA: 0x0038FADE File Offset: 0x0038DCDE
		public Instance(IStateMachineTarget master, RocketSelfDestructMonitor.Def def)
			: base(master)
		{
		}

		// Token: 0x04007855 RID: 30805
		public KBatchedAnimController eyes;
	}
}
