using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x0200011D RID: 285
[EntityConfigOrder(1)]
public class GoldBellyConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600053E RID: 1342 RVA: 0x00029AEC File Offset: 0x00027CEC
	public static GameObject CreateGoldBelly(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseBellyConfig.BaseBelly(id, name, desc, anim_file, "GoldBellyBaseTrait", is_baby, "king_"), MooTuning.PEN_SIZE_PER_CREATURE);
		gameObject.AddOrGet<WarmBlooded>().BaseGenerationKW = 1.3f;
		Trait trait = Db.Get().CreateTrait("GoldBellyBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, BellyTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -BellyTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 50f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 200f, name, false, false, true));
		string text = "PollenGerms";
		gameObject.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = text;
		WellFedShearable.Def def = gameObject.AddOrGetDef<WellFedShearable.Def>();
		def.effectId = "GoldBellyWellFed";
		def.caloriesPerCycle = BellyTuning.STANDARD_CALORIES_PER_CYCLE;
		def.growthDurationCycles = 10f;
		def.dropMass = 250f;
		def.itemDroppedOnShear = GoldBellyConfig.SCALE_GROWTH_EMIT_ELEMENT;
		def.requiredDiet = "FriesCarrot";
		def.levelCount = 6;
		def.scaleGrowthSymbols = GoldBellyConfig.SCALE_SYMBOLS;
		GameObject gameObject2 = BaseBellyConfig.SetupDiet(gameObject, BaseBellyConfig.StandardDiets(), BellyTuning.CALORIES_PER_UNIT_EATEN, 1f);
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x0600053F RID: 1343 RVA: 0x00029C90 File Offset: 0x00027E90
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06000540 RID: 1344 RVA: 0x00029C97 File Offset: 0x00027E97
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000541 RID: 1345 RVA: 0x00029C9C File Offset: 0x00027E9C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToFertileCreature(GoldBellyConfig.CreateGoldBelly("GoldBelly", CREATURES.SPECIES.ICEBELLY.VARIANT_GOLD.NAME, CREATURES.SPECIES.ICEBELLY.VARIANT_GOLD.DESC, "ice_belly_kanim", false), this, "GoldBellyEgg", CREATURES.SPECIES.ICEBELLY.VARIANT_GOLD.EGG_NAME, CREATURES.SPECIES.ICEBELLY.VARIANT_GOLD.DESC, "egg_icebelly_kanim", 8f, "GoldBellyBaby", 120.00001f, 40f, BellyTuning.EGG_CHANCES_GOLD, GoldBellyConfig.EGG_SORT_ORDER, true, false, 1f, false);
		gameObject.AddOrGetDef<OvercrowdingMonitor.Def>();
		gameObject.AddTag(GameTags.LargeCreature);
		return gameObject;
	}

	// Token: 0x06000542 RID: 1346 RVA: 0x00029D29 File Offset: 0x00027F29
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000543 RID: 1347 RVA: 0x00029D2B File Offset: 0x00027F2B
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003DF RID: 991
	public const string ID = "GoldBelly";

	// Token: 0x040003E0 RID: 992
	public const string BASE_TRAIT_ID = "GoldBellyBaseTrait";

	// Token: 0x040003E1 RID: 993
	public const string EGG_ID = "GoldBellyEgg";

	// Token: 0x040003E2 RID: 994
	public const int GERMS_EMMITED_PER_KG_POOPED = 1000;

	// Token: 0x040003E3 RID: 995
	public static Tag SCALE_GROWTH_EMIT_ELEMENT = "GoldBellyCrown";

	// Token: 0x040003E4 RID: 996
	public static float SCALE_INITIAL_GROWTH_PCT = 0.25f;

	// Token: 0x040003E5 RID: 997
	public const float SCALE_GROWTH_TIME_IN_CYCLES = 10f;

	// Token: 0x040003E6 RID: 998
	public const float GOLD_PER_CYCLE = 25f;

	// Token: 0x040003E7 RID: 999
	public static int EGG_SORT_ORDER = 0;

	// Token: 0x040003E8 RID: 1000
	public static KAnimHashedString[] SCALE_SYMBOLS = new KAnimHashedString[] { "antler_0", "antler_1", "antler_2", "antler_3", "antler_4" };
}
