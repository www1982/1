using System;

// Token: 0x02000511 RID: 1297
public static class StateMachineExtensions
{
	// Token: 0x06001BBC RID: 7100 RVA: 0x00097538 File Offset: 0x00095738
	public static bool IsNullOrStopped(this StateMachine.Instance smi)
	{
		return smi == null || !smi.IsRunning();
	}
}
