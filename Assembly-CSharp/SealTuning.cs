using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x020000BF RID: 191
public static class SealTuning
{
	// Token: 0x04000286 RID: 646
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "SealEgg".ToTag(),
			weight = 1f
		}
	};

	// Token: 0x04000287 RID: 647
	public const float STANDARD_CALORIES_PER_CYCLE = 100000f;

	// Token: 0x04000288 RID: 648
	public const float STANDARD_STARVE_CYCLES = 10f;

	// Token: 0x04000289 RID: 649
	public const float STANDARD_STOMACH_SIZE = 1000000f;

	// Token: 0x0400028A RID: 650
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER3;

	// Token: 0x0400028B RID: 651
	public static float EGG_MASS = 2f;
}
