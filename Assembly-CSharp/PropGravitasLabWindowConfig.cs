using System;
using TUNING;
using UnityEngine;

// Token: 0x020003A2 RID: 930
public class PropGravitasLabWindowConfig : IBuildingConfig
{
	// Token: 0x060012F8 RID: 4856 RVA: 0x0006C91C File Offset: 0x0006AB1C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "PropGravitasLabWindow";
		int num = 2;
		int num2 = 3;
		string text2 = "gravitas_lab_window_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier_TINY = BUILDINGS.CONSTRUCTION_MASS_KG.TIER_TINY;
		string[] glasses = MATERIALS.GLASSES;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.NotInTiles;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier_TINY, glasses, num5, buildLocationRule, DECOR.BONUS.TIER0, none, 0.2f);
		buildingDef.Entombable = false;
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.DefaultAnimState = "on";
		buildingDef.ObjectLayer = ObjectLayer.Backwall;
		buildingDef.SceneLayer = Grid.SceneLayer.Backwall;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x060012F9 RID: 4857 RVA: 0x0006C9B0 File Offset: 0x0006ABB0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<AnimTileable>().objectLayer = ObjectLayer.Backwall;
		go.AddComponent<ZoneTile>();
		go.GetComponent<PrimaryElement>().SetElement(SimHashes.Glass, true);
		go.GetComponent<PrimaryElement>().Temperature = 273f;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Gravitas, false);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	// Token: 0x060012FA RID: 4858 RVA: 0x0006CA17 File Offset: 0x0006AC17
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000B93 RID: 2963
	public const string ID = "PropGravitasLabWindow";
}
