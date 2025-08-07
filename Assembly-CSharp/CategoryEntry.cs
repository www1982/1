using System;
using System.Collections.Generic;

// Token: 0x02000C7C RID: 3196
public class CategoryEntry : CodexEntry
{
	// Token: 0x17000706 RID: 1798
	// (get) Token: 0x060061E4 RID: 25060 RVA: 0x002466B1 File Offset: 0x002448B1
	// (set) Token: 0x060061E5 RID: 25061 RVA: 0x002466B9 File Offset: 0x002448B9
	public bool largeFormat { get; set; }

	// Token: 0x17000707 RID: 1799
	// (get) Token: 0x060061E6 RID: 25062 RVA: 0x002466C2 File Offset: 0x002448C2
	// (set) Token: 0x060061E7 RID: 25063 RVA: 0x002466CA File Offset: 0x002448CA
	public bool sort { get; set; }

	// Token: 0x060061E8 RID: 25064 RVA: 0x002466D3 File Offset: 0x002448D3
	public CategoryEntry(string category, List<ContentContainer> contentContainers, string name, List<CodexEntry> entriesInCategory, bool largeFormat, bool sort)
		: base(category, contentContainers, name)
	{
		this.entriesInCategory = entriesInCategory;
		this.largeFormat = largeFormat;
		this.sort = sort;
	}

	// Token: 0x0400425F RID: 16991
	public List<CodexEntry> entriesInCategory = new List<CodexEntry>();
}
