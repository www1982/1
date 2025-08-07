using System;
using System.Collections.Generic;

// Token: 0x0200092E RID: 2350
public class TagNameComparer : IComparer<Tag>
{
	// Token: 0x060041D7 RID: 16855 RVA: 0x001782C1 File Offset: 0x001764C1
	public TagNameComparer()
	{
	}

	// Token: 0x060041D8 RID: 16856 RVA: 0x001782C9 File Offset: 0x001764C9
	public TagNameComparer(Tag firstTag)
	{
		this.firstTag = firstTag;
	}

	// Token: 0x060041D9 RID: 16857 RVA: 0x001782D8 File Offset: 0x001764D8
	public int Compare(Tag x, Tag y)
	{
		if (x == y)
		{
			return 0;
		}
		if (this.firstTag.IsValid)
		{
			if (x == this.firstTag && y != this.firstTag)
			{
				return 1;
			}
			if (x != this.firstTag && y == this.firstTag)
			{
				return -1;
			}
		}
		return x.ProperNameStripLink().CompareTo(y.ProperNameStripLink());
	}

	// Token: 0x04002B56 RID: 11094
	private Tag firstTag;
}
