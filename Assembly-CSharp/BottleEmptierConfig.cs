using System;
using TUNING;
using UnityEngine;

// Token: 0x0200002D RID: 45
public class BottleEmptierConfig : IBuildingConfig
{
	// Token: 0x060000CA RID: 202 RVA: 0x00006E80 File Offset: 0x00005080
	public override BuildingDef CreateBuildingDef()
	{
		string text = "BottleEmptier";
		int num = 1;
		int num2 = 3;
		string text2 = "liquidator_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = false;
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		return buildingDef;
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00006EE8 File Offset: 0x000050E8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		Storage storage = go.AddOrGet<Storage>();
		storage.storageFilters = STORAGEFILTERS.LIQUIDS;
		storage.showInUI = true;
		storage.showDescriptor = true;
		storage.capacityKg = 200f;
		storage.gunTargetOffset = new Vector2(0f, 2f);
		go.AddOrGet<TreeFilterable>();
		go.AddOrGet<BottleEmptier>();
	}

	// Token: 0x060000CC RID: 204 RVA: 0x00006F47 File Offset: 0x00005147
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400008A RID: 138
	public const string ID = "BottleEmptier";
}
