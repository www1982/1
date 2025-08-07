using System;
using UnityEngine;

// Token: 0x02000946 RID: 2374
public struct GridArea
{
	// Token: 0x170004CD RID: 1229
	// (get) Token: 0x060043F2 RID: 17394 RVA: 0x001873CA File Offset: 0x001855CA
	public Vector2I Min
	{
		get
		{
			return this.min;
		}
	}

	// Token: 0x170004CE RID: 1230
	// (get) Token: 0x060043F3 RID: 17395 RVA: 0x001873D2 File Offset: 0x001855D2
	public Vector2I Max
	{
		get
		{
			return this.max;
		}
	}

	// Token: 0x060043F4 RID: 17396 RVA: 0x001873DC File Offset: 0x001855DC
	public void SetArea(int cell, int width, int height)
	{
		Vector2I vector2I = Grid.CellToXY(cell);
		Vector2I vector2I2 = new Vector2I(vector2I.x + width, vector2I.y + height);
		this.SetExtents(vector2I.x, vector2I.y, vector2I2.x, vector2I2.y);
	}

	// Token: 0x060043F5 RID: 17397 RVA: 0x00187428 File Offset: 0x00185628
	public void SetExtents(int min_x, int min_y, int max_x, int max_y)
	{
		this.min.x = Math.Max(min_x, 0);
		this.min.y = Math.Max(min_y, 0);
		this.max.x = Math.Min(max_x, Grid.WidthInCells);
		this.max.y = Math.Min(max_y, Grid.HeightInCells);
		this.MinCell = Grid.XYToCell(this.min.x, this.min.y);
		this.MaxCell = Grid.XYToCell(this.max.x, this.max.y);
	}

	// Token: 0x060043F6 RID: 17398 RVA: 0x001874C8 File Offset: 0x001856C8
	public bool Contains(int cell)
	{
		if (cell >= this.MinCell && cell < this.MaxCell)
		{
			int num = cell % Grid.WidthInCells;
			return num >= this.Min.x && num < this.Max.x;
		}
		return false;
	}

	// Token: 0x060043F7 RID: 17399 RVA: 0x0018750F File Offset: 0x0018570F
	public bool Contains(int x, int y)
	{
		return x >= this.min.x && x < this.max.x && y >= this.min.y && y < this.max.y;
	}

	// Token: 0x060043F8 RID: 17400 RVA: 0x0018754C File Offset: 0x0018574C
	public bool Contains(Vector3 pos)
	{
		return (float)this.min.x <= pos.x && pos.x < (float)this.max.x && (float)this.min.y <= pos.y && pos.y <= (float)this.max.y;
	}

	// Token: 0x060043F9 RID: 17401 RVA: 0x001875AE File Offset: 0x001857AE
	public void RunIfInside(int cell, Action<int> action)
	{
		if (this.Contains(cell))
		{
			action(cell);
		}
	}

	// Token: 0x060043FA RID: 17402 RVA: 0x001875C0 File Offset: 0x001857C0
	public void Run(Action<int> action)
	{
		for (int i = this.min.y; i < this.max.y; i++)
		{
			for (int j = this.min.x; j < this.max.x; j++)
			{
				int num = Grid.XYToCell(j, i);
				action(num);
			}
		}
	}

	// Token: 0x060043FB RID: 17403 RVA: 0x0018761C File Offset: 0x0018581C
	public void RunOnDifference(GridArea subtract_area, Action<int> action)
	{
		for (int i = this.min.y; i < this.max.y; i++)
		{
			for (int j = this.min.x; j < this.max.x; j++)
			{
				if (!subtract_area.Contains(j, i))
				{
					int num = Grid.XYToCell(j, i);
					action(num);
				}
			}
		}
	}

	// Token: 0x060043FC RID: 17404 RVA: 0x00187683 File Offset: 0x00185883
	public int GetCellCount()
	{
		return (this.max.x - this.min.x) * (this.max.y - this.min.y);
	}

	// Token: 0x04002D8D RID: 11661
	private Vector2I min;

	// Token: 0x04002D8E RID: 11662
	private Vector2I max;

	// Token: 0x04002D8F RID: 11663
	private int MinCell;

	// Token: 0x04002D90 RID: 11664
	private int MaxCell;
}
