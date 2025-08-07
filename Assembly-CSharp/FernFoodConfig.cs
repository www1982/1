using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001D6 RID: 470
public class FernFoodConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600096C RID: 2412 RVA: 0x0003D43A File Offset: 0x0003B63A
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x0003D441 File Offset: 0x0003B641
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x0003D444 File Offset: 0x0003B644
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity(FernFoodConfig.ID, global::STRINGS.ITEMS.FOOD.FERNFOOD.NAME, global::STRINGS.ITEMS.FOOD.FERNFOOD.DESC, 1f, true, Assets.GetAnim("megafrond_grain_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.FERNFOOD);
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0003D4A8 File Offset: 0x0003B6A8
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x0003D4AA File Offset: 0x0003B6AA
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006C4 RID: 1732
	public static string ID = "FernFood";
}
