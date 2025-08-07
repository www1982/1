using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200001B RID: 27
public class ArcadeMachineConfig : IBuildingConfig
{
	// Token: 0x06000075 RID: 117 RVA: 0x000053D8 File Offset: 0x000035D8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ArcadeMachine";
		int num = 3;
		int num2 = 3;
		string text2 = "arcade_cabinet_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.Floodable = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = true;
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 1200f;
		buildingDef.SelfHeatKilowattsWhenActive = 2f;
		buildingDef.AddSearchTerms(SEARCH_TERMS.MORALE);
		return buildingDef;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00005470 File Offset: 0x00003670
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.RecBuilding, false);
		go.AddOrGet<ArcadeMachine>();
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.RecRoom.Id;
		roomTracker.requirement = RoomTracker.Requirement.Recommended;
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x06000077 RID: 119 RVA: 0x000054C2 File Offset: 0x000036C2
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000062 RID: 98
	public const string ID = "ArcadeMachine";

	// Token: 0x04000063 RID: 99
	public const string SPECIFIC_EFFECT = "PlayedArcade";

	// Token: 0x04000064 RID: 100
	public const string TRACKING_EFFECT = "RecentlyPlayedArcade";
}
