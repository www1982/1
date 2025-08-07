using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001CA RID: 458
public class ColdWheatBreadConfig : IEntityConfig
{
	// Token: 0x0600092F RID: 2351 RVA: 0x0003CE98 File Offset: 0x0003B098
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("ColdWheatBread", global::STRINGS.ITEMS.FOOD.COLDWHEATBREAD.NAME, global::STRINGS.ITEMS.FOOD.COLDWHEATBREAD.DESC, 1f, false, Assets.GetAnim("frostbread_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.COLD_WHEAT_BREAD);
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x0003CEFC File Offset: 0x0003B0FC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x0003CEFE File Offset: 0x0003B0FE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006AC RID: 1708
	public const string ID = "ColdWheatBread";

	// Token: 0x040006AD RID: 1709
	public static ComplexRecipe recipe;
}
