using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000474 RID: 1140
public class BionicGunkSpillChore : Chore<BionicGunkSpillChore.StatesInstance>
{
	// Token: 0x060017FA RID: 6138 RVA: 0x00084C1C File Offset: 0x00082E1C
	public static bool HasSuit(BionicGunkSpillChore.StatesInstance smi)
	{
		return smi.GetComponent<SuitEquipper>().IsWearingAirtightSuit();
	}

	// Token: 0x060017FB RID: 6139 RVA: 0x00084C30 File Offset: 0x00082E30
	public static void ExpellGunkUpdate(BionicGunkSpillChore.StatesInstance smi, float dt)
	{
		float num = GunkMonitor.GUNK_CAPACITY * (dt / 10f);
		if (num >= smi.gunkMonitor.CurrentGunkMass)
		{
			smi.GoTo(smi.sm.pst);
			return;
		}
		smi.gunkMonitor.ExpellGunk(num, null);
	}

	// Token: 0x060017FC RID: 6140 RVA: 0x00084C78 File Offset: 0x00082E78
	public BionicGunkSpillChore(IStateMachineTarget target)
		: base(Db.Get().ChoreTypes.ExpellGunk, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new BionicGunkSpillChore.StatesInstance(this, target.gameObject);
	}

	// Token: 0x04000DCF RID: 3535
	public const float EVENT_DURATION = 10f;

	// Token: 0x04000DD0 RID: 3536
	public const string PRE_ANIM_NAME = "oiloverload_pre";

	// Token: 0x04000DD1 RID: 3537
	public const string LOOP_ANIM_NAME = "oiloverload_loop";

	// Token: 0x04000DD2 RID: 3538
	public const string PST_ANIM_NAME = "overload_pst";

	// Token: 0x04000DD3 RID: 3539
	public const string SUIT_PRE_ANIM_NAME = "oiloverload_helmet_pre";

	// Token: 0x04000DD4 RID: 3540
	public const string SUIT_LOOP_ANIM_NAME = "oiloverload_helmet_loop";

	// Token: 0x04000DD5 RID: 3541
	public const string SUIT_PST_ANIM_NAME = "oiloverload_helmet_pst";

