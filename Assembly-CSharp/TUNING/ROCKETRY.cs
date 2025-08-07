using System;
using UnityEngine;

namespace TUNING
{
	// Token: 0x02000F98 RID: 3992
	public class ROCKETRY
	{
		// Token: 0x06007C51 RID: 31825 RVA: 0x0031E161 File Offset: 0x0031C361
		public static float MassFromPenaltyPercentage(float penaltyPercentage = 0.5f)
		{
			return -(1f / Mathf.Pow(penaltyPercentage - 1f, 5f));
		}

		// Token: 0x06007C52 RID: 31826 RVA: 0x0031E17C File Offset: 0x0031C37C
		public static float CalculateMassWithPenalty(float realMass)
		{
			float num = Mathf.Pow(realMass / ROCKETRY.MASS_PENALTY_DIVISOR, ROCKETRY.MASS_PENALTY_EXPONENT);
			return Mathf.Max(realMass, num);
		}

		// Token: 0x04005CE9 RID: 23785
		public static float MISSION_DURATION_SCALE = 1800f;

		// Token: 0x04005CEA RID: 23786
		public static float MASS_PENALTY_EXPONENT = 3.2f;

		// Token: 0x04005CEB RID: 23787
		public static float MASS_PENALTY_DIVISOR = 300f;

		// Token: 0x04005CEC RID: 23788
		public const float SELF_DESTRUCT_REFUND_FACTOR = 0.5f;

		// Token: 0x04005CED RID: 23789
		public static float CARGO_CAPACITY_SCALE = 10f;

		// Token: 0x04005CEE RID: 23790
		public static float LIQUID_CARGO_BAY_CLUSTER_CAPACITY = 2700f;

		// Token: 0x04005CEF RID: 23791
		public static float SOLID_CARGO_BAY_CLUSTER_CAPACITY = 2700f;

		// Token: 0x04005CF0 RID: 23792
		public static float GAS_CARGO_BAY_CLUSTER_CAPACITY = 1100f;

		// Token: 0x04005CF1 RID: 23793
		public const float ENTITIES_CARGO_BAY_CLUSTER_CAPACITY = 100f;

		// Token: 0x04005CF2 RID: 23794
		public static Vector2I ROCKET_INTERIOR_SIZE = new Vector2I(32, 32);

		// Token: 0x0200217B RID: 8571
		public class DESTINATION_RESEARCH
		{
			// Token: 0x040099E5 RID: 39397
			public static int EVERGREEN = 10;

			// Token: 0x040099E6 RID: 39398
			public static int BASIC = 50;

			// Token: 0x040099E7 RID: 39399
			public static int HIGH = 150;
		}

		// Token: 0x0200217C RID: 8572
		public class DESTINATION_ANALYSIS
		{
			// Token: 0x040099E8 RID: 39400
			public static int DISCOVERED = 50;

			// Token: 0x040099E9 RID: 39401
			public static int COMPLETE = 100;

			// Token: 0x040099EA RID: 39402
			public static float DEFAULT_CYCLES_PER_DISCOVERY = 0.5f;
		}

		// Token: 0x0200217D RID: 8573
		public class DESTINATION_THRUST_COSTS
		{
			// Token: 0x040099EB RID: 39403
			public static int LOW = 3;

			// Token: 0x040099EC RID: 39404
			public static int MID = 5;

			// Token: 0x040099ED RID: 39405
			public static int HIGH = 7;

			// Token: 0x040099EE RID: 39406
			public static int VERY_HIGH = 9;
		}

		// Token: 0x0200217E RID: 8574
		public class CLUSTER_FOW
		{
			// Token: 0x040099EF RID: 39407
			public static float POINTS_TO_REVEAL = 100f;

			// Token: 0x040099F0 RID: 39408
			public static float DEFAULT_CYCLES_PER_REVEAL = 0.5f;
		}

		// Token: 0x0200217F RID: 8575
		public class ENGINE_EFFICIENCY
		{
			// Token: 0x040099F1 RID: 39409
			public static float WEAK = 20f;

			// Token: 0x040099F2 RID: 39410
			public static float MEDIUM = 40f;

			// Token: 0x040099F3 RID: 39411
			public static float STRONG = 60f;

			// Token: 0x040099F4 RID: 39412
			public static float BOOSTER = 30f;
		}

		// Token: 0x02002180 RID: 8576
		public class ROCKET_HEIGHT
		{
			// Token: 0x040099F5 RID: 39413
			public static int VERY_SHORT = 10;

