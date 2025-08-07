using System;
using STRINGS;
using UnityEngine;

// Token: 0x020000CB RID: 203
[EntityConfigOrder(2)]
public class BabyChameleonConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000388 RID: 904 RVA: 0x0001E9A1 File Offset: 0x0001CBA1
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x06000389 RID: 905 RVA: 0x0001E9A8 File Offset: 0x0001CBA8
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600038A RID: 906 RVA: 0x0001E9AB File Offset: 0x0001CBAB
	public GameObject CreatePrefab()
	{
		GameObject gameObject = ChameleonConfig.CreateChameleon("ChameleonBaby", CREATURES.SPECIES.CHAMELEON.BABY.NAME, CREATURES.SPECIES.CHAMELEON.BABY.DESC, "baby_chameleo_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Chameleon", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600038B RID: 907 RVA: 0x0001E9E9 File Offset: 0x0001CBE9
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600038C RID: 908 RVA: 0x0001E9EB File Offset: 0x0001CBEB
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040002BA RID: 698
	public const string ID = "ChameleonBaby";
}
