using System;

// Token: 0x020009E1 RID: 2529
public class CreatureDebugGoToMonitor : GameStateMachine<CreatureDebugGoToMonitor, CreatureDebugGoToMonitor.Instance, IStateMachineTarget, CreatureDebugGoToMonitor.Def>
{
	// Token: 0x06004A0A RID: 18954 RVA: 0x001ACE65 File Offset: 0x001AB065
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.HasDebugDestination, new StateMachine<CreatureDebugGoToMonitor, CreatureDebugGoToMonitor.Instance, IStateMachineTarget, CreatureDebugGoToMonitor.Def>.Transition.ConditionCallback(CreatureDebugGoToMonitor.HasTargetCell), new Action<CreatureDebugGoToMonitor.Instance>(CreatureDebugGoToMonitor.ClearTargetCell));
	}

	// Token: 0x06004A0B RID: 18955 RVA: 0x001ACE98 File Offset: 0x001AB098
	private static bool HasTargetCell(CreatureDebugGoToMonitor.Instance smi)
	{
		return smi.targetCell != Grid.InvalidCell;
	}

	// Token: 0x06004A0C RID: 18956 RVA: 0x001ACEAA File Offset: 0x001AB0AA
	private static void ClearTargetCell(CreatureDebugGoToMonitor.Instance smi)
	{
		smi.targetCell = Grid.InvalidCell;
	}

	// Token: 0x02001A2B RID: 6699
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001A2C RID: 6700
	public new class Instance : GameStateMachine<CreatureDebugGoToMonitor, CreatureDebugGoToMonitor.Instance, IStateMachineTarget, CreatureDebugGoToMonitor.Def>.GameInstance
	{
		// Token: 0x0600A274 RID: 41588 RVA: 0x003A12AE File Offset: 0x0039F4AE
		public Instance(IStateMachineTarget target, CreatureDebugGoToMonitor.Def def)
			: base(target, def)
		{
		}

		// Token: 0x0600A275 RID: 41589 RVA: 0x003A12C3 File Offset: 0x0039F4C3
		public void GoToCursor()
		{
			this.targetCell = DebugHandler.GetMouseCell();
		}

		// Token: 0x0600A276 RID: 41590 RVA: 0x003A12D0 File Offset: 0x0039F4D0
		public void GoToCell(int cellIndex)
		{
			this.targetCell = cellIndex;
		}

		// Token: 0x04007EDD RID: 32477
		public int targetCell = Grid.InvalidCell;
	}
}
