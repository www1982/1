using System;
using TUNING;
using UnityEngine;

// Token: 0x0200016C RID: 364
public class FirePoleConfig : IBuildingConfig
{
	// Token: 0x060006F5 RID: 1781 RVA: 0x00030928 File Offset: 0x0002EB28
	public override BuildingDef CreateBuildingDef()
	{
		string text = "FirePole";
		int num = 1;
		int num2 = 1;
		string text2 = "firepole_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER0, none, 0.2f);
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

	// Token: 0x060006F6 RID: 1782 RVA: 0x000309B1 File Offset: 0x0002EBB1
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		GeneratedBuildings.MakeBuildingAlwaysOperational(go);
		Ladder ladder = go.AddOrGet<Ladder>();
		ladder.isPole = true;
		ladder.upwardsMovementSpeedMultiplier = 0.25f;
		ladder.downwardsMovementSpeedMultiplier = 4f;
		go.AddOrGet<AnimTileable>();
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x000309E2 File Offset: 0x0002EBE2
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400054B RID: 1355
	public const string ID = "FirePole";
}
