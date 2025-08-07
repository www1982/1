using System;

// Token: 0x02000DCD RID: 3533
public interface IActivationRangeTarget
{
	// Token: 0x170007A8 RID: 1960
	// (get) Token: 0x06006F67 RID: 28519
	// (set) Token: 0x06006F68 RID: 28520
	float ActivateValue { get; set; }

	// Token: 0x170007A9 RID: 1961
	// (get) Token: 0x06006F69 RID: 28521
	// (set) Token: 0x06006F6A RID: 28522
	float DeactivateValue { get; set; }

	// Token: 0x170007AA RID: 1962
	// (get) Token: 0x06006F6B RID: 28523
	float MinValue { get; }

	// Token: 0x170007AB RID: 1963
	// (get) Token: 0x06006F6C RID: 28524
	float MaxValue { get; }

	// Token: 0x170007AC RID: 1964
	// (get) Token: 0x06006F6D RID: 28525
	bool UseWholeNumbers { get; }

	// Token: 0x170007AD RID: 1965
	// (get) Token: 0x06006F6E RID: 28526
	string ActivationRangeTitleText { get; }

	// Token: 0x170007AE RID: 1966
	// (get) Token: 0x06006F6F RID: 28527
	string ActivateSliderLabelText { get; }

	// Token: 0x170007AF RID: 1967
	// (get) Token: 0x06006F70 RID: 28528
	string DeactivateSliderLabelText { get; }

	// Token: 0x170007B0 RID: 1968
	// (get) Token: 0x06006F71 RID: 28529
	string ActivateTooltip { get; }

	// Token: 0x170007B1 RID: 1969
	// (get) Token: 0x06006F72 RID: 28530
	string DeactivateTooltip { get; }
}
