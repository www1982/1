using System;
using System.Collections.Generic;

// Token: 0x020004AA RID: 1194
public abstract class StandardChoreBase : Chore
{
	// Token: 0x1700008B RID: 139
	// (get) Token: 0x06001904 RID: 6404 RVA: 0x0008AE9D File Offset: 0x0008909D
	// (set) Token: 0x06001905 RID: 6405 RVA: 0x0008AEA5 File Offset: 0x000890A5
	public override int id { get; protected set; }

	// Token: 0x1700008C RID: 140
	// (get) Token: 0x06001906 RID: 6406 RVA: 0x0008AEAE File Offset: 0x000890AE
	// (set) Token: 0x06001907 RID: 6407 RVA: 0x0008AEB6 File Offset: 0x000890B6
	public override int priorityMod { get; protected set; }

	// Token: 0x1700008D RID: 141
	// (get) Token: 0x06001908 RID: 6408 RVA: 0x0008AEBF File Offset: 0x000890BF
	// (set) Token: 0x06001909 RID: 6409 RVA: 0x0008AEC7 File Offset: 0x000890C7
	public override ChoreType choreType { get; protected set; }

	// Token: 0x1700008E RID: 142
	// (get) Token: 0x0600190A RID: 6410 RVA: 0x0008AED0 File Offset: 0x000890D0
	// (set) Token: 0x0600190B RID: 6411 RVA: 0x0008AED8 File Offset: 0x000890D8
	public override ChoreDriver driver { get; protected set; }

	// Token: 0x1700008F RID: 143
	// (get) Token: 0x0600190C RID: 6412 RVA: 0x0008AEE1 File Offset: 0x000890E1
	// (set) Token: 0x0600190D RID: 6413 RVA: 0x0008AEE9 File Offset: 0x000890E9
	public override ChoreDriver lastDriver { get; protected set; }

	// Token: 0x0600190E RID: 6414 RVA: 0x0008AEF2 File Offset: 0x000890F2
	public override bool SatisfiesUrge(Urge urge)
	{
		return urge == this.choreType.urge;
	}

	// Token: 0x0600190F RID: 6415 RVA: 0x0008AF02 File Offset: 0x00089102
	public override bool IsValid()
	{
		return this.provider != null && this.gameObject.GetMyWorldId() != -1;
	}

	// Token: 0x17000090 RID: 144
	// (get) Token: 0x06001910 RID: 6416 RVA: 0x0008AF25 File Offset: 0x00089125
	// (set) Token: 0x06001911 RID: 6417 RVA: 0x0008AF2D File Offset: 0x0008912D
	public override IStateMachineTarget target { get; protected set; }

	// Token: 0x17000091 RID: 145
	// (get) Token: 0x06001912 RID: 6418 RVA: 0x0008AF36 File Offset: 0x00089136
	// (set) Token: 0x06001913 RID: 6419 RVA: 0x0008AF3E File Offset: 0x0008913E
	public override bool isComplete { get; protected set; }

	// Token: 0x17000092 RID: 146
	// (get) Token: 0x06001914 RID: 6420 RVA: 0x0008AF47 File Offset: 0x00089147
	// (set) Token: 0x06001915 RID: 6421 RVA: 0x0008AF4F File Offset: 0x0008914F
	public override bool IsPreemptable { get; protected set; }

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x06001916 RID: 6422 RVA: 0x0008AF58 File Offset: 0x00089158
	// (set) Token: 0x06001917 RID: 6423 RVA: 0x0008AF60 File Offset: 0x00089160
	public override ChoreConsumer overrideTarget { get; protected set; }

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x06001918 RID: 6424 RVA: 0x0008AF69 File Offset: 0x00089169
	// (set) Token: 0x06001919 RID: 6425 RVA: 0x0008AF71 File Offset: 0x00089171
	public override Prioritizable prioritizable { get; protected set; }

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x0600191A RID: 6426 RVA: 0x0008AF7A File Offset: 0x0008917A
	// (set) Token: 0x0600191B RID: 6427 RVA: 0x0008AF82 File Offset: 0x00089182
	public override ChoreProvider provider { get; set; }

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x0600191C RID: 6428 RVA: 0x0008AF8B File Offset: 0x0008918B
	// (set) Token: 0x0600191D RID: 6429 RVA: 0x0008AF93 File Offset: 0x00089193
	public override bool runUntilComplete { get; set; }

