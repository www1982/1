using System;
using System.Collections.Generic;
using Klei;
using TUNING;
using UnityEngine;

// Token: 0x02000197 RID: 407
public class GeyserGenericConfig : IMultiEntityConfig
{
	// Token: 0x060007D2 RID: 2002 RVA: 0x000350C8 File Offset: 0x000332C8
	public List<GameObject> CreatePrefabs()
	{
		List<GameObject> list = new List<GameObject>();
		List<GeyserGenericConfig.GeyserPrefabParams> configs = this.GenerateConfigs();
		foreach (GeyserGenericConfig.GeyserPrefabParams geyserPrefabParams in configs)
		{
			list.Add(GeyserGenericConfig.CreateGeyser(geyserPrefabParams.id, geyserPrefabParams.anim, geyserPrefabParams.width, geyserPrefabParams.height, Strings.Get(geyserPrefabParams.nameStringKey), Strings.Get(geyserPrefabParams.descStringKey), geyserPrefabParams.geyserType.idHash, geyserPrefabParams.geyserType.geyserTemperature, geyserPrefabParams.geyserType.requiredDlcIds, geyserPrefabParams.geyserType.forbiddenDlcIds));
		}
		configs.RemoveAll((GeyserGenericConfig.GeyserPrefabParams x) => !x.isGenericGeyser);
		GameObject gameObject = EntityTemplates.CreateEntity("GeyserGeneric", "Random Geyser Spawner", true);
		gameObject.AddOrGet<SaveLoadRoot>();
		gameObject.GetComponent<KPrefabID>().prefabInitFn += delegate(GameObject inst)
		{
			int num = 0;
			if (SaveLoader.Instance.clusterDetailSave != null)
			{
				num = SaveLoader.Instance.clusterDetailSave.globalWorldSeed;
			}
			else
			{
				global::Debug.LogWarning("Could not load global world seed for geysers");
			}
			num = num + (int)inst.transform.GetPosition().x + (int)inst.transform.GetPosition().y;
			List<GeyserGenericConfig.GeyserPrefabParams> list2 = configs.FindAll((GeyserGenericConfig.GeyserPrefabParams x) => Game.IsCorrectDlcActiveForCurrentSave(x.geyserType));
			int num2 = new KRandom(num).Next(0, configs.Count);
			GameUtil.KInstantiate(Assets.GetPrefab(list2[num2].id), inst.transform.GetPosition(), Grid.SceneLayer.BuildingBack, null, 0).SetActive(true);
			inst.DeleteObject();
		};
		list.Add(gameObject);
		return list;
	}

	// Token: 0x060007D3 RID: 2003 RVA: 0x00035210 File Offset: 0x00033410
	public static GameObject CreateGeyser(string id, string anim, int width, int height, string name, string desc, HashedString presetType, float geyserTemperature)
	{
		return GeyserGenericConfig.CreateGeyser(id, anim, width, height, name, desc, presetType, geyserTemperature, null, null);
	}

