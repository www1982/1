using System;
using Database;

// Token: 0x0200055E RID: 1374
public class BalloonArtistFacadeInfo : IBlueprintInfo, IHasDlcRestrictions
{
	// Token: 0x17000105 RID: 261
	// (get) Token: 0x06001E6E RID: 7790 RVA: 0x000A4DE8 File Offset: 0x000A2FE8
	// (set) Token: 0x06001E6F RID: 7791 RVA: 0x000A4DF0 File Offset: 0x000A2FF0
	public string id { get; set; }

	// Token: 0x17000106 RID: 262
	// (get) Token: 0x06001E70 RID: 7792 RVA: 0x000A4DF9 File Offset: 0x000A2FF9
	// (set) Token: 0x06001E71 RID: 7793 RVA: 0x000A4E01 File Offset: 0x000A3001
	public string name { get; set; }

	// Token: 0x17000107 RID: 263
	// (get) Token: 0x06001E72 RID: 7794 RVA: 0x000A4E0A File Offset: 0x000A300A
	// (set) Token: 0x06001E73 RID: 7795 RVA: 0x000A4E12 File Offset: 0x000A3012
	public string desc { get; set; }

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06001E74 RID: 7796 RVA: 0x000A4E1B File Offset: 0x000A301B
	// (set) Token: 0x06001E75 RID: 7797 RVA: 0x000A4E23 File Offset: 0x000A3023
	public PermitRarity rarity { get; set; }

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x06001E76 RID: 7798 RVA: 0x000A4E2C File Offset: 0x000A302C
	// (set) Token: 0x06001E77 RID: 7799 RVA: 0x000A4E34 File Offset: 0x000A3034
	public string animFile { get; set; }

	// Token: 0x06001E78 RID: 7800 RVA: 0x000A4E40 File Offset: 0x000A3040
	public BalloonArtistFacadeInfo(string id, string name, string desc, PermitRarity rarity, string animFile, BalloonArtistFacadeType balloonFacadeType, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
	{
		this.id = id;
		this.name = name;
		this.desc = desc;
		this.rarity = rarity;
		this.animFile = animFile;
		this.balloonFacadeType = balloonFacadeType;
		this.requiredDlcIds = requiredDlcIds;
		this.forbiddenDlcIds = forbiddenDlcIds;
	}

	// Token: 0x06001E79 RID: 7801 RVA: 0x000A4E90 File Offset: 0x000A3090
	public string[] GetRequiredDlcIds()
	{
		return this.requiredDlcIds;
	}

	// Token: 0x06001E7A RID: 7802 RVA: 0x000A4E98 File Offset: 0x000A3098
	public string[] GetForbiddenDlcIds()
	{
		return this.forbiddenDlcIds;
	}

	// Token: 0x040011C6 RID: 4550
	public BalloonArtistFacadeType balloonFacadeType;

	// Token: 0x040011C7 RID: 4551
	public string[] requiredDlcIds;

	// Token: 0x040011C8 RID: 4552
	public string[] forbiddenDlcIds;
}
