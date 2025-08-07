using System;
using UnityEngine;

// Token: 0x02000D5C RID: 3420
public class StandardMessageDialog : MessageDialog
{
	// Token: 0x06006A12 RID: 27154 RVA: 0x00280926 File Offset: 0x0027EB26
	public override bool CanDisplay(Message message)
	{
		return typeof(Message).IsAssignableFrom(message.GetType());
	}

	// Token: 0x06006A13 RID: 27155 RVA: 0x0028093D File Offset: 0x0027EB3D
	public override void SetMessage(Message base_message)
	{
		this.message = base_message;
		this.description.text = this.message.GetMessageBody();
	}

	// Token: 0x06006A14 RID: 27156 RVA: 0x0028095C File Offset: 0x0027EB5C
	public override void OnClickAction()
	{
	}

	// Token: 0x0400485F RID: 18527
	[SerializeField]
	private LocText description;

	// Token: 0x04004860 RID: 18528
	private Message message;
}
