using System;

// Token: 0x0200058C RID: 1420
public class CleaningMonitor : GameStateMachine<CleaningMonitor, CleaningMonitor.Instance, IStateMachineTarget, CleaningMonitor.Def>
{
	// Token: 0x06002070 RID: 8304 RVA: 0x000BB438 File Offset: 0x000B9638
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.clean;
		this.clean.ToggleBehaviour(GameTags.Creatures.Cleaning, (CleaningMonitor.Instance smi) => smi.CanCleanElementState(), delegate(CleaningMonitor.Instance smi)
		{
			smi.GoTo(this.cooldown);
		});
		this.cooldown.ScheduleGoTo((CleaningMonitor.Instance smi) => smi.def.coolDown, this.clean);
	}

	// Token: 0x040012E6 RID: 4838
	public GameStateMachine<CleaningMonitor, CleaningMonitor.Instance, IStateMachineTarget, CleaningMonitor.Def>.State cooldown;

	// Token: 0x040012E7 RID: 4839
	public GameStateMachine<CleaningMonitor, CleaningMonitor.Instance, IStateMachineTarget, CleaningMonitor.Def>.State clean;

	// Token: 0x020013E1 RID: 5089
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006AF8 RID: 27384
		public Element.State elementState = Element.State.Liquid;

		// Token: 0x04006AF9 RID: 27385
		public CellOffset[] cellOffsets;

		// Token: 0x04006AFA RID: 27386
		public float coolDown = 30f;
	}

	// Token: 0x020013E2 RID: 5090
	public new class Instance : GameStateMachine<CleaningMonitor, CleaningMonitor.Instance, IStateMachineTarget, CleaningMonitor.Def>.GameInstance
	{
		// Token: 0x06008BB7 RID: 35767 RVA: 0x003538AA File Offset: 0x00351AAA
		public Instance(IStateMachineTarget master, CleaningMonitor.Def def)
			: base(master, def)
		{
		}

		// Token: 0x06008BB8 RID: 35768 RVA: 0x003538B4 File Offset: 0x00351AB4
		public bool CanCleanElementState()
		{
			int num = Grid.PosToCell(base.smi.transform.GetPosition());
			if (!Grid.IsValidCell(num))
			{
				return false;
			}
			if (!Grid.IsLiquid(num) && base.smi.def.elementState == Element.State.Liquid)
			{
				return false;
			}
			if (Grid.DiseaseCount[num] > 0)
			{
				return true;
			}
			if (base.smi.def.cellOffsets != null)
			{
				foreach (CellOffset cellOffset in base.smi.def.cellOffsets)
				{
					int num2 = Grid.OffsetCell(num, cellOffset);
					if (Grid.IsValidCell(num2) && Grid.DiseaseCount[num2] > 0)
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
