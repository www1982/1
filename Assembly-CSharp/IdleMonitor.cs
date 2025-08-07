using System;

// Token: 0x020009F5 RID: 2549
public class IdleMonitor : GameStateMachine<IdleMonitor, IdleMonitor.Instance>
{
	// Token: 0x06004A65 RID: 19045 RVA: 0x001AF160 File Offset: 0x001AD360
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.idle;
		this.idle.TagTransition(GameTags.Dying, this.stopped, false).ToggleRecurringChore(new Func<IdleMonitor.Instance, Chore>(this.CreateIdleChore), null);
		this.stopped.DoNothing();
	}

	// Token: 0x06004A66 RID: 19046 RVA: 0x001AF1A0 File Offset: 0x001AD3A0
	private Chore CreateIdleChore(IdleMonitor.Instance smi)
	{
		return new IdleChore(smi.master);
	}

	// Token: 0x04003121 RID: 12577
	public GameStateMachine<IdleMonitor, IdleMonitor.Instance, IStateMachineTarget, object>.State idle;

	// Token: 0x04003122 RID: 12578
	public GameStateMachine<IdleMonitor, IdleMonitor.Instance, IStateMachineTarget, object>.State stopped;

	// Token: 0x02001A62 RID: 6754
	public new class Instance : GameStateMachine<IdleMonitor, IdleMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A35D RID: 41821 RVA: 0x003A3D8A File Offset: 0x003A1F8A
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}
	}
}
