using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000068 RID: 104
public class CrownMouldingConfig : IBuildingConfig
{
	// Token: 0x060001EF RID: 495 RVA: 0x0000E14C File Offset: 0x0000C34C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CrownMoulding";
		int num = 1;
		int num2 = 1;
		string text2 = "crown_moulding_kanim";
		int num3 = 10;
		float num4 = 30f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 800f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnCeiling;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, new EffectorValues
		{
			amount = 5,
			radius = 3
		}, none, 0.2f);
		buildingDef.DefaultAnimState = "S_U";
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.AddSearchTerms(SEARCH_TERMS.MORALE);
		return buildingDef;
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
		go.AddOrGet<AnimTileable>();
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x0000E20A File Offset: 0x0000C40A
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000132 RID: 306
	public const string ID = "CrownMoulding";
}
