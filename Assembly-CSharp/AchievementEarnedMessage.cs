using System;
using STRINGS;

// Token: 0x02000D49 RID: 3401
public class AchievementEarnedMessage : Message
{
	// Token: 0x0600697C RID: 27004 RVA: 0x0027F8D0 File Offset: 0x0027DAD0
	public override bool ShowDialog()
	{
		return false;
	}

	// Token: 0x0600697D RID: 27005 RVA: 0x0027F8D3 File Offset: 0x0027DAD3
	public override string GetSound()
	{
		return "AI_Notification_ResearchComplete";
	}

	// Token: 0x0600697E RID: 27006 RVA: 0x0027F8DA File Offset: 0x0027DADA
	public override string GetMessageBody()
	{
		return "";
	}

	// Token: 0x0600697F RID: 27007 RVA: 0x0027F8E1 File Offset: 0x0027DAE1
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.COLONY_ACHIEVEMENT_EARNED.NAME;
	}

	// Token: 0x06006980 RID: 27008 RVA: 0x0027F8ED File Offset: 0x0027DAED
	public override string GetTooltip()
	{
		return MISC.NOTIFICATIONS.COLONY_ACHIEVEMENT_EARNED.TOOLTIP;
	}

	// Token: 0x06006981 RID: 27009 RVA: 0x0027F8F9 File Offset: 0x0027DAF9
	public override bool IsValid()
	{
		return true;
	}

	// Token: 0x06006982 RID: 27010 RVA: 0x0027F8FC File Offset: 0x0027DAFC
	public override void OnClick()
	{
		RetireColonyUtility.SaveColonySummaryData();
		MainMenu.ActivateRetiredColoniesScreenFromData(PauseScreen.Instance.transform.parent.gameObject, RetireColonyUtility.GetCurrentColonyRetiredColonyData());
	}
}
