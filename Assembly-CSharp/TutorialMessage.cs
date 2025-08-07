using System;
using KSerialization;

// Token: 0x02000D5F RID: 3423
public class TutorialMessage : GenericMessage, IHasDlcRestrictions
{
	// Token: 0x06006A1F RID: 27167 RVA: 0x00280A1E File Offset: 0x0027EC1E
	public string[] GetRequiredDlcIds()
	{
		return this.requiredDlcIds;
	}

	// Token: 0x06006A20 RID: 27168 RVA: 0x00280A26 File Offset: 0x0027EC26
	public string[] GetForbiddenDlcIds()
	{
		return this.forbiddenDlcIds;
	}

	// Token: 0x06006A21 RID: 27169 RVA: 0x00280A2E File Offset: 0x0027EC2E
	public TutorialMessage()
	{
	}

	// Token: 0x06006A22 RID: 27170 RVA: 0x00280A38 File Offset: 0x0027EC38
	public TutorialMessage(Tutorial.TutorialMessages messageId, string title, string body, string tooltip, string videoClipId = null, string videoOverlayName = null, string videoTitleText = null, string icon = "", string[] requiredDlcIds = null, string[] forbiddenDlcIds = null)
		: base(title, body, tooltip, null)
	{
		this.messageId = messageId;
		this.videoClipId = videoClipId;
		this.videoOverlayName = videoOverlayName;
		this.videoTitleText = videoTitleText;
		this.icon = icon;
		this.requiredDlcIds = requiredDlcIds;
		this.forbiddenDlcIds = forbiddenDlcIds;
	}

	// Token: 0x04004864 RID: 18532
	[Serialize]
	public Tutorial.TutorialMessages messageId;

	// Token: 0x04004865 RID: 18533
	public string videoClipId;

	// Token: 0x04004866 RID: 18534
	public string videoOverlayName;

	// Token: 0x04004867 RID: 18535
	public string videoTitleText;

	// Token: 0x04004868 RID: 18536
	public string icon;

	// Token: 0x04004869 RID: 18537
	public string[] requiredDlcIds;

	// Token: 0x0400486A RID: 18538
	public string[] forbiddenDlcIds;
}
