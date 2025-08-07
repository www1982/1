using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200017A RID: 378
public class BlueGrassConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x06000733 RID: 1843 RVA: 0x00031CA8 File Offset: 0x0002FEA8
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x00031CAF File Offset: 0x0002FEAF
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x06000735 RID: 1845 RVA: 0x00031CB4 File Offset: 0x0002FEB4
	public GameObject CreatePrefab()
	{
		string text = "BlueGrass";
		string text2 = global::STRINGS.CREATURES.SPECIES.BLUE_GRASS.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.BLUE_GRASS.DESC;
		float num = 2f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("bluegrass_kanim"), "idle_full", Grid.SceneLayer.BuildingFront, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 240f);
		GameObject gameObject2 = gameObject;
		float num2 = 193.15f;
		float num3 = 193.15f;
		float num4 = 273.15f;
		float num5 = 273.15f;
		string text4 = global::STRINGS.CREATURES.SPECIES.BLUE_GRASS.NAME;
		EntityTemplates.ExtendEntityToBasicPlant(gameObject2, num2, num3, num4, num5, new SimHashes[] { SimHashes.CarbonDioxide }, true, 0f, 0f, "OxyRock", true, true, true, true, 2400f, 0f, 2200f, "BlueGrassOriginal", text4);
		ElementConsumer elementConsumer = gameObject.AddOrGet<ElementConsumer>();
		elementConsumer.showInStatusPanel = true;
		elementConsumer.storeOnConsume = false;
		elementConsumer.elementToConsume = SimHashes.CarbonDioxide;
		elementConsumer.configuration = ElementConsumer.Configuration.Element;
		elementConsumer.consumptionRadius = 2;
		elementConsumer.EnableConsumption(true);
		elementConsumer.sampleCellOffset = new Vector3(0f, 0f);
		elementConsumer.consumptionRate = 0.0005f;
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.Ice.CreateTag(),
				massConsumptionRate = 0.033333335f
			}
		});
		gameObject.GetComponent<UprootedMonitor>();
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<BlueGrass>();
		GameObject gameObject3 = gameObject;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Harvest;
		string text5 = "BlueGrassSeed";
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.BLUE_GRASS.NAME;
		string text7 = global::STRINGS.CREATURES.SPECIES.SEEDS.BLUE_GRASS.DESC;
		KAnimFile anim = Assets.GetAnim("seed_bluegrass_kanim");
		string text8 = "object";
		int num6 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		text4 = global::STRINGS.CREATURES.SPECIES.BLUE_GRASS.DOMESTICATEDDESC;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject3, this, productionType, text5, text6, text7, anim, text8, num6, list, receptacleDirection, default(Tag), 4, text4, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), "BlueGrass_preview", Assets.GetAnim("bluegrass_kanim"), "place", 1, 1);
		return gameObject;
	}

	// Token: 0x06000736 RID: 1846 RVA: 0x00031EAA File Offset: 0x000300AA
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000737 RID: 1847 RVA: 0x00031EAC File Offset: 0x000300AC
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000563 RID: 1379
	public const string ID = "BlueGrass";

	// Token: 0x04000564 RID: 1380
	public const string SEED_ID = "BlueGrassSeed";

	// Token: 0x04000565 RID: 1381
	public const float CO2_RATE = 0.002f;

	// Token: 0x04000566 RID: 1382
	public const float FERTILIZATION_RATE = 20f;
}
