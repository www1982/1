using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001FD RID: 509
public class SwampDelightsConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000A28 RID: 2600 RVA: 0x0003E996 File Offset: 0x0003CB96
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x0003E99D File Offset: 0x0003CB9D
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x0003E9A0 File Offset: 0x0003CBA0
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("SwampDelights", global::STRINGS.ITEMS.FOOD.SWAMPDELIGHTS.NAME, global::STRINGS.ITEMS.FOOD.SWAMPDELIGHTS.DESC, 1f, false, Assets.GetAnim("swamp_delights_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.7f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SWAMP_DELIGHTS);
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x0003EA04 File Offset: 0x0003CC04
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A2C RID: 2604 RVA: 0x0003EA06 File Offset: 0x0003CC06
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400071A RID: 1818
	public const string ID = "SwampDelights";
}
