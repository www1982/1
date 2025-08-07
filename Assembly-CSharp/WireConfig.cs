using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000441 RID: 1089
public class WireConfig : BaseWireConfig
{
	// Token: 0x060016A6 RID: 5798 RVA: 0x00080884 File Offset: 0x0007EA84
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Wire";
		string text2 = "utilities_electric_kanim";
		float num = 3f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		float num2 = 0.05f;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = base.CreateBuildingDef(text, text2, num, tier, num2, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER0, none);
		buildingDef.AddSearchTerms(SEARCH_TERMS.POWER);
		buildingDef.AddSearchTerms(SEARCH_TERMS.WIRE);
		return buildingDef;
	}

	// Token: 0x060016A7 RID: 5799 RVA: 0x000808DC File Offset: 0x0007EADC
	public override void DoPostConfigureComplete(GameObject go)
	{
		base.DoPostConfigureComplete(Wire.WattageRating.Max1000, go);
	}

	// Token: 0x04000D48 RID: 3400
	public const string ID = "Wire";
}
