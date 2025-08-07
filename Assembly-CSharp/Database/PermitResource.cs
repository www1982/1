using System;

namespace Database
{
	// Token: 0x02000EFF RID: 3839
	public abstract class PermitResource : Resource, IHasDlcRestrictions
	{
		// Token: 0x060079B9 RID: 31161 RVA: 0x002FF994 File Offset: 0x002FDB94
		public PermitResource(string id, string Name, string Desc, PermitCategory permitCategory, PermitRarity rarity, string[] requiredDlcIds, string[] forbiddenDlcIds)
			: base(id, Name)
		{
			DebugUtil.DevAssert(Name != null, "Name must be provided for permit with id \"" + id + "\" of type " + base.GetType().Name, null);
			DebugUtil.DevAssert(Desc != null, "Description must be provided for permit with id \"" + id + "\" of type " + base.GetType().Name, null);
			this.Description = Desc;
			this.Category = permitCategory;
			this.Rarity = rarity;
			this.requiredDlcIds = requiredDlcIds;
			this.forbiddenDlcIds = forbiddenDlcIds;
		}

		// Token: 0x060079BA RID: 31162
		public abstract PermitPresentationInfo GetPermitPresentationInfo();

		// Token: 0x060079BB RID: 31163 RVA: 0x002FFA1A File Offset: 0x002FDC1A
		public bool IsOwnableOnServer()
		{
			return this.Rarity != PermitRarity.Universal && this.Rarity != PermitRarity.UniversalLocked;
		}

		// Token: 0x060079BC RID: 31164 RVA: 0x002FFA33 File Offset: 0x002FDC33
		public bool IsUnlocked()
		{
			return this.Rarity == PermitRarity.Universal || PermitItems.IsPermitUnlocked(this);
		}

		// Token: 0x060079BD RID: 31165 RVA: 0x002FFA46 File Offset: 0x002FDC46
		public string GetDlcIdFrom()
		{
			return DlcManager.GetMostSignificantDlc(this);
		}

		// Token: 0x060079BE RID: 31166 RVA: 0x002FFA4E File Offset: 0x002FDC4E
		public string[] GetRequiredDlcIds()
		{
			return this.requiredDlcIds;
		}

		// Token: 0x060079BF RID: 31167 RVA: 0x002FFA56 File Offset: 0x002FDC56
		public string[] GetForbiddenDlcIds()
		{
			return this.forbiddenDlcIds;
		}

		// Token: 0x04005889 RID: 22665
		public string Description;

		// Token: 0x0400588A RID: 22666
		public PermitCategory Category;

		// Token: 0x0400588B RID: 22667
		public PermitRarity Rarity;

		// Token: 0x0400588C RID: 22668
		public string[] requiredDlcIds;

		// Token: 0x0400588D RID: 22669
		public string[] forbiddenDlcIds;
	}
}
