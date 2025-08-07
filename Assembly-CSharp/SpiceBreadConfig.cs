using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001F6 RID: 502
public class SpiceBreadConfig : IEntityConfig
{
	// Token: 0x06000A08 RID: 2568 RVA: 0x0003E5E8 File Offset: 0x0003C7E8
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("SpiceBread", global::STRINGS.ITEMS.FOOD.SPICEBREAD.NAME, global::STRINGS.ITEMS.FOOD.SPICEBREAD.DESC, 1f, false, Assets.GetAnim("pepperbread_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.9f, 0.6f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SPICEBREAD);
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0003E64C File Offset: 0x0003C84C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A0A RID: 2570 RVA: 0x0003E64E File Offset: 0x0003C84E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000705 RID: 1797
	public const string ID = "SpiceBread";

	// Token: 0x04000706 RID: 1798
	public static ComplexRecipe recipe;
}
