using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001DD RID: 477
public class GammaMushConfig : IEntityConfig
{
	// Token: 0x0600098D RID: 2445 RVA: 0x0003D770 File Offset: 0x0003B970
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("GammaMush", global::STRINGS.ITEMS.FOOD.GAMMAMUSH.NAME, global::STRINGS.ITEMS.FOOD.GAMMAMUSH.DESC, 1f, false, Assets.GetAnim("mushbarfried_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.GAMMAMUSH);
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x0003D7D4 File Offset: 0x0003B9D4
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600098F RID: 2447 RVA: 0x0003D7D6 File Offset: 0x0003B9D6
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006D0 RID: 1744
	public const string ID = "GammaMush";

	// Token: 0x040006D1 RID: 1745
	public static ComplexRecipe recipe;
}
