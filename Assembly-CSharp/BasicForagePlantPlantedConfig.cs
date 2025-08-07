using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000177 RID: 375
public class BasicForagePlantPlantedConfig : IEntityConfig
{
	// Token: 0x06000727 RID: 1831 RVA: 0x000317E8 File Offset: 0x0002F9E8
	public GameObject CreatePrefab()
	{
		string text = "BasicForagePlantPlanted";
		string text2 = global::STRINGS.CREATURES.SPECIES.BASICFORAGEPLANTPLANTED.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.BASICFORAGEPLANTPLANTED.DESC;
		float num = 100f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("muckroot_kanim"), "idle", Grid.SceneLayer.BuildingBack, 1, 1, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		gameObject.AddOrGet<SimTemperatureTransfer>();
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<EntombVulnerable>();
		gameObject.AddOrGet<DrowningMonitor>();
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddOrGet<Uprootable>();
		gameObject.AddOrGet<UprootedMonitor>();
		gameObject.AddOrGet<Harvestable>();
		gameObject.AddOrGet<HarvestDesignatable>();
		gameObject.AddOrGet<SeedProducer>().Configure("BasicForagePlant", SeedProducer.ProductionType.DigOnly, 1);
		gameObject.AddOrGet<BasicForagePlantPlanted>();
		gameObject.AddOrGet<KBatchedAnimController>().randomiseLoopedOffset = true;
		return gameObject;
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x000318BF File Offset: 0x0002FABF
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x000318C1 File Offset: 0x0002FAC1
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400055B RID: 1371
	public const string ID = "BasicForagePlantPlanted";
}
