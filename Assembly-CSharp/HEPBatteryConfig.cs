using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000240 RID: 576
public class HEPBatteryConfig : IBuildingConfig
{
	// Token: 0x06000BA2 RID: 2978 RVA: 0x000467FD File Offset: 0x000449FD
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000BA3 RID: 2979 RVA: 0x00046804 File Offset: 0x00044A04
	public override BuildingDef CreateBuildingDef()
	{
		string text = "HEPBattery";
		int num = 3;
		int num2 = 3;
		string text2 = "radbolt_battery_kanim";
		int num3 = 30;
		float num4 = 120f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER2, none, 0.2f);
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Radiation.ID;
		buildingDef.UseHighEnergyParticleInputPort = true;
		buildingDef.HighEnergyParticleInputOffset = new CellOffset(0, 1);
		buildingDef.UseHighEnergyParticleOutputPort = true;
		buildingDef.HighEnergyParticleOutputOffset = new CellOffset(0, 2);
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(0, 0);
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.ExhaustKilowattsWhenActive = 0.25f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		buildingDef.AddLogicPowerPort = true;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.RadiationIDs, "HEPBattery");
		buildingDef.LogicOutputPorts = new List<LogicPorts.Port> { LogicPorts.Port.OutputPort("HEP_STORAGE", new CellOffset(1, 1), global::STRINGS.BUILDINGS.PREFABS.HEPBATTERY.LOGIC_PORT_STORAGE, global::STRINGS.BUILDINGS.PREFABS.HEPBATTERY.LOGIC_PORT_STORAGE_ACTIVE, global::STRINGS.BUILDINGS.PREFABS.HEPBATTERY.LOGIC_PORT_STORAGE_INACTIVE, false, false) };
		buildingDef.LogicInputPorts = new List<LogicPorts.Port> { LogicPorts.Port.InputPort(HEPBattery.FIRE_PORT_ID, new CellOffset(0, 2), global::STRINGS.BUILDINGS.PREFABS.HEPBATTERY.LOGIC_PORT, global::STRINGS.BUILDINGS.PREFABS.HEPBATTERY.LOGIC_PORT_ACTIVE, global::STRINGS.BUILDINGS.PREFABS.HEPBATTERY.LOGIC_PORT_INACTIVE, false, false) };
		return buildingDef;
	}

	// Token: 0x06000BA4 RID: 2980 RVA: 0x00046978 File Offset: 0x00044B78
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		HighEnergyParticleStorage highEnergyParticleStorage = go.AddOrGet<HighEnergyParticleStorage>();
		highEnergyParticleStorage.capacity = 1000f;
		highEnergyParticleStorage.autoStore = true;
		highEnergyParticleStorage.PORT_ID = "HEP_STORAGE";
		highEnergyParticleStorage.showCapacityStatusItem = true;
		highEnergyParticleStorage.showCapacityAsMainStatus = true;
		go.AddOrGet<LoopingSounds>();
		HEPBattery.Def def = go.AddOrGetDef<HEPBattery.Def>();
		def.minLaunchInterval = 1f;
		def.minSlider = 0f;
		def.maxSlider = 100f;
		def.particleDecayRate = 0.5f;
	}

	// Token: 0x06000BA5 RID: 2981 RVA: 0x000469F3 File Offset: 0x00044BF3
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040007FC RID: 2044
	public const string ID = "HEPBattery";

	// Token: 0x040007FD RID: 2045
	public const float MIN_LAUNCH_INTERVAL = 1f;

	// Token: 0x040007FE RID: 2046
	public const int MIN_SLIDER = 0;

	// Token: 0x040007FF RID: 2047
	public const int MAX_SLIDER = 100;

	// Token: 0x04000800 RID: 2048
	public const float HEP_CAPACITY = 1000f;

	// Token: 0x04000801 RID: 2049
	public const float DISABLED_DECAY_RATE = 0.5f;

	// Token: 0x04000802 RID: 2050
	public const string STORAGE_PORT_ID = "HEP_STORAGE";

	// Token: 0x04000803 RID: 2051
	public const string FIRE_PORT_ID = "HEP_FIRE";
}
