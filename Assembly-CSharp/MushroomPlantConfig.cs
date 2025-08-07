using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001A1 RID: 417
public class MushroomPlantConfig : IEntityConfig
{
	// Token: 0x0600080B RID: 2059 RVA: 0x000369A4 File Offset: 0x00034BA4
	public GameObject CreatePrefab()
	{
		string text = "MushroomPlant";
		string text2 = global::STRINGS.CREATURES.SPECIES.MUSHROOMPLANT.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.MUSHROOMPLANT.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("fungusplant_kanim"), "idle_empty", Grid.SceneLayer.BuildingFront, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 228.15f, 278.15f, 308.15f, 398.15f, new SimHashes[] { SimHashes.CarbonDioxide }, true, 0f, 0.15f, MushroomConfig.ID, true, true, true, true, 2400f, 0f, 4600f, "MushroomPlantOriginal", global::STRINGS.CREATURES.SPECIES.MUSHROOMPLANT.NAME);
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.SlimeMold,
				massConsumptionRate = 0.006666667f
			}
		});
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<IlluminationVulnerable>().SetPrefersDarkness(true);
		GameObject gameObject2 = gameObject;
		IHasDlcRestrictions hasDlcRestrictions = this as IHasDlcRestrictions;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Harvest;
		string text4 = "MushroomSeed";
		string text5 = global::STRINGS.CREATURES.SPECIES.SEEDS.MUSHROOMPLANT.NAME;
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.MUSHROOMPLANT.DESC;
		KAnimFile anim = Assets.GetAnim("seed_fungusplant_kanim");
		string text7 = "object";
		int num2 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		string text8 = global::STRINGS.CREATURES.SPECIES.MUSHROOMPLANT.DOMESTICATEDDESC;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject2, hasDlcRestrictions, productionType, text4, text5, text6, anim, text7, num2, list, receptacleDirection, default(Tag), 3, text8, EntityTemplates.CollisionShape.CIRCLE, 0.33f, 0.33f, null, "", false), "MushroomPlant_preview", Assets.GetAnim("fungusplant_kanim"), "place", 1, 2);
		SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("bristleblossom_kanim", "PrickleFlower_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		return gameObject;
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x00036B76 File Offset: 0x00034D76
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x00036B78 File Offset: 0x00034D78
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005FD RID: 1533
	public const float FERTILIZATION_RATE = 0.006666667f;

	// Token: 0x040005FE RID: 1534
	public const string ID = "MushroomPlant";

	// Token: 0x040005FF RID: 1535
	public const string SEED_ID = "MushroomSeed";
}
