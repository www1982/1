using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000182 RID: 386
public class CritterTrapPlantConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x0600075A RID: 1882 RVA: 0x00032C9A File Offset: 0x00030E9A
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x0600075B RID: 1883 RVA: 0x00032CA1 File Offset: 0x00030EA1
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x00032CA4 File Offset: 0x00030EA4
	public GameObject CreatePrefab()
	{
		string text = "CritterTrapPlant";
		string text2 = global::STRINGS.CREATURES.SPECIES.CRITTERTRAPPLANT.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.CRITTERTRAPPLANT.DESC;
		float num = 4f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		KAnimFile anim = Assets.GetAnim("venus_critter_trap_kanim");
		string text4 = "idle_open";
		Grid.SceneLayer sceneLayer = Grid.SceneLayer.BuildingBack;
		int num2 = 1;
		int num3 = 2;
		EffectorValues effectorValues = tier;
		float freezing_ = global::TUNING.CREATURES.TEMPERATURE.FREEZING_3;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, anim, text4, sceneLayer, num2, num3, effectorValues, default(EffectorValues), SimHashes.Creature, null, freezing_);
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, global::TUNING.CREATURES.TEMPERATURE.FREEZING_10, global::TUNING.CREATURES.TEMPERATURE.FREEZING_9, global::TUNING.CREATURES.TEMPERATURE.FREEZING, global::TUNING.CREATURES.TEMPERATURE.COOL, null, false, 0f, 0.15f, "PlantMeat", true, true, true, false, 2400f, 0f, 2200f, "CritterTrapPlantOriginal", global::STRINGS.CREATURES.SPECIES.CRITTERTRAPPLANT.NAME);
		global::UnityEngine.Object.DestroyImmediate(gameObject.GetComponent<MutantPlant>());
		CritterTrapPlant critterTrapPlant = gameObject.AddOrGet<CritterTrapPlant>();
		critterTrapPlant.CONSUMABLE_TAGs = CritterTrapPlantConfig.AllowedPreyTags;
		critterTrapPlant.gasOutputRate = 0.041666668f;
		critterTrapPlant.outputElement = SimHashes.Hydrogen;
		critterTrapPlant.gasVentThreshold = 33.25f;
		TrapTrigger trapTrigger = gameObject.AddOrGet<TrapTrigger>();
		trapTrigger.trappableCreatures = CritterTrapPlantConfig.AllowedPreyTags;
		trapTrigger.trappedOffset = new Vector2(0.5f, 0f);
		trapTrigger.enabled = false;
		gameObject.AddOrGet<LoopingSounds>();
		gameObject.AddOrGet<Storage>();
		Tag tag = ElementLoader.FindElementByHash(SimHashes.DirtyWater).tag;
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = tag,
				massConsumptionRate = 0.016666668f
			}
		});
		GameObject gameObject2 = gameObject;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Hidden;
		string text5 = "CritterTrapPlantSeed";
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.CRITTERTRAPPLANT.NAME;
		string text7 = global::STRINGS.CREATURES.SPECIES.SEEDS.CRITTERTRAPPLANT.DESC;
		KAnimFile anim2 = Assets.GetAnim("seed_critter_trap_kanim");
		string text8 = "object";
		int num4 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		string text9 = global::STRINGS.CREATURES.SPECIES.CRITTERTRAPPLANT.DOMESTICATEDDESC;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject2, this, productionType, text5, text6, text7, anim2, text8, num4, list, receptacleDirection, default(Tag), 21, text9, EntityTemplates.CollisionShape.CIRCLE, 0.3f, 0.3f, null, "", false), "CritterTrapPlant_preview", Assets.GetAnim("venus_critter_trap_kanim"), "place", 1, 2);
		return gameObject;
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x00032EA8 File Offset: 0x000310A8
	public void OnPrefabInit(GameObject inst)
	{
		CritterTrapPlant component = inst.GetComponent<CritterTrapPlant>();
		inst.GetComponent<TrapTrigger>().customConditionsToTrap = new Func<GameObject, bool>(component.IsEntityEdible);
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x00032ED4 File Offset: 0x000310D4
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x04000586 RID: 1414
	public const string ID = "CritterTrapPlant";

	// Token: 0x04000587 RID: 1415
	public const float WATER_RATE = 0.016666668f;

	// Token: 0x04000588 RID: 1416
	public const float GAS_RATE = 0.041666668f;

	// Token: 0x04000589 RID: 1417
	public const float GAS_VENT_THRESHOLD = 33.25f;

	// Token: 0x0400058A RID: 1418
	private static Tag[] AllowedPreyTags = new Tag[]
	{
		GameTags.Creatures.Walker,
		GameTags.Creatures.Hoverer
	};
}
