using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001F3 RID: 499
public class SmokedDinosaurMeatConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060009F6 RID: 2550 RVA: 0x0003E476 File Offset: 0x0003C676
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x060009F7 RID: 2551 RVA: 0x0003E47D File Offset: 0x0003C67D
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060009F8 RID: 2552 RVA: 0x0003E480 File Offset: 0x0003C680
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("SmokedDinosaurMeat", global::STRINGS.ITEMS.FOOD.SMOKEDDINOSAURMEAT.NAME, global::STRINGS.ITEMS.FOOD.SMOKEDDINOSAURMEAT.DESC, 1f, false, Assets.GetAnim("dinobrisket_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SMOKED_DINOSAURMEAT);
	}

	// Token: 0x060009F9 RID: 2553 RVA: 0x0003E4E4 File Offset: 0x0003C6E4
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060009FA RID: 2554 RVA: 0x0003E4E6 File Offset: 0x0003C6E6
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040006FF RID: 1791
	public const string ID = "SmokedDinosaurMeat";

	// Token: 0x04000700 RID: 1792
	public static ComplexRecipe recipe;
}
