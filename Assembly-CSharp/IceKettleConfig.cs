using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x02000250 RID: 592
public class IceKettleConfig : IBuildingConfig
{
	// Token: 0x06000BFA RID: 3066 RVA: 0x00048C79 File Offset: 0x00046E79
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x06000BFB RID: 3067 RVA: 0x00048C80 File Offset: 0x00046E80
	public override BuildingDef CreateBuildingDef()
	{
		string text = "IceKettle";
		int num = 2;
		int num2 = 2;
		string text2 = "icemelter_kettle_kanim";
		int num3 = 100;
		float num4 = 10f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.NONE, none, 0.2f);
		float num6 = 3.7500002f;
		buildingDef.SelfHeatKilowattsWhenActive = num6 * 0.4f;
		buildingDef.ExhaustKilowattsWhenActive = num6 - buildingDef.SelfHeatKilowattsWhenActive;
		buildingDef.Floodable = false;
		buildingDef.Entombable = true;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "large";
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
		buildingDef.DefaultAnimState = "on";
		buildingDef.POIUnlockable = true;
		buildingDef.ShowInBuildMenu = true;
		buildingDef.AddSearchTerms(SEARCH_TERMS.WATER);
		return buildingDef;
	}

	// Token: 0x06000BFC RID: 3068 RVA: 0x00048D50 File Offset: 0x00046F50
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		go.AddTag(GameTags.LiquidSource);
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = Mathf.Ceil(152.80188f);
		storage.showInUI = true;
		storage.allowItemRemoval = false;
		storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.capacity = Mathf.Ceil(152.80188f);
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.requestedItemTag = IceKettleConfig.FUEL_TAG;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
		manualDeliveryKG.ShowStatusItem = false;
		Storage storage2 = go.AddComponent<Storage>();
		storage2.capacityKg = 1000f;
		storage2.showInUI = true;
		storage2.allowItemRemoval = false;
		storage2.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		ManualDeliveryKG manualDeliveryKG2 = go.AddComponent<ManualDeliveryKG>();
		manualDeliveryKG2.capacity = 1000f;
		manualDeliveryKG2.SetStorage(storage2);
		manualDeliveryKG2.requestedItemTag = IceKettleConfig.TARGET_ELEMENT_TAG;
		manualDeliveryKG2.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
		manualDeliveryKG2.refillMass = 100f;
		manualDeliveryKG2.ShowStatusItem = false;
		Storage storage3 = go.AddComponent<Storage>();
		storage3.capacityKg = 500f;
		storage3.showInUI = true;
		storage3.allowItemRemoval = true;
		storage3.showDescriptor = true;
		storage3.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		IceKettle.Def def = go.AddOrGetDef<IceKettle.Def>();
		def.exhaust_tag = SimHashes.CarbonDioxide;
		def.targetElementTag = IceKettleConfig.TARGET_ELEMENT_TAG;
		def.KGToMeltPerBatch = 100f;
		def.KGMeltedPerSecond = 20f;
		def.fuelElementTag = IceKettleConfig.FUEL_TAG;
		def.TargetTemperature = 298.15f;
		def.EnergyPerUnitOfLumber = 4000f;
		def.ExhaustMassPerUnitOfLumber = 0.142f;
		go.AddOrGet<IceKettleWorkable>().storage = storage3;
	}

	// Token: 0x06000BFD RID: 3069 RVA: 0x00048F07 File Offset: 0x00047107
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x04000838 RID: 2104
	public const string ID = "IceKettle";

	// Token: 0x04000839 RID: 2105
	public const SimHashes TARGET_ELEMENT = SimHashes.Ice;

	// Token: 0x0400083A RID: 2106
	public const float MASS_KG_PER_BATCH = 100f;

	// Token: 0x0400083B RID: 2107
	public const float CAPACITY = 1000f;

	// Token: 0x0400083C RID: 2108
	public const float FINAL_PRODUCT_CAPACITY = 500f;

	// Token: 0x0400083D RID: 2109
	public static Tag TARGET_ELEMENT_TAG = SimHashes.Ice.CreateTag();

	// Token: 0x0400083E RID: 2110
	public const float TARGET_TEMPERATURE = 298.15f;

	// Token: 0x0400083F RID: 2111
	public const float PRODUCTION_PER_SECOND = 20f;

	// Token: 0x04000840 RID: 2112
	public static Tag FUEL_TAG = SimHashes.WoodLog.CreateTag();

	// Token: 0x04000841 RID: 2113
	public const SimHashes EXHAUST_TAG = SimHashes.CarbonDioxide;

	// Token: 0x04000842 RID: 2114
	public const float TOTAL_ENERGY_OF_LUMBER = 7750f;

	// Token: 0x04000843 RID: 2115
	public const float ENERGY_OF_LUMBER_TAKEN_FOR_BUILDING_SELF_HEAT = 3750f;

	// Token: 0x04000844 RID: 2116
	public const float ENERGY_PER_UNIT_OF_LUMBER_TAKEN_FOR_MELTING = 4000f;

	// Token: 0x04000845 RID: 2117
	public const float FUEL_UNITS_REQUIRED_TO_MELT_ABSOLUTE_ZERO_BATCH = 15.280188f;

	// Token: 0x04000846 RID: 2118
	public const float FUEL_CAPACITY = 152.80188f;
}
