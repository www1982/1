using System;
using System.Collections.Generic;
using KSerialization;
using UnityEngine;

// Token: 0x02000960 RID: 2400
[AddComponentMenu("KMonoBehaviour/scripts/Immigration")]
public class Immigration : KMonoBehaviour, ISaveLoadable, ISim200ms, IPersonalPriorityManager
{
	// Token: 0x060044DE RID: 17630 RVA: 0x0018B12B File Offset: 0x0018932B
	public static void DestroyInstance()
	{
		Immigration.Instance = null;
	}

	// Token: 0x060044DF RID: 17631 RVA: 0x0018B134 File Offset: 0x00189334
	protected override void OnPrefabInit()
	{
		this.bImmigrantAvailable = false;
		Immigration.Instance = this;
		int num = Math.Min(this.spawnIdx, this.spawnInterval.Length - 1);
		this.timeBeforeSpawn = this.spawnInterval[num];
		this.SetupDLCCarePackages();
		this.ResetPersonalPriorities();
		this.ConfigureCarePackages();
	}

	// Token: 0x060044E0 RID: 17632 RVA: 0x0018B184 File Offset: 0x00189384
	private void SetupDLCCarePackages()
	{
		Dictionary<string, List<CarePackageInfo>> dictionary = new Dictionary<string, List<CarePackageInfo>>();
		string text = "DLC2_ID";
		List<CarePackageInfo> list = new List<CarePackageInfo>();
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Cinnabar).tag.ToString(), 2000f, () => Immigration.CycleCondition(12) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Cinnabar).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.WoodLog).tag.ToString(), 200f, () => Immigration.CycleCondition(24) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.WoodLog).tag)));
		list.Add(new CarePackageInfo("WoodDeerBaby", 1f, () => Immigration.CycleCondition(24)));
		list.Add(new CarePackageInfo("SealBaby", 1f, () => Immigration.CycleCondition(48)));
		list.Add(new CarePackageInfo("IceBellyEgg", 1f, () => Immigration.CycleCondition(100)));
		list.Add(new CarePackageInfo("Pemmican", 3f, null));
		list.Add(new CarePackageInfo("FriesCarrot", 3f, () => Immigration.CycleCondition(24)));
		list.Add(new CarePackageInfo("IceFlowerSeed", 3f, null));
		list.Add(new CarePackageInfo("BlueGrassSeed", 1f, null));
		list.Add(new CarePackageInfo("CarrotPlantSeed", 1f, () => Immigration.CycleCondition(24)));
		list.Add(new CarePackageInfo("SpaceTreeSeed", 1f, () => Immigration.CycleCondition(24)));
		list.Add(new CarePackageInfo("HardSkinBerryPlantSeed", 3f, null));
		dictionary.Add(text, list);
		string text2 = "DLC3_ID";
		List<CarePackageInfo> list2 = new List<CarePackageInfo>();
		list2.Add(new CarePackageInfo("DisposableElectrobank_RawMetal", 3f, () => Immigration.CycleCondition(12)));
		dictionary.Add(text2, list2);
		string text3 = "DLC4_ID";
		List<CarePackageInfo> list3 = new List<CarePackageInfo>();
		list3.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Peat).tag.ToString(), 3000f, null));
		list3.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.NickelOre).tag.ToString(), 2000f, () => Immigration.CycleCondition(12) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.NickelOre).tag)));
		list3.Add(new CarePackageInfo("GardenFoodPlantSeed", 1f, null));
		list3.Add(new CarePackageInfo("GardenDecorPlantSeed", 1f, null));
		list3.Add(new CarePackageInfo("ButterflyPlantSeed", 1f, null));
		list3.Add(new CarePackageInfo("DinofernSeed", 1f, () => Immigration.CycleCondition(48)));
		list3.Add(new CarePackageInfo("DewDripperPlantSeed", 1f, () => Immigration.CycleCondition(48)));
		list3.Add(new CarePackageInfo("KelpPlantSeed", 1f, () => Immigration.CycleCondition(48)));
		list3.Add(new CarePackageInfo("FlyTrapPlantSeed", 1f, () => Immigration.CycleCondition(48)));
		list3.Add(new CarePackageInfo("VineMotherSeed", 1f, () => Immigration.CycleCondition(48)));
		list3.Add(new CarePackageInfo("GardenForagePlant", 3f, null));
		list3.Add(new CarePackageInfo(VineFruitConfig.ID, 6f, null));
		list3.Add(new CarePackageInfo("SmokedDinosaurMeat", 1f, () => Immigration.CycleCondition(48)));
		list3.Add(new CarePackageInfo("StegoBaby", 1f, null));
		list3.Add(new CarePackageInfo("ChameleonEgg", 1f, () => Immigration.CycleCondition(48)));
		list3.Add(new CarePackageInfo("MosquitoEgg", 3f, () => Immigration.CycleCondition(48)));
		list3.Add(new CarePackageInfo("PrehistoricPacuEgg", 1f, () => Immigration.CycleCondition(100)));
		list3.Add(new CarePackageInfo("RaptorEgg", 1f, () => Immigration.CycleCondition(100)));
		dictionary.Add(text3, list3);
		this.carePackagesByDlc = dictionary;
		foreach (KeyValuePair<Tag, BionicUpgradeComponentConfig.BionicUpgradeData> keyValuePair in BionicUpgradeComponentConfig.UpgradesData)
		{
			if (keyValuePair.Value.isCarePackage)
			{
				this.carePackagesByDlc["DLC3_ID"].Add(new CarePackageInfo(keyValuePair.Key.Name, 1f, () => Immigration.HasMinionModelCondition(BionicMinionConfig.MODEL)));
			}
		}
	}

	// Token: 0x060044E1 RID: 17633 RVA: 0x0018B7BC File Offset: 0x001899BC
	private void ConfigureCarePackages()
	{
		if (DlcManager.FeatureClusterSpaceEnabled())
		{
			this.ConfigureMultiWorldCarePackages();
		}
		else
		{
			this.ConfigureBaseGameCarePackages();
		}
		foreach (string text in SaveLoader.Instance.GameInfo.dlcIds)
		{
			if (this.carePackagesByDlc.ContainsKey(text))
			{
				this.carePackages.AddRange(this.carePackagesByDlc[text]);
			}
		}
	}

	// Token: 0x060044E2 RID: 17634 RVA: 0x0018B84C File Offset: 0x00189A4C
	private void ConfigureBaseGameCarePackages()
	{
		List<CarePackageInfo> list = new List<CarePackageInfo>();
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.SandStone).tag.ToString(), 1000f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Dirt).tag.ToString(), 500f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Algae).tag.ToString(), 500f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.OxyRock).tag.ToString(), 100f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Water).tag.ToString(), 2000f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Sand).tag.ToString(), 3000f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Carbon).tag.ToString(), 3000f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Fertilizer).tag.ToString(), 3000f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Ice).tag.ToString(), 4000f, () => Immigration.CycleCondition(12)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Brine).tag.ToString(), 2000f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.SaltWater).tag.ToString(), 2000f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Rust).tag.ToString(), 1000f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Cuprite).tag.ToString(), 2000f, () => Immigration.CycleCondition(12) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Cuprite).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.GoldAmalgam).tag.ToString(), 2000f, () => Immigration.CycleCondition(12) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.GoldAmalgam).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Copper).tag.ToString(), 400f, () => Immigration.CycleCondition(24) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Copper).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Iron).tag.ToString(), 400f, () => Immigration.CycleCondition(24) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Iron).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Lime).tag.ToString(), 150f, () => Immigration.CycleCondition(48) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Lime).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Polypropylene).tag.ToString(), 500f, () => Immigration.CycleCondition(48) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Polypropylene).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Glass).tag.ToString(), 200f, () => Immigration.CycleCondition(48) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Glass).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Steel).tag.ToString(), 100f, () => Immigration.CycleCondition(48) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Steel).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Ethanol).tag.ToString(), 100f, () => Immigration.CycleCondition(48) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Ethanol).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.AluminumOre).tag.ToString(), 100f, () => Immigration.CycleCondition(48) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.AluminumOre).tag)));
		list.Add(new CarePackageInfo("PrickleGrassSeed", 3f, null));
		list.Add(new CarePackageInfo("LeafyPlantSeed", 3f, null));
		list.Add(new CarePackageInfo("CactusPlantSeed", 3f, null));
		list.Add(new CarePackageInfo("MushroomSeed", 1f, null));
		list.Add(new CarePackageInfo("PrickleFlowerSeed", 2f, null));
		list.Add(new CarePackageInfo("OxyfernSeed", 1f, null));
		list.Add(new CarePackageInfo("ForestTreeSeed", 1f, null));
		list.Add(new CarePackageInfo(BasicFabricMaterialPlantConfig.SEED_ID, 3f, () => Immigration.CycleCondition(24)));
		list.Add(new CarePackageInfo("SwampLilySeed", 1f, () => Immigration.CycleCondition(24)));
		list.Add(new CarePackageInfo("ColdBreatherSeed", 1f, () => Immigration.CycleCondition(24)));
		list.Add(new CarePackageInfo("SpiceVineSeed", 1f, () => Immigration.CycleCondition(24)));
		list.Add(new CarePackageInfo("SaltPlantSeed", 1f, () => Immigration.CycleCondition(24)));
		list.Add(new CarePackageInfo("BasicSingleHarvestPlantSeed", 1f, () => Immigration.CycleCondition(24)));
		list.Add(new CarePackageInfo("FieldRation", 5f, null));
		list.Add(new CarePackageInfo("BasicForagePlant", 6f, null));
		list.Add(new CarePackageInfo("CookedEgg", 3f, () => Immigration.CycleCondition(6)));
		list.Add(new CarePackageInfo(PrickleFruitConfig.ID, 3f, () => Immigration.CycleCondition(12)));
		list.Add(new CarePackageInfo("FriedMushroom", 3f, () => Immigration.CycleCondition(24)));
		list.Add(new CarePackageInfo("CookedMeat", 3f, () => Immigration.CycleCondition(48)));
		list.Add(new CarePackageInfo("SpicyTofu", 3f, () => Immigration.CycleCondition(48)));
		list.Add(new CarePackageInfo("LightBugBaby", 1f, null));
		list.Add(new CarePackageInfo("HatchBaby", 1f, null));
		list.Add(new CarePackageInfo("PuftBaby", 1f, null));
		list.Add(new CarePackageInfo("SquirrelBaby", 1f, null));
		list.Add(new CarePackageInfo("CrabBaby", 1f, null));
		list.Add(new CarePackageInfo("DreckoBaby", 1f, () => Immigration.CycleCondition(24)));
		list.Add(new CarePackageInfo("Pacu", 8f, () => Immigration.CycleCondition(24)));
		list.Add(new CarePackageInfo("MoleBaby", 1f, () => Immigration.CycleCondition(48)));
		list.Add(new CarePackageInfo("OilfloaterBaby", 1f, () => Immigration.CycleCondition(48)));
		list.Add(new CarePackageInfo("LightBugEgg", 3f, null));
		list.Add(new CarePackageInfo("HatchEgg", 3f, null));
		list.Add(new CarePackageInfo("PuftEgg", 3f, null));
		list.Add(new CarePackageInfo("OilfloaterEgg", 3f, () => Immigration.CycleCondition(12)));
		list.Add(new CarePackageInfo("MoleEgg", 3f, () => Immigration.CycleCondition(24)));
		list.Add(new CarePackageInfo("DreckoEgg", 3f, () => Immigration.CycleCondition(24)));
		list.Add(new CarePackageInfo("SquirrelEgg", 2f, null));
		list.Add(new CarePackageInfo("BasicCure", 3f, null));
		list.Add(new CarePackageInfo("CustomClothing", 1f, null, "SELECTRANDOM"));
		list.Add(new CarePackageInfo("Funky_Vest", 1f, null));
		this.carePackages = list;
	}

	// Token: 0x060044E3 RID: 17635 RVA: 0x0018C2DC File Offset: 0x0018A4DC
	private void ConfigureMultiWorldCarePackages()
	{
		List<CarePackageInfo> list = new List<CarePackageInfo>();
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.SandStone).tag.ToString(), 1000f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Dirt).tag.ToString(), 500f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Algae).tag.ToString(), 500f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.OxyRock).tag.ToString(), 100f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Water).tag.ToString(), 2000f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Sand).tag.ToString(), 3000f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Carbon).tag.ToString(), 3000f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Fertilizer).tag.ToString(), 3000f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Ice).tag.ToString(), 4000f, () => Immigration.CycleCondition(12)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Brine).tag.ToString(), 2000f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.SaltWater).tag.ToString(), 2000f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Rust).tag.ToString(), 1000f, null));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Cuprite).tag.ToString(), 2000f, () => Immigration.CycleCondition(12) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Cuprite).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.GoldAmalgam).tag.ToString(), 2000f, () => Immigration.CycleCondition(12) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.GoldAmalgam).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Copper).tag.ToString(), 400f, () => Immigration.CycleCondition(24) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Copper).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Iron).tag.ToString(), 400f, () => Immigration.CycleCondition(24) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Iron).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Lime).tag.ToString(), 150f, () => Immigration.CycleCondition(48) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Lime).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Polypropylene).tag.ToString(), 500f, () => Immigration.CycleCondition(48) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Polypropylene).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Glass).tag.ToString(), 200f, () => Immigration.CycleCondition(48) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Glass).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Steel).tag.ToString(), 100f, () => Immigration.CycleCondition(48) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Steel).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.Ethanol).tag.ToString(), 100f, () => Immigration.CycleCondition(48) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.Ethanol).tag)));
		list.Add(new CarePackageInfo(ElementLoader.FindElementByHash(SimHashes.AluminumOre).tag.ToString(), 100f, () => Immigration.CycleCondition(48) && Immigration.DiscoveredCondition(ElementLoader.FindElementByHash(SimHashes.AluminumOre).tag)));
		list.Add(new CarePackageInfo("PrickleGrassSeed", 3f, null));
		list.Add(new CarePackageInfo("LeafyPlantSeed", 3f, null));
		list.Add(new CarePackageInfo("CactusPlantSeed", 3f, null));
		list.Add(new CarePackageInfo("WineCupsSeed", 3f, null));
		list.Add(new CarePackageInfo("CylindricaSeed", 3f, null));
		list.Add(new CarePackageInfo("MushroomSeed", 1f, null));
		list.Add(new CarePackageInfo("PrickleFlowerSeed", 2f, () => Immigration.DiscoveredCondition("PrickleFlowerSeed") || Immigration.CycleCondition(500)));
		list.Add(new CarePackageInfo("OxyfernSeed", 1f, null));
		list.Add(new CarePackageInfo("BasicSingleHarvestPlantSeed", 1f, () => Immigration.DiscoveredCondition("BasicSingleHarvestPlantSeed") || Immigration.CycleCondition(500)));
		list.Add(new CarePackageInfo("ForestTreeSeed", 1f, () => Immigration.DiscoveredCondition("ForestTreeSeed") || Immigration.CycleCondition(500)));
		list.Add(new CarePackageInfo(BasicFabricMaterialPlantConfig.SEED_ID, 3f, () => Immigration.CycleCondition(24) && (Immigration.DiscoveredCondition(BasicFabricMaterialPlantConfig.SEED_ID) || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("SwampLilySeed", 1f, () => Immigration.CycleCondition(24) && (Immigration.DiscoveredCondition("SwampLilySeed") || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("ColdBreatherSeed", 1f, () => Immigration.CycleCondition(24) && (Immigration.DiscoveredCondition("ColdBreatherSeed") || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("SpiceVineSeed", 1f, () => Immigration.CycleCondition(24) && (Immigration.DiscoveredCondition("SpiceVineSeed") || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("WormPlantSeed", 1f, () => Immigration.CycleCondition(24) && (Immigration.DiscoveredCondition("WormPlantSeed") || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("SaltPlantSeed", 1f, () => Immigration.CycleCondition(24) && (Immigration.DiscoveredCondition("SaltPlantSeed") || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("FieldRation", 5f, null));
		list.Add(new CarePackageInfo("BasicForagePlant", 6f, () => Immigration.DiscoveredCondition("BasicForagePlant")));
		list.Add(new CarePackageInfo("ForestForagePlant", 2f, () => Immigration.DiscoveredCondition("ForestForagePlant")));
		list.Add(new CarePackageInfo("SwampForagePlant", 2f, () => Immigration.DiscoveredCondition("SwampForagePlant")));
		list.Add(new CarePackageInfo("CookedEgg", 3f, () => Immigration.CycleCondition(6)));
		list.Add(new CarePackageInfo(PrickleFruitConfig.ID, 3f, () => Immigration.CycleCondition(12) && (Immigration.DiscoveredCondition(PrickleFruitConfig.ID) || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("FriedMushroom", 3f, () => Immigration.CycleCondition(24)));
		list.Add(new CarePackageInfo("CookedMeat", 3f, () => Immigration.CycleCondition(48)));
		list.Add(new CarePackageInfo("SpicyTofu", 3f, () => Immigration.CycleCondition(48)));
		list.Add(new CarePackageInfo("WormSuperFood", 2f, () => Immigration.DiscoveredCondition("WormPlantSeed") || Immigration.CycleCondition(500)));
		list.Add(new CarePackageInfo("LightBugBaby", 1f, () => Immigration.DiscoveredCondition("LightBugEgg") || Immigration.CycleCondition(500)));
		list.Add(new CarePackageInfo("HatchBaby", 1f, () => Immigration.DiscoveredCondition("HatchEgg") || Immigration.CycleCondition(500)));
		list.Add(new CarePackageInfo("PuftBaby", 1f, () => Immigration.DiscoveredCondition("PuftEgg") || Immigration.CycleCondition(500)));
		list.Add(new CarePackageInfo("SquirrelBaby", 1f, () => Immigration.DiscoveredCondition("SquirrelEgg") || Immigration.CycleCondition(24) || Immigration.CycleCondition(500)));
		list.Add(new CarePackageInfo("CrabBaby", 1f, () => Immigration.DiscoveredCondition("CrabEgg") || Immigration.CycleCondition(500)));
		list.Add(new CarePackageInfo("DreckoBaby", 1f, () => Immigration.CycleCondition(24) && (Immigration.DiscoveredCondition("DreckoEgg") || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("Pacu", 8f, () => Immigration.CycleCondition(24) && (Immigration.DiscoveredCondition("PacuEgg") || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("MoleBaby", 1f, () => Immigration.CycleCondition(48) && (Immigration.DiscoveredCondition("MoleEgg") || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("OilfloaterBaby", 1f, () => Immigration.CycleCondition(48) && (Immigration.DiscoveredCondition("OilfloaterEgg") || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("DivergentBeetleBaby", 1f, () => Immigration.CycleCondition(48) && (Immigration.DiscoveredCondition("DivergentBeetleEgg") || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("StaterpillarBaby", 1f, () => Immigration.CycleCondition(48) && (Immigration.DiscoveredCondition("StaterpillarEgg") || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("LightBugEgg", 3f, () => Immigration.DiscoveredCondition("LightBugEgg") || Immigration.CycleCondition(500)));
		list.Add(new CarePackageInfo("HatchEgg", 3f, () => Immigration.DiscoveredCondition("HatchEgg") || Immigration.CycleCondition(500)));
		list.Add(new CarePackageInfo("PuftEgg", 3f, () => Immigration.DiscoveredCondition("PuftEgg") || Immigration.CycleCondition(500)));
		list.Add(new CarePackageInfo("OilfloaterEgg", 3f, () => Immigration.CycleCondition(12) && (Immigration.DiscoveredCondition("OilfloaterEgg") || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("MoleEgg", 3f, () => Immigration.CycleCondition(24) && (Immigration.DiscoveredCondition("MoleEgg") || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("DreckoEgg", 3f, () => Immigration.CycleCondition(24) && (Immigration.DiscoveredCondition("DreckoEgg") || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("SquirrelEgg", 2f, () => Immigration.DiscoveredCondition("SquirrelEgg") || Immigration.CycleCondition(24) || Immigration.CycleCondition(500)));
		list.Add(new CarePackageInfo("DivergentBeetleEgg", 2f, () => Immigration.CycleCondition(48) && (Immigration.DiscoveredCondition("DivergentBeetleEgg") || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("StaterpillarEgg", 2f, () => Immigration.CycleCondition(48) && (Immigration.DiscoveredCondition("StaterpillarEgg") || Immigration.CycleCondition(500))));
		list.Add(new CarePackageInfo("BasicCure", 3f, null));
		list.Add(new CarePackageInfo("CustomClothing", 1f, null, "SELECTRANDOM"));
		list.Add(new CarePackageInfo("Funky_Vest", 1f, null));
		this.carePackages = list;
	}

	// Token: 0x060044E4 RID: 17636 RVA: 0x0018D09F File Offset: 0x0018B29F
	private static bool CycleCondition(int cycle)
	{
		return GameClock.Instance.GetCycle() >= cycle;
	}

	// Token: 0x060044E5 RID: 17637 RVA: 0x0018D0B1 File Offset: 0x0018B2B1
	private static bool DiscoveredCondition(Tag tag)
	{
		return DiscoveredResources.Instance.IsDiscovered(tag);
	}

	// Token: 0x060044E6 RID: 17638 RVA: 0x0018D0C0 File Offset: 0x0018B2C0
	private static bool HasMinionModelCondition(Tag model)
	{
		Components.Cmps<MinionIdentity> cmps;
		return Components.LiveMinionIdentitiesByModel.TryGetValue(model, out cmps) && cmps.Count > 0;
	}

	// Token: 0x170004EE RID: 1262
	// (get) Token: 0x060044E7 RID: 17639 RVA: 0x0018D0E7 File Offset: 0x0018B2E7
	public bool ImmigrantsAvailable
	{
		get
		{
			return this.bImmigrantAvailable;
		}
	}

	// Token: 0x060044E8 RID: 17640 RVA: 0x0018D0F0 File Offset: 0x0018B2F0
	public int EndImmigration()
	{
		this.bImmigrantAvailable = false;
		this.spawnIdx++;
		int num = Math.Min(this.spawnIdx, this.spawnInterval.Length - 1);
		this.timeBeforeSpawn = this.spawnInterval[num];
		return this.spawnTable[num];
	}

	// Token: 0x060044E9 RID: 17641 RVA: 0x0018D13E File Offset: 0x0018B33E
	public float GetTimeRemaining()
	{
		return this.timeBeforeSpawn;
	}

	// Token: 0x060044EA RID: 17642 RVA: 0x0018D148 File Offset: 0x0018B348
	public float GetTotalWaitTime()
	{
		int num = Math.Min(this.spawnIdx, this.spawnInterval.Length - 1);
		return this.spawnInterval[num];
	}

	// Token: 0x060044EB RID: 17643 RVA: 0x0018D174 File Offset: 0x0018B374
	public void Sim200ms(float dt)
	{
		if (this.IsHalted() || this.bImmigrantAvailable)
		{
			return;
		}
		this.timeBeforeSpawn -= dt;
		this.timeBeforeSpawn = Math.Max(this.timeBeforeSpawn, 0f);
		if (this.timeBeforeSpawn <= 0f)
		{
			this.bImmigrantAvailable = true;
		}
	}

	// Token: 0x060044EC RID: 17644 RVA: 0x0018D1CC File Offset: 0x0018B3CC
	private bool IsHalted()
	{
		foreach (Telepad telepad in Components.Telepads.Items)
		{
			Operational component = telepad.GetComponent<Operational>();
			if (component != null && component.IsOperational)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060044ED RID: 17645 RVA: 0x0018D23C File Offset: 0x0018B43C
	public int GetPersonalPriority(ChoreGroup group)
	{
		int num;
		if (!this.defaultPersonalPriorities.TryGetValue(group.IdHash, out num))
		{
			num = 3;
		}
		return num;
	}

	// Token: 0x060044EE RID: 17646 RVA: 0x0018D264 File Offset: 0x0018B464
	public CarePackageInfo RandomCarePackage()
	{
		List<CarePackageInfo> list = new List<CarePackageInfo>();
		foreach (CarePackageInfo carePackageInfo in this.carePackages)
		{
			if (carePackageInfo.requirement == null || carePackageInfo.requirement())
			{
				list.Add(carePackageInfo);
			}
		}
		return list[global::UnityEngine.Random.Range(0, list.Count)];
	}

	// Token: 0x060044EF RID: 17647 RVA: 0x0018D2E4 File Offset: 0x0018B4E4
	public void SetPersonalPriority(ChoreGroup group, int value)
	{
		this.defaultPersonalPriorities[group.IdHash] = value;
	}

	// Token: 0x060044F0 RID: 17648 RVA: 0x0018D2F8 File Offset: 0x0018B4F8
	public int GetAssociatedSkillLevel(ChoreGroup group)
	{
		return 0;
	}

	// Token: 0x060044F1 RID: 17649 RVA: 0x0018D2FC File Offset: 0x0018B4FC
	public void ApplyDefaultPersonalPriorities(GameObject minion)
	{
		IPersonalPriorityManager instance = Immigration.Instance;
		IPersonalPriorityManager component = minion.GetComponent<ChoreConsumer>();
		foreach (ChoreGroup choreGroup in Db.Get().ChoreGroups.resources)
		{
			int personalPriority = instance.GetPersonalPriority(choreGroup);
			component.SetPersonalPriority(choreGroup, personalPriority);
		}
	}

	// Token: 0x060044F2 RID: 17650 RVA: 0x0018D370 File Offset: 0x0018B570
	public void ResetPersonalPriorities()
	{
		bool advancedPersonalPriorities = Game.Instance.advancedPersonalPriorities;
		foreach (ChoreGroup choreGroup in Db.Get().ChoreGroups.resources)
		{
			this.defaultPersonalPriorities[choreGroup.IdHash] = (advancedPersonalPriorities ? choreGroup.DefaultPersonalPriority : 3);
		}
	}

	// Token: 0x060044F3 RID: 17651 RVA: 0x0018D3F0 File Offset: 0x0018B5F0
	public bool IsChoreGroupDisabled(ChoreGroup g)
	{
		return false;
	}

	// Token: 0x04002E14 RID: 11796
	public float[] spawnInterval;

	// Token: 0x04002E15 RID: 11797
	public int[] spawnTable;

	// Token: 0x04002E16 RID: 11798
	[Serialize]
	private Dictionary<HashedString, int> defaultPersonalPriorities = new Dictionary<HashedString, int>();

	// Token: 0x04002E17 RID: 11799
	[Serialize]
	public float timeBeforeSpawn = float.PositiveInfinity;

	// Token: 0x04002E18 RID: 11800
	[Serialize]
	private bool bImmigrantAvailable;

	// Token: 0x04002E19 RID: 11801
	[Serialize]
	private int spawnIdx;

	// Token: 0x04002E1A RID: 11802
	private List<CarePackageInfo> carePackages;

	// Token: 0x04002E1B RID: 11803
	private Dictionary<string, List<CarePackageInfo>> carePackagesByDlc;

	// Token: 0x04002E1C RID: 11804
	public static Immigration Instance;

	// Token: 0x04002E1D RID: 11805
	private const int CYCLE_THRESHOLD_A = 6;

	// Token: 0x04002E1E RID: 11806
	private const int CYCLE_THRESHOLD_B = 12;

	// Token: 0x04002E1F RID: 11807
	private const int CYCLE_THRESHOLD_C = 24;

	// Token: 0x04002E20 RID: 11808
	private const int CYCLE_THRESHOLD_D = 48;

	// Token: 0x04002E21 RID: 11809
	private const int CYCLE_THRESHOLD_E = 100;

	// Token: 0x04002E22 RID: 11810
	private const int CYCLE_THRESHOLD_UNLOCK_EVERYTHING = 500;

	// Token: 0x04002E23 RID: 11811
	public const string FACADE_SELECT_RANDOM = "SELECTRANDOM";
}