	// Token: 0x17000097 RID: 151
	// (get) Token: 0x0600191E RID: 6430 RVA: 0x0008AF9C File Offset: 0x0008919C
	// (set) Token: 0x0600191F RID: 6431 RVA: 0x0008AFA4 File Offset: 0x000891A4
	public override bool isExpanded { get; protected set; }

	// Token: 0x06001920 RID: 6432 RVA: 0x0008AFAD File Offset: 0x000891AD
	public override bool CanPreempt(Chore.Precondition.Context context)
	{
		return this.IsPreemptable;
	}

	// Token: 0x06001921 RID: 6433 RVA: 0x0008AFB5 File Offset: 0x000891B5
	public override void PrepareChore(ref Chore.Precondition.Context context)
	{
	}

	// Token: 0x06001922 RID: 6434 RVA: 0x0008AFB7 File Offset: 0x000891B7
	public override string GetReportName(string context = null)
	{
		if (context == null || this.choreType.reportName == null)
		{
			return this.choreType.Name;
		}
		return string.Format(this.choreType.reportName, context);
	}

	// Token: 0x06001923 RID: 6435 RVA: 0x0008AFE8 File Offset: 0x000891E8
	public override void Cancel(string reason)
	{
		if (!this.RemoveFromProvider())
		{
			return;
		}
		if (this.addToDailyReport)
		{
			ReportManager.Instance.ReportValue(ReportManager.ReportType.ChoreStatus, -1f, this.choreType.Name, GameUtil.GetChoreName(this, null));
			SaveGame.Instance.ColonyAchievementTracker.LogSuitChore((this.driver != null) ? this.driver : this.lastDriver);
		}
		this.End(reason);
		this.Cleanup();
	}

	// Token: 0x06001924 RID: 6436 RVA: 0x0008B060 File Offset: 0x00089260
	public override void Cleanup()
	{
		this.ClearPrioritizable();
	}

	// Token: 0x06001925 RID: 6437 RVA: 0x0008B068 File Offset: 0x00089268
	public override ReportManager.ReportType GetReportType()
	{
		return this.reportType;
	}

	// Token: 0x06001926 RID: 6438 RVA: 0x0008B070 File Offset: 0x00089270
	public override void AddPrecondition(Chore.Precondition precondition, object data = null)
	{
		this.arePreconditionsDirty = true;
		this.preconditions.Add(new Chore.PreconditionInstance
		{
			condition = precondition,
			data = data
		});
	}

	// Token: 0x06001927 RID: 6439 RVA: 0x0008B0A8 File Offset: 0x000892A8
	public override void CollectChores(ChoreConsumerState consumer_state, List<Chore.Precondition.Context> succeeded_contexts, List<Chore.Precondition.Context> incomplete_contexts, List<Chore.Precondition.Context> failed_contexts, bool is_attempting_override)
	{
		Chore.Precondition.Context context = new Chore.Precondition.Context(this, consumer_state, is_attempting_override, null);
		context.RunPreconditions();
		if (!context.IsComplete())
		{
			incomplete_contexts.Add(context);
			return;
		}
		if (context.IsSuccess())
		{
			succeeded_contexts.Add(context);
			return;
		}
		failed_contexts.Add(context);
	}

	// Token: 0x06001928 RID: 6440 RVA: 0x0008B0F2 File Offset: 0x000892F2
	public override void Fail(string reason)
	{
		if (this.provider == null)
		{
			return;
		}
		if (this.driver == null)
		{
			return;
		}
		if (!this.runUntilComplete)
		{
			this.Cancel(reason);
			return;
		}
		this.End(reason);
	}

	// Token: 0x06001929 RID: 6441 RVA: 0x0008B12C File Offset: 0x0008932C
	public override void Reserve(ChoreDriver reserver)
	{
		if (this.driver != null && this.driver != reserver && reserver != null)
		{
			Debug.LogErrorFormat("Chore.Reserve: driver already set {0} {1} {2}, provider {3}, driver {4} -> {5}", new object[]
			{
				this.id,
				base.GetType(),
				this.choreType.Id,
				this.provider,
				this.driver,
				reserver
			});
		}
		this.driver = reserver;
	}

