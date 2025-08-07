using System;
using UnityEngine;

// Token: 0x02000996 RID: 2454
public class UIShake : KMonoBehaviour, IRenderEveryTick
{
	// Token: 0x170004FE RID: 1278
	// (get) Token: 0x06004732 RID: 18226 RVA: 0x0019AAD3 File Offset: 0x00198CD3
	public float Intensity
	{
		get
		{
			return this.intensity;
		}
	}

	// Token: 0x06004733 RID: 18227 RVA: 0x0019AADC File Offset: 0x00198CDC
	public void RenderEveryTick(float dt)
	{
		if (this.intensity != 0f || this.lastIntensity != 0f)
		{
			this.lastIntensity = this.intensity;
			Vector2 vector = new Vector2(global::UnityEngine.Random.Range(-1f, 1f) * this.MaxOffsets.x * this.intensity, global::UnityEngine.Random.Range(-1f, 1f) * this.MaxOffsets.y * this.intensity);
			Vector2 vector2 = this.initialLocalPosition + vector;
			this.transform.anchoredPosition = vector2;
		}
	}

	// Token: 0x06004734 RID: 18228 RVA: 0x0019AB73 File Offset: 0x00198D73
	public void SetIntensity(float intensity)
	{
		this.intensity = intensity;
	}

	// Token: 0x06004735 RID: 18229 RVA: 0x0019AB7C File Offset: 0x00198D7C
	protected override void OnPrefabInit()
	{
		this.transform = base.transform as RectTransform;
		this.initialLocalPosition = this.transform.anchoredPosition;
	}

	// Token: 0x04002F10 RID: 12048
	public Vector2 MaxOffsets = Vector2.one;

	// Token: 0x04002F11 RID: 12049
	private float lastIntensity;

	// Token: 0x04002F12 RID: 12050
	private float intensity;

	// Token: 0x04002F13 RID: 12051
	private Vector2 initialLocalPosition;

	// Token: 0x04002F14 RID: 12052
	private new RectTransform transform;
}
