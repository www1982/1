using System;

// Token: 0x02000E37 RID: 3639
public interface ISliderControl
{
	// Token: 0x170007F9 RID: 2041
	// (get) Token: 0x06007355 RID: 29525
	string SliderTitleKey { get; }

	// Token: 0x170007FA RID: 2042
	// (get) Token: 0x06007356 RID: 29526
	string SliderUnits { get; }

	// Token: 0x06007357 RID: 29527
	int SliderDecimalPlaces(int index);

	// Token: 0x06007358 RID: 29528
	float GetSliderMin(int index);

	// Token: 0x06007359 RID: 29529
	float GetSliderMax(int index);

	// Token: 0x0600735A RID: 29530
	float GetSliderValue(int index);

	// Token: 0x0600735B RID: 29531
	void SetSliderValue(float percent, int index);

	// Token: 0x0600735C RID: 29532
	string GetSliderTooltipKey(int index);

	// Token: 0x0600735D RID: 29533
	string GetSliderTooltip(int index);
}
