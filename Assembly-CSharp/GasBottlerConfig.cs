using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000219 RID: 537
public class GasBottlerConfig : IBuildingConfig
{
	// Token: 0x06000AC3 RID: 2755 RVA: 0x00040CC0 File Offset: 0x0003EEC0
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("GasBottler", 3, 2, "gas_bottler_kanim", 100, 120f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER4, MATERIALS.ALL_METALS, 800f, BuildLocationRule.OnFloor, BUILDINGS.DECOR.PENALTY.TIER1, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.Floodable = false;
		buildingDef.ViewMode = OverlayModes.GasConduits.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.GasVentIDs, "GasBottler");
		return buildingDef;
	}

	// Token: 0x06000AC4 RID: 2756 RVA: 0x00040D44 File Offset: 0x0003EF44
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
		storage.showDescriptor = true;
		storage.storageFilters = STORAGEFILTERS.GASES;
		storage.capacityKg = 200f;
		storage.SetDefaultStoredItemModifiers(GasBottlerConfig.GasBottlerStoredItemModifiers);
		storage.allowItemRemoval = false;
		go.AddTag(GameTags.GasSource);
		DropAllWorkable dropAllWorkable = go.AddOrGet<DropAllWorkable>();
		dropAllWorkable.removeTags = new List<Tag> { GameTags.GasSource };
		dropAllWorkable.resetTargetWorkableOnCompleteWork = true;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.storage = storage;
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.ignoreMinMassCheck = true;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.alwaysConsume = true;
		conduitConsumer.capacityKG = 200f;
		conduitConsumer.keepZeroMassObject = false;
		Bottler bottler = go.AddOrGet<Bottler>();
		bottler.storage = storage;
		bottler.workTime = 9f;
		bottler.userMaxCapacity = 25f;
		bottler.consumer = conduitConsumer;
	}

	// Token: 0x06000AC5 RID: 2757 RVA: 0x00040E1A File Offset: 0x0003F01A
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits, false);
	}

	// Token: 0x04000765 RID: 1893
	public const string ID = "GasBottler";

	// Token: 0x04000766 RID: 1894
	private const ConduitType CONDUIT_TYPE = ConduitType.Gas;

	// Token: 0x04000767 RID: 1895
	private const int WIDTH = 3;

	// Token: 0x04000768 RID: 1896
	private const int HEIGHT = 2;

	// Token: 0x04000769 RID: 1897
	private const float DEFAULT_FILL_LEVEL = 25f;

	// Token: 0x0400076A RID: 1898
	private const float CAPACITY = 200f;

	// Token: 0x0400076B RID: 1899
	private static readonly List<Storage.StoredItemModifier> GasBottlerStoredItemModifiers = new List<Storage.StoredItemModifier> { Storage.StoredItemModifier.Hide };
}
