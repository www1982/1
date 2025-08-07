using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200030C RID: 780
public class HarvestablePOIConfig : IMultiEntityConfig
{
	// Token: 0x0600100D RID: 4109 RVA: 0x0005FAF0 File Offset: 0x0005DCF0
	public List<GameObject> CreatePrefabs()
	{
		List<GameObject> list = new List<GameObject>();
		foreach (HarvestablePOIConfig.HarvestablePOIParams harvestablePOIParams in this.GenerateConfigs())
		{
			list.Add(HarvestablePOIConfig.CreateHarvestablePOI(harvestablePOIParams.id, harvestablePOIParams.anim, Strings.Get(harvestablePOIParams.nameStringKey), harvestablePOIParams.descStringKey, harvestablePOIParams.poiType.idHash, harvestablePOIParams.poiType.canProvideArtifacts, harvestablePOIParams.poiType.GetRequiredDlcIds(), harvestablePOIParams.poiType.GetForbiddenDlcIds()));
		}
		return list;
	}

	// Token: 0x0600100E RID: 4110 RVA: 0x0005FB9C File Offset: 0x0005DD9C
	public static GameObject CreateHarvestablePOI(string id, string anim, string name, StringKey descStringKey, HashedString poiType, bool canProvideArtifacts = false)
	{
		return HarvestablePOIConfig.CreateHarvestablePOI(id, anim, name, descStringKey, poiType, canProvideArtifacts, DlcManager.EXPANSION1, null);
	}

