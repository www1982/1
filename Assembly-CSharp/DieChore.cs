using System;

// Token: 0x0200047B RID: 1147
public class DieChore : Chore<DieChore.StatesInstance>
{
	// Token: 0x0600181C RID: 6172 RVA: 0x000864C4 File Offset: 0x000846C4
	public DieChore(IStateMachineTarget master, Death death)
		: base(Db.Get().ChoreTypes.Die, master, master.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.compulsory, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		this.showAvailabilityInHoverText = false;
		base.smi = new DieChore.StatesInstance(this, death);
	}

	// Token: 0x02001271 RID: 4721
	public class StatesInstance : GameStateMachine<DieChore.States, DieChore.StatesInstance, DieChore, object>.GameInstance
	{
		// Token: 0x0600867C RID: 34428 RVA: 0x0033DB2C File Offset: 0x0033BD2C
		public StatesInstance(DieChore master, Death death)
			: base(master)
		{
			base.sm.death.Set(death, base.smi, false);
		}

		// Token: 0x0600867D RID: 34429 RVA: 0x0033DB50 File Offset: 0x0033BD50
		public void PlayPreAnim()
		{
			string preAnim = base.sm.death.Get(base.smi).preAnim;
			base.GetComponent<KAnimControllerBase>().Play(preAnim, KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x02001272 RID: 4722
	public class States : GameStateMachine<DieChore.States, DieChore.StatesInstance, DieChore>
	{
		// Token: 0x0600867E RID: 34430 RVA: 0x0033DB98 File Offset: 0x0033BD98
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.dying;
			this.dying.OnAnimQueueComplete(this.dead).Enter("PlayAnim", delegate(DieChore.StatesInstance smi)
			{
				smi.PlayPreAnim();
			});
			this.dead.ReturnSuccess();
		}

		// Token: 0x0400664F RID: 26191
		public GameStateMachine<DieChore.States, DieChore.StatesInstance, DieChore, object>.State dying;

		// Token: 0x04006650 RID: 26192
		public GameStateMachine<DieChore.States, DieChore.StatesInstance, DieChore, object>.State dead;

		// Token: 0x04006651 RID: 26193
		public StateMachine<DieChore.States, DieChore.StatesInstance, DieChore, object>.ResourceParameter<Death> death;
	}
}
