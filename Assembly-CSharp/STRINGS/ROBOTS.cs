using System;

namespace STRINGS
{
	// Token: 0x02000FAE RID: 4014
	public class ROBOTS
	{
		// Token: 0x04005DB3 RID: 23987
		public static LocString CATEGORY_NAME = "Robots";

		// Token: 0x02002439 RID: 9273
		public class STATS
		{
			// Token: 0x0200377D RID: 14205
			public class INTERNALBATTERY
			{
				// Token: 0x0400E116 RID: 57622
				public static LocString NAME = "Rechargeable Battery";

				// Token: 0x0400E117 RID: 57623
				public static LocString TOOLTIP = "When this bot's battery runs out it must temporarily stop working to go recharge";
			}

			// Token: 0x0200377E RID: 14206
			public class INTERNALCHEMICALBATTERY
			{
				// Token: 0x0400E118 RID: 57624
				public static LocString NAME = "Chemical Battery";

				// Token: 0x0400E119 RID: 57625
				public static LocString TOOLTIP = "This bot will shut down permanently when its battery runs out";
			}

			// Token: 0x0200377F RID: 14207
			public class INTERNALBIOBATTERY
			{
				// Token: 0x0400E11A RID: 57626
				public static LocString NAME = "Biofuel";

				// Token: 0x0400E11B RID: 57627
				public static LocString TOOLTIP = "This bot will shut down permanently when its biofuel runs out";
			}

			// Token: 0x02003780 RID: 14208
			public class INTERNALELECTROBANK
			{
				// Token: 0x0400E11C RID: 57628
				public static LocString NAME = "Power Bank";

