using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001CE RID: 462
public class CookedPikeappleConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600093F RID: 2367 RVA: 0x0003D058 File Offset: 0x0003B258
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x0003D05F File Offset: 0x0003B25F
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x0003D064 File Offset: 0x0003B264
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("CookedPikeapple", global::STRINGS.ITEMS.FOOD.COOKEDPIKEAPPLE.NAME, global::STRINGS.ITEMS.FOOD.COOKEDPIKEAPPLE.DESC, 1f, false, Assets.GetAnim("iceberry_cooked_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.6f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.COOKED_PIKEAPPLE);
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x0003D0C8 File Offset: 0x0003B2C8
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x0003D0CA File Offset: 0x0003B2CA
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006B4 RID: 1716
	public const string ID = "CookedPikeapple";

	// Token: 0x040006B5 RID: 1717
	public static ComplexRecipe recipe;
}
