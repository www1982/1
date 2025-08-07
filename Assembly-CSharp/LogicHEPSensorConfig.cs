using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200029F RID: 671
public class LogicHEPSensorConfig : IBuildingConfig
{
	// Token: 0x06000D9F RID: 3487 RVA: 0x000501A1 File Offset: 0x0004E3A1
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x000501A8 File Offset: 0x0004E3A8
	public override BuildingDef CreateBuildingDef()
	{
		string id = LogicHEPSensorConfig.ID;
		int num = 1;
		int num2 = 1;
		string text = LogicHEPSensorConfig.kanim;
		int num3 = 30;
		float num4 = 30f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, refined_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.ViewMode = OverlayModes.Logic.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.SceneLayer = Grid.SceneLayer.Building;
		buildingDef.AlwaysOperational = true;
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(LogicSwitch.PORT_ID, new CellOffset(0, 0), global::STRINGS.BUILDINGS.PREFABS.LOGICHEPSENSOR.LOGIC_PORT, global::STRINGS.BUILDINGS.PREFABS.LOGICHEPSENSOR.LOGIC_PORT_ACTIVE, global::STRINGS.BUILDINGS.PREFABS.LOGICHEPSENSOR.LOGIC_PORT_INACTIVE, true, false) };
		SoundEventVolumeCache.instance.AddVolume(LogicHEPSensorConfig.kanim, "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume(LogicHEPSensorConfig.kanim, "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, LogicHEPSensorConfig.ID);
		buildingDef.AddSearchTerms(SEARCH_TERMS.AUTOMATION);
		return buildingDef;
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x000502BB File Offset: 0x0004E4BB
	public override void DoPostConfigureComplete(GameObject go)
	{
		LogicHEPSensor logicHEPSensor = go.AddOrGet<LogicHEPSensor>();
		logicHEPSensor.manuallyControlled = false;
		logicHEPSensor.activateOnHigherThan = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x040008EB RID: 2283
	public static string ID = "LogicHEPSensor";

	// Token: 0x040008EC RID: 2284
	private static readonly string kanim = "radbolt_sensor_kanim";
}
