using System;
using UnityEngine;

// Token: 0x02000A29 RID: 2601
[SkipSaveFileSerialization]
public class Climacophobic : StateMachineComponent<Climacophobic.StatesInstance>
{
	// Token: 0x06004B95 RID: 19349 RVA: 0x001B6996 File Offset: 0x001B4B96
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x06004B96 RID: 19350 RVA: 0x001B69A4 File Offset: 0x001B4BA4
	protected bool IsUncomfortable()
	{
		int num = 5;
		int num2 = Grid.PosToCell(base.gameObject);
		if (this.isCellLadder(num2))
		{
			int num3 = 1;
			bool flag = true;
			bool flag2 = true;
			for (int i = 1; i < num; i++)
			{
				int num4 = Grid.OffsetCell(num2, 0, i);
				int num5 = Grid.OffsetCell(num2, 0, -i);
				if (flag && this.isCellLadder(num4))
				{
					num3++;
				}
				else
				{
					flag = false;
				}
				if (flag2 && this.isCellLadder(num5))
				{
					num3++;
				}
				else
				{
					flag2 = false;
				}
			}
			return num3 >= num;
		}
		return false;
	}

	// Token: 0x06004B97 RID: 19351 RVA: 0x001B6A2C File Offset: 0x001B4C2C
	private bool isCellLadder(int cell)
	{
		if (!Grid.IsValidCell(cell))
		{
			return false;
		}
		GameObject gameObject = Grid.Objects[cell, 1];
		return !(gameObject == null) && !(gameObject.GetComponent<Ladder>() == null);
	}

	// Token: 0x02001AE8 RID: 6888
	public class StatesInstance : GameStateMachine<Climacophobic.States, Climacophobic.StatesInstance, Climacophobic, object>.GameInstance
	{
		// Token: 0x0600A58C RID: 42380 RVA: 0x003A94FE File Offset: 0x003A76FE
		public StatesInstance(Climacophobic master)
			: base(master)
		{
		}
	}

	// Token: 0x02001AE9 RID: 6889
	public class States : GameStateMachine<Climacophobic.States, Climacophobic.StatesInstance, Climacophobic>
	{
		// Token: 0x0600A58D RID: 42381 RVA: 0x003A9508 File Offset: 0x003A7708
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.satisfied;
			this.root.Update("ClimacophobicCheck", delegate(Climacophobic.StatesInstance smi, float dt)
			{
				if (smi.master.IsUncomfortable())
				{
					smi.GoTo(this.suffering);
					return;
				}
				smi.GoTo(this.satisfied);
			}, UpdateRate.SIM_1000ms, false);
			this.suffering.AddEffect("Vertigo").ToggleExpression(Db.Get().Expressions.Uncomfortable, null);
			this.satisfied.DoNothing();
		}

		// Token: 0x0400815B RID: 33115
		public GameStateMachine<Climacophobic.States, Climacophobic.StatesInstance, Climacophobic, object>.State satisfied;

		// Token: 0x0400815C RID: 33116
		public GameStateMachine<Climacophobic.States, Climacophobic.StatesInstance, Climacophobic, object>.State suffering;
	}
}
