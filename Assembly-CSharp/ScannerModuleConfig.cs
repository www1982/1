using System;
using TUNING;
using UnityEngine;

// Token: 0x020003DB RID: 987
public class ScannerModuleConfig : IBuildingConfig
{
	// Token: 0x0600142F RID: 5167 RVA: 0x00073FFC File Offset: 0x000721FC
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06001430 RID: 5168 RVA: 0x00074004 File Offset: 0x00072204
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ScannerModule";
		int num = 5;
		int num2 = 5;
		string text2 = "rocket_scanner_module_kanim";
		int num3 = 1000;
		float num4 = 120f;
		float[] array = new float[] { 350f, 1000f };
		string[] array2 = new string[]
		{
			SimHashes.Steel.ToString(),
			SimHashes.Polypropylene.ToString()
		};
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier, 0.2f);
		BuildingTemplates.CreateRocketBuildingDef(buildingDef);
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.AttachmentSlotTag = GameTags.Rocket;
		buildingDef.ObjectLayer = ObjectLayer.Building;
		buildingDef.RequiresPowerInput = false;
		buildingDef.attachablePosition = new CellOffset(0, 0);
		buildingDef.CanMove = true;
		buildingDef.Cancellable = false;
		return buildingDef;
	}

	// Token: 0x06001431 RID: 5169 RVA: 0x000740E0 File Offset: 0x000722E0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.AddOrGetDef<ScannerModule.Def>().scanRadius = 0;
		go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
		{
			new BuildingAttachPoint.HardPoint(new CellOffset(0, 5), GameTags.Rocket, null)
		};
	}

	// Token: 0x06001432 RID: 5170 RVA: 0x00074150 File Offset: 0x00072350
	public override void DoPostConfigureComplete(GameObject go)
	{
		BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MINOR_PLUS, 0f, 0f);
	}

	// Token: 0x04000C40 RID: 3136
	public const string ID = "ScannerModule";
}
