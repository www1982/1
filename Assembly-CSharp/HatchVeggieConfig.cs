using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000125 RID: 293
[EntityConfigOrder(1)]
public class HatchVeggieConfig : IEntityConfig
{
	// Token: 0x0600056B RID: 1387 RVA: 0x0002A584 File Offset: 0x00028784
	public static GameObject CreateHatch(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseHatchConfig.BaseHatch(id, name, desc, anim_file, "HatchVeggieBaseTrait", is_baby, "veg_"), HatchTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("HatchVeggieBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, HatchTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -HatchTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = BaseHatchConfig.VeggieDiet(SimHashes.Carbon.CreateTag(), HatchVeggieConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.GOOD_3, null, 0f);
		list.AddRange(BaseHatchConfig.FoodDiet(SimHashes.Carbon.CreateTag(), HatchVeggieConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.GOOD_3, null, 0f));
		return BaseHatchConfig.SetupDiet(gameObject, list, HatchVeggieConfig.CALORIES_PER_KG_OF_ORE, HatchVeggieConfig.MIN_POOP_SIZE_IN_KG);
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x0002A6E0 File Offset: 0x000288E0
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(HatchVeggieConfig.CreateHatch("HatchVeggie", global::STRINGS.CREATURES.SPECIES.HATCH.VARIANT_VEGGIE.NAME, global::STRINGS.CREATURES.SPECIES.HATCH.VARIANT_VEGGIE.DESC, "hatch_kanim", false), this as IHasDlcRestrictions, "HatchVeggieEgg", global::STRINGS.CREATURES.SPECIES.HATCH.VARIANT_VEGGIE.EGG_NAME, global::STRINGS.CREATURES.SPECIES.HATCH.VARIANT_VEGGIE.DESC, "egg_hatch_kanim", HatchTuning.EGG_MASS, "HatchVeggieBaby", 60.000004f, 20f, HatchTuning.EGG_CHANCES_VEGGIE, HatchVeggieConfig.EGG_SORT_ORDER, true, false, 1f, false);
	}

	// Token: 0x0600056D RID: 1389 RVA: 0x0002A760 File Offset: 0x00028960
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600056E RID: 1390 RVA: 0x0002A762 File Offset: 0x00028962
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000404 RID: 1028
	public const string ID = "HatchVeggie";

	// Token: 0x04000405 RID: 1029
	public const string BASE_TRAIT_ID = "HatchVeggieBaseTrait";

	// Token: 0x04000406 RID: 1030
	public const string EGG_ID = "HatchVeggieEgg";

	// Token: 0x04000407 RID: 1031
	private const SimHashes EMIT_ELEMENT = SimHashes.Carbon;

	// Token: 0x04000408 RID: 1032
	private static float KG_ORE_EATEN_PER_CYCLE = 140f;

	// Token: 0x04000409 RID: 1033
	private static float CALORIES_PER_KG_OF_ORE = HatchTuning.STANDARD_CALORIES_PER_CYCLE / HatchVeggieConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x0400040A RID: 1034
	private static float MIN_POOP_SIZE_IN_KG = 50f;

	// Token: 0x0400040B RID: 1035
	public static int EGG_SORT_ORDER = HatchConfig.EGG_SORT_ORDER + 1;
}
