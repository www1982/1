using System;

namespace Klei
{
	// Token: 0x02000FC0 RID: 4032
	public class SimSaveFileStructure
	{
		// Token: 0x06007CA1 RID: 31905 RVA: 0x0031F759 File Offset: 0x0031D959
		public SimSaveFileStructure()
		{
			this.worldDetail = new WorldDetailSave();
		}

		// Token: 0x04005E1D RID: 24093
		public int WidthInCells;

		// Token: 0x04005E1E RID: 24094
		public int HeightInCells;

		// Token: 0x04005E1F RID: 24095
		public int x;

		// Token: 0x04005E20 RID: 24096
		public int y;

		// Token: 0x04005E21 RID: 24097
		public byte[] Sim;

		// Token: 0x04005E22 RID: 24098
		public WorldDetailSave worldDetail;
	}
}
