using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class CheckpointConfig : IBuildingConfig
{
	// Token: 0x06000124 RID: 292 RVA: 0x00008F60 File Offset: 0x00007160
	public override BuildingDef CreateBuildingDef()
	{
		string text = "Checkpoint";
		int num = 1;
		int num2 = 3;
		string text2 = "checkpoint_kanim";
		int num3 = 30;
		float num4 = 30f;
		string[] refined_METALS = MATERIALS.REFINED_METALS;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;
		string[] array = refined_METALS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues tier2 = NOISE_POLLUTION.NOISY.TIER0;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(text, num, num2, text2, num3, num4, tier, array, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.BONUS.TIER1, tier2, 0.2f);
		buildingDef.ForegroundLayer = Grid.SceneLayer.Front;
		buildingDef.PermittedRotations = PermittedRotations.FlipH;
		buildingDef.PreventIdleTraversalPastBuilding = true;
		buildingDef.Floodable = false;
		buildingDef.RequiresPowerInput = true;
		buildingDef.PowerInputOffset = new CellOffset(0, 2);
		buildingDef.EnergyConsumptionWhenActive = 10f;
		buildingDef.SelfHeatKilowattsWhenActive = 0.5f;
		buildingDef.LogicInputPorts = new List<LogicPorts.Port> { LogicPorts.Port.InputPort(Checkpoint.PORT_ID, new CellOffset(0, 2), global::STRINGS.BUILDINGS.PREFABS.CHECKPOINT.LOGIC_PORT, global::STRINGS.BUILDINGS.PREFABS.CHECKPOINT.LOGIC_PORT_ACTIVE, global::STRINGS.BUILDINGS.PREFABS.CHECKPOINT.LOGIC_PORT_INACTIVE, true, false) };
		return buildingDef;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x00009031 File Offset: 0x00007231
	public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
	{
		go.AddOrGet<Checkpoint>();
	}

	// Token: 0x06000126 RID: 294 RVA: 0x0000903A File Offset: 0x0000723A
	public override void DoPostConfigureComplete(GameObject go)
	{
	}

	// Token: 0x040000B1 RID: 177
	public const string ID = "Checkpoint";
}
