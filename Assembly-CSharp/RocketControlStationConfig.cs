using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003CA RID: 970
public class RocketControlStationConfig : IBuildingConfig
{
	// Token: 0x060013CD RID: 5069 RVA: 0x00072377 File Offset: 0x00070577
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060013CE RID: 5070 RVA: 0x00072380 File Offset: 0x00070580
	public override BuildingDef CreateBuildingDef()
	{
		string id = RocketControlStationConfig.ID;
		int num = 2;
		int num2 = 2;
		string text = "rocket_control_station_kanim";
		int num3 = 30;
		float num4 = 60f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER3;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, raw_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.BONUS.TIER2, tier2, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.Repairable = false;
		buildingDef.Floodable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.DefaultAnimState = "off";
		buildingDef.OnePerWorld = true;
		buildingDef.RequiredSkillPerkID = Db.Get().SkillPerks.CanUseRocketControlStation.Id;
		buildingDef.LogicInputPorts = new List<LogicPorts.Port> { LogicPorts.Port.InputPort(RocketControlStation.PORT_ID, new CellOffset(0, 0), global::STRINGS.BUILDINGS.PREFABS.ROCKETCONTROLSTATION.LOGIC_PORT, global::STRINGS.BUILDINGS.PREFABS.ROCKETCONTROLSTATION.LOGIC_PORT_ACTIVE, global::STRINGS.BUILDINGS.PREFABS.ROCKETCONTROLSTATION.LOGIC_PORT_INACTIVE, false, false) };
		buildingDef.AddSearchTerms(SEARCH_TERMS.ROCKET);
		return buildingDef;
	}

	// Token: 0x060013CF RID: 5071 RVA: 0x0007246F File Offset: 0x0007066F
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		KPrefabID component = go.GetComponent<KPrefabID>();
		component.AddTag(GameTags.RocketInteriorBuilding, false);
		component.AddTag(GameTags.UniquePerWorld, false);
	}

	// Token: 0x060013D0 RID: 5072 RVA: 0x00072490 File Offset: 0x00070690
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		go.AddOrGet<RocketControlStationIdleWorkable>().workLayer = Grid.SceneLayer.BuildingUse;
		go.AddOrGet<RocketControlStationLaunchWorkable>().workLayer = Grid.SceneLayer.BuildingUse;
		go.AddOrGet<RocketControlStation>();
		go.AddOrGetDef<PoweredController.Def>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.RocketInterior, false);
	}

	// Token: 0x04000C09 RID: 3081
	public static string ID = "RocketControlStation";

	// Token: 0x04000C0A RID: 3082
	public const float CONSOLE_WORK_TIME = 30f;

	// Token: 0x04000C0B RID: 3083
	public const float CONSOLE_IDLE_TIME = 120f;

	// Token: 0x04000C0C RID: 3084
	public const float WARNING_COOLDOWN = 30f;

	// Token: 0x04000C0D RID: 3085
	public const float DEFAULT_SPEED = 1f;

	// Token: 0x04000C0E RID: 3086
	public const float SLOW_SPEED = 0.5f;

	// Token: 0x04000C0F RID: 3087
	public const float SUPER_SPEED = 1.5f;

	// Token: 0x04000C10 RID: 3088
	public const float DEFAULT_PILOT_MODIFIER = 1f;
}
