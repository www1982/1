using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x020000C5 RID: 197
public static class StegoTuning
{
	// Token: 0x0400029C RID: 668
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "StegoEgg".ToTag(),
			weight = 1f
		}
	};

	// Token: 0x0400029D RID: 669
	public static float VINE_FOOD_PER_CYCLE = 4f;

	// Token: 0x0400029E RID: 670
	public static readonly float PEAT_PRODUCED_PER_CYCLE = 200f;

	// Token: 0x0400029F RID: 671
	public static readonly float STANDARD_CALORIES_PER_CYCLE = StegoTuning.VINE_FOOD_PER_CYCLE * 325000f;

	// Token: 0x040002A0 RID: 672
	public static readonly float STANDARD_STARVE_CYCLES = 10f;

	// Token: 0x040002A1 RID: 673
	public static readonly float STANDARD_STOMACH_SIZE = StegoTuning.STANDARD_CALORIES_PER_CYCLE * StegoTuning.STANDARD_STARVE_CYCLES;

	// Token: 0x040002A2 RID: 674
	public static readonly float CALORIES_PER_KG_OF_ORE = StegoTuning.STANDARD_CALORIES_PER_CYCLE / StegoTuning.VINE_FOOD_PER_CYCLE;

	// Token: 0x040002A3 RID: 675
	public static float CALORIES_PER_UNIT_EATEN = FOOD.FOOD_TYPES.VINEFRUIT.CaloriesPerUnit;

	// Token: 0x040002A4 RID: 676
	public static float MIN_POOP_SIZE_IN_KG = StegoTuning.VINE_FOOD_PER_CYCLE;

	// Token: 0x040002A5 RID: 677
	public static Tag POOP_ELEMENT = SimHashes.Peat.CreateTag();

	// Token: 0x040002A6 RID: 678
	public const float EGG_MASS = 8f;

	// Token: 0x040002A7 RID: 679
	public const float STOMP_COOLDOWN = 60f;

	// Token: 0x040002A8 RID: 680
	public static readonly int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER4;

	// Token: 0x040002A9 RID: 681
	public const int SEARCH_RADIUS = 10;
}
