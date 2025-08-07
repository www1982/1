using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x02000E7A RID: 3706
public class VideoScreen : KModalScreen
{
	// Token: 0x06007623 RID: 30243 RVA: 0x002D347C File Offset: 0x002D167C
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.ConsumeMouseScroll = true;
		this.closeButton.onClick += delegate
		{
			this.Stop();
		};
		this.proceedButton.onClick += delegate
		{
			this.Stop();
		};
		this.videoPlayer.isLooping = false;
		this.videoPlayer.loopPointReached += delegate(VideoPlayer data)
		{
			if (this.victoryLoopQueued)
			{
				base.StartCoroutine(this.SwitchToVictoryLoop());
				return;
			}
			if (!this.videoPlayer.isLooping)
			{
				this.Stop();
			}
		};
		VideoScreen.Instance = this;
		this.Show(false);
	}

	// Token: 0x06007624 RID: 30244 RVA: 0x002D34F4 File Offset: 0x002D16F4
	protected override void OnForcedCleanUp()
	{
		VideoScreen.Instance = null;
		base.OnForcedCleanUp();
	}

	// Token: 0x06007625 RID: 30245 RVA: 0x002D3502 File Offset: 0x002D1702
	protected override void OnShow(bool show)
	{
		base.transform.SetAsLastSibling();
		base.OnShow(show);
		this.screen = this.videoPlayer.gameObject.GetComponent<RawImage>();
	}

	// Token: 0x06007626 RID: 30246 RVA: 0x002D352C File Offset: 0x002D172C
	public void DisableAllMedia()
	{
		this.overlayContainer.gameObject.SetActive(false);
		this.videoPlayer.gameObject.SetActive(false);
		this.slideshow.gameObject.SetActive(false);
	}

	// Token: 0x06007627 RID: 30247 RVA: 0x002D3564 File Offset: 0x002D1764
	public void PlaySlideShow(Sprite[] sprites)
	{
		this.Show(true);
		this.DisableAllMedia();
		this.slideshow.updateType = SlideshowUpdateType.preloadedSprites;
		this.slideshow.gameObject.SetActive(true);
		this.slideshow.SetSprites(sprites);
		this.slideshow.SetPaused(false);
	}

	// Token: 0x06007628 RID: 30248 RVA: 0x002D35B4 File Offset: 0x002D17B4
	public void PlaySlideShow(string[] files)
	{
		this.Show(true);
		this.DisableAllMedia();
		this.slideshow.updateType = SlideshowUpdateType.loadOnDemand;
		this.slideshow.gameObject.SetActive(true);
		this.slideshow.SetFiles(files, 0);
		this.slideshow.SetPaused(false);
	}

	// Token: 0x06007629 RID: 30249 RVA: 0x002D3604 File Offset: 0x002D1804
	public override void OnKeyDown(KButtonEvent e)
	{
		if (e.IsAction(global::Action.Escape))
		{
			if (this.slideshow.gameObject.activeSelf && e.TryConsume(global::Action.Escape))
			{
				this.Stop();
				return;
			}
			if (e.TryConsume(global::Action.Escape))
			{
				if (this.videoSkippable)
				{
					this.Stop();
				}
				return;
			}
		}
		base.OnKeyDown(e);
	}

	// Token: 0x0600762A RID: 30250 RVA: 0x002D365C File Offset: 0x002D185C
	public void PlayVideo(VideoClip clip, bool unskippable = false, EventReference overrideAudioSnapshot = default(EventReference), bool showProceedButton = false, bool syncAudio = true)
	{
		global::Debug.Assert(clip != null);
		for (int i = 0; i < this.overlayContainer.childCount; i++)
		{
			global::UnityEngine.Object.Destroy(this.overlayContainer.GetChild(i).gameObject);
		}
		this.Show(true);
		this.videoPlayer.isLooping = false;
		this.activeAudioSnapshot = (overrideAudioSnapshot.IsNull ? AudioMixerSnapshots.Get().TutorialVideoPlayingSnapshot : overrideAudioSnapshot);
		AudioMixer.instance.Start(this.activeAudioSnapshot);
		this.DisableAllMedia();
		this.videoPlayer.gameObject.SetActive(true);
		this.renderTexture = new RenderTexture(Convert.ToInt32(clip.width), Convert.ToInt32(clip.height), 16);
		this.screen.texture = this.renderTexture;
		this.videoPlayer.targetTexture = this.renderTexture;
		this.videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
		this.videoPlayer.clip = clip;
		this.videoPlayer.timeReference = (syncAudio ? VideoTimeReference.ExternalTime : VideoTimeReference.Freerun);
		this.videoPlayer.Play();
		if (this.audioHandle.isValid())
		{
			KFMOD.EndOneShot(this.audioHandle);
			this.audioHandle.clearHandle();
		}
		this.audioHandle = KFMOD.BeginOneShot(GlobalAssets.GetSound("vid_" + clip.name, false), Vector3.zero, 1f);
		KFMOD.EndOneShot(this.audioHandle);
		this.videoSkippable = !unskippable;
		this.closeButton.gameObject.SetActive(this.videoSkippable);
		this.proceedButton.gameObject.SetActive(showProceedButton && this.videoSkippable);
	}

	// Token: 0x0600762B RID: 30251 RVA: 0x002D380C File Offset: 0x002D1A0C
	public void QueueVictoryVideoLoop(bool queue, string message = "", string victoryAchievement = "", string loopVideo = "", bool showAchievements = true, bool syncAudio = false)
	{
		this.victoryLoopQueued = queue;
		this.victoryLoopMessage = message;
		this.victoryLoopClip = loopVideo;
		this.victoryLoopSyncAudio = syncAudio;
		this.OnStop = (global::System.Action)Delegate.Combine(this.OnStop, new global::System.Action(delegate
		{
			if (showAchievements)
			{
				RetireColonyUtility.SaveColonySummaryData();
				MainMenu.ActivateRetiredColoniesScreenFromData(this.transform.parent.gameObject, RetireColonyUtility.GetCurrentColonyRetiredColonyData());
			}
		}));
	}

	// Token: 0x0600762C RID: 30252 RVA: 0x002D3870 File Offset: 0x002D1A70
	public void SetOverlayText(string overlayTemplate, List<string> strings)
	{
		VideoOverlay videoOverlay = null;
		foreach (VideoOverlay videoOverlay2 in this.overlayPrefabs)
		{
			if (videoOverlay2.name == overlayTemplate)
			{
				videoOverlay = videoOverlay2;
				break;
			}
		}
		DebugUtil.Assert(videoOverlay != null, "Could not find a template named ", overlayTemplate);
		global::Util.KInstantiateUI<VideoOverlay>(videoOverlay.gameObject, this.overlayContainer.gameObject, true).SetText(strings);
		this.overlayContainer.gameObject.SetActive(true);
	}

	// Token: 0x0600762D RID: 30253 RVA: 0x002D3910 File Offset: 0x002D1B10
	private IEnumerator SwitchToVictoryLoop()
	{
		this.victoryLoopQueued = false;
		Color color = this.fadeOverlay.color;
		for (float i = 0f; i < 1f; i += Time.unscaledDeltaTime)
		{
			this.fadeOverlay.color = new Color(color.r, color.g, color.b, i);
			yield return SequenceUtil.WaitForNextFrame;
		}
		this.fadeOverlay.color = new Color(color.r, color.g, color.b, 1f);
		MusicManager.instance.PlaySong("Music_Victory_03_StoryAndSummary", false);
		MusicManager.instance.SetSongParameter("Music_Victory_03_StoryAndSummary", "songSection", 1f, true);
		this.closeButton.gameObject.SetActive(true);
		this.proceedButton.gameObject.SetActive(true);
		this.SetOverlayText("VictoryEnd", new List<string> { this.victoryLoopMessage });
		this.videoPlayer.clip = Assets.GetVideo(this.victoryLoopClip);
		this.videoPlayer.isLooping = true;
		this.videoPlayer.Play();
		this.proceedButton.gameObject.SetActive(true);
		this.videoPlayer.timeReference = (this.victoryLoopSyncAudio ? VideoTimeReference.ExternalTime : VideoTimeReference.Freerun);
		yield return SequenceUtil.WaitForSecondsRealtime(1f);
		for (float i = 1f; i >= 0f; i -= Time.unscaledDeltaTime)
		{
			this.fadeOverlay.color = new Color(color.r, color.g, color.b, i);
			yield return SequenceUtil.WaitForNextFrame;
		}
		this.fadeOverlay.color = new Color(color.r, color.g, color.b, 0f);
		yield break;
	}

	// Token: 0x0600762E RID: 30254 RVA: 0x002D3920 File Offset: 0x002D1B20
	public void Stop()
	{
		this.videoPlayer.Stop();
		this.screen.texture = null;
		this.videoPlayer.targetTexture = null;
		if (!this.activeAudioSnapshot.IsNull)
		{
			AudioMixer.instance.Stop(this.activeAudioSnapshot, FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			this.audioHandle.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		}
		if (this.OnStop != null)
		{
			this.OnStop();
		}
		this.Show(false);
	}

	// Token: 0x0600762F RID: 30255 RVA: 0x002D3998 File Offset: 0x002D1B98
	public override void ScreenUpdate(bool topLevel)
	{
		base.ScreenUpdate(topLevel);
		if (this.audioHandle.isValid())
		{
			int num;
			this.audioHandle.getTimelinePosition(out num);
			this.videoPlayer.externalReferenceTime = (double)((float)num / 1000f);
		}
	}

	// Token: 0x040051EF RID: 20975
	public static VideoScreen Instance;

	// Token: 0x040051F0 RID: 20976
	[SerializeField]
	private VideoPlayer videoPlayer;

	// Token: 0x040051F1 RID: 20977
	[SerializeField]
	private Slideshow slideshow;

	// Token: 0x040051F2 RID: 20978
	[SerializeField]
	private KButton closeButton;

	// Token: 0x040051F3 RID: 20979
	[SerializeField]
	private KButton proceedButton;

	// Token: 0x040051F4 RID: 20980
	[SerializeField]
	private RectTransform overlayContainer;

	// Token: 0x040051F5 RID: 20981
	[SerializeField]
	private List<VideoOverlay> overlayPrefabs;

	// Token: 0x040051F6 RID: 20982
	private RawImage screen;

	// Token: 0x040051F7 RID: 20983
	private RenderTexture renderTexture;

	// Token: 0x040051F8 RID: 20984
	private EventReference activeAudioSnapshot;

	// Token: 0x040051F9 RID: 20985
	[SerializeField]
	private Image fadeOverlay;

	// Token: 0x040051FA RID: 20986
	private EventInstance audioHandle;

	// Token: 0x040051FB RID: 20987
	private bool victoryLoopQueued;

	// Token: 0x040051FC RID: 20988
	private string victoryLoopMessage = "";

	// Token: 0x040051FD RID: 20989
	private string victoryLoopClip = "";

	// Token: 0x040051FE RID: 20990
	private bool victoryLoopSyncAudio;

	// Token: 0x040051FF RID: 20991
	private bool videoSkippable = true;

	// Token: 0x04005200 RID: 20992
	public global::System.Action OnStop;
}
