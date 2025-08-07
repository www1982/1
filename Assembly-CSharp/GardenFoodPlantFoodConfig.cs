using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000192 RID: 402
public class GardenFoodPlantFoodConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060007B8 RID: 1976 RVA: 0x00034B11 File Offset: 0x00032D11
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x00034B18 File Offset: 0x00032D18
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060007BA RID: 1978 RVA: 0x00034B1C File Offset: 0x00032D1C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("GardenFoodPlantFood", global::STRINGS.ITEMS.FOOD.GARDENFOODPLANTFOOD.NAME, global::STRINGS.ITEMS.FOOD.GARDENFOODPLANTFOOD.DESC, 1f, false, Assets.GetAnim("spikefruit_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.GARDENFOODPLANT);
		return gameObject;
	}

	// Token: 0x060007BB RID: 1979 RVA: 0x00034B82 File Offset: 0x00032D82
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060007BC RID: 1980 RVA: 0x00034B84 File Offset: 0x00032D84
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005BB RID: 1467
	public const string ID = "GardenFoodPlantFood";
}
