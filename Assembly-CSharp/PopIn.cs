using System;
using UnityEngine;

// Token: 0x02000D9D RID: 3485
public class PopIn : MonoBehaviour
{
	// Token: 0x06006D19 RID: 27929 RVA: 0x00294BE4 File Offset: 0x00292DE4
	private void OnEnable()
	{
		this.StartPopIn(true);
	}

	// Token: 0x06006D1A RID: 27930 RVA: 0x00294BF0 File Offset: 0x00292DF0
	private void Update()
	{
		float num = Mathf.Lerp(base.transform.localScale.x, this.targetScale, Time.unscaledDeltaTime * this.speed);
		base.transform.localScale = new Vector3(num, num, 1f);
	}

	// Token: 0x06006D1B RID: 27931 RVA: 0x00294C3C File Offset: 0x00292E3C
	public void StartPopIn(bool force_reset = false)
	{
		if (force_reset)
		{
			base.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
		}
		this.targetScale = 1f;
	}

	// Token: 0x06006D1C RID: 27932 RVA: 0x00294C6B File Offset: 0x00292E6B
	public void StartPopOut()
	{
		this.targetScale = 0f;
	}

	// Token: 0x04004A78 RID: 19064
	private float targetScale;

	// Token: 0x04004A79 RID: 19065
	public float speed;
}
