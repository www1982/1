using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000149 RID: 329
[EntityConfigOrder(2)]
public class BabyPacuConfig : IEntityConfig
{
	// Token: 0x06000627 RID: 1575 RVA: 0x0002D4BE File Offset: 0x0002B6BE
	public GameObject CreatePrefab()
	{
		GameObject gameObject = PacuConfig.CreatePacu("PacuBaby", CREATURES.SPECIES.PACU.BABY.NAME, CREATURES.SPECIES.PACU.BABY.DESC, "baby_pacu_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Pacu", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x0002D4FC File Offset: 0x0002B6FC
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x0002D4FE File Offset: 0x0002B6FE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400049B RID: 1179
	public const string ID = "PacuBaby";
}
