using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000181 RID: 385
public class ColdWheatConfig : IEntityConfig
{
	// Token: 0x06000756 RID: 1878 RVA: 0x00032A84 File Offset: 0x00030C84
	public GameObject CreatePrefab()
	{
		string text = "ColdWheat";
		string text2 = global::STRINGS.CREATURES.SPECIES.COLDWHEAT.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.COLDWHEAT.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("coldwheat_kanim"), "idle_empty", Grid.SceneLayer.BuildingFront, 1, 1, tier, default(EffectorValues), SimHashes.Creature, null, 255f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 118.149994f, 218.15f, 278.15f, 358.15f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, "ColdWheatSeed", true, true, true, true, 2400f, 0f, 12200f, "ColdWheatOriginal", global::STRINGS.CREATURES.SPECIES.COLDWHEAT.NAME);
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.Dirt,
				massConsumptionRate = 0.008333334f
			}
		});
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.Water,
				massConsumptionRate = 0.033333335f
			}
		});
		gameObject.AddOrGet<StandardCropPlant>();
		GameObject gameObject2 = gameObject;
		IHasDlcRestrictions hasDlcRestrictions = this as IHasDlcRestrictions;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Crop;
		string text4 = "ColdWheatSeed";
		string text5 = global::STRINGS.CREATURES.SPECIES.SEEDS.COLDWHEAT.NAME;
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.COLDWHEAT.DESC;
		KAnimFile anim = Assets.GetAnim("seed_coldwheat_kanim");
		string text7 = "object";
		int num2 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		string text8 = global::STRINGS.CREATURES.SPECIES.COLDWHEAT.DOMESTICATEDDESC;
		GameObject gameObject3 = EntityTemplates.CreateAndRegisterSeedForPlant(gameObject2, hasDlcRestrictions, productionType, text4, text5, text6, anim, text7, num2, list, receptacleDirection, default(Tag), 3, text8, EntityTemplates.CollisionShape.CIRCLE, 0.2f, 0.2f, null, "", true);
		EntityTemplates.ExtendEntityToFood(gameObject3, FOOD.FOOD_TYPES.COLD_WHEAT_SEED);
		EntityTemplates.CreateAndRegisterPreviewForPlant(gameObject3, "ColdWheat_preview", Assets.GetAnim("coldwheat_kanim"), "place", 1, 1);
		SoundEventVolumeCache.instance.AddVolume("coldwheat_kanim", "ColdWheat_grow", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("coldwheat_kanim", "ColdWheat_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		return gameObject;
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x00032C8E File Offset: 0x00030E8E
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000758 RID: 1880 RVA: 0x00032C90 File Offset: 0x00030E90
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000582 RID: 1410
	public const string ID = "ColdWheat";

	// Token: 0x04000583 RID: 1411
	public const string SEED_ID = "ColdWheatSeed";

	// Token: 0x04000584 RID: 1412
	public const float FERTILIZATION_RATE = 0.008333334f;

	// Token: 0x04000585 RID: 1413
	public const float WATER_RATE = 0.033333335f;
}
