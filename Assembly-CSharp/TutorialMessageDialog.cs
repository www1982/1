using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;
using UnityEngine.Video;

// Token: 0x02000D60 RID: 3424
public class TutorialMessageDialog : MessageDialog
{
	// Token: 0x17000785 RID: 1925
	// (get) Token: 0x06006A23 RID: 27171 RVA: 0x00280A87 File Offset: 0x0027EC87
	public override bool CanDontShowAgain
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06006A24 RID: 27172 RVA: 0x00280A8A File Offset: 0x0027EC8A
	public override bool CanDisplay(Message message)
	{
		return typeof(TutorialMessage).IsAssignableFrom(message.GetType());
	}

	// Token: 0x06006A25 RID: 27173 RVA: 0x00280AA4 File Offset: 0x0027ECA4
	public override void SetMessage(Message base_message)
	{
		this.message = base_message as TutorialMessage;
		this.description.text = this.message.GetMessageBody();
		if (!string.IsNullOrEmpty(this.message.videoClipId))
		{
			VideoClip video = Assets.GetVideo(this.message.videoClipId);
			this.SetVideo(video, this.message.videoOverlayName, this.message.videoTitleText);
		}
	}

	// Token: 0x06006A26 RID: 27174 RVA: 0x00280B14 File Offset: 0x0027ED14
	public void SetVideo(VideoClip clip, string overlayName, string titleText)
	{
		if (this.videoWidget == null)
		{
			this.videoWidget = Util.KInstantiateUI(this.videoWidgetPrefab, base.transform.gameObject, true).GetComponent<VideoWidget>();
			this.videoWidget.transform.SetAsFirstSibling();
		}
		this.videoWidget.SetClip(clip, overlayName, new List<string>
		{
			titleText,
			VIDEOS.TUTORIAL_HEADER
		});
	}

	// Token: 0x06006A27 RID: 27175 RVA: 0x00280B8A File Offset: 0x0027ED8A
	public override void OnClickAction()
	{
	}

	// Token: 0x06006A28 RID: 27176 RVA: 0x00280B8C File Offset: 0x0027ED8C
	public override void OnDontShowAgain()
	{
		Tutorial.Instance.HideTutorialMessage(this.message.messageId);
	}

	// Token: 0x0400486B RID: 18539
	[SerializeField]
	private LocText description;

	// Token: 0x0400486C RID: 18540
	private TutorialMessage message;

	// Token: 0x0400486D RID: 18541
	[SerializeField]
	private GameObject videoWidgetPrefab;

	// Token: 0x0400486E RID: 18542
	private VideoWidget videoWidget;
}
