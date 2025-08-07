using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000309 RID: 777
public class GasGrassHarvestedConfig : IEntityConfig
{
	// Token: 0x06000FFE RID: 4094 RVA: 0x0005F928 File Offset: 0x0005DB28
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("GasGrassHarvested", CREATURES.SPECIES.GASGRASS.NAME, CREATURES.SPECIES.GASGRASS.DESC, 1f, false, Assets.GetAnim("harvested_gassygrass_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, true, 0, SimHashes.Creature, new List<Tag> { GameTags.Other });
		gameObject.AddOrGet<EntitySplitter>();
		return gameObject;
	}

	// Token: 0x06000FFF RID: 4095 RVA: 0x0005F998 File Offset: 0x0005DB98
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001000 RID: 4096 RVA: 0x0005F99A File Offset: 0x0005DB9A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A24 RID: 2596
	public const string ID = "GasGrassHarvested";
}
