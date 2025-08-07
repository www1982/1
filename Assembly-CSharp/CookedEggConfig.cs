using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001CB RID: 459
public class CookedEggConfig : IEntityConfig
{
	// Token: 0x06000933 RID: 2355 RVA: 0x0003CF08 File Offset: 0x0003B108
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("CookedEgg", global::STRINGS.ITEMS.FOOD.COOKEDEGG.NAME, global::STRINGS.ITEMS.FOOD.COOKEDEGG.DESC, 1f, false, Assets.GetAnim("cookedegg_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.COOKED_EGG);
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x0003CF6C File Offset: 0x0003B16C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x0003CF6E File Offset: 0x0003B16E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006AE RID: 1710
	public const string ID = "CookedEgg";

	// Token: 0x040006AF RID: 1711
	public static ComplexRecipe recipe;
}
