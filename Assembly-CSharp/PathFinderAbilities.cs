using System;

// Token: 0x020004D5 RID: 1237
public abstract class PathFinderAbilities
{
	// Token: 0x06001A82 RID: 6786 RVA: 0x00092AED File Offset: 0x00090CED
	public PathFinderAbilities(Navigator navigator)
	{
		this.navigator = navigator;
	}

	// Token: 0x06001A83 RID: 6787 RVA: 0x00092AFC File Offset: 0x00090CFC
	public void Refresh()
	{
		this.prefabInstanceID = this.navigator.gameObject.GetComponent<KPrefabID>().InstanceID;
		this.Refresh(this.navigator);
	}

	// Token: 0x06001A84 RID: 6788
	protected abstract void Refresh(Navigator navigator);

	// Token: 0x06001A85 RID: 6789
	public abstract bool TraversePath(ref PathFinder.PotentialPath path, int from_cell, NavType from_nav_type, int cost, int transition_id, bool submerged);

	// Token: 0x06001A86 RID: 6790 RVA: 0x00092B25 File Offset: 0x00090D25
	public virtual int GetSubmergedPathCostPenalty(PathFinder.PotentialPath path, NavGrid.Link link)
	{
		return 0;
	}

	// Token: 0x04000F6C RID: 3948
	protected Navigator navigator;

	// Token: 0x04000F6D RID: 3949
	protected int prefabInstanceID;
}
