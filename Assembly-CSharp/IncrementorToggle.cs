using System;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000CEA RID: 3306
public class IncrementorToggle : MultiToggle
{
	// Token: 0x060065B5 RID: 26037 RVA: 0x00264AD4 File Offset: 0x00262CD4
	protected override void Update()
	{
		if (this.clickHeldDown)
		{
			this.totalHeldTime += Time.unscaledDeltaTime;
			if (this.timeToNextIncrement <= 0f)
			{
				this.PlayClickSound();
				this.onClick();
				this.timeToNextIncrement = Mathf.Lerp(this.timeBetweenIncrementsMax, this.timeBetweenIncrementsMin, this.totalHeldTime / 2.5f);
				return;
			}
			this.timeToNextIncrement -= Time.unscaledDeltaTime;
		}
	}

	// Token: 0x060065B6 RID: 26038 RVA: 0x00264B50 File Offset: 0x00262D50
	private void PlayClickSound()
	{
		if (this.play_sound_on_click)
		{
			if (this.states[this.state].on_click_override_sound_path == "")
			{
				KFMOD.PlayUISound(GlobalAssets.GetSound("HUD_Click", false));
				return;
			}
			KFMOD.PlayUISound(GlobalAssets.GetSound(this.states[this.state].on_click_override_sound_path, false));
		}
	}

	// Token: 0x060065B7 RID: 26039 RVA: 0x00264BB9 File Offset: 0x00262DB9
	public override void OnPointerUp(PointerEventData eventData)
	{
		base.OnPointerUp(eventData);
		this.timeToNextIncrement = this.timeBetweenIncrementsMax;
	}

	// Token: 0x060065B8 RID: 26040 RVA: 0x00264BD0 File Offset: 0x00262DD0
	public override void OnPointerDown(PointerEventData eventData)
	{
		if (!this.clickHeldDown)
		{
			this.clickHeldDown = true;
			this.PlayClickSound();
			if (this.onClick != null)
			{
				this.onClick();
			}
		}
		if (this.states.Length - 1 < this.state)
		{
			global::Debug.LogWarning("Multi toggle has too few / no states");
		}
		base.RefreshHoverColor();
	}

	// Token: 0x060065B9 RID: 26041 RVA: 0x00264C27 File Offset: 0x00262E27
	public override void OnPointerClick(PointerEventData eventData)
	{
		base.RefreshHoverColor();
	}

	// Token: 0x040045AA RID: 17834
	private float timeBetweenIncrementsMin = 0.033f;

	// Token: 0x040045AB RID: 17835
	private float timeBetweenIncrementsMax = 0.25f;

	// Token: 0x040045AC RID: 17836
	private const float incrementAccelerationScale = 2.5f;

	// Token: 0x040045AD RID: 17837
	private float timeToNextIncrement;
}
