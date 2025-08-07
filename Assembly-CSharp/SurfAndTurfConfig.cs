using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001FB RID: 507
public class SurfAndTurfConfig : IEntityConfig
{
	// Token: 0x06000A1F RID: 2591 RVA: 0x0003E89C File Offset: 0x0003CA9C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("SurfAndTurf", global::STRINGS.ITEMS.FOOD.SURFANDTURF.NAME, global::STRINGS.ITEMS.FOOD.SURFANDTURF.DESC, 1f, false, Assets.GetAnim("surfnturf_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SURF_AND_TURF);
	}

	// Token: 0x06000A20 RID: 2592 RVA: 0x0003E900 File Offset: 0x0003CB00
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A21 RID: 2593 RVA: 0x0003E902 File Offset: 0x0003CB02
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000713 RID: 1811
	public const string ID = "SurfAndTurf";

	// Token: 0x04000714 RID: 1812
	public static ComplexRecipe recipe;
}
