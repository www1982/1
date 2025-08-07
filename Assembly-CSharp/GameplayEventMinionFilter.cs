using System;

// Token: 0x020004BD RID: 1213
public class GameplayEventMinionFilter
{
	// Token: 0x04000EEE RID: 3822
	public string id;

	// Token: 0x04000EEF RID: 3823
	public GameplayEventMinionFilter.FilterFn filter;

	// Token: 0x020012F6 RID: 4854
	// (Invoke) Token: 0x06008847 RID: 34887
	public delegate bool FilterFn(MinionIdentity minion);
}
