using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x020000B6 RID: 182
public static class PacuTuning
{
	// Token: 0x04000252 RID: 594
	public static float MASS = 200f;

	// Token: 0x04000253 RID: 595
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuTropicalEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuCleanerEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x04000254 RID: 596
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_TROPICAL = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuEgg".ToTag(),
			weight = 0.32f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuTropicalEgg".ToTag(),
			weight = 0.65f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuCleanerEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x04000255 RID: 597
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_CLEANER = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuEgg".ToTag(),
			weight = 0.32f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuCleanerEgg".ToTag(),
			weight = 0.65f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PacuTropicalEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x04000256 RID: 598
	public static float STANDARD_CALORIES_PER_CYCLE = 100000f;

	// Token: 0x04000257 RID: 599
	public static float STANDARD_STARVE_CYCLES = 5f;

	// Token: 0x04000258 RID: 600
	public static float STANDARD_STOMACH_SIZE = PacuTuning.STANDARD_CALORIES_PER_CYCLE * PacuTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x04000259 RID: 601
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER2;

	// Token: 0x0400025A RID: 602
	public static float EGG_MASS = 4f;
}
