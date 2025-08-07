using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000048 RID: 72
public class CometDetectorConfig : IBuildingConfig
{
	// Token: 0x06000167 RID: 359 RVA: 0x0000A88C File Offset: 0x00008A8C
	public override BuildingDef CreateBuildingDef()
	{
		string id = CometDetectorConfig.ID;
		int num = 2;
		int num2 = 4;
		string text = "meteor_detector_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, refined_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.Floodable = true;
		buildingDef.Entombable = true;
		buildingDef.RequiresPowerInput = true;
		buildingDef.AddLogicPowerPort = false;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.ViewMode = OverlayModes.Logic.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), global::STRINGS.BUILDINGS.PREFABS.LOGICSWITCH.LOGIC_PORT, global::STRINGS.BUILDINGS.PREFABS.LOGICSWITCH.LOGIC_PORT_ACTIVE, global::STRINGS.BUILDINGS.PREFABS.LOGICSWITCH.LOGIC_PORT_INACTIVE, true, false) };
		SoundEventVolumeCache.instance.AddVolume("world_element_sensor_kanim", "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("world_element_sensor_kanim", "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, CometDetectorConfig.ID);
		return buildingDef;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x0000A9A4 File Offset: 0x00008BA4
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		if (DlcManager.IsExpansion1Active())
		{
			go.AddOrGetDef<ClusterCometDetector.Def>();
		}
		else
		{
			go.AddOrGetDef<CometDetector.Def>();
		}
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits, false);
		CometDetectorConfig.AddVisualizer(go);
	}

	// Token: 0x06000169 RID: 361 RVA: 0x0000A9F0 File Offset: 0x00008BF0
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		CometDetectorConfig.AddVisualizer(go);
	}

	// Token: 0x0600016A RID: 362 RVA: 0x0000A9F8 File Offset: 0x00008BF8
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		CometDetectorConfig.AddVisualizer(go);
	}

	// Token: 0x0600016B RID: 363 RVA: 0x0000AA00 File Offset: 0x00008C00
	private static void AddVisualizer(GameObject prefab)
	{
		ScannerNetworkVisualizer scannerNetworkVisualizer = prefab.AddOrGet<ScannerNetworkVisualizer>();
		scannerNetworkVisualizer.RangeMin = -15;
		scannerNetworkVisualizer.RangeMax = 15;
	}

	// Token: 0x040000DA RID: 218
	public static string ID = "CometDetector";

	// Token: 0x040000DB RID: 219
	public const float COVERAGE_REQUIRED_01 = 0.5f;

	// Token: 0x040000DC RID: 220
	public const float BEST_WARNING_TIME_IN_SECONDS = 200f;

	// Token: 0x040000DD RID: 221
	public const float WORST_WARNING_TIME_IN_SECONDS = 1f;

	// Token: 0x040000DE RID: 222
	public const int SCAN_RADIUS = 15;

	// Token: 0x040000DF RID: 223
	public static readonly SkyVisibilityInfo SKY_VISIBILITY_INFO = new SkyVisibilityInfo(new CellOffset(0, 0), 15, new CellOffset(0, 0), 15, 1);

	// Token: 0x040000E0 RID: 224
	public const float LOGIC_SIGNAL_DELAY_ON_LOAD = 3f;
}
