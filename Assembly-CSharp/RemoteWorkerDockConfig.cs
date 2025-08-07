using System;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020003BD RID: 957
public class RemoteWorkerDockConfig : IBuildingConfig
{
	// Token: 0x06001378 RID: 4984 RVA: 0x0006EDE0 File Offset: 0x0006CFE0
	public override BuildingDef CreateBuildingDef()
	{
		string id = RemoteWorkerDockConfig.ID;
		int num = 1;
		int num2 = 2;
		string text = "remote_work_dock_kanim";
		int num3 = 100;
		float num4 = 60f;
		float[] tier = global::TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER4;
		string[] plastics = MATERIALS.PLASTICS;
		float num5 = 1600f;
		BuildLocationRule buildLocationRule = BuildLocationRule.OnFloor;
		EffectorValues none = NOISE_POLLUTION.NONE;
		BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, num, num2, text, num3, num4, tier, plastics, num5, buildLocationRule, global::TUNING.BUILDINGS.DECOR.PENALTY.TIER1, none, 0.2f);
		buildingDef.Overheatable = false;
		buildingDef.AudioCategory = "Plastic";
		buildingDef.InputConduitType = ConduitType.Liquid;
		buildingDef.OutputConduitType = ConduitType.Liquid;
		buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
		buildingDef.UtilityInputOffset = new CellOffset(0, 1);
		buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
		buildingDef.PowerInputOffset = new CellOffset(0, 0);
		buildingDef.RequiresPowerInput = true;
		buildingDef.EnergyConsumptionWhenActive = 120f;
		buildingDef.SelfHeatKilowattsWhenActive = 2f;
		buildingDef.ExhaustKilowattsWhenActive = 0f;
		buildingDef.AddSearchTerms(SEARCH_TERMS.ROBOT);
		return buildingDef;
	}

	// Token: 0x06001379 RID: 4985 RVA: 0x0006EEB0 File Offset: 0x0006D0B0
	public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
	{
		base.DoPostConfigurePreview(def, go);
		this.AddVisualizer(go);
	}

	// Token: 0x0600137A RID: 4986 RVA: 0x0006EEC1 File Offset: 0x0006D0C1
	public override void DoPostConfigureUnderConstruction(GameObject go)
	{
		this.AddVisualizer(go);
	}

	// Token: 0x0600137B RID: 4987 RVA: 0x0006EECC File Offset: 0x0006D0CC
	public override void DoPostConfigureComplete(GameObject go)
	{
		go.AddOrGet<RemoteWorkerDock>();
		go.AddOrGet<RemoteWorkerDockAnimSM>();
		go.AddOrGet<Operational>();
		go.AddOrGet<UserNameable>();
		go.AddComponent<Storage>().SetDefaultStoredItemModifiers(Storage.StandardInsulatedStorage);
		ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
		conduitConsumer.conduitType = ConduitType.Liquid;
		conduitConsumer.capacityTag = GameTags.LubricatingOil;
		conduitConsumer.capacityKG = 50f;
		conduitConsumer.wrongElementResult = ConduitConsumer.WrongElementResult.Dump;
		ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
		conduitDispenser.conduitType = ConduitType.Liquid;
		conduitDispenser.elementFilter = new SimHashes[] { SimHashes.LiquidGunk };
		this.AddVisualizer(go);
		go.AddOrGet<RangeVisualizer>();
	}

	// Token: 0x0600137C RID: 4988 RVA: 0x0006EF5C File Offset: 0x0006D15C
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC3;
	}

	// Token: 0x0600137D RID: 4989 RVA: 0x0006EF64 File Offset: 0x0006D164
	private void AddVisualizer(GameObject prefab)
	{
		RangeVisualizer rangeVisualizer = prefab.AddOrGet<RangeVisualizer>();
		rangeVisualizer.RangeMin.x = -12;
		rangeVisualizer.RangeMin.y = 0;
		rangeVisualizer.RangeMax.x = 12;
		rangeVisualizer.RangeMax.y = 0;
		rangeVisualizer.OriginOffset = default(Vector2I);
		rangeVisualizer.BlockingTileVisible = false;
		prefab.GetComponent<KPrefabID>().instantiateFn += delegate(GameObject go)
		{
			go.GetComponent<RangeVisualizer>().BlockingCb = new Func<int, bool>(RemoteWorkerDockConfig.DockPathBlockingCB);
		};
	}

	// Token: 0x0600137E RID: 4990 RVA: 0x0006EFE8 File Offset: 0x0006D1E8
	public static bool DockPathBlockingCB(int cell)
	{
		int num = Grid.CellAbove(cell);
		int num2 = Grid.CellBelow(cell);
		return num == Grid.InvalidCell || num2 == Grid.InvalidCell || (!Grid.Foundation[num2] && !Grid.Solid[num2]) || (Grid.Solid[cell] || Grid.Solid[num]);
	}

	// Token: 0x04000BC4 RID: 3012
	public static string ID = "RemoteWorkerDock";

	// Token: 0x04000BC5 RID: 3013
	public const float NEW_WORKER_DELAY_SECONDS = 2f;

	// Token: 0x04000BC6 RID: 3014
	public const int WORK_RANGE = 12;

	// Token: 0x04000BC7 RID: 3015
	public const float LUBRICANT_CAPACITY_KG = 50f;

	// Token: 0x04000BC8 RID: 3016
	public const string ON_EMPTY_ANIM = "on_empty";

	// Token: 0x04000BC9 RID: 3017
	public const string ON_FULL_ANIM = "on_full";

	// Token: 0x04000BCA RID: 3018
	public const string OFF_EMPTY_ANIM = "off_empty";

	// Token: 0x04000BCB RID: 3019
	public const string OFF_FULL_ANIM = "off_full";

	// Token: 0x04000BCC RID: 3020
	public const string NEW_WORKER_ANIM = "new_worker";
}
