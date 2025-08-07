using System;
using TUNING;
using UnityEngine;

// Token: 0x020003D8 RID: 984
public class RoleStationConfig : IBuildingConfig
{
	// Token: 0x06001423 RID: 5155 RVA: 0x00073A8C File Offset: 0x00071C8C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "RoleStation";
		int num = 2;
		int num2 = 2;
		string text2 = "job_station_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.Deprecated = true;
		return buildingDef;
	}

	// Token: 0x06001424 RID: 5156 RVA: 0x00073AF6 File Offset: 0x00071CF6
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		Prioritizable.AddRef(go);
	}

	// Token: 0x06001425 RID: 5157 RVA: 0x00073B0A File Offset: 0x00071D0A
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000C30 RID: 3120
	public const string ID = "RoleStation";
}
