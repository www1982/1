using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class CritterDropOffConfig : IBuildingConfig
{
	// Token: 0x060001E7 RID: 487 RVA: 0x0000DDFC File Offset: 0x0000BFFC
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("CritterDropOff", 1, 3, "relocator_dropoff_02_kanim", 10, 10f, global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER1, MATERIALS.RAW_METALS, 1600f, BuildLocationRule.OnFloor, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER2, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.ViewMode = OverlayModes.Rooms.ID;
		buildingDef.LogicInputPorts = new List<LogicPorts.Port> { LogicPorts.Port.InputPort("CritterDropOffInput", new CellOffset(0, 0), global::STRINGS.BUILDINGS.PREFABS.CRITTERDROPOFF.LOGIC_INPUT.DESC, global::STRINGS.BUILDINGS.PREFABS.CRITTERDROPOFF.LOGIC_INPUT.LOGIC_PORT_ACTIVE, global::STRINGS.BUILDINGS.PREFABS.CRITTERDROPOFF.LOGIC_INPUT.LOGIC_PORT_INACTIVE, false, false) };
		buildingDef.AddSearchTerms(SEARCH_TERMS.CRITTER);
		buildingDef.AddSearchTerms(SEARCH_TERMS.RANCHING);
		return buildingDef;
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x0000DEC0 File Offset: 0x0000C0C0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(GameTags.CodexCategories.CreatureRelocator, false);
		Storage storage = go.AddOrGet<Storage>();
		storage.allowItemRemoval = false;
		storage.showDescriptor = true;
		storage.storageFilters = STORAGEFILTERS.BAGABLE_CREATURES;
		storage.workAnims = new HashedString[] { "place", "release" };
		storage.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_restrain_creature_kanim") };
		storage.workAnimPlayMode = KAnim.PlayMode.Once;
		storage.synchronizeAnims = false;
		storage.useGunForDelivery = false;
		storage.allowSettingOnlyFetchMarkedItems = false;
		go.AddOrGet<CreatureDeliveryPoint>();
		go.AddOrGet<BaggableCritterCapacityTracker>().filteredCount = true;
		go.AddOrGet<TreeFilterable>();
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x0000DF80 File Offset: 0x0000C180
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400012E RID: 302
	public const string ID = "CritterDropOff";

	// Token: 0x0400012F RID: 303
	public const string INPUT_PORT = "CritterDropOffInput";
}
