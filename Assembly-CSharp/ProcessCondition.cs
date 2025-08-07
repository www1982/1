using System;

// Token: 0x02000A79 RID: 2681
public abstract class ProcessCondition
{
	// Token: 0x06004DC5 RID: 19909
	public abstract ProcessCondition.Status EvaluateCondition();

	// Token: 0x06004DC6 RID: 19910
	public abstract bool ShowInUI();

	// Token: 0x06004DC7 RID: 19911
	public abstract string GetStatusMessage(ProcessCondition.Status status);

	// Token: 0x06004DC8 RID: 19912 RVA: 0x001C240A File Offset: 0x001C060A
	public string GetStatusMessage()
	{
		return this.GetStatusMessage(this.EvaluateCondition());
	}

	// Token: 0x06004DC9 RID: 19913
	public abstract string GetStatusTooltip(ProcessCondition.Status status);

	// Token: 0x06004DCA RID: 19914 RVA: 0x001C2418 File Offset: 0x001C0618
	public string GetStatusTooltip()
	{
		return this.GetStatusTooltip(this.EvaluateCondition());
	}

	// Token: 0x06004DCB RID: 19915 RVA: 0x001C2426 File Offset: 0x001C0626
	public virtual StatusItem GetStatusItem(ProcessCondition.Status status)
	{
		return null;
	}

	// Token: 0x06004DCC RID: 19916 RVA: 0x001C2429 File Offset: 0x001C0629
	public virtual ProcessCondition GetParentCondition()
	{
		return this.parentCondition;
	}

	// Token: 0x040033B3 RID: 13235
	protected ProcessCondition parentCondition;

	// Token: 0x02001B5F RID: 7007
	public enum ProcessConditionType
	{
		// Token: 0x0400829C RID: 33436
		RocketFlight,
		// Token: 0x0400829D RID: 33437
		RocketPrep,
		// Token: 0x0400829E RID: 33438
		RocketStorage,
		// Token: 0x0400829F RID: 33439
		RocketBoard,
		// Token: 0x040082A0 RID: 33440
		All
	}

	// Token: 0x02001B60 RID: 7008
	public enum Status
	{
		// Token: 0x040082A2 RID: 33442
		Failure,
		// Token: 0x040082A3 RID: 33443
		Warning,
		// Token: 0x040082A4 RID: 33444
		Ready
	}
}
