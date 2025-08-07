using System;
using UnityEngine;

// Token: 0x02000AA0 RID: 2720
public class CameraRenderTexture : MonoBehaviour
{
	// Token: 0x06004EEA RID: 20202 RVA: 0x001C7D90 File Offset: 0x001C5F90
	private void Awake()
	{
		this.material = new Material(Shader.Find("Klei/PostFX/CameraRenderTexture"));
	}

	// Token: 0x06004EEB RID: 20203 RVA: 0x001C7DA7 File Offset: 0x001C5FA7
	private void Start()
	{
		if (ScreenResize.Instance != null)
		{
			ScreenResize instance = ScreenResize.Instance;
			instance.OnResize = (global::System.Action)Delegate.Combine(instance.OnResize, new global::System.Action(this.OnResize));
		}
		this.OnResize();
	}

	// Token: 0x06004EEC RID: 20204 RVA: 0x001C7DE4 File Offset: 0x001C5FE4
	private void OnResize()
	{
		if (this.resultTexture != null)
		{
			this.resultTexture.DestroyRenderTexture();
		}
		this.resultTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32);
		this.resultTexture.name = base.name;
		this.resultTexture.filterMode = FilterMode.Point;
		this.resultTexture.autoGenerateMips = false;
		if (this.TextureName != "")
		{
			Shader.SetGlobalTexture(this.TextureName, this.resultTexture);
		}
	}

	// Token: 0x06004EED RID: 20205 RVA: 0x001C7E6D File Offset: 0x001C606D
	private void OnRenderImage(RenderTexture source, RenderTexture dest)
	{
		Graphics.Blit(source, this.resultTexture, this.material);
	}

	// Token: 0x06004EEE RID: 20206 RVA: 0x001C7E81 File Offset: 0x001C6081
	public RenderTexture GetTexture()
	{
		return this.resultTexture;
	}

	// Token: 0x06004EEF RID: 20207 RVA: 0x001C7E89 File Offset: 0x001C6089
	public bool ShouldFlip()
	{
		return false;
	}

	// Token: 0x04003469 RID: 13417
	public string TextureName;

	// Token: 0x0400346A RID: 13418
	private RenderTexture resultTexture;

	// Token: 0x0400346B RID: 13419
	private Material material;
}
