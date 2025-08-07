using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001C3 RID: 451
public class BasicPlantFoodConfig : IEntityConfig
{
	// Token: 0x06000907 RID: 2311 RVA: 0x0003CA0C File Offset: 0x0003AC0C
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("BasicPlantFood", global::STRINGS.ITEMS.FOOD.BASICPLANTFOOD.NAME, global::STRINGS.ITEMS.FOOD.BASICPLANTFOOD.DESC, 1f, false, Assets.GetAnim("meallicegrain_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.BASICPLANTFOOD);
		return gameObject;
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x0003CA72 File Offset: 0x0003AC72
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x0003CA74 File Offset: 0x0003AC74
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400069A RID: 1690
	public const string ID = "BasicPlantFood";
}
