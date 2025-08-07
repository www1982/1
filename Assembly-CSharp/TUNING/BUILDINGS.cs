using System;
using System.Collections.Generic;

namespace TUNING
{
	// Token: 0x02000F7E RID: 3966
	public class BUILDINGS
	{
		// Token: 0x04005B6A RID: 23402
		public const float DEFAULT_STORAGE_CAPACITY = 2000f;

		// Token: 0x04005B6B RID: 23403
		public const float STANDARD_MANUAL_REFILL_LEVEL = 0.2f;

		// Token: 0x04005B6C RID: 23404
		public const float MASS_TEMPERATURE_SCALE = 0.2f;

		// Token: 0x04005B6D RID: 23405
		public const float AIRCONDITIONER_TEMPDELTA = -14f;

		// Token: 0x04005B6E RID: 23406
		public const float MAX_ENVIRONMENT_DELTA = -50f;

		// Token: 0x04005B6F RID: 23407
		public const float COMPOST_FLIP_TIME = 20f;

		// Token: 0x04005B70 RID: 23408
		public const int TUBE_LAUNCHER_MAX_CHARGES = 3;

		// Token: 0x04005B71 RID: 23409
		public const float TUBE_LAUNCHER_RECHARGE_TIME = 10f;

		// Token: 0x04005B72 RID: 23410
		public const float TUBE_LAUNCHER_WORK_TIME = 1f;

		// Token: 0x04005B73 RID: 23411
		public const float SMELTER_INGOT_INPUTKG = 500f;

		// Token: 0x04005B74 RID: 23412
		public const float SMELTER_INGOT_OUTPUTKG = 100f;

		// Token: 0x04005B75 RID: 23413
		public const float SMELTER_FABRICATIONTIME = 120f;

		// Token: 0x04005B76 RID: 23414
		public const float GEOREFINERY_SLAB_INPUTKG = 1000f;

		// Token: 0x04005B77 RID: 23415
		public const float GEOREFINERY_SLAB_OUTPUTKG = 200f;

		// Token: 0x04005B78 RID: 23416
		public const float GEOREFINERY_FABRICATIONTIME = 120f;

		// Token: 0x04005B79 RID: 23417
		public const float MASS_BURN_RATE_HYDROGENGENERATOR = 0.1f;

		// Token: 0x04005B7A RID: 23418
		public const float COOKER_FOOD_TEMPERATURE = 368.15f;

		// Token: 0x04005B7B RID: 23419
		public const float OVERHEAT_DAMAGE_INTERVAL = 7.5f;

		// Token: 0x04005B7C RID: 23420
		public const float MIN_BUILD_TEMPERATURE = 0f;

		// Token: 0x04005B7D RID: 23421
		public const float MAX_BUILD_TEMPERATURE = 318.15f;

		// Token: 0x04005B7E RID: 23422
		public const float MELTDOWN_TEMPERATURE = 533.15f;

		// Token: 0x04005B7F RID: 23423
		public const float REPAIR_FORCE_TEMPERATURE = 293.15f;

		// Token: 0x04005B80 RID: 23424
		public const int REPAIR_EFFECTIVENESS_BASE = 10;

