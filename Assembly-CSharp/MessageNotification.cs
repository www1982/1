using System;
using System.Collections.Generic;

// Token: 0x020005DD RID: 1501
public class MessageNotification : Notification
{
	// Token: 0x060022C5 RID: 8901 RVA: 0x000C7A91 File Offset: 0x000C5C91
	private string OnToolTip(List<Notification> notifications, string tooltipText)
	{
		return tooltipText;
	}

	// Token: 0x060022C6 RID: 8902 RVA: 0x000C7A94 File Offset: 0x000C5C94
	public MessageNotification(Message m)
		: base(m.GetTitle(), NotificationType.Messages, null, null, false, 0f, null, null, null, true, false, true)
	{
		MessageNotification <>4__this = this;
		this.message = m;
		base.Type = m.GetMessageType();
		this.showDismissButton = m.ShowDismissButton();
		if (!this.message.PlayNotificationSound())
		{
			this.playSound = false;
		}
		base.ToolTip = (List<Notification> notifications, object data) => <>4__this.OnToolTip(notifications, m.GetTooltip());
		base.clickFocus = null;
	}

	// Token: 0x04001426 RID: 5158
	public Message message;
}
