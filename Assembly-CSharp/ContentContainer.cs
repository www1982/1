using System;
using System.Collections.Generic;
using KSerialization.Converters;
using UnityEngine;

// Token: 0x02000C87 RID: 3207
public class ContentContainer : IHasDlcRestrictions
{
	// Token: 0x0600629F RID: 25247 RVA: 0x00251A9A File Offset: 0x0024FC9A
	public ContentContainer()
	{
		this.content = new List<ICodexWidget>();
	}

	// Token: 0x060062A0 RID: 25248 RVA: 0x00251AAD File Offset: 0x0024FCAD
	public ContentContainer(List<ICodexWidget> content, ContentContainer.ContentLayout contentLayout)
	{
		this.content = content;
		this.contentLayout = contentLayout;
	}

	// Token: 0x17000721 RID: 1825
	// (get) Token: 0x060062A1 RID: 25249 RVA: 0x00251AC3 File Offset: 0x0024FCC3
	// (set) Token: 0x060062A2 RID: 25250 RVA: 0x00251ACB File Offset: 0x0024FCCB
	public List<ICodexWidget> content { get; set; }

	// Token: 0x17000722 RID: 1826
	// (get) Token: 0x060062A3 RID: 25251 RVA: 0x00251AD4 File Offset: 0x0024FCD4
	// (set) Token: 0x060062A4 RID: 25252 RVA: 0x00251ADC File Offset: 0x0024FCDC
	public string lockID { get; set; }

	// Token: 0x17000723 RID: 1827
	// (get) Token: 0x060062A5 RID: 25253 RVA: 0x00251AE5 File Offset: 0x0024FCE5
	// (set) Token: 0x060062A6 RID: 25254 RVA: 0x00251AED File Offset: 0x0024FCED
	public string[] requiredDlcIds { get; set; }

	// Token: 0x17000724 RID: 1828
	// (get) Token: 0x060062A7 RID: 25255 RVA: 0x00251AF6 File Offset: 0x0024FCF6
	// (set) Token: 0x060062A8 RID: 25256 RVA: 0x00251AFE File Offset: 0x0024FCFE
	public string[] forbiddenDlcIds { get; set; }

	// Token: 0x17000725 RID: 1829
	// (get) Token: 0x060062A9 RID: 25257 RVA: 0x00251B07 File Offset: 0x0024FD07
	// (set) Token: 0x060062AA RID: 25258 RVA: 0x00251B0F File Offset: 0x0024FD0F
	[StringEnumConverter]
	public ContentContainer.ContentLayout contentLayout { get; set; }

	// Token: 0x17000726 RID: 1830
	// (get) Token: 0x060062AB RID: 25259 RVA: 0x00251B18 File Offset: 0x0024FD18
	// (set) Token: 0x060062AC RID: 25260 RVA: 0x00251B20 File Offset: 0x0024FD20
	public bool showBeforeGeneratedContent { get; set; }

	// Token: 0x060062AD RID: 25261 RVA: 0x00251B29 File Offset: 0x0024FD29
	public string[] GetRequiredDlcIds()
	{
		return this.requiredDlcIds;
	}

	// Token: 0x060062AE RID: 25262 RVA: 0x00251B31 File Offset: 0x0024FD31
	public string[] GetForbiddenDlcIds()
	{
		return this.forbiddenDlcIds;
	}

	// Token: 0x040042CA RID: 17098
	public GameObject go;

	// Token: 0x02001E5E RID: 7774
	public enum ContentLayout
	{
		// Token: 0x04008D5D RID: 36189
		Vertical,
		// Token: 0x04008D5E RID: 36190
		Horizontal,
		// Token: 0x04008D5F RID: 36191
		Grid,
		// Token: 0x04008D60 RID: 36192
		GridTwoColumn,
		// Token: 0x04008D61 RID: 36193
		GridTwoColumnTall
	}
}
