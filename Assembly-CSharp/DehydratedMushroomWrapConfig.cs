using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001E5 RID: 485
public class DehydratedMushroomWrapConfig : IEntityConfig
{
	// Token: 0x060009B2 RID: 2482 RVA: 0x0003DC30 File Offset: 0x0003BE30
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009B3 RID: 2483 RVA: 0x0003DC32 File Offset: 0x0003BE32
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x060009B4 RID: 2484 RVA: 0x0003DC34 File Offset: 0x0003BE34
	public GameObject CreatePrefab()
	{
		KAnimFile anim = Assets.GetAnim("dehydrated_food_mushroom_wrap_kanim");
		GameObject gameObject = EntityTemplates.CreateLooseEntity(DehydratedMushroomWrapConfig.ID.Name, global::STRINGS.ITEMS.FOOD.MUSHROOMWRAP.DEHYDRATED.NAME, global::STRINGS.ITEMS.FOOD.MUSHROOMWRAP.DEHYDRATED.DESC, 1f, true, anim, "idle", Grid.SceneLayer.BuildingFront, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.7f, true, 0, SimHashes.Polypropylene, null);
		EntityTemplates.ExtendEntityToDehydratedFoodPackage(gameObject, FOOD.FOOD_TYPES.MUSHROOM_WRAP);
		return gameObject;
	}

	// Token: 0x040006E0 RID: 1760
	public static Tag ID = new Tag("DehydratedMushroomWrap");

	// Token: 0x040006E1 RID: 1761
	public const float MASS = 1f;

	// Token: 0x040006E2 RID: 1762
	public const string ANIM_FILE = "dehydrated_food_mushroom_wrap_kanim";

	// Token: 0x040006E3 RID: 1763
	public const string INITIAL_ANIM = "idle";
}
