using System;

// Token: 0x02000BC8 RID: 3016
public interface IUtilityItem
{
	// Token: 0x1700068E RID: 1678
	// (get) Token: 0x06005A56 RID: 23126
	// (set) Token: 0x06005A57 RID: 23127
	UtilityConnections Connections { get; set; }

	// Token: 0x06005A58 RID: 23128
	void UpdateConnections(UtilityConnections Connections);

	// Token: 0x06005A59 RID: 23129
	int GetNetworkID();

	// Token: 0x06005A5A RID: 23130
	UtilityNetwork GetNetworkForDirection(Direction d);
}
