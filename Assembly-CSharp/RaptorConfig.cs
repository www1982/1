using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000156 RID: 342
[EntityConfigOrder(1)]
public class RaptorConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600066A RID: 1642 RVA: 0x0002E35C File Offset: 0x0002C55C
	public static GameObject CreateRaptor(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseRaptorConfig.BaseRaptor(id, name, desc, anim_file, "RaptorBaseTrait", is_baby, null);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, global::TUNING.CREATURES.SPACE_REQUIREMENTS.TIER4);
		Trait trait = Db.Get().CreateTrait("RaptorBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, RaptorTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -RaptorTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 50f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 200f, name, false, false, true));
		gameObject = BaseRaptorConfig.SetupDiet(gameObject, BaseRaptorConfig.StandardDiets());
		WellFedShearable.Def def = gameObject.AddOrGetDef<WellFedShearable.Def>();
		def.effectId = "RaptorWellFed";
		def.scaleGrowthSymbols = new KAnimHashedString[] { "body_feathers", "tail_feather" };
		def.caloriesPerCycle = RaptorTuning.STANDARD_CALORIES_PER_CYCLE;
		def.growthDurationCycles = RaptorConfig.SCALE_GROWTH_TIME_IN_CYCLES;
		def.dropMass = RaptorConfig.FIBER_PER_CYCLE * RaptorConfig.SCALE_GROWTH_TIME_IN_CYCLES;
		def.itemDroppedOnShear = RaptorConfig.SCALE_GROWTH_EMIT_ELEMENT;
		def.levelCount = 2;
		gameObject.AddTag(GameTags.OriginalCreature);
		return gameObject;
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0002E4F1 File Offset: 0x0002C6F1
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x0002E4F8 File Offset: 0x0002C6F8
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x0002E4FC File Offset: 0x0002C6FC
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToFertileCreature(RaptorConfig.CreateRaptor("Raptor", global::STRINGS.CREATURES.SPECIES.RAPTOR.NAME, global::STRINGS.CREATURES.SPECIES.RAPTOR.DESC, "raptor_kanim", false), this, "RaptorEgg", global::STRINGS.CREATURES.SPECIES.RAPTOR.EGG_NAME, global::STRINGS.CREATURES.SPECIES.RAPTOR.DESC, "egg_raptor_kanim", 8f, "RaptorBaby", 120.00001f, 40f, RaptorTuning.EGG_CHANCES_BASE, RaptorConfig.EGG_SORT_ORDER, true, false, 1f, false);
		gameObject.AddTag(GameTags.LargeCreature);
		return gameObject;
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x0002E582 File Offset: 0x0002C782
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x0002E584 File Offset: 0x0002C784
	public void OnSpawn(GameObject inst)
	{
		new CritterEmoteMonitor.Instance(inst.GetComponent<StateMachineController>(), this.RaptorEmotes).StartSM();
	}

	// Token: 0x040004D3 RID: 1235
	public const string ID = "Raptor";

	// Token: 0x040004D4 RID: 1236
	public const string BASE_TRAIT_ID = "RaptorBaseTrait";

	// Token: 0x040004D5 RID: 1237
	public const string EGG_ID = "RaptorEgg";

	// Token: 0x040004D6 RID: 1238
	public static int EGG_SORT_ORDER = 0;

	// Token: 0x040004D7 RID: 1239
	public static float SCALE_GROWTH_TIME_IN_CYCLES = 4f;

	// Token: 0x040004D8 RID: 1240
	public static float SCALE_INITIAL_GROWTH_PCT = 0.9f;

	// Token: 0x040004D9 RID: 1241
	public static float FIBER_PER_CYCLE = 1f;

	// Token: 0x040004DA RID: 1242
	public static Tag SCALE_GROWTH_EMIT_ELEMENT = FeatherFabricConfig.ID;

	// Token: 0x040004DB RID: 1243
	public static KAnimHashedString[] SCALE_SYMBOLS = new KAnimHashedString[] { "scale_0", "scale_1", "scale_2" };

	// Token: 0x040004DC RID: 1244
	public List<Emote> RaptorEmotes = new List<Emote>
	{
		Db.Get().Emotes.Critter.Roar,
		Db.Get().Emotes.Critter.RaptorSignal
	};
}
