using System;
using Database;

// Token: 0x0200055B RID: 1371
public class ArtableInfo : IBlueprintInfo, IHasDlcRestrictions
{
	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x06001E47 RID: 7751 RVA: 0x000A4B5A File Offset: 0x000A2D5A
	// (set) Token: 0x06001E48 RID: 7752 RVA: 0x000A4B62 File Offset: 0x000A2D62
	public string id { get; set; }

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x06001E49 RID: 7753 RVA: 0x000A4B6B File Offset: 0x000A2D6B
	// (set) Token: 0x06001E4A RID: 7754 RVA: 0x000A4B73 File Offset: 0x000A2D73
	public string name { get; set; }

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x06001E4B RID: 7755 RVA: 0x000A4B7C File Offset: 0x000A2D7C
	// (set) Token: 0x06001E4C RID: 7756 RVA: 0x000A4B84 File Offset: 0x000A2D84
	public string desc { get; set; }

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x06001E4D RID: 7757 RVA: 0x000A4B8D File Offset: 0x000A2D8D
	// (set) Token: 0x06001E4E RID: 7758 RVA: 0x000A4B95 File Offset: 0x000A2D95
	public PermitRarity rarity { get; set; }

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x06001E4F RID: 7759 RVA: 0x000A4B9E File Offset: 0x000A2D9E
	// (set) Token: 0x06001E50 RID: 7760 RVA: 0x000A4BA6 File Offset: 0x000A2DA6
	public string animFile { get; set; }

	// Token: 0x06001E51 RID: 7761 RVA: 0x000A4BB0 File Offset: 0x000A2DB0
	public ArtableInfo(string id, string name, string desc, PermitRarity rarity, string animFile, string anim, int decor_value, bool cheer_on_complete, string status_id, string prefabId, string symbolname = "", string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
	{
		this.id = id;
		this.name = name;
		this.desc = desc;
		this.rarity = rarity;
		this.animFile = animFile;
		this.anim = anim;
		this.decor_value = decor_value;
		this.cheer_on_complete = cheer_on_complete;
		this.status_id = status_id;
		this.prefabId = prefabId;
		this.symbolname = symbolname;
		this.requiredDlcIds = requiredDlcIds;
		this.forbiddenDlcIds = forbiddenDlcIds;
	}

	// Token: 0x06001E52 RID: 7762 RVA: 0x000A4C28 File Offset: 0x000A2E28
	public string[] GetRequiredDlcIds()
	{
		return this.requiredDlcIds;
	}

	// Token: 0x06001E53 RID: 7763 RVA: 0x000A4C30 File Offset: 0x000A2E30
	public string[] GetForbiddenDlcIds()
	{
		return this.forbiddenDlcIds;
	}

	// Token: 0x040011A7 RID: 4519
	public string anim;

	// Token: 0x040011A8 RID: 4520
	public int decor_value;

	// Token: 0x040011A9 RID: 4521
	public bool cheer_on_complete;

	// Token: 0x040011AA RID: 4522
	public string status_id;

	// Token: 0x040011AB RID: 4523
	public string prefabId;

	// Token: 0x040011AC RID: 4524
	public string symbolname;

	// Token: 0x040011AD RID: 4525
	public string[] requiredDlcIds;

	// Token: 0x040011AE RID: 4526
	public string[] forbiddenDlcIds;
}
