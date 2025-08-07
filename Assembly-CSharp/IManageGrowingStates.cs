using System;

// Token: 0x0200086F RID: 2159
public interface IManageGrowingStates
{
	// Token: 0x06003B4F RID: 15183
	float TimeUntilNextHarvest();

	// Token: 0x06003B50 RID: 15184
	float PercentGrown();

	// Token: 0x06003B51 RID: 15185
	Crop GetCropComponent();

	// Token: 0x06003B52 RID: 15186
	void OverrideMaturityLevel(float percentage);

	// Token: 0x06003B53 RID: 15187
	float DomesticGrowthTime();

	// Token: 0x06003B54 RID: 15188
	float WildGrowthTime();

	// Token: 0x06003B55 RID: 15189
	bool IsWildPlanted();
}
