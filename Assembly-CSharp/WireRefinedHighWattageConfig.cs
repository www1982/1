using System;
using TUNING;
using UnityEngine;

// Token: 0x02000446 RID: 1094
public class WireRefinedHighWattageConfig : BaseWireConfig
{
	// Token: 0x060016B9 RID: 5817 RVA: 0x00080B2C File Offset: 0x0007ED2C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "WireRefinedHighWattage";
		string text2 = "utilities_electric_conduct_hiwatt_kanim";
		float num = 3f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		float num2 = 0.05f;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = base.CreateBuildingDef(text, text2, num, tier, num2, BUILDINGS.DECOR.PENALTY.TIER3, none);
		buildingDef.MaterialCategory = MATERIALS.REFINED_METALS;
		buildingDef.BuildLocationRule = BuildLocationRule.NotInTiles;
		return buildingDef;
	}

	// Token: 0x060016BA RID: 5818 RVA: 0x00080B76 File Offset: 0x0007ED76
	public override void DoPostConfigureComplete(GameObject go)
	{
		base.DoPostConfigureComplete(Wire.WattageRating.Max50000, go);
	}

	// Token: 0x060016BB RID: 5819 RVA: 0x00080B80 File Offset: 0x0007ED80
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		base.DoPostConfigureUnderConstruction(go);
		go.GetComponent<Constructable>().requiredSkillPerk = Db.Get().SkillPerks.CanPowerTinker.Id;
	}

	// Token: 0x04000D4D RID: 3405
	public const string ID = "WireRefinedHighWattage";
}
