using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x0200025A RID: 602
public class PowerStationToolsConfig : IEntityConfig
{
	// Token: 0x06000C2E RID: 3118 RVA: 0x00049730 File Offset: 0x00047930
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity("PowerStationTools", ITEMS.INDUSTRIAL_PRODUCTS.POWER_STATION_TOOLS.NAME, ITEMS.INDUSTRIAL_PRODUCTS.POWER_STATION_TOOLS.DESC, 5f, true, Assets.GetAnim("kit_electrician_kanim"), "object", Grid.SceneLayer.Front, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.6f, true, 0, SimHashes.Creature, new List<Tag>
		{
			GameTags.IndustrialProduct,
			GameTags.MiscPickupable
		});
		gameObject.AddOrGet<EntitySplitter>();
		return gameObject;
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x000497AB File Offset: 0x000479AB
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x000497AD File Offset: 0x000479AD
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400086C RID: 2156
	public const string ID = "PowerStationTools";

	// Token: 0x0400086D RID: 2157
	public static readonly Tag tag = TagManager.Create("PowerStationTools");

	// Token: 0x0400086E RID: 2158
	public const float MASS = 5f;
}
