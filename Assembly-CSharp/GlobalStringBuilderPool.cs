using System;
using System.Text;

// Token: 0x02000452 RID: 1106
public static class GlobalStringBuilderPool
{
	// Token: 0x06001707 RID: 5895 RVA: 0x00081DDB File Offset: 0x0007FFDB
	public static StringBuilder Alloc()
	{
		return GlobalStringBuilderPool.pool.GetInstance();
	}

	// Token: 0x06001708 RID: 5896 RVA: 0x00081DE7 File Offset: 0x0007FFE7
	public static void Free(StringBuilder sb)
	{
		if (sb != null)
		{
			sb.Clear();
		}
		GlobalStringBuilderPool.pool.ReleaseInstance(sb);
	}

	// Token: 0x06001709 RID: 5897 RVA: 0x00081DFE File Offset: 0x0007FFFE
	public static string ReturnAndFree(StringBuilder sb)
	{
		string text = sb.ToString();
		GlobalStringBuilderPool.Free(sb);
		return text;
	}

	// Token: 0x04000D8C RID: 3468
	private static ObjectPool<StringBuilder> pool = new ObjectPool<StringBuilder>(() => new StringBuilder(4096), 4);
}
