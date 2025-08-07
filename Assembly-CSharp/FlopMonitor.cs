using System;
using UnityEngine;

// Token: 0x02000597 RID: 1431
public class FlopMonitor : GameStateMachine<FlopMonitor, FlopMonitor.Instance, IStateMachineTarget, FlopMonitor.Def>
{
	// Token: 0x060020B1 RID: 8369 RVA: 0x000BCE52 File Offset: 0x000BB052
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleBehaviour(GameTags.Creatures.Flopping, (FlopMonitor.Instance smi) => smi.ShouldBeginFlopping(), null);
	}

	// Token: 0x02001408 RID: 5128
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001409 RID: 5129
	public new class Instance : GameStateMachine<FlopMonitor, FlopMonitor.Instance, IStateMachineTarget, FlopMonitor.Def>.GameInstance
	{
		// Token: 0x06008C3D RID: 35901 RVA: 0x00355462 File Offset: 0x00353662
		public Instance(IStateMachineTarget master, FlopMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06008C3E RID: 35902 RVA: 0x0035546C File Offset: 0x0035366C
		public bool ShouldBeginFlopping()
		{
			Vector3 position = base.transform.GetPosition();
			position.y += CreatureFallMonitor.FLOOR_DISTANCE;
			int num = Grid.PosToCell(base.transform.GetPosition());
			int num2 = Grid.PosToCell(position);
			return Grid.IsValidCell(num2) && Grid.Solid[num2] && !Grid.IsSubstantialLiquid(num, 0.35f) && !Grid.IsLiquid(Grid.CellAbove(num));
		}
	}
}
