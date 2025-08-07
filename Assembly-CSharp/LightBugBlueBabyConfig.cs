using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200012C RID: 300
[EntityConfigOrder(2)]
public class LightBugBlueBabyConfig : IEntityConfig
{
	// Token: 0x06000593 RID: 1427 RVA: 0x0002B000 File Offset: 0x00029200
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugBlueConfig.CreateLightBug("LightBugBlueBaby", CREATURES.SPECIES.LIGHTBUG.VARIANT_BLUE.BABY.NAME, CREATURES.SPECIES.LIGHTBUG.VARIANT_BLUE.BABY.DESC, "baby_lightbug_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "LightBugBlue", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x0002B03E File Offset: 0x0002923E
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x0002B040 File Offset: 0x00029240
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000423 RID: 1059
	public const string ID = "LightBugBlueBaby";
}
