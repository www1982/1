using System;
using UnityEngine;

// Token: 0x02000A9E RID: 2718
public static class Blur
{
	// Token: 0x06004EE7 RID: 20199 RVA: 0x001C7CFE File Offset: 0x001C5EFE
	public static RenderTexture Run(Texture2D image)
	{
		if (Blur.blurMaterial == null)
		{
			Blur.blurMaterial = new Material(Shader.Find("Klei/PostFX/Blur"));
		}
		return null;
	}

	// Token: 0x04003466 RID: 13414
	private static Material blurMaterial;
}
