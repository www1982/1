using System;
using TUNING;
using UnityEngine;

// Token: 0x02000427 RID: 1063
public class TelescopeConfig : IBuildingConfig
{
	// Token: 0x060015E1 RID: 5601 RVA: 0x0007CEFF File Offset: 0x0007B0FF
	public override string[] GetForbiddenDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060015E2 RID: 5602 RVA: 0x0007CF08 File Offset: 0x0007B108
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Telescope";
		int num = 4;
		int num2 = 6;
		string text2 = "telescope_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.ExhaustKilowattsWhenActive = 0.125f;
		buildingDef.SelfHeatKilowattsWhenActive = 0f;
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.RequiredSkillPerkID = Db.Get().SkillPerks.CanStudyWorldObjects.Id;
		return buildingDef;
	}

	// Token: 0x060015E3 RID: 5603 RVA: 0x0007CFC8 File Offset: 0x0007B1C8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.ScienceBuilding, false);
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		Prioritizable.AddRef(go);
		Telescope telescope = go.AddOrGet<Telescope>();
		telescope.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_telescope_kanim") };
		telescope.requiredSkillPerk = Db.Get().SkillPerks.CanStudyWorldObjects.Id;
		telescope.workLayer = Grid.SceneLayer.BuildingFront;
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 1000f;
		storage.showInUI = true;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.consumptionRate = 1f;
		conduitConsumer.capacityTag = ElementLoader.FindElementByHash(SimHashes.Oxygen).tag;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		conduitConsumer.capacityKG = 10f;
		conduitConsumer.forceAlwaysSatisfied = true;
		go.AddOrGetDef<PoweredController.Def>();
	}

	// Token: 0x060015E4 RID: 5604 RVA: 0x0007D0A2 File Offset: 0x0007B2A2
	public override void DoPostConfigureComplete(GameObject go)
	{
		TelescopeConfig.AddVisualizer(go);
	}

	// Token: 0x060015E5 RID: 5605 RVA: 0x0007D0AA File Offset: 0x0007B2AA
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		TelescopeConfig.AddVisualizer(go);
	}

	// Token: 0x060015E6 RID: 5606 RVA: 0x0007D0B2 File Offset: 0x0007B2B2
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		TelescopeConfig.AddVisualizer(go);
	}

	// Token: 0x060015E7 RID: 5607 RVA: 0x0007D0BA File Offset: 0x0007B2BA
	private static void AddVisualizer(GameObject prefab)
	{
		SkyVisibilityVisualizer skyVisibilityVisualizer = prefab.AddOrGet<SkyVisibilityVisualizer>();
		skyVisibilityVisualizer.OriginOffset.y = 3;
		skyVisibilityVisualizer.TwoWideOrgin = true;
		skyVisibilityVisualizer.RangeMin = -4;
		skyVisibilityVisualizer.RangeMax = 5;
		skyVisibilityVisualizer.SkipOnModuleInteriors = true;
	}

	// Token: 0x04000CF1 RID: 3313
	public const string ID = "Telescope";

	// Token: 0x04000CF2 RID: 3314
	public const float POINTS_PER_DAY = 2f;

	// Token: 0x04000CF3 RID: 3315
	public const float MASS_PER_POINT = 2f;

	// Token: 0x04000CF4 RID: 3316
	public const float CAPACITY = 30f;

	// Token: 0x04000CF5 RID: 3317
	public const int SCAN_RADIUS = 4;

	// Token: 0x04000CF6 RID: 3318
	public const int VERTICAL_SCAN_OFFSET = 3;

	// Token: 0x04000CF7 RID: 3319
	public static readonly SkyVisibilityInfo SKY_VISIBILITY_INFO = new SkyVisibilityInfo(new CellOffset(0, 3), 4, new CellOffset(1, 3), 4, 0);

	// Token: 0x04000CF8 RID: 3320
	public static readonly Tag INPUT_MATERIAL = GameTags.Glass;
}
