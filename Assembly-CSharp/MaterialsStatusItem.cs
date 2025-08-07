using System;

// Token: 0x02000B9F RID: 2975
public class MaterialsStatusItem : StatusItem
{
	// Token: 0x060058C4 RID: 22724 RVA: 0x00201858 File Offset: 0x001FFA58
	public MaterialsStatusItem(string id, string prefix, string icon, StatusItem.IconType icon_type, NotificationType notification_type, bool allow_multiples, HashedString overlay)
		: base(id, prefix, icon, icon_type, notification_type, allow_multiples, overlay, true, 129022, null)
	{
	}
}
