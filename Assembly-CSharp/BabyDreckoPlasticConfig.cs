using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200011B RID: 283
[EntityConfigOrder(2)]
public class BabyDreckoPlasticConfig : IEntityConfig
{
	// Token: 0x06000536 RID: 1334 RVA: 0x00029799 File Offset: 0x00027999
	public GameObject CreatePrefab()
	{
		GameObject gameObject = DreckoPlasticConfig.CreateDrecko("DreckoPlasticBaby", CREATURES.SPECIES.DRECKO.VARIANT_PLASTIC.BABY.NAME, CREATURES.SPECIES.DRECKO.VARIANT_PLASTIC.BABY.DESC, "baby_drecko_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "DreckoPlastic", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000537 RID: 1335 RVA: 0x000297D7 File Offset: 0x000279D7
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000538 RID: 1336 RVA: 0x000297D9 File Offset: 0x000279D9
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003D5 RID: 981
	public const string ID = "DreckoPlasticBaby";
}
