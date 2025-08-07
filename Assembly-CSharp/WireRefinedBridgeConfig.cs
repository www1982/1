using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000443 RID: 1091
public class WireRefinedBridgeConfig : WireBridgeConfig
{
	// Token: 0x060016AD RID: 5805 RVA: 0x0008096A File Offset: 0x0007EB6A
	protected override string GetID()
	{
		return "WireRefinedBridge";
	}

	// Token: 0x060016AE RID: 5806 RVA: 0x00080974 File Offset: 0x0007EB74
	public override BuildingDef CreateBuildingDef()
	{
		BuildingDef buildingDef = base.CreateBuildingDef();
		buildingDef.AnimFiles = new KAnimFile[] { Assets.GetAnim("utilityelectricbridgeconductive_kanim") };
		buildingDef.Mass = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER0;
		buildingDef.MaterialCategory = MATERIALS.REFINED_METALS;
		buildingDef.AddSearchTerms(SEARCH_TERMS.POWER);
		buildingDef.AddSearchTerms(SEARCH_TERMS.WIRE);
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.WireIDs, "WireRefinedBridge");
		return buildingDef;
	}

	// Token: 0x060016AF RID: 5807 RVA: 0x000809EC File Offset: 0x0007EBEC
	protected override WireUtilityNetworkLink AddNetworkLink(GameObject go)
	{
		WireUtilityNetworkLink wireUtilityNetworkLink = base.AddNetworkLink(go);
		wireUtilityNetworkLink.maxWattageRating = Wire.WattageRating.Max2000;
		return wireUtilityNetworkLink;
	}

	// Token: 0x04000D4A RID: 3402
	public new const string ID = "WireRefinedBridge";
}
