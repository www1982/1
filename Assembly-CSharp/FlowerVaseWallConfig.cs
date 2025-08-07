using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001BF RID: 447
public class FlowerVaseWallConfig : IBuildingConfig
{
	// Token: 0x060008F4 RID: 2292 RVA: 0x0003C3E8 File Offset: 0x0003A5E8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "FlowerVaseWall";
		int num = 1;
		int num2 = 1;
		string text2 = "flowervase_wall_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnWall;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.AudioCategory = "Glass";
		buildingDef.AudioSize = "large";
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.AddSearchTerms(SEARCH_TERMS.MORALE);
		return buildingDef;
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x0003C474 File Offset: 0x0003A674
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<Storage>();
		Prioritizable.AddRef(go);
		PlantablePlot plantablePlot = go.AddOrGet<PlantablePlot>();
		plantablePlot.AddDepositTag(GameTags.DecorSeed);
		plantablePlot.occupyingObjectVisualOffset = new Vector3(0f, -0.25f, 0f);
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x0003C4C9 File Offset: 0x0003A6C9
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000693 RID: 1683
	public const string ID = "FlowerVaseWall";
}
