using System;
using TUNING;
using UnityEngine;

// Token: 0x02000332 RID: 818
public class ModularLaunchpadPortBridgeConfig : IBuildingConfig
{
	// Token: 0x060010E5 RID: 4325 RVA: 0x00063AA0 File Offset: 0x00061CA0
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1;
	}

	// Token: 0x060010E6 RID: 4326 RVA: 0x00063AA8 File Offset: 0x00061CA8
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ModularLaunchpadPortBridge";
		int num = 1;
		int num2 = 2;
		string text2 = "rocket_loader_extension_kanim";
		int num3 = 1000;
		float num4 = 60f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER3;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.SceneLayer = Grid.SceneLayer.BuildingBack;
		buildingDef.OverheatTemperature = 2273.15f;
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.DefaultAnimState = "idle";
		buildingDef.UseStructureTemperature = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "medium";
		return buildingDef;
	}

	// Token: 0x060010E7 RID: 4327 RVA: 0x00063B3C File Offset: 0x00061D3C
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		KPrefabID component = go.GetComponent<KPrefabID>();
		component.AddTag(GameTags.ModularConduitPort, false);
		component.AddTag(GameTags.NotRocketInteriorBuilding, false);
		component.AddTag(BaseModularLaunchpadPortConfig.LinkTag, false);
		ChainedBuilding.Def def = go.AddOrGetDef<ChainedBuilding.Def>();
		def.headBuildingTag = "LaunchPad".ToTag();
		def.linkBuildingTag = BaseModularLaunchpadPortConfig.LinkTag;
		def.objectLayer = ObjectLayer.Building;
		go.AddOrGet<FakeFloorAdder>().floorOffsets = new CellOffset[]
		{
			new CellOffset(0, 1)
		};
	}

	// Token: 0x060010E8 RID: 4328 RVA: 0x00063BB8 File Offset: 0x00061DB8
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000AAF RID: 2735
	public const string ID = "ModularLaunchpadPortBridge";
}
