using System;

// Token: 0x02000E0E RID: 3598
public interface IMultiSliderControl
{
	// Token: 0x170007D0 RID: 2000
	// (get) Token: 0x06007194 RID: 29076
	string SidescreenTitleKey { get; }

	// Token: 0x06007195 RID: 29077
	bool SidescreenEnabled();

	// Token: 0x170007D1 RID: 2001
	// (get) Token: 0x06007196 RID: 29078
	ISliderControl[] sliderControls { get; }
}
