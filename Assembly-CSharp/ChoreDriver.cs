using System;
using STRINGS;

// Token: 0x020004AF RID: 1199
public class ChoreDriver : StateMachineComponent<ChoreDriver.StatesInstance>
{
	// Token: 0x06001981 RID: 6529 RVA: 0x0008CA75 File Offset: 0x0008AC75
	public Chore GetCurrentChore()
	{
		return base.smi.GetCurrentChore();
	}

	// Token: 0x06001982 RID: 6530 RVA: 0x0008CA82 File Offset: 0x0008AC82
	public bool HasChore()
	{
		return base.smi.GetCurrentChore() != null;
	}

	// Token: 0x06001983 RID: 6531 RVA: 0x0008CA92 File Offset: 0x0008AC92
	public void StopChore()
	{
		base.smi.sm.stop.Trigger(base.smi);
	}

	// Token: 0x06001984 RID: 6532 RVA: 0x0008CAB0 File Offset: 0x0008ACB0
	public void SetChore(Chore.Precondition.Context context)
	{
		Chore currentChore = base.smi.GetCurrentChore();
		if (currentChore != context.chore)
		{
			this.StopChore();
			if (context.chore.IsValid())
			{
				context.chore.PrepareChore(ref context);
				this.context = context;
				base.smi.sm.nextChore.Set(context.chore, base.smi, false);
				return;
			}
			string text = "Null";
			string text2 = "Null";
			if (currentChore != null)
			{
				text = currentChore.GetType().Name;
			}
			if (context.chore != null)
			{
				text2 = context.chore.GetType().Name;
			}
			Debug.LogWarning(string.Concat(new string[] { "Stopping chore ", text, " to start ", text2, " but stopping the first chore cancelled the second one." }));
		}
	}

