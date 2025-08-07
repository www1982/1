using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200015D RID: 349
public class SquirrelConfig : IEntityConfig
{
	// Token: 0x06000698 RID: 1688 RVA: 0x0002F0B4 File Offset: 0x0002D2B4
	public static GameObject CreateSquirrel(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseSquirrelConfig.BaseSquirrel(id, name, desc, anim_file, "SquirrelBaseTrait", is_baby, null, false), SquirrelTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("SquirrelBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, SquirrelTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -SquirrelTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		Diet.Info[] array = BaseSquirrelConfig.BasicDiet(SimHashes.Dirt.CreateTag(), SquirrelConfig.CALORIES_PER_DAY_OF_PLANT_EATEN, SquirrelConfig.KG_POOP_PER_DAY_OF_PLANT, null, 0f);
		GameObject gameObject2 = BaseSquirrelConfig.SetupDiet(gameObject, array, SquirrelConfig.MIN_POOP_SIZE_KG);
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x0002F1EC File Offset: 0x0002D3EC
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(SquirrelConfig.CreateSquirrel("Squirrel", CREATURES.SPECIES.SQUIRREL.NAME, CREATURES.SPECIES.SQUIRREL.DESC, "squirrel_kanim", false), this as IHasDlcRestrictions, "SquirrelEgg", CREATURES.SPECIES.SQUIRREL.EGG_NAME, CREATURES.SPECIES.SQUIRREL.DESC, "egg_squirrel_kanim", SquirrelTuning.EGG_MASS, "SquirrelBaby", 60.000004f, 20f, SquirrelTuning.EGG_CHANCES_BASE, SquirrelConfig.EGG_SORT_ORDER, true, false, 1f, false);
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x0002F26C File Offset: 0x0002D46C
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x0002F26E File Offset: 0x0002D46E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004F4 RID: 1268
	public const string ID = "Squirrel";

	// Token: 0x040004F5 RID: 1269
	public const string BASE_TRAIT_ID = "SquirrelBaseTrait";

	// Token: 0x040004F6 RID: 1270
	public const string EGG_ID = "SquirrelEgg";

	// Token: 0x040004F7 RID: 1271
	public const float OXYGEN_RATE = 0.023437504f;

	// Token: 0x040004F8 RID: 1272
	public const float BABY_OXYGEN_RATE = 0.011718752f;

	// Token: 0x040004F9 RID: 1273
	private const SimHashes EMIT_ELEMENT = SimHashes.Dirt;

	// Token: 0x040004FA RID: 1274
	public static float DAYS_PLANT_GROWTH_EATEN_PER_CYCLE = 0.4f;

	// Token: 0x040004FB RID: 1275
	private static float CALORIES_PER_DAY_OF_PLANT_EATEN = SquirrelTuning.STANDARD_CALORIES_PER_CYCLE / SquirrelConfig.DAYS_PLANT_GROWTH_EATEN_PER_CYCLE;

	// Token: 0x040004FC RID: 1276
	private static float KG_POOP_PER_DAY_OF_PLANT = 50f;

	// Token: 0x040004FD RID: 1277
	private static float MIN_POOP_SIZE_KG = 40f;

	// Token: 0x040004FE RID: 1278
	public static int EGG_SORT_ORDER = 0;
}
