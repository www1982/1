using System;

// Token: 0x020009B1 RID: 2481
public interface ILogicEventSender : ILogicNetworkConnection
{
	// Token: 0x0600483E RID: 18494
	void LogicTick();

	// Token: 0x0600483F RID: 18495
	int GetLogicCell();

	// Token: 0x06004840 RID: 18496
	int GetLogicValue();
}
