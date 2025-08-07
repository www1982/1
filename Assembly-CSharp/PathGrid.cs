using System;
using System.Collections.Generic;

// Token: 0x020004D7 RID: 1239
public class PathGrid
{
	// Token: 0x06001A8D RID: 6797 RVA: 0x00092B5C File Offset: 0x00090D5C
	public void SetGroupProber(IGroupProber group_prober)
	{
		this.groupProber = group_prober;
	}

	// Token: 0x06001A8E RID: 6798 RVA: 0x00092B68 File Offset: 0x00090D68
	public PathGrid(int width_in_cells, int height_in_cells, bool apply_offset, NavType[] valid_nav_types)
	{
		this.applyOffset = apply_offset;
		this.widthInCells = width_in_cells;
		this.heightInCells = height_in_cells;
		this.ValidNavTypes = valid_nav_types;
		int num = 0;
		this.NavTypeTable = new int[11];
		for (int i = 0; i < this.NavTypeTable.Length; i++)
		{
			this.NavTypeTable[i] = -1;
			for (int j = 0; j < this.ValidNavTypes.Length; j++)
			{
				if (this.ValidNavTypes[j] == (NavType)i)
				{
					this.NavTypeTable[i] = num++;
					break;
				}
			}
		}
		DebugUtil.DevAssert(true, "Cell packs nav type into 4 bits!", null);
		this.Cells = new PathFinder.Cell[width_in_cells * height_in_cells * this.ValidNavTypes.Length];
		this.ProberCells = new PathGrid.ProberCell[width_in_cells * height_in_cells];
		this.serialNo = 0;
		this.previousSerialNo = -1;
		this.isUpdating = false;
	}

	// Token: 0x06001A8F RID: 6799 RVA: 0x00092C42 File Offset: 0x00090E42
	public void OnCleanUp()
	{
		if (this.groupProber != null)
		{
			this.groupProber.ReleaseProber(this);
		}
	}

	// Token: 0x06001A90 RID: 6800 RVA: 0x00092C59 File Offset: 0x00090E59
	public void ResetUpdate()
	{
		this.previousSerialNo = -1;
	}

	// Token: 0x06001A91 RID: 6801 RVA: 0x00092C64 File Offset: 0x00090E64
	public void BeginUpdate(int root_cell, bool isContinuation)
	{
		this.isUpdating = true;
		this.freshlyOccupiedCells.Clear();
		if (isContinuation)
		{
			return;
		}
		if (this.applyOffset)
		{
			Grid.CellToXY(root_cell, out this.rootX, out this.rootY);
			this.rootX -= this.widthInCells / 2;
			this.rootY -= this.heightInCells / 2;
		}
		this.serialNo += 1;
		if (this.groupProber != null)
		{
			this.groupProber.SetValidSerialNos(this, this.previousSerialNo, this.serialNo);
		}
	}

	// Token: 0x06001A92 RID: 6802 RVA: 0x00092CFC File Offset: 0x00090EFC
	public void EndUpdate(bool isComplete)
	{
		this.isUpdating = false;
		if (this.groupProber != null)
		{
			this.groupProber.Occupy(this, this.serialNo, this.freshlyOccupiedCells);
		}
		if (!isComplete)
		{
			return;
		}
		if (this.groupProber != null)
		{
			this.groupProber.SetValidSerialNos(this, this.serialNo, this.serialNo);
		}
		this.previousSerialNo = this.serialNo;
	}

	// Token: 0x06001A93 RID: 6803 RVA: 0x00092D60 File Offset: 0x00090F60
	private bool IsValidSerialNo(short serialNo)
	{
		return serialNo == this.serialNo || (!this.isUpdating && this.previousSerialNo != -1 && serialNo == this.previousSerialNo);
	}

	// Token: 0x06001A94 RID: 6804 RVA: 0x00092D89 File Offset: 0x00090F89
	public PathFinder.Cell GetCell(PathFinder.PotentialPath potential_path, out bool is_cell_in_range)
	{
		return this.GetCell(potential_path.cell, potential_path.navType, out is_cell_in_range);
	}

	// Token: 0x06001A95 RID: 6805 RVA: 0x00092DA0 File Offset: 0x00090FA0
	public PathFinder.Cell GetCell(int cell, NavType nav_type, out bool is_cell_in_range)
	{
		int num = this.OffsetCell(cell);
		is_cell_in_range = -1 != num;
		if (!is_cell_in_range)
		{
			return PathGrid.InvalidCell;
		}
		PathFinder.Cell cell2 = this.Cells[num * this.ValidNavTypes.Length + this.NavTypeTable[(int)nav_type]];
		if (!this.IsValidSerialNo(cell2.queryId))
		{
			return PathGrid.InvalidCell;
		}
		return cell2;
	}

