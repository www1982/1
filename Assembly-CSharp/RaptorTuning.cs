using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x020000BC RID: 188
public static class RaptorTuning
{
	// Token: 0x0400027C RID: 636
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "RaptorEgg".ToTag(),
			weight = 1f
		}
	};

	// Token: 0x0400027D RID: 637
	public static float STANDARD_CALORIES_PER_CYCLE = 0.5f * FOOD.FOOD_TYPES.MEAT.CaloriesPerUnit;

	// Token: 0x0400027E RID: 638
	public const float STANDARD_STARVE_CYCLES = 10f;

	// Token: 0x0400027F RID: 639
	public static float STANDARD_STOMACH_SIZE = RaptorTuning.STANDARD_CALORIES_PER_CYCLE * 10f;

	// Token: 0x04000280 RID: 640
	public const float EGG_MASS = 8f;

	// Token: 0x04000281 RID: 641
	public static float CALORIES_PER_UNIT_EATEN = FOOD.FOOD_TYPES.MEAT.CaloriesPerUnit;

	// Token: 0x04000282 RID: 642
	public const float MIN_POOP_SIZE_IN_KG = 0.1f;

	// Token: 0x04000283 RID: 643
	public static float BASE_PRODUCTION_RATE = 128f;

	// Token: 0x04000284 RID: 644
	public static float PREY_PRODUCTION_RATE = 256f;

	// Token: 0x04000285 RID: 645
	public static Tag POOP_ELEMENT = SimHashes.BrineIce.CreateTag();
}
