using System;

namespace TUNING
{
	// Token: 0x02000F87 RID: 3975
	public class ROBOTS
	{
		// Token: 0x02002147 RID: 8519
		public class SCOUTBOT
		{
			// Token: 0x0400984F RID: 38991
			public static float CARRY_CAPACITY = DUPLICANTSTATS.STANDARD.BaseStats.CARRY_CAPACITY;

			// Token: 0x04009850 RID: 38992
			public static readonly float DIGGING = 1f;

			// Token: 0x04009851 RID: 38993
			public static readonly float CONSTRUCTION = 1f;

			// Token: 0x04009852 RID: 38994
			public static readonly float ATHLETICS = 1f;

			// Token: 0x04009853 RID: 38995
			public static readonly float HIT_POINTS = 100f;

			// Token: 0x04009854 RID: 38996
			public static readonly float BATTERY_DEPLETION_RATE = 30f;

			// Token: 0x04009855 RID: 38997
			public static readonly float BATTERY_CAPACITY = ROBOTS.SCOUTBOT.BATTERY_DEPLETION_RATE * 10f * 600f;
		}

		// Token: 0x02002148 RID: 8520
		public class MORBBOT
		{
			// Token: 0x04009856 RID: 38998
			public static float CARRY_CAPACITY = DUPLICANTSTATS.STANDARD.BaseStats.CARRY_CAPACITY * 2f;

			// Token: 0x04009857 RID: 38999
			public const float DIGGING = 1f;

			// Token: 0x04009858 RID: 39000
			public const float CONSTRUCTION = 1f;

			// Token: 0x04009859 RID: 39001
			public const float ATHLETICS = 3f;

			// Token: 0x0400985A RID: 39002
			public static readonly float HIT_POINTS = 100f;

			// Token: 0x0400985B RID: 39003
			public const float LIFETIME = 6000f;

			// Token: 0x0400985C RID: 39004
			public const float BATTERY_DEPLETION_RATE = 30f;

			// Token: 0x0400985D RID: 39005
			public const float BATTERY_CAPACITY = 180000f;

			// Token: 0x0400985E RID: 39006
			public const float DECONSTRUCTION_WORK_TIME = 10f;
		}

		// Token: 0x02002149 RID: 8521
		public class FETCHDRONE
		{
			// Token: 0x0400985F RID: 39007
			public static float CARRY_CAPACITY = DUPLICANTSTATS.STANDARD.BaseStats.CARRY_CAPACITY * 2f;

			// Token: 0x04009860 RID: 39008
			public static readonly float HIT_POINTS = 100f;

			// Token: 0x04009861 RID: 39009
			public const float BATTERY_DEPLETION_RATE = 50f;
		}
	}
}
