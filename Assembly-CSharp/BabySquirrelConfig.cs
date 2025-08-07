using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200015E RID: 350
[EntityConfigOrder(2)]
public class BabySquirrelConfig : IEntityConfig
{
	// Token: 0x0600069E RID: 1694 RVA: 0x0002F2AE File Offset: 0x0002D4AE
	public GameObject CreatePrefab()
	{
		GameObject gameObject = SquirrelConfig.CreateSquirrel("SquirrelBaby", CREATURES.SPECIES.SQUIRREL.BABY.NAME, CREATURES.SPECIES.SQUIRREL.BABY.DESC, "baby_squirrel_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Squirrel", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x0002F2EC File Offset: 0x0002D4EC
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x0002F2EE File Offset: 0x0002D4EE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004FF RID: 1279
	public const string ID = "SquirrelBaby";
}
