using System;

// Token: 0x0200072F RID: 1839
public class FoodRehydratorSM : GameStateMachine<FoodRehydratorSM, FoodRehydratorSM.StatesInstance, IStateMachineTarget, FoodRehydratorSM.Def>
{
	// Token: 0x06002E63 RID: 11875 RVA: 0x0010A024 File Offset: 0x00108224
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.EnterTransition(this.off, (FoodRehydratorSM.StatesInstance smi) => !smi.operational.IsFunctional).EnterTransition(this.on, (FoodRehydratorSM.StatesInstance smi) => smi.operational.IsFunctional);
		this.off.PlayAnim("off", KAnim.PlayMode.Loop).EnterTransition(this.on, (FoodRehydratorSM.StatesInstance smi) => smi.operational.IsFunctional).EventTransition(GameHashes.FunctionalChanged, this.on, (FoodRehydratorSM.StatesInstance smi) => smi.operational.IsFunctional);
		this.on.PlayAnim("on", KAnim.PlayMode.Loop).EnterTransition(this.off, (FoodRehydratorSM.StatesInstance smi) => !smi.operational.IsFunctional).EnterTransition(this.active, (FoodRehydratorSM.StatesInstance smi) => smi.operational.IsActive)
			.EventTransition(GameHashes.FunctionalChanged, this.off, (FoodRehydratorSM.StatesInstance smi) => !smi.operational.IsFunctional)
			.EventTransition(GameHashes.ActiveChanged, this.active, (FoodRehydratorSM.StatesInstance smi) => smi.operational.IsActive);
		this.active.EnterTransition(this.off, (FoodRehydratorSM.StatesInstance smi) => !smi.operational.IsFunctional).EnterTransition(this.on, (FoodRehydratorSM.StatesInstance smi) => !smi.operational.IsActive).EventTransition(GameHashes.FunctionalChanged, this.off, (FoodRehydratorSM.StatesInstance smi) => !smi.operational.IsFunctional)
			.EventTransition(GameHashes.ActiveChanged, this.postactive, (FoodRehydratorSM.StatesInstance smi) => !smi.operational.IsActive);
		this.postactive.OnAnimQueueComplete(this.on);
	}

	// Token: 0x04001B73 RID: 7027
	private GameStateMachine<FoodRehydratorSM, FoodRehydratorSM.StatesInstance, IStateMachineTarget, FoodRehydratorSM.Def>.State off;

	// Token: 0x04001B74 RID: 7028
	private GameStateMachine<FoodRehydratorSM, FoodRehydratorSM.StatesInstance, IStateMachineTarget, FoodRehydratorSM.Def>.State on;

	// Token: 0x04001B75 RID: 7029
	private GameStateMachine<FoodRehydratorSM, FoodRehydratorSM.StatesInstance, IStateMachineTarget, FoodRehydratorSM.Def>.State active;

	// Token: 0x04001B76 RID: 7030
	private GameStateMachine<FoodRehydratorSM, FoodRehydratorSM.StatesInstance, IStateMachineTarget, FoodRehydratorSM.Def>.State postactive;

	// Token: 0x020015DB RID: 5595
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x020015DC RID: 5596
	public class StatesInstance : GameStateMachine<FoodRehydratorSM, FoodRehydratorSM.StatesInstance, IStateMachineTarget, FoodRehydratorSM.Def>.GameInstance
	{
		// Token: 0x06009301 RID: 37633 RVA: 0x00369385 File Offset: 0x00367585
		public StatesInstance(IStateMachineTarget master, FoodRehydratorSM.Def def)
			: base(master, def)
		{
		}

		// Token: 0x04007117 RID: 28951
		[MyCmpReq]
		public Operational operational;
	}
}
