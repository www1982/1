using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000161 RID: 353
public class StaterpillarConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060006AC RID: 1708 RVA: 0x0002F558 File Offset: 0x0002D758
	public static GameObject CreateStaterpillar(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseStaterpillarConfig.BaseStaterpillar(id, name, desc, anim_file, "StaterpillarBaseTrait", is_baby, ObjectLayer.Wire, StaterpillarGeneratorConfig.ID, Tag.Invalid, null, 283.15f, 313.15f, 173.15f, 373.15f, null), global::TUNING.CREATURES.SPACE_REQUIREMENTS.TIER3);
		Trait trait = Db.Get().CreateTrait("StaterpillarBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, StaterpillarTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -StaterpillarTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		List<Diet.Info> list = new List<Diet.Info>();
		list.AddRange(BaseStaterpillarConfig.RawMetalDiet(SimHashes.Hydrogen.CreateTag(), StaterpillarConfig.CALORIES_PER_KG_OF_ORE, StaterpillarTuning.POOP_CONVERSTION_RATE, null, 0f));
		list.AddRange(BaseStaterpillarConfig.RefinedMetalDiet(SimHashes.Hydrogen.CreateTag(), StaterpillarConfig.CALORIES_PER_KG_OF_ORE, StaterpillarTuning.POOP_CONVERSTION_RATE, null, 0f));
		GameObject gameObject2 = BaseStaterpillarConfig.SetupDiet(gameObject, list);
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x0002F6DB File Offset: 0x0002D8DB
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x0002F6E2 File Offset: 0x0002D8E2
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x0002F6E8 File Offset: 0x0002D8E8
	public virtual GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(StaterpillarConfig.CreateStaterpillar("Staterpillar", global::STRINGS.CREATURES.SPECIES.STATERPILLAR.NAME, global::STRINGS.CREATURES.SPECIES.STATERPILLAR.DESC, "caterpillar_kanim", false), this, "StaterpillarEgg", global::STRINGS.CREATURES.SPECIES.STATERPILLAR.EGG_NAME, global::STRINGS.CREATURES.SPECIES.STATERPILLAR.DESC, "egg_caterpillar_kanim", StaterpillarTuning.EGG_MASS, "StaterpillarBaby", 60.000004f, 20f, StaterpillarTuning.EGG_CHANCES_BASE, 0, true, false, 1f, false);
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x0002F75F File Offset: 0x0002D95F
	public void OnPrefabInit(GameObject prefab)
	{
		prefab.GetComponent<KBatchedAnimController>().SetSymbolVisiblity("gulp", false);
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x0002F777 File Offset: 0x0002D977
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400050C RID: 1292
	public const string ID = "Staterpillar";

	// Token: 0x0400050D RID: 1293
	public const string BASE_TRAIT_ID = "StaterpillarBaseTrait";

	// Token: 0x0400050E RID: 1294
	public const string EGG_ID = "StaterpillarEgg";

	// Token: 0x0400050F RID: 1295
	public const int EGG_SORT_ORDER = 0;

	// Token: 0x04000510 RID: 1296
	private static float KG_ORE_EATEN_PER_CYCLE = 60f;

	// Token: 0x04000511 RID: 1297
	private static float CALORIES_PER_KG_OF_ORE = StaterpillarTuning.STANDARD_CALORIES_PER_CYCLE / StaterpillarConfig.KG_ORE_EATEN_PER_CYCLE;
}
