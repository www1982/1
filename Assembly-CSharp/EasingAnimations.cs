using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C63 RID: 3171
public class EasingAnimations : MonoBehaviour
{
	// Token: 0x170006F3 RID: 1779
	// (get) Token: 0x060060C3 RID: 24771 RVA: 0x0023C62C File Offset: 0x0023A82C
	public bool IsPlaying
	{
		get
		{
			return this.animationCoroutine != null;
		}
	}

	// Token: 0x060060C4 RID: 24772 RVA: 0x0023C637 File Offset: 0x0023A837
	private void Start()
	{
		if (this.animationMap == null || this.animationMap.Count == 0)
		{
			this.Initialize();
		}
	}

	// Token: 0x060060C5 RID: 24773 RVA: 0x0023C654 File Offset: 0x0023A854
	private void Initialize()
	{
		this.animationMap = new Dictionary<string, EasingAnimations.AnimationScales>();
		foreach (EasingAnimations.AnimationScales animationScales in this.scales)
		{
			this.animationMap.Add(animationScales.name, animationScales);
		}
	}

	// Token: 0x060060C6 RID: 24774 RVA: 0x0023C69C File Offset: 0x0023A89C
	public void PlayAnimation(string animationName, float delay = 0f)
	{
		if (this.animationMap == null || this.animationMap.Count == 0)
		{
			this.Initialize();
		}
		if (!this.animationMap.ContainsKey(animationName))
		{
			return;
		}
		if (this.animationCoroutine != null)
		{
			base.StopCoroutine(this.animationCoroutine);
		}
		this.currentAnimation = this.animationMap[animationName];
		this.currentAnimation.currentScale = this.currentAnimation.startScale;
		base.transform.localScale = Vector3.one * this.currentAnimation.currentScale;
		this.animationCoroutine = base.StartCoroutine(this.ExecuteAnimation(delay));
	}

	// Token: 0x060060C7 RID: 24775 RVA: 0x0023C742 File Offset: 0x0023A942
	private IEnumerator ExecuteAnimation(float delay)
	{
		float startTime = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup < startTime + delay)
		{
			yield return SequenceUtil.WaitForNextFrame;
		}
		startTime = Time.realtimeSinceStartup;
		bool keepAnimating = true;
		while (keepAnimating)
		{
			float num = Time.realtimeSinceStartup - startTime;
			this.currentAnimation.currentScale = this.GetEasing(num * this.currentAnimation.easingMultiplier);
			if (this.currentAnimation.endScale > this.currentAnimation.startScale)
			{
				keepAnimating = this.currentAnimation.currentScale < this.currentAnimation.endScale - 0.025f;
			}
			else
			{
				keepAnimating = this.currentAnimation.currentScale > this.currentAnimation.endScale + 0.025f;
			}
			if (!keepAnimating)
			{
				this.currentAnimation.currentScale = this.currentAnimation.endScale;
			}
			base.transform.localScale = Vector3.one * this.currentAnimation.currentScale;
			yield return SequenceUtil.WaitForEndOfFrame;
		}
		this.animationCoroutine = null;
		if (this.OnAnimationDone != null)
		{
			this.OnAnimationDone(this.currentAnimation.name);
		}
		yield break;
	}

	// Token: 0x060060C8 RID: 24776 RVA: 0x0023C758 File Offset: 0x0023A958
	private float GetEasing(float t)
	{
		EasingAnimations.AnimationScales.AnimationType type = this.currentAnimation.type;
		if (type == EasingAnimations.AnimationScales.AnimationType.EaseOutBack)
		{
			return this.EaseOutBack(this.currentAnimation.currentScale, this.currentAnimation.endScale, t);
		}
		if (type == EasingAnimations.AnimationScales.AnimationType.EaseInBack)
		{
			return this.EaseInBack(this.currentAnimation.currentScale, this.currentAnimation.endScale, t);
		}
		return this.EaseInOutBack(this.currentAnimation.currentScale, this.currentAnimation.endScale, t);
	}

	// Token: 0x060060C9 RID: 24777 RVA: 0x0023C7D4 File Offset: 0x0023A9D4
	public float EaseInOutBack(float start, float end, float value)
	{
		float num = 1.70158f;
		end -= start;
		value /= 0.5f;
		if (value < 1f)
		{
			num *= 1.525f;
			return end * 0.5f * (value * value * ((num + 1f) * value - num)) + start;
		}
		value -= 2f;
		num *= 1.525f;
		return end * 0.5f * (value * value * ((num + 1f) * value + num) + 2f) + start;
	}

	// Token: 0x060060CA RID: 24778 RVA: 0x0023C850 File Offset: 0x0023AA50
	public float EaseInBack(float start, float end, float value)
	{
		end -= start;
		value /= 1f;
		float num = 1.70158f;
		return end * value * value * ((num + 1f) * value - num) + start;
	}

	// Token: 0x060060CB RID: 24779 RVA: 0x0023C884 File Offset: 0x0023AA84
	public float EaseOutBack(float start, float end, float value)
	{
		float num = 1.70158f;
		end -= start;
		value -= 1f;
		return end * (value * value * ((num + 1f) * value + num) + 1f) + start;
	}

	// Token: 0x04004156 RID: 16726
	public EasingAnimations.AnimationScales[] scales;

	// Token: 0x04004157 RID: 16727
	private EasingAnimations.AnimationScales currentAnimation;

	// Token: 0x04004158 RID: 16728
	private Coroutine animationCoroutine;

	// Token: 0x04004159 RID: 16729
	private Dictionary<string, EasingAnimations.AnimationScales> animationMap;

	// Token: 0x0400415A RID: 16730
	public Action<string> OnAnimationDone;

	// Token: 0x02001E26 RID: 7718
	[Serializable]
	public struct AnimationScales
	{
		// Token: 0x04008CA7 RID: 36007
		public string name;

		// Token: 0x04008CA8 RID: 36008
		public float startScale;

		// Token: 0x04008CA9 RID: 36009
		public float endScale;

		// Token: 0x04008CAA RID: 36010
		public EasingAnimations.AnimationScales.AnimationType type;

		// Token: 0x04008CAB RID: 36011
		public float easingMultiplier;

		// Token: 0x04008CAC RID: 36012
		[HideInInspector]
		public float currentScale;

		// Token: 0x020028F7 RID: 10487
		public enum AnimationType
		{
			// Token: 0x0400B56C RID: 46444
			EaseInOutBack,
			// Token: 0x0400B56D RID: 46445
			EaseOutBack,
			// Token: 0x0400B56E RID: 46446
			EaseInBack
		}
	}
}
