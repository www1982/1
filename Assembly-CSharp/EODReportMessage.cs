using System;
using KSerialization;

// Token: 0x02000D4F RID: 3407
public class EODReportMessage : Message
{
	// Token: 0x060069A6 RID: 27046 RVA: 0x0027FB1A File Offset: 0x0027DD1A
	public EODReportMessage(string title, string tooltip)
	{
		this.day = GameUtil.GetCurrentCycle();
		this.title = title;
		this.tooltip = tooltip;
	}

	// Token: 0x060069A7 RID: 27047 RVA: 0x0027FB3B File Offset: 0x0027DD3B
	public EODReportMessage()
	{
	}

	// Token: 0x060069A8 RID: 27048 RVA: 0x0027FB43 File Offset: 0x0027DD43
	public override string GetSound()
	{
		return null;
	}

	// Token: 0x060069A9 RID: 27049 RVA: 0x0027FB46 File Offset: 0x0027DD46
	public override string GetMessageBody()
	{
		return "";
	}

	// Token: 0x060069AA RID: 27050 RVA: 0x0027FB4D File Offset: 0x0027DD4D
	public override string GetTooltip()
	{
		return this.tooltip;
	}

	// Token: 0x060069AB RID: 27051 RVA: 0x0027FB55 File Offset: 0x0027DD55
	public override string GetTitle()
	{
		return this.title;
	}

	// Token: 0x060069AC RID: 27052 RVA: 0x0027FB5D File Offset: 0x0027DD5D
	public void OpenReport()
	{
		ManagementMenu.Instance.OpenReports(this.day);
	}

	// Token: 0x060069AD RID: 27053 RVA: 0x0027FB6F File Offset: 0x0027DD6F
	public override bool ShowDialog()
	{
		return false;
	}

	// Token: 0x060069AE RID: 27054 RVA: 0x0027FB72 File Offset: 0x0027DD72
	public override void OnClick()
	{
		this.OpenReport();
	}

	// Token: 0x0400483B RID: 18491
	[Serialize]
	private int day;

	// Token: 0x0400483C RID: 18492
	[Serialize]
	private string title;

	// Token: 0x0400483D RID: 18493
	[Serialize]
	private string tooltip;
}
