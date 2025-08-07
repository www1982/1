using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D57 RID: 3415
public class NotificationAnimator : MonoBehaviour
{
	// Token: 0x060069EA RID: 27114 RVA: 0x00280266 File Offset: 0x0027E466
	public void Begin(bool startOffset = true)
	{
		this.Reset();
		this.animating = true;
		if (startOffset)
		{
			this.layoutElement.minWidth = 100f;
			return;
		}
		this.layoutElement.minWidth = 1f;
		this.speed = -10f;
	}

	// Token: 0x060069EB RID: 27115 RVA: 0x002802A4 File Offset: 0x0027E4A4
	private void Reset()
	{
		this.bounceCount = 2;
		this.layoutElement = base.GetComponent<LayoutElement>();
		this.layoutElement.minWidth = 0f;
		this.speed = 1f;
	}

	// Token: 0x060069EC RID: 27116 RVA: 0x002802D4 File Offset: 0x0027E4D4
	public void Stop()
	{
		this.Reset();
		this.animating = false;
	}

	// Token: 0x060069ED RID: 27117 RVA: 0x002802E4 File Offset: 0x0027E4E4
	private void LateUpdate()
	{
		if (!this.animating)
		{
			return;
		}
		this.layoutElement.minWidth -= this.speed;
		this.speed += 0.5f;
		if (this.layoutElement.minWidth <= 0f)
		{
			if (this.bounceCount > 0)
			{
				this.bounceCount--;
				this.speed = -this.speed / Mathf.Pow(2f, (float)(2 - this.bounceCount));
				this.layoutElement.minWidth = -this.speed;
				return;
			}
			this.layoutElement.minWidth = 0f;
			this.Stop();
		}
	}

	// Token: 0x0400484F RID: 18511
	private const float START_SPEED = 1f;

	// Token: 0x04004850 RID: 18512
	private const float ACCELERATION = 0.5f;

	// Token: 0x04004851 RID: 18513
	private const float BOUNCE_DAMPEN = 2f;

	// Token: 0x04004852 RID: 18514
	private const int BOUNCE_COUNT = 2;

	// Token: 0x04004853 RID: 18515
	private const float OFFSETX = 100f;

	// Token: 0x04004854 RID: 18516
	private float speed = 1f;

	// Token: 0x04004855 RID: 18517
	private int bounceCount = 2;

	// Token: 0x04004856 RID: 18518
	private LayoutElement layoutElement;

	// Token: 0x04004857 RID: 18519
	[SerializeField]
	private bool animating = true;
}
