using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001CF RID: 463
public class CurryConfig : IEntityConfig
{
	// Token: 0x06000945 RID: 2373 RVA: 0x0003D0D4 File Offset: 0x0003B2D4
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("Curry", global::STRINGS.ITEMS.FOOD.CURRY.NAME, global::STRINGS.ITEMS.FOOD.CURRY.DESC, 1f, false, Assets.GetAnim("curried_beans_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.9f, 0.5f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.CURRY);
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x0003D138 File Offset: 0x0003B338
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x0003D13A File Offset: 0x0003B33A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006B6 RID: 1718
	public const string ID = "Curry";
}
