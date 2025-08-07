using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200019C RID: 412
public class IceCavesForagePlantPlantedConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060007F0 RID: 2032 RVA: 0x000361FC File Offset: 0x000343FC
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x060007F1 RID: 2033 RVA: 0x00036203 File Offset: 0x00034403
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060007F2 RID: 2034 RVA: 0x00036208 File Offset: 0x00034408
	public GameObject CreatePrefab()
	{
		string text = "IceCavesForagePlantPlanted";
		string text2 = global::STRINGS.CREATURES.SPECIES.ICECAVESFORAGEPLANTPLANTED.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.ICECAVESFORAGEPLANTPLANTED.DESC;
		float num = 100f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		KAnimFile anim = Assets.GetAnim("frozenberries_kanim");
		string text4 = "idle";
		Grid.SceneLayer sceneLayer = Grid.SceneLayer.BuildingBack;
		int num2 = 1;
		int num3 = 2;
		EffectorValues effectorValues = tier;
		List<Tag> list = new List<Tag> { GameTags.Hanging };
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, anim, text4, sceneLayer, num2, num3, effectorValues, default(EffectorValues), SimHashes.Creature, list, 253.15f);
		EntityTemplates.MakeHangingOffsets(gameObject, 1, 2);
		gameObject.AddOrGet<SimTemperatureTransfer>();
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		gameObject.AddOrGet<EntombVulnerable>();
		gameObject.AddOrGet<DrowningMonitor>();
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddOrGet<Uprootable>();
		gameObject.AddOrGet<UprootedMonitor>().monitorCells = new CellOffset[]
		{
			new CellOffset(0, 1)
		};
		gameObject.AddOrGet<Harvestable>();
		gameObject.AddOrGet<HarvestDesignatable>();
		gameObject.AddOrGet<SeedProducer>().Configure("IceCavesForagePlant", SeedProducer.ProductionType.DigOnly, 2);
		gameObject.AddOrGet<BasicForagePlantPlanted>();
		gameObject.AddOrGet<KBatchedAnimController>().randomiseLoopedOffset = true;
		return gameObject;
	}

	// Token: 0x060007F3 RID: 2035 RVA: 0x00036311 File Offset: 0x00034511
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x00036313 File Offset: 0x00034513
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005E9 RID: 1513
	public const string ID = "IceCavesForagePlantPlanted";
}
