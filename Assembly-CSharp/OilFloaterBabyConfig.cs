using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000140 RID: 320
[EntityConfigOrder(2)]
public class OilFloaterBabyConfig : IEntityConfig
{
	// Token: 0x060005FE RID: 1534 RVA: 0x0002CC4A File Offset: 0x0002AE4A
	public GameObject CreatePrefab()
	{
		GameObject gameObject = OilFloaterConfig.CreateOilFloater("OilfloaterBaby", CREATURES.SPECIES.OILFLOATER.BABY.NAME, CREATURES.SPECIES.OILFLOATER.BABY.DESC, "baby_oilfloater_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "Oilfloater", null, false, 5f);
		return gameObject;
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x0002CC88 File Offset: 0x0002AE88
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x0002CC8A File Offset: 0x0002AE8A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400047B RID: 1147
	public const string ID = "OilfloaterBaby";
}
