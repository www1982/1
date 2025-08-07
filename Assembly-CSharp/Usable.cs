using System;

// Token: 0x020004B6 RID: 1206
public abstract class Usable : KMonoBehaviour, IStateMachineTarget
{
	// Token: 0x060019BA RID: 6586
	public abstract void StartUsing(User user);

	// Token: 0x060019BB RID: 6587 RVA: 0x0008DD38 File Offset: 0x0008BF38
	protected void StartUsing(StateMachine.Instance smi, User user)
	{
		DebugUtil.Assert(this.smi == null);
		DebugUtil.Assert(smi != null);
		this.smi = smi;
		smi.OnStop = (Action<string, StateMachine.Status>)Delegate.Combine(smi.OnStop, new Action<string, StateMachine.Status>(user.OnStateMachineStop));
		smi.StartSM();
	}

	// Token: 0x060019BC RID: 6588 RVA: 0x0008DD8C File Offset: 0x0008BF8C
	public void StopUsing(User user)
	{
		if (this.smi != null)
		{
			StateMachine.Instance instance = this.smi;
			instance.OnStop = (Action<string, StateMachine.Status>)Delegate.Remove(instance.OnStop, new Action<string, StateMachine.Status>(user.OnStateMachineStop));
			this.smi.StopSM("Usable.StopUsing");
			this.smi = null;
		}
	}

	// Token: 0x04000EC6 RID: 3782
	private StateMachine.Instance smi;
}
