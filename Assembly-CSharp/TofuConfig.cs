using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000202 RID: 514
public class TofuConfig : IEntityConfig
{
	// Token: 0x06000A41 RID: 2625 RVA: 0x0003EC04 File Offset: 0x0003CE04
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("Tofu", global::STRINGS.ITEMS.FOOD.TOFU.NAME, global::STRINGS.ITEMS.FOOD.TOFU.DESC, 1f, false, Assets.GetAnim("loafu_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.9f, 0.6f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.TOFU);
	}

	// Token: 0x06000A42 RID: 2626 RVA: 0x0003EC68 File Offset: 0x0003CE68
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A43 RID: 2627 RVA: 0x0003EC6A File Offset: 0x0003CE6A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000723 RID: 1827
	public const string ID = "Tofu";

	// Token: 0x04000724 RID: 1828
	public const string ANIM = "loafu_kanim";

	// Token: 0x04000725 RID: 1829
	public static ComplexRecipe recipe;
}
