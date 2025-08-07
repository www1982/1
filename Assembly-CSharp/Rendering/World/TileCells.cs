using System;

namespace Rendering.World
{
	// Token: 0x02000EA9 RID: 3753
	public struct TileCells
	{
		// Token: 0x0600781F RID: 30751 RVA: 0x002E8E14 File Offset: 0x002E7014
		public TileCells(int tile_x, int tile_y)
		{
			int num = Grid.WidthInCells - 1;
			int num2 = Grid.HeightInCells - 1;
			this.Cell0 = Grid.XYToCell(Math.Min(Math.Max(tile_x - 1, 0), num), Math.Min(Math.Max(tile_y - 1, 0), num2));
			this.Cell1 = Grid.XYToCell(Math.Min(tile_x, num), Math.Min(Math.Max(tile_y - 1, 0), num2));
			this.Cell2 = Grid.XYToCell(Math.Min(Math.Max(tile_x - 1, 0), num), Math.Min(tile_y, num2));
			this.Cell3 = Grid.XYToCell(Math.Min(tile_x, num), Math.Min(tile_y, num2));
		}

		// Token: 0x04005378 RID: 21368
		public int Cell0;

		// Token: 0x04005379 RID: 21369
		public int Cell1;

		// Token: 0x0400537A RID: 21370
		public int Cell2;

		// Token: 0x0400537B RID: 21371
		public int Cell3;
	}
}
