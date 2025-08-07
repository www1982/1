using System;
using System.Collections.Generic;

namespace TUNING
{
	// Token: 0x02000F8D RID: 3981
	public class FOOD
	{
		// Token: 0x04005C21 RID: 23585
		public const float EATING_SECONDS_PER_CALORIE = 2E-05f;

		// Token: 0x04005C22 RID: 23586
		public static float FOOD_CALORIES_PER_CYCLE = -DUPLICANTSTATS.STANDARD.BaseStats.CALORIES_BURNED_PER_CYCLE;

		// Token: 0x04005C23 RID: 23587
		public const int FOOD_AMOUNT_INGREDIENT_ONLY = 0;

		// Token: 0x04005C24 RID: 23588
		public const float KCAL_SMALL_PORTION = 600000f;

		// Token: 0x04005C25 RID: 23589
		public const float KCAL_BONUS_COOKING_LOW = 250000f;

		// Token: 0x04005C26 RID: 23590
		public const float KCAL_BASIC_PORTION = 800000f;

		// Token: 0x04005C27 RID: 23591
		public const float KCAL_PREPARED_FOOD = 4000000f;

		// Token: 0x04005C28 RID: 23592
		public const float KCAL_BONUS_COOKING_BASIC = 400000f;

		// Token: 0x04005C29 RID: 23593
		public const float KCAL_BONUS_COOKING_DEEPFRIED = 1200000f;

		// Token: 0x04005C2A RID: 23594
		public const float DEFAULT_PRESERVE_TEMPERATURE = 255.15f;

		// Token: 0x04005C2B RID: 23595
		public const float DEFAULT_ROT_TEMPERATURE = 277.15f;

		// Token: 0x04005C2C RID: 23596
		public const float HIGH_PRESERVE_TEMPERATURE = 283.15f;

		// Token: 0x04005C2D RID: 23597
		public const float HIGH_ROT_TEMPERATURE = 308.15f;

		// Token: 0x04005C2E RID: 23598
		public const float EGG_COOK_TEMPERATURE = 344.15f;

		// Token: 0x04005C2F RID: 23599
		public const float DEFAULT_MASS = 1f;

		// Token: 0x04005C30 RID: 23600
		public const float DEFAULT_SPICE_MASS = 1f;

		// Token: 0x04005C31 RID: 23601
		public const float ROT_TO_ELEMENT_TIME = 600f;

		// Token: 0x04005C32 RID: 23602
		public const int MUSH_BAR_SPAWN_GERMS = 1000;

		// Token: 0x04005C33 RID: 23603
		public const float IDEAL_TEMPERATURE_TOLERANCE = 10f;

		// Token: 0x04005C34 RID: 23604
		public const int FOOD_QUALITY_AWFUL = -1;

		// Token: 0x04005C35 RID: 23605
		public const int FOOD_QUALITY_TERRIBLE = 0;

		// Token: 0x04005C36 RID: 23606
		public const int FOOD_QUALITY_MEDIOCRE = 1;

		// Token: 0x04005C37 RID: 23607
		public const int FOOD_QUALITY_GOOD = 2;

		// Token: 0x04005C38 RID: 23608
		public const int FOOD_QUALITY_GREAT = 3;

		// Token: 0x04005C39 RID: 23609
		public const int FOOD_QUALITY_AMAZING = 4;

		// Token: 0x04005C3A RID: 23610
		public const int FOOD_QUALITY_WONDERFUL = 5;

		// Token: 0x04005C3B RID: 23611
		public const int FOOD_QUALITY_MORE_WONDERFUL = 6;

		// Token: 0x02002163 RID: 8547
		public class SPOIL_TIME
		{
			// Token: 0x040098FC RID: 39164
			public const float DEFAULT = 4800f;

			// Token: 0x040098FD RID: 39165
			public const float QUICK = 2400f;

			// Token: 0x040098FE RID: 39166
			public const float SLOW = 9600f;

