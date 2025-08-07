using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000B4D RID: 2893
public class LargeImpactorUINotificationHitEffects : KMonoBehaviour, IRenderEveryTick
{
	// Token: 0x17000639 RID: 1593
	// (get) Token: 0x06005621 RID: 22049 RVA: 0x001F3BC2 File Offset: 0x001F1DC2
	private float Intensity
	{
		get
		{
			return this.timer / this.duration;
		}
	}

	// Token: 0x06005622 RID: 22050 RVA: 0x001F3BD1 File Offset: 0x001F1DD1
	public void PlayHitEffect()
	{
		this.timer = this.duration;
	}

	// Token: 0x06005623 RID: 22051 RVA: 0x001F3BE0 File Offset: 0x001F1DE0
	public void RenderEveryTick(float dt)
	{
		if (this.lastTimerValue != this.timer)
		{
			this.lastTimerValue = this.timer;
			this.hitBackgorund.Opacity(this.Intensity);
			this.heartIcon.color = Color.Lerp(this.heartIconOriginalColor, this.HighlightedColor, this.Intensity);
			this.healthBarFill.color = Color.Lerp(this.healthBarOriginalColor, this.HighlightedColor, this.Intensity);
			this.shake.SetIntensity(this.Intensity);
		}
		this.timer = Mathf.Clamp(this.timer - dt, 0f, this.duration);
	}

	// Token: 0x06005624 RID: 22052 RVA: 0x001F3C8B File Offset: 0x001F1E8B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.heartIconOriginalColor = this.heartIcon.color;
		this.healthBarOriginalColor = this.healthBarFill.color;
	}

	// Token: 0x040039A1 RID: 14753
	public Image hitBackgorund;

	// Token: 0x040039A2 RID: 14754
	public Image heartIcon;

	// Token: 0x040039A3 RID: 14755
	public Image healthBarFill;

	// Token: 0x040039A4 RID: 14756
	public UIShake shake;

	// Token: 0x040039A5 RID: 14757
	public Color HighlightedColor = Color.yellow;

	// Token: 0x040039A6 RID: 14758
	private Color heartIconOriginalColor;

	// Token: 0x040039A7 RID: 14759
	private Color healthBarOriginalColor;

	// Token: 0x040039A8 RID: 14760
	private float duration = 0.4f;

	// Token: 0x040039A9 RID: 14761
	private float lastTimerValue;

	// Token: 0x040039AA RID: 14762
	private float timer;
}
