using System;
using STRINGS;

// Token: 0x020000EC RID: 236
public class FixedCaptureStates : GameStateMachine<FixedCaptureStates, FixedCaptureStates.Instance, IStateMachineTarget, FixedCaptureStates.Def>
{
	// Token: 0x06000449 RID: 1097 RVA: 0x000238E0 File Offset: 0x00021AE0
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.capture;
		this.root.Exit("AbandonedCapturePoint", delegate(FixedCaptureStates.Instance smi)
		{
			smi.AbandonedCapturePoint();
		});
		this.capture.EventTransition(GameHashes.CapturePointNoLongerAvailable, null, null).DefaultState(this.capture.cheer);
		GameStateMachine<FixedCaptureStates, FixedCaptureStates.Instance, IStateMachineTarget, FixedCaptureStates.Def>.State state = this.capture.cheer.DefaultState(this.capture.cheer.pre);
		string text = CREATURES.STATUSITEMS.EXCITED_TO_BE_RANCHED.NAME;
		string text2 = CREATURES.STATUSITEMS.EXCITED_TO_BE_RANCHED.TOOLTIP;
		string text3 = "";
		StatusItem.IconType iconType = StatusItem.IconType.Info;
		NotificationType notificationType = NotificationType.Neutral;
		bool flag = false;
		StatusItemCategory statusItemCategory = Db.Get().StatusItemCategories.Main;
		state.ToggleStatusItem(text, text2, text3, iconType, notificationType, flag, default(HashedString), 129022, null, null, statusItemCategory);
		this.capture.cheer.pre.ScheduleGoTo(0.9f, this.capture.cheer.cheer);
		this.capture.cheer.cheer.Enter("FaceRancher", delegate(FixedCaptureStates.Instance smi)
		{
			smi.GetComponent<Facing>().Face(smi.GetCapturePoint().transform.GetPosition());
		}).PlayAnim("excited_loop").OnAnimQueueComplete(this.capture.cheer.pst);
		this.capture.cheer.pst.ScheduleGoTo(0.2f, this.capture.move);
		GameStateMachine<FixedCaptureStates, FixedCaptureStates.Instance, IStateMachineTarget, FixedCaptureStates.Def>.State state2 = this.capture.move.DefaultState(this.capture.move.movetoranch);
		string text4 = CREATURES.STATUSITEMS.GETTING_WRANGLED.NAME;
		string text5 = CREATURES.STATUSITEMS.GETTING_WRANGLED.TOOLTIP;
		string text6 = "";
		StatusItem.IconType iconType2 = StatusItem.IconType.Info;
		NotificationType notificationType2 = NotificationType.Neutral;
		bool flag2 = false;
		statusItemCategory = Db.Get().StatusItemCategories.Main;
		state2.ToggleStatusItem(text4, text5, text6, iconType2, notificationType2, flag2, default(HashedString), 129022, null, null, statusItemCategory);
		this.capture.move.movetoranch.Enter("Speedup", delegate(FixedCaptureStates.Instance smi)
		{
			smi.GetComponent<Navigator>().defaultSpeed = smi.originalSpeed * 1.25f;
		}).MoveTo(new Func<FixedCaptureStates.Instance, int>(FixedCaptureStates.GetTargetCaptureCell), this.capture.move.waitforranchertobeready, null, false).Exit("RestoreSpeed", delegate(FixedCaptureStates.Instance smi)
		{
			smi.GetComponent<Navigator>().defaultSpeed = smi.originalSpeed;
		});
		this.capture.move.waitforranchertobeready.Enter("SetCreatureAtRanchingStation", delegate(FixedCaptureStates.Instance smi)
		{
			smi.GetCapturePoint().Trigger(-1992722293, null);
		}).EventTransition(GameHashes.RancherReadyAtCapturePoint, this.capture.ranching, null);
		GameStateMachine<FixedCaptureStates, FixedCaptureStates.Instance, IStateMachineTarget, FixedCaptureStates.Def>.State ranching = this.capture.ranching;
		string text7 = CREATURES.STATUSITEMS.GETTING_WRANGLED.NAME;
		string text8 = CREATURES.STATUSITEMS.GETTING_WRANGLED.TOOLTIP;
		string text9 = "";
		StatusItem.IconType iconType3 = StatusItem.IconType.Info;
		NotificationType notificationType3 = NotificationType.Neutral;
		bool flag3 = false;
		statusItemCategory = Db.Get().StatusItemCategories.Main;
		ranching.ToggleStatusItem(text7, text8, text9, iconType3, notificationType3, flag3, default(HashedString), 129022, null, null, statusItemCategory);
		this.behaviourcomplete.BehaviourComplete(GameTags.Creatures.WantsToGetCaptured, false);
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x00023BEE File Offset: 0x00021DEE
	private static FixedCapturePoint.Instance GetCapturePoint(FixedCaptureStates.Instance smi)
	{
		return smi.GetSMI<FixedCapturableMonitor.Instance>().targetCapturePoint;
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x00023BFC File Offset: 0x00021DFC
	private static int GetTargetCaptureCell(FixedCaptureStates.Instance smi)
	{
		FixedCapturePoint.Instance capturePoint = FixedCaptureStates.GetCapturePoint(smi);
		return capturePoint.def.getTargetCapturePoint(capturePoint);
	}

	// Token: 0x04000325 RID: 805
	private FixedCaptureStates.CaptureStates capture;

	// Token: 0x04000326 RID: 806
	private GameStateMachine<FixedCaptureStates, FixedCaptureStates.Instance, IStateMachineTarget, FixedCaptureStates.Def>.State behaviourcomplete;

	// Token: 0x020010EA RID: 4330
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020010EB RID: 4331
	public new class Instance : GameStateMachine<FixedCaptureStates, FixedCaptureStates.Instance, IStateMachineTarget, FixedCaptureStates.Def>.GameInstance
	{
		// Token: 0x0600810A RID: 33034 RVA: 0x0032E4D8 File Offset: 0x0032C6D8
		public Instance(Chore<FixedCaptureStates.Instance> chore, FixedCaptureStates.Def def)
			: base(chore, def)
		{
			this.originalSpeed = base.GetComponent<Navigator>().defaultSpeed;
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.WantsToGetCaptured);
		}

		// Token: 0x0600810B RID: 33035 RVA: 0x0032E510 File Offset: 0x0032C710
		public FixedCapturePoint.Instance GetCapturePoint()
		{
			FixedCapturableMonitor.Instance smi = this.GetSMI<FixedCapturableMonitor.Instance>();
			if (smi == null)
			{
				return null;
			}
			return smi.targetCapturePoint;
		}

		// Token: 0x0600810C RID: 33036 RVA: 0x0032E52F File Offset: 0x0032C72F
		public void AbandonedCapturePoint()
		{
			if (this.GetCapturePoint() != null)
			{
				this.GetCapturePoint().Trigger(-1000356449, null);
			}
		}

		// Token: 0x04006198 RID: 24984
		public float originalSpeed;
	}

