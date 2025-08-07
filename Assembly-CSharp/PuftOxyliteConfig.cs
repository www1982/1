using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000154 RID: 340
public class PuftOxyliteConfig : IEntityConfig
{
	// Token: 0x06000660 RID: 1632 RVA: 0x0002E0D0 File Offset: 0x0002C2D0
	public static GameObject CreatePuftOxylite(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BasePuftConfig.BasePuft(id, name, desc, "PuftOxyliteBaseTrait", anim_file, is_baby, "com_", 273.15f, 333.15f, 223.15f, 373.15f);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, PuftTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("PuftOxyliteBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, PuftTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -PuftTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 75f, name, false, false, true));
		gameObject = BasePuftConfig.SetupDiet(gameObject, SimHashes.Oxygen.CreateTag(), SimHashes.OxyRock.CreateTag(), PuftOxyliteConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.GOOD_2, null, 0f, PuftOxyliteConfig.MIN_POOP_SIZE_IN_KG);
		gameObject.AddOrGetDef<LureableMonitor.Def>().lures = new Tag[]
		{
			SimHashes.OxyRock.CreateTag(),
			GameTags.Creatures.FlyersLure
		};
		return gameObject;
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x0002E24C File Offset: 0x0002C44C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(PuftOxyliteConfig.CreatePuftOxylite("PuftOxylite", global::STRINGS.CREATURES.SPECIES.PUFT.VARIANT_OXYLITE.NAME, global::STRINGS.CREATURES.SPECIES.PUFT.VARIANT_OXYLITE.DESC, "puft_kanim", false), this as IHasDlcRestrictions, "PuftOxyliteEgg", global::STRINGS.CREATURES.SPECIES.PUFT.VARIANT_OXYLITE.EGG_NAME, global::STRINGS.CREATURES.SPECIES.PUFT.VARIANT_OXYLITE.DESC, "egg_puft_kanim", PuftTuning.EGG_MASS, "PuftOxyliteBaby", 45f, 15f, PuftTuning.EGG_CHANCES_OXYLITE, PuftOxyliteConfig.EGG_SORT_ORDER, true, false, 1f, false);
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x0002E2CC File Offset: 0x0002C4CC
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x0002E2CE File Offset: 0x0002C4CE
	public void OnSpawn(GameObject inst)
	{
		BasePuftConfig.OnSpawn(inst);
	}

	// Token: 0x040004C9 RID: 1225
	public const string ID = "PuftOxylite";

	// Token: 0x040004CA RID: 1226
	public const string BASE_TRAIT_ID = "PuftOxyliteBaseTrait";

	// Token: 0x040004CB RID: 1227
	public const string EGG_ID = "PuftOxyliteEgg";

	// Token: 0x040004CC RID: 1228
	public const SimHashes CONSUME_ELEMENT = SimHashes.Oxygen;

	// Token: 0x040004CD RID: 1229
	public const SimHashes EMIT_ELEMENT = SimHashes.OxyRock;

	// Token: 0x040004CE RID: 1230
	private static float KG_ORE_EATEN_PER_CYCLE = 50f;

	// Token: 0x040004CF RID: 1231
	private static float CALORIES_PER_KG_OF_ORE = PuftTuning.STANDARD_CALORIES_PER_CYCLE / PuftOxyliteConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040004D0 RID: 1232
	private static float MIN_POOP_SIZE_IN_KG = 25f;

	// Token: 0x040004D1 RID: 1233
	public static int EGG_SORT_ORDER = PuftConfig.EGG_SORT_ORDER + 2;
}
