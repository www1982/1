using System;
using UnityEngine;

// Token: 0x02000BFC RID: 3068
public class FillRenderTargetEffect : MonoBehaviour
{
	// Token: 0x06005C70 RID: 23664 RVA: 0x0021C38D File Offset: 0x0021A58D
	public void SetFillTexture(Texture tex)
	{
		this.fillTexture = tex;
	}

	// Token: 0x06005C71 RID: 23665 RVA: 0x0021C396 File Offset: 0x0021A596
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(this.fillTexture, null);
	}

	// Token: 0x04003D3E RID: 15678
	private Texture fillTexture;
}
