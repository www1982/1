using System;
using UnityEngine;

// Token: 0x02000CC7 RID: 3271
public class EventInfoDataHelper
{
	// Token: 0x060064D4 RID: 25812 RVA: 0x0025E6AC File Offset: 0x0025C8AC
	public static EventInfoData GenerateStoryTraitData(string titleText, string descriptionText, string buttonText, string animFileName, EventInfoDataHelper.PopupType popupType, string buttonTooltip = null, GameObject[] minions = null, global::System.Action callback = null)
	{
		EventInfoData eventInfoData = new EventInfoData(titleText, descriptionText, animFileName);
		eventInfoData.minions = minions;
		if (popupType <= EventInfoDataHelper.PopupType.NORMAL || popupType != EventInfoDataHelper.PopupType.COMPLETE)
		{
			eventInfoData.showCallback = delegate
			{
				KFMOD.PlayUISound(GlobalAssets.GetSound("StoryTrait_Activation_Popup", false));
			};
		}
		else
		{
			eventInfoData.showCallback = delegate
			{
				MusicManager.instance.PlaySong("Stinger_StoryTraitUnlock", false);
			};
		}
		EventInfoData.Option option = eventInfoData.AddOption(buttonText, null);
		option.callback = callback;
		option.tooltip = buttonTooltip;
		return eventInfoData;
	}

	// Token: 0x02001E8E RID: 7822
	public enum PopupType
	{
		// Token: 0x04008DEB RID: 36331
		NONE = -1,
		// Token: 0x04008DEC RID: 36332
		BEGIN,
		// Token: 0x04008DED RID: 36333
		NORMAL,
		// Token: 0x04008DEE RID: 36334
		COMPLETE
	}
}
