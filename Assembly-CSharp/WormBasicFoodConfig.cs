using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000204 RID: 516
public class WormBasicFoodConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000A4C RID: 2636 RVA: 0x0003ECFE File Offset: 0x0003CEFE
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000A4D RID: 2637 RVA: 0x0003ED05 File Offset: 0x0003CF05
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000A4E RID: 2638 RVA: 0x0003ED08 File Offset: 0x0003CF08
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("WormBasicFood", global::STRINGS.ITEMS.FOOD.WORMBASICFOOD.NAME, global::STRINGS.ITEMS.FOOD.WORMBASICFOOD.DESC, 1f, false, Assets.GetAnim("wormwood_roast_nuts_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.7f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.WORMBASICFOOD);
	}

	// Token: 0x06000A4F RID: 2639 RVA: 0x0003ED6C File Offset: 0x0003CF6C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A50 RID: 2640 RVA: 0x0003ED6E File Offset: 0x0003CF6E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000728 RID: 1832
	public const string ID = "WormBasicFood";

	// Token: 0x04000729 RID: 1833
	public static ComplexRecipe recipe;
}
