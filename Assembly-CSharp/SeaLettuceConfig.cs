using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001A9 RID: 425
public class SeaLettuceConfig : IEntityConfig
{
	// Token: 0x06000831 RID: 2097 RVA: 0x000378D4 File Offset: 0x00035AD4
	public GameObject CreatePrefab()
	{
		string id = SeaLettuceConfig.ID;
		string text = global::STRINGS.CREATURES.SPECIES.SEALETTUCE.NAME;
		string text2 = global::STRINGS.CREATURES.SPECIES.SEALETTUCE.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.BONUS.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, text, text2, num, Assets.GetAnim("sea_lettuce_kanim"), "idle_empty", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 308.15f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 248.15f, 295.15f, 338.15f, 398.15f, new SimHashes[]
		{
			SimHashes.Water,
			SimHashes.SaltWater,
			SimHashes.Brine
		}, false, 0f, 0.15f, "Lettuce", true, true, true, true, 2400f, 0f, 7400f, SeaLettuceConfig.ID + "Original", global::STRINGS.CREATURES.SPECIES.SEALETTUCE.NAME);
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.SaltWater.CreateTag(),
				massConsumptionRate = 0.008333334f
			}
		});
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.BleachStone.CreateTag(),
				massConsumptionRate = 0.00083333335f
			}
		});
		gameObject.GetComponent<DrowningMonitor>().canDrownToDeath = false;
		gameObject.GetComponent<DrowningMonitor>().livesUnderWater = true;
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<LoopingSounds>();
		GameObject gameObject2 = gameObject;
		IHasDlcRestrictions hasDlcRestrictions = this as IHasDlcRestrictions;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Harvest;
		string text3 = "SeaLettuceSeed";
		string text4 = global::STRINGS.CREATURES.SPECIES.SEEDS.SEALETTUCE.NAME;
		string text5 = global::STRINGS.CREATURES.SPECIES.SEEDS.SEALETTUCE.DESC;
		KAnimFile anim = Assets.GetAnim("seed_sealettuce_kanim");
		string text6 = "object";
		int num2 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.WaterSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		string text7 = global::STRINGS.CREATURES.SPECIES.SEALETTUCE.DOMESTICATEDDESC;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject2, hasDlcRestrictions, productionType, text3, text4, text5, anim, text6, num2, list, receptacleDirection, default(Tag), 3, text7, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, null, "", false), SeaLettuceConfig.ID + "_preview", Assets.GetAnim("sea_lettuce_kanim"), "place", 1, 2);
		SoundEventVolumeCache.instance.AddVolume("sea_lettuce_kanim", "SeaLettuce_grow", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("sea_lettuce_kanim", "SeaLettuce_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		return gameObject;
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x00037B0F File Offset: 0x00035D0F
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x00037B11 File Offset: 0x00035D11
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400061B RID: 1563
	public static string ID = "SeaLettuce";

	// Token: 0x0400061C RID: 1564
	public const string SEED_ID = "SeaLettuceSeed";

	// Token: 0x0400061D RID: 1565
	public const float WATER_RATE = 0.008333334f;

	// Token: 0x0400061E RID: 1566
	public const float FERTILIZATION_RATE = 0.00083333335f;
}
