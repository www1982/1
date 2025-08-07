using System;
using System.Collections.Generic;

// Token: 0x02000D85 RID: 3461
public class NotificationManager : KMonoBehaviour
{
	// Token: 0x1700078D RID: 1933
	// (get) Token: 0x06006BB0 RID: 27568 RVA: 0x0028A6DB File Offset: 0x002888DB
	// (set) Token: 0x06006BB1 RID: 27569 RVA: 0x0028A6E2 File Offset: 0x002888E2
	public static NotificationManager Instance { get; private set; }

	// Token: 0x1400002E RID: 46
	// (add) Token: 0x06006BB2 RID: 27570 RVA: 0x0028A6EC File Offset: 0x002888EC
	// (remove) Token: 0x06006BB3 RID: 27571 RVA: 0x0028A724 File Offset: 0x00288924
	public event Action<Notification> notificationAdded;

	// Token: 0x1400002F RID: 47
	// (add) Token: 0x06006BB4 RID: 27572 RVA: 0x0028A75C File Offset: 0x0028895C
	// (remove) Token: 0x06006BB5 RID: 27573 RVA: 0x0028A794 File Offset: 0x00288994
	public event Action<Notification> notificationRemoved;

	// Token: 0x06006BB6 RID: 27574 RVA: 0x0028A7C9 File Offset: 0x002889C9
	protected override void OnPrefabInit()
	{
		Debug.Assert(NotificationManager.Instance == null);
		NotificationManager.Instance = this;
	}

	// Token: 0x06006BB7 RID: 27575 RVA: 0x0028A7E1 File Offset: 0x002889E1
	protected override void OnForcedCleanUp()
	{
		NotificationManager.Instance = null;
	}

	// Token: 0x06006BB8 RID: 27576 RVA: 0x0028A7E9 File Offset: 0x002889E9
	public void AddNotification(Notification notification)
	{
		this.pendingNotifications.Add(notification);
		if (NotificationScreen.Instance != null)
		{
			NotificationScreen.Instance.AddPendingNotification(notification);
		}
	}

	// Token: 0x06006BB9 RID: 27577 RVA: 0x0028A810 File Offset: 0x00288A10
	public void RemoveNotification(Notification notification)
	{
		this.pendingNotifications.Remove(notification);
		if (NotificationScreen.Instance != null)
		{
			NotificationScreen.Instance.RemovePendingNotification(notification);
		}
		if (this.notifications.Remove(notification))
		{
			this.notificationRemoved(notification);
		}
	}

	// Token: 0x06006BBA RID: 27578 RVA: 0x0028A85C File Offset: 0x00288A5C
	private void Update()
	{
		int i = 0;
		while (i < this.pendingNotifications.Count)
		{
			if (this.pendingNotifications[i].IsReady())
			{
				this.DoAddNotification(this.pendingNotifications[i]);
				this.pendingNotifications.RemoveAt(i);
			}
			else
			{
				i++;
			}
		}
	}

	// Token: 0x06006BBB RID: 27579 RVA: 0x0028A8B2 File Offset: 0x00288AB2
	private void DoAddNotification(Notification notification)
	{
		this.notifications.Add(notification);
		if (this.notificationAdded != null)
		{
			this.notificationAdded(notification);
		}
	}

	// Token: 0x04004961 RID: 18785
	private List<Notification> pendingNotifications = new List<Notification>();

	// Token: 0x04004962 RID: 18786
	private List<Notification> notifications = new List<Notification>();
}
