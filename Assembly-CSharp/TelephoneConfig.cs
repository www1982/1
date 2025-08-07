using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000425 RID: 1061
public class TelephoneConfig : IBuildingConfig
{
	// Token: 0x060015D8 RID: 5592 RVA: 0x0007CB59 File Offset: 0x0007AD59
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060015D9 RID: 5593 RVA: 0x0007CB60 File Offset: 0x0007AD60
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Telephone";
		int num = 1;
		int num2 = 2;
		string text2 = "telephone_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.BONUS.TIER1, none, 0.2f);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.Floodable = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = true;
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(0, 0);
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.SelfHeatKilowattsWhenActive = 0.5f;
		buildingDef.AddSearchTerms(SEARCH_TERMS.MORALE);
		return buildingDef;
	}

	// Token: 0x060015DA RID: 5594 RVA: 0x0007CC04 File Offset: 0x0007AE04
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.RecBuilding, false);
		Telephone telephone = go.AddOrGet<Telephone>();
		telephone.babbleEffect = "TelephoneBabble";
		telephone.chatEffect = "TelephoneChat";
		telephone.longDistanceEffect = "TelephoneLongDistance";
		telephone.trackingEffect = "RecentlyTelephoned";
		go.AddOrGet<TelephoneCallerWorkable>().basePriority = RELAXATION.PRIORITY.TIER5;
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.RecRoom.Id;
		roomTracker.requirement = RoomTracker.Requirement.Recommended;
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x060015DB RID: 5595 RVA: 0x0007CC90 File Offset: 0x0007AE90
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000CE9 RID: 3305
	public const string ID = "Telephone";

	// Token: 0x04000CEA RID: 3306
	public const float ringTime = 15f;

	// Token: 0x04000CEB RID: 3307
	public const float callTime = 25f;
}
