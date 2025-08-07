using System;

// Token: 0x02000101 RID: 257
public class MoltStatesChore : GameStateMachine<MoltStatesChore, MoltStatesChore.Instance, IStateMachineTarget, MoltStatesChore.Def>
{
	// Token: 0x0600049F RID: 1183 RVA: 0x00025BC4 File Offset: 0x00023DC4
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.molting;
		this.molting.PlayAnim((MoltStatesChore.Instance smi) => smi.def.moltAnimName, KAnim.PlayMode.Once).ScheduleGoTo(5f, this.complete).OnAnimQueueComplete(this.complete);
		this.complete.BehaviourComplete(GameTags.Creatures.ReadyToMolt, false);
	}

	// Token: 0x04000356 RID: 854
	public GameStateMachine<MoltStatesChore, MoltStatesChore.Instance, IStateMachineTarget, MoltStatesChore.Def>.State molting;

	// Token: 0x04000357 RID: 855
	public GameStateMachine<MoltStatesChore, MoltStatesChore.Instance, IStateMachineTarget, MoltStatesChore.Def>.State complete;

	// Token: 0x02001126 RID: 4390
	public class Def : StateMachine.BaseDef
	{
		// Token: 0x04006212 RID: 25106
		public string moltAnimName;
	}

	// Token: 0x02001127 RID: 4391
	public new class Instance : GameStateMachine<MoltStatesChore, MoltStatesChore.Instance, IStateMachineTarget, MoltStatesChore.Def>.GameInstance
	{
		// Token: 0x06008195 RID: 33173 RVA: 0x0032F41F File Offset: 0x0032D61F
		public Instance(Chore<MoltStatesChore.Instance> chore, MoltStatesChore.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.ReadyToMolt);
		}
	}
}
