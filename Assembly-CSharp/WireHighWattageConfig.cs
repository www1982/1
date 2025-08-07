using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000442 RID: 1090
public class WireHighWattageConfig : BaseWireConfig
{
	// Token: 0x060016A9 RID: 5801 RVA: 0x000808F0 File Offset: 0x0007EAF0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "HighWattageWire";
		string text2 = "utilities_electric_insulated_kanim";
		float num = 3f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		float num2 = 0.05f;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = base.CreateBuildingDef(text, text2, num, tier, num2, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER5, none);
		buildingDef.BuildLocationRule = BuildLocationRule.NotInTiles;
		buildingDef.AddSearchTerms(SEARCH_TERMS.POWER);
		buildingDef.AddSearchTerms(SEARCH_TERMS.WIRE);
		return buildingDef;
	}

	// Token: 0x060016AA RID: 5802 RVA: 0x0008094F File Offset: 0x0007EB4F
	public override void DoPostConfigureComplete(GameObject go)
	{
		base.DoPostConfigureComplete(Wire.WattageRating.Max20000, go);
	}

	// Token: 0x060016AB RID: 5803 RVA: 0x00080959 File Offset: 0x0007EB59
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
	}

	// Token: 0x04000D49 RID: 3401
	public const string ID = "HighWattageWire";
}
