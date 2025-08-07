using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000117 RID: 279
[EntityConfigOrder(2)]
public class BabyWormConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000520 RID: 1312 RVA: 0x000290ED File Offset: 0x000272ED
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x000290F4 File Offset: 0x000272F4
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x000290F7 File Offset: 0x000272F7
	public GameObject CreatePrefab()
	{
		GameObject gameObject = DivergentWormConfig.CreateWorm("DivergentWormBaby", CREATURES.SPECIES.DIVERGENT.VARIANT_WORM.BABY.NAME, CREATURES.SPECIES.DIVERGENT.VARIANT_WORM.BABY.DESC, "baby_worm_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "DivergentWorm", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x00029135 File Offset: 0x00027335
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x00029137 File Offset: 0x00027337
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003B9 RID: 953
	public const string ID = "DivergentWormBaby";
}
