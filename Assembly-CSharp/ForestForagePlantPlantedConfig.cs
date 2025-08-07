using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200018C RID: 396
public class ForestForagePlantPlantedConfig : IEntityConfig
{
	// Token: 0x06000797 RID: 1943 RVA: 0x00033E00 File Offset: 0x00032000
	public GameObject CreatePrefab()
	{
		string text = "ForestForagePlantPlanted";
		string text2 = global::STRINGS.CREATURES.SPECIES.FORESTFORAGEPLANTPLANTED.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.FORESTFORAGEPLANTPLANTED.DESC;
		float num = 100f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("podmelon_kanim"), "idle", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		gameObject.AddOrGet<SimTemperatureTransfer>();
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<EntombVulnerable>();
		gameObject.AddOrGet<DrowningMonitor>();
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddOrGet<Uprootable>();
		gameObject.AddOrGet<UprootedMonitor>();
		gameObject.AddOrGet<Harvestable>();
		gameObject.AddOrGet<HarvestDesignatable>();
		gameObject.AddOrGet<SeedProducer>().Configure("ForestForagePlant", SeedProducer.ProductionType.DigOnly, 1);
		gameObject.AddOrGet<BasicForagePlantPlanted>();
		gameObject.AddOrGet<KBatchedAnimController>().randomiseLoopedOffset = true;
		return gameObject;
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x00033ED7 File Offset: 0x000320D7
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x00033ED9 File Offset: 0x000320D9
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005AB RID: 1451
	public const string ID = "ForestForagePlantPlanted";
}
