using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200014E RID: 334
public class PuftAlphaConfig : IEntityConfig
{
	// Token: 0x06000642 RID: 1602 RVA: 0x0002D870 File Offset: 0x0002BA70
	public static GameObject CreatePuftAlpha(string id, string name, string desc, string anim_file, bool is_baby)
	{
		string text = "alp_";
		GameObject gameObject = BasePuftConfig.BasePuft(id, name, desc, "PuftAlphaBaseTrait", anim_file, is_baby, text, 293.15f, 313.15f, 223.15f, 373.15f);
		EntityTemplates.ExtendEntityToWildCreature(gameObject, PuftTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("PuftAlphaBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, PuftTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -PuftTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 75f, name, false, false, true));
		gameObject = BasePuftConfig.SetupDiet(gameObject, new List<Diet.Info>
		{
			new Diet.Info(new HashSet<Tag>(new Tag[] { SimHashes.ContaminatedOxygen.CreateTag() }), SimHashes.SlimeMold.CreateTag(), PuftAlphaConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.BAD_2, "SlimeLung", 0f, false, Diet.Info.FoodType.EatSolid, false, null),
			new Diet.Info(new HashSet<Tag>(new Tag[] { SimHashes.ChlorineGas.CreateTag() }), SimHashes.BleachStone.CreateTag(), PuftAlphaConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.BAD_2, "SlimeLung", 0f, false, Diet.Info.FoodType.EatSolid, false, null),
			new Diet.Info(new HashSet<Tag>(new Tag[] { SimHashes.Oxygen.CreateTag() }), SimHashes.OxyRock.CreateTag(), PuftAlphaConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.BAD_2, "SlimeLung", 0f, false, Diet.Info.FoodType.EatSolid, false, null)
		}.ToArray(), PuftAlphaConfig.CALORIES_PER_KG_OF_ORE, PuftAlphaConfig.MIN_POOP_SIZE_IN_KG);
		gameObject.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = "SlimeLung";
		return gameObject;
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0002DA98 File Offset: 0x0002BC98
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(PuftAlphaConfig.CreatePuftAlpha("PuftAlpha", global::STRINGS.CREATURES.SPECIES.PUFT.VARIANT_ALPHA.NAME, global::STRINGS.CREATURES.SPECIES.PUFT.VARIANT_ALPHA.DESC, "puft_kanim", false), this as IHasDlcRestrictions, "PuftAlphaEgg", global::STRINGS.CREATURES.SPECIES.PUFT.VARIANT_ALPHA.EGG_NAME, global::STRINGS.CREATURES.SPECIES.PUFT.VARIANT_ALPHA.DESC, "egg_puft_kanim", PuftTuning.EGG_MASS, "PuftAlphaBaby", 45f, 15f, PuftTuning.EGG_CHANCES_ALPHA, PuftAlphaConfig.EGG_SORT_ORDER, true, false, 1f, false);
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x0002DB18 File Offset: 0x0002BD18
	public void OnPrefabInit(GameObject inst)
	{
		inst.GetComponent<KBatchedAnimController>().animScale *= 1.1f;
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0002DB31 File Offset: 0x0002BD31
	public void OnSpawn(GameObject inst)
	{
		BasePuftConfig.OnSpawn(inst);
	}

	// Token: 0x040004A7 RID: 1191
	public const string ID = "PuftAlpha";

	// Token: 0x040004A8 RID: 1192
	public const string BASE_TRAIT_ID = "PuftAlphaBaseTrait";

	// Token: 0x040004A9 RID: 1193
	public const string EGG_ID = "PuftAlphaEgg";

	// Token: 0x040004AA RID: 1194
	public const SimHashes CONSUME_ELEMENT = SimHashes.ContaminatedOxygen;

	// Token: 0x040004AB RID: 1195
	public const SimHashes EMIT_ELEMENT = SimHashes.SlimeMold;

	// Token: 0x040004AC RID: 1196
	public const string EMIT_DISEASE = "SlimeLung";

	// Token: 0x040004AD RID: 1197
	public const float EMIT_DISEASE_PER_KG = 0f;

	// Token: 0x040004AE RID: 1198
	private static float KG_ORE_EATEN_PER_CYCLE = 30f;

	// Token: 0x040004AF RID: 1199
	private static float CALORIES_PER_KG_OF_ORE = PuftTuning.STANDARD_CALORIES_PER_CYCLE / PuftAlphaConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040004B0 RID: 1200
	private static float MIN_POOP_SIZE_IN_KG = 5f;

	// Token: 0x040004B1 RID: 1201
	public static int EGG_SORT_ORDER = PuftConfig.EGG_SORT_ORDER + 1;
}
