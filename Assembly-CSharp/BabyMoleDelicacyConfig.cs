using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200013A RID: 314
[EntityConfigOrder(2)]
public class BabyMoleDelicacyConfig : IEntityConfig
{
	// Token: 0x060005DB RID: 1499 RVA: 0x0002C48E File Offset: 0x0002A68E
	public GameObject CreatePrefab()
	{
		GameObject gameObject = MoleDelicacyConfig.CreateMole("MoleDelicacyBaby", CREATURES.SPECIES.MOLE.VARIANT_DELICACY.BABY.NAME, CREATURES.SPECIES.MOLE.VARIANT_DELICACY.BABY.DESC, "baby_driller_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "MoleDelicacy", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060005DC RID: 1500 RVA: 0x0002C4CC File Offset: 0x0002A6CC
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060005DD RID: 1501 RVA: 0x0002C4CE File Offset: 0x0002A6CE
	public void OnSpawn(GameObject inst)
	{
		MoleConfig.SetSpawnNavType(inst);
	}

	// Token: 0x0400045B RID: 1115
	public const string ID = "MoleDelicacyBaby";
}
