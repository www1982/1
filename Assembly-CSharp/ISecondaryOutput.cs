using System;

// Token: 0x020006F6 RID: 1782
public interface ISecondaryOutput
{
	// Token: 0x06002C98 RID: 11416
	bool HasSecondaryConduitType(ConduitType type);

	// Token: 0x06002C99 RID: 11417
	CellOffset GetSecondaryConduitOffset(ConduitType type);
}