	// Token: 0x02001267 RID: 4711
	public class States : GameStateMachine<BionicGunkSpillChore.States, BionicGunkSpillChore.StatesInstance, BionicGunkSpillChore>
	{
		// Token: 0x0600862D RID: 34349 RVA: 0x0033BD28 File Offset: 0x00339F28
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.enter;
			base.Target(this.worker);
			this.root.ToggleAnims("anim_bionic_oil_overload_kanim", 0f).ToggleEffect("ExpellingGunk").ToggleTag(GameTags.MakingMess)
				.DoNotification((BionicGunkSpillChore.StatesInstance smi) => smi.stressfullyEmptyingGunk)
				.Enter(delegate(BionicGunkSpillChore.StatesInstance smi)
				{
					if (Sim.IsRadiationEnabled() && smi.master.gameObject.GetAmounts().Get(Db.Get().Amounts.RadiationBalance).value > 0f)
					{
						smi.master.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, null);
					}
				});
			this.enter.DefaultState(this.enter.noSuit);
			this.enter.noSuit.EventTransition(GameHashes.EquippedItemEquipper, this.enter.suit, new StateMachine<BionicGunkSpillChore.States, BionicGunkSpillChore.StatesInstance, BionicGunkSpillChore, object>.Transition.ConditionCallback(BionicGunkSpillChore.HasSuit)).PlayAnim("oiloverload_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.running);
			this.enter.suit.EventTransition(GameHashes.UnequippedItemEquipper, this.enter.noSuit, GameStateMachine<BionicGunkSpillChore.States, BionicGunkSpillChore.StatesInstance, BionicGunkSpillChore, object>.Not(new StateMachine<BionicGunkSpillChore.States, BionicGunkSpillChore.StatesInstance, BionicGunkSpillChore, object>.Transition.ConditionCallback(BionicGunkSpillChore.HasSuit))).PlayAnim("oiloverload_helmet_pre", KAnim.PlayMode.Once).OnAnimQueueComplete(this.running);
			this.running.DefaultState(this.running.noSuit).Update(new Action<BionicGunkSpillChore.StatesInstance, float>(BionicGunkSpillChore.ExpellGunkUpdate), UpdateRate.SIM_200ms, false);
			this.running.noSuit.EventTransition(GameHashes.EquippedItemEquipper, this.running.suit, new StateMachine<BionicGunkSpillChore.States, BionicGunkSpillChore.StatesInstance, BionicGunkSpillChore, object>.Transition.ConditionCallback(BionicGunkSpillChore.HasSuit)).PlayAnim("oiloverload_loop", KAnim.PlayMode.Loop);
			this.running.suit.EventTransition(GameHashes.UnequippedItemEquipper, this.running.noSuit, GameStateMachine<BionicGunkSpillChore.States, BionicGunkSpillChore.StatesInstance, BionicGunkSpillChore, object>.Not(new StateMachine<BionicGunkSpillChore.States, BionicGunkSpillChore.StatesInstance, BionicGunkSpillChore, object>.Transition.ConditionCallback(BionicGunkSpillChore.HasSuit))).PlayAnim("oiloverload_helmet_loop", KAnim.PlayMode.Loop);
			this.pst.DefaultState(this.pst.noSuit);
			this.pst.noSuit.EventTransition(GameHashes.EquippedItemEquipper, this.pst.suit, new StateMachine<BionicGunkSpillChore.States, BionicGunkSpillChore.StatesInstance, BionicGunkSpillChore, object>.Transition.ConditionCallback(BionicGunkSpillChore.HasSuit)).PlayAnim("overload_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.complete);
			this.pst.suit.EventTransition(GameHashes.UnequippedItemEquipper, this.pst.noSuit, GameStateMachine<BionicGunkSpillChore.States, BionicGunkSpillChore.StatesInstance, BionicGunkSpillChore, object>.Not(new StateMachine<BionicGunkSpillChore.States, BionicGunkSpillChore.StatesInstance, BionicGunkSpillChore, object>.Transition.ConditionCallback(BionicGunkSpillChore.HasSuit))).PlayAnim("oiloverload_helmet_pst", KAnim.PlayMode.Once).OnAnimQueueComplete(this.complete);
			this.complete.ReturnSuccess();
		}

		// Token: 0x040065F9 RID: 26105
		public BionicGunkSpillChore.States.SuitAnimState enter;

		// Token: 0x040065FA RID: 26106
		public BionicGunkSpillChore.States.SuitAnimState running;

		// Token: 0x040065FB RID: 26107
		public BionicGunkSpillChore.States.SuitAnimState pst;

		// Token: 0x040065FC RID: 26108
		public GameStateMachine<BionicGunkSpillChore.States, BionicGunkSpillChore.StatesInstance, BionicGunkSpillChore, object>.State complete;

		// Token: 0x040065FD RID: 26109
		public StateMachine<BionicGunkSpillChore.States, BionicGunkSpillChore.StatesInstance, BionicGunkSpillChore, object>.TargetParameter worker;

		// Token: 0x02002639 RID: 9785
		public class SuitAnimState : GameStateMachine<BionicGunkSpillChore.States, BionicGunkSpillChore.StatesInstance, BionicGunkSpillChore, object>.State
		{
			// Token: 0x0400A9FE RID: 43518
			public GameStateMachine<BionicGunkSpillChore.States, BionicGunkSpillChore.StatesInstance, BionicGunkSpillChore, object>.State noSuit;

			// Token: 0x0400A9FF RID: 43519
			public GameStateMachine<BionicGunkSpillChore.States, BionicGunkSpillChore.StatesInstance, BionicGunkSpillChore, object>.State suit;
		}
	}

	// Token: 0x02001268 RID: 4712
	public class StatesInstance : GameStateMachine<BionicGunkSpillChore.States, BionicGunkSpillChore.StatesInstance, BionicGunkSpillChore, object>.GameInstance
	{
		// Token: 0x0600862F RID: 34351 RVA: 0x0033BFB4 File Offset: 0x0033A1B4
		public StatesInstance(BionicGunkSpillChore master, GameObject worker)
			: base(master)
		{
			this.gunkMonitor = worker.GetSMI<GunkMonitor.Instance>();
			base.sm.worker.Set(worker, base.smi, false);
		}

		// Token: 0x040065FE RID: 26110
		public Notification stressfullyEmptyingGunk = new Notification(DUPLICANTS.STATUSITEMS.STRESSFULLYEMPTYINGOIL.NOTIFICATION_NAME, NotificationType.Bad, (List<Notification> notificationList, object data) => DUPLICANTS.STATUSITEMS.STRESSFULLYEMPTYINGOIL.NOTIFICATION_TOOLTIP + notificationList.ReduceMessages(false), null, true, 0f, null, null, null, true, false, false);

		// Token: 0x040065FF RID: 26111
		public GunkMonitor.Instance gunkMonitor;
	}
}
