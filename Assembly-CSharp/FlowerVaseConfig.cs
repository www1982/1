using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001BC RID: 444
public class FlowerVaseConfig : IBuildingConfig
{
	// Token: 0x060008E8 RID: 2280 RVA: 0x0003C0DC File Offset: 0x0003A2DC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "FlowerVase";
		int num = 1;
		int num2 = 1;
		string text2 = "flowervase_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.AudioCategory = "Glass";
		buildingDef.AudioSize = "large";
		buildingDef.AddSearchTerms(SEARCH_TERMS.MORALE);
		return buildingDef;
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x0003C161 File Offset: 0x0003A361
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<Storage>();
		Prioritizable.AddRef(go);
		PlantablePlot plantablePlot = go.AddOrGet<PlantablePlot>();
		plantablePlot.IsOffGround = true;
		plantablePlot.AddDepositTag(GameTags.DecorSeed);
		go.AddOrGet<FlowerVase>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x0003C19F File Offset: 0x0003A39F
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000690 RID: 1680
	public const string ID = "FlowerVase";
}
