using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001B3 RID: 435
public class SwampHarvestPlantConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060008AF RID: 2223 RVA: 0x0003AC10 File Offset: 0x00038E10
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x0003AC17 File Offset: 0x00038E17
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060008B1 RID: 2225 RVA: 0x0003AC1C File Offset: 0x00038E1C
	public GameObject CreatePrefab()
	{
		string text = "SwampHarvestPlant";
		string text2 = global::STRINGS.CREATURES.SPECIES.SWAMPHARVESTPLANT.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.SWAMPHARVESTPLANT.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.PENALTY.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("swampcrop_kanim"), "idle_empty", Grid.SceneLayer.BuildingBack, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 293f);
		GameObject gameObject2 = gameObject;
		float num2 = 218.15f;
		float num3 = 283.15f;
		float num4 = 303.15f;
		float num5 = 398.15f;
		string text4 = SwampFruitConfig.ID;
		EntityTemplates.ExtendEntityToBasicPlant(gameObject2, num2, num3, num4, num5, new SimHashes[]
		{
			SimHashes.Oxygen,
			SimHashes.ContaminatedOxygen,
			SimHashes.CarbonDioxide
		}, true, 0f, 0.15f, text4, true, true, true, true, 2400f, 0f, 4600f, "SwampHarvestPlantOriginal", gameObject.PrefabID().Name);
		gameObject.AddOrGet<IlluminationVulnerable>().SetPrefersDarkness(true);
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.DirtyWater,
				massConsumptionRate = 0.06666667f
			}
		});
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<LoopingSounds>();
		GameObject gameObject3 = gameObject;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Harvest;
		string text5 = "SwampHarvestPlantSeed";
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.SWAMPHARVESTPLANT.NAME;
		string text7 = global::STRINGS.CREATURES.SPECIES.SEEDS.SWAMPHARVESTPLANT.DESC;
		KAnimFile anim = Assets.GetAnim("seed_swampcrop_kanim");
		string text8 = "object";
		int num6 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		text4 = global::STRINGS.CREATURES.SPECIES.SWAMPHARVESTPLANT.DOMESTICATEDDESC;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject3, this, productionType, text5, text6, text7, anim, text8, num6, list, receptacleDirection, default(Tag), 2, text4, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), "SwampHarvestPlant_preview", Assets.GetAnim("swampcrop_kanim"), "place", 1, 2);
		return gameObject;
	}

	// Token: 0x060008B2 RID: 2226 RVA: 0x0003ADC7 File Offset: 0x00038FC7
	public void OnPrefabInit(GameObject prefab)
	{
	}

	// Token: 0x060008B3 RID: 2227 RVA: 0x0003ADC9 File Offset: 0x00038FC9
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000664 RID: 1636
	public const string ID = "SwampHarvestPlant";

	// Token: 0x04000665 RID: 1637
	public const string SEED_ID = "SwampHarvestPlantSeed";

	// Token: 0x04000666 RID: 1638
	public const float WATER_RATE = 0.06666667f;
}
