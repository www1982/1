using System;

namespace TUNING
{
	// Token: 0x02000F86 RID: 3974
	public class NOISE_POLLUTION
	{
		// Token: 0x04005BB9 RID: 23481
		public static readonly EffectorValues NONE = new EffectorValues
		{
			amount = 0,
			radius = 0
		};

		// Token: 0x04005BBA RID: 23482
		public static readonly EffectorValues CONE_OF_SILENCE = new EffectorValues
		{
			amount = -120,
			radius = 5
		};

		// Token: 0x04005BBB RID: 23483
		public static float DUPLICANT_TIME_THRESHOLD = 3f;

		// Token: 0x02002143 RID: 8515
		public class LENGTHS
		{
			// Token: 0x04009835 RID: 38965
			public static float VERYSHORT = 0.25f;

			// Token: 0x04009836 RID: 38966
			public static float SHORT = 0.5f;

			// Token: 0x04009837 RID: 38967
			public static float NORMAL = 1f;

			// Token: 0x04009838 RID: 38968
			public static float LONG = 1.5f;

			// Token: 0x04009839 RID: 38969
			public static float VERYLONG = 2f;
		}

		// Token: 0x02002144 RID: 8516
		public class NOISY
		{
			// Token: 0x0400983A RID: 38970
			public static readonly EffectorValues TIER0 = new EffectorValues
			{
				amount = 45,
				radius = 10
			};

			// Token: 0x0400983B RID: 38971
			public static readonly EffectorValues TIER1 = new EffectorValues
			{
				amount = 55,
				radius = 10
			};

			// Token: 0x0400983C RID: 38972
			public static readonly EffectorValues TIER2 = new EffectorValues
			{
				amount = 65,
				radius = 10
			};

			// Token: 0x0400983D RID: 38973
			public static readonly EffectorValues TIER3 = new EffectorValues
			{
				amount = 75,
				radius = 15
			};

			// Token: 0x0400983E RID: 38974
			public static readonly EffectorValues TIER4 = new EffectorValues
			{
				amount = 90,
				radius = 15
			};

			// Token: 0x0400983F RID: 38975
			public static readonly EffectorValues TIER5 = new EffectorValues
			{
				amount = 105,
				radius = 20
			};

			// Token: 0x04009840 RID: 38976
			public static readonly EffectorValues TIER6 = new EffectorValues
			{
				amount = 125,
				radius = 20
			};
		}

		// Token: 0x02002145 RID: 8517
		public class CREATURES
		{
			// Token: 0x04009841 RID: 38977
			public static readonly EffectorValues TIER0 = new EffectorValues
			{
				amount = 30,
				radius = 5
			};

			// Token: 0x04009842 RID: 38978
			public static readonly EffectorValues TIER1 = new EffectorValues
			{
				amount = 35,
				radius = 5
			};

			// Token: 0x04009843 RID: 38979
			public static readonly EffectorValues TIER2 = new EffectorValues
			{
				amount = 45,
				radius = 5
			};

			// Token: 0x04009844 RID: 38980
			public static readonly EffectorValues TIER3 = new EffectorValues
			{
				amount = 55,
				radius = 5
			};

			// Token: 0x04009845 RID: 38981
			public static readonly EffectorValues TIER4 = new EffectorValues
			{
				amount = 65,
				radius = 5
			};

			// Token: 0x04009846 RID: 38982
			public static readonly EffectorValues TIER5 = new EffectorValues
			{
				amount = 75,
				radius = 5
			};

			// Token: 0x04009847 RID: 38983
			public static readonly EffectorValues TIER6 = new EffectorValues
			{
				amount = 90,
				radius = 10
			};

			// Token: 0x04009848 RID: 38984
			public static readonly EffectorValues TIER7 = new EffectorValues
			{
				amount = 105,
				radius = 10
			};
		}

		// Token: 0x02002146 RID: 8518
		public class DAMPEN
		{
			// Token: 0x04009849 RID: 38985
			public static readonly EffectorValues TIER0 = new EffectorValues
			{
				amount = -5,
				radius = 1
			};

			// Token: 0x0400984A RID: 38986
			public static readonly EffectorValues TIER1 = new EffectorValues
			{
				amount = -10,
				radius = 2
			};

			// Token: 0x0400984B RID: 38987
			public static readonly EffectorValues TIER2 = new EffectorValues
			{
				amount = -15,
				radius = 3
			};

			// Token: 0x0400984C RID: 38988
			public static readonly EffectorValues TIER3 = new EffectorValues
			{
				amount = -20,
				radius = 4
			};

			// Token: 0x0400984D RID: 38989
			public static readonly EffectorValues TIER4 = new EffectorValues
			{
				amount = -20,
				radius = 5
			};

			// Token: 0x0400984E RID: 38990
			public static readonly EffectorValues TIER5 = new EffectorValues
			{
				amount = -25,
				radius = 6
			};
		}
	}
}
