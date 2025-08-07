using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000165 RID: 357
public class StaterpillarLiquidConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060006C8 RID: 1736 RVA: 0x0002FBC8 File Offset: 0x0002DDC8
	public static GameObject CreateStaterpillarLiquid(string id, string name, string desc, string anim_file, bool is_baby)
	{
		InhaleStates.Def def = new InhaleStates.Def
		{
			behaviourTag = GameTags.Creatures.WantsToStore,
			inhaleAnimPre = "liquid_consume_pre",
			inhaleAnimLoop = "liquid_consume_loop",
			inhaleAnimPst = "liquid_consume_pst",
			useStorage = true,
			alwaysPlayPstAnim = true,
			inhaleTime = StaterpillarLiquidConfig.INHALE_TIME,
			storageStatusItem = Db.Get().CreatureStatusItems.LookingForLiquid
		};
		GameObject gameObject = BaseStaterpillarConfig.BaseStaterpillar(id, name, desc, anim_file, "StaterpillarLiquidBaseTrait", is_baby, ObjectLayer.LiquidConduit, StaterpillarLiquidConnectorConfig.ID, GameTags.Unbreathable, "wtr_", 263.15f, 313.15f, 173.15f, 373.15f, def);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, global::TUNING.CREATURES.SPACE_REQUIREMENTS.TIER3);
		if (!is_baby)
		{
			GasAndLiquidConsumerMonitor.Def def2 = gameObject.AddOrGetDef<GasAndLiquidConsumerMonitor.Def>();
			def2.behaviourTag = GameTags.Creatures.WantsToStore;
			def2.consumableElementTag = GameTags.Liquid;
			def2.transitionTag = new Tag[] { GameTags.Creature };
			def2.minCooldown = StaterpillarLiquidConfig.COOLDOWN_MIN;
			def2.maxCooldown = StaterpillarLiquidConfig.COOLDOWN_MAX;
			def2.consumptionRate = StaterpillarLiquidConfig.CONSUMPTION_RATE;
		}
		Trait trait = Db.Get().CreateTrait("StaterpillarLiquidBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, StaterpillarTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -StaterpillarTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = new List<Diet.Info>();
		list.AddRange(BaseStaterpillarConfig.RawMetalDiet(SimHashes.Hydrogen.CreateTag(), StaterpillarLiquidConfig.CALORIES_PER_KG_OF_ORE, StaterpillarTuning.POOP_CONVERSTION_RATE, null, 0f));
		list.AddRange(BaseStaterpillarConfig.RefinedMetalDiet(SimHashes.Hydrogen.CreateTag(), StaterpillarLiquidConfig.CALORIES_PER_KG_OF_ORE, StaterpillarTuning.POOP_CONVERSTION_RATE, null, 0f));
		gameObject = BaseStaterpillarConfig.SetupDiet(gameObject, list);
		Storage storage = gameObject.AddComponent<Storage>();
		storage.capacityKg = StaterpillarLiquidConfig.STORAGE_CAPACITY;
		storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		return gameObject;
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x0002FE1F File Offset: 0x0002E01F
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x0002FE26 File Offset: 0x0002E026
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x0002FE2C File Offset: 0x0002E02C
	public virtual GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(StaterpillarLiquidConfig.CreateStaterpillarLiquid("StaterpillarLiquid", global::STRINGS.CREATURES.SPECIES.STATERPILLAR.VARIANT_LIQUID.NAME, global::STRINGS.CREATURES.SPECIES.STATERPILLAR.VARIANT_LIQUID.DESC, "caterpillar_kanim", false), this, "StaterpillarLiquidEgg", global::STRINGS.CREATURES.SPECIES.STATERPILLAR.VARIANT_LIQUID.EGG_NAME, global::STRINGS.CREATURES.SPECIES.STATERPILLAR.VARIANT_LIQUID.DESC, "egg_caterpillar_kanim", StaterpillarTuning.EGG_MASS, "StaterpillarLiquidBaby", 60.000004f, 20f, StaterpillarTuning.EGG_CHANCES_LIQUID, 2, true, false, 1f, false);
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x0002FEA3 File Offset: 0x0002E0A3
	public void OnPrefabInit(GameObject prefab)
	{
		KBatchedAnimController component = prefab.GetComponent<KBatchedAnimController>();
		component.SetSymbolVisiblity("electric_bolt_c_bloom", false);
		component.SetSymbolVisiblity("gulp", false);
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x0002FECC File Offset: 0x0002E0CC
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400051F RID: 1311
	public const string ID = "StaterpillarLiquid";

	// Token: 0x04000520 RID: 1312
	public const string BASE_TRAIT_ID = "StaterpillarLiquidBaseTrait";

	// Token: 0x04000521 RID: 1313
	public const string EGG_ID = "StaterpillarLiquidEgg";

	// Token: 0x04000522 RID: 1314
	public const int EGG_SORT_ORDER = 2;

	// Token: 0x04000523 RID: 1315
	private static float KG_ORE_EATEN_PER_CYCLE = 30f;

	// Token: 0x04000524 RID: 1316
	private static float CALORIES_PER_KG_OF_ORE = StaterpillarTuning.STANDARD_CALORIES_PER_CYCLE / StaterpillarLiquidConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000525 RID: 1317
	private static float STORAGE_CAPACITY = 1000f;

	// Token: 0x04000526 RID: 1318
	private static float COOLDOWN_MIN = 20f;

	// Token: 0x04000527 RID: 1319
	private static float COOLDOWN_MAX = 40f;

	// Token: 0x04000528 RID: 1320
	private static float CONSUMPTION_RATE = 10f;

	// Token: 0x04000529 RID: 1321
	private static float INHALE_TIME = 6f;
}
