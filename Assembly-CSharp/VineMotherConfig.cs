using System;
using System.Collections.Generic;
using Klei.AI;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001B9 RID: 441
public class VineMotherConfig : IEntityConfig, IHasDlcRestrictions
{
	// Token: 0x060008D2 RID: 2258 RVA: 0x0003BA6C File Offset: 0x00039C6C
	public string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x060008D3 RID: 2259 RVA: 0x0003BA73 File Offset: 0x00039C73
	public string[] GetForbiddenDlcIds()
	{
		return null;
	}

	// Token: 0x060008D4 RID: 2260 RVA: 0x0003BA78 File Offset: 0x00039C78
	public GameObject CreatePrefab()
	{
		string text = "VineMother";
		string text2 = global::STRINGS.CREATURES.SPECIES.VINEMOTHER.NAME;
		string text3 = global::STRINGS.CREATURES.SPECIES.VINEMOTHER.DESC;
		float num = 2f;
		EffectorValues tier = DECOR.BONUS.TIER1;
		GameObject gameObject = EntityTemplates.CreatePlacedEntity(text, text2, text3, num, Assets.GetAnim("vine_mother_kanim"), "object", Grid.SceneLayer.BuildingFront, 1, 2, tier, default(EffectorValues), SimHashes.Creature, null, 308.15f);
		string text4 = "VineMotherOriginal";
		EntityTemplates.ExtendEntityToBasicPlant(gameObject, 273.15f, 298.15f, 318.15f, 378.15f, VineMotherConfig.ALLOWED_ELEMENTS, false, 0f, 0.15f, null, true, false, true, false, 2400f, 0f, 2200f, text4, global::STRINGS.CREATURES.SPECIES.VINEMOTHER.NAME);
		WiltCondition component = gameObject.GetComponent<WiltCondition>();
		component.WiltDelay = 0f;
		component.RecoveryDelay = 0f;
		KPrefabID component2 = gameObject.GetComponent<KPrefabID>();
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.HarvestableIDs, component2.PrefabID().ToString());
		gameObject.AddOrGet<Traits>();
		Db.Get().traits.Get(text4);
		gameObject.GetComponent<Modifiers>().initialTraits.Add(text4);
		VineMother.Def def = gameObject.AddOrGetDef<VineMother.Def>();
		def.BRANCH_PREFAB_NAME = "VineBranch";
		def.MAX_BRANCH_COUNT = 24;
		gameObject.AddOrGet<HarvestDesignatable>();
		EntityTemplates.ExtendPlantToIrrigated(gameObject, new PlantElementAbsorber.ConsumeInfo[]
		{
			new PlantElementAbsorber.ConsumeInfo
			{
				tag = GameTags.Water,
				massConsumptionRate = 0.15f
			}
		});
		GameObject gameObject2 = gameObject;
		SeedProducer.ProductionType productionType = SeedProducer.ProductionType.Hidden;
		string text5 = "VineMotherSeed";
		string text6 = global::STRINGS.CREATURES.SPECIES.SEEDS.VINEMOTHER.NAME;
		string text7 = global::STRINGS.CREATURES.SPECIES.SEEDS.VINEMOTHER.DESC;
		KAnimFile anim = Assets.GetAnim("seed_vine_kanim");
		string text8 = "object";
		int num2 = 1;
		List<Tag> list = new List<Tag>();
		list.Add(GameTags.CropSeed);
		SingleEntityReceptacle.ReceptacleDirection receptacleDirection = SingleEntityReceptacle.ReceptacleDirection.Top;
		string text9 = global::STRINGS.CREATURES.SPECIES.VINEMOTHER.DOMESTICATEDDESC;
		EntityTemplates.CreateAndRegisterPreviewForPlant(EntityTemplates.CreateAndRegisterSeedForPlant(gameObject2, this, productionType, text5, text6, text7, anim, text8, num2, list, receptacleDirection, default(Tag), 12, text9, EntityTemplates.CollisionShape.RECTANGLE, 0.8f, 0.6f, null, "", false), "VineMother_preview", Assets.GetAnim("vine_mother_kanim"), "place", 1, 2);
		return gameObject;
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x0003BC81 File Offset: 0x00039E81
	public void OnPrefabInit(GameObject inst)
	{
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x0003BC83 File Offset: 0x00039E83
	public void OnSpawn(GameObject inst)
	{
	}

	// Token: 0x0400067D RID: 1661
	public const string ID = "VineMother";

	// Token: 0x0400067E RID: 1662
	public const string SEED_ID = "VineMotherSeed";

	// Token: 0x0400067F RID: 1663
	public const int MAX_BRANCH_NETWORK_COUNT = 12;

	// Token: 0x04000680 RID: 1664
	public static SimHashes[] ALLOWED_ELEMENTS = new SimHashes[]
	{
		SimHashes.Oxygen,
		SimHashes.CarbonDioxide,
		SimHashes.ContaminatedOxygen
	};

	// Token: 0x04000681 RID: 1665
	public const float IRRIGATION_RATE = 0.15f;

	// Token: 0x04000682 RID: 1666
	public const float TEMPERATURE_LETHAL_LOW = 273.15f;

	// Token: 0x04000683 RID: 1667
	public const float TEMPERATURE_WARNING_LOW = 298.15f;

	// Token: 0x04000684 RID: 1668
	public const float TEMPERATURE_WARNING_HIGH = 318.15f;

	// Token: 0x04000685 RID: 1669
	public const float TEMPERATURE_LETHAL_HIGH = 378.15f;
}
