using System;
using TUNING;
using UnityEngine;

// Token: 0x0200001C RID: 28
public class ArtifactAnalysisStationConfig : IBuildingConfig
{
	// Token: 0x06000079 RID: 121 RVA: 0x000054CC File Offset: 0x000036CC
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x000054D4 File Offset: 0x000036D4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ArtifactAnalysisStation";
		int num = 4;
		int num2 = 4;
		string text2 = "artifact_analysis_kanim";
		int num3 = 30;
		float num4 = 60f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER6;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.SelfHeatKilowattsWhenActive = 1f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.AudioSize = "large";
		buildingDef.RequiredSkillPerkID = Db.Get().SkillPerks.CanStudyArtifact.Id;
		return buildingDef;
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00005574 File Offset: 0x00003774
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<DropAllWorkable>();
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		go.AddOrGetDef<ArtifactAnalysisStation.Def>();
		go.AddOrGet<ArtifactAnalysisStationWorkable>();
		Prioritizable.AddRef(go);
		Storage storage = go.AddOrGet<Storage>();
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
		manualDeliveryKG.RequestedItemTag = GameTags.CharmedArtifact;
		manualDeliveryKG.refillMass = 1f * ArtifactConfig.ARTIFACT_MASS;
		manualDeliveryKG.MinimumMass = 1f * ArtifactConfig.ARTIFACT_MASS;
		manualDeliveryKG.capacity = 1f * ArtifactConfig.ARTIFACT_MASS;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00005613 File Offset: 0x00003813
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000065 RID: 101
	public const string ID = "ArtifactAnalysisStation";

	// Token: 0x04000066 RID: 102
	public const float WORK_TIME = 150f;
}
