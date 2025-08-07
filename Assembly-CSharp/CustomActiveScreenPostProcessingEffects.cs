using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AA1 RID: 2721
public class CustomActiveScreenPostProcessingEffects : MonoBehaviour
{
	// Token: 0x06004EF1 RID: 20209 RVA: 0x001C7E94 File Offset: 0x001C6094
	public void RegisterEffect(Func<RenderTexture, Material> effectFn)
	{
		this.ActiveEffects.Add(effectFn);
	}

	// Token: 0x06004EF2 RID: 20210 RVA: 0x001C7EA2 File Offset: 0x001C60A2
	public void UnregisterEffect(Func<RenderTexture, Material> effectFn)
	{
		this.ActiveEffects.Remove(effectFn);
	}

	// Token: 0x06004EF3 RID: 20211 RVA: 0x001C7EB4 File Offset: 0x001C60B4
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.CheckTemporaryRenderTextureValidity(ref this.previousSource, source);
		this.CheckTemporaryRenderTextureValidity(ref this.previousDestination, source);
		Graphics.Blit(source, this.previousSource);
		foreach (Func<RenderTexture, Material> func in this.ActiveEffects)
		{
			Graphics.Blit(this.previousSource, this.previousDestination, func(source));
			this.previousSource.DiscardContents();
			Graphics.Blit(this.previousDestination, this.previousSource);
		}
		Graphics.Blit(this.previousSource, destination);
		this.previousSource.Release();
		this.previousDestination.Release();
	}

	// Token: 0x06004EF4 RID: 20212 RVA: 0x001C7F7C File Offset: 0x001C617C
	private void CheckTemporaryRenderTextureValidity(ref RenderTexture temporaryTexture, RenderTexture source)
	{
		if (temporaryTexture == null || temporaryTexture.width != source.width || temporaryTexture.height != source.height || temporaryTexture.depth != source.depth || temporaryTexture.format != source.format)
		{
			if (temporaryTexture != null)
			{
				temporaryTexture.Release();
			}
			temporaryTexture = RenderTexture.GetTemporary(source.width, source.height, source.depth, source.format);
		}
	}

	// Token: 0x0400346C RID: 13420
	private List<Func<RenderTexture, Material>> ActiveEffects = new List<Func<RenderTexture, Material>>();

	// Token: 0x0400346D RID: 13421
	private RenderTexture previousSource;

	// Token: 0x0400346E RID: 13422
	private RenderTexture previousDestination;
}
