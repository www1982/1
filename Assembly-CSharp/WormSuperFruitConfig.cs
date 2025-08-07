using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000207 RID: 519
public class WormSuperFruitConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000A5E RID: 2654 RVA: 0x0003EE70 File Offset: 0x0003D070
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x0003EE77 File Offset: 0x0003D077
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000A60 RID: 2656 RVA: 0x0003EE7C File Offset: 0x0003D07C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("WormSuperFruit", global::STRINGS.ITEMS.FOOD.WORMSUPERFRUIT.NAME, global::STRINGS.ITEMS.FOOD.WORMSUPERFRUIT.DESC, 1f, false, Assets.GetAnim("wormwood_super_fruits_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.WORMSUPERFRUIT);
	}

	// Token: 0x06000A61 RID: 2657 RVA: 0x0003EEE0 File Offset: 0x0003D0E0
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A62 RID: 2658 RVA: 0x0003EEE2 File Offset: 0x0003D0E2
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400072D RID: 1837
	public const string ID = "WormSuperFruit";
}
