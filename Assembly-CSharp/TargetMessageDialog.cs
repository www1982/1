using System;
using UnityEngine;

// Token: 0x02000D5E RID: 3422
public class TargetMessageDialog : MessageDialog
{
	// Token: 0x06006A1A RID: 27162 RVA: 0x00280997 File Offset: 0x0027EB97
	public override bool CanDisplay(Message message)
	{
		return typeof(TargetMessage).IsAssignableFrom(message.GetType());
	}

	// Token: 0x06006A1B RID: 27163 RVA: 0x002809AE File Offset: 0x0027EBAE
	public override void SetMessage(Message base_message)
	{
		this.message = (TargetMessage)base_message;
		this.description.text = this.message.GetMessageBody();
	}

	// Token: 0x06006A1C RID: 27164 RVA: 0x002809D4 File Offset: 0x0027EBD4
	public override void OnClickAction()
	{
		MessageTarget target = this.message.GetTarget();
		SelectTool.Instance.SelectAndFocus(target.GetPosition(), target.GetSelectable());
	}

	// Token: 0x06006A1D RID: 27165 RVA: 0x00280A03 File Offset: 0x0027EC03
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.message.OnCleanUp();
	}

	// Token: 0x04004862 RID: 18530
	[SerializeField]
	private LocText description;

	// Token: 0x04004863 RID: 18531
	private TargetMessage message;
}
