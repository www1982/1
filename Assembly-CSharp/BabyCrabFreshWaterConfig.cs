using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000111 RID: 273
[EntityConfigOrder(2)]
public class BabyCrabFreshWaterConfig : IEntityConfig
{
	// Token: 0x060004FC RID: 1276 RVA: 0x00028810 File Offset: 0x00026A10
	public GameObject CreatePrefab()
	{
		GameObject gameObject = CrabFreshWaterConfig.CreateCrabFreshWater("CrabFreshWaterBaby", CREATURES.SPECIES.CRAB.VARIANT_FRESH_WATER.BABY.NAME, CREATURES.SPECIES.CRAB.VARIANT_FRESH_WATER.BABY.DESC, "baby_pincher_kanim", true, "ShellfishMeat", 4);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "CrabFreshWater", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060004FD RID: 1277 RVA: 0x0002885F File Offset: 0x00026A5F
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060004FE RID: 1278 RVA: 0x00028861 File Offset: 0x00026A61
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000395 RID: 917
	public const string ID = "CrabFreshWaterBaby";
}
