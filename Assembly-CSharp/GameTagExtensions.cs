using System;
using UnityEngine;

// Token: 0x0200092D RID: 2349
public static class GameTagExtensions
{
	// Token: 0x060041D2 RID: 16850 RVA: 0x0017827F File Offset: 0x0017647F
	public static GameObject Prefab(this Tag tag)
	{
		return Assets.GetPrefab(tag);
	}

	// Token: 0x060041D3 RID: 16851 RVA: 0x00178287 File Offset: 0x00176487
	public static string ProperName(this Tag tag)
	{
		return TagManager.GetProperName(tag, false);
	}

	// Token: 0x060041D4 RID: 16852 RVA: 0x00178290 File Offset: 0x00176490
	public static string ProperNameStripLink(this Tag tag)
	{
		return TagManager.GetProperName(tag, true);
	}

	// Token: 0x060041D5 RID: 16853 RVA: 0x00178299 File Offset: 0x00176499
	public static Tag Create(SimHashes id)
	{
		return TagManager.Create(id.ToString());
	}

	// Token: 0x060041D6 RID: 16854 RVA: 0x001782AD File Offset: 0x001764AD
	public static Tag CreateTag(this SimHashes id)
	{
		return TagManager.Create(id.ToString());
	}
}
