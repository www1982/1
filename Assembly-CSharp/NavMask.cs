using System;

// Token: 0x020004CE RID: 1230
public class NavMask
{
	// Token: 0x06001A63 RID: 6755 RVA: 0x00091F76 File Offset: 0x00090176
	public virtual bool IsTraversable(PathFinder.PotentialPath path, int from_cell, int cost, int transition_id, PathFinderAbilities abilities)
	{
		return true;
	}

	// Token: 0x06001A64 RID: 6756 RVA: 0x00091F79 File Offset: 0x00090179
	public virtual void ApplyTraversalToPath(ref PathFinder.PotentialPath path, int from_cell)
	{
	}
}
