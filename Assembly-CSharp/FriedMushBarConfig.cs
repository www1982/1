using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001D9 RID: 473
public class FriedMushBarConfig : IEntityConfig
{
	// Token: 0x0600097B RID: 2427 RVA: 0x0003D5A4 File Offset: 0x0003B7A4
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("FriedMushBar", global::STRINGS.ITEMS.FOOD.FRIEDMUSHBAR.NAME, global::STRINGS.ITEMS.FOOD.FRIEDMUSHBAR.DESC, 1f, false, Assets.GetAnim("mushbarfried_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.FRIEDMUSHBAR);
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x0003D608 File Offset: 0x0003B808
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x0003D60A File Offset: 0x0003B80A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006C7 RID: 1735
	public const string ID = "FriedMushBar";

	// Token: 0x040006C8 RID: 1736
	public static ComplexRecipe recipe;
}
