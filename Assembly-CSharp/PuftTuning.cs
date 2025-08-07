using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x020000BA RID: 186
public static class PuftTuning
{
	// Token: 0x04000273 RID: 627
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftAlphaEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftOxyliteEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftBleachstoneEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x04000274 RID: 628
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_ALPHA = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftAlphaEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x04000275 RID: 629
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_OXYLITE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftEgg".ToTag(),
			weight = 0.31f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftAlphaEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftOxyliteEgg".ToTag(),
			weight = 0.67f
		}
	};

	// Token: 0x04000276 RID: 630
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BLEACHSTONE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftEgg".ToTag(),
			weight = 0.31f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftAlphaEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "PuftBleachstoneEgg".ToTag(),
			weight = 0.67f
		}
	};

	// Token: 0x04000277 RID: 631
	public static float STANDARD_CALORIES_PER_CYCLE = 200000f;

	// Token: 0x04000278 RID: 632
	public static float STANDARD_STARVE_CYCLES = 6f;

	// Token: 0x04000279 RID: 633
	public static float STANDARD_STOMACH_SIZE = PuftTuning.STANDARD_CALORIES_PER_CYCLE * PuftTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x0400027A RID: 634
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER4;

	// Token: 0x0400027B RID: 635
	public static float EGG_MASS = 0.5f;
}
