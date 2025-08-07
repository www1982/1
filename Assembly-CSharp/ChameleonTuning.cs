using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x0200009E RID: 158
public static class ChameleonTuning
{
	// Token: 0x040001E3 RID: 483
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "ChameleonEgg".ToTag(),
			weight = 0.98f
		}
	};

	// Token: 0x040001E4 RID: 484
	public static float STANDARD_CALORIES_PER_CYCLE = 2000000f;

	// Token: 0x040001E5 RID: 485
	public static float STANDARD_STARVE_CYCLES = 5f;

	// Token: 0x040001E6 RID: 486
	public static float STANDARD_STOMACH_SIZE = ChameleonTuning.STANDARD_CALORIES_PER_CYCLE * ChameleonTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x040001E7 RID: 487
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER1;

	// Token: 0x040001E8 RID: 488
	public static float EGG_MASS = 2f;

	// Token: 0x040001E9 RID: 489
	public const float HARVEST_COOLDOWN = 150f;
}
