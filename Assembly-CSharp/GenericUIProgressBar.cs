using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000CD3 RID: 3283
[AddComponentMenu("KMonoBehaviour/scripts/GenericUIProgressBar")]
public class GenericUIProgressBar : KMonoBehaviour
{
	// Token: 0x06006536 RID: 25910 RVA: 0x00260D91 File Offset: 0x0025EF91
	public void SetMaxValue(float max)
	{
		this.maxValue = max;
	}

	// Token: 0x06006537 RID: 25911 RVA: 0x00260D9C File Offset: 0x0025EF9C
	public void SetFillPercentage(float value)
	{
		this.fill.fillAmount = value;
		this.label.text = Util.FormatWholeNumber(Mathf.Min(this.maxValue, this.maxValue * value)) + "/" + this.maxValue.ToString();
	}

	// Token: 0x04004523 RID: 17699
	public Image fill;

	// Token: 0x04004524 RID: 17700
	public LocText label;

	// Token: 0x04004525 RID: 17701
	private float maxValue;
}
