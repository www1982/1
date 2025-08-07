using System;
using TUNING;
using UnityEngine;

// Token: 0x0200023C RID: 572
public class GravitasLabLightConfig : IBuildingConfig
{
	// Token: 0x06000B8A RID: 2954 RVA: 0x00046280 File Offset: 0x00044480
	public override BuildingDef CreateBuildingDef()
	{
		string text = "GravitasLabLight";
		int num = 1;
		int num2 = 1;
		string text2 = "gravitas_lab_light_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnCeiling;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.ShowInBuildMenu = false;
		buildingDef.Entombable = false;
		buildingDef.Floodable = false;
		buildingDef.Invincible = true;
		buildingDef.AudioCategory = "Metal";
		return buildingDef;
	}

	// Token: 0x06000B8B RID: 2955 RVA: 0x000462ED File Offset: 0x000444ED
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddTag(GameTags.Gravitas);
	}

	// Token: 0x06000B8C RID: 2956 RVA: 0x000462FA File Offset: 0x000444FA
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040007F5 RID: 2037
	public const string ID = "GravitasLabLight";
}
