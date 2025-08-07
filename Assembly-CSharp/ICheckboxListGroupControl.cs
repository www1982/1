using System;

// Token: 0x02000DDF RID: 3551
public interface ICheckboxListGroupControl
{
	// Token: 0x170007BF RID: 1983
	// (get) Token: 0x0600701B RID: 28699
	string Title { get; }

	// Token: 0x170007C0 RID: 1984
	// (get) Token: 0x0600701C RID: 28700
	string Description { get; }

	// Token: 0x0600701D RID: 28701
	ICheckboxListGroupControl.ListGroup[] GetData();

	// Token: 0x0600701E RID: 28702
	bool SidescreenEnabled();

	// Token: 0x0600701F RID: 28703
	int CheckboxSideScreenSortOrder();

	// Token: 0x02001FF5 RID: 8181
	public struct ListGroup
	{
		// Token: 0x0600B4E3 RID: 46307 RVA: 0x003DE586 File Offset: 0x003DC786
		public ListGroup(string title, ICheckboxListGroupControl.CheckboxItem[] checkboxItems, Func<string, string> resolveTitleCallback = null, global::System.Action onItemClicked = null)
		{
			this.title = title;
			this.checkboxItems = checkboxItems;
			this.resolveTitleCallback = resolveTitleCallback;
			this.onItemClicked = onItemClicked;
		}

		// Token: 0x040092AF RID: 37551
		public Func<string, string> resolveTitleCallback;

		// Token: 0x040092B0 RID: 37552
		public global::System.Action onItemClicked;

		// Token: 0x040092B1 RID: 37553
		public string title;

		// Token: 0x040092B2 RID: 37554
		public ICheckboxListGroupControl.CheckboxItem[] checkboxItems;
	}

	// Token: 0x02001FF6 RID: 8182
	public struct CheckboxItem
	{
		// Token: 0x040092B3 RID: 37555
		public string text;

		// Token: 0x040092B4 RID: 37556
		public string tooltip;

		// Token: 0x040092B5 RID: 37557
		public bool isOn;

		// Token: 0x040092B6 RID: 37558
		public Func<string, bool> overrideLinkActions;

		// Token: 0x040092B7 RID: 37559
		public Func<string, object, string> resolveTooltipCallback;
	}
}
