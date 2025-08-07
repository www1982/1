using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000123 RID: 291
[EntityConfigOrder(1)]
public class HatchMetalConfig : IEntityConfig
{
	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000560 RID: 1376 RVA: 0x0002A31C File Offset: 0x0002851C
	public static HashSet<Tag> METAL_ORE_TAGS
	{
		get
		{
			return new HashSet<Tag>(GameTags.BasicMetalOres)
			{
				SimHashes.GoldAmalgam.CreateTag(),
				SimHashes.Wolframite.CreateTag()
			};
		}
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x0002A34C File Offset: 0x0002854C
	public static GameObject CreateHatch(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseHatchConfig.BaseHatch(id, name, desc, anim_file, "HatchMetalBaseTrait", is_baby, "mtl_"), HatchTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("HatchMetalBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, HatchTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -HatchTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 400f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = BaseHatchConfig.MetalDiet(GameTags.Metal, HatchMetalConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.GOOD_1, null, 0f);
		return BaseHatchConfig.SetupDiet(gameObject, list, HatchMetalConfig.CALORIES_PER_KG_OF_ORE, HatchMetalConfig.MIN_POOP_SIZE_IN_KG);
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x0002A47C File Offset: 0x0002867C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(HatchMetalConfig.CreateHatch("HatchMetal", global::STRINGS.CREATURES.SPECIES.HATCH.VARIANT_METAL.NAME, global::STRINGS.CREATURES.SPECIES.HATCH.VARIANT_METAL.DESC, "hatch_kanim", false), this as IHasDlcRestrictions, "HatchMetalEgg", global::STRINGS.CREATURES.SPECIES.HATCH.VARIANT_METAL.EGG_NAME, global::STRINGS.CREATURES.SPECIES.HATCH.VARIANT_METAL.DESC, "egg_hatch_kanim", HatchTuning.EGG_MASS, "HatchMetalBaby", 60.000004f, 20f, HatchTuning.EGG_CHANCES_METAL, HatchMetalConfig.EGG_SORT_ORDER, true, false, 1f, false);
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x0002A4FC File Offset: 0x000286FC
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x0002A4FE File Offset: 0x000286FE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003FC RID: 1020
	public const string ID = "HatchMetal";

	// Token: 0x040003FD RID: 1021
	public const string BASE_TRAIT_ID = "HatchMetalBaseTrait";

	// Token: 0x040003FE RID: 1022
	public const string EGG_ID = "HatchMetalEgg";

	// Token: 0x040003FF RID: 1023
	private static float KG_ORE_EATEN_PER_CYCLE = 100f;

	// Token: 0x04000400 RID: 1024
	private static float CALORIES_PER_KG_OF_ORE = HatchTuning.STANDARD_CALORIES_PER_CYCLE / HatchMetalConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000401 RID: 1025
	private static float MIN_POOP_SIZE_IN_KG = 10f;

	// Token: 0x04000402 RID: 1026
	public static int EGG_SORT_ORDER = HatchConfig.EGG_SORT_ORDER + 3;
}
