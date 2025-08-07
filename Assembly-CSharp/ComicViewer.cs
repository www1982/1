using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000C9F RID: 3231
public class ComicViewer : KScreen
{
	// Token: 0x06006373 RID: 25459 RVA: 0x00255D80 File Offset: 0x00253F80
	public void ShowComic(ComicData comic, bool isVictoryComic)
	{
		for (int i = 0; i < Mathf.Max(comic.images.Length, comic.stringKeys.Length); i++)
		{
			GameObject gameObject = Util.KInstantiateUI(this.panelPrefab, this.contentContainer, true);
			this.activePanels.Add(gameObject);
			gameObject.GetComponentInChildren<Image>().sprite = comic.images[i];
			gameObject.GetComponentInChildren<LocText>().SetText(comic.stringKeys[i]);
		}
		this.closeButton.ClearOnClick();
		if (isVictoryComic)
		{
			this.closeButton.onClick += delegate
			{
				this.Stop();
				this.Show(false);
			};
			return;
		}
		this.closeButton.onClick += delegate
		{
			this.Stop();
		};
	}

	// Token: 0x06006374 RID: 25460 RVA: 0x00255E2F File Offset: 0x0025402F
	public void Stop()
	{
		this.OnStop();
		this.Show(false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x04004393 RID: 17299
	public GameObject panelPrefab;

	// Token: 0x04004394 RID: 17300
	public GameObject contentContainer;

	// Token: 0x04004395 RID: 17301
	public List<GameObject> activePanels = new List<GameObject>();

	// Token: 0x04004396 RID: 17302
	public KButton closeButton;

	// Token: 0x04004397 RID: 17303
	public global::System.Action OnStop;
}
