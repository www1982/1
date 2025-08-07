using System;
using Klei.AI;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000E77 RID: 3703
public class ValueTrendImageToggle : MonoBehaviour
{
	// Token: 0x06007618 RID: 30232 RVA: 0x002D3214 File Offset: 0x002D1414
	public void SetValue(AmountInstance ainstance)
	{
		float delta = ainstance.GetDelta();
		Sprite sprite = null;
		if (ainstance.paused || delta == 0f)
		{
			this.targetImage.gameObject.SetActive(false);
		}
		else
		{
			this.targetImage.gameObject.SetActive(true);
			if (delta <= -ainstance.amount.visualDeltaThreshold * 2f)
			{
				sprite = this.Down_Three;
			}
			else if (delta <= -ainstance.amount.visualDeltaThreshold)
			{
				sprite = this.Down_Two;
			}
			else if (delta <= 0f)
			{
				sprite = this.Down_One;
			}
			else if (delta > ainstance.amount.visualDeltaThreshold * 2f)
			{
				sprite = this.Up_Three;
			}
			else if (delta > ainstance.amount.visualDeltaThreshold)
			{
				sprite = this.Up_Two;
			}
			else if (delta > 0f)
			{
				sprite = this.Up_One;
			}
		}
		this.targetImage.sprite = sprite;
	}

	// Token: 0x040051E4 RID: 20964
	public Image targetImage;

	// Token: 0x040051E5 RID: 20965
	public Sprite Up_One;

	// Token: 0x040051E6 RID: 20966
	public Sprite Up_Two;

	// Token: 0x040051E7 RID: 20967
	public Sprite Up_Three;

	// Token: 0x040051E8 RID: 20968
	public Sprite Down_One;

	// Token: 0x040051E9 RID: 20969
	public Sprite Down_Two;

	// Token: 0x040051EA RID: 20970
	public Sprite Down_Three;

	// Token: 0x040051EB RID: 20971
	public Sprite Zero;
}
