using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000179 RID: 377
public class BeanPlantConfig : IEntityConfig
{
	// Token: 0x0600072F RID: 1839 RVA: 0x00031AB4 File Offset: 0x0002FCB4
	public GameObject CreatePrefab()
	{
		string text = "BeanPlant";
		string text2 = global::STRINGS.CREATURES.SPECIES.BEAN_PLANT.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.BEAN_PLANT.DESC;
		float num = 2f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("beanplant_kanim"), "idle_empty", Grid.SceneLayer.BuildingFront, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 258.15f);
		GameObject gameObject2 = gameObject;
		float num2 = 198.15f;
		float num3 = 248.15f;
		float num4 = 273.15f;
		float num5 = 323.15f;
		string text4 = global::STRINGS.CREATURES.SPECIES.BEAN_PLANT.NAME;
		EntityTemplates.ExtendEntityToBasicPlant(gameObject2, num2, num3, num4, num5, new SimHashes[] { SimHashes.CarbonDioxide }, true, 0f, 0.025f, "BeanPlantSeed", true, true, true, true, 2400f, 0f, 9800f, "BeanPlantOriginal", text4);
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.Ethanol.CreateTag(),
				massConsumptionRate = 0.033333335f
			}
		});
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.Dirt.CreateTag(),
				massConsumptionRate = 0.008333334f
			}
		});
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<DirectlyEdiblePlant_Growth>();
		GameObject gameObject3 = gameObject;
		IHasDlcRestrictions hasDlcRestrictions = this as IHasDlcRestrictions;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Crop;
		string text5 = "BeanPlantSeed";
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.BEAN_PLANT.NAME;
		string text7 = global::STRINGS.CREATURES.SPECIES.SEEDS.BEAN_PLANT.DESC;
		KAnimFile anim = Assets.GetAnim("seed_beanplant_kanim");
		string text8 = "object";
		int num6 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		text4 = global::STRINGS.CREATURES.SPECIES.BEAN_PLANT.DOMESTICATEDDESC;
		GameObject gameObject4 = EntityTemplates.CreateAndRegisterSeedForPlant(gameObject3, hasDlcRestrictions, productionType, text5, text6, text7, anim, text8, num6, list, receptacleDirection, default(Tag), 3, text4, EntityTemplates.CollisionShape.RECTANGLE, 0.6f, 0.3f, null, "", true);
		EntityTemplates.ExtendEntityToFood(gameObject4, FOOD.FOOD_TYPES.BEAN);
		EntityTemplates.CreateAndRegisterPreviewForPlant(gameObject4, "BeanPlant_preview", Assets.GetAnim("beanplant_kanim"), "place", 1, 2);
		return gameObject;
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x00031C9C File Offset: 0x0002FE9C
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x00031C9E File Offset: 0x0002FE9E
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400055F RID: 1375
	public const string ID = "BeanPlant";

	// Token: 0x04000560 RID: 1376
	public const string SEED_ID = "BeanPlantSeed";

	// Token: 0x04000561 RID: 1377
	public const float FERTILIZATION_RATE = 0.008333334f;

	// Token: 0x04000562 RID: 1378
	public const float WATER_RATE = 0.033333335f;
}
