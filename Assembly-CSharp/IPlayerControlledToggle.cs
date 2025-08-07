using System;

// Token: 0x02000E1A RID: 3610
public interface IPlayerControlledToggle
{
	// Token: 0x0600721D RID: 29213
	void ToggledByPlayer();

	// Token: 0x0600721E RID: 29214
	bool ToggledOn();

	// Token: 0x0600721F RID: 29215
	KSelectable GetSelectable();

	// Token: 0x170007E6 RID: 2022
	// (get) Token: 0x06007220 RID: 29216
	string SideScreenTitleKey { get; }

	// Token: 0x170007E7 RID: 2023
	// (get) Token: 0x06007221 RID: 29217
	// (set) Token: 0x06007222 RID: 29218
	bool ToggleRequested { get; set; }
}
