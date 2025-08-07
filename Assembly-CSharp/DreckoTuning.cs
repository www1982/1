using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x020000A6 RID: 166
public static class DreckoTuning
{
	// Token: 0x04000208 RID: 520
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "DreckoEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "DreckoPlasticEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x04000209 RID: 521
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_PLASTIC = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "DreckoEgg".ToTag(),
			weight = 0.35f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "DreckoPlasticEgg".ToTag(),
			weight = 0.65f
		}
	};

	// Token: 0x0400020A RID: 522
	public static float STANDARD_CALORIES_PER_CYCLE = 2000000f;

	// Token: 0x0400020B RID: 523
	public static float STANDARD_STARVE_CYCLES = 5f;

	// Token: 0x0400020C RID: 524
	public static float STANDARD_STOMACH_SIZE = DreckoTuning.STANDARD_CALORIES_PER_CYCLE * DreckoTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x0400020D RID: 525
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER3;

	// Token: 0x0400020E RID: 526
	public static float EGG_MASS = 2f;
}
