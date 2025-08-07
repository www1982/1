using System;
using System.Collections.Generic;

// Token: 0x020000B1 RID: 177
public static class MosquitoTuning
{
	// Token: 0x04000243 RID: 579
	public const float BASE_EGG_DROP_TIME = 0.9f;

	// Token: 0x04000244 RID: 580
	public const float EGG_MASS = 1f;

	// Token: 0x04000245 RID: 581
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "MosquitoEgg".ToTag(),
			weight = 1f
		}
	};
}
