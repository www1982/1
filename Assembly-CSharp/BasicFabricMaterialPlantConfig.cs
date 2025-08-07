using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000175 RID: 373
public class BasicFabricMaterialPlantConfig : IEntityConfig
{
	// Token: 0x0600071E RID: 1822 RVA: 0x00031568 File Offset: 0x0002F768
	public GameObject CreatePrefab()
	{
		string id = BasicFabricMaterialPlantConfig.ID;
		string text = global::STRINGS.CREATURES.SPECIES.BASICFABRICMATERIALPLANT.NAME;
		string text2 = global::STRINGS.CREATURES.SPECIES.BASICFABRICMATERIALPLANT.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.BONUS.TIER0;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, text, text2, num, Assets.GetAnim("swampreed_kanim"), "idle_empty", Grid.SceneLayer.BuildingBack, 1, 3, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		GameObject gameObject2 = gameObject;
		float num2 = 248.15f;
		float num3 = 295.15f;
		float num4 = 310.15f;
		float num5 = 398.15f;
		string text3 = BasicFabricConfig.ID;
		EntityTemplates.ExtendEntityToBasicPlant(gameObject2, num2, num3, num4, num5, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide,
			SimHashes.DirtyWater,
			SimHashes.Water
		}, false, 0f, 0.15f, text3, false, true, true, true, 2400f, 0f, 4600f, BasicFabricMaterialPlantConfig.ID + "Original", global::STRINGS.CREATURES.SPECIES.BASICFABRICMATERIALPLANT.NAME);
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.DirtyWater,
				massConsumptionRate = 0.26666668f
			}
		});
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<DirectlyEdiblePlant_Growth>();
		gameObject.AddOrGet<LoopingSounds>();
		GameObject gameObject3 = gameObject;
		IHasDlcRestrictions hasDlcRestrictions = this as IHasDlcRestrictions;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Harvest;
		string seed_ID = BasicFabricMaterialPlantConfig.SEED_ID;
		string text4 = global::STRINGS.CREATURES.SPECIES.SEEDS.BASICFABRICMATERIALPLANT.NAME;
		string text5 = global::STRINGS.CREATURES.SPECIES.SEEDS.BASICFABRICMATERIALPLANT.DESC;
		KAnimFile anim = Assets.GetAnim("seed_swampreed_kanim");
		string text6 = "object";
		int num6 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.WaterSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		text3 = global::STRINGS.CREATURES.SPECIES.BASICFABRICMATERIALPLANT.DOMESTICATEDDESC;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject3, hasDlcRestrictions, productionType, seed_ID, text4, text5, anim, text6, num6, list, receptacleDirection, default(Tag), 20, text3, EntityTemplates.CollisionShape.CIRCLE, 0.25f, 0.25f, null, "", false), BasicFabricMaterialPlantConfig.ID + "_preview", Assets.GetAnim("swampreed_kanim"), "place", 1, 3);
		SoundEventVolumeCache.instance.AddVolume("swampreed_kanim", "FabricPlant_grow", NOISE_POLLUTION.CREATURES.TIER3);
		SoundEventVolumeCache.instance.AddVolume("swampreed_kanim", "FabricPlant_harvest", NOISE_POLLUTION.CREATURES.TIER3);
		return gameObject;
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x00031755 File Offset: 0x0002F955
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x00031757 File Offset: 0x0002F957
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000557 RID: 1367
	public static string ID = "BasicFabricPlant";

	// Token: 0x04000558 RID: 1368
	public static string SEED_ID = "BasicFabricMaterialPlantSeed";

	// Token: 0x04000559 RID: 1369
	public const float WATER_RATE = 0.26666668f;
}
