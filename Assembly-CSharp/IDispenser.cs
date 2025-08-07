using System;
using System.Collections.Generic;

// Token: 0x02000DF0 RID: 3568
public interface IDispenser
{
	// Token: 0x060070B8 RID: 28856
	List<Tag> DispensedItems();

	// Token: 0x060070B9 RID: 28857
	Tag SelectedItem();

	// Token: 0x060070BA RID: 28858
	void SelectItem(Tag tag);

	// Token: 0x060070BB RID: 28859
	void OnOrderDispense();

	// Token: 0x060070BC RID: 28860
	void OnCancelDispense();

	// Token: 0x060070BD RID: 28861
	bool HasOpenChore();

	// Token: 0x14000030 RID: 48
	// (add) Token: 0x060070BE RID: 28862
	// (remove) Token: 0x060070BF RID: 28863
	event global::System.Action OnStopWorkEvent;
}
