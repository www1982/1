using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020001BE RID: 446
public class FlowerVaseHangingFancyConfig : IBuildingConfig
{
	// Token: 0x060008F0 RID: 2288 RVA: 0x0003C2A4 File Offset: 0x0003A4A4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "FlowerVaseHangingFancy";
		int num = 1;
		int num2 = 2;
		string text2 = "flowervase_hanging_kanim";
		int num3 = 10;
		float num4 = 10f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER1;
		string[] transparents = MATERIALS.TRANSPARENTS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnCeiling;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, transparents, num5, buildLocationRule, new EffectorValues
		{
			amount = global::TUNING.BUILDINGS.DECOR.BONUS.TIER1.amount,
			radius = global::TUNING.BUILDINGS.DECOR.BONUS.TIER3.radius
		}, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.AudioCategory = "Glass";
		buildingDef.AudioSize = "large";
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingBack;
		buildingDef.ForegroundLayer = Grid.SceneLayer.BuildingUse;
		buildingDef.GenerateOffsets(1, 1);
		buildingDef.AddSearchTerms(SEARCH_TERMS.GLASS);
		buildingDef.AddSearchTerms(SEARCH_TERMS.MORALE);
		return buildingDef;
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x0003C378 File Offset: 0x0003A578
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<Storage>();
		Prioritizable.AddRef(go);
		PlantablePlot plantablePlot = go.AddOrGet<PlantablePlot>();
		plantablePlot.AddDepositTag(GameTags.DecorSeed);
		plantablePlot.plantLayer = Grid.SceneLayer.BuildingUse;
		plantablePlot.occupyingObjectVisualOffset = new Vector3(0f, -0.45f, 0f);
		go.AddOrGet<FlowerVase>();
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x0003C3DC File Offset: 0x0003A5DC
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000692 RID: 1682
	public const string ID = "FlowerVaseHangingFancy";
}
