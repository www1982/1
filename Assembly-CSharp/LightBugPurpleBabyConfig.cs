using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000136 RID: 310
[EntityConfigOrder(2)]
public class LightBugPurpleBabyConfig : IEntityConfig
{
	// Token: 0x060005C5 RID: 1477 RVA: 0x0002BD2C File Offset: 0x00029F2C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = LightBugPurpleConfig.CreateLightBug("LightBugPurpleBaby", CREATURES.SPECIES.LIGHTBUG.VARIANT_PURPLE.BABY.NAME, CREATURES.SPECIES.LIGHTBUG.VARIANT_PURPLE.BABY.DESC, "baby_lightbug_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "LightBugPurple", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060005C6 RID: 1478 RVA: 0x0002BD6A File Offset: 0x00029F6A
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x0002BD6C File Offset: 0x00029F6C
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000446 RID: 1094
	public const string ID = "LightBugPurpleBaby";
}
