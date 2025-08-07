using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001F9 RID: 505
public class SpicyTofuConfig : IEntityConfig
{
	// Token: 0x06000A16 RID: 2582 RVA: 0x0003E7A0 File Offset: 0x0003C9A0
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("SpicyTofu", global::STRINGS.ITEMS.FOOD.SPICYTOFU.NAME, global::STRINGS.ITEMS.FOOD.SPICYTOFU.DESC, 1f, false, Assets.GetAnim("spicey_tofu_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.9f, 0.6f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SPICY_TOFU);
	}

	// Token: 0x06000A17 RID: 2583 RVA: 0x0003E804 File Offset: 0x0003CA04
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A18 RID: 2584 RVA: 0x0003E806 File Offset: 0x0003CA06
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400070D RID: 1805
	public const string ID = "SpicyTofu";

	// Token: 0x0400070E RID: 1806
	public static ComplexRecipe recipe;
}
