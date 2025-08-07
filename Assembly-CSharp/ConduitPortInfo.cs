using System;

// Token: 0x020006FA RID: 1786
[Serializable]
public class ConduitPortInfo
{
	// Token: 0x06002CBC RID: 11452 RVA: 0x00101A59 File Offset: 0x000FFC59
	public ConduitPortInfo(ConduitType type, CellOffset offset)
	{
		this.conduitType = type;
		this.offset = offset;
	}

	// Token: 0x04001A64 RID: 6756
	public ConduitType conduitType;

	// Token: 0x04001A65 RID: 6757
	public CellOffset offset;
}
