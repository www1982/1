using System;
using TUNING;
using UnityEngine;

// Token: 0x02000445 RID: 1093
public class WireRefinedConfig : BaseWireConfig
{
	// Token: 0x060016B6 RID: 5814 RVA: 0x00080AD4 File Offset: 0x0007ECD4
	public override BuildingDef CreateBuildingDef()
	{
		string text = "WireRefined";
		string text2 = "utilities_electric_conduct_kanim";
		float num = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		float num2 = 0.05f;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = base.CreateBuildingDef(text, text2, num, tier, num2, BUILDINGS.DECOR.NONE, none);
		buildingDef.MaterialCategory = MATERIALS.REFINED_METALS;
		return buildingDef;
	}

	// Token: 0x060016B7 RID: 5815 RVA: 0x00080B17 File Offset: 0x0007ED17
	public override void DoPostConfigureComplete(GameObject go)
	{
		base.DoPostConfigureComplete(Wire.WattageRating.Max2000, go);
	}

	// Token: 0x04000D4C RID: 3404
	public const string ID = "WireRefined";
}
