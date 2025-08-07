using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200010E RID: 270
[EntityConfigOrder(1)]
public class CrabConfig : IEntityConfig
{
	// Token: 0x060004EC RID: 1260 RVA: 0x00028214 File Offset: 0x00026414
	public static GameObject CreateCrab(string id, string name, string desc, string anim_file, bool is_baby, string deathDropID, float deathDropCount)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseCrabConfig.BaseCrab(id, name, desc, anim_file, "CrabBaseTrait", is_baby, null, deathDropID, deathDropCount), CrabTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("CrabBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, CrabTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -CrabTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = BaseCrabConfig.BasicDiet(SimHashes.Sand.CreateTag(), CrabConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL, null, 0f);
		GameObject gameObject2 = BaseCrabConfig.SetupDiet(gameObject, list, CrabConfig.CALORIES_PER_KG_OF_ORE, CrabConfig.MIN_POOP_SIZE_IN_KG);
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x060004ED RID: 1261 RVA: 0x00028354 File Offset: 0x00026554
	public GameObject CreatePrefab()
	{
		GameObject gameObject = CrabConfig.CreateCrab("Crab", global::STRINGS.CREATURES.SPECIES.CRAB.NAME, global::STRINGS.CREATURES.SPECIES.CRAB.DESC, "pincher_kanim", false, "CrabShell", 10f);
		gameObject = EntityTemplates.ExtendEntityToFertileCreature(gameObject, this as IHasDlcRestrictions, "CrabEgg", global::STRINGS.CREATURES.SPECIES.CRAB.EGG_NAME, global::STRINGS.CREATURES.SPECIES.CRAB.DESC, "egg_pincher_kanim", CrabTuning.EGG_MASS, "CrabBaby", 60.000004f, 20f, CrabTuning.EGG_CHANCES_BASE, CrabConfig.EGG_SORT_ORDER, true, false, 1f, false);
		gameObject.AddOrGetDef<EggProtectionMonitor.Def>().allyTags = new Tag[] { GameTags.Creatures.CrabFriend };
		return gameObject;
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x000283FF File Offset: 0x000265FF
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x00028401 File Offset: 0x00026601
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000383 RID: 899
	public const string ID = "Crab";

	// Token: 0x04000384 RID: 900
	public const string BASE_TRAIT_ID = "CrabBaseTrait";

	// Token: 0x04000385 RID: 901
	public const string EGG_ID = "CrabEgg";

	// Token: 0x04000386 RID: 902
	private const SimHashes EMIT_ELEMENT = SimHashes.Sand;

	// Token: 0x04000387 RID: 903
	private static float KG_ORE_EATEN_PER_CYCLE = 70f;

	// Token: 0x04000388 RID: 904
	private static float CALORIES_PER_KG_OF_ORE = CrabTuning.STANDARD_CALORIES_PER_CYCLE / CrabConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000389 RID: 905
	private static float MIN_POOP_SIZE_IN_KG = 25f;

	// Token: 0x0400038A RID: 906
	public static int EGG_SORT_ORDER = 0;
}
