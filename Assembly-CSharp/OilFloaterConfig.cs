using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200013F RID: 319
public class OilFloaterConfig : IEntityConfig
{
	// Token: 0x060005F8 RID: 1528 RVA: 0x0002CA3C File Offset: 0x0002AC3C
	public static GameObject CreateOilFloater(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseOilFloaterConfig.BaseOilFloater(id, name, desc, anim_file, "OilfloaterBaseTrait", 323.15f, 413.15f, 273.15f, 473.15f, is_baby, null);
		EntityTemplates.ExtendEntityToWildCreature(gameObject, OilFloaterTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("OilfloaterBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, OilFloaterTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -OilFloaterTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		GameObject gameObject2 = BaseOilFloaterConfig.SetupDiet(gameObject, SimHashes.CarbonDioxide.CreateTag(), SimHashes.CrudeOil.CreateTag(), OilFloaterConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL, null, 0f, OilFloaterConfig.MIN_POOP_SIZE_IN_KG);
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x0002CB8C File Offset: 0x0002AD8C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = OilFloaterConfig.CreateOilFloater("Oilfloater", global::STRINGS.CREATURES.SPECIES.OILFLOATER.NAME, global::STRINGS.CREATURES.SPECIES.OILFLOATER.DESC, "oilfloater_kanim", false);
		EntityTemplates.ExtendEntityToFertileCreature(gameObject, this as IHasDlcRestrictions, "OilfloaterEgg", global::STRINGS.CREATURES.SPECIES.OILFLOATER.EGG_NAME, global::STRINGS.CREATURES.SPECIES.OILFLOATER.DESC, "egg_oilfloater_kanim", OilFloaterTuning.EGG_MASS, "OilfloaterBaby", 60.000004f, 20f, OilFloaterTuning.EGG_CHANCES_BASE, OilFloaterConfig.EGG_SORT_ORDER, true, false, 1f, false);
		return gameObject;
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x0002CC0E File Offset: 0x0002AE0E
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x0002CC10 File Offset: 0x0002AE10
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000472 RID: 1138
	public const string ID = "Oilfloater";

	// Token: 0x04000473 RID: 1139
	public const string BASE_TRAIT_ID = "OilfloaterBaseTrait";

	// Token: 0x04000474 RID: 1140
	public const string EGG_ID = "OilfloaterEgg";

	// Token: 0x04000475 RID: 1141
	public const SimHashes CONSUME_ELEMENT = SimHashes.CarbonDioxide;

	// Token: 0x04000476 RID: 1142
	public const SimHashes EMIT_ELEMENT = SimHashes.CrudeOil;

	// Token: 0x04000477 RID: 1143
	private static float KG_ORE_EATEN_PER_CYCLE = 20f;

	// Token: 0x04000478 RID: 1144
	private static float CALORIES_PER_KG_OF_ORE = OilFloaterTuning.STANDARD_CALORIES_PER_CYCLE / OilFloaterConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000479 RID: 1145
	private static float MIN_POOP_SIZE_IN_KG = 0.5f;

	// Token: 0x0400047A RID: 1146
	public static int EGG_SORT_ORDER = 400;
}
