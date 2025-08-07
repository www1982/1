using System;
using KSerialization;
using STRINGS;

// Token: 0x02000D59 RID: 3417
public class ResearchCompleteMessage : Message
{
	// Token: 0x060069F9 RID: 27129 RVA: 0x002804D8 File Offset: 0x0027E6D8
	public ResearchCompleteMessage()
	{
	}

	// Token: 0x060069FA RID: 27130 RVA: 0x002804EB File Offset: 0x0027E6EB
	public ResearchCompleteMessage(Tech tech)
	{
		this.tech.Set(tech);
	}

	// Token: 0x060069FB RID: 27131 RVA: 0x0028050A File Offset: 0x0027E70A
	public override string GetSound()
	{
		return "AI_Notification_ResearchComplete";
	}

	// Token: 0x060069FC RID: 27132 RVA: 0x00280514 File Offset: 0x0027E714
	public override string GetMessageBody()
	{
		Tech tech = this.tech.Get();
		string text = "";
		for (int i = 0; i < tech.unlockedItems.Count; i++)
		{
			if (i != 0)
			{
				text += ", ";
			}
			text += tech.unlockedItems[i].Name;
		}
		return string.Format(MISC.NOTIFICATIONS.RESEARCHCOMPLETE.MESSAGEBODY, tech.Name, text);
	}

	// Token: 0x060069FD RID: 27133 RVA: 0x00280586 File Offset: 0x0027E786
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.RESEARCHCOMPLETE.NAME;
	}

	// Token: 0x060069FE RID: 27134 RVA: 0x00280594 File Offset: 0x0027E794
	public override string GetTooltip()
	{
		Tech tech = this.tech.Get();
		return string.Format(MISC.NOTIFICATIONS.RESEARCHCOMPLETE.TOOLTIP, tech.Name);
	}

	// Token: 0x060069FF RID: 27135 RVA: 0x002805C2 File Offset: 0x0027E7C2
	public override bool IsValid()
	{
		return this.tech.Get() != null;
	}

	// Token: 0x0400485B RID: 18523
	[Serialize]
	private ResourceRef<Tech> tech = new ResourceRef<Tech>();
}
