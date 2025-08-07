using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001E2 RID: 482
public class MushBarConfig : IEntityConfig
{
	// Token: 0x060009A4 RID: 2468 RVA: 0x0003D9C8 File Offset: 0x0003BBC8
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("MushBar", global::STRINGS.ITEMS.FOOD.MUSHBAR.NAME, global::STRINGS.ITEMS.FOOD.MUSHBAR.DESC, 1f, false, Assets.GetAnim("mushbar_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.MUSHBAR);
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x0003DA2C File Offset: 0x0003BC2C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x0003DA2E File Offset: 0x0003BC2E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006D8 RID: 1752
	public const string ID = "MushBar";

	// Token: 0x040006D9 RID: 1753
	public const string ANIM = "mushbar_kanim";

	// Token: 0x040006DA RID: 1754
	public static ComplexRecipe recipe;
}
