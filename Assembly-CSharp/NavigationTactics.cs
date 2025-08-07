using System;

// Token: 0x020005E9 RID: 1513
public static class NavigationTactics
{
	// Token: 0x04001474 RID: 5236
	public static NavTactic ReduceTravelDistance = new NavTactic(0, 0, 1, 4);

	// Token: 0x04001475 RID: 5237
	public static NavTactic Range_2_AvoidOverlaps = new NavTactic(2, 6, 12, 1);

	// Token: 0x04001476 RID: 5238
	public static NavTactic Range_3_ProhibitOverlap = new NavTactic(3, 6, 9999, 1);

	// Token: 0x04001477 RID: 5239
	public static NavTactic FetchDronePickup = new NavTactic(1, 0, 0, 0, 1, 0, 1, 1);
}
