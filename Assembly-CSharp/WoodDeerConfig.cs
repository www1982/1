using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000169 RID: 361
public class WoodDeerConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060006E3 RID: 1763 RVA: 0x000301EC File Offset: 0x0002E3EC
	public static GameObject CreateWoodDeer(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseDeerConfig.BaseDeer(id, name, desc, anim_file, "WoodDeerBaseTrait", is_baby, null), DeerTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("WoodDeerBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, 1000000f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -166.66667f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		GameObject gameObject2 = BaseDeerConfig.SetupDiet(gameObject, new List<Diet.Info>
		{
			BaseDeerConfig.CreateDietInfo("HardSkinBerryPlant", SimHashes.Dirt.CreateTag(), WoodDeerConfig.HARD_SKIN_CALORIES_PER_KG, WoodDeerConfig.POOP_MASS_CONVERSION_MULTIPLIER, null, 0f),
			new Diet.Info(new HashSet<Tag> { "HardSkinBerry" }, SimHashes.Dirt.CreateTag(), WoodDeerConfig.CONSUMABLE_PLANT_MATURITY_LEVELS * WoodDeerConfig.HARD_SKIN_CALORIES_PER_KG / 1f, WoodDeerConfig.POOP_MASS_CONVERSION_MULTIPLIER * 3f, null, 0f, false, Diet.Info.FoodType.EatSolid, false, null),
			BaseDeerConfig.CreateDietInfo("PrickleFlower", SimHashes.Dirt.CreateTag(), WoodDeerConfig.BRISTLE_CALORIES_PER_KG / 2f, WoodDeerConfig.POOP_MASS_CONVERSION_MULTIPLIER, null, 0f),
			new Diet.Info(new HashSet<Tag> { PrickleFruitConfig.ID }, SimHashes.Dirt.CreateTag(), WoodDeerConfig.CONSUMABLE_PLANT_MATURITY_LEVELS * WoodDeerConfig.BRISTLE_CALORIES_PER_KG / 1f, WoodDeerConfig.POOP_MASS_CONVERSION_MULTIPLIER * 6f, null, 0f, false, Diet.Info.FoodType.EatSolid, false, null)
		}.ToArray(), WoodDeerConfig.MIN_KG_CONSUMED_BEFORE_POOPING);
		gameObject2.AddTag(GameTags.OriginalCreature);
		WellFedShearable.Def def = gameObject2.AddOrGetDef<WellFedShearable.Def>();
		def.effectId = "WoodDeerWellFed";
		def.caloriesPerCycle = 100000f;
		def.growthDurationCycles = WoodDeerConfig.ANTLER_GROWTH_TIME_IN_CYCLES;
		def.dropMass = WoodDeerConfig.WOOD_MASS_PER_ANTLER;
		def.itemDroppedOnShear = WoodLogConfig.TAG;
		def.levelCount = 6;
		return gameObject2;
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x00030450 File Offset: 0x0002E650
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x00030457 File Offset: 0x0002E657
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x0003045C File Offset: 0x0002E65C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(WoodDeerConfig.CreateWoodDeer("WoodDeer", global::STRINGS.CREATURES.SPECIES.WOODDEER.NAME, global::STRINGS.CREATURES.SPECIES.WOODDEER.DESC, "ice_floof_kanim", false), this, "WoodDeerEgg", global::STRINGS.CREATURES.SPECIES.WOODDEER.EGG_NAME, global::STRINGS.CREATURES.SPECIES.WOODDEER.DESC, "egg_ice_floof_kanim", DeerTuning.EGG_MASS, "WoodDeerBaby", 60.000004f, 20f, DeerTuning.EGG_CHANCES_BASE, WoodDeerConfig.EGG_SORT_ORDER, true, false, 1f, false);
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x000304D7 File Offset: 0x0002E6D7
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x000304D9 File Offset: 0x0002E6D9
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000531 RID: 1329
	public const string ID = "WoodDeer";

	// Token: 0x04000532 RID: 1330
	public const string BASE_TRAIT_ID = "WoodDeerBaseTrait";

	// Token: 0x04000533 RID: 1331
	public const string EGG_ID = "WoodDeerEgg";

	// Token: 0x04000534 RID: 1332
	private const SimHashes EMIT_ELEMENT = SimHashes.Dirt;

	// Token: 0x04000535 RID: 1333
	public const float CALORIES_PER_PLANT_BITE = 100000f;

	// Token: 0x04000536 RID: 1334
	public const float DAYS_PLANT_GROWTH_EATEN_PER_CYCLE = 0.2f;

	// Token: 0x04000537 RID: 1335
	public static float CONSUMABLE_PLANT_MATURITY_LEVELS = CROPS.CROP_TYPES.Find((Crop.CropVal m) => m.cropId == "HardSkinBerry").cropDuration / 600f;

	// Token: 0x04000538 RID: 1336
	public static float KG_PLANT_EATEN_A_DAY = 0.2f * WoodDeerConfig.CONSUMABLE_PLANT_MATURITY_LEVELS;

	// Token: 0x04000539 RID: 1337
	public static float HARD_SKIN_CALORIES_PER_KG = 100000f / WoodDeerConfig.KG_PLANT_EATEN_A_DAY;

	// Token: 0x0400053A RID: 1338
	public static float BRISTLE_CALORIES_PER_KG = WoodDeerConfig.HARD_SKIN_CALORIES_PER_KG * 2f;

	// Token: 0x0400053B RID: 1339
	public static float ANTLER_GROWTH_TIME_IN_CYCLES = 6f;

	// Token: 0x0400053C RID: 1340
	public static float ANTLER_STARTING_GROWTH_PCT = 0.5f;

	// Token: 0x0400053D RID: 1341
	public static float WOOD_PER_CYCLE = 60f;

	// Token: 0x0400053E RID: 1342
	public static float WOOD_MASS_PER_ANTLER = WoodDeerConfig.WOOD_PER_CYCLE * WoodDeerConfig.ANTLER_GROWTH_TIME_IN_CYCLES;

	// Token: 0x0400053F RID: 1343
	private static float POOP_MASS_CONVERSION_MULTIPLIER = 8.333334f;

	// Token: 0x04000540 RID: 1344
	private static float MIN_KG_CONSUMED_BEFORE_POOPING = 1f;

	// Token: 0x04000541 RID: 1345
	public static int EGG_SORT_ORDER = 0;
}
