using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001D4 RID: 468
public class DeepFriedShellfishConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000960 RID: 2400 RVA: 0x0003D340 File Offset: 0x0003B540
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x0003D347 File Offset: 0x0003B547
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x0003D34C File Offset: 0x0003B54C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("DeepFriedShellfish", global::STRINGS.ITEMS.FOOD.DEEPFRIEDSHELLFISH.NAME, global::STRINGS.ITEMS.FOOD.DEEPFRIEDSHELLFISH.DESC, 1f, false, Assets.GetAnim("deepfried_shellfish_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.DEEP_FRIED_SHELLFISH);
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x0003D3B0 File Offset: 0x0003B5B0
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x0003D3B2 File Offset: 0x0003B5B2
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006C1 RID: 1729
	public const string ID = "DeepFriedShellfish";

	// Token: 0x040006C2 RID: 1730
	public static ComplexRecipe recipe;
}
