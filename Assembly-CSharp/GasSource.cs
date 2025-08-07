using System;
using KSerialization;

// Token: 0x020005BD RID: 1469
[SerializationConfig(MemberSerialization.OptIn)]
public class GasSource : SubstanceSource
{
	// Token: 0x060021E0 RID: 8672 RVA: 0x000C3D2C File Offset: 0x000C1F2C
	protected override CellOffset[] GetOffsetGroup()
	{
		return OffsetGroups.LiquidSource;
	}

	// Token: 0x060021E1 RID: 8673 RVA: 0x000C3D33 File Offset: 0x000C1F33
	protected override IChunkManager GetChunkManager()
	{
		return GasSourceManager.Instance;
	}

	// Token: 0x060021E2 RID: 8674 RVA: 0x000C3D3A File Offset: 0x000C1F3A
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
	}
}
