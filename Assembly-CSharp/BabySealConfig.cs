using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200015B RID: 347
[EntityConfigOrder(2)]
public class BabySealConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600068E RID: 1678 RVA: 0x0002EF6D File Offset: 0x0002D16D
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0002EF74 File Offset: 0x0002D174
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x0002EF77 File Offset: 0x0002D177
	public GameObject CreatePrefab()
	{
		GameObject gameObject = SealConfig.CreateSeal("SealBaby", CREATURES.SPECIES.SEAL.BABY.NAME, CREATURES.SPECIES.SEAL.BABY.DESC, "baby_seal_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Seal", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x0002EFB5 File Offset: 0x0002D1B5
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x0002EFB7 File Offset: 0x0002D1B7
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004F2 RID: 1266
	public const string ID = "SealBaby";
}
