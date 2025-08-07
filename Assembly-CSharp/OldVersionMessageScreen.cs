using System;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000D86 RID: 3462
[AddComponentMenu("KMonoBehaviour/scripts/SplashMessageScreen")]
public class OldVersionMessageScreen : KModalScreen
{
	// Token: 0x06006BBD RID: 27581 RVA: 0x0028A8F4 File Offset: 0x00288AF4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.forumButton.onClick += delegate
		{
			App.OpenWebURL("https://forums.kleientertainment.com/forums/topic/140474-previous-update-steam-branch-access/");
		};
		this.confirmButton.onClick += delegate
		{
			base.gameObject.SetActive(false);
			AudioMixer.instance.Stop(AudioMixerSnapshots.Get().FrontEndWelcomeScreenSnapshot, STOP_MODE.ALLOWFADEOUT);
		};
		this.quitButton.onClick += delegate
		{
			App.Quit();
		};
	}

	// Token: 0x06006BBE RID: 27582 RVA: 0x0028A974 File Offset: 0x00288B74
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.messageContainer.sizeDelta = new Vector2(Mathf.Max(384f, (float)Screen.width * 0.25f), this.messageContainer.sizeDelta.y);
		AudioMixer.instance.Start(AudioMixerSnapshots.Get().FrontEndWelcomeScreenSnapshot);
	}

	// Token: 0x04004963 RID: 18787
	public KButton forumButton;

	// Token: 0x04004964 RID: 18788
	public KButton confirmButton;

	// Token: 0x04004965 RID: 18789
	public KButton quitButton;

	// Token: 0x04004966 RID: 18790
	public LocText bodyText;

	// Token: 0x04004967 RID: 18791
	public bool previewInEditor;

	// Token: 0x04004968 RID: 18792
	public RectTransform messageContainer;
}
