using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020002B3 RID: 691
public class LogicWireConfig : BaseLogicWireConfig
{
	// Token: 0x06000E00 RID: 3584 RVA: 0x00051E5C File Offset: 0x0005005C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LogicWire";
		string text2 = "logic_wires_kanim";
		float num = 3f;
		float[] tier_TINY = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER_TINY;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = base.CreateBuildingDef(text, text2, num, tier_TINY, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER0, none);
		buildingDef.AddSearchTerms(SEARCH_TERMS.AUTOMATION);
		return buildingDef;
	}

	// Token: 0x06000E01 RID: 3585 RVA: 0x00051E9F File Offset: 0x0005009F
	public override void DoPostConfigureComplete(GameObject go)
	{
		base.DoPostConfigureComplete(LogicWire.BitDepth.OneBit, go);
	}

	// Token: 0x04000905 RID: 2309
	public const string ID = "LogicWire";
}
