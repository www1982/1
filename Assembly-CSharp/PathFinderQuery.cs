using System;

// Token: 0x020004D6 RID: 1238
public class PathFinderQuery
{
	// Token: 0x06001A87 RID: 6791 RVA: 0x00092B28 File Offset: 0x00090D28
	public virtual bool IsMatch(int cell, int parent_cell, int cost)
	{
		return true;
	}

	// Token: 0x06001A88 RID: 6792 RVA: 0x00092B2B File Offset: 0x00090D2B
	public void SetResult(int cell, int cost, NavType nav_type)
	{
		this.resultCell = cell;
		this.resultNavType = nav_type;
	}

	// Token: 0x06001A89 RID: 6793 RVA: 0x00092B3B File Offset: 0x00090D3B
	public void ClearResult()
	{
		this.resultCell = -1;
	}

	// Token: 0x06001A8A RID: 6794 RVA: 0x00092B44 File Offset: 0x00090D44
	public virtual int GetResultCell()
	{
		return this.resultCell;
	}

	// Token: 0x06001A8B RID: 6795 RVA: 0x00092B4C File Offset: 0x00090D4C
	public NavType GetResultNavType()
	{
		return this.resultNavType;
	}

	// Token: 0x04000F6E RID: 3950
	protected int resultCell;

	// Token: 0x04000F6F RID: 3951
	private NavType resultNavType;
}