			// Token: 0x040098FF RID: 39167
			public const float VERYSLOW = 19200f;
		}

		// Token: 0x02002164 RID: 8548
		public class FOOD_TYPES
		{
			// Token: 0x04009900 RID: 39168
			public static readonly EdiblesManager.FoodInfo FIELDRATION = new EdiblesManager.FoodInfo("FieldRation", 800000f, -1, 255.15f, 277.15f, 19200f, false, null, null);

			// Token: 0x04009901 RID: 39169
			public static readonly EdiblesManager.FoodInfo MUSHBAR = new EdiblesManager.FoodInfo("MushBar", 800000f, -1, 255.15f, 277.15f, 4800f, true, null, null);

			// Token: 0x04009902 RID: 39170
			public static readonly EdiblesManager.FoodInfo BASICPLANTFOOD = new EdiblesManager.FoodInfo("BasicPlantFood", 600000f, -1, 255.15f, 277.15f, 4800f, true, null, null);

			// Token: 0x04009903 RID: 39171
			public static readonly EdiblesManager.FoodInfo VINEFRUIT = new EdiblesManager.FoodInfo(VineFruitConfig.ID, 325000f, 0, 255.15f, 277.15f, 4800f, true, DlcManager.DLC4, null);

			// Token: 0x04009904 RID: 39172
			public static readonly EdiblesManager.FoodInfo BASICFORAGEPLANT = new EdiblesManager.FoodInfo("BasicForagePlant", 800000f, -1, 255.15f, 277.15f, 4800f, false, null, null);

			// Token: 0x04009905 RID: 39173
			public static readonly EdiblesManager.FoodInfo FORESTFORAGEPLANT = new EdiblesManager.FoodInfo("ForestForagePlant", 6400000f, -1, 255.15f, 277.15f, 4800f, false, null, null);

			// Token: 0x04009906 RID: 39174
			public static readonly EdiblesManager.FoodInfo SWAMPFORAGEPLANT = new EdiblesManager.FoodInfo("SwampForagePlant", 2400000f, -1, 255.15f, 277.15f, 4800f, false, DlcManager.EXPANSION1, null);

			// Token: 0x04009907 RID: 39175
			public static readonly EdiblesManager.FoodInfo ICECAVESFORAGEPLANT = new EdiblesManager.FoodInfo("IceCavesForagePlant", 800000f, -1, 255.15f, 277.15f, 4800f, false, DlcManager.DLC2, null);

			// Token: 0x04009908 RID: 39176
			public static readonly EdiblesManager.FoodInfo MUSHROOM = new EdiblesManager.FoodInfo(MushroomConfig.ID, 2400000f, 0, 255.15f, 277.15f, 4800f, true, null, null);

			// Token: 0x04009909 RID: 39177
			public static readonly EdiblesManager.FoodInfo LETTUCE = new EdiblesManager.FoodInfo("Lettuce", 400000f, 0, 255.15f, 277.15f, 2400f, true, null, null).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.EXPANSION1, null);

			// Token: 0x0400990A RID: 39178
			public static readonly EdiblesManager.FoodInfo RAWEGG = new EdiblesManager.FoodInfo("RawEgg", 1600000f, -1, 255.15f, 277.15f, 4800f, true, null, null);

			// Token: 0x0400990B RID: 39179
			public static readonly EdiblesManager.FoodInfo MEAT = new EdiblesManager.FoodInfo("Meat", 1600000f, -1, 255.15f, 277.15f, 4800f, true, null, null);

			// Token: 0x0400990C RID: 39180
			public static readonly EdiblesManager.FoodInfo PLANTMEAT = new EdiblesManager.FoodInfo("PlantMeat", 1200000f, 1, 255.15f, 277.15f, 2400f, true, DlcManager.EXPANSION1, null);

			// Token: 0x0400990D RID: 39181
			public static readonly EdiblesManager.FoodInfo PRICKLEFRUIT = new EdiblesManager.FoodInfo(PrickleFruitConfig.ID, 1600000f, 0, 255.15f, 277.15f, 4800f, true, null, null);

