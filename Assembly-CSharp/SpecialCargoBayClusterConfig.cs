using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200040B RID: 1035
public class SpecialCargoBayClusterConfig : IBuildingConfig
{
	// Token: 0x06001531 RID: 5425 RVA: 0x00078AEA File Offset: 0x00076CEA
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06001532 RID: 5426 RVA: 0x00078AF4 File Offset: 0x00076CF4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SpecialCargoBayCluster";
		int num = 3;
		int num2 = 1;
		string text2 = "rocket_storage_live_small_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] hollow_TIER = global::TUNING.BUILDINGS.ROCKETRY_MASS_KG.HOLLOW_TIER1;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.BuildingAttachPoint;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, hollow_TIER, refined_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.RequiresPowerInput = false;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		buildingDef.ShowInBuildMenu = false;
		buildingDef.AddSearchTerms(SEARCH_TERMS.TRANSPORT);
		return buildingDef;
	}

	// Token: 0x06001533 RID: 5427 RVA: 0x00078BAC File Offset: 0x00076DAC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 1), GameTags.Rocket, null)
		};
	}

	// Token: 0x06001534 RID: 5428 RVA: 0x00078C10 File Offset: 0x00076E10
	public override void DoPostConfigureComplete(GameObject go)
	{
		Storage storage = go.AddOrGet<Storage>();
		storage.SetDefaultStoredItemModifiers(SpecialCargoBayClusterConfig.StoredCrittersModifiers);
		storage.showCapacityStatusItem = false;
		storage.showInUI = false;
		storage.storageFilters = new List<Tag> { GameTags.BagableCreature };
		storage.allowSettingOnlyFetchMarkedItems = false;
		storage.allowItemRemoval = false;
		Storage storage2 = go.AddComponent<Storage>();
		storage2.SetDefaultStoredItemModifiers(SpecialCargoBayClusterConfig.StoredLootModifiers);
		storage2.showCapacityStatusItem = false;
		storage2.showInUI = false;
		storage2.allowSettingOnlyFetchMarkedItems = false;
		storage2.allowItemRemoval = false;
		go.AddOrGet<CopyBuildingSettings>();
		go.AddOrGetDef<SpecialCargoBayCluster.Def>();
		SpecialCargoBayClusterReceptacle specialCargoBayClusterReceptacle = go.AddOrGet<SpecialCargoBayClusterReceptacle>();
		specialCargoBayClusterReceptacle.AddDepositTag(GameTags.BagableCreature);
		specialCargoBayClusterReceptacle.AddAdditionalCriteria((GameObject obj) => obj.HasTag(GameTags.Creatures.Deliverable));
		specialCargoBayClusterReceptacle.sideProductStorage = storage2;
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.INSIGNIFICANT, 0f, 0f);
		SymbolOverrideControllerUtil.AddToPrefab(go);
		go.AddOrGet<Prioritizable>();
		Prioritizable.AddRef(go);
	}

	// Token: 0x04000C95 RID: 3221
	public const string ID = "SpecialCargoBayCluster";

	// Token: 0x04000C96 RID: 3222
	private static readonly List<Storage.StoredItemModifier> StoredCrittersModifiers = new List<Storage.StoredItemModifier>
	{
		Storage.StoredItemModifier.Insulate,
		Storage.StoredItemModifier.Hide
	};

	// Token: 0x04000C97 RID: 3223
	private static readonly List<Storage.StoredItemModifier> StoredLootModifiers = new List<Storage.StoredItemModifier>
	{
		Storage.StoredItemModifier.Hide,
		Storage.StoredItemModifier.Seal
	};
}
