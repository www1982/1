using System;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

// Token: 0x02000052 RID: 82
public class ContactConductivePipeBridgeConfig : IBuildingConfig
{
	// Token: 0x0600019A RID: 410 RVA: 0x0000B554 File Offset: 0x00009754
	public override BuildingDef CreateBuildingDef()
	{
		string text = "ContactConductivePipeBridge";
		int num = 3;
		int num2 = 1;
		string text2 = "contactConductivePipeBridge_kanim";
		int num3 = 30;
		float num4 = 10f;
		float[] tier = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float num5 = 2400f;
		BuildLocationRule buildLocationRule = BuildLocationRule.NoLiquidConduitAtOrigin;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, refined_METALS, num5, buildLocationRule, BUILDINGS.DECOR.NONE, none, 0.2f);
		buildingDef.Floodable = false;
		buildingDef.Entombable = false;
		buildingDef.Overheatable = false;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.ObjectLayer = ObjectLayer.LiquidConduitConnection;
		buildingDef.SceneLayer = Grid.SceneLayer.LiquidConduitBridges;
		buildingDef.AudioCategory = "Metal";
		buildingDef.AudioSize = "small";
		buildingDef.BaseTimeUntilRepair = -1f;
		buildingDef.PermittedRotations = PermittedRotations.R360;
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.UtilityInputOffset = new CellOffset(-1, 0);
		buildingDef.UtilityOutputOffset = new CellOffset(1, 0);
		buildingDef.UseStructureTemperature = true;
		buildingDef.ReplacementTags = new List<Tag>();
		buildingDef.ReplacementTags.Add(GameTags.Pipes);
		buildingDef.ThermalConductivity = 2f;
		GeneratedBuildings.RegisterWithOverlay(OverlayScreen.LiquidVentIDs, "ContactConductivePipeBridge");
		return buildingDef;
	}

	// Token: 0x0600019B RID: 411 RVA: 0x0000B656 File Offset: 0x00009856
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
		go.AddOrGet<StructureToStructureTemperature>();
		ContactConductivePipeBridge.Def def = go.AddOrGetDef<ContactConductivePipeBridge.Def>();
		def.pumpKGRate = 10f;
		def.type = ConduitType.Liquid;
	}

	// Token: 0x0600019C RID: 412 RVA: 0x0000B68B File Offset: 0x0000988B
	public override void DoPostConfigureComplete(GameObject go)
	{
		global::UnityEngine.Object.DestroyImmediate(go.GetComponent<RequireInputs>());
		global::UnityEngine.Object.DestroyImmediate(go.GetComponent<RequireOutputs>());
		global::UnityEngine.Object.DestroyImmediate(go.GetComponent<ConduitConsumer>());
		global::UnityEngine.Object.DestroyImmediate(go.GetComponent<ConduitDispenser>());
	}

	// Token: 0x040000F1 RID: 241
	public const float LIQUID_CAPACITY_KG = 10f;

	// Token: 0x040000F2 RID: 242
	public const float GAS_CAPACITY_KG = 0.5f;

	// Token: 0x040000F3 RID: 243
	public const float TEMPERATURE_EXCHANGE_WITH_STORAGE_MODIFIER = 50f;

	// Token: 0x040000F4 RID: 244
	public const float BUILDING_TO_BUILDING_TEMPERATURE_SCALE = 0.001f;

	// Token: 0x040000F5 RID: 245
	public const string ID = "ContactConductivePipeBridge";

	// Token: 0x040000F6 RID: 246
	public const float NO_LIQUIDS_COOLDOWN = 1.5f;

	// Token: 0x040000F7 RID: 247
	private const ConduitType CONDUIT_TYPE = ConduitType.Liquid;
}
