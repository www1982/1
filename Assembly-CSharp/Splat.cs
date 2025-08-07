using System;

// Token: 0x02000B9D RID: 2973
public class Splat : GameStateMachine<Splat, Splat.StatesInstance>
{
	// Token: 0x060058C0 RID: 22720 RVA: 0x002017C8 File Offset: 0x001FF9C8
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.ToggleChore((Splat.StatesInstance smi) => new WorkChore<SplatWorkable>(Db.Get().ChoreTypes.Mop, smi.master, null, true, null, null, null, true, null, false, true, null, false, true, true, PriorityScreen.PriorityClass.basic, 5, false, true), this.complete);
		this.complete.Enter(delegate(Splat.StatesInstance smi)
		{
			Util.KDestroyGameObject(smi.master.gameObject);
		});
	}

	// Token: 0x04003AEB RID: 15083
	public GameStateMachine<Splat, Splat.StatesInstance, IStateMachineTarget, object>.State complete;

	// Token: 0x02001CC6 RID: 7366
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001CC7 RID: 7367
	public class StatesInstance : GameStateMachine<Splat, Splat.StatesInstance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600AC40 RID: 44096 RVA: 0x003C15C5 File Offset: 0x003BF7C5
		public StatesInstance(IStateMachineTarget master, Splat.Def def)
			: base(master, def)
		{
		}
	}
}
