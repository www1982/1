using System;
using System.Collections.Generic;
using System.Diagnostics;

// Token: 0x02000908 RID: 2312
public class CellEventLogger : EventLogger<CellEventInstance, CellEvent>
{
	// Token: 0x06004070 RID: 16496 RVA: 0x00169365 File Offset: 0x00167565
	public static void DestroyInstance()
	{
		CellEventLogger.Instance = null;
	}

	// Token: 0x06004071 RID: 16497 RVA: 0x0016936D File Offset: 0x0016756D
	[Conditional("ENABLE_CELL_EVENT_LOGGER")]
	public void LogCallbackSend(int cell, int callback_id)
	{
		if (callback_id != -1)
		{
			this.CallbackToCellMap[callback_id] = cell;
		}
	}

	// Token: 0x06004072 RID: 16498 RVA: 0x00169380 File Offset: 0x00167580
	[Conditional("ENABLE_CELL_EVENT_LOGGER")]
	public void LogCallbackReceive(int callback_id)
	{
		int invalidCell = Grid.InvalidCell;
		this.CallbackToCellMap.TryGetValue(callback_id, out invalidCell);
	}

	// Token: 0x06004073 RID: 16499 RVA: 0x001693A4 File Offset: 0x001675A4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		CellEventLogger.Instance = this;
		this.SimMessagesSolid = base.AddEvent(new CellSolidEvent("SimMessageSolid", "Sim Message", false, true)) as CellSolidEvent;
		this.SimCellOccupierDestroy = base.AddEvent(new CellSolidEvent("SimCellOccupierClearSolid", "Sim Cell Occupier Destroy", false, true)) as CellSolidEvent;
		this.SimCellOccupierForceSolid = base.AddEvent(new CellSolidEvent("SimCellOccupierForceSolid", "Sim Cell Occupier Force Solid", false, true)) as CellSolidEvent;
		this.SimCellOccupierSolidChanged = base.AddEvent(new CellSolidEvent("SimCellOccupierSolidChanged", "Sim Cell Occupier Solid Changed", false, true)) as CellSolidEvent;
		this.DoorOpen = base.AddEvent(new CellElementEvent("DoorOpen", "Door Open", true, true)) as CellElementEvent;
		this.DoorClose = base.AddEvent(new CellElementEvent("DoorClose", "Door Close", true, true)) as CellElementEvent;
		this.Excavator = base.AddEvent(new CellElementEvent("Excavator", "Excavator", true, true)) as CellElementEvent;
		this.DebugTool = base.AddEvent(new CellElementEvent("DebugTool", "Debug Tool", true, true)) as CellElementEvent;
		this.SandBoxTool = base.AddEvent(new CellElementEvent("SandBoxTool", "Sandbox Tool", true, true)) as CellElementEvent;
		this.TemplateLoader = base.AddEvent(new CellElementEvent("TemplateLoader", "Template Loader", true, true)) as CellElementEvent;
		this.Scenario = base.AddEvent(new CellElementEvent("Scenario", "Scenario", true, true)) as CellElementEvent;
		this.SimCellOccupierOnSpawn = base.AddEvent(new CellElementEvent("SimCellOccupierOnSpawn", "Sim Cell Occupier OnSpawn", true, true)) as CellElementEvent;
		this.SimCellOccupierDestroySelf = base.AddEvent(new CellElementEvent("SimCellOccupierDestroySelf", "Sim Cell Occupier Destroy Self", true, true)) as CellElementEvent;
		this.WorldGapManager = base.AddEvent(new CellElementEvent("WorldGapManager", "World Gap Manager", true, true)) as CellElementEvent;
		this.ReceiveElementChanged = base.AddEvent(new CellElementEvent("ReceiveElementChanged", "Sim Message", false, false)) as CellElementEvent;
		this.ObjectSetSimOnSpawn = base.AddEvent(new CellElementEvent("ObjectSetSimOnSpawn", "Object set sim on spawn", true, true)) as CellElementEvent;
		this.DecompositionDirtyWater = base.AddEvent(new CellElementEvent("DecompositionDirtyWater", "Decomposition dirty water", true, true)) as CellElementEvent;
		this.SendCallback = base.AddEvent(new CellCallbackEvent("SendCallback", true, true)) as CellCallbackEvent;
		this.ReceiveCallback = base.AddEvent(new CellCallbackEvent("ReceiveCallback", false, true)) as CellCallbackEvent;
		this.Dig = base.AddEvent(new CellDigEvent(true)) as CellDigEvent;
		this.WorldDamageDelayedSpawnFX = base.AddEvent(new CellAddRemoveSubstanceEvent("WorldDamageDelayedSpawnFX", "World Damage Delayed Spawn FX", false)) as CellAddRemoveSubstanceEvent;
		this.OxygenModifierSimUpdate = base.AddEvent(new CellAddRemoveSubstanceEvent("OxygenModifierSimUpdate", "Oxygen Modifier SimUpdate", false)) as CellAddRemoveSubstanceEvent;
		this.LiquidChunkOnStore = base.AddEvent(new CellAddRemoveSubstanceEvent("LiquidChunkOnStore", "Liquid Chunk On Store", false)) as CellAddRemoveSubstanceEvent;
		this.FallingWaterAddToSim = base.AddEvent(new CellAddRemoveSubstanceEvent("FallingWaterAddToSim", "Falling Water Add To Sim", false)) as CellAddRemoveSubstanceEvent;
		this.ExploderOnSpawn = base.AddEvent(new CellAddRemoveSubstanceEvent("ExploderOnSpawn", "Exploder OnSpawn", false)) as CellAddRemoveSubstanceEvent;
		this.ExhaustSimUpdate = base.AddEvent(new CellAddRemoveSubstanceEvent("ExhaustSimUpdate", "Exhaust SimUpdate", false)) as CellAddRemoveSubstanceEvent;
		this.ElementConsumerSimUpdate = base.AddEvent(new CellAddRemoveSubstanceEvent("ElementConsumerSimUpdate", "Element Consumer SimUpdate", false)) as CellAddRemoveSubstanceEvent;
		this.SublimatesEmit = base.AddEvent(new CellAddRemoveSubstanceEvent("SublimatesEmit", "Sublimates Emit", false)) as CellAddRemoveSubstanceEvent;
		this.Mop = base.AddEvent(new CellAddRemoveSubstanceEvent("Mop", "Mop", false)) as CellAddRemoveSubstanceEvent;
		this.OreMelted = base.AddEvent(new CellAddRemoveSubstanceEvent("OreMelted", "Ore Melted", false)) as CellAddRemoveSubstanceEvent;
		this.ConstructTile = base.AddEvent(new CellAddRemoveSubstanceEvent("ConstructTile", "ConstructTile", false)) as CellAddRemoveSubstanceEvent;
		this.Dumpable = base.AddEvent(new CellAddRemoveSubstanceEvent("Dympable", "Dumpable", false)) as CellAddRemoveSubstanceEvent;
		this.Cough = base.AddEvent(new CellAddRemoveSubstanceEvent("Cough", "Cough", false)) as CellAddRemoveSubstanceEvent;
		this.Meteor = base.AddEvent(new CellAddRemoveSubstanceEvent("Meteor", "Meteor", false)) as CellAddRemoveSubstanceEvent;
		this.ElementChunkTransition = base.AddEvent(new CellAddRemoveSubstanceEvent("ElementChunkTransition", "Element Chunk Transition", false)) as CellAddRemoveSubstanceEvent;
		this.OxyrockEmit = base.AddEvent(new CellAddRemoveSubstanceEvent("OxyrockEmit", "Oxyrock Emit", false)) as CellAddRemoveSubstanceEvent;
		this.BleachstoneEmit = base.AddEvent(new CellAddRemoveSubstanceEvent("BleachstoneEmit", "Bleachstone Emit", false)) as CellAddRemoveSubstanceEvent;
		this.UnstableGround = base.AddEvent(new CellAddRemoveSubstanceEvent("UnstableGround", "Unstable Ground", false)) as CellAddRemoveSubstanceEvent;
		this.ConduitFlowEmptyConduit = base.AddEvent(new CellAddRemoveSubstanceEvent("ConduitFlowEmptyConduit", "Conduit Flow Empty Conduit", false)) as CellAddRemoveSubstanceEvent;
		this.ConduitConsumerWrongElement = base.AddEvent(new CellAddRemoveSubstanceEvent("ConduitConsumerWrongElement", "Conduit Consumer Wrong Element", false)) as CellAddRemoveSubstanceEvent;
		this.OverheatableMeltingDown = base.AddEvent(new CellAddRemoveSubstanceEvent("OverheatableMeltingDown", "Overheatable MeltingDown", false)) as CellAddRemoveSubstanceEvent;
		this.FabricatorProduceMelted = base.AddEvent(new CellAddRemoveSubstanceEvent("FabricatorProduceMelted", "Fabricator Produce Melted", false)) as CellAddRemoveSubstanceEvent;
		this.PumpSimUpdate = base.AddEvent(new CellAddRemoveSubstanceEvent("PumpSimUpdate", "Pump SimUpdate", false)) as CellAddRemoveSubstanceEvent;
		this.WallPumpSimUpdate = base.AddEvent(new CellAddRemoveSubstanceEvent("WallPumpSimUpdate", "Wall Pump SimUpdate", false)) as CellAddRemoveSubstanceEvent;
		this.Vomit = base.AddEvent(new CellAddRemoveSubstanceEvent("Vomit", "Vomit", false)) as CellAddRemoveSubstanceEvent;
		this.Tears = base.AddEvent(new CellAddRemoveSubstanceEvent("Tears", "Tears", false)) as CellAddRemoveSubstanceEvent;
		this.Pee = base.AddEvent(new CellAddRemoveSubstanceEvent("Pee", "Pee", false)) as CellAddRemoveSubstanceEvent;
		this.AlgaeHabitat = base.AddEvent(new CellAddRemoveSubstanceEvent("AlgaeHabitat", "AlgaeHabitat", false)) as CellAddRemoveSubstanceEvent;
		this.CO2FilterOxygen = base.AddEvent(new CellAddRemoveSubstanceEvent("CO2FilterOxygen", "CO2FilterOxygen", false)) as CellAddRemoveSubstanceEvent;
		this.ToiletEmit = base.AddEvent(new CellAddRemoveSubstanceEvent("ToiletEmit", "ToiletEmit", false)) as CellAddRemoveSubstanceEvent;
		this.ElementEmitted = base.AddEvent(new CellAddRemoveSubstanceEvent("ElementEmitted", "Element Emitted", false)) as CellAddRemoveSubstanceEvent;
		this.CO2ManagerFixedUpdate = base.AddEvent(new CellModifyMassEvent("CO2ManagerFixedUpdate", "CO2Manager FixedUpdate", false)) as CellModifyMassEvent;
		this.EnvironmentConsumerFixedUpdate = base.AddEvent(new CellModifyMassEvent("EnvironmentConsumerFixedUpdate", "EnvironmentConsumer FixedUpdate", false)) as CellModifyMassEvent;
		this.ExcavatorShockwave = base.AddEvent(new CellModifyMassEvent("ExcavatorShockwave", "Excavator Shockwave", false)) as CellModifyMassEvent;
		this.OxygenBreatherSimUpdate = base.AddEvent(new CellModifyMassEvent("OxygenBreatherSimUpdate", "Oxygen Breather SimUpdate", false)) as CellModifyMassEvent;
		this.CO2ScrubberSimUpdate = base.AddEvent(new CellModifyMassEvent("CO2ScrubberSimUpdate", "CO2Scrubber SimUpdate", false)) as CellModifyMassEvent;
		this.RiverSourceSimUpdate = base.AddEvent(new CellModifyMassEvent("RiverSourceSimUpdate", "RiverSource SimUpdate", false)) as CellModifyMassEvent;
		this.RiverTerminusSimUpdate = base.AddEvent(new CellModifyMassEvent("RiverTerminusSimUpdate", "RiverTerminus SimUpdate", false)) as CellModifyMassEvent;
		this.DebugToolModifyMass = base.AddEvent(new CellModifyMassEvent("DebugToolModifyMass", "DebugTool ModifyMass", false)) as CellModifyMassEvent;
		this.EnergyGeneratorModifyMass = base.AddEvent(new CellModifyMassEvent("EnergyGeneratorModifyMass", "EnergyGenerator ModifyMass", false)) as CellModifyMassEvent;
		this.SolidFilterEvent = base.AddEvent(new CellSolidFilterEvent("SolidFilterEvent", true)) as CellSolidFilterEvent;
	}

	// Token: 0x040027F9 RID: 10233
	public static CellEventLogger Instance;

	// Token: 0x040027FA RID: 10234
	public CellSolidEvent SimMessagesSolid;

	// Token: 0x040027FB RID: 10235
	public CellSolidEvent SimCellOccupierDestroy;

	// Token: 0x040027FC RID: 10236
	public CellSolidEvent SimCellOccupierForceSolid;

	// Token: 0x040027FD RID: 10237
	public CellSolidEvent SimCellOccupierSolidChanged;

	// Token: 0x040027FE RID: 10238
	public CellElementEvent DoorOpen;

	// Token: 0x040027FF RID: 10239
	public CellElementEvent DoorClose;

	// Token: 0x04002800 RID: 10240
	public CellElementEvent Excavator;

	// Token: 0x04002801 RID: 10241
	public CellElementEvent DebugTool;

	// Token: 0x04002802 RID: 10242
	public CellElementEvent SandBoxTool;

	// Token: 0x04002803 RID: 10243
	public CellElementEvent TemplateLoader;

	// Token: 0x04002804 RID: 10244
	public CellElementEvent Scenario;

	// Token: 0x04002805 RID: 10245
	public CellElementEvent SimCellOccupierOnSpawn;

	// Token: 0x04002806 RID: 10246
	public CellElementEvent SimCellOccupierDestroySelf;

	// Token: 0x04002807 RID: 10247
	public CellElementEvent WorldGapManager;

	// Token: 0x04002808 RID: 10248
	public CellElementEvent ReceiveElementChanged;

	// Token: 0x04002809 RID: 10249
	public CellElementEvent ObjectSetSimOnSpawn;

	// Token: 0x0400280A RID: 10250
	public CellElementEvent DecompositionDirtyWater;

	// Token: 0x0400280B RID: 10251
	public CellElementEvent LaunchpadDesolidify;

	// Token: 0x0400280C RID: 10252
	public CellCallbackEvent SendCallback;

	// Token: 0x0400280D RID: 10253
	public CellCallbackEvent ReceiveCallback;

	// Token: 0x0400280E RID: 10254
	public CellDigEvent Dig;

	// Token: 0x0400280F RID: 10255
	public CellAddRemoveSubstanceEvent WorldDamageDelayedSpawnFX;

	// Token: 0x04002810 RID: 10256
	public CellAddRemoveSubstanceEvent SublimatesEmit;

	// Token: 0x04002811 RID: 10257
	public CellAddRemoveSubstanceEvent OxygenModifierSimUpdate;

	// Token: 0x04002812 RID: 10258
	public CellAddRemoveSubstanceEvent LiquidChunkOnStore;

	// Token: 0x04002813 RID: 10259
	public CellAddRemoveSubstanceEvent FallingWaterAddToSim;

	// Token: 0x04002814 RID: 10260
	public CellAddRemoveSubstanceEvent ExploderOnSpawn;

	// Token: 0x04002815 RID: 10261
	public CellAddRemoveSubstanceEvent ExhaustSimUpdate;

	// Token: 0x04002816 RID: 10262
	public CellAddRemoveSubstanceEvent ElementConsumerSimUpdate;

	// Token: 0x04002817 RID: 10263
	public CellAddRemoveSubstanceEvent ElementChunkTransition;

	// Token: 0x04002818 RID: 10264
	public CellAddRemoveSubstanceEvent OxyrockEmit;

	// Token: 0x04002819 RID: 10265
	public CellAddRemoveSubstanceEvent BleachstoneEmit;

	// Token: 0x0400281A RID: 10266
	public CellAddRemoveSubstanceEvent UnstableGround;

	// Token: 0x0400281B RID: 10267
	public CellAddRemoveSubstanceEvent ConduitFlowEmptyConduit;

	// Token: 0x0400281C RID: 10268
	public CellAddRemoveSubstanceEvent ConduitConsumerWrongElement;

	// Token: 0x0400281D RID: 10269
	public CellAddRemoveSubstanceEvent OverheatableMeltingDown;

	// Token: 0x0400281E RID: 10270
	public CellAddRemoveSubstanceEvent FabricatorProduceMelted;

	// Token: 0x0400281F RID: 10271
	public CellAddRemoveSubstanceEvent PumpSimUpdate;

	// Token: 0x04002820 RID: 10272
	public CellAddRemoveSubstanceEvent WallPumpSimUpdate;

	// Token: 0x04002821 RID: 10273
	public CellAddRemoveSubstanceEvent Vomit;

	// Token: 0x04002822 RID: 10274
	public CellAddRemoveSubstanceEvent Tears;

	// Token: 0x04002823 RID: 10275
	public CellAddRemoveSubstanceEvent Pee;

	// Token: 0x04002824 RID: 10276
	public CellAddRemoveSubstanceEvent AlgaeHabitat;

	// Token: 0x04002825 RID: 10277
	public CellAddRemoveSubstanceEvent CO2FilterOxygen;

	// Token: 0x04002826 RID: 10278
	public CellAddRemoveSubstanceEvent ToiletEmit;

	// Token: 0x04002827 RID: 10279
	public CellAddRemoveSubstanceEvent ElementEmitted;

	// Token: 0x04002828 RID: 10280
	public CellAddRemoveSubstanceEvent Mop;

	// Token: 0x04002829 RID: 10281
	public CellAddRemoveSubstanceEvent OreMelted;

	// Token: 0x0400282A RID: 10282
	public CellAddRemoveSubstanceEvent ConstructTile;

	// Token: 0x0400282B RID: 10283
	public CellAddRemoveSubstanceEvent Dumpable;

	// Token: 0x0400282C RID: 10284
	public CellAddRemoveSubstanceEvent Cough;

	// Token: 0x0400282D RID: 10285
	public CellAddRemoveSubstanceEvent Meteor;

	// Token: 0x0400282E RID: 10286
	public CellModifyMassEvent CO2ManagerFixedUpdate;

	// Token: 0x0400282F RID: 10287
	public CellModifyMassEvent EnvironmentConsumerFixedUpdate;

	// Token: 0x04002830 RID: 10288
	public CellModifyMassEvent ExcavatorShockwave;

	// Token: 0x04002831 RID: 10289
	public CellModifyMassEvent OxygenBreatherSimUpdate;

	// Token: 0x04002832 RID: 10290
	public CellModifyMassEvent CO2ScrubberSimUpdate;

	// Token: 0x04002833 RID: 10291
	public CellModifyMassEvent RiverSourceSimUpdate;

	// Token: 0x04002834 RID: 10292
	public CellModifyMassEvent RiverTerminusSimUpdate;

	// Token: 0x04002835 RID: 10293
	public CellModifyMassEvent DebugToolModifyMass;

	// Token: 0x04002836 RID: 10294
	public CellModifyMassEvent EnergyGeneratorModifyMass;

	// Token: 0x04002837 RID: 10295
	public CellSolidFilterEvent SolidFilterEvent;

	// Token: 0x04002838 RID: 10296
	public Dictionary<int, int> CallbackToCellMap = new Dictionary<int, int>();
}
