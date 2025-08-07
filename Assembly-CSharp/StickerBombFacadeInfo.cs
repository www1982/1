using System;
using Database;

// Token: 0x0200055F RID: 1375
public class StickerBombFacadeInfo : IBlueprintInfo, IHasDlcRestrictions
{
	// Token: 0x1700010A RID: 266
	// (get) Token: 0x06001E7B RID: 7803 RVA: 0x000A4EA0 File Offset: 0x000A30A0
	// (set) Token: 0x06001E7C RID: 7804 RVA: 0x000A4EA8 File Offset: 0x000A30A8
	public string id { get; set; }

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x06001E7D RID: 7805 RVA: 0x000A4EB1 File Offset: 0x000A30B1
	// (set) Token: 0x06001E7E RID: 7806 RVA: 0x000A4EB9 File Offset: 0x000A30B9
	public string name { get; set; }

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x06001E7F RID: 7807 RVA: 0x000A4EC2 File Offset: 0x000A30C2
	// (set) Token: 0x06001E80 RID: 7808 RVA: 0x000A4ECA File Offset: 0x000A30CA
	public string desc { get; set; }

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x06001E81 RID: 7809 RVA: 0x000A4ED3 File Offset: 0x000A30D3
	// (set) Token: 0x06001E82 RID: 7810 RVA: 0x000A4EDB File Offset: 0x000A30DB
	public PermitRarity rarity { get; set; }

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x06001E83 RID: 7811 RVA: 0x000A4EE4 File Offset: 0x000A30E4
	// (set) Token: 0x06001E84 RID: 7812 RVA: 0x000A4EEC File Offset: 0x000A30EC
	public string animFile { get; set; }

	// Token: 0x06001E85 RID: 7813 RVA: 0x000A4EF8 File Offset: 0x000A30F8
	public StickerBombFacadeInfo(string id, string name, string desc, PermitRarity rarity, string animFile, string sticker, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
	{
		this.id = id;
		this.name = name;
		this.desc = desc;
		this.rarity = rarity;
		this.animFile = animFile;
		this.sticker = sticker;
		this.requiredDlcIds = requiredDlcIds;
		this.forbiddenDlcIds = forbiddenDlcIds;
	}

	// Token: 0x06001E86 RID: 7814 RVA: 0x000A4F48 File Offset: 0x000A3148
	public string[] GetRequiredDlcIds()
	{
		return this.requiredDlcIds;
	}

	// Token: 0x06001E87 RID: 7815 RVA: 0x000A4F50 File Offset: 0x000A3150
	public string[] GetForbiddenDlcIds()
	{
		return this.forbiddenDlcIds;
	}

	// Token: 0x040011CE RID: 4558
	public string sticker;

	// Token: 0x040011CF RID: 4559
	public string[] requiredDlcIds;

	// Token: 0x040011D0 RID: 4560
	public string[] forbiddenDlcIds;
}
