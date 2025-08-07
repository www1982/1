using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000141 RID: 321
public class OilFloaterDecorConfig : IEntityConfig
{
	// Token: 0x06000602 RID: 1538 RVA: 0x0002CC94 File Offset: 0x0002AE94
	public static GameObject CreateOilFloater(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseOilFloaterConfig.BaseOilFloater(id, name, desc, anim_file, "OilfloaterDecorBaseTrait", 273.15f, 323.15f, 223.15f, 373.15f, is_baby, "oxy_");
		gameObject.AddOrGet<DecorProvider>().SetValues(DECOR.BONUS.TIER6);
		EntityTemplates.ExtendEntityToWildCreature(gameObject, OilFloaterTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("OilfloaterDecorBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, OilFloaterTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -OilFloaterTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 150f, name, false, false, true));
		return BaseOilFloaterConfig.SetupDiet(gameObject, SimHashes.Oxygen.CreateTag(), Tag.Invalid, OilFloaterDecorConfig.CALORIES_PER_KG_OF_ORE, 0f, null, 0f, 0f);
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x0002CDE8 File Offset: 0x0002AFE8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = OilFloaterDecorConfig.CreateOilFloater("OilfloaterDecor", global::STRINGS.CREATURES.SPECIES.OILFLOATER.VARIANT_DECOR.NAME, global::STRINGS.CREATURES.SPECIES.OILFLOATER.VARIANT_DECOR.DESC, "oilfloater_kanim", false);
		EntityTemplates.ExtendEntityToFertileCreature(gameObject, this as IHasDlcRestrictions, "OilfloaterDecorEgg", global::STRINGS.CREATURES.SPECIES.OILFLOATER.VARIANT_DECOR.EGG_NAME, global::STRINGS.CREATURES.SPECIES.OILFLOATER.VARIANT_DECOR.DESC, "egg_oilfloater_kanim", OilFloaterTuning.EGG_MASS, "OilfloaterDecorBaby", 90f, 30f, OilFloaterTuning.EGG_CHANCES_DECOR, OilFloaterDecorConfig.EGG_SORT_ORDER, true, false, 1f, false);
		return gameObject;
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x0002CE6A File Offset: 0x0002B06A
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x0002CE6C File Offset: 0x0002B06C
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400047C RID: 1148
	public const string ID = "OilfloaterDecor";

	// Token: 0x0400047D RID: 1149
	public const string BASE_TRAIT_ID = "OilfloaterDecorBaseTrait";

	// Token: 0x0400047E RID: 1150
	public const string EGG_ID = "OilfloaterDecorEgg";

	// Token: 0x0400047F RID: 1151
	public const SimHashes CONSUME_ELEMENT = SimHashes.Oxygen;

	// Token: 0x04000480 RID: 1152
	private static float KG_ORE_EATEN_PER_CYCLE = 30f;

	// Token: 0x04000481 RID: 1153
	private static float CALORIES_PER_KG_OF_ORE = OilFloaterTuning.STANDARD_CALORIES_PER_CYCLE / OilFloaterDecorConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000482 RID: 1154
	public static int EGG_SORT_ORDER = OilFloaterConfig.EGG_SORT_ORDER + 2;
}
