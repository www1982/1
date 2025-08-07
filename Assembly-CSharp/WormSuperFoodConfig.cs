using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000206 RID: 518
public class WormSuperFoodConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000A58 RID: 2648 RVA: 0x0003EDF4 File Offset: 0x0003CFF4
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000A59 RID: 2649 RVA: 0x0003EDFB File Offset: 0x0003CFFB
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000A5A RID: 2650 RVA: 0x0003EE00 File Offset: 0x0003D000
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("WormSuperFood", global::STRINGS.ITEMS.FOOD.WORMSUPERFOOD.NAME, global::STRINGS.ITEMS.FOOD.WORMSUPERFOOD.DESC, 1f, false, Assets.GetAnim("wormwood_preserved_berries_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.7f, 0.6f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.WORMSUPERFOOD);
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x0003EE64 File Offset: 0x0003D064
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x0003EE66 File Offset: 0x0003D066
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400072B RID: 1835
	public const string ID = "WormSuperFood";

	// Token: 0x0400072C RID: 1836
	public static ComplexRecipe recipe;
}