	// Token: 0x0600100F RID: 4111 RVA: 0x0005FBB4 File Offset: 0x0005DDB4
	public static GameObject CreateHarvestablePOI(string id, string anim, string name, StringKey descStringKey, HashedString poiType, bool canProvideArtifacts = false, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
	{
		GameObject gameObject = EntityTemplates.CreateEntity(id, id, true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.AddOrGet<HarvestablePOIConfigurator>().presetType = poiType;
		HarvestablePOIClusterGridEntity harvestablePOIClusterGridEntity = gameObject.AddOrGet<HarvestablePOIClusterGridEntity>();
		harvestablePOIClusterGridEntity.m_name = name;
		harvestablePOIClusterGridEntity.m_Anim = anim;
		gameObject.AddOrGetDef<HarvestablePOIStates.Def>();
		if (canProvideArtifacts)
		{
			gameObject.AddOrGetDef<ArtifactPOIStates.Def>();
			gameObject.AddOrGet<ArtifactPOIConfigurator>().presetType = ArtifactPOIConfigurator.defaultArtifactPoiType.idHash;
		}
		gameObject.AddOrGet<InfoDescription>().description = Strings.Get(descStringKey);
		KPrefabID component = gameObject.GetComponent<KPrefabID>();
		component.requiredDlcIds = requiredDlcIds;
		component.forbiddenDlcIds = forbiddenDlcIds;
		return gameObject;
	}

	// Token: 0x06001010 RID: 4112 RVA: 0x0005FC44 File Offset: 0x0005DE44
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06001011 RID: 4113 RVA: 0x0005FC46 File Offset: 0x0005DE46
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x06001012 RID: 4114 RVA: 0x0005FC48 File Offset: 0x0005DE48
	private List<HarvestablePOIConfig.HarvestablePOIParams> GenerateConfigs()
	{
		List<HarvestablePOIConfig.HarvestablePOIParams> list = new List<HarvestablePOIConfig.HarvestablePOIParams>();
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("cloud", new HarvestablePOIConfigurator.HarvestablePOIType("CarbonAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.RefinedCarbon,
				1.5f
			},
			{
				SimHashes.Carbon,
				5.5f
			}
		}, 30000f, 45000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("metallic_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("MetallicAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.MoltenIron,
				1.25f
			},
			{
				SimHashes.Cuprite,
				1.75f
			},
			{
				SimHashes.Obsidian,
				7f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("satellite_field", new HarvestablePOIConfigurator.HarvestablePOIType("SatelliteField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Sand,
				3f
			},
			{
				SimHashes.IronOre,
				3f
			},
			{
				SimHashes.MoltenCopper,
				2.67f
			},
			{
				SimHashes.Glass,
				1.33f
			}
		}, 30000f, 45000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("rocky_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("RockyAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Cuprite,
				2f
			},
			{
				SimHashes.SedimentaryRock,
				4f
			},
			{
				SimHashes.IgneousRock,
				4f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("interstellar_ice_field", new HarvestablePOIConfigurator.HarvestablePOIType("InterstellarIceField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Ice,
				2.5f
			},
			{
				SimHashes.SolidCarbonDioxide,
				7f
			},
			{
				SimHashes.SolidOxygen,
				0.5f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, new List<string>
		{
			Db.Get().OrbitalTypeCategories.iceCloud.Id,
			Db.Get().OrbitalTypeCategories.iceRock.Id
		}, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("organic_mass_field", new HarvestablePOIConfigurator.HarvestablePOIType("OrganicMassField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.SlimeMold,
				3f
			},
			{
				SimHashes.Algae,
				3f
			},
			{
				SimHashes.ContaminatedOxygen,
				1f
			},
			{
				SimHashes.Dirt,
				3f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("ice_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("IceAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Ice,
				6f
			},
			{
				SimHashes.SolidCarbonDioxide,
				2f
			},
			{
				SimHashes.Oxygen,
				1.5f
			},
			{
				SimHashes.SolidMethane,
				0.5f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, new List<string>
		{
			Db.Get().OrbitalTypeCategories.iceCloud.Id,
			Db.Get().OrbitalTypeCategories.iceRock.Id
		}, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("gas_giant_cloud", new HarvestablePOIConfigurator.HarvestablePOIType("GasGiantCloud", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Methane,
				1f
			},
			{
				SimHashes.LiquidMethane,
				1f
			},
			{
				SimHashes.SolidMethane,
				1f
			},
			{
				SimHashes.Hydrogen,
				7f
			}
		}, 15000f, 20000f, 30000f, 60000f, true, HarvestablePOIConfig.GasFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("chlorine_cloud", new HarvestablePOIConfigurator.HarvestablePOIType("ChlorineCloud", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Chlorine,
				2.5f
			},
			{
				SimHashes.BleachStone,
				7.5f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.GasFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("gilded_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("GildedAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Gold,
				2.5f
			},
			{
				SimHashes.Fullerene,
				1f
			},
			{
				SimHashes.RefinedCarbon,
				1f
			},
			{
				SimHashes.SedimentaryRock,
				4.5f
			},
			{
				SimHashes.Regolith,
				1f
			}
		}, 30000f, 45000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("glimmering_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("GlimmeringAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.MoltenTungsten,
				2f
			},
			{
				SimHashes.Wolframite,
				6f
			},
			{
				SimHashes.Carbon,
				1f
			},
			{
				SimHashes.CarbonDioxide,
				1f
			}
		}, 30000f, 45000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("helium_cloud", new HarvestablePOIConfigurator.HarvestablePOIType("HeliumCloud", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Hydrogen,
				2f
			},
			{
				SimHashes.Water,
				8f
			}
		}, 30000f, 45000f, 30000f, 60000f, true, HarvestablePOIConfig.GasFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("oily_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("OilyAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.SolidCarbonDioxide,
				7.75f
			},
			{
				SimHashes.SolidMethane,
				1.125f
			},
			{
				SimHashes.CrudeOil,
				1.125f
			}
		}, 15000f, 25000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("oxidized_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("OxidizedAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Rust,
				8f
			},
			{
				SimHashes.SolidCarbonDioxide,
				2f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("salty_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("SaltyAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.SaltWater,
				5f
			},
			{
				SimHashes.Brine,
				4f
			},
			{
				SimHashes.SolidCarbonDioxide,
				1f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("frozen_ore_field", new HarvestablePOIConfigurator.HarvestablePOIType("FrozenOreField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Ice,
				2.33f
			},
			{
				SimHashes.DirtyIce,
				2.33f
			},
			{
				SimHashes.Snow,
				1.83f
			},
			{
				SimHashes.AluminumOre,
				2f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("foresty_ore_field", new HarvestablePOIConfigurator.HarvestablePOIType("ForestyOreField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.IgneousRock,
				7f
			},
			{
				SimHashes.AluminumOre,
				1f
			},
			{
				SimHashes.CarbonDioxide,
				2f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("swampy_ore_field", new HarvestablePOIConfigurator.HarvestablePOIType("SwampyOreField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Mud,
				2f
			},
			{
				SimHashes.ToxicSand,
				7f
			},
			{
				SimHashes.Cobaltite,
				1f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("sandy_ore_field", new HarvestablePOIConfigurator.HarvestablePOIType("SandyOreField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.SandStone,
				4f
			},
			{
				SimHashes.Algae,
				2f
			},
			{
				SimHashes.Cuprite,
				1f
			},
			{
				SimHashes.Sand,
				3f
			}
		}, 54000f, 81000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("radioactive_gas_cloud", new HarvestablePOIConfigurator.HarvestablePOIType("RadioactiveGasCloud", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.UraniumOre,
				2f
			},
			{
				SimHashes.Chlorine,
				2f
			},
			{
				SimHashes.CarbonDioxide,
				7f
			}
		}, 5000f, 10000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("radioactive_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("RadioactiveAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.UraniumOre,
				2f
			},
			{
				SimHashes.Sulfur,
				3f
			},
			{
				SimHashes.BleachStone,
				2f
			},
			{
				SimHashes.Rust,
				4f
			}
		}, 5000f, 10000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("oxygen_rich_asteroid_field", new HarvestablePOIConfigurator.HarvestablePOIType("OxygenRichAsteroidField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Water,
				4f
			},
			{
				SimHashes.ContaminatedOxygen,
				2f
			},
			{
				SimHashes.Ice,
				4f
			}
		}, 15000f, 25000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("interstellar_ocean", new HarvestablePOIConfigurator.HarvestablePOIType("InterstellarOcean", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.SaltWater,
				2.5f
			},
			{
				SimHashes.Brine,
				2.5f
			},
			{
				SimHashes.Salt,
				2.5f
			},
			{
				SimHashes.Ice,
				2.5f
			}
		}, 15000f, 25000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1, null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("ceres_debris_field", new HarvestablePOIConfigurator.HarvestablePOIType("DLC2CeresField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Cinnabar,
				4.5f
			},
			{
				SimHashes.Mercury,
				2.5f
			},
			{
				SimHashes.Ice,
				2.5f
			}
		}, 15000f, 25000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1.Append(DlcManager.DLC2), null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("ceres_starting_field", new HarvestablePOIConfigurator.HarvestablePOIType("DLC2CeresOreField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Cinnabar,
				2.5f
			},
			{
				SimHashes.Mercury,
				2.5f
			},
			{
				SimHashes.Ice,
				3.5f
			}
		}, 15000f, 25000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1.Append(DlcManager.DLC2), null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("prehistoric_SO1", new HarvestablePOIConfigurator.HarvestablePOIType("DLC4PrehistoricMixingField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.NickelOre,
				4.5f
			},
			{
				SimHashes.Peat,
				2.5f
			},
			{
				SimHashes.Shale,
				1f
			},
			{
				SimHashes.Amber,
				1f
			},
			{
				SimHashes.Iridium,
				0.5f
			}
		}, 15000f, 25000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1.Append(DlcManager.DLC4), null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("prehistoric_SO2", new HarvestablePOIConfigurator.HarvestablePOIType("DLC4PrehistoricOreField", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.NickelOre,
				1.5f
			},
			{
				SimHashes.Peat,
				2.5f
			},
			{
				SimHashes.Shale,
				1f
			},
			{
				SimHashes.Amber,
				1f
			}
		}, 15000f, 25000f, 30000f, 60000f, true, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1.Append(DlcManager.DLC4), null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("prehistoric_impactor_1", new HarvestablePOIConfigurator.HarvestablePOIType("DLC4ImpactorDebrisField1", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Iridium,
				1.7f
			},
			{
				SimHashes.MaficRock,
				2.3f
			},
			{
				SimHashes.Gold,
				2.1f
			},
			{
				SimHashes.Granite,
				3.9f
			}
		}, 35000f, 45000f, 30000f, 30000f, false, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1.Append(DlcManager.DLC4), null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("prehistoric_impactor_2", new HarvestablePOIConfigurator.HarvestablePOIType("DLC4ImpactorDebrisField2", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.Isoresin,
				1.8f
			},
			{
				SimHashes.Petroleum,
				3.5f
			},
			{
				SimHashes.LiquidSulfur,
				4.7f
			}
		}, 33400f, 66800f, 30000f, 30000f, false, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1.Append(DlcManager.DLC4), null)));
		list.Add(new HarvestablePOIConfig.HarvestablePOIParams("prehistoric_impactor_3", new HarvestablePOIConfigurator.HarvestablePOIType("DLC4ImpactorDebrisField3", new Dictionary<SimHashes, float>
		{
			{
				SimHashes.MoltenIridium,
				3.7f
			},
			{
				SimHashes.LiquidOxygen,
				0.6f
			},
			{
				SimHashes.LiquidHydrogen,
				0.6f
			},
			{
				SimHashes.Magma,
				5.1f
			}
		}, 110000f, 137500f, 30000f, 30000f, false, HarvestablePOIConfig.AsteroidFieldOrbit, 20, DlcManager.EXPANSION1.Append(DlcManager.DLC4), null)));
		list.RemoveAll((HarvestablePOIConfig.HarvestablePOIParams poi) => !DlcManager.IsCorrectDlcSubscribed(poi.poiType));
		return list;
	}

	// Token: 0x04000A2A RID: 2602
	public const string CarbonAsteroidField = "CarbonAsteroidField";

	// Token: 0x04000A2B RID: 2603
	public const string MetallicAsteroidField = "MetallicAsteroidField";

	// Token: 0x04000A2C RID: 2604
	public const string SatelliteField = "SatelliteField";

	// Token: 0x04000A2D RID: 2605
	public const string RockyAsteroidField = "RockyAsteroidField";

	// Token: 0x04000A2E RID: 2606
	public const string InterstellarIceField = "InterstellarIceField";

	// Token: 0x04000A2F RID: 2607
	public const string OrganicMassField = "OrganicMassField";

	// Token: 0x04000A30 RID: 2608
	public const string IceAsteroidField = "IceAsteroidField";

	// Token: 0x04000A31 RID: 2609
	public const string GasGiantCloud = "GasGiantCloud";

	// Token: 0x04000A32 RID: 2610
	public const string ChlorineCloud = "ChlorineCloud";

	// Token: 0x04000A33 RID: 2611
	public const string GildedAsteroidField = "GildedAsteroidField";

	// Token: 0x04000A34 RID: 2612
	public const string GlimmeringAsteroidField = "GlimmeringAsteroidField";

	// Token: 0x04000A35 RID: 2613
	public const string HeliumCloud = "HeliumCloud";

	// Token: 0x04000A36 RID: 2614
	public const string OilyAsteroidField = "OilyAsteroidField";

	// Token: 0x04000A37 RID: 2615
	public const string OxidizedAsteroidField = "OxidizedAsteroidField";

	// Token: 0x04000A38 RID: 2616
	public const string SaltyAsteroidField = "SaltyAsteroidField";

	// Token: 0x04000A39 RID: 2617
	public const string FrozenOreField = "FrozenOreField";

	// Token: 0x04000A3A RID: 2618
	public const string ForestyOreField = "ForestyOreField";

	// Token: 0x04000A3B RID: 2619
	public const string SwampyOreField = "SwampyOreField";

	// Token: 0x04000A3C RID: 2620
	public const string SandyOreField = "SandyOreField";

	// Token: 0x04000A3D RID: 2621
	public const string RadioactiveGasCloud = "RadioactiveGasCloud";

	// Token: 0x04000A3E RID: 2622
	public const string RadioactiveAsteroidField = "RadioactiveAsteroidField";

	// Token: 0x04000A3F RID: 2623
	public const string OxygenRichAsteroidField = "OxygenRichAsteroidField";

	// Token: 0x04000A40 RID: 2624
	public const string InterstellarOcean = "InterstellarOcean";

	// Token: 0x04000A41 RID: 2625
	public const string DLC2CeresField = "DLC2CeresField";

	// Token: 0x04000A42 RID: 2626
	public const string DLC2CeresOreField = "DLC2CeresOreField";

	// Token: 0x04000A43 RID: 2627
	public const string DLC4PrehistoricMixingField = "DLC4PrehistoricMixingField";

	// Token: 0x04000A44 RID: 2628
	public const string DLC4PrehistoricOreField = "DLC4PrehistoricOreField";

	// Token: 0x04000A45 RID: 2629
	public const string DLC4ImpactorDebrisField1 = "DLC4ImpactorDebrisField1";

	// Token: 0x04000A46 RID: 2630
	public const string DLC4ImpactorDebrisField2 = "DLC4ImpactorDebrisField2";

	// Token: 0x04000A47 RID: 2631
	public const string DLC4ImpactorDebrisField3 = "DLC4ImpactorDebrisField3";

	// Token: 0x04000A48 RID: 2632
	private static readonly List<string> GasFieldOrbit = new List<string>
	{
		Db.Get().OrbitalTypeCategories.iceCloud.Id,
		Db.Get().OrbitalTypeCategories.heliumCloud.Id,
		Db.Get().OrbitalTypeCategories.purpleGas.Id,
		Db.Get().OrbitalTypeCategories.radioactiveGas.Id
	};

	// Token: 0x04000A49 RID: 2633
	private static readonly List<string> AsteroidFieldOrbit = new List<string>
	{
		Db.Get().OrbitalTypeCategories.iceRock.Id,
		Db.Get().OrbitalTypeCategories.frozenOre.Id,
		Db.Get().OrbitalTypeCategories.rocky.Id
	};

	// Token: 0x020011D1 RID: 4561
	public struct HarvestablePOIParams
	{
		// Token: 0x06008402 RID: 33794 RVA: 0x00334D08 File Offset: 0x00332F08
		public HarvestablePOIParams(string anim, HarvestablePOIConfigurator.HarvestablePOIType poiType)
		{
			this.id = "HarvestableSpacePOI_" + poiType.id;
			this.anim = anim;
			this.nameStringKey = new StringKey("STRINGS.UI.SPACEDESTINATIONS.HARVESTABLE_POI." + poiType.id.ToUpper() + ".NAME");
			this.descStringKey = new StringKey("STRINGS.UI.SPACEDESTINATIONS.HARVESTABLE_POI." + poiType.id.ToUpper() + ".DESC");
			this.poiType = poiType;
		}

		// Token: 0x0400643F RID: 25663
		public string id;

		// Token: 0x04006440 RID: 25664
		public string anim;

		// Token: 0x04006441 RID: 25665
		public StringKey nameStringKey;

		// Token: 0x04006442 RID: 25666
		public StringKey descStringKey;

		// Token: 0x04006443 RID: 25667
		public HarvestablePOIConfigurator.HarvestablePOIType poiType;
	}
}
