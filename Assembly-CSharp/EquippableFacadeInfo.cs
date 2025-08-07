using System;
using Database;

// Token: 0x02000560 RID: 1376
public class EquippableFacadeInfo : IBlueprintInfo, IHasDlcRestrictions
{
	// Token: 0x1700010F RID: 271
	// (get) Token: 0x06001E88 RID: 7816 RVA: 0x000A4F58 File Offset: 0x000A3158
	// (set) Token: 0x06001E89 RID: 7817 RVA: 0x000A4F60 File Offset: 0x000A3160
	public string id { get; set; }

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x06001E8A RID: 7818 RVA: 0x000A4F69 File Offset: 0x000A3169
	// (set) Token: 0x06001E8B RID: 7819 RVA: 0x000A4F71 File Offset: 0x000A3171
	public string name { get; set; }

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x06001E8C RID: 7820 RVA: 0x000A4F7A File Offset: 0x000A317A
	// (set) Token: 0x06001E8D RID: 7821 RVA: 0x000A4F82 File Offset: 0x000A3182
	public string desc { get; set; }

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x06001E8E RID: 7822 RVA: 0x000A4F8B File Offset: 0x000A318B
	// (set) Token: 0x06001E8F RID: 7823 RVA: 0x000A4F93 File Offset: 0x000A3193
	public PermitRarity rarity { get; set; }

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x06001E90 RID: 7824 RVA: 0x000A4F9C File Offset: 0x000A319C
	// (set) Token: 0x06001E91 RID: 7825 RVA: 0x000A4FA4 File Offset: 0x000A31A4
	public string animFile { get; set; }

	// Token: 0x06001E92 RID: 7826 RVA: 0x000A4FB0 File Offset: 0x000A31B0
	public EquippableFacadeInfo(string id, string name, string desc, PermitRarity rarity, string defID, string buildOverride, string animFile, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
	{
		this.id = id;
		this.name = name;
		this.desc = desc;
		this.rarity = rarity;
		this.defID = defID;
		this.buildOverride = buildOverride;
		this.animFile = animFile;
		this.requiredDlcIds = requiredDlcIds;
		this.forbiddenDlcIds = forbiddenDlcIds;
	}

	// Token: 0x06001E93 RID: 7827 RVA: 0x000A5008 File Offset: 0x000A3208
	public string[] GetRequiredDlcIds()
	{
		return this.requiredDlcIds;
	}

	// Token: 0x06001E94 RID: 7828 RVA: 0x000A5010 File Offset: 0x000A3210
	public string[] GetForbiddenDlcIds()
	{
		return this.forbiddenDlcIds;
	}

	// Token: 0x040011D5 RID: 4565
	public string buildOverride;

	// Token: 0x040011D6 RID: 4566
	public string defID;

	// Token: 0x040011D8 RID: 4568
	public string[] requiredDlcIds;

	// Token: 0x040011D9 RID: 4569
	public string[] forbiddenDlcIds;
}
