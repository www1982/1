using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000135 RID: 309
public class LightBugPurpleConfig : IEntityConfig
{
	// Token: 0x060005BF RID: 1471 RVA: 0x0002BAD4 File Offset: 0x00029CD4
	public static GameObject CreateLightBug(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseLightBugConfig.BaseLightBug(id, name, desc, anim_file, "LightBugPurpleBaseTrait", LIGHT2D.LIGHTBUG_COLOR_PURPLE, DECOR.BONUS.TIER6, is_baby, "prp_");
		EntityTemplates.ExtendEntityToWildCreature(gameObject, LightBugTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("LightBugPurpleBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, LightBugTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -LightBugTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 5f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 25f, name, false, false, true));
		HashSet<Tag> hashSet = new HashSet<Tag>();
		hashSet.Add(TagManager.Create("FriedMushroom"));
		hashSet.Add(TagManager.Create("GrilledPrickleFruit"));
		if (DlcManager.IsContentSubscribed("DLC2_ID"))
		{
			hashSet.Add(TagManager.Create("CookedPikeapple"));
		}
		hashSet.Add(TagManager.Create(SpiceNutConfig.ID));
		hashSet.Add(TagManager.Create("SpiceBread"));
		hashSet.Add(SimHashes.Phosphorite.CreateTag());
		return BaseLightBugConfig.SetupDiet(gameObject, hashSet, Tag.Invalid, LightBugPurpleConfig.CALORIES_PER_KG_OF_ORE);
	}

	// Token: 0x060005C0 RID: 1472 RVA: 0x0002BC70 File Offset: 0x00029E70
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugPurpleConfig.CreateLightBug("LightBugPurple", global::STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_PURPLE.NAME, global::STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_PURPLE.DESC, "lightbug_kanim", false);
		EntityTemplates.ExtendEntityToFertileCreature(gameObject, this as IHasDlcRestrictions, "LightBugPurpleEgg", global::STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_PURPLE.EGG_NAME, global::STRINGS.CREATURES.SPECIES.LIGHTBUG.VARIANT_PURPLE.DESC, "egg_lightbug_kanim", LightBugTuning.EGG_MASS, "LightBugPurpleBaby", 15.000001f, 5f, LightBugTuning.EGG_CHANCES_PURPLE, LightBugPurpleConfig.EGG_SORT_ORDER, true, false, 1f, false);
		return gameObject;
	}

	// Token: 0x060005C1 RID: 1473 RVA: 0x0002BCF2 File Offset: 0x00029EF2
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060005C2 RID: 1474 RVA: 0x0002BCF4 File Offset: 0x00029EF4
	public void OnSpawn(GameObject inst)
	{
		BaseLightBugConfig.SetupLoopingSounds(inst);
	}

	// Token: 0x04000440 RID: 1088
	public const string ID = "LightBugPurple";

	// Token: 0x04000441 RID: 1089
	public const string BASE_TRAIT_ID = "LightBugPurpleBaseTrait";

	// Token: 0x04000442 RID: 1090
	public const string EGG_ID = "LightBugPurpleEgg";

	// Token: 0x04000443 RID: 1091
	private static float KG_ORE_EATEN_PER_CYCLE = 1f;

	// Token: 0x04000444 RID: 1092
	private static float CALORIES_PER_KG_OF_ORE = LightBugTuning.STANDARD_CALORIES_PER_CYCLE / LightBugPurpleConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x04000445 RID: 1093
	public static int EGG_SORT_ORDER = LightBugConfig.EGG_SORT_ORDER + 2;
}
