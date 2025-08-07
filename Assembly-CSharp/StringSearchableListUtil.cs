using System;
using System.Linq;

// Token: 0x02000460 RID: 1120
public static class StringSearchableListUtil
{
	// Token: 0x06001778 RID: 6008 RVA: 0x00082BC8 File Offset: 0x00080DC8
	public static bool DoAnyTagsMatchFilter(string[] lowercaseTags, in string filter)
	{
		string text = filter.Trim().ToLowerInvariant();
		string[] array = text.Split(' ', StringSplitOptions.None);
		for (int i = 0; i < lowercaseTags.Length; i++)
		{
			string tag = lowercaseTags[i];
			if (StringSearchableListUtil.DoesTagMatchFilter(tag, in text))
			{
				return true;
			}
			if (array.Select((string f) => StringSearchableListUtil.DoesTagMatchFilter(tag, in f)).All((bool result) => result))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06001779 RID: 6009 RVA: 0x00082C5B File Offset: 0x00080E5B
	public static bool DoesTagMatchFilter(string lowercaseTag, in string filter)
	{
		return string.IsNullOrWhiteSpace(filter) || lowercaseTag.Contains(filter);
	}

	// Token: 0x0600177A RID: 6010 RVA: 0x00082C75 File Offset: 0x00080E75
	public static bool ShouldUseFilter(string filter)
	{
		return !string.IsNullOrWhiteSpace(filter);
	}
}
