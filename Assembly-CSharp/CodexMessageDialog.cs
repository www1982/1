using System;
using UnityEngine;

// Token: 0x02000D4A RID: 3402
public class CodexMessageDialog : MessageDialog
{
	// Token: 0x06006984 RID: 27012 RVA: 0x0027F92A File Offset: 0x0027DB2A
	public override bool CanDisplay(Message message)
	{
		return typeof(CodexUnlockedMessage).IsAssignableFrom(message.GetType());
	}

	// Token: 0x06006985 RID: 27013 RVA: 0x0027F941 File Offset: 0x0027DB41
	public override void SetMessage(Message base_message)
	{
		this.message = (CodexUnlockedMessage)base_message;
		this.description.text = this.message.GetMessageBody();
	}

	// Token: 0x06006986 RID: 27014 RVA: 0x0027F965 File Offset: 0x0027DB65
	public override void OnClickAction()
	{
	}

	// Token: 0x06006987 RID: 27015 RVA: 0x0027F967 File Offset: 0x0027DB67
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		this.message.OnCleanUp();
	}

	// Token: 0x04004834 RID: 18484
	[SerializeField]
	private LocText description;

	// Token: 0x04004835 RID: 18485
	private CodexUnlockedMessage message;
}
