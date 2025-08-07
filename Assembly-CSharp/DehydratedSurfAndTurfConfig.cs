using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001FC RID: 508
public class DehydratedSurfAndTurfConfig : IEntityConfig
{
	// Token: 0x06000A23 RID: 2595 RVA: 0x0003E90C File Offset: 0x0003CB0C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x0003E90E File Offset: 0x0003CB0E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x0003E910 File Offset: 0x0003CB10
	public GameObject CreatePrefab()
	{
		KAnimFile anim = Assets.GetAnim("dehydrated_food_surf_and_turf_kanim");
		GameObject gameObject = EntityTemplates.CreateLooseEntity(DehydratedSurfAndTurfConfig.ID.Name, global::STRINGS.ITEMS.FOOD.SURFANDTURF.DEHYDRATED.NAME, global::STRINGS.ITEMS.FOOD.SURFANDTURF.DEHYDRATED.DESC, 1f, true, anim, "idle", Grid.SceneLayer.BuildingFront, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.7f, true, 0, SimHashes.Polypropylene, null);
		EntityTemplates.ExtendEntityToDehydratedFoodPackage(gameObject, FOOD.FOOD_TYPES.SURF_AND_TURF);
		return gameObject;
	}

	// Token: 0x04000715 RID: 1813
	public static Tag ID = new Tag("DehydratedSurfAndTurf");

	// Token: 0x04000716 RID: 1814
	public const float MASS = 1f;

	// Token: 0x04000717 RID: 1815
	public const int FABRICATION_TIME_SECONDS = 300;

	// Token: 0x04000718 RID: 1816
	public const string ANIM_FILE = "dehydrated_food_surf_and_turf_kanim";

	// Token: 0x04000719 RID: 1817
	public const string INITIAL_ANIM = "idle";
}
