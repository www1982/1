using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000038 RID: 56
public class CanvasWideConfig : IBuildingConfig
{
	// Token: 0x06000106 RID: 262 RVA: 0x00008550 File Offset: 0x00006750
	public override BuildingDef CreateBuildingDef()
	{
		string text = "CanvasWide";
		int num = 3;
		int num2 = 2;
		string text2 = "painting_wide_off_kanim";
		int num3 = 30;
		float num4 = 120f;
		float[] array = new float[] { 400f, 1f };
		string[] array2 = new string[] { "Metal", "BuildingFiber" };
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array, array2, num5, buildLocationRule, new EffectorValues
		{
			amount = 15,
			radius = 6
		}, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.SceneLayer = Grid.SceneLayer.InteriorWall;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.ViewMode = OverlayModes.Decor.ID;
		buildingDef.DefaultAnimState = "off";
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.RequiredSkillPerkID = Db.Get().SkillPerks.CanArt.Id;
		buildingDef.AddSearchTerms(SEARCH_TERMS.MORALE);
		buildingDef.AddSearchTerms(SEARCH_TERMS.ARTWORK);
		return buildingDef;
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00008650 File Offset: 0x00006850
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<BuildingComplete>().isArtable = true;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Decoration, false);
	}

	// Token: 0x06000108 RID: 264 RVA: 0x0000866F File Offset: 0x0000686F
	public override void DoPostConfigureComplete(GameObject go)
	{
		SymbolOverrideControllerUtil.AddToPrefab(go);
		go.AddComponent<Painting>().defaultAnimName = "off";
	}

	// Token: 0x040000A4 RID: 164
	public const string ID = "CanvasWide";
}
