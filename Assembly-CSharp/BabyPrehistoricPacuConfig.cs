using System;
using STRINGS;
using UnityEngine;

// Token: 0x0200014D RID: 333
[EntityConfigOrder(2)]
public class BabyPrehistoricPacuConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600063C RID: 1596 RVA: 0x0002D81C File Offset: 0x0002BA1C
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x0600063D RID: 1597 RVA: 0x0002D823 File Offset: 0x0002BA23
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600063E RID: 1598 RVA: 0x0002D826 File Offset: 0x0002BA26
	public GameObject CreatePrefab()
	{
		GameObject gameObject = PrehistoricPacuConfig.CreatePrehistoricPacu("PrehistoricPacuBaby", CREATURES.SPECIES.PREHISTORICPACU.BABY.NAME, CREATURES.SPECIES.PREHISTORICPACU.BABY.DESC, "baby_paculacanth_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "PrehistoricPacu", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x0002D864 File Offset: 0x0002BA64
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0002D866 File Offset: 0x0002BA66
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040004A6 RID: 1190
	public const string ID = "PrehistoricPacuBaby";
}
