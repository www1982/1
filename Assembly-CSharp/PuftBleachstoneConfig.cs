using System;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000150 RID: 336
public class PuftBleachstoneConfig : IEntityConfig
{
	// Token: 0x0600064C RID: 1612 RVA: 0x0002DBC4 File Offset: 0x0002BDC4
	public static GameObject CreatePuftBleachstone(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = BasePuftConfig.BasePuft(id, name, desc, "PuftBleachstoneBaseTrait", anim_file, is_baby, "anti_", 273.15f, 333.15f, 223.15f, 373.15f);
		gameObject = EntityTemplates.ExtendEntityToWildCreature(gameObject, PuftTuning.PEN_SIZE_PER_CREATURE);
		Trait trait = Db.Get().CreateTrait("PuftBleachstoneBaseTrait", name, name, null, false, null, true, true);
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.maxAttribute.Id, PuftTuning.STANDARD_STOMACH_SIZE, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Calories.deltaAttribute.Id, -PuftTuning.STANDARD_CALORIES_PER_CYCLE / 600f, UI.TOOLTIPS.BASE_VALUE, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.HitPoints.maxAttribute.Id, 25f, name, false, false, true));
		trait.Add(new AttributeModifier(Db.Get().Amounts.Age.maxAttribute.Id, 75f, name, false, false, true));
		gameObject = BasePuftConfig.SetupDiet(gameObject, SimHashes.ChlorineGas.CreateTag(), SimHashes.BleachStone.CreateTag(), PuftBleachstoneConfig.CALORIES_PER_KG_OF_ORE, global::TUNING.CREATURES.CONVERSION_EFFICIENCY.GOOD_2, null, 0f, PuftBleachstoneConfig.MIN_POOP_SIZE_IN_KG);
		gameObject.AddOrGetDef<LureableMonitor.Def>().lures = new Tag[]
		{
			SimHashes.BleachStone.CreateTag(),
			GameTags.Creatures.FlyersLure
		};
		return gameObject;
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0002DD40 File Offset: 0x0002BF40
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(PuftBleachstoneConfig.CreatePuftBleachstone("PuftBleachstone", global::STRINGS.CREATURES.SPECIES.PUFT.VARIANT_BLEACHSTONE.NAME, global::STRINGS.CREATURES.SPECIES.PUFT.VARIANT_BLEACHSTONE.DESC, "puft_kanim", false), this as IHasDlcRestrictions, "PuftBleachstoneEgg", global::STRINGS.CREATURES.SPECIES.PUFT.VARIANT_BLEACHSTONE.EGG_NAME, global::STRINGS.CREATURES.SPECIES.PUFT.VARIANT_BLEACHSTONE.DESC, "egg_puft_kanim", PuftTuning.EGG_MASS, "PuftBleachstoneBaby", 45f, 15f, PuftTuning.EGG_CHANCES_BLEACHSTONE, PuftBleachstoneConfig.EGG_SORT_ORDER, true, false, 1f, false);
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0002DDC0 File Offset: 0x0002BFC0
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0002DDC2 File Offset: 0x0002BFC2
	public void OnSpawn(GameObject inst)
	{
		BasePuftConfig.OnSpawn(inst);
	}

	// Token: 0x040004B3 RID: 1203
	public const string ID = "PuftBleachstone";

	// Token: 0x040004B4 RID: 1204
	public const string BASE_TRAIT_ID = "PuftBleachstoneBaseTrait";

	// Token: 0x040004B5 RID: 1205
	public const string EGG_ID = "PuftBleachstoneEgg";

	// Token: 0x040004B6 RID: 1206
	public const SimHashes CONSUME_ELEMENT = SimHashes.ChlorineGas;

	// Token: 0x040004B7 RID: 1207
	public const SimHashes EMIT_ELEMENT = SimHashes.BleachStone;

	// Token: 0x040004B8 RID: 1208
	private static float KG_ORE_EATEN_PER_CYCLE = 30f;

	// Token: 0x040004B9 RID: 1209
	private static float CALORIES_PER_KG_OF_ORE = PuftTuning.STANDARD_CALORIES_PER_CYCLE / PuftBleachstoneConfig.KG_ORE_EATEN_PER_CYCLE;

	// Token: 0x040004BA RID: 1210
	private static float MIN_POOP_SIZE_IN_KG = 15f;

	// Token: 0x040004BB RID: 1211
	public static int EGG_SORT_ORDER = PuftConfig.EGG_SORT_ORDER + 3;
}
