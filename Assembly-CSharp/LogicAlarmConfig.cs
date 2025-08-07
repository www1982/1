using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200028E RID: 654
public class LogicAlarmConfig : IBuildingConfig
{
	// Token: 0x06000D34 RID: 3380 RVA: 0x0004ED9C File Offset: 0x0004CF9C
	public override BuildingDef CreateBuildingDef()
	{
		string id = LogicAlarmConfig.ID;
		int num = 1;
		int num2 = 1;
		string text = "alarm_sensor_kanim";
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
		buildingDef.LogicInputPorts = new List<LogicPorts.Port> { LogicPorts.Port.InputPort(LogicAlarm.INPUT_PORT_ID, new CellOffset(0, 0), global::STRINGS.BUILDINGS.PREFABS.LOGICALARM.LOGIC_PORT, global::STRINGS.BUILDINGS.PREFABS.LOGICALARM.INPUT_PORT_ACTIVE, global::STRINGS.BUILDINGS.PREFABS.LOGICALARM.INPUT_PORT_INACTIVE, true, false) };
		SoundEventVolumeCache.instance.AddVolume("door_internal_kanim", "Open_DoorInternal", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("door_internal_kanim", "Close_DoorInternal", NOISE_POLLUTION.NOISY.TIER3);
		GeneratedBuildings.RegisterWithOverlay(OverlayModes.Logic.HighlightItemIDs, LogicAlarmConfig.ID);
		buildingDef.AddSearchTerms(SEARCH_TERMS.AUTOMATION);
		return buildingDef;
	}

	// Token: 0x06000D35 RID: 3381 RVA: 0x0004EEAF File Offset: 0x0004D0AF
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicAlarm>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x040008DA RID: 2266
	public static string ID = "LogicAlarm";
}
