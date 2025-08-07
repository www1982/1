using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200017E RID: 382
public class CarrotPlantConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000747 RID: 1863 RVA: 0x00032442 File Offset: 0x00030642
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x00032449 File Offset: 0x00030649
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x0003244C File Offset: 0x0003064C
	public GameObject CreatePrefab()
	{
		string text = "CarrotPlant";
		string text2 = global::STRINGS.CREATURES.SPECIES.CARROTPLANT.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.CARROTPLANT.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.PENALTY.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("purpleroot_kanim"), "idle_empty", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 255f);
		GameObject gameObject2 = gameObject;
		float num2 = 118.149994f;
		float num3 = 218.15f;
		float num4 = 259.15f;
		float num5 = 269.15f;
		string text4 = CarrotConfig.ID;
		EntityTemplates.ExtendEntityToBasicPlant(gameObject2, num2, num3, num4, num5, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, text4, true, true, true, true, 2400f, 0f, 4600f, "CarrotPlantOriginal", global::STRINGS.CREATURES.SPECIES.CARROTPLANT.NAME);
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<DirectlyEdiblePlant_Growth>();
		gameObject.AddOrGet<LoopingSounds>();
		GameObject gameObject3 = gameObject;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Harvest;
		string text5 = "CarrotPlantSeed";
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.CARROTPLANT.NAME;
		string text7 = global::STRINGS.CREATURES.SPECIES.SEEDS.CARROTPLANT.DESC;
		KAnimFile anim = Assets.GetAnim("seed_purpleroot_kanim");
		string text8 = "object";
		int num6 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		text4 = global::STRINGS.CREATURES.SPECIES.CARROTPLANT.DOMESTICATEDDESC;
		GameObject gameObject4 = EntityTemplates.CreateAndRegisterSeedForPlant(gameObject3, this, productionType, text5, text6, text7, anim, text8, num6, list, receptacleDirection, default(Tag), 1, text4, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false);
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.Ethanol.CreateTag(),
				massConsumptionRate = 0.025f
			}
		});
		EntityTemplates.CreateAndRegisterPreviewForPlant(gameObject4, "CarrotPlant_preview", Assets.GetAnim("purpleroot_kanim"), "place", 1, 2);
		SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_grow", NOISE_POLLUTION.CREATURES.TIER3);
		return gameObject;
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x0003262A File Offset: 0x0003082A
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x0003262C File Offset: 0x0003082C
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000571 RID: 1393
	public const string ID = "CarrotPlant";

	// Token: 0x04000572 RID: 1394
	public const string SEED_ID = "CarrotPlantSeed";

	// Token: 0x04000573 RID: 1395
	public const float Temperature_lethal_low = 118.149994f;

	// Token: 0x04000574 RID: 1396
	public const float Temperature_warning_low = 218.15f;

	// Token: 0x04000575 RID: 1397
	public const float Temperature_lethal_high = 269.15f;

	// Token: 0x04000576 RID: 1398
	public const float Temperature_warning_high = 259.15f;

	// Token: 0x04000577 RID: 1399
	public const float FERTILIZATION_RATE = 0.025f;
}
