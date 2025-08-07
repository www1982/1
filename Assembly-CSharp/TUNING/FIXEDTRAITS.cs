using System;

namespace TUNING
{
	// Token: 0x02000F80 RID: 3968
	public class FIXEDTRAITS
	{
		// Token: 0x02002133 RID: 8499
		public class NORTHERNLIGHTS
		{
			// Token: 0x040097DE RID: 38878
			public static int NONE = 0;

			// Token: 0x040097DF RID: 38879
			public static int ENABLED = 1;

			// Token: 0x040097E0 RID: 38880
			public static int DEFAULT_VALUE = FIXEDTRAITS.NORTHERNLIGHTS.NONE;

			// Token: 0x02002919 RID: 10521
			public class NAME
			{
				// Token: 0x0400B5D3 RID: 46547
				public static string NONE = "northernLightsNone";

				// Token: 0x0400B5D4 RID: 46548
				public static string ENABLED = "northernLightsOn";

				// Token: 0x0400B5D5 RID: 46549
				public static string DEFAULT = FIXEDTRAITS.NORTHERNLIGHTS.NAME.NONE;
			}
		}

		// Token: 0x02002134 RID: 8500
		public class LARGEIMPACTORFRAGMENTS
		{
			// Token: 0x040097E1 RID: 38881
			public static int NONE = 0;

			// Token: 0x040097E2 RID: 38882
			public static int ALLOWED = 1;

			// Token: 0x040097E3 RID: 38883
			public static int DEFAULT_VALUE = FIXEDTRAITS.LARGEIMPACTORFRAGMENTS.NONE;

			// Token: 0x0200291A RID: 10522
			public class NAME
			{
				// Token: 0x0400B5D6 RID: 46550
				public static string NONE = "largeImpactorFragmentsNone";

				// Token: 0x0400B5D7 RID: 46551
				public static string ALLOWED = "largeImpactorFragmentsAllowed";

				// Token: 0x0400B5D8 RID: 46552
				public static string DEFAULT = FIXEDTRAITS.LARGEIMPACTORFRAGMENTS.NAME.NONE;
			}
		}

		// Token: 0x02002135 RID: 8501
		public class SUNLIGHT
		{
			// Token: 0x040097E4 RID: 38884
			public static int DEFAULT_SPACED_OUT_SUNLIGHT = 40000;

			// Token: 0x040097E5 RID: 38885
			public static int NONE = 0;

			// Token: 0x040097E6 RID: 38886
			public static int VERY_VERY_LOW = (int)((float)FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 0.25f);

			// Token: 0x040097E7 RID: 38887
			public static int VERY_LOW = (int)((float)FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 0.5f);

			// Token: 0x040097E8 RID: 38888
			public static int LOW = (int)((float)FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 0.75f);

			// Token: 0x040097E9 RID: 38889
			public static int MED_LOW = (int)((float)FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 0.875f);

			// Token: 0x040097EA RID: 38890
			public static int MED = FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT;

			// Token: 0x040097EB RID: 38891
			public static int MED_HIGH = (int)((float)FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 1.25f);

			// Token: 0x040097EC RID: 38892
			public static int HIGH = (int)((float)FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 1.5f);

			// Token: 0x040097ED RID: 38893
			public static int VERY_HIGH = FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 2;

			// Token: 0x040097EE RID: 38894
			public static int VERY_VERY_HIGH = (int)((float)FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 2.5f);

			// Token: 0x040097EF RID: 38895
			public static int VERY_VERY_VERY_HIGH = FIXEDTRAITS.SUNLIGHT.DEFAULT_SPACED_OUT_SUNLIGHT * 3;

			// Token: 0x040097F0 RID: 38896
			public static int DEFAULT_VALUE = FIXEDTRAITS.SUNLIGHT.VERY_HIGH;

			// Token: 0x0200291B RID: 10523
			public class NAME
			{
				// Token: 0x0400B5D9 RID: 46553
				public static string NONE = "sunlightNone";

				// Token: 0x0400B5DA RID: 46554
				public static string VERY_VERY_LOW = "sunlightVeryVeryLow";

				// Token: 0x0400B5DB RID: 46555
				public static string VERY_LOW = "sunlightVeryLow";

				// Token: 0x0400B5DC RID: 46556
				public static string LOW = "sunlightLow";

				// Token: 0x0400B5DD RID: 46557
				public static string MED_LOW = "sunlightMedLow";

