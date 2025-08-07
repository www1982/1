using System;
using System.Collections.Generic;
using System.Diagnostics;
using Database;

// Token: 0x0200055D RID: 1373
[DebuggerDisplay("{id} - {name}")]
public class BuildingFacadeInfo : IBlueprintInfo, IHasDlcRestrictions
{
	// Token: 0x17000100 RID: 256
	// (get) Token: 0x06001E61 RID: 7777 RVA: 0x000A4D2B File Offset: 0x000A2F2B
	// (set) Token: 0x06001E62 RID: 7778 RVA: 0x000A4D33 File Offset: 0x000A2F33
	public string id { get; set; }

	// Token: 0x17000101 RID: 257
	// (get) Token: 0x06001E63 RID: 7779 RVA: 0x000A4D3C File Offset: 0x000A2F3C
	// (set) Token: 0x06001E64 RID: 7780 RVA: 0x000A4D44 File Offset: 0x000A2F44
	public string name { get; set; }

	// Token: 0x17000102 RID: 258
	// (get) Token: 0x06001E65 RID: 7781 RVA: 0x000A4D4D File Offset: 0x000A2F4D
	// (set) Token: 0x06001E66 RID: 7782 RVA: 0x000A4D55 File Offset: 0x000A2F55
	public string desc { get; set; }

	// Token: 0x17000103 RID: 259
	// (get) Token: 0x06001E67 RID: 7783 RVA: 0x000A4D5E File Offset: 0x000A2F5E
	// (set) Token: 0x06001E68 RID: 7784 RVA: 0x000A4D66 File Offset: 0x000A2F66
	public PermitRarity rarity { get; set; }

	// Token: 0x17000104 RID: 260
	// (get) Token: 0x06001E69 RID: 7785 RVA: 0x000A4D6F File Offset: 0x000A2F6F
	// (set) Token: 0x06001E6A RID: 7786 RVA: 0x000A4D77 File Offset: 0x000A2F77
	public string animFile { get; set; }

	// Token: 0x06001E6B RID: 7787 RVA: 0x000A4D80 File Offset: 0x000A2F80
	public BuildingFacadeInfo(string id, string name, string desc, PermitRarity rarity, string prefabId, string animFile, Dictionary<string, string> workables = null, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
	{
		this.id = id;
		this.name = name;
		this.desc = desc;
		this.rarity = rarity;
		this.prefabId = prefabId;
		this.animFile = animFile;
		this.workables = workables;
		this.requiredDlcIds = requiredDlcIds;
		this.forbiddenDlcIds = forbiddenDlcIds;
	}

	// Token: 0x06001E6C RID: 7788 RVA: 0x000A4DD8 File Offset: 0x000A2FD8
	public string[] GetRequiredDlcIds()
	{
		return this.requiredDlcIds;
	}

	// Token: 0x06001E6D RID: 7789 RVA: 0x000A4DE0 File Offset: 0x000A2FE0
	public string[] GetForbiddenDlcIds()
	{
		return this.forbiddenDlcIds;
	}

	// Token: 0x040011BC RID: 4540
	public string prefabId;

	// Token: 0x040011BE RID: 4542
	public Dictionary<string, string> workables;

	// Token: 0x040011BF RID: 4543
	public string[] requiredDlcIds;

	// Token: 0x040011C0 RID: 4544
	public string[] forbiddenDlcIds;
}
