using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000249 RID: 585
public class HighEnergyParticleSpawnerConfig : IBuildingConfig
{
	// Token: 0x06000BD7 RID: 3031 RVA: 0x00047E2C File Offset: 0x0004602C
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x06000BD8 RID: 3032 RVA: 0x00047E34 File Offset: 0x00046034
	public override BuildingDef CreateBuildingDef()
	{
		string text = "HighEnergyParticleSpawner";
		int num = 1;
		int num2 = 2;
		string text2 = "radiation_collector_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.NotInTiles;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Radiation.ID;
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.UseHighEnergyParticleOutputPort = true;
		buildingDef.HighEnergyParticleOutputOffset = new CellOffset(0, 1);
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(0, 0);
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.ExhaustKilowattsWhenActive = 1f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.DiseaseCellVisName = "RadiationSickness";
		buildingDef.UtilityOutputOffset = CellOffset.none;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.RadiationIDs, "HighEnergyParticleSpawner");
		buildingDef.Deprecated = !Sim.IsRadiationEnabled();
		buildingDef.AddSearchTerms(SEARCH_TERMS.POWER);
		buildingDef.AddSearchTerms(SEARCH_TERMS.GENERATOR);
		return buildingDef;
	}

	// Token: 0x06000BD9 RID: 3033 RVA: 0x00047F44 File Offset: 0x00046144
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		Prioritizable.AddRef(go);
		go.AddOrGet<HighEnergyParticleStorage>().capacity = 500f;
		go.AddOrGet<LoopingSounds>();
		HighEnergyParticleSpawner highEnergyParticleSpawner = go.AddOrGet<HighEnergyParticleSpawner>();
		highEnergyParticleSpawner.minLaunchInterval = 2f;
		highEnergyParticleSpawner.radiationSampleRate = 0.2f;
		highEnergyParticleSpawner.minSlider = 50;
		highEnergyParticleSpawner.maxSlider = 500;
	}

	// Token: 0x06000BDA RID: 3034 RVA: 0x00047FB1 File Offset: 0x000461B1
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000820 RID: 2080
	public const string ID = "HighEnergyParticleSpawner";

	// Token: 0x04000821 RID: 2081
	public const float MIN_LAUNCH_INTERVAL = 2f;

	// Token: 0x04000822 RID: 2082
	public const float RADIATION_SAMPLE_RATE = 0.2f;

	// Token: 0x04000823 RID: 2083
	public const float HEP_PER_RAD = 0.1f;

	// Token: 0x04000824 RID: 2084
	public const int MIN_SLIDER = 50;

	// Token: 0x04000825 RID: 2085
	public const int MAX_SLIDER = 500;

	// Token: 0x04000826 RID: 2086
	public const float DISABLED_CONSUMPTION_RATE = 1f;
}
