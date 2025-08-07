using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

// Token: 0x02000E7B RID: 3707
[AddComponentMenu("KMonoBehaviour/scripts/VideoWidget")]
public class VideoWidget : KMonoBehaviour
{
	// Token: 0x06007634 RID: 30260 RVA: 0x002D3A3B File Offset: 0x002D1C3B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.button.onClick += this.Clicked;
		this.rawImage = this.thumbnailPlayer.GetComponent<RawImage>();
	}

	// Token: 0x06007635 RID: 30261 RVA: 0x002D3A6C File Offset: 0x002D1C6C
	private void Clicked()
	{
		VideoScreen.Instance.PlayVideo(this.clip, false, default(EventReference), false, true);
		if (!string.IsNullOrEmpty(this.overlayName))
		{
			VideoScreen.Instance.SetOverlayText(this.overlayName, this.texts);
		}
	}

	// Token: 0x06007636 RID: 30262 RVA: 0x002D3AB8 File Offset: 0x002D1CB8
	public void SetClip(VideoClip clip, string overlayName = null, List<string> texts = null)
	{
		if (clip == null)
		{
			global::Debug.LogWarning("Tried to assign null video clip to VideoWidget");
			return;
		}
		this.clip = clip;
		this.overlayName = overlayName;
		this.texts = texts;
		this.renderTexture = new RenderTexture(Convert.ToInt32(clip.width), Convert.ToInt32(clip.height), 16);
		this.thumbnailPlayer.targetTexture = this.renderTexture;
		this.rawImage.texture = this.renderTexture;
		base.StartCoroutine(this.ConfigureThumbnail());
	}

	// Token: 0x06007637 RID: 30263 RVA: 0x002D3B40 File Offset: 0x002D1D40
	private IEnumerator ConfigureThumbnail()
	{
		this.thumbnailPlayer.audioOutputMode = VideoAudioOutputMode.None;
		this.thumbnailPlayer.clip = this.clip;
		this.thumbnailPlayer.time = 0.0;
		this.thumbnailPlayer.Play();
		yield return null;
		yield break;
	}

	// Token: 0x06007638 RID: 30264 RVA: 0x002D3B4F File Offset: 0x002D1D4F
	private void Update()
	{
		if (this.thumbnailPlayer.isPlaying && this.thumbnailPlayer.time > 2.0)
		{
			this.thumbnailPlayer.Pause();
		}
	}

	// Token: 0x04005201 RID: 20993
	[SerializeField]
	private VideoClip clip;

	// Token: 0x04005202 RID: 20994
	[SerializeField]
	private VideoPlayer thumbnailPlayer;

	// Token: 0x04005203 RID: 20995
	[SerializeField]
	private KButton button;

	// Token: 0x04005204 RID: 20996
	[SerializeField]
	private string overlayName;

	// Token: 0x04005205 RID: 20997
	[SerializeField]
	private List<string> texts;

	// Token: 0x04005206 RID: 20998
	private RenderTexture renderTexture;

	// Token: 0x04005207 RID: 20999
	private RawImage rawImage;
}
