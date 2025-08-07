using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000D80 RID: 3456
public class ManagementScreenNotificationOverlay : KMonoBehaviour
{
	// Token: 0x06006B91 RID: 27537 RVA: 0x0028A10C File Offset: 0x0028830C
	protected void OnEnable()
	{
	}

	// Token: 0x06006B92 RID: 27538 RVA: 0x0028A10E File Offset: 0x0028830E
	protected override void OnDisable()
	{
	}

	// Token: 0x06006B93 RID: 27539 RVA: 0x0028A110 File Offset: 0x00288310
	private NotificationAlertBar CreateAlertBar(ManagementMenuNotification notification)
	{
		NotificationAlertBar notificationAlertBar = Util.KInstantiateUI<NotificationAlertBar>(this.alertBarPrefab.gameObject, this.alertContainer.gameObject, false);
		notificationAlertBar.Init(notification);
		notificationAlertBar.gameObject.SetActive(true);
		return notificationAlertBar;
	}

	// Token: 0x06006B94 RID: 27540 RVA: 0x0028A141 File Offset: 0x00288341
	private void NotificationsChanged()
	{
	}

	// Token: 0x0400494C RID: 18764
	public global::Action currentMenu;

	// Token: 0x0400494D RID: 18765
	public NotificationAlertBar alertBarPrefab;

	// Token: 0x0400494E RID: 18766
	public RectTransform alertContainer;

	// Token: 0x0400494F RID: 18767
	private List<NotificationAlertBar> alertBars = new List<NotificationAlertBar>();
}
