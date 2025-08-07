using System;
using UnityEngine;

// Token: 0x020006FB RID: 1787
[AddComponentMenu("KMonoBehaviour/scripts/ConduitSecondaryInput")]
public class ConduitSecondaryInput : KMonoBehaviour, ISecondaryInput
{
	// Token: 0x06002CBD RID: 11453 RVA: 0x00101A6F File Offset: 0x000FFC6F
	public bool HasSecondaryConduitType(ConduitType type)
	{
		return this.portInfo.conduitType == type;
	}

	// Token: 0x06002CBE RID: 11454 RVA: 0x00101A7F File Offset: 0x000FFC7F
	public CellOffset GetSecondaryConduitOffset(ConduitType type)
	{
		if (this.portInfo.conduitType == type)
		{
			return this.portInfo.offset;
		}
		return CellOffset.none;
	}

	// Token: 0x04001A66 RID: 6758
	[SerializeField]
	public ConduitPortInfo portInfo;
}
