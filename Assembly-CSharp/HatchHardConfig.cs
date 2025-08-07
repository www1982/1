using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000121 RID: 289
[EntityConfigOrder(1)]
public class HatchHardConfig : IEntityConfig
{
	// Token: 0x06000556 RID: 1366 RVA: 0x0002A0B8 File Offset: 0x000282B8
	public static GameObject CreateHatch(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseHatchConfig.BaseHatch(id, name, desc, anim_file, "HatchHardBaseTrait", is_baby, "hvy_"), HatchTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("HatchHardBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, HatchTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -HatchTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 200f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = BaseHatchConfig.HardRockDiet(SimHashes.Carbon.CreateTag(), HatchHardConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL, null, 0f);
		list.AddRange(BaseHatchConfig.MetalDiet(SimHashes.Carbon.CreateTag(), HatchHardConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.BAD_1, null, 0f));
		return BaseHatchConfig.SetupDiet(gameObject, list, HatchHardConfig.CALORIES_PER_KG_OF_ORE, HatchHardConfig.MIN_POOP_SIZE_IN_KG);
	}

	// Token: 0x06000557 RID: 1367 RVA: 0x0002A214 File Offset: 0x00028414
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(HatchHardConfig.CreateHatch("HatchHard", global::STRINGS.CREATURES.SPECIES.HATCH.VARIANT_HARD.NAME, global::STRINGS.CREATURES.SPECIES.HATCH.VARIANT_HARD.DESC, "hatch_kanim", false), this as IHasDlcRestrictions, "HatchHardEgg", global::STRINGS.CREATURES.SPECIES.HATCH.VARIANT_HARD.EGG_NAME, global::STRINGS.CREATURES.SPECIES.HATCH.VARIANT_HARD.DESC, "egg_hatch_kanim", HatchTuning.EGG_MASS, "HatchHardBaby", 60.000004f, 20f, HatchTuning.EGG_CHANCES_HARD, HatchHardConfig.EGG_SORT_ORDER, true, false, 1f, false);
	}

	// Token: 0x06000558 RID: 1368 RVA: 0x0002A294 File Offset: 0x00028494
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000559 RID: 1369 RVA: 0x0002A296 File Offset: 0x00028496
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003F3 RID: 1011
	public const string ID = "HatchHard";

	// Token: 0x040003F4 RID: 1012
	public const string BASE_TRAIT_ID = "HatchHardBaseTrait";

	// Token: 0x040003F5 RID: 1013
	public const string EGG_ID = "HatchHardEgg";

	// Token: 0x040003F6 RID: 1014
	private const SimHashes EMIT_ELEMENT = SimHashes.Carbon;

	// Token: 0x040003F7 RID: 1015
	private static float KG_ORE_EATEN_PER_CYCLE = 140f;

	// Token: 0x040003F8 RID: 1016
	private static float CALORIES_PER_KG_OF_ORE = HatchTuning.STANDARD_CALORIES_PER_CYCLE / HatchHardConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040003F9 RID: 1017
	private static float MIN_POOP_SIZE_IN_KG = 25f;

	// Token: 0x040003FA RID: 1018
	public static int EGG_SORT_ORDER = HatchConfig.EGG_SORT_ORDER + 2;
}
