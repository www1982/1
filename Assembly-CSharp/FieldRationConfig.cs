using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001D7 RID: 471
public class FieldRationConfig : IEntityConfig
{
	// Token: 0x06000973 RID: 2419 RVA: 0x0003D4C0 File Offset: 0x0003B6C0
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("FieldRation", global::STRINGS.ITEMS.FOOD.FIELDRATION.NAME, global::STRINGS.ITEMS.FOOD.FIELDRATION.DESC, 1f, false, Assets.GetAnim("fieldration_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.FIELDRATION);
	}

	// Token: 0x06000974 RID: 2420 RVA: 0x0003D524 File Offset: 0x0003B724
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000975 RID: 2421 RVA: 0x0003D526 File Offset: 0x0003B726
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006C5 RID: 1733
	public const string ID = "FieldRation";
}
