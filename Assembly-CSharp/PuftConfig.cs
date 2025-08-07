using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000152 RID: 338
public class PuftConfig : IEntityConfig
{
	// Token: 0x06000656 RID: 1622 RVA: 0x0002DE54 File Offset: 0x0002C054
	public static GameObject CreatePuft(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BasePuftConfig.BasePuft(id, name, global::STRINGS.CREATURES.SPECIES.PUFT.DESC, "PuftBaseTrait", anim_file, is_baby, null, 288.15f, 328.15f, 223.15f, 373.15f);
		EntityTemplates.ExtendEntityToWildCreature(gameObject, PuftTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("PuftBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, PuftTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -PuftTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 75f, name, false, false, true));
		GameObject gameObject2 = BasePuftConfig.SetupDiet(gameObject, SimHashes.ContaminatedOxygen.CreateTag(), SimHashes.SlimeMold.CreateTag(), PuftConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.GOOD_2, "SlimeLung", 0f, PuftConfig.MIN_POOP_SIZE_IN_KG);
		gameObject2.AddOrGet<DiseaseSourceVisualizer>().alwaysShowDisease = "SlimeLung";
		gameObject2.AddTag(GameTags.OriginalCreature);
		return gameObject2;
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x0002DFC4 File Offset: 0x0002C1C4
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(PuftConfig.CreatePuft("Puft", global::STRINGS.CREATURES.SPECIES.PUFT.NAME, global::STRINGS.CREATURES.SPECIES.PUFT.DESC, "puft_kanim", false), this as IHasDlcRestrictions, "PuftEgg", global::STRINGS.CREATURES.SPECIES.PUFT.EGG_NAME, global::STRINGS.CREATURES.SPECIES.PUFT.DESC, "egg_puft_kanim", PuftTuning.EGG_MASS, "PuftBaby", 45f, 15f, PuftTuning.EGG_CHANCES_BASE, PuftConfig.EGG_SORT_ORDER, true, false, 1f, false);
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x0002E044 File Offset: 0x0002C244
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0002E046 File Offset: 0x0002C246
	public void OnSpawn(GameObject inst)
	{
		BasePuftConfig.OnSpawn(inst);
	}

	// Token: 0x040004BD RID: 1213
	public const string ID = "Puft";

	// Token: 0x040004BE RID: 1214
	public const string BASE_TRAIT_ID = "PuftBaseTrait";

	// Token: 0x040004BF RID: 1215
	public const string EGG_ID = "PuftEgg";

	// Token: 0x040004C0 RID: 1216
	public const SimHashes CONSUME_ELEMENT = SimHashes.ContaminatedOxygen;

	// Token: 0x040004C1 RID: 1217
	public const SimHashes EMIT_ELEMENT = SimHashes.SlimeMold;

	// Token: 0x040004C2 RID: 1218
	public const string EMIT_DISEASE = "SlimeLung";

	// Token: 0x040004C3 RID: 1219
	public const float EMIT_DISEASE_PER_KG = 0f;

	// Token: 0x040004C4 RID: 1220
	private static float KG_ORE_EATEN_PER_CYCLE = 50f;

	// Token: 0x040004C5 RID: 1221
	private static float CALORIES_PER_KG_OF_ORE = PuftTuning.STANDARD_CALORIES_PER_CYCLE / PuftConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040004C6 RID: 1222
	private static float MIN_POOP_SIZE_IN_KG = 15f;

	// Token: 0x040004C7 RID: 1223
	public static int EGG_SORT_ORDER = 300;
}
