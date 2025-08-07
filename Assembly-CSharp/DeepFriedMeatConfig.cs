using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001D2 RID: 466
public class DeepFriedMeatConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000954 RID: 2388 RVA: 0x0003D248 File Offset: 0x0003B448
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x0003D24F File Offset: 0x0003B44F
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x0003D254 File Offset: 0x0003B454
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("DeepFriedMeat", global::STRINGS.ITEMS.FOOD.DEEPFRIEDMEAT.NAME, global::STRINGS.ITEMS.FOOD.DEEPFRIEDMEAT.DESC, 1f, false, Assets.GetAnim("deepfried_meat_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.DEEP_FRIED_MEAT);
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x0003D2B8 File Offset: 0x0003B4B8
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x0003D2BA File Offset: 0x0003B4BA
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006BD RID: 1725
	public const string ID = "DeepFriedMeat";

	// Token: 0x040006BE RID: 1726
	public static ComplexRecipe recipe;
}
