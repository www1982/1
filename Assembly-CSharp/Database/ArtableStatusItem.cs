using System;

namespace Database
{
	// Token: 0x02000ED2 RID: 3794
	public class ArtableStatusItem : StatusItem
	{
		// Token: 0x06007923 RID: 31011 RVA: 0x002ED51C File Offset: 0x002EB71C
		public ArtableStatusItem(string id, ArtableStatuses.ArtableStatusType statusType)
			: base(id, "BUILDING", "", StatusItem.IconType.Info, NotificationType.Neutral, false, OverlayModes.None.ID, true, 129022, null)
		{
			this.StatusType = statusType;
		}

		// Token: 0x04005421 RID: 21537
		public ArtableStatuses.ArtableStatusType StatusType;
	}
}
