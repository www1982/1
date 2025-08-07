using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001C7 RID: 455
public class DehydratedFoodPackageConfig : IEntityConfig
{
	// Token: 0x0600091C RID: 2332 RVA: 0x0003CC00 File Offset: 0x0003AE00
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x0003CC02 File Offset: 0x0003AE02
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x0003CC04 File Offset: 0x0003AE04
	public GameObject CreatePrefab()
	{
		KAnimFile anim = Assets.GetAnim("dehydrated_food_burger_kanim");
		GameObject gameObject = EntityTemplates.CreateLooseEntity(DehydratedFoodPackageConfig.ID.Name, global::STRINGS.ITEMS.FOOD.BURGER.DEHYDRATED.NAME, global::STRINGS.ITEMS.FOOD.BURGER.DEHYDRATED.DESC, 1f, true, anim, "idle", Grid.SceneLayer.BuildingFront, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.7f, true, 0, SimHashes.Polypropylene, null);
		EntityTemplates.ExtendEntityToDehydratedFoodPackage(gameObject, FOOD.FOOD_TYPES.BURGER);
		return gameObject;
	}

	// Token: 0x040006A3 RID: 1699
	public static Tag ID = new Tag("DehydratedFoodPackage");

	// Token: 0x040006A4 RID: 1700
	public const float MASS = 1f;

	// Token: 0x040006A5 RID: 1701
	public const string ANIM_FILE = "dehydrated_food_burger_kanim";

	// Token: 0x040006A6 RID: 1702
	public const string INITIAL_ANIM = "idle";
}
