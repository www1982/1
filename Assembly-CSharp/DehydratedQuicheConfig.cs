using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001ED RID: 493
public class DehydratedQuicheConfig : IEntityConfig
{
	// Token: 0x060009DB RID: 2523 RVA: 0x0003E11C File Offset: 0x0003C31C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x0003E11E File Offset: 0x0003C31E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x0003E120 File Offset: 0x0003C320
	public GameObject CreatePrefab()
	{
		KAnimFile anim = Assets.GetAnim("dehydrated_food_quiche_kanim");
		GameObject gameObject = EntityTemplates.CreateLooseEntity(DehydratedQuicheConfig.ID.Name, global::STRINGS.ITEMS.FOOD.QUICHE.DEHYDRATED.NAME, global::STRINGS.ITEMS.FOOD.QUICHE.DEHYDRATED.DESC, 1f, true, anim, "idle", Grid.SceneLayer.BuildingFront, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.7f, true, 0, SimHashes.Polypropylene, null);
		EntityTemplates.ExtendEntityToDehydratedFoodPackage(gameObject, FOOD.FOOD_TYPES.QUICHE);
		return gameObject;
	}

	// Token: 0x040006F2 RID: 1778
	public static Tag ID = new Tag("DehydratedQuiche");

	// Token: 0x040006F3 RID: 1779
	public const float MASS = 1f;

	// Token: 0x040006F4 RID: 1780
	public const string ANIM_FILE = "dehydrated_food_quiche_kanim";

	// Token: 0x040006F5 RID: 1781
	public const string INITIAL_ANIM = "idle";
}
