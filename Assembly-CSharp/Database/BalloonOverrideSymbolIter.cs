using System;
using UnityEngine;

namespace Database
{
	// Token: 0x02000EDD RID: 3805
	public class BalloonOverrideSymbolIter
	{
		// Token: 0x06007946 RID: 31046 RVA: 0x002EEEBC File Offset: 0x002ED0BC
		public BalloonOverrideSymbolIter(Option<BalloonArtistFacadeResource> facade)
		{
			global::Debug.Assert(facade.IsNone() || facade.Unwrap().balloonOverrideSymbolIDs.Length != 0);
			this.facade = facade;
			if (facade.IsSome())
			{
				this.index = global::UnityEngine.Random.Range(0, facade.Unwrap().balloonOverrideSymbolIDs.Length);
			}
			this.Next();
		}

		// Token: 0x06007947 RID: 31047 RVA: 0x002EEF21 File Offset: 0x002ED121
		public BalloonOverrideSymbol Current()
		{
			return this.current;
		}

		// Token: 0x06007948 RID: 31048 RVA: 0x002EEF2C File Offset: 0x002ED12C
		public BalloonOverrideSymbol Next()
		{
			if (this.facade.IsSome())
			{
				BalloonArtistFacadeResource balloonArtistFacadeResource = this.facade.Unwrap();
				this.current = new BalloonOverrideSymbol(balloonArtistFacadeResource.animFilename, balloonArtistFacadeResource.balloonOverrideSymbolIDs[this.index]);
				this.index = (this.index + 1) % balloonArtistFacadeResource.balloonOverrideSymbolIDs.Length;
				return this.current;
			}
			return default(BalloonOverrideSymbol);
		}

		// Token: 0x04005484 RID: 21636
		public readonly Option<BalloonArtistFacadeResource> facade;

		// Token: 0x04005485 RID: 21637
		private BalloonOverrideSymbol current;

		// Token: 0x04005486 RID: 21638
		private int index;
	}
}
