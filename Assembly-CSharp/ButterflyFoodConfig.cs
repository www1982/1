using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001C8 RID: 456
public class ButterflyFoodConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000921 RID: 2337 RVA: 0x0003CC8A File Offset: 0x0003AE8A
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x0003CC91 File Offset: 0x0003AE91
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x0003CC94 File Offset: 0x0003AE94
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("ButterflyFood", global::STRINGS.ITEMS.FOOD.BUTTERFLYFOOD.NAME, global::STRINGS.ITEMS.FOOD.BUTTERFLYFOOD.DESC, 1f, false, Assets.GetAnim("fried_mimillet_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.85f, 0.75f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.BUTTERFLYFOOD);
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x0003CCF8 File Offset: 0x0003AEF8
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x0003CCFA File Offset: 0x0003AEFA
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006A7 RID: 1703
	public const string ID = "ButterflyFood";

	// Token: 0x040006A8 RID: 1704
	public static ComplexRecipe recipe;
}
