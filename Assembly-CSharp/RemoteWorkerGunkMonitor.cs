using System;

// Token: 0x02000A98 RID: 2712
public class RemoteWorkerGunkMonitor : StateMachineComponent<RemoteWorkerGunkMonitor.StatesInstance>
{
	// Token: 0x06004EAB RID: 20139 RVA: 0x001C731C File Offset: 0x001C551C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x17000568 RID: 1384
	// (get) Token: 0x06004EAC RID: 20140 RVA: 0x001C732F File Offset: 0x001C552F
	public float Gunk
	{
		get
		{
			return this.storage.GetMassAvailable(SimHashes.LiquidGunk);
		}
	}

	// Token: 0x06004EAD RID: 20141 RVA: 0x001C7341 File Offset: 0x001C5541
	public float GunkLevel()
	{
		return this.Gunk / 20.000002f;
	}

	// Token: 0x04003436 RID: 13366
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04003437 RID: 13367
	public const float CAPACITY_KG = 20.000002f;

	// Token: 0x04003438 RID: 13368
	public const float HIGH_LEVEL = 16.000002f;

	// Token: 0x04003439 RID: 13369
	public const float DRAIN_AMOUNT_KG_PER_S = 3.3333337f;

	// Token: 0x02001B78 RID: 7032
	public class StatesInstance : GameStateMachine<RemoteWorkerGunkMonitor.States, RemoteWorkerGunkMonitor.StatesInstance, RemoteWorkerGunkMonitor, object>.GameInstance
	{
		// Token: 0x0600A7A8 RID: 42920 RVA: 0x003B1A67 File Offset: 0x003AFC67
		public StatesInstance(RemoteWorkerGunkMonitor master)
			: base(master)
		{
		}
	}

	// Token: 0x02001B79 RID: 7033
	public class States : GameStateMachine<RemoteWorkerGunkMonitor.States, RemoteWorkerGunkMonitor.StatesInstance, RemoteWorkerGunkMonitor>
	{
		// Token: 0x0600A7A9 RID: 42921 RVA: 0x003B1A70 File Offset: 0x003AFC70
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.InitializeStates(out default_state);
			default_state = this.ok;
			this.ok.Transition(this.full_gunk, new StateMachine<RemoteWorkerGunkMonitor.States, RemoteWorkerGunkMonitor.StatesInstance, RemoteWorkerGunkMonitor, object>.Transition.ConditionCallback(RemoteWorkerGunkMonitor.States.IsFullOfGunk), UpdateRate.SIM_200ms).Transition(this.high_gunk, new StateMachine<RemoteWorkerGunkMonitor.States, RemoteWorkerGunkMonitor.StatesInstance, RemoteWorkerGunkMonitor, object>.Transition.ConditionCallback(RemoteWorkerGunkMonitor.States.IsGunkHigh), UpdateRate.SIM_200ms);
			this.high_gunk.Transition(this.full_gunk, new StateMachine<RemoteWorkerGunkMonitor.States, RemoteWorkerGunkMonitor.StatesInstance, RemoteWorkerGunkMonitor, object>.Transition.ConditionCallback(RemoteWorkerGunkMonitor.States.IsFullOfGunk), UpdateRate.SIM_200ms).Transition(this.ok, new StateMachine<RemoteWorkerGunkMonitor.States, RemoteWorkerGunkMonitor.StatesInstance, RemoteWorkerGunkMonitor, object>.Transition.ConditionCallback(RemoteWorkerGunkMonitor.States.IsGunkLevelOk), UpdateRate.SIM_200ms).ToggleStatusItem(Db.Get().DuplicantStatusItems.RemoteWorkerHighGunkLevel, null);
			this.full_gunk.Transition(this.high_gunk, new StateMachine<RemoteWorkerGunkMonitor.States, RemoteWorkerGunkMonitor.StatesInstance, RemoteWorkerGunkMonitor, object>.Transition.ConditionCallback(RemoteWorkerGunkMonitor.States.IsGunkHigh), UpdateRate.SIM_200ms).Transition(this.ok, new StateMachine<RemoteWorkerGunkMonitor.States, RemoteWorkerGunkMonitor.StatesInstance, RemoteWorkerGunkMonitor, object>.Transition.ConditionCallback(RemoteWorkerGunkMonitor.States.IsGunkLevelOk), UpdateRate.SIM_200ms).ToggleStatusItem(Db.Get().DuplicantStatusItems.RemoteWorkerFullGunkLevel, null);
		}

		// Token: 0x0600A7AA RID: 42922 RVA: 0x003B1B5B File Offset: 0x003AFD5B
		public static bool IsGunkLevelOk(RemoteWorkerGunkMonitor.StatesInstance smi)
		{
			return smi.master.Gunk < 16.000002f;
		}

		// Token: 0x0600A7AB RID: 42923 RVA: 0x003B1B6F File Offset: 0x003AFD6F
		public static bool IsGunkHigh(RemoteWorkerGunkMonitor.StatesInstance smi)
		{
			return smi.master.Gunk >= 16.000002f && smi.master.Gunk < 20.000002f;
		}

		// Token: 0x0600A7AC RID: 42924 RVA: 0x003B1B97 File Offset: 0x003AFD97
		public static bool IsFullOfGunk(RemoteWorkerGunkMonitor.StatesInstance smi)
		{
			return smi.master.Gunk >= 20.000002f;
		}

		// Token: 0x040082FA RID: 33530
		private GameStateMachine<RemoteWorkerGunkMonitor.States, RemoteWorkerGunkMonitor.StatesInstance, RemoteWorkerGunkMonitor, object>.State ok;

		// Token: 0x040082FB RID: 33531
		private GameStateMachine<RemoteWorkerGunkMonitor.States, RemoteWorkerGunkMonitor.StatesInstance, RemoteWorkerGunkMonitor, object>.State high_gunk;

		// Token: 0x040082FC RID: 33532
		private GameStateMachine<RemoteWorkerGunkMonitor.States, RemoteWorkerGunkMonitor.StatesInstance, RemoteWorkerGunkMonitor, object>.State full_gunk;
	}
}
