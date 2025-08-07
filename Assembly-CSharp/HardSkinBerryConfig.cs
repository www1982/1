using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000199 RID: 409
public class HardSkinBerryConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060007DE RID: 2014 RVA: 0x00035F14 File Offset: 0x00034114
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x00035F1B File Offset: 0x0003411B
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x00035F20 File Offset: 0x00034120
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("HardSkinBerry", global::STRINGS.ITEMS.FOOD.HARDSKINBERRY.NAME, global::STRINGS.ITEMS.FOOD.HARDSKINBERRY.DESC, 1f, false, Assets.GetAnim("iceBerry_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.HARDSKINBERRY);
		return gameObject;
	}

	// Token: 0x060007E1 RID: 2017 RVA: 0x00035F86 File Offset: 0x00034186
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x00035F88 File Offset: 0x00034188
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005E0 RID: 1504
	public const string ID = "HardSkinBerry";
}
