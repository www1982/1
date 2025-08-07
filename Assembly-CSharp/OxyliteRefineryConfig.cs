using System;
using TUNING;
using UnityEngine;

// Token: 0x02000362 RID: 866
public class OxyliteRefineryConfig : IBuildingConfig
{
	// Token: 0x060011CE RID: 4558 RVA: 0x00067EFC File Offset: 0x000660FC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "OxyliteRefinery";
		int num = 3;
		int num2 = 4;
		string text2 = "oxylite_refinery_kanim";
		int num3 = 100;
		float num4 = 480f;
		string[] array = new string[] { "RefinedMetal", "Plastic" };
		float[] array2 = new float[]
		{
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER5[0],
			BUILDINGS.CONSTRUCTION_MASS_KG.TIER2[0]
		};
		string[] array3 = array;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier = NOISE_POLLUTION.NOISY.TIER5;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, array2, array3, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, tier, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(0, 0);
		buildingDef.EnergyConsumptionWhenActive = 1200f;
		buildingDef.ExhaustKilowattsWhenActive = 8f;
		buildingDef.SelfHeatKilowattsWhenActive = 4f;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 1));
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.InputConduitType = ConduitType.Gas;
		buildingDef.UtilityInputOffset = new CellOffset(1, 0);
		return buildingDef;
	}

	// Token: 0x060011CF RID: 4559 RVA: 0x00067FD8 File Offset: 0x000661D8
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Tag tag = SimHashes.Oxygen.CreateTag();
		Tag tag2 = SimHashes.Gold.CreateTag();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
		OxyliteRefinery oxyliteRefinery = go.AddOrGet<OxyliteRefinery>();
		oxyliteRefinery.emitTag = SimHashes.OxyRock.CreateTag();
		oxyliteRefinery.emitMass = 10f;
		oxyliteRefinery.dropOffset = new Vector3(0f, 1f);
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Gas;
		conduitConsumer.consumptionRate = 1.2f;
		conduitConsumer.capacityTag = tag;
		conduitConsumer.capacityKG = 6f;
		conduitConsumer.forceAlwaysSatisfied = true;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 23.2f;
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		storage.showInUI = true;
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = tag2;
		manualDeliveryKG.refillMass = 1.8000001f;
		manualDeliveryKG.capacity = 7.2000003f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(tag, 0.6f, true),
			new ElementConverter.ConsumedElement(tag2, 0.003f, true)
		};
		elementConverter.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(0.6f, SimHashes.OxyRock, 303.15f, false, true, 0f, 0.5f, 1f, byte.MaxValue, 0, true)
		};
		Prioritizable.AddRef(go);
	}

	// Token: 0x060011D0 RID: 4560 RVA: 0x0006815D File Offset: 0x0006635D
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGetDef<PoweredActiveController.Def>();
	}

	// Token: 0x04000B4D RID: 2893
	public const string ID = "OxyliteRefinery";

	// Token: 0x04000B4E RID: 2894
	public const float EMIT_MASS = 10f;

	// Token: 0x04000B4F RID: 2895
	public const float INPUT_O2_PER_SECOND = 0.6f;

	// Token: 0x04000B50 RID: 2896
	public const float OXYLITE_PER_SECOND = 0.6f;

	// Token: 0x04000B51 RID: 2897
	public const float GOLD_PER_SECOND = 0.003f;

	// Token: 0x04000B52 RID: 2898
	public const float OUTPUT_TEMP = 303.15f;

	// Token: 0x04000B53 RID: 2899
	public const float REFILL_RATE = 2400f;

	// Token: 0x04000B54 RID: 2900
	public const float GOLD_STORAGE_AMOUNT = 7.2000003f;

	// Token: 0x04000B55 RID: 2901
	public const float O2_STORAGE_AMOUNT = 6f;

	// Token: 0x04000B56 RID: 2902
	public const float STORAGE_CAPACITY = 23.2f;
}
