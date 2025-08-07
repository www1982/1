using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D97 RID: 3479
public class PlanCategoryNotifications : MonoBehaviour
{
	// Token: 0x06006CAB RID: 27819 RVA: 0x0029103B File Offset: 0x0028F23B
	public void ToggleAttention(bool active)
	{
		if (!this.AttentionImage)
		{
			return;
		}
		this.AttentionImage.gameObject.SetActive(active);
	}

	// Token: 0x04004A14 RID: 18964
	public Image AttentionImage;
}
