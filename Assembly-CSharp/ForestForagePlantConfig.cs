using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200018B RID: 395
public class ForestForagePlantConfig : IEntityConfig
{
	// Token: 0x06000793 RID: 1939 RVA: 0x00033D90 File Offset: 0x00031F90
	public GameObject CreatePrefab()
	{
		return EntityTemplates.ExtendEntityToFood(EntityTemplates.CreateLooseEntity("ForestForagePlant", global::STRINGS.ITEMS.FOOD.FORESTFORAGEPLANT.NAME, global::STRINGS.ITEMS.FOOD.FORESTFORAGEPLANT.DESC, 1f, false, Assets.GetAnim("podmelon_fruit_kanim"), "object", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, true, 0, SimHashes.Creature, null), FOOD.FOOD_TYPES.FORESTFORAGEPLANT);
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x00033DF4 File Offset: 0x00031FF4
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x00033DF6 File Offset: 0x00031FF6
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005AA RID: 1450
	public const string ID = "ForestForagePlant";
}
