using System;
using UnityEngine;

// Token: 0x02000AF1 RID: 2801
public struct Extents
{
	// Token: 0x06005228 RID: 21032 RVA: 0x001DEE8C File Offset: 0x001DD08C
	public static Extents OneCell(int cell)
	{
		int num;
		int num2;
		Grid.CellToXY(cell, out num, out num2);
		return new Extents(num, num2, 1, 1);
	}

	// Token: 0x06005229 RID: 21033 RVA: 0x001DEEAC File Offset: 0x001DD0AC
	public Extents(int x, int y, int width, int height)
	{
		this.x = x;
		this.y = y;
		this.width = width;
		this.height = height;
	}

	// Token: 0x0600522A RID: 21034 RVA: 0x001DEECC File Offset: 0x001DD0CC
	public Extents(int cell, int radius)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		this.x = num - radius;
		this.y = num2 - radius;
		this.width = radius * 2 + 1;
		this.height = radius * 2 + 1;
	}

	// Token: 0x0600522B RID: 21035 RVA: 0x001DEF0F File Offset: 0x001DD10F
	public Extents(int center_x, int center_y, int radius)
	{
		this.x = center_x - radius;
		this.y = center_y - radius;
		this.width = radius * 2 + 1;
		this.height = radius * 2 + 1;
	}

	// Token: 0x0600522C RID: 21036 RVA: 0x001DEF3C File Offset: 0x001DD13C
	public Extents(int cell, CellOffset[] offsets)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		int num3 = num;
		int num4 = num2;
		foreach (CellOffset cellOffset in offsets)
		{
			int num5 = 0;
			int num6 = 0;
			Grid.CellToXY(Grid.OffsetCell(cell, cellOffset), out num5, out num6);
			num = Math.Min(num, num5);
			num2 = Math.Min(num2, num6);
			num3 = Math.Max(num3, num5);
			num4 = Math.Max(num4, num6);
		}
		this.x = num;
		this.y = num2;
		this.width = num3 - num + 1;
		this.height = num4 - num2 + 1;
	}

	// Token: 0x0600522D RID: 21037 RVA: 0x001DEFD8 File Offset: 0x001DD1D8
	public Extents(int cell, CellOffset[] offsets, Extents.BoundExtendsToGridFlag _)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		int num3 = num;
		int num4 = num2;
		foreach (CellOffset cellOffset in offsets)
		{
			int num5 = 0;
			int num6 = 0;
			int num7 = Grid.OffsetCell(cell, cellOffset);
			if (Grid.IsValidCell(num7))
			{
				Grid.CellToXY(num7, out num5, out num6);
				num = Math.Min(num, num5);
				num2 = Math.Min(num2, num6);
				num3 = Math.Max(num3, num5);
				num4 = Math.Max(num4, num6);
			}
		}
		this.x = num;
		this.y = num2;
		this.width = num3 - num + 1;
		this.height = num4 - num2 + 1;
	}

	// Token: 0x0600522E RID: 21038 RVA: 0x001DF080 File Offset: 0x001DD280
	public Extents(int cell, CellOffset[] offsets, Orientation orientation)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		int num3 = num;
		int num4 = num2;
		for (int i = 0; i < offsets.Length; i++)
		{
			CellOffset rotatedCellOffset = Rotatable.GetRotatedCellOffset(offsets[i], orientation);
			int num5 = 0;
			int num6 = 0;
			Grid.CellToXY(Grid.OffsetCell(cell, rotatedCellOffset), out num5, out num6);
			num = Math.Min(num, num5);
			num2 = Math.Min(num2, num6);
			num3 = Math.Max(num3, num5);
			num4 = Math.Max(num4, num6);
		}
		this.x = num;
		this.y = num2;
		this.width = num3 - num + 1;
		this.height = num4 - num2 + 1;
	}

	// Token: 0x0600522F RID: 21039 RVA: 0x001DF120 File Offset: 0x001DD320
	public Extents(int cell, CellOffset[][] offset_table)
	{
		int num = 0;
		int num2 = 0;
		Grid.CellToXY(cell, out num, out num2);
		int num3 = num;
		int num4 = num2;
		foreach (CellOffset[] array in offset_table)
		{
			int num5 = 0;
			int num6 = 0;
			Grid.CellToXY(Grid.OffsetCell(cell, array[0]), out num5, out num6);
			num = Math.Min(num, num5);
			num2 = Math.Min(num2, num6);
			num3 = Math.Max(num3, num5);
			num4 = Math.Max(num4, num6);
		}
		this.x = num;
		this.y = num2;
		this.width = num3 - num + 1;
		this.height = num4 - num2 + 1;
	}

	// Token: 0x06005230 RID: 21040 RVA: 0x001DF1BC File Offset: 0x001DD3BC
	public bool Contains(Vector2I pos)
	{
		return this.x <= pos.x && pos.x < this.x + this.width && this.y <= pos.y && pos.y < this.y + this.height;
	}

	// Token: 0x06005231 RID: 21041 RVA: 0x001DF214 File Offset: 0x001DD414
	public bool Contains(Vector3 pos)
	{
		return (float)this.x <= pos.x && pos.x < (float)(this.x + this.width) && (float)this.y <= pos.y && pos.y < (float)(this.y + this.height);
	}

	// Token: 0x04003740 RID: 14144
	public int x;

	// Token: 0x04003741 RID: 14145
	public int y;

	// Token: 0x04003742 RID: 14146
	public int width;

	// Token: 0x04003743 RID: 14147
	public int height;

	// Token: 0x04003744 RID: 14148
	public static Extents.BoundExtendsToGridFlag BoundsCheckCoords;

	// Token: 0x02001BFA RID: 7162
	public struct BoundExtendsToGridFlag
	{
	}
}
