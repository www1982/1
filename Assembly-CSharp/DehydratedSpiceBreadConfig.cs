using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001F7 RID: 503
public class DehydratedSpiceBreadConfig : IEntityConfig
{
	// Token: 0x06000A0C RID: 2572 RVA: 0x0003E658 File Offset: 0x0003C858
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A0D RID: 2573 RVA: 0x0003E65A File Offset: 0x0003C85A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x06000A0E RID: 2574 RVA: 0x0003E65C File Offset: 0x0003C85C
	public GameObject CreatePrefab()
	{
		KAnimFile anim = Assets.GetAnim("dehydrated_food_spicebread_kanim");
		GameObject gameObject = EntityTemplates.CreateLooseEntity(DehydratedSpiceBreadConfig.ID.Name, global::STRINGS.ITEMS.FOOD.SPICEBREAD.DEHYDRATED.NAME, global::STRINGS.ITEMS.FOOD.SPICEBREAD.DEHYDRATED.DESC, 1f, true, anim, "idle", Grid.SceneLayer.BuildingFront, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.7f, true, 0, SimHashes.Polypropylene, null);
		EntityTemplates.ExtendEntityToDehydratedFoodPackage(gameObject, FOOD.FOOD_TYPES.SPICEBREAD);
		return gameObject;
	}

	// Token: 0x04000707 RID: 1799
	public static Tag ID = new Tag("DehydratedSpiceBread");

	// Token: 0x04000708 RID: 1800
	public const float MASS = 1f;

	// Token: 0x04000709 RID: 1801
	public const string ANIM_FILE = "dehydrated_food_spicebread_kanim";

	// Token: 0x0400070A RID: 1802
	public const string INITIAL_ANIM = "idle";
}