	// Token: 0x060007D4 RID: 2004 RVA: 0x00035230 File Offset: 0x00033430
	public static GameObject CreateGeyser(string id, string anim, int width, int height, string name, string desc, HashedString presetType, float geyserTemperature, string[] requiredDlcIds, string[] forbiddenDlcIds)
	{
		float num = 2000f;
		EffectorValues tier = BUILDINGS.DECOR.BONUS.TIER1;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER6;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, name, desc, num, Assets.GetAnim(anim), "inactive", Grid.SceneLayer.BuildingBack, width, height, tier, tier2, SimHashes.Creature, new List<Tag> { GameTags.GeyserFeature }, 293f);
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		PrimaryElement component = gameObject.GetComponent<PrimaryElement>();
		component.SetElement(SimHashes.Katairite, true);
		component.Temperature = geyserTemperature;
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddOrGet<Uncoverable>();
		gameObject.AddOrGet<Geyser>().outputOffset = new Vector2I(0, 1);
		gameObject.AddOrGet<GeyserConfigurator>().presetType = presetType;
		Studyable studyable = gameObject.AddOrGet<Studyable>();
		studyable.meterTrackerSymbol = "geotracker_target";
		studyable.meterAnim = "tracker";
		gameObject.AddOrGet<LoopingSounds>();
		SoundEventVolumeCache.instance.AddVolume("geyser_side_steam_kanim", "Geyser_shake_LP", NOISE_POLLUTION.NOISY.TIER5);
		SoundEventVolumeCache.instance.AddVolume("geyser_side_steam_kanim", "Geyser_erupt_LP", NOISE_POLLUTION.NOISY.TIER6);
		KPrefabID component2 = gameObject.GetComponent<KPrefabID>();
		component2.requiredDlcIds = requiredDlcIds;
		component2.forbiddenDlcIds = forbiddenDlcIds;
		return gameObject;
	}

	// Token: 0x060007D5 RID: 2005 RVA: 0x00035354 File Offset: 0x00033554
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060007D6 RID: 2006 RVA: 0x00035356 File Offset: 0x00033556
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x060007D7 RID: 2007 RVA: 0x00035358 File Offset: 0x00033558
	private List<GeyserGenericConfig.GeyserPrefabParams> GenerateConfigs()
	{
		List<GeyserGenericConfig.GeyserPrefabParams> list = new List<GeyserGenericConfig.GeyserPrefabParams>();
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_steam_kanim", 2, 4, new GeyserConfigurator.GeyserType("steam", SimHashes.Steam, GeyserConfigurator.GeyserShape.Gas, 383.15f, 1000f, 2000f, 5f, null, null, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_steam_hot_kanim", 2, 4, new GeyserConfigurator.GeyserType("hot_steam", SimHashes.Steam, GeyserConfigurator.GeyserShape.Gas, 773.15f, 500f, 1000f, 5f, null, null, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_liquid_water_hot_kanim", 4, 2, new GeyserConfigurator.GeyserType("hot_water", SimHashes.Water, GeyserConfigurator.GeyserShape.Liquid, 368.15f, 2000f, 4000f, 500f, null, null, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_liquid_water_slush_kanim", 4, 2, new GeyserConfigurator.GeyserType("slush_water", SimHashes.DirtyWater, GeyserConfigurator.GeyserShape.Liquid, 263.15f, 1000f, 2000f, 500f, null, null, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 263f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_liquid_water_filthy_kanim", 4, 2, new GeyserConfigurator.GeyserType("filthy_water", SimHashes.DirtyWater, GeyserConfigurator.GeyserShape.Liquid, 303.15f, 2000f, 4000f, 500f, null, null, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f).AddDisease(new SimUtil.DiseaseInfo
		{
			idx = Db.Get().Diseases.GetIndex("FoodPoisoning"),
			count = 20000
		}), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_liquid_salt_water_cool_slush_kanim", 4, 2, new GeyserConfigurator.GeyserType("slush_salt_water", SimHashes.Brine, GeyserConfigurator.GeyserShape.Liquid, 263.15f, 1000f, 2000f, 500f, null, null, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 263f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_liquid_salt_water_kanim", 4, 2, new GeyserConfigurator.GeyserType("salt_water", SimHashes.SaltWater, GeyserConfigurator.GeyserShape.Liquid, 368.15f, 2000f, 4000f, 500f, null, null, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_volcano_small_kanim", 3, 3, new GeyserConfigurator.GeyserType("small_volcano", SimHashes.Magma, GeyserConfigurator.GeyserShape.Molten, 2000f, 400f, 800f, 150f, null, null, 6000f, 12000f, 0.005f, 0.01f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_volcano_big_kanim", 3, 3, new GeyserConfigurator.GeyserType("big_volcano", SimHashes.Magma, GeyserConfigurator.GeyserShape.Molten, 2000f, 800f, 1600f, 150f, null, null, 6000f, 12000f, 0.005f, 0.01f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_liquid_co2_kanim", 4, 2, new GeyserConfigurator.GeyserType("liquid_co2", SimHashes.LiquidCarbonDioxide, GeyserConfigurator.GeyserShape.Liquid, 218f, 100f, 200f, 50f, null, null, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 218f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_co2_hot_kanim", 2, 4, new GeyserConfigurator.GeyserType("hot_co2", SimHashes.CarbonDioxide, GeyserConfigurator.GeyserShape.Gas, 773.15f, 70f, 140f, 5f, null, null, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_hydrogen_hot_kanim", 2, 4, new GeyserConfigurator.GeyserType("hot_hydrogen", SimHashes.Hydrogen, GeyserConfigurator.GeyserShape.Gas, 773.15f, 70f, 140f, 5f, null, null, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_po2_hot_kanim", 2, 4, new GeyserConfigurator.GeyserType("hot_po2", SimHashes.ContaminatedOxygen, GeyserConfigurator.GeyserShape.Gas, 773.15f, 70f, 140f, 5f, null, null, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_po2_slimy_kanim", 2, 4, new GeyserConfigurator.GeyserType("slimy_po2", SimHashes.ContaminatedOxygen, GeyserConfigurator.GeyserShape.Gas, 333.15f, 70f, 140f, 5f, null, null, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f).AddDisease(new SimUtil.DiseaseInfo
		{
			idx = Db.Get().Diseases.GetIndex("SlimeLung"),
			count = 5000
		}), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_chlorine_kanim", 2, 4, new GeyserConfigurator.GeyserType("chlorine_gas", SimHashes.ChlorineGas, GeyserConfigurator.GeyserShape.Gas, 333.15f, 70f, 140f, 5f, null, null, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_chlorine_kanim", 2, 4, new GeyserConfigurator.GeyserType("chlorine_gas_cool", SimHashes.ChlorineGas, GeyserConfigurator.GeyserShape.Gas, 278.15f, 70f, 140f, 5f, null, null, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), false));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_gas_methane_kanim", 2, 4, new GeyserConfigurator.GeyserType("methane", SimHashes.Methane, GeyserConfigurator.GeyserShape.Gas, 423.15f, 70f, 140f, 5f, null, null, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_copper_kanim", 3, 3, new GeyserConfigurator.GeyserType("molten_copper", SimHashes.MoltenCopper, GeyserConfigurator.GeyserShape.Molten, 2500f, 200f, 400f, 150f, null, null, 480f, 1080f, 0.016666668f, 0.1f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_iron_kanim", 3, 3, new GeyserConfigurator.GeyserType("molten_iron", SimHashes.MoltenIron, GeyserConfigurator.GeyserShape.Molten, 2800f, 200f, 400f, 150f, null, null, 480f, 1080f, 0.016666668f, 0.1f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_gold_kanim", 3, 3, new GeyserConfigurator.GeyserType("molten_gold", SimHashes.MoltenGold, GeyserConfigurator.GeyserShape.Molten, 2900f, 200f, 400f, 150f, null, null, 480f, 1080f, 0.016666668f, 0.1f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_aluminum_kanim", 3, 3, new GeyserConfigurator.GeyserType("molten_aluminum", SimHashes.MoltenAluminum, GeyserConfigurator.GeyserShape.Molten, 2000f, 200f, 400f, 150f, DlcManager.EXPANSION1, null, 480f, 1080f, 0.016666668f, 0.1f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_tungsten_kanim", 3, 3, new GeyserConfigurator.GeyserType("molten_tungsten", SimHashes.MoltenTungsten, GeyserConfigurator.GeyserShape.Molten, 4000f, 200f, 400f, 150f, DlcManager.EXPANSION1, null, 480f, 1080f, 0.016666668f, 0.1f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), false));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_niobium_kanim", 3, 3, new GeyserConfigurator.GeyserType("molten_niobium", SimHashes.MoltenNiobium, GeyserConfigurator.GeyserShape.Molten, 3500f, 800f, 1600f, 150f, DlcManager.EXPANSION1, null, 6000f, 12000f, 0.005f, 0.01f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), false));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_molten_cobalt_kanim", 3, 3, new GeyserConfigurator.GeyserType("molten_cobalt", SimHashes.MoltenCobalt, GeyserConfigurator.GeyserShape.Molten, 2500f, 200f, 400f, 150f, DlcManager.EXPANSION1, null, 480f, 1080f, 0.016666668f, 0.1f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_liquid_oil_kanim", 4, 2, new GeyserConfigurator.GeyserType("oil_drip", SimHashes.CrudeOil, GeyserConfigurator.GeyserShape.Liquid, 600f, 1f, 250f, 50f, null, null, 600f, 600f, 1f, 1f, 100f, 500f, 0.4f, 0.8f, 372.15f), true));
		list.Add(new GeyserGenericConfig.GeyserPrefabParams("geyser_liquid_sulfur_kanim", 4, 2, new GeyserConfigurator.GeyserType("liquid_sulfur", SimHashes.LiquidSulfur, GeyserConfigurator.GeyserShape.Liquid, 438.34998f, 1000f, 2000f, 500f, DlcManager.EXPANSION1, null, 60f, 1140f, 0.1f, 0.9f, 15000f, 135000f, 0.4f, 0.8f, 372.15f), true));
		list.RemoveAll((GeyserGenericConfig.GeyserPrefabParams geyser) => !DlcManager.IsCorrectDlcSubscribed(geyser.geyserType));
		return list;
	}

	// Token: 0x040005C3 RID: 1475
	public const string ID = "GeyserGeneric";

	// Token: 0x040005C4 RID: 1476
	public const string Steam = "steam";

	// Token: 0x040005C5 RID: 1477
	public const string HotSteam = "hot_steam";

	// Token: 0x040005C6 RID: 1478
	public const string HotWater = "hot_water";

	// Token: 0x040005C7 RID: 1479
	public const string SlushWater = "slush_water";

	// Token: 0x040005C8 RID: 1480
	public const string FilthyWater = "filthy_water";

	// Token: 0x040005C9 RID: 1481
	public const string SlushSaltWater = "slush_salt_water";

	// Token: 0x040005CA RID: 1482
	public const string SaltWater = "salt_water";

	// Token: 0x040005CB RID: 1483
	public const string SmallVolcano = "small_volcano";

	// Token: 0x040005CC RID: 1484
	public const string BigVolcano = "big_volcano";

	// Token: 0x040005CD RID: 1485
	public const string LiquidCO2 = "liquid_co2";

	// Token: 0x040005CE RID: 1486
	public const string HotCO2 = "hot_co2";

	// Token: 0x040005CF RID: 1487
	public const string HotHydrogen = "hot_hydrogen";

	// Token: 0x040005D0 RID: 1488
	public const string HotPO2 = "hot_po2";

	// Token: 0x040005D1 RID: 1489
	public const string SlimyPO2 = "slimy_po2";

	// Token: 0x040005D2 RID: 1490
	public const string ChlorineGas = "chlorine_gas";

	// Token: 0x040005D3 RID: 1491
	public const string ChlorineGasCool = "chlorine_gas_cool";

	// Token: 0x040005D4 RID: 1492
	public const string Methane = "methane";

	// Token: 0x040005D5 RID: 1493
	public const string MoltenCopper = "molten_copper";

	// Token: 0x040005D6 RID: 1494
	public const string MoltenIron = "molten_iron";

	// Token: 0x040005D7 RID: 1495
	public const string MoltenGold = "molten_gold";

	// Token: 0x040005D8 RID: 1496
	public const string MoltenAluminum = "molten_aluminum";

	// Token: 0x040005D9 RID: 1497
	public const string MoltenTungsten = "molten_tungsten";

	// Token: 0x040005DA RID: 1498
	public const string MoltenNiobium = "molten_niobium";

	// Token: 0x040005DB RID: 1499
	public const string MoltenCobalt = "molten_cobalt";

	// Token: 0x040005DC RID: 1500
	public const string OilDrip = "oil_drip";

	// Token: 0x040005DD RID: 1501
	public const string LiquidSulfur = "liquid_sulfur";

	// Token: 0x0200115D RID: 4445
	public struct GeyserPrefabParams
	{
		// Token: 0x0600822C RID: 33324 RVA: 0x00330ED4 File Offset: 0x0032F0D4
		public GeyserPrefabParams(string anim, int width, int height, GeyserConfigurator.GeyserType geyserType, bool isGenericGeyser)
		{
			this.id = "GeyserGeneric_" + geyserType.id;
			this.anim = anim;
			this.width = width;
			this.height = height;
			this.nameStringKey = new StringKey("STRINGS.CREATURES.SPECIES.GEYSER." + geyserType.id.ToUpper() + ".NAME");
			this.descStringKey = new StringKey("STRINGS.CREATURES.SPECIES.GEYSER." + geyserType.id.ToUpper() + ".DESC");
			this.geyserType = geyserType;
			this.isGenericGeyser = isGenericGeyser;
		}

		// Token: 0x040062A9 RID: 25257
		public string id;

		// Token: 0x040062AA RID: 25258
		public string anim;

		// Token: 0x040062AB RID: 25259
		public int width;

		// Token: 0x040062AC RID: 25260
		public int height;

		// Token: 0x040062AD RID: 25261
		public StringKey nameStringKey;

		// Token: 0x040062AE RID: 25262
		public StringKey descStringKey;

		// Token: 0x040062AF RID: 25263
		public GeyserConfigurator.GeyserType geyserType;

		// Token: 0x040062B0 RID: 25264
		public bool isGenericGeyser;
	}

	// Token: 0x0200115E RID: 4446
	private static class TEMPERATURES
	{
		// Token: 0x040062B1 RID: 25265
		public const float BELOW_FREEZING = 263.15f;

		// Token: 0x040062B2 RID: 25266
		public const float DUPE_NORMAL = 303.15f;

		// Token: 0x040062B3 RID: 25267
		public const float DUPE_HOT = 333.15f;

		// Token: 0x040062B4 RID: 25268
		public const float BELOW_BOILING = 368.15f;

		// Token: 0x040062B5 RID: 25269
		public const float ABOVE_BOILING = 383.15f;

		// Token: 0x040062B6 RID: 25270
		public const float HOT1 = 423.15f;

		// Token: 0x040062B7 RID: 25271
		public const float HOT2 = 773.15f;

		// Token: 0x040062B8 RID: 25272
		public const float MOLTEN_MAGMA = 2000f;
	}

	// Token: 0x0200115F RID: 4447
	public static class RATES
	{
		// Token: 0x040062B9 RID: 25273
		public const float GAS_SMALL_MIN = 40f;

		// Token: 0x040062BA RID: 25274
		public const float GAS_SMALL_MAX = 80f;

		// Token: 0x040062BB RID: 25275
		public const float GAS_NORMAL_MIN = 70f;

		// Token: 0x040062BC RID: 25276
		public const float GAS_NORMAL_MAX = 140f;

		// Token: 0x040062BD RID: 25277
		public const float GAS_BIG_MIN = 100f;

		// Token: 0x040062BE RID: 25278
		public const float GAS_BIG_MAX = 200f;

		// Token: 0x040062BF RID: 25279
		public const float LIQUID_SMALL_MIN = 500f;

		// Token: 0x040062C0 RID: 25280
		public const float LIQUID_SMALL_MAX = 1000f;

		// Token: 0x040062C1 RID: 25281
		public const float LIQUID_NORMAL_MIN = 1000f;

		// Token: 0x040062C2 RID: 25282
		public const float LIQUID_NORMAL_MAX = 2000f;

		// Token: 0x040062C3 RID: 25283
		public const float LIQUID_BIG_MIN = 2000f;

		// Token: 0x040062C4 RID: 25284
		public const float LIQUID_BIG_MAX = 4000f;

		// Token: 0x040062C5 RID: 25285
		public const float MOLTEN_NORMAL_MIN = 200f;

		// Token: 0x040062C6 RID: 25286
		public const float MOLTEN_NORMAL_MAX = 400f;

		// Token: 0x040062C7 RID: 25287
		public const float MOLTEN_BIG_MIN = 400f;

		// Token: 0x040062C8 RID: 25288
		public const float MOLTEN_BIG_MAX = 800f;

		// Token: 0x040062C9 RID: 25289
		public const float MOLTEN_HUGE_MIN = 800f;

		// Token: 0x040062CA RID: 25290
		public const float MOLTEN_HUGE_MAX = 1600f;
	}

	// Token: 0x02001160 RID: 4448
	public static class MAX_PRESSURES
	{
		// Token: 0x040062CB RID: 25291
		public const float GAS = 5f;

		// Token: 0x040062CC RID: 25292
		public const float GAS_HIGH = 15f;

		// Token: 0x040062CD RID: 25293
		public const float MOLTEN = 150f;

		// Token: 0x040062CE RID: 25294
		public const float LIQUID_SMALL = 50f;

		// Token: 0x040062CF RID: 25295
		public const float LIQUID = 500f;
	}

	// Token: 0x02001161 RID: 4449
	public static class ITERATIONS
	{
		// Token: 0x02002616 RID: 9750
		public static class INFREQUENT_MOLTEN
		{
			// Token: 0x0400A98E RID: 43406
			public const float PCT_MIN = 0.005f;

			// Token: 0x0400A98F RID: 43407
			public const float PCT_MAX = 0.01f;

			// Token: 0x0400A990 RID: 43408
			public const float LEN_MIN = 6000f;

			// Token: 0x0400A991 RID: 43409
			public const float LEN_MAX = 12000f;
		}

		// Token: 0x02002617 RID: 9751
		public static class FREQUENT_MOLTEN
		{
			// Token: 0x0400A992 RID: 43410
			public const float PCT_MIN = 0.016666668f;

			// Token: 0x0400A993 RID: 43411
			public const float PCT_MAX = 0.1f;

			// Token: 0x0400A994 RID: 43412
			public const float LEN_MIN = 480f;

			// Token: 0x0400A995 RID: 43413
			public const float LEN_MAX = 1080f;
		}
	}
}
