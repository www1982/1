using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200014F RID: 335
[EntityConfigOrder(2)]
public class BabyPuftAlphaConfig : IEntityConfig
{
	// Token: 0x06000648 RID: 1608 RVA: 0x0002DB73 File Offset: 0x0002BD73
	public GameObject CreatePrefab()
	{
		GameObject gameObject = PuftAlphaConfig.CreatePuftAlpha("PuftAlphaBaby", CREATURES.SPECIES.PUFT.VARIANT_ALPHA.BABY.NAME, CREATURES.SPECIES.PUFT.VARIANT_ALPHA.BABY.DESC, "baby_puft_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "PuftAlpha", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0002DBB1 File Offset: 0x0002BDB1
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x0002DBB3 File Offset: 0x0002BDB3
	public void OnSpawn(GameObject inst)
	{
		BasePuftConfig.OnSpawn(inst);
	}

	// Token: 0x040004B2 RID: 1202
	public const string ID = "PuftAlphaBaby";
}
