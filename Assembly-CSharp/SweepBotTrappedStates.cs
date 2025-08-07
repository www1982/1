using System;

// Token: 0x020003C5 RID: 965
public class SweepBotTrappedStates : GameStateMachine<SweepBotTrappedStates, SweepBotTrappedStates.Instance, IStateMachineTarget, SweepBotTrappedStates.Def>
{
	// Token: 0x060013AA RID: 5034 RVA: 0x00070018 File Offset: 0x0006E218
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.blockedStates.evaluating;
		this.blockedStates.ToggleStatusItem(Db.Get().RobotStatusItems.CantReachStation, (SweepBotTrappedStates.Instance smi) => smi.gameObject, Db.Get().StatusItemCategories.Main).TagTransition(GameTags.Robots.Behaviours.TrappedBehaviour, this.behaviourcomplete, true);
		this.blockedStates.evaluating.Enter(delegate(SweepBotTrappedStates.Instance smi)
		{
			if (smi.sm.GetSweepLocker(smi) == null)
			{
				smi.GoTo(this.blockedStates.noHome);
				return;
			}
			smi.GoTo(this.blockedStates.blocked);
		});
		this.blockedStates.blocked.ToggleChore((SweepBotTrappedStates.Instance smi) => new RescueSweepBotChore(smi.master, smi.master.gameObject, smi.sm.GetSweepLocker(smi).gameObject), this.behaviourcomplete, this.blockedStates.evaluating).PlayAnim("react_stuck", KAnim.PlayMode.Loop);
		this.blockedStates.noHome.PlayAnim("react_stuck", KAnim.PlayMode.Once).OnAnimQueueComplete(this.blockedStates.evaluating);
		this.behaviourcomplete.BehaviourComplete(GameTags.Robots.Behaviours.TrappedBehaviour, false);
	}

	// Token: 0x060013AB RID: 5035 RVA: 0x00070130 File Offset: 0x0006E330
	public Storage GetSweepLocker(SweepBotTrappedStates.Instance smi)
	{
		StorageUnloadMonitor.Instance smi2 = smi.master.gameObject.GetSMI<StorageUnloadMonitor.Instance>();
		if (smi2 == null)
		{
			return null;
		}
		return smi2.sm.sweepLocker.Get(smi2);
	}

	// Token: 0x04000BE5 RID: 3045
	public SweepBotTrappedStates.BlockedStates blockedStates;

	// Token: 0x04000BE6 RID: 3046
	public GameStateMachine<SweepBotTrappedStates, SweepBotTrappedStates.Instance, IStateMachineTarget, SweepBotTrappedStates.Def>.State behaviourcomplete;

	// Token: 0x02001201 RID: 4609
	public class Def : StateMachine.BaseDef
	{
	}

	// Token: 0x02001202 RID: 4610
	public new class Instance : GameStateMachine<SweepBotTrappedStates, SweepBotTrappedStates.Instance, IStateMachineTarget, SweepBotTrappedStates.Def>.GameInstance
	{
		// Token: 0x060084A5 RID: 33957 RVA: 0x003364E2 File Offset: 0x003346E2
		public Instance(Chore<SweepBotTrappedStates.Instance> chore, SweepBotTrappedStates.Def def)
			: base(chore, def)
		{
			chore.AddPrecondition(ChorePreconditions.instance.CheckBehaviourPrecondition, GameTags.Robots.Behaviours.TrappedBehaviour);
		}
	}

	// Token: 0x02001203 RID: 4611
	public class BlockedStates : GameStateMachine<SweepBotTrappedStates, SweepBotTrappedStates.Instance, IStateMachineTarget, SweepBotTrappedStates.Def>.State
	{
		// Token: 0x040064D7 RID: 25815
		public GameStateMachine<SweepBotTrappedStates, SweepBotTrappedStates.Instance, IStateMachineTarget, SweepBotTrappedStates.Def>.State evaluating;

		// Token: 0x040064D8 RID: 25816
		public GameStateMachine<SweepBotTrappedStates, SweepBotTrappedStates.Instance, IStateMachineTarget, SweepBotTrappedStates.Def>.State blocked;

		// Token: 0x040064D9 RID: 25817
		public GameStateMachine<SweepBotTrappedStates, SweepBotTrappedStates.Instance, IStateMachineTarget, SweepBotTrappedStates.Def>.State noHome;
	}
}
