using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200010F RID: 271
[EntityConfigOrder(2)]
public class BabyCrabConfig : IEntityConfig
{
	// Token: 0x060004F2 RID: 1266 RVA: 0x00028438 File Offset: 0x00026638
	public GameObject CreatePrefab()
	{
		GameObject gameObject = CrabConfig.CreateCrab("CrabBaby", CREATURES.SPECIES.CRAB.BABY.NAME, CREATURES.SPECIES.CRAB.BABY.DESC, "baby_pincher_kanim", true, "CrabShell", 5f);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Crab", "CrabShell", false, 5f);
		gameObject.AddOrGetDef<BabyMonitor.Def>().onGrowDropUnits = 5f;
		return gameObject;
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x0002849F File Offset: 0x0002669F
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x000284A1 File Offset: 0x000266A1
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400038B RID: 907
	public const string ID = "CrabBaby";
}
