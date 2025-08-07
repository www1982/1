using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000120 RID: 288
[EntityConfigOrder(2)]
public class BabyHatchConfig : IEntityConfig
{
	// Token: 0x06000552 RID: 1362 RVA: 0x0002A06C File Offset: 0x0002826C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = HatchConfig.CreateHatch("HatchBaby", CREATURES.SPECIES.HATCH.BABY.NAME, CREATURES.SPECIES.HATCH.BABY.DESC, "baby_hatch_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Hatch", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000553 RID: 1363 RVA: 0x0002A0AA File Offset: 0x000282AA
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000554 RID: 1364 RVA: 0x0002A0AC File Offset: 0x000282AC
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003F2 RID: 1010
	public const string ID = "HatchBaby";
}
