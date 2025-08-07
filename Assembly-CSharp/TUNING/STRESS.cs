using System;

namespace TUNING
{
	// Token: 0x02000F84 RID: 3972
	public class STRESS
	{
		// Token: 0x04005BB3 RID: 23475
		public static float ACTING_OUT_RESET = 60f;

		// Token: 0x04005BB4 RID: 23476
		public static float VOMIT_AMOUNT = 0.90000004f;

		// Token: 0x04005BB5 RID: 23477
		public static float TEARS_RATE = 0.040000003f;

		// Token: 0x04005BB6 RID: 23478
		public static int BANSHEE_WAIL_RADIUS = 8;

		// Token: 0x0200213F RID: 8511
		public class SHOCKER
		{
			// Token: 0x0400981A RID: 38938
			public static int SHOCK_RADIUS = 4;

			// Token: 0x0400981B RID: 38939
			public static float DAMAGE_RATE = 2.5f;

			// Token: 0x0400981C RID: 38940
			public static float POWER_CONSUMPTION_RATE = 2000f;

			// Token: 0x0400981D RID: 38941
			public static float FAKE_POWER_CONSUMPTION_RATE = STRESS.SHOCKER.POWER_CONSUMPTION_RATE * 0.25f;

			// Token: 0x0400981E RID: 38942
			public static float MAX_POWER_USE = 120000f;
		}
	}
}
