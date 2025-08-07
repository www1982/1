using System;
using UnityEngine;

// Token: 0x0200056F RID: 1391
[SkipSaveFileSerialization]
[AddComponentMenu("KMonoBehaviour/scripts/Approachable")]
public class Approachable : KMonoBehaviour, IApproachable
{
	// Token: 0x06001F0A RID: 7946 RVA: 0x000B1D96 File Offset: 0x000AFF96
	public CellOffset[] GetOffsets()
	{
		return OffsetGroups.Use;
	}

	// Token: 0x06001F0B RID: 7947 RVA: 0x000B1D9D File Offset: 0x000AFF9D
	public int GetCell()
	{
		return Grid.PosToCell(this);
	}
}
