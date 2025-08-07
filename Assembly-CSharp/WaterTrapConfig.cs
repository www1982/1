using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000BDF RID: 3039
public class WaterTrapConfig : IBuildingConfig
{
	// Token: 0x06005B18 RID: 23320 RVA: 0x0020E970 File Offset: 0x0020CB70
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("WaterTrap", 1, 2, "critter_trap_water_kanim", 10, 10f, global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3, MATERIALS.RAW_METALS, 1600f, BuildLocationRule.Anywhere, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER2, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
		buildingDef.LogicInputPorts = new List<LogicPorts.Port> { LogicPorts.Port.InputPort(LogicOperationalController.PORT_ID, new CellOffset(0, 0), global::STRINGS.BUILDINGS.PREFABS.REUSABLETRAP.INPUT_LOGIC_PORT, global::STRINGS.BUILDINGS.PREFABS.REUSABLETRAP.INPUT_LOGIC_PORT_ACTIVE, global::STRINGS.BUILDINGS.PREFABS.REUSABLETRAP.INPUT_LOGIC_PORT_INACTIVE, false, false) };
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort("TRAP_HAS_PREY_STATUS_PORT", new CellOffset(0, 1), global::STRINGS.BUILDINGS.PREFABS.REUSABLETRAP.LOGIC_PORT, global::STRINGS.BUILDINGS.PREFABS.REUSABLETRAP.LOGIC_PORT_ACTIVE, global::STRINGS.BUILDINGS.PREFABS.REUSABLETRAP.LOGIC_PORT_INACTIVE, false, false) };
		buildingDef.AudioCategory = "Metal";
		buildingDef.Floodable = false;
		buildingDef.AddSearchTerms(SEARCH_TERMS.RANCHING);
		buildingDef.AddSearchTerms(SEARCH_TERMS.CRITTER);
		return buildingDef;
	}

	// Token: 0x06005B19 RID: 23321 RVA: 0x0020EA70 File Offset: 0x0020CC70
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		go.AddOrGet<Prioritizable>();
		Prioritizable.AddRef(go);
		ArmTrapWorkable armTrapWorkable = go.AddOrGet<ArmTrapWorkable>();
		armTrapWorkable.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_critter_trap_water_kanim") };
		armTrapWorkable.initialOffsets = new CellOffset[]
		{
			new CellOffset(0, 1)
		};
		go.AddOrGet<Operational>();
		RangeVisualizer rangeVisualizer = go.AddOrGet<RangeVisualizer>();
		rangeVisualizer.OriginOffset = new Vector2I(0, 0);
		rangeVisualizer.BlockingTileVisible = false;
		Storage storage = go.AddOrGet<Storage>();
		storage.allowItemRemoval = true;
		storage.SetDefaultStoredItemModifiers(WaterTrapConfig.StoredItemModifiers);
		storage.sendOnStoreOnSpawn = true;
		TrapTrigger trapTrigger = go.AddOrGet<TrapTrigger>();
		trapTrigger.trappableCreatures = new Tag[] { GameTags.Creatures.Swimmer };
		trapTrigger.trappedOffset = new Vector2(0f, 1f);
		go.AddOrGetDef<WaterTrapTrail.Def>();
		ReusableTrap.Def def = go.AddOrGetDef<ReusableTrap.Def>();
		def.releaseCellOffset = new CellOffset(0, 1);
		def.OUTPUT_LOGIC_PORT_ID = "TRAP_HAS_PREY_STATUS_PORT";
		def.lures = new Tag[] { GameTags.Creatures.FishTrapLure };
		def.usingSymbolChaseCapturingAnimations = true;
		def.getTrappedAnimationNameCallback = () => "trapped";
		go.AddOrGet<LogicPorts>();
		go.AddOrGet<LogicOperationalController>();
	}

	// Token: 0x06005B1A RID: 23322 RVA: 0x0020EBC0 File Offset: 0x0020CDC0
	private static void AddGuide(GameObject go, bool occupy_tiles)
	{
		GameObject gameObject = new GameObject();
		gameObject.transform.parent = go.transform;
		gameObject.transform.SetLocalPosition(Vector3.zero);
		KBatchedAnimController kbatchedAnimController = gameObject.AddComponent<KBatchedAnimController>();
		kbatchedAnimController.Offset = go.GetComponent<Building>().Def.GetVisualizerOffset();
		kbatchedAnimController.AnimFiles = new KAnimFile[] { Assets.GetAnim(new HashedString("critter_trap_water_kanim")) };
		kbatchedAnimController.initialAnim = "place_guide";
		kbatchedAnimController.visibilityType = KAnimControllerBase.VisibilityType.OffscreenUpdate;
		kbatchedAnimController.isMovable = true;
		WaterTrapGuide waterTrapGuide = gameObject.AddComponent<WaterTrapGuide>();
		waterTrapGuide.parent = go;
		waterTrapGuide.occupyTiles = occupy_tiles;
	}

	// Token: 0x06005B1B RID: 23323 RVA: 0x0020EC5C File Offset: 0x0020CE5C
	public override void DoPostConfigureComplete(GameObject go)
	{
		WaterTrapConfig.AddGuide(go.GetComponent<Building>().Def.BuildingPreview, false);
		WaterTrapConfig.AddGuide(go.GetComponent<Building>().Def.BuildingUnderConstruction, false);
		Lure.Def def = go.AddOrGetDef<Lure.Def>();
		def.defaultLurePoints = new CellOffset[]
		{
			new CellOffset(0, 0)
		};
		def.radius = 32;
		go.AddOrGet<FakeFloorAdder>().floorOffsets = new CellOffset[]
		{
			new CellOffset(0, 0)
		};
	}

	// Token: 0x04003C7E RID: 15486
	public const string ID = "WaterTrap";

	// Token: 0x04003C7F RID: 15487
	public const string OUTPUT_LOGIC_PORT_ID = "TRAP_HAS_PREY_STATUS_PORT";

	// Token: 0x04003C80 RID: 15488
	public const int TRAIL_LENGTH = 4;

	// Token: 0x04003C81 RID: 15489
	private static readonly List<Storage.StoredItemModifier> StoredItemModifiers = new List<Storage.StoredItemModifier>();
}
