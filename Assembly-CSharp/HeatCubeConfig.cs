using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x0200030D RID: 781
public class HeatCubeConfig : IEntityConfig
{
	// Token: 0x06001015 RID: 4117 RVA: 0x00060C0C File Offset: 0x0005EE0C
	public GameObject CreatePrefab()
	{
		return EntityTemplates.CreateLooseEntity("HeatCube", "Heat Cube", "A cube that holds heat.", 1000f, true, Assets.GetAnim("copper_kanim"), "idle_tallstone", Grid.SceneLayer.Ore, EntityTemplates.CollisionShape.RECTANGLE, 1f, 1f, true, SORTORDER.BUILDINGELEMENTS, SimHashes.Diamond, new List<Tag>
		{
			GameTags.MiscPickupable,
			GameTags.IndustrialIngredient
		});
	}

	// Token: 0x06001016 RID: 4118 RVA: 0x00060C7A File Offset: 0x0005EE7A
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001017 RID: 4119 RVA: 0x00060C7C File Offset: 0x0005EE7C
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000A4A RID: 2634
	public const string ID = "HeatCube";
}
