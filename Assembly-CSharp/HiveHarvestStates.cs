using System;

// Token: 0x020000F4 RID: 244
public class HiveHarvestStates : GameStateMachine<HiveHarvestStates, HiveHarvestStates.Instance, IStateMachineTarget, HiveHarvestStates.Def>
{
	// Token: 0x06000466 RID: 1126 RVA: 0x00024513 File Offset: 0x00022713
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.root;
		this.root.DoNothing();
	}

	// Token: 0x02001102 RID: 4354
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001103 RID: 4355
	public new class Instance : GameStateMachine<HiveHarvestStates, HiveHarvestStates.Instance, IStateMachineTarget, HiveHarvestStates.Def>.GameInstance
	{
		// Token: 0x0600813C RID: 33084 RVA: 0x0032EA96 File Offset: 0x0032CC96
		public Instance(Chore<HiveHarvestStates.Instance> chore, HiveHarvestStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Creatures.Behaviours.HarvestHiveBehaviour);
		}
	}
}
