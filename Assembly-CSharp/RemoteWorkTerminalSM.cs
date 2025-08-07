using System;

// Token: 0x02000A91 RID: 2705
public class RemoteWorkTerminalSM : StateMachineComponent<RemoteWorkTerminalSM.StatesInstance>
{
	// Token: 0x06004E94 RID: 20116 RVA: 0x001C720B File Offset: 0x001C540B
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x0400342E RID: 13358
	[MyCmpGet]
	private RemoteWorkTerminal terminal;

	// Token: 0x0400342F RID: 13359
	[MyCmpGet]
	private Operational operational;

	// Token: 0x02001B74 RID: 7028
	public class StatesInstance : GameStateMachine<RemoteWorkTerminalSM.States, RemoteWorkTerminalSM.StatesInstance, RemoteWorkTerminalSM, object>.GameInstance
	{
		// Token: 0x0600A79C RID: 42908 RVA: 0x003B17E6 File Offset: 0x003AF9E6
		public StatesInstance(RemoteWorkTerminalSM master)
			: base(master)
		{
		}
	}

	// Token: 0x02001B75 RID: 7029
	public class States : GameStateMachine<RemoteWorkTerminalSM.States, RemoteWorkTerminalSM.StatesInstance, RemoteWorkTerminalSM>
	{
		// Token: 0x0600A79D RID: 42909 RVA: 0x003B17F0 File Offset: 0x003AF9F0
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.offline;
			this.offline.Transition(this.online, GameStateMachine<RemoteWorkTerminalSM.States, RemoteWorkTerminalSM.StatesInstance, RemoteWorkTerminalSM, object>.And(new StateMachine<RemoteWorkTerminalSM.States, RemoteWorkTerminalSM.StatesInstance, RemoteWorkTerminalSM, object>.Transition.ConditionCallback(RemoteWorkTerminalSM.States.HasAssignedDock), new StateMachine<RemoteWorkTerminalSM.States, RemoteWorkTerminalSM.StatesInstance, RemoteWorkTerminalSM, object>.Transition.ConditionCallback(RemoteWorkTerminalSM.States.IsOperational)), UpdateRate.SIM_200ms).Transition(this.offline.no_dock, GameStateMachine<RemoteWorkTerminalSM.States, RemoteWorkTerminalSM.StatesInstance, RemoteWorkTerminalSM, object>.Not(new StateMachine<RemoteWorkTerminalSM.States, RemoteWorkTerminalSM.StatesInstance, RemoteWorkTerminalSM, object>.Transition.ConditionCallback(RemoteWorkTerminalSM.States.HasAssignedDock)), UpdateRate.SIM_200ms);
			this.offline.no_dock.Transition(this.offline, new StateMachine<RemoteWorkTerminalSM.States, RemoteWorkTerminalSM.StatesInstance, RemoteWorkTerminalSM, object>.Transition.ConditionCallback(RemoteWorkTerminalSM.States.HasAssignedDock), UpdateRate.SIM_200ms).ToggleStatusItem(Db.Get().BuildingStatusItems.RemoteWorkTerminalNoDock, null);
			this.online.ToggleRecurringChore(new Func<RemoteWorkTerminalSM.StatesInstance, Chore>(RemoteWorkTerminalSM.States.CreateChore), null).Transition(this.offline, GameStateMachine<RemoteWorkTerminalSM.States, RemoteWorkTerminalSM.StatesInstance, RemoteWorkTerminalSM, object>.Not(GameStateMachine<RemoteWorkTerminalSM.States, RemoteWorkTerminalSM.StatesInstance, RemoteWorkTerminalSM, object>.And(new StateMachine<RemoteWorkTerminalSM.States, RemoteWorkTerminalSM.StatesInstance, RemoteWorkTerminalSM, object>.Transition.ConditionCallback(RemoteWorkTerminalSM.States.HasAssignedDock), new StateMachine<RemoteWorkTerminalSM.States, RemoteWorkTerminalSM.StatesInstance, RemoteWorkTerminalSM, object>.Transition.ConditionCallback(RemoteWorkTerminalSM.States.IsOperational))), UpdateRate.SIM_200ms);
		}

		// Token: 0x0600A79E RID: 42910 RVA: 0x003B18D7 File Offset: 0x003AFAD7
		public static bool IsOperational(RemoteWorkTerminalSM.StatesInstance smi)
		{
			return smi.master.operational.IsOperational;
		}

		// Token: 0x0600A79F RID: 42911 RVA: 0x003B18E9 File Offset: 0x003AFAE9
		public static bool HasAssignedDock(RemoteWorkTerminalSM.StatesInstance smi)
		{
			return smi.master.terminal.CurrentDock != null;
		}

		// Token: 0x0600A7A0 RID: 42912 RVA: 0x003B1901 File Offset: 0x003AFB01
		public static Chore CreateChore(RemoteWorkTerminalSM.StatesInstance smi)
		{
			return new RemoteChore(smi.master.terminal);
		}

		// Token: 0x040082F5 RID: 33525
		public GameStateMachine<RemoteWorkTerminalSM.States, RemoteWorkTerminalSM.StatesInstance, RemoteWorkTerminalSM, object>.State online;

		// Token: 0x040082F6 RID: 33526
		public RemoteWorkTerminalSM.States.OfflineStates offline;

		// Token: 0x0200289B RID: 10395
		public class OfflineStates : GameStateMachine<RemoteWorkTerminalSM.States, RemoteWorkTerminalSM.StatesInstance, RemoteWorkTerminalSM, object>.State
		{
			// Token: 0x0400B40E RID: 46094
			public GameStateMachine<RemoteWorkTerminalSM.States, RemoteWorkTerminalSM.StatesInstance, RemoteWorkTerminalSM, object>.State no_dock;
		}
	}
}
