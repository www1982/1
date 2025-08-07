using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000E89 RID: 3721
public class OptionSelector : MonoBehaviour
{
	// Token: 0x06007691 RID: 30353 RVA: 0x002D6049 File Offset: 0x002D4249
	private void Start()
	{
		this.selectedItem.GetComponent<KButton>().onBtnClick += this.OnClick;
	}

	// Token: 0x06007692 RID: 30354 RVA: 0x002D6067 File Offset: 0x002D4267
	public void Initialize(object id)
	{
		this.id = id;
	}

	// Token: 0x06007693 RID: 30355 RVA: 0x002D6070 File Offset: 0x002D4270
	private void OnClick(KKeyCode button)
	{
		if (button == KKeyCode.Mouse0)
		{
			this.OnChangePriority(this.id, 1);
			return;
		}
		if (button != KKeyCode.Mouse1)
		{
			return;
		}
		this.OnChangePriority(this.id, -1);
	}

	// Token: 0x06007694 RID: 30356 RVA: 0x002D60A8 File Offset: 0x002D42A8
	public void ConfigureItem(bool disabled, OptionSelector.DisplayOptionInfo display_info)
	{
		HierarchyReferences component = this.selectedItem.GetComponent<HierarchyReferences>();
		KImage kimage = component.GetReference("BG") as KImage;
		if (display_info.bgOptions == null)
		{
			kimage.gameObject.SetActive(false);
		}
		else
		{
			kimage.sprite = display_info.bgOptions[display_info.bgIndex];
		}
		KImage kimage2 = component.GetReference("FG") as KImage;
		if (display_info.fgOptions == null)
		{
			kimage2.gameObject.SetActive(false);
		}
		else
		{
			kimage2.sprite = display_info.fgOptions[display_info.fgIndex];
		}
		KImage kimage3 = component.GetReference("Fill") as KImage;
		if (kimage3 != null)
		{
			kimage3.enabled = !disabled;
			kimage3.color = display_info.fillColour;
		}
		KImage kimage4 = component.GetReference("Outline") as KImage;
		if (kimage4 != null)
		{
			kimage4.enabled = !disabled;
		}
	}

	// Token: 0x04005289 RID: 21129
	private object id;

	// Token: 0x0400528A RID: 21130
	public Action<object, int> OnChangePriority;

	// Token: 0x0400528B RID: 21131
	[SerializeField]
	private KImage selectedItem;

	// Token: 0x0400528C RID: 21132
	[SerializeField]
	private KImage itemTemplate;

	// Token: 0x0200207B RID: 8315
	public class DisplayOptionInfo
	{
		// Token: 0x04009462 RID: 37986
		public IList<Sprite> bgOptions;

		// Token: 0x04009463 RID: 37987
		public IList<Sprite> fgOptions;

		// Token: 0x04009464 RID: 37988
		public int bgIndex;

		// Token: 0x04009465 RID: 37989
		public int fgIndex;

		// Token: 0x04009466 RID: 37990
		public Color32 fillColour;
	}
}
