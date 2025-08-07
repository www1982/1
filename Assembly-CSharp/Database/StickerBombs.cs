using System;

namespace Database
{
	// Token: 0x02000F14 RID: 3860
	public class StickerBombs : ResourceSet<DbStickerBomb>
	{
		// Token: 0x06007A02 RID: 31234 RVA: 0x00303F30 File Offset: 0x00302130
		public StickerBombs(ResourceSet parent)
			: base("StickerBombs", parent)
		{
			foreach (StickerBombFacadeInfo stickerBombFacadeInfo in Blueprints.Get().all.stickerBombFacades)
			{
				this.Add(stickerBombFacadeInfo.id, stickerBombFacadeInfo.name, stickerBombFacadeInfo.desc, stickerBombFacadeInfo.rarity, stickerBombFacadeInfo.animFile, stickerBombFacadeInfo.sticker, stickerBombFacadeInfo.requiredDlcIds, stickerBombFacadeInfo.GetForbiddenDlcIds());
			}
		}

		// Token: 0x06007A03 RID: 31235 RVA: 0x00303FC8 File Offset: 0x003021C8
		private DbStickerBomb Add(string id, string name, string desc, PermitRarity rarity, string animfilename, string symbolName, string[] requiredDlcIds, string[] forbiddenDlcIds)
		{
			DbStickerBomb dbStickerBomb = new DbStickerBomb(id, name, desc, rarity, animfilename, symbolName, requiredDlcIds, forbiddenDlcIds);
			this.resources.Add(dbStickerBomb);
			return dbStickerBomb;
		}

		// Token: 0x06007A04 RID: 31236 RVA: 0x00303FF5 File Offset: 0x003021F5
		public DbStickerBomb GetRandomSticker()
		{
			return this.resources.GetRandom<DbStickerBomb>();
		}
	}
}
