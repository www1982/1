using System;
using KSerialization;

// Token: 0x02000D52 RID: 3410
[SerializationConfig(MemberSerialization.OptIn)]
public abstract class Message : ISaveLoadable
{
	// Token: 0x060069BD RID: 27069
	public abstract string GetTitle();

	// Token: 0x060069BE RID: 27070
	public abstract string GetSound();

	// Token: 0x060069BF RID: 27071
	public abstract string GetMessageBody();

	// Token: 0x060069C0 RID: 27072
	public abstract string GetTooltip();

	// Token: 0x060069C1 RID: 27073 RVA: 0x0027FD31 File Offset: 0x0027DF31
	public virtual bool ShowDialog()
	{
		return true;
	}

	// Token: 0x060069C2 RID: 27074 RVA: 0x0027FD34 File Offset: 0x0027DF34
	public virtual void OnCleanUp()
	{
	}

	// Token: 0x060069C3 RID: 27075 RVA: 0x0027FD36 File Offset: 0x0027DF36
	public virtual bool IsValid()
	{
		return true;
	}

	// Token: 0x060069C4 RID: 27076 RVA: 0x0027FD39 File Offset: 0x0027DF39
	public virtual bool PlayNotificationSound()
	{
		return true;
	}

	// Token: 0x060069C5 RID: 27077 RVA: 0x0027FD3C File Offset: 0x0027DF3C
	public virtual void OnClick()
	{
	}

	// Token: 0x060069C6 RID: 27078 RVA: 0x0027FD3E File Offset: 0x0027DF3E
	public virtual NotificationType GetMessageType()
	{
		return NotificationType.Messages;
	}

	// Token: 0x060069C7 RID: 27079 RVA: 0x0027FD41 File Offset: 0x0027DF41
	public virtual bool ShowDismissButton()
	{
		return true;
	}
}
