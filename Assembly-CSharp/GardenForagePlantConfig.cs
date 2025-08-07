using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000193 RID: 403
public class GardenForagePlantConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060007BE RID: 1982 RVA: 0x00034B8E File Offset: 0x00032D8E
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x060007BF RID: 1983 RVA: 0x00034B95 File Offset: 0x00032D95
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x00034B98 File Offset: 0x00032D98
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("GardenForagePlant", global::STRINGS.ITEMS.FOOD.GARDENFORAGEPLANT.NAME, global::STRINGS.ITEMS.FOOD.GARDENFORAGEPLANT.DESC, 1f, false, Assets.GetAnim("fatplantfood_kanim"), "object", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.GARDENFORAGEPLANT);
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x00034BFC File Offset: 0x00032DFC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x00034BFE File Offset: 0x00032DFE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005BC RID: 1468
	public const string ID = "GardenForagePlant";
}
