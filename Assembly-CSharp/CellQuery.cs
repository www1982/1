using System;

// Token: 0x020004DD RID: 1245
public class CellQuery : PathFinderQuery
{
	// Token: 0x06001AB1 RID: 6833 RVA: 0x00093451 File Offset: 0x00091651
	public CellQuery Reset(int target_cell)
	{
		this.targetCell = target_cell;
		return this;
	}

	// Token: 0x06001AB2 RID: 6834 RVA: 0x0009345B File Offset: 0x0009165B
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		return cell == this.targetCell;
	}

	// Token: 0x04000F91 RID: 3985
	private int targetCell;
}
