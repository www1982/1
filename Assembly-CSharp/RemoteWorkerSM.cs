using System;
using Klei;
using KSerialization;
using UnityEngine;

// Token: 0x02000A9B RID: 2715
public class RemoteWorkerSM : StateMachineComponent<RemoteWorkerSM.StatesInstance>
{
	// Token: 0x1700056D RID: 1389
	// (get) Token: 0x06004EBB RID: 20155 RVA: 0x001C74F0 File Offset: 0x001C56F0
	// (set) Token: 0x06004EBC RID: 20156 RVA: 0x001C74F8 File Offset: 0x001C56F8
	public bool Docked
	{
		get
		{
			return this.docked;
		}
		set
		{
			this.docked = value;
		}
	}

	// Token: 0x06004EBD RID: 20157 RVA: 0x001C7501 File Offset: 0x001C5701
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x06004EBE RID: 20158 RVA: 0x001C7514 File Offset: 0x001C5714
	public void SetNextChore(Chore.Precondition.Context next)
	{
		if (this.nextChore != null)
		{
			this.nextChore.Value.chore.Reserve(null);
		}
		this.nextChore = new Chore.Precondition.Context?(next);
		next.chore.Reserve(this.driver);
	}

	// Token: 0x06004EBF RID: 20159 RVA: 0x001C7561 File Offset: 0x001C5761
	public void StartNextChore()
	{
		if (this.nextChore != null)
		{
			this.driver.SetChore(this.nextChore.Value);
			this.nextChore = null;
		}
	}

	// Token: 0x06004EC0 RID: 20160 RVA: 0x001C7592 File Offset: 0x001C5792
	public bool HasChoreQueued()
	{
		return this.nextChore != null;
	}

	// Token: 0x1700056E RID: 1390
	// (get) Token: 0x06004EC1 RID: 20161 RVA: 0x001C759F File Offset: 0x001C579F
	// (set) Token: 0x06004EC2 RID: 20162 RVA: 0x001C75B2 File Offset: 0x001C57B2
	public RemoteWorkerDock HomeDepot
	{
		get
		{
			Ref<RemoteWorkerDock> @ref = this.homeDepot;
			if (@ref == null)
			{
				return null;
			}
			return @ref.Get();
		}
		set
		{
			this.homeDepot = new Ref<RemoteWorkerDock>(value);
		}
	}

	// Token: 0x1700056F RID: 1391
	// (get) Token: 0x06004EC3 RID: 20163 RVA: 0x001C75C0 File Offset: 0x001C57C0
	public ChoreConsumerState ConsumerState
	{
		get
		{
			return this.consumer.consumerState;
		}
	}

	// Token: 0x17000570 RID: 1392
	// (get) Token: 0x06004EC4 RID: 20164 RVA: 0x001C75CD File Offset: 0x001C57CD
	// (set) Token: 0x06004EC5 RID: 20165 RVA: 0x001C75D5 File Offset: 0x001C57D5
	public bool ActivelyControlled { get; set; }

	// Token: 0x17000571 RID: 1393
	// (get) Token: 0x06004EC6 RID: 20166 RVA: 0x001C75DE File Offset: 0x001C57DE
	// (set) Token: 0x06004EC7 RID: 20167 RVA: 0x001C75E6 File Offset: 0x001C57E6
	public bool ActivelyWorking { get; set; }

	// Token: 0x17000572 RID: 1394
	// (get) Token: 0x06004EC8 RID: 20168 RVA: 0x001C75EF File Offset: 0x001C57EF
	// (set) Token: 0x06004EC9 RID: 20169 RVA: 0x001C75F7 File Offset: 0x001C57F7
	public bool Available { get; set; }

	// Token: 0x17000573 RID: 1395
	// (get) Token: 0x06004ECA RID: 20170 RVA: 0x001C7600 File Offset: 0x001C5800
	public bool RequiresMaintnence
	{
		get
		{
			return this.power.IsLowPower;
		}
	}

	// Token: 0x06004ECB RID: 20171 RVA: 0x001C7610 File Offset: 0x001C5810
	public void TickResources(float dt)
	{
		this.power.ApplyDeltaEnergy(-0.1f * dt);
		float num;
		SimUtil.DiseaseInfo diseaseInfo;
		float num2;
		this.storage.ConsumeAndGetDisease(GameTags.LubricatingOil, 0.033333335f * dt, out num, out diseaseInfo, out num2);
		if (num > 0f)
		{
			this.storage.AddElement(SimHashes.LiquidGunk, num, num2, diseaseInfo.idx, diseaseInfo.count, true, true);
		}
	}

