using System;
using UnityEngine;

// Token: 0x0200057F RID: 1407
[AddComponentMenu("KMonoBehaviour/scripts/Chattable")]
public class Chattable : KMonoBehaviour, IApproachable
{
	// Token: 0x06001FDD RID: 8157 RVA: 0x000B70A1 File Offset: 0x000B52A1
	public CellOffset[] GetOffsets()
	{
		return OffsetGroups.Chat;
	}

	// Token: 0x06001FDE RID: 8158 RVA: 0x000B70A8 File Offset: 0x000B52A8
	public int GetCell()
	{
		return Grid.PosToCell(this);
	}
}
