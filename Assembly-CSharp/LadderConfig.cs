using System;
using TUNING;
using UnityEngine;

// Token: 0x0200026B RID: 619
public class LadderConfig : IBuildingConfig
{
	// Token: 0x06000C87 RID: 3207 RVA: 0x0004B2A0 File Offset: 0x000494A0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Ladder";
		int num = 1;
		int num2 = 1;
		string text2 = "ladder_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] raw_MINERALS_OR_WOOD = MATERIALS.RAW_MINERALS_OR_WOOD;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS_OR_WOOD, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
		BuildingTemplates.CreateLadderDef(buildingDef);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.Entombable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.DragBuild = true;
		return buildingDef;
	}

	// Token: 0x06000C88 RID: 3208 RVA: 0x0004B329 File Offset: 0x00049529
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		Ladder ladder = go.AddOrGet<Ladder>();
		ladder.upwardsMovementSpeedMultiplier = 1f;
		ladder.downwardsMovementSpeedMultiplier = 1f;
		go.AddOrGet<AnimTileable>();
	}

	// Token: 0x06000C89 RID: 3209 RVA: 0x0004B353 File Offset: 0x00049553
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000895 RID: 2197
	public const string ID = "Ladder";
}
