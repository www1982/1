using System;
using UnityEngine;

// Token: 0x0200056E RID: 1390
public interface IApproachable
{
	// Token: 0x06001F07 RID: 7943
	CellOffset[] GetOffsets();

	// Token: 0x06001F08 RID: 7944
	int GetCell();

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x06001F09 RID: 7945
	Transform transform { get; }
}
