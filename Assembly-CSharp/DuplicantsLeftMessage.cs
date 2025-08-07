using System;
using STRINGS;

// Token: 0x02000D4E RID: 3406
public class DuplicantsLeftMessage : Message
{
	// Token: 0x060069A1 RID: 27041 RVA: 0x0027FAE7 File Offset: 0x0027DCE7
	public override string GetSound()
	{
		return "";
	}

	// Token: 0x060069A2 RID: 27042 RVA: 0x0027FAEE File Offset: 0x0027DCEE
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.DUPLICANTABSORBED.NAME;
	}

	// Token: 0x060069A3 RID: 27043 RVA: 0x0027FAFA File Offset: 0x0027DCFA
	public override string GetMessageBody()
	{
		return MISC.NOTIFICATIONS.DUPLICANTABSORBED.MESSAGEBODY;
	}

	// Token: 0x060069A4 RID: 27044 RVA: 0x0027FB06 File Offset: 0x0027DD06
	public override string GetTooltip()
	{
		return MISC.NOTIFICATIONS.DUPLICANTABSORBED.TOOLTIP;
	}
}
