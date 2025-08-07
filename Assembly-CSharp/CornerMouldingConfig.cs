using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200005E RID: 94
public class CornerMouldingConfig : IBuildingConfig
{
	// Token: 0x060001BC RID: 444 RVA: 0x0000CDCC File Offset: 0x0000AFCC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CornerMoulding";
		int num = 1;
		int num2 = 1;
		string text2 = "corner_tile_kanim";
		int num3 = 10;
		float num4 = 30f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.InCorner;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, new EffectorValues
		{
			amount = 5,
			radius = 3
		}, none, 0.2f);
		buildingDef.DefaultAnimState = "corner";
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.AddSearchTerms(SEARCH_TERMS.MORALE);
		return buildingDef;
	}

	// Token: 0x060001BD RID: 445 RVA: 0x0000CE77 File Offset: 0x0000B077
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x060001BE RID: 446 RVA: 0x0000CE8A File Offset: 0x0000B08A
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400011E RID: 286
	public const string ID = "CornerMoulding";
}
