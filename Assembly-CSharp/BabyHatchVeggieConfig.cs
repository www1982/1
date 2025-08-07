using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000126 RID: 294
[EntityConfigOrder(2)]
public class BabyHatchVeggieConfig : IEntityConfig
{
	// Token: 0x06000571 RID: 1393 RVA: 0x0002A79E File Offset: 0x0002899E
	public GameObject CreatePrefab()
	{
		GameObject gameObject = HatchVeggieConfig.CreateHatch("HatchVeggieBaby", CREATURES.SPECIES.HATCH.VARIANT_VEGGIE.BABY.NAME, CREATURES.SPECIES.HATCH.VARIANT_VEGGIE.BABY.DESC, "baby_hatch_kanim", true);
		EntityTemplates.ExtendEntityToBeingABaby(gameObject, "HatchVeggie", null, false, 5f);
		return gameObject;
	}

	// Token: 0x06000572 RID: 1394 RVA: 0x0002A7DC File Offset: 0x000289DC
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000573 RID: 1395 RVA: 0x0002A7DE File Offset: 0x000289DE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400040C RID: 1036
	public const string ID = "HatchVeggieBaby";
}
