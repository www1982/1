using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000119 RID: 281
[EntityConfigOrder(2)]
public class BabyDreckoConfig : IEntityConfig
{
	// Token: 0x0600052C RID: 1324 RVA: 0x00029459 File Offset: 0x00027659
	public GameObject CreatePrefab()
	{
		GameObject gameObject = DreckoConfig.CreateDrecko("DreckoBaby", CREATURES.SPECIES.DRECKO.BABY.NAME, CREATURES.SPECIES.DRECKO.BABY.DESC, "baby_drecko_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Drecko", null, false, 5f);
		return gameObject;
	}

	// Token: 0x0600052D RID: 1325 RVA: 0x00029497 File Offset: 0x00027697
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600052E RID: 1326 RVA: 0x00029499 File Offset: 0x00027699
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040003C7 RID: 967
	public const string ID = "DreckoBaby";
}
