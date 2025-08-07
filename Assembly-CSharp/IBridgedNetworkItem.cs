using System;
using System.Collections.Generic;

// Token: 0x020006F8 RID: 1784
public interface IBridgedNetworkItem
{
	// Token: 0x06002C9C RID: 11420
	void AddNetworks(ICollection<UtilityNetwork> networks);

	// Token: 0x06002C9D RID: 11421
	bool IsConnectedToNetworks(ICollection<UtilityNetwork> networks);

	// Token: 0x06002C9E RID: 11422
	int GetNetworkCell();
}
