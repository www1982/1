using System;
using TUNING;
using UnityEngine;

// Token: 0x0200002C RID: 44
public class BottleEmptierConduitLiquidConfig : IBuildingConfig
{
	// Token: 0x060000C6 RID: 198 RVA: 0x00006D2C File Offset: 0x00004F2C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "BottleEmptierConduitLiquid";
		int num = 1;
		int num2 = 2;
		string text2 = "bottle_emptier_liquid_conduit_kanim";
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
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.LiquidVentIDs, "BottleEmptierConduitLiquid");
		return buildingDef;
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00006DC0 File Offset: 0x00004FC0
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		KPrefabID component = go.GetComponent<KPrefabID>();
		component.AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		component.AddTag(GameTags.OverlayBehindConduits, false);
		Storage storage = go.AddOrGet<Storage>();
		storage.storageFilters = STORAGEFILTERS.LIQUIDS;
		storage.showInUI = true;
		storage.showDescriptor = true;
		storage.capacityKg = 200f;
		storage.gunTargetOffset = new Vector2(0f, 1f);
		go.AddOrGet<TreeFilterable>();
		BottleEmptier bottleEmptier = go.AddOrGet<BottleEmptier>();
		bottleEmptier.emit = false;
		bottleEmptier.emptyRate = 5f;
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Liquid;
		conduitDispenser.elementFilter = null;
		conduitDispenser.storage = storage;
		go.AddOrGet<RequireOutputs>().ignoreFullPipe = true;
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x00006E74 File Offset: 0x00005074
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000087 RID: 135
	public const string ID = "BottleEmptierConduitLiquid";

	// Token: 0x04000088 RID: 136
	private const int WIDTH = 1;

	// Token: 0x04000089 RID: 137
	private const int HEIGHT = 2;
}
