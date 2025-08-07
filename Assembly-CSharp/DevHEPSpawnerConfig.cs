using System;
using TUNING;
using UnityEngine;

// Token: 0x0200006F RID: 111
public class DevHEPSpawnerConfig : IBuildingConfig
{
	// Token: 0x06000214 RID: 532 RVA: 0x0000EFC2 File Offset: 0x0000D1C2
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000215 RID: 533 RVA: 0x0000EFCC File Offset: 0x0000D1CC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "DevHEPSpawner";
		int num = 1;
		int num2 = 1;
		string text2 = "dev_radbolt_generator_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.NotInTiles;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Invincible = true;
		buildingDef.Overheatable = false;
		buildingDef.Entombable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Radiation.ID;
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.UseHighEnergyParticleOutputPort = true;
		buildingDef.HighEnergyParticleOutputOffset = new CellOffset(0, 0);
		buildingDef.RequiresPowerInput = false;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.RadiationIDs, "DevHEPSpawner");
		buildingDef.Deprecated = !Sim.IsRadiationEnabled();
		buildingDef.DebugOnly = true;
		return buildingDef;
	}

	// Token: 0x06000216 RID: 534 RVA: 0x0000F0A4 File Offset: 0x0000D2A4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddTag(GameTags.DevBuilding);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		Prioritizable.AddRef(go);
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<DevHEPSpawner>().boltAmount = 50f;
		go.AddOrGet<LogicOperationalController>();
	}

	// Token: 0x06000217 RID: 535 RVA: 0x0000F0F5 File Offset: 0x0000D2F5
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000155 RID: 341
	public const string ID = "DevHEPSpawner";
}
