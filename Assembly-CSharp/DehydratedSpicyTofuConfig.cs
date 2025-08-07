using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001FA RID: 506
public class DehydratedSpicyTofuConfig : IEntityConfig
{
	// Token: 0x06000A1A RID: 2586 RVA: 0x0003E810 File Offset: 0x0003CA10
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A1B RID: 2587 RVA: 0x0003E812 File Offset: 0x0003CA12
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x06000A1C RID: 2588 RVA: 0x0003E814 File Offset: 0x0003CA14
	public GameObject CreatePrefab()
	{
		KAnimFile anim = Assets.GetAnim("dehydrated_food_spicy_tofu_kanim");
		GameObject gameObject = EntityTemplates.CreateLooseEntity(DehydratedSpicyTofuConfig.ID.Name, global::STRINGS.ITEMS.FOOD.SPICYTOFU.DEHYDRATED.NAME, global::STRINGS.ITEMS.FOOD.SPICYTOFU.DEHYDRATED.DESC, 1f, true, anim, "idle", Grid.SceneLayer.BuildingFront, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.7f, true, 0, SimHashes.Polypropylene, null);
		EntityTemplates.ExtendEntityToDehydratedFoodPackage(gameObject, FOOD.FOOD_TYPES.SPICY_TOFU);
		return gameObject;
	}

	// Token: 0x0400070F RID: 1807
	public static Tag ID = new Tag("DehydratedSpicyTofu");

	// Token: 0x04000710 RID: 1808
	public const float MASS = 1f;

	// Token: 0x04000711 RID: 1809
	public const string ANIM_FILE = "dehydrated_food_spicy_tofu_kanim";

	// Token: 0x04000712 RID: 1810
	public const string INITIAL_ANIM = "idle";
}
