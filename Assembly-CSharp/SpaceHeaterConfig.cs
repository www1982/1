using System;
using TUNING;
using UnityEngine;

// Token: 0x02000409 RID: 1033
public class SpaceHeaterConfig : IBuildingConfig
{
	// Token: 0x06001525 RID: 5413 RVA: 0x00078730 File Offset: 0x00076930
	public override BuildingDef CreateBuildingDef()
	{
		string text = "SpaceHeater";
		int num = 2;
		int num2 = 2;
		string text2 = "spaceheater_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER2;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.BONUS.TIER1, tier2, 0.2f);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 240f;
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.SelfHeatKilowattsWhenActive = 0f;
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(1, 0));
		buildingDef.ViewMode = OverlayModes.Temperature.ID;
		buildingDef.AudioCategory = "HollowMetal";
		buildingDef.OverheatTemperature = 398.15f;
		return buildingDef;
	}

	// Token: 0x06001526 RID: 5414 RVA: 0x000787D4 File Offset: 0x000769D4
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.WarmingStation, false);
		go.AddOrGet<KBatchedAnimHeatPostProcessingEffect>();
		SpaceHeater spaceHeater = go.AddOrGet<SpaceHeater>();
		spaceHeater.targetTemperature = 343.15f;
		spaceHeater.produceHeat = true;
		WarmthProvider.Def def = go.AddOrGetDef<WarmthProvider.Def>();
		def.RangeMax = SpaceHeaterConfig.MAX_RANGE;
		def.RangeMin = SpaceHeaterConfig.MIN_RANGE;
		go.AddOrGetDef<ColdImmunityProvider.Def>().range = new CellOffset[][]
		{
			new CellOffset[]
			{
				new CellOffset(-1, 0),
				new CellOffset(2, 0)
			},
			new CellOffset[]
			{
				new CellOffset(0, 0),
				new CellOffset(1, 0)
			}
		};
		this.AddVisualizer(go);
	}

	// Token: 0x06001527 RID: 5415 RVA: 0x00078894 File Offset: 0x00076A94
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		this.AddVisualizer(go);
	}

	// Token: 0x06001528 RID: 5416 RVA: 0x0007889D File Offset: 0x00076A9D
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		this.AddVisualizer(go);
	}

	// Token: 0x06001529 RID: 5417 RVA: 0x000788A6 File Offset: 0x00076AA6
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<LogicOperationalController>();
		go.AddOrGetDef<PoweredActiveController.Def>();
	}

	// Token: 0x0600152A RID: 5418 RVA: 0x000788B8 File Offset: 0x00076AB8
	private void AddVisualizer(GameObject go)
	{
		RangeVisualizer rangeVisualizer = go.AddOrGet<RangeVisualizer>();
		rangeVisualizer.RangeMax = SpaceHeaterConfig.MAX_RANGE;
		rangeVisualizer.RangeMin = SpaceHeaterConfig.MIN_RANGE;
		rangeVisualizer.BlockingTileVisible = false;
		go.AddOrGet<EntityCellVisualizer>().AddPort(EntityCellVisualizer.Ports.HeatSource, default(CellOffset));
	}

	// Token: 0x04000C82 RID: 3202
	public const string ID = "SpaceHeater";

	// Token: 0x04000C83 RID: 3203
	public const float MAX_SELF_HEAT = 32f;

	// Token: 0x04000C84 RID: 3204
	public const float MAX_EXHAUST_HEAT = 4f;

	// Token: 0x04000C85 RID: 3205
	public const float MIN_POWER_USAGE = 120f;

	// Token: 0x04000C86 RID: 3206
	public const float MAX_POWER_USAGE = 240f;

	// Token: 0x04000C87 RID: 3207
	public static Vector2I MAX_RANGE = new Vector2I(5, 5);

	// Token: 0x04000C88 RID: 3208
	public static Vector2I MIN_RANGE = new Vector2I(-4, -4);
}
