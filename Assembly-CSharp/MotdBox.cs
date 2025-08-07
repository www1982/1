using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000D71 RID: 3441
public class MotdBox : KMonoBehaviour
{
	// Token: 0x06006AF7 RID: 27383 RVA: 0x002865B4 File Offset: 0x002847B4
	public void Config(MotdBox.PageData[] data)
	{
		this.pageDatas = data;
		if (this.pageButtons != null)
		{
			for (int i = this.pageButtons.Length - 1; i >= 0; i--)
			{
				global::UnityEngine.Object.Destroy(this.pageButtons[i]);
			}
			this.pageButtons = null;
		}
		this.pageButtons = new GameObject[data.Length];
		for (int j = 0; j < this.pageButtons.Length; j++)
		{
			int idx = j;
			GameObject gameObject = Util.KInstantiateUI(this.pageCarouselButtonPrefab, this.pageCarouselContainer, false);
			gameObject.SetActive(true);
			this.pageButtons[j] = gameObject;
			MultiToggle component = gameObject.GetComponent<MultiToggle>();
			component.onClick = (global::System.Action)Delegate.Combine(component.onClick, new global::System.Action(delegate
			{
				this.SwitchPage(idx);
			}));
		}
		this.SwitchPage(0);
	}

	// Token: 0x06006AF8 RID: 27384 RVA: 0x00286680 File Offset: 0x00284880
	private void SwitchPage(int newPage)
	{
		this.selectedPage = newPage;
		for (int i = 0; i < this.pageButtons.Length; i++)
		{
			this.pageButtons[i].GetComponent<MultiToggle>().ChangeState((i == this.selectedPage) ? 1 : 0);
		}
		this.image.texture = this.pageDatas[newPage].Texture;
		this.headerLabel.SetText(this.pageDatas[newPage].HeaderText);
		this.urlOpener.SetURL(this.pageDatas[newPage].URL);
		if (string.IsNullOrEmpty(this.pageDatas[newPage].ImageText))
		{
			this.imageLabel.gameObject.SetActive(false);
			this.imageLabel.SetText("");
			return;
		}
		this.imageLabel.gameObject.SetActive(true);
		this.imageLabel.SetText(this.pageDatas[newPage].ImageText);
	}

	// Token: 0x040048DA RID: 18650
	[SerializeField]
	private GameObject pageCarouselContainer;

	// Token: 0x040048DB RID: 18651
	[SerializeField]
	private GameObject pageCarouselButtonPrefab;

	// Token: 0x040048DC RID: 18652
	[SerializeField]
	private RawImage image;

	// Token: 0x040048DD RID: 18653
	[SerializeField]
	private LocText headerLabel;

	// Token: 0x040048DE RID: 18654
	[SerializeField]
	private LocText imageLabel;

	// Token: 0x040048DF RID: 18655
	[SerializeField]
	private URLOpenFunction urlOpener;

	// Token: 0x040048E0 RID: 18656
	private int selectedPage;

	// Token: 0x040048E1 RID: 18657
	private GameObject[] pageButtons;

	// Token: 0x040048E2 RID: 18658
	private MotdBox.PageData[] pageDatas;

	// Token: 0x02001F59 RID: 8025
	public class PageData
	{
		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x0600B30D RID: 45837 RVA: 0x003D8DB3 File Offset: 0x003D6FB3
		// (set) Token: 0x0600B30E RID: 45838 RVA: 0x003D8DBB File Offset: 0x003D6FBB
		public Texture2D Texture { get; set; }

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x0600B30F RID: 45839 RVA: 0x003D8DC4 File Offset: 0x003D6FC4
		// (set) Token: 0x0600B310 RID: 45840 RVA: 0x003D8DCC File Offset: 0x003D6FCC
		public string HeaderText { get; set; }

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x0600B311 RID: 45841 RVA: 0x003D8DD5 File Offset: 0x003D6FD5
		// (set) Token: 0x0600B312 RID: 45842 RVA: 0x003D8DDD File Offset: 0x003D6FDD
		public string ImageText { get; set; }

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x0600B313 RID: 45843 RVA: 0x003D8DE6 File Offset: 0x003D6FE6
		// (set) Token: 0x0600B314 RID: 45844 RVA: 0x003D8DEE File Offset: 0x003D6FEE
		public string URL { get; set; }
	}
}
