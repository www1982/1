using System;

// Token: 0x02000A2D RID: 2605
[SkipSaveFileSerialization]
public class SensitiveFeet : StateMachineComponent<SensitiveFeet.StatesInstance>
{
	// Token: 0x06004BA0 RID: 19360 RVA: 0x001B6AE6 File Offset: 0x001B4CE6
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x06004BA1 RID: 19361 RVA: 0x001B6AF4 File Offset: 0x001B4CF4
	protected bool IsUncomfortable()
	{
		int num = Grid.CellBelow(Grid.PosToCell(base.gameObject));
		return Grid.IsValidCell(num) && Grid.Solid[num] && Grid.Objects[num, 9] == null;
	}

	// Token: 0x02001AF0 RID: 6896
	public class StatesInstance : GameStateMachine<SensitiveFeet.States, SensitiveFeet.StatesInstance, SensitiveFeet, object>.GameInstance
	{
		// Token: 0x0600A59D RID: 42397 RVA: 0x003A9768 File Offset: 0x003A7968
		public StatesInstance(SensitiveFeet master)
			: base(master)
		{
		}
	}

	// Token: 0x02001AF1 RID: 6897
	public class States : GameStateMachine<SensitiveFeet.States, SensitiveFeet.StatesInstance, SensitiveFeet>
	{
		// Token: 0x0600A59E RID: 42398 RVA: 0x003A9774 File Offset: 0x003A7974
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.satisfied;
			this.root.Update("SensitiveFeetCheck", delegate(SensitiveFeet.StatesInstance smi, float dt)
			{
				if (smi.master.IsUncomfortable())
				{
					smi.GoTo(this.suffering);
					return;
				}
				smi.GoTo(this.satisfied);
			}, UpdateRate.SIM_1000ms, false);
			this.suffering.AddEffect("UncomfortableFeet").ToggleExpression(Db.Get().Expressions.Uncomfortable, null);
			this.satisfied.DoNothing();
		}

		// Token: 0x04008161 RID: 33121
		public GameStateMachine<SensitiveFeet.States, SensitiveFeet.StatesInstance, SensitiveFeet, object>.State satisfied;

		// Token: 0x04008162 RID: 33122
		public GameStateMachine<SensitiveFeet.States, SensitiveFeet.StatesInstance, SensitiveFeet, object>.State suffering;
	}
}
