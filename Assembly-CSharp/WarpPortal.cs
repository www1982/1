using System;
using System.Collections;
using System.Collections.Generic;
using Klei.AI;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x020007F3 RID: 2035
public class WarpPortal : Workable
{
	// Token: 0x170003BB RID: 955
	// (get) Token: 0x06003755 RID: 14165 RVA: 0x00133924 File Offset: 0x00131B24
	public bool ReadyToWarp
	{
		get
		{
			return this.warpPortalSMI.IsInsideState(this.warpPortalSMI.sm.occupied.waiting);
		}
	}

	// Token: 0x170003BC RID: 956
	// (get) Token: 0x06003756 RID: 14166 RVA: 0x00133946 File Offset: 0x00131B46
	public bool IsWorking
	{
		get
		{
			return this.warpPortalSMI.IsInsideState(this.warpPortalSMI.sm.occupied);
		}
	}

	// Token: 0x06003757 RID: 14167 RVA: 0x00133963 File Offset: 0x00131B63
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.assignable.OnAssign += this.Assign;
	}

	// Token: 0x06003758 RID: 14168 RVA: 0x00133984 File Offset: 0x00131B84
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.warpPortalSMI = new WarpPortal.WarpPortalSM.Instance(this);
		this.warpPortalSMI.sm.isCharged.Set(!this.IsConsumed, this.warpPortalSMI, false);
		this.warpPortalSMI.StartSM();
		this.selectEventHandle = Game.Instance.Subscribe(-1503271301, new Action<object>(this.OnObjectSelected));
	}

	// Token: 0x06003759 RID: 14169 RVA: 0x001339F5 File Offset: 0x00131BF5
	private void OnObjectSelected(object data)
	{
		if (data != null && (GameObject)data == base.gameObject && Components.LiveMinionIdentities.Count > 0)
		{
			this.Discover();
		}
	}

	// Token: 0x0600375A RID: 14170 RVA: 0x00133A20 File Offset: 0x00131C20
	protected override void OnCleanUp()
	{
		Game.Instance.Unsubscribe(this.selectEventHandle);
		base.OnCleanUp();
	}

	// Token: 0x0600375B RID: 14171 RVA: 0x00133A38 File Offset: 0x00131C38
	private void Discover()
	{
		if (this.discovered)
		{
			return;
		}
		ClusterManager.Instance.GetWorld(this.GetTargetWorldID()).SetDiscovered(true);
		SimpleEvent.StatesInstance statesInstance = GameplayEventManager.Instance.StartNewEvent(Db.Get().GameplayEvents.WarpWorldReveal, -1, null).smi as SimpleEvent.StatesInstance;
		statesInstance.minions = new GameObject[] { Components.LiveMinionIdentities[0].gameObject };
		statesInstance.callback = delegate
		{
			ManagementMenu.Instance.OpenClusterMap();
			ClusterMapScreen.Instance.SetTargetFocusPosition(ClusterManager.Instance.GetWorld(this.GetTargetWorldID()).GetMyWorldLocation(), 0.5f);
		};
		statesInstance.ShowEventPopup();
		this.discovered = true;
	}

	// Token: 0x0600375C RID: 14172 RVA: 0x00133AC8 File Offset: 0x00131CC8
	public void StartWarpSequence()
	{
		this.warpPortalSMI.GoTo(this.warpPortalSMI.sm.occupied.warping);
	}

	// Token: 0x0600375D RID: 14173 RVA: 0x00133AEA File Offset: 0x00131CEA
	public void CancelAssignment()
	{
		this.CancelChore();
		this.assignable.Unassign();
		this.warpPortalSMI.GoTo(this.warpPortalSMI.sm.idle);
	}

	// Token: 0x0600375E RID: 14174 RVA: 0x00133B18 File Offset: 0x00131D18
	private int GetTargetWorldID()
	{
		SaveGame.Instance.GetComponent<WorldGenSpawner>().SpawnTag(WarpReceiverConfig.ID);
		foreach (WarpReceiver warpReceiver in global::UnityEngine.Object.FindObjectsOfType<WarpReceiver>())
		{
			if (warpReceiver.GetMyWorldId() != this.GetMyWorldId())
			{
				return warpReceiver.GetMyWorldId();
			}
		}
		global::Debug.LogError("No receiver world found for warp portal sender");
		return -1;
	}

	// Token: 0x0600375F RID: 14175 RVA: 0x00133B74 File Offset: 0x00131D74
	private void Warp()
	{
		if (base.worker == null || base.worker.HasTag(GameTags.Dying) || base.worker.HasTag(GameTags.Dead))
		{
			return;
		}
		WarpReceiver warpReceiver = null;
		foreach (WarpReceiver warpReceiver2 in global::UnityEngine.Object.FindObjectsOfType<WarpReceiver>())
		{
			if (warpReceiver2.GetMyWorldId() != this.GetMyWorldId())
			{
				warpReceiver = warpReceiver2;
				break;
			}
		}
		if (warpReceiver == null)
		{
			SaveGame.Instance.GetComponent<WorldGenSpawner>().SpawnTag(WarpReceiverConfig.ID);
			warpReceiver = global::UnityEngine.Object.FindObjectOfType<WarpReceiver>();
		}
		if (warpReceiver != null)
		{
			this.delayWarpRoutine = base.StartCoroutine(this.DelayedWarp(warpReceiver));
		}
		else
		{
			global::Debug.LogWarning("No warp receiver found - maybe POI stomping or failure to spawn?");
		}
		if (SelectTool.Instance.selected == base.GetComponent<KSelectable>())
		{
			SelectTool.Instance.Select(null, true);
		}
	}

	// Token: 0x06003760 RID: 14176 RVA: 0x00133C4E File Offset: 0x00131E4E
	public IEnumerator DelayedWarp(WarpReceiver receiver)
	{
		yield return SequenceUtil.WaitForEndOfFrame;
		int myWorldId = base.worker.GetMyWorldId();
		int myWorldId2 = receiver.GetMyWorldId();
		GameUtil.FocusCameraOnWorld(myWorldId2, Grid.CellToPos(Grid.PosToCell(receiver)), 10f, null, true);
		WorkerBase worker = base.worker;
		worker.StopWork();
		receiver.ReceiveWarpedDuplicant(worker);
		ClusterManager.Instance.MigrateMinion(worker.GetComponent<MinionIdentity>(), myWorldId2, myWorldId);
		this.delayWarpRoutine = null;
		yield break;
	}

	// Token: 0x06003761 RID: 14177 RVA: 0x00133C64 File Offset: 0x00131E64
	public void SetAssignable(bool set_it)
	{
		this.assignable.SetCanBeAssigned(set_it);
		this.RefreshSideScreen();
	}

	// Token: 0x06003762 RID: 14178 RVA: 0x00133C78 File Offset: 0x00131E78
	private void Assign(IAssignableIdentity new_assignee)
	{
		this.CancelChore();
		if (new_assignee != null)
		{
			this.ActivateChore();
		}
	}

	// Token: 0x06003763 RID: 14179 RVA: 0x00133C8C File Offset: 0x00131E8C
	private void ActivateChore()
	{
		global::Debug.Assert(this.chore == null);
		this.chore = new WorkChore<Workable>(Db.Get().ChoreTypes.Migrate, this, null, true, delegate(Chore o)
		{
			this.CompleteChore();
		}, null, null, true, null, false, true, Assets.GetAnim("anim_interacts_warp_portal_sender_kanim"), false, true, false, PriorityScreen.PriorityClass.high, 5, false, true);
		base.SetWorkTime(float.PositiveInfinity);
		this.workLayer = Grid.SceneLayer.Building;
		this.workAnims = new HashedString[] { "sending_pre", "sending_loop" };
		this.workingPstComplete = new HashedString[] { "sending_pst" };
		this.workingPstFailed = new HashedString[] { "idle_loop" };
		this.showProgressBar = false;
	}

	// Token: 0x06003764 RID: 14180 RVA: 0x00133D6E File Offset: 0x00131F6E
	private void CancelChore()
	{
		if (this.chore == null)
		{
			return;
		}
		this.chore.Cancel("User cancelled");
		this.chore = null;
		if (this.delayWarpRoutine != null)
		{
			base.StopCoroutine(this.delayWarpRoutine);
			this.delayWarpRoutine = null;
		}
	}

	// Token: 0x06003765 RID: 14181 RVA: 0x00133DAB File Offset: 0x00131FAB
	private void CompleteChore()
	{
		this.IsConsumed = true;
		this.chore.Cleanup();
		this.chore = null;
	}

	// Token: 0x06003766 RID: 14182 RVA: 0x00133DC6 File Offset: 0x00131FC6
	public void RefreshSideScreen()
	{
		if (base.GetComponent<KSelectable>().IsSelected)
		{
			DetailsScreen.Instance.Refresh(base.gameObject);
		}
	}

	// Token: 0x04002196 RID: 8598
	[MyCmpReq]
	public Assignable assignable;

	// Token: 0x04002197 RID: 8599
	[MyCmpAdd]
	public Notifier notifier;

	// Token: 0x04002198 RID: 8600
	private Chore chore;

	// Token: 0x04002199 RID: 8601
	private WarpPortal.WarpPortalSM.Instance warpPortalSMI;

	// Token: 0x0400219A RID: 8602
	private Notification notification;

	// Token: 0x0400219B RID: 8603
	public const float RECHARGE_TIME = 3000f;

	// Token: 0x0400219C RID: 8604
	[Serialize]
	public bool IsConsumed;

	// Token: 0x0400219D RID: 8605
	[Serialize]
	public float rechargeProgress;

	// Token: 0x0400219E RID: 8606
	[Serialize]
	private bool discovered;

	// Token: 0x0400219F RID: 8607
	private int selectEventHandle = -1;

	// Token: 0x040021A0 RID: 8608
	private Coroutine delayWarpRoutine;

	// Token: 0x040021A1 RID: 8609
	private static readonly HashedString[] printing_anim = new HashedString[] { "printing_pre", "printing_loop", "printing_pst" };

	// Token: 0x02001753 RID: 5971
	public class WarpPortalSM : GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal>
	{
		// Token: 0x060098A2 RID: 39074 RVA: 0x00381CDC File Offset: 0x0037FEDC
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			this.root.Enter(delegate(WarpPortal.WarpPortalSM.Instance smi)
			{
				if (smi.master.rechargeProgress != 0f)
				{
					smi.GoTo(this.recharging);
				}
			}).DefaultState(this.idle);
			this.idle.PlayAnim("idle", KAnim.PlayMode.Loop).Enter(delegate(WarpPortal.WarpPortalSM.Instance smi)
			{
				smi.master.IsConsumed = false;
				smi.sm.isCharged.Set(true, smi, false);
				smi.master.SetAssignable(true);
			}).Exit(delegate(WarpPortal.WarpPortalSM.Instance smi)
			{
				smi.master.SetAssignable(false);
			})
				.WorkableStartTransition((WarpPortal.WarpPortalSM.Instance smi) => smi.master, this.become_occupied)
				.ParamTransition<bool>(this.isCharged, this.recharging, GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.IsFalse);
			this.become_occupied.Enter(delegate(WarpPortal.WarpPortalSM.Instance smi)
			{
				this.worker.Set(smi.master.worker, smi);
				smi.GoTo(this.occupied.get_on);
			});
			this.occupied.OnTargetLost(this.worker, this.idle).Target(this.worker).TagTransition(GameTags.Dying, this.idle, false)
				.Target(this.masterTarget)
				.Exit(delegate(WarpPortal.WarpPortalSM.Instance smi)
				{
					this.worker.Set(null, smi);
				});
			this.occupied.get_on.PlayAnim("sending_pre").OnAnimQueueComplete(this.occupied.waiting);
			this.occupied.waiting.PlayAnim("sending_loop", KAnim.PlayMode.Loop).ToggleNotification((WarpPortal.WarpPortalSM.Instance smi) => smi.CreateDupeWaitingNotification()).Enter(delegate(WarpPortal.WarpPortalSM.Instance smi)
			{
				smi.master.RefreshSideScreen();
			})
				.Exit(delegate(WarpPortal.WarpPortalSM.Instance smi)
				{
					smi.master.RefreshSideScreen();
				});
			this.occupied.warping.PlayAnim("sending_pst").OnAnimQueueComplete(this.do_warp);
			this.do_warp.Enter(delegate(WarpPortal.WarpPortalSM.Instance smi)
			{
				smi.master.Warp();
			}).GoTo(this.recharging);
			this.recharging.Enter(delegate(WarpPortal.WarpPortalSM.Instance smi)
			{
				smi.master.SetAssignable(false);
				smi.master.IsConsumed = true;
				this.isCharged.Set(false, smi, false);
			}).PlayAnim("recharge", KAnim.PlayMode.Loop).ToggleStatusItem(Db.Get().BuildingStatusItems.WarpPortalCharging, (WarpPortal.WarpPortalSM.Instance smi) => smi.master)
				.Update(delegate(WarpPortal.WarpPortalSM.Instance smi, float dt)
				{
					smi.master.rechargeProgress += dt;
					if (smi.master.rechargeProgress > 3000f)
					{
						this.isCharged.Set(true, smi, false);
						smi.master.rechargeProgress = 0f;
						smi.GoTo(this.idle);
					}
				}, UpdateRate.SIM_200ms, false);
		}

		// Token: 0x0400755C RID: 30044
		public GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.State idle;

		// Token: 0x0400755D RID: 30045
		public GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.State become_occupied;

		// Token: 0x0400755E RID: 30046
		public WarpPortal.WarpPortalSM.OccupiedStates occupied;

		// Token: 0x0400755F RID: 30047
		public GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.State do_warp;

		// Token: 0x04007560 RID: 30048
		public GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.State recharging;

		// Token: 0x04007561 RID: 30049
		public StateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.BoolParameter isCharged;

		// Token: 0x04007562 RID: 30050
		private StateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.TargetParameter worker;

		// Token: 0x020027FD RID: 10237
		public class OccupiedStates : GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.State
		{
			// Token: 0x0400B16E RID: 45422
			public GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.State get_on;

			// Token: 0x0400B16F RID: 45423
			public GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.State waiting;

			// Token: 0x0400B170 RID: 45424
			public GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.State warping;
		}

		// Token: 0x020027FE RID: 10238
		public new class Instance : GameStateMachine<WarpPortal.WarpPortalSM, WarpPortal.WarpPortalSM.Instance, WarpPortal, object>.GameInstance
		{
			// Token: 0x0600CA3E RID: 51774 RVA: 0x004172CC File Offset: 0x004154CC
			public Instance(WarpPortal master)
				: base(master)
			{
			}

			// Token: 0x0600CA3F RID: 51775 RVA: 0x004172D8 File Offset: 0x004154D8
			public Notification CreateDupeWaitingNotification()
			{
				if (base.master.worker != null)
				{
					return new Notification(MISC.NOTIFICATIONS.WARP_PORTAL_DUPE_READY.NAME.Replace("{dupe}", base.master.worker.name), NotificationType.Neutral, (List<Notification> notificationList, object data) => MISC.NOTIFICATIONS.WARP_PORTAL_DUPE_READY.TOOLTIP.Replace("{dupe}", base.master.worker.name), null, false, 0f, null, null, base.master.transform, true, false, false);
				}
				return null;
			}
		}
	}
}
