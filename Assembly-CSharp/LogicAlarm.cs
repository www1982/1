using System;
using System.Collections.Generic;
using KSerialization;
using STRINGS;
using UnityEngine;

// Token: 0x0200075A RID: 1882
[SerializationConfig(MemberSerialization.OptIn)]
[AddComponentMenu("KMonoBehaviour/scripts/LogicAlarm")]
public class LogicAlarm : KMonoBehaviour, ISaveLoadable
{
	// Token: 0x06002FFD RID: 12285 RVA: 0x001131A0 File Offset: 0x001113A0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<LogicAlarm>(-905833192, LogicAlarm.OnCopySettingsDelegate);
	}

	// Token: 0x06002FFE RID: 12286 RVA: 0x001131BC File Offset: 0x001113BC
	private void OnCopySettings(object data)
	{
		LogicAlarm component = ((GameObject)data).GetComponent<LogicAlarm>();
		if (component != null)
		{
			this.notificationName = component.notificationName;
			this.notificationType = component.notificationType;
			this.pauseOnNotify = component.pauseOnNotify;
			this.zoomOnNotify = component.zoomOnNotify;
			this.cooldown = component.cooldown;
			this.notificationTooltip = component.notificationTooltip;
		}
	}

	// Token: 0x06002FFF RID: 12287 RVA: 0x00113228 File Offset: 0x00111428
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.notifier = base.gameObject.AddComponent<Notifier>();
		base.Subscribe<LogicAlarm>(-801688580, LogicAlarm.OnLogicValueChangedDelegate);
		if (string.IsNullOrEmpty(this.notificationName))
		{
			this.notificationName = UI.UISIDESCREENS.LOGICALARMSIDESCREEN.NAME_DEFAULT;
		}
		if (string.IsNullOrEmpty(this.notificationTooltip))
		{
			this.notificationTooltip = UI.UISIDESCREENS.LOGICALARMSIDESCREEN.TOOLTIP_DEFAULT;
		}
		this.UpdateVisualState();
		this.UpdateNotification(false);
	}

	// Token: 0x06003000 RID: 12288 RVA: 0x001132A4 File Offset: 0x001114A4
	private void UpdateVisualState()
	{
		base.GetComponent<KBatchedAnimController>().Play(this.wasOn ? LogicAlarm.ON_ANIMS : LogicAlarm.OFF_ANIMS, KAnim.PlayMode.Once);
	}

	// Token: 0x06003001 RID: 12289 RVA: 0x001132C8 File Offset: 0x001114C8
	public void OnLogicValueChanged(object data)
	{
		LogicValueChanged logicValueChanged = (LogicValueChanged)data;
		if (logicValueChanged.portID != LogicAlarm.INPUT_PORT_ID)
		{
			return;
		}
		int newValue = logicValueChanged.newValue;
		if (LogicCircuitNetwork.IsBitActive(0, newValue))
		{
			if (!this.wasOn)
			{
				this.PushNotification();
				this.wasOn = true;
				if (this.pauseOnNotify && !SpeedControlScreen.Instance.IsPaused)
				{
					SpeedControlScreen.Instance.Pause(false, false);
				}
				if (this.zoomOnNotify)
				{
					GameUtil.FocusCameraOnWorld(base.gameObject.GetMyWorldId(), base.transform.GetPosition(), 8f, null, true);
				}
				this.UpdateVisualState();
				return;
			}
		}
		else if (this.wasOn)
		{
			this.wasOn = false;
			this.UpdateVisualState();
		}
	}

	// Token: 0x06003002 RID: 12290 RVA: 0x0011337A File Offset: 0x0011157A
	private void PushNotification()
	{
		this.notification.Clear();
		this.notifier.Add(this.notification, "");
	}

	// Token: 0x06003003 RID: 12291 RVA: 0x001133A0 File Offset: 0x001115A0
	public void UpdateNotification(bool clear)
	{
		if (this.notification != null && clear)
		{
			this.notification.Clear();
			this.lastNotificationCreated = null;
		}
		if (this.notification != this.lastNotificationCreated || this.lastNotificationCreated == null)
		{
			this.notification = this.CreateNotification();
		}
	}

	// Token: 0x06003004 RID: 12292 RVA: 0x001133F0 File Offset: 0x001115F0
	public Notification CreateNotification()
	{
		base.GetComponent<KSelectable>();
		Notification notification = new Notification(this.notificationName, this.notificationType, (List<Notification> n, object d) => this.notificationTooltip, null, true, 0f, null, null, null, false, false, false);
		this.lastNotificationCreated = notification;
		return notification;
	}

	// Token: 0x04001C9D RID: 7325
	[Serialize]
	public string notificationName;

	// Token: 0x04001C9E RID: 7326
	[Serialize]
	public string notificationTooltip;

	// Token: 0x04001C9F RID: 7327
	[Serialize]
	public NotificationType notificationType;

	// Token: 0x04001CA0 RID: 7328
	[Serialize]
	public bool pauseOnNotify;

	// Token: 0x04001CA1 RID: 7329
	[Serialize]
	public bool zoomOnNotify;

	// Token: 0x04001CA2 RID: 7330
	[Serialize]
	public float cooldown;

	// Token: 0x04001CA3 RID: 7331
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001CA4 RID: 7332
	private bool wasOn;

	// Token: 0x04001CA5 RID: 7333
	private Notifier notifier;

	// Token: 0x04001CA6 RID: 7334
	private Notification notification;

	// Token: 0x04001CA7 RID: 7335
	private Notification lastNotificationCreated;

	// Token: 0x04001CA8 RID: 7336
	private static readonly EventSystem.IntraObjectHandler<LogicAlarm> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<LogicAlarm>(delegate(LogicAlarm component, object data)
	{
		component.OnCopySettings(data);
	});

	// Token: 0x04001CA9 RID: 7337
	private static readonly EventSystem.IntraObjectHandler<LogicAlarm> OnLogicValueChangedDelegate = new EventSystem.IntraObjectHandler<LogicAlarm>(delegate(LogicAlarm component, object data)
	{
		component.OnLogicValueChanged(data);
	});

	// Token: 0x04001CAA RID: 7338
	public static readonly HashedString INPUT_PORT_ID = new HashedString("LogicAlarmInput");

	// Token: 0x04001CAB RID: 7339
	protected static readonly HashedString[] ON_ANIMS = new HashedString[] { "on_pre", "on_loop" };

	// Token: 0x04001CAC RID: 7340
	protected static readonly HashedString[] OFF_ANIMS = new HashedString[] { "on_pst", "off" };
}
