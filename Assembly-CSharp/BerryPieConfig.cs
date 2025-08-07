using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001C4 RID: 452
public class BerryPieConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600090B RID: 2315 RVA: 0x0003CA7E File Offset: 0x0003AC7E
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x0003CA85 File Offset: 0x0003AC85
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x0003CA88 File Offset: 0x0003AC88
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("BerryPie", global::STRINGS.ITEMS.FOOD.BERRYPIE.NAME, global::STRINGS.ITEMS.FOOD.BERRYPIE.DESC, 1f, false, Assets.GetAnim("wormwood_berry_pie_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.55f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.BERRY_PIE);
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x0003CAEC File Offset: 0x0003ACEC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x0003CAEE File Offset: 0x0003ACEE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400069B RID: 1691
	public const string ID = "BerryPie";

	// Token: 0x0400069C RID: 1692
	public static ComplexRecipe recipe;
}