	// Token: 0x0600192A RID: 6442 RVA: 0x0008B1B0 File Offset: 0x000893B0
	public override void Begin(Chore.Precondition.Context context)
	{
		if (this.driver != null && this.driver != context.consumerState.choreDriver)
		{
			Debug.LogErrorFormat("Chore.Begin driver already set {0} {1} {2}, provider {3}, driver {4} -> {5}", new object[]
			{
				this.id,
				base.GetType(),
				this.choreType.Id,
				this.provider,
				this.driver,
				context.consumerState.choreDriver
			});
		}
		if (this.provider == null)
		{
			Debug.LogErrorFormat("Chore.Begin provider is null {0} {1} {2}, provider {3}, driver {4}", new object[]
			{
				this.id,
				base.GetType(),
				this.choreType.Id,
				this.provider,
				this.driver
			});
		}
		this.driver = context.consumerState.choreDriver;
		StateMachine.Instance smi = this.GetSMI();
		smi.OnStop = (Action<string, StateMachine.Status>)Delegate.Combine(smi.OnStop, new Action<string, StateMachine.Status>(this.OnStateMachineStop));
		KSelectable component = this.driver.GetComponent<KSelectable>();
		if (component != null)
		{
			component.SetStatusItem(Db.Get().StatusItemCategories.Main, this.GetStatusItem(), this);
		}
		smi.StartSM();
		if (this.onBegin != null)
		{
			this.onBegin(this);
		}
	}

	// Token: 0x0600192B RID: 6443 RVA: 0x0008B312 File Offset: 0x00089512
	public override bool InProgress()
	{
		return this.driver != null;
	}

	// Token: 0x0600192C RID: 6444
	protected abstract StateMachine.Instance GetSMI();

	// Token: 0x0600192D RID: 6445 RVA: 0x0008B320 File Offset: 0x00089520
	public StandardChoreBase(ChoreType chore_type, IStateMachineTarget target, ChoreProvider chore_provider, bool run_until_complete, Action<Chore> on_complete, Action<Chore> on_begin, Action<Chore> on_end, PriorityScreen.PriorityClass priority_class, int priority_value, bool is_preemptable, bool allow_in_context_menu, int priority_mod, bool add_to_daily_report, ReportManager.ReportType report_type)
	{
		this.target = target;
		if (priority_value == 2147483647)
		{
			priority_class = PriorityScreen.PriorityClass.topPriority;
			priority_value = 2;
		}
		if (priority_value < 1 || priority_value > 9)
		{
			Debug.LogErrorFormat("Priority Value Out Of Range: {0}", new object[] { priority_value });
		}
		this.masterPriority = new PrioritySetting(priority_class, priority_value);
		this.priorityMod = priority_mod;
		this.id = Chore.GetNextChoreID();
		if (chore_provider == null)
		{
			chore_provider = GlobalChoreProvider.Instance;
			DebugUtil.Assert(chore_provider != null);
		}
		this.choreType = chore_type;
		this.runUntilComplete = run_until_complete;
		this.onComplete = on_complete;
		this.onEnd = on_end;
		this.onBegin = on_begin;
		this.IsPreemptable = is_preemptable;
		this.AddPrecondition(ChorePreconditions.instance.IsValid, null);
		this.AddPrecondition(ChorePreconditions.instance.IsPermitted, null);
		this.AddPrecondition(ChorePreconditions.instance.IsPreemptable, null);
		this.AddPrecondition(ChorePreconditions.instance.HasUrge, null);
		this.AddPrecondition(ChorePreconditions.instance.IsMoreSatisfyingEarly, null);
		this.AddPrecondition(ChorePreconditions.instance.IsMoreSatisfyingLate, null);
		this.AddPrecondition(ChorePreconditions.instance.IsOverrideTargetNullOrMe, null);
		chore_provider.AddChore(this);
	}

	// Token: 0x0600192E RID: 6446 RVA: 0x0008B464 File Offset: 0x00089664
	public virtual void SetPriorityMod(int priorityMod)
	{
		this.priorityMod = priorityMod;
	}

	// Token: 0x0600192F RID: 6447 RVA: 0x0008B470 File Offset: 0x00089670
	public override List<Chore.PreconditionInstance> GetPreconditions()
	{
		if (this.arePreconditionsDirty)
		{
			List<Chore.PreconditionInstance> list = this.preconditions;
			lock (list)
			{
				if (this.arePreconditionsDirty)
				{
					this.preconditions.Sort((Chore.PreconditionInstance x, Chore.PreconditionInstance y) => x.condition.sortOrder.CompareTo(y.condition.sortOrder));
					this.arePreconditionsDirty = false;
				}
			}
		}
		return this.preconditions;
	}

	// Token: 0x06001930 RID: 6448 RVA: 0x0008B4F4 File Offset: 0x000896F4
	protected void SetPrioritizable(Prioritizable prioritizable)
	{
		if (prioritizable != null && prioritizable.IsPrioritizable())
		{
			this.prioritizable = prioritizable;
			this.masterPriority = prioritizable.GetMasterPriority();
			prioritizable.onPriorityChanged = (Action<PrioritySetting>)Delegate.Combine(prioritizable.onPriorityChanged, new Action<PrioritySetting>(this.OnMasterPriorityChanged));
		}
	}

