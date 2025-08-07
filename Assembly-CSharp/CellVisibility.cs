using System;

// Token: 0x02000806 RID: 2054
public class CellVisibility
{
	// Token: 0x060037ED RID: 14317 RVA: 0x001365E0 File Offset: 0x001347E0
	public CellVisibility()
	{
		Grid.GetVisibleExtents(out this.MinX, out this.MinY, out this.MaxX, out this.MaxY);
	}

	// Token: 0x060037EE RID: 14318 RVA: 0x00136608 File Offset: 0x00134808
	public bool IsVisible(int cell)
	{
		int num = Grid.CellColumn(cell);
		if (num < this.MinX || num > this.MaxX)
		{
			return false;
		}
		int num2 = Grid.CellRow(cell);
		return num2 >= this.MinY && num2 <= this.MaxY;
	}

	// Token: 0x04002202 RID: 8706
	private int MinX;

	// Token: 0x04002203 RID: 8707
	private int MinY;

	// Token: 0x04002204 RID: 8708
	private int MaxX;

	// Token: 0x04002205 RID: 8709
	private int MaxY;
}
