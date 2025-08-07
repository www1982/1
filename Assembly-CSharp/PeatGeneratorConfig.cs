using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000369 RID: 873
public class PeatGeneratorConfig : IBuildingConfig
{
	// Token: 0x060011EC RID: 4588 RVA: 0x00068992 File Offset: 0x00066B92
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC4;
	}

	// Token: 0x060011ED RID: 4589 RVA: 0x0006899C File Offset: 0x00066B9C
	public override BuildingDef CreateBuildingDef()
	{
		string text = "PeatGenerator";
		int num = 3;
		int num2 = 2;
		string text2 = "generatorpeat_kanim";
		int num3 = 100;
		float num4 = 120f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER2, tier2, 0.2f);
		buildingDef.GeneratorWattageRating = 480f;
		buildingDef.GeneratorBaseCapacity = buildingDef.GeneratorWattageRating;
		buildingDef.ExhaustKilowattsWhenActive = 4f;
		buildingDef.SelfHeatKilowattsWhenActive = 0.5f;
		buildingDef.RequiresPowerOutput = true;
		buildingDef.PowerOutputOffset = new CellOffset(0, 0);
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.AudioSize = "large";
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		buildingDef.AddSearchTerms(SEARCH_TERMS.POWER);
		buildingDef.AddSearchTerms(SEARCH_TERMS.GENERATOR);
		return buildingDef;
	}

	// Token: 0x060011EE RID: 4590 RVA: 0x00068A78 File Offset: 0x00066C78
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.PowerBuilding, false);
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.GeneratorType, false);
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.HeavyDutyGeneratorType, false);
		EnergyGenerator energyGenerator = go.AddOrGet<EnergyGenerator>();
		energyGenerator.formula = new EnergyGenerator.Formula
		{
			inputs = new EnergyGenerator.InputItem[]
			{
				new EnergyGenerator.InputItem(SimHashes.Peat.CreateTag(), 1f, 600f)
			},
			outputs = new EnergyGenerator.OutputItem[]
			{
				new EnergyGenerator.OutputItem(SimHashes.CarbonDioxide, 0.04f, false, new CellOffset(0, 1), 383.15f),
				new EnergyGenerator.OutputItem(SimHashes.DirtyWater, 0.2f, false, new CellOffset(1, 1), 313.15f)
			}
		};
		energyGenerator.meterOffset = Meter.Offset.Infront;
		energyGenerator.SetSliderValue(50f, 0);
		energyGenerator.powerDistributionOrder = 9;
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 600f;
		go.AddOrGet<LoopingSounds>();
		Prioritizable.AddRef(go);
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = SimHashes.Peat.CreateTag();
		manualDeliveryKG.capacity = storage.capacityKg;
		manualDeliveryKG.refillMass = 100f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.PowerFetch.IdHash;
		Tinkerable.MakePowerTinkerable(go);
	}

	// Token: 0x060011EF RID: 4591 RVA: 0x00068BED File Offset: 0x00066DED
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGetDef<PoweredActiveController.Def>();
	}

	// Token: 0x04000B5E RID: 2910
	public const string ID = "PeatGenerator";

	// Token: 0x04000B5F RID: 2911
	private const float PEAT_BURN_RATE = 1f;

	// Token: 0x04000B60 RID: 2912
	public const float EXHAUST_LIQUID_RATE = 0.2f;

	// Token: 0x04000B61 RID: 2913
	public const float EXHAUST_GAS_RATE = 0.04f;

	// Token: 0x04000B62 RID: 2914
	private const float PEAT_CAPACITY = 600f;

	// Token: 0x04000B63 RID: 2915
	public const float CO2_OUTPUT_TEMPERATURE = 383.15f;

	// Token: 0x04000B64 RID: 2916
	public const float LIQUID_OUTPUT_TEMPERATURE = 313.15f;
}
