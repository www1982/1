using System;

// Token: 0x02000A97 RID: 2711
public class RemoteWorkerOilMonitor : StateMachineComponent<RemoteWorkerOilMonitor.StatesInstance>
{
	// Token: 0x06004EA7 RID: 20135 RVA: 0x001C72E1 File Offset: 0x001C54E1
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x17000567 RID: 1383
	// (get) Token: 0x06004EA8 RID: 20136 RVA: 0x001C72F4 File Offset: 0x001C54F4
	public float Oil
	{
		get
		{
			return this.storage.GetMassAvailable(GameTags.LubricatingOil);
		}
	}

	// Token: 0x06004EA9 RID: 20137 RVA: 0x001C7306 File Offset: 0x001C5506
	public float OilLevel()
	{
		return this.Oil / 20.000002f;
	}

	// Token: 0x04003431 RID: 13361
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04003432 RID: 13362
	public const float CAPACITY_KG = 20.000002f;

	// Token: 0x04003433 RID: 13363
	public const float LOW_LEVEL = 4.0000005f;

	// Token: 0x04003434 RID: 13364
	public const float FILL_RATE_KG_PER_S = 2.5000002f;

	// Token: 0x04003435 RID: 13365
	public const float CONSUMPTION_RATE_KG_PER_S = 0.033333335f;

	// Token: 0x02001B76 RID: 7030
	public class StatesInstance : GameStateMachine<RemoteWorkerOilMonitor.States, RemoteWorkerOilMonitor.StatesInstance, RemoteWorkerOilMonitor, object>.GameInstance
	{
		// Token: 0x0600A7A2 RID: 42914 RVA: 0x003B191B File Offset: 0x003AFB1B
		public StatesInstance(RemoteWorkerOilMonitor master)
			: base(master)
		{
		}
	}

	// Token: 0x02001B77 RID: 7031
	public class States : GameStateMachine<RemoteWorkerOilMonitor.States, RemoteWorkerOilMonitor.StatesInstance, RemoteWorkerOilMonitor>
	{
		// Token: 0x0600A7A3 RID: 42915 RVA: 0x003B1924 File Offset: 0x003AFB24
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			base.InitializeStates(out default_state);
			default_state = this.ok;
			this.ok.Transition(this.out_of_oil, new StateMachine<RemoteWorkerOilMonitor.States, RemoteWorkerOilMonitor.StatesInstance, RemoteWorkerOilMonitor, object>.Transition.ConditionCallback(RemoteWorkerOilMonitor.States.IsOutOfOil), UpdateRate.SIM_200ms).Transition(this.low_oil, new StateMachine<RemoteWorkerOilMonitor.States, RemoteWorkerOilMonitor.StatesInstance, RemoteWorkerOilMonitor, object>.Transition.ConditionCallback(RemoteWorkerOilMonitor.States.IsLowOnOil), UpdateRate.SIM_200ms);
			this.low_oil.Transition(this.out_of_oil, new StateMachine<RemoteWorkerOilMonitor.States, RemoteWorkerOilMonitor.StatesInstance, RemoteWorkerOilMonitor, object>.Transition.ConditionCallback(RemoteWorkerOilMonitor.States.IsOutOfOil), UpdateRate.SIM_200ms).Transition(this.ok, new StateMachine<RemoteWorkerOilMonitor.States, RemoteWorkerOilMonitor.StatesInstance, RemoteWorkerOilMonitor, object>.Transition.ConditionCallback(RemoteWorkerOilMonitor.States.IsOkForOil), UpdateRate.SIM_200ms).ToggleStatusItem(Db.Get().DuplicantStatusItems.RemoteWorkerLowOil, null);
			this.out_of_oil.Transition(this.low_oil, new StateMachine<RemoteWorkerOilMonitor.States, RemoteWorkerOilMonitor.StatesInstance, RemoteWorkerOilMonitor, object>.Transition.ConditionCallback(RemoteWorkerOilMonitor.States.IsLowOnOil), UpdateRate.SIM_200ms).Transition(this.ok, new StateMachine<RemoteWorkerOilMonitor.States, RemoteWorkerOilMonitor.StatesInstance, RemoteWorkerOilMonitor, object>.Transition.ConditionCallback(RemoteWorkerOilMonitor.States.IsOkForOil), UpdateRate.SIM_200ms).ToggleStatusItem(Db.Get().DuplicantStatusItems.RemoteWorkerOutOfOil, null);
		}

		// Token: 0x0600A7A4 RID: 42916 RVA: 0x003B1A0F File Offset: 0x003AFC0F
		public static bool IsOkForOil(RemoteWorkerOilMonitor.StatesInstance smi)
		{
			return smi.master.Oil > 4.0000005f;
		}

		// Token: 0x0600A7A5 RID: 42917 RVA: 0x003B1A23 File Offset: 0x003AFC23
		public static bool IsLowOnOil(RemoteWorkerOilMonitor.StatesInstance smi)
		{
			return smi.master.Oil >= float.Epsilon && smi.master.Oil < 4.0000005f;
		}

		// Token: 0x0600A7A6 RID: 42918 RVA: 0x003B1A4B File Offset: 0x003AFC4B
		public static bool IsOutOfOil(RemoteWorkerOilMonitor.StatesInstance smi)
		{
			return smi.master.Oil < float.Epsilon;
		}

		// Token: 0x040082F7 RID: 33527
		private GameStateMachine<RemoteWorkerOilMonitor.States, RemoteWorkerOilMonitor.StatesInstance, RemoteWorkerOilMonitor, object>.State ok;

		// Token: 0x040082F8 RID: 33528
		private GameStateMachine<RemoteWorkerOilMonitor.States, RemoteWorkerOilMonitor.StatesInstance, RemoteWorkerOilMonitor, object>.State low_oil;

		// Token: 0x040082F9 RID: 33529
		private GameStateMachine<RemoteWorkerOilMonitor.States, RemoteWorkerOilMonitor.StatesInstance, RemoteWorkerOilMonitor, object>.State out_of_oil;
	}
}
