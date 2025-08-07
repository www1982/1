using System;

// Token: 0x02000E31 RID: 3633
public interface ICheckboxControl
{
	// Token: 0x170007EF RID: 2031
	// (get) Token: 0x06007311 RID: 29457
	string CheckboxTitleKey { get; }

	// Token: 0x170007F0 RID: 2032
	// (get) Token: 0x06007312 RID: 29458
	string CheckboxLabel { get; }

	// Token: 0x170007F1 RID: 2033
	// (get) Token: 0x06007313 RID: 29459
	string CheckboxTooltip { get; }

	// Token: 0x06007314 RID: 29460
	bool GetCheckboxValue();

	// Token: 0x06007315 RID: 29461
	void SetCheckboxValue(bool value);
}
