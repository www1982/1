using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x02000E50 RID: 3664
[AddComponentMenu("KMonoBehaviour/scripts/SliderContainer")]
public class SliderContainer : KMonoBehaviour
{
	// Token: 0x060074A7 RID: 29863 RVA: 0x002C869C File Offset: 0x002C689C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.slider.onValueChanged.AddListener(new UnityAction<float>(this.UpdateSliderLabel));
	}

	// Token: 0x060074A8 RID: 29864 RVA: 0x002C86C0 File Offset: 0x002C68C0
	public void UpdateSliderLabel(float newValue)
	{
		if (this.isPercentValue)
		{
			this.valueLabel.text = (newValue * 100f).ToString("F0") + "%";
			return;
		}
		this.valueLabel.text = newValue.ToString();
	}

	// Token: 0x0400509F RID: 20639
	public bool isPercentValue = true;

	// Token: 0x040050A0 RID: 20640
	public KSlider slider;

	// Token: 0x040050A1 RID: 20641
	public LocText nameLabel;

	// Token: 0x040050A2 RID: 20642
	public LocText valueLabel;
}
