using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000162 RID: 354
[EntityConfigOrder(2)]
public class BabyStaterpillarConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060006B4 RID: 1716 RVA: 0x0002F79D File Offset: 0x0002D99D
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x0002F7A4 File Offset: 0x0002D9A4
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x0002F7A7 File Offset: 0x0002D9A7
	public GameObject CreatePrefab()
	{
		GameObject gameObject = StaterpillarConfig.CreateStaterpillar("StaterpillarBaby", CREATURES.SPECIES.STATERPILLAR.BABY.NAME, CREATURES.SPECIES.STATERPILLAR.BABY.DESC, "baby_caterpillar_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Staterpillar", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x0002F7E5 File Offset: 0x0002D9E5
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x0002F7E7 File Offset: 0x0002D9E7
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000512 RID: 1298
	public const string ID = "StaterpillarBaby";
}
