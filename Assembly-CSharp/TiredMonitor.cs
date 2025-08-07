using System;

// Token: 0x02000A19 RID: 2585
public class TiredMonitor : GameStateMachine<TiredMonitor, TiredMonitor.Instance>
{
	// Token: 0x06004B25 RID: 19237 RVA: 0x001B404C File Offset: 0x001B224C
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.EventTransition(GameHashes.SleepFail, this.tired, null);
		this.tired.Enter(delegate(TiredMonitor.Instance smi)
		{
			smi.SetInterruptDay();
		}).EventTransition(GameHashes.NewDay, (TiredMonitor.Instance smi) => GameClock.Instance, this.root, (TiredMonitor.Instance smi) => smi.AllowInterruptClear()).ToggleExpression(Db.Get().Expressions.Tired, null)
			.ToggleAnims("anim_loco_walk_slouch_kanim", 0f)
			.ToggleAnims("anim_idle_slouch_kanim", 0f);
	}

	// Token: 0x040031D0 RID: 12752
	public GameStateMachine<TiredMonitor, TiredMonitor.Instance, IStateMachineTarget, object>.State tired;

	// Token: 0x02001AC5 RID: 6853
	public new class Instance : GameStateMachine<TiredMonitor, TiredMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A500 RID: 42240 RVA: 0x003A7DEA File Offset: 0x003A5FEA
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}

		// Token: 0x0600A501 RID: 42241 RVA: 0x003A7E01 File Offset: 0x003A6001
		public void SetInterruptDay()
		{
			this.interruptedDay = GameClock.Instance.GetCycle();
		}

		// Token: 0x0600A502 RID: 42242 RVA: 0x003A7E13 File Offset: 0x003A6013
		public bool AllowInterruptClear()
		{
			bool flag = GameClock.Instance.GetCycle() > this.interruptedDay + 1;
			if (flag)
			{
				this.interruptedDay = -1;
			}
			return flag;
		}

		// Token: 0x040080DB RID: 32987
		public int disturbedDay = -1;

		// Token: 0x040080DC RID: 32988
		public int interruptedDay = -1;
	}
}
