using System;

// Token: 0x02000BB1 RID: 2993
public interface IReadonlyTags
{
	// Token: 0x0600598C RID: 22924
	bool HasTag(string tag);

	// Token: 0x0600598D RID: 22925
	bool HasTag(int hashtag);

	// Token: 0x0600598E RID: 22926
	bool HasTags(int[] tags);
}
