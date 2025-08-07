using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001BB RID: 443
public class WormPlantConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060008E0 RID: 2272 RVA: 0x0003BE3E File Offset: 0x0003A03E
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060008E1 RID: 2273 RVA: 0x0003BE45 File Offset: 0x0003A045
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060008E2 RID: 2274 RVA: 0x0003BE48 File Offset: 0x0003A048
	public static GameObject BaseWormPlant(string id, string name, string desc, string animFile, EffectorValues decor, string cropID)
	{
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(id, name, desc, 1f, Assets.GetAnim(animFile), "idle_empty", Grid.SceneLayer.BuildingBack, 1, 2, decor, default(EffectorValues), SimHashes.Creature, null, 307.15f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 273.15f, 288.15f, 323.15f, 373.15f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, cropID, true, true, true, true, 2400f, 0f, 9800f, id + "Original", name);
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.Sulfur.CreateTag(),
				massConsumptionRate = 0.016666668f
			}
		});
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<LoopingSounds>();
		return gameObject;
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x0003BF34 File Offset: 0x0003A134
	public GameObject CreatePrefab()
	{
		GameObject gameObject = WormPlantConfig.BaseWormPlant("WormPlant", global::STRINGS.CREATURES.SPECIES.WORMPLANT.NAME, global::STRINGS.CREATURES.SPECIES.WORMPLANT.DESC, "wormwood_kanim", WormPlantConfig.BASIC_DECOR, "WormBasicFruit");
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Harvest;
		string text = "WormPlantSeed";
		string text2 = global::STRINGS.CREATURES.SPECIES.SEEDS.WORMPLANT.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.SEEDS.WORMPLANT.DESC;
		KAnimFile anim = Assets.GetAnim("seed_wormwood_kanim");
		string text4 = "object";
		int num = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		string text5 = global::STRINGS.CREATURES.SPECIES.WORMPLANT.DOMESTICATEDDESC;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject, this, productionType, text, text2, text3, anim, text4, num, list, receptacleDirection, default(Tag), 3, text5, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), "WormPlant_preview", Assets.GetAnim("wormwood_kanim"), "place", 1, 2);
		return gameObject;
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x0003C000 File Offset: 0x0003A200
	public void OnPrefabInit(GameObject prefab)
	{
		TransformingPlant transformingPlant = prefab.AddOrGet<TransformingPlant>();
		transformingPlant.transformPlantId = "SuperWormPlant";
		transformingPlant.SubscribeToTransformEvent(GameHashes.CropTended);
		transformingPlant.useGrowthTimeRatio = true;
		transformingPlant.eventDataCondition = delegate(object data)
		{
			CropTendingStates.CropTendingEventData cropTendingEventData = (CropTendingStates.CropTendingEventData)data;
			if (cropTendingEventData != null)
			{
				CreatureBrain component = cropTendingEventData.source.GetComponent<CreatureBrain>();
				if (component != null && component.species == GameTags.Creatures.Species.DivergentSpecies)
				{
					return true;
				}
			}
			return false;
		};
		transformingPlant.fxKAnim = "plant_transform_fx_kanim";
		transformingPlant.fxAnim = "plant_transform";
		prefab.AddOrGet<StandardCropPlant>().anims = WormPlantConfig.animSet;
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x0003C07A File Offset: 0x0003A27A
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400068A RID: 1674
	public const string ID = "WormPlant";

	// Token: 0x0400068B RID: 1675
	public const string SEED_ID = "WormPlantSeed";

	// Token: 0x0400068C RID: 1676
	public const float SULFUR_CONSUMPTION_RATE = 0.016666668f;

	// Token: 0x0400068D RID: 1677
	public static readonly EffectorValues BASIC_DECOR = DECOR.PENALTY.TIER0;

	// Token: 0x0400068E RID: 1678
	public const string BASIC_CROP_ID = "WormBasicFruit";

	// Token: 0x0400068F RID: 1679
	private static StandardCropPlant.AnimSet animSet = new StandardCropPlant.AnimSet
	{
		grow = "basic_grow",
		grow_pst = "basic_grow_pst",
		idle_full = "basic_idle_full",
		wilt_base = "basic_wilt",
		harvest = "basic_harvest"
	};
}
