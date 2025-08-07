using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000026 RID: 38
public class BatteryMediumConfig : BaseBatteryConfig
{
	// Token: 0x060000AB RID: 171 RVA: 0x000065A8 File Offset: 0x000047A8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "BatteryMedium";
		int num = 2;
		int num2 = 2;
		int num3 = 30;
		string text2 = "batterymed_kanim";
		float num4 = 60f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 800f;
		float num6 = 0.25f;
		float num7 = 1f;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = base.CreateBuildingDef(text, num, num2, num3, text2, num4, tier, all_METALS, num5, num6, num7, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER2, tier2);
		SoundEventVolumeCache.instance.AddVolume("batterymed_kanim", "Battery_med_rattle", NOISE_POLLUTION.NOISY.TIER2);
		buildingDef.AddSearchTerms(SEARCH_TERMS.POWER);
		return buildingDef;
	}

	// Token: 0x060000AC RID: 172 RVA: 0x0000661C File Offset: 0x0000481C
	public override void DoPostConfigureComplete(GameObject go)
	{
		Battery battery = go.AddOrGet<Battery>();
		battery.capacity = 40000f;
		battery.joulesLostPerSecond = 3.3333333f;
		base.DoPostConfigureComplete(go);
	}

	// Token: 0x060000AD RID: 173 RVA: 0x00006640 File Offset: 0x00004840
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		base.ConfigureBuildingTemplate(go, prefab_tag);
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.PowerBuilding, false);
	}

	// Token: 0x0400007A RID: 122
	public const string ID = "BatteryMedium";
}
