using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CD4 RID: 3284
[AddComponentMenu("KMonoBehaviour/scripts/GoodnessSlider")]
public class GoodnessSlider : KMonoBehaviour
{
	// Token: 0x06006539 RID: 25913 RVA: 0x00260DF5 File Offset: 0x0025EFF5
	protected override void OnSpawn()
	{
		base.Spawn();
		this.UpdateValues();
	}

	// Token: 0x0600653A RID: 25914 RVA: 0x00260E04 File Offset: 0x0025F004
	public void UpdateValues()
	{
		this.text.color = (this.fill.color = this.gradient.Evaluate(this.slider.value));
		for (int i = 0; i < this.gradient.colorKeys.Length; i++)
		{
			if (this.gradient.colorKeys[i].time < this.slider.value)
			{
				this.text.text = this.names[i];
			}
			if (i == this.gradient.colorKeys.Length - 1 && this.gradient.colorKeys[i - 1].time < this.slider.value)
			{
				this.text.text = this.names[i];
			}
		}
	}

	// Token: 0x04004526 RID: 17702
	public Image icon;

	// Token: 0x04004527 RID: 17703
	public Text text;

	// Token: 0x04004528 RID: 17704
	public Slider slider;

	// Token: 0x04004529 RID: 17705
	public Image fill;

	// Token: 0x0400452A RID: 17706
	public Gradient gradient;

	// Token: 0x0400452B RID: 17707
	public string[] names;
}
