using System;

// Token: 0x02000A9D RID: 2717
internal class RemoteWorkerDockAnimSM : StateMachineComponent<RemoteWorkerDockAnimSM.StatesInstance>
{
	// Token: 0x06004EE5 RID: 20197 RVA: 0x001C7CE3 File Offset: 0x001C5EE3
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x04003464 RID: 13412
	[MyCmpAdd]
	private RemoteWorkerDock dock;

	// Token: 0x04003465 RID: 13413
	[MyCmpGet]
	private Operational operational;

	// Token: 0x02001B85 RID: 7045
	public class StatesInstance : GameStateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.GameInstance
	{
		// Token: 0x0600A7E8 RID: 42984 RVA: 0x003B3054 File Offset: 0x003B1254
		public StatesInstance(RemoteWorkerDockAnimSM master)
			: base(master)
		{
		}
	}

	// Token: 0x02001B86 RID: 7046
	public class States : GameStateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM>
	{
		// Token: 0x0600A7E9 RID: 42985 RVA: 0x003B3060 File Offset: 0x003B1260
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.off;
			this.off.EnterTransition(this.off.full, new StateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.Transition.ConditionCallback(RemoteWorkerDockAnimSM.States.HasWorkerStored)).EnterTransition(this.off.empty, GameStateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.Not(new StateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.Transition.ConditionCallback(RemoteWorkerDockAnimSM.States.HasWorkerStored))).Transition(this.on, new StateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.Transition.ConditionCallback(RemoteWorkerDockAnimSM.States.IsOnline), UpdateRate.SIM_200ms);
			this.off.full.QueueAnim("off_full", false, null).Transition(this.off.empty, GameStateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.Not(new StateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.Transition.ConditionCallback(RemoteWorkerDockAnimSM.States.HasWorkerStored)), UpdateRate.SIM_200ms);
			this.off.empty.QueueAnim("off_empty", false, null).Transition(this.off.full, new StateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.Transition.ConditionCallback(RemoteWorkerDockAnimSM.States.HasWorkerStored), UpdateRate.SIM_200ms);
			this.on.EnterTransition(this.on.full, new StateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.Transition.ConditionCallback(RemoteWorkerDockAnimSM.States.HasWorkerStored)).EnterTransition(this.on.empty, GameStateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.Not(new StateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.Transition.ConditionCallback(RemoteWorkerDockAnimSM.States.HasWorkerStored))).Transition(this.off, GameStateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.Not(new StateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.Transition.ConditionCallback(RemoteWorkerDockAnimSM.States.IsOnline)), UpdateRate.SIM_200ms);
			this.on.full.QueueAnim("on_full", false, null).Transition(this.off.empty, GameStateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.Not(new StateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.Transition.ConditionCallback(RemoteWorkerDockAnimSM.States.HasWorkerStored)), UpdateRate.SIM_200ms);
			this.on.empty.QueueAnim("on_empty", false, null).Transition(this.on.full, new StateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.Transition.ConditionCallback(RemoteWorkerDockAnimSM.States.HasWorkerStored), UpdateRate.SIM_200ms);
		}

		// Token: 0x0600A7EA RID: 42986 RVA: 0x003B3210 File Offset: 0x003B1410
		public static bool IsOnline(RemoteWorkerDockAnimSM.StatesInstance smi)
		{
			return smi.master.operational.IsOperational && smi.master.dock.RemoteWorker != null;
		}

		// Token: 0x0600A7EB RID: 42987 RVA: 0x003B323C File Offset: 0x003B143C
		public static bool HasWorkerStored(RemoteWorkerDockAnimSM.StatesInstance smi)
		{
			return smi.master.dock.RemoteWorker != null && smi.master.dock.RemoteWorker.Docked;
		}

		// Token: 0x04008316 RID: 33558
		public RemoteWorkerDockAnimSM.States.FullOrEmptyState on;

		// Token: 0x04008317 RID: 33559
		public RemoteWorkerDockAnimSM.States.FullOrEmptyState off;

		// Token: 0x020028A1 RID: 10401
		public class FullOrEmptyState : GameStateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.State
		{
			// Token: 0x0400B42A RID: 46122
			public GameStateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.State full;

			// Token: 0x0400B42B RID: 46123
			public GameStateMachine<RemoteWorkerDockAnimSM.States, RemoteWorkerDockAnimSM.StatesInstance, RemoteWorkerDockAnimSM, object>.State empty;
		}
	}
}
