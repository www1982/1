using System;
using System.Collections.Generic;
using TUNING;

// Token: 0x0200009A RID: 154
public static class BellyTuning
{
	// Token: 0x040001C8 RID: 456
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_BASE = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "IceBellyEgg".ToTag(),
			weight = 1f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "GoldBellyEgg".ToTag(),
			weight = 0f
		}
	};

	// Token: 0x040001C9 RID: 457
	public static List<FertilityMonitor.BreedingChance> EGG_CHANCES_GOLD = new List<FertilityMonitor.BreedingChance>
	{
		new FertilityMonitor.BreedingChance
		{
			egg = "IceBellyEgg".ToTag(),
			weight = 0.02f
		},
		new FertilityMonitor.BreedingChance
		{
			egg = "GoldBellyEgg".ToTag(),
			weight = 0.98f
		}
	};

	// Token: 0x040001CA RID: 458
	public const float KW_GENERATED_TO_WARM_UP = 1.3f;

	// Token: 0x040001CB RID: 459
	public static float STANDARD_CALORIES_PER_CYCLE = 4f * FOOD.FOOD_TYPES.CARROT.CaloriesPerUnit / (CROPS.CROP_TYPES.Find((Crop.CropVal m) => m.cropId == CarrotConfig.ID).cropDuration / 600f);

	// Token: 0x040001CC RID: 460
	public const float STANDARD_STARVE_CYCLES = 10f;

	// Token: 0x040001CD RID: 461
	public static float STANDARD_STOMACH_SIZE = BellyTuning.STANDARD_CALORIES_PER_CYCLE * 10f;

	// Token: 0x040001CE RID: 462
	public static int PEN_SIZE_PER_CREATURE = CREATURES.SPACE_REQUIREMENTS.TIER3;

	// Token: 0x040001CF RID: 463
	public const float EGG_MASS = 8f;

	// Token: 0x040001D0 RID: 464
	public const int GERMS_EMMITED_PER_KG_POOPED = 1000;

	// Token: 0x040001D1 RID: 465
	public static string GERM_ID_EMMITED_ON_POOP = "PollenGerms";

	// Token: 0x040001D2 RID: 466
	public static float CALORIES_PER_UNIT_EATEN = FOOD.FOOD_TYPES.CARROT.CaloriesPerUnit;

	// Token: 0x040001D3 RID: 467
	public static float CONSUMABLE_PLANT_MATURITY_LEVELS = CROPS.CROP_TYPES.Find((Crop.CropVal m) => m.cropId == CarrotConfig.ID).cropDuration / 600f;

	// Token: 0x040001D4 RID: 468
	public const float CONSUMED_MASS_TO_POOP_MASS_MULTIPLIER = 67.474f;

	// Token: 0x040001D5 RID: 469
	public const float MIN_POOP_SIZE_IN_KG = 1f;
}
