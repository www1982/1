using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000025 RID: 37
public class BatteryConfig : BaseBatteryConfig
{
	// Token: 0x060000A7 RID: 167 RVA: 0x000064E4 File Offset: 0x000046E4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Battery";
		int num = 1;
		int num2 = 2;
		int num3 = 30;
		string text2 = "batterysm_kanim";
		float num4 = 30f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 800f;
		float num6 = 0.25f;
		float num7 = 1f;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = base.CreateBuildingDef(text, num, num2, num3, text2, num4, tier, all_METALS, num5, num6, num7, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER1, none);
		buildingDef.Breakable = true;
		SoundEventVolumeCache.instance.AddVolume("batterysm_kanim", "Battery_rattle", NOISE_POLLUTION.NOISY.TIER1);
		buildingDef.AddSearchTerms(SEARCH_TERMS.POWER);
		return buildingDef;
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x0000655F File Offset: 0x0000475F
	public override void DoPostConfigureComplete(GameObject go)
	{
		Battery battery = go.AddOrGet<Battery>();
		battery.capacity = 10000f;
		battery.joulesLostPerSecond = 1.6666666f;
		base.DoPostConfigureComplete(go);
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x00006583 File Offset: 0x00004783
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		base.ConfigureBuildingTemplate(go, prefab_tag);
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.PowerBuilding, false);
	}

	// Token: 0x04000079 RID: 121
	public const string ID = "Battery";
}
