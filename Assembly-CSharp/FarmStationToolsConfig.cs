using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000257 RID: 599
public class FarmStationToolsConfig : IEntityConfig
{
	// Token: 0x06000C1D RID: 3101 RVA: 0x00049518 File Offset: 0x00047718
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("FarmStationTools", ITEMS.INDUSTRIAL_PRODUCTS.FARM_STATION_TOOLS.NAME, ITEMS.INDUSTRIAL_PRODUCTS.FARM_STATION_TOOLS.DESC, 5f, true, Assets.GetAnim("kit_planttender_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.6f, true, 0, SimHashes.Creature, new List<Tag> { GameTags.MiscPickupable });
		gameObject.AddOrGet<EntitySplitter>();
		return gameObject;
	}

	// Token: 0x06000C1E RID: 3102 RVA: 0x00049588 File Offset: 0x00047788
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000C1F RID: 3103 RVA: 0x0004958A File Offset: 0x0004778A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000863 RID: 2147
	public const string ID = "FarmStationTools";

	// Token: 0x04000864 RID: 2148
	public static readonly Tag tag = TagManager.Create("FarmStationTools");

	// Token: 0x04000865 RID: 2149
	public const float MASS = 5f;
}
