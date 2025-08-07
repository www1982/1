using System;
using TUNING;
using UnityEngine;

// Token: 0x0200001E RID: 30
public class AstronautTrainingCenterConfig : IBuildingConfig
{
	// Token: 0x06000083 RID: 131 RVA: 0x000057E4 File Offset: 0x000039E4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "AstronautTrainingCenter";
		int num = 5;
		int num2 = 5;
		string text2 = "centrifuge_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 480f;
		buildingDef.ExhaustKilowattsWhenActive = 0.5f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.PowerInputOffset = new CellOffset(-2, 0);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.Deprecated = true;
		return buildingDef;
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00005888 File Offset: 0x00003A88
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		Prioritizable.AddRef(go);
		AstronautTrainingCenter astronautTrainingCenter = go.AddOrGet<AstronautTrainingCenter>();
		astronautTrainingCenter.workTime = float.PositiveInfinity;
		astronautTrainingCenter.requiredSkillPerk = Db.Get().SkillPerks.CanTrainToBeAstronaut.Id;
		astronautTrainingCenter.daysToMasterRole = 10f;
		astronautTrainingCenter.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_centrifuge_kanim") };
		astronautTrainingCenter.workLayer = Grid.SceneLayer.BuildingFront;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00005904 File Offset: 0x00003B04
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000068 RID: 104
	public const string ID = "AstronautTrainingCenter";
}