	// Token: 0x06004ECC RID: 20172 RVA: 0x001C7676 File Offset: 0x001C5876
	public GameObject FindStation()
	{
		if (Components.ComplexFabricators.Count == 0)
		{
			return null;
		}
		return Components.ComplexFabricators[0].gameObject;
	}

	// Token: 0x06004ECD RID: 20173 RVA: 0x001C7696 File Offset: 0x001C5896
	public bool HasHomeDepot()
	{
		return !this.HomeDepot.IsNullOrDestroyed();
	}

	// Token: 0x04003440 RID: 13376
	[MyCmpAdd]
	private RemoteWorkerCapacitor power;

	// Token: 0x04003441 RID: 13377
	[MyCmpAdd]
	private RemoteWorkerGunkMonitor gunk;

	// Token: 0x04003442 RID: 13378
	[MyCmpAdd]
	private RemoteWorkerOilMonitor oil;

	// Token: 0x04003443 RID: 13379
	[MyCmpAdd]
	private ChoreDriver driver;

	// Token: 0x04003444 RID: 13380
	[MyCmpGet]
	private ChoreConsumer consumer;

	// Token: 0x04003445 RID: 13381
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04003446 RID: 13382
	public bool playNewWorker;

	// Token: 0x04003447 RID: 13383
	[Serialize]
	private bool docked = true;

	// Token: 0x04003448 RID: 13384
	private Chore.Precondition.Context? nextChore;

	// Token: 0x04003449 RID: 13385
	private const string LostAnim_pre = "sos_pre";

	// Token: 0x0400344A RID: 13386
	private const string LostAnim_loop = "sos_loop";

	// Token: 0x0400344B RID: 13387
	private const string LostAnim_pst = "sos_pst";

	// Token: 0x0400344C RID: 13388
	private const string DeathAnim = "explode";

	// Token: 0x0400344D RID: 13389
	[Serialize]
	private Ref<RemoteWorkerDock> homeDepot;

	// Token: 0x02001B7C RID: 7036
	public class StatesInstance : GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.GameInstance
	{
		// Token: 0x0600A7B4 RID: 42932 RVA: 0x003B1D29 File Offset: 0x003AFF29
		public StatesInstance(RemoteWorkerSM master)
			: base(master)
		{
			base.sm.homedock.Set(base.smi.master.HomeDepot, base.smi);
		}
	}

