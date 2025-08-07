using System;
using TUNING;
using UnityEngine;

// Token: 0x02000251 RID: 593
public class IceMachineConfig : IBuildingConfig
{
	// Token: 0x06000C00 RID: 3072 RVA: 0x00048F34 File Offset: 0x00047134
	public override BuildingDef CreateBuildingDef()
	{
		string text = "IceMachine";
		int num = 2;
		int num2 = 3;
		string text2 = "freezerator_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = this.energyConsumption;
		buildingDef.ExhaustKilowattsWhenActive = 4f;
		buildingDef.SelfHeatKilowattsWhenActive = 12f;
		buildingDef.ViewMode = OverlayModes.Temperature.ID;
		buildingDef.AudioCategory = "Metal";
		return buildingDef;
	}

	// Token: 0x06000C01 RID: 3073 RVA: 0x00048FBC File Offset: 0x000471BC
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		Storage storage = go.AddOrGet<Storage>();
		storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		storage.showInUI = true;
		storage.capacityKg = 60f;
		Storage storage2 = go.AddComponent<Storage>();
		storage2.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		storage2.showInUI = true;
		storage2.capacityKg = 300f;
		storage2.allowItemRemoval = true;
		storage2.ignoreSourcePriority = true;
		storage2.allowUIItemRemoval = true;
		go.AddOrGet<LoopingSounds>();
		Prioritizable.AddRef(go);
		IceMachine iceMachine = go.AddOrGet<IceMachine>();
		iceMachine.SetStorages(storage, storage2);
		iceMachine.targetTemperature = 253.15f;
		iceMachine.heatRemovalRate = 80f;
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.RequestedItemTag = GameTags.Water;
		manualDeliveryKG.capacity = 60f;
		manualDeliveryKG.refillMass = 12f;
		manualDeliveryKG.MinimumMass = 10f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
	}

	// Token: 0x06000C02 RID: 3074 RVA: 0x000490A8 File Offset: 0x000472A8
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000847 RID: 2119
	public const string ID = "IceMachine";

	// Token: 0x04000848 RID: 2120
	private const float WATER_STORAGE = 60f;

	// Token: 0x04000849 RID: 2121
	private const float ICE_STORAGE = 300f;

	// Token: 0x0400084A RID: 2122
	private const float WATER_INPUT_RATE = 0.5f;

	// Token: 0x0400084B RID: 2123
	private const float ICE_OUTPUT_RATE = 0.5f;

	// Token: 0x0400084C RID: 2124
	private const float ICE_PER_LOAD = 30f;

	// Token: 0x0400084D RID: 2125
	private const float TARGET_ICE_TEMP = 253.15f;

	// Token: 0x0400084E RID: 2126
	private const float KDTU_TRANSFER_RATE = 80f;

	// Token: 0x0400084F RID: 2127
	private const float THERMAL_CONSERVATION = 0.2f;

	// Token: 0x04000850 RID: 2128
	private float energyConsumption = 240f;

	// Token: 0x04000851 RID: 2129
	public static Tag[] ELEMENT_OPTIONS = new Tag[]
	{
		SimHashes.Ice.CreateTag(),
		SimHashes.Snow.CreateTag()
	};
}
