using System;

namespace Database
{
	// Token: 0x02000EDC RID: 3804
	public readonly struct BalloonOverrideSymbol
	{
		// Token: 0x06007943 RID: 31043 RVA: 0x002EEE2C File Offset: 0x002ED02C
		public BalloonOverrideSymbol(string animFileID, string animFileSymbolID)
		{
			if (string.IsNullOrEmpty(animFileID) || string.IsNullOrEmpty(animFileSymbolID))
			{
				this = default(BalloonOverrideSymbol);
				return;
			}
			this.animFileID = animFileID;
			this.animFileSymbolID = animFileSymbolID;
			this.animFile = Assets.GetAnim(animFileID);
			this.symbol = this.animFile.Value.GetData().build.GetSymbol(animFileSymbolID);
		}

		// Token: 0x06007944 RID: 31044 RVA: 0x002EEEA0 File Offset: 0x002ED0A0
		public void ApplyTo(BalloonArtist.Instance artist)
		{
			artist.SetBalloonSymbolOverride(this);
		}

		// Token: 0x06007945 RID: 31045 RVA: 0x002EEEAE File Offset: 0x002ED0AE
		public void ApplyTo(BalloonFX.Instance balloon)
		{
			balloon.SetBalloonSymbolOverride(this);
		}

		// Token: 0x04005480 RID: 21632
		public readonly Option<KAnim.Build.Symbol> symbol;

		// Token: 0x04005481 RID: 21633
		public readonly Option<KAnimFile> animFile;

		// Token: 0x04005482 RID: 21634
		public readonly string animFileID;

		// Token: 0x04005483 RID: 21635
		public readonly string animFileSymbolID;
	}
}
