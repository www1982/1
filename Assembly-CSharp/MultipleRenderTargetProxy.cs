using System;
using UnityEngine;

// Token: 0x02000AB2 RID: 2738
public class MultipleRenderTargetProxy : MonoBehaviour
{
	// Token: 0x06004F6C RID: 20332 RVA: 0x001CB810 File Offset: 0x001C9A10
	private void Start()
	{
		if (ScreenResize.Instance != null)
		{
			ScreenResize instance = ScreenResize.Instance;
			instance.OnResize = (global::System.Action)Delegate.Combine(instance.OnResize, new global::System.Action(this.OnResize));
		}
		this.CreateRenderTarget();
		ShaderReloader.Register(new global::System.Action(this.OnShadersReloaded));
	}

	// Token: 0x06004F6D RID: 20333 RVA: 0x001CB867 File Offset: 0x001C9A67
	public void ToggleColouredOverlayView(bool enabled)
	{
		this.colouredOverlayBufferEnabled = enabled;
		this.CreateRenderTarget();
	}

	// Token: 0x06004F6E RID: 20334 RVA: 0x001CB878 File Offset: 0x001C9A78
	private void CreateRenderTarget()
	{
		RenderBuffer[] array = new RenderBuffer[this.colouredOverlayBufferEnabled ? 3 : 2];
		this.Textures[0] = this.RecreateRT(this.Textures[0], 24, RenderTextureFormat.ARGB32);
		this.Textures[0].filterMode = FilterMode.Point;
		this.Textures[0].name = "MRT0";
		this.Textures[1] = this.RecreateRT(this.Textures[1], 0, RenderTextureFormat.R8);
		this.Textures[1].filterMode = FilterMode.Point;
		this.Textures[1].name = "MRT1";
		array[0] = this.Textures[0].colorBuffer;
		array[1] = this.Textures[1].colorBuffer;
		if (this.colouredOverlayBufferEnabled)
		{
			this.Textures[2] = this.RecreateRT(this.Textures[2], 0, RenderTextureFormat.ARGB32);
			this.Textures[2].filterMode = FilterMode.Bilinear;
			this.Textures[2].name = "MRT2";
			array[2] = this.Textures[2].colorBuffer;
		}
		base.GetComponent<Camera>().SetTargetBuffers(array, this.Textures[0].depthBuffer);
		this.OnShadersReloaded();
	}

	// Token: 0x06004F6F RID: 20335 RVA: 0x001CB9A4 File Offset: 0x001C9BA4
	private RenderTexture RecreateRT(RenderTexture rt, int depth, RenderTextureFormat format)
	{
		RenderTexture renderTexture = rt;
		if (rt == null || rt.width != Screen.width || rt.height != Screen.height || rt.format != format)
		{
			if (rt != null)
			{
				rt.DestroyRenderTexture();
			}
			renderTexture = new RenderTexture(Screen.width, Screen.height, depth, format);
		}
		return renderTexture;
	}

	// Token: 0x06004F70 RID: 20336 RVA: 0x001CBA01 File Offset: 0x001C9C01
	private void OnResize()
	{
		this.CreateRenderTarget();
	}

	// Token: 0x06004F71 RID: 20337 RVA: 0x001CBA09 File Offset: 0x001C9C09
	private void Update()
	{
		if (!this.Textures[0].IsCreated())
		{
			this.CreateRenderTarget();
		}
	}

	// Token: 0x06004F72 RID: 20338 RVA: 0x001CBA20 File Offset: 0x001C9C20
	private void OnShadersReloaded()
	{
		Shader.SetGlobalTexture("_MRT0", this.Textures[0]);
		Shader.SetGlobalTexture("_MRT1", this.Textures[1]);
		if (this.colouredOverlayBufferEnabled)
		{
			Shader.SetGlobalTexture("_MRT2", this.Textures[2]);
		}
	}

	// Token: 0x0400356C RID: 13676
	public RenderTexture[] Textures = new RenderTexture[3];

	// Token: 0x0400356D RID: 13677
	private bool colouredOverlayBufferEnabled;
}
