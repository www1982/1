using System;

// Token: 0x02000DDA RID: 3546
public interface ISidescreenButtonControl
{
	// Token: 0x170007B5 RID: 1973
	// (get) Token: 0x06006FF7 RID: 28663
	string SidescreenButtonText { get; }

	// Token: 0x170007B6 RID: 1974
	// (get) Token: 0x06006FF8 RID: 28664
	string SidescreenButtonTooltip { get; }

	// Token: 0x06006FF9 RID: 28665
	void SetButtonTextOverride(ButtonMenuTextOverride textOverride);

	// Token: 0x06006FFA RID: 28666
	bool SidescreenEnabled();

	// Token: 0x06006FFB RID: 28667
	bool SidescreenButtonInteractable();

	// Token: 0x06006FFC RID: 28668
	void OnSidescreenButtonPressed();

	// Token: 0x06006FFD RID: 28669
	int HorizontalGroupID();

	// Token: 0x06006FFE RID: 28670
	int ButtonSideScreenSortOrder();
}
