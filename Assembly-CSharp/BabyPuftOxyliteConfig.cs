using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000155 RID: 341
[EntityConfigOrder(2)]
public class BabyPuftOxyliteConfig : IEntityConfig
{
	// Token: 0x06000666 RID: 1638 RVA: 0x0002E310 File Offset: 0x0002C510
	public GameObject CreatePrefab()
	{
		GameObject gameObject = PuftOxyliteConfig.CreatePuftOxylite("PuftOxyliteBaby", CREATURES.SPECIES.PUFT.VARIANT_OXYLITE.BABY.NAME, CREATURES.SPECIES.PUFT.VARIANT_OXYLITE.BABY.DESC, "baby_puft_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "PuftOxylite", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x0002E34E File Offset: 0x0002C54E
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x0002E350 File Offset: 0x0002C550
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004D2 RID: 1234
	public const string ID = "PuftOxyliteBaby";
}
