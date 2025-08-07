using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200014B RID: 331
[EntityConfigOrder(2)]
public class BabyPacuTropicalConfig : IEntityConfig
{
	// Token: 0x06000631 RID: 1585 RVA: 0x0002D5FF File Offset: 0x0002B7FF
	public GameObject CreatePrefab()
	{
		GameObject gameObject = PacuTropicalConfig.CreatePacu("PacuTropicalBaby", CREATURES.SPECIES.PACU.VARIANT_TROPICAL.BABY.NAME, CREATURES.SPECIES.PACU.VARIANT_TROPICAL.BABY.DESC, "baby_pacu_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "PacuTropical", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000632 RID: 1586 RVA: 0x0002D63D File Offset: 0x0002B83D
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000633 RID: 1587 RVA: 0x0002D63F File Offset: 0x0002B83F
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004A1 RID: 1185
	public const string ID = "PacuTropicalBaby";
}
