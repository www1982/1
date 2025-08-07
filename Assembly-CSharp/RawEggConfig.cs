using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001EE RID: 494
public class RawEggConfig : IEntityConfig
{
	// Token: 0x060009E0 RID: 2528 RVA: 0x0003E1A8 File Offset: 0x0003C3A8
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("RawEgg", global::STRINGS.ITEMS.FOOD.RAWEGG.NAME, global::STRINGS.ITEMS.FOOD.RAWEGG.DESC, 1f, false, Assets.GetAnim("rawegg_kanim"), "object", Grid.SceneLayer.Ore, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.RAWEGG);
		TemperatureCookable temperatureCookable = gameObject.AddOrGet<TemperatureCookable>();
		temperatureCookable.cookTemperature = 344.15f;
		temperatureCookable.cookedID = "CookedEgg";
		return gameObject;
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x0003E229 File Offset: 0x0003C429
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x0003E22B File Offset: 0x0003C42B
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006F6 RID: 1782
	public const string ID = "RawEgg";
}
