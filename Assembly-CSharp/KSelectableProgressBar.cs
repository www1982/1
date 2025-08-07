using System;

// Token: 0x020005CF RID: 1487
public class KSelectableProgressBar : KSelectable
{
	// Token: 0x06002263 RID: 8803 RVA: 0x000C5734 File Offset: 0x000C3934
	public override string GetName()
	{
		int num = (int)(this.progressBar.PercentFull * (float)this.scaleAmount);
		return string.Format("{0} {1}/{2}", this.entityName, num, this.scaleAmount);
	}

	// Token: 0x040013F9 RID: 5113
	[MyCmpGet]
	private ProgressBar progressBar;

	// Token: 0x040013FA RID: 5114
	private int scaleAmount = 100;
}
