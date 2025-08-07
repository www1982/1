using System;
using KSerialization;
using STRINGS;

// Token: 0x02000D58 RID: 3416
public class POITechResearchCompleteMessage : Message
{
	// Token: 0x060069EF RID: 27119 RVA: 0x002803B8 File Offset: 0x0027E5B8
	public POITechResearchCompleteMessage()
	{
	}

	// Token: 0x060069F0 RID: 27120 RVA: 0x002803C0 File Offset: 0x0027E5C0
	public POITechResearchCompleteMessage(POITechItemUnlocks.Def unlocked_items)
	{
		this.unlockedItemsdef = unlocked_items;
		this.popupName = unlocked_items.PopUpName;
		this.animName = unlocked_items.animName;
	}

	// Token: 0x060069F1 RID: 27121 RVA: 0x002803EC File Offset: 0x0027E5EC
	public override string GetSound()
	{
		return "AI_Notification_ResearchComplete";
	}

	// Token: 0x060069F2 RID: 27122 RVA: 0x002803F4 File Offset: 0x0027E5F4
	public override string GetMessageBody()
	{
		string text = "";
		for (int i = 0; i < this.unlockedItemsdef.POITechUnlockIDs.Count; i++)
		{
			TechItem techItem = Db.Get().TechItems.TryGet(this.unlockedItemsdef.POITechUnlockIDs[i]);
			if (techItem != null)
			{
				text = text + "\n    • " + techItem.Name;
			}
		}
		return string.Format(MISC.NOTIFICATIONS.POIRESEARCHUNLOCKCOMPLETE_NOLORE.MESSAGEBODY, text);
	}

	// Token: 0x060069F3 RID: 27123 RVA: 0x00280468 File Offset: 0x0027E668
	public override string GetTitle()
	{
		return MISC.NOTIFICATIONS.POIRESEARCHUNLOCKCOMPLETE_NOLORE.NAME;
	}

	// Token: 0x060069F4 RID: 27124 RVA: 0x00280474 File Offset: 0x0027E674
	public override string GetTooltip()
	{
		return string.Format(MISC.NOTIFICATIONS.POIRESEARCHUNLOCKCOMPLETE_NOLORE.TOOLTIP, this.popupName);
	}

	// Token: 0x060069F5 RID: 27125 RVA: 0x0028048B File Offset: 0x0027E68B
	public override bool IsValid()
	{
		return this.unlockedItemsdef != null;
	}

	// Token: 0x060069F6 RID: 27126 RVA: 0x00280496 File Offset: 0x0027E696
	public override bool ShowDialog()
	{
		EventInfoData eventInfoData = new EventInfoData(MISC.NOTIFICATIONS.POIRESEARCHUNLOCKCOMPLETE_NOLORE.NAME, this.GetMessageBody(), this.animName);
		eventInfoData.AddDefaultOption(null);
		EventInfoScreen.ShowPopup(eventInfoData);
		Messenger.Instance.RemoveMessage(this);
		return false;
	}

	// Token: 0x060069F7 RID: 27127 RVA: 0x002804D2 File Offset: 0x0027E6D2
	public override bool ShowDismissButton()
	{
		return false;
	}

	// Token: 0x060069F8 RID: 27128 RVA: 0x002804D5 File Offset: 0x0027E6D5
	public override NotificationType GetMessageType()
	{
		return NotificationType.Messages;
	}

	// Token: 0x04004858 RID: 18520
	[Serialize]
	public POITechItemUnlocks.Def unlockedItemsdef;

	// Token: 0x04004859 RID: 18521
	[Serialize]
	public string popupName;

	// Token: 0x0400485A RID: 18522
	[Serialize]
	public string animName;
}
