using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000142 RID: 322
[EntityConfigOrder(2)]
public class OilFloaterDecorBabyConfig : IEntityConfig
{
	// Token: 0x06000608 RID: 1544 RVA: 0x0002CE9E File Offset: 0x0002B09E
	public GameObject CreatePrefab()
	{
		GameObject gameObject = OilFloaterDecorConfig.CreateOilFloater("OilfloaterDecorBaby", CREATURES.SPECIES.OILFLOATER.VARIANT_DECOR.BABY.NAME, CREATURES.SPECIES.OILFLOATER.VARIANT_DECOR.BABY.DESC, "baby_oilfloater_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "OilfloaterDecor", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x0002CEDC File Offset: 0x0002B0DC
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x0002CEDE File Offset: 0x0002B0DE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000483 RID: 1155
	public const string ID = "OilfloaterDecorBaby";
}