		// Token: 0x04005B81 RID: 23425
		public static Dictionary<string, string> PLANSUBCATEGORYSORTING = new Dictionary<string, string>
		{
			{ "Ladder", "ladders" },
			{ "FirePole", "ladders" },
			{ "LadderFast", "ladders" },
			{ "Tile", "tiles" },
			{ "SnowTile", "tiles" },
			{ "WoodTile", "tiles" },
			{ "GasPermeableMembrane", "tiles" },
			{ "MeshTile", "tiles" },
			{ "InsulationTile", "tiles" },
			{ "PlasticTile", "tiles" },
			{ "MetalTile", "tiles" },
			{ "GlassTile", "tiles" },
			{ "StorageTile", "tiles" },
			{ "BunkerTile", "tiles" },
			{ "ExteriorWall", "tiles" },
			{ "CarpetTile", "tiles" },
			{ "ExobaseHeadquarters", "printingpods" },
			{ "Door", "doors" },
			{ "ManualPressureDoor", "doors" },
			{ "PressureDoor", "doors" },
			{ "BunkerDoor", "doors" },
			{ "StorageLocker", "storage" },
			{ "StorageLockerSmart", "storage" },
			{ "LiquidReservoir", "storage" },
			{ "GasReservoir", "storage" },
			{ "ObjectDispenser", "storage" },
			{ "TravelTube", "transport" },
			{ "TravelTubeEntrance", "transport" },
			{ "TravelTubeWallBridge", "transport" },
			{
				RemoteWorkerDockConfig.ID,
				"operations"
			},
			{
				RemoteWorkTerminalConfig.ID,
				"operations"
			},
			{ "MineralDeoxidizer", "producers" },
			{ "SublimationStation", "producers" },
			{ "Oxysconce", "producers" },
			{ "Electrolyzer", "producers" },
			{ "RustDeoxidizer", "producers" },
			{ "AirFilter", "scrubbers" },
			{ "CO2Scrubber", "scrubbers" },
			{ "AlgaeHabitat", "scrubbers" },
			{ "DevGenerator", "generators" },
			{ "ManualGenerator", "generators" },
			{ "Generator", "generators" },
			{ "WoodGasGenerator", "generators" },
			{ "PeatGenerator", "generators" },
			{ "HydrogenGenerator", "generators" },
			{ "MethaneGenerator", "generators" },
			{ "PetroleumGenerator", "generators" },
			{ "SteamTurbine", "generators" },
			{ "SteamTurbine2", "generators" },
			{ "SolarPanel", "generators" },
			{ "Wire", "wires" },
			{ "WireBridge", "wires" },
			{ "HighWattageWire", "wires" },
			{ "WireBridgeHighWattage", "wires" },
			{ "WireRefined", "wires" },
			{ "WireRefinedBridge", "wires" },
			{ "WireRefinedHighWattage", "wires" },
			{ "WireRefinedBridgeHighWattage", "wires" },
			{ "Battery", "batteries" },
			{ "BatteryMedium", "batteries" },
			{ "BatterySmart", "batteries" },
			{ "ElectrobankCharger", "electrobankbuildings" },
			{ "SmallElectrobankDischarger", "electrobankbuildings" },
			{ "LargeElectrobankDischarger", "electrobankbuildings" },
			{ "PowerTransformerSmall", "powercontrol" },
			{ "PowerTransformer", "powercontrol" },
			{
				SwitchConfig.ID,
				"switches"
			},
			{
				LogicPowerRelayConfig.ID,
				"switches"
			},
			{
				TemperatureControlledSwitchConfig.ID,
				"switches"
			},
			{
				PressureSwitchLiquidConfig.ID,
				"switches"
			},
			{
				PressureSwitchGasConfig.ID,
				"switches"
			},
			{ "MicrobeMusher", "cooking" },
			{ "CookingStation", "cooking" },
			{ "Deepfryer", "cooking" },
			{ "GourmetCookingStation", "cooking" },
			{ "SpiceGrinder", "cooking" },
			{ "FoodDehydrator", "cooking" },
			{ "FoodRehydrator", "cooking" },
			{ "Smoker", "cooking" },
			{ "PlanterBox", "farming" },
			{ "FarmTile", "farming" },
			{ "HydroponicFarm", "farming" },
			{ "RationBox", "storage" },
			{ "Refrigerator", "storage" },
			{ "CreatureDeliveryPoint", "ranching" },
			{ "CritterDropOff", "ranching" },
			{ "CritterPickUp", "ranching" },
			{ "FishDeliveryPoint", "ranching" },
			{ "CreatureFeeder", "ranching" },
			{ "FishFeeder", "ranching" },
			{ "MilkFeeder", "ranching" },
			{ "EggIncubator", "ranching" },
			{ "EggCracker", "ranching" },
			{ "CreatureGroundTrap", "ranching" },
			{ "CreatureAirTrap", "ranching" },
			{ "WaterTrap", "ranching" },
			{ "CritterCondo", "ranching" },
			{ "UnderwaterCritterCondo", "ranching" },
			{ "AirBorneCritterCondo", "ranching" },
			{ "Outhouse", "washroom" },
			{ "FlushToilet", "washroom" },
			{ "WallToilet", "washroom" },
			{
				ShowerConfig.ID,
				"washroom"
			},
			{ "GunkEmptier", "washroom" },
			{ "LiquidConduit", "pipes" },
			{ "InsulatedLiquidConduit", "pipes" },
			{ "LiquidConduitRadiant", "pipes" },
			{ "LiquidConduitBridge", "pipes" },
			{ "ContactConductivePipeBridge", "pipes" },
			{ "LiquidVent", "pipes" },
			{ "LiquidPump", "pumps" },
			{ "LiquidMiniPump", "pumps" },
			{ "LiquidPumpingStation", "pumps" },
			{ "DevPumpLiquid", "pumps" },
			{ "BottleEmptier", "valves" },
			{ "LiquidFilter", "valves" },
			{ "LiquidConduitPreferentialFlow", "valves" },
			{ "LiquidConduitOverflow", "valves" },
			{ "LiquidValve", "valves" },
			{ "LiquidLogicValve", "valves" },
			{ "LiquidLimitValve", "valves" },
			{ "LiquidBottler", "valves" },
			{ "BottleEmptierConduitLiquid", "valves" },
			{
				LiquidConduitElementSensorConfig.ID,
				"sensors"
			},
			{
				LiquidConduitDiseaseSensorConfig.ID,
				"sensors"
			},
			{
				LiquidConduitTemperatureSensorConfig.ID,
				"sensors"
			},
			{ "ModularLaunchpadPortLiquid", "buildmenuports" },
			{ "ModularLaunchpadPortLiquidUnloader", "buildmenuports" },
			{ "GasConduit", "pipes" },
			{ "InsulatedGasConduit", "pipes" },
			{ "GasConduitRadiant", "pipes" },
			{ "GasConduitBridge", "pipes" },
			{ "GasVent", "pipes" },
			{ "GasVentHighPressure", "pipes" },
			{ "GasPump", "pumps" },
			{ "GasMiniPump", "pumps" },
			{ "DevPumpGas", "pumps" },
			{ "GasBottler", "valves" },
			{ "BottleEmptierGas", "valves" },
			{ "BottleEmptierConduitGas", "valves" },
			{ "GasFilter", "valves" },
			{ "GasConduitPreferentialFlow", "valves" },
			{ "GasConduitOverflow", "valves" },
			{ "GasValve", "valves" },
			{ "GasLogicValve", "valves" },
			{ "GasLimitValve", "valves" },
			{
				GasConduitElementSensorConfig.ID,
				"sensors"
			},
			{
				GasConduitDiseaseSensorConfig.ID,
				"sensors"
			},
			{
				GasConduitTemperatureSensorConfig.ID,
				"sensors"
			},
			{ "ModularLaunchpadPortGas", "buildmenuports" },
			{ "ModularLaunchpadPortGasUnloader", "buildmenuports" },
			{ "Compost", "organic" },
			{ "FertilizerMaker", "organic" },
			{ "AlgaeDistillery", "organic" },
			{ "EthanolDistillery", "organic" },
			{ "SludgePress", "organic" },
			{ "MilkFatSeparator", "organic" },
			{ "MilkPress", "organic" },
			{ "IceKettle", "materials" },
			{ "WaterPurifier", "materials" },
			{ "Desalinator", "materials" },
			{ "RockCrusher", "materials" },
			{ "Kiln", "materials" },
			{ "MetalRefinery", "materials" },
			{ "GlassForge", "materials" },
			{ "OilRefinery", "oil" },
			{ "Polymerizer", "oil" },
			{ "OxyliteRefinery", "advanced" },
			{ "ChemicalRefinery", "advanced" },
			{ "SupermaterialRefinery", "advanced" },
			{ "DiamondPress", "advanced" },
			{ "Chlorinator", "advanced" },
			{ "WashBasin", "hygiene" },
			{ "WashSink", "hygiene" },
			{ "HandSanitizer", "hygiene" },
			{ "DecontaminationShower", "hygiene" },
			{ "Apothecary", "medical" },
			{ "DoctorStation", "medical" },
			{ "AdvancedDoctorStation", "medical" },
			{ "MedicalCot", "medical" },
			{ "DevLifeSupport", "medical" },
			{ "MassageTable", "wellness" },
			{ "Grave", "wellness" },
			{ "OilChanger", "wellness" },
			{ "Bed", "beds" },
			{ "LuxuryBed", "beds" },
			{
				LadderBedConfig.ID,
				"beds"
			},
			{ "FloorLamp", "lights" },
			{ "CeilingLight", "lights" },
			{ "SunLamp", "lights" },
			{ "DevLightGenerator", "lights" },
			{ "MercuryCeilingLight", "lights" },
			{ "DiningTable", "dining" },
			{ "WaterCooler", "recreation" },
			{ "Phonobox", "recreation" },
			{ "ArcadeMachine", "recreation" },
			{ "EspressoMachine", "recreation" },
			{ "HotTub", "recreation" },
			{ "MechanicalSurfboard", "recreation" },
			{ "Sauna", "recreation" },
			{ "Juicer", "recreation" },
			{ "SodaFountain", "recreation" },
			{ "BeachChair", "recreation" },
			{ "VerticalWindTunnel", "recreation" },
			{ "Telephone", "recreation" },
			{ "FlowerVase", "decor" },
			{ "FlowerVaseWall", "decor" },
			{ "FlowerVaseHanging", "decor" },
			{ "FlowerVaseHangingFancy", "decor" },
			{
				PixelPackConfig.ID,
				"decor"
			},
			{ "SmallSculpture", "decor" },
			{ "Sculpture", "decor" },
			{ "IceSculpture", "decor" },
			{ "MarbleSculpture", "decor" },
			{ "MetalSculpture", "decor" },
			{ "WoodSculpture", "decor" },
			{ "FossilSculpture", "decor" },
			{ "CeilingFossilSculpture", "decor" },
			{ "CrownMoulding", "decor" },
			{ "CornerMoulding", "decor" },
			{ "Canvas", "decor" },
			{ "CanvasWide", "decor" },
			{ "CanvasTall", "decor" },
			{ "ItemPedestal", "decor" },
			{ "ParkSign", "decor" },
			{ "MonumentBottom", "decor" },
			{ "MonumentMiddle", "decor" },
			{ "MonumentTop", "decor" },
			{ "ResearchCenter", "research" },
			{ "AdvancedResearchCenter", "research" },
			{ "GeoTuner", "research" },
			{ "NuclearResearchCenter", "research" },
			{ "OrbitalResearchCenter", "research" },
			{ "CosmicResearchCenter", "research" },
			{ "DLC1CosmicResearchCenter", "research" },
			{ "DataMiner", "research" },
			{ "ArtifactAnalysisStation", "archaeology" },
			{ "MissileFabricator", "meteordefense" },
			{ "AstronautTrainingCenter", "exploration" },
			{ "PowerControlStation", "industrialstation" },
			{ "ResetSkillsStation", "industrialstation" },
			{ "RoleStation", "workstations" },
			{ "RanchStation", "ranching" },
			{ "ShearingStation", "ranching" },
			{ "MilkingStation", "ranching" },
			{ "FarmStation", "farming" },
			{ "GeneticAnalysisStation", "farming" },
			{ "CraftingTable", "manufacturing" },
			{ "AdvancedCraftingTable", "manufacturing" },
			{ "ClothingFabricator", "manufacturing" },
			{ "ClothingAlterationStation", "manufacturing" },
			{ "SuitFabricator", "manufacturing" },
			{ "OxygenMaskMarker", "equipment" },
			{ "OxygenMaskLocker", "equipment" },
			{ "SuitMarker", "equipment" },
			{ "SuitLocker", "equipment" },
			{ "JetSuitMarker", "equipment" },
			{ "JetSuitLocker", "equipment" },
			{ "MissileLauncher", "missiles" },
			{ "LeadSuitMarker", "equipment" },
			{ "LeadSuitLocker", "equipment" },
			{ "Campfire", "temperature" },
			{ "DevHeater", "temperature" },
			{ "SpaceHeater", "temperature" },
			{ "LiquidHeater", "temperature" },
			{ "LiquidConditioner", "temperature" },
			{ "LiquidCooledFan", "temperature" },
			{ "IceCooledFan", "temperature" },
			{ "IceMachine", "temperature" },
			{ "AirConditioner", "temperature" },
			{ "ThermalBlock", "temperature" },
			{ "OreScrubber", "sanitation" },
			{ "OilWellCap", "oil" },
			{ "SweepBotStation", "sanitation" },
			{ "LogicWire", "wires" },
			{ "LogicWireBridge", "wires" },
			{ "LogicRibbon", "wires" },
			{ "LogicRibbonBridge", "wires" },
			{
				LogicRibbonReaderConfig.ID,
				"wires"
			},
			{
				LogicRibbonWriterConfig.ID,
				"wires"
			},
			{ "LogicDuplicantSensor", "sensors" },
			{
				LogicPressureSensorGasConfig.ID,
				"sensors"
			},
			{
				LogicPressureSensorLiquidConfig.ID,
				"sensors"
			},
			{
				LogicTemperatureSensorConfig.ID,
				"sensors"
			},
			{
				LogicLightSensorConfig.ID,
				"sensors"
			},
			{
				LogicWattageSensorConfig.ID,
				"sensors"
			},
			{
				LogicTimeOfDaySensorConfig.ID,
				"sensors"
			},
			{
				LogicTimerSensorConfig.ID,
				"sensors"
			},
			{
				LogicDiseaseSensorConfig.ID,
				"sensors"
			},
			{
				LogicElementSensorGasConfig.ID,
				"sensors"
			},
			{
				LogicElementSensorLiquidConfig.ID,
				"sensors"
			},
			{
				LogicCritterCountSensorConfig.ID,
				"sensors"
			},
			{
				LogicRadiationSensorConfig.ID,
				"sensors"
			},
			{
				LogicHEPSensorConfig.ID,
				"sensors"
			},
			{
				CometDetectorConfig.ID,
				"sensors"
			},
			{
				LogicCounterConfig.ID,
				"logicmanager"
			},
			{ "Checkpoint", "logicmanager" },
			{
				LogicAlarmConfig.ID,
				"logicmanager"
			},
			{
				LogicHammerConfig.ID,
				"logicaudio"
			},
			{
				LogicSwitchConfig.ID,
				"switches"
			},
			{ "FloorSwitch", "switches" },
			{ "LogicGateNOT", "logicgates" },
			{ "LogicGateAND", "logicgates" },
			{ "LogicGateOR", "logicgates" },
			{ "LogicGateBUFFER", "logicgates" },
			{ "LogicGateFILTER", "logicgates" },
			{ "LogicGateXOR", "logicgates" },
			{
				LogicMemoryConfig.ID,
				"logicgates"
			},
			{ "LogicGateMultiplexer", "logicgates" },
			{ "LogicGateDemultiplexer", "logicgates" },
			{ "LogicInterasteroidSender", "transmissions" },
			{ "LogicInterasteroidReceiver", "transmissions" },
			{ "SolidConduit", "conveyancestructures" },
			{ "SolidConduitBridge", "conveyancestructures" },
			{ "SolidConduitInbox", "conveyancestructures" },
			{ "SolidConduitOutbox", "conveyancestructures" },
			{ "SolidFilter", "conveyancestructures" },
			{ "SolidVent", "conveyancestructures" },
			{ "DevPumpSolid", "pumps" },
			{ "SolidLogicValve", "valves" },
			{ "SolidLimitValve", "valves" },
			{
				SolidConduitDiseaseSensorConfig.ID,
				"sensors"
			},
			{
				SolidConduitElementSensorConfig.ID,
				"sensors"
			},
			{
				SolidConduitTemperatureSensorConfig.ID,
				"sensors"
			},
			{ "AutoMiner", "automated" },
			{ "SolidTransferArm", "automated" },
			{ "ModularLaunchpadPortSolid", "buildmenuports" },
			{ "ModularLaunchpadPortSolidUnloader", "buildmenuports" },
			{ "Telescope", "telescopes" },
			{ "ClusterTelescope", "telescopes" },
			{ "ClusterTelescopeEnclosed", "telescopes" },
			{ "LaunchPad", "rocketstructures" },
			{ "Gantry", "rocketstructures" },
			{ "ModularLaunchpadPortBridge", "rocketstructures" },
			{ "RailGun", "fittings" },
			{ "RailGunPayloadOpener", "fittings" },
			{ "LandingBeacon", "rocketnav" },
			{ "SteamEngine", "engines" },
			{ "KeroseneEngine", "engines" },
			{ "HydrogenEngine", "engines" },
			{ "SolidBooster", "engines" },
			{ "LiquidFuelTank", "tanks" },
			{ "OxidizerTank", "tanks" },
			{ "OxidizerTankLiquid", "tanks" },
			{ "CargoBay", "cargo" },
			{ "GasCargoBay", "cargo" },
			{ "LiquidCargoBay", "cargo" },
			{ "SpecialCargoBay", "cargo" },
			{ "CommandModule", "rocketnav" },
			{
				RocketControlStationConfig.ID,
				"rocketnav"
			},
			{
				LogicClusterLocationSensorConfig.ID,
				"rocketnav"
			},
			{ "MissionControl", "rocketnav" },
			{ "MissionControlCluster", "rocketnav" },
			{ "RoboPilotCommandModule", "rocketnav" },
			{ "TouristModule", "module" },
			{ "ResearchModule", "module" },
			{ "RocketInteriorPowerPlug", "fittings" },
			{ "RocketInteriorLiquidInput", "fittings" },
			{ "RocketInteriorLiquidOutput", "fittings" },
			{ "RocketInteriorGasInput", "fittings" },
			{ "RocketInteriorGasOutput", "fittings" },
			{ "RocketInteriorSolidInput", "fittings" },
			{ "RocketInteriorSolidOutput", "fittings" },
			{ "ManualHighEnergyParticleSpawner", "producers" },
			{ "HighEnergyParticleSpawner", "producers" },
			{ "DevHEPSpawner", "producers" },
			{ "HighEnergyParticleRedirector", "transmissions" },
			{ "HEPBattery", "batteries" },
			{ "HEPBridgeTile", "transmissions" },
			{ "NuclearReactor", "producers" },
			{ "UraniumCentrifuge", "producers" },
			{ "RadiationLight", "producers" },
			{ "DevRadiationGenerator", "producers" }
		};

