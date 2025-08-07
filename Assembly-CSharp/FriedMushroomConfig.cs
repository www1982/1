using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001DA RID: 474
public class FriedMushroomConfig : IEntityConfig
{
	// Token: 0x0600097F RID: 2431 RVA: 0x0003D614 File Offset: 0x0003B814
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("FriedMushroom", global::STRINGS.ITEMS.FOOD.FRIEDMUSHROOM.NAME, global::STRINGS.ITEMS.FOOD.FRIEDMUSHROOM.DESC, 1f, false, Assets.GetAnim("funguscapfried_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.6f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.FRIED_MUSHROOM);
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x0003D678 File Offset: 0x0003B878
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000981 RID: 2433 RVA: 0x0003D67A File Offset: 0x0003B87A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006C9 RID: 1737
	public const string ID = "FriedMushroom";

	// Token: 0x040006CA RID: 1738
	public static ComplexRecipe recipe;
}
