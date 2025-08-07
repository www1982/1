using System;

// Token: 0x020004BB RID: 1211
public abstract class GameplayEvent<StateMachineInstanceType> : GameplayEvent where StateMachineInstanceType : StateMachine.Instance
{
	// Token: 0x060019E7 RID: 6631 RVA: 0x0008E5A3 File Offset: 0x0008C7A3
	public GameplayEvent(string id, int priority = 0, int importance = 0, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
		: base(id, priority, importance, requiredDlcIds, forbiddenDlcIds)
	{
	}
}
