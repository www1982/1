using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000C98 RID: 3224
public class CodexVideo : CodexWidget<CodexVideo>
{
	// Token: 0x1700073B RID: 1851
	// (get) Token: 0x0600631A RID: 25370 RVA: 0x00253CFD File Offset: 0x00251EFD
	// (set) Token: 0x0600631B RID: 25371 RVA: 0x00253D05 File Offset: 0x00251F05
	public string name { get; set; }

	// Token: 0x1700073C RID: 1852
	// (get) Token: 0x0600631D RID: 25373 RVA: 0x00253D17 File Offset: 0x00251F17
	// (set) Token: 0x0600631C RID: 25372 RVA: 0x00253D0E File Offset: 0x00251F0E
	public string videoName
	{
		get
		{
			return "--> " + (this.name ?? "NULL");
		}
		set
		{
			this.name = value;
		}
	}

	// Token: 0x1700073D RID: 1853
	// (get) Token: 0x0600631E RID: 25374 RVA: 0x00253D32 File Offset: 0x00251F32
	// (set) Token: 0x0600631F RID: 25375 RVA: 0x00253D3A File Offset: 0x00251F3A
	public string overlayName { get; set; }

	// Token: 0x1700073E RID: 1854
	// (get) Token: 0x06006320 RID: 25376 RVA: 0x00253D43 File Offset: 0x00251F43
	// (set) Token: 0x06006321 RID: 25377 RVA: 0x00253D4B File Offset: 0x00251F4B
	public List<string> overlayTexts { get; set; }

	// Token: 0x06006322 RID: 25378 RVA: 0x00253D54 File Offset: 0x00251F54
	public void ConfigureVideo(VideoWidget videoWidget, string clipName, string overlayName = null, List<string> overlayTexts = null)
	{
		videoWidget.SetClip(Assets.GetVideo(clipName), overlayName, overlayTexts);
	}

	// Token: 0x06006323 RID: 25379 RVA: 0x00253D65 File Offset: 0x00251F65
	public override void Configure(GameObject contentGameObject, Transform displayPane, Dictionary<CodexTextStyle, TextStyleSetting> textStyles)
	{
		this.ConfigureVideo(contentGameObject.GetComponent<VideoWidget>(), this.name, this.overlayName, this.overlayTexts);
		base.ConfigurePreferredLayout(contentGameObject);
	}
}