		// Token: 0x04005B82 RID: 23426
		public static List<PlanScreen.PlanInfo> PLANORDER = new List<PlanScreen.PlanInfo>
		{
			new PlanScreen.PlanInfo(new HashedString("Base"), false, new List<string>
			{
				"Ladder", "FirePole", "LadderFast", "Tile", "SnowTile", "WoodTile", "GasPermeableMembrane", "MeshTile", "InsulationTile", "PlasticTile",
				"MetalTile", "GlassTile", "StorageTile", "BunkerTile", "CarpetTile", "ExteriorWall", "ExobaseHeadquarters", "Door", "ManualPressureDoor", "PressureDoor",
				"BunkerDoor", "StorageLocker", "StorageLockerSmart", "LiquidReservoir", "GasReservoir", "ObjectDispenser", "TravelTube", "TravelTubeEntrance", "TravelTubeWallBridge"
			}, null, null),
			new PlanScreen.PlanInfo(new HashedString("Oxygen"), false, new List<string> { "MineralDeoxidizer", "SublimationStation", "Oxysconce", "AlgaeHabitat", "AirFilter", "CO2Scrubber", "Electrolyzer", "RustDeoxidizer" }, null, null),
			new PlanScreen.PlanInfo(new HashedString("Power"), false, new List<string>
			{
				"DevGenerator",
				"ManualGenerator",
				"Generator",
				"WoodGasGenerator",
				"PeatGenerator",
				"HydrogenGenerator",
				"MethaneGenerator",
				"PetroleumGenerator",
				"SteamTurbine",
				"SteamTurbine2",
				"SolarPanel",
				"Wire",
				"WireBridge",
				"HighWattageWire",
				"WireBridgeHighWattage",
				"WireRefined",
				"WireRefinedBridge",
				"WireRefinedHighWattage",
				"WireRefinedBridgeHighWattage",
				"Battery",
				"BatteryMedium",
				"BatterySmart",
				"ElectrobankCharger",
				"SmallElectrobankDischarger",
				"LargeElectrobankDischarger",
				"PowerTransformerSmall",
				"PowerTransformer",
				SwitchConfig.ID,
				LogicPowerRelayConfig.ID,
				TemperatureControlledSwitchConfig.ID,
				PressureSwitchLiquidConfig.ID,
				PressureSwitchGasConfig.ID
			}, null, null),
			new PlanScreen.PlanInfo(new HashedString("Food"), false, new List<string>
			{
				"MicrobeMusher", "CookingStation", "Deepfryer", "GourmetCookingStation", "SpiceGrinder", "FoodDehydrator", "FoodRehydrator", "Smoker", "PlanterBox", "FarmTile",
				"HydroponicFarm", "RationBox", "Refrigerator", "CreatureDeliveryPoint", "CritterPickUp", "CritterDropOff", "FishDeliveryPoint", "CreatureFeeder", "FishFeeder", "MilkFeeder",
				"EggIncubator", "EggCracker", "CreatureGroundTrap", "WaterTrap", "CreatureAirTrap", "CritterCondo", "UnderwaterCritterCondo", "AirBorneCritterCondo"
			}, null, null),
			new PlanScreen.PlanInfo(new HashedString("Plumbing"), false, new List<string>
			{
				"DevPumpLiquid",
				"Outhouse",
				"FlushToilet",
				"WallToilet",
				ShowerConfig.ID,
				"GunkEmptier",
				"LiquidPumpingStation",
				"BottleEmptier",
				"BottleEmptierConduitLiquid",
				"LiquidBottler",
				"LiquidConduit",
				"InsulatedLiquidConduit",
				"LiquidConduitRadiant",
				"LiquidConduitBridge",
				"LiquidConduitPreferentialFlow",
				"LiquidConduitOverflow",
				"LiquidPump",
				"LiquidMiniPump",
				"LiquidVent",
				"LiquidFilter",
				"LiquidValve",
				"LiquidLogicValve",
				"LiquidLimitValve",
				LiquidConduitElementSensorConfig.ID,
				LiquidConduitDiseaseSensorConfig.ID,
				LiquidConduitTemperatureSensorConfig.ID,
				"ModularLaunchpadPortLiquid",
				"ModularLaunchpadPortLiquidUnloader",
				"ContactConductivePipeBridge"
			}, null, null),
			new PlanScreen.PlanInfo(new HashedString("HVAC"), false, new List<string>
			{
				"DevPumpGas",
				"GasConduit",
				"InsulatedGasConduit",
				"GasConduitRadiant",
				"GasConduitBridge",
				"GasConduitPreferentialFlow",
				"GasConduitOverflow",
				"GasPump",
				"GasMiniPump",
				"GasVent",
				"GasVentHighPressure",
				"GasFilter",
				"GasValve",
				"GasLogicValve",
				"GasLimitValve",
				"GasBottler",
				"BottleEmptierGas",
				"BottleEmptierConduitGas",
				"ModularLaunchpadPortGas",
				"ModularLaunchpadPortGasUnloader",
				GasConduitElementSensorConfig.ID,
				GasConduitDiseaseSensorConfig.ID,
				GasConduitTemperatureSensorConfig.ID
			}, null, null),
			new PlanScreen.PlanInfo(new HashedString("Refining"), false, new List<string>
			{
				"Compost", "WaterPurifier", "Desalinator", "FertilizerMaker", "AlgaeDistillery", "EthanolDistillery", "RockCrusher", "Kiln", "SludgePress", "MetalRefinery",
				"GlassForge", "OilRefinery", "Polymerizer", "OxyliteRefinery", "Chlorinator", "ChemicalRefinery", "SupermaterialRefinery", "DiamondPress", "MilkFatSeparator", "MilkPress"
			}, null, null),
			new PlanScreen.PlanInfo(new HashedString("Medical"), false, new List<string>
			{
				"DevLifeSupport", "WashBasin", "WashSink", "HandSanitizer", "DecontaminationShower", "OilChanger", "Apothecary", "DoctorStation", "AdvancedDoctorStation", "MedicalCot",
				"MassageTable", "Grave"
			}, null, null),
			new PlanScreen.PlanInfo(new HashedString("Furniture"), false, new List<string>
			{
				"Bed",
				"LuxuryBed",
				LadderBedConfig.ID,
				"FloorLamp",
				"CeilingLight",
				"SunLamp",
				"DevLightGenerator",
				"MercuryCeilingLight",
				"DiningTable",
				"WaterCooler",
				"Phonobox",
				"ArcadeMachine",
				"EspressoMachine",
				"HotTub",
				"MechanicalSurfboard",
				"Sauna",
				"Juicer",
				"SodaFountain",
				"BeachChair",
				"VerticalWindTunnel",
				PixelPackConfig.ID,
				"Telephone",
				"FlowerVase",
				"FlowerVaseWall",
				"FlowerVaseHanging",
				"FlowerVaseHangingFancy",
				"SmallSculpture",
				"Sculpture",
				"IceSculpture",
				"WoodSculpture",
				"MarbleSculpture",
				"MetalSculpture",
				"FossilSculpture",
				"CeilingFossilSculpture",
				"CrownMoulding",
				"CornerMoulding",
				"Canvas",
				"CanvasWide",
				"CanvasTall",
				"ItemPedestal",
				"MonumentBottom",
				"MonumentMiddle",
				"MonumentTop",
				"ParkSign"
			}, null, null),
			new PlanScreen.PlanInfo(new HashedString("Equipment"), false, new List<string>
			{
				"ResearchCenter",
				"AdvancedResearchCenter",
				"NuclearResearchCenter",
				"OrbitalResearchCenter",
				"CosmicResearchCenter",
				"DLC1CosmicResearchCenter",
				"Telescope",
				"GeoTuner",
				"DataMiner",
				"PowerControlStation",
				"FarmStation",
				"GeneticAnalysisStation",
				"RanchStation",
				"ShearingStation",
				"MilkingStation",
				"RoleStation",
				"ResetSkillsStation",
				"ArtifactAnalysisStation",
				RemoteWorkerDockConfig.ID,
				RemoteWorkTerminalConfig.ID,
				"MissileFabricator",
				"CraftingTable",
				"AdvancedCraftingTable",
				"ClothingFabricator",
				"ClothingAlterationStation",
				"SuitFabricator",
				"OxygenMaskMarker",
				"OxygenMaskLocker",
				"SuitMarker",
				"SuitLocker",
				"JetSuitMarker",
				"JetSuitLocker",
				"LeadSuitMarker",
				"LeadSuitLocker",
				"AstronautTrainingCenter"
			}, null, null),
			new PlanScreen.PlanInfo(new HashedString("Utilities"), true, new List<string>
			{
				"Campfire", "DevHeater", "IceKettle", "SpaceHeater", "LiquidHeater", "LiquidCooledFan", "IceCooledFan", "IceMachine", "AirConditioner", "LiquidConditioner",
				"OreScrubber", "OilWellCap", "ThermalBlock", "SweepBotStation"
			}, null, null),
			new PlanScreen.PlanInfo(new HashedString("Automation"), true, new List<string>
			{
				"LogicWire",
				"LogicWireBridge",
				"LogicRibbon",
				"LogicRibbonBridge",
				LogicSwitchConfig.ID,
				"LogicDuplicantSensor",
				LogicPressureSensorGasConfig.ID,
				LogicPressureSensorLiquidConfig.ID,
				LogicTemperatureSensorConfig.ID,
				LogicLightSensorConfig.ID,
				LogicWattageSensorConfig.ID,
				LogicTimeOfDaySensorConfig.ID,
				LogicTimerSensorConfig.ID,
				LogicDiseaseSensorConfig.ID,
				LogicElementSensorGasConfig.ID,
				LogicElementSensorLiquidConfig.ID,
				LogicCritterCountSensorConfig.ID,
				LogicRadiationSensorConfig.ID,
				LogicHEPSensorConfig.ID,
				LogicCounterConfig.ID,
				LogicAlarmConfig.ID,
				LogicHammerConfig.ID,
				"LogicInterasteroidSender",
				"LogicInterasteroidReceiver",
				LogicRibbonReaderConfig.ID,
				LogicRibbonWriterConfig.ID,
				"FloorSwitch",
				"Checkpoint",
				CometDetectorConfig.ID,
				"LogicGateNOT",
				"LogicGateAND",
				"LogicGateOR",
				"LogicGateBUFFER",
				"LogicGateFILTER",
				"LogicGateXOR",
				LogicMemoryConfig.ID,
				"LogicGateMultiplexer",
				"LogicGateDemultiplexer"
			}, null, null),
			new PlanScreen.PlanInfo(new HashedString("Conveyance"), true, new List<string>
			{
				"DevPumpSolid",
				"SolidTransferArm",
				"SolidConduit",
				"SolidConduitBridge",
				"SolidConduitInbox",
				"SolidConduitOutbox",
				"SolidFilter",
				"SolidVent",
				"SolidLogicValve",
				"SolidLimitValve",
				SolidConduitDiseaseSensorConfig.ID,
				SolidConduitElementSensorConfig.ID,
				SolidConduitTemperatureSensorConfig.ID,
				"AutoMiner",
				"ModularLaunchpadPortSolid",
				"ModularLaunchpadPortSolidUnloader"
			}, null, null),
			new PlanScreen.PlanInfo(new HashedString("Rocketry"), true, new List<string>
			{
				"ClusterTelescope",
				"ClusterTelescopeEnclosed",
				"MissionControl",
				"MissionControlCluster",
				"LaunchPad",
				"Gantry",
				"SteamEngine",
				"KeroseneEngine",
				"SolidBooster",
				"LiquidFuelTank",
				"OxidizerTank",
				"OxidizerTankLiquid",
				"CargoBay",
				"GasCargoBay",
				"LiquidCargoBay",
				"CommandModule",
				"RoboPilotCommandModule",
				"TouristModule",
				"ResearchModule",
				"SpecialCargoBay",
				"HydrogenEngine",
				RocketControlStationConfig.ID,
				"RocketInteriorPowerPlug",
				"RocketInteriorLiquidInput",
				"RocketInteriorLiquidOutput",
				"RocketInteriorGasInput",
				"RocketInteriorGasOutput",
				"RocketInteriorSolidInput",
				"RocketInteriorSolidOutput",
				LogicClusterLocationSensorConfig.ID,
				"RailGun",
				"RailGunPayloadOpener",
				"LandingBeacon",
				"MissileLauncher",
				"ModularLaunchpadPortBridge"
			}, null, null),
			new PlanScreen.PlanInfo(new HashedString("HEP"), true, new List<string> { "RadiationLight", "ManualHighEnergyParticleSpawner", "NuclearReactor", "UraniumCentrifuge", "HighEnergyParticleSpawner", "DevHEPSpawner", "HighEnergyParticleRedirector", "HEPBattery", "HEPBridgeTile", "DevRadiationGenerator" }, DlcManager.EXPANSION1, null)
		};

