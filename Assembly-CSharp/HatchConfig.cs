using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200011F RID: 287
[EntityConfigOrder(1)]
public class HatchConfig : IEntityConfig
{
	// Token: 0x0600054C RID: 1356 RVA: 0x00029E50 File Offset: 0x00028050
	public static GameObject CreateHatch(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseHatchConfig.BaseHatch(id, name, desc, anim_file, "HatchBaseTrait", is_baby, null), HatchTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("HatchBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, HatchTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -HatchTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = BaseHatchConfig.BasicRockDiet(SimHashes.Carbon.CreateTag(), HatchConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL, null, 0f);
		list.AddRange(BaseHatchConfig.FoodDiet(SimHashes.Carbon.CreateTag(), HatchConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.GOOD_1, null, 0f));
		GameObject gameObject2 = BaseHatchConfig.SetupDiet(gameObject, list, HatchConfig.CALORIES_PER_KG_OF_ORE, HatchConfig.MIN_POOP_SIZE_IN_KG);
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x0600054D RID: 1357 RVA: 0x00029FB4 File Offset: 0x000281B4
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(HatchConfig.CreateHatch("Hatch", global::STRINGS.CREATURES.SPECIES.HATCH.NAME, global::STRINGS.CREATURES.SPECIES.HATCH.DESC, "hatch_kanim", false), this as IHasDlcRestrictions, "HatchEgg", global::STRINGS.CREATURES.SPECIES.HATCH.EGG_NAME, global::STRINGS.CREATURES.SPECIES.HATCH.DESC, "egg_hatch_kanim", HatchTuning.EGG_MASS, "HatchBaby", 60.000004f, 20f, HatchTuning.EGG_CHANCES_BASE, HatchConfig.EGG_SORT_ORDER, true, false, 1f, false);
	}

	// Token: 0x0600054E RID: 1358 RVA: 0x0002A034 File Offset: 0x00028234
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600054F RID: 1359 RVA: 0x0002A036 File Offset: 0x00028236
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003EA RID: 1002
	public const string ID = "Hatch";

	// Token: 0x040003EB RID: 1003
	public const string BASE_TRAIT_ID = "HatchBaseTrait";

	// Token: 0x040003EC RID: 1004
	public const string EGG_ID = "HatchEgg";

	// Token: 0x040003ED RID: 1005
	private const SimHashes EMIT_ELEMENT = SimHashes.Carbon;

	// Token: 0x040003EE RID: 1006
	private static float KG_ORE_EATEN_PER_CYCLE = 140f;

	// Token: 0x040003EF RID: 1007
	private static float CALORIES_PER_KG_OF_ORE = HatchTuning.STANDARD_CALORIES_PER_CYCLE / HatchConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040003F0 RID: 1008
	private static float MIN_POOP_SIZE_IN_KG = 25f;

	// Token: 0x040003F1 RID: 1009
	public static int EGG_SORT_ORDER = 0;
}
