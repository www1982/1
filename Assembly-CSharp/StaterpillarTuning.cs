using System;
using System.Collections.Generic;

// Token: 0x020000C3 RID: 195
public static class StaterpillarTuning
{
	// Token: 0x04000294 RID: 660
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarGasEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarLiquidEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x04000295 RID: 661
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_GAS = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarEgg".ToTag(),
			weight = 0.32f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarGasEgg".ToTag(),
			weight = 0.66f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarLiquidEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x04000296 RID: 662
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_LIQUID = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarEgg".ToTag(),
			weight = 0.32f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarGasEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "StaterpillarLiquidEgg".ToTag(),
			weight = 0.66f
		}
	};

	// Token: 0x04000297 RID: 663
	public static float STANDARD_CALORIES_PER_CYCLE = 2000000f;

	// Token: 0x04000298 RID: 664
	public static float STANDARD_STARVE_CYCLES = 5f;

	// Token: 0x04000299 RID: 665
	public static float STANDARD_STOMACH_SIZE = StaterpillarTuning.STANDARD_CALORIES_PER_CYCLE * StaterpillarTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x0400029A RID: 666
	public static float POOP_CONVERSTION_RATE = 0.05f;

	// Token: 0x0400029B RID: 667
	public static float EGG_MASS = 2f;
}
