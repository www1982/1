using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002AA RID: 682
public class LogicRibbonConfig : BaseLogicWireConfig
{
	// Token: 0x06000DD8 RID: 3544 RVA: 0x00051230 File Offset: 0x0004F430
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LogicRibbon";
		string text2 = "logic_ribbon_kanim";
		float num = 10f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = base.CreateBuildingDef(text, text2, num, tier, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER0, none);
		buildingDef.AddSearchTerms(SEARCH_TERMS.AUTOMATION);
		return buildingDef;
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x00051273 File Offset: 0x0004F473
	public override void DoPostConfigureComplete(GameObject go)
	{
		base.DoPostConfigureComplete(LogicWire.BitDepth.FourBit, go);
	}

	// Token: 0x040008FA RID: 2298
	public const string ID = "LogicRibbon";
}
