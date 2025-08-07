using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000151 RID: 337
[EntityConfigOrder(2)]
public class BabyPuftBleachstoneConfig : IEntityConfig
{
	// Token: 0x06000652 RID: 1618 RVA: 0x0002DE04 File Offset: 0x0002C004
	public GameObject CreatePrefab()
	{
		GameObject gameObject = PuftBleachstoneConfig.CreatePuftBleachstone("PuftBleachstoneBaby", CREATURES.SPECIES.PUFT.VARIANT_BLEACHSTONE.BABY.NAME, CREATURES.SPECIES.PUFT.VARIANT_BLEACHSTONE.BABY.DESC, "baby_puft_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "PuftBleachstone", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x0002DE42 File Offset: 0x0002C042
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x0002DE44 File Offset: 0x0002C044
	public void OnSpawn(GameObject inst)
	{
		BasePuftConfig.OnSpawn(inst);
	}

	// Token: 0x040004BC RID: 1212
	public const string ID = "PuftBleachstoneBaby";
}
