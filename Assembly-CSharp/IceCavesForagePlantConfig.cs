using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200019B RID: 411
public class IceCavesForagePlantConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060007EA RID: 2026 RVA: 0x00036182 File Offset: 0x00034382
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x060007EB RID: 2027 RVA: 0x00036189 File Offset: 0x00034389
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060007EC RID: 2028 RVA: 0x0003618C File Offset: 0x0003438C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("IceCavesForagePlant", global::STRINGS.ITEMS.FOOD.ICECAVESFORAGEPLANT.NAME, global::STRINGS.ITEMS.FOOD.ICECAVESFORAGEPLANT.DESC, 1f, false, Assets.GetAnim("frozenberries_fruit_kanim"), "object", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.ICECAVESFORAGEPLANT);
	}

	// Token: 0x060007ED RID: 2029 RVA: 0x000361F0 File Offset: 0x000343F0
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x000361F2 File Offset: 0x000343F2
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005E8 RID: 1512
	public const string ID = "IceCavesForagePlant";
}
