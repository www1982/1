using System;

namespace STRINGS
{
	// Token: 0x02000FB6 RID: 4022
	public static class WORLD_TRAITS
	{
		// Token: 0x04005DC3 RID: 24003
		public static LocString MISSING_TRAIT = "<missing traits>";

		// Token: 0x02002595 RID: 9621
		public static class NO_TRAITS
		{
			// Token: 0x0400A7A0 RID: 42912
			public static LocString NAME = "<i>This world is stable and has no unusual features.</i>";

			// Token: 0x0400A7A1 RID: 42913
			public static LocString NAME_SHORTHAND = "No unusual features";

			// Token: 0x0400A7A2 RID: 42914
			public static LocString DESCRIPTION = "This world exists in a particularly stable configuration each time it is encountered";
		}

		// Token: 0x02002596 RID: 9622
		public static class BOULDERS_LARGE
		{
			// Token: 0x0400A7A3 RID: 42915
			public static LocString NAME = "Large Boulders";

			// Token: 0x0400A7A4 RID: 42916
			public static LocString DESCRIPTION = "Huge boulders make digging through this world more difficult";
		}

		// Token: 0x02002597 RID: 9623
		public static class BOULDERS_MEDIUM
		{
			// Token: 0x0400A7A5 RID: 42917
			public static LocString NAME = "Medium Boulders";

			// Token: 0x0400A7A6 RID: 42918
			public static LocString DESCRIPTION = "Mid-sized boulders make digging through this world more difficult";
		}

		// Token: 0x02002598 RID: 9624
		public static class BOULDERS_MIXED
		{
			// Token: 0x0400A7A7 RID: 42919
			public static LocString NAME = "Mixed Boulders";

			// Token: 0x0400A7A8 RID: 42920
			public static LocString DESCRIPTION = "Boulders of various sizes make digging through this world more difficult";
		}

		// Token: 0x02002599 RID: 9625
		public static class BOULDERS_SMALL
		{
			// Token: 0x0400A7A9 RID: 42921
			public static LocString NAME = "Small Boulders";

			// Token: 0x0400A7AA RID: 42922
			public static LocString DESCRIPTION = "Tiny boulders make digging through this world more difficult";
		}

		// Token: 0x0200259A RID: 9626
		public static class DEEP_OIL
		{
			// Token: 0x0400A7AB RID: 42923
			public static LocString NAME = "Trapped Oil";

			// Token: 0x0400A7AC RID: 42924
			public static LocString DESCRIPTION = string.Concat(new string[]
			{
				"Most of the ",
				UI.PRE_KEYWORD,
				"Oil",
				UI.PST_KEYWORD,
				" in this world will need to be extracted with ",
				BUILDINGS.PREFABS.OILWELLCAP.NAME,
				"s"
			});
		}

		// Token: 0x0200259B RID: 9627
		public static class FROZEN_CORE
		{
			// Token: 0x0400A7AD RID: 42925
			public static LocString NAME = "Frozen Core";

			// Token: 0x0400A7AE RID: 42926
			public static LocString DESCRIPTION = "This world has a chilly core of solid " + ELEMENTS.ICE.NAME;
		}

		// Token: 0x0200259C RID: 9628
		public static class GEOACTIVE
		{
			// Token: 0x0400A7AF RID: 42927
			public static LocString NAME = "Geoactive";

			// Token: 0x0400A7B0 RID: 42928
			public static LocString DESCRIPTION = string.Concat(new string[]
			{
				"This world has more ",
				UI.PRE_KEYWORD,
				"Geysers",
				UI.PST_KEYWORD,
				" and ",
				UI.PRE_KEYWORD,
				"Vents",
				UI.PST_KEYWORD,
				" than usual"
			});
		}

		// Token: 0x0200259D RID: 9629
		public static class GEODES
		{
			// Token: 0x0400A7B1 RID: 42929
			public static LocString NAME = "Geodes";

			// Token: 0x0400A7B2 RID: 42930
			public static LocString DESCRIPTION = "Large geodes containing rare material caches are deposited across this world";
		}

		// Token: 0x0200259E RID: 9630
		public static class GEODORMANT
		{
			// Token: 0x0400A7B3 RID: 42931
			public static LocString NAME = "Geodormant";

			// Token: 0x0400A7B4 RID: 42932
			public static LocString DESCRIPTION = string.Concat(new string[]
			{
				"This world has fewer ",
				UI.PRE_KEYWORD,
				"Geysers",
				UI.PST_KEYWORD,
				" and ",
				UI.PRE_KEYWORD,
				"Vents",
				UI.PST_KEYWORD,
				" than usual"
			});
		}

		// Token: 0x0200259F RID: 9631
		public static class GLACIERS_LARGE
		{
			// Token: 0x0400A7B5 RID: 42933
			public static LocString NAME = "Large Glaciers";

			// Token: 0x0400A7B6 RID: 42934
			public static LocString DESCRIPTION = "Huge chunks of primordial " + ELEMENTS.ICE.NAME + " are scattered across this world";
		}

		// Token: 0x020025A0 RID: 9632
		public static class IRREGULAR_OIL
		{
			// Token: 0x0400A7B7 RID: 42935
			public static LocString NAME = "Irregular Oil";

