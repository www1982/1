using System;

namespace Database
{
	// Token: 0x02000ED9 RID: 3801
	public class BalloonArtistFacades : ResourceSet<BalloonArtistFacadeResource>
	{
		// Token: 0x06007935 RID: 31029 RVA: 0x002EEB84 File Offset: 0x002ECD84
		public BalloonArtistFacades(ResourceSet parent)
			: base("BalloonArtistFacades", parent)
		{
			foreach (BalloonArtistFacadeInfo balloonArtistFacadeInfo in Blueprints.Get().all.balloonArtistFacades)
			{
				this.Add(balloonArtistFacadeInfo.id, balloonArtistFacadeInfo.name, balloonArtistFacadeInfo.desc, balloonArtistFacadeInfo.rarity, balloonArtistFacadeInfo.animFile, balloonArtistFacadeInfo.balloonFacadeType, balloonArtistFacadeInfo.GetRequiredDlcIds(), balloonArtistFacadeInfo.GetForbiddenDlcIds());
			}
		}

		// Token: 0x06007936 RID: 31030 RVA: 0x002EEC1C File Offset: 0x002ECE1C
		[Obsolete("Please use Add(...) with required/forbidden")]
		public void Add(string id, string name, string desc, PermitRarity rarity, string animFile, BalloonArtistFacadeType balloonFacadeType)
		{
			this.Add(id, name, desc, rarity, animFile, balloonFacadeType, null, null);
		}

		// Token: 0x06007937 RID: 31031 RVA: 0x002EEC3C File Offset: 0x002ECE3C
		[Obsolete("Please use Add(...) with required/forbidden")]
		public void Add(string id, string name, string desc, PermitRarity rarity, string animFile, BalloonArtistFacadeType balloonFacadeType, string[] dlcIds)
		{
			this.Add(id, name, desc, rarity, animFile, balloonFacadeType, null, null);
		}

		// Token: 0x06007938 RID: 31032 RVA: 0x002EEC5C File Offset: 0x002ECE5C
		public void Add(string id, string name, string desc, PermitRarity rarity, string animFile, BalloonArtistFacadeType balloonFacadeType, string[] requiredDlcIds, string[] forbiddenDlcIds)
		{
			BalloonArtistFacadeResource balloonArtistFacadeResource = new BalloonArtistFacadeResource(id, name, desc, rarity, animFile, balloonFacadeType, requiredDlcIds, forbiddenDlcIds);
			this.resources.Add(balloonArtistFacadeResource);
		}
	}
}
