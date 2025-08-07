using System;
using TUNING;
using UnityEngine;

// Token: 0x02000070 RID: 112
public class DevHeaterConfig : IBuildingConfig
{
	// Token: 0x06000219 RID: 537 RVA: 0x0000F100 File Offset: 0x0000D300
	public override BuildingDef CreateBuildingDef()
	{
		string text = "DevHeater";
		int num = 1;
		int num2 = 1;
		string text2 = "dev_generator_kanim";
		int num3 = 100;
		float num4 = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, tier2, 0.2f);
		buildingDef.RequiresPowerInput = false;
		buildingDef.ViewMode = OverlayModes.Light.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.AudioSize = "large";
		buildingDef.Floodable = false;
		buildingDef.DebugOnly = true;
		buildingDef.Overheatable = false;
		SoundEventVolumeCache.instance.AddVolume("dev_lightgenerator_kanim", "PowerSwitch_on", NOISE_POLLUTION.NOISY.TIER3);
		SoundEventVolumeCache.instance.AddVolume("dev_lightgenerator_kanim", "PowerSwitch_off", NOISE_POLLUTION.NOISY.TIER3);
		return buildingDef;
	}

	// Token: 0x0600021A RID: 538 RVA: 0x0000F1B5 File Offset: 0x0000D3B5
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
	}

	// Token: 0x0600021B RID: 539 RVA: 0x0000F1B7 File Offset: 0x0000D3B7
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddTag(GameTags.DevBuilding);
	}

	// Token: 0x0600021C RID: 540 RVA: 0x0000F1C4 File Offset: 0x0000D3C4
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<DirectVolumeHeater>();
	}

	// Token: 0x04000156 RID: 342
	public const string ID = "DevHeater";
}
