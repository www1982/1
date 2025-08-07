using System;

// Token: 0x02000A28 RID: 2600
[SkipSaveFileSerialization]
public class Claustrophobic : StateMachineComponent<Claustrophobic.StatesInstance>
{
	// Token: 0x06004B92 RID: 19346 RVA: 0x001B68F6 File Offset: 0x001B4AF6
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x06004B93 RID: 19347 RVA: 0x001B6904 File Offset: 0x001B4B04
	protected bool IsUncomfortable()
	{
		int num = 4;
		int num2 = Grid.PosToCell(base.gameObject);
		for (int i = 0; i < num - 1; i++)
		{
			int num3 = Grid.OffsetCell(num2, 0, i);
			if (Grid.IsValidCell(num3) && Grid.Solid[num3])
			{
				return true;
			}
			if (Grid.IsValidCell(Grid.CellRight(num2)) && Grid.IsValidCell(Grid.CellLeft(num2)) && Grid.Solid[Grid.CellRight(num2)] && Grid.Solid[Grid.CellLeft(num2)])
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x02001AE6 RID: 6886
	public class StatesInstance : GameStateMachine<Claustrophobic.States, Claustrophobic.StatesInstance, Claustrophobic, object>.GameInstance
	{
		// Token: 0x0600A588 RID: 42376 RVA: 0x003A945F File Offset: 0x003A765F
		public StatesInstance(Claustrophobic master)
			: base(master)
		{
		}
	}

	// Token: 0x02001AE7 RID: 6887
	public class States : GameStateMachine<Claustrophobic.States, Claustrophobic.StatesInstance, Claustrophobic>
	{
		// Token: 0x0600A589 RID: 42377 RVA: 0x003A9468 File Offset: 0x003A7668
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.satisfied;
			this.root.Update("ClaustrophobicCheck", delegate(Claustrophobic.StatesInstance smi, float dt)
			{
				if (smi.master.IsUncomfortable())
				{
					smi.GoTo(this.suffering);
					return;
				}
				smi.GoTo(this.satisfied);
			}, UpdateRate.SIM_1000ms, false);
			this.suffering.AddEffect("Claustrophobic").ToggleExpression(Db.Get().Expressions.Uncomfortable, null);
			this.satisfied.DoNothing();
		}

		// Token: 0x04008159 RID: 33113
		public GameStateMachine<Claustrophobic.States, Claustrophobic.StatesInstance, Claustrophobic, object>.State satisfied;

		// Token: 0x0400815A RID: 33114
		public GameStateMachine<Claustrophobic.States, Claustrophobic.StatesInstance, Claustrophobic, object>.State suffering;
	}
}
