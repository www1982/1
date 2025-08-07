using System;
using Database;

// Token: 0x0200055C RID: 1372
public class ClothingItemInfo : IBlueprintInfo, IHasDlcRestrictions
{
	// Token: 0x170000FB RID: 251
	// (get) Token: 0x06001E54 RID: 7764 RVA: 0x000A4C38 File Offset: 0x000A2E38
	// (set) Token: 0x06001E55 RID: 7765 RVA: 0x000A4C40 File Offset: 0x000A2E40
	public string id { get; set; }

	// Token: 0x170000FC RID: 252
	// (get) Token: 0x06001E56 RID: 7766 RVA: 0x000A4C49 File Offset: 0x000A2E49
	// (set) Token: 0x06001E57 RID: 7767 RVA: 0x000A4C51 File Offset: 0x000A2E51
	public string name { get; set; }

	// Token: 0x170000FD RID: 253
	// (get) Token: 0x06001E58 RID: 7768 RVA: 0x000A4C5A File Offset: 0x000A2E5A
	// (set) Token: 0x06001E59 RID: 7769 RVA: 0x000A4C62 File Offset: 0x000A2E62
	public string desc { get; set; }

	// Token: 0x170000FE RID: 254
	// (get) Token: 0x06001E5A RID: 7770 RVA: 0x000A4C6B File Offset: 0x000A2E6B
	// (set) Token: 0x06001E5B RID: 7771 RVA: 0x000A4C73 File Offset: 0x000A2E73
	public PermitRarity rarity { get; set; }

	// Token: 0x170000FF RID: 255
	// (get) Token: 0x06001E5C RID: 7772 RVA: 0x000A4C7C File Offset: 0x000A2E7C
	// (set) Token: 0x06001E5D RID: 7773 RVA: 0x000A4C84 File Offset: 0x000A2E84
	public string animFile { get; set; }

	// Token: 0x06001E5E RID: 7774 RVA: 0x000A4C90 File Offset: 0x000A2E90
	public ClothingItemInfo(string id, string name, string desc, PermitCategory category, PermitRarity rarity, string animFile, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
	{
		Option<ClothingOutfitUtility.OutfitType> outfitTypeFor = PermitCategories.GetOutfitTypeFor(category);
		if (outfitTypeFor.IsNone())
		{
			throw new Exception(string.Format("Expected permit category {0} on ClothingItemResource \"{1}\" to have an {2} but none found.", category, id, "OutfitType"));
		}
		this.id = id;
		this.name = name;
		this.desc = desc;
		this.outfitType = outfitTypeFor.Unwrap();
		this.category = category;
		this.rarity = rarity;
		this.animFile = animFile;
		this.requiredDlcIds = requiredDlcIds;
		this.forbiddenDlcIds = forbiddenDlcIds;
	}

	// Token: 0x06001E5F RID: 7775 RVA: 0x000A4D1B File Offset: 0x000A2F1B
	public string[] GetRequiredDlcIds()
	{
		return this.requiredDlcIds;
	}

	// Token: 0x06001E60 RID: 7776 RVA: 0x000A4D23 File Offset: 0x000A2F23
	public string[] GetForbiddenDlcIds()
	{
		return this.forbiddenDlcIds;
	}

	// Token: 0x040011B2 RID: 4530
	public ClothingOutfitUtility.OutfitType outfitType;

	// Token: 0x040011B3 RID: 4531
	public PermitCategory category;

	// Token: 0x040011B6 RID: 4534
	private string[] requiredDlcIds;

	// Token: 0x040011B7 RID: 4535
	private string[] forbiddenDlcIds;
}
