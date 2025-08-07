using System;

// Token: 0x0200077D RID: 1917
public class MakeBaseSolid : GameStateMachine<MakeBaseSolid, MakeBaseSolid.Instance, IStateMachineTarget, MakeBaseSolid.Def>
{
	// Token: 0x0600326C RID: 12908 RVA: 0x0011C4FE File Offset: 0x0011A6FE
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.Enter(new StateMachine<MakeBaseSolid, MakeBaseSolid.Instance, IStateMachineTarget, MakeBaseSolid.Def>.State.Callback(MakeBaseSolid.ConvertToSolid)).Exit(new StateMachine<MakeBaseSolid, MakeBaseSolid.Instance, IStateMachineTarget, MakeBaseSolid.Def>.State.Callback(MakeBaseSolid.ConvertToVacuum));
	}

	// Token: 0x0600326D RID: 12909 RVA: 0x0011C534 File Offset: 0x0011A734
	private static void ConvertToSolid(MakeBaseSolid.Instance smi)
	{
		if (smi.buildingComplete == null)
		{
			return;
		}
		int num = Grid.PosToCell(smi.gameObject);
		PrimaryElement component = smi.GetComponent<PrimaryElement>();
		Building component2 = smi.GetComponent<Building>();
		foreach (CellOffset cellOffset in smi.def.solidOffsets)
		{
			CellOffset rotatedOffset = component2.GetRotatedOffset(cellOffset);
			int num2 = Grid.OffsetCell(num, rotatedOffset);
			if (smi.def.occupyFoundationLayer)
			{
				SimMessages.ReplaceAndDisplaceElement(num2, component.ElementID, CellEventLogger.Instance.SimCellOccupierOnSpawn, component.Mass, component.Temperature, byte.MaxValue, 0, -1);
				Grid.Objects[num2, 9] = smi.gameObject;
			}
			else
			{
				SimMessages.ReplaceAndDisplaceElement(num2, SimHashes.Vacuum, CellEventLogger.Instance.SimCellOccupierOnSpawn, 0f, 0f, byte.MaxValue, 0, -1);
			}
			Grid.Foundation[num2] = true;
			Grid.SetSolid(num2, true, CellEventLogger.Instance.SimCellOccupierForceSolid);
			SimMessages.SetCellProperties(num2, 103);
			Grid.RenderedByWorld[num2] = false;
			World.Instance.OnSolidChanged(num2);
			GameScenePartitioner.Instance.TriggerEvent(num2, GameScenePartitioner.Instance.solidChangedLayer, null);
		}
	}

	// Token: 0x0600326E RID: 12910 RVA: 0x0011C680 File Offset: 0x0011A880
	private static void ConvertToVacuum(MakeBaseSolid.Instance smi)
	{
		if (smi.buildingComplete == null)
		{
			return;
		}
		int num = Grid.PosToCell(smi.gameObject);
		Building component = smi.GetComponent<Building>();
		foreach (CellOffset cellOffset in smi.def.solidOffsets)
		{
			CellOffset rotatedOffset = component.GetRotatedOffset(cellOffset);
			int num2 = Grid.OffsetCell(num, rotatedOffset);
			SimMessages.ReplaceAndDisplaceElement(num2, SimHashes.Vacuum, CellEventLogger.Instance.SimCellOccupierOnSpawn, 0f, -1f, byte.MaxValue, 0, -1);
			Grid.Objects[num2, 9] = null;
			Grid.Foundation[num2] = false;
			Grid.SetSolid(num2, false, CellEventLogger.Instance.SimCellOccupierDestroy);
			SimMessages.ClearCellProperties(num2, 103);
			Grid.RenderedByWorld[num2] = true;
			World.Instance.OnSolidChanged(num2);
			GameScenePartitioner.Instance.TriggerEvent(num2, GameScenePartitioner.Instance.solidChangedLayer, null);
		}
	}

	// Token: 0x04001E35 RID: 7733
	private const Sim.Cell.Properties floorCellProperties = (Sim.Cell.Properties)103;

	// Token: 0x02001662 RID: 5730
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04007290 RID: 29328
		public CellOffset[] solidOffsets;

		// Token: 0x04007291 RID: 29329
		public bool occupyFoundationLayer = true;
	}

	// Token: 0x02001663 RID: 5731
	public new class Instance : GameStateMachine<MakeBaseSolid, MakeBaseSolid.Instance, IStateMachineTarget, MakeBaseSolid.Def>.GameInstance
	{
		// Token: 0x060094E7 RID: 38119 RVA: 0x003716A9 File Offset: 0x0036F8A9
		public Instance(IStateMachineTarget master, MakeBaseSolid.Def def)
			: base(master, def)
		{
		}

		// Token: 0x04007292 RID: 29330
		[MyCmpGet]
		public BuildingComplete buildingComplete;
	}
}
