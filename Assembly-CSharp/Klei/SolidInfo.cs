using System;

namespace Klei
{
	// Token: 0x02000FBA RID: 4026
	public struct SolidInfo
	{
		// Token: 0x06007C9A RID: 31898 RVA: 0x0031F640 File Offset: 0x0031D840
		public SolidInfo(int cellIdx, bool isSolid)
		{
			this.cellIdx = cellIdx;
			this.isSolid = isSolid;
		}

		// Token: 0x04005DFD RID: 24061
		public int cellIdx;

		// Token: 0x04005DFE RID: 24062
		public bool isSolid;
	}
}
