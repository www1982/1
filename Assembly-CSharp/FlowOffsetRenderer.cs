using System;
using UnityEngine;

// Token: 0x02000923 RID: 2339
[AddComponentMenu("KMonoBehaviour/scripts/FlowOffsetRenderer")]
public class FlowOffsetRenderer : KMonoBehaviour
{
	// Token: 0x06004140 RID: 16704 RVA: 0x0016EA98 File Offset: 0x0016CC98
	protected override void OnSpawn()
	{
		this.FlowMaterial = new Material(Shader.Find("Klei/Flow"));
		ScreenResize instance = ScreenResize.Instance;
		instance.OnResize = (global::System.Action)Delegate.Combine(instance.OnResize, new global::System.Action(this.OnResize));
		this.OnResize();
		this.DoUpdate(0.1f);
	}

	// Token: 0x06004141 RID: 16705 RVA: 0x0016EAF4 File Offset: 0x0016CCF4
	private void OnResize()
	{
		for (int i = 0; i < this.OffsetTextures.Length; i++)
		{
			if (this.OffsetTextures[i] != null)
			{
				this.OffsetTextures[i].DestroyRenderTexture();
			}
			this.OffsetTextures[i] = new RenderTexture(Screen.width / 2, Screen.height / 2, 0, RenderTextureFormat.ARGBHalf);
			this.OffsetTextures[i].filterMode = FilterMode.Bilinear;
			this.OffsetTextures[i].name = "FlowOffsetTexture";
		}
	}

	// Token: 0x06004142 RID: 16706 RVA: 0x0016EB70 File Offset: 0x0016CD70
	private void LateUpdate()
	{
		if ((Time.deltaTime > 0f && Time.timeScale > 0f) || this.forceUpdate)
		{
			float num = Time.deltaTime / Time.timeScale;
			this.DoUpdate(num * Time.timeScale / 4f + num * 0.5f);
		}
	}

	// Token: 0x06004143 RID: 16707 RVA: 0x0016EBC4 File Offset: 0x0016CDC4
	private void DoUpdate(float dt)
	{
		this.CurrentTime += dt;
		float num = this.CurrentTime * this.PhaseMultiplier;
		num -= (float)((int)num);
		float num2 = num - (float)((int)num);
		float num3 = 1f;
		if (num2 <= this.GasPhase0)
		{
			num3 = 0f;
		}
		this.GasPhase0 = num2;
		float num4 = 1f;
		float num5 = num + 0.5f - (float)((int)(num + 0.5f));
		if (num5 <= this.GasPhase1)
		{
			num4 = 0f;
		}
		this.GasPhase1 = num5;
		Shader.SetGlobalVector(this.ParametersName, new Vector4(this.GasPhase0, 0f, 0f, 0f));
		Shader.SetGlobalVector("_NoiseParameters", new Vector4(this.NoiseInfluence, this.NoiseScale, 0f, 0f));
		RenderTexture renderTexture = this.OffsetTextures[this.OffsetIdx];
		this.OffsetIdx = (this.OffsetIdx + 1) % 2;
		RenderTexture renderTexture2 = this.OffsetTextures[this.OffsetIdx];
		Material flowMaterial = this.FlowMaterial;
		flowMaterial.SetTexture("_PreviousOffsetTex", renderTexture);
		flowMaterial.SetVector("_FlowParameters", new Vector4(Time.deltaTime * this.OffsetSpeed, num3, num4, 0f));
		flowMaterial.SetVector("_MinFlow", new Vector4(this.MinFlow0.x, this.MinFlow0.y, this.MinFlow1.x, this.MinFlow1.y));
		flowMaterial.SetVector("_VisibleArea", new Vector4(0f, 0f, (float)Grid.WidthInCells, (float)Grid.HeightInCells));
		flowMaterial.SetVector("_LiquidGasMask", new Vector4(this.LiquidGasMask.x, this.LiquidGasMask.y, 0f, 0f));
		Graphics.Blit(renderTexture, renderTexture2, flowMaterial);
		Shader.SetGlobalTexture(this.OffsetTextureName, renderTexture2);
	}

	// Token: 0x040028C2 RID: 10434
	private float GasPhase0;

	// Token: 0x040028C3 RID: 10435
	private float GasPhase1;

	// Token: 0x040028C4 RID: 10436
	public float PhaseMultiplier;

	// Token: 0x040028C5 RID: 10437
	public float NoiseInfluence;

	// Token: 0x040028C6 RID: 10438
	public float NoiseScale;

	// Token: 0x040028C7 RID: 10439
	public float OffsetSpeed;

	// Token: 0x040028C8 RID: 10440
	public string OffsetTextureName;

	// Token: 0x040028C9 RID: 10441
	public string ParametersName;

	// Token: 0x040028CA RID: 10442
	public Vector2 MinFlow0;

	// Token: 0x040028CB RID: 10443
	public Vector2 MinFlow1;

	// Token: 0x040028CC RID: 10444
	public Vector2 LiquidGasMask;

	// Token: 0x040028CD RID: 10445
	[SerializeField]
	private Material FlowMaterial;

	// Token: 0x040028CE RID: 10446
	[SerializeField]
	private bool forceUpdate;

	// Token: 0x040028CF RID: 10447
	private TextureLerper FlowLerper;

	// Token: 0x040028D0 RID: 10448
	public RenderTexture[] OffsetTextures = new RenderTexture[2];

	// Token: 0x040028D1 RID: 10449
	private int OffsetIdx;

	// Token: 0x040028D2 RID: 10450
	private float CurrentTime;
}
