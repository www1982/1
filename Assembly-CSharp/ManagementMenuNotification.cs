using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005EE RID: 1518
public class ManagementMenuNotification : Notification
{
	// Token: 0x17000174 RID: 372
	// (get) Token: 0x06002387 RID: 9095 RVA: 0x000CB58D File Offset: 0x000C978D
	// (set) Token: 0x06002388 RID: 9096 RVA: 0x000CB595 File Offset: 0x000C9795
	public bool hasBeenViewed { get; private set; }

	// Token: 0x17000175 RID: 373
	// (get) Token: 0x06002389 RID: 9097 RVA: 0x000CB59E File Offset: 0x000C979E
	// (set) Token: 0x0600238A RID: 9098 RVA: 0x000CB5A6 File Offset: 0x000C97A6
	public string highlightTarget { get; set; }

	// Token: 0x0600238B RID: 9099 RVA: 0x000CB5B0 File Offset: 0x000C97B0
	public ManagementMenuNotification(global::Action targetMenu, NotificationValence valence, string highlightTarget, string title, NotificationType type, Func<List<Notification>, object, string> tooltip = null, object tooltip_data = null, bool expires = true, float delay = 0f, Notification.ClickCallback custom_click_callback = null, object custom_click_data = null, Transform click_focus = null, bool volume_attenuation = true)
		: base(title, type, tooltip, tooltip_data, expires, delay, custom_click_callback, custom_click_data, click_focus, volume_attenuation, false, false)
	{
		this.targetMenu = targetMenu;
		this.valence = valence;
		this.highlightTarget = highlightTarget;
	}

	// Token: 0x0600238C RID: 9100 RVA: 0x000CB5EE File Offset: 0x000C97EE
	public void View()
	{
		this.hasBeenViewed = true;
		ManagementMenu.Instance.notificationDisplayer.NotificationWasViewed(this);
	}

	// Token: 0x040014AE RID: 5294
	public global::Action targetMenu;

	// Token: 0x040014AF RID: 5295
	public NotificationValence valence;
}