	// Token: 0x06001A96 RID: 6806 RVA: 0x00092DFC File Offset: 0x00090FFC
	public void SetCell(PathFinder.PotentialPath potential_path, ref PathFinder.Cell cell_data)
	{
		int num = this.OffsetCell(potential_path.cell);
		if (-1 == num)
		{
			return;
		}
		cell_data.queryId = this.serialNo;
		int num2 = this.NavTypeTable[(int)potential_path.navType];
		int num3 = num * this.ValidNavTypes.Length + num2;
		this.Cells[num3] = cell_data;
		if (potential_path.navType != NavType.Tube)
		{
			PathGrid.ProberCell proberCell = this.ProberCells[num];
			if (cell_data.queryId != proberCell.queryId || cell_data.cost < proberCell.cost)
			{
				proberCell.queryId = cell_data.queryId;
				proberCell.cost = cell_data.cost;
				this.ProberCells[num] = proberCell;
				this.freshlyOccupiedCells.Add(potential_path.cell);
			}
		}
	}

	// Token: 0x06001A97 RID: 6807 RVA: 0x00092EC0 File Offset: 0x000910C0
	public int GetCostIgnoreProberOffset(int cell, CellOffset[] offsets)
	{
		int num = -1;
		foreach (CellOffset cellOffset in offsets)
		{
			int num2 = Grid.OffsetCell(cell, cellOffset);
			if (Grid.IsValidCell(num2))
			{
				PathGrid.ProberCell proberCell = this.ProberCells[num2];
				if (this.IsValidSerialNo(proberCell.queryId) && (num == -1 || proberCell.cost < num))
				{
					num = proberCell.cost;
				}
			}
		}
		return num;
	}

	// Token: 0x06001A98 RID: 6808 RVA: 0x00092F30 File Offset: 0x00091130
	public int GetCost(int cell)
	{
		int num = this.OffsetCell(cell);
		if (-1 == num)
		{
			return -1;
		}
		PathGrid.ProberCell proberCell = this.ProberCells[num];
		if (!this.IsValidSerialNo(proberCell.queryId))
		{
			return -1;
		}
		return proberCell.cost;
	}

	// Token: 0x06001A99 RID: 6809 RVA: 0x00092F70 File Offset: 0x00091170
	private int OffsetCell(int cell)
	{
		if (!this.applyOffset)
		{
			return cell;
		}
		int num;
		int num2;
		Grid.CellToXY(cell, out num, out num2);
		if (num < this.rootX || num >= this.rootX + this.widthInCells || num2 < this.rootY || num2 >= this.rootY + this.heightInCells)
		{
			return -1;
		}
		int num3 = num - this.rootX;
		return (num2 - this.rootY) * this.widthInCells + num3;
	}

	// Token: 0x04000F70 RID: 3952
	private PathFinder.Cell[] Cells;

	// Token: 0x04000F71 RID: 3953
	private PathGrid.ProberCell[] ProberCells;

	// Token: 0x04000F72 RID: 3954
	private List<int> freshlyOccupiedCells = new List<int>();

	// Token: 0x04000F73 RID: 3955
	private NavType[] ValidNavTypes;

	// Token: 0x04000F74 RID: 3956
	private int[] NavTypeTable;

	// Token: 0x04000F75 RID: 3957
	private int widthInCells;

	// Token: 0x04000F76 RID: 3958
	private int heightInCells;

	// Token: 0x04000F77 RID: 3959
	private bool applyOffset;

	// Token: 0x04000F78 RID: 3960
	private int rootX;

	// Token: 0x04000F79 RID: 3961
	private int rootY;

	// Token: 0x04000F7A RID: 3962
	private short serialNo;

	// Token: 0x04000F7B RID: 3963
	private short previousSerialNo;

	// Token: 0x04000F7C RID: 3964
	private bool isUpdating;

	// Token: 0x04000F7D RID: 3965
	private IGroupProber groupProber;

	// Token: 0x04000F7E RID: 3966
	public static readonly PathFinder.Cell InvalidCell = new PathFinder.Cell
	{
		cost = -1
	};

	// Token: 0x02001334 RID: 4916
	private struct ProberCell
	{
		// Token: 0x040068B6 RID: 26806
		public int cost;

		// Token: 0x040068B7 RID: 26807
		public short queryId;
	}
}
