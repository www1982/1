using System;

// Token: 0x020009A8 RID: 2472
public class LiquidFetchMask
{
	// Token: 0x060047D0 RID: 18384 RVA: 0x0019E394 File Offset: 0x0019C594
	public LiquidFetchMask(CellOffset[][] offset_table)
	{
		for (int i = 0; i < offset_table.Length; i++)
		{
			for (int j = 0; j < offset_table[i].Length; j++)
			{
				this.maxOffset.x = Math.Max(this.maxOffset.x, Math.Abs(offset_table[i][j].x));
				this.maxOffset.y = Math.Max(this.maxOffset.y, Math.Abs(offset_table[i][j].y));
			}
		}
		this.isLiquidAvailable = new bool[Grid.CellCount];
		for (int k = 0; k < Grid.CellCount; k++)
		{
			this.RefreshCell(k);
		}
	}

	// Token: 0x060047D1 RID: 18385 RVA: 0x0019E448 File Offset: 0x0019C648
	private void RefreshCell(int cell)
	{
		CellOffset offset = Grid.GetOffset(cell);
		int num = Math.Max(0, offset.y - this.maxOffset.y);
		while (num < Grid.HeightInCells && num < offset.y + this.maxOffset.y)
		{
			int num2 = Math.Max(0, offset.x - this.maxOffset.x);
			while (num2 < Grid.WidthInCells && num2 < offset.x + this.maxOffset.x)
			{
				if (Grid.Element[Grid.XYToCell(num2, num)].IsLiquid)
				{
					this.isLiquidAvailable[cell] = true;
					return;
				}
				num2++;
			}
			num++;
		}
		this.isLiquidAvailable[cell] = false;
	}

	// Token: 0x060047D2 RID: 18386 RVA: 0x0019E4FB File Offset: 0x0019C6FB
	public void MarkDirty(int cell)
	{
		this.RefreshCell(cell);
	}

	// Token: 0x060047D3 RID: 18387 RVA: 0x0019E504 File Offset: 0x0019C704
	public bool IsLiquidAvailable(int cell)
	{
		return this.isLiquidAvailable[cell];
	}

	// Token: 0x060047D4 RID: 18388 RVA: 0x0019E50E File Offset: 0x0019C70E
	public void Destroy()
	{
		this.isLiquidAvailable = null;
	}

	// Token: 0x04002F91 RID: 12177
	private bool[] isLiquidAvailable;

	// Token: 0x04002F92 RID: 12178
	private CellOffset maxOffset;
}
