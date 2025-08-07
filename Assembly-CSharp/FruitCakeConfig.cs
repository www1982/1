using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001DC RID: 476
public class FruitCakeConfig : IEntityConfig
{
	// Token: 0x06000989 RID: 2441 RVA: 0x0003D700 File Offset: 0x0003B900
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("FruitCake", global::STRINGS.ITEMS.FOOD.FRUITCAKE.NAME, global::STRINGS.ITEMS.FOOD.FRUITCAKE.DESC, 1f, false, Assets.GetAnim("fruitcake_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.FRUITCAKE);
	}

	// Token: 0x0600098A RID: 2442 RVA: 0x0003D764 File Offset: 0x0003B964
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600098B RID: 2443 RVA: 0x0003D766 File Offset: 0x0003B966
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006CD RID: 1741
	public const string ID = "FruitCake";

	// Token: 0x040006CE RID: 1742
	public const string ANIM = "fruitcake_kanim";

	// Token: 0x040006CF RID: 1743
	public static ComplexRecipe recipe;
}
