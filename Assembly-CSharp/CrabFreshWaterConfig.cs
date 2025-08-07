using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000110 RID: 272
[EntityConfigOrder(1)]
public class CrabFreshWaterConfig : IEntityConfig
{
	// Token: 0x060004F6 RID: 1270 RVA: 0x000284AC File Offset: 0x000266AC
	public static GameObject CreateCrabFreshWater(string id, string name, string desc, string anim_file, bool is_baby, string deathDropID = null, int deathDropCount = 0)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseCrabConfig.BaseCrab(id, name, desc, anim_file, "CrabFreshWaterBaseTrait", is_baby, CrabFreshWaterConfig.animPrefix, deathDropID, (float)deathDropCount), CrabTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("CrabFreshWaterBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, CrabTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -CrabTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = BaseCrabConfig.DietWithSlime(SimHashes.Sand.CreateTag(), CrabFreshWaterConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL, null, 0f);
		return BaseCrabConfig.SetupDiet(gameObject, list, CrabFreshWaterConfig.CALORIES_PER_KG_OF_ORE, CrabFreshWaterConfig.MIN_POOP_SIZE_IN_KG);
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x000285E8 File Offset: 0x000267E8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = CrabFreshWaterConfig.CreateCrabFreshWater("CrabFreshWater", global::STRINGS.CREATURES.SPECIES.CRAB.VARIANT_FRESH_WATER.NAME, global::STRINGS.CREATURES.SPECIES.CRAB.VARIANT_FRESH_WATER.DESC, "pincher_kanim", false, "ShellfishMeat", 4);
		gameObject = EntityTemplates.ExtendEntityToFertileCreature(gameObject, this as IHasDlcRestrictions, "CrabFreshWaterEgg", global::STRINGS.CREATURES.SPECIES.CRAB.VARIANT_FRESH_WATER.EGG_NAME, global::STRINGS.CREATURES.SPECIES.CRAB.VARIANT_FRESH_WATER.DESC, "egg_pincher_kanim", CrabTuning.EGG_MASS, "CrabFreshWaterBaby", 60.000004f, 20f, CrabTuning.EGG_CHANCES_FRESH, CrabFreshWaterConfig.EGG_SORT_ORDER, true, false, 1f, false);
		EggProtectionMonitor.Def def = gameObject.AddOrGetDef<EggProtectionMonitor.Def>();
		def.allyTags = new Tag[] { GameTags.Creatures.CrabFriend };
		def.animPrefix = CrabFreshWaterConfig.animPrefix;
		DiseaseEmitter diseaseEmitter = gameObject.AddComponent<DiseaseEmitter>();
		List<Disease> list = new List<Disease>
		{
			Db.Get().Diseases.FoodGerms,
			Db.Get().Diseases.PollenGerms,
			Db.Get().Diseases.SlimeGerms,
			Db.Get().Diseases.ZombieSpores
		};
		if (DlcManager.IsExpansion1Active())
		{
			list.Add(Db.Get().Diseases.RadiationPoisoning);
		}
		diseaseEmitter.SetDiseases(list);
		diseaseEmitter.emitRange = 2;
		diseaseEmitter.emitCount = -1 * Mathf.RoundToInt((float)DUPLICANTSTATS.STANDARD.Secretions.DISEASE_PER_PEE / 600f * 6f * 2f * 4f / 9f);
		CleaningMonitor.Def def2 = gameObject.AddOrGetDef<CleaningMonitor.Def>();
		def2.elementState = Element.State.Liquid;
		def2.cellOffsets = new CellOffset[]
		{
			new CellOffset(1, 0),
			new CellOffset(-1, 0),
			new CellOffset(0, 1),
			new CellOffset(-1, 1),
			new CellOffset(1, 1)
		};
		def2.coolDown = 30f;
		return gameObject;
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x000287CB File Offset: 0x000269CB
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060004F9 RID: 1273 RVA: 0x000287CD File Offset: 0x000269CD
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400038C RID: 908
	public const string ID = "CrabFreshWater";

	// Token: 0x0400038D RID: 909
	public const string BASE_TRAIT_ID = "CrabFreshWaterBaseTrait";

	// Token: 0x0400038E RID: 910
	public const string EGG_ID = "CrabFreshWaterEgg";

	// Token: 0x0400038F RID: 911
	private const SimHashes EMIT_ELEMENT = SimHashes.Sand;

	// Token: 0x04000390 RID: 912
	private static float KG_ORE_EATEN_PER_CYCLE = 70f;

	// Token: 0x04000391 RID: 913
	private static float CALORIES_PER_KG_OF_ORE = CrabTuning.STANDARD_CALORIES_PER_CYCLE / CrabFreshWaterConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000392 RID: 914
	private static float MIN_POOP_SIZE_IN_KG = 25f;

	// Token: 0x04000393 RID: 915
	public static int EGG_SORT_ORDER = 0;

	// Token: 0x04000394 RID: 916
	private static string animPrefix = "fresh_";
}
