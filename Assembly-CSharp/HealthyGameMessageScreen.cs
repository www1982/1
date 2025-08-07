using System;
using UnityEngine;

// Token: 0x02000CE2 RID: 3298
[AddComponentMenu("KMonoBehaviour/scripts/HealthyGameMessageScreen")]
public class HealthyGameMessageScreen : KMonoBehaviour
{
	// Token: 0x0600659E RID: 26014 RVA: 0x0026468B File Offset: 0x0026288B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.confirmButton.onClick += delegate
		{
			this.PlayIntroShort();
		};
		this.confirmButton.gameObject.SetActive(false);
	}

	// Token: 0x0600659F RID: 26015 RVA: 0x002646BC File Offset: 0x002628BC
	private void PlayIntroShort()
	{
		string @string = KPlayerPrefs.GetString("PlayShortOnLaunch", "");
		if (!string.IsNullOrEmpty(MainMenu.Instance.IntroShortName) && @string != MainMenu.Instance.IntroShortName)
		{
			VideoScreen component = KScreenManager.AddChild(FrontEndManager.Instance.gameObject, ScreenPrefabs.Instance.VideoScreen.gameObject).GetComponent<VideoScreen>();
			component.PlayVideo(Assets.GetVideo(MainMenu.Instance.IntroShortName), false, AudioMixerSnapshots.Get().MainMenuVideoPlayingSnapshot, false, true);
			component.OnStop = (global::System.Action)Delegate.Combine(component.OnStop, new global::System.Action(delegate
			{
				KPlayerPrefs.SetString("PlayShortOnLaunch", MainMenu.Instance.IntroShortName);
				if (base.gameObject != null)
				{
					global::UnityEngine.Object.Destroy(base.gameObject);
				}
			}));
			return;
		}
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060065A0 RID: 26016 RVA: 0x0026476E File Offset: 0x0026296E
	protected override void OnSpawn()
	{
		base.OnSpawn();
		global::UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x060065A1 RID: 26017 RVA: 0x00264784 File Offset: 0x00262984
	private void Update()
	{
		if (!DistributionPlatform.Inst.IsDLCStatusReady())
		{
			return;
		}
		if (this.isFirstUpdate)
		{
			this.isFirstUpdate = false;
			this.spawnTime = Time.unscaledTime;
			return;
		}
		float num = Mathf.Min(Time.unscaledDeltaTime, 0.033333335f);
		float num2 = Time.unscaledTime - this.spawnTime;
		if (num2 < this.totalTime - this.fadeTime)
		{
			this.canvasGroup.alpha = this.canvasGroup.alpha + num * (1f / this.fadeTime);
			return;
		}
		if (num2 >= this.totalTime + 0.75f)
		{
			this.canvasGroup.alpha = 1f;
			this.confirmButton.gameObject.SetActive(true);
			return;
		}
		if (num2 >= this.totalTime - this.fadeTime)
		{
			this.canvasGroup.alpha = this.canvasGroup.alpha - num * (1f / this.fadeTime);
		}
	}

	// Token: 0x0400459F RID: 17823
	public KButton confirmButton;

	// Token: 0x040045A0 RID: 17824
	public CanvasGroup canvasGroup;

	// Token: 0x040045A1 RID: 17825
	private float spawnTime;

	// Token: 0x040045A2 RID: 17826
	private float totalTime = 10f;

	// Token: 0x040045A3 RID: 17827
	private float fadeTime = 1.5f;

	// Token: 0x040045A4 RID: 17828
	private bool isFirstUpdate = true;
}
