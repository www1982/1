using System;
using System.Collections.Generic;

// Token: 0x020004E2 RID: 1250
public class MineableCellQuery : PathFinderQuery
{
	// Token: 0x06001AC2 RID: 6850 RVA: 0x000936E4 File Offset: 0x000918E4
	public MineableCellQuery Reset(Tag element, int max_results)
	{
		this.element = element;
		this.max_results = max_results;
		this.result_cells.Clear();
		return this;
	}

	// Token: 0x06001AC3 RID: 6851 RVA: 0x00093700 File Offset: 0x00091900
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		if (!this.result_cells.Contains(cell) && this.CheckValidMineCell(this.element, cell))
		{
			this.result_cells.Add(cell);
		}
		return this.result_cells.Count >= this.max_results;
	}

	// Token: 0x06001AC4 RID: 6852 RVA: 0x0009374C File Offset: 0x0009194C
	private bool CheckValidMineCell(Tag element, int testCell)
	{
		if (!Grid.IsValidCell(testCell))
		{
			return false;
		}
		foreach (Direction direction in MineableCellQuery.DIRECTION_CHECKS)
		{
			int cellInDirection = Grid.GetCellInDirection(testCell, direction);
			if (Grid.IsValidCell(cellInDirection) && Grid.IsSolidCell(cellInDirection) && !Grid.Foundation[cellInDirection] && Grid.Element[cellInDirection].tag == element)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000F9B RID: 3995
	public List<int> result_cells = new List<int>();

	// Token: 0x04000F9C RID: 3996
	private Tag element;

	// Token: 0x04000F9D RID: 3997
	private int max_results;

	// Token: 0x04000F9E RID: 3998
	public static List<Direction> DIRECTION_CHECKS = new List<Direction>
	{
		Direction.Down,
		Direction.Right,
		Direction.Left,
		Direction.Up
	};
}