		// Token: 0x04005B83 RID: 23427
		public static List<Type> COMPONENT_DESCRIPTION_ORDER = new List<Type>
		{
			typeof(BottleEmptier),
			typeof(CookingStation),
			typeof(GourmetCookingStation),
			typeof(RoleStation),
			typeof(ResearchCenter),
			typeof(NuclearResearchCenter),
			typeof(LiquidCooledFan),
			typeof(HandSanitizer),
			typeof(HandSanitizer.Work),
			typeof(PlantAirConditioner),
			typeof(Clinic),
			typeof(BuildingElementEmitter),
			typeof(ElementConverter),
			typeof(ElementConsumer),
			typeof(PassiveElementConsumer),
			typeof(TinkerStation),
			typeof(EnergyConsumer),
			typeof(AirConditioner),
			typeof(Storage),
			typeof(Battery),
			typeof(AirFilter),
			typeof(FlushToilet),
			typeof(Toilet),
			typeof(EnergyGenerator),
			typeof(MassageTable),
			typeof(Shower),
			typeof(Ownable),
			typeof(PlantablePlot),
			typeof(RelaxationPoint),
			typeof(BuildingComplete),
			typeof(Building),
			typeof(BuildingPreview),
			typeof(BuildingUnderConstruction),
			typeof(Crop),
			typeof(Growing),
			typeof(Equippable),
			typeof(ColdBreather),
			typeof(ResearchPointObject),
			typeof(SuitTank),
			typeof(IlluminationVulnerable),
			typeof(TemperatureVulnerable),
			typeof(ExternalTemperatureMonitor),
			typeof(CritterTemperatureMonitor),
			typeof(PressureVulnerable),
			typeof(SubmersionMonitor),
			typeof(BatterySmart),
			typeof(Compost),
			typeof(Refrigerator),
			typeof(Bed),
			typeof(OreScrubber),
			typeof(OreScrubber.Work),
			typeof(MinimumOperatingTemperature),
			typeof(RoomTracker),
			typeof(EnergyConsumerSelfSustaining),
			typeof(ArcadeMachine),
			typeof(Telescope),
			typeof(EspressoMachine),
			typeof(JetSuitTank),
			typeof(Phonobox),
			typeof(ArcadeMachine),
			typeof(BeachChair),
			typeof(Sauna),
			typeof(VerticalWindTunnel),
			typeof(HotTub),
			typeof(Juicer),
			typeof(SodaFountain),
			typeof(MechanicalSurfboard),
			typeof(BottleEmptier),
			typeof(AccessControl),
			typeof(GammaRayOven),
			typeof(Reactor),
			typeof(HighEnergyParticlePort),
			typeof(LeadSuitTank),
			typeof(ActiveParticleConsumer.Def),
			typeof(WaterCooler),
			typeof(Edible),
			typeof(PlantableSeed),
			typeof(SicknessTrigger),
			typeof(MedicinalPill),
			typeof(SeedProducer),
			typeof(Geyser),
			typeof(SpaceHeater),
			typeof(Overheatable),
			typeof(CreatureCalorieMonitor.Def),
			typeof(LureableMonitor.Def),
			typeof(FertilizationMonitor.Def),
			typeof(IrrigationMonitor.Def),
			typeof(ScaleGrowthMonitor.Def),
			typeof(TravelTubeEntrance.Work),
			typeof(ToiletWorkableUse),
			typeof(ReceptacleMonitor),
			typeof(Light2D),
			typeof(Ladder),
			typeof(SimCellOccupier),
			typeof(Vent),
			typeof(LogicPorts),
			typeof(Capturable),
			typeof(Trappable),
			typeof(SpaceArtifact),
			typeof(MessStation),
			typeof(PlantElementEmitter),
			typeof(Radiator),
			typeof(DecorProvider)
		};

