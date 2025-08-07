using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001D8 RID: 472
public class FishMeatConfig : IEntityConfig
{
	// Token: 0x06000977 RID: 2423 RVA: 0x0003D530 File Offset: 0x0003B730
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("FishMeat", global::STRINGS.ITEMS.FOOD.FISHMEAT.NAME, global::STRINGS.ITEMS.FOOD.FISHMEAT.DESC, 1f, false, Assets.GetAnim("pacufillet_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.FISH_MEAT);
		return gameObject;
	}

	// Token: 0x06000978 RID: 2424 RVA: 0x0003D596 File Offset: 0x0003B796
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000979 RID: 2425 RVA: 0x0003D598 File Offset: 0x0003B798
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006C6 RID: 1734
	public const string ID = "FishMeat";
}
