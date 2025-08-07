using System;

// Token: 0x020004E6 RID: 1254
public class SafetyQuery : PathFinderQuery
{
	// Token: 0x06001AD1 RID: 6865 RVA: 0x00093E97 File Offset: 0x00092097
	public SafetyQuery(SafetyChecker checker, KMonoBehaviour cmp, int max_cost)
	{
		this.checker = checker;
		this.cmp = cmp;
		this.maxCost = max_cost;
	}

	// Token: 0x06001AD2 RID: 6866 RVA: 0x00093EB4 File Offset: 0x000920B4
	public void Reset()
	{
		this.targetCell = PathFinder.InvalidCell;
		this.targetCost = int.MaxValue;
		this.targetConditions = 0;
		this.context = new SafetyChecker.Context(this.cmp);
	}

	// Token: 0x06001AD3 RID: 6867 RVA: 0x00093EE4 File Offset: 0x000920E4
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		bool flag = false;
		int safetyConditions = this.checker.GetSafetyConditions(cell, cost, this.context, out flag);
		if (safetyConditions != 0 && (safetyConditions > this.targetConditions || (safetyConditions == this.targetConditions && cost < this.targetCost)))
		{
			this.targetCell = cell;
			this.targetConditions = safetyConditions;
			this.targetCost = cost;
			if (flag)
			{
				return true;
			}
		}
		return cost >= this.maxCost;
	}

	// Token: 0x06001AD4 RID: 6868 RVA: 0x00093F4D File Offset: 0x0009214D
	public override int GetResultCell()
	{
		return this.targetCell;
	}

	// Token: 0x04000FB9 RID: 4025
	private int targetCell;

	// Token: 0x04000FBA RID: 4026
	private int targetCost;

	// Token: 0x04000FBB RID: 4027
	private int targetConditions;

	// Token: 0x04000FBC RID: 4028
	private int maxCost;

	// Token: 0x04000FBD RID: 4029
	private SafetyChecker checker;

	// Token: 0x04000FBE RID: 4030
	private KMonoBehaviour cmp;

	// Token: 0x04000FBF RID: 4031
	private SafetyChecker.Context context;
}
