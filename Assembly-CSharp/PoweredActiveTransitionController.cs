using System;

// Token: 0x02000059 RID: 89
public class PoweredActiveTransitionController : GameStateMachine<PoweredActiveTransitionController, PoweredActiveTransitionController.Instance, IStateMachineTarget, PoweredActiveTransitionController.Def>
{
	// Token: 0x060001AC RID: 428 RVA: 0x0000BE28 File Offset: 0x0000A028
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.off;
		this.off.PlayAnim("off").EventTransition(GameHashes.OperationalChanged, this.on_pre, (PoweredActiveTransitionController.Instance smi) => smi.GetComponent<Operational>().IsOperational);
		this.on_pre.PlayAnim("working_pre").OnAnimQueueComplete(this.on);
		this.on.PlayAnim("on").EventTransition(GameHashes.OperationalChanged, this.on_pst, (PoweredActiveTransitionController.Instance smi) => !smi.GetComponent<Operational>().IsOperational).EventTransition(GameHashes.ActiveChanged, this.working, (PoweredActiveTransitionController.Instance smi) => smi.GetComponent<Operational>().IsActive);
		this.on_pst.PlayAnim("working_pst").OnAnimQueueComplete(this.off);
		this.working.PlayAnim("working_loop", KAnim.PlayMode.Loop).EventTransition(GameHashes.OperationalChanged, this.on_pst, (PoweredActiveTransitionController.Instance smi) => !smi.GetComponent<Operational>().IsOperational).EventTransition(GameHashes.ActiveChanged, this.on, (PoweredActiveTransitionController.Instance smi) => !smi.GetComponent<Operational>().IsActive)
			.Enter(delegate(PoweredActiveTransitionController.Instance smi)
			{
				if (smi.def.showWorkingStatus)
				{
					smi.GetComponent<KSelectable>().AddStatusItem(Db.Get().BuildingStatusItems.Working, null);
				}
			})
			.Exit(delegate(PoweredActiveTransitionController.Instance smi)
			{
				if (smi.def.showWorkingStatus)
				{
					smi.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().BuildingStatusItems.Working, false);
				}
			});
	}

	// Token: 0x0400010D RID: 269
	public GameStateMachine<PoweredActiveTransitionController, PoweredActiveTransitionController.Instance, IStateMachineTarget, PoweredActiveTransitionController.Def>.State off;

	// Token: 0x0400010E RID: 270
	public GameStateMachine<PoweredActiveTransitionController, PoweredActiveTransitionController.Instance, IStateMachineTarget, PoweredActiveTransitionController.Def>.State on;

	// Token: 0x0400010F RID: 271
	public GameStateMachine<PoweredActiveTransitionController, PoweredActiveTransitionController.Instance, IStateMachineTarget, PoweredActiveTransitionController.Def>.State on_pre;

	// Token: 0x04000110 RID: 272
	public GameStateMachine<PoweredActiveTransitionController, PoweredActiveTransitionController.Instance, IStateMachineTarget, PoweredActiveTransitionController.Def>.State on_pst;

	// Token: 0x04000111 RID: 273
	public GameStateMachine<PoweredActiveTransitionController, PoweredActiveTransitionController.Instance, IStateMachineTarget, PoweredActiveTransitionController.Def>.State working;

	// Token: 0x02001048 RID: 4168
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006032 RID: 24626
		public bool showWorkingStatus;
	}

	// Token: 0x02001049 RID: 4169
	public new class Instance : GameStateMachine<PoweredActiveTransitionController, PoweredActiveTransitionController.Instance, IStateMachineTarget, PoweredActiveTransitionController.Def>.GameInstance
	{
		// Token: 0x06007F82 RID: 32642 RVA: 0x0032C1A1 File Offset: 0x0032A3A1
		public Instance(IStateMachineTarget master, PoweredActiveTransitionController.Def def)
			: base(master, def)
		{
		}
	}
}
