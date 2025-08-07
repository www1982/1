using System;

namespace TUNING
{
	// Token: 0x02000F91 RID: 3985
	public class RADIATION
	{
		// Token: 0x04005C54 RID: 23636
		public const float GERM_RAD_SCALE = 0.01f;

		// Token: 0x04005C55 RID: 23637
		public const float STANDARD_DAILY_RECOVERY = 100f;

		// Token: 0x04005C56 RID: 23638
		public const float EXTRA_VOMIT_RECOVERY = 20f;

		// Token: 0x04005C57 RID: 23639
		public const float REACT_THRESHOLD = 133f;

		// Token: 0x02002170 RID: 8560
		public class STANDARD_EMITTER
		{
			// Token: 0x04009982 RID: 39298
			public const float STEADY_PULSE_RATE = 0.2f;

			// Token: 0x04009983 RID: 39299
			public const float DOUBLE_SPEED_PULSE_RATE = 0.1f;

			// Token: 0x04009984 RID: 39300
			public const float RADIUS_SCALE = 1f;
		}

		// Token: 0x02002171 RID: 8561
		public class RADIATION_PER_SECOND
		{
			// Token: 0x04009985 RID: 39301
			public const float TRIVIAL = 60f;

			// Token: 0x04009986 RID: 39302
			public const float VERY_LOW = 120f;

			// Token: 0x04009987 RID: 39303
			public const float LOW = 240f;

			// Token: 0x04009988 RID: 39304
			public const float MODERATE = 600f;

			// Token: 0x04009989 RID: 39305
			public const float HIGH = 1800f;

			// Token: 0x0400998A RID: 39306
			public const float VERY_HIGH = 4800f;

			// Token: 0x0400998B RID: 39307
			public const int EXTREME = 9600;
		}

		// Token: 0x02002172 RID: 8562
		public class RADIATION_CONSTANT_RADS_PER_CYCLE
		{
			// Token: 0x0400998C RID: 39308
			public const float LESS_THAN_TRIVIAL = 60f;

			// Token: 0x0400998D RID: 39309
			public const float TRIVIAL = 120f;

			// Token: 0x0400998E RID: 39310
			public const float VERY_LOW = 240f;

			// Token: 0x0400998F RID: 39311
			public const float LOW = 480f;

			// Token: 0x04009990 RID: 39312
			public const float MODERATE = 1200f;

			// Token: 0x04009991 RID: 39313
			public const float MODERATE_PLUS = 2400f;

			// Token: 0x04009992 RID: 39314
			public const float HIGH = 3600f;

			// Token: 0x04009993 RID: 39315
			public const float VERY_HIGH = 8400f;

			// Token: 0x04009994 RID: 39316
			public const int EXTREME = 16800;
		}
	}
}
