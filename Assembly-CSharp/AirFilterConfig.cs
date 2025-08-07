using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000016 RID: 22
public class AirFilterConfig : IBuildingConfig
{
	// Token: 0x06000060 RID: 96 RVA: 0x000047DC File Offset: 0x000029DC
	public override BuildingDef CreateBuildingDef()
	{
		string text = "AirFilter";
		int num = 1;
		int num2 = 1;
		string text2 = "co2filter_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] raw_MINERALS = MATERIALS.RAW_MINERALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_MINERALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.Oxygen.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(0, 0);
		buildingDef.EnergyConsumptionWhenActive = 5f;
		buildingDef.ExhaustKilowattsWhenActive = 0.125f;
		buildingDef.SelfHeatKilowattsWhenActive = 0.5f;
		buildingDef.AddSearchTerms(SEARCH_TERMS.FILTER);
		return buildingDef;
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00004884 File Offset: 0x00002A84
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		Prioritizable.AddRef(go);
		Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
		storage.showInUI = true;
		storage.capacityKg = 200f;
		storage.SetDefaultStoredItemModifiers(Storage.StandardSealedStorage);
		ElementConsumer elementConsumer = go.AddOrGet<ElementConsumer>();
		elementConsumer.elementToConsume = SimHashes.ContaminatedOxygen;
		elementConsumer.consumptionRate = 0.5f;
		elementConsumer.capacityKG = 0.5f;
		elementConsumer.consumptionRadius = 3;
		elementConsumer.showInStatusPanel = true;
		elementConsumer.sampleCellOffset = new Vector3(0f, 0f, 0f);
		elementConsumer.isRequired = false;
		elementConsumer.storeOnConsume = true;
		elementConsumer.showDescriptor = false;
		elementConsumer.ignoreActiveChanged = true;
		ElementDropper elementDropper = go.AddComponent<ElementDropper>();
		elementDropper.emitMass = 10f;
		elementDropper.emitTag = new Tag("Clay");
		elementDropper.emitOffset = new Vector3(0f, 0f, 0f);
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(new Tag("Filter"), 0.13333334f, true),
			new ElementConverter.ConsumedElement(new Tag("ContaminatedOxygen"), 0.1f, true)
		};
		elementConverter.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(0.14333335f, SimHashes.Clay, 0f, false, true, 0f, 0.5f, 0.25f, byte.MaxValue, 0, true),
			new ElementConverter.OutputElement(0.089999996f, SimHashes.Oxygen, 0f, false, false, 0f, 0f, 0.75f, byte.MaxValue, 0, true)
		};
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = new Tag("Filter");
		manualDeliveryKG.capacity = 320.00003f;
		manualDeliveryKG.refillMass = 32.000004f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
		go.AddOrGet<AirFilter>().filterTag = new Tag("Filter");
		go.AddOrGet<KBatchedAnimController>().randomiseLoopedOffset = true;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00004A91 File Offset: 0x00002C91
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGetDef<ActiveController.Def>();
	}

	// Token: 0x0400004B RID: 75
	public const string ID = "AirFilter";

	// Token: 0x0400004C RID: 76
	public const float DIRTY_AIR_CONSUMPTION_RATE = 0.1f;

	// Token: 0x0400004D RID: 77
	private const float SAND_CONSUMPTION_RATE = 0.13333334f;

	// Token: 0x0400004E RID: 78
	private const float REFILL_RATE = 2400f;

	// Token: 0x0400004F RID: 79
	private const float SAND_STORAGE_AMOUNT = 320.00003f;

	// Token: 0x04000050 RID: 80
	private const float CLAY_PER_LOAD = 10f;
}
