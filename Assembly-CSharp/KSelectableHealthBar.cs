using System;

// Token: 0x020005CE RID: 1486
public class KSelectableHealthBar : KSelectable
{
	// Token: 0x06002261 RID: 8801 RVA: 0x000C56E0 File Offset: 0x000C38E0
	public override string GetName()
	{
		int num = (int)(this.progressBar.PercentFull * (float)this.scaleAmount);
		return string.Format("{0} {1}/{2}", this.entityName, num, this.scaleAmount);
	}

	// Token: 0x040013F7 RID: 5111
	[MyCmpGet]
	private ProgressBar progressBar;

	// Token: 0x040013F8 RID: 5112
	private int scaleAmount = 100;
}
