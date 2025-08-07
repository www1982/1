using System;
using TUNING;
using UnityEngine;

// Token: 0x0200002E RID: 46
public class BottleEmptierGasConfig : IBuildingConfig
{
	// Token: 0x060000CE RID: 206 RVA: 0x00006F54 File Offset: 0x00005154
	public override BuildingDef CreateBuildingDef()
	{
		string text = "BottleEmptierGas";
		int num = 1;
		int num2 = 3;
		string text2 = "gas_emptying_station_kanim";
		int num3 = 30;
		float num4 = 60f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER1;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER2, tier2, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.AudioCategory = "Metal";
		buildingDef.Overheatable = false;
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		return buildingDef;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x00006FBC File Offset: 0x000051BC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		Storage storage = go.AddOrGet<Storage>();
		storage.storageFilters = STORAGEFILTERS.GASES;
		storage.showInUI = true;
		storage.showDescriptor = true;
		storage.capacityKg = 200f;
		storage.gunTargetOffset = new Vector2(0f, 2f);
		go.AddOrGet<TreeFilterable>();
		BottleEmptier bottleEmptier = go.AddOrGet<BottleEmptier>();
		bottleEmptier.isGasEmptier = true;
		bottleEmptier.emptyRate = 0.25f;
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x0000702B File Offset: 0x0000522B
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x0400008B RID: 139
	public const string ID = "BottleEmptierGas";
}
