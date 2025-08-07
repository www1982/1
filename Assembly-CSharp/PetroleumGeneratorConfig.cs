using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200036A RID: 874
public class PetroleumGeneratorConfig : IBuildingConfig
{
	// Token: 0x060011F1 RID: 4593 RVA: 0x00068C08 File Offset: 0x00066E08
	public override BuildingDef CreateBuildingDef()
	{
		string text = "PetroleumGenerator";
		int num = 3;
		int num2 = 4;
		string text2 = "generatorpetrol_kanim";
		int num3 = 100;
		float num4 = 480f;
		string[] array = new string[] { "Metal" };
		float[] array2 = new float[] { global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5[0] };
		string[] array3 = array;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array2, array3, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER2, tier, 0.2f);
		buildingDef.GeneratorWattageRating = 2000f;
		buildingDef.GeneratorBaseCapacity = buildingDef.GeneratorWattageRating;
		buildingDef.ExhaustKilowattsWhenActive = 4f;
		buildingDef.SelfHeatKilowattsWhenActive = 16f;
		buildingDef.ViewMode = OverlayModes.Power.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.UtilityInputOffset = new CellOffset(-1, 0);
		buildingDef.RequiresPowerOutput = true;
		buildingDef.PowerOutputOffset = new CellOffset(1, 0);
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.AddSearchTerms(SEARCH_TERMS.POWER);
		buildingDef.AddSearchTerms(SEARCH_TERMS.GENERATOR);
		return buildingDef;
	}

	// Token: 0x060011F2 RID: 4594 RVA: 0x00068D04 File Offset: 0x00066F04
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.PowerBuilding, false);
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.GeneratorType, false);
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.HeavyDutyGeneratorType, false);
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<Storage>();
		BuildingDef def = go.GetComponent<Building>().Def;
		float num = 20f;
		go.AddOrGet<LoopingSounds>();
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = def.InputConduitType;
		conduitConsumer.consumptionRate = 10f;
		conduitConsumer.capacityTag = GameTags.CombustibleLiquid;
		conduitConsumer.capacityKG = num;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		EnergyGenerator energyGenerator = go.AddOrGet<EnergyGenerator>();
		energyGenerator.powerDistributionOrder = 8;
		energyGenerator.ignoreBatteryRefillPercent = true;
		energyGenerator.hasMeter = true;
		energyGenerator.formula = new EnergyGenerator.Formula
		{
			inputs = new EnergyGenerator.InputItem[]
			{
				new EnergyGenerator.InputItem(GameTags.CombustibleLiquid, 2f, num)
			},
			outputs = new EnergyGenerator.OutputItem[]
			{
				new EnergyGenerator.OutputItem(SimHashes.CarbonDioxide, 0.5f, false, new CellOffset(0, 3), 383.15f),
				new EnergyGenerator.OutputItem(SimHashes.DirtyWater, 0.75f, false, new CellOffset(1, 1), 313.15f)
			}
		};
		Tinkerable.MakePowerTinkerable(go);
		go.AddOrGetDef<PoweredActiveController.Def>();
	}

	// Token: 0x04000B65 RID: 2917
	public const string ID = "PetroleumGenerator";

	// Token: 0x04000B66 RID: 2918
	public const float CONSUMPTION_RATE = 2f;

	// Token: 0x04000B67 RID: 2919
	private const SimHashes INPUT_ELEMENT = SimHashes.Petroleum;

	// Token: 0x04000B68 RID: 2920
	private const SimHashes EXHAUST_ELEMENT_GAS = SimHashes.CarbonDioxide;

	// Token: 0x04000B69 RID: 2921
	private const SimHashes EXHAUST_ELEMENT_LIQUID = SimHashes.DirtyWater;

	// Token: 0x04000B6A RID: 2922
	public const float EFFICIENCY_RATE = 0.5f;

	// Token: 0x04000B6B RID: 2923
	public const float EXHAUST_GAS_RATE = 0.5f;

	// Token: 0x04000B6C RID: 2924
	public const float EXHAUST_LIQUID_RATE = 0.75f;

	// Token: 0x04000B6D RID: 2925
	private const int WIDTH = 3;

	// Token: 0x04000B6E RID: 2926
	private const int HEIGHT = 4;
}
