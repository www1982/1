using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001CD RID: 461
public class CookedMeatConfig : IEntityConfig
{
	// Token: 0x0600093B RID: 2363 RVA: 0x0003CFE8 File Offset: 0x0003B1E8
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("CookedMeat", global::STRINGS.ITEMS.FOOD.COOKEDMEAT.NAME, global::STRINGS.ITEMS.FOOD.COOKEDMEAT.DESC, 1f, false, Assets.GetAnim("barbeque_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.COOKED_MEAT);
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x0003D04C File Offset: 0x0003B24C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x0003D04E File Offset: 0x0003B24E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006B2 RID: 1714
	public const string ID = "CookedMeat";

	// Token: 0x040006B3 RID: 1715
	public static ComplexRecipe recipe;
}