				// Token: 0x0400E11D RID: 57629
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"When this bot's ",
					UI.PRE_KEYWORD,
					"Power Bank",
					UI.PST_KEYWORD,
					" runs out, it will stop working until a fully charged one is delivered"
				});
			}
		}

		// Token: 0x0200243A RID: 9274
		public class ATTRIBUTES
		{
			// Token: 0x02003781 RID: 14209
			public class INTERNALBATTERYDELTA
			{
				// Token: 0x0400E11E RID: 57630
				public static LocString NAME = "Rechargeable Battery Drain";

				// Token: 0x0400E11F RID: 57631
				public static LocString TOOLTIP = "The rate at which battery life is depleted";
			}
		}

		// Token: 0x0200243B RID: 9275
		public class STATUSITEMS
		{
			// Token: 0x02003782 RID: 14210
			public class CANTREACHSTATION
			{
				// Token: 0x0400E120 RID: 57632
				public static LocString NAME = "Unreachable Dock";

				// Token: 0x0400E121 RID: 57633
				public static LocString DESC = "Obstacles are preventing {0} from heading home";

				// Token: 0x0400E122 RID: 57634
				public static LocString TOOLTIP = "Obstacles are preventing {0} from heading home";
			}

			// Token: 0x02003783 RID: 14211
			public class MOVINGTOCHARGESTATION
			{
				// Token: 0x0400E123 RID: 57635
				public static LocString NAME = "Traveling to Dock";

				// Token: 0x0400E124 RID: 57636
				public static LocString DESC = "{0} is on its way home to recharge";

				// Token: 0x0400E125 RID: 57637
				public static LocString TOOLTIP = "{0} is on its way home to recharge";
			}

			// Token: 0x02003784 RID: 14212
			public class LOWBATTERY
			{
				// Token: 0x0400E126 RID: 57638
				public static LocString NAME = "Low Battery";

				// Token: 0x0400E127 RID: 57639
				public static LocString DESC = "{0}'s battery is low and needs to recharge";

				// Token: 0x0400E128 RID: 57640
				public static LocString TOOLTIP = "{0}'s battery is low and needs to recharge";
			}

			// Token: 0x02003785 RID: 14213
			public class LOWBATTERYNOCHARGE
			{
				// Token: 0x0400E129 RID: 57641
				public static LocString NAME = "Low Battery";

				// Token: 0x0400E12A RID: 57642
				public static LocString DESC = "{0}'s battery is low\n\nThe internal battery cannot be recharged and this robot will cease functioning after it is depleted.";

				// Token: 0x0400E12B RID: 57643
				public static LocString TOOLTIP = "{0}'s battery is low\n\nThe internal battery cannot be recharged and this robot will cease functioning after it is depleted.";
			}

			// Token: 0x02003786 RID: 14214
			public class DEADBATTERY
			{
				// Token: 0x0400E12C RID: 57644
				public static LocString NAME = "Shut Down";

				// Token: 0x0400E12D RID: 57645
				public static LocString DESC = "RIP {0}\n\n{0}'s battery has been depleted and cannot be recharged";

				// Token: 0x0400E12E RID: 57646
				public static LocString TOOLTIP = "RIP {0}\n\n{0}'s battery has been depleted and cannot be recharged";
			}

			// Token: 0x02003787 RID: 14215
			public class DEADBATTERYFLYDO
			{
				// Token: 0x0400E12F RID: 57647
				public static LocString NAME = "Shut Down";

				// Token: 0x0400E130 RID: 57648
				public static LocString DESC = "{0}'s battery has been depleted\n\n{0} will resume function when a new battery has been delivered";

				// Token: 0x0400E131 RID: 57649
				public static LocString TOOLTIP = "{0}'s battery has been depleted\n\n{0} will resume function when a new battery has been delivered";
			}

			// Token: 0x02003788 RID: 14216
			public class DUSTBINFULL
			{
				// Token: 0x0400E132 RID: 57650
				public static LocString NAME = "Dust Bin Full";

				// Token: 0x0400E133 RID: 57651
				public static LocString DESC = "{0} must return to its dock to unload";

				// Token: 0x0400E134 RID: 57652
				public static LocString TOOLTIP = "{0} must return to its dock to unload";
			}

			// Token: 0x02003789 RID: 14217
			public class WORKING
			{
				// Token: 0x0400E135 RID: 57653
				public static LocString NAME = "Working";

				// Token: 0x0400E136 RID: 57654
				public static LocString DESC = "{0} is working diligently. Great job, {0}!";

				// Token: 0x0400E137 RID: 57655
				public static LocString TOOLTIP = "{0} is working diligently. Great job, {0}!";
			}

			// Token: 0x0200378A RID: 14218
			public class UNLOADINGSTORAGE
			{
				// Token: 0x0400E138 RID: 57656
				public static LocString NAME = "Unloading";

				// Token: 0x0400E139 RID: 57657
				public static LocString DESC = "{0} is emptying out its dust bin";

				// Token: 0x0400E13A RID: 57658
				public static LocString TOOLTIP = "{0} is emptying out its dust bin";
			}

			// Token: 0x0200378B RID: 14219
			public class CHARGING
			{
				// Token: 0x0400E13B RID: 57659
				public static LocString NAME = "Charging";

				// Token: 0x0400E13C RID: 57660
				public static LocString DESC = "{0} is recharging its battery";

				// Token: 0x0400E13D RID: 57661
				public static LocString TOOLTIP = "{0} is recharging its battery";
			}

			// Token: 0x0200378C RID: 14220
			public class REACTPOSITIVE
			{
				// Token: 0x0400E13E RID: 57662
				public static LocString NAME = "Happy Reaction";

				// Token: 0x0400E13F RID: 57663
				public static LocString DESC = "This bot saw something nice!";

				// Token: 0x0400E140 RID: 57664
				public static LocString TOOLTIP = "This bot saw something nice!";
			}

			// Token: 0x0200378D RID: 14221
			public class REACTNEGATIVE
			{
				// Token: 0x0400E141 RID: 57665
				public static LocString NAME = "Bothered Reaction";

				// Token: 0x0400E142 RID: 57666
				public static LocString DESC = "This bot saw something upsetting";

				// Token: 0x0400E143 RID: 57667
				public static LocString TOOLTIP = "This bot saw something upsetting";
			}
		}

		// Token: 0x0200243C RID: 9276
		public class MODELS
		{
			// Token: 0x0200378E RID: 14222
			public class MORB
			{
				// Token: 0x0400E144 RID: 57668
				public static LocString NAME = UI.FormatAsLink("Biobot", "STORYTRAITMORBROVER");

				// Token: 0x0400E145 RID: 57669
				public static LocString DESC = "A Pathogen-Fueled Extravehicular Geo-Exploratory Guidebot (model Y), aka \"P.E.G.G.Y.\"\n\nIt can be assigned basic building tasks and digging duties in hazardous environments.";

				// Token: 0x0400E146 RID: 57670
				public static LocString CODEX_DESC = "The pathogen-fueled guidebot is designed to maximize a colony's chances of surviving in hostile environments by meeting three core outcomes:\n\n1. Filtration and removal of toxins from environment;\n2. Safe disposal of filtered toxins through conversion into usable biofuel;\n3. Creation of geo-exploration equipment for colony expansion with minimal colonist endangerment.\n\nThe elements aggregated during this process may result in the unintentional spread of contaminants. Specialized training required for safe handling.";
			}

			// Token: 0x0200378F RID: 14223
			public class SCOUT
			{
				// Token: 0x0400E147 RID: 57671
				public static LocString NAME = "Rover";

				// Token: 0x0400E148 RID: 57672
				public static LocString DESC = "A curious bot that can remotely explore new " + UI.CLUSTERMAP.PLANETOID_KEYWORD + " locations.";
			}

			// Token: 0x02003790 RID: 14224
			public class SWEEPBOT
			{
				// Token: 0x0400E149 RID: 57673
				public static LocString NAME = UI.FormatAsLink("Sweepy", "SWEEPY");

				// Token: 0x0400E14A RID: 57674
				public static LocString DESC = string.Concat(new string[]
				{
					"An automated sweeping robot.\n\nSweeps up ",
					UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
					" debris and ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" spills and stores the material back in its ",
					UI.FormatAsLink("Sweepy Dock", "SWEEPBOTSTATION"),
					"."
				});
			}

			// Token: 0x02003791 RID: 14225
			public class FLYDO
			{
				// Token: 0x0400E14B RID: 57675
				public static LocString NAME = UI.FormatAsLink("Flydo", "FETCHDRONE");

				// Token: 0x0400E14C RID: 57676
				public static LocString DESC = "A programmable delivery robot.\n\nPicks up " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " objects for delivery to selected destinations.";
			}
		}
	}
}
