using System;
using Database;

// Token: 0x02000561 RID: 1377
public class MonumentPartInfo : IBlueprintInfo, IHasDlcRestrictions
{
	// Token: 0x17000114 RID: 276
	// (get) Token: 0x06001E95 RID: 7829 RVA: 0x000A5018 File Offset: 0x000A3218
	// (set) Token: 0x06001E96 RID: 7830 RVA: 0x000A5020 File Offset: 0x000A3220
	public string id { get; set; }

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x06001E97 RID: 7831 RVA: 0x000A5029 File Offset: 0x000A3229
	// (set) Token: 0x06001E98 RID: 7832 RVA: 0x000A5031 File Offset: 0x000A3231
	public string name { get; set; }

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06001E99 RID: 7833 RVA: 0x000A503A File Offset: 0x000A323A
	// (set) Token: 0x06001E9A RID: 7834 RVA: 0x000A5042 File Offset: 0x000A3242
	public string desc { get; set; }

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x06001E9B RID: 7835 RVA: 0x000A504B File Offset: 0x000A324B
	// (set) Token: 0x06001E9C RID: 7836 RVA: 0x000A5053 File Offset: 0x000A3253
	public PermitRarity rarity { get; set; }

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06001E9D RID: 7837 RVA: 0x000A505C File Offset: 0x000A325C
	// (set) Token: 0x06001E9E RID: 7838 RVA: 0x000A5064 File Offset: 0x000A3264
	public string animFile { get; set; }

	// Token: 0x06001E9F RID: 7839 RVA: 0x000A5070 File Offset: 0x000A3270
	public MonumentPartInfo(string id, string name, string desc, PermitRarity rarity, string animFilename, string state, string symbolName, MonumentPartResource.Part part, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
	{
		this.id = id;
		this.name = name;
		this.desc = desc;
		this.rarity = rarity;
		this.animFile = animFilename;
		this.state = state;
		this.symbolName = symbolName;
		this.part = part;
		this.requiredDlcIds = requiredDlcIds;
		this.forbiddenDlcIds = forbiddenDlcIds;
	}

	// Token: 0x06001EA0 RID: 7840 RVA: 0x000A50D0 File Offset: 0x000A32D0
	public string[] GetRequiredDlcIds()
	{
		return this.requiredDlcIds;
	}

	// Token: 0x06001EA1 RID: 7841 RVA: 0x000A50D8 File Offset: 0x000A32D8
	public string[] GetForbiddenDlcIds()
	{
		return this.forbiddenDlcIds;
	}

	// Token: 0x040011DF RID: 4575
	public string state;

	// Token: 0x040011E0 RID: 4576
	public string symbolName;

	// Token: 0x040011E1 RID: 4577
	public MonumentPartResource.Part part;

	// Token: 0x040011E2 RID: 4578
	public string[] requiredDlcIds;

	// Token: 0x040011E3 RID: 4579
	public string[] forbiddenDlcIds;
}
