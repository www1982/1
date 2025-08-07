using System;
using UnityEngine;

// Token: 0x02000BFB RID: 3067
public class BloomEffect : MonoBehaviour
{
	// Token: 0x170006C0 RID: 1728
	// (get) Token: 0x06005C69 RID: 23657 RVA: 0x0021C117 File Offset: 0x0021A317
	protected Material material
	{
		get
		{
			if (this.m_Material == null)
			{
				this.m_Material = new Material(this.blurShader);
				this.m_Material.hideFlags = HideFlags.DontSave;
			}
			return this.m_Material;
		}
	}

	// Token: 0x06005C6A RID: 23658 RVA: 0x0021C14B File Offset: 0x0021A34B
	protected void OnDisable()
	{
		if (this.m_Material)
		{
			global::UnityEngine.Object.DestroyImmediate(this.m_Material);
		}
	}

	// Token: 0x06005C6B RID: 23659 RVA: 0x0021C168 File Offset: 0x0021A368
	protected void Start()
	{
		if (!this.blurShader || !this.material.shader.isSupported)
		{
			base.enabled = false;
			return;
		}
		this.BloomMaskMaterial = new Material(Shader.Find("Klei/PostFX/BloomMask"));
		this.BloomCompositeMaterial = new Material(Shader.Find("Klei/PostFX/BloomComposite"));
	}

	// Token: 0x06005C6C RID: 23660 RVA: 0x0021C1C8 File Offset: 0x0021A3C8
	public void FourTapCone(RenderTexture source, RenderTexture dest, int iteration)
	{
		float num = 0.5f + (float)iteration * this.blurSpread;
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x06005C6D RID: 23661 RVA: 0x0021C234 File Offset: 0x0021A434
	private void DownSample4x(RenderTexture source, RenderTexture dest)
	{
		float num = 1f;
		Graphics.BlitMultiTap(source, dest, this.material, new Vector2[]
		{
			new Vector2(-num, -num),
			new Vector2(-num, num),
			new Vector2(num, num),
			new Vector2(num, -num)
		});
	}

	// Token: 0x06005C6E RID: 23662 RVA: 0x0021C298 File Offset: 0x0021A498
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0);
		temporary.name = "bloom_source";
		Graphics.Blit(source, temporary, this.BloomMaskMaterial);
		int num = Math.Max(source.width / 4, 4);
		int num2 = Math.Max(source.height / 4, 4);
		RenderTexture renderTexture = RenderTexture.GetTemporary(num, num2, 0);
		renderTexture.name = "bloom_downsampled";
		this.DownSample4x(temporary, renderTexture);
		RenderTexture.ReleaseTemporary(temporary);
		for (int i = 0; i < this.iterations; i++)
		{
			RenderTexture temporary2 = RenderTexture.GetTemporary(num, num2, 0);
			temporary2.name = "bloom_blurred";
			this.FourTapCone(renderTexture, temporary2, i);
			RenderTexture.ReleaseTemporary(renderTexture);
			renderTexture = temporary2;
		}
		this.BloomCompositeMaterial.SetTexture("_BloomTex", renderTexture);
		Graphics.Blit(source, destination, this.BloomCompositeMaterial);
		RenderTexture.ReleaseTemporary(renderTexture);
	}

	// Token: 0x04003D38 RID: 15672
	private Material BloomMaskMaterial;

	// Token: 0x04003D39 RID: 15673
	private Material BloomCompositeMaterial;

	// Token: 0x04003D3A RID: 15674
	public int iterations = 3;

	// Token: 0x04003D3B RID: 15675
	public float blurSpread = 0.6f;

	// Token: 0x04003D3C RID: 15676
	public Shader blurShader;

	// Token: 0x04003D3D RID: 15677
	private Material m_Material;
}
