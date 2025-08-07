using System;
using UnityEngine;

// Token: 0x020008C8 RID: 2248
public class DreamBubble : KMonoBehaviour
{
	// Token: 0x17000451 RID: 1105
	// (get) Token: 0x06003E4C RID: 15948 RVA: 0x0015C726 File Offset: 0x0015A926
	// (set) Token: 0x06003E4B RID: 15947 RVA: 0x0015C71D File Offset: 0x0015A91D
	public bool IsVisible { get; private set; }

	// Token: 0x06003E4D RID: 15949 RVA: 0x0015C72E File Offset: 0x0015A92E
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.dreamBackgroundComponent.SetSymbolVisiblity(this.snapToPivotSymbol, false);
		this.SetVisibility(false);
	}

	// Token: 0x06003E4E RID: 15950 RVA: 0x0015C754 File Offset: 0x0015A954
	public void Tick(float dt)
	{
		if (this._currentDream != null && this._currentDream.Icons.Length != 0)
		{
			float num = this._timePassedSinceDreamStarted / this._currentDream.secondPerImage;
			int num2 = Mathf.FloorToInt(num);
			float num3 = num - (float)num2;
			int num4 = (int)Mathf.Repeat((float)Mathf.FloorToInt(num), (float)this._currentDream.Icons.Length);
			if (this.dreamContentComponent.sprite != this._currentDream.Icons[num4])
			{
				this.dreamContentComponent.sprite = this._currentDream.Icons[num4];
			}
			this.dreamContentComponent.rectTransform.localScale = Vector3.one * num3;
			this._color.a = (Mathf.Sin(num3 * 6.2831855f - 1.5707964f) + 1f) * 0.5f;
			this.dreamContentComponent.color = this._color;
			this._timePassedSinceDreamStarted += dt;
		}
	}

	// Token: 0x06003E4F RID: 15951 RVA: 0x0015C850 File Offset: 0x0015AA50
	public void SetDream(Dream dream)
	{
		this._currentDream = dream;
		this.dreamBackgroundComponent.Stop();
		this.dreamBackgroundComponent.AnimFiles = new KAnimFile[] { Assets.GetAnim(dream.BackgroundAnim) };
		this.dreamContentComponent.color = this._color;
		this.dreamContentComponent.enabled = dream != null && dream.Icons != null && dream.Icons.Length != 0;
		this._timePassedSinceDreamStarted = 0f;
		this._color.a = 0f;
	}

	// Token: 0x06003E50 RID: 15952 RVA: 0x0015C8E4 File Offset: 0x0015AAE4
	public void SetVisibility(bool visible)
	{
		this.IsVisible = visible;
		this.dreamBackgroundComponent.SetVisiblity(visible);
		this.dreamContentComponent.gameObject.SetActive(visible);
		if (visible)
		{
			if (this._currentDream != null)
			{
				this.dreamBackgroundComponent.Play("dream_loop", KAnim.PlayMode.Loop, 1f, 0f);
			}
			this.dreamBubbleBorderKanim.Play("dream_bubble_loop", KAnim.PlayMode.Loop, 1f, 0f);
			this.maskKanim.Play("dream_bubble_mask", KAnim.PlayMode.Loop, 1f, 0f);
			return;
		}
		this.dreamBackgroundComponent.Stop();
		this.maskKanim.Stop();
		this.dreamBubbleBorderKanim.Stop();
	}

	// Token: 0x06003E51 RID: 15953 RVA: 0x0015C9A2 File Offset: 0x0015ABA2
	public void StopDreaming()
	{
		this._currentDream = null;
		this.SetVisibility(false);
	}

	// Token: 0x0400264D RID: 9805
	public KBatchedAnimController dreamBackgroundComponent;

	// Token: 0x0400264E RID: 9806
	public KBatchedAnimController maskKanim;

	// Token: 0x0400264F RID: 9807
	public KBatchedAnimController dreamBubbleBorderKanim;

	// Token: 0x04002650 RID: 9808
	public KImage dreamContentComponent;

	// Token: 0x04002651 RID: 9809
	private const string dreamBackgroundAnimationName = "dream_loop";

	// Token: 0x04002652 RID: 9810
	private const string dreamMaskAnimationName = "dream_bubble_mask";

	// Token: 0x04002653 RID: 9811
	private const string dreamBubbleBorderAnimationName = "dream_bubble_loop";

	// Token: 0x04002654 RID: 9812
	private HashedString snapToPivotSymbol = new HashedString("snapto_pivot");

	// Token: 0x04002656 RID: 9814
	private Dream _currentDream;

	// Token: 0x04002657 RID: 9815
	private float _timePassedSinceDreamStarted;

	// Token: 0x04002658 RID: 9816
	private Color _color = Color.white;

	// Token: 0x04002659 RID: 9817
	private const float PI_2 = 6.2831855f;

	// Token: 0x0400265A RID: 9818
	private const float HALF_PI = 1.5707964f;
}
