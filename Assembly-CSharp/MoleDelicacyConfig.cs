using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000139 RID: 313
public class MoleDelicacyConfig : IEntityConfig
{
	// Token: 0x060005D4 RID: 1492 RVA: 0x0002C0F0 File Offset: 0x0002A2F0
	public static GameObject CreateMole(string id, string name, string desc, string anim_file, bool is_baby = false)
	{
		GameObject gameObject = BaseMoleConfig.BaseMole(id, name, desc, "MoleDelicacyBaseTrait", anim_file, is_baby, 173.15f, 373.15f, 73.149994f, 773.15f, "del_", 5);
		gameObject.AddTag(GameTags.Creatures.Digger);
		EntityTemplates.ExtendEntityToWildCreature(gameObject, MoleTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("MoleDelicacyBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, MoleTuning.DELICACY_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -MoleTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		Diet diet = new Diet(BaseMoleConfig.SimpleOreDiet(new List<Tag>
		{
			SimHashes.Regolith.CreateTag(),
			SimHashes.Dirt.CreateTag(),
			SimHashes.IronOre.CreateTag()
		}, MoleDelicacyConfig.CALORIES_PER_KG_OF_DIRT, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.NORMAL).ToArray());
		CreatureCalorieMonitor.Def def = gameObject.AddOrGetDef<CreatureCalorieMonitor.Def>();
		def.diet = diet;
		def.minConsumedCaloriesBeforePooping = MoleDelicacyConfig.MIN_POOP_SIZE_IN_CALORIES;
		gameObject.AddOrGetDef<SolidConsumerMonitor.Def>().diet = diet;
		gameObject.AddOrGetDef<OvercrowdingMonitor.Def>().spaceRequiredPerCreature = 0;
		gameObject.AddOrGet<LoopingSounds>();
		if (!is_baby)
		{
			ElementGrowthMonitor.Def def2 = gameObject.AddOrGetDef<ElementGrowthMonitor.Def>();
			def2.defaultGrowthRate = 1f / MoleDelicacyConfig.GINGER_GROWTH_TIME_IN_CYCLES / 600f;
			def2.dropMass = MoleDelicacyConfig.GINGER_PER_CYCLE * MoleDelicacyConfig.GINGER_GROWTH_TIME_IN_CYCLES;
			def2.itemDroppedOnShear = MoleDelicacyConfig.SHEAR_DROP_ELEMENT;
			def2.levelCount = 5;
			def2.minTemperature = MoleDelicacyConfig.MIN_GROWTH_TEMPERATURE;
			def2.maxTemperature = MoleDelicacyConfig.MAX_GROWTH_TEMPERATURE;
		}
		else
		{
			gameObject.GetComponent<Modifiers>().initialAmounts.Add(Db.Get().Amounts.ElementGrowth.Id);
		}
		return gameObject;
	}

	// Token: 0x060005D5 RID: 1493 RVA: 0x0002C320 File Offset: 0x0002A520
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(MoleDelicacyConfig.CreateMole("MoleDelicacy", global::STRINGS.CREATURES.SPECIES.MOLE.VARIANT_DELICACY.NAME, global::STRINGS.CREATURES.SPECIES.MOLE.VARIANT_DELICACY.DESC, "driller_kanim", false), this as IHasDlcRestrictions, "MoleDelicacyEgg", global::STRINGS.CREATURES.SPECIES.MOLE.VARIANT_DELICACY.EGG_NAME, global::STRINGS.CREATURES.SPECIES.MOLE.VARIANT_DELICACY.DESC, "egg_driller_kanim", MoleTuning.EGG_MASS, "MoleDelicacyBaby", 60.000004f, 20f, MoleTuning.EGG_CHANCES_DELICACY, MoleDelicacyConfig.EGG_SORT_ORDER, true, false, 1f, false);
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x0002C3A0 File Offset: 0x0002A5A0
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x0002C3A2 File Offset: 0x0002A5A2
	public void OnSpawn(GameObject inst)
	{
		MoleDelicacyConfig.SetSpawnNavType(inst);
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x0002C3AC File Offset: 0x0002A5AC
	public static void SetSpawnNavType(GameObject inst)
	{
		int num = Grid.PosToCell(inst);
		Navigator component = inst.GetComponent<Navigator>();
		if (component != null)
		{
			if (Grid.IsSolidCell(num))
			{
				component.SetCurrentNavType(NavType.Solid);
				inst.transform.SetPosition(Grid.CellToPosCBC(num, Grid.SceneLayer.FXFront));
				inst.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.FXFront);
				return;
			}
			inst.GetComponent<KBatchedAnimController>().SetSceneLayer(Grid.SceneLayer.Creatures);
		}
	}

	// Token: 0x0400044E RID: 1102
	public const string ID = "MoleDelicacy";

	// Token: 0x0400044F RID: 1103
	public const string BASE_TRAIT_ID = "MoleDelicacyBaseTrait";

	// Token: 0x04000450 RID: 1104
	public const string EGG_ID = "MoleDelicacyEgg";

	// Token: 0x04000451 RID: 1105
	private static float MIN_POOP_SIZE_IN_CALORIES = 2400000f;

	// Token: 0x04000452 RID: 1106
	private static float CALORIES_PER_KG_OF_DIRT = 1000f;

	// Token: 0x04000453 RID: 1107
	public static int EGG_SORT_ORDER = 800;

	// Token: 0x04000454 RID: 1108
	public static float GINGER_GROWTH_TIME_IN_CYCLES = 8f;

	// Token: 0x04000455 RID: 1109
	public static float GINGER_PER_CYCLE = 1f;

	// Token: 0x04000456 RID: 1110
	public static Tag SHEAR_DROP_ELEMENT = GingerConfig.ID;

	// Token: 0x04000457 RID: 1111
	public static float MIN_GROWTH_TEMPERATURE = 343.15f;

	// Token: 0x04000458 RID: 1112
	public static float MAX_GROWTH_TEMPERATURE = 353.15f;

	// Token: 0x04000459 RID: 1113
	public static float EGG_CHANCES_TEMPERATURE_MIN = 333.15f;

	// Token: 0x0400045A RID: 1114
	public static float EGG_CHANCES_TEMPERATURE_MAX = 373.15f;
}
