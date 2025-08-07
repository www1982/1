using System;

// Token: 0x020007D8 RID: 2008
public interface IThresholdSwitch
{
	// Token: 0x1700038C RID: 908
	// (get) Token: 0x0600362C RID: 13868
	// (set) Token: 0x0600362D RID: 13869
	float Threshold { get; set; }

	// Token: 0x1700038D RID: 909
	// (get) Token: 0x0600362E RID: 13870
	// (set) Token: 0x0600362F RID: 13871
	bool ActivateAboveThreshold { get; set; }

	// Token: 0x1700038E RID: 910
	// (get) Token: 0x06003630 RID: 13872
	float CurrentValue { get; }

	// Token: 0x1700038F RID: 911
	// (get) Token: 0x06003631 RID: 13873
	float RangeMin { get; }

	// Token: 0x17000390 RID: 912
	// (get) Token: 0x06003632 RID: 13874
	float RangeMax { get; }

	// Token: 0x06003633 RID: 13875
	float GetRangeMinInputField();

	// Token: 0x06003634 RID: 13876
	float GetRangeMaxInputField();

	// Token: 0x17000391 RID: 913
	// (get) Token: 0x06003635 RID: 13877
	LocString Title { get; }

	// Token: 0x17000392 RID: 914
	// (get) Token: 0x06003636 RID: 13878
	LocString ThresholdValueName { get; }

	// Token: 0x06003637 RID: 13879
	LocString ThresholdValueUnits();

	// Token: 0x06003638 RID: 13880
	string Format(float value, bool units);

	// Token: 0x17000393 RID: 915
	// (get) Token: 0x06003639 RID: 13881
	string AboveToolTip { get; }

	// Token: 0x17000394 RID: 916
	// (get) Token: 0x0600363A RID: 13882
	string BelowToolTip { get; }

	// Token: 0x0600363B RID: 13883
	float ProcessedSliderValue(float input);

	// Token: 0x0600363C RID: 13884
	float ProcessedInputValue(float input);

	// Token: 0x17000395 RID: 917
	// (get) Token: 0x0600363D RID: 13885
	ThresholdScreenLayoutType LayoutType { get; }

	// Token: 0x17000396 RID: 918
	// (get) Token: 0x0600363E RID: 13886
	int IncrementScale { get; }

	// Token: 0x17000397 RID: 919
	// (get) Token: 0x0600363F RID: 13887
	NonLinearSlider.Range[] GetRanges { get; }
}
