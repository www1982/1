using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001EC RID: 492
public class QuicheConfig : IEntityConfig
{
	// Token: 0x060009D7 RID: 2519 RVA: 0x0003E0AC File Offset: 0x0003C2AC
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("Quiche", global::STRINGS.ITEMS.FOOD.QUICHE.NAME, global::STRINGS.ITEMS.FOOD.QUICHE.DESC, 1f, false, Assets.GetAnim("quiche_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.QUICHE);
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x0003E110 File Offset: 0x0003C310
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x0003E112 File Offset: 0x0003C312
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006F0 RID: 1776
	public const string ID = "Quiche";

	// Token: 0x040006F1 RID: 1777
	public static ComplexRecipe recipe;
}
