using System;
using System.Collections.Generic;

// Token: 0x02000D81 RID: 3457
public class NotificationAlertBar : KMonoBehaviour
{
	// Token: 0x06006B96 RID: 27542 RVA: 0x0028A158 File Offset: 0x00288358
	public void Init(ManagementMenuNotification notification)
	{
		this.notification = notification;
		this.thisButton.onClick += this.OnThisButtonClicked;
		this.background.colorStyleSetting = this.alertColorStyle[(int)notification.valence];
		this.background.ApplyColorStyleSetting();
		this.text.text = notification.titleText;
		this.tooltip.SetSimpleTooltip(notification.ToolTip(null, notification.tooltipData));
		this.muteButton.onClick += this.OnMuteButtonClicked;
	}

	// Token: 0x06006B97 RID: 27543 RVA: 0x0028A1F0 File Offset: 0x002883F0
	private void OnThisButtonClicked()
	{
		NotificationHighlightController componentInParent = base.GetComponentInParent<NotificationHighlightController>();
		if (componentInParent != null)
		{
			componentInParent.SetActiveTarget(this.notification);
			return;
		}
		this.notification.View();
	}

	// Token: 0x06006B98 RID: 27544 RVA: 0x0028A225 File Offset: 0x00288425
	private void OnMuteButtonClicked()
	{
	}

	// Token: 0x04004950 RID: 18768
	public ManagementMenuNotification notification;

	// Token: 0x04004951 RID: 18769
	public KButton thisButton;

	// Token: 0x04004952 RID: 18770
	public KImage background;

	// Token: 0x04004953 RID: 18771
	public LocText text;

	// Token: 0x04004954 RID: 18772
	public ToolTip tooltip;

	// Token: 0x04004955 RID: 18773
	public KButton muteButton;

	// Token: 0x04004956 RID: 18774
	public List<ColorStyleSetting> alertColorStyle;
}
