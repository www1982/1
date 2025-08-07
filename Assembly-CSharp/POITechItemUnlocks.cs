using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000A50 RID: 2640
public class POITechItemUnlocks : GameStateMachine<POITechItemUnlocks, POITechItemUnlocks.Instance, IStateMachineTarget, POITechItemUnlocks.Def>
{
	// Token: 0x06004C8D RID: 19597 RVA: 0x001BC380 File Offset: 0x001BA580
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.locked;
		base.serializable = StateMachine.SerializeType.ParamsOnly;
		this.locked.PlayAnim("on", KAnim.PlayMode.Loop).ParamTransition<bool>(this.isUnlocked, this.unlocked, GameStateMachine<POITechItemUnlocks, POITechItemUnlocks.Instance, IStateMachineTarget, POITechItemUnlocks.Def>.IsTrue);
		this.unlocked.ParamTransition<bool>(this.seenNotification, this.unlocked.notify, GameStateMachine<POITechItemUnlocks, POITechItemUnlocks.Instance, IStateMachineTarget, POITechItemUnlocks.Def>.IsFalse).ParamTransition<bool>(this.seenNotification, this.unlocked.done, GameStateMachine<POITechItemUnlocks, POITechItemUnlocks.Instance, IStateMachineTarget, POITechItemUnlocks.Def>.IsTrue);
		this.unlocked.notify.PlayAnim("notify", KAnim.PlayMode.Loop).ToggleStatusItem(Db.Get().MiscStatusItems.AttentionRequired, null).ToggleNotification(delegate(POITechItemUnlocks.Instance smi)
		{
			smi.notificationReference = EventInfoScreen.CreateNotification(POITechItemUnlocks.GenerateEventPopupData(smi), null);
			smi.notificationReference.Type = NotificationType.MessageImportant;
			return smi.notificationReference;
		});
		this.unlocked.done.PlayAnim("off");
	}

	// Token: 0x06004C8E RID: 19598 RVA: 0x001BC468 File Offset: 0x001BA668
	private static void OnNotificationAknowledged(object o)
	{
		GameObject gameObject = (GameObject)o;
		Game.Instance.Trigger(1633134300, gameObject);
	}

	// Token: 0x06004C8F RID: 19599 RVA: 0x001BC48C File Offset: 0x001BA68C
	private static string GetMessageBody(POITechItemUnlocks.Instance smi)
	{
		string text = "";
		foreach (TechItem techItem in smi.unlockTechItems)
		{
			text = text + "\n    • " + techItem.Name;
		}
		return string.Format((smi.def.loreUnlockId != null) ? MISC.NOTIFICATIONS.POIRESEARCHUNLOCKCOMPLETE.MESSAGEBODY : MISC.NOTIFICATIONS.POIRESEARCHUNLOCKCOMPLETE_NOLORE.MESSAGEBODY, text);
	}

	// Token: 0x06004C90 RID: 19600 RVA: 0x001BC514 File Offset: 0x001BA714
	private static EventInfoData GenerateEventPopupData(POITechItemUnlocks.Instance smi)
	{
		EventInfoData eventInfoData = new EventInfoData(MISC.NOTIFICATIONS.POIRESEARCHUNLOCKCOMPLETE.NAME, POITechItemUnlocks.GetMessageBody(smi), smi.def.animName);
		int num = Mathf.Max(2, Components.LiveMinionIdentities.Count);
		GameObject[] array = new GameObject[num];
		using (IEnumerator<MinionIdentity> enumerator = Components.LiveMinionIdentities.Shuffle<MinionIdentity>().GetEnumerator())
		{
			for (int i = 0; i < num; i++)
			{
				if (!enumerator.MoveNext())
				{
					num = 0;
					array = new GameObject[num];
					break;
				}
				array[i] = enumerator.Current.gameObject;
			}
		}
		eventInfoData.minions = array;
		if (smi.def.loreUnlockId != null)
		{
			eventInfoData.AddOption(MISC.NOTIFICATIONS.POIRESEARCHUNLOCKCOMPLETE.BUTTON_VIEW_LORE, null).callback = delegate
			{
				smi.sm.seenNotification.Set(true, smi, false);
				smi.notificationReference = null;
				Game.Instance.unlocks.Unlock(smi.def.loreUnlockId, true);
				ManagementMenu.Instance.OpenCodexToLockId(smi.def.loreUnlockId, false);
				POITechItemUnlocks.OnNotificationAknowledged(smi.gameObject);
			};
		}
		eventInfoData.AddDefaultOption(delegate
		{
			smi.sm.seenNotification.Set(true, smi, false);
			smi.notificationReference = null;
			POITechItemUnlocks.OnNotificationAknowledged(smi.gameObject);
		});
		eventInfoData.clickFocus = smi.gameObject.transform;
		return eventInfoData;
	}

	// Token: 0x040032BE RID: 12990
	public GameStateMachine<POITechItemUnlocks, POITechItemUnlocks.Instance, IStateMachineTarget, POITechItemUnlocks.Def>.State locked;

	// Token: 0x040032BF RID: 12991
	public POITechItemUnlocks.UnlockedStates unlocked;

	// Token: 0x040032C0 RID: 12992
	public StateMachine<POITechItemUnlocks, POITechItemUnlocks.Instance, IStateMachineTarget, POITechItemUnlocks.Def>.BoolParameter isUnlocked;

	// Token: 0x040032C1 RID: 12993
	public StateMachine<POITechItemUnlocks, POITechItemUnlocks.Instance, IStateMachineTarget, POITechItemUnlocks.Def>.BoolParameter pendingChore;

	// Token: 0x040032C2 RID: 12994
	public StateMachine<POITechItemUnlocks, POITechItemUnlocks.Instance, IStateMachineTarget, POITechItemUnlocks.Def>.BoolParameter seenNotification;

	// Token: 0x02001B08 RID: 6920
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04008182 RID: 33154
		public List<string> POITechUnlockIDs;

		// Token: 0x04008183 RID: 33155
		public LocString PopUpName;

		// Token: 0x04008184 RID: 33156
		public string animName;

		// Token: 0x04008185 RID: 33157
		public string loreUnlockId;
	}

	// Token: 0x02001B09 RID: 6921
	public new class Instance : GameStateMachine<POITechItemUnlocks, POITechItemUnlocks.Instance, IStateMachineTarget, POITechItemUnlocks.Def>.GameInstance, ISidescreenButtonControl
	{
		// Token: 0x0600A5E2 RID: 42466 RVA: 0x003AA308 File Offset: 0x003A8508
		public Instance(IStateMachineTarget master, POITechItemUnlocks.Def def)
			: base(master, def)
		{
			this.unlockTechItems = new List<TechItem>(def.POITechUnlockIDs.Count);
			foreach (string text in def.POITechUnlockIDs)
			{
				TechItem techItem = Db.Get().TechItems.TryGet(text);
				if (techItem != null)
				{
					this.unlockTechItems.Add(techItem);
				}
				else
				{
					DebugUtil.DevAssert(false, "Invalid tech item " + text + " for POI Tech Unlock", null);
				}
			}
		}

		// Token: 0x0600A5E3 RID: 42467 RVA: 0x003AA3AC File Offset: 0x003A85AC
		public override void StartSM()
		{
			base.Subscribe(-1503271301, new Action<object>(this.OnBuildingSelect));
			this.UpdateUnlocked();
			base.StartSM();
			if (base.sm.pendingChore.Get(this) && this.unlockChore == null)
			{
				this.CreateChore();
			}
		}

		// Token: 0x0600A5E4 RID: 42468 RVA: 0x003AA3FD File Offset: 0x003A85FD
		public override void StopSM(string reason)
		{
			base.Unsubscribe(-1503271301, new Action<object>(this.OnBuildingSelect));
			base.StopSM(reason);
		}

		// Token: 0x0600A5E5 RID: 42469 RVA: 0x003AA420 File Offset: 0x003A8620
		public void OnBuildingSelect(object obj)
		{
			if (!(bool)obj)
			{
				return;
			}
			if (!base.sm.seenNotification.Get(this) && this.notificationReference != null)
			{
				this.notificationReference.customClickCallback(this.notificationReference.customClickData);
			}
		}

		// Token: 0x0600A5E6 RID: 42470 RVA: 0x003AA46C File Offset: 0x003A866C
		private void ShowPopup()
		{
		}

		// Token: 0x0600A5E7 RID: 42471 RVA: 0x003AA470 File Offset: 0x003A8670
		public void UnlockTechItems()
		{
			foreach (TechItem techItem in this.unlockTechItems)
			{
				if (techItem != null)
				{
					techItem.POIUnlocked();
				}
			}
			MusicManager.instance.PlaySong("Stinger_ResearchComplete", false);
			this.UpdateUnlocked();
		}

		// Token: 0x0600A5E8 RID: 42472 RVA: 0x003AA4DC File Offset: 0x003A86DC
		private void UpdateUnlocked()
		{
			bool flag = true;
			using (List<TechItem>.Enumerator enumerator = this.unlockTechItems.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsComplete())
					{
						flag = false;
						break;
					}
				}
			}
			base.sm.isUnlocked.Set(flag, base.smi, false);
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x0600A5E9 RID: 42473 RVA: 0x003AA550 File Offset: 0x003A8750
		public string SidescreenButtonText
		{
			get
			{
				if (base.sm.isUnlocked.Get(base.smi))
				{
					return UI.USERMENUACTIONS.OPEN_TECHUNLOCKS.ALREADY_RUMMAGED;
				}
				if (this.unlockChore != null)
				{
					return UI.USERMENUACTIONS.OPEN_TECHUNLOCKS.NAME_OFF;
				}
				return UI.USERMENUACTIONS.OPEN_TECHUNLOCKS.NAME;
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x0600A5EA RID: 42474 RVA: 0x003AA5A0 File Offset: 0x003A87A0
		public string SidescreenButtonTooltip
		{
			get
			{
				if (base.sm.isUnlocked.Get(base.smi))
				{
					return UI.USERMENUACTIONS.OPEN_TECHUNLOCKS.TOOLTIP_ALREADYRUMMAGED;
				}
				if (this.unlockChore != null)
				{
					return UI.USERMENUACTIONS.OPEN_TECHUNLOCKS.TOOLTIP_OFF;
				}
				return UI.USERMENUACTIONS.OPEN_TECHUNLOCKS.TOOLTIP;
			}
		}

		// Token: 0x0600A5EB RID: 42475 RVA: 0x003AA5ED File Offset: 0x003A87ED
		public void SetButtonTextOverride(ButtonMenuTextOverride textOverride)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600A5EC RID: 42476 RVA: 0x003AA5F4 File Offset: 0x003A87F4
		public bool SidescreenEnabled()
		{
			return base.smi.IsInsideState(base.sm.locked);
		}

		// Token: 0x0600A5ED RID: 42477 RVA: 0x003AA60C File Offset: 0x003A880C
		public bool SidescreenButtonInteractable()
		{
			return base.smi.IsInsideState(base.sm.locked);
		}

		// Token: 0x0600A5EE RID: 42478 RVA: 0x003AA624 File Offset: 0x003A8824
		public void OnSidescreenButtonPressed()
		{
			if (this.unlockChore == null)
			{
				base.smi.sm.pendingChore.Set(true, base.smi, false);
				base.smi.CreateChore();
				return;
			}
			base.smi.sm.pendingChore.Set(false, base.smi, false);
			base.smi.CancelChore();
		}

		// Token: 0x0600A5EF RID: 42479 RVA: 0x003AA68C File Offset: 0x003A888C
		private void CreateChore()
		{
			Workable component = base.smi.master.GetComponent<POITechItemUnlockWorkable>();
			Prioritizable.AddRef(base.gameObject);
			base.Trigger(1980521255, null);
			this.unlockChore = new WorkChore<POITechItemUnlockWorkable>(Db.Get().ChoreTypes.Research, component, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true);
		}

		// Token: 0x0600A5F0 RID: 42480 RVA: 0x003AA6ED File Offset: 0x003A88ED
		private void CancelChore()
		{
			this.unlockChore.Cancel("UserCancel");
			this.unlockChore = null;
			Prioritizable.RemoveRef(base.gameObject);
			base.Trigger(1980521255, null);
		}

		// Token: 0x0600A5F1 RID: 42481 RVA: 0x003AA71D File Offset: 0x003A891D
		public int HorizontalGroupID()
		{
			return -1;
		}

		// Token: 0x0600A5F2 RID: 42482 RVA: 0x003AA720 File Offset: 0x003A8920
		public int ButtonSideScreenSortOrder()
		{
			return 20;
		}

		// Token: 0x04008186 RID: 33158
		public List<TechItem> unlockTechItems;

		// Token: 0x04008187 RID: 33159
		public Notification notificationReference;

		// Token: 0x04008188 RID: 33160
		private Chore unlockChore;
	}

	// Token: 0x02001B0A RID: 6922
	public class UnlockedStates : GameStateMachine<POITechItemUnlocks, POITechItemUnlocks.Instance, IStateMachineTarget, POITechItemUnlocks.Def>.State
	{
		// Token: 0x04008189 RID: 33161
		public GameStateMachine<POITechItemUnlocks, POITechItemUnlocks.Instance, IStateMachineTarget, POITechItemUnlocks.Def>.State notify;

		// Token: 0x0400818A RID: 33162
		public GameStateMachine<POITechItemUnlocks, POITechItemUnlocks.Instance, IStateMachineTarget, POITechItemUnlocks.Def>.State done;
	}
}
