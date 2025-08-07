using System;
using TUNING;
using UnityEngine;

// Token: 0x020003A3 RID: 931
public class PropGravitasLabWindowHorizontalConfig : IBuildingConfig
{
	// Token: 0x060012FC RID: 4860 RVA: 0x0006CA21 File Offset: 0x0006AC21
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060012FD RID: 4861 RVA: 0x0006CA28 File Offset: 0x0006AC28
	public override BuildingDef CreateBuildingDef()
	{
		string text = "PropGravitasLabWindowHorizontal";
		int num = 3;
		int num2 = 2;
		string text2 = "gravitas_lab_window_horizontal_kanim";
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

	// Token: 0x060012FE RID: 4862 RVA: 0x0006CABC File Offset: 0x0006ACBC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<AnimTileable>().objectLayer = ObjectLayer.Backwall;
		go.AddComponent<ZoneTile>();
		go.GetComponent<PrimaryElement>().SetElement(SimHashes.Glass, true);
		go.GetComponent<PrimaryElement>().Temperature = 273f;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Gravitas, false);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	// Token: 0x060012FF RID: 4863 RVA: 0x0006CB23 File Offset: 0x0006AD23
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000B94 RID: 2964
	public const string ID = "PropGravitasLabWindowHorizontal";
}
