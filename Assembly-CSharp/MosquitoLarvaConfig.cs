using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200013E RID: 318
[EntityConfigOrder(2)]
public class MosquitoLarvaConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060005F2 RID: 1522 RVA: 0x0002C9E8 File Offset: 0x0002ABE8
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x0002C9EF File Offset: 0x0002ABEF
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x0002C9F2 File Offset: 0x0002ABF2
	public GameObject CreatePrefab()
	{
		GameObject gameObject = MosquitoConfig.CreateMosquito("MosquitoBaby", CREATURES.SPECIES.MOSQUITO.BABY.NAME, CREATURES.SPECIES.MOSQUITO.BABY.DESC, "baby_mosquito_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Mosquito", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x0002CA30 File Offset: 0x0002AC30
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x0002CA32 File Offset: 0x0002AC32
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000471 RID: 1137
	public const string ID = "MosquitoBaby";
}