	// Token: 0x02001B7D RID: 7037
	public class States : GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM>
	{
		// Token: 0x0600A7B5 RID: 42933 RVA: 0x003B1D58 File Offset: 0x003AFF58
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.uncontrolled;
			this.controlled.Enter(delegate(RemoteWorkerSM.StatesInstance smi)
			{
				smi.master.Available = false;
			}).EnterTransition(this.controlled.exit_dock, new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.IsInsideDock)).EnterTransition(this.controlled.working, GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Not(new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.IsInsideDock)))
				.Transition(this.uncontrolled, GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Not(new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.HasRemoteOperator)), UpdateRate.SIM_200ms)
				.Transition(this.incapacitated.lost, GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Not(new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.CanReachDepot)), UpdateRate.SIM_200ms)
				.Transition(this.incapacitated.die, GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Not(new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.HasHomeDepot)), UpdateRate.SIM_200ms)
				.Update(new Action<RemoteWorkerSM.StatesInstance, float>(RemoteWorkerSM.States.TickResources), UpdateRate.SIM_200ms, false);
			this.controlled.exit_dock.ToggleWork<RemoteWorkerDock.ExitableDock>(this.homedock, this.controlled.working, this.controlled.working, (RemoteWorkerSM.StatesInstance _) => true);
			this.controlled.working.Enter(delegate(RemoteWorkerSM.StatesInstance smi)
			{
				smi.master.ActivelyWorking = true;
			}).Exit(delegate(RemoteWorkerSM.StatesInstance smi)
			{
				smi.master.ActivelyWorking = false;
			}).DefaultState(this.controlled.working.find_work);
			this.controlled.working.find_work.Enter(delegate(RemoteWorkerSM.StatesInstance smi)
			{
				if (RemoteWorkerSM.States.HasChore(smi))
				{
					smi.GoTo(this.controlled.working.do_work);
					return;
				}
				RemoteWorkerSM.States.SetNextChore(smi);
				smi.GoTo(RemoteWorkerSM.States.HasChore(smi) ? this.controlled.working.do_work : this.controlled.no_work);
			});
			this.controlled.working.do_work.Exit(new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State.Callback(RemoteWorkerSM.States.ClearChore)).Transition(this.controlled.working.find_work, GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Not(new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.HasChore)), UpdateRate.SIM_200ms);
			this.controlled.no_work.Transition(this.controlled.working.do_work, new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.HasChore), UpdateRate.SIM_200ms).Transition(this.controlled.working.find_work, new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.HasChoreQueued), UpdateRate.SIM_200ms);
			this.uncontrolled.EnterTransition(this.uncontrolled.working.new_worker, new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.IsNewWorker)).EnterTransition(this.uncontrolled.idle, GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.And(new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.IsInsideDock), GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Not(new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.IsNewWorker)))).EnterTransition(this.uncontrolled.approach_dock, GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.And(GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Not(new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.IsInsideDock)), GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Not(new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.IsNewWorker))))
				.Transition(this.controlled.working.find_work, new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.HasRemoteOperator), UpdateRate.SIM_200ms)
				.Transition(this.incapacitated.lost, GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Not(new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.CanReachDepot)), UpdateRate.SIM_200ms)
				.Transition(this.incapacitated.die, GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Not(new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.HasHomeDepot)), UpdateRate.SIM_200ms);
			this.uncontrolled.approach_dock.Enter(delegate(RemoteWorkerSM.StatesInstance smi)
			{
				smi.master.Available = true;
			}).MoveTo<IApproachable>(this.homedock, this.uncontrolled.working.enter, this.incapacitated.lost, null, null);
			this.uncontrolled.working.Enter(delegate(RemoteWorkerSM.StatesInstance smi)
			{
				smi.master.Available = false;
			});
			this.uncontrolled.working.new_worker.Enter(delegate(RemoteWorkerSM.StatesInstance smi)
			{
				smi.master.playNewWorker = false;
			}).ToggleWork<RemoteWorkerDock.NewWorker>(this.homedock, this.uncontrolled.working.recharge, this.uncontrolled.working.recharge, (RemoteWorkerSM.StatesInstance _) => true);
			this.uncontrolled.working.enter.ToggleWork<RemoteWorkerDock.EnterableDock>(this.homedock, this.uncontrolled.working.recharge, this.uncontrolled.idle, (RemoteWorkerSM.StatesInstance _) => true);
			this.uncontrolled.working.recharge.ToggleWork<RemoteWorkerDock.WorkerRecharger>(this.homedock, this.uncontrolled.working.recharge_pst, this.uncontrolled.idle, (RemoteWorkerSM.StatesInstance _) => true);
			this.uncontrolled.working.recharge_pst.OnAnimQueueComplete(this.uncontrolled.working.drain_gunk).ScheduleGoTo(1f, this.uncontrolled.working.drain_gunk);
			this.uncontrolled.working.drain_gunk.ToggleWork<RemoteWorkerDock.WorkerGunkRemover>(this.homedock, this.uncontrolled.working.drain_gunk_pst, this.uncontrolled.idle, (RemoteWorkerSM.StatesInstance _) => true);
			this.uncontrolled.working.drain_gunk_pst.OnAnimQueueComplete(this.uncontrolled.working.fill_oil).ScheduleGoTo(1f, this.uncontrolled.working.fill_oil);
			this.uncontrolled.working.fill_oil.ToggleWork<RemoteWorkerDock.WorkerOilRefiller>(this.homedock, this.uncontrolled.working.fill_oil_pst, this.uncontrolled.idle, (RemoteWorkerSM.StatesInstance _) => true);
			this.uncontrolled.working.fill_oil_pst.OnAnimQueueComplete(this.uncontrolled.idle).ScheduleGoTo(1f, this.uncontrolled.idle);
			this.uncontrolled.idle.Enter(delegate(RemoteWorkerSM.StatesInstance smi)
			{
				smi.master.Available = true;
			}).PlayAnim(RemoteWorkerConfig.IDLE_IN_DOCK_ANIM, KAnim.PlayMode.Loop).Transition(this.uncontrolled.working.recharge, GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.And(new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.RequiresMaintnence), new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.DockIsOperational)), UpdateRate.SIM_1000ms);
			this.incapacitated.lost.Enter(delegate(RemoteWorkerSM.StatesInstance smi)
			{
				smi.Play("sos_pre", KAnim.PlayMode.Once);
				smi.Queue("sos_loop", KAnim.PlayMode.Loop);
				RemoteWorkerSM.States.ClearChore(smi);
			}).ToggleStatusItem(Db.Get().DuplicantStatusItems.UnreachableDock, null).Transition(this.incapacitated.lost_recovery, new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.CanReachDepot), UpdateRate.SIM_200ms)
				.Transition(this.incapacitated.die, GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Not(new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.Transition.ConditionCallback(RemoteWorkerSM.States.HasHomeDepot)), UpdateRate.SIM_200ms);
			this.incapacitated.lost_recovery.PlayAnim("sos_pst").OnAnimQueueComplete(this.controlled);
			this.incapacitated.die.Enter(new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State.Callback(RemoteWorkerSM.States.ClearChore)).PlayAnim("explode").OnAnimQueueComplete(this.incapacitated.explode)
				.ToggleStatusItem(Db.Get().DuplicantStatusItems.NoHomeDock, null);
			this.incapacitated.explode.Enter(new StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State.Callback(RemoteWorkerSM.States.Explode));
		}

		// Token: 0x0600A7B6 RID: 42934 RVA: 0x003B2541 File Offset: 0x003B0741
		public static bool IsNewWorker(RemoteWorkerSM.StatesInstance smi)
		{
			return smi.master.playNewWorker;
		}

		// Token: 0x0600A7B7 RID: 42935 RVA: 0x003B254E File Offset: 0x003B074E
		public static void SetNextChore(RemoteWorkerSM.StatesInstance smi)
		{
			smi.master.StartNextChore();
		}

		// Token: 0x0600A7B8 RID: 42936 RVA: 0x003B255B File Offset: 0x003B075B
		public static void ClearChore(RemoteWorkerSM.StatesInstance smi)
		{
			smi.master.driver.StopChore();
		}

		// Token: 0x0600A7B9 RID: 42937 RVA: 0x003B256D File Offset: 0x003B076D
		public static bool HasChore(RemoteWorkerSM.StatesInstance smi)
		{
			return smi.master.driver.HasChore();
		}

		// Token: 0x0600A7BA RID: 42938 RVA: 0x003B257F File Offset: 0x003B077F
		public static bool HasChoreQueued(RemoteWorkerSM.StatesInstance smi)
		{
			return smi.master.HasChoreQueued();
		}

		// Token: 0x0600A7BB RID: 42939 RVA: 0x003B258C File Offset: 0x003B078C
		public static bool CanReachDepot(RemoteWorkerSM.StatesInstance smi)
		{
			int depotCell = RemoteWorkerSM.States.GetDepotCell(smi);
			return depotCell != Grid.InvalidCell && smi.master.GetComponent<Navigator>().CanReach(depotCell);
		}

		// Token: 0x0600A7BC RID: 42940 RVA: 0x003B25BC File Offset: 0x003B07BC
		public static int GetDepotCell(RemoteWorkerSM.StatesInstance smi)
		{
			RemoteWorkerDock homeDepot = smi.master.HomeDepot;
			if (homeDepot == null)
			{
				return Grid.InvalidCell;
			}
			return Grid.PosToCell(homeDepot);
		}

		// Token: 0x0600A7BD RID: 42941 RVA: 0x003B25EA File Offset: 0x003B07EA
		public static bool HasRemoteOperator(RemoteWorkerSM.StatesInstance smi)
		{
			return smi.master.ActivelyControlled;
		}

		// Token: 0x0600A7BE RID: 42942 RVA: 0x003B25F7 File Offset: 0x003B07F7
		public static bool RequiresMaintnence(RemoteWorkerSM.StatesInstance smi)
		{
			return smi.master.RequiresMaintnence;
		}

		// Token: 0x0600A7BF RID: 42943 RVA: 0x003B2604 File Offset: 0x003B0804
		public static bool DockIsOperational(RemoteWorkerSM.StatesInstance smi)
		{
			return smi.master.HomeDepot != null && smi.master.HomeDepot.IsOperational;
		}

		// Token: 0x0600A7C0 RID: 42944 RVA: 0x003B262B File Offset: 0x003B082B
		public static bool HasHomeDepot(RemoteWorkerSM.StatesInstance smi)
		{
			return RemoteWorkerSM.States.GetDepotCell(smi) != Grid.InvalidCell;
		}

		// Token: 0x0600A7C1 RID: 42945 RVA: 0x003B263D File Offset: 0x003B083D
		public static void StopWork(RemoteWorkerSM.StatesInstance smi)
		{
			if (smi.master.driver.HasChore())
			{
				smi.master.driver.StopChore();
			}
		}

		// Token: 0x0600A7C2 RID: 42946 RVA: 0x003B2661 File Offset: 0x003B0861
		public static bool IsInsideDock(RemoteWorkerSM.StatesInstance smi)
		{
			return smi.master.Docked;
		}

		// Token: 0x0600A7C3 RID: 42947 RVA: 0x003B2670 File Offset: 0x003B0870
		public static void Explode(RemoteWorkerSM.StatesInstance smi)
		{
			Game.Instance.SpawnFX(SpawnFXHashes.MeteorImpactDust, smi.master.transform.position, 0f);
			PrimaryElement component = smi.master.GetComponent<PrimaryElement>();
			component.Element.substance.SpawnResource(Grid.CellToPosCCC(Grid.PosToCell(smi.master.gameObject), Grid.SceneLayer.Ore), 42f, component.Temperature, component.DiseaseIdx, component.DiseaseCount, false, false, false);
			Util.KDestroyGameObject(smi.master.gameObject);
		}

		// Token: 0x0600A7C4 RID: 42948 RVA: 0x003B26FF File Offset: 0x003B08FF
		public static void TickResources(RemoteWorkerSM.StatesInstance smi, float dt)
		{
			if (dt > 0f)
			{
				smi.master.TickResources(dt);
			}
		}

		// Token: 0x04008300 RID: 33536
		public RemoteWorkerSM.States.ControlledStates controlled;

		// Token: 0x04008301 RID: 33537
		public RemoteWorkerSM.States.UncontrolledStates uncontrolled;

		// Token: 0x04008302 RID: 33538
		public RemoteWorkerSM.States.IncapacitatedStates incapacitated;

		// Token: 0x04008303 RID: 33539
		public StateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.TargetParameter homedock;

		// Token: 0x0200289D RID: 10397
		public class ControlledStates : GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State
		{
			// Token: 0x0400B411 RID: 46097
			public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State exit_dock;

			// Token: 0x0400B412 RID: 46098
			public RemoteWorkerSM.States.ControlledStates.WorkingStates working;

			// Token: 0x0400B413 RID: 46099
			public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State no_work;

			// Token: 0x0200388D RID: 14477
			public class WorkingStates : GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State
			{
				// Token: 0x0400E482 RID: 58498
				public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State find_work;

				// Token: 0x0400E483 RID: 58499
				public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State do_work;
			}
		}

		// Token: 0x0200289E RID: 10398
		public class UncontrolledStates : GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State
		{
			// Token: 0x0400B414 RID: 46100
			public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State approach_dock;

			// Token: 0x0400B415 RID: 46101
			public RemoteWorkerSM.States.UncontrolledStates.WorkingDockStates working;

			// Token: 0x0400B416 RID: 46102
			public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State idle;

			// Token: 0x0200388E RID: 14478
			public class WorkingDockStates : GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State
			{
				// Token: 0x0400E484 RID: 58500
				public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State new_worker;

				// Token: 0x0400E485 RID: 58501
				public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State enter;

				// Token: 0x0400E486 RID: 58502
				public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State recharge;

				// Token: 0x0400E487 RID: 58503
				public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State recharge_pst;

				// Token: 0x0400E488 RID: 58504
				public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State drain_gunk;

				// Token: 0x0400E489 RID: 58505
				public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State drain_gunk_pst;

				// Token: 0x0400E48A RID: 58506
				public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State fill_oil;

				// Token: 0x0400E48B RID: 58507
				public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State fill_oil_pst;
			}
		}

		// Token: 0x0200289F RID: 10399
		public class IncapacitatedStates : GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State
		{
			// Token: 0x0400B417 RID: 46103
			public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State lost;

			// Token: 0x0400B418 RID: 46104
			public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State lost_recovery;

			// Token: 0x0400B419 RID: 46105
			public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State die;

			// Token: 0x0400B41A RID: 46106
			public GameStateMachine<RemoteWorkerSM.States, RemoteWorkerSM.StatesInstance, RemoteWorkerSM, object>.State explode;
		}
	}
}
