using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000130 RID: 304
[EntityConfigOrder(2)]
public class LightBugCrystalBabyConfig : IEntityConfig
{
	// Token: 0x060005A7 RID: 1447 RVA: 0x0002B544 File Offset: 0x00029744
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugCrystalConfig.CreateLightBug("LightBugCrystalBaby", CREATURES.SPECIES.LIGHTBUG.VARIANT_CRYSTAL.BABY.NAME, CREATURES.SPECIES.LIGHTBUG.VARIANT_CRYSTAL.BABY.DESC, "baby_lightbug_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "LightBugCrystal", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x0002B582 File Offset: 0x00029782
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x0002B584 File Offset: 0x00029784
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000431 RID: 1073
	public const string ID = "LightBugCrystalBaby";
}
