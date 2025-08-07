using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000174 RID: 372
public class BasicFabricConfig : IEntityConfig
{
	// Token: 0x06000719 RID: 1817 RVA: 0x00031498 File Offset: 0x0002F698
	public GameObject CreatePrefab()
	{
		GameObject gameObject = EntityTemplates.CreateLooseEntity(BasicFabricConfig.ID, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.BASIC_FABRIC.NAME, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.BASIC_FABRIC.DESC, 1f, true, Assets.GetAnim("swampreedwool_kanim"), "object", Grid.SceneLayer.BuildingBack, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.45f, true, SORTORDER.BUILDINGELEMENTS + BasicFabricTuning.SORTORDER, SimHashes.Creature, new List<Tag>
		{
			GameTags.IndustrialIngredient,
			GameTags.BuildingFiber
		});
		gameObject.AddOrGet<EntitySplitter>();
		gameObject.AddOrGet<PrefabAttributeModifiers>().AddAttributeDescriptor(this.decorModifier);
		return gameObject;
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x0003152E File Offset: 0x0002F72E
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x00031530 File Offset: 0x0002F730
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000555 RID: 1365
	public static string ID = "BasicFabric";

	// Token: 0x04000556 RID: 1366
	private AttributeModifier decorModifier = new AttributeModifier("Decor", 0.1f, global::STRINGS.ITEMS.INDUSTRIAL_PRODUCTS.BASIC_FABRIC.NAME, true, false, true);
}
