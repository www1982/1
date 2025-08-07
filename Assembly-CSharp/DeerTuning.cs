using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x020000A2 RID: 162
public static class DeerTuning
{
	// Token: 0x040001F3 RID: 499
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "WoodDeerEgg".ToTag(),
			weight = 1f
		}
	};

	// Token: 0x040001F4 RID: 500
	public const float STANDARD_CALORIES_PER_CYCLE = 100000f;

	// Token: 0x040001F5 RID: 501
	public const float STANDARD_STARVE_CYCLES = 10f;

	// Token: 0x040001F6 RID: 502
	public const float STANDARD_STOMACH_SIZE = 1000000f;

	// Token: 0x040001F7 RID: 503
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER3;

	// Token: 0x040001F8 RID: 504
	public static int PEN_SIZE_PER_CREATURE_HUG = CREATURES.SPACE_REQUIREMENTS.TIER1;

	// Token: 0x040001F9 RID: 505
	public static float EGG_MASS = 2f;

	// Token: 0x040001FA RID: 506
	public static float DROP_ANTLER_DURATION = 1200f;
}