			// Token: 0x0400A7B8 RID: 42936
			public static LocString DESCRIPTION = string.Concat(new string[]
			{
				"The ",
				UI.PRE_KEYWORD,
				"Oil",
				UI.PST_KEYWORD,
				" on this asteroid is anything but regular!"
			});
		}

		// Token: 0x020025A1 RID: 9633
		public static class MAGMA_VENTS
		{
			// Token: 0x0400A7B9 RID: 42937
			public static LocString NAME = "Magma Channels";

			// Token: 0x0400A7BA RID: 42938
			public static LocString DESCRIPTION = "The " + ELEMENTS.MAGMA.NAME + " from this world's core has leaked into the mantle and crust";
		}

		// Token: 0x020025A2 RID: 9634
		public static class METAL_POOR
		{
			// Token: 0x0400A7BB RID: 42939
			public static LocString NAME = "Metal Poor";

			// Token: 0x0400A7BC RID: 42940
			public static LocString DESCRIPTION = string.Concat(new string[]
			{
				"There is a reduced amount of ",
				UI.PRE_KEYWORD,
				"Metal Ore",
				UI.PST_KEYWORD,
				" on this world, proceed with caution!"
			});
		}

		// Token: 0x020025A3 RID: 9635
		public static class METAL_RICH
		{
			// Token: 0x0400A7BD RID: 42941
			public static LocString NAME = "Metal Rich";

			// Token: 0x0400A7BE RID: 42942
			public static LocString DESCRIPTION = "This asteroid is an abundant source of " + UI.PRE_KEYWORD + "Metal Ore" + UI.PST_KEYWORD;
		}

		// Token: 0x020025A4 RID: 9636
		public static class MISALIGNED_START
		{
			// Token: 0x0400A7BF RID: 42943
			public static LocString NAME = "Alternate Pod Location";

			// Token: 0x0400A7C0 RID: 42944
			public static LocString DESCRIPTION = "The " + BUILDINGS.PREFABS.HEADQUARTERSCOMPLETE.NAME + " didn't end up in the asteroid's exact center this time... but it's still nowhere near the surface";
		}

		// Token: 0x020025A5 RID: 9637
		public static class SLIME_SPLATS
		{
			// Token: 0x0400A7C1 RID: 42945
			public static LocString NAME = "Slime Molds";

			// Token: 0x0400A7C2 RID: 42946
			public static LocString DESCRIPTION = "Sickly " + ELEMENTS.SLIMEMOLD.NAME + " growths have crept all over this world";
		}

		// Token: 0x020025A6 RID: 9638
		public static class SUBSURFACE_OCEAN
		{
			// Token: 0x0400A7C3 RID: 42947
			public static LocString NAME = "Subsurface Ocean";

			// Token: 0x0400A7C4 RID: 42948
			public static LocString DESCRIPTION = "Below the crust of this world is a " + ELEMENTS.SALTWATER.NAME + " sea";
		}

		// Token: 0x020025A7 RID: 9639
		public static class VOLCANOES
		{
			// Token: 0x0400A7C5 RID: 42949
			public static LocString NAME = "Volcanic Activity";

			// Token: 0x0400A7C6 RID: 42950
			public static LocString DESCRIPTION = string.Concat(new string[]
			{
				"Several active ",
				UI.PRE_KEYWORD,
				"Volcanoes",
				UI.PST_KEYWORD,
				" have been detected in this world"
			});
		}

		// Token: 0x020025A8 RID: 9640
		public static class RADIOACTIVE_CRUST
		{
			// Token: 0x0400A7C7 RID: 42951
			public static LocString NAME = "Radioactive Crust";

			// Token: 0x0400A7C8 RID: 42952
			public static LocString DESCRIPTION = "Deposits of " + ELEMENTS.URANIUMORE.NAME + " are found in this world's crust";
		}

		// Token: 0x020025A9 RID: 9641
		public static class LUSH_CORE
		{
			// Token: 0x0400A7C9 RID: 42953
			public static LocString NAME = "Lush Core";

			// Token: 0x0400A7CA RID: 42954
			public static LocString DESCRIPTION = "This world has a lush forest core";
		}

		// Token: 0x020025AA RID: 9642
		public static class METAL_CAVES
		{
			// Token: 0x0400A7CB RID: 42955
			public static LocString NAME = "Metallic Caves";

			// Token: 0x0400A7CC RID: 42956
			public static LocString DESCRIPTION = "This world has caves of metal ore";
		}

		// Token: 0x020025AB RID: 9643
		public static class DISTRESS_SIGNAL
		{
			// Token: 0x0400A7CD RID: 42957
			public static LocString NAME = "Frozen Friend";

			// Token: 0x0400A7CE RID: 42958
			public static LocString DESCRIPTION = "This world contains a frozen friend from a long time ago";
		}

		// Token: 0x020025AC RID: 9644
		public static class CRASHED_SATELLITES
		{
			// Token: 0x0400A7CF RID: 42959
			public static LocString NAME = "Crashed Satellites";

			// Token: 0x0400A7D0 RID: 42960
			public static LocString DESCRIPTION = "This world contains crashed radioactive satellites";
		}
	}
}
