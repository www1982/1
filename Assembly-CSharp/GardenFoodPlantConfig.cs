using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000191 RID: 401
public class GardenFoodPlantConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060007B2 RID: 1970 RVA: 0x00034918 File Offset: 0x00032B18
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x0003491F File Offset: 0x00032B1F
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x00034924 File Offset: 0x00032B24
	public GameObject CreatePrefab()
	{
		string text = "GardenFoodPlant";
		string text2 = global::STRINGS.CREATURES.SPECIES.GARDENFOODPLANT.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.GARDENFOODPLANT.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.PENALTY.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("spike_fruit_kanim"), "idle_empty", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 263.15f, 268.15f, 313.15f, 323.15f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, "GardenFoodPlantFood", true, true, true, true, 2400f, 0f, 4600f, "GardenFoodPlantOriginal", global::STRINGS.CREATURES.SPECIES.GARDENFOODPLANT.NAME);
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<DirectlyEdiblePlant_Growth>();
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGetDef<PollinationMonitor.Def>();
		GameObject gameObject2 = gameObject;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Harvest;
		string text4 = "GardenFoodPlantSeed";
		string text5 = global::STRINGS.CREATURES.SPECIES.SEEDS.GARDENFOODPLANT.NAME;
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.GARDENFOODPLANT.DESC;
		KAnimFile anim = Assets.GetAnim("seed_spikefruit_kanim");
		string text7 = "object";
		int num2 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		string text8 = global::STRINGS.CREATURES.SPECIES.GARDENFOODPLANT.DOMESTICATEDDESC;
		GameObject gameObject3 = EntityTemplates.CreateAndRegisterSeedForPlant(gameObject2, this, productionType, text4, text5, text6, anim, text7, num2, list, receptacleDirection, default(Tag), 1, text8, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false);
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.Peat.CreateTag(),
				massConsumptionRate = 0.016666668f
			}
		});
		EntityTemplates.CreateAndRegisterPreviewForPlant(gameObject3, "GardenFoodPlant_preview", Assets.GetAnim("spike_fruit_kanim"), "place", 1, 2);
		SoundEventVolumeCache.instance.AddVolume("spike_fruit_kanim", "spike_fruit_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("spike_fruit_kanim", "spike_fruit_LP", NOISE_POLLUTION.CREATURES.TIER4);
		return gameObject;
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x00034B05 File Offset: 0x00032D05
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x00034B07 File Offset: 0x00032D07
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005B9 RID: 1465
	public const string ID = "GardenFoodPlant";

	// Token: 0x040005BA RID: 1466
	public const string SEED_ID = "GardenFoodPlantSeed";
}
