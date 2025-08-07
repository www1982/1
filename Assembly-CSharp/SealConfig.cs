using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200015A RID: 346
public class SealConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000686 RID: 1670 RVA: 0x0002ECC8 File Offset: 0x0002CEC8
	public static GameObject CreateSeal(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BaseSealConfig.BaseSeal(id, name, desc, anim_file, "SealBaseTrait", is_baby, null);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, SealTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("SealBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, SquirrelTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -SquirrelTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 100f, name, false, false, true));
		gameObject = BaseSealConfig.SetupDiet(gameObject, new List<Diet.Info>
		{
			new Diet.Info(new HashSet<Tag> { "SpaceTree" }, SimHashes.Ethanol.CreateTag(), 2500f, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.GOOD_3, null, 0f, false, Diet.Info.FoodType.EatPlantStorage, false, null),
			new Diet.Info(new HashSet<Tag> { SimHashes.Sucrose.CreateTag() }, SimHashes.Ethanol.CreateTag(), 3246.7532f, 1.2987013f, null, 0f, false, Diet.Info.FoodType.EatSolid, false, new string[] { "eat_ore_pre", "eat_ore_loop", "eat_ore_pst" })
		}, 2500f, SealConfig.MIN_POOP_SIZE_IN_KG);
		gameObject.AddOrGetDef<CreaturePoopLoot.Def>().Loot = new CreaturePoopLoot.LootData[]
		{
			new CreaturePoopLoot.LootData
			{
				tag = "SpaceTreeSeed",
				probability = 0.2f
			}
		};
		gameObject.AddTag(GameTags.OriginalCreature);
		return gameObject;
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x0002EEC9 File Offset: 0x0002D0C9
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0002EED0 File Offset: 0x0002D0D0
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0002EED4 File Offset: 0x0002D0D4
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(SealConfig.CreateSeal("Seal", global::STRINGS.CREATURES.SPECIES.SEAL.NAME, global::STRINGS.CREATURES.SPECIES.SEAL.DESC, "seal_kanim", false), this, "SealEgg", global::STRINGS.CREATURES.SPECIES.SEAL.EGG_NAME, global::STRINGS.CREATURES.SPECIES.SEAL.DESC, "egg_seal_kanim", SealTuning.EGG_MASS, "SealBaby", 60.000004f, 20f, SealTuning.EGG_CHANCES_BASE, SealConfig.EGG_SORT_ORDER, true, false, 1f, false);
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0002EF4F File Offset: 0x0002D14F
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0002EF51 File Offset: 0x0002D151
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004EA RID: 1258
	public const string ID = "Seal";

	// Token: 0x040004EB RID: 1259
	public const string BASE_TRAIT_ID = "SealBaseTrait";

	// Token: 0x040004EC RID: 1260
	public const string EGG_ID = "SealEgg";

	// Token: 0x040004ED RID: 1261
	public const float SUGAR_TREE_SEED_PROBABILITY_ON_POOP = 0.2f;

	// Token: 0x040004EE RID: 1262
	public const float SUGAR_WATER_KG_CONSUMED_PER_DAY = 40f;

	// Token: 0x040004EF RID: 1263
	public const float CALORIES_PER_1KG_OF_SUGAR_WATER = 2500f;

	// Token: 0x040004F0 RID: 1264
	private static float MIN_POOP_SIZE_IN_KG = 10f;

	// Token: 0x040004F1 RID: 1265
	public static int EGG_SORT_ORDER = 0;
}
