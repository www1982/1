using System;

namespace TUNING
{
	// Token: 0x02000F9C RID: 3996
	public class METEORS
	{
		// Token: 0x0200218A RID: 8586
		public class DIFFICULTY
		{
			// Token: 0x02002937 RID: 10551
			public class PEROID_MULTIPLIER
			{
				// Token: 0x0400B65A RID: 46682
				public const float INFREQUENT = 2f;

				// Token: 0x0400B65B RID: 46683
				public const float INTENSE = 1f;

				// Token: 0x0400B65C RID: 46684
				public const float DOOMED = 1f;
			}

			// Token: 0x02002938 RID: 10552
			public class SECONDS_PER_METEOR_MULTIPLIER
			{
				// Token: 0x0400B65D RID: 46685
				public const float INFREQUENT = 1.5f;

				// Token: 0x0400B65E RID: 46686
				public const float INTENSE = 0.8f;

				// Token: 0x0400B65F RID: 46687
				public const float DOOMED = 0.5f;
			}

			// Token: 0x02002939 RID: 10553
			public class BOMBARD_OFF_MULTIPLIER
			{
				// Token: 0x0400B660 RID: 46688
				public const float INFREQUENT = 1f;

				// Token: 0x0400B661 RID: 46689
				public const float INTENSE = 1f;

				// Token: 0x0400B662 RID: 46690
				public const float DOOMED = 0.5f;
			}

			// Token: 0x0200293A RID: 10554
			public class BOMBARD_ON_MULTIPLIER
			{
				// Token: 0x0400B663 RID: 46691
				public const float INFREQUENT = 1f;

				// Token: 0x0400B664 RID: 46692
				public const float INTENSE = 1f;

				// Token: 0x0400B665 RID: 46693
				public const float DOOMED = 1f;
			}

			// Token: 0x0200293B RID: 10555
			public class MASS_MULTIPLIER
			{
				// Token: 0x0400B666 RID: 46694
				public const float INFREQUENT = 1f;

				// Token: 0x0400B667 RID: 46695
				public const float INTENSE = 0.8f;

				// Token: 0x0400B668 RID: 46696
				public const float DOOMED = 0.5f;
			}
		}

		// Token: 0x0200218B RID: 8587
		public class IDENTIFY_DURATION
		{
			// Token: 0x04009A2E RID: 39470
			public const float TIER1 = 20f;
		}

		// Token: 0x0200218C RID: 8588
		public class PEROID
		{
			// Token: 0x04009A2F RID: 39471
			public const float TIER1 = 5f;

			// Token: 0x04009A30 RID: 39472
			public const float TIER2 = 10f;

			// Token: 0x04009A31 RID: 39473
			public const float TIER3 = 20f;

			// Token: 0x04009A32 RID: 39474
			public const float TIER4 = 30f;
		}

		// Token: 0x0200218D RID: 8589
		public class DURATION
		{
			// Token: 0x04009A33 RID: 39475
			public const float TIER0 = 1800f;

			// Token: 0x04009A34 RID: 39476
			public const float TIER1 = 3000f;

			// Token: 0x04009A35 RID: 39477
			public const float TIER2 = 4200f;

			// Token: 0x04009A36 RID: 39478
			public const float TIER3 = 6000f;
		}

		// Token: 0x0200218E RID: 8590
		public class DURATION_CLUSTER
		{
			// Token: 0x04009A37 RID: 39479
			public const float TIER0 = 75f;

			// Token: 0x04009A38 RID: 39480
			public const float TIER1 = 150f;

			// Token: 0x04009A39 RID: 39481
			public const float TIER2 = 300f;

			// Token: 0x04009A3A RID: 39482
			public const float TIER3 = 600f;

			// Token: 0x04009A3B RID: 39483
			public const float TIER4 = 1800f;

			// Token: 0x04009A3C RID: 39484
			public const float TIER5 = 3000f;
		}

		// Token: 0x0200218F RID: 8591
		public class TRAVEL_DURATION
		{
			// Token: 0x04009A3D RID: 39485
			public const float TIER0 = 600f;

			// Token: 0x04009A3E RID: 39486
			public const float TIER1 = 3000f;

			// Token: 0x04009A3F RID: 39487
			public const float TIER2 = 4500f;

			// Token: 0x04009A40 RID: 39488
			public const float TIER3 = 6000f;

			// Token: 0x04009A41 RID: 39489
			public const float TIER4 = 12000f;

			// Token: 0x04009A42 RID: 39490
			public const float TIER5 = 30000f;

			// Token: 0x04009A43 RID: 39491
			public const float TIER6 = 60000f;
		}

		// Token: 0x02002190 RID: 8592
		public class BOMBARDMENT_ON
		{
			// Token: 0x04009A44 RID: 39492
			public static MathUtil.MinMax NONE = new MathUtil.MinMax(1f, 1f);

			// Token: 0x04009A45 RID: 39493
			public static MathUtil.MinMax UNLIMITED = new MathUtil.MinMax(10000f, 10000f);

			// Token: 0x04009A46 RID: 39494
			public static MathUtil.MinMax CYCLE = new MathUtil.MinMax(600f, 600f);
		}

		// Token: 0x02002191 RID: 8593
		public class BOMBARDMENT_OFF
		{
			// Token: 0x04009A47 RID: 39495
			public static MathUtil.MinMax NONE = new MathUtil.MinMax(1f, 1f);
		}

		// Token: 0x02002192 RID: 8594
		public class TRAVELDURATION
		{
			// Token: 0x04009A48 RID: 39496
			public static float TIER0 = 0f;

			// Token: 0x04009A49 RID: 39497
			public static float TIER1 = 5f;

			// Token: 0x04009A4A RID: 39498
			public static float TIER2 = 10f;

			// Token: 0x04009A4B RID: 39499
			public static float TIER3 = 20f;

			// Token: 0x04009A4C RID: 39500
			public static float TIER4 = 30f;
		}
	}
}
