using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001F8 RID: 504
public class SpiceNutConfig : IEntityConfig
{
	// Token: 0x06000A11 RID: 2577 RVA: 0x0003E6E4 File Offset: 0x0003C8E4
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(SpiceNutConfig.ID, global::STRINGS.ITEMS.FOOD.SPICENUT.NAME, global::STRINGS.ITEMS.FOOD.SPICENUT.DESC, 1f, false, Assets.GetAnim("spicenut_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, true, 0, SimHashes.Creature, null);
		EntityTemplates.ExtendEntityToFood(gameObject, FOOD.FOOD_TYPES.SPICENUT);
		SoundEventVolumeCache.instance.AddVolume("vinespicenut_kanim", "VineSpiceNut_grow", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("vinespicenut_kanim", "VineSpiceNut_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		return gameObject;
	}

	// Token: 0x06000A12 RID: 2578 RVA: 0x0003E77C File Offset: 0x0003C97C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000A13 RID: 2579 RVA: 0x0003E77E File Offset: 0x0003C97E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400070B RID: 1803
	public static float SEEDS_PER_FRUIT = 1f;

	// Token: 0x0400070C RID: 1804
	public static string ID = "SpiceNut";
}
