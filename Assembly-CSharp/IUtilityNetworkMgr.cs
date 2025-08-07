using System;
using System.Collections.Generic;

// Token: 0x02000BD0 RID: 3024
public interface IUtilityNetworkMgr
{
	// Token: 0x06005A81 RID: 23169
	bool CanAddConnection(UtilityConnections new_connection, int cell, bool is_physical_building, out string fail_reason);

	// Token: 0x06005A82 RID: 23170
	void AddConnection(UtilityConnections new_connection, int cell, bool is_physical_building);

	// Token: 0x06005A83 RID: 23171
	void StashVisualGrids();

	// Token: 0x06005A84 RID: 23172
	void UnstashVisualGrids();

	// Token: 0x06005A85 RID: 23173
	string GetVisualizerString(int cell);

	// Token: 0x06005A86 RID: 23174
	string GetVisualizerString(UtilityConnections connections);

	// Token: 0x06005A87 RID: 23175
	UtilityConnections GetConnections(int cell, bool is_physical_building);

	// Token: 0x06005A88 RID: 23176
	UtilityConnections GetDisplayConnections(int cell);

	// Token: 0x06005A89 RID: 23177
	void SetConnections(UtilityConnections connections, int cell, bool is_physical_building);

	// Token: 0x06005A8A RID: 23178
	void ClearCell(int cell, bool is_physical_building);

	// Token: 0x06005A8B RID: 23179
	void ForceRebuildNetworks();

	// Token: 0x06005A8C RID: 23180
	void AddToNetworks(int cell, object item, bool is_endpoint);

	// Token: 0x06005A8D RID: 23181
	void RemoveFromNetworks(int cell, object vent, bool is_endpoint);

	// Token: 0x06005A8E RID: 23182
	object GetEndpoint(int cell);

	// Token: 0x06005A8F RID: 23183
	UtilityNetwork GetNetworkForDirection(int cell, Direction direction);

	// Token: 0x06005A90 RID: 23184
	UtilityNetwork GetNetworkForCell(int cell);

	// Token: 0x06005A91 RID: 23185
	void AddNetworksRebuiltListener(Action<IList<UtilityNetwork>, ICollection<int>> listener);

	// Token: 0x06005A92 RID: 23186
	void RemoveNetworksRebuiltListener(Action<IList<UtilityNetwork>, ICollection<int>> listener);

	// Token: 0x06005A93 RID: 23187
	IList<UtilityNetwork> GetNetworks();
}
