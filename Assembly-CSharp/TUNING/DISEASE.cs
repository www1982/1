using System;

namespace TUNING
{
	// Token: 0x02000F8A RID: 3978
	public class DISEASE
	{
		// Token: 0x04005C03 RID: 23555
		public const int COUNT_SCALER = 1000;

		// Token: 0x04005C04 RID: 23556
		public const int GENERIC_EMIT_COUNT = 100000;

		// Token: 0x04005C05 RID: 23557
		public const float GENERIC_EMIT_INTERVAL = 5f;

		// Token: 0x04005C06 RID: 23558
		public const float GENERIC_INFECTION_RADIUS = 1.5f;

		// Token: 0x04005C07 RID: 23559
		public const float GENERIC_INFECTION_INTERVAL = 5f;

		// Token: 0x04005C08 RID: 23560
		public const float STINKY_EMIT_MASS = 0.0025000002f;

		// Token: 0x04005C09 RID: 23561
		public const float STINKY_EMIT_INTERVAL = 2.5f;

		// Token: 0x04005C0A RID: 23562
		public const float STORAGE_TRANSFER_RATE = 0.05f;

		// Token: 0x04005C0B RID: 23563
		public const float WORKABLE_TRANSFER_RATE = 0.33f;

		// Token: 0x04005C0C RID: 23564
		public const float LADDER_TRANSFER_RATE = 0.005f;

		// Token: 0x04005C0D RID: 23565
		public const float INTERNAL_GERM_DEATH_MULTIPLIER = -0.00066666666f;

		// Token: 0x04005C0E RID: 23566
		public const float INTERNAL_GERM_DEATH_ADDEND = -0.8333333f;

		// Token: 0x04005C0F RID: 23567
		public const float MINIMUM_IMMUNE_DAMAGE = 0.00016666666f;

		// Token: 0x0200215E RID: 8542
		public class DURATION
		{
			// Token: 0x040098D8 RID: 39128
			public const float LONG = 10800f;

			// Token: 0x040098D9 RID: 39129
			public const float LONGISH = 4620f;

			// Token: 0x040098DA RID: 39130
			public const float NORMAL = 2220f;

			// Token: 0x040098DB RID: 39131
			public const float SHORT = 1020f;

			// Token: 0x040098DC RID: 39132
			public const float TEMPORARY = 180f;

			// Token: 0x040098DD RID: 39133
			public const float VERY_BRIEF = 60f;
		}

		// Token: 0x0200215F RID: 8543
		public class IMMUNE_ATTACK_STRENGTH_PERCENT
		{
			// Token: 0x040098DE RID: 39134
			public const float SLOW_3 = 0.00025f;

			// Token: 0x040098DF RID: 39135
			public const float SLOW_2 = 0.0005f;

			// Token: 0x040098E0 RID: 39136
			public const float SLOW_1 = 0.00125f;

			// Token: 0x040098E1 RID: 39137
			public const float NORMAL = 0.005f;

			// Token: 0x040098E2 RID: 39138
			public const float FAST_1 = 0.0125f;

			// Token: 0x040098E3 RID: 39139
			public const float FAST_2 = 0.05f;

			// Token: 0x040098E4 RID: 39140
			public const float FAST_3 = 0.125f;
		}

		// Token: 0x02002160 RID: 8544
		public class RADIATION_KILL_RATE
		{
			// Token: 0x040098E5 RID: 39141
			public const float NO_EFFECT = 0f;

			// Token: 0x040098E6 RID: 39142
			public const float SLOW = 1f;

			// Token: 0x040098E7 RID: 39143
			public const float NORMAL = 2.5f;

			// Token: 0x040098E8 RID: 39144
			public const float FAST = 5f;
		}

		// Token: 0x02002161 RID: 8545
		public static class GROWTH_FACTOR
		{
			// Token: 0x040098E9 RID: 39145
			public const float NONE = float.PositiveInfinity;

			// Token: 0x040098EA RID: 39146
			public const float DEATH_1 = 12000f;

			// Token: 0x040098EB RID: 39147
			public const float DEATH_2 = 6000f;

			// Token: 0x040098EC RID: 39148
			public const float DEATH_3 = 3000f;

			// Token: 0x040098ED RID: 39149
			public const float DEATH_4 = 1200f;

			// Token: 0x040098EE RID: 39150
			public const float DEATH_5 = 300f;

			// Token: 0x040098EF RID: 39151
			public const float DEATH_MAX = 10f;

			// Token: 0x040098F0 RID: 39152
			public const float DEATH_INSTANT = 0f;

			// Token: 0x040098F1 RID: 39153
			public const float GROWTH_1 = -12000f;

			// Token: 0x040098F2 RID: 39154
			public const float GROWTH_2 = -6000f;

			// Token: 0x040098F3 RID: 39155
			public const float GROWTH_3 = -3000f;

			// Token: 0x040098F4 RID: 39156
			public const float GROWTH_4 = -1200f;

			// Token: 0x040098F5 RID: 39157
			public const float GROWTH_5 = -600f;

			// Token: 0x040098F6 RID: 39158
			public const float GROWTH_6 = -300f;

			// Token: 0x040098F7 RID: 39159
			public const float GROWTH_7 = -150f;
		}

		// Token: 0x02002162 RID: 8546
		public static class UNDERPOPULATION_DEATH_RATE
		{
			// Token: 0x040098F8 RID: 39160
			public const float NONE = 0f;

			// Token: 0x040098F9 RID: 39161
			private const float BASE_NUM_TO_KILL = 400f;

			// Token: 0x040098FA RID: 39162
			public const float SLOW = 0.6666667f;

			// Token: 0x040098FB RID: 39163
			public const float FAST = 2.6666667f;
		}
	}
}
