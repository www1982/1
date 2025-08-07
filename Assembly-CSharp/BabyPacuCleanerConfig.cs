using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000146 RID: 326
[EntityConfigOrder(2)]
public class BabyPacuCleanerConfig : IEntityConfig
{
	// Token: 0x0600061C RID: 1564 RVA: 0x0002D380 File Offset: 0x0002B580
	public GameObject CreatePrefab()
	{
		GameObject gameObject = PacuCleanerConfig.CreatePacu("PacuCleanerBaby", CREATURES.SPECIES.PACU.VARIANT_CLEANER.BABY.NAME, CREATURES.SPECIES.PACU.VARIANT_CLEANER.BABY.DESC, "baby_pacu_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "PacuCleaner", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x0002D3BE File Offset: 0x0002B5BE
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x0002D3C0 File Offset: 0x0002B5C0
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000496 RID: 1174
	public const string ID = "PacuCleanerBaby";
}
