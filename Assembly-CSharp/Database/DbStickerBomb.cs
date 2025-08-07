using System;

namespace Database
{
	// Token: 0x02000F13 RID: 3859
	public class DbStickerBomb : PermitResource
	{
		// Token: 0x06007A00 RID: 31232 RVA: 0x00303EA7 File Offset: 0x003020A7
		public DbStickerBomb(string id, string name, string desc, PermitRarity rarity, string animfilename, string sticker, string[] requiredDlcIds, string[] forbiddenDlcIds)
			: base(id, name, desc, PermitCategory.Artwork, rarity, requiredDlcIds, forbiddenDlcIds)
		{
			this.id = id;
			this.sticker = sticker;
			this.animFile = Assets.GetAnim(animfilename);
		}

		// Token: 0x06007A01 RID: 31233 RVA: 0x00303EDC File Offset: 0x003020DC
		public override PermitPresentationInfo GetPermitPresentationInfo()
		{
			return new PermitPresentationInfo
			{
				sprite = Def.GetUISpriteFromMultiObjectAnim(this.animFile, string.Format("{0}_{1}", "idle_sticker", this.sticker), false, string.Format("{0}_{1}", "sticker", this.sticker))
			};
		}

		// Token: 0x04005938 RID: 22840
		public string id;

		// Token: 0x04005939 RID: 22841
		public string sticker;

		// Token: 0x0400593A RID: 22842
		public KAnimFile animFile;

		// Token: 0x0400593B RID: 22843
		private const string stickerAnimPrefix = "idle_sticker";

		// Token: 0x0400593C RID: 22844
		private const string stickerSymbolPrefix = "sticker";
	}
}
