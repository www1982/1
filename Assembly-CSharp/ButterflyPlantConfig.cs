using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200017C RID: 380
public class ButterflyPlantConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600073D RID: 1853 RVA: 0x000320A9 File Offset: 0x000302A9
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x0600073E RID: 1854 RVA: 0x000320B0 File Offset: 0x000302B0
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x000320B4 File Offset: 0x000302B4
	public GameObject CreatePrefab()
	{
		string text = "ButterflyPlant";
		string text2 = global::STRINGS.CREATURES.SPECIES.BUTTERFLYPLANT.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.BUTTERFLYPLANT.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("pollinator_plant_kanim"), "idle_empty", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 233.15f, 283.15f, 318.15f, 353.15f, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide,
			SimHashes.ChlorineGas
		}, true, 0f, 0.15f, "Butterfly", true, true, true, true, 2400f, 0f, 7400f, "ButterflyPlantOriginal", global::STRINGS.CREATURES.SPECIES.BUTTERFLYPLANT.NAME);
		global::UnityEngine.Object.DestroyImmediate(gameObject.GetComponent<MutantPlant>());
		global::UnityEngine.Object.DestroyImmediate(gameObject.GetComponent<HarvestDesignatable>());
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.Dirt,
				massConsumptionRate = 0.016666668f
			}
		});
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<LoopingSounds>();
		GameObject gameObject2 = gameObject;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Crop;
		string text4 = "ButterflyPlantSeed";
		string text5 = global::STRINGS.CREATURES.SPECIES.SEEDS.BUTTERFLYPLANTSEED.NAME;
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.BUTTERFLYPLANTSEED.DESC;
		KAnimFile anim = Assets.GetAnim("seed_pollinator_plant_kanim");
		string text7 = "object";
		int num2 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		string text8 = global::STRINGS.CREATURES.SPECIES.BUTTERFLYPLANT.DOMESTICATEDDESC;
		GameObject gameObject3 = EntityTemplates.CreateAndRegisterSeedForPlant(gameObject2, this, productionType, text4, text5, text6, anim, text7, num2, list, receptacleDirection, default(Tag), 2, text8, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", true);
		EntityTemplates.ExtendEntityToFood(gameObject3, FOOD.FOOD_TYPES.BUTTERFLY_SEED);
		EntityTemplates.CreateAndRegisterPreviewForPlant(gameObject3, "ButterflyPlant_preview", Assets.GetAnim("pollinator_plant_kanim"), "place", 1, 2);
		gameObject.AddOrGet<Growing>().maxAge = 0f;
		gameObject.AddOrGet<Crop>().cropSpawnOffset = new Vector3(-0.0365f, 1.26175f, 0f);
		return gameObject;
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x0003229E File Offset: 0x0003049E
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x000322A0 File Offset: 0x000304A0
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400056B RID: 1387
	public const string ID = "ButterflyPlant";

	// Token: 0x0400056C RID: 1388
	public const string SEED_ID = "ButterflyPlantSeed";
}
