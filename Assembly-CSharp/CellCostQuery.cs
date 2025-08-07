using System;

// Token: 0x020004DB RID: 1243
public class CellCostQuery : PathFinderQuery
{
	// Token: 0x170000AA RID: 170
	// (get) Token: 0x06001AAA RID: 6826 RVA: 0x000933BA File Offset: 0x000915BA
	// (set) Token: 0x06001AAB RID: 6827 RVA: 0x000933C2 File Offset: 0x000915C2
	public int resultCost { get; private set; }

	// Token: 0x06001AAC RID: 6828 RVA: 0x000933CB File Offset: 0x000915CB
	public void Reset(int target_cell, int max_cost)
	{
		this.targetCell = target_cell;
		this.maxCost = max_cost;
		this.resultCost = -1;
	}

	// Token: 0x06001AAD RID: 6829 RVA: 0x000933E2 File Offset: 0x000915E2
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		if (cost > this.maxCost)
		{
			return true;
		}
		if (cell == this.targetCell)
		{
			this.resultCost = cost;
			return true;
		}
		return false;
	}

	// Token: 0x04000F8E RID: 3982
	private int targetCell;

	// Token: 0x04000F8F RID: 3983
	private int maxCost;
}
