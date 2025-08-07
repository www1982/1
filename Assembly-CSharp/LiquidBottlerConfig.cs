using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000276 RID: 630
public class LiquidBottlerConfig : IBuildingConfig
{
	// Token: 0x06000CC4 RID: 3268 RVA: 0x0004C74C File Offset: 0x0004A94C
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("LiquidBottler", 3, 2, "liquid_bottler_kanim", 100, 120f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER4, MATERIALS.ALL_METALS, 800f, BuildLocationRule.OnFloor, BUILDINGS.DECOR.PENALTY.TIER1, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.Floodable = false;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.LiquidVentIDs, "LiquidBottler");
		return buildingDef;
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x0004C7D0 File Offset: 0x0004A9D0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
		storage.showDescriptor = true;
		storage.storageFilters = STORAGEFILTERS.LIQUIDS;
		storage.capacityKg = 200f;
		storage.SetDefaultStoredItemModifiers(LiquidBottlerConfig.LiquidBottlerStoredItemModifiers);
		storage.allowItemRemoval = false;
		go.AddTag(GameTags.LiquidSource);
		DropAllWorkable dropAllWorkable = go.AddOrGet<DropAllWorkable>();
		dropAllWorkable.removeTags = new List<Tag> { GameTags.LiquidSource };
		dropAllWorkable.resetTargetWorkableOnCompleteWork = true;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.storage = storage;
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.ignoreMinMassCheck = true;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.alwaysConsume = true;
		conduitConsumer.capacityKG = 200f;
		conduitConsumer.keepZeroMassObject = false;
		Bottler bottler = go.AddOrGet<Bottler>();
		bottler.storage = storage;
		bottler.workTime = 9f;
		bottler.consumer = conduitConsumer;
		bottler.userMaxCapacity = 200f;
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x0004C8A6 File Offset: 0x0004AAA6
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits, false);
	}

	// Token: 0x040008A6 RID: 2214
	public const string ID = "LiquidBottler";

	// Token: 0x040008A7 RID: 2215
	private const ConduitType CONDUIT_TYPE = ConduitType.Liquid;

	// Token: 0x040008A8 RID: 2216
	private const int WIDTH = 3;

	// Token: 0x040008A9 RID: 2217
	private const int HEIGHT = 2;

	// Token: 0x040008AA RID: 2218
	private const float CAPACITY = 200f;

	// Token: 0x040008AB RID: 2219
	private static readonly List<Storage.StoredItemModifier> LiquidBottlerStoredItemModifiers = new List<Storage.StoredItemModifier>
	{
		Storage.StoredItemModifier.Hide,
		Storage.StoredItemModifier.Seal
	};
}
