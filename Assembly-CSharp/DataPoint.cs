using System;

// Token: 0x0200063E RID: 1598
public struct DataPoint
{
	// Token: 0x0600268F RID: 9871 RVA: 0x000DBAA2 File Offset: 0x000D9CA2
	public DataPoint(float start, float end, float value)
	{
		this.periodStart = start;
		this.periodEnd = end;
		this.periodValue = value;
	}

	// Token: 0x04001685 RID: 5765
	public float periodStart;

	// Token: 0x04001686 RID: 5766
	public float periodEnd;

	// Token: 0x04001687 RID: 5767
	public float periodValue;
}
