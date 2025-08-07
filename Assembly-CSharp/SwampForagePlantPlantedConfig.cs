using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001B2 RID: 434
public class SwampForagePlantPlantedConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060008A9 RID: 2217 RVA: 0x0003AB28 File Offset: 0x00038D28
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x0003AB2F File Offset: 0x00038D2F
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x0003AB34 File Offset: 0x00038D34
	public GameObject CreatePrefab()
	{
		string text = "SwampForagePlantPlanted";
		string text2 = global::STRINGS.CREATURES.SPECIES.SWAMPFORAGEPLANTPLANTED.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.SWAMPFORAGEPLANTPLANTED.DESC;
		float num = 100f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("swamptuber_kanim"), "idle", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		gameObject.AddOrGet<SimTemperatureTransfer>();
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<EntombVulnerable>();
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddOrGet<Uprootable>();
		gameObject.AddOrGet<UprootedMonitor>();
		gameObject.AddOrGet<Harvestable>();
		gameObject.AddOrGet<HarvestDesignatable>();
		gameObject.AddOrGet<SeedProducer>().Configure("SwampForagePlant", SeedProducer.ProductionType.DigOnly, 1);
		gameObject.AddOrGet<BasicForagePlantPlanted>();
		gameObject.AddOrGet<KBatchedAnimController>().randomiseLoopedOffset = true;
		return gameObject;
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x0003AC04 File Offset: 0x00038E04
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x0003AC06 File Offset: 0x00038E06
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000663 RID: 1635
	public const string ID = "SwampForagePlantPlanted";
}
