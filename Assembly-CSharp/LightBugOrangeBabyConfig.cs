using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000132 RID: 306
[EntityConfigOrder(2)]
public class LightBugOrangeBabyConfig : IEntityConfig
{
	// Token: 0x060005B1 RID: 1457 RVA: 0x0002B7D4 File Offset: 0x000299D4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugOrangeConfig.CreateLightBug("LightBugOrangeBaby", CREATURES.SPECIES.LIGHTBUG.VARIANT_ORANGE.BABY.NAME, CREATURES.SPECIES.LIGHTBUG.VARIANT_ORANGE.BABY.DESC, "baby_lightbug_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "LightBugOrange", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x0002B812 File Offset: 0x00029A12
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x0002B814 File Offset: 0x00029A14
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000438 RID: 1080
	public const string ID = "LightBugOrangeBaby";
}