			// Token: 0x040099F6 RID: 39414
			public static int SHORT = 16;

			// Token: 0x040099F7 RID: 39415
			public static int MEDIUM = 20;

			// Token: 0x040099F8 RID: 39416
			public static int TALL = 25;

			// Token: 0x040099F9 RID: 39417
			public static int VERY_TALL = 35;

			// Token: 0x040099FA RID: 39418
			public static int MAX_MODULE_STACK_HEIGHT = ROCKETRY.ROCKET_HEIGHT.VERY_TALL - 5;
		}

		// Token: 0x02002181 RID: 8577
		public class OXIDIZER_EFFICIENCY
		{
			// Token: 0x040099FB RID: 39419
			public static float VERY_LOW = 0.334f;

			// Token: 0x040099FC RID: 39420
			public static float LOW = 1f;

			// Token: 0x040099FD RID: 39421
			public static float HIGH = 1.33f;
		}

		// Token: 0x02002182 RID: 8578
		public class DLC1_OXIDIZER_EFFICIENCY
		{
			// Token: 0x040099FE RID: 39422
			public static float VERY_LOW = 1f;

			// Token: 0x040099FF RID: 39423
			public static float LOW = 2f;

			// Token: 0x04009A00 RID: 39424
			public static float HIGH = 4f;
		}

		// Token: 0x02002183 RID: 8579
		public class CARGO_CONTAINER_MASS
		{
			// Token: 0x04009A01 RID: 39425
			public static float STATIC_MASS = 1000f;

			// Token: 0x04009A02 RID: 39426
			public static float PAYLOAD_MASS = 1000f;
		}

		// Token: 0x02002184 RID: 8580
		public class BURDEN
		{
			// Token: 0x04009A03 RID: 39427
			public static int INSIGNIFICANT = 1;

			// Token: 0x04009A04 RID: 39428
			public static int MINOR = 2;

			// Token: 0x04009A05 RID: 39429
			public static int MINOR_PLUS = 3;

			// Token: 0x04009A06 RID: 39430
			public static int MODERATE = 4;

			// Token: 0x04009A07 RID: 39431
			public static int MODERATE_PLUS = 5;

			// Token: 0x04009A08 RID: 39432
			public static int MAJOR = 6;

			// Token: 0x04009A09 RID: 39433
			public static int MAJOR_PLUS = 7;

			// Token: 0x04009A0A RID: 39434
			public static int MEGA = 9;

			// Token: 0x04009A0B RID: 39435
			public static int MONUMENTAL = 15;
		}

		// Token: 0x02002185 RID: 8581
		public class ENGINE_POWER
		{
			// Token: 0x04009A0C RID: 39436
			public static int EARLY_WEAK = 16;

			// Token: 0x04009A0D RID: 39437
			public static int EARLY_STRONG = 23;

			// Token: 0x04009A0E RID: 39438
			public static int MID_VERY_STRONG = 48;

			// Token: 0x04009A0F RID: 39439
			public static int MID_STRONG = 31;

			// Token: 0x04009A10 RID: 39440
			public static int MID_WEAK = 27;

			// Token: 0x04009A11 RID: 39441
			public static int LATE_STRONG = 34;

			// Token: 0x04009A12 RID: 39442
			public static int LATE_VERY_STRONG = 55;
		}

		// Token: 0x02002186 RID: 8582
		public class FUEL_COST_PER_DISTANCE
		{
			// Token: 0x04009A13 RID: 39443
			public static float VERY_LOW = 0.033333335f;

			// Token: 0x04009A14 RID: 39444
			public static float LOW = 0.0375f;

			// Token: 0x04009A15 RID: 39445
			public static float MEDIUM = 0.075f;

			// Token: 0x04009A16 RID: 39446
			public static float HIGH = 0.09375f;

			// Token: 0x04009A17 RID: 39447
			public static float VERY_HIGH = 0.15f;

			// Token: 0x04009A18 RID: 39448
			public static float GAS_VERY_LOW = 0.025f;

			// Token: 0x04009A19 RID: 39449
			public static float GAS_LOW = 0.027777778f;

			// Token: 0x04009A1A RID: 39450
			public static float GAS_HIGH = 0.041666668f;

			// Token: 0x04009A1B RID: 39451
			public static float PARTICLES = 0.33333334f;
		}
	}
}
