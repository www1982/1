using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000B47 RID: 2887
public class LargeImpactorNotificationMonitor : GameStateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>
{
	// Token: 0x060055E2 RID: 21986 RVA: 0x001F2EE4 File Offset: 0x001F10E4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		default_state = this.undiscovered;
		this.undiscovered.ParamTransition<bool>(this.HasBeenDiscovered, this.discovered, GameStateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.IsTrue).EventHandler(GameHashes.DiscoveredSpace, (LargeImpactorNotificationMonitor.Instance smi) => Game.Instance, new GameStateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.GameEvent.Callback(LargeImpactorNotificationMonitor.OnDuplicantReachedSpace)).EventHandler(GameHashes.DLCPOICompleted, (LargeImpactorNotificationMonitor.Instance smi) => Game.Instance, new GameStateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.GameEvent.Callback(LargeImpactorNotificationMonitor.OnPOIActivated));
		this.discovered.DefaultState(this.discovered.sequence);
		this.discovered.sequence.ParamTransition<bool>(this.SequenceCompleted, this.discovered.notification, GameStateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.IsTrue).Enter(new StateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.State.Callback(LargeImpactorNotificationMonitor.RevealSurface)).Enter(new StateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.State.Callback(LargeImpactorNotificationMonitor.PlaySequence))
			.EventHandler(GameHashes.SequenceCompleted, new StateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.State.Callback(LargeImpactorNotificationMonitor.CompleteSequence));
		this.discovered.notification.DefaultState(this.discovered.notification.delayEntry);
		this.discovered.notification.delayEntry.ScheduleGoTo(3f, this.discovered.notification.running);
		this.discovered.notification.running.Enter(new StateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.State.Callback(LargeImpactorNotificationMonitor.PlayNotificationEnterSound)).Enter(new StateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.State.Callback(LargeImpactorNotificationMonitor.SetLandingZoneVisualizationToActive)).ScheduleAction("Toggle off the visualization after a delay", 2f, new Action<LargeImpactorNotificationMonitor.Instance>(LargeImpactorNotificationMonitor.FoldTheVisualization))
			.ToggleNotification((LargeImpactorNotificationMonitor.Instance smi) => smi.notification);
	}

	// Token: 0x060055E3 RID: 21987 RVA: 0x001F30BB File Offset: 0x001F12BB
	public static void CompleteSequence(LargeImpactorNotificationMonitor.Instance smi)
	{
		smi.sm.SequenceCompleted.Set(true, smi, false);
	}

	// Token: 0x060055E4 RID: 21988 RVA: 0x001F30D1 File Offset: 0x001F12D1
	public static void Discover(LargeImpactorNotificationMonitor.Instance smi)
	{
		smi.sm.HasBeenDiscovered.Set(true, smi, false);
	}

	// Token: 0x060055E5 RID: 21989 RVA: 0x001F30E7 File Offset: 0x001F12E7
	public static void RevealSurface(LargeImpactorNotificationMonitor.Instance smi)
	{
		smi.RevealSurface();
	}

	// Token: 0x060055E6 RID: 21990 RVA: 0x001F30EF File Offset: 0x001F12EF
	public static void PlayNotificationEnterSound(LargeImpactorNotificationMonitor.Instance smi)
	{
		KFMOD.PlayUISound(GlobalAssets.GetSound("Notification_Imperative", false));
	}

	// Token: 0x060055E7 RID: 21991 RVA: 0x001F3101 File Offset: 0x001F1301
	public static void SetLandingZoneVisualizationToActive(LargeImpactorNotificationMonitor.Instance smi)
	{
		smi.GetComponent<LargeImpactorVisualizer>().Active = true;
	}

	// Token: 0x060055E8 RID: 21992 RVA: 0x001F3110 File Offset: 0x001F1310
	public static void FoldTheVisualization(LargeImpactorNotificationMonitor.Instance smi)
	{
		LargeImpactorVisualizer component = smi.GetComponent<LargeImpactorVisualizer>();
		if (!component.Folded)
		{
			component.SetFoldedState(true);
		}
	}

	// Token: 0x060055E9 RID: 21993 RVA: 0x001F3133 File Offset: 0x001F1333
	public static void OnPOIActivated(LargeImpactorNotificationMonitor.Instance smi, object obj)
	{
		if (((GameObject)obj).PrefabID() == "POIDlc4TechUnlock")
		{
			LargeImpactorNotificationMonitor.Discover(smi);
		}
	}

	// Token: 0x060055EA RID: 21994 RVA: 0x001F3158 File Offset: 0x001F1358
	public static void OnDuplicantReachedSpace(LargeImpactorNotificationMonitor.Instance smi, object obj)
	{
		int myWorldId = ((GameObject)obj).GetMyWorldId();
		int myWorldId2 = smi.gameObject.GetMyWorldId();
		if (myWorldId == myWorldId2)
		{
			LargeImpactorNotificationMonitor.Discover(smi);
		}
	}

	// Token: 0x060055EB RID: 21995 RVA: 0x001F3185 File Offset: 0x001F1385
	public static void PlaySequence(LargeImpactorNotificationMonitor.Instance smi)
	{
		smi.PlaySequence();
	}

	// Token: 0x0400394C RID: 14668
	public const string NOTIFICATION_PREFAB_ID = "LargeImpactNotification";

	// Token: 0x0400394D RID: 14669
	public GameStateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.State undiscovered;

	// Token: 0x0400394E RID: 14670
	public LargeImpactorNotificationMonitor.DiscoveredStates discovered;

	// Token: 0x0400394F RID: 14671
	public StateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.BoolParameter HasBeenDiscovered;

	// Token: 0x04003950 RID: 14672
	public StateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.BoolParameter SequenceCompleted;

	// Token: 0x02001C69 RID: 7273
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001C6A RID: 7274
	public class NotificationStates : GameStateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.State
	{
		// Token: 0x0400863F RID: 34367
		public GameStateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.State delayEntry;

		// Token: 0x04008640 RID: 34368
		public GameStateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.State running;
	}

	// Token: 0x02001C6B RID: 7275
	public class DiscoveredStates : GameStateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.State
	{
		// Token: 0x04008641 RID: 34369
		public GameStateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.State sequence;

		// Token: 0x04008642 RID: 34370
		public LargeImpactorNotificationMonitor.NotificationStates notification;
	}

	// Token: 0x02001C6C RID: 7276
	public new class Instance : GameStateMachine<LargeImpactorNotificationMonitor, LargeImpactorNotificationMonitor.Instance, IStateMachineTarget, LargeImpactorNotificationMonitor.Def>.GameInstance
	{
		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x0600AAEA RID: 43754 RVA: 0x003BC0D9 File Offset: 0x003BA2D9
		public bool HasRevealSequencePlayed
		{
			get
			{
				return base.sm.SequenceCompleted.Get(this);
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x0600AAEC RID: 43756 RVA: 0x003BC0F5 File Offset: 0x003BA2F5
		// (set) Token: 0x0600AAEB RID: 43755 RVA: 0x003BC0EC File Offset: 0x003BA2EC
		public Notification notification { get; private set; }

		// Token: 0x0600AAED RID: 43757 RVA: 0x003BC100 File Offset: 0x003BA300
		public Instance(IStateMachineTarget master, LargeImpactorNotificationMonitor.Def def)
			: base(master, def)
		{
			this.notifier = base.gameObject.AddOrGet<Notifier>();
			LargeImpactorStatus.Instance smi = base.smi.GetSMI<LargeImpactorStatus.Instance>();
			string text = MISC.NOTIFICATIONS.INCOMINGPREHISTORICASTEROIDNOTIFICATION.NAME;
			NotificationType notificationType = NotificationType.Custom;
			object obj = smi;
			this.notification = new Notification(text, notificationType, new Func<List<Notification>, object, string>(this.ResolveNotificationTooltip), obj, false, 0f, null, null, null, true, false, false);
			this.notification.customNotificationID = "LargeImpactNotification";
		}

		// Token: 0x0600AAEE RID: 43758 RVA: 0x003BC174 File Offset: 0x003BA374
		private string ResolveNotificationTooltip(List<Notification> not, object data)
		{
			LargeImpactorStatus.Instance instance = (LargeImpactorStatus.Instance)data;
			return GameUtil.SafeStringFormat(MISC.NOTIFICATIONS.INCOMINGPREHISTORICASTEROIDNOTIFICATION.TOOLTIP, new object[]
			{
				GameUtil.GetFormattedInt((float)instance.Health, GameUtil.TimeSlice.None),
				GameUtil.GetFormattedInt((float)instance.def.MAX_HEALTH, GameUtil.TimeSlice.None),
				GameUtil.GetFormattedCycles(instance.TimeRemainingBeforeCollision, "F1", false)
			});
		}

		// Token: 0x0600AAEF RID: 43759 RVA: 0x003BC1D8 File Offset: 0x003BA3D8
		public void RevealSurface()
		{
			GameplayEventInstance gameplayEventInstance = GameplayEventManager.Instance.GetGameplayEventInstance(Db.Get().GameplayEvents.LargeImpactor.Id, -1);
			if (gameplayEventInstance != null)
			{
				WorldContainer world = ClusterManager.Instance.GetWorld(gameplayEventInstance.worldId);
				if (world != null && !world.IsSurfaceRevealed)
				{
					world.RevealSurface();
				}
			}
		}

		// Token: 0x0600AAF0 RID: 43760 RVA: 0x003BC235 File Offset: 0x003BA435
		public void SetNotificationVisibility(bool visible)
		{
			if (visible)
			{
				this.notifier.Add(this.notification, "");
				return;
			}
			this.notifier.Remove(this.notification);
		}

		// Token: 0x0600AAF1 RID: 43761 RVA: 0x003BC264 File Offset: 0x003BA464
		public void PlaySequence()
		{
			this.AbortSequenceCoroutine();
			this.CreateReticleForSequence();
			GameplayEventInstance gameplayEventInstance = GameplayEventManager.Instance.GetGameplayEventInstance(Db.Get().GameplayEvents.LargeImpactor.Id, -1);
			if (gameplayEventInstance != null)
			{
				WorldContainer world = ClusterManager.Instance.GetWorld(gameplayEventInstance.worldId);
				this.sequenceCoroutine = LargeImpactorRevealSequence.Start(this.notifier, this.sequenceReticle, world);
			}
		}

		// Token: 0x0600AAF2 RID: 43762 RVA: 0x003BC2D0 File Offset: 0x003BA4D0
		private void CreateReticleForSequence()
		{
			this.DeleteReticleObject();
			this.sequenceReticle = Util.KInstantiateUI<LargeImpactorSequenceUIReticle>(ScreenPrefabs.Instance.largeImpactorSequenceReticlePrefab.gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject, true);
			LargeImpactorStatus.Instance smi = base.gameObject.GetSMI<LargeImpactorStatus.Instance>();
			this.sequenceReticle.SetTarget(smi);
		}

		// Token: 0x0600AAF3 RID: 43763 RVA: 0x003BC325 File Offset: 0x003BA525
		private void DeleteReticleObject()
		{
			if (this.sequenceReticle != null)
			{
				this.sequenceReticle.gameObject.DeleteObject();
			}
		}

		// Token: 0x0600AAF4 RID: 43764 RVA: 0x003BC345 File Offset: 0x003BA545
		private void AbortSequenceCoroutine()
		{
			if (this.sequenceCoroutine != null)
			{
				this.notifier.StopCoroutine(this.sequenceCoroutine);
				this.sequenceCoroutine = null;
			}
		}

		// Token: 0x0600AAF5 RID: 43765 RVA: 0x003BC367 File Offset: 0x003BA567
		protected override void OnCleanUp()
		{
			this.AbortSequenceCoroutine();
			this.DeleteReticleObject();
			base.OnCleanUp();
		}

		// Token: 0x04008643 RID: 34371
		private Notifier notifier;

		// Token: 0x04008645 RID: 34373
		private Coroutine sequenceCoroutine;

		// Token: 0x04008646 RID: 34374
		private LargeImpactorSequenceUIReticle sequenceReticle;
	}
}
