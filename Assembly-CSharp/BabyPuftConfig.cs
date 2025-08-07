using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000153 RID: 339
[EntityConfigOrder(2)]
public class BabyPuftConfig : IEntityConfig
{
	// Token: 0x0600065C RID: 1628 RVA: 0x0002E086 File Offset: 0x0002C286
	public GameObject CreatePrefab()
	{
		GameObject gameObject = PuftConfig.CreatePuft("PuftBaby", CREATURES.SPECIES.PUFT.BABY.NAME, CREATURES.SPECIES.PUFT.BABY.DESC, "baby_puft_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Puft", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x0002E0C4 File Offset: 0x0002C2C4
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x0002E0C6 File Offset: 0x0002C2C6
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004C8 RID: 1224
	public const string ID = "PuftBaby";
}
