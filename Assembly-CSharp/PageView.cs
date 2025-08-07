using System;
using UnityEngine;

// Token: 0x02000D92 RID: 3474
[AddComponentMenu("KMonoBehaviour/scripts/PageView")]
public class PageView : KMonoBehaviour
{
	// Token: 0x17000793 RID: 1939
	// (get) Token: 0x06006C56 RID: 27734 RVA: 0x0028E8EA File Offset: 0x0028CAEA
	public int ChildrenPerPage
	{
		get
		{
			return this.childrenPerPage;
		}
	}

	// Token: 0x06006C57 RID: 27735 RVA: 0x0028E8F2 File Offset: 0x0028CAF2
	private void Update()
	{
		if (this.oldChildCount != base.transform.childCount)
		{
			this.oldChildCount = base.transform.childCount;
			this.RefreshPage();
		}
	}

	// Token: 0x06006C58 RID: 27736 RVA: 0x0028E920 File Offset: 0x0028CB20
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		MultiToggle multiToggle = this.nextButton;
		multiToggle.onClick = (global::System.Action)Delegate.Combine(multiToggle.onClick, new global::System.Action(delegate
		{
			this.currentPage = (this.currentPage + 1) % this.pageCount;
			if (this.OnChangePage != null)
			{
				this.OnChangePage(this.currentPage);
			}
			this.RefreshPage();
		}));
		MultiToggle multiToggle2 = this.prevButton;
		multiToggle2.onClick = (global::System.Action)Delegate.Combine(multiToggle2.onClick, new global::System.Action(delegate
		{
			this.currentPage--;
			if (this.currentPage < 0)
			{
				this.currentPage += this.pageCount;
			}
			if (this.OnChangePage != null)
			{
				this.OnChangePage(this.currentPage);
			}
			this.RefreshPage();
		}));
	}

	// Token: 0x17000794 RID: 1940
	// (get) Token: 0x06006C59 RID: 27737 RVA: 0x0028E984 File Offset: 0x0028CB84
	private int pageCount
	{
		get
		{
			int num = base.transform.childCount / this.childrenPerPage;
			if (base.transform.childCount % this.childrenPerPage != 0)
			{
				num++;
			}
			return num;
		}
	}

	// Token: 0x06006C5A RID: 27738 RVA: 0x0028E9C0 File Offset: 0x0028CBC0
	private void RefreshPage()
	{
		for (int i = 0; i < base.transform.childCount; i++)
		{
			if (i < this.currentPage * this.childrenPerPage)
			{
				base.transform.GetChild(i).gameObject.SetActive(false);
			}
			else if (i >= this.currentPage * this.childrenPerPage + this.childrenPerPage)
			{
				base.transform.GetChild(i).gameObject.SetActive(false);
			}
			else
			{
				base.transform.GetChild(i).gameObject.SetActive(true);
			}
		}
		this.pageLabel.SetText((this.currentPage % this.pageCount + 1).ToString() + "/" + this.pageCount.ToString());
	}

	// Token: 0x040049DD RID: 18909
	[SerializeField]
	private MultiToggle nextButton;

	// Token: 0x040049DE RID: 18910
	[SerializeField]
	private MultiToggle prevButton;

	// Token: 0x040049DF RID: 18911
	[SerializeField]
	private LocText pageLabel;

	// Token: 0x040049E0 RID: 18912
	[SerializeField]
	private int childrenPerPage = 8;

	// Token: 0x040049E1 RID: 18913
	private int currentPage;

	// Token: 0x040049E2 RID: 18914
	private int oldChildCount;

	// Token: 0x040049E3 RID: 18915
	public Action<int> OnChangePage;
}
