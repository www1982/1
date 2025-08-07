using System;

// Token: 0x02000D84 RID: 3460
public class NotificationHighlightTarget : KMonoBehaviour
{
	// Token: 0x06006BAC RID: 27564 RVA: 0x0028A681 File Offset: 0x00288881
	protected void OnEnable()
	{
		this.controller = base.GetComponentInParent<NotificationHighlightController>();
		if (this.controller != null)
		{
			this.controller.AddTarget(this);
		}
	}

	// Token: 0x06006BAD RID: 27565 RVA: 0x0028A6A9 File Offset: 0x002888A9
	protected override void OnDisable()
	{
		if (this.controller != null)
		{
			this.controller.RemoveTarget(this);
		}
	}

	// Token: 0x06006BAE RID: 27566 RVA: 0x0028A6C5 File Offset: 0x002888C5
	public void View()
	{
		base.GetComponentInParent<NotificationHighlightController>().TargetViewed(this);
	}

	// Token: 0x0400495C RID: 18780
	public string targetKey;

	// Token: 0x0400495D RID: 18781
	private NotificationHighlightController controller;
}
