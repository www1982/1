using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001E6 RID: 486
public class PancakesConfig : IEntityConfig
{
	// Token: 0x060009B7 RID: 2487 RVA: 0x0003DCBC File Offset: 0x0003BEBC
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("Pancakes", global::STRINGS.ITEMS.FOOD.PANCAKES.NAME, global::STRINGS.ITEMS.FOOD.PANCAKES.DESC, 1f, false, Assets.GetAnim("stackedpancakes_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.8f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.PANCAKES);
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x0003DD20 File Offset: 0x0003BF20
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x0003DD22 File Offset: 0x0003BF22
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006E4 RID: 1764
	public const string ID = "Pancakes";

	// Token: 0x040006E5 RID: 1765
	public static ComplexRecipe recipe;
}
