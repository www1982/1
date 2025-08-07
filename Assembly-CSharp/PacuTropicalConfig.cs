using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200014A RID: 330
[EntityConfigOrder(1)]
public class PacuTropicalConfig : IEntityConfig
{
	// Token: 0x0600062B RID: 1579 RVA: 0x0002D508 File Offset: 0x0002B708
	public static GameObject CreatePacu(string id, string name, string desc, string anim_file, bool is_baby)
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToWildCreature(BasePacuConfig.CreatePrefab(id, "PacuTropicalBaseTrait", name, desc, anim_file, is_baby, "trp_", 303.15f, 353.15f, 283.15f, 373.15f), PacuTuning.PEN_SIZE_PER_CREATURE, false);
		gameObject.AddOrGet<DecorProvider>().SetValues(PacuTropicalConfig.DECOR);
		return gameObject;
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x0002D55C File Offset: 0x0002B75C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFertileCreature(EntityTemplates.ExtendEntityToWildCreature(PacuTropicalConfig.CreatePacu("PacuTropical", global::STRINGS.CREATURES.SPECIES.PACU.VARIANT_TROPICAL.NAME, global::STRINGS.CREATURES.SPECIES.PACU.VARIANT_TROPICAL.DESC, "pacu_kanim", false), PacuTuning.PEN_SIZE_PER_CREATURE, false), this as IHasDlcRestrictions, "PacuTropicalEgg", global::STRINGS.CREATURES.SPECIES.PACU.VARIANT_TROPICAL.EGG_NAME, global::STRINGS.CREATURES.SPECIES.PACU.VARIANT_TROPICAL.DESC, "egg_pacu_kanim", PacuTuning.EGG_MASS, "PacuTropicalBaby", 15.000001f, 5f, PacuTuning.EGG_CHANCES_TROPICAL, 502, false, true, 0.75f, false);
	}

	// Token: 0x0600062D RID: 1581 RVA: 0x0002D5E7 File Offset: 0x0002B7E7
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0002D5E9 File Offset: 0x0002B7E9
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400049C RID: 1180
	public const string ID = "PacuTropical";

	// Token: 0x0400049D RID: 1181
	public const string BASE_TRAIT_ID = "PacuTropicalBaseTrait";

	// Token: 0x0400049E RID: 1182
	public const string EGG_ID = "PacuTropicalEgg";

	// Token: 0x0400049F RID: 1183
	public static readonly EffectorValues DECOR = global::TUNING.BUILDINGS.DECOR.BONUS.TIER4;

	// Token: 0x040004A0 RID: 1184
	public const int EGG_SORT_ORDER = 502;
}
