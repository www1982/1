using System;

// Token: 0x020006F7 RID: 1783
public interface ISecondaryInput
{
	// Token: 0x06002C9A RID: 11418
	bool HasSecondaryConduitType(ConduitType type);

	// Token: 0x06002C9B RID: 11419
	CellOffset GetSecondaryConduitOffset(ConduitType type);
}
