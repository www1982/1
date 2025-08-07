using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020009A0 RID: 2464
public static class KPrefabIDExtensions
{
	// Token: 0x0600477D RID: 18301 RVA: 0x0019C56E File Offset: 0x0019A76E
	public static Tag PrefabID(this Component cmp)
	{
		return cmp.GetComponent<KPrefabID>().PrefabID();
	}

	// Token: 0x0600477E RID: 18302 RVA: 0x0019C57B File Offset: 0x0019A77B
	public static Tag PrefabID(this GameObject go)
	{
		return go.GetComponent<KPrefabID>().PrefabID();
	}

	// Token: 0x0600477F RID: 18303 RVA: 0x0019C588 File Offset: 0x0019A788
	public static Tag PrefabID(this StateMachine.Instance smi)
	{
		return smi.GetComponent<KPrefabID>().PrefabID();
	}

	// Token: 0x06004780 RID: 18304 RVA: 0x0019C595 File Offset: 0x0019A795
	public static bool IsPrefabID(this Component cmp, Tag id)
	{
		return cmp.GetComponent<KPrefabID>().IsPrefabID(id);
	}

	// Token: 0x06004781 RID: 18305 RVA: 0x0019C5A3 File Offset: 0x0019A7A3
	public static bool IsPrefabID(this GameObject go, Tag id)
	{
		return go.GetComponent<KPrefabID>().IsPrefabID(id);
	}

	// Token: 0x06004782 RID: 18306 RVA: 0x0019C5B1 File Offset: 0x0019A7B1
	public static bool HasTag(this Component cmp, Tag tag)
	{
		return cmp.GetComponent<KPrefabID>().HasTag(tag);
	}

	// Token: 0x06004783 RID: 18307 RVA: 0x0019C5BF File Offset: 0x0019A7BF
	public static bool HasTag(this GameObject go, Tag tag)
	{
		return go.GetComponent<KPrefabID>().HasTag(tag);
	}

	// Token: 0x06004784 RID: 18308 RVA: 0x0019C5CD File Offset: 0x0019A7CD
	public static bool HasAnyTags(this Component cmp, Tag[] tags)
	{
		return cmp.GetComponent<KPrefabID>().HasAnyTags(tags);
	}

	// Token: 0x06004785 RID: 18309 RVA: 0x0019C5DB File Offset: 0x0019A7DB
	public static bool HasAnyTags(this Component cmp, List<Tag> tags)
	{
		return cmp.GetComponent<KPrefabID>().HasAnyTags(tags);
	}

	// Token: 0x06004786 RID: 18310 RVA: 0x0019C5E9 File Offset: 0x0019A7E9
	public static bool HasAnyTags(this GameObject go, Tag[] tags)
	{
		return go.GetComponent<KPrefabID>().HasAnyTags(tags);
	}

	// Token: 0x06004787 RID: 18311 RVA: 0x0019C5F7 File Offset: 0x0019A7F7
	public static bool HasAnyTags(this GameObject go, List<Tag> tags)
	{
		return go.GetComponent<KPrefabID>().HasAnyTags(tags);
	}

	// Token: 0x06004788 RID: 18312 RVA: 0x0019C605 File Offset: 0x0019A805
	public static bool HasAllTags(this Component cmp, Tag[] tags)
	{
		return cmp.GetComponent<KPrefabID>().HasAllTags(tags);
	}

	// Token: 0x06004789 RID: 18313 RVA: 0x0019C613 File Offset: 0x0019A813
	public static bool HasAllTags(this GameObject go, Tag[] tags)
	{
		return go.GetComponent<KPrefabID>().HasAllTags(tags);
	}

	// Token: 0x0600478A RID: 18314 RVA: 0x0019C621 File Offset: 0x0019A821
	public static void AddTag(this GameObject go, Tag tag)
	{
		go.GetComponent<KPrefabID>().AddTag(tag, false);
	}

	// Token: 0x0600478B RID: 18315 RVA: 0x0019C630 File Offset: 0x0019A830
	public static void AddTag(this Component cmp, Tag tag)
	{
		cmp.GetComponent<KPrefabID>().AddTag(tag, false);
	}

	// Token: 0x0600478C RID: 18316 RVA: 0x0019C63F File Offset: 0x0019A83F
	public static void RemoveTag(this GameObject go, Tag tag)
	{
		go.GetComponent<KPrefabID>().RemoveTag(tag);
	}

	// Token: 0x0600478D RID: 18317 RVA: 0x0019C64D File Offset: 0x0019A84D
	public static void RemoveTag(this Component cmp, Tag tag)
	{
		cmp.GetComponent<KPrefabID>().RemoveTag(tag);
	}
}
