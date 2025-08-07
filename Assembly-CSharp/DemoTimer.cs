using System;
using Klei;
using STRINGS;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CB4 RID: 3252
public class DemoTimer : MonoBehaviour
{
	// Token: 0x0600641E RID: 25630 RVA: 0x0025988B File Offset: 0x00257A8B
	public static void DestroyInstance()
	{
		DemoTimer.Instance = null;
	}

	// Token: 0x0600641F RID: 25631 RVA: 0x00259894 File Offset: 0x00257A94
	private void Start()
	{
		DemoTimer.Instance = this;
		if (GenericGameSettings.instance != null)
		{
			if (GenericGameSettings.instance.demoMode)
			{
				this.duration = (float)GenericGameSettings.instance.demoTime;
				this.labelText.gameObject.SetActive(GenericGameSettings.instance.showDemoTimer);
				this.clockImage.gameObject.SetActive(GenericGameSettings.instance.showDemoTimer);
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
		else
		{
			base.gameObject.SetActive(false);
		}
		this.duration = (float)GenericGameSettings.instance.demoTime;
		this.fadeOutScreen = Util.KInstantiateUI(this.Prefab_FadeOutScreen, GameScreenManager.Instance.ssOverlayCanvas.gameObject, false);
		Image component = this.fadeOutScreen.GetComponent<Image>();
		component.raycastTarget = false;
		this.fadeOutColor = component.color;
		this.fadeOutColor.a = 0f;
		this.fadeOutScreen.GetComponent<Image>().color = this.fadeOutColor;
	}

	// Token: 0x06006420 RID: 25632 RVA: 0x00259994 File Offset: 0x00257B94
	private void Update()
	{
		if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.BackQuote))
		{
			this.CountdownActive = !this.CountdownActive;
			this.UpdateLabel();
		}
		if (this.demoOver || !this.CountdownActive)
		{
			return;
		}
		if (this.beginTime == -1f)
		{
			this.beginTime = Time.unscaledTime;
		}
		this.elapsed = Mathf.Clamp(0f, Time.unscaledTime - this.beginTime, this.duration);
		if (this.elapsed + 5f >= this.duration)
		{
			float num = (this.duration - this.elapsed) / 5f;
			this.fadeOutColor.a = Mathf.Min(1f, 1f - Mathf.Sqrt(num));
			this.fadeOutScreen.GetComponent<Image>().color = this.fadeOutColor;
		}
		if (this.elapsed >= this.duration)
		{
			this.EndDemo();
		}
		this.UpdateLabel();
	}

	// Token: 0x06006421 RID: 25633 RVA: 0x00259A9C File Offset: 0x00257C9C
	private void UpdateLabel()
	{
		int num = Mathf.RoundToInt(this.duration - this.elapsed);
		int num2 = Mathf.FloorToInt((float)(num / 60));
		int num3 = num % 60;
		this.labelText.text = string.Concat(new string[]
		{
			UI.DEMOOVERSCREEN.TIMEREMAINING,
			" ",
			num2.ToString("00"),
			":",
			num3.ToString("00")
		});
		if (!this.CountdownActive)
		{
			this.labelText.text = UI.DEMOOVERSCREEN.TIMERINACTIVE;
		}
	}

	// Token: 0x06006422 RID: 25634 RVA: 0x00259B38 File Offset: 0x00257D38
	public void EndDemo()
	{
		if (this.demoOver)
		{
			return;
		}
		this.demoOver = true;
		Util.KInstantiateUI(this.Prefab_DemoOverScreen, GameScreenManager.Instance.ssOverlayCanvas.gameObject, false).GetComponent<DemoOverScreen>().Show(true);
	}

	// Token: 0x04004431 RID: 17457
	public static DemoTimer Instance;

	// Token: 0x04004432 RID: 17458
	public LocText labelText;

	// Token: 0x04004433 RID: 17459
	public Image clockImage;

	// Token: 0x04004434 RID: 17460
	public GameObject Prefab_DemoOverScreen;

	// Token: 0x04004435 RID: 17461
	public GameObject Prefab_FadeOutScreen;

	// Token: 0x04004436 RID: 17462
	private float duration;

	// Token: 0x04004437 RID: 17463
	private float elapsed;

	// Token: 0x04004438 RID: 17464
	private bool demoOver;

	// Token: 0x04004439 RID: 17465
	private float beginTime = -1f;

	// Token: 0x0400443A RID: 17466
	public bool CountdownActive;

	// Token: 0x0400443B RID: 17467
	private GameObject fadeOutScreen;

	// Token: 0x0400443C RID: 17468
	private Color fadeOutColor;
}