		// Token: 0x0200211A RID: 8474
		public class PHARMACY
		{
			// Token: 0x02002912 RID: 10514
			public class FABRICATIONTIME
			{
				// Token: 0x0400B5AE RID: 46510
				public const float TIER0 = 50f;

				// Token: 0x0400B5AF RID: 46511
				public const float TIER1 = 100f;

				// Token: 0x0400B5B0 RID: 46512
				public const float TIER2 = 200f;
			}
		}

		// Token: 0x0200211B RID: 8475
		public class NUCLEAR_REACTOR
		{
			// Token: 0x02002913 RID: 10515
			public class REACTOR_MASSES
			{
				// Token: 0x0400B5B1 RID: 46513
				public const float MIN = 1f;

				// Token: 0x0400B5B2 RID: 46514
				public const float MAX = 10f;
			}
		}

		// Token: 0x0200211C RID: 8476
		public class OVERPRESSURE
		{
			// Token: 0x04009751 RID: 38737
			public const float TIER0 = 1.8f;
		}

		// Token: 0x0200211D RID: 8477
		public class OVERHEAT_TEMPERATURES
		{
			// Token: 0x04009752 RID: 38738
			public const float LOW_3 = 10f;

			// Token: 0x04009753 RID: 38739
			public const float LOW_2 = 328.15f;

