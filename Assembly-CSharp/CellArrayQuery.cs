using System;

// Token: 0x020004DA RID: 1242
public class CellArrayQuery : PathFinderQuery
{
	// Token: 0x06001AA7 RID: 6823 RVA: 0x00093379 File Offset: 0x00091579
	public CellArrayQuery Reset(int[] target_cells)
	{
		this.targetCells = target_cells;
		return this;
	}

	// Token: 0x06001AA8 RID: 6824 RVA: 0x00093384 File Offset: 0x00091584
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		for (int i = 0; i < this.targetCells.Length; i++)
		{
			if (this.targetCells[i] == cell)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000F8D RID: 3981
	private int[] targetCells;
}
