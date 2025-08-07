using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using UnityEngine;

// Token: 0x02000118 RID: 280
public class DreckoConfig : IEntityConfig
{
	// Token: 0x06000526 RID: 1318 RVA: 0x00029144 File Offset: 0x00027344
	public static GameObject CreateDrecko(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseDreckoConfig.BaseDrecko(id, name, desc, anim_file, "DreckoBaseTrait", is_baby, "fbr_", 283.15f, 333.15f, 243.15f, 373.15f);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, DreckoTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("DreckoBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, DreckoTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -DreckoTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 150f, name, false, false, true));
		Diet diet = new Diet(new Diet.Info[]
		{
			new Diet.Info(new HashSet<Tag>
			{
				"SpiceVine".ToTag(),
				SwampLilyConfig.ID.ToTag(),
				"BasicSingleHarvestPlant".ToTag()
			}, DreckoConfig.POOP_ELEMENT, DreckoConfig.CALORIES_PER_DAY_OF_PLANT_EATEN, DreckoConfig.KG_POOP_PER_DAY_OF_PLANT, null, 0f, false, Diet.Info.FoodType.EatPlantDirectly, false, null)
		});
		CreatureCalorieMonitor.Def def = gameObject.AddOrGetDef<CreatureCalorieMonitor.Def>();
		def.diet = diet;
		def.minConsumedCaloriesBeforePooping = DreckoConfig.MIN_POOP_SIZE_IN_CALORIES;
		gameObject.AddOrGetDef<SolidConsumerMonitor.Def>().diet = diet;
		ScaleGrowthMonitor.Def def2 = gameObject.AddOrGetDef<ScaleGrowthMonitor.Def>();
		def2.defaultGrowthRate = 1f / DreckoConfig.SCALE_GROWTH_TIME_IN_CYCLES / 600f;
		def2.dropMass = DreckoConfig.FIBER_PER_CYCLE * DreckoConfig.SCALE_GROWTH_TIME_IN_CYCLES;
		def2.itemDroppedOnShear = DreckoConfig.EMIT_ELEMENT;
		def2.levelCount = 6;
		def2.targetAtmosphere = SimHashes.Hydrogen;
		gameObject.AddTag(GameTags.OriginalCreature);
		return gameObject;
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x00029340 File Offset: 0x00027540
	public virtual GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(DreckoConfig.CreateDrecko("Drecko", CREATURES.SPECIES.DRECKO.NAME, CREATURES.SPECIES.DRECKO.DESC, "drecko_kanim", false), this as IHasDlcRestrictions, "DreckoEgg", CREATURES.SPECIES.DRECKO.EGG_NAME, CREATURES.SPECIES.DRECKO.DESC, "egg_drecko_kanim", DreckoTuning.EGG_MASS, "DreckoBaby", 90f, 30f, DreckoTuning.EGG_CHANCES_BASE, DreckoConfig.EGG_SORT_ORDER, true, false, 1f, false);
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x000293C0 File Offset: 0x000275C0
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x000293C2 File Offset: 0x000275C2
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003BA RID: 954
	public const string ID = "Drecko";

	// Token: 0x040003BB RID: 955
	public const string BASE_TRAIT_ID = "DreckoBaseTrait";

	// Token: 0x040003BC RID: 956
	public const string EGG_ID = "DreckoEgg";

	// Token: 0x040003BD RID: 957
	public static Tag POOP_ELEMENT = SimHashes.Phosphorite.CreateTag();

	// Token: 0x040003BE RID: 958
	public static Tag EMIT_ELEMENT = BasicFabricConfig.ID;

	// Token: 0x040003BF RID: 959
	private static float DAYS_PLANT_GROWTH_EATEN_PER_CYCLE = 0.75f;

	// Token: 0x040003C0 RID: 960
	private static float CALORIES_PER_DAY_OF_PLANT_EATEN = DreckoTuning.STANDARD_CALORIES_PER_CYCLE / DreckoConfig.DAYS_PLANT_GROWTH_EATEN_PER_CYCLE;

	// Token: 0x040003C1 RID: 961
	private static float KG_POOP_PER_DAY_OF_PLANT = 13.33f;

	// Token: 0x040003C2 RID: 962
	private static float MIN_POOP_SIZE_IN_KG = 1.5f;

	// Token: 0x040003C3 RID: 963
	private static float MIN_POOP_SIZE_IN_CALORIES = DreckoConfig.CALORIES_PER_DAY_OF_PLANT_EATEN * DreckoConfig.MIN_POOP_SIZE_IN_KG / DreckoConfig.KG_POOP_PER_DAY_OF_PLANT;

	// Token: 0x040003C4 RID: 964
	public static float SCALE_GROWTH_TIME_IN_CYCLES = 8f;

	// Token: 0x040003C5 RID: 965
	public static float FIBER_PER_CYCLE = 0.25f;

	// Token: 0x040003C6 RID: 966
	public static int EGG_SORT_ORDER = 800;
}
