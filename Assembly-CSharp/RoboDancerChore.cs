using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x02000499 RID: 1177
public class RoboDancerChore : Chore<RoboDancerChore.StatesInstance>, IWorkerPrioritizable
{
	// Token: 0x0600189D RID: 6301 RVA: 0x00089844 File Offset: 0x00087A44
	public RoboDancerChore(IStateMachineTarget target)
		: base(Db.Get().ChoreTypes.JoyReaction, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.high, 5, false, true, 0, false, ReportManager.ReportType.PersonalTime)
	{
		this.showAvailabilityInHoverText = false;
		base.smi = new RoboDancerChore.StatesInstance(this, target.gameObject);
		this.AddPrecondition(ChorePreconditions.instance.IsNotRedAlert, null);
		this.AddPrecondition(ChorePreconditions.instance.IsScheduledTime, Db.Get().ScheduleBlockTypes.Recreation);
		this.AddPrecondition(ChorePreconditions.instance.CanDoWorkerPrioritizable, this);
	}

	// Token: 0x0600189E RID: 6302 RVA: 0x000898DE File Offset: 0x00087ADE
	public bool GetWorkerPriority(WorkerBase worker, out int priority)
	{
		priority = this.basePriority;
		return true;
	}

	// Token: 0x04000E45 RID: 3653
	private int basePriority = RELAXATION.PRIORITY.TIER1;

	// Token: 0x020012BB RID: 4795
	public class States : GameStateMachine<RoboDancerChore.States, RoboDancerChore.StatesInstance, RoboDancerChore>
	{
		// Token: 0x06008791 RID: 34705 RVA: 0x00344B40 File Offset: 0x00342D40
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.goToStand;
			base.Target(this.roboDancer);
			this.idle.EventTransition(GameHashes.ScheduleBlocksTick, this.goToStand, (RoboDancerChore.StatesInstance smi) => !smi.IsRecTime());
			this.goToStand.MoveTo((RoboDancerChore.StatesInstance smi) => smi.GetTargetCell(), this.dancing, this.idle, false);
			this.dancing.ToggleEffect("Dancing").ToggleAnims("anim_bionic_joy_kanim", 0f).DefaultState(this.dancing.pre)
				.Update(delegate(RoboDancerChore.StatesInstance smi, float dt)
				{
					RoboDancer.Instance smi2 = this.roboDancer.Get(smi).GetSMI<RoboDancer.Instance>();
					RoboDancer sm = smi2.sm;
					sm.hasAudience.Set(smi.HasAudience(), smi2, false);
					sm.timeSpentDancing.Set(sm.timeSpentDancing.Get(smi2) + dt, smi2, false);
				}, UpdateRate.SIM_33ms, false)
				.Exit(delegate(RoboDancerChore.StatesInstance smi)
				{
					smi.ClearAudienceWorkables();
				});
			this.dancing.pre.QueueAnim("robotdance_pre", false, null).OnAnimQueueComplete(this.dancing.variation_1).Enter(delegate(RoboDancerChore.StatesInstance smi)
			{
				smi.ClearAudienceWorkables();
				smi.CreateAudienceWorkables();
			});
			this.dancing.variation_1.QueueAnim("robotdance_loop", false, null).OnAnimQueueComplete(this.dancing.variation_2);
			this.dancing.variation_2.QueueAnim("robotdance_2_loop", false, null).OnAnimQueueComplete(this.dancing.pst);
			this.dancing.pst.QueueAnim("robotdance_pst", false, null).OnAnimQueueComplete(this.dancing.pre);
		}

		// Token: 0x04006748 RID: 26440
		public StateMachine<RoboDancerChore.States, RoboDancerChore.StatesInstance, RoboDancerChore, object>.TargetParameter roboDancer;

		// Token: 0x04006749 RID: 26441
		public GameStateMachine<RoboDancerChore.States, RoboDancerChore.StatesInstance, RoboDancerChore, object>.State idle;

		// Token: 0x0400674A RID: 26442
		public GameStateMachine<RoboDancerChore.States, RoboDancerChore.StatesInstance, RoboDancerChore, object>.State goToStand;

		// Token: 0x0400674B RID: 26443
		public RoboDancerChore.States.DancingStates dancing;

		// Token: 0x0200267C RID: 9852
		public class DancingStates : GameStateMachine<RoboDancerChore.States, RoboDancerChore.StatesInstance, RoboDancerChore, object>.State
		{
			// Token: 0x0400AAFD RID: 43773
			public GameStateMachine<RoboDancerChore.States, RoboDancerChore.StatesInstance, RoboDancerChore, object>.State pre;

			// Token: 0x0400AAFE RID: 43774
			public GameStateMachine<RoboDancerChore.States, RoboDancerChore.StatesInstance, RoboDancerChore, object>.State variation_1;

			// Token: 0x0400AAFF RID: 43775
			public GameStateMachine<RoboDancerChore.States, RoboDancerChore.StatesInstance, RoboDancerChore, object>.State variation_2;

