using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200019A RID: 410
public class HardSkinBerryPlantConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060007E4 RID: 2020 RVA: 0x00035F92 File Offset: 0x00034192
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x00035F99 File Offset: 0x00034199
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x00035F9C File Offset: 0x0003419C
	public GameObject CreatePrefab()
	{
		string text = "HardSkinBerryPlant";
		string text2 = global::STRINGS.CREATURES.SPECIES.HARDSKINBERRYPLANT.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.HARDSKINBERRYPLANT.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.PENALTY.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("ice_berry_bush_kanim"), "idle_empty", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 255f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 118.149994f, 218.15f, 259.15f, 269.15f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, "HardSkinBerry", true, true, true, true, 2400f, 0f, 4600f, "HardSkinBerryPlantOriginal", global::STRINGS.CREATURES.SPECIES.HARDSKINBERRYPLANT.NAME);
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<DirectlyEdiblePlant_Growth>();
		gameObject.AddOrGet<LoopingSounds>();
		GameObject gameObject2 = gameObject;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Harvest;
		string text4 = "HardSkinBerryPlantSeed";
		string text5 = global::STRINGS.CREATURES.SPECIES.SEEDS.HARDSKINBERRYPLANT.NAME;
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.HARDSKINBERRYPLANT.DESC;
		KAnimFile anim = Assets.GetAnim("seed_ice_berry_kanim");
		string text7 = "object";
		int num2 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		string text8 = global::STRINGS.CREATURES.SPECIES.HARDSKINBERRYPLANT.DOMESTICATEDDESC;
		GameObject gameObject3 = EntityTemplates.CreateAndRegisterSeedForPlant(gameObject2, this, productionType, text4, text5, text6, anim, text7, num2, list, receptacleDirection, default(Tag), 1, text8, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false);
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.Phosphorite.CreateTag(),
				massConsumptionRate = 0.008333334f
			}
		});
		EntityTemplates.CreateAndRegisterPreviewForPlant(gameObject3, "HardSkinBerryPlant_preview", Assets.GetAnim("ice_berry_bush_kanim"), "place", 1, 2);
		SoundEventVolumeCache.instance.AddVolume("meallice_kanim", "MealLice_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("meallice_kanim", "MealLice_LP", NOISE_POLLUTION.CREATURES.TIER4);
		return gameObject;
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x00036176 File Offset: 0x00034376
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x00036178 File Offset: 0x00034378
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005E1 RID: 1505
	public const string ID = "HardSkinBerryPlant";

	// Token: 0x040005E2 RID: 1506
	public const string SEED_ID = "HardSkinBerryPlantSeed";

	// Token: 0x040005E3 RID: 1507
	public const float Temperature_lethal_low = 118.149994f;

	// Token: 0x040005E4 RID: 1508
	public const float Temperature_warning_low = 218.15f;

	// Token: 0x040005E5 RID: 1509
	public const float Temperature_lethal_high = 269.15f;

	// Token: 0x040005E6 RID: 1510
	public const float Temperature_warning_high = 259.15f;

	// Token: 0x040005E7 RID: 1511
	public const float FERTILIZATION_RATE = 0.008333334f;
}
