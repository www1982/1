using System;
using KSerialization;
using STRINGS;

// Token: 0x02000D5B RID: 3419
public class SkillMasteredMessage : Message
{
	// Token: 0x06006A0B RID: 27147 RVA: 0x00280888 File Offset: 0x0027EA88
	public SkillMasteredMessage()
	{
	}

	// Token: 0x06006A0C RID: 27148 RVA: 0x00280890 File Offset: 0x0027EA90
	public SkillMasteredMessage(MinionResume resume)
	{
		this.minionName = resume.GetProperName();
	}

	// Token: 0x06006A0D RID: 27149 RVA: 0x002808A4 File Offset: 0x0027EAA4
	public override string GetSound()
	{
		return "AI_Notification_ResearchComplete";
	}

	// Token: 0x06006A0E RID: 27150 RVA: 0x002808AC File Offset: 0x0027EAAC
	public override string GetMessageBody()
	{
		Debug.Assert(this.minionName != null);
		string text = string.Format(MISC.NOTIFICATIONS.SKILL_POINT_EARNED.LINE, this.minionName);
		return string.Format(MISC.NOTIFICATIONS.SKILL_POINT_EARNED.MESSAGEBODY, text);
	}

	// Token: 0x06006A0F RID: 27151 RVA: 0x002808ED File Offset: 0x0027EAED
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.SKILL_POINT_EARNED.NAME.Replace("{Duplicant}", this.minionName);
	}

	// Token: 0x06006A10 RID: 27152 RVA: 0x00280904 File Offset: 0x0027EB04
	public override string GetTooltip()
	{
		return MISC.NOTIFICATIONS.SKILL_POINT_EARNED.TOOLTIP.Replace("{Duplicant}", this.minionName);
	}

	// Token: 0x06006A11 RID: 27153 RVA: 0x0028091B File Offset: 0x0027EB1B
	public override bool IsValid()
	{
		return this.minionName != null;
	}

	// Token: 0x0400485E RID: 18526
	[Serialize]
	private string minionName;
}
