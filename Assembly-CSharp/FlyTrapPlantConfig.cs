using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200018A RID: 394
public class FlyTrapPlantConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600078C RID: 1932 RVA: 0x00033B4A File Offset: 0x00031D4A
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x00033B51 File Offset: 0x00031D51
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600078E RID: 1934 RVA: 0x00033B54 File Offset: 0x00031D54
	public GameObject CreatePrefab()
	{
		string text = "FlyTrapPlant";
		string text2 = global::STRINGS.CREATURES.SPECIES.FLYTRAPPLANT.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.FLYTRAPPLANT.DESC;
		float num = 1f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		KAnimFile anim = Assets.GetAnim("ceiling_carnie_kanim");
		string text4 = "idle_empty";
		Grid.SceneLayer sceneLayer = Grid.SceneLayer.BuildingFront;
		int num2 = 1;
		int num3 = 2;
		EffectorValues effectorValues = tier;
		List<Tag> list = new List<Tag> { GameTags.Hanging };
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, anim, text4, sceneLayer, num2, num3, effectorValues, default(EffectorValues), SimHashes.Creature, list, 291.15f);
		EntityTemplates.MakeHangingOffsets(gameObject, 1, 2);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 273.15f, 283.15f, 328.15f, 348.15f, null, true, 0f, 0.15f, SimHashes.Amber.ToString(), true, true, true, true, 2400f, 0f, 7400f, "FlyTrapPlantOriginal", global::STRINGS.CREATURES.SPECIES.FLYTRAPPLANT.NAME);
		gameObject.GetComponent<UprootedMonitor>().monitorCells = new CellOffset[]
		{
			new CellOffset(0, 1)
		};
		gameObject.AddOrGet<StandardCropPlant>();
		gameObject.AddOrGet<FlytrapConsumptionMonitor>();
		gameObject.AddOrGet<Growing>().MaxMaturityValuePercentageToSpawnWith = 0f;
		GameObject gameObject2 = gameObject;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Harvest;
		string text5 = "FlyTrapPlantSeed";
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.FLYTRAPPLANT.NAME;
		string text7 = global::STRINGS.CREATURES.SPECIES.SEEDS.FLYTRAPPLANT.DESC;
		KAnimFile anim2 = Assets.GetAnim("seed_ceiling_carnie_kanim");
		string text8 = "object";
		int num4 = 1;
		List<Tag> list2 = new List<Tag>();
		list2.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Bottom;
		string text9 = global::STRINGS.CREATURES.SPECIES.FLYTRAPPLANT.DOMESTICATEDDESC;
		EntityTemplates.MakeHangingOffsets(EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject2, this, productionType, text5, text6, text7, anim2, text8, num4, list2, receptacleDirection, default(Tag), 4, text9, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), "FlyTrapPlant_preview", Assets.GetAnim("ceiling_carnie_kanim"), "place", 1, 2), 1, 2);
		return gameObject;
	}

	// Token: 0x0600078F RID: 1935 RVA: 0x00033D07 File Offset: 0x00031F07
	public void OnPrefabInit(GameObject inst)
	{
		inst.AddOrGet<StandardCropPlant>().anims = FlyTrapPlantConfig.Default_StandardCropAnimSet;
	}

	// Token: 0x06000790 RID: 1936 RVA: 0x00033D19 File Offset: 0x00031F19
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x040005A4 RID: 1444
	public const string ID = "FlyTrapPlant";

	// Token: 0x040005A5 RID: 1445
	public const string SEED_ID = "FlyTrapPlantSeed";

	// Token: 0x040005A6 RID: 1446
	public static readonly StandardCropPlant.AnimSet Default_StandardCropAnimSet = new StandardCropPlant.AnimSet
	{
		pre_grow = "grow_pre",
		grow = "grow",
		grow_pst = "grow_pst",
		idle_full = "idle_full",
		wilt_base = "wilt",
		harvest = "harvest",
		waning = "waning",
		grow_playmode = KAnim.PlayMode.Paused
	};

	// Token: 0x040005A7 RID: 1447
	public const int DIGESTION_DURATION_CYCLES = 12;

	// Token: 0x040005A8 RID: 1448
	public const float DIGESTION_DURATION = 7200f;

	// Token: 0x040005A9 RID: 1449
	public const int AMBER_PER_HARVEST_KG = 264;
}
