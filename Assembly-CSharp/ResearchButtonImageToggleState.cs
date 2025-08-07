using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C33 RID: 3123
public class ResearchButtonImageToggleState : ImageToggleState
{
	// Token: 0x06005ED9 RID: 24281 RVA: 0x0022C7D0 File Offset: 0x0022A9D0
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		Research.Instance.Subscribe(-1914338957, new Action<object>(this.UpdateActiveResearch));
		Research.Instance.Subscribe(-125623018, new Action<object>(this.RefreshProgressBar));
		this.toggle = base.GetComponent<KToggle>();
	}

	// Token: 0x06005EDA RID: 24282 RVA: 0x0022C827 File Offset: 0x0022AA27
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.UpdateActiveResearch(null);
		this.RestartCoroutine();
	}

	// Token: 0x06005EDB RID: 24283 RVA: 0x0022C83C File Offset: 0x0022AA3C
	protected override void OnCleanUp()
	{
		this.AbortCoroutine();
		Research.Instance.Unsubscribe(-1914338957, new Action<object>(this.UpdateActiveResearch));
		Research.Instance.Unsubscribe(-125623018, new Action<object>(this.RefreshProgressBar));
		base.OnCleanUp();
	}

	// Token: 0x06005EDC RID: 24284 RVA: 0x0022C88B File Offset: 0x0022AA8B
	protected override void OnCmpEnable()
	{
		base.OnCmpEnable();
		this.RestartCoroutine();
	}

	// Token: 0x06005EDD RID: 24285 RVA: 0x0022C899 File Offset: 0x0022AA99
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		this.AbortCoroutine();
	}

	// Token: 0x06005EDE RID: 24286 RVA: 0x0022C8A7 File Offset: 0x0022AAA7
	private void AbortCoroutine()
	{
		if (this.scrollIconCoroutine != null)
		{
			base.StopCoroutine(this.scrollIconCoroutine);
		}
		this.scrollIconCoroutine = null;
	}

	// Token: 0x06005EDF RID: 24287 RVA: 0x0022C8C4 File Offset: 0x0022AAC4
	private void RestartCoroutine()
	{
		this.AbortCoroutine();
		if (base.gameObject.activeInHierarchy)
		{
			this.scrollIconCoroutine = base.StartCoroutine(this.ScrollIcon());
		}
	}

	// Token: 0x06005EE0 RID: 24288 RVA: 0x0022C8EC File Offset: 0x0022AAEC
	private void UpdateActiveResearch(object o)
	{
		TechInstance activeResearch = Research.Instance.GetActiveResearch();
		if (activeResearch == null)
		{
			this.currentResearchIcons = null;
		}
		else
		{
			this.currentResearchIcons = new Sprite[activeResearch.tech.unlockedItems.Count];
			for (int i = 0; i < activeResearch.tech.unlockedItems.Count; i++)
			{
				TechItem techItem = activeResearch.tech.unlockedItems[i];
				this.currentResearchIcons[i] = techItem.UISprite();
			}
		}
		this.ResetCoroutineTimers();
		this.RefreshProgressBar(o);
	}

	// Token: 0x06005EE1 RID: 24289 RVA: 0x0022C974 File Offset: 0x0022AB74
	public void RefreshProgressBar(object o)
	{
		TechInstance activeResearch = Research.Instance.GetActiveResearch();
		if (activeResearch == null)
		{
			this.progressBar.fillAmount = 0f;
			return;
		}
		this.progressBar.fillAmount = activeResearch.GetTotalPercentageComplete();
	}

	// Token: 0x06005EE2 RID: 24290 RVA: 0x0022C9B1 File Offset: 0x0022ABB1
	public void SetProgressBarVisibility(bool viisble)
	{
		this.progressBar.enabled = viisble;
	}

	// Token: 0x06005EE3 RID: 24291 RVA: 0x0022C9BF File Offset: 0x0022ABBF
	public override void SetActive()
	{
		base.SetActive();
		this.SetProgressBarVisibility(false);
	}

	// Token: 0x06005EE4 RID: 24292 RVA: 0x0022C9CE File Offset: 0x0022ABCE
	public override void SetDisabledActive()
	{
		base.SetDisabledActive();
		this.SetProgressBarVisibility(false);
	}

	// Token: 0x06005EE5 RID: 24293 RVA: 0x0022C9DD File Offset: 0x0022ABDD
	public override void SetDisabled()
	{
		base.SetDisabled();
		this.SetProgressBarVisibility(false);
	}

	// Token: 0x06005EE6 RID: 24294 RVA: 0x0022C9EC File Offset: 0x0022ABEC
	public override void SetInactive()
	{
		base.SetInactive();
		this.SetProgressBarVisibility(true);
		this.RefreshProgressBar(null);
	}

	// Token: 0x06005EE7 RID: 24295 RVA: 0x0022CA02 File Offset: 0x0022AC02
	private void ResetCoroutineTimers()
	{
		this.mainIconScreenTime = 0f;
		this.itemScreenTime = 0f;
		this.item_idx = -1;
	}

	// Token: 0x170006E6 RID: 1766
	// (get) Token: 0x06005EE8 RID: 24296 RVA: 0x0022CA21 File Offset: 0x0022AC21
	private bool ReadyToDisplayIcons
	{
		get
		{
			return this.progressBar.enabled && this.currentResearchIcons != null && this.item_idx >= 0 && this.item_idx < this.currentResearchIcons.Length;
		}
	}

	// Token: 0x06005EE9 RID: 24297 RVA: 0x0022CA53 File Offset: 0x0022AC53
	private IEnumerator ScrollIcon()
	{
		while (Application.isPlaying)
		{
			if (this.mainIconScreenTime < this.researchLogoDuration)
			{
				this.toggle.fgImage.Opacity(1f);
				if (this.toggle.fgImage.overrideSprite != null)
				{
					this.toggle.fgImage.overrideSprite = null;
				}
				this.item_idx = 0;
				this.itemScreenTime = 0f;
				this.mainIconScreenTime += Time.unscaledDeltaTime;
				if (this.progressBar.enabled && this.mainIconScreenTime >= this.researchLogoDuration && this.ReadyToDisplayIcons)
				{
					yield return this.toggle.fgImage.FadeAway(this.fadingDuration, () => this.progressBar.enabled && this.mainIconScreenTime >= this.researchLogoDuration && this.ReadyToDisplayIcons);
				}
				yield return null;
			}
			else if (this.ReadyToDisplayIcons)
			{
				if (this.toggle.fgImage.overrideSprite != this.currentResearchIcons[this.item_idx])
				{
					this.toggle.fgImage.overrideSprite = this.currentResearchIcons[this.item_idx];
				}
				yield return this.toggle.fgImage.FadeToVisible(this.fadingDuration, () => this.ReadyToDisplayIcons);
				while (this.itemScreenTime < this.durationPerResearchItemIcon && this.ReadyToDisplayIcons)
				{
					this.itemScreenTime += Time.unscaledDeltaTime;
					yield return null;
				}
				yield return this.toggle.fgImage.FadeAway(this.fadingDuration, () => this.ReadyToDisplayIcons);
				if (this.ReadyToDisplayIcons)
				{
					this.itemScreenTime = 0f;
					this.item_idx++;
				}
				yield return null;
			}
			else
			{
				this.mainIconScreenTime = 0f;
				yield return null;
			}
		}
		yield break;
	}

	// Token: 0x04003F44 RID: 16196
	public Image progressBar;

	// Token: 0x04003F45 RID: 16197
	private KToggle toggle;

	// Token: 0x04003F46 RID: 16198
	[Header("Scroll Options")]
	public float researchLogoDuration = 5f;

	// Token: 0x04003F47 RID: 16199
	public float durationPerResearchItemIcon = 0.6f;

	// Token: 0x04003F48 RID: 16200
	public float fadingDuration = 0.2f;

	// Token: 0x04003F49 RID: 16201
	private Coroutine scrollIconCoroutine;

	// Token: 0x04003F4A RID: 16202
	private Sprite[] currentResearchIcons;

	// Token: 0x04003F4B RID: 16203
	private float mainIconScreenTime;

	// Token: 0x04003F4C RID: 16204
	private float itemScreenTime;

	// Token: 0x04003F4D RID: 16205
	private int item_idx = -1;
}
