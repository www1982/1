using System;

// Token: 0x02000A2F RID: 2607
[SkipSaveFileSerialization]
public class Workaholic : StateMachineComponent<Workaholic.StatesInstance>
{
	// Token: 0x06004BA6 RID: 19366 RVA: 0x001B6C58 File Offset: 0x001B4E58
	protected override void OnSpawn()
	{
		base.smi.StartSM();
	}

	// Token: 0x06004BA7 RID: 19367 RVA: 0x001B6C65 File Offset: 0x001B4E65
	protected bool IsUncomfortable()
	{
		return base.smi.master.GetComponent<ChoreDriver>().GetCurrentChore() is IdleChore;
	}

	// Token: 0x02001AF4 RID: 6900
	public class StatesInstance : GameStateMachine<Workaholic.States, Workaholic.StatesInstance, Workaholic, object>.GameInstance
	{
		// Token: 0x0600A5A6 RID: 42406 RVA: 0x003A991F File Offset: 0x003A7B1F
		public StatesInstance(Workaholic master)
			: base(master)
		{
		}
	}

	// Token: 0x02001AF5 RID: 6901
	public class States : GameStateMachine<Workaholic.States, Workaholic.StatesInstance, Workaholic>
	{
		// Token: 0x0600A5A7 RID: 42407 RVA: 0x003A9928 File Offset: 0x003A7B28
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.satisfied;
			this.root.Update("WorkaholicCheck", delegate(Workaholic.StatesInstance smi, float dt)
			{
				if (smi.master.IsUncomfortable())
				{
					smi.GoTo(this.suffering);
					return;
				}
				smi.GoTo(this.satisfied);
			}, UpdateRate.SIM_1000ms, false);
			this.suffering.AddEffect("Restless").ToggleExpression(Db.Get().Expressions.Uncomfortable, null);
			this.satisfied.DoNothing();
		}

		// Token: 0x04008165 RID: 33125
		public GameStateMachine<Workaholic.States, Workaholic.StatesInstance, Workaholic, object>.State satisfied;

		// Token: 0x04008166 RID: 33126
		public GameStateMachine<Workaholic.States, Workaholic.StatesInstance, Workaholic, object>.State suffering;
	}
}
