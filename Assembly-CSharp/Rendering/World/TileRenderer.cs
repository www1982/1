using System;
using System.Collections.Generic;

namespace Rendering.World
{
	// Token: 0x02000EAB RID: 3755
	public abstract class TileRenderer : KMonoBehaviour
	{
		// Token: 0x06007821 RID: 30753 RVA: 0x002E8ED4 File Offset: 0x002E70D4
		protected override void OnSpawn()
		{
			this.Masks = this.GetMasks();
			this.TileGridWidth = Grid.WidthInCells + 1;
			this.TileGridHeight = Grid.HeightInCells + 1;
			this.BrushGrid = new int[this.TileGridWidth * this.TileGridHeight * 4];
			for (int i = 0; i < this.BrushGrid.Length; i++)
			{
				this.BrushGrid[i] = -1;
			}
			this.TileGrid = new Tile[this.TileGridWidth * this.TileGridHeight];
			for (int j = 0; j < this.TileGrid.Length; j++)
			{
				int num = j % this.TileGridWidth;
				int num2 = j / this.TileGridWidth;
				this.TileGrid[j] = new Tile(j, num, num2, this.Masks.Length);
			}
			this.LoadBrushes();
			this.VisibleAreaUpdater = new VisibleAreaUpdater(new Action<int>(this.UpdateOutsideView), new Action<int>(this.UpdateInsideView), "TileRenderer");
		}

		// Token: 0x06007822 RID: 30754 RVA: 0x002E8FC4 File Offset: 0x002E71C4
		protected virtual Mask[] GetMasks()
		{
			return new Mask[]
			{
				new Mask(this.Atlas, 0, false, false, false, false),
				new Mask(this.Atlas, 2, false, false, true, false),
				new Mask(this.Atlas, 2, false, true, true, false),
				new Mask(this.Atlas, 1, false, false, true, false),
				new Mask(this.Atlas, 2, false, false, false, false),
				new Mask(this.Atlas, 1, true, false, false, false),
				new Mask(this.Atlas, 3, false, false, false, false),
				new Mask(this.Atlas, 4, false, false, true, false),
				new Mask(this.Atlas, 2, false, true, false, false),
				new Mask(this.Atlas, 3, true, false, false, false),
				new Mask(this.Atlas, 1, true, false, true, false),
				new Mask(this.Atlas, 4, false, true, true, false),
				new Mask(this.Atlas, 1, false, false, false, false),
				new Mask(this.Atlas, 4, false, false, false, false),
				new Mask(this.Atlas, 4, false, true, false, false),
				new Mask(this.Atlas, 0, false, false, false, true)
			};
		}

		// Token: 0x06007823 RID: 30755 RVA: 0x002E9150 File Offset: 0x002E7350
		private void UpdateInsideView(int cell)
		{
			foreach (int num in this.GetCellTiles(cell))
			{
				this.ClearTiles.Add(num);
				this.DirtyTiles.Add(num);
			}
		}

		// Token: 0x06007824 RID: 30756 RVA: 0x002E9194 File Offset: 0x002E7394
		private void UpdateOutsideView(int cell)
		{
			foreach (int num in this.GetCellTiles(cell))
			{
				this.ClearTiles.Add(num);
			}
		}

		// Token: 0x06007825 RID: 30757 RVA: 0x002E91C8 File Offset: 0x002E73C8
		private int[] GetCellTiles(int cell)
		{
			int num = 0;
			int num2 = 0;
			Grid.CellToXY(cell, out num, out num2);
			this.CellTiles[0] = num2 * this.TileGridWidth + num;
			this.CellTiles[1] = num2 * this.TileGridWidth + (num + 1);
			this.CellTiles[2] = (num2 + 1) * this.TileGridWidth + num;
			this.CellTiles[3] = (num2 + 1) * this.TileGridWidth + (num + 1);
			return this.CellTiles;
		}

		// Token: 0x06007826 RID: 30758
		public abstract void LoadBrushes();

		// Token: 0x06007827 RID: 30759 RVA: 0x002E9239 File Offset: 0x002E7439
		public void MarkDirty(int cell)
		{
			this.VisibleAreaUpdater.UpdateCell(cell);
		}

		// Token: 0x06007828 RID: 30760 RVA: 0x002E9248 File Offset: 0x002E7448
		private void LateUpdate()
		{
			foreach (int num in this.ClearTiles)
			{
				this.Clear(ref this.TileGrid[num], this.Brushes, this.BrushGrid);
			}
			this.ClearTiles.Clear();
			foreach (int num2 in this.DirtyTiles)
			{
				this.MarkDirty(ref this.TileGrid[num2], this.Brushes, this.BrushGrid);
			}
			this.DirtyTiles.Clear();
			this.VisibleAreaUpdater.Update();
			foreach (Brush brush in this.DirtyBrushes)
			{
				brush.Refresh();
			}
			this.DirtyBrushes.Clear();
			foreach (Brush brush2 in this.ActiveBrushes)
			{
				brush2.Render();
			}
		}

		// Token: 0x06007829 RID: 30761
		public abstract void MarkDirty(ref Tile tile, Brush[] brush_array, int[] brush_grid);

		// Token: 0x0600782A RID: 30762 RVA: 0x002E93B8 File Offset: 0x002E75B8
		public void Clear(ref Tile tile, Brush[] brush_array, int[] brush_grid)
		{
			for (int i = 0; i < 4; i++)
			{
				int num = tile.Idx * 4 + i;
				if (brush_grid[num] != -1)
				{
					brush_array[brush_grid[num]].Remove(tile.Idx);
				}
			}
		}

		// Token: 0x0400537F RID: 21375
		private Tile[] TileGrid;

		// Token: 0x04005380 RID: 21376
		private int[] BrushGrid;

		// Token: 0x04005381 RID: 21377
		protected int TileGridWidth;

		// Token: 0x04005382 RID: 21378
		protected int TileGridHeight;

		// Token: 0x04005383 RID: 21379
		private int[] CellTiles = new int[4];

		// Token: 0x04005384 RID: 21380
		protected Brush[] Brushes;

		// Token: 0x04005385 RID: 21381
		protected Mask[] Masks;

		// Token: 0x04005386 RID: 21382
		protected List<Brush> DirtyBrushes = new List<Brush>();

		// Token: 0x04005387 RID: 21383
		protected List<Brush> ActiveBrushes = new List<Brush>();

		// Token: 0x04005388 RID: 21384
		private VisibleAreaUpdater VisibleAreaUpdater;

		// Token: 0x04005389 RID: 21385
		private HashSet<int> ClearTiles = new HashSet<int>();

		// Token: 0x0400538A RID: 21386
		private HashSet<int> DirtyTiles = new HashSet<int>();

		// Token: 0x0400538B RID: 21387
		public TextureAtlas Atlas;
	}
}
