using System;
using TUNING;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class CampfireConfig : IBuildingConfig
{
	// Token: 0x060000F5 RID: 245 RVA: 0x00007F29 File Offset: 0x00006129
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC2;
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x00007F30 File Offset: 0x00006130
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Campfire";
		int num = 1;
		int num2 = 2;
		string text2 = "campfire_small_kanim";
		int num3 = 100;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] raw_METALS = MATERIALS.RAW_METALS;
		float num5 = 9999f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, raw_METALS, num5, buildLocationRule, CampfireConfig.DECOR_ON, none, 0.1f);
		buildingDef.Floodable = true;
		buildingDef.Entombable = true;
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.SelfHeatKilowattsWhenActive = 0f;
		buildingDef.ViewMode = OverlayModes.Temperature.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
		buildingDef.DefaultAnimState = "on";
		buildingDef.OverheatTemperature = 10000f;
		buildingDef.Overheatable = false;
		buildingDef.POIUnlockable = true;
		buildingDef.ShowInBuildMenu = true;
		return buildingDef;
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00008000 File Offset: 0x00006200
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<BuildingComplete>().isManuallyOperated = true;
		KPrefabID component = go.GetComponent<KPrefabID>();
		component.AddTag(RoomConstraints.ConstraintTags.WarmingStation, false);
		component.AddTag(RoomConstraints.ConstraintTags.Decoration, false);
		Storage storage = go.AddOrGet<Storage>();
		storage.capacityKg = 45f;
		storage.showInUI = true;
		storage.allowItemRemoval = false;
		storage.SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		ManualDeliveryKG manualDeliveryKG = go.AddOrGet<ManualDeliveryKG>();
		manualDeliveryKG.capacity = 45f;
		manualDeliveryKG.SetStorage(storage);
		manualDeliveryKG.requestedItemTag = CampfireConfig.FUEL_TAG;
		manualDeliveryKG.refillMass = 18f;
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.FetchCritical.IdHash;
		manualDeliveryKG.MinimumMass = 0.025f;
		ElementConverter elementConverter = go.AddOrGet<ElementConverter>();
		elementConverter.consumedElements = new ElementConverter.ConsumedElement[]
		{
			new ElementConverter.ConsumedElement(CampfireConfig.FUEL_TAG, 0.025f, true)
		};
		elementConverter.outputElements = new ElementConverter.OutputElement[]
		{
			new ElementConverter.OutputElement(0.004f, SimHashes.CarbonDioxide, 303.15f, false, false, 0f, 1f, 1f, byte.MaxValue, 0, true)
		};
		this.AddVisualizer(go);
		Operational operational = go.AddOrGet<Operational>();
		Light2D light2D = go.AddOrGet<Light2D>();
		light2D.Range = 6f;
		light2D.Color = new Color(0.8f, 0.6f, 0f, 1f);
		light2D.Lux = 450;
		Campfire.Def def = go.AddOrGetDef<Campfire.Def>();
		def.fuelTag = CampfireConfig.FUEL_TAG;
		def.initialFuelMass = 5f;
		WarmthProvider.Def def2 = go.AddOrGetDef<WarmthProvider.Def>();
		def2.RangeMax = new Vector2I(4, 3);
		def2.RangeMin = new Vector2I(-4, 0);
		go.AddOrGetDef<ColdImmunityProvider.Def>().range = new CellOffset[][]
		{
			new CellOffset[]
			{
				new CellOffset(-1, 0),
				new CellOffset(1, 0)
			},
			new CellOffset[]
			{
				new CellOffset(0, 0)
			}
		};
		DirectVolumeHeater directVolumeHeater = go.AddOrGet<DirectVolumeHeater>();
		directVolumeHeater.operational = operational;
		directVolumeHeater.DTUs = 20000f;
		directVolumeHeater.width = 9;
		directVolumeHeater.height = 4;
		directVolumeHeater.maximumExternalTemperature = 343.15f;
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x00008224 File Offset: 0x00006424
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		this.AddVisualizer(go);
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x0000822D File Offset: 0x0000642D
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		this.AddVisualizer(go);
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00008236 File Offset: 0x00006436
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00008238 File Offset: 0x00006438
	private void AddVisualizer(GameObject go)
	{
		RangeVisualizer rangeVisualizer = go.AddOrGet<RangeVisualizer>();
		rangeVisualizer.RangeMax = new Vector2I(4, 3);
		rangeVisualizer.RangeMin = new Vector2I(-4, 0);
		rangeVisualizer.BlockingTileVisible = false;
		go.AddOrGet<EntityCellVisualizer>().AddPort(EntityCellVisualizer.Ports.HeatSource, default(CellOffset));
	}

	// Token: 0x04000096 RID: 150
	public const string ID = "Campfire";

	// Token: 0x04000097 RID: 151
	public const int RANGE_X = 4;

	// Token: 0x04000098 RID: 152
	public const int RANGE_Y = 3;

	// Token: 0x04000099 RID: 153
	public static Tag FUEL_TAG = SimHashes.WoodLog.ToString();

	// Token: 0x0400009A RID: 154
	public const float FUEL_CONSUMPTION_RATE = 0.025f;

	// Token: 0x0400009B RID: 155
	public const float FUEL_CONSTRUCTION_MASS = 5f;

	// Token: 0x0400009C RID: 156
	public const float FUEL_CAPACITY = 45f;

	// Token: 0x0400009D RID: 157
	public const float EXHAUST_RATE = 0.004f;

	// Token: 0x0400009E RID: 158
	public const SimHashes EXHAUST_TAG = SimHashes.CarbonDioxide;

	// Token: 0x0400009F RID: 159
	private const float EXHAUST_TEMPERATURE = 303.15f;

	// Token: 0x040000A0 RID: 160
	public static readonly EffectorValues DECOR_ON = BUILDINGS.DECOR.BONUS.TIER3;

	// Token: 0x040000A1 RID: 161
	public static readonly EffectorValues DECOR_OFF = BUILDINGS.DECOR.NONE;
}
