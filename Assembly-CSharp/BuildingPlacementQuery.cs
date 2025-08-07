using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004D9 RID: 1241
public class BuildingPlacementQuery : PathFinderQuery
{
	// Token: 0x06001AA3 RID: 6819 RVA: 0x0009321D File Offset: 0x0009141D
	public BuildingPlacementQuery Reset(int max_results, GameObject toPlace)
	{
		this.max_results = max_results;
		this.toPlace = toPlace;
		this.cellOffsets = toPlace.GetComponent<OccupyArea>().OccupiedCellsOffsets;
		this.result_cells.Clear();
		return this;
	}

	// Token: 0x06001AA4 RID: 6820 RVA: 0x0009324A File Offset: 0x0009144A
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		if (!this.result_cells.Contains(cell) && this.CheckValidPlaceCell(cell))
		{
			this.result_cells.Add(cell);
		}
		return this.result_cells.Count >= this.max_results;
	}

	// Token: 0x06001AA5 RID: 6821 RVA: 0x00093288 File Offset: 0x00091488
	private bool CheckValidPlaceCell(int testCell)
	{
		if (!Grid.IsValidCell(testCell) || Grid.IsSolidCell(testCell) || Grid.ObjectLayers[1].ContainsKey(testCell))
		{
			return false;
		}
		bool flag = true;
		int widthInCells = this.toPlace.GetComponent<OccupyArea>().GetWidthInCells();
		int num = testCell;
		for (int i = 0; i < widthInCells; i++)
		{
			int cellInDirection = Grid.GetCellInDirection(num, Direction.Down);
			if (!Grid.IsValidCell(cellInDirection) || !Grid.IsSolidCell(cellInDirection))
			{
				flag = false;
				break;
			}
			num = Grid.GetCellInDirection(num, Direction.Right);
		}
		if (flag)
		{
			for (int j = 0; j < this.cellOffsets.Length; j++)
			{
				CellOffset cellOffset = this.cellOffsets[j];
				int num2 = Grid.OffsetCell(testCell, cellOffset);
				if (!Grid.IsValidCell(num2) || Grid.IsSolidCell(num2) || !Grid.IsValidBuildingCell(num2) || Grid.ObjectLayers[1].ContainsKey(num2))
				{
					flag = false;
					break;
				}
			}
		}
		return flag;
	}

	// Token: 0x04000F89 RID: 3977
	public List<int> result_cells = new List<int>();

	// Token: 0x04000F8A RID: 3978
	private int max_results;

	// Token: 0x04000F8B RID: 3979
	private GameObject toPlace;

	// Token: 0x04000F8C RID: 3980
	private CellOffset[] cellOffsets;
}
