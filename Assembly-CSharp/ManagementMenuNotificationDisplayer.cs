using System;
using System.Collections.Generic;

// Token: 0x02000D7F RID: 3455
public class ManagementMenuNotificationDisplayer : NotificationDisplayer
{
	// Token: 0x1700078C RID: 1932
	// (get) Token: 0x06006B86 RID: 27526 RVA: 0x00289FB6 File Offset: 0x002881B6
	// (set) Token: 0x06006B87 RID: 27527 RVA: 0x00289FBE File Offset: 0x002881BE
	public List<ManagementMenuNotification> displayedManagementMenuNotifications { get; private set; }

	// Token: 0x1400002D RID: 45
	// (add) Token: 0x06006B88 RID: 27528 RVA: 0x00289FC8 File Offset: 0x002881C8
	// (remove) Token: 0x06006B89 RID: 27529 RVA: 0x0028A000 File Offset: 0x00288200
	public event global::System.Action onNotificationsChanged;

	// Token: 0x06006B8A RID: 27530 RVA: 0x0028A035 File Offset: 0x00288235
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.displayedManagementMenuNotifications = new List<ManagementMenuNotification>();
	}

	// Token: 0x06006B8B RID: 27531 RVA: 0x0028A048 File Offset: 0x00288248
	public void NotificationWasViewed(ManagementMenuNotification notification)
	{
		this.onNotificationsChanged();
	}

	// Token: 0x06006B8C RID: 27532 RVA: 0x0028A055 File Offset: 0x00288255
	protected override void OnNotificationAdded(Notification notification)
	{
		this.displayedManagementMenuNotifications.Add(notification as ManagementMenuNotification);
		this.onNotificationsChanged();
	}

	// Token: 0x06006B8D RID: 27533 RVA: 0x0028A073 File Offset: 0x00288273
	protected override void OnNotificationRemoved(Notification notification)
	{
		this.displayedManagementMenuNotifications.Remove(notification as ManagementMenuNotification);
		this.onNotificationsChanged();
	}

	// Token: 0x06006B8E RID: 27534 RVA: 0x0028A092 File Offset: 0x00288292
	protected override bool ShouldDisplayNotification(Notification notification)
	{
		return notification is ManagementMenuNotification;
	}

	// Token: 0x06006B8F RID: 27535 RVA: 0x0028A0A0 File Offset: 0x002882A0
	public List<ManagementMenuNotification> GetNotificationsForAction(global::Action hotKey)
	{
		List<ManagementMenuNotification> list = new List<ManagementMenuNotification>();
		foreach (ManagementMenuNotification managementMenuNotification in this.displayedManagementMenuNotifications)
		{
			if (managementMenuNotification.targetMenu == hotKey)
			{
				list.Add(managementMenuNotification);
			}
		}
		return list;
	}
}
