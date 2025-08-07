using System;

// Token: 0x020009B2 RID: 2482
public interface ILogicEventReceiver : ILogicNetworkConnection
{
	// Token: 0x06004841 RID: 18497
	void ReceiveLogicEvent(int value);

	// Token: 0x06004842 RID: 18498
	int GetLogicCell();
}
