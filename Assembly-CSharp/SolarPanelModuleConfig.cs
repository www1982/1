using System;
using TUNING;
using UnityEngine;

// Token: 0x020003F8 RID: 1016
public class SolarPanelModuleConfig : IBuildingConfig
{
	// Token: 0x060014CC RID: 5324 RVA: 0x00076F24 File Offset: 0x00075124
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060014CD RID: 5325 RVA: 0x00076F2C File Offset: 0x0007512C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SolarPanelModule";
		int num = 3;
		int num2 = 1;
		string text2 = "rocket_solar_panel_module_kanim";
		int num3 = 1000;
		float num4 = 30f;
		float[] hollow_TIER = BUILDINGS.ROCKETRY_MASS_KG.HOLLOW_TIER1;
		string[] glasses = MATERIALS.GLASSES;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, hollow_TIER, glasses, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.DefaultAnimState = "grounded";
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.GeneratorWattageRating = 60f;
		buildingDef.GeneratorBaseCapacity = buildingDef.GeneratorWattageRating;
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.SelfHeatKilowattsWhenActive = 0f;
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.ForegroundLayer = Grid.SceneLayer.Front;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.PowerInputOffset = SolarPanelModuleConfig.PLUG_OFFSET;
		buildingDef.PowerOutputOffset = SolarPanelModuleConfig.PLUG_OFFSET;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.RequiresPowerOutput = true;
		buildingDef.UseWhitePowerOutputConnectorColour = true;
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		return buildingDef;
	}

	// Token: 0x060014CE RID: 5326 RVA: 0x0007701C File Offset: 0x0007521C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddComponent<RequireInputs>();
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 1), GameTags.Rocket, null)
		};
		go.AddComponent<PartialLightBlocking>();
	}

	// Token: 0x060014CF RID: 5327 RVA: 0x0007708E File Offset: 0x0007528E
	public override void DoPostConfigureComplete(GameObject go)
	{
		Prioritizable.AddRef(go);
		go.AddOrGet<ModuleSolarPanel>().showConnectedConsumerStatusItems = false;
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.INSIGNIFICANT, 0f, 0f);
		go.GetComponent<RocketModule>().operationalLandedRequired = false;
	}

	// Token: 0x04000C64 RID: 3172
	public const string ID = "SolarPanelModule";

	// Token: 0x04000C65 RID: 3173
	private static readonly CellOffset PLUG_OFFSET = new CellOffset(-1, 0);

	// Token: 0x04000C66 RID: 3174
	private const float EFFICIENCY_RATIO = 0.75f;

	// Token: 0x04000C67 RID: 3175
	public const float MAX_WATTS = 60f;
}
