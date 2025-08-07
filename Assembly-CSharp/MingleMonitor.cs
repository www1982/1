using System;

// Token: 0x020009FB RID: 2555
public class MingleMonitor : GameStateMachine<MingleMonitor, MingleMonitor.Instance>
{
	// Token: 0x06004A7C RID: 19068 RVA: 0x001AFBC5 File Offset: 0x001ADDC5
	public override void InitializeStates(out StateMachine.BaseState default_state)
	{
		default_state = this.mingle;
		base.serializable = StateMachine.SerializeType.Never;
		this.mingle.ToggleRecurringChore(new Func<MingleMonitor.Instance, Chore>(this.CreateMingleChore), null);
	}

	// Token: 0x06004A7D RID: 19069 RVA: 0x001AFBEF File Offset: 0x001ADDEF
	private Chore CreateMingleChore(MingleMonitor.Instance smi)
	{
		return new MingleChore(smi.master);
	}

	// Token: 0x04003139 RID: 12601
	public GameStateMachine<MingleMonitor, MingleMonitor.Instance, IStateMachineTarget, object>.State mingle;

	// Token: 0x02001A72 RID: 6770
	public new class Instance : GameStateMachine<MingleMonitor, MingleMonitor.Instance, IStateMachineTarget, object>.GameInstance
	{
		// Token: 0x0600A39D RID: 41885 RVA: 0x003A461D File Offset: 0x003A281D
		public Instance(IStateMachineTarget master)
			: base(master)
		{
		}
	}
}