			// Token: 0x04009754 RID: 38740
			public const float LOW_1 = 338.15f;

			// Token: 0x04009755 RID: 38741
			public const float NORMAL = 348.15f;

			// Token: 0x04009756 RID: 38742
			public const float HIGH_1 = 363.15f;

			// Token: 0x04009757 RID: 38743
			public const float HIGH_2 = 398.15f;

			// Token: 0x04009758 RID: 38744
			public const float HIGH_3 = 1273.15f;

			// Token: 0x04009759 RID: 38745
			public const float HIGH_4 = 2273.15f;
		}

		// Token: 0x0200211E RID: 8478
		public class OVERHEAT_MATERIAL_MOD
		{
			// Token: 0x0400975A RID: 38746
			public const float LOW_3 = -200f;

			// Token: 0x0400975B RID: 38747
			public const float LOW_2 = -20f;

			// Token: 0x0400975C RID: 38748
			public const float LOW_1 = -10f;

			// Token: 0x0400975D RID: 38749
			public const float NORMAL = 0f;

			// Token: 0x0400975E RID: 38750
			public const float HIGH_1 = 15f;

			// Token: 0x0400975F RID: 38751
			public const float HIGH_2 = 50f;

			// Token: 0x04009760 RID: 38752
			public const float HIGH_3 = 200f;

			// Token: 0x04009761 RID: 38753
			public const float HIGH_4 = 500f;

			// Token: 0x04009762 RID: 38754
			public const float HIGH_5 = 900f;
		}

		// Token: 0x0200211F RID: 8479
		public class DECOR_MATERIAL_MOD
		{
			// Token: 0x04009763 RID: 38755
			public const float NORMAL = 0f;

			// Token: 0x04009764 RID: 38756
			public const float HIGH_1 = 0.1f;

			// Token: 0x04009765 RID: 38757
			public const float HIGH_2 = 0.2f;

			// Token: 0x04009766 RID: 38758
			public const float HIGH_3 = 0.5f;

			// Token: 0x04009767 RID: 38759
			public const float HIGH_4 = 1f;
		}

		// Token: 0x02002120 RID: 8480
		public class CONSTRUCTION_MASS_KG
		{
			// Token: 0x04009768 RID: 38760
			public static readonly float[] TIER_TINY = new float[] { 5f };

			// Token: 0x04009769 RID: 38761
			public static readonly float[] TIER0 = new float[] { 25f };

			// Token: 0x0400976A RID: 38762
			public static readonly float[] TIER1 = new float[] { 50f };

			// Token: 0x0400976B RID: 38763
			public static readonly float[] TIER2 = new float[] { 100f };

			// Token: 0x0400976C RID: 38764
			public static readonly float[] TIER3 = new float[] { 200f };

			// Token: 0x0400976D RID: 38765
			public static readonly float[] TIER4 = new float[] { 400f };

			// Token: 0x0400976E RID: 38766
			public static readonly float[] TIER5 = new float[] { 800f };

			// Token: 0x0400976F RID: 38767
			public static readonly float[] TIER6 = new float[] { 1200f };

			// Token: 0x04009770 RID: 38768
			public static readonly float[] TIER7 = new float[] { 2000f };
		}

		// Token: 0x02002121 RID: 8481
		public class ROCKETRY_MASS_KG
		{
			// Token: 0x04009771 RID: 38769
			public static float[] COMMAND_MODULE_MASS = new float[] { 200f };

			// Token: 0x04009772 RID: 38770
			public static float[] CARGO_MASS = new float[] { 1000f };

			// Token: 0x04009773 RID: 38771
			public static float[] CARGO_MASS_SMALL = new float[] { 400f };

			// Token: 0x04009774 RID: 38772
			public static float[] FUEL_TANK_DRY_MASS = new float[] { 100f };

			// Token: 0x04009775 RID: 38773
			public static float[] FUEL_TANK_WET_MASS = new float[] { 900f };

			// Token: 0x04009776 RID: 38774
			public static float[] FUEL_TANK_WET_MASS_SMALL = new float[] { 300f };

			// Token: 0x04009777 RID: 38775
			public static float[] FUEL_TANK_WET_MASS_GAS = new float[] { 100f };

			// Token: 0x04009778 RID: 38776
			public static float[] FUEL_TANK_WET_MASS_GAS_LARGE = new float[] { 150f };

			// Token: 0x04009779 RID: 38777
			public static float[] OXIDIZER_TANK_OXIDIZER_MASS = new float[] { 900f };

