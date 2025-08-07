using System;

namespace Rendering.World
{
	// Token: 0x02000EAA RID: 3754
	public struct Tile
	{
		// Token: 0x06007820 RID: 30752 RVA: 0x002E8EB5 File Offset: 0x002E70B5
		public Tile(int idx, int tile_x, int tile_y, int mask_count)
		{
			this.Idx = idx;
			this.TileCells = new TileCells(tile_x, tile_y);
			this.MaskCount = mask_count;
		}

		// Token: 0x0400537C RID: 21372
		public int Idx;

		// Token: 0x0400537D RID: 21373
		public TileCells TileCells;

		// Token: 0x0400537E RID: 21374
		public int MaskCount;
	}
}
