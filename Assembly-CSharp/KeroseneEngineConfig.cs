using System;
using TUNING;
using UnityEngine;

// Token: 0x02000268 RID: 616
public class KeroseneEngineConfig : IBuildingConfig
{
	// Token: 0x06000C77 RID: 3191 RVA: 0x0004AB8B File Offset: 0x00048D8B
	public override string[] GetForbiddenDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000C78 RID: 3192 RVA: 0x0004AB94 File Offset: 0x00048D94
	public override BuildingDef CreateBuildingDef()
	{
		string text = "KeroseneEngine";
		int num = 7;
		int num2 = 5;
		string text2 = "rocket_petroleum_engine_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] engine_MASS_SMALL = BUILDINGS.ROCKETRY_MASS_KG.ENGINE_MASS_SMALL;
		string[] array = new string[] { SimHashes.Steel.ToString() };
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, engine_MASS_SMALL, array, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
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

	// Token: 0x06000C79 RID: 3193 RVA: 0x0004AC44 File Offset: 0x00048E44
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

	// Token: 0x06000C7A RID: 3194 RVA: 0x0004ACA8 File Offset: 0x00048EA8
	public override void DoPostConfigureComplete(GameObject go)
	{
		RocketEngine rocketEngine = go.AddOrGet<RocketEngine>();
		rocketEngine.fuelTag = ElementLoader.FindElementByHash(SimHashes.Petroleum).tag;
		rocketEngine.efficiency = ROCKETRY.ENGINE_EFFICIENCY.MEDIUM;
		rocketEngine.explosionEffectHash = SpawnFXHashes.MeteorImpactDust;
		BuildingTemplates.ExtendBuildingToRocketModule(go, "rocket_petroleum_engine_bg_kanim", false);
	}

	// Token: 0x0400088B RID: 2187
	public const string ID = "KeroseneEngine";
}
