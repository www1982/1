using System;
using UnityEngine;

// Token: 0x02000AA6 RID: 2726
public class GridCompositor : MonoBehaviour
{
	// Token: 0x06004F0B RID: 20235 RVA: 0x001C92FC File Offset: 0x001C74FC
	public static void DestroyInstance()
	{
		GridCompositor.Instance = null;
	}

	// Token: 0x06004F0C RID: 20236 RVA: 0x001C9304 File Offset: 0x001C7504
	private void Awake()
	{
		GridCompositor.Instance = this;
		base.enabled = false;
	}

	// Token: 0x06004F0D RID: 20237 RVA: 0x001C9313 File Offset: 0x001C7513
	private void Start()
	{
		this.material = new Material(Shader.Find("Klei/PostFX/GridCompositor"));
	}

	// Token: 0x06004F0E RID: 20238 RVA: 0x001C932A File Offset: 0x001C752A
	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(src, dest, this.material);
	}

	// Token: 0x06004F0F RID: 20239 RVA: 0x001C9339 File Offset: 0x001C7539
	public void ToggleMajor(bool on)
	{
		this.onMajor = on;
		this.Refresh();
	}

	// Token: 0x06004F10 RID: 20240 RVA: 0x001C9348 File Offset: 0x001C7548
	public void ToggleMinor(bool on)
	{
		this.onMinor = on;
		this.Refresh();
	}

	// Token: 0x06004F11 RID: 20241 RVA: 0x001C9357 File Offset: 0x001C7557
	private void Refresh()
	{
		base.enabled = this.onMinor || this.onMajor;
	}

	// Token: 0x04003477 RID: 13431
	public Material material;

	// Token: 0x04003478 RID: 13432
	public static GridCompositor Instance;

	// Token: 0x04003479 RID: 13433
	private bool onMajor;

	// Token: 0x0400347A RID: 13434
	private bool onMinor;
}
