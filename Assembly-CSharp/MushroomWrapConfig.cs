using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001E4 RID: 484
public class MushroomWrapConfig : IEntityConfig
{
	// Token: 0x060009AE RID: 2478 RVA: 0x0003DBC0 File Offset: 0x0003BDC0
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("MushroomWrap", global::STRINGS.ITEMS.FOOD.MUSHROOMWRAP.NAME, global::STRINGS.ITEMS.FOOD.MUSHROOMWRAP.DESC, 1f, false, Assets.GetAnim("mushroom_wrap_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.5f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.MUSHROOM_WRAP);
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x0003DC24 File Offset: 0x0003BE24
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009B0 RID: 2480 RVA: 0x0003DC26 File Offset: 0x0003BE26
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006DE RID: 1758
	public const string ID = "MushroomWrap";

	// Token: 0x040006DF RID: 1759
	public static ComplexRecipe recipe;
}
