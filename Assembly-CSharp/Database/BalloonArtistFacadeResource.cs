using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000EDB RID: 3803
	public class BalloonArtistFacadeResource : PermitResource
	{
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06007939 RID: 31033 RVA: 0x002EEC88 File Offset: 0x002ECE88
		// (set) Token: 0x0600793A RID: 31034 RVA: 0x002EEC90 File Offset: 0x002ECE90
		public string animFilename { get; private set; }

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x0600793B RID: 31035 RVA: 0x002EEC99 File Offset: 0x002ECE99
		// (set) Token: 0x0600793C RID: 31036 RVA: 0x002EECA1 File Offset: 0x002ECEA1
		public KAnimFile AnimFile { get; private set; }

		// Token: 0x0600793D RID: 31037 RVA: 0x002EECAC File Offset: 0x002ECEAC
		public BalloonArtistFacadeResource(string id, string name, string desc, PermitRarity rarity, string animFile, BalloonArtistFacadeType balloonFacadeType, string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
			: base(id, name, desc, PermitCategory.JoyResponse, rarity, requiredDlcIds, forbiddenDlcIds)
		{
			this.AnimFile = Assets.GetAnim(animFile);
			this.animFilename = animFile;
			this.balloonFacadeType = balloonFacadeType;
			Db.Get().Accessories.AddAccessories(id, this.AnimFile);
			this.balloonOverrideSymbolIDs = this.GetBalloonOverrideSymbolIDs();
			Debug.Assert(this.balloonOverrideSymbolIDs.Length != 0);
		}

		// Token: 0x0600793E RID: 31038 RVA: 0x002EED20 File Offset: 0x002ECF20
		public override PermitPresentationInfo GetPermitPresentationInfo()
		{
			PermitPresentationInfo permitPresentationInfo = default(PermitPresentationInfo);
			permitPresentationInfo.sprite = Def.GetUISpriteFromMultiObjectAnim(this.AnimFile, "ui", false, "");
			permitPresentationInfo.SetFacadeForText(UI.KLEI_INVENTORY_SCREEN.BALLOON_ARTIST_FACADE_FOR);
			return permitPresentationInfo;
		}

		// Token: 0x0600793F RID: 31039 RVA: 0x002EED64 File Offset: 0x002ECF64
		public BalloonOverrideSymbol GetNextOverride()
		{
			int num = this.nextSymbolIndex;
			this.nextSymbolIndex = (this.nextSymbolIndex + 1) % this.balloonOverrideSymbolIDs.Length;
			return new BalloonOverrideSymbol(this.animFilename, this.balloonOverrideSymbolIDs[num]);
		}

		// Token: 0x06007940 RID: 31040 RVA: 0x002EEDA2 File Offset: 0x002ECFA2
		public BalloonOverrideSymbolIter GetSymbolIter()
		{
			return new BalloonOverrideSymbolIter(this);
		}

		// Token: 0x06007941 RID: 31041 RVA: 0x002EEDAF File Offset: 0x002ECFAF
		public BalloonOverrideSymbol GetOverrideAt(int index)
		{
			return new BalloonOverrideSymbol(this.animFilename, this.balloonOverrideSymbolIDs[index]);
		}

		// Token: 0x06007942 RID: 31042 RVA: 0x002EEDC4 File Offset: 0x002ECFC4
		private string[] GetBalloonOverrideSymbolIDs()
		{
			KAnim.Build build = this.AnimFile.GetData().build;
			BalloonArtistFacadeType balloonArtistFacadeType = this.balloonFacadeType;
			string[] array;
			if (balloonArtistFacadeType != BalloonArtistFacadeType.Single)
			{
				if (balloonArtistFacadeType != BalloonArtistFacadeType.ThreeSet)
				{
					throw new NotImplementedException();
				}
				array = new string[] { "body1", "body2", "body3" };
			}
			else
			{
				array = new string[] { "body" };
			}
			return array;
		}

		// Token: 0x0400547D RID: 21629
		private BalloonArtistFacadeType balloonFacadeType;

		// Token: 0x0400547E RID: 21630
		public readonly string[] balloonOverrideSymbolIDs;

		// Token: 0x0400547F RID: 21631
		public int nextSymbolIndex;
	}
}
