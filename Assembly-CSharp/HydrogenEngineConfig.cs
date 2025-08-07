using System;
using TUNING;
using UnityEngine;

// Token: 0x0200024C RID: 588
public class HydrogenEngineConfig : IBuildingConfig
{
	// Token: 0x06000BE7 RID: 3047 RVA: 0x0004848A File Offset: 0x0004668A
	public override string[] GetForbiddenDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x00048494 File Offset: 0x00046694
	public override BuildingDef CreateBuildingDef()
	{
		string text = "HydrogenEngine";
		int num = 7;
		int num2 = 5;
		string text2 = "rocket_hydrogen_engine_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] engine_MASS_LARGE = BUILDINGS.ROCKETRY_MASS_KG.ENGINE_MASS_LARGE;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, engine_MASS_LARGE, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingFront;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.RequiresPowerInput = false;
		buildingDef.CanMove = true;
		return buildingDef;
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x00048544 File Offset: 0x00046744
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 5), GameTags.Rocket, null)
		};
	}

	// Token: 0x06000BEA RID: 3050 RVA: 0x000485A8 File Offset: 0x000467A8
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x06000BEB RID: 3051 RVA: 0x000485AA File Offset: 0x000467AA
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
	}

	// Token: 0x06000BEC RID: 3052 RVA: 0x000485AC File Offset: 0x000467AC
	public override void DoPostConfigureComplete(GameObject go)
	{
		RocketEngine rocketEngine = go.AddOrGet<RocketEngine>();
		rocketEngine.fuelTag = ElementLoader.FindElementByHash(SimHashes.LiquidHydrogen).tag;
		rocketEngine.efficiency = ROCKETRY.ENGINE_EFFICIENCY.STRONG;
		rocketEngine.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		rocketEngine.exhaustElement = SimHashes.Steam;
		rocketEngine.exhaustTemperature = 2000f;
		BuildingTemplates.ExtendBuildingToRocketModule(go, "rocket_hydrogen_engine_bg_kanim", false);
	}

	// Token: 0x04000830 RID: 2096
	public const string ID = "HydrogenEngine";
}
