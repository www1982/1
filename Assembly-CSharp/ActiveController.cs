using System;

// Token: 0x02000053 RID: 83
public class ActiveController : GameStateMachine<ActiveController, ActiveController.Instance>
{
	// Token: 0x0600019E RID: 414 RVA: 0x0000B6C4 File Offset: 0x000098C4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.off.PlayAnim("off").EventTransition(GameHashes.ActiveChanged, this.working_pre, (ActiveController.Instance smi) => smi.GetComponent<Operational>().IsActive);
		this.working_pre.PlayAnim("working_pre").OnAnimQueueComplete(this.working_loop);
		this.working_loop.PlayAnim("working_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.ActiveChanged, this.working_pst, (ActiveController.Instance smi) => !smi.GetComponent<Operational>().IsActive);
		this.working_pst.PlayAnim("working_pst").OnAnimQueueComplete(this.off);
	}

	// Token: 0x040000F8 RID: 248
	public GameStateMachine<ActiveController, ActiveController.Instance, IStateMachineTarget, object>.State off;

	// Token: 0x040000F9 RID: 249
	public GameStateMachine<ActiveController, ActiveController.Instance, IStateMachineTarget, object>.State working_pre;

	// Token: 0x040000FA RID: 250
	public GameStateMachine<ActiveController, ActiveController.Instance, IStateMachineTarget, object>.State working_loop;

	// Token: 0x040000FB RID: 251
	public GameStateMachine<ActiveController, ActiveController.Instance, IStateMachineTarget, object>.State working_pst;

	// Token: 0x02001034 RID: 4148
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001035 RID: 4149
	public new class Instance : GameStateMachine<ActiveController, ActiveController.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x06007F50 RID: 32592 RVA: 0x0032BE9D File Offset: 0x0032A09D
		public Instance(IStateMachineTarget master, ActiveController.Def def)
			: base(master, def)
		{
		}
	}
}
