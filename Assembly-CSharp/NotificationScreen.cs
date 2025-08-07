using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D7D RID: 3453
public class NotificationScreen : KScreen
{
	// Token: 0x1700078A RID: 1930
	// (get) Token: 0x06006B64 RID: 27492 RVA: 0x00288D36 File Offset: 0x00286F36
	// (set) Token: 0x06006B65 RID: 27493 RVA: 0x00288D3D File Offset: 0x00286F3D
	public static NotificationScreen Instance { get; private set; }

	// Token: 0x06006B66 RID: 27494 RVA: 0x00288D45 File Offset: 0x00286F45
	public static void DestroyInstance()
	{
		NotificationScreen.Instance = null;
	}

	// Token: 0x06006B67 RID: 27495 RVA: 0x00288D4D File Offset: 0x00286F4D
	public void AddPendingNotification(Notification notification)
	{
		this.pendingNotifications.Add(notification);
	}

	// Token: 0x06006B68 RID: 27496 RVA: 0x00288D5B File Offset: 0x00286F5B
	public void RemovePendingNotification(Notification notification)
	{
		this.dirty = true;
		this.pendingNotifications.Remove(notification);
		this.RemoveNotification(notification);
	}

	// Token: 0x06006B69 RID: 27497 RVA: 0x00288D78 File Offset: 0x00286F78
	public void RemoveNotification(Notification notification)
	{
		NotificationScreen.Entry entry = null;
		this.entriesByMessage.TryGetValue(notification.titleText, out entry);
		if (entry == null)
		{
			return;
		}
		this.notifications.Remove(notification);
		entry.Remove(notification);
		if (entry.notifications.Count == 0)
		{
			global::UnityEngine.Object.Destroy(entry.label);
			this.entriesByMessage[notification.titleText] = null;
			this.entries.Remove(entry);
		}
	}

