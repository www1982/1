using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200012E RID: 302
[EntityConfigOrder(2)]
public class LightBugBabyConfig : IEntityConfig
{
	// Token: 0x0600059D RID: 1437 RVA: 0x0002B2A4 File Offset: 0x000294A4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugConfig.CreateLightBug("LightBugBaby", CREATURES.SPECIES.LIGHTBUG.BABY.NAME, CREATURES.SPECIES.LIGHTBUG.BABY.DESC, "baby_lightbug_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "LightBug", null, false, 5f);
		gameObject.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.LightSource, false);
		return gameObject;
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0002B2FE File Offset: 0x000294FE
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0002B300 File Offset: 0x00029500
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400042A RID: 1066
	public const string ID = "LightBugBaby";
}
