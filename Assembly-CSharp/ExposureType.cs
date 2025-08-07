using System;
using System.Collections.Generic;

// Token: 0x020009F0 RID: 2544
public class ExposureType
{
	// Token: 0x04003106 RID: 12550
	public string germ_id;

	// Token: 0x04003107 RID: 12551
	public string sickness_id;

	// Token: 0x04003108 RID: 12552
	public string infection_effect;

	// Token: 0x04003109 RID: 12553
	public int exposure_threshold;

	// Token: 0x0400310A RID: 12554
	public bool infect_immediately;

	// Token: 0x0400310B RID: 12555
	public List<string> required_traits;

	// Token: 0x0400310C RID: 12556
	public List<string> excluded_traits;

	// Token: 0x0400310D RID: 12557
	public List<string> excluded_effects;

	// Token: 0x0400310E RID: 12558
	public int base_resistance;
}
