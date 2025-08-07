using System;
using TUNING;
using UnityEngine;

// Token: 0x0200028D RID: 653
public class LiquidVentConfig : IBuildingConfig
{
	// Token: 0x06000D30 RID: 3376 RVA: 0x0004EC38 File Offset: 0x0004CE38
	public override BuildingDef CreateBuildingDef()
	{
		string text = "LiquidVent";
		int num = 1;
		int num2 = 1;
		string text2 = "ventliquid_kanim";
		int num3 = 30;
		float num4 = 30f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] all_METALS = MATERIALS.ALL_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.Anywhere;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, all_METALS, num5, buildLocationRule, BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.Floodable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.AudioCategory = "Metal";
		buildingDef.UtilityInputOffset = new CellOffset(0, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
		buildingDef.LogicInputPorts = LogicOperationalController.CreateSingleInputPortList(new CellOffset(0, 0));
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.LiquidVentIDs, "LiquidVent");
		SoundEventVolumeCache.instance.AddVolume("ventliquid_kanim", "LiquidVent_squirt", NOISE_POLLUTION.NOISY.TIER0);
		return buildingDef;
	}

	// Token: 0x06000D31 RID: 3377 RVA: 0x0004ED00 File Offset: 0x0004CF00
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<LoopingSounds>();
		go.AddOrGet<Exhaust>();
		go.AddOrGet<LogicOperationalController>();
		Vent vent = go.AddOrGet<Vent>();
		vent.conduitType = ConduitType.Liquid;
		vent.endpointType = Endpoint.Sink;
		vent.overpressureMass = 1000f;
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.ignoreMinMassCheck = true;
		BuildingTemplates.CreateDefaultStorage(go, false).showInUI = true;
		go.AddOrGet<SimpleVent>();
	}

	// Token: 0x06000D32 RID: 3378 RVA: 0x0004ED67 File Offset: 0x0004CF67
	public override void DoPostConfigureComplete(GameObject go)
	{
		VentController.Def def = go.AddOrGetDef<VentController.Def>();
		def.usingDynamicColor = true;
		def.outputSubstanceAnimName = "leak";
		go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayInFrontOfConduits, false);
	}

	// Token: 0x040008D7 RID: 2263
	public const string ID = "LiquidVent";

	// Token: 0x040008D8 RID: 2264
	public const float OVERPRESSURE_MASS = 1000f;

	// Token: 0x040008D9 RID: 2265
	private const ConduitType CONDUIT_TYPE = ConduitType.Liquid;
}