	// Token: 0x020010EC RID: 4332
	public class CaptureStates : GameStateMachine<FixedCaptureStates, FixedCaptureStates.Instance, IStateMachineTarget, FixedCaptureStates.Def>.State
	{
		// Token: 0x04006199 RID: 24985
		public FixedCaptureStates.CaptureStates.CheerStates cheer;

		// Token: 0x0400619A RID: 24986
		public FixedCaptureStates.CaptureStates.MoveStates move;

		// Token: 0x0400619B RID: 24987
		public GameStateMachine<FixedCaptureStates, FixedCaptureStates.Instance, IStateMachineTarget, FixedCaptureStates.Def>.State ranching;

		// Token: 0x0200260F RID: 9743
		public class CheerStates : GameStateMachine<FixedCaptureStates, FixedCaptureStates.Instance, IStateMachineTarget, FixedCaptureStates.Def>.State
		{
			// Token: 0x0400A986 RID: 43398
			public GameStateMachine<FixedCaptureStates, FixedCaptureStates.Instance, IStateMachineTarget, FixedCaptureStates.Def>.State pre;

			// Token: 0x0400A987 RID: 43399
			public GameStateMachine<FixedCaptureStates, FixedCaptureStates.Instance, IStateMachineTarget, FixedCaptureStates.Def>.State cheer;

			// Token: 0x0400A988 RID: 43400
			public GameStateMachine<FixedCaptureStates, FixedCaptureStates.Instance, IStateMachineTarget, FixedCaptureStates.Def>.State pst;
		}

		// Token: 0x02002610 RID: 9744
		public class MoveStates : GameStateMachine<FixedCaptureStates, FixedCaptureStates.Instance, IStateMachineTarget, FixedCaptureStates.Def>.State
		{
			// Token: 0x0400A989 RID: 43401
			public GameStateMachine<FixedCaptureStates, FixedCaptureStates.Instance, IStateMachineTarget, FixedCaptureStates.Def>.State movetoranch;

			// Token: 0x0400A98A RID: 43402
			public GameStateMachine<FixedCaptureStates, FixedCaptureStates.Instance, IStateMachineTarget, FixedCaptureStates.Def>.State waitforranchertobeready;
		}
	}
}
