using System;

// Token: 0x020009E5 RID: 2533
public class DebugGoToMonitor : GameStateMachine<DebugGoToMonitor, DebugGoToMonitor.Instance, IStateMachineTarget, DebugGoToMonitor.Def>
{
	// Token: 0x06004A1A RID: 18970 RVA: 0x001AD3C8 File Offset: 0x001AB5C8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.satisfied;
		this.satisfied.DoNothing();
		this.hastarget.ToggleChore((DebugGoToMonitor.Instance smi) => new MoveChore(smi.master, Db.Get().ChoreTypes.DebugGoTo, (MoveChore.StatesInstance smii) => smi.targetCellIndex, false), this.satisfied);
	}

	// Token: 0x040030E1 RID: 12513
	public GameStateMachine<DebugGoToMonitor, DebugGoToMonitor.Instance, IStateMachineTarget, DebugGoToMonitor.Def>.State satisfied;

	// Token: 0x040030E2 RID: 12514
	public GameStateMachine<DebugGoToMonitor, DebugGoToMonitor.Instance, IStateMachineTarget, DebugGoToMonitor.Def>.State hastarget;

	// Token: 0x02001A35 RID: 6709
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001A36 RID: 6710
	public new class Instance : GameStateMachine<DebugGoToMonitor, DebugGoToMonitor.Instance, IStateMachineTarget, DebugGoToMonitor.Def>.GameInstance
	{
		// Token: 0x0600A297 RID: 41623 RVA: 0x003A1645 File Offset: 0x0039F845
		public Instance(IStateMachineTarget target, DebugGoToMonitor.Def def)
			: base(target, def)
		{
		}

		// Token: 0x0600A298 RID: 41624 RVA: 0x003A165C File Offset: 0x0039F85C
		public void GoToCursor()
		{
			this.targetCellIndex = DebugHandler.GetMouseCell();
			if (base.smi.GetCurrentState() == base.smi.sm.satisfied)
			{
				base.smi.GoTo(base.smi.sm.hastarget);
			}
		}

		// Token: 0x0600A299 RID: 41625 RVA: 0x003A16AC File Offset: 0x0039F8AC
		public void GoToCell(int cellIndex)
		{
			this.targetCellIndex = cellIndex;
			if (base.smi.GetCurrentState() == base.smi.sm.satisfied)
			{
				base.smi.GoTo(base.smi.sm.hastarget);
			}
		}

		// Token: 0x04007EF1 RID: 32497
		public int targetCellIndex = Grid.InvalidCell;
	}
}
