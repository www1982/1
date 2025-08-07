using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000116 RID: 278
[EntityConfigOrder(1)]
public class DivergentWormConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000518 RID: 1304 RVA: 0x00028DB0 File Offset: 0x00026FB0
	public static GameObject CreateWorm(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BaseDivergentConfig.BaseDivergent(id, name, desc, 200f, anim_file, "DivergentWormBaseTrait", is_baby, 8f, null, "DivergentCropTendedWorm", 3, false), DivergentTuning.PEN_SIZE_PER_CREATURE_WORM);
		Trait trait = Db.Get().CreateTrait("DivergentWormBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, DivergentTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -DivergentTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 150f, name, false, false, true));
		gameObject.AddWeapon(2f, 3f, AttackProperties.DamageType.Standard, AttackProperties.TargetType.Single, 1, 0f);
		List<Diet.Info> list = BaseDivergentConfig.BasicSulfurDiet(SimHashes.Mud.CreateTag(), DivergentWormConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.BAD_2, null, 0f);
		list.Add(new Diet.Info(new HashSet<Tag> { SimHashes.Sucrose.CreateTag() }, SimHashes.Mud.CreateTag(), DivergentWormConfig.CALORIES_PER_KG_OF_SUCROSE, 1f, null, 0f, false, Diet.Info.FoodType.EatSolid, false, null));
		GameObject gameObject2 = BaseDivergentConfig.SetupDiet(gameObject, list, DivergentWormConfig.CALORIES_PER_KG_OF_ORE, DivergentWormConfig.MINI_POOP_SIZE_IN_KG);
		SegmentedCreature.Def def = gameObject2.AddOrGetDef<SegmentedCreature.Def>();
		def.segmentTrackerSymbol = new HashedString("segmenttracker");
		def.numBodySegments = 5;
		def.midAnim = Assets.GetAnim("worm_torso_kanim");
		def.tailAnim = Assets.GetAnim("worm_tail_kanim");
		def.animFrameOffset = 2;
		def.pathSpacing = 0.2f;
		def.numPathNodes = 15;
		def.minSegmentSpacing = 0.1f;
		def.maxSegmentSpacing = 0.4f;
		def.retractionSegmentSpeed = 1f;
		def.retractionPathSpeed = 2f;
		def.compressedMaxScale = 0.25f;
		def.headOffset = new Vector3(0.12f, 0.4f, 0f);
		return gameObject2;
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x00028FFB File Offset: 0x000271FB
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x0600051A RID: 1306 RVA: 0x00029002 File Offset: 0x00027202
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x00029008 File Offset: 0x00027208
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToFertileCreature(DivergentWormConfig.CreateWorm("DivergentWorm", global::STRINGS.CREATURES.SPECIES.DIVERGENT.VARIANT_WORM.NAME, global::STRINGS.CREATURES.SPECIES.DIVERGENT.VARIANT_WORM.DESC, "worm_head_kanim", false), this, "DivergentWormEgg", global::STRINGS.CREATURES.SPECIES.DIVERGENT.VARIANT_WORM.EGG_NAME, global::STRINGS.CREATURES.SPECIES.DIVERGENT.VARIANT_WORM.DESC, "egg_worm_kanim", DivergentTuning.EGG_MASS, "DivergentWormBaby", 90f, 30f, DivergentTuning.EGG_CHANCES_WORM, DivergentWormConfig.EGG_SORT_ORDER, true, false, 1f, false);
		gameObject.AddTag(GameTags.Creatures.Pollinator);
		return gameObject;
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x0002908E File Offset: 0x0002728E
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x00029090 File Offset: 0x00027290
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003AA RID: 938
	public const string ID = "DivergentWorm";

	// Token: 0x040003AB RID: 939
	public const string BASE_TRAIT_ID = "DivergentWormBaseTrait";

	// Token: 0x040003AC RID: 940
	public const string EGG_ID = "DivergentWormEgg";

	// Token: 0x040003AD RID: 941
	private const float LIFESPAN = 150f;

	// Token: 0x040003AE RID: 942
	public const float CROP_TENDED_MULTIPLIER_EFFECT = 0.5f;

	// Token: 0x040003AF RID: 943
	public const float CROP_TENDED_MULTIPLIER_DURATION = 600f;

	// Token: 0x040003B0 RID: 944
	private const int NUM_SEGMENTS = 5;

	// Token: 0x040003B1 RID: 945
	private const SimHashes EMIT_ELEMENT = SimHashes.Mud;

	// Token: 0x040003B2 RID: 946
	private static float KG_ORE_EATEN_PER_CYCLE = 50f;

	// Token: 0x040003B3 RID: 947
	private static float KG_SUCROSE_EATEN_PER_CYCLE = 30f;

	// Token: 0x040003B4 RID: 948
	private static float CALORIES_PER_KG_OF_ORE = DivergentTuning.STANDARD_CALORIES_PER_CYCLE / DivergentWormConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040003B5 RID: 949
	private static float CALORIES_PER_KG_OF_SUCROSE = DivergentTuning.STANDARD_CALORIES_PER_CYCLE / DivergentWormConfig.KG_SUCROSE_EATEN_PER_CYCLE;

	// Token: 0x040003B6 RID: 950
	public static int EGG_SORT_ORDER = 0;

	// Token: 0x040003B7 RID: 951
	private static float MINI_POOP_SIZE_IN_KG = 4f;

	// Token: 0x040003B8 RID: 952
	public const string CROP_TENDING_EFFECT = "DivergentCropTendedWorm";
}
