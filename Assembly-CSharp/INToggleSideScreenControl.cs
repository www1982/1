using System;
using System.Collections.Generic;

// Token: 0x02000E10 RID: 3600
public interface INToggleSideScreenControl
{
	// Token: 0x170007D2 RID: 2002
	// (get) Token: 0x0600719B RID: 29083
	string SidescreenTitleKey { get; }

	// Token: 0x170007D3 RID: 2003
	// (get) Token: 0x0600719C RID: 29084
	List<LocString> Options { get; }

	// Token: 0x170007D4 RID: 2004
	// (get) Token: 0x0600719D RID: 29085
	List<LocString> Tooltips { get; }

	// Token: 0x170007D5 RID: 2005
	// (get) Token: 0x0600719E RID: 29086
	string Description { get; }

	// Token: 0x170007D6 RID: 2006
	// (get) Token: 0x0600719F RID: 29087
	int SelectedOption { get; }

	// Token: 0x170007D7 RID: 2007
	// (get) Token: 0x060071A0 RID: 29088
	int QueuedOption { get; }

	// Token: 0x060071A1 RID: 29089
	void QueueSelectedOption(int option);
}
