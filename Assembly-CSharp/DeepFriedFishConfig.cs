using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001D1 RID: 465
public class DeepFriedFishConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600094E RID: 2382 RVA: 0x0003D1CE File Offset: 0x0003B3CE
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x0003D1D5 File Offset: 0x0003B3D5
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x0003D1D8 File Offset: 0x0003B3D8
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("DeepFriedFish", global::STRINGS.ITEMS.FOOD.DEEPFRIEDFISH.NAME, global::STRINGS.ITEMS.FOOD.DEEPFRIEDFISH.DESC, 1f, false, Assets.GetAnim("deepfried_fish_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.DEEP_FRIED_FISH);
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x0003D23C File Offset: 0x0003B43C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x0003D23E File Offset: 0x0003B43E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006BB RID: 1723
	public const string ID = "DeepFriedFish";

	// Token: 0x040006BC RID: 1724
	public static ComplexRecipe recipe;
}
