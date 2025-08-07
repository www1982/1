using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000194 RID: 404
public class GardenForagePlantPlantedConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060007C4 RID: 1988 RVA: 0x00034C08 File Offset: 0x00032E08
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x060007C5 RID: 1989 RVA: 0x00034C0F File Offset: 0x00032E0F
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060007C6 RID: 1990 RVA: 0x00034C14 File Offset: 0x00032E14
	public GameObject CreatePrefab()
	{
		string text = "GardenForagePlantPlanted";
		string text2 = global::STRINGS.CREATURES.SPECIES.GARDENFORAGEPLANTPLANTED.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.GARDENFORAGEPLANTPLANTED.DESC;
		float num = 100f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("fatplant_kanim"), "idle", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		gameObject.AddOrGet<SimTemperatureTransfer>();
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<EntombVulnerable>();
		gameObject.AddOrGet<DrowningMonitor>();
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddOrGet<Uprootable>();
		gameObject.AddOrGet<UprootedMonitor>();
		gameObject.AddOrGet<Harvestable>();
		gameObject.AddOrGet<HarvestDesignatable>();
		gameObject.AddOrGet<SeedProducer>().Configure("GardenForagePlant", SeedProducer.ProductionType.DigOnly, 1);
		gameObject.AddOrGet<BasicForagePlantPlanted>();
		gameObject.AddOrGet<KBatchedAnimController>().randomiseLoopedOffset = true;
		return gameObject;
	}

	// Token: 0x060007C7 RID: 1991 RVA: 0x00034CEB File Offset: 0x00032EEB
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x00034CED File Offset: 0x00032EED
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005BD RID: 1469
	public const string ID = "GardenForagePlantPlanted";
}