			// Token: 0x0400990E RID: 39182
			public static readonly EdiblesManager.FoodInfo SWAMPFRUIT = new EdiblesManager.FoodInfo(SwampFruitConfig.ID, 1840000f, 0, 255.15f, 277.15f, 2400f, true, DlcManager.EXPANSION1, null);

			// Token: 0x0400990F RID: 39183
			public static readonly EdiblesManager.FoodInfo FISH_MEAT = new EdiblesManager.FoodInfo("FishMeat", 1000000f, 2, 255.15f, 277.15f, 2400f, true, null, null).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.EXPANSION1, null);

			// Token: 0x04009910 RID: 39184
			public static readonly EdiblesManager.FoodInfo SHELLFISH_MEAT = new EdiblesManager.FoodInfo("ShellfishMeat", 1000000f, 2, 255.15f, 277.15f, 2400f, true, null, null).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.EXPANSION1, null);

			// Token: 0x04009911 RID: 39185
			public static readonly EdiblesManager.FoodInfo JAWBOFILLET = new EdiblesManager.FoodInfo("PrehistoricPacuFillet", 1000000f, 3, 255.15f, 277.15f, 2400f, true, DlcManager.DLC4, null);

			// Token: 0x04009912 RID: 39186
			public static readonly EdiblesManager.FoodInfo WORMBASICFRUIT = new EdiblesManager.FoodInfo("WormBasicFruit", 800000f, 0, 255.15f, 277.15f, 4800f, true, DlcManager.EXPANSION1, null);

			// Token: 0x04009913 RID: 39187
			public static readonly EdiblesManager.FoodInfo WORMSUPERFRUIT = new EdiblesManager.FoodInfo("WormSuperFruit", 250000f, 1, 255.15f, 277.15f, 2400f, true, DlcManager.EXPANSION1, null);

			// Token: 0x04009914 RID: 39188
			public static readonly EdiblesManager.FoodInfo HARDSKINBERRY = new EdiblesManager.FoodInfo("HardSkinBerry", 800000f, -1, 255.15f, 277.15f, 9600f, true, DlcManager.DLC2, null);

			// Token: 0x04009915 RID: 39189
			public static readonly EdiblesManager.FoodInfo CARROT = new EdiblesManager.FoodInfo(CarrotConfig.ID, 4000000f, 0, 255.15f, 277.15f, 9600f, true, DlcManager.DLC2, null);

			// Token: 0x04009916 RID: 39190
			public static readonly EdiblesManager.FoodInfo PEMMICAN = new EdiblesManager.FoodInfo("Pemmican", FOOD.FOOD_TYPES.HARDSKINBERRY.CaloriesPerUnit * 2f + 1000000f, 2, 255.15f, 277.15f, 19200f, false, DlcManager.DLC2, null);

			// Token: 0x04009917 RID: 39191
			public static readonly EdiblesManager.FoodInfo FRIES_CARROT = new EdiblesManager.FoodInfo("FriesCarrot", 5400000f, 3, 255.15f, 277.15f, 2400f, true, DlcManager.DLC2, null);

			// Token: 0x04009918 RID: 39192
			public static readonly EdiblesManager.FoodInfo BUTTERFLYFOOD = new EdiblesManager.FoodInfo("ButterflyFood", 1500000f, 1, 255.15f, 277.15f, 4800f, true, DlcManager.DLC4, null);

			// Token: 0x04009919 RID: 39193
			public static readonly EdiblesManager.FoodInfo DEEP_FRIED_MEAT = new EdiblesManager.FoodInfo("DeepFriedMeat", 4000000f, 3, 255.15f, 277.15f, 2400f, true, DlcManager.DLC2, null);

			// Token: 0x0400991A RID: 39194
			public static readonly EdiblesManager.FoodInfo DEEP_FRIED_NOSH = new EdiblesManager.FoodInfo("DeepFriedNosh", 5000000f, 3, 255.15f, 277.15f, 4800f, true, DlcManager.DLC2, null);

			// Token: 0x0400991B RID: 39195
			public static readonly EdiblesManager.FoodInfo DEEP_FRIED_FISH = new EdiblesManager.FoodInfo("DeepFriedFish", 4200000f, 4, 255.15f, 277.15f, 2400f, true, DlcManager.DLC2, null).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.EXPANSION1, null);

			// Token: 0x0400991C RID: 39196
			public static readonly EdiblesManager.FoodInfo DEEP_FRIED_SHELLFISH = new EdiblesManager.FoodInfo("DeepFriedShellfish", 4200000f, 4, 255.15f, 277.15f, 2400f, true, DlcManager.DLC2, null).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.EXPANSION1, null);

			// Token: 0x0400991D RID: 39197
			public static readonly EdiblesManager.FoodInfo GARDENFOODPLANT = new EdiblesManager.FoodInfo("GardenFoodPlantFood", 800000f, -1, 255.15f, 277.15f, 9600f, true, DlcManager.DLC4, null);

			// Token: 0x0400991E RID: 39198
			public static readonly EdiblesManager.FoodInfo GARDENFORAGEPLANT = new EdiblesManager.FoodInfo("GardenForagePlant", 800000f, -1, 255.15f, 277.15f, 4800f, false, DlcManager.DLC4, null);

			// Token: 0x0400991F RID: 39199
			public static readonly EdiblesManager.FoodInfo PICKLEDMEAL = new EdiblesManager.FoodInfo("PickledMeal", 1800000f, -1, 255.15f, 277.15f, 19200f, true, null, null);

			// Token: 0x04009920 RID: 39200
			public static readonly EdiblesManager.FoodInfo BASICPLANTBAR = new EdiblesManager.FoodInfo("BasicPlantBar", 1700000f, 0, 255.15f, 277.15f, 4800f, true, null, null);

			// Token: 0x04009921 RID: 39201
			public static readonly EdiblesManager.FoodInfo FRIEDMUSHBAR = new EdiblesManager.FoodInfo("FriedMushBar", 1050000f, 0, 255.15f, 277.15f, 4800f, true, null, null);

			// Token: 0x04009922 RID: 39202
			public static readonly EdiblesManager.FoodInfo GAMMAMUSH = new EdiblesManager.FoodInfo("GammaMush", 1050000f, 1, 255.15f, 277.15f, 2400f, true, null, null);

			// Token: 0x04009923 RID: 39203
			public static readonly EdiblesManager.FoodInfo GRILLED_PRICKLEFRUIT = new EdiblesManager.FoodInfo("GrilledPrickleFruit", 2000000f, 1, 255.15f, 277.15f, 4800f, true, null, null);

			// Token: 0x04009924 RID: 39204
			public static readonly EdiblesManager.FoodInfo SWAMP_DELIGHTS = new EdiblesManager.FoodInfo("SwampDelights", 2240000f, 1, 255.15f, 277.15f, 4800f, true, DlcManager.EXPANSION1, null);

			// Token: 0x04009925 RID: 39205
			public static readonly EdiblesManager.FoodInfo FRIED_MUSHROOM = new EdiblesManager.FoodInfo("FriedMushroom", 2800000f, 1, 255.15f, 277.15f, 4800f, true, null, null);

			// Token: 0x04009926 RID: 39206
			public static readonly EdiblesManager.FoodInfo COOKED_PIKEAPPLE = new EdiblesManager.FoodInfo("CookedPikeapple", 1200000f, 1, 255.15f, 277.15f, 4800f, true, DlcManager.DLC2, null);

			// Token: 0x04009927 RID: 39207
			public static readonly EdiblesManager.FoodInfo COLD_WHEAT_BREAD = new EdiblesManager.FoodInfo("ColdWheatBread", 1200000f, 2, 255.15f, 277.15f, 4800f, true, null, null);

			// Token: 0x04009928 RID: 39208
			public static readonly EdiblesManager.FoodInfo COOKED_EGG = new EdiblesManager.FoodInfo("CookedEgg", 2800000f, 2, 255.15f, 277.15f, 2400f, true, null, null);

			// Token: 0x04009929 RID: 39209
			public static readonly EdiblesManager.FoodInfo COOKED_FISH = new EdiblesManager.FoodInfo("CookedFish", 1600000f, 3, 255.15f, 277.15f, 2400f, true, null, null).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.EXPANSION1, null);

			// Token: 0x0400992A RID: 39210
			public static readonly EdiblesManager.FoodInfo SMOKED_VEGETABLES = new EdiblesManager.FoodInfo("SmokedVegetables", 2862500f, 2, 255.15f, 277.15f, 9600f, true, DlcManager.DLC4, null);

			// Token: 0x0400992B RID: 39211
			public static readonly EdiblesManager.FoodInfo PANCAKES = new EdiblesManager.FoodInfo("Pancakes", 3600000f, 3, 255.15f, 277.15f, 4800f, true, null, null);

			// Token: 0x0400992C RID: 39212
			public static readonly EdiblesManager.FoodInfo SMOKED_FISH = new EdiblesManager.FoodInfo("SmokedFish", 2800000f, 3, 255.15f, 277.15f, 19200f, true, DlcManager.DLC4, null).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.EXPANSION1, null);

			// Token: 0x0400992D RID: 39213
			public static readonly EdiblesManager.FoodInfo COOKED_MEAT = new EdiblesManager.FoodInfo("CookedMeat", 4000000f, 3, 255.15f, 277.15f, 2400f, true, null, null);

			// Token: 0x0400992E RID: 39214
			public static readonly EdiblesManager.FoodInfo SMOKED_DINOSAURMEAT = new EdiblesManager.FoodInfo("SmokedDinosaurMeat", 5000000f, 3, 255.15f, 277.15f, 4800f, true, DlcManager.DLC4, null);

			// Token: 0x0400992F RID: 39215
			public static readonly EdiblesManager.FoodInfo WORMBASICFOOD = new EdiblesManager.FoodInfo("WormBasicFood", 1200000f, 1, 255.15f, 277.15f, 4800f, true, DlcManager.EXPANSION1, null);

			// Token: 0x04009930 RID: 39216
			public static readonly EdiblesManager.FoodInfo WORMSUPERFOOD = new EdiblesManager.FoodInfo("WormSuperFood", 2400000f, 3, 255.15f, 277.15f, 19200f, true, DlcManager.EXPANSION1, null);

			// Token: 0x04009931 RID: 39217
			public static readonly EdiblesManager.FoodInfo FRUITCAKE = new EdiblesManager.FoodInfo("FruitCake", 4000000f, 3, 255.15f, 277.15f, 19200f, false, null, null);

			// Token: 0x04009932 RID: 39218
			public static readonly EdiblesManager.FoodInfo SALSA = new EdiblesManager.FoodInfo("Salsa", 4400000f, 4, 255.15f, 277.15f, 2400f, true, null, null);

			// Token: 0x04009933 RID: 39219
			public static readonly EdiblesManager.FoodInfo SURF_AND_TURF = new EdiblesManager.FoodInfo("SurfAndTurf", 6000000f, 4, 255.15f, 277.15f, 2400f, true, null, null).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.EXPANSION1, null);

			// Token: 0x04009934 RID: 39220
			public static readonly EdiblesManager.FoodInfo MUSHROOM_WRAP = new EdiblesManager.FoodInfo("MushroomWrap", 4800000f, 4, 255.15f, 277.15f, 2400f, true, null, null).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.EXPANSION1, null);

			// Token: 0x04009935 RID: 39221
			public static readonly EdiblesManager.FoodInfo TOFU = new EdiblesManager.FoodInfo("Tofu", 3600000f, 2, 255.15f, 277.15f, 2400f, true, null, null);

			// Token: 0x04009936 RID: 39222
			public static readonly EdiblesManager.FoodInfo CURRY = new EdiblesManager.FoodInfo("Curry", 5000000f, 4, 255.15f, 277.15f, 9600f, true, null, null).AddEffects(new List<string> { "HotStuff", "WarmTouchFood" }, null, null);

			// Token: 0x04009937 RID: 39223
			public static readonly EdiblesManager.FoodInfo SPICEBREAD = new EdiblesManager.FoodInfo("SpiceBread", 4000000f, 5, 255.15f, 277.15f, 4800f, true, null, null);

			// Token: 0x04009938 RID: 39224
			public static readonly EdiblesManager.FoodInfo SPICY_TOFU = new EdiblesManager.FoodInfo("SpicyTofu", 4000000f, 5, 255.15f, 277.15f, 2400f, true, null, null).AddEffects(new List<string> { "WarmTouchFood" }, null, null);

			// Token: 0x04009939 RID: 39225
			public static readonly EdiblesManager.FoodInfo QUICHE = new EdiblesManager.FoodInfo("Quiche", 6400000f, 5, 255.15f, 277.15f, 2400f, true, null, null).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.EXPANSION1, null);

			// Token: 0x0400993A RID: 39226
			public static readonly EdiblesManager.FoodInfo BERRY_PIE = new EdiblesManager.FoodInfo("BerryPie", 4200000f, 5, 255.15f, 277.15f, 2400f, true, DlcManager.EXPANSION1, null);

			// Token: 0x0400993B RID: 39227
			public static readonly EdiblesManager.FoodInfo BURGER = new EdiblesManager.FoodInfo("Burger", 6000000f, 6, 255.15f, 277.15f, 2400f, true, null, null).AddEffects(new List<string> { "GoodEats" }, null, null).AddEffects(new List<string> { "SeafoodRadiationResistance" }, DlcManager.EXPANSION1, null);

			// Token: 0x0400993C RID: 39228
			public static readonly EdiblesManager.FoodInfo BEAN = new EdiblesManager.FoodInfo("BeanPlantSeed", 0f, 3, 255.15f, 277.15f, 4800f, true, null, null);

			// Token: 0x0400993D RID: 39229
			public static readonly EdiblesManager.FoodInfo SPICENUT = new EdiblesManager.FoodInfo(SpiceNutConfig.ID, 0f, 0, 255.15f, 277.15f, 2400f, true, null, null);

			// Token: 0x0400993E RID: 39230
			public static readonly EdiblesManager.FoodInfo COLD_WHEAT_SEED = new EdiblesManager.FoodInfo("ColdWheatSeed", 0f, 0, 283.15f, 308.15f, 9600f, true, null, null);

			// Token: 0x0400993F RID: 39231
			public static readonly EdiblesManager.FoodInfo FERNFOOD = new EdiblesManager.FoodInfo(FernFoodConfig.ID, 0f, 2, 255.15f, 277.15f, 9600f, true, DlcManager.DLC4, null);

			// Token: 0x04009940 RID: 39232
			public static readonly EdiblesManager.FoodInfo BUTTERFLY_SEED = new EdiblesManager.FoodInfo("ButterflyPlantSeed", 0f, 2, 255.15f, 277.15f, 4800f, true, DlcManager.DLC4, null);

			// Token: 0x04009941 RID: 39233
			public static readonly EdiblesManager.FoodInfo DINOSAURMEAT = new EdiblesManager.FoodInfo("DinosaurMeat", 0f, -1, 255.15f, 277.15f, 2400f, true, DlcManager.DLC4, null);
		}

		// Token: 0x02002165 RID: 8549
		public class RECIPES
		{
			// Token: 0x04009942 RID: 39234
			public static float SMALL_COOK_TIME = 30f;

			// Token: 0x04009943 RID: 39235
			public static float STANDARD_COOK_TIME = 50f;
		}
	}
}
