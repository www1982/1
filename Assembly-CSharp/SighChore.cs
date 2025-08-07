using System;
using UnityEngine;

// Token: 0x0200049B RID: 1179
public class SighChore : Chore<SighChore.StatesInstance>
{
	// Token: 0x060018A4 RID: 6308 RVA: 0x00089C2C File Offset: 0x00087E2C
	public SighChore(IStateMachineTarget target)
		: base(Db.Get().ChoreTypes.Sigh, target, target.GetComponent<ChoreProvider>(), false, null, null, null, PriorityScreen.PriorityClass.basic, 5, false, true, 0, false, ReportManager.ReportType.WorkTime)
	{
		base.smi = new SighChore.StatesInstance(this, target.gameObject);
	}

	// Token: 0x020012C0 RID: 4800
	public class StatesInstance : GameStateMachine<SighChore.States, SighChore.StatesInstance, SighChore, object>.GameInstance
	{
		// Token: 0x060087A2 RID: 34722 RVA: 0x00345302 File Offset: 0x00343502
		public StatesInstance(SighChore master, GameObject sigher)
			: base(master)
		{
			base.sm.sigher.Set(sigher, base.smi, false);
		}
	}

	// Token: 0x020012C1 RID: 4801
	public class States : GameStateMachine<SighChore.States, SighChore.StatesInstance, SighChore>
	{
		// Token: 0x060087A3 RID: 34723 RVA: 0x00345324 File Offset: 0x00343524
		public override void InitializeStates(out StateMachine.BaseState default_state)
		{
			default_state = this.root;
			base.Target(this.sigher);
			this.root.PlayAnim("emote_depressed").OnAnimQueueComplete(null);
		}

		// Token: 0x0400675B RID: 26459
		public StateMachine<SighChore.States, SighChore.StatesInstance, SighChore, object>.TargetParameter sigher;
	}
}
