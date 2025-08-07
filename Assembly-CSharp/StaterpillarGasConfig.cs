using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000163 RID: 355
public class StaterpillarGasConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060006BA RID: 1722 RVA: 0x0002F7F4 File Offset: 0x0002D9F4
	public static GameObject CreateStaterpillarGas(string id, string name, string desc, string anim_file, bool is_baby)
	{
		InhaleStates.Def def = new InhaleStates.Def
		{
			behaviourTag = GameTags.Creatures.WantsToStore,
			inhaleAnimPre = "gas_consume_pre",
			inhaleAnimLoop = "gas_consume_loop",
			inhaleAnimPst = "gas_consume_pst",
			useStorage = true,
			alwaysPlayPstAnim = true,
			inhaleTime = StaterpillarGasConfig.INHALE_TIME,
			storageStatusItem = Db.Get().CreatureStatusItems.LookingForGas
		};
		GameObject gameObject = BaseStaterpillarConfig.BaseStaterpillar(id, name, desc, anim_file, "StaterpillarGasBaseTrait", is_baby, ObjectLayer.GasConduit, StaterpillarGasConnectorConfig.ID, GameTags.Unbreathable, "gas_", 263.15f, 313.15f, 173.15f, 373.15f, def);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, global::TUNING.CREATURES.SPACE_REQUIREMENTS.TIER3);
		if (!is_baby)
		{
			GasAndLiquidConsumerMonitor.Def def2 = gameObject.AddOrGetDef<GasAndLiquidConsumerMonitor.Def>();
			def2.behaviourTag = GameTags.Creatures.WantsToStore;
			def2.consumableElementTag = GameTags.Unbreathable;
			def2.transitionTag = new Tag[] { GameTags.Creature };
			def2.minCooldown = StaterpillarGasConfig.COOLDOWN_MIN;
			def2.maxCooldown = StaterpillarGasConfig.COOLDOWN_MAX;
			def2.consumptionRate = StaterpillarGasConfig.CONSUMPTION_RATE;
		}
		Trait trait = Db.Get().CreateTrait("StaterpillarGasBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, StaterpillarTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -StaterpillarTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = new List<Diet.Info>();
		list.AddRange(BaseStaterpillarConfig.RawMetalDiet(SimHashes.Hydrogen.CreateTag(), StaterpillarGasConfig.CALORIES_PER_KG_OF_ORE, StaterpillarTuning.POOP_CONVERSTION_RATE, null, 0f));
		list.AddRange(BaseStaterpillarConfig.RefinedMetalDiet(SimHashes.Hydrogen.CreateTag(), StaterpillarGasConfig.CALORIES_PER_KG_OF_ORE, StaterpillarTuning.POOP_CONVERSTION_RATE, null, 0f));
		gameObject = BaseStaterpillarConfig.SetupDiet(gameObject, list);
		Storage storage = gameObject.AddComponent<Storage>();
		storage.capacityKg = StaterpillarGasConfig.STORAGE_CAPACITY;
		storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		return gameObject;
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x0002FA4B File Offset: 0x0002DC4B
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x0002FA52 File Offset: 0x0002DC52
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x0002FA58 File Offset: 0x0002DC58
	public virtual GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(StaterpillarGasConfig.CreateStaterpillarGas("StaterpillarGas", global::STRINGS.CREATURES.SPECIES.STATERPILLAR.VARIANT_GAS.NAME, global::STRINGS.CREATURES.SPECIES.STATERPILLAR.VARIANT_GAS.DESC, "caterpillar_kanim", false), this, "StaterpillarGasEgg", global::STRINGS.CREATURES.SPECIES.STATERPILLAR.VARIANT_GAS.EGG_NAME, global::STRINGS.CREATURES.SPECIES.STATERPILLAR.VARIANT_GAS.DESC, "egg_caterpillar_kanim", StaterpillarTuning.EGG_MASS, "StaterpillarGasBaby", 60.000004f, 20f, StaterpillarTuning.EGG_CHANCES_GAS, 1, true, false, 1f, false);
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x0002FACF File Offset: 0x0002DCCF
	public void OnPrefabInit(GameObject prefab)
	{
		KBatchedAnimController component = prefab.GetComponent<KBatchedAnimController>();
		component.SetSymbolVisiblity("electric_bolt_c_bloom", false);
		component.SetSymbolVisiblity("gulp", false);
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x0002FAF8 File Offset: 0x0002DCF8
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000513 RID: 1299
	public const string ID = "StaterpillarGas";

	// Token: 0x04000514 RID: 1300
	public const string BASE_TRAIT_ID = "StaterpillarGasBaseTrait";

	// Token: 0x04000515 RID: 1301
	public const string EGG_ID = "StaterpillarGasEgg";

	// Token: 0x04000516 RID: 1302
	public const int EGG_SORT_ORDER = 1;

	// Token: 0x04000517 RID: 1303
	private static float KG_ORE_EATEN_PER_CYCLE = 30f;

	// Token: 0x04000518 RID: 1304
	private static float CALORIES_PER_KG_OF_ORE = StaterpillarTuning.STANDARD_CALORIES_PER_CYCLE / StaterpillarGasConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000519 RID: 1305
	private static float STORAGE_CAPACITY = 100f;

	// Token: 0x0400051A RID: 1306
	private static float COOLDOWN_MIN = 20f;

	// Token: 0x0400051B RID: 1307
	private static float COOLDOWN_MAX = 40f;

	// Token: 0x0400051C RID: 1308
	private static float CONSUMPTION_RATE = 0.5f;

	// Token: 0x0400051D RID: 1309
	private static float INHALE_TIME = 6f;
}
