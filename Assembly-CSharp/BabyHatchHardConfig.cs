using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000122 RID: 290
[EntityConfigOrder(2)]
public class BabyHatchHardConfig : IEntityConfig
{
	// Token: 0x0600055C RID: 1372 RVA: 0x0002A2D2 File Offset: 0x000284D2
	public GameObject CreatePrefab()
	{
		GameObject gameObject = HatchHardConfig.CreateHatch("HatchHardBaby", CREATURES.SPECIES.HATCH.VARIANT_HARD.BABY.NAME, CREATURES.SPECIES.HATCH.VARIANT_HARD.BABY.DESC, "baby_hatch_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "HatchHard", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600055D RID: 1373 RVA: 0x0002A310 File Offset: 0x00028510
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600055E RID: 1374 RVA: 0x0002A312 File Offset: 0x00028512
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003FB RID: 1019
	public const string ID = "HatchHardBaby";
}
