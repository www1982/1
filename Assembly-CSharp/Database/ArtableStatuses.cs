using System;

namespace Database
{
	// Token: 0x02000ED1 RID: 3793
	public class ArtableStatuses : ResourceSet<ArtableStatusItem>
	{
		// Token: 0x06007921 RID: 31009 RVA: 0x002ED494 File Offset: 0x002EB694
		public ArtableStatuses(ResourceSet parent)
			: base("ArtableStatuses", parent)
		{
			this.AwaitingArting = this.Add("AwaitingArting", ArtableStatuses.ArtableStatusType.AwaitingArting);
			this.LookingUgly = this.Add("LookingUgly", ArtableStatuses.ArtableStatusType.LookingUgly);
			this.LookingOkay = this.Add("LookingOkay", ArtableStatuses.ArtableStatusType.LookingOkay);
			this.LookingGreat = this.Add("LookingGreat", ArtableStatuses.ArtableStatusType.LookingGreat);
		}

		// Token: 0x06007922 RID: 31010 RVA: 0x002ED4F8 File Offset: 0x002EB6F8
		public ArtableStatusItem Add(string id, ArtableStatuses.ArtableStatusType statusType)
		{
			ArtableStatusItem artableStatusItem = new ArtableStatusItem(id, statusType);
			this.resources.Add(artableStatusItem);
			return artableStatusItem;
		}

		// Token: 0x0400541D RID: 21533
		public ArtableStatusItem AwaitingArting;

		// Token: 0x0400541E RID: 21534
		public ArtableStatusItem LookingUgly;

		// Token: 0x0400541F RID: 21535
		public ArtableStatusItem LookingOkay;

		// Token: 0x04005420 RID: 21536
		public ArtableStatusItem LookingGreat;

		// Token: 0x020020D6 RID: 8406
		public enum ArtableStatusType
		{
			// Token: 0x04009565 RID: 38245
			AwaitingArting,
			// Token: 0x04009566 RID: 38246
			LookingUgly,
			// Token: 0x04009567 RID: 38247
			LookingOkay,
			// Token: 0x04009568 RID: 38248
			LookingGreat
		}
	}
}
