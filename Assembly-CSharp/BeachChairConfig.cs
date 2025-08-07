using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000029 RID: 41
public class BeachChairConfig : IBuildingConfig
{
	// Token: 0x060000B9 RID: 185 RVA: 0x00006934 File Offset: 0x00004B34
	public override BuildingDef CreateBuildingDef()
	{
		string text = "BeachChair";
		int num = 2;
		int num2 = 3;
		string text2 = "beach_chair_kanim";
		int num3 = 30;
		float num4 = 60f;
		float[] array = new float[] { 400f, 2f };
		string[] array2 = new string[] { "BuildableRaw", "BuildingFiber" };
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.BONUS.TIER4, none, 0.2f);
		buildingDef.Floodable = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = true;
		buildingDef.AddSearchTerms(SEARCH_TERMS.MORALE);
		return buildingDef;
	}

	// Token: 0x060000BA RID: 186 RVA: 0x000069C8 File Offset: 0x00004BC8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.RecBuilding, false);
		go.AddOrGet<BeachChairWorkable>().basePriority = RELAXATION.PRIORITY.TIER4;
		BeachChair beachChair = go.AddOrGet<BeachChair>();
		beachChair.specificEffectUnlit = "BeachChairUnlit";
		beachChair.specificEffectLit = "BeachChairLit";
		beachChair.trackingEffect = "RecentlyBeachChair";
		RoomTracker roomTracker = go.AddOrGet<RoomTracker>();
		roomTracker.requiredRoomType = Db.Get().RoomTypes.RecRoom.Id;
		roomTracker.requirement = RoomTracker.Requirement.Recommended;
		go.AddOrGet<AnimTileable>();
		go.AddOrGetDef<RocketUsageRestriction.Def>();
	}

	// Token: 0x060000BB RID: 187 RVA: 0x00006A50 File Offset: 0x00004C50
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400007F RID: 127
	public const string ID = "BeachChair";

	// Token: 0x04000080 RID: 128
	public static readonly int TAN_LUX = DUPLICANTSTATS.STANDARD.Light.HIGH_LIGHT;

	// Token: 0x04000081 RID: 129
	private const float TANK_SIZE_KG = 20f;

	// Token: 0x04000082 RID: 130
	private const float SPILL_RATE_KG = 0.05f;
}
