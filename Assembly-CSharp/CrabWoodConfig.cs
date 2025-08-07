using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000112 RID: 274
[EntityConfigOrder(1)]
public class CrabWoodConfig : IEntityConfig
{
	// Token: 0x06000500 RID: 1280 RVA: 0x0002886C File Offset: 0x00026A6C
	public static GameObject CreateCrabWood(string id, string name, string desc, string anim_file, bool is_baby, string deathDropID = "CrabWoodShell", float deathDropCount = 5f)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseCrabConfig.BaseCrab(id, name, desc, anim_file, "CrabWoodBaseTrait", is_baby, CrabWoodConfig.animPrefix, deathDropID, deathDropCount), CrabTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("CrabWoodBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, CrabTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -CrabTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = BaseCrabConfig.DietWithSlime(SimHashes.Sand.CreateTag(), CrabWoodConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.BAD_1, null, 0f);
		return BaseCrabConfig.SetupDiet(gameObject, list, CrabWoodConfig.CALORIES_PER_KG_OF_ORE, CrabWoodConfig.MIN_POOP_SIZE_IN_KG);
	}

	// Token: 0x06000501 RID: 1281 RVA: 0x000289A8 File Offset: 0x00026BA8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = CrabWoodConfig.CreateCrabWood("CrabWood", global::STRINGS.CREATURES.SPECIES.CRAB.VARIANT_WOOD.NAME, global::STRINGS.CREATURES.SPECIES.CRAB.VARIANT_WOOD.DESC, "pincher_kanim", false, "CrabWoodShell", 500f);
		gameObject = EntityTemplates.ExtendEntityToFertileCreature(gameObject, this as IHasDlcRestrictions, "CrabWoodEgg", global::STRINGS.CREATURES.SPECIES.CRAB.VARIANT_WOOD.EGG_NAME, global::STRINGS.CREATURES.SPECIES.CRAB.VARIANT_WOOD.DESC, "egg_pincher_kanim", CrabTuning.EGG_MASS, "CrabWoodBaby", 60.000004f, 20f, CrabTuning.EGG_CHANCES_WOOD, CrabWoodConfig.EGG_SORT_ORDER, true, false, 1f, false);
		EggProtectionMonitor.Def def = gameObject.AddOrGetDef<EggProtectionMonitor.Def>();
		def.allyTags = new Tag[] { GameTags.Creatures.CrabFriend };
		def.animPrefix = CrabWoodConfig.animPrefix;
		MoltDropperMonitor.Def def2 = gameObject.AddOrGetDef<MoltDropperMonitor.Def>();
		def2.onGrowDropID = "CrabWoodShell";
		def2.massToDrop = 100f;
		def2.isReadyToMolt = new Func<MoltDropperMonitor.Instance, bool>(CrabTuning.IsReadyToMolt);
		return gameObject;
	}

	// Token: 0x06000502 RID: 1282 RVA: 0x00028A8D File Offset: 0x00026C8D
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000503 RID: 1283 RVA: 0x00028A8F File Offset: 0x00026C8F
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000396 RID: 918
	public const string ID = "CrabWood";

	// Token: 0x04000397 RID: 919
	public const string BASE_TRAIT_ID = "CrabWoodBaseTrait";

	// Token: 0x04000398 RID: 920
	public const string EGG_ID = "CrabWoodEgg";

	// Token: 0x04000399 RID: 921
	private const SimHashes EMIT_ELEMENT = SimHashes.Sand;

	// Token: 0x0400039A RID: 922
	private static float KG_ORE_EATEN_PER_CYCLE = 70f;

	// Token: 0x0400039B RID: 923
	private static float CALORIES_PER_KG_OF_ORE = CrabTuning.STANDARD_CALORIES_PER_CYCLE / CrabWoodConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x0400039C RID: 924
	private static float MIN_POOP_SIZE_IN_KG = 25f;

	// Token: 0x0400039D RID: 925
	public static int EGG_SORT_ORDER = 0;

	// Token: 0x0400039E RID: 926
	private static string animPrefix = "wood_";
}
