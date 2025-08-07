using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200012A RID: 298
[EntityConfigOrder(2)]
public class LightBugBlackBabyConfig : IEntityConfig
{
	// Token: 0x06000589 RID: 1417 RVA: 0x0002AD48 File Offset: 0x00028F48
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugBlackConfig.CreateLightBug("LightBugBlackBaby", CREATURES.SPECIES.LIGHTBUG.VARIANT_BLACK.BABY.NAME, CREATURES.SPECIES.LIGHTBUG.VARIANT_BLACK.BABY.DESC, "baby_lightbug_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "LightBugBlack", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x0002AD86 File Offset: 0x00028F86
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x0002AD88 File Offset: 0x00028F88
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400041C RID: 1052
	public const string ID = "LightBugBlackBaby";
}
