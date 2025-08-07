using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000144 RID: 324
[EntityConfigOrder(2)]
public class OilFloaterHighTempBabyConfig : IEntityConfig
{
	// Token: 0x06000612 RID: 1554 RVA: 0x0002D0F4 File Offset: 0x0002B2F4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = OilFloaterHighTempConfig.CreateOilFloater("OilfloaterHighTempBaby", CREATURES.SPECIES.OILFLOATER.VARIANT_HIGHTEMP.BABY.NAME, CREATURES.SPECIES.OILFLOATER.VARIANT_HIGHTEMP.BABY.DESC, "baby_oilfloater_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "OilfloaterHighTemp", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x0002D132 File Offset: 0x0002B332
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x0002D134 File Offset: 0x0002B334
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400048D RID: 1165
	public const string ID = "OilfloaterHighTempBaby";
}
