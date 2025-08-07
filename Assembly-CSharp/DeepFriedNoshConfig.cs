using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001D3 RID: 467
public class DeepFriedNoshConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600095A RID: 2394 RVA: 0x0003D2C4 File Offset: 0x0003B4C4
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x0003D2CB File Offset: 0x0003B4CB
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("DeepFriedNosh", global::STRINGS.ITEMS.FOOD.DEEPFRIEDNOSH.NAME, global::STRINGS.ITEMS.FOOD.DEEPFRIEDNOSH.DESC, 1f, false, Assets.GetAnim("deepfried_nosh_beans_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.DEEP_FRIED_NOSH);
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x0003D334 File Offset: 0x0003B534
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x0003D336 File Offset: 0x0003B536
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006BF RID: 1727
	public const string ID = "DeepFriedNosh";

	// Token: 0x040006C0 RID: 1728
	public static ComplexRecipe recipe;
}