			// Token: 0x0400977A RID: 38778
			public static float[] ENGINE_MASS_SMALL = new float[] { 200f };

			// Token: 0x0400977B RID: 38779
			public static float[] ENGINE_MASS_LARGE = new float[] { 500f };

			// Token: 0x0400977C RID: 38780
			public static float[] NOSE_CONE_TIER1 = new float[] { 200f, 100f };

			// Token: 0x0400977D RID: 38781
			public static float[] NOSE_CONE_TIER2 = new float[] { 400f, 200f };

			// Token: 0x0400977E RID: 38782
			public static float[] HOLLOW_TIER1 = new float[] { 200f };

			// Token: 0x0400977F RID: 38783
			public static float[] HOLLOW_TIER2 = new float[] { 400f };

			// Token: 0x04009780 RID: 38784
			public static float[] HOLLOW_TIER3 = new float[] { 800f };

			// Token: 0x04009781 RID: 38785
			public static float[] DENSE_TIER0 = new float[] { 200f };

			// Token: 0x04009782 RID: 38786
			public static float[] DENSE_TIER1 = new float[] { 500f };

			// Token: 0x04009783 RID: 38787
			public static float[] DENSE_TIER2 = new float[] { 1000f };

			// Token: 0x04009784 RID: 38788
			public static float[] DENSE_TIER3 = new float[] { 2000f };
		}

		// Token: 0x02002122 RID: 8482
		public class ENERGY_CONSUMPTION_WHEN_ACTIVE
		{
			// Token: 0x04009785 RID: 38789
			public const float TIER0 = 0f;

			// Token: 0x04009786 RID: 38790
			public const float TIER1 = 5f;

			// Token: 0x04009787 RID: 38791
			public const float TIER2 = 60f;

			// Token: 0x04009788 RID: 38792
			public const float TIER3 = 120f;

			// Token: 0x04009789 RID: 38793
			public const float TIER4 = 240f;

			// Token: 0x0400978A RID: 38794
			public const float TIER5 = 480f;

			// Token: 0x0400978B RID: 38795
			public const float TIER6 = 960f;

			// Token: 0x0400978C RID: 38796
			public const float TIER7 = 1200f;

			// Token: 0x0400978D RID: 38797
			public const float TIER8 = 1600f;
		}

		// Token: 0x02002123 RID: 8483
		public class EXHAUST_ENERGY_ACTIVE
		{
			// Token: 0x0400978E RID: 38798
			public const float TIER0 = 0f;

			// Token: 0x0400978F RID: 38799
			public const float TIER1 = 0.125f;

			// Token: 0x04009790 RID: 38800
			public const float TIER2 = 0.25f;

			// Token: 0x04009791 RID: 38801
			public const float TIER3 = 0.5f;

			// Token: 0x04009792 RID: 38802
			public const float TIER4 = 1f;

			// Token: 0x04009793 RID: 38803
			public const float TIER5 = 2f;

			// Token: 0x04009794 RID: 38804
			public const float TIER6 = 4f;

			// Token: 0x04009795 RID: 38805
			public const float TIER7 = 8f;

			// Token: 0x04009796 RID: 38806
			public const float TIER8 = 16f;
		}

		// Token: 0x02002124 RID: 8484
		public class JOULES_LEAK_PER_CYCLE
		{
			// Token: 0x04009797 RID: 38807
			public const float TIER0 = 400f;

			// Token: 0x04009798 RID: 38808
			public const float TIER1 = 1000f;

			// Token: 0x04009799 RID: 38809
			public const float TIER2 = 2000f;
		}

		// Token: 0x02002125 RID: 8485
		public class SELF_HEAT_KILOWATTS
		{
			// Token: 0x0400979A RID: 38810
			public const float TIER0 = 0f;

			// Token: 0x0400979B RID: 38811
			public const float TIER1 = 0.5f;

			// Token: 0x0400979C RID: 38812
			public const float TIER2 = 1f;

			// Token: 0x0400979D RID: 38813
			public const float TIER3 = 2f;

			// Token: 0x0400979E RID: 38814
			public const float TIER4 = 4f;

			// Token: 0x0400979F RID: 38815
			public const float TIER5 = 8f;

			// Token: 0x040097A0 RID: 38816
			public const float TIER6 = 16f;

			// Token: 0x040097A1 RID: 38817
			public const float TIER7 = 32f;

			// Token: 0x040097A2 RID: 38818
			public const float TIER8 = 64f;

			// Token: 0x040097A3 RID: 38819
			public const float TIER_NUCLEAR = 16384f;
		}

		// Token: 0x02002126 RID: 8486
		public class MELTING_POINT_KELVIN
		{
			// Token: 0x040097A4 RID: 38820
			public const float TIER0 = 800f;

			// Token: 0x040097A5 RID: 38821
			public const float TIER1 = 1600f;

			// Token: 0x040097A6 RID: 38822
			public const float TIER2 = 2400f;

			// Token: 0x040097A7 RID: 38823
			public const float TIER3 = 3200f;

			// Token: 0x040097A8 RID: 38824
			public const float TIER4 = 9999f;
		}

		// Token: 0x02002127 RID: 8487
		public class CONSTRUCTION_TIME_SECONDS
		{
			// Token: 0x040097A9 RID: 38825
			public const float TIER0 = 3f;

			// Token: 0x040097AA RID: 38826
			public const float TIER1 = 10f;

			// Token: 0x040097AB RID: 38827
			public const float TIER2 = 30f;

			// Token: 0x040097AC RID: 38828
			public const float TIER3 = 60f;

			// Token: 0x040097AD RID: 38829
			public const float TIER4 = 120f;

			// Token: 0x040097AE RID: 38830
			public const float TIER5 = 240f;

			// Token: 0x040097AF RID: 38831
			public const float TIER6 = 480f;
		}

		// Token: 0x02002128 RID: 8488
		public class HITPOINTS
		{
			// Token: 0x040097B0 RID: 38832
			public const int TIER0 = 10;

			// Token: 0x040097B1 RID: 38833
			public const int TIER1 = 30;

			// Token: 0x040097B2 RID: 38834
			public const int TIER2 = 100;

			// Token: 0x040097B3 RID: 38835
			public const int TIER3 = 250;

			// Token: 0x040097B4 RID: 38836
			public const int TIER4 = 1000;
		}

		// Token: 0x02002129 RID: 8489
		public class DAMAGE_SOURCES
		{
			// Token: 0x040097B5 RID: 38837
			public const int CONDUIT_CONTENTS_BOILED = 1;

			// Token: 0x040097B6 RID: 38838
			public const int CONDUIT_CONTENTS_FROZE = 1;

			// Token: 0x040097B7 RID: 38839
			public const int BAD_INPUT_ELEMENT = 1;

			// Token: 0x040097B8 RID: 38840
			public const int BUILDING_OVERHEATED = 1;

			// Token: 0x040097B9 RID: 38841
			public const int HIGH_LIQUID_PRESSURE = 10;

			// Token: 0x040097BA RID: 38842
			public const int MICROMETEORITE = 1;

			// Token: 0x040097BB RID: 38843
			public const int CORROSIVE_ELEMENT = 1;
		}

		// Token: 0x0200212A RID: 8490
		public class RELOCATION_TIME_SECONDS
		{
			// Token: 0x040097BC RID: 38844
			public const float DECONSTRUCT = 4f;

			// Token: 0x040097BD RID: 38845
			public const float CONSTRUCT = 4f;
		}

		// Token: 0x0200212B RID: 8491
		public class WORK_TIME_SECONDS
		{
			// Token: 0x040097BE RID: 38846
			public const float VERYSHORT_WORK_TIME = 5f;

