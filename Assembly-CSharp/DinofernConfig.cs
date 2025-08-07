using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000186 RID: 390
public class DinofernConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000775 RID: 1909 RVA: 0x00033366 File Offset: 0x00031566
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x0003336D File Offset: 0x0003156D
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000777 RID: 1911 RVA: 0x00033370 File Offset: 0x00031570
	public GameObject CreatePrefab()
	{
		string text = "Dinofern";
		string text2 = global::STRINGS.CREATURES.SPECIES.DINOFERN.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.DINOFERN.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.PENALTY.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("prehistoric_fern_kanim"), "idle_full", Grid.SceneLayer.BuildingBack, 3, 3, tier, default(EffectorValues), SimHashes.Creature, null, 253.15f);
		float num2 = 218.15f;
		float num3 = 228.15f;
		float num4 = 288.15f;
		float num5 = 308.15f;
		string text4 = FernFoodConfig.ID;
		GameObject gameObject2 = EntityTemplates.ExtendEntityToBasicPlant(gameObject, num2, num3, num4, num5, new SimHashes[] { SimHashes.ChlorineGas }, true, 0f, 0.5f, text4, true, false, true, true, 2400f, 0f, 2200f, "DinofernOriginal", global::STRINGS.CREATURES.SPECIES.DINOFERN.NAME);
		gameObject2.AddOrGet<LoopingSounds>();
		gameObject2.AddOrGet<StandardCropPlant>();
		gameObject2.AddOrGet<Dinofern>();
		Storage storage = gameObject2.AddOrGet<Storage>();
		storage.showInUI = false;
		storage.capacityKg = 1f;
		ElementConsumer elementConsumer = gameObject2.AddOrGet<ElementConsumer>();
		elementConsumer.showInStatusPanel = true;
		elementConsumer.storeOnConsume = false;
		elementConsumer.elementToConsume = SimHashes.ChlorineGas;
		elementConsumer.configuration = ElementConsumer.Configuration.Element;
		elementConsumer.consumptionRadius = 4;
		elementConsumer.EnableConsumption(false);
		elementConsumer.sampleCellOffset = new Vector3(0f, 0f);
		elementConsumer.consumptionRate = 0.09f;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Hidden;
		string text5 = "DinofernSeed";
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.DINOFERN.NAME;
		string text7 = global::STRINGS.CREATURES.SPECIES.SEEDS.DINOFERN.DESC;
		KAnimFile anim = Assets.GetAnim("seed_megafrond_kanim");
		string text8 = "object";
		int num6 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		text4 = global::STRINGS.CREATURES.SPECIES.DINOFERN.DOMESTICATEDDESC;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject2, this, productionType, text5, text6, text7, anim, text8, num6, list, receptacleDirection, default(Tag), 20, text4, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), "Dinofern_preview", Assets.GetAnim("prehistoric_fern_kanim"), "place", 3, 3);
		SoundEventVolumeCache.instance.AddVolume("oxy_fern_kanim", "MealLice_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("oxy_fern_kanim", "MealLice_LP", NOISE_POLLUTION.CREATURES.TIER4);
		return gameObject2;
	}

	// Token: 0x06000778 RID: 1912 RVA: 0x00033570 File Offset: 0x00031770
	public void OnPrefabInit(GameObject prefab)
	{
		prefab.AddOrGet<StandardCropPlant>().anims = new StandardCropPlant.AnimSet
		{
			pre_grow = "expand",
			grow = "grow",
			grow_pst = "grow_pst",
			idle_full = "idle_full",
			wilt_base = "wilt",
			harvest = "harvest",
			waning = "waning"
		};
	}

	// Token: 0x06000779 RID: 1913 RVA: 0x000335DA File Offset: 0x000317DA
	public void OnSpawn(GameObject inst)
	{
		inst.GetComponent<Dinofern>().SetConsumptionRate();
	}

	// Token: 0x04000595 RID: 1429
	public const string ID = "Dinofern";

	// Token: 0x04000596 RID: 1430
	public const string SEED_ID = "DinofernSeed";

	// Token: 0x04000597 RID: 1431
	public const float CHLORINE_CONSUMPTION_RATE = 0.09f;
}
