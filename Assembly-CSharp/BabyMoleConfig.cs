using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000138 RID: 312
[EntityConfigOrder(2)]
public class BabyMoleConfig : IEntityConfig
{
	// Token: 0x060005D0 RID: 1488 RVA: 0x0002C09D File Offset: 0x0002A29D
	public GameObject CreatePrefab()
	{
		GameObject gameObject = MoleConfig.CreateMole("MoleBaby", CREATURES.SPECIES.MOLE.BABY.NAME, CREATURES.SPECIES.MOLE.BABY.DESC, "baby_driller_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Mole", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060005D1 RID: 1489 RVA: 0x0002C0DB File Offset: 0x0002A2DB
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060005D2 RID: 1490 RVA: 0x0002C0DD File Offset: 0x0002A2DD
	public void OnSpawn(GameObject inst)
	{
		MoleConfig.SetSpawnNavType(inst);
	}

	// Token: 0x0400044D RID: 1101
	public const string ID = "MoleBaby";
}
