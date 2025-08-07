using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001F0 RID: 496
public class SalsaConfig : IEntityConfig
{
	// Token: 0x060009E9 RID: 2537 RVA: 0x0003E308 File Offset: 0x0003C508
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("Salsa", global::STRINGS.ITEMS.FOOD.SALSA.NAME, global::STRINGS.ITEMS.FOOD.SALSA.DESC, 1f, false, Assets.GetAnim("zestysalsa_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.5f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SALSA);
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x0003E36C File Offset: 0x0003C56C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x0003E36E File Offset: 0x0003C56E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006F8 RID: 1784
	public const string ID = "Salsa";

	// Token: 0x040006F9 RID: 1785
	public static ComplexRecipe recipe;
}
