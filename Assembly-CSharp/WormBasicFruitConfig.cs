using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000205 RID: 517
public class WormBasicFruitConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000A52 RID: 2642 RVA: 0x0003ED78 File Offset: 0x0003CF78
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000A53 RID: 2643 RVA: 0x0003ED7F File Offset: 0x0003CF7F
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000A54 RID: 2644 RVA: 0x0003ED84 File Offset: 0x0003CF84
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("WormBasicFruit", global::STRINGS.ITEMS.FOOD.WORMBASICFRUIT.NAME, global::STRINGS.ITEMS.FOOD.WORMBASICFRUIT.DESC, 1f, false, Assets.GetAnim("wormwood_basic_fruit_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.7f, 0.4f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.WORMBASICFRUIT);
	}

	// Token: 0x06000A55 RID: 2645 RVA: 0x0003EDE8 File Offset: 0x0003CFE8
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A56 RID: 2646 RVA: 0x0003EDEA File Offset: 0x0003CFEA
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400072A RID: 1834
	public const string ID = "WormBasicFruit";
}
