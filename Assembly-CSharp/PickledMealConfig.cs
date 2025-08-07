using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001E8 RID: 488
public class PickledMealConfig : IEntityConfig
{
	// Token: 0x060009C1 RID: 2497 RVA: 0x0003DDA8 File Offset: 0x0003BFA8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("PickledMeal", global::STRINGS.ITEMS.FOOD.PICKLEDMEAL.NAME, global::STRINGS.ITEMS.FOOD.PICKLEDMEAL.DESC, 1f, false, Assets.GetAnim("pickledmeal_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.7f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.PICKLEDMEAL);
		gameObject.GetComponent<KPrefabID>().AddTag(GameTags.Pickled, false);
		return gameObject;
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x0003DE1D File Offset: 0x0003C01D
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x0003DE1F File Offset: 0x0003C01F
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006E9 RID: 1769
	public const string ID = "PickledMeal";

	// Token: 0x040006EA RID: 1770
	public static ComplexRecipe recipe;
}