			// Token: 0x0400AB00 RID: 43776
			public GameStateMachine<RoboDancerChore.States, RoboDancerChore.StatesInstance, RoboDancerChore, object>.State pst;
		}
	}

	// Token: 0x020012BC RID: 4796
	public class StatesInstance : GameStateMachine<RoboDancerChore.States, RoboDancerChore.StatesInstance, RoboDancerChore, object>.GameInstance
	{
		// Token: 0x06008794 RID: 34708 RVA: 0x00344D5C File Offset: 0x00342F5C
		public StatesInstance(RoboDancerChore master, GameObject roboDancer)
		{
			Chore.Precondition precondition = default(Chore.Precondition);
			precondition.id = "IsNotRoboHyped";
			precondition.description = "__ Duplicant hasn't watched the dance yet";
			precondition.fn = delegate(ref Chore.Precondition.Context context, object data)
			{
				return !(context.consumerState.consumer == null) && !context.consumerState.gameObject.GetComponent<Effects>().HasEffect(WatchRoboDancerWorkable.TRACKING_EFFECT);
			};
			this.IsNotRoboHyped = precondition;
			base..ctor(master);
			this.roboDancer = roboDancer;
			base.sm.roboDancer.Set(roboDancer, base.smi, false);
		}

		// Token: 0x06008795 RID: 34709 RVA: 0x00344DE9 File Offset: 0x00342FE9
		public bool IsRecTime()
		{
			return base.master.GetComponent<Schedulable>().IsAllowed(Db.Get().ScheduleBlockTypes.Recreation);
		}

		// Token: 0x06008796 RID: 34710 RVA: 0x00344E0C File Offset: 0x0034300C
		public int GetTargetCell()
		{
			Navigator component = base.GetComponent<Navigator>();
			float num = float.MaxValue;
			SocialGatheringPoint socialGatheringPoint = null;
			foreach (SocialGatheringPoint socialGatheringPoint2 in Components.SocialGatheringPoints.GetItems((int)Grid.WorldIdx[Grid.PosToCell(this)]))
			{
				float num2 = (float)component.GetNavigationCost(Grid.PosToCell(socialGatheringPoint2));
				if (num2 != -1f && num2 < num)
				{
					num = num2;
					socialGatheringPoint = socialGatheringPoint2;
				}
			}
			if (socialGatheringPoint != null)
			{
				return Grid.PosToCell(socialGatheringPoint);
			}
			return Grid.PosToCell(base.master.gameObject);
		}

		// Token: 0x06008797 RID: 34711 RVA: 0x00344EBC File Offset: 0x003430BC
		public bool HasAudience()
		{
			if (base.smi.watchWorkables == null)
			{
				return false;
			}
			WatchRoboDancerWorkable[] array = base.smi.watchWorkables;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].worker)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008798 RID: 34712 RVA: 0x00344F04 File Offset: 0x00343104
		public void CreateAudienceWorkables()
		{
			int num = Grid.PosToCell(base.gameObject);
			Vector3Int[] array = new Vector3Int[]
			{
				Vector3Int.left * 3,
				Vector3Int.left * 2,
				Vector3Int.left,
				Vector3Int.right,
				Vector3Int.right * 2,
				Vector3Int.right * 3
			};
			int num2 = 0;
			for (int i = 0; i < this.audienceWorkables.Length; i++)
			{
				int num3 = Grid.OffsetCell(num, array[i].x, array[i].y);
				if (Grid.IsValidCellInWorld(num3, (int)Grid.WorldIdx[num]))
				{
					GameObject gameObject = ChoreHelpers.CreateLocator("WatchRoboDancerWorkable", Grid.CellToPos(num3));
					this.audienceWorkables[i] = gameObject;
					KSelectable kselectable = gameObject.AddOrGet<KSelectable>();
					kselectable.SetName("WatchRoboDancerWorkable");
					kselectable.IsSelectable = false;
					WatchRoboDancerWorkable watchRoboDancerWorkable = gameObject.AddOrGet<WatchRoboDancerWorkable>();
					watchRoboDancerWorkable.owner = this.roboDancer;
					WorkChore<WatchRoboDancerWorkable> workChore = new WorkChore<WatchRoboDancerWorkable>(Db.Get().ChoreTypes.JoyReaction, watchRoboDancerWorkable, null, true, null, null, null, true, Db.Get().ScheduleBlockTypes.Recreation, false, true, null, false, true, true, PriorityScreen.PriorityClass.high, 5, false, true);
					workChore.AddPrecondition(ChorePreconditions.instance.IsNotARobot, null);
					workChore.AddPrecondition(this.IsNotRoboHyped, workChore);
					num2++;
				}
			}
			this.watchWorkables = new WatchRoboDancerWorkable[num2];
			for (int j = 0; j < num2; j++)
			{
				this.watchWorkables[j] = this.audienceWorkables[j].GetComponent<WatchRoboDancerWorkable>();
			}
		}

		// Token: 0x06008799 RID: 34713 RVA: 0x003450AC File Offset: 0x003432AC
		public void ClearAudienceWorkables()
		{
			for (int i = 0; i < this.audienceWorkables.Length; i++)
			{
				if (!(this.audienceWorkables[i] == null))
				{
					WorkerBase worker = this.audienceWorkables[i].GetComponent<WatchRoboDancerWorkable>().worker;
					if (worker != null)
					{
						this.audienceWorkables[i].GetComponent<WatchRoboDancerWorkable>().CompleteWork(worker);
					}
					ChoreHelpers.DestroyLocator(this.audienceWorkables[i]);
				}
			}
			this.watchWorkables = null;
		}

		// Token: 0x0400674C RID: 26444
		private GameObject roboDancer;

		// Token: 0x0400674D RID: 26445
		private GameObject[] audienceWorkables = new GameObject[4];

		// Token: 0x0400674E RID: 26446
		private WatchRoboDancerWorkable[] watchWorkables;

		// Token: 0x0400674F RID: 26447
		private Chore.Precondition IsNotRoboHyped;
	}
}
