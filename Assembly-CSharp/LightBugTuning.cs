using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x020000AA RID: 170
public static class LightBugTuning
{
	// Token: 0x04000218 RID: 536
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugOrangeEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x04000219 RID: 537
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_ORANGE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugEgg".ToTag(),
			weight = 0.33f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugOrangeEgg".ToTag(),
			weight = 0.66f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugPurpleEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x0400021A RID: 538
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_PURPLE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugOrangeEgg".ToTag(),
			weight = 0.33f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugPurpleEgg".ToTag(),
			weight = 0.66f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugPinkEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x0400021B RID: 539
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_PINK = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugPurpleEgg".ToTag(),
			weight = 0.33f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugPinkEgg".ToTag(),
			weight = 0.66f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugBlueEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x0400021C RID: 540
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BLUE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugPinkEgg".ToTag(),
			weight = 0.33f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugBlueEgg".ToTag(),
			weight = 0.66f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugBlackEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x0400021D RID: 541
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BLACK = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugBlueEgg".ToTag(),
			weight = 0.33f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugBlackEgg".ToTag(),
			weight = 0.66f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugCrystalEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x0400021E RID: 542
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_CRYSTAL = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugCrystalEgg".ToTag(),
			weight = 0.98f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "LightBugEgg".ToTag(),
			weight = 0.02f
		}
	};

	// Token: 0x0400021F RID: 543
	public static float STANDARD_CALORIES_PER_CYCLE = 40000f;

	// Token: 0x04000220 RID: 544
	public static float STANDARD_STARVE_CYCLES = 8f;

	// Token: 0x04000221 RID: 545
	public static float STANDARD_STOMACH_SIZE = LightBugTuning.STANDARD_CALORIES_PER_CYCLE * LightBugTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x04000222 RID: 546
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER3;

	// Token: 0x04000223 RID: 547
	public static float EGG_MASS = 0.2f;
}