				// Token: 0x0400B5DE RID: 46558
				public static string MED = "sunlightMed";

				// Token: 0x0400B5DF RID: 46559
				public static string MED_HIGH = "sunlightMedHigh";

				// Token: 0x0400B5E0 RID: 46560
				public static string HIGH = "sunlightHigh";

				// Token: 0x0400B5E1 RID: 46561
				public static string VERY_HIGH = "sunlightVeryHigh";

				// Token: 0x0400B5E2 RID: 46562
				public static string VERY_VERY_HIGH = "sunlightVeryVeryHigh";

				// Token: 0x0400B5E3 RID: 46563
				public static string VERY_VERY_VERY_HIGH = "sunlightVeryVeryVeryHigh";

				// Token: 0x0400B5E4 RID: 46564
				public static string DEFAULT = FIXEDTRAITS.SUNLIGHT.NAME.VERY_HIGH;
			}
		}

		// Token: 0x02002136 RID: 8502
		public class COSMICRADIATION
		{
			// Token: 0x040097F1 RID: 38897
			public static int BASELINE = 250;

			// Token: 0x040097F2 RID: 38898
			public static int NONE = 0;

			// Token: 0x040097F3 RID: 38899
			public static int VERY_VERY_LOW = (int)((float)FIXEDTRAITS.COSMICRADIATION.BASELINE * 0.25f);

			// Token: 0x040097F4 RID: 38900
			public static int VERY_LOW = (int)((float)FIXEDTRAITS.COSMICRADIATION.BASELINE * 0.5f);

			// Token: 0x040097F5 RID: 38901
			public static int LOW = (int)((float)FIXEDTRAITS.COSMICRADIATION.BASELINE * 0.75f);

			// Token: 0x040097F6 RID: 38902
			public static int MED_LOW = (int)((float)FIXEDTRAITS.COSMICRADIATION.BASELINE * 0.875f);

			// Token: 0x040097F7 RID: 38903
			public static int MED = FIXEDTRAITS.COSMICRADIATION.BASELINE;

			// Token: 0x040097F8 RID: 38904
			public static int MED_HIGH = (int)((float)FIXEDTRAITS.COSMICRADIATION.BASELINE * 1.25f);

			// Token: 0x040097F9 RID: 38905
			public static int HIGH = (int)((float)FIXEDTRAITS.COSMICRADIATION.BASELINE * 1.5f);

			// Token: 0x040097FA RID: 38906
			public static int VERY_HIGH = FIXEDTRAITS.COSMICRADIATION.BASELINE * 2;

			// Token: 0x040097FB RID: 38907
			public static int VERY_VERY_HIGH = FIXEDTRAITS.COSMICRADIATION.BASELINE * 3;

			// Token: 0x040097FC RID: 38908
			public static int DEFAULT_VALUE = FIXEDTRAITS.COSMICRADIATION.MED;

			// Token: 0x040097FD RID: 38909
			public static float TELESCOPE_RADIATION_SHIELDING = 0.5f;

			// Token: 0x0200291C RID: 10524
			public class NAME
			{
				// Token: 0x0400B5E5 RID: 46565
				public static string NONE = "cosmicRadiationNone";

				// Token: 0x0400B5E6 RID: 46566
				public static string VERY_VERY_LOW = "cosmicRadiationVeryVeryLow";

				// Token: 0x0400B5E7 RID: 46567
				public static string VERY_LOW = "cosmicRadiationVeryLow";

				// Token: 0x0400B5E8 RID: 46568
				public static string LOW = "cosmicRadiationLow";

				// Token: 0x0400B5E9 RID: 46569
				public static string MED_LOW = "cosmicRadiationMedLow";

				// Token: 0x0400B5EA RID: 46570
				public static string MED = "cosmicRadiationMed";

				// Token: 0x0400B5EB RID: 46571
				public static string MED_HIGH = "cosmicRadiationMedHigh";

				// Token: 0x0400B5EC RID: 46572
				public static string HIGH = "cosmicRadiationHigh";

				// Token: 0x0400B5ED RID: 46573
				public static string VERY_HIGH = "cosmicRadiationVeryHigh";

				// Token: 0x0400B5EE RID: 46574
				public static string VERY_VERY_HIGH = "cosmicRadiationVeryVeryHigh";

				// Token: 0x0400B5EF RID: 46575
				public static string DEFAULT = FIXEDTRAITS.COSMICRADIATION.NAME.MED;
			}
		}
	}
}
