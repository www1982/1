using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200019E RID: 414
public class KelpPlantConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060007FC RID: 2044 RVA: 0x000364B9 File Offset: 0x000346B9
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x000364C0 File Offset: 0x000346C0
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x000364C4 File Offset: 0x000346C4
	public GameObject CreatePrefab()
	{
		string text = "KelpPlant";
		string text2 = global::STRINGS.CREATURES.SPECIES.KELPPLANT.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.KELPPLANT.DESC;
		float num = 4f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		KAnimFile anim = Assets.GetAnim("kelp_kanim");
		string text4 = "idle_empty";
		Grid.SceneLayer sceneLayer = Grid.SceneLayer.BuildingFront;
		int num2 = 1;
		int num3 = 2;
		EffectorValues effectorValues = tier;
		List<Tag> list = new List<Tag> { GameTags.Hanging };
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, anim, text4, sceneLayer, num2, num3, effectorValues, default(EffectorValues), SimHashes.Creature, list, 297.15f);
		EntityTemplates.MakeHangingOffsets(gameObject, 1, 2);
		GameObject gameObject2 = gameObject;
		float num4 = 253.15f;
		float num5 = 263.15f;
		float num6 = 358.15f;
		float num7 = 373.15f;
		string id = KelpConfig.ID;
		string text5 = global::STRINGS.CREATURES.SPECIES.KELPPLANT.NAME;
		EntityTemplates.ExtendEntityToBasicPlant(gameObject2, num4, num5, num6, num7, KelpPlantConfig.ALLOWED_ELEMENTS, false, 0f, 0.15f, id, false, true, true, true, 2400f, 0f, 7400f, "KelpPlantOriginal", text5);
		gameObject.AddOrGet<PressureVulnerable>().allCellsMustBeSafe = true;
		EntityTemplates.ExtendPlantToFertilizable(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = SimHashes.ToxicSand.ToString(),
				massConsumptionRate = 0.016666668f
			}
		});
		gameObject.AddOrGet<DirectlyEdiblePlant_Growth>();
		gameObject.GetComponent<UprootedMonitor>().monitorCells = new CellOffset[]
		{
			new CellOffset(0, 1)
		};
		gameObject.AddOrGet<StandardCropPlant>();
		GameObject gameObject3 = gameObject;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Harvest;
		string text6 = "KelpPlantSeed";
		string text7 = global::STRINGS.CREATURES.SPECIES.SEEDS.KELPPLANT.NAME;
		string text8 = global::STRINGS.CREATURES.SPECIES.SEEDS.KELPPLANT.DESC;
		KAnimFile anim2 = Assets.GetAnim("seed_kelp_kanim");
		string text9 = "object";
		int num8 = 1;
		List<Tag> list2 = new List<Tag>();
		list2.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Bottom;
		text5 = global::STRINGS.CREATURES.SPECIES.KELPPLANT.DOMESTICATEDDESC;
		EntityTemplates.MakeHangingOffsets(EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject3, this, productionType, text6, text7, text8, anim2, text9, num8, list2, receptacleDirection, default(Tag), 4, text5, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), "KelpPlant_preview", Assets.GetAnim("kelp_kanim"), "place", 1, 2), 1, 2);
		return gameObject;
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x000366BA File Offset: 0x000348BA
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x000366BC File Offset: 0x000348BC
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005EE RID: 1518
	public const string ID = "KelpPlant";

	// Token: 0x040005EF RID: 1519
	public const string SEED_ID = "KelpPlantSeed";

	// Token: 0x040005F0 RID: 1520
	public const int YIELD_UNITS_PER_HARVEST = 50;

	// Token: 0x040005F1 RID: 1521
	public const float LIFETIME_CYCLES = 5f;

	// Token: 0x040005F2 RID: 1522
	public const float FERTILIZATION_RATE = 0.016666668f;

	// Token: 0x040005F3 RID: 1523
	public static SimHashes[] ALLOWED_ELEMENTS = new SimHashes[]
	{
		SimHashes.Water,
		SimHashes.DirtyWater,
		SimHashes.SaltWater,
		SimHashes.Brine,
		SimHashes.PhytoOil,
		SimHashes.NaturalResin
	};

	// Token: 0x040005F4 RID: 1524
	public const float CALCULATED_YIELD_MASS_PER_HARVEST = 50f;

	// Token: 0x040005F5 RID: 1525
	public const float CALCULATED_YIELD_MASS_PER_CYCLE = 10f;

	// Token: 0x040005F6 RID: 1526
	public const float CALCULATED_GROWTH_PER_CYCLE = 0.2f;

	// Token: 0x040005F7 RID: 1527
	public const float CALCULATED_LIFETIME_SEC = 3000f;
}
