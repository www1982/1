using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000113 RID: 275
[EntityConfigOrder(2)]
public class BabyCrabWoodConfig : IEntityConfig
{
	// Token: 0x06000506 RID: 1286 RVA: 0x00028AD0 File Offset: 0x00026CD0
	public GameObject CreatePrefab()
	{
		GameObject gameObject = CrabWoodConfig.CreateCrabWood("CrabWoodBaby", CREATURES.SPECIES.CRAB.VARIANT_WOOD.BABY.NAME, CREATURES.SPECIES.CRAB.VARIANT_WOOD.BABY.DESC, "baby_pincher_kanim", true, "CrabWoodShell", 10f);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "CrabWood", "CrabWoodShell", false, 5f);
		gameObject.AddOrGetDef<BabyMonitor.Def>().onGrowDropUnits = 50f;
		return gameObject;
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x00028B37 File Offset: 0x00026D37
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x00028B39 File Offset: 0x00026D39
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400039F RID: 927
	public const string ID = "CrabWoodBaby";
}
