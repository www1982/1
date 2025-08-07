using System;
using TUNING;
using UnityEngine;

// Token: 0x0200002B RID: 43
public class BottleEmptierConduitGasConfig : IBuildingConfig
{
	// Token: 0x060000C2 RID: 194 RVA: 0x00006BD0 File Offset: 0x00004DD0
	public override BuildingDef CreateBuildingDef()
	{
		string text = "BottleEmptierConduitGas";
		int num = 1;
		int num2 = 2;
		string text2 = "bottle_emptier_gas_conduit_kanim";
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
		buildingDef.OutputConduitType = ConduitType.Gas;
		buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
		buildingDef.ViewMode = OverlayModes.GasConduits.ID;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.GasVentIDs, "BottleEmptierConduitGas");
		return buildingDef;
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00006C64 File Offset: 0x00004E64
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Prioritizable.AddRef(go);
		KPrefabID component = go.GetComponent<KPrefabID>();
		component.AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		component.AddTag(GameTags.OverlayBehindConduits, false);
		Storage storage = go.AddOrGet<Storage>();
		storage.storageFilters = STORAGEFILTERS.GASES;
		storage.showInUI = true;
		storage.showDescriptor = true;
		storage.capacityKg = 200f;
		storage.gunTargetOffset = new Vector2(0f, 1f);
		go.AddOrGet<TreeFilterable>();
		BottleEmptier bottleEmptier = go.AddOrGet<BottleEmptier>();
		bottleEmptier.isGasEmptier = true;
		bottleEmptier.emptyRate = 0.25f;
		bottleEmptier.emit = false;
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Gas;
		conduitDispenser.elementFilter = null;
		conduitDispenser.storage = storage;
		go.AddOrGet<RequireOutputs>().ignoreFullPipe = true;
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x00006D1F File Offset: 0x00004F1F
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000084 RID: 132
	public const string ID = "BottleEmptierConduitGas";

	// Token: 0x04000085 RID: 133
	private const int WIDTH = 1;

	// Token: 0x04000086 RID: 134
	private const int HEIGHT = 2;
}
