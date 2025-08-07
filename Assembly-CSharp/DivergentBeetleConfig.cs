using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000114 RID: 276
[EntityConfigOrder(1)]
public class DivergentBeetleConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600050A RID: 1290 RVA: 0x00028B44 File Offset: 0x00026D44
	public static GameObject CreateDivergentBeetle(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseDivergentConfig.BaseDivergent(id, name, desc, 50f, anim_file, "DivergentBeetleBaseTrait", is_baby, 8f, null, "DivergentCropTended", 1, true), DivergentTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("DivergentBeetleBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, DivergentTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -DivergentTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 75f, name, false, false, true));
		List<Diet.Info> list = BaseDivergentConfig.BasicSulfurDiet(SimHashes.Sucrose.CreateTag(), DivergentBeetleConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL, null, 0f);
		GameObject gameObject2 = BaseDivergentConfig.SetupDiet(gameObject, list, DivergentBeetleConfig.CALORIES_PER_KG_OF_ORE, DivergentBeetleConfig.MIN_POOP_SIZE_IN_KG);
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x0600050B RID: 1291 RVA: 0x00028C91 File Offset: 0x00026E91
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x0600050C RID: 1292 RVA: 0x00028C98 File Offset: 0x00026E98
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x00028C9C File Offset: 0x00026E9C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToFertileCreature(DivergentBeetleConfig.CreateDivergentBeetle("DivergentBeetle", global::STRINGS.CREATURES.SPECIES.DIVERGENT.VARIANT_BEETLE.NAME, global::STRINGS.CREATURES.SPECIES.DIVERGENT.VARIANT_BEETLE.DESC, "critter_kanim", false), this, "DivergentBeetleEgg", global::STRINGS.CREATURES.SPECIES.DIVERGENT.VARIANT_BEETLE.EGG_NAME, global::STRINGS.CREATURES.SPECIES.DIVERGENT.VARIANT_BEETLE.DESC, "egg_critter_kanim", DivergentTuning.EGG_MASS, "DivergentBeetleBaby", 45f, 15f, DivergentTuning.EGG_CHANCES_BEETLE, DivergentBeetleConfig.EGG_SORT_ORDER, true, false, 1f, false);
		gameObject.AddTag(GameTags.Creatures.Pollinator);
		return gameObject;
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00028D22 File Offset: 0x00026F22
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x00028D24 File Offset: 0x00026F24
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003A0 RID: 928
	public const string ID = "DivergentBeetle";

	// Token: 0x040003A1 RID: 929
	public const string BASE_TRAIT_ID = "DivergentBeetleBaseTrait";

	// Token: 0x040003A2 RID: 930
	public const string EGG_ID = "DivergentBeetleEgg";

	// Token: 0x040003A3 RID: 931
	private const float LIFESPAN = 75f;

	// Token: 0x040003A4 RID: 932
	private const SimHashes EMIT_ELEMENT = SimHashes.Sucrose;

	// Token: 0x040003A5 RID: 933
	private static float KG_ORE_EATEN_PER_CYCLE = 20f;

	// Token: 0x040003A6 RID: 934
	private static float CALORIES_PER_KG_OF_ORE = DivergentTuning.STANDARD_CALORIES_PER_CYCLE / DivergentBeetleConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040003A7 RID: 935
	private static float MIN_POOP_SIZE_IN_KG = 4f;

	// Token: 0x040003A8 RID: 936
	public static int EGG_SORT_ORDER = 0;
}
