using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000160 RID: 352
[EntityConfigOrder(2)]
public class BabySquirrelHugConfig : IEntityConfig
{
	// Token: 0x060006A8 RID: 1704 RVA: 0x0002F50E File Offset: 0x0002D70E
	public GameObject CreatePrefab()
	{
		GameObject gameObject = SquirrelHugConfig.CreateSquirrelHug("SquirrelHugBaby", CREATURES.SPECIES.SQUIRREL.VARIANT_HUG.BABY.NAME, CREATURES.SPECIES.SQUIRREL.VARIANT_HUG.BABY.DESC, "baby_squirrel_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "SquirrelHug", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x0002F54C File Offset: 0x0002D74C
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x0002F54E File Offset: 0x0002D74E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400050B RID: 1291
	public const string ID = "SquirrelHugBaby";
}
