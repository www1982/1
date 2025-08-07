using System;
using Database;

// Token: 0x02000569 RID: 1385
public class Blueprints_U53 : BlueprintProvider
{
	// Token: 0x06001ECB RID: 7883 RVA: 0x000B0538 File Offset: 0x000AE738
	public override void SetupBlueprints()
	{
		base.AddBuilding("LuxuryBed", PermitRarity.Loyalty, "permit_elegantbed_hatch", "elegantbed_hatch_kanim");
		base.AddBuilding("LuxuryBed", PermitRarity.Loyalty, "permit_elegantbed_pipsqueak", "elegantbed_pipsqueak_kanim");
	}
}
