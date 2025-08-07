using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000176 RID: 374
public class BasicForagePlantConfig : IEntityConfig
{
	// Token: 0x06000723 RID: 1827 RVA: 0x00031778 File Offset: 0x0002F978
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("BasicForagePlant", global::STRINGS.ITEMS.FOOD.BASICFORAGEPLANT.NAME, global::STRINGS.ITEMS.FOOD.BASICFORAGEPLANT.DESC, 1f, false, Assets.GetAnim("muckrootvegetable_kanim"), "object", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.BASICFORAGEPLANT);
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x000317DC File Offset: 0x0002F9DC
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x000317DE File Offset: 0x0002F9DE
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400055A RID: 1370
	public const string ID = "BasicForagePlant";
}