			// Token: 0x040097BF RID: 38847
			public const float SHORT_WORK_TIME = 15f;

			// Token: 0x040097C0 RID: 38848
			public const float MEDIUM_WORK_TIME = 30f;

			// Token: 0x040097C1 RID: 38849
			public const float LONG_WORK_TIME = 90f;

			// Token: 0x040097C2 RID: 38850
			public const float VERY_LONG_WORK_TIME = 150f;

			// Token: 0x040097C3 RID: 38851
			public const float EXTENSIVE_WORK_TIME = 180f;
		}

		// Token: 0x0200212C RID: 8492
		public class FABRICATION_TIME_SECONDS
		{
			// Token: 0x040097C4 RID: 38852
			public const float VERY_SHORT = 20f;

			// Token: 0x040097C5 RID: 38853
			public const float SHORT = 40f;

			// Token: 0x040097C6 RID: 38854
			public const float MODERATE = 80f;

			// Token: 0x040097C7 RID: 38855
			public const float LONG = 250f;
		}

		// Token: 0x0200212D RID: 8493
		public class DECOR
		{
			// Token: 0x040097C8 RID: 38856
			public static readonly EffectorValues NONE = new EffectorValues
			{
				amount = 0,
				radius = 1
			};

			// Token: 0x02002914 RID: 10516
			public class BONUS
			{
				// Token: 0x0400B5B3 RID: 46515
				public static readonly EffectorValues TIER0 = new EffectorValues
				{
					amount = 5,
					radius = 1
				};

				// Token: 0x0400B5B4 RID: 46516
				public static readonly EffectorValues TIER1 = new EffectorValues
				{
					amount = 10,
					radius = 2
				};

				// Token: 0x0400B5B5 RID: 46517
				public static readonly EffectorValues TIER2 = new EffectorValues
				{
					amount = 15,
					radius = 3
				};

				// Token: 0x0400B5B6 RID: 46518
				public static readonly EffectorValues TIER3 = new EffectorValues
				{
					amount = 20,
					radius = 4
				};

				// Token: 0x0400B5B7 RID: 46519
				public static readonly EffectorValues TIER4 = new EffectorValues
				{
					amount = 25,
					radius = 5
				};

				// Token: 0x0400B5B8 RID: 46520
				public static readonly EffectorValues TIER5 = new EffectorValues
				{
					amount = 30,
					radius = 6
				};

				// Token: 0x02003893 RID: 14483
				public class MONUMENT
				{
					// Token: 0x0400E49E RID: 58526
					public static readonly EffectorValues COMPLETE = new EffectorValues
					{
						amount = 40,
						radius = 10
					};

					// Token: 0x0400E49F RID: 58527
					public static readonly EffectorValues INCOMPLETE = new EffectorValues
					{
						amount = 10,
						radius = 5
					};
				}
			}

			// Token: 0x02002915 RID: 10517
			public class PENALTY
			{
				// Token: 0x0400B5B9 RID: 46521
				public static readonly EffectorValues TIER0 = new EffectorValues
				{
					amount = -5,
					radius = 1
				};

				// Token: 0x0400B5BA RID: 46522
				public static readonly EffectorValues TIER1 = new EffectorValues
				{
					amount = -10,
					radius = 2
				};

				// Token: 0x0400B5BB RID: 46523
				public static readonly EffectorValues TIER2 = new EffectorValues
				{
					amount = -15,
					radius = 3
				};

				// Token: 0x0400B5BC RID: 46524
				public static readonly EffectorValues TIER3 = new EffectorValues
				{
					amount = -20,
					radius = 4
				};

				// Token: 0x0400B5BD RID: 46525
				public static readonly EffectorValues TIER4 = new EffectorValues
				{
					amount = -20,
					radius = 5
				};

				// Token: 0x0400B5BE RID: 46526
				public static readonly EffectorValues TIER5 = new EffectorValues
				{
					amount = -25,
					radius = 6
				};
			}
		}

		// Token: 0x0200212E RID: 8494
		public class MASS_KG
		{
			// Token: 0x040097C9 RID: 38857
			public const float TIER0 = 25f;

			// Token: 0x040097CA RID: 38858
			public const float TIER1 = 50f;

			// Token: 0x040097CB RID: 38859
			public const float TIER2 = 100f;

			// Token: 0x040097CC RID: 38860
			public const float TIER3 = 200f;

			// Token: 0x040097CD RID: 38861
			public const float TIER4 = 400f;

			// Token: 0x040097CE RID: 38862
			public const float TIER5 = 800f;

			// Token: 0x040097CF RID: 38863
			public const float TIER6 = 1200f;

			// Token: 0x040097D0 RID: 38864
			public const float TIER7 = 2000f;
		}

		// Token: 0x0200212F RID: 8495
		public class UPGRADES
		{
			// Token: 0x040097D1 RID: 38865
			public const float BUILDTIME_TIER0 = 120f;

			// Token: 0x02002916 RID: 10518
			public class MATERIALTAGS
			{
				// Token: 0x0400B5BF RID: 46527
				public const string METAL = "Metal";

				// Token: 0x0400B5C0 RID: 46528
				public const string REFINEDMETAL = "RefinedMetal";

				// Token: 0x0400B5C1 RID: 46529
				public const string CARBON = "Carbon";
			}

			// Token: 0x02002917 RID: 10519
			public class MATERIALMASS
			{
				// Token: 0x0400B5C2 RID: 46530
				public const int TIER0 = 100;

				// Token: 0x0400B5C3 RID: 46531
				public const int TIER1 = 200;

				// Token: 0x0400B5C4 RID: 46532
				public const int TIER2 = 400;

				// Token: 0x0400B5C5 RID: 46533
				public const int TIER3 = 500;
			}

			// Token: 0x02002918 RID: 10520
			public class MODIFIERAMOUNTS
			{
				// Token: 0x0400B5C6 RID: 46534
				public const float MANUALGENERATOR_ENERGYGENERATION = 1.2f;

				// Token: 0x0400B5C7 RID: 46535
				public const float MANUALGENERATOR_CAPACITY = 2f;

				// Token: 0x0400B5C8 RID: 46536
				public const float PROPANEGENERATOR_ENERGYGENERATION = 1.6f;

				// Token: 0x0400B5C9 RID: 46537
				public const float PROPANEGENERATOR_HEATGENERATION = 1.6f;

				// Token: 0x0400B5CA RID: 46538
				public const float GENERATOR_HEATGENERATION = 0.8f;

				// Token: 0x0400B5CB RID: 46539
				public const float GENERATOR_ENERGYGENERATION = 1.3f;

				// Token: 0x0400B5CC RID: 46540
				public const float TURBINE_ENERGYGENERATION = 1.2f;

				// Token: 0x0400B5CD RID: 46541
				public const float TURBINE_CAPACITY = 1.2f;

				// Token: 0x0400B5CE RID: 46542
				public const float SUITRECHARGER_EXECUTIONTIME = 1.2f;

				// Token: 0x0400B5CF RID: 46543
				public const float SUITRECHARGER_HEATGENERATION = 1.2f;

				// Token: 0x0400B5D0 RID: 46544
				public const float STORAGELOCKER_CAPACITY = 2f;

				// Token: 0x0400B5D1 RID: 46545
				public const float SOLARPANEL_ENERGYGENERATION = 1.2f;

				// Token: 0x0400B5D2 RID: 46546
				public const float SMELTER_HEATGENERATION = 0.7f;
			}
		}
	}
}
