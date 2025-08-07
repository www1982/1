using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200015F RID: 351
public class SquirrelHugConfig : IEntityConfig
{
	// Token: 0x060006A2 RID: 1698 RVA: 0x0002F2F8 File Offset: 0x0002D4F8
	public static GameObject CreateSquirrelHug(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseSquirrelConfig.BaseSquirrel(id, name, desc, anim_file, "SquirrelHugBaseTrait", is_baby, "hug_", true);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, SquirrelTuning.PEN_SIZE_PER_CREATURE_HUG);
		gameObject.AddOrGet<DecorProvider>().SetValues(DECOR.BONUS.TIER3);
		Trait trait = Db.Get().CreateTrait("SquirrelHugBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, SquirrelTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -SquirrelTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		Diet.Info[] array = BaseSquirrelConfig.BasicDiet(SimHashes.Dirt.CreateTag(), SquirrelHugConfig.CALORIES_PER_DAY_OF_PLANT_EATEN, SquirrelHugConfig.KG_POOP_PER_DAY_OF_PLANT, null, 0f);
		gameObject = BaseSquirrelConfig.SetupDiet(gameObject, array, SquirrelHugConfig.MIN_POOP_SIZE_KG);
		if (!is_baby)
		{
			gameObject.AddOrGetDef<HugMonitor.Def>();
		}
		return gameObject;
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x0002F44C File Offset: 0x0002D64C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(SquirrelHugConfig.CreateSquirrelHug("SquirrelHug", global::STRINGS.CREATURES.SPECIES.SQUIRREL.VARIANT_HUG.NAME, global::STRINGS.CREATURES.SPECIES.SQUIRREL.VARIANT_HUG.DESC, "squirrel_kanim", false), this as IHasDlcRestrictions, "SquirrelHugEgg", global::STRINGS.CREATURES.SPECIES.SQUIRREL.VARIANT_HUG.EGG_NAME, global::STRINGS.CREATURES.SPECIES.SQUIRREL.VARIANT_HUG.DESC, "egg_squirrel_kanim", SquirrelTuning.EGG_MASS, "SquirrelHugBaby", 60.000004f, 20f, SquirrelTuning.EGG_CHANCES_HUG, SquirrelHugConfig.EGG_SORT_ORDER, true, false, 1f, false);
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x0002F4CC File Offset: 0x0002D6CC
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x0002F4CE File Offset: 0x0002D6CE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000500 RID: 1280
	public const string ID = "SquirrelHug";

	// Token: 0x04000501 RID: 1281
	public const string BASE_TRAIT_ID = "SquirrelHugBaseTrait";

	// Token: 0x04000502 RID: 1282
	public const string EGG_ID = "SquirrelHugEgg";

	// Token: 0x04000503 RID: 1283
	public const float OXYGEN_RATE = 0.023437504f;

	// Token: 0x04000504 RID: 1284
	public const float BABY_OXYGEN_RATE = 0.011718752f;

	// Token: 0x04000505 RID: 1285
	private const SimHashes EMIT_ELEMENT = SimHashes.Dirt;

	// Token: 0x04000506 RID: 1286
	public static float DAYS_PLANT_GROWTH_EATEN_PER_CYCLE = 0.5f;

	// Token: 0x04000507 RID: 1287
	private static float CALORIES_PER_DAY_OF_PLANT_EATEN = SquirrelTuning.STANDARD_CALORIES_PER_CYCLE / SquirrelHugConfig.DAYS_PLANT_GROWTH_EATEN_PER_CYCLE;

	// Token: 0x04000508 RID: 1288
	private static float KG_POOP_PER_DAY_OF_PLANT = 25f;

	// Token: 0x04000509 RID: 1289
	private static float MIN_POOP_SIZE_KG = 40f;

	// Token: 0x0400050A RID: 1290
	public static int EGG_SORT_ORDER = 0;
}
