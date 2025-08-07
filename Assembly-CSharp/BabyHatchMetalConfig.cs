using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000124 RID: 292
[EntityConfigOrder(2)]
public class BabyHatchMetalConfig : IEntityConfig
{
	// Token: 0x06000567 RID: 1383 RVA: 0x0002A53A File Offset: 0x0002873A
	public GameObject CreatePrefab()
	{
		GameObject gameObject = HatchMetalConfig.CreateHatch("HatchMetalBaby", CREATURES.SPECIES.HATCH.VARIANT_METAL.BABY.NAME, CREATURES.SPECIES.HATCH.VARIANT_METAL.BABY.DESC, "baby_hatch_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "HatchMetal", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000568 RID: 1384 RVA: 0x0002A578 File Offset: 0x00028778
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000569 RID: 1385 RVA: 0x0002A57A File Offset: 0x0002877A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000403 RID: 1027
	public const string ID = "HatchMetalBaby";
}
