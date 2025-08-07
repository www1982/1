using System;
using UnityEngine;

// Token: 0x02000CE1 RID: 3297
public class HealthBar : ProgressBar
{
	// Token: 0x17000760 RID: 1888
	// (get) Token: 0x06006596 RID: 26006 RVA: 0x00264534 File Offset: 0x00262734
	private bool ShouldShow
	{
		get
		{
			return this.showTimer > 0f || base.PercentFull < this.alwaysShowThreshold;
		}
	}

	// Token: 0x06006597 RID: 26007 RVA: 0x00264553 File Offset: 0x00262753
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.barColor = ProgressBarsConfig.Instance.GetBarColor("HealthBar");
		base.gameObject.SetActive(this.ShouldShow);
	}

	// Token: 0x06006598 RID: 26008 RVA: 0x00264581 File Offset: 0x00262781
	public void OnChange()
	{
		base.enabled = true;
		this.showTimer = this.maxShowTime;
	}

	// Token: 0x06006599 RID: 26009 RVA: 0x00264598 File Offset: 0x00262798
	public override void Update()
	{
		base.Update();
		if (Time.timeScale > 0f)
		{
			this.showTimer = Mathf.Max(0f, this.showTimer - Time.unscaledDeltaTime);
		}
		if (!this.ShouldShow)
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600659A RID: 26010 RVA: 0x002645E7 File Offset: 0x002627E7
	private void OnBecameInvisible()
	{
		base.enabled = false;
	}

	// Token: 0x0600659B RID: 26011 RVA: 0x002645F0 File Offset: 0x002627F0
	private void OnBecameVisible()
	{
		base.enabled = true;
	}

	// Token: 0x0600659C RID: 26012 RVA: 0x002645FC File Offset: 0x002627FC
	public override void OnOverlayChanged(object data = null)
	{
		if (!this.autoHide)
		{
			return;
		}
		if ((HashedString)data == OverlayModes.None.ID)
		{
			if (!base.gameObject.activeSelf && this.ShouldShow)
			{
				base.enabled = true;
				base.gameObject.SetActive(true);
				return;
			}
		}
		else if (base.gameObject.activeSelf)
		{
			base.enabled = false;
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0400459C RID: 17820
	private float showTimer;

	// Token: 0x0400459D RID: 17821
	private float maxShowTime = 10f;

	// Token: 0x0400459E RID: 17822
	private float alwaysShowThreshold = 0.8f;
}
