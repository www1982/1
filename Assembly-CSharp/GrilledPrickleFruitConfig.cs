using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001DE RID: 478
public class GrilledPrickleFruitConfig : IEntityConfig
{
	// Token: 0x06000991 RID: 2449 RVA: 0x0003D7E0 File Offset: 0x0003B9E0
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("GrilledPrickleFruit", global::STRINGS.ITEMS.FOOD.GRILLEDPRICKLEFRUIT.NAME, global::STRINGS.ITEMS.FOOD.GRILLEDPRICKLEFRUIT.DESC, 1f, false, Assets.GetAnim("gristleberry_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.7f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.GRILLED_PRICKLEFRUIT);
	}

	// Token: 0x06000992 RID: 2450 RVA: 0x0003D844 File Offset: 0x0003BA44
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000993 RID: 2451 RVA: 0x0003D846 File Offset: 0x0003BA46
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006D2 RID: 1746
	public const string ID = "GrilledPrickleFruit";

	// Token: 0x040006D3 RID: 1747
	public static ComplexRecipe recipe;
}
