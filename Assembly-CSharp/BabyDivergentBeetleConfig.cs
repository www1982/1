using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000115 RID: 277
[EntityConfigOrder(2)]
public class BabyDivergentBeetleConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000512 RID: 1298 RVA: 0x00028D5A File Offset: 0x00026F5A
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00028D61 File Offset: 0x00026F61
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x00028D64 File Offset: 0x00026F64
	public GameObject CreatePrefab()
	{
		GameObject gameObject = DivergentBeetleConfig.CreateDivergentBeetle("DivergentBeetleBaby", CREATURES.SPECIES.DIVERGENT.VARIANT_BEETLE.BABY.NAME, CREATURES.SPECIES.DIVERGENT.VARIANT_BEETLE.BABY.DESC, "baby_critter_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "DivergentBeetle", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x00028DA2 File Offset: 0x00026FA2
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x00028DA4 File Offset: 0x00026FA4
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003A9 RID: 937
	public const string ID = "DivergentBeetleBaby";
}
