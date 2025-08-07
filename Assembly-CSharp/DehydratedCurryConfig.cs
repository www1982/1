using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001D0 RID: 464
public class DehydratedCurryConfig : IEntityConfig
{
	// Token: 0x06000949 RID: 2377 RVA: 0x0003D144 File Offset: 0x0003B344
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x0003D146 File Offset: 0x0003B346
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x0003D148 File Offset: 0x0003B348
	public GameObject CreatePrefab()
	{
		KAnimFile anim = Assets.GetAnim("dehydrated_food_curry_kanim");
		GameObject gameObject = EntityTemplates.CreateLooseEntity(DehydratedCurryConfig.ID.Name, global::STRINGS.ITEMS.FOOD.CURRY.DEHYDRATED.NAME, global::STRINGS.ITEMS.FOOD.CURRY.DEHYDRATED.DESC, 1f, true, anim, "idle", Grid.SceneLayer.BuildingFront, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.7f, true, 0, SimHashes.Polypropylene, null);
		EntityTemplates.ExtendEntityToDehydratedFoodPackage(gameObject, FOOD.FOOD_TYPES.CURRY);
		return gameObject;
	}

	// Token: 0x040006B7 RID: 1719
	public static Tag ID = new Tag("DehydratedCurry");

	// Token: 0x040006B8 RID: 1720
	public const float MASS = 1f;

	// Token: 0x040006B9 RID: 1721
	public const string ANIM_FILE = "dehydrated_food_curry_kanim";

	// Token: 0x040006BA RID: 1722
	public const string INITIAL_ANIM = "idle";
}
