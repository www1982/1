using System;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000127 RID: 295
[EntityConfigOrder(1)]
public class IceBellyConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000575 RID: 1397 RVA: 0x0002A7E8 File Offset: 0x000289E8
	public static GameObject CreateIceBelly(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseBellyConfig.BaseBelly(id, name, desc, anim_file, "IceBellyBaseTrait", is_baby, null), MooTuning.PEN_SIZE_PER_CREATURE);
		gameObject.AddOrGet<WarmBlooded>().BaseGenerationKW = 1.3f;
		Trait trait = Db.Get().CreateTrait("IceBellyBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, BellyTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -BellyTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 200f, name, false, false, true));
		gameObject.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = BellyTuning.GERM_ID_EMMITED_ON_POOP;
		WellFedShearable.Def def = gameObject.AddOrGetDef<WellFedShearable.Def>();
		def.effectId = "IceBellyWellFed";
		def.caloriesPerCycle = BellyTuning.STANDARD_CALORIES_PER_CYCLE;
		def.growthDurationCycles = IceBellyConfig.SCALE_GROWTH_TIME_IN_CYCLES;
		def.dropMass = IceBellyConfig.FIBER_PER_CYCLE * IceBellyConfig.SCALE_GROWTH_TIME_IN_CYCLES;
		def.itemDroppedOnShear = IceBellyConfig.SCALE_GROWTH_EMIT_ELEMENT;
		def.levelCount = 6;
		def.hideSymbols = GoldBellyConfig.SCALE_SYMBOLS;
		GameObject gameObject2 = BaseBellyConfig.SetupDiet(gameObject, BaseBellyConfig.StandardDiets(), BellyTuning.CALORIES_PER_UNIT_EATEN, 1f);
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x06000576 RID: 1398 RVA: 0x0002A97C File Offset: 0x00028B7C
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06000577 RID: 1399 RVA: 0x0002A983 File Offset: 0x00028B83
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000578 RID: 1400 RVA: 0x0002A988 File Offset: 0x00028B88
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToFertileCreature(IceBellyConfig.CreateIceBelly("IceBelly", CREATURES.SPECIES.ICEBELLY.NAME, CREATURES.SPECIES.ICEBELLY.DESC, "ice_belly_kanim", false), this, "IceBellyEgg", CREATURES.SPECIES.ICEBELLY.EGG_NAME, CREATURES.SPECIES.ICEBELLY.DESC, "egg_icebelly_kanim", 8f, "IceBellyBaby", 120.00001f, 40f, BellyTuning.EGG_CHANCES_BASE, IceBellyConfig.EGG_SORT_ORDER, true, false, 1f, false);
		gameObject.AddTag(GameTags.LargeCreature);
		return gameObject;
	}

	// Token: 0x06000579 RID: 1401 RVA: 0x0002AA0E File Offset: 0x00028C0E
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600057A RID: 1402 RVA: 0x0002AA10 File Offset: 0x00028C10
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400040D RID: 1037
	public const string ID = "IceBelly";

	// Token: 0x0400040E RID: 1038
	public const string BASE_TRAIT_ID = "IceBellyBaseTrait";

	// Token: 0x0400040F RID: 1039
	public const string EGG_ID = "IceBellyEgg";

	// Token: 0x04000410 RID: 1040
	public static Tag SCALE_GROWTH_EMIT_ELEMENT = BasicFabricConfig.ID;

	// Token: 0x04000411 RID: 1041
	public static float SCALE_INITIAL_GROWTH_PCT = 0.25f;

	// Token: 0x04000412 RID: 1042
	public static float SCALE_GROWTH_TIME_IN_CYCLES = 10f;

	// Token: 0x04000413 RID: 1043
	public static float FIBER_PER_CYCLE = 0.5f;

	// Token: 0x04000414 RID: 1044
	public static int EGG_SORT_ORDER = 0;
}
