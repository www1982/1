using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000252 RID: 594
public class IceSculptureConfig : IBuildingConfig
{
	// Token: 0x06000C05 RID: 3077 RVA: 0x000490EC File Offset: 0x000472EC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "IceSculpture";
		int num = 2;
		int num2 = 2;
		string text2 = "icesculpture_kanim";
		int num3 = 10;
		float num4 = 120f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] array = new string[] { "Ice" };
		float num5 = 273.15f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, array, num5, buildLocationRule, new EffectorValues
		{
			amount = 35,
			radius = 8
		}, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.DefaultAnimState = "slab";
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.Temperature = 253.15f;
		buildingDef.RequiredSkillPerkID = Db.Get().SkillPerks.CanArt.Id;
		buildingDef.AddSearchTerms(SEARCH_TERMS.STATUE);
		buildingDef.AddSearchTerms(SEARCH_TERMS.MORALE);
		buildingDef.AddSearchTerms(SEARCH_TERMS.ARTWORK);
		return buildingDef;
	}

	// Token: 0x06000C06 RID: 3078 RVA: 0x000491E6 File Offset: 0x000473E6
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isArtable = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x06000C07 RID: 3079 RVA: 0x00049205 File Offset: 0x00047405
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddComponent<Sculpture>().defaultAnimName = "slab";
	}

	// Token: 0x04000852 RID: 2130
	public const string ID = "IceSculpture";
}
