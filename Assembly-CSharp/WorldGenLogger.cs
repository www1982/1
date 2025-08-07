using System;

// Token: 0x02000E91 RID: 3729
public static class WorldGenLogger
{
	// Token: 0x060076D2 RID: 30418 RVA: 0x002D82B2 File Offset: 0x002D64B2
	public static void LogException(string message, string stack)
	{
		Debug.LogError(message + "\n" + stack);
	}
}
