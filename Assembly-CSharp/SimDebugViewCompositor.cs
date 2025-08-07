using System;
using UnityEngine;

// Token: 0x02000AB7 RID: 2743
public class SimDebugViewCompositor : MonoBehaviour
{
	// Token: 0x06004F88 RID: 20360 RVA: 0x001CBF33 File Offset: 0x001CA133
	private void Awake()
	{
		SimDebugViewCompositor.Instance = this;
	}

	// Token: 0x06004F89 RID: 20361 RVA: 0x001CBF3B File Offset: 0x001CA13B
	private void OnDestroy()
	{
		SimDebugViewCompositor.Instance = null;
	}

	// Token: 0x06004F8A RID: 20362 RVA: 0x001CBF43 File Offset: 0x001CA143
	private void Start()
	{
		this.material = new Material(Shader.Find("Klei/PostFX/SimDebugViewCompositor"));
		this.Toggle(false);
	}

	// Token: 0x06004F8B RID: 20363 RVA: 0x001CBF61 File Offset: 0x001CA161
	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		Graphics.Blit(src, dest, this.material);
	}

	// Token: 0x06004F8C RID: 20364 RVA: 0x001CBF70 File Offset: 0x001CA170
	public void Toggle(bool is_on)
	{
		base.enabled = is_on;
	}

	// Token: 0x04003585 RID: 13701
	public Material material;

	// Token: 0x04003586 RID: 13702
	public static SimDebugViewCompositor Instance;
}
