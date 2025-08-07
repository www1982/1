using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000028 RID: 40
public class BatterySmartConfig : BaseBatteryConfig
{
	// Token: 0x060000B5 RID: 181 RVA: 0x0000681C File Offset: 0x00004A1C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "BatterySmart";
		int num = 2;
		int num2 = 2;
		int num3 = 30;
		string text2 = "smartbattery_kanim";
		float num4 = 60f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 800f;
		float num6 = 0f;
		float num7 = 0.5f;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = base.CreateBuildingDef(text, num, num2, num3, text2, num4, tier, refined_METALS, num5, num6, num7, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER2, tier2);
		SoundEventVolumeCache.instance.AddVolume("batterymed_kanim", "Battery_med_rattle", NOISE_POLLUTION.NOISY.TIER2);
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort(BatterySmart.PORT_ID, new CellOffset(0, 0), global::STRINGS.BUILDINGS.PREFABS.BATTERYSMART.LOGIC_PORT, global::STRINGS.BUILDINGS.PREFABS.BATTERYSMART.LOGIC_PORT_ACTIVE, global::STRINGS.BUILDINGS.PREFABS.BATTERYSMART.LOGIC_PORT_INACTIVE, true, false) };
		buildingDef.AddSearchTerms(SEARCH_TERMS.POWER);
		buildingDef.AddSearchTerms(SEARCH_TERMS.BATTERY);
		return buildingDef;
	}

	// Token: 0x060000B6 RID: 182 RVA: 0x000068E2 File Offset: 0x00004AE2
	public override void DoPostConfigureComplete(GameObject go)
	{
		BatterySmart batterySmart = go.AddOrGet<BatterySmart>();
		batterySmart.capacity = 20000f;
		batterySmart.joulesLostPerSecond = 0.6666667f;
		batterySmart.powerSortOrder = 1000;
		base.DoPostConfigureComplete(go);
	}

	// Token: 0x060000B7 RID: 183 RVA: 0x00006911 File Offset: 0x00004B11
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		base.ConfigureBuildingTemplate(go, prefab_tag);
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.PowerBuilding, false);
	}

	// Token: 0x0400007E RID: 126
	public const string ID = "BatterySmart";
}
