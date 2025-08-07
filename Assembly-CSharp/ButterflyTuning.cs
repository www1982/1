using System;

// Token: 0x0200009C RID: 156
public static class ButterflyTuning
{
	// Token: 0x040001D6 RID: 470
	public static readonly float SEARCH_COOLDOWN = 60f;

	// Token: 0x040001D7 RID: 471
	public const int SEARCH_RADIUS = 10;

	// Token: 0x040001D8 RID: 472
	public const int EARLY_OUT_SEARCH_THRESHOLD = 5;

	// Token: 0x040001D9 RID: 473
	public const string POLLINATED_EFFECT = "ButterflyPollinated";

	// Token: 0x040001DA RID: 474
	public const float CROP_TENDED_MULTIPLIER_DURATION = 600f;

	// Token: 0x040001DB RID: 475
	public const float CROP_TENDED_MULTIPLIER_EFFECT = 0.25f;

	// Token: 0x040001DC RID: 476
	public const float EFFECT_DECOR_MULTIPLIER = 1f;

	// Token: 0x040001DD RID: 477
	public const float CROP_DURATION = 3000f;

	// Token: 0x040001DE RID: 478
	public const float FERTILIZER_RATE = 0.016666668f;

	// Token: 0x040001DF RID: 479
	public const float LETHAL_LOW = 233.15f;

	// Token: 0x040001E0 RID: 480
	public const float WARNING_LOW = 283.15f;

	// Token: 0x040001E1 RID: 481
	public const float WARNING_HIGH = 318.15f;

	// Token: 0x040001E2 RID: 482
	public const float LETHAL_HIGH = 353.15f;
}
