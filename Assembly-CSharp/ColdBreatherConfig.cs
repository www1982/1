using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000180 RID: 384
public class ColdBreatherConfig : IEntityConfig
{
	// Token: 0x06000751 RID: 1873 RVA: 0x00032764 File Offset: 0x00030964
	public GameObject CreatePrefab()
	{
		string text = "ColdBreather";
		string text2 = global::STRINGS.CREATURES.SPECIES.COLDBREATHER.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.COLDBREATHER.DESC;
		float num = 400f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER2;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("coldbreather_kanim"), "grow_seed", Grid.SceneLayer.BuildingFront, 1, 2, tier, tier2, SimHashes.Creature, null, 293f);
		gameObject.AddOrGet<ReceptacleMonitor>();
		gameObject.AddOrGet<EntombVulnerable>();
		gameObject.AddOrGet<WiltCondition>();
		gameObject.AddOrGet<Prioritizable>();
		gameObject.AddOrGet<Uprootable>();
		gameObject.AddOrGet<UprootedMonitor>();
		gameObject.AddOrGet<DrowningMonitor>();
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.Phosphorite.CreateTag(),
				massConsumptionRate = 0.006666667f
			}
		});
		gameObject.AddOrGet<TemperatureVulnerable>().Configure(213.15f, 183.15f, 368.15f, 463.15f);
		gameObject.AddOrGet<OccupyArea>().objectLayers = new ObjectLayer[] { ObjectLayer.Building };
		ColdBreather coldBreather = gameObject.AddOrGet<ColdBreather>();
		coldBreather.deltaEmitTemperature = -5f;
		coldBreather.emitOffsetCell = new Vector3(0f, 1f);
		coldBreather.consumptionRate = 1f;
		gameObject.AddOrGet<KBatchedAnimController>().randomiseLoopedOffset = true;
		BuildingTemplates.CreateDefaultStorage(gameObject, false).showInUI = false;
		ElementConsumer elementConsumer = gameObject.AddOrGet<ElementConsumer>();
		elementConsumer.storeOnConsume = true;
		elementConsumer.configuration = ElementConsumer.Configuration.AllGas;
		elementConsumer.capacityKG = 2f;
		elementConsumer.consumptionRate = 0.25f;
		elementConsumer.consumptionRadius = 1;
		elementConsumer.sampleCellOffset = new Vector3(0f, 0f);
		SimTemperatureTransfer component = gameObject.GetComponent<SimTemperatureTransfer>();
		component.SurfaceArea = 10f;
		component.Thickness = 0.001f;
		if (DlcManager.FeatureRadiationEnabled())
		{
			RadiationEmitter radiationEmitter = gameObject.AddComponent<RadiationEmitter>();
			radiationEmitter.emitType = RadiationEmitter.RadiationEmitterType.Constant;
			radiationEmitter.radiusProportionalToRads = false;
			radiationEmitter.emitRadiusX = 6;
			radiationEmitter.emitRadiusY = radiationEmitter.emitRadiusX;
			radiationEmitter.emitRads = 480f;
			radiationEmitter.emissionOffset = new Vector3(0f, 0f, 0f);
		}
		GameObject gameObject2 = gameObject;
		IHasDlcRestrictions hasDlcRestrictions = this as IHasDlcRestrictions;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Hidden;
		string text4 = "ColdBreatherSeed";
		string text5 = global::STRINGS.CREATURES.SPECIES.SEEDS.COLDBREATHER.NAME;
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.COLDBREATHER.DESC;
		KAnimFile anim = Assets.GetAnim("seed_coldbreather_kanim");
		string text7 = "object";
		int num2 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		string text8 = global::STRINGS.CREATURES.SPECIES.COLDBREATHER.DOMESTICATEDDESC;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject2, hasDlcRestrictions, productionType, text4, text5, text6, anim, text7, num2, list, receptacleDirection, default(Tag), 21, text8, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), "ColdBreather_preview", Assets.GetAnim("coldbreather_kanim"), "place", 1, 2);
		SoundEventVolumeCache.instance.AddVolume("coldbreather_kanim", "ColdBreather_grow", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("coldbreather_kanim", "ColdBreather_intake", NOISE_POLLUTION.CREATURES.TIER3);
		gameObject.AddOrGet<EntityCellVisualizer>();
		return gameObject;
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x00032A34 File Offset: 0x00030C34
	public void OnPrefabInit(GameObject inst)
	{
		inst.AddOrGet<EntityCellVisualizer>().AddPort(EntityCellVisualizer.Ports.HeatSink, default(CellOffset));
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x00032A5A File Offset: 0x00030C5A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000579 RID: 1401
	public const string ID = "ColdBreather";

	// Token: 0x0400057A RID: 1402
	public static readonly Tag TAG = TagManager.Create("ColdBreather");

	// Token: 0x0400057B RID: 1403
	public const float FERTILIZATION_RATE = 0.006666667f;

	// Token: 0x0400057C RID: 1404
	public const SimHashes FERTILIZER = SimHashes.Phosphorite;

	// Token: 0x0400057D RID: 1405
	public const float TEMP_DELTA = -5f;

	// Token: 0x0400057E RID: 1406
	public const float CONSUMPTION_RATE = 1f;

	// Token: 0x0400057F RID: 1407
	public const float RADIATION_STRENGTH = 480f;

	// Token: 0x04000580 RID: 1408
	public const string SEED_ID = "ColdBreatherSeed";

	// Token: 0x04000581 RID: 1409
	public static readonly Tag SEED_TAG = TagManager.Create("ColdBreatherSeed");
}
