using System;
using TUNING;
using UnityEngine;

// Token: 0x0200026C RID: 620
public class LadderFastConfig : IBuildingConfig
{
	// Token: 0x06000C8B RID: 3211 RVA: 0x0004B360 File Offset: 0x00049560
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LadderFast";
		int num = 1;
		int num2 = 1;
		string text2 = "ladder_plastic_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] plastics = MATERIALS.PLASTICS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, plastics, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
		BuildingTemplates.CreateLadderDef(buildingDef);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.Entombable = false;
		buildingDef.AudioCategory = "Plastic";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.DragBuild = true;
		return buildingDef;
	}

	// Token: 0x06000C8C RID: 3212 RVA: 0x0004B3E9 File Offset: 0x000495E9
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		Ladder ladder = go.AddOrGet<Ladder>();
		ladder.upwardsMovementSpeedMultiplier = 1.2f;
		ladder.downwardsMovementSpeedMultiplier = 1.2f;
		go.AddOrGet<AnimTileable>();
	}

	// Token: 0x06000C8D RID: 3213 RVA: 0x0004B413 File Offset: 0x00049613
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000896 RID: 2198
	public const string ID = "LadderFast";
}
