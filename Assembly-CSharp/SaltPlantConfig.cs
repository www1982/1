using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001A7 RID: 423
public class SaltPlantConfig : IEntityConfig
{
	// Token: 0x06000826 RID: 2086 RVA: 0x000374B4 File Offset: 0x000356B4
	public GameObject CreatePrefab()
	{
		string text = "SaltPlant";
		string text2 = global::STRINGS.CREATURES.SPECIES.SALTPLANT.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.SALTPLANT.DESC;
		float num = 2f;
		EffectorValues tier = DECOR.PENALTY.TIER1;
		KAnimFile anim = Assets.GetAnim("saltplant_kanim");
		string text4 = "idle_empty";
		Grid.SceneLayer sceneLayer = Grid.SceneLayer.BuildingFront;
		int num2 = 1;
		int num3 = 2;
		EffectorValues effectorValues = tier;
		List<Tag> list = new List<Tag> { GameTags.Hanging };
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, anim, text4, sceneLayer, num2, num3, effectorValues, default(EffectorValues), SimHashes.Creature, list, 258.15f);
		EntityTemplates.MakeHangingOffsets(gameObject, 1, 2);
		GameObject gameObject2 = gameObject;
		float num4 = 198.15f;
		float num5 = 248.15f;
		float num6 = 323.15f;
		float num7 = 393.15f;
		string text5 = SimHashes.Salt.ToString();
		string text6 = global::STRINGS.CREATURES.SPECIES.SALTPLANT.NAME;
		EntityTemplates.ExtendEntityToBasicPlant(gameObject2, num4, num5, num6, num7, new SimHashes[] { SimHashes.ChlorineGas }, true, 0f, 0.025f, text5, true, true, true, true, 2400f, 0f, 7400f, "SaltPlantOriginal", text6);
		gameObject.AddOrGet<SaltPlant>();
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.Sand.CreateTag(),
				massConsumptionRate = 0.011666667f
			}
		});
		Storage storage = gameObject.AddOrGet<Storage>();
		storage.showInUI = false;
		storage.capacityKg = 1f;
		ElementConsumer elementConsumer = gameObject.AddOrGet<ElementConsumer>();
		elementConsumer.showInStatusPanel = true;
		elementConsumer.showDescriptor = true;
		elementConsumer.storeOnConsume = false;
		elementConsumer.elementToConsume = SimHashes.ChlorineGas;
		elementConsumer.configuration = ElementConsumer.Configuration.Element;
		elementConsumer.consumptionRadius = 4;
		elementConsumer.sampleCellOffset = new Vector3(0f, -1f);
		elementConsumer.consumptionRate = 0.006f;
		gameObject.GetComponent<UprootedMonitor>().monitorCells = new CellOffset[]
		{
			new CellOffset(0, 1)
		};
		gameObject.AddOrGet<StandardCropPlant>();
		GameObject gameObject3 = gameObject;
		IHasDlcRestrictions hasDlcRestrictions = this as IHasDlcRestrictions;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Harvest;
		string text7 = "SaltPlantSeed";
		string text8 = global::STRINGS.CREATURES.SPECIES.SEEDS.SALTPLANT.NAME;
		string text9 = global::STRINGS.CREATURES.SPECIES.SEEDS.SALTPLANT.DESC;
		KAnimFile anim2 = Assets.GetAnim("seed_saltplant_kanim");
		string text10 = "object";
		int num8 = 1;
		List<Tag> list2 = new List<Tag>();
		list2.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Bottom;
		text6 = global::STRINGS.CREATURES.SPECIES.SALTPLANT.DOMESTICATEDDESC;
		EntityTemplates.MakeHangingOffsets(EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject3, hasDlcRestrictions, productionType, text7, text8, text9, anim2, text10, num8, list2, receptacleDirection, default(Tag), 5, text6, EntityTemplates.CollisionShape.CIRCLE, 0.35f, 0.35f, null, "", false), "SaltPlant_preview", Assets.GetAnim("saltplant_kanim"), "place", 1, 2), 1, 2);
		return gameObject;
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x00037716 File Offset: 0x00035916
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x00037718 File Offset: 0x00035918
	public void OnSpawn(GameObject inst)
	{
		inst.GetComponent<ElementConsumer>().EnableConsumption(true);
	}

	// Token: 0x04000610 RID: 1552
	public const string ID = "SaltPlant";

	// Token: 0x04000611 RID: 1553
	public const string SEED_ID = "SaltPlantSeed";

	// Token: 0x04000612 RID: 1554
	public const float FERTILIZATION_RATE = 0.011666667f;

	// Token: 0x04000613 RID: 1555
	public const float CHLORINE_CONSUMPTION_RATE = 0.006f;
}
