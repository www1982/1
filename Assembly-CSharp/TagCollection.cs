using System;
using System.Collections.Generic;

// Token: 0x02000BB2 RID: 2994
public class TagCollection : IReadonlyTags
{
	// Token: 0x0600598F RID: 22927 RVA: 0x00205475 File Offset: 0x00203675
	public TagCollection()
	{
	}

	// Token: 0x06005990 RID: 22928 RVA: 0x00205488 File Offset: 0x00203688
	public TagCollection(int[] initialTags)
	{
		for (int i = 0; i < initialTags.Length; i++)
		{
			this.tags.Add(initialTags[i]);
		}
	}

	// Token: 0x06005991 RID: 22929 RVA: 0x002054C4 File Offset: 0x002036C4
	public TagCollection(string[] initialTags)
	{
		for (int i = 0; i < initialTags.Length; i++)
		{
			this.tags.Add(Hash.SDBMLower(initialTags[i]));
		}
	}

	// Token: 0x06005992 RID: 22930 RVA: 0x00205504 File Offset: 0x00203704
	public TagCollection(TagCollection initialTags)
	{
		if (initialTags != null && initialTags.tags != null)
		{
			this.tags.UnionWith(initialTags.tags);
		}
	}

	// Token: 0x06005993 RID: 22931 RVA: 0x00205534 File Offset: 0x00203734
	public TagCollection Append(TagCollection others)
	{
		foreach (int num in others.tags)
		{
			this.tags.Add(num);
		}
		return this;
	}

	// Token: 0x06005994 RID: 22932 RVA: 0x00205590 File Offset: 0x00203790
	public void AddTag(string tag)
	{
		this.tags.Add(Hash.SDBMLower(tag));
	}

	// Token: 0x06005995 RID: 22933 RVA: 0x002055A4 File Offset: 0x002037A4
	public void AddTag(int tag)
	{
		this.tags.Add(tag);
	}

	// Token: 0x06005996 RID: 22934 RVA: 0x002055B3 File Offset: 0x002037B3
	public void RemoveTag(string tag)
	{
		this.tags.Remove(Hash.SDBMLower(tag));
	}

	// Token: 0x06005997 RID: 22935 RVA: 0x002055C7 File Offset: 0x002037C7
	public void RemoveTag(int tag)
	{
		this.tags.Remove(tag);
	}

	// Token: 0x06005998 RID: 22936 RVA: 0x002055D6 File Offset: 0x002037D6
	public bool HasTag(string tag)
	{
		return this.tags.Contains(Hash.SDBMLower(tag));
	}

	// Token: 0x06005999 RID: 22937 RVA: 0x002055E9 File Offset: 0x002037E9
	public bool HasTag(int tag)
	{
		return this.tags.Contains(tag);
	}

	// Token: 0x0600599A RID: 22938 RVA: 0x002055F8 File Offset: 0x002037F8
	public bool HasTags(int[] searchTags)
	{
		for (int i = 0; i < searchTags.Length; i++)
		{
			if (!this.tags.Contains(searchTags[i]))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x04003B66 RID: 15206
	private HashSet<int> tags = new HashSet<int>();
}
