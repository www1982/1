using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000148 RID: 328
[EntityConfigOrder(1)]
public class PacuConfig : IEntityConfig
{
	// Token: 0x06000622 RID: 1570 RVA: 0x0002D3E0 File Offset: 0x0002B5E0
	public static GameObject CreatePacu(string id, string name, string desc, string anim_file, bool is_baby)
	{
		return EntityTemplates.ExtendEntityToWildCreature(BasePacuConfig.CreatePrefab(id, "PacuBaseTrait", name, desc, anim_file, is_baby, null, 273.15f, 333.15f, 253.15f, 373.15f), PacuTuning.PEN_SIZE_PER_CREATURE, false);
	}

	// Token: 0x06000623 RID: 1571 RVA: 0x0002D420 File Offset: 0x0002B620
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToFertileCreature(PacuConfig.CreatePacu("Pacu", CREATURES.SPECIES.PACU.NAME, CREATURES.SPECIES.PACU.DESC, "pacu_kanim", false), this as IHasDlcRestrictions, "PacuEgg", CREATURES.SPECIES.PACU.EGG_NAME, CREATURES.SPECIES.PACU.DESC, "egg_pacu_kanim", PacuTuning.EGG_MASS, "PacuBaby", 15.000001f, 5f, PacuTuning.EGG_CHANCES_BASE, 500, false, true, 0.75f, false);
		gameObject.AddTag(GameTags.OriginalCreature);
		return gameObject;
	}

	// Token: 0x06000624 RID: 1572 RVA: 0x0002D4AB File Offset: 0x0002B6AB
	public void OnPrefabInit(GameObject prefab)
	{
		prefab.AddOrGet<LoopingSounds>();
	}

	// Token: 0x06000625 RID: 1573 RVA: 0x0002D4B4 File Offset: 0x0002B6B4
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000497 RID: 1175
	public const string ID = "Pacu";

	// Token: 0x04000498 RID: 1176
	public const string BASE_TRAIT_ID = "PacuBaseTrait";

	// Token: 0x04000499 RID: 1177
	public const string EGG_ID = "PacuEgg";

	// Token: 0x0400049A RID: 1178
	public const int EGG_SORT_ORDER = 500;
}
