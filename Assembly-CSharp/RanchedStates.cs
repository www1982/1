using System;
using STRINGS;

// Token: 0x02000104 RID: 260
public class RanchedStates : GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>
{
	// Token: 0x060004A8 RID: 1192 RVA: 0x00025EDC File Offset: 0x000240DC
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.ranch;
		this.root.Exit("AbandonedRanchStation", delegate(RanchedStates.Instance smi)
		{
			if (smi.Monitor.TargetRanchStation != null)
			{
				if (smi.Monitor.TargetRanchStation.IsCritterInQueue(smi.Monitor))
				{
					Debug.LogWarning("Why are we exiting RanchedStates while in the queue?");
					smi.Monitor.TargetRanchStation.Abandon(smi.Monitor);
				}
				smi.Monitor.TargetRanchStation = null;
			}
			smi.sm.ranchTarget.Set(null, smi);
		});
		this.ranch.EnterTransition(this.ranch.Cheer, (RanchedStates.Instance smi) => RanchedStates.IsCrittersTurn(smi)).EventHandler(GameHashes.RanchStationNoLongerAvailable, delegate(RanchedStates.Instance smi)
		{
			smi.GoTo(null);
		}).BehaviourComplete(GameTags.Creatures.WantsToGetRanched, true)
			.Update(delegate(RanchedStates.Instance smi, float deltaSeconds)
			{
				RanchStation.Instance ranchStation = smi.GetRanchStation();
				if (ranchStation.IsNullOrDestroyed())
				{
					smi.StopSM("No more target ranch station.");
					return;
				}
				Option<CavityInfo> option = Option.Maybe<CavityInfo>(Game.Instance.roomProber.GetCavityForCell(Grid.PosToCell(smi)));
				Option<CavityInfo> cavityInfo = ranchStation.GetCavityInfo();
				if (option.IsNone() || cavityInfo.IsNone())
				{
					smi.StopSM("No longer in any cavity.");
					return;
				}
				if (option.Unwrap() != cavityInfo.Unwrap())
				{
					smi.StopSM("Critter is in a different cavity");
					return;
				}
			}, UpdateRate.SIM_200ms, false)
			.EventHandler(GameHashes.RancherReadyAtRanchStation, delegate(RanchedStates.Instance smi)
			{
				smi.UpdateWaitingState();
			})
			.Exit(new StateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State.Callback(RanchedStates.ClearLayerOverride));
		GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State cheer = this.ranch.Cheer;
		string text = CREATURES.STATUSITEMS.EXCITED_TO_GET_RANCHED.NAME;
		string text2 = CREATURES.STATUSITEMS.EXCITED_TO_GET_RANCHED.TOOLTIP;
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		StatusItemCategory statusItemCategory = Db.Get().StatusItemCategories.Main;
		cheer.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, statusItemCategory).Enter("FaceRancher", delegate(RanchedStates.Instance smi)
		{
			smi.GetComponent<Facing>().Face(smi.GetRanchStation().transform.GetPosition());
		}).PlayAnim("excited_loop")
			.OnAnimQueueComplete(this.ranch.Cheer.Pst)
			.ScheduleGoTo((RanchedStates.Instance smi) => smi.cheerAnimLength, this.ranch.Move);
		this.ranch.Cheer.Pst.ScheduleGoTo(0.2f, this.ranch.Move);
		GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State state = this.ranch.Move.DefaultState(this.ranch.Move.MoveToRanch).Enter("Speedup", delegate(RanchedStates.Instance smi)
		{
			smi.GetComponent<Navigator>().defaultSpeed = smi.OriginalSpeed * 1.25f;
		});
		string text4 = CREATURES.STATUSITEMS.EXCITED_TO_GET_RANCHED.NAME;
		string text5 = CREATURES.STATUSITEMS.EXCITED_TO_GET_RANCHED.TOOLTIP;
		string text6 = "";
		StatusItem.IconType iconType2 = StatusItem.IconType.Info;
		NotificationType notificationType2 = NotificationType.Neutral;
		bool flag2 = false;
		statusItemCategory = Db.Get().StatusItemCategories.Main;
		state.ToggleStatusItem(text4, text5, text6, iconType2, notificationType2, flag2, default(HashedString), 129022, null, null, statusItemCategory).Exit("RestoreSpeed", delegate(RanchedStates.Instance smi)
		{
			smi.GetComponent<Navigator>().defaultSpeed = smi.OriginalSpeed;
		});
		this.ranch.Move.MoveToRanch.EnterTransition(this.ranch.Wait.WaitInLine, GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.Not(new StateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.Transition.ConditionCallback(RanchedStates.IsCrittersTurn))).MoveTo(new Func<RanchedStates.Instance, int>(RanchedStates.GetRanchNavTarget), this.ranch.Wait.WaitInLine, null, false).Target(this.ranchTarget)
			.EventTransition(GameHashes.CreatureArrivedAtRanchStation, this.ranch.Wait.WaitInLine, (RanchedStates.Instance smi) => !RanchedStates.IsCrittersTurn(smi));
		this.ranch.Wait.WaitInLine.EnterTransition(this.ranch.Ranching, new StateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.Transition.ConditionCallback(RanchedStates.IsCrittersTurn)).Enter(delegate(RanchedStates.Instance smi)
		{
			smi.EnterQueue();
		}).EventTransition(GameHashes.DestinationReached, this.ranch.Wait.Waiting, null);
		this.ranch.Wait.Waiting.Face(this.ranchTarget, 0f).PlayAnim((RanchedStates.Instance smi) => smi.def.StartWaitingAnim, KAnim.PlayMode.Once).QueueAnim((RanchedStates.Instance smi) => smi.def.WaitingAnim, true, null);
		this.ranch.Wait.DoneWaiting.PlayAnim((RanchedStates.Instance smi) => smi.def.EndWaitingAnim, KAnim.PlayMode.Once).OnAnimQueueComplete(this.ranch.Move.MoveToRanch);
		this.ranch.Ranching.Enter(new StateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State.Callback(RanchedStates.GetOnTable)).Enter("SetCreatureAtRanchingStation", delegate(RanchedStates.Instance smi)
		{
			smi.GetRanchStation().MessageCreatureArrived(smi);
			smi.AnimController.SetSceneLayer(Grid.SceneLayer.BuildingUse);
		}).EventTransition(GameHashes.RanchingComplete, this.ranch.Wavegoodbye, null)
			.ToggleMainStatusItem(delegate(RanchedStates.Instance smi)
			{
				RanchStation.Instance ranchStation2 = RanchedStates.GetRanchStation(smi);
				if (ranchStation2 != null)
				{
					return ranchStation2.def.CreatureRanchingStatusItem;
				}
				return Db.Get().CreatureStatusItems.GettingRanched;
			}, null);
		GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State state2 = this.ranch.Wavegoodbye.Enter(new StateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State.Callback(RanchedStates.ClearLayerOverride)).OnAnimQueueComplete(this.ranch.Runaway);
		string text7 = CREATURES.STATUSITEMS.EXCITED_TO_BE_RANCHED.NAME;
		string text8 = CREATURES.STATUSITEMS.EXCITED_TO_BE_RANCHED.TOOLTIP;
		string text9 = "";
		StatusItem.IconType iconType3 = StatusItem.IconType.Info;
		NotificationType notificationType3 = NotificationType.Neutral;
		bool flag3 = false;
		statusItemCategory = Db.Get().StatusItemCategories.Main;
		state2.ToggleStatusItem(text7, text8, text9, iconType3, notificationType3, flag3, default(HashedString), 129022, null, null, statusItemCategory);
		GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State state3 = this.ranch.Runaway.MoveTo(new Func<RanchedStates.Instance, int>(RanchedStates.GetRunawayCell), null, null, false);
		string text10 = CREATURES.STATUSITEMS.IDLE.NAME;
		string text11 = CREATURES.STATUSITEMS.IDLE.TOOLTIP;
		string text12 = "";
		StatusItem.IconType iconType4 = StatusItem.IconType.Info;
		NotificationType notificationType4 = NotificationType.Neutral;
		bool flag4 = false;
		statusItemCategory = Db.Get().StatusItemCategories.Main;
		state3.ToggleStatusItem(text10, text11, text12, iconType4, notificationType4, flag4, default(HashedString), 129022, null, null, statusItemCategory);
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x0002649E File Offset: 0x0002469E
	private static void ClearLayerOverride(RanchedStates.Instance smi)
	{
		smi.AnimController.SetSceneLayer(Grid.SceneLayer.Creatures);
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x000264AD File Offset: 0x000246AD
	private static RanchStation.Instance GetRanchStation(RanchedStates.Instance smi)
	{
		return smi.GetRanchStation();
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x000264B8 File Offset: 0x000246B8
	private static void GetOnTable(RanchedStates.Instance smi)
	{
		Navigator navigator = smi.Get<Navigator>();
		if (navigator.IsValidNavType(NavType.Floor))
		{
			navigator.SetCurrentNavType(NavType.Floor);
		}
		smi.Get<Facing>().SetFacing(false);
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x000264E8 File Offset: 0x000246E8
	private static bool IsCrittersTurn(RanchedStates.Instance smi)
	{
		RanchStation.Instance ranchStation = RanchedStates.GetRanchStation(smi);
		return ranchStation != null && ranchStation.IsRancherReady && ranchStation.TryGetRanched(smi);
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x00026514 File Offset: 0x00024714
	private static int GetRanchNavTarget(RanchedStates.Instance smi)
	{
		RanchStation.Instance ranchStation = RanchedStates.GetRanchStation(smi);
		int num = smi.ModifyNavTargetForCritter(ranchStation.GetRanchNavTarget());
		if (smi.HasTag(GameTags.LargeCreature))
		{
			ref Vector2I ptr = Grid.PosToXY(smi.gameObject.transform.position);
			Vector2I vector2I = Grid.CellToXY(num);
			if (ptr.x > vector2I.x)
			{
				num = Grid.CellLeft(num);
			}
		}
		return num;
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00026574 File Offset: 0x00024774
	private static int GetRunawayCell(RanchedStates.Instance smi)
	{
		int num = Grid.PosToCell(smi.transform.GetPosition());
		int num2 = Grid.OffsetCell(num, 2, 0);
		if (Grid.Solid[num2])
		{
			num2 = Grid.OffsetCell(num, -2, 0);
		}
		return num2;
	}

	// Token: 0x0400035F RID: 863
	private RanchedStates.RanchStates ranch;

	// Token: 0x04000360 RID: 864
	private StateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.TargetParameter ranchTarget;

	// Token: 0x0200112F RID: 4399
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006220 RID: 25120
		public string StartWaitingAnim = "queue_pre";

		// Token: 0x04006221 RID: 25121
		public string WaitingAnim = "queue_loop";

		// Token: 0x04006222 RID: 25122
		public string EndWaitingAnim = "queue_pst";

		// Token: 0x04006223 RID: 25123
		public int WaitCellOffset = 1;
	}

	// Token: 0x02001130 RID: 4400
	public new class Instance : GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.GameInstance
	{
		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060081AC RID: 33196 RVA: 0x0032F8A0 File Offset: 0x0032DAA0
		public RanchableMonitor.Instance Monitor
		{
			get
			{
				if (this.ranchMonitor == null)
				{
					this.ranchMonitor = this.GetSMI<RanchableMonitor.Instance>();
				}
				return this.ranchMonitor;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x060081AD RID: 33197 RVA: 0x0032F8BC File Offset: 0x0032DABC
		public KBatchedAnimController AnimController
		{
			get
			{
				return this.animController;
			}
		}

		// Token: 0x060081AE RID: 33198 RVA: 0x0032F8C4 File Offset: 0x0032DAC4
		public Instance(Chore<RanchedStates.Instance> chore, RanchedStates.Def def)
			: base(chore, def)
		{
			this.animController = base.GetComponent<KBatchedAnimController>();
			this.OriginalSpeed = this.Monitor.NavComponent.defaultSpeed;
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToGetRanched);
			KAnim.Anim anim = base.smi.Get<KBatchedAnimController>().AnimFiles[0].GetData().GetAnim("excited_loop");
			this.cheerAnimLength = ((anim != null) ? (anim.totalTime + 0.2f) : 1.2f);
		}

		// Token: 0x060081AF RID: 33199 RVA: 0x0032F953 File Offset: 0x0032DB53
		public RanchStation.Instance GetRanchStation()
		{
			if (this.Monitor != null)
			{
				return this.Monitor.TargetRanchStation;
			}
			return null;
		}

		// Token: 0x060081B0 RID: 33200 RVA: 0x0032F96A File Offset: 0x0032DB6A
		public void EnterQueue()
		{
			if (this.GetRanchStation() != null)
			{
				this.InitializeWaitCell();
				this.Monitor.NavComponent.GoTo(this.waitCell, null);
			}
		}

		// Token: 0x060081B1 RID: 33201 RVA: 0x0032F992 File Offset: 0x0032DB92
		public void AbandonRanchStation()
		{
			if (this.Monitor.TargetRanchStation == null || this.status == StateMachine.Status.Failed)
			{
				return;
			}
			this.StopSM("Abandoned Ranch");
		}

		// Token: 0x060081B2 RID: 33202 RVA: 0x0032F9B8 File Offset: 0x0032DBB8
		public void SetRanchStation(RanchStation.Instance ranch_station)
		{
			if (this.Monitor.TargetRanchStation != null && this.Monitor.TargetRanchStation != ranch_station)
			{
				this.Monitor.TargetRanchStation.Abandon(base.smi.Monitor);
			}
			base.smi.sm.ranchTarget.Set(ranch_station.gameObject, base.smi, false);
			this.Monitor.TargetRanchStation = ranch_station;
		}

		// Token: 0x060081B3 RID: 33203 RVA: 0x0032FA2A File Offset: 0x0032DC2A
		public int ModifyNavTargetForCritter(int navCell)
		{
			if (base.smi.HasTag(GameTags.Creatures.Flyer))
			{
				return Grid.CellAbove(navCell);
			}
			return navCell;
		}

		// Token: 0x060081B4 RID: 33204 RVA: 0x0032FA48 File Offset: 0x0032DC48
		private void InitializeWaitCell()
		{
			if (this.GetRanchStation() == null)
			{
				return;
			}
			int num = 0;
			Extents stationExtents = this.Monitor.TargetRanchStation.StationExtents;
			int num2 = this.ModifyNavTargetForCritter(Grid.XYToCell(stationExtents.x, stationExtents.y));
			int num3 = 0;
			int num4;
			if (Grid.Raycast(num2, new Vector2I(-1, 0), out num4, base.def.WaitCellOffset, ~(Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable)))
			{
				num3 = 1 + base.def.WaitCellOffset - num4;
				num = this.ModifyNavTargetForCritter(Grid.XYToCell(stationExtents.x + 1, stationExtents.y));
			}
			int num5 = 0;
			int num6;
			if (num3 != 0 && Grid.Raycast(num, new Vector2I(1, 0), out num6, base.def.WaitCellOffset, ~(Grid.BuildFlags.DupePassable | Grid.BuildFlags.DupeImpassable)))
			{
				num5 = base.def.WaitCellOffset - num6;
			}
			int num7 = (base.def.WaitCellOffset - num3) * -1;
			if (num3 == base.def.WaitCellOffset)
			{
				num7 = 1 + base.def.WaitCellOffset - num5;
			}
			CellOffset cellOffset = new CellOffset(num7, 0);
			this.waitCell = Grid.OffsetCell(num2, cellOffset);
		}

		// Token: 0x060081B5 RID: 33205 RVA: 0x0032FB58 File Offset: 0x0032DD58
		public void UpdateWaitingState()
		{
			if (!RanchedStates.IsCrittersTurn(base.smi))
			{
				base.smi.GoTo(base.smi.sm.ranch.Wait.WaitInLine);
				return;
			}
			if (base.smi.IsInsideState(base.sm.ranch.Wait.Waiting))
			{
				base.smi.GoTo(base.smi.sm.ranch.Wait.DoneWaiting);
				return;
			}
			base.smi.GoTo(base.smi.sm.ranch.Cheer);
		}

		// Token: 0x04006224 RID: 25124
		public float OriginalSpeed;

		// Token: 0x04006225 RID: 25125
		private int waitCell;

		// Token: 0x04006226 RID: 25126
		private KBatchedAnimController animController;

		// Token: 0x04006227 RID: 25127
		private RanchableMonitor.Instance ranchMonitor;

		// Token: 0x04006228 RID: 25128
		public float cheerAnimLength;
	}

	// Token: 0x02001131 RID: 4401
	public class RanchStates : GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State
	{
		// Token: 0x04006229 RID: 25129
		public RanchedStates.CheerStates Cheer;

		// Token: 0x0400622A RID: 25130
		public RanchedStates.MoveStates Move;

		// Token: 0x0400622B RID: 25131
		public RanchedStates.WaitStates Wait;

		// Token: 0x0400622C RID: 25132
		public GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State Ranching;

		// Token: 0x0400622D RID: 25133
		public GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State Wavegoodbye;

		// Token: 0x0400622E RID: 25134
		public GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State Runaway;
	}

	// Token: 0x02001132 RID: 4402
	public class CheerStates : GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State
	{
		// Token: 0x0400622F RID: 25135
		public GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State Cheer;

		// Token: 0x04006230 RID: 25136
		public GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State Pst;
	}

	// Token: 0x02001133 RID: 4403
	public class MoveStates : GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State
	{
		// Token: 0x04006231 RID: 25137
		public GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State MoveToRanch;
	}

	// Token: 0x02001134 RID: 4404
	public class WaitStates : GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State
	{
		// Token: 0x04006232 RID: 25138
		public GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State WaitInLine;

		// Token: 0x04006233 RID: 25139
		public GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State Waiting;

		// Token: 0x04006234 RID: 25140
		public GameStateMachine<RanchedStates, RanchedStates.Instance, IStateMachineTarget, RanchedStates.Def>.State DoneWaiting;
	}
}
