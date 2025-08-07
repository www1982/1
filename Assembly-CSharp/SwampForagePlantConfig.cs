using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001B1 RID: 433
public class SwampForagePlantConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060008A3 RID: 2211 RVA: 0x0003AAAC File Offset: 0x00038CAC
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x0003AAB3 File Offset: 0x00038CB3
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x0003AAB8 File Offset: 0x00038CB8
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("SwampForagePlant", global::STRINGS.ITEMS.FOOD.SWAMPFORAGEPLANT.NAME, global::STRINGS.ITEMS.FOOD.SWAMPFORAGEPLANT.DESC, 1f, false, Assets.GetAnim("swamptuber_vegetable_kanim"), "object", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.SWAMPFORAGEPLANT);
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x0003AB1C File Offset: 0x00038D1C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x0003AB1E File Offset: 0x00038D1E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000662 RID: 1634
	public const string ID = "SwampForagePlant";
}
