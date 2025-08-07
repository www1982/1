using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000C64 RID: 3172
public class ExpandRevealUIContent : MonoBehaviour
{
	// Token: 0x060060CD RID: 24781 RVA: 0x0023C8C8 File Offset: 0x0023AAC8
	private void OnDisable()
	{
		if (this.BGChildFitter)
		{
			this.BGChildFitter.WidthScale = (this.BGChildFitter.HeightScale = 0f);
		}
		if (this.MaskChildFitter)
		{
			if (this.MaskChildFitter.fitWidth)
			{
				this.MaskChildFitter.WidthScale = 0f;
			}
			if (this.MaskChildFitter.fitHeight)
			{
				this.MaskChildFitter.HeightScale = 0f;
			}
		}
		if (this.BGRectStretcher)
		{
			this.BGRectStretcher.XStretchFactor = (this.BGRectStretcher.YStretchFactor = 0f);
			this.BGRectStretcher.UpdateStretching();
		}
		if (this.MaskRectStretcher)
		{
			this.MaskRectStretcher.XStretchFactor = (this.MaskRectStretcher.YStretchFactor = 0f);
			this.MaskRectStretcher.UpdateStretching();
		}
	}

	// Token: 0x060060CE RID: 24782 RVA: 0x0023C9B4 File Offset: 0x0023ABB4
	public void Expand(Action<object> completeCallback)
	{
		if (this.MaskChildFitter && this.MaskRectStretcher)
		{
			global::Debug.LogWarning("ExpandRevealUIContent has references to both a MaskChildFitter and a MaskRectStretcher. It should have only one or the other. ChildFitter to match child size, RectStretcher to match parent size.");
		}
		if (this.BGChildFitter && this.BGRectStretcher)
		{
			global::Debug.LogWarning("ExpandRevealUIContent has references to both a BGChildFitter and a BGRectStretcher . It should have only one or the other.  ChildFitter to match child size, RectStretcher to match parent size.");
		}
		if (this.activeRoutine != null)
		{
			base.StopCoroutine(this.activeRoutine);
		}
		this.CollapsedImmediate();
		this.activeRoutineCompleteCallback = completeCallback;
		this.activeRoutine = base.StartCoroutine(this.ExpandRoutine(null));
	}

	// Token: 0x060060CF RID: 24783 RVA: 0x0023CA40 File Offset: 0x0023AC40
	public void Collapse(Action<object> completeCallback)
	{
		if (this.activeRoutine != null)
		{
			if (this.activeRoutineCompleteCallback != null)
			{
				this.activeRoutineCompleteCallback(null);
			}
			base.StopCoroutine(this.activeRoutine);
		}
		this.activeRoutineCompleteCallback = completeCallback;
		if (base.gameObject.activeInHierarchy)
		{
			this.activeRoutine = base.StartCoroutine(this.CollapseRoutine(completeCallback));
			return;
		}
		this.activeRoutine = null;
		if (completeCallback != null)
		{
			completeCallback(null);
		}
	}

	// Token: 0x060060D0 RID: 24784 RVA: 0x0023CAAE File Offset: 0x0023ACAE
	private IEnumerator ExpandRoutine(Action<object> completeCallback)
	{
		this.Collapsing = false;
		this.Expanding = true;
		float num = 0f;
		foreach (Keyframe keyframe in this.expandAnimation.keys)
		{
			if (keyframe.time > num)
			{
				num = keyframe.time;
			}
		}
		float duration = num / this.speedScale;
		for (float remaining = duration; remaining >= 0f; remaining -= Time.unscaledDeltaTime * this.speedScale)
		{
			this.SetStretch(this.expandAnimation.Evaluate(duration - remaining));
			yield return null;
		}
		this.SetStretch(this.expandAnimation.Evaluate(duration));
		if (completeCallback != null)
		{
			completeCallback(null);
		}
		this.activeRoutine = null;
		this.Expanding = false;
		yield break;
	}

	// Token: 0x060060D1 RID: 24785 RVA: 0x0023CAC4 File Offset: 0x0023ACC4
	private void SetStretch(float value)
	{
		if (this.BGRectStretcher)
		{
			if (this.BGRectStretcher.StretchX)
			{
				this.BGRectStretcher.XStretchFactor = value;
			}
			if (this.BGRectStretcher.StretchY)
			{
				this.BGRectStretcher.YStretchFactor = value;
			}
		}
		if (this.MaskRectStretcher)
		{
			if (this.MaskRectStretcher.StretchX)
			{
				this.MaskRectStretcher.XStretchFactor = value;
			}
			if (this.MaskRectStretcher.StretchY)
			{
				this.MaskRectStretcher.YStretchFactor = value;
			}
		}
		if (this.BGChildFitter)
		{
			if (this.BGChildFitter.fitWidth)
			{
				this.BGChildFitter.WidthScale = value;
			}
			if (this.BGChildFitter.fitHeight)
			{
				this.BGChildFitter.HeightScale = value;
			}
		}
		if (this.MaskChildFitter)
		{
			if (this.MaskChildFitter.fitWidth)
			{
				this.MaskChildFitter.WidthScale = value;
			}
			if (this.MaskChildFitter.fitHeight)
			{
				this.MaskChildFitter.HeightScale = value;
			}
		}
	}

	// Token: 0x060060D2 RID: 24786 RVA: 0x0023CBCD File Offset: 0x0023ADCD
	private IEnumerator CollapseRoutine(Action<object> completeCallback)
	{
		this.Expanding = false;
		this.Collapsing = true;
		float num = 0f;
		foreach (Keyframe keyframe in this.collapseAnimation.keys)
		{
			if (keyframe.time > num)
			{
				num = keyframe.time;
			}
		}
		float duration = num;
		for (float remaining = duration; remaining >= 0f; remaining -= Time.unscaledDeltaTime)
		{
			this.SetStretch(this.collapseAnimation.Evaluate(duration - remaining));
			yield return null;
		}
		this.SetStretch(this.collapseAnimation.Evaluate(duration));
		if (completeCallback != null)
		{
			completeCallback(null);
		}
		this.activeRoutine = null;
		this.Collapsing = false;
		base.gameObject.SetActive(false);
		yield break;
	}

	// Token: 0x060060D3 RID: 24787 RVA: 0x0023CBE4 File Offset: 0x0023ADE4
	public void CollapsedImmediate()
	{
		float num = (float)this.collapseAnimation.length;
		this.SetStretch(this.collapseAnimation.Evaluate(num));
	}

	// Token: 0x0400415B RID: 16731
	private Coroutine activeRoutine;

	// Token: 0x0400415C RID: 16732
	private Action<object> activeRoutineCompleteCallback;

	// Token: 0x0400415D RID: 16733
	public AnimationCurve expandAnimation;

	// Token: 0x0400415E RID: 16734
	public AnimationCurve collapseAnimation;

	// Token: 0x0400415F RID: 16735
	public KRectStretcher MaskRectStretcher;

	// Token: 0x04004160 RID: 16736
	public KRectStretcher BGRectStretcher;

	// Token: 0x04004161 RID: 16737
	public KChildFitter MaskChildFitter;

	// Token: 0x04004162 RID: 16738
	public KChildFitter BGChildFitter;

	// Token: 0x04004163 RID: 16739
	public float speedScale = 1f;

	// Token: 0x04004164 RID: 16740
	public bool Collapsing;

	// Token: 0x04004165 RID: 16741
	public bool Expanding;
}
