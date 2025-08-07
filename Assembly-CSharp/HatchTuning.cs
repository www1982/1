using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x020000A8 RID: 168
public static class HatchTuning
{
	// Token: 0x0400020F RID: 527
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "HatchEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "HatchHardEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "HatchVeggieEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x04000210 RID: 528
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_HARD = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "HatchEgg".ToTag(),
			weight = 0.32f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "HatchHardEgg".ToTag(),
			weight = 0.65f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "HatchMetalEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x04000211 RID: 529
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_VEGGIE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "HatchEgg".ToTag(),
			weight = 0.33f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "HatchVeggieEgg".ToTag(),
			weight = 0.67f
		}
	};

	// Token: 0x04000212 RID: 530
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_METAL = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "HatchEgg".ToTag(),
			weight = 0.11f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "HatchHardEgg".ToTag(),
			weight = 0.22f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "HatchMetalEgg".ToTag(),
			weight = 0.67f
		}
	};

	// Token: 0x04000213 RID: 531
	public static float STANDARD_CALORIES_PER_CYCLE = 700000f;

	// Token: 0x04000214 RID: 532
	public static float STANDARD_STARVE_CYCLES = 10f;

	// Token: 0x04000215 RID: 533
	public static float STANDARD_STOMACH_SIZE = HatchTuning.STANDARD_CALORIES_PER_CYCLE * HatchTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x04000216 RID: 534
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER3;

	// Token: 0x04000217 RID: 535
	public static float EGG_MASS = 2f;
}
