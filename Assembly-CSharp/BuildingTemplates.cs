using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000030 RID: 48
public class BuildingTemplates
{
	// Token: 0x060000D2 RID: 210 RVA: 0x00007038 File Offset: 0x00005238
	public static BuildingDef CreateBuildingDef(string id, int width, int height, string anim, int hitpoints, float construction_time, float[] construction_mass, string[] construction_materials, float melting_point, BuildLocationRule build_location_rule, EffectorValues decor, EffectorValues noise, float temperature_modification_mass_scale = 0.2f)
	{
		BuildingDef buildingDef = ScriptableObject.CreateInstance<BuildingDef>();
		buildingDef.PrefabID = id;
		buildingDef.InitDef();
		buildingDef.name = id;
		buildingDef.Mass = construction_mass;
		buildingDef.MassForTemperatureModification = construction_mass[0] * temperature_modification_mass_scale;
		buildingDef.WidthInCells = width;
		buildingDef.HeightInCells = height;
		buildingDef.HitPoints = hitpoints;
		buildingDef.ConstructionTime = construction_time;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.MaterialCategory = construction_materials;
		buildingDef.BaseMeltingPoint = melting_point;
		if (build_location_rule == BuildLocationRule.Anywhere || build_location_rule == BuildLocationRule.Tile || build_location_rule - BuildLocationRule.Conduit <= 2)
		{
			buildingDef.ContinuouslyCheckFoundation = false;
		}
		else
		{
			buildingDef.ContinuouslyCheckFoundation = true;
		}
		buildingDef.BuildLocationRule = build_location_rule;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.AnimFiles = new KAnimFile[] { Assets.GetAnim(anim) };
		buildingDef.GenerateOffsets();
		buildingDef.BaseDecor = (float)decor.amount;
		buildingDef.BaseDecorRadius = (float)decor.radius;
		buildingDef.BaseNoisePollution = noise.amount;
		buildingDef.BaseNoisePollutionRadius = noise.radius;
		return buildingDef;
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x00007130 File Offset: 0x00005330
	public static void CreateStandardBuildingDef(BuildingDef def)
	{
		def.Breakable = true;
	}

	// Token: 0x060000D4 RID: 212 RVA: 0x0000713C File Offset: 0x0000533C
	public static void CreateFoundationTileDef(BuildingDef def)
	{
		def.IsFoundation = true;
		def.TileLayer = ObjectLayer.FoundationTile;
		def.ReplacementLayer = ObjectLayer.ReplacementTile;
		def.ReplacementCandidateLayers = new List<ObjectLayer>
		{
			ObjectLayer.FoundationTile,
			ObjectLayer.LadderTile,
			ObjectLayer.Backwall
		};
		def.ReplacementTags = new List<Tag>
		{
			GameTags.FloorTiles,
			GameTags.Ladders,
			GameTags.Backwall
		};
		def.EquivalentReplacementLayers = new List<ObjectLayer> { ObjectLayer.ReplacementLadder };
	}

	// Token: 0x060000D5 RID: 213 RVA: 0x000071C1 File Offset: 0x000053C1
	public static void CreateLadderDef(BuildingDef def)
	{
		def.TileLayer = ObjectLayer.LadderTile;
		def.ReplacementLayer = ObjectLayer.ReplacementLadder;
		def.ReplacementTags = new List<Tag> { GameTags.Ladders };
		def.EquivalentReplacementLayers = new List<ObjectLayer> { ObjectLayer.ReplacementTile };
	}

	// Token: 0x060000D6 RID: 214 RVA: 0x000071FC File Offset: 0x000053FC
	public static void CreateElectricalBuildingDef(BuildingDef def)
	{
		BuildingTemplates.CreateStandardBuildingDef(def);
		def.RequiresPowerInput = true;
		def.ViewMode = OverlayModes.Power.ID;
		def.AudioCategory = "HollowMetal";
	}

	// Token: 0x060000D7 RID: 215 RVA: 0x00007221 File Offset: 0x00005421
	public static void CreateRocketBuildingDef(BuildingDef def)
	{
		BuildingTemplates.CreateStandardBuildingDef(def);
		def.Invincible = true;
		def.DefaultAnimState = "grounded";
		def.UseStructureTemperature = false;
	}

	// Token: 0x060000D8 RID: 216 RVA: 0x00007242 File Offset: 0x00005442
	public static void CreateMonumentBuildingDef(BuildingDef def)
	{
		BuildingTemplates.CreateStandardBuildingDef(def);
		def.Invincible = true;
	}

	// Token: 0x060000D9 RID: 217 RVA: 0x00007251 File Offset: 0x00005451
	public static Storage CreateDefaultStorage(GameObject go, bool forceCreate = false)
	{
		Storage storage = (forceCreate ? go.AddComponent<Storage>() : go.AddOrGet<Storage>());
		storage.capacityKg = 2000f;
		return storage;
	}

	// Token: 0x060000DA RID: 218 RVA: 0x00007270 File Offset: 0x00005470
	public static void CreateComplexFabricatorStorage(GameObject go, ComplexFabricator fabricator)
	{
		fabricator.inStorage = go.AddComponent<Storage>();
		fabricator.inStorage.capacityKg = 20000f;
		fabricator.inStorage.showInUI = true;
		fabricator.inStorage.SetDefaultStoredItemModifiers(Storage.StandardFabricatorStorage);
		fabricator.buildStorage = go.AddComponent<Storage>();
		fabricator.buildStorage.capacityKg = 20000f;
		fabricator.buildStorage.showInUI = true;
		fabricator.buildStorage.SetDefaultStoredItemModifiers(Storage.StandardFabricatorStorage);
		fabricator.outStorage = go.AddComponent<Storage>();
		fabricator.outStorage.capacityKg = 20000f;
		fabricator.outStorage.showInUI = true;
		fabricator.outStorage.SetDefaultStoredItemModifiers(Storage.StandardFabricatorStorage);
	}

	// Token: 0x060000DB RID: 219 RVA: 0x00007325 File Offset: 0x00005525
	public static void DoPostConfigure(GameObject go)
	{
	}

	// Token: 0x060000DC RID: 220 RVA: 0x00007328 File Offset: 0x00005528
	public static GameObject ExtendBuildingToRocketModule(GameObject template, string vanillaBGAnim, bool clusterRocket = false)
	{
		template.AddTag(GameTags.RocketModule);
		RocketModule rocketModule;
		if (clusterRocket)
		{
			rocketModule = template.AddOrGet<RocketModuleCluster>();
		}
		else
		{
			rocketModule = template.AddOrGet<RocketModule>();
		}
		if (vanillaBGAnim != null)
		{
			rocketModule.SetBGKAnim(Assets.GetAnim(vanillaBGAnim));
		}
		KBatchedAnimController component = template.GetComponent<KBatchedAnimController>();
		component.isMovable = true;
		component.initialMode = KAnim.PlayMode.Loop;
		BuildingDef def = template.GetComponent<Building>().Def;
		def.ShowInBuildMenu = def.ShowInBuildMenu && !DlcManager.FeatureClusterSpaceEnabled();
		if (def.WidthInCells == 3)
		{
			template.AddOrGet<VerticalModuleTiler>();
		}
		GameObject buildingUnderConstruction = def.BuildingUnderConstruction;
		if (clusterRocket)
		{
			buildingUnderConstruction.AddOrGet<RocketModuleCluster>();
		}
		else
		{
			buildingUnderConstruction.AddOrGet<RocketModule>();
		}
		AttachableBuilding component2 = template.GetComponent<AttachableBuilding>();
		if (component2 != null)
		{
			buildingUnderConstruction.AddOrGet<AttachableBuilding>().attachableToTag = component2.attachableToTag;
		}
		BuildingAttachPoint component3 = template.GetComponent<BuildingAttachPoint>();
		if (component3 != null)
		{
			buildingUnderConstruction.AddOrGet<BuildingAttachPoint>().points = component3.points;
		}
		template.GetComponent<Building>().Def.ThermalConductivity = 0.1f;
		Storage component4 = template.GetComponent<Storage>();
		if (component4 != null)
		{
			component4.showUnreachableStatus = true;
		}
		return template;
	}

	// Token: 0x060000DD RID: 221 RVA: 0x0000743C File Offset: 0x0000563C
	public static GameObject ExtendBuildingToRocketModuleCluster(GameObject template, string vanillaBGAnim, int burden, float enginePower = 0f, float fuelCostPerDistance = 0f)
	{
		template.AddTag(GameTags.RocketModule);
		template = BuildingTemplates.ExtendBuildingToRocketModule(template, vanillaBGAnim, true);
		BuildingDef def = template.GetComponent<Building>().Def;
		GameObject buildingUnderConstruction = def.BuildingUnderConstruction;
		DebugUtil.Assert(Array.IndexOf<string>(def.RequiredDlcIds, "EXPANSION1_ID") != -1, "Only expansion1 rocket engines should be expanded to Cluster Modules.");
		template.AddOrGet<ReorderableBuilding>();
		buildingUnderConstruction.AddOrGet<ReorderableBuilding>();
		if (def.Cancellable)
		{
			global::Debug.LogError(def.Name + " Def should be marked 'Cancellable = false' as they implement their own cancel logic in ReorderableBuilding");
		}
		template.GetComponent<ReorderableBuilding>().buildConditions.Add(new ResearchCompleted());
		template.GetComponent<ReorderableBuilding>().buildConditions.Add(new MaterialsAvailable());
		template.GetComponent<ReorderableBuilding>().buildConditions.Add(new PlaceSpaceAvailable());
		template.GetComponent<ReorderableBuilding>().buildConditions.Add(new RocketHeightLimit());
		if (template.GetComponent<RocketEngineCluster>())
		{
			template.GetComponent<ReorderableBuilding>().buildConditions.Add(new LimitOneEngine());
			template.GetComponent<ReorderableBuilding>().buildConditions.Add(new EngineOnBottom());
		}
		if (template.GetComponent<PassengerRocketModule>())
		{
			template.GetComponent<ReorderableBuilding>().buildConditions.Add(new NoFreeRocketInterior());
		}
		if (template.GetComponent<CargoBay>())
		{
			template.AddOrGet<CargoBayConduit>();
		}
		RocketModulePerformance rocketModulePerformance = new RocketModulePerformance((float)burden, fuelCostPerDistance, enginePower);
		template.GetComponent<RocketModuleCluster>().performanceStats = rocketModulePerformance;
		template.GetComponent<Building>().Def.BuildingUnderConstruction.GetComponent<RocketModuleCluster>().performanceStats = rocketModulePerformance;
		def.ShowInBuildMenu = false;
		return template;
	}

	// Token: 0x060000DE RID: 222 RVA: 0x000075B8 File Offset: 0x000057B8
	public static GameObject ExtendBuildingToClusterCargoBay(GameObject template, float capacity, List<Tag> storageFilters, CargoBay.CargoType cargoType)
	{
		Storage storage = template.AddOrGet<Storage>();
		storage.capacityKg = capacity;
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		storage.showCapacityStatusItem = true;
		storage.storageFilters = storageFilters;
		storage.allowSettingOnlyFetchMarkedItems = false;
		CargoBayCluster cargoBayCluster = template.AddOrGet<CargoBayCluster>();
		cargoBayCluster.storage = storage;
		cargoBayCluster.storageType = cargoType;
		TreeFilterable treeFilterable = template.AddOrGet<TreeFilterable>();
		treeFilterable.dropIncorrectOnFilterChange = false;
		treeFilterable.autoSelectStoredOnLoad = false;
		return template;
	}

	// Token: 0x060000DF RID: 223 RVA: 0x0000761A File Offset: 0x0000581A
	public static void ExtendBuildingToGravitas(GameObject template)
	{
		template.GetComponent<Deconstructable>().allowDeconstruction = false;
		template.AddOrGet<Demolishable>();
	}
}
