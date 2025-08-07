using System;
using STRINGS;

namespace Database
{
	// Token: 0x02000EF7 RID: 3831
	public class MonumentPartResource : PermitResource
	{
		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x060079A2 RID: 31138 RVA: 0x002FEECD File Offset: 0x002FD0CD
		// (set) Token: 0x060079A3 RID: 31139 RVA: 0x002FEED5 File Offset: 0x002FD0D5
		public KAnimFile AnimFile { get; private set; }

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x060079A4 RID: 31140 RVA: 0x002FEEDE File Offset: 0x002FD0DE
		// (set) Token: 0x060079A5 RID: 31141 RVA: 0x002FEEE6 File Offset: 0x002FD0E6
		public string SymbolName { get; private set; }

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x060079A6 RID: 31142 RVA: 0x002FEEEF File Offset: 0x002FD0EF
		// (set) Token: 0x060079A7 RID: 31143 RVA: 0x002FEEF7 File Offset: 0x002FD0F7
		public string State { get; private set; }

		// Token: 0x060079A8 RID: 31144 RVA: 0x002FEF00 File Offset: 0x002FD100
		public MonumentPartResource(string id, string name, string desc, PermitRarity rarity, string animFilename, string state, string symbolName, MonumentPartResource.Part part, string[] requiredDlcIds, string[] forbiddenDlcIds)
			: base(id, name, desc, PermitCategory.Artwork, rarity, requiredDlcIds, forbiddenDlcIds)
		{
			this.AnimFile = Assets.GetAnim(animFilename);
			this.SymbolName = symbolName;
			this.State = state;
			this.part = part;
		}

		// Token: 0x060079A9 RID: 31145 RVA: 0x002FEF40 File Offset: 0x002FD140
		public override PermitPresentationInfo GetPermitPresentationInfo()
		{
			PermitPresentationInfo permitPresentationInfo = default(PermitPresentationInfo);
			permitPresentationInfo.sprite = Def.GetUISpriteFromMultiObjectAnim(this.AnimFile, "ui", false, "");
			permitPresentationInfo.SetFacadeForText(UI.KLEI_INVENTORY_SCREEN.MONUMENT_PART_FACADE_FOR);
			return permitPresentationInfo;
		}

		// Token: 0x0400584E RID: 22606
		public MonumentPartResource.Part part;

		// Token: 0x020020E8 RID: 8424
		public enum Part
		{
			// Token: 0x040096B0 RID: 38576
			Bottom,
			// Token: 0x040096B1 RID: 38577
			Middle,
			// Token: 0x040096B2 RID: 38578
			Top
		}
	}
}
