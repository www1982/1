using System;
using STRINGS;
using UnityEngine;

// Token: 0x020005A6 RID: 1446
public class StompStates : GameStateMachine<StompStates, StompStates.Instance, IStateMachineTarget, StompStates.Def>
{
	// Token: 0x06002105 RID: 8453 RVA: 0x000BE8F4 File Offset: 0x000BCAF4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.Never;
		default_state = this.approach;
		this.root.Enter(new StateMachine<StompStates, StompStates.Instance, IStateMachineTarget, StompStates.Def>.State.Callback(StompStates.RefreshTarget));
		this.approach.InitializeStates(this.stomper, this.target, (StompStates.Instance smi) => smi.TargetOffsets, this.stomp, this.failed, null).ToggleMainStatusItem(new Func<StompStates.Instance, StatusItem>(StompStates.GetGoingToStompStatusItem), null).OnTargetLost(this.target, this.failed)
			.Target(this.target)
			.EventTransition(GameHashes.Harvest, this.failed, null)
			.EventTransition(GameHashes.Uprooted, this.failed, null)
			.EventTransition(GameHashes.QueueDestroyObject, this.failed, null);
		this.stomp.DefaultState(this.stomp.pre).ToggleMainStatusItem(new Func<StompStates.Instance, StatusItem>(StompStates.GetStompingStatusItem), null);
		this.stomp.pre.Enter(new StateMachine<StompStates, StompStates.Instance, IStateMachineTarget, StompStates.Def>.State.Callback(StompStates.ResetStompLoopTimer)).PlayAnim("stomping_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.stomp.loop);
		this.stomp.loop.ParamTransition<float>(this.stompingLoopTimer, this.stomp.pst, GameStateMachine<StompStates, StompStates.Instance, IStateMachineTarget, StompStates.Def>.IsLTZero).PlayAnim("stomping_loop", KAnim.PlayMode.Loop).Update(new Action<StompStates.Instance, float>(StompStates.StompUpdate), UpdateRate.SIM_200ms, false);
		this.stomp.pst.PlayAnim("stomping_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.complete);
		this.complete.BehaviourComplete(GameTags.Creatures.WantsToStomp, false);
		this.failed.Enter(new StateMachine<StompStates, StompStates.Instance, IStateMachineTarget, StompStates.Def>.State.Callback(StompStates.ReportFailure)).EnterGoTo(null);
	}

	// Token: 0x06002106 RID: 8454 RVA: 0x000BEAC5 File Offset: 0x000BCCC5
	private static StatusItem GetGoingToStompStatusItem(StompStates.Instance smi)
	{
		return StompStates.GetStatusItem(smi, CREATURES.STATUSITEMS.GOING_TO_STOMP.NAME, CREATURES.STATUSITEMS.GOING_TO_STOMP.TOOLTIP);
	}

	// Token: 0x06002107 RID: 8455 RVA: 0x000BEAE1 File Offset: 0x000BCCE1
	private static StatusItem GetStompingStatusItem(StompStates.Instance smi)
	{
		return StompStates.GetStatusItem(smi, CREATURES.STATUSITEMS.STOMPING.NAME, CREATURES.STATUSITEMS.STOMPING.TOOLTIP);
	}

	// Token: 0x06002108 RID: 8456 RVA: 0x000BEB00 File Offset: 0x000BCD00
	private static StatusItem GetStatusItem(StompStates.Instance smi, string name, string tooltip)
	{
		return new StatusItem(smi.GetCurrentState().longName, name, tooltip, "", StatusItem.IconType.Info, NotificationType.Neutral, false, default(HashedString), 129022, true, null);
	}

	// Token: 0x06002109 RID: 8457 RVA: 0x000BEB37 File Offset: 0x000BCD37
	private static void ResetStompLoopTimer(StompStates.Instance smi)
	{
		smi.sm.stompingLoopTimer.Set(0f, smi, false);
	}

	// Token: 0x0600210A RID: 8458 RVA: 0x000BEB54 File Offset: 0x000BCD54
	private static void StompUpdate(StompStates.Instance smi, float dt)
	{
		if (smi.StompLoopTimer <= 1.8333334f)
		{
			smi.sm.stompingLoopTimer.Set(smi.StompLoopTimer + dt, smi, false);
			return;
		}
		if (smi.HarvestAnyOneIntersectingPlant())
		{
			StompStates.ResetStompLoopTimer(smi);
			return;
		}
		smi.sm.stompingLoopTimer.Set(-1f, smi, false);
	}

	// Token: 0x0600210B RID: 8459 RVA: 0x000BEBB4 File Offset: 0x000BCDB4
	private static void RefreshTarget(StompStates.Instance smi)
	{
		StompMonitor.Instance smi2 = smi.GetSMI<StompMonitor.Instance>();
		smi.SetTarget(smi2.Target);
	}

	// Token: 0x0600210C RID: 8460 RVA: 0x000BEBD4 File Offset: 0x000BCDD4
	private static void ReportFailure(StompStates.Instance smi)
	{
		StompMonitor.Instance smi2 = smi.GetSMI<StompMonitor.Instance>();
		if (smi2 != null)
		{
			smi2.sm.StompStateFailed.Trigger(smi2);
		}
	}

	// Token: 0x04001339 RID: 4921
	public const string PRE_STOMP_ANIM_NAME = "stomping_pre";

	// Token: 0x0400133A RID: 4922
	public const string LOOP_STOMP_ANIM_NAME = "stomping_loop";

	// Token: 0x0400133B RID: 4923
	public const string PST_STOMP_ANIM_NAME = "stomping_pst";

	// Token: 0x0400133C RID: 4924
	private const int STOMP_LOOP_ANIM_FRAME_COUNT = 55;

	// Token: 0x0400133D RID: 4925
	private const float STOMP_LOOP_ANIM_DURATION = 1.8333334f;

	// Token: 0x0400133E RID: 4926
	public GameStateMachine<StompStates, StompStates.Instance, IStateMachineTarget, StompStates.Def>.ApproachSubState<IApproachable> approach;

	// Token: 0x0400133F RID: 4927
	public StompStates.StompState stomp;

	// Token: 0x04001340 RID: 4928
	public GameStateMachine<StompStates, StompStates.Instance, IStateMachineTarget, StompStates.Def>.State complete;

	// Token: 0x04001341 RID: 4929
	public GameStateMachine<StompStates, StompStates.Instance, IStateMachineTarget, StompStates.Def>.State failed;

	// Token: 0x04001342 RID: 4930
	public StateMachine<StompStates, StompStates.Instance, IStateMachineTarget, StompStates.Def>.FloatParameter stompingLoopTimer;

	// Token: 0x04001343 RID: 4931
	public StateMachine<StompStates, StompStates.Instance, IStateMachineTarget, StompStates.Def>.TargetParameter stomper;

	// Token: 0x04001344 RID: 4932
	public StateMachine<StompStates, StompStates.Instance, IStateMachineTarget, StompStates.Def>.TargetParameter target;

	// Token: 0x02001436 RID: 5174
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001437 RID: 5175
	public class StompState : GameStateMachine<StompStates, StompStates.Instance, IStateMachineTarget, StompStates.Def>.State
	{
		// Token: 0x04006BE9 RID: 27625
		public GameStateMachine<StompStates, StompStates.Instance, IStateMachineTarget, StompStates.Def>.State pre;

		// Token: 0x04006BEA RID: 27626
		public GameStateMachine<StompStates, StompStates.Instance, IStateMachineTarget, StompStates.Def>.State loop;

		// Token: 0x04006BEB RID: 27627
		public GameStateMachine<StompStates, StompStates.Instance, IStateMachineTarget, StompStates.Def>.State pst;
	}

	// Token: 0x02001438 RID: 5176
	public new class Instance : GameStateMachine<StompStates, StompStates.Instance, IStateMachineTarget, StompStates.Def>.GameInstance
	{
		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06008CDF RID: 36063 RVA: 0x0035733C File Offset: 0x0035553C
		public float StompLoopTimer
		{
			get
			{
				return base.sm.stompingLoopTimer.Get(this);
			}
		}

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06008CE0 RID: 36064 RVA: 0x0035734F File Offset: 0x0035554F
		public GameObject CurrentTarget
		{
			get
			{
				return base.sm.target.Get(this);
			}
		}

		// Token: 0x06008CE1 RID: 36065 RVA: 0x00357364 File Offset: 0x00355564
		public Instance(Chore<StompStates.Instance> chore, StompStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToStomp);
			this.occupyArea = base.GetComponent<OccupyArea>();
			base.sm.stomper.Set(base.smi.gameObject, base.smi, false);
		}

		// Token: 0x06008CE2 RID: 36066 RVA: 0x003573C4 File Offset: 0x003555C4
		public void SetTarget(GameObject target)
		{
			base.smi.sm.target.Set(target, base.smi, false);
			if (this.CurrentTarget == null)
			{
				this.TargetOffsets = new CellOffset[]
				{
					new CellOffset(0, 0)
				};
				return;
			}
			ListPool<CellOffset, StompStates.Instance>.PooledList pooledList = ListPool<CellOffset, StompStates.Instance>.Allocate();
			StompMonitor.Def.GetObjectCellsOffsetsWithExtraBottomPadding(this.CurrentTarget, pooledList);
			this.TargetOffsets = pooledList.ToArray();
			pooledList.Recycle();
		}

		// Token: 0x06008CE3 RID: 36067 RVA: 0x0035743C File Offset: 0x0035563C
		public bool HarvestAnyOneIntersectingPlant()
		{
			int num = Grid.PosToCell(base.gameObject);
			bool flag = false;
			for (int i = 0; i < this.occupyArea.OccupiedCellsOffsets.Length; i++)
			{
				int num2 = Grid.OffsetCell(num, this.occupyArea.OccupiedCellsOffsets[i]);
				if (Grid.IsValidCell(num2))
				{
					GameObject gameObject = Grid.Objects[num2, 5];
					gameObject = ((gameObject != null) ? gameObject : Grid.Objects[num2, 1]);
					if (!(gameObject == null))
					{
						Harvestable component = gameObject.GetComponent<Harvestable>();
						if (!(component == null) && component.CanBeHarvested)
						{
							component.Trigger(2127324410, true);
							component.Harvest();
							flag = true;
							break;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x04006BEC RID: 27628
		public CellOffset[] TargetOffsets;

		// Token: 0x04006BED RID: 27629
		private OccupyArea occupyArea;
	}
}
