using System;
using UnityEngine;

// Token: 0x02000C65 RID: 3173
public class SizePulse : MonoBehaviour
{
	// Token: 0x060060D5 RID: 24789 RVA: 0x0023CC24 File Offset: 0x0023AE24
	private void Start()
	{
		if (base.GetComponents<SizePulse>().Length > 1)
		{
			global::UnityEngine.Object.Destroy(this);
		}
		RectTransform rectTransform = (RectTransform)base.transform;
		this.from = rectTransform.localScale;
		this.cur = this.from;
		this.to = this.from * this.multiplier;
	}

	// Token: 0x060060D6 RID: 24790 RVA: 0x0023CC84 File Offset: 0x0023AE84
	private void Update()
	{
		float num = (this.updateWhenPaused ? Time.unscaledDeltaTime : Time.deltaTime);
		num *= this.speed;
		SizePulse.State state = this.state;
		if (state != SizePulse.State.Up)
		{
			if (state == SizePulse.State.Down)
			{
				this.cur = Vector2.Lerp(this.cur, this.from, num);
				if ((this.from - this.cur).sqrMagnitude < 0.0001f)
				{
					this.cur = this.from;
					this.state = SizePulse.State.Finished;
					if (this.onComplete != null)
					{
						this.onComplete();
					}
				}
			}
		}
		else
		{
			this.cur = Vector2.Lerp(this.cur, this.to, num);
			if ((this.to - this.cur).sqrMagnitude < 0.0001f)
			{
				this.cur = this.to;
				this.state = SizePulse.State.Down;
			}
		}
		((RectTransform)base.transform).localScale = new Vector3(this.cur.x, this.cur.y, 1f);
	}

	// Token: 0x04004166 RID: 16742
	public global::System.Action onComplete;

	// Token: 0x04004167 RID: 16743
	public Vector2 from = Vector2.one;

	// Token: 0x04004168 RID: 16744
	public Vector2 to = Vector2.one;

	// Token: 0x04004169 RID: 16745
	public float multiplier = 1.25f;

	// Token: 0x0400416A RID: 16746
	public float speed = 1f;

	// Token: 0x0400416B RID: 16747
	public bool updateWhenPaused;

	// Token: 0x0400416C RID: 16748
	private Vector2 cur;

	// Token: 0x0400416D RID: 16749
	private SizePulse.State state;

	// Token: 0x02001E2A RID: 7722
	private enum State
	{
		// Token: 0x04008CC0 RID: 36032
		Up,
		// Token: 0x04008CC1 RID: 36033
		Down,
		// Token: 0x04008CC2 RID: 36034
		Finished
	}
}
