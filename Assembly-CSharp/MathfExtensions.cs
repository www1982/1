using System;

// Token: 0x0200033D RID: 829
public static class MathfExtensions
{
	// Token: 0x0600112B RID: 4395 RVA: 0x00064931 File Offset: 0x00062B31
	public static long Max(this long a, long b)
	{
		if (a < b)
		{
			return b;
		}
		return a;
	}

	// Token: 0x0600112C RID: 4396 RVA: 0x0006493A File Offset: 0x00062B3A
	public static long Min(this long a, long b)
	{
		if (a > b)
		{
			return b;
		}
		return a;
	}
}