	// Token: 0x06006B6A RID: 27498 RVA: 0x00288DEC File Offset: 0x00286FEC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		NotificationScreen.Instance = this;
		foreach (NotificationScreen.CustomNotificationPrefabs customNotificationPrefabs in this.customNotificationPrefabs)
		{
			if (customNotificationPrefabs.notificationPrefab != null)
			{
				customNotificationPrefabs.notificationPrefab.SetActive(false);
			}
		}
		this.MessagesPrefab.gameObject.SetActive(false);
		this.LabelPrefab.gameObject.SetActive(false);
		this.InitNotificationSounds();
	}

	// Token: 0x06006B6B RID: 27499 RVA: 0x00288E88 File Offset: 0x00287088
	private void OnNewMessage(object data)
	{
		Message message = (Message)data;
		this.notifier.Add(new MessageNotification(message), "");
	}

	// Token: 0x06006B6C RID: 27500 RVA: 0x00288EB4 File Offset: 0x002870B4
	private void ShowMessage(MessageNotification mn)
	{
		mn.message.OnClick();
		if (mn.message.ShowDialog())
		{
			for (int i = 0; i < this.dialogPrefabs.Count; i++)
			{
				if (this.dialogPrefabs[i].CanDisplay(mn.message))
				{
					if (this.messageDialog != null)
					{
						global::UnityEngine.Object.Destroy(this.messageDialog.gameObject);
						this.messageDialog = null;
					}
					this.messageDialog = global::Util.KInstantiateUI<MessageDialogFrame>(ScreenPrefabs.Instance.MessageDialogFrame.gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject, false);
					MessageDialog messageDialog = global::Util.KInstantiateUI<MessageDialog>(this.dialogPrefabs[i].gameObject, GameScreenManager.Instance.ssOverlayCanvas.gameObject, false);
					this.messageDialog.SetMessage(messageDialog, mn.message);
					this.messageDialog.Show(true);
					break;
				}
			}
		}
		Messenger.Instance.RemoveMessage(mn.message);
		mn.Clear();
	}

	// Token: 0x06006B6D RID: 27501 RVA: 0x00288FC0 File Offset: 0x002871C0
	public void OnClickNextMessage()
	{
		Notification notification2 = this.notifications.Find((Notification notification) => notification.Type == NotificationType.Messages);
		this.ShowMessage((MessageNotification)notification2);
	}

	// Token: 0x06006B6E RID: 27502 RVA: 0x00289004 File Offset: 0x00287204
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.initTime = KTime.Instance.UnscaledGameTime;
		LocText[] array = this.LabelPrefab.GetComponentsInChildren<LocText>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].color = GlobalAssets.Instance.colorSet.NotificationNormal;
		}
		array = this.MessagesPrefab.GetComponentsInChildren<LocText>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].color = GlobalAssets.Instance.colorSet.NotificationNormal;
		}
		base.Subscribe(Messenger.Instance.gameObject, 1558809273, new Action<object>(this.OnNewMessage));
		foreach (Message message in Messenger.Instance.Messages)
		{
			Notification notification = new MessageNotification(message);
			notification.playSound = false;
			this.notifier.Add(notification, "");
		}
	}

	// Token: 0x06006B6F RID: 27503 RVA: 0x00289110 File Offset: 0x00287310
	protected override void OnActivate()
	{
		base.OnActivate();
		this.dirty = true;
	}

	// Token: 0x06006B70 RID: 27504 RVA: 0x00289120 File Offset: 0x00287320
	public void AddNotification(Notification notification)
	{
		if (DebugHandler.NotificationsDisabled)
		{
			return;
		}
		this.notifications.Add(notification);
		NotificationScreen.Entry entry;
		this.entriesByMessage.TryGetValue(notification.titleText, out entry);
		if (entry == null)
		{
			HierarchyReferences hierarchyReferences;
			if (notification.Type == NotificationType.Custom)
			{
				NotificationScreen.CustomNotificationPrefabs customNotificationPrefabs = this.customNotificationPrefabs.Find((NotificationScreen.CustomNotificationPrefabs d) => d.ID == notification.customNotificationID);
				global::Debug.Assert(customNotificationPrefabs != null, "Custom notification prefab not found for notification ID: " + notification.customNotificationID);
				hierarchyReferences = global::Util.KInstantiateUI<HierarchyReferences>(customNotificationPrefabs.notificationPrefab, customNotificationPrefabs.parentFolder, false);
			}
			else if (notification.Type == NotificationType.Messages)
			{
				hierarchyReferences = global::Util.KInstantiateUI<HierarchyReferences>(this.MessagesPrefab, this.MessagesFolder, false);
			}
			else
			{
				hierarchyReferences = global::Util.KInstantiateUI<HierarchyReferences>(this.LabelPrefab, this.LabelsFolder, false);
			}
			Button reference = hierarchyReferences.GetReference<Button>("DismissButton");
			reference.gameObject.SetActive(notification.showDismissButton);
			if (notification.showDismissButton)
			{
				reference.onClick.AddListener(delegate
				{
					NotificationScreen.Entry entry2;
					if (!this.entriesByMessage.TryGetValue(notification.titleText, out entry2))
					{
						return;
					}
					for (int i = entry2.notifications.Count - 1; i >= 0; i--)
					{
						Notification notification2 = entry2.notifications[i];
						MessageNotification messageNotification2 = notification2 as MessageNotification;
						if (messageNotification2 != null)
						{
							Messenger.Instance.RemoveMessage(messageNotification2.message);
						}
						notification2.Clear();
					}
				});
			}
			hierarchyReferences.GetReference<NotificationAnimator>("Animator").Begin(true);
			hierarchyReferences.gameObject.SetActive(true);
			if (notification.ToolTip != null)
			{
				ToolTip tooltip = hierarchyReferences.GetReference<ToolTip>("ToolTip");
				tooltip.OnToolTip = delegate
				{
					tooltip.ClearMultiStringTooltip();
					tooltip.AddMultiStringTooltip(notification.ToolTip(entry.notifications, notification.tooltipData), this.TooltipTextStyle);
					return "";
				};
			}
			KImage reference2 = hierarchyReferences.GetReference<KImage>("Icon");
			LocText reference3 = hierarchyReferences.GetReference<LocText>("Text");
			Button reference4 = hierarchyReferences.GetReference<Button>("MainButton");
			ColorBlock colors = reference4.colors;
			switch (notification.Type)
			{
			case NotificationType.Bad:
			case NotificationType.DuplicantThreatening:
				colors.normalColor = GlobalAssets.Instance.colorSet.NotificationBadBG;
				reference3.color = GlobalAssets.Instance.colorSet.NotificationBad;
				reference2.color = GlobalAssets.Instance.colorSet.NotificationBad;
				reference2.sprite = ((notification.Type == NotificationType.Bad) ? this.icon_bad : this.icon_threatening);
				goto IL_049F;
			case NotificationType.Tutorial:
				colors.normalColor = GlobalAssets.Instance.colorSet.NotificationTutorialBG;
				reference3.color = GlobalAssets.Instance.colorSet.NotificationTutorial;
				reference2.color = GlobalAssets.Instance.colorSet.NotificationTutorial;
				reference2.sprite = this.icon_warning;
				goto IL_049F;
			case NotificationType.Messages:
			{
				colors.normalColor = GlobalAssets.Instance.colorSet.NotificationMessageBG;
				reference3.color = GlobalAssets.Instance.colorSet.NotificationMessage;
				reference2.color = GlobalAssets.Instance.colorSet.NotificationMessage;
				reference2.sprite = this.icon_message;
				MessageNotification messageNotification = notification as MessageNotification;
				if (messageNotification == null)
				{
					goto IL_049F;
				}
				TutorialMessage tutorialMessage = messageNotification.message as TutorialMessage;
				if (tutorialMessage != null && !string.IsNullOrEmpty(tutorialMessage.videoClipId))
				{
					reference2.sprite = this.icon_video;
					goto IL_049F;
				}
				goto IL_049F;
			}
			case NotificationType.Event:
				colors.normalColor = GlobalAssets.Instance.colorSet.NotificationEventBG;
				reference3.color = GlobalAssets.Instance.colorSet.NotificationEvent;
				reference2.color = GlobalAssets.Instance.colorSet.NotificationEvent;
				reference2.sprite = this.icon_event;
				goto IL_049F;
			case NotificationType.MessageImportant:
				colors.normalColor = GlobalAssets.Instance.colorSet.NotificationMessageImportantBG;
				reference3.color = GlobalAssets.Instance.colorSet.NotificationMessageImportant;
				reference2.color = GlobalAssets.Instance.colorSet.NotificationMessageImportant;
				reference2.sprite = this.icon_message_important;
				goto IL_049F;
			case NotificationType.Custom:
				goto IL_049F;
			}
			colors.normalColor = GlobalAssets.Instance.colorSet.NotificationNormalBG;
			reference3.color = GlobalAssets.Instance.colorSet.NotificationNormal;
			reference2.color = GlobalAssets.Instance.colorSet.NotificationNormal;
			reference2.sprite = this.icon_normal;
			IL_049F:
			reference4.colors = colors;
			reference4.onClick.AddListener(delegate
			{
				this.OnClick(entry);
			});
			string text = "";
			if (KTime.Instance.UnscaledGameTime - this.initTime > 5f && notification.playSound)
			{
				this.PlayDingSound(notification, 0);
			}
			else
			{
				text = "too early";
			}
			if (AudioDebug.Get().debugNotificationSounds)
			{
				global::Debug.Log("Notification(" + notification.titleText + "):" + text);
			}
			entry = new NotificationScreen.Entry(hierarchyReferences.gameObject);
			this.entriesByMessage[notification.titleText] = entry;
			this.entries.Add(entry);
		}
		entry.Add(notification);
		this.dirty = true;
		this.SortNotifications();
	}

	// Token: 0x06006B71 RID: 27505 RVA: 0x002896B8 File Offset: 0x002878B8
	private void SortNotifications()
	{
		this.notifications.Sort(delegate(Notification n1, Notification n2)
		{
			if (n1.Type == n2.Type)
			{
				return n1.Idx - n2.Idx;
			}
			return n1.Type - n2.Type;
		});
		foreach (Notification notification in this.notifications)
		{
			NotificationScreen.Entry entry = null;
			this.entriesByMessage.TryGetValue(notification.titleText, out entry);
			if (entry != null)
			{
				entry.label.GetComponent<RectTransform>().SetAsLastSibling();
			}
		}
	}

	// Token: 0x06006B72 RID: 27506 RVA: 0x00289758 File Offset: 0x00287958
	private void PlayDingSound(Notification notification, int count)
	{
		string text;
		if (!this.notificationSounds.TryGetValue(notification.Type, out text))
		{
			text = "Notification";
		}
		float num;
		if (!this.timeOfLastNotification.TryGetValue(text, out num))
		{
			num = 0f;
		}
		float num2 = (notification.volume_attenuation ? ((Time.time - num) / this.soundDecayTime) : 1f);
		this.timeOfLastNotification[text] = Time.time;
		string text2;
		if (count > 1)
		{
			text2 = GlobalAssets.GetSound(text + "_AddCount", true);
			if (text2 == null)
			{
				text2 = GlobalAssets.GetSound(text, false);
			}
		}
		else
		{
			text2 = GlobalAssets.GetSound(text, false);
		}
		if (notification.playSound)
		{
			EventInstance eventInstance = KFMOD.BeginOneShot(text2, Vector3.zero, 1f);
			eventInstance.setParameterByName("timeSinceLast", num2, false);
			KFMOD.EndOneShot(eventInstance);
		}
	}

	// Token: 0x06006B73 RID: 27507 RVA: 0x00289824 File Offset: 0x00287A24
	private void Update()
	{
		int i = 0;
		while (i < this.pendingNotifications.Count)
		{
			if (this.pendingNotifications[i].IsReady())
			{
				this.AddNotification(this.pendingNotifications[i]);
				this.pendingNotifications.RemoveAt(i);
			}
			else
			{
				i++;
			}
		}
		int num = 0;
		int num2 = 0;
		for (int j = 0; j < this.notifications.Count; j++)
		{
			Notification notification = this.notifications[j];
			if (notification.Type == NotificationType.Messages)
			{
				num2++;
			}
			else
			{
				num++;
			}
			if (notification.expires && KTime.Instance.UnscaledGameTime - notification.Time > this.lifetime)
			{
				this.dirty = true;
				if (notification.Notifier == null)
				{
					this.RemovePendingNotification(notification);
				}
				else
				{
					notification.Clear();
				}
			}
		}
	}

	// Token: 0x06006B74 RID: 27508 RVA: 0x00289900 File Offset: 0x00287B00
	private void OnClick(NotificationScreen.Entry entry)
	{
		Notification nextClickedNotification = entry.NextClickedNotification;
		base.PlaySound3D(GlobalAssets.GetSound("HUD_Click_Open", false));
		if (nextClickedNotification.customClickCallback != null)
		{
			nextClickedNotification.customClickCallback(nextClickedNotification.customClickData);
		}
		else
		{
			if (nextClickedNotification.clickFocus != null)
			{
				Vector3 position = nextClickedNotification.clickFocus.GetPosition();
				position.z = -40f;
				ClusterGridEntity component = nextClickedNotification.clickFocus.GetComponent<ClusterGridEntity>();
				KSelectable component2 = nextClickedNotification.clickFocus.GetComponent<KSelectable>();
				int myWorldId = nextClickedNotification.clickFocus.gameObject.GetMyWorldId();
				if (myWorldId != -1)
				{
					GameUtil.FocusCameraOnWorld(myWorldId, position, 10f, null, true);
				}
				else if (DlcManager.FeatureClusterSpaceEnabled() && component != null && component.IsVisible)
				{
					ManagementMenu.Instance.OpenClusterMap();
					ClusterMapScreen.Instance.SetTargetFocusPosition(component.Location, 0.5f);
				}
				if (component2 != null)
				{
					if (DlcManager.FeatureClusterSpaceEnabled() && component != null && component.IsVisible)
					{
						ClusterMapSelectTool.Instance.Select(component2, false);
					}
					else
					{
						SelectTool.Instance.Select(component2, false);
					}
				}
			}
			else if (nextClickedNotification.Notifier != null)
			{
				SelectTool.Instance.Select(nextClickedNotification.Notifier.GetComponent<KSelectable>(), false);
			}
			if (nextClickedNotification.Type == NotificationType.Messages)
			{
				this.ShowMessage((MessageNotification)nextClickedNotification);
			}
		}
		if (nextClickedNotification.clearOnClick)
		{
			nextClickedNotification.Clear();
		}
	}

	// Token: 0x06006B75 RID: 27509 RVA: 0x00289A67 File Offset: 0x00287C67
	private void PositionLocatorIcon()
	{
	}

	// Token: 0x06006B76 RID: 27510 RVA: 0x00289A6C File Offset: 0x00287C6C
	private void InitNotificationSounds()
	{
		this.notificationSounds[NotificationType.Good] = "Notification";
		this.notificationSounds[NotificationType.BadMinor] = "Notification";
		this.notificationSounds[NotificationType.Bad] = "Warning";
		this.notificationSounds[NotificationType.Neutral] = "Notification";
		this.notificationSounds[NotificationType.Tutorial] = "Notification";
		this.notificationSounds[NotificationType.Messages] = "Message";
		this.notificationSounds[NotificationType.DuplicantThreatening] = "Warning_DupeThreatening";
		this.notificationSounds[NotificationType.Event] = "Message";
		this.notificationSounds[NotificationType.MessageImportant] = "Message_Important";
	}

	// Token: 0x06006B77 RID: 27511 RVA: 0x00289B14 File Offset: 0x00287D14
	public Sprite GetNotificationIcon(NotificationType type)
	{
		switch (type)
		{
		case NotificationType.Bad:
			return this.icon_bad;
		case NotificationType.Tutorial:
			return this.icon_warning;
		case NotificationType.Messages:
			return this.icon_message;
		case NotificationType.DuplicantThreatening:
			return this.icon_threatening;
		case NotificationType.Event:
			return this.icon_event;
		case NotificationType.MessageImportant:
			return this.icon_message_important;
		}
		return this.icon_normal;
	}

	// Token: 0x06006B78 RID: 27512 RVA: 0x00289B80 File Offset: 0x00287D80
	public Color GetNotificationColour(NotificationType type)
	{
		switch (type)
		{
		case NotificationType.Bad:
			return GlobalAssets.Instance.colorSet.NotificationBad;
		case NotificationType.Tutorial:
			return GlobalAssets.Instance.colorSet.NotificationTutorial;
		case NotificationType.Messages:
			return GlobalAssets.Instance.colorSet.NotificationMessage;
		case NotificationType.DuplicantThreatening:
			return GlobalAssets.Instance.colorSet.NotificationBad;
		case NotificationType.Event:
			return GlobalAssets.Instance.colorSet.NotificationEvent;
		case NotificationType.MessageImportant:
			return GlobalAssets.Instance.colorSet.NotificationMessageImportant;
		}
		return GlobalAssets.Instance.colorSet.NotificationNormal;
	}

	// Token: 0x06006B79 RID: 27513 RVA: 0x00289C50 File Offset: 0x00287E50
	public Color GetNotificationBGColour(NotificationType type)
	{
		switch (type)
		{
		case NotificationType.Bad:
			return GlobalAssets.Instance.colorSet.NotificationBadBG;
		case NotificationType.Tutorial:
			return GlobalAssets.Instance.colorSet.NotificationTutorialBG;
		case NotificationType.Messages:
			return GlobalAssets.Instance.colorSet.NotificationMessageBG;
		case NotificationType.DuplicantThreatening:
			return GlobalAssets.Instance.colorSet.NotificationBadBG;
		case NotificationType.Event:
			return GlobalAssets.Instance.colorSet.NotificationEventBG;
		case NotificationType.MessageImportant:
			return GlobalAssets.Instance.colorSet.NotificationMessageImportantBG;
		}
		return GlobalAssets.Instance.colorSet.NotificationNormalBG;
	}

	// Token: 0x06006B7A RID: 27514 RVA: 0x00289D1D File Offset: 0x00287F1D
	public string GetNotificationSound(NotificationType type)
	{
		return this.notificationSounds[type];
	}

	// Token: 0x0400491D RID: 18717
	public float lifetime;

	// Token: 0x0400491E RID: 18718
	public bool dirty;

	// Token: 0x0400491F RID: 18719
	public GameObject LabelPrefab;

	// Token: 0x04004920 RID: 18720
	public GameObject LabelsFolder;

	// Token: 0x04004921 RID: 18721
	public GameObject MessagesPrefab;

	// Token: 0x04004922 RID: 18722
	public GameObject MessagesFolder;

	// Token: 0x04004923 RID: 18723
	public List<NotificationScreen.CustomNotificationPrefabs> customNotificationPrefabs;

	// Token: 0x04004924 RID: 18724
	private MessageDialogFrame messageDialog;

	// Token: 0x04004925 RID: 18725
	private float initTime;

	// Token: 0x04004926 RID: 18726
	[MyCmpAdd]
	private Notifier notifier;

	// Token: 0x04004927 RID: 18727
	[SerializeField]
	private List<MessageDialog> dialogPrefabs = new List<MessageDialog>();

	// Token: 0x04004928 RID: 18728
	[SerializeField]
	private Color badColorBG;

	// Token: 0x04004929 RID: 18729
	[SerializeField]
	private Color badColor = Color.red;

	// Token: 0x0400492A RID: 18730
	[SerializeField]
	private Color normalColorBG;

	// Token: 0x0400492B RID: 18731
	[SerializeField]
	private Color normalColor = Color.white;

	// Token: 0x0400492C RID: 18732
	[SerializeField]
	private Color warningColorBG;

	// Token: 0x0400492D RID: 18733
	[SerializeField]
	private Color warningColor;

	// Token: 0x0400492E RID: 18734
	[SerializeField]
	private Color messageColorBG;

	// Token: 0x0400492F RID: 18735
	[SerializeField]
	private Color messageColor;

	// Token: 0x04004930 RID: 18736
	[SerializeField]
	private Color messageImportantColorBG;

	// Token: 0x04004931 RID: 18737
	[SerializeField]
	private Color messageImportantColor;

	// Token: 0x04004932 RID: 18738
	[SerializeField]
	private Color eventColorBG;

	// Token: 0x04004933 RID: 18739
	[SerializeField]
	private Color eventColor;

	// Token: 0x04004934 RID: 18740
	public Sprite icon_normal;

	// Token: 0x04004935 RID: 18741
	public Sprite icon_warning;

	// Token: 0x04004936 RID: 18742
	public Sprite icon_bad;

	// Token: 0x04004937 RID: 18743
	public Sprite icon_threatening;

	// Token: 0x04004938 RID: 18744
	public Sprite icon_message;

	// Token: 0x04004939 RID: 18745
	public Sprite icon_message_important;

	// Token: 0x0400493A RID: 18746
	public Sprite icon_video;

	// Token: 0x0400493B RID: 18747
	public Sprite icon_event;

	// Token: 0x0400493C RID: 18748
	private List<Notification> pendingNotifications = new List<Notification>();

	// Token: 0x0400493D RID: 18749
	private List<Notification> notifications = new List<Notification>();

	// Token: 0x0400493E RID: 18750
	public TextStyleSetting TooltipTextStyle;

	// Token: 0x0400493F RID: 18751
	private Dictionary<NotificationType, string> notificationSounds = new Dictionary<NotificationType, string>();

	// Token: 0x04004940 RID: 18752
	private Dictionary<string, float> timeOfLastNotification = new Dictionary<string, float>();

	// Token: 0x04004941 RID: 18753
	private float soundDecayTime = 10f;

	// Token: 0x04004942 RID: 18754
	private List<NotificationScreen.Entry> entries = new List<NotificationScreen.Entry>();

	// Token: 0x04004943 RID: 18755
	private Dictionary<string, NotificationScreen.Entry> entriesByMessage = new Dictionary<string, NotificationScreen.Entry>();

	// Token: 0x02001F66 RID: 8038
	[Serializable]
	public class CustomNotificationPrefabs
	{
		// Token: 0x040090B1 RID: 37041
		public string ID;

		// Token: 0x040090B2 RID: 37042
		public GameObject notificationPrefab;

		// Token: 0x040090B3 RID: 37043
		public GameObject parentFolder;
	}

	// Token: 0x02001F67 RID: 8039
	private class Entry
	{
		// Token: 0x0600B329 RID: 45865 RVA: 0x003D90EA File Offset: 0x003D72EA
		public Entry(GameObject label)
		{
			this.label = label;
		}

		// Token: 0x0600B32A RID: 45866 RVA: 0x003D9104 File Offset: 0x003D7304
		public void Add(Notification notification)
		{
			this.notifications.Add(notification);
			this.UpdateMessage(notification, true);
		}

		// Token: 0x0600B32B RID: 45867 RVA: 0x003D911A File Offset: 0x003D731A
		public void Remove(Notification notification)
		{
			this.notifications.Remove(notification);
			this.UpdateMessage(notification, false);
		}

		// Token: 0x0600B32C RID: 45868 RVA: 0x003D9134 File Offset: 0x003D7334
		public void UpdateMessage(Notification notification, bool playSound = true)
		{
			if (Game.IsQuitting())
			{
				return;
			}
			this.message = notification.titleText;
			if (this.notifications.Count > 1)
			{
				if (playSound && (notification.Type == NotificationType.Bad || notification.Type == NotificationType.DuplicantThreatening))
				{
					NotificationScreen.Instance.PlayDingSound(notification, this.notifications.Count);
				}
				this.message = this.message + " (" + this.notifications.Count.ToString() + ")";
			}
			if (this.label != null)
			{
				this.label.GetComponent<HierarchyReferences>().GetReference<LocText>("Text").text = this.message;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x0600B32D RID: 45869 RVA: 0x003D91EC File Offset: 0x003D73EC
		public Notification NextClickedNotification
		{
			get
			{
				List<Notification> list = this.notifications;
				int num = this.clickIdx;
				this.clickIdx = num + 1;
				return list[num % this.notifications.Count];
			}
		}

		// Token: 0x040090B4 RID: 37044
		public string message;

		// Token: 0x040090B5 RID: 37045
		public int clickIdx;

		// Token: 0x040090B6 RID: 37046
		public GameObject label;

		// Token: 0x040090B7 RID: 37047
		public List<Notification> notifications = new List<Notification>();
	}
}
