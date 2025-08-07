using System;
using UnityEngine;

// Token: 0x02000D73 RID: 3443
public class MotdData_Box
{
	// Token: 0x06006AFE RID: 27390 RVA: 0x00286874 File Offset: 0x00284A74
	public bool ShouldDisplay()
	{
		long num = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
		return num >= this.startTime && this.finishTime >= num;
	}

	// Token: 0x040048E5 RID: 18661
	public string category;

	// Token: 0x040048E6 RID: 18662
	public string guid;

	// Token: 0x040048E7 RID: 18663
	public long startTime;

	// Token: 0x040048E8 RID: 18664
	public long finishTime;

	// Token: 0x040048E9 RID: 18665
	public string title;

	// Token: 0x040048EA RID: 18666
	public string text;

	// Token: 0x040048EB RID: 18667
	public string image;

	// Token: 0x040048EC RID: 18668
	public string href;

	// Token: 0x040048ED RID: 18669
	public Texture2D resolvedImage;

	// Token: 0x040048EE RID: 18670
	public bool resolvedImageIsFromDisk;
}