	// Token: 0x06001931 RID: 6449 RVA: 0x0008B547 File Offset: 0x00089747
	private void ClearPrioritizable()
	{
		if (this.prioritizable != null)
		{
			Prioritizable prioritizable = this.prioritizable;
			prioritizable.onPriorityChanged = (Action<PrioritySetting>)Delegate.Remove(prioritizable.onPriorityChanged, new Action<PrioritySetting>(this.OnMasterPriorityChanged));
		}
	}

	// Token: 0x06001932 RID: 6450 RVA: 0x0008B57E File Offset: 0x0008977E
	private void OnMasterPriorityChanged(PrioritySetting priority)
	{
		this.masterPriority = priority;
	}

	// Token: 0x06001933 RID: 6451 RVA: 0x0008B587 File Offset: 0x00089787
	public void SetOverrideTarget(ChoreConsumer chore_consumer)
	{
		if (chore_consumer != null)
		{
			string name = chore_consumer.name;
		}
		this.overrideTarget = chore_consumer;
		this.Fail("New override target");
	}

	// Token: 0x06001934 RID: 6452 RVA: 0x0008B5AC File Offset: 0x000897AC
	protected virtual void End(string reason)
	{
		if (this.driver != null)
		{
			KSelectable component = this.driver.GetComponent<KSelectable>();
			if (component != null)
			{
				component.SetStatusItem(Db.Get().StatusItemCategories.Main, null, null);
			}
		}
		StateMachine.Instance smi = this.GetSMI();
		smi.OnStop = (Action<string, StateMachine.Status>)Delegate.Remove(smi.OnStop, new Action<string, StateMachine.Status>(this.OnStateMachineStop));
		smi.StopSM(reason);
		if (this.driver == null)
		{
			return;
		}
		this.lastDriver = this.driver;
		this.driver = null;
		if (this.onEnd != null)
		{
			this.onEnd(this);
		}
		if (this.onExit != null)
		{
			this.onExit(this);
		}
		this.driver = null;
	}

	// Token: 0x06001935 RID: 6453 RVA: 0x0008B674 File Offset: 0x00089874
	protected void Succeed(string reason)
	{
		if (!this.RemoveFromProvider())
		{
			return;
		}
		this.isComplete = true;
		if (this.onComplete != null)
		{
			this.onComplete(this);
		}
		if (this.addToDailyReport)
		{
			ReportManager.Instance.ReportValue(ReportManager.ReportType.ChoreStatus, -1f, this.choreType.Name, GameUtil.GetChoreName(this, null));
			SaveGame.Instance.ColonyAchievementTracker.LogSuitChore((this.driver != null) ? this.driver : this.lastDriver);
		}
		this.End(reason);
		this.Cleanup();
	}

	// Token: 0x06001936 RID: 6454 RVA: 0x0008B707 File Offset: 0x00089907
	protected virtual StatusItem GetStatusItem()
	{
		return this.choreType.statusItem;
	}

	// Token: 0x06001937 RID: 6455 RVA: 0x0008B714 File Offset: 0x00089914
	protected virtual void OnStateMachineStop(string reason, StateMachine.Status status)
	{
		if (status == StateMachine.Status.Success)
		{
			this.Succeed(reason);
			return;
		}
		this.Fail(reason);
	}

	// Token: 0x06001938 RID: 6456 RVA: 0x0008B729 File Offset: 0x00089929
	private bool RemoveFromProvider()
	{
		if (this.provider != null)
		{
			this.provider.RemoveChore(this);
			return true;
		}
		return false;
	}

	// Token: 0x04000E77 RID: 3703
	private Action<Chore> onBegin;

	// Token: 0x04000E78 RID: 3704
	private Action<Chore> onEnd;

	// Token: 0x04000E79 RID: 3705
	public Action<Chore> onCleanup;

	// Token: 0x04000E7A RID: 3706
	private List<Chore.PreconditionInstance> preconditions = new List<Chore.PreconditionInstance>();

	// Token: 0x04000E7B RID: 3707
	private bool arePreconditionsDirty;

	// Token: 0x04000E7C RID: 3708
	public bool addToDailyReport;

	// Token: 0x04000E7D RID: 3709
	public ReportManager.ReportType reportType;
}
