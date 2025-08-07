using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001E0 RID: 480
public class LettuceConfig : IEntityConfig
{
	// Token: 0x0600099C RID: 2460 RVA: 0x0003D8E4 File Offset: 0x0003BAE4
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("Lettuce", global::STRINGS.ITEMS.FOOD.LETTUCE.NAME, global::STRINGS.ITEMS.FOOD.LETTUCE.DESC, 1f, false, Assets.GetAnim("sea_lettuce_leaves_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.LETTUCE);
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x0003D948 File Offset: 0x0003BB48
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x0003D94A File Offset: 0x0003BB4A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006D6 RID: 1750
	public const string ID = "Lettuce";
}
