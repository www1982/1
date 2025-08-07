using System;
using TUNING;
using UnityEngine;

// Token: 0x020003A9 RID: 937
public class PropGravitasWallPurpleConfig : IBuildingConfig
{
	// Token: 0x06001319 RID: 4889 RVA: 0x0006CF24 File Offset: 0x0006B124
	public override BuildingDef CreateBuildingDef()
	{
		string text = "PropGravitasWallPurple";
		int num = 1;
		int num2 = 1;
		string text2 = "walls_gravitas_purple_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.NotInTiles;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, DECOR.BONUS.TIER0, none, 0.2f);
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.Entombable = false;
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.DefaultAnimState = "off";
		buildingDef.ObjectLayer = ObjectLayer.Backwall;
		buildingDef.SceneLayer = Grid.SceneLayer.Backwall;
		buildingDef.ShowInBuildMenu = false;
		return buildingDef;
	}

	// Token: 0x0600131A RID: 4890 RVA: 0x0006CFBC File Offset: 0x0006B1BC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<AnimTileable>().objectLayer = ObjectLayer.Backwall;
		go.AddComponent<ZoneTile>();
		go.GetComponent<PrimaryElement>().SetElement(SimHashes.Granite, true);
		go.GetComponent<PrimaryElement>().Temperature = 273f;
		go.GetComponent<KPrefabID>().AddTag(GameTags.Gravitas, false);
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
	}

	// Token: 0x0600131B RID: 4891 RVA: 0x0006D023 File Offset: 0x0006B223
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000B96 RID: 2966
	public const string ID = "PropGravitasWallPurple";
}