	// Token: 0x06001985 RID: 6533 RVA: 0x0008CB84 File Offset: 0x0008AD84
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.smi.StartSM();
	}

	// Token: 0x04000EB0 RID: 3760
	[MyCmpAdd]
	private User user;

	// Token: 0x04000EB1 RID: 3761
	private Chore.Precondition.Context context;

	// Token: 0x020012E5 RID: 4837
	public class StatesInstance : GameStateMachine<ChoreDriver.States, ChoreDriver.StatesInstance, ChoreDriver, object>.GameInstance
	{
		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06008810 RID: 34832 RVA: 0x00348673 File Offset: 0x00346873
		// (set) Token: 0x06008811 RID: 34833 RVA: 0x0034867B File Offset: 0x0034687B
		public string masterProperName { get; private set; }

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06008812 RID: 34834 RVA: 0x00348684 File Offset: 0x00346884
		// (set) Token: 0x06008813 RID: 34835 RVA: 0x0034868C File Offset: 0x0034688C
		public KPrefabID masterPrefabId { get; private set; }

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06008814 RID: 34836 RVA: 0x00348695 File Offset: 0x00346895
		// (set) Token: 0x06008815 RID: 34837 RVA: 0x0034869D File Offset: 0x0034689D
		public Navigator navigator { get; private set; }

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06008816 RID: 34838 RVA: 0x003486A6 File Offset: 0x003468A6
		// (set) Token: 0x06008817 RID: 34839 RVA: 0x003486AE File Offset: 0x003468AE
		public WorkerBase worker { get; private set; }

		// Token: 0x06008818 RID: 34840 RVA: 0x003486B8 File Offset: 0x003468B8
		public StatesInstance(ChoreDriver master)
			: base(master)
		{
			this.masterProperName = base.master.GetProperName();
			this.masterPrefabId = base.master.GetComponent<KPrefabID>();
			this.navigator = base.master.GetComponent<Navigator>();
			this.worker = base.master.GetComponent<WorkerBase>();
			this.choreConsumer = base.GetComponent<ChoreConsumer>();
			ChoreConsumer choreConsumer = this.choreConsumer;
			choreConsumer.choreRulesChanged = (global::System.Action)Delegate.Combine(choreConsumer.choreRulesChanged, new global::System.Action(this.OnChoreRulesChanged));
		}

		// Token: 0x06008819 RID: 34841 RVA: 0x00348744 File Offset: 0x00346944
		public void BeginChore()
		{
			Chore nextChore = this.GetNextChore();
			Chore chore = base.smi.sm.currentChore.Set(nextChore, base.smi, false);
			if (chore != null && chore.IsPreemptable && chore.driver != null)
			{
				chore.Fail("Preemption!");
			}
			base.smi.sm.nextChore.Set(null, base.smi, false);
			Chore chore2 = chore;
			chore2.onExit = (Action<Chore>)Delegate.Combine(chore2.onExit, new Action<Chore>(this.OnChoreExit));
			chore.Begin(base.master.context);
			base.Trigger(-1988963660, chore);
		}

		// Token: 0x0600881A RID: 34842 RVA: 0x003487F8 File Offset: 0x003469F8
		public void EndChore(string reason)
		{
			if (this.GetCurrentChore() != null)
			{
				Chore currentChore = this.GetCurrentChore();
				base.smi.sm.currentChore.Set(null, base.smi, false);
				Chore chore = currentChore;
				chore.onExit = (Action<Chore>)Delegate.Remove(chore.onExit, new Action<Chore>(this.OnChoreExit));
				currentChore.Fail(reason);
				base.Trigger(1745615042, currentChore);
			}
			if (base.smi.choreConsumer.prioritizeBrainIfNoChore)
			{
				Game.BrainScheduler.PrioritizeBrain(this.brain);
			}
		}

		// Token: 0x0600881B RID: 34843 RVA: 0x00348889 File Offset: 0x00346A89
		private void OnChoreExit(Chore chore)
		{
			base.smi.sm.stop.Trigger(base.smi);
		}

		// Token: 0x0600881C RID: 34844 RVA: 0x003488A6 File Offset: 0x00346AA6
		public Chore GetNextChore()
		{
			return base.smi.sm.nextChore.Get(base.smi);
		}

		// Token: 0x0600881D RID: 34845 RVA: 0x003488C3 File Offset: 0x00346AC3
		public Chore GetCurrentChore()
		{
			return base.smi.sm.currentChore.Get(base.smi);
		}

		// Token: 0x0600881E RID: 34846 RVA: 0x003488E0 File Offset: 0x00346AE0
		private void OnChoreRulesChanged()
		{
			Chore currentChore = this.GetCurrentChore();
			if (currentChore != null && !this.choreConsumer.IsPermittedOrEnabled(currentChore.choreType, currentChore))
			{
				this.EndChore("Permissions changed");
			}
		}

		// Token: 0x040067D9 RID: 26585
		private ChoreConsumer choreConsumer;

		// Token: 0x040067DA RID: 26586
		[MyCmpGet]
		private Brain brain;
	}

	// Token: 0x020012E6 RID: 4838
	public class States : GameStateMachine<ChoreDriver.States, ChoreDriver.StatesInstance, ChoreDriver>
	{
		// Token: 0x0600881F RID: 34847 RVA: 0x00348918 File Offset: 0x00346B18
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.nochore;
			this.saveHistory = true;
			this.nochore.Update(delegate(ChoreDriver.StatesInstance smi, float dt)
			{
				if (smi.masterPrefabId.HasTag(GameTags.BaseMinion) && !smi.masterPrefabId.HasTag(GameTags.Dead))
				{
					ReportManager.Instance.ReportValue(ReportManager.ReportType.WorkTime, dt, string.Format(UI.ENDOFDAYREPORT.NOTES.TIME_SPENT, DUPLICANTS.CHORES.THINKING.NAME), smi.master.GetProperName());
				}
			}, UpdateRate.SIM_200ms, false).ParamTransition<Chore>(this.nextChore, this.haschore, (ChoreDriver.StatesInstance smi, Chore next_chore) => next_chore != null);
			this.haschore.Enter("BeginChore", delegate(ChoreDriver.StatesInstance smi)
			{
				smi.BeginChore();
			}).Update(delegate(ChoreDriver.StatesInstance smi, float dt)
			{
				if (smi.masterPrefabId.HasTag(GameTags.BaseMinion) && !smi.masterPrefabId.HasTag(GameTags.Dead))
				{
					Chore chore = this.currentChore.Get(smi);
					if (chore == null)
					{
						return;
					}
					if (smi.navigator.IsMoving())
					{
						ReportManager.Instance.ReportValue(ReportManager.ReportType.TravelTime, dt, GameUtil.GetChoreName(chore, null), smi.master.GetProperName());
						return;
					}
					ReportManager.ReportType reportType = chore.GetReportType();
					Workable workable = smi.worker.GetWorkable();
					if (workable != null)
					{
						ReportManager.ReportType reportType2 = workable.GetReportType();
						if (reportType != reportType2)
						{
							reportType = reportType2;
						}
					}
					ReportManager.Instance.ReportValue(reportType, dt, string.Format(UI.ENDOFDAYREPORT.NOTES.WORK_TIME, GameUtil.GetChoreName(chore, null)), smi.master.GetProperName());
				}
			}, UpdateRate.SIM_200ms, false).Exit("EndChore", delegate(ChoreDriver.StatesInstance smi)
			{
				smi.EndChore("ChoreDriver.SignalStop");
			})
				.OnSignal(this.stop, this.nochore);
		}

		// Token: 0x040067DB RID: 26587
		public StateMachine<ChoreDriver.States, ChoreDriver.StatesInstance, ChoreDriver, object>.ObjectParameter<Chore> currentChore;

		// Token: 0x040067DC RID: 26588
		public StateMachine<ChoreDriver.States, ChoreDriver.StatesInstance, ChoreDriver, object>.ObjectParameter<Chore> nextChore;

		// Token: 0x040067DD RID: 26589
		public StateMachine<ChoreDriver.States, ChoreDriver.StatesInstance, ChoreDriver, object>.Signal stop;

		// Token: 0x040067DE RID: 26590
		public GameStateMachine<ChoreDriver.States, ChoreDriver.StatesInstance, ChoreDriver, object>.State nochore;

		// Token: 0x040067DF RID: 26591
		public GameStateMachine<ChoreDriver.States, ChoreDriver.StatesInstance, ChoreDriver, object>.State haschore;
	}
}
