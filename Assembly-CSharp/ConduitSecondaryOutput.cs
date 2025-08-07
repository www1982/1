using System;
using UnityEngine;

// Token: 0x020006FC RID: 1788
[AddComponentMenu("KMonoBehaviour/scripts/ConduitSecondaryOutput")]
public class ConduitSecondaryOutput : KMonoBehaviour, ISecondaryOutput
{
	// Token: 0x06002CC0 RID: 11456 RVA: 0x00101AA8 File Offset: 0x000FFCA8
	public bool HasSecondaryConduitType(ConduitType type)
	{
		return this.portInfo.conduitType == type;
	}

	// Token: 0x06002CC1 RID: 11457 RVA: 0x00101AB8 File Offset: 0x000FFCB8
	public CellOffset GetSecondaryConduitOffset(ConduitType type)
	{
		if (type == this.portInfo.conduitType)
		{
			return this.portInfo.offset;
		}
		return CellOffset.none;
	}

	// Token: 0x04001A67 RID: 6759
	[SerializeField]
	public ConduitPortInfo portInfo;
}
