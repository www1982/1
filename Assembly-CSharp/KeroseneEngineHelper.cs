using System;
using STRINGS;

// Token: 0x02000267 RID: 615
internal static class KeroseneEngineHelper
{
	// Token: 0x1700001F RID: 31
	// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0004AB4D File Offset: 0x00048D4D
	public static string ID
	{
		get
		{
			if (DlcManager.IsExpansion1Active())
			{
				return "KeroseneEngineCluster";
			}
			return "KeroseneEngine";
		}
	}

	// Token: 0x17000020 RID: 32
	// (get) Token: 0x06000C75 RID: 3189 RVA: 0x0004AB61 File Offset: 0x00048D61
	public static string CODEXID
	{
		get
		{
			return KeroseneEngineHelper.ID.ToUpperInvariant();
		}
	}

	// Token: 0x17000021 RID: 33
	// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0004AB6D File Offset: 0x00048D6D
	public static string NAME
	{
		get
		{
			if (DlcManager.IsExpansion1Active())
			{
				return BUILDINGS.PREFABS.KEROSENEENGINECLUSTER.NAME;
			}
			return BUILDINGS.PREFABS.KEROSENEENGINE.NAME;
		}
	}
}
