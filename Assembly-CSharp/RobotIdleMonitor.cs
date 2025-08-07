using System;

// Token: 0x02000880 RID: 2176
public class RobotIdleMonitor : GameStateMachine<RobotIdleMonitor, RobotIdleMonitor.Instance>
{
	// Token: 0x06003BE0 RID: 15328 RVA: 0x0014C000 File Offset: 0x0014A200
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		this.idle.PlayAnim("idle_loop", KAnim.PlayMode.Loop).Transition(this.working, (RobotIdleMonitor.Instance smi) => !RobotIdleMonitor.CheckShouldIdle(smi), UpdateRate.SIM_200ms);
		this.working.Transition(this.idle, (RobotIdleMonitor.Instance smi) => RobotIdleMonitor.CheckShouldIdle(smi), UpdateRate.SIM_200ms);
	}

	// Token: 0x06003BE1 RID: 15329 RVA: 0x0014C084 File Offset: 0x0014A284
	private static bool CheckShouldIdle(RobotIdleMonitor.Instance smi)
	{
		FallMonitor.Instance smi2 = smi.master.gameObject.GetSMI<FallMonitor.Instance>();
		return smi2 == null || (!smi.master.gameObject.GetComponent<ChoreConsumer>().choreDriver.HasChore() && smi2.GetCurrentState() == smi2.sm.standing);
	}

	// Token: 0x040024B9 RID: 9401
	public GameStateMachine<RobotIdleMonitor, RobotIdleMonitor.Instance, IStateMachineTarget, object>.State idle;

	// Token: 0x040024BA RID: 9402
	public GameStateMachine<RobotIdleMonitor, RobotIdleMonitor.Instance, IStateMachineTarget, object>.State working;

	// Token: 0x0200183A RID: 6202
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x0200183B RID: 6203
	public new class Instance : GameStateMachine<RobotIdleMonitor, RobotIdleMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06009BF0 RID: 39920 RVA: 0x0038FAA6 File Offset: 0x0038DCA6
		public Instance(IStateMachineTarget master, RobotIdleMonitor.Def def)
			: base(master)
		{
		}

		// Token: 0x04007851 RID: 30801
		public KBatchedAnimController eyes;
	}
}
