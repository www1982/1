using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001C6 RID: 454
public class BurgerConfig : IEntityConfig
{
	// Token: 0x06000918 RID: 2328 RVA: 0x0003CB90 File Offset: 0x0003AD90
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("Burger", global::STRINGS.ITEMS.FOOD.BURGER.NAME, global::STRINGS.ITEMS.FOOD.BURGER.DESC, 1f, false, Assets.GetAnim("frost_burger_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.BURGER);
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x0003CBF4 File Offset: 0x0003ADF4
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x0003CBF6 File Offset: 0x0003ADF6
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006A1 RID: 1697
	public const string ID = "Burger";

	// Token: 0x040006A2 RID: 1698
	public static ComplexRecipe recipe;
}
