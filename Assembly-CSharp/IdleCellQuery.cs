using System;

// Token: 0x020004E0 RID: 1248
public class IdleCellQuery : PathFinderQuery
{
	// Token: 0x06001ABB RID: 6843 RVA: 0x000935F7 File Offset: 0x000917F7
	public IdleCellQuery Reset(MinionBrain brain, int max_cost)
	{
		this.brain = brain;
		this.maxCost = max_cost;
		this.targetCell = Grid.InvalidCell;
		return this;
	}

	// Token: 0x06001ABC RID: 6844 RVA: 0x00093614 File Offset: 0x00091814
	public override bool IsMatch(int cell, int parent_cell, int cost)
	{
		SafeCellQuery.SafeFlags flags = SafeCellQuery.GetFlags(cell, this.brain, false, (SafeCellQuery.SafeFlags)0);
		if ((flags & SafeCellQuery.SafeFlags.IsClear) != (SafeCellQuery.SafeFlags)0 && (flags & SafeCellQuery.SafeFlags.IsNotLadder) != (SafeCellQuery.SafeFlags)0 && (flags & SafeCellQuery.SafeFlags.IsNotTube) != (SafeCellQuery.SafeFlags)0 && (flags & SafeCellQuery.SafeFlags.IsBreathable) != (SafeCellQuery.SafeFlags)0 && (flags & SafeCellQuery.SafeFlags.IsNotLiquid) != (SafeCellQuery.SafeFlags)0)
		{
			this.targetCell = cell;
		}
		return cost > this.maxCost;
	}

	// Token: 0x06001ABD RID: 6845 RVA: 0x0009365E File Offset: 0x0009185E
	public override int GetResultCell()
	{
		return this.targetCell;
	}

	// Token: 0x04000F95 RID: 3989
	private MinionBrain brain;

	// Token: 0x04000F96 RID: 3990
	private int targetCell;

	// Token: 0x04000F97 RID: 3991
	private int maxCost;
}
