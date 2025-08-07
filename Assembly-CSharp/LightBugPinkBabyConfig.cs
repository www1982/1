using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000134 RID: 308
[EntityConfigOrder(2)]
public class LightBugPinkBabyConfig : IEntityConfig
{
	// Token: 0x060005BB RID: 1467 RVA: 0x0002BA88 File Offset: 0x00029C88
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugPinkConfig.CreateLightBug("LightBugPinkBaby", CREATURES.SPECIES.LIGHTBUG.VARIANT_PINK.BABY.NAME, CREATURES.SPECIES.LIGHTBUG.VARIANT_PINK.BABY.DESC, "baby_lightbug_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "LightBugPink", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x0002BAC6 File Offset: 0x00029CC6
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060005BD RID: 1469 RVA: 0x0002BAC8 File Offset: 0x00029CC8
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400043F RID: 1087
	public const string ID = "LightBugPinkBaby";
}
