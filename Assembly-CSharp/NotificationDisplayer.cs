using System;
using System.Collections.Generic;

// Token: 0x02000D82 RID: 3458
public abstract class NotificationDisplayer : KMonoBehaviour
{
	// Token: 0x06006B9A RID: 27546 RVA: 0x0028A22F File Offset: 0x0028842F
	protected override void OnSpawn()
	{
		this.displayedNotifications = new List<Notification>();
		NotificationManager.Instance.notificationAdded += this.NotificationAdded;
		NotificationManager.Instance.notificationRemoved += this.NotificationRemoved;
	}

	// Token: 0x06006B9B RID: 27547 RVA: 0x0028A268 File Offset: 0x00288468
	public void NotificationAdded(Notification notification)
	{
		if (this.ShouldDisplayNotification(notification))
		{
			this.displayedNotifications.Add(notification);
			this.OnNotificationAdded(notification);
		}
	}

	// Token: 0x06006B9C RID: 27548
	protected abstract void OnNotificationAdded(Notification notification);

	// Token: 0x06006B9D RID: 27549 RVA: 0x0028A286 File Offset: 0x00288486
	public void NotificationRemoved(Notification notification)
	{
		if (this.displayedNotifications.Contains(notification))
		{
			this.displayedNotifications.Remove(notification);
			this.OnNotificationRemoved(notification);
		}
	}

	// Token: 0x06006B9E RID: 27550
	protected abstract void OnNotificationRemoved(Notification notification);

	// Token: 0x06006B9F RID: 27551
	protected abstract bool ShouldDisplayNotification(Notification notification);

	// Token: 0x04004957 RID: 18775
	protected List<Notification> displayedNotifications;
}
