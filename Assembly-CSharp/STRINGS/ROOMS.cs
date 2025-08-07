using System;
using TUNING;

namespace STRINGS
{
	// Token: 0x02000FA6 RID: 4006
	public class ROOMS
	{
		// Token: 0x020023FA RID: 9210
		public class CATEGORY
		{
			// Token: 0x02002F41 RID: 12097
			public class NONE
			{
				// Token: 0x0400CCE8 RID: 52456
				public static LocString NAME = "None";
			}

			// Token: 0x02002F42 RID: 12098
			public class FOOD
			{
				// Token: 0x0400CCE9 RID: 52457
				public static LocString NAME = "Dining";
			}

			// Token: 0x02002F43 RID: 12099
			public class SLEEP
			{
				// Token: 0x0400CCEA RID: 52458
				public static LocString NAME = "Sleep";
			}

			// Token: 0x02002F44 RID: 12100
			public class RECREATION
			{
				// Token: 0x0400CCEB RID: 52459
				public static LocString NAME = "Recreation";
			}

			// Token: 0x02002F45 RID: 12101
			public class BATHROOM
			{
				// Token: 0x0400CCEC RID: 52460
				public static LocString NAME = "Washroom";
			}

			// Token: 0x02002F46 RID: 12102
			public class BIONIC
			{
				// Token: 0x0400CCED RID: 52461
				public static LocString NAME = "";
			}

			// Token: 0x02002F47 RID: 12103
			public class HOSPITAL
			{
				// Token: 0x0400CCEE RID: 52462
				public static LocString NAME = "Medical";
			}

			// Token: 0x02002F48 RID: 12104
			public class INDUSTRIAL
			{
				// Token: 0x0400CCEF RID: 52463
				public static LocString NAME = "Industrial";
			}

			// Token: 0x02002F49 RID: 12105
			public class AGRICULTURAL
			{
				// Token: 0x0400CCF0 RID: 52464
				public static LocString NAME = "Agriculture";
			}

			// Token: 0x02002F4A RID: 12106
			public class PARK
			{
				// Token: 0x0400CCF1 RID: 52465
				public static LocString NAME = "Parks";
			}

			// Token: 0x02002F4B RID: 12107
			public class SCIENCE
			{
				// Token: 0x0400CCF2 RID: 52466
				public static LocString NAME = "Science";
			}
		}

		// Token: 0x020023FB RID: 9211
		public class TYPES
		{
			// Token: 0x0400A305 RID: 41733
			public static LocString CONFLICTED = "Conflicted Room";

			// Token: 0x02002F4C RID: 12108
			public class NEUTRAL
			{
				// Token: 0x0400CCF3 RID: 52467
				public static LocString NAME = "Miscellaneous Room";

				// Token: 0x0400CCF4 RID: 52468
				public static LocString DESCRIPTION = "An enclosed space with plenty of potential and no dedicated use.";

				// Token: 0x0400CCF5 RID: 52469
				public static LocString EFFECT = "- No effect";

				// Token: 0x0400CCF6 RID: 52470
				public static LocString TOOLTIP = "This area has walls and doors but no dedicated use";
			}

			// Token: 0x02002F4D RID: 12109
			public class LATRINE
			{
				// Token: 0x0400CCF7 RID: 52471
				public static LocString NAME = "Latrine";

				// Token: 0x0400CCF8 RID: 52472
				public static LocString DESCRIPTION = "It's a step up from doing one's business in full view of the rest of the colony.\n\nUsing a toilet in an enclosed room will improve Duplicants' Morale.";

				// Token: 0x0400CCF9 RID: 52473
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400CCFA RID: 52474
				public static LocString TOOLTIP = "Using a toilet in an enclosed room will improve Duplicants' Morale";
			}

			// Token: 0x02002F4E RID: 12110
			public class BIONICUPKEEP
			{
				// Token: 0x0400CCFB RID: 52475
				public static LocString NAME = "";

				// Token: 0x0400CCFC RID: 52476
				public static LocString DESCRIPTION = "";

				// Token: 0x0400CCFD RID: 52477
				public static LocString EFFECT = "";

				// Token: 0x0400CCFE RID: 52478
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002F4F RID: 12111
			public class PLUMBEDBATHROOM
			{
				// Token: 0x0400CCFF RID: 52479
				public static LocString NAME = "Washroom";

				// Token: 0x0400CD00 RID: 52480
				public static LocString DESCRIPTION = "A sanctuary of personal hygiene.\n\nUsing a fully plumbed Washroom will improve Duplicants' Morale.";

				// Token: 0x0400CD01 RID: 52481
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400CD02 RID: 52482
				public static LocString TOOLTIP = "Using a fully plumbed Washroom will improve Duplicants' Morale";
			}

			// Token: 0x02002F50 RID: 12112
			public class BARRACKS
			{
				// Token: 0x0400CD03 RID: 52483
				public static LocString NAME = "Barracks";

				// Token: 0x0400CD04 RID: 52484
				public static LocString DESCRIPTION = "A basic communal sleeping area for up-and-coming colonies.\n\nSleeping in Barracks will improve Duplicants' Morale.";

				// Token: 0x0400CD05 RID: 52485
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400CD06 RID: 52486
				public static LocString TOOLTIP = "Sleeping in Barracks will improve Duplicants' Morale";
			}

			// Token: 0x02002F51 RID: 12113
			public class BEDROOM
			{
				// Token: 0x0400CD07 RID: 52487
				public static LocString NAME = "Luxury Barracks";

				// Token: 0x0400CD08 RID: 52488
				public static LocString DESCRIPTION = "An upscale communal sleeping area full of things that greatly enhance quality of rest for occupants.\n\nSleeping in a Luxury Barracks will improve Duplicants' Morale.";

				// Token: 0x0400CD09 RID: 52489
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400CD0A RID: 52490
				public static LocString TOOLTIP = "Sleeping in a Luxury Barracks will improve Duplicants' Morale";
			}

			// Token: 0x02002F52 RID: 12114
			public class PRIVATE_BEDROOM
			{
				// Token: 0x0400CD0B RID: 52491
				public static LocString NAME = "Private Bedroom";

				// Token: 0x0400CD0C RID: 52492
				public static LocString DESCRIPTION = "A comfortable, roommate-free retreat where tired Duplicants can get uninterrupted rest.\n\nSleeping in a Private Bedroom will greatly improve Duplicants' Morale.";

				// Token: 0x0400CD0D RID: 52493
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400CD0E RID: 52494
				public static LocString TOOLTIP = "Sleeping in a Private Bedroom will greatly improve Duplicants' Morale";
			}

			// Token: 0x02002F53 RID: 12115
			public class MESSHALL
			{
				// Token: 0x0400CD0F RID: 52495
				public static LocString NAME = "Mess Hall";

				// Token: 0x0400CD10 RID: 52496
				public static LocString DESCRIPTION = "A simple dining room setup that's easy to improve upon.\n\nEating at a mess table in a Mess Hall will increase Duplicants' Morale.";

				// Token: 0x0400CD11 RID: 52497
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400CD12 RID: 52498
				public static LocString TOOLTIP = "Eating at a Mess Table in a Mess Hall will improve Duplicants' Morale";
			}

			// Token: 0x02002F54 RID: 12116
			public class KITCHEN
			{
				// Token: 0x0400CD13 RID: 52499
				public static LocString NAME = "Kitchen";

				// Token: 0x0400CD14 RID: 52500
				public static LocString DESCRIPTION = "A cooking area equipped to take meals to the next level.\n\nAdding ingredients from a Spice Grinder to foods cooked on an Electric Grill or Gas Range provides a variety of positive benefits.";

				// Token: 0x0400CD15 RID: 52501
				public static LocString EFFECT = "- Enables Spice Grinder use";

				// Token: 0x0400CD16 RID: 52502
				public static LocString TOOLTIP = "Using a Spice Grinder in a Kitchen adds benefits to foods cooked on Electric Grill or Gas Range";
			}

			// Token: 0x02002F55 RID: 12117
			public class GREATHALL
			{
				// Token: 0x0400CD17 RID: 52503
				public static LocString NAME = "Great Hall";

				// Token: 0x0400CD18 RID: 52504
				public static LocString DESCRIPTION = "A great place to eat, with great decor and great company. Great!\n\nEating in a Great Hall will significantly improve Duplicants' Morale.";

				// Token: 0x0400CD19 RID: 52505
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400CD1A RID: 52506
				public static LocString TOOLTIP = "Eating in a Great Hall will significantly improve Duplicants' Morale";
			}

			// Token: 0x02002F56 RID: 12118
			public class HOSPITAL
			{
				// Token: 0x0400CD1B RID: 52507
				public static LocString NAME = "Hospital";

				// Token: 0x0400CD1C RID: 52508
				public static LocString DESCRIPTION = "A dedicated medical facility that helps minimize recovery time.\n\nSick Duplicants assigned to medical buildings located within a Hospital are also less likely to spread Disease.";

				// Token: 0x0400CD1D RID: 52509
				public static LocString EFFECT = "- Quarantine sick Duplicants";

				// Token: 0x0400CD1E RID: 52510
				public static LocString TOOLTIP = "Sick Duplicants assigned to medical buildings located within a Hospital are less likely to spread Disease";
			}

			// Token: 0x02002F57 RID: 12119
			public class MASSAGE_CLINIC
			{
				// Token: 0x0400CD1F RID: 52511
				public static LocString NAME = "Massage Clinic";

				// Token: 0x0400CD20 RID: 52512
				public static LocString DESCRIPTION = "A soothing space with a very relaxing ambience, especially when well-decorated.\n\nReceiving massages at a Massage Clinic will significantly improve Stress reduction.";

				// Token: 0x0400CD21 RID: 52513
				public static LocString EFFECT = "- Massage stress relief bonus";

				// Token: 0x0400CD22 RID: 52514
				public static LocString TOOLTIP = "Receiving massages at a Massage Clinic will significantly improve Stress reduction";
			}

			// Token: 0x02002F58 RID: 12120
			public class POWER_PLANT
			{
				// Token: 0x0400CD23 RID: 52515
				public static LocString NAME = "Power Plant";

				// Token: 0x0400CD24 RID: 52516
				public static LocString DESCRIPTION = "The perfect place for Duplicants to flex their Electrical Engineering skills.\n\nHeavy-duty generators built within a Power Plant can be tuned up using microchips from power control stations to improve their " + UI.FormatAsLink("Power", "POWER") + " production.";

				// Token: 0x0400CD25 RID: 52517
				public static LocString EFFECT = "- Enables " + ITEMS.INDUSTRIAL_PRODUCTS.POWER_STATION_TOOLS.NAME + " tune-ups on heavy-duty generators";

				// Token: 0x0400CD26 RID: 52518
				public static LocString TOOLTIP = "Heavy-duty generators built in a Power Plant can be tuned up using microchips from Power Control Stations to improve their Power production";
			}

			// Token: 0x02002F59 RID: 12121
			public class MACHINE_SHOP
			{
				// Token: 0x0400CD27 RID: 52519
				public static LocString NAME = "Machine Shop";

				// Token: 0x0400CD28 RID: 52520
				public static LocString DESCRIPTION = "It smells like elbow grease.\n\nDuplicants working in a Machine Shop can maintain buildings and increase their production speed.";

				// Token: 0x0400CD29 RID: 52521
				public static LocString EFFECT = "- Increased fabrication efficiency";

				// Token: 0x0400CD2A RID: 52522
				public static LocString TOOLTIP = "Duplicants working in a Machine Shop can maintain buildings and increase their production speed";
			}

			// Token: 0x02002F5A RID: 12122
			public class FARM
			{
				// Token: 0x0400CD2B RID: 52523
				public static LocString NAME = "Greenhouse";

				// Token: 0x0400CD2C RID: 52524
				public static LocString DESCRIPTION = "An enclosed agricultural space best utilized by Duplicants with Crop Tending skills.\n\nCrops grown within a Greenhouse can be tended with Farm Station fertilizer to increase their growth speed.";

				// Token: 0x0400CD2D RID: 52525
				public static LocString EFFECT = "- Enables Farm Station use";

				// Token: 0x0400CD2E RID: 52526
				public static LocString TOOLTIP = "Crops grown within a Greenhouse can be tended with Farm Station fertilizer to increase their growth speed";
			}

			// Token: 0x02002F5B RID: 12123
			public class CREATUREPEN
			{
				// Token: 0x0400CD2F RID: 52527
				public static LocString NAME = "Stable";

				// Token: 0x0400CD30 RID: 52528
				public static LocString DESCRIPTION = "Critters don't mind it here, as long as things don't get too crowded.\n\nStabled critters can be tended to in order to improve their happiness, hasten their domestication and increase their production.\n\nEnables the use of Grooming Stations, Shearing Stations, Critter Condos, Critter Fountains and Milking Stations.";

				// Token: 0x0400CD31 RID: 52529
				public static LocString EFFECT = "- Critter taming and mood bonus";

				// Token: 0x0400CD32 RID: 52530
				public static LocString TOOLTIP = "A stable enables Grooming Station, Critter Condo, Critter Fountain, Shearing Station and Milking Station use";
			}

			// Token: 0x02002F5C RID: 12124
			public class REC_ROOM
			{
				// Token: 0x0400CD33 RID: 52531
				public static LocString NAME = "Recreation Room";

				// Token: 0x0400CD34 RID: 52532
				public static LocString DESCRIPTION = "Where Duplicants go to mingle with off-duty peers and indulge in a little R&R.\n\nScheduled Downtime will further improve Morale for Duplicants visiting a Recreation Room.";

				// Token: 0x0400CD35 RID: 52533
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400CD36 RID: 52534
				public static LocString TOOLTIP = "Scheduled Downtime will further improve Morale for Duplicants visiting a Recreation Room";
			}

			// Token: 0x02002F5D RID: 12125
			public class PARK
			{
				// Token: 0x0400CD37 RID: 52535
				public static LocString NAME = "Park";

				// Token: 0x0400CD38 RID: 52536
				public static LocString DESCRIPTION = "A little greenery goes a long way.\n\nPassing through natural spaces throughout the day will raise the Morale of Duplicants.";

				// Token: 0x0400CD39 RID: 52537
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400CD3A RID: 52538
				public static LocString TOOLTIP = "Passing through natural spaces throughout the day will raise the Morale of Duplicants";
			}

			// Token: 0x02002F5E RID: 12126
			public class NATURERESERVE
			{
				// Token: 0x0400CD3B RID: 52539
				public static LocString NAME = "Nature Reserve";

				// Token: 0x0400CD3C RID: 52540
				public static LocString DESCRIPTION = "A lot of greenery goes an even longer way.\n\nPassing through a Nature Reserve will grant higher Morale bonuses to Duplicants than a Park.";

				// Token: 0x0400CD3D RID: 52541
				public static LocString EFFECT = "- Morale bonus";

				// Token: 0x0400CD3E RID: 52542
				public static LocString TOOLTIP = "A Nature Reserve will grant higher Morale bonuses to Duplicants than a Park";
			}

			// Token: 0x02002F5F RID: 12127
			public class LABORATORY
			{
				// Token: 0x0400CD3F RID: 52543
				public static LocString NAME = "Laboratory";

				// Token: 0x0400CD40 RID: 52544
				public static LocString DESCRIPTION = "Where wild hypotheses meet rigorous scientific experimentation.\n\nScience stations built in a Laboratory function more efficiently.\n\nA Laboratory enables the use of the Geotuner and the Mission Control Station.";

				// Token: 0x0400CD41 RID: 52545
				public static LocString EFFECT = "- Efficiency bonus";

				// Token: 0x0400CD42 RID: 52546
				public static LocString TOOLTIP = "Science buildings built in a Laboratory function more efficiently\n\nA Laboratory enables Geotuner and Mission Control Station use";
			}

			// Token: 0x02002F60 RID: 12128
			public class PRIVATE_BATHROOM
			{
				// Token: 0x0400CD43 RID: 52547
				public static LocString NAME = "Private Bathroom";

				// Token: 0x0400CD44 RID: 52548
				public static LocString DESCRIPTION = "Finally, a place to truly be alone with one's thoughts.\n\nDuplicants relieve even more Stress when using the toilet in a Private Bathroom than in a Latrine.";

				// Token: 0x0400CD45 RID: 52549
				public static LocString EFFECT = "- Stress relief bonus";

				// Token: 0x0400CD46 RID: 52550
				public static LocString TOOLTIP = "Duplicants relieve even more stress when using the toilet in a Private Bathroom than in a Latrine";
			}

			// Token: 0x02002F61 RID: 12129
			public class BIONIC_UPKEEP
			{
				// Token: 0x0400CD47 RID: 52551
				public static LocString NAME = "";

				// Token: 0x0400CD48 RID: 52552
				public static LocString DESCRIPTION = "";

				// Token: 0x0400CD49 RID: 52553
				public static LocString EFFECT = "";

				// Token: 0x0400CD4A RID: 52554
				public static LocString TOOLTIP = "";
			}
		}

		// Token: 0x020023FC RID: 9212
		public class CRITERIA
		{
			// Token: 0x0400A306 RID: 41734
			public static LocString HEADER = "<b>Requirements:</b>";

			// Token: 0x0400A307 RID: 41735
			public static LocString NEUTRAL_TYPE = "Enclosed by wall tile";

			// Token: 0x0400A308 RID: 41736
			public static LocString POSSIBLE_TYPES_HEADER = "Possible Room Types";

			// Token: 0x0400A309 RID: 41737
			public static LocString NO_TYPE_CONFLICTS = "Remove conflicting buildings";

			// Token: 0x0400A30A RID: 41738
			public static LocString IN_CODE_ERROR = "String Key Not Found: {0}";

			// Token: 0x02002F62 RID: 12130
			public class CRITERIA_FAILED
			{
				// Token: 0x0400CD4B RID: 52555
				public static LocString MISSING_BUILDING = "Missing {0}";

				// Token: 0x0400CD4C RID: 52556
				public static LocString FAILED = "{0}";
			}

			// Token: 0x02002F63 RID: 12131
			public static class DECORATION
			{
				// Token: 0x0400CD4D RID: 52557
				public static LocString NAME = UI.FormatAsLink("Decor item", "REQUIREMENTCLASSDECORATION");

				// Token: 0x0400CD4E RID: 52558
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.DECORATION.NAME;
			}

			// Token: 0x02002F64 RID: 12132
			public class CEILING_HEIGHT
			{
				// Token: 0x0400CD4F RID: 52559
				public static LocString NAME = "Minimum height: {0} tiles";

				// Token: 0x0400CD50 RID: 52560
				public static LocString DESCRIPTION = "Must have a ceiling height of at least {0} tiles";

				// Token: 0x0400CD51 RID: 52561
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.CEILING_HEIGHT.NAME;
			}

			// Token: 0x02002F65 RID: 12133
			public class MINIMUM_SIZE
			{
				// Token: 0x0400CD52 RID: 52562
				public static LocString NAME = "Minimum size: {0} tiles";

				// Token: 0x0400CD53 RID: 52563
				public static LocString DESCRIPTION = "Must have an area of at least {0} tiles";

				// Token: 0x0400CD54 RID: 52564
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.MINIMUM_SIZE.NAME;
			}

			// Token: 0x02002F66 RID: 12134
			public class MAXIMUM_SIZE
			{
				// Token: 0x0400CD55 RID: 52565
				public static LocString NAME = "Maximum size: {0} tiles";

				// Token: 0x0400CD56 RID: 52566
				public static LocString DESCRIPTION = "Must have an area no larger than {0} tiles";

				// Token: 0x0400CD57 RID: 52567
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.MAXIMUM_SIZE.NAME;
			}

			// Token: 0x02002F67 RID: 12135
			public class INDUSTRIALMACHINERY
			{
				// Token: 0x0400CD58 RID: 52568
				public static LocString NAME = UI.FormatAsLink("Industrial machinery", "REQUIREMENTCLASSINDUSTRIALMACHINERY");

				// Token: 0x0400CD59 RID: 52569
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.INDUSTRIALMACHINERY.NAME;
			}

			// Token: 0x02002F68 RID: 12136
			public class HAS_BED
			{
				// Token: 0x0400CD5A RID: 52570
				public static LocString NAME = "One or more " + UI.FormatAsLink("beds", "REQUIREMENTCLASSBEDTYPE");

				// Token: 0x0400CD5B RID: 52571
				public static LocString DESCRIPTION = "Requires at least one Cot or Comfy Bed";

				// Token: 0x0400CD5C RID: 52572
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.HAS_BED.NAME;
			}

			// Token: 0x02002F69 RID: 12137
			public class HAS_LUXURY_BED
			{
				// Token: 0x0400CD5D RID: 52573
				public static LocString NAME = "One or more " + UI.FormatAsLink("Comfy Beds", "LUXURYBED");

				// Token: 0x0400CD5E RID: 52574
				public static LocString DESCRIPTION = "Requires at least one Comfy Bed";

				// Token: 0x0400CD5F RID: 52575
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.HAS_LUXURY_BED.NAME;
			}

			// Token: 0x02002F6A RID: 12138
			public class LUXURYBEDTYPE
			{
				// Token: 0x0400CD60 RID: 52576
				public static LocString NAME = "Single " + UI.FormatAsLink("Comfy Bed", "LUXURYBED");

				// Token: 0x0400CD61 RID: 52577
				public static LocString DESCRIPTION = "Must have no more than one Comfy Bed";

				// Token: 0x0400CD62 RID: 52578
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.LUXURYBEDTYPE.NAME;
			}

			// Token: 0x02002F6B RID: 12139
			public class BED_SINGLE
			{
				// Token: 0x0400CD63 RID: 52579
				public static LocString NAME = "Single " + UI.FormatAsLink("beds", "REQUIREMENTCLASSBEDTYPE");

				// Token: 0x0400CD64 RID: 52580
				public static LocString DESCRIPTION = "Must have no more than one Cot or Comfy Bed";

				// Token: 0x0400CD65 RID: 52581
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.BED_SINGLE.NAME;
			}

			// Token: 0x02002F6C RID: 12140
			public class IS_BACKWALLED
			{
				// Token: 0x0400CD66 RID: 52582
				public static LocString NAME = "Has backwall tiles";

				// Token: 0x0400CD67 RID: 52583
				public static LocString DESCRIPTION = "Must be covered in backwall tiles";

				// Token: 0x0400CD68 RID: 52584
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.IS_BACKWALLED.NAME;
			}

			// Token: 0x02002F6D RID: 12141
			public class NO_COTS
			{
				// Token: 0x0400CD69 RID: 52585
				public static LocString NAME = "No " + UI.FormatAsLink("Cots", "BED");

				// Token: 0x0400CD6A RID: 52586
				public static LocString DESCRIPTION = "Room cannot contain a Cot";

				// Token: 0x0400CD6B RID: 52587
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.NO_COTS.NAME;
			}

			// Token: 0x02002F6E RID: 12142
			public class NO_LUXURY_BEDS
			{
				// Token: 0x0400CD6C RID: 52588
				public static LocString NAME = "No " + UI.FormatAsLink("Comfy Beds", "LUXURYBED");

				// Token: 0x0400CD6D RID: 52589
				public static LocString DESCRIPTION = "Room cannot contain a Comfy Bed";

				// Token: 0x0400CD6E RID: 52590
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.NO_LUXURY_BEDS.NAME;
			}

			// Token: 0x02002F6F RID: 12143
			public class BEDTYPE
			{
				// Token: 0x0400CD6F RID: 52591
				public static LocString NAME = UI.FormatAsLink("Beds", "REQUIREMENTCLASSBEDTYPE");

				// Token: 0x0400CD70 RID: 52592
				public static LocString DESCRIPTION = "Requires two or more Cots or Comfy Beds";

				// Token: 0x0400CD71 RID: 52593
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.BEDTYPE.NAME;
			}

			// Token: 0x02002F70 RID: 12144
			public class BUILDING_DECOR_POSITIVE
			{
				// Token: 0x0400CD72 RID: 52594
				public static LocString NAME = "Positive " + UI.FormatAsLink("decor", "REQUIREMENTCLASSDECORATION");

				// Token: 0x0400CD73 RID: 52595
				public static LocString DESCRIPTION = "Requires at least one building with positive decor";

				// Token: 0x0400CD74 RID: 52596
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.BUILDING_DECOR_POSITIVE.NAME;
			}

			// Token: 0x02002F71 RID: 12145
			public class DECORATIVE_ITEM
			{
				// Token: 0x0400CD75 RID: 52597
				public static LocString NAME = UI.FormatAsLink("Decor item", "REQUIREMENTCLASSDECORATION") + " ({0})";

				// Token: 0x0400CD76 RID: 52598
				public static LocString DESCRIPTION = "Requires {0} or more Decor items";

				// Token: 0x0400CD77 RID: 52599
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.DECORATIVE_ITEM.NAME;
			}

			// Token: 0x02002F72 RID: 12146
			public class DECOR20
			{
				// Token: 0x0600DA4A RID: 55882 RVA: 0x0044810C File Offset: 0x0044630C
				// Note: this type is marked as 'beforefieldinit'.
				static DECOR20()
				{
					string text = "Requires a decorative item with a minimum Decor value of ";
					int amount = BUILDINGS.DECOR.BONUS.TIER3.amount;
					ROOMS.CRITERIA.DECOR20.DESCRIPTION = text + amount.ToString();
					ROOMS.CRITERIA.DECOR20.CONFLICT_DESCRIPTION = ROOMS.CRITERIA.DECOR20.NAME;
				}

				// Token: 0x0400CD78 RID: 52600
				public static LocString NAME = UI.FormatAsLink("Fancy decor item", "REQUIREMENTCLASSDECORATION");

				// Token: 0x0400CD79 RID: 52601
				public static LocString DESCRIPTION;

				// Token: 0x0400CD7A RID: 52602
				public static LocString CONFLICT_DESCRIPTION;
			}

			// Token: 0x02002F73 RID: 12147
			public class CLINIC
			{
				// Token: 0x0400CD7B RID: 52603
				public static LocString NAME = UI.FormatAsLink("Medical equipment", "REQUIREMENTCLASSCLINIC");

				// Token: 0x0400CD7C RID: 52604
				public static LocString DESCRIPTION = "Requires one or more Sick Bays or Disease Clinics";

				// Token: 0x0400CD7D RID: 52605
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.CLINIC.NAME;
			}

			// Token: 0x02002F74 RID: 12148
			public class POWERPLANT
			{
				// Token: 0x0400CD7E RID: 52606
				public static LocString NAME = UI.FormatAsLink("Heavy-Duty Generator", "REQUIREMENTCLASSGENERATORTYPE") + "\n    • Two or more " + UI.FormatAsLink("Power Buildings", "REQUIREMENTCLASSPOWERBUILDING");

				// Token: 0x0400CD7F RID: 52607
				public static LocString DESCRIPTION = "Requires a Heavy-Duty Generator and two or more Power Buildings";

				// Token: 0x0400CD80 RID: 52608
				public static LocString CONFLICT_DESCRIPTION = "Heavy-Duty Generator and two or more Power buildings";
			}

			// Token: 0x02002F75 RID: 12149
			public class FARMSTATIONTYPE
			{
				// Token: 0x0400CD81 RID: 52609
				public static LocString NAME = UI.FormatAsLink("Farm Station", "FARMSTATION");

				// Token: 0x0400CD82 RID: 52610
				public static LocString DESCRIPTION = "Requires a single Farm Station";

				// Token: 0x0400CD83 RID: 52611
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.FARMSTATIONTYPE.NAME;
			}

			// Token: 0x02002F76 RID: 12150
			public class FARMBUILDING
			{
				// Token: 0x0400CD84 RID: 52612
				public static LocString NAME = UI.FormatAsLink("Farm Building", "FARMBUILDING");

				// Token: 0x0400CD85 RID: 52613
				public static LocString DESCRIPTION = "";

				// Token: 0x0400CD86 RID: 52614
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.FARMBUILDING.NAME;
			}

			// Token: 0x02002F77 RID: 12151
			public class CREATURE_FEEDER
			{
				// Token: 0x0400CD87 RID: 52615
				public static LocString NAME = UI.FormatAsLink("Critter Feeder", "CREATUREFEEDER");

				// Token: 0x0400CD88 RID: 52616
				public static LocString DESCRIPTION = "Requires a single Critter Feeder";

				// Token: 0x0400CD89 RID: 52617
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.CREATURE_FEEDER.NAME;
			}

			// Token: 0x02002F78 RID: 12152
			public class RANCHSTATIONTYPE
			{
				// Token: 0x0400CD8A RID: 52618
				public static LocString NAME = UI.FormatAsLink("Ranching building", "REQUIREMENTCLASSRANCHSTATIONTYPE");

				// Token: 0x0400CD8B RID: 52619
				public static LocString DESCRIPTION = "Requires a single Grooming Station, Critter Condo, Critter Fountain, Shearing Station or Milking Station";

				// Token: 0x0400CD8C RID: 52620
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.RANCHSTATIONTYPE.NAME;
			}

			// Token: 0x02002F79 RID: 12153
			public class SPICESTATION
			{
				// Token: 0x0400CD8D RID: 52621
				public static LocString NAME = UI.FormatAsLink("Spice Grinder", "SPICEGRINDER");

				// Token: 0x0400CD8E RID: 52622
				public static LocString DESCRIPTION = "Requires a single Spice Grinder";

				// Token: 0x0400CD8F RID: 52623
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.SPICESTATION.NAME;
			}

			// Token: 0x02002F7A RID: 12154
			public class COOKTOP
			{
				// Token: 0x0400CD90 RID: 52624
				public static LocString NAME = UI.FormatAsLink("Cooking station", "REQUIREMENTCLASSCOOKTOP");

				// Token: 0x0400CD91 RID: 52625
				public static LocString DESCRIPTION = "Requires a single Electric Grill or Gas Range";

				// Token: 0x0400CD92 RID: 52626
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.COOKTOP.NAME;
			}

			// Token: 0x02002F7B RID: 12155
			public class REFRIGERATOR
			{
				// Token: 0x0400CD93 RID: 52627
				public static LocString NAME = UI.FormatAsLink("Refrigerator", "REFRIGERATOR");

				// Token: 0x0400CD94 RID: 52628
				public static LocString DESCRIPTION = "Requires a single Refrigerator";

				// Token: 0x0400CD95 RID: 52629
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.REFRIGERATOR.NAME;
			}

			// Token: 0x02002F7C RID: 12156
			public class RECBUILDING
			{
				// Token: 0x0400CD96 RID: 52630
				public static LocString NAME = UI.FormatAsLink("Recreational building", "REQUIREMENTCLASSRECBUILDING");

				// Token: 0x0400CD97 RID: 52631
				public static LocString DESCRIPTION = "Requires one or more recreational buildings";

				// Token: 0x0400CD98 RID: 52632
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.RECBUILDING.NAME;
			}

			// Token: 0x02002F7D RID: 12157
			public class PARK
			{
				// Token: 0x0400CD99 RID: 52633
				public static LocString NAME = UI.FormatAsLink("Park Sign", "PARKSIGN");

				// Token: 0x0400CD9A RID: 52634
				public static LocString DESCRIPTION = "Requires one or more Park Signs";

				// Token: 0x0400CD9B RID: 52635
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.PARK.NAME;
			}

			// Token: 0x02002F7E RID: 12158
			public class MACHINESHOPTYPE
			{
				// Token: 0x0400CD9C RID: 52636
				public static LocString NAME = "Mechanics Station";

				// Token: 0x0400CD9D RID: 52637
				public static LocString DESCRIPTION = "Requires requires one or more Mechanics Stations";

				// Token: 0x0400CD9E RID: 52638
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.MACHINESHOPTYPE.NAME;
			}

			// Token: 0x02002F7F RID: 12159
			public class FOOD_BOX
			{
				// Token: 0x0400CD9F RID: 52639
				public static LocString NAME = "Food storage";

				// Token: 0x0400CDA0 RID: 52640
				public static LocString DESCRIPTION = "Requires one or more Ration Boxes or Refrigerators";

				// Token: 0x0400CDA1 RID: 52641
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.FOOD_BOX.NAME;
			}

			// Token: 0x02002F80 RID: 12160
			public class LIGHTSOURCE
			{
				// Token: 0x0400CDA2 RID: 52642
				public static LocString NAME = UI.FormatAsLink("Light source", "REQUIREMENTCLASSLIGHTSOURCE");

				// Token: 0x0400CDA3 RID: 52643
				public static LocString DESCRIPTION = "Requires one or more light sources";

				// Token: 0x0400CDA4 RID: 52644
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.LIGHTSOURCE.NAME;
			}

			// Token: 0x02002F81 RID: 12161
			public class DESTRESSINGBUILDING
			{
				// Token: 0x0400CDA5 RID: 52645
				public static LocString NAME = UI.FormatAsLink("De-Stressing Building", "MASSAGETABLE");

				// Token: 0x0400CDA6 RID: 52646
				public static LocString DESCRIPTION = "Requires one or more De-Stressing buildings";

				// Token: 0x0400CDA7 RID: 52647
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.DESTRESSINGBUILDING.NAME;
			}

			// Token: 0x02002F82 RID: 12162
			public class MASSAGE_TABLE
			{
				// Token: 0x0400CDA8 RID: 52648
				public static LocString NAME = UI.FormatAsLink("Massage Table", "MASSAGETABLE");

				// Token: 0x0400CDA9 RID: 52649
				public static LocString DESCRIPTION = "Requires one or more Massage Tables";

				// Token: 0x0400CDAA RID: 52650
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.MASSAGE_TABLE.NAME;
			}

			// Token: 0x02002F83 RID: 12163
			public class MESSTABLE
			{
				// Token: 0x0400CDAB RID: 52651
				public static LocString NAME = UI.FormatAsLink("Mess Table", "DININGTABLE");

				// Token: 0x0400CDAC RID: 52652
				public static LocString DESCRIPTION = "Requires a single Mess Table";

				// Token: 0x0400CDAD RID: 52653
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.MESSTABLE.NAME;
			}

			// Token: 0x02002F84 RID: 12164
			public class NO_MESS_STATION
			{
				// Token: 0x0400CDAE RID: 52654
				public static LocString NAME = "No " + UI.FormatAsLink("Mess Table", "DININGTABLE");

				// Token: 0x0400CDAF RID: 52655
				public static LocString DESCRIPTION = "Cannot contain a Mess Table";

				// Token: 0x0400CDB0 RID: 52656
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.NO_MESS_STATION.NAME;
			}

			// Token: 0x02002F85 RID: 12165
			public class MESS_STATION_MULTIPLE
			{
				// Token: 0x0400CDB1 RID: 52657
				public static LocString NAME = UI.FormatAsLink("Mess Tables", "DININGTABLE");

				// Token: 0x0400CDB2 RID: 52658
				public static LocString DESCRIPTION = "Requires two or more Mess Tables";

				// Token: 0x0400CDB3 RID: 52659
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.MESS_STATION_MULTIPLE.NAME;
			}

			// Token: 0x02002F86 RID: 12166
			public class RESEARCH_STATION
			{
				// Token: 0x0400CDB4 RID: 52660
				public static LocString NAME = UI.FormatAsLink("Research station", "REQUIREMENTCLASSRESEARCH_STATION");

				// Token: 0x0400CDB5 RID: 52661
				public static LocString DESCRIPTION = "Requires one or more Research Stations or Super Computers";

				// Token: 0x0400CDB6 RID: 52662
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.RESEARCH_STATION.NAME;
			}

			// Token: 0x02002F87 RID: 12167
			public class BIONICUPKEEP
			{
				// Token: 0x0400CDB7 RID: 52663
				public static LocString NAME = UI.FormatAsLink("Bionic service station", "GROUPBIONICUPKEEP");

				// Token: 0x0400CDB8 RID: 52664
				public static LocString DESCRIPTION = "Requires at least one Lubrication Station and one Gunk Extractor";

				// Token: 0x0400CDB9 RID: 52665
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.BIONICUPKEEP.NAME;
			}

			// Token: 0x02002F88 RID: 12168
			public class BIONIC_GUNKEMPTIER
			{
				// Token: 0x0400CDBA RID: 52666
				public static LocString NAME = UI.FormatAsLink("Gunk Extractor", "REQUIREMENTCLASSBIONIC_GUNKEMPTIER");

				// Token: 0x0400CDBB RID: 52667
				public static LocString DESCRIPTION = "Requires one or more Gunk Extractors";

				// Token: 0x0400CDBC RID: 52668
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.BIONIC_GUNKEMPTIER.NAME;
			}

			// Token: 0x02002F89 RID: 12169
			public class BIONIC_LUBRICATION
			{
				// Token: 0x0400CDBD RID: 52669
				public static LocString NAME = UI.FormatAsLink("Lubrication Station", "REQUIREMENTCLASSBIONIC_LUBRICATION");

				// Token: 0x0400CDBE RID: 52670
				public static LocString DESCRIPTION = "Requires one or more Lubrication Stations";

				// Token: 0x0400CDBF RID: 52671
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.BIONIC_LUBRICATION.NAME;
			}

			// Token: 0x02002F8A RID: 12170
			public class TOILETTYPE
			{
				// Token: 0x0400CDC0 RID: 52672
				public static LocString NAME = UI.FormatAsLink("Toilet", "REQUIREMENTCLASSTOILETTYPE");

				// Token: 0x0400CDC1 RID: 52673
				public static LocString DESCRIPTION = "Requires one or more Outhouses or Lavatories";

				// Token: 0x0400CDC2 RID: 52674
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.TOILETTYPE.NAME;
			}

			// Token: 0x02002F8B RID: 12171
			public class FLUSHTOILETTYPE
			{
				// Token: 0x0400CDC3 RID: 52675
				public static LocString NAME = UI.FormatAsLink("Flush Toilet", "REQUIREMENTCLASSFLUSHTOILETTYPE");

				// Token: 0x0400CDC4 RID: 52676
				public static LocString DESCRIPTION = "Requires one or more Lavatories";

				// Token: 0x0400CDC5 RID: 52677
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.FLUSHTOILETTYPE.NAME;
			}

			// Token: 0x02002F8C RID: 12172
			public class NO_OUTHOUSES
			{
				// Token: 0x0400CDC6 RID: 52678
				public static LocString NAME = "No " + UI.FormatAsLink("Outhouses", "OUTHOUSE");

				// Token: 0x0400CDC7 RID: 52679
				public static LocString DESCRIPTION = "Cannot contain basic Outhouses";

				// Token: 0x0400CDC8 RID: 52680
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.NO_OUTHOUSES.NAME;
			}

			// Token: 0x02002F8D RID: 12173
			public class WASHSTATION
			{
				// Token: 0x0400CDC9 RID: 52681
				public static LocString NAME = UI.FormatAsLink("Wash station", "REQUIREMENTCLASSWASHSTATION");

				// Token: 0x0400CDCA RID: 52682
				public static LocString DESCRIPTION = "Requires one or more Wash Basins, Sinks, Hand Sanitizers, or Showers";

				// Token: 0x0400CDCB RID: 52683
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.WASHSTATION.NAME;
			}

			// Token: 0x02002F8E RID: 12174
			public class ADVANCEDWASHSTATION
			{
				// Token: 0x0400CDCC RID: 52684
				public static LocString NAME = UI.FormatAsLink("Plumbed wash station", "REQUIREMENTCLASSWASHSTATION");

				// Token: 0x0400CDCD RID: 52685
				public static LocString DESCRIPTION = "Requires one or more Sinks, Hand Sanitizers, or Showers";

				// Token: 0x0400CDCE RID: 52686
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.ADVANCEDWASHSTATION.NAME;
			}

			// Token: 0x02002F8F RID: 12175
			public class NO_INDUSTRIAL_MACHINERY
			{
				// Token: 0x0400CDCF RID: 52687
				public static LocString NAME = "No " + UI.FormatAsLink("industrial machinery", "REQUIREMENTCLASSINDUSTRIALMACHINERY");

				// Token: 0x0400CDD0 RID: 52688
				public static LocString DESCRIPTION = "Cannot contain any building labeled Industrial Machinery";

				// Token: 0x0400CDD1 RID: 52689
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.NO_INDUSTRIAL_MACHINERY.NAME;
			}

			// Token: 0x02002F90 RID: 12176
			public class WILDANIMAL
			{
				// Token: 0x0400CDD2 RID: 52690
				public static LocString NAME = "Wildlife";

				// Token: 0x0400CDD3 RID: 52691
				public static LocString DESCRIPTION = "Requires at least one wild critter";

				// Token: 0x0400CDD4 RID: 52692
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.WILDANIMAL.NAME;
			}

			// Token: 0x02002F91 RID: 12177
			public class WILDANIMALS
			{
				// Token: 0x0400CDD5 RID: 52693
				public static LocString NAME = "More wildlife";

				// Token: 0x0400CDD6 RID: 52694
				public static LocString DESCRIPTION = "Requires two or more wild critters";

				// Token: 0x0400CDD7 RID: 52695
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.WILDANIMALS.NAME;
			}

			// Token: 0x02002F92 RID: 12178
			public class WILDPLANT
			{
				// Token: 0x0400CDD8 RID: 52696
				public static LocString NAME = "Two wild plants";

				// Token: 0x0400CDD9 RID: 52697
				public static LocString DESCRIPTION = "Requires two or more wild plants";

				// Token: 0x0400CDDA RID: 52698
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.WILDPLANT.NAME;
			}

			// Token: 0x02002F93 RID: 12179
			public class WILDPLANTS
			{
				// Token: 0x0400CDDB RID: 52699
				public static LocString NAME = "Four wild plants";

				// Token: 0x0400CDDC RID: 52700
				public static LocString DESCRIPTION = "Requires four or more wild plants";

				// Token: 0x0400CDDD RID: 52701
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.WILDPLANTS.NAME;
			}

			// Token: 0x02002F94 RID: 12180
			public class SCIENCEBUILDING
			{
				// Token: 0x0400CDDE RID: 52702
				public static LocString NAME = UI.FormatAsLink("Science building", "REQUIREMENTCLASSSCIENCEBUILDING");

				// Token: 0x0400CDDF RID: 52703
				public static LocString DESCRIPTION = "Requires one or more science buildings";

				// Token: 0x0400CDE0 RID: 52704
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.SCIENCEBUILDING.NAME;
			}

			// Token: 0x02002F95 RID: 12181
			public class SCIENCE_BUILDINGS
			{
				// Token: 0x0400CDE1 RID: 52705
				public static LocString NAME = "Two " + UI.FormatAsLink("science buildings", "REQUIREMENTCLASSSCIENCEBUILDING");

				// Token: 0x0400CDE2 RID: 52706
				public static LocString DESCRIPTION = "Requires two or more science buildings";

				// Token: 0x0400CDE3 RID: 52707
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.SCIENCE_BUILDINGS.NAME;
			}

			// Token: 0x02002F96 RID: 12182
			public class ROCKETINTERIOR
			{
				// Token: 0x0400CDE4 RID: 52708
				public static LocString NAME = UI.FormatAsLink("Rocket interior", "REQUIREMENTCLASSROCKETINTERIOR");

				// Token: 0x0400CDE5 RID: 52709
				public static LocString DESCRIPTION = "Must be built inside a rocket";

				// Token: 0x0400CDE6 RID: 52710
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.ROCKETINTERIOR.NAME;
			}

			// Token: 0x02002F97 RID: 12183
			public class WARMINGSTATION
			{
				// Token: 0x0400CDE7 RID: 52711
				public static LocString NAME = UI.FormatAsLink("Warming station", "REQUIREMENTCLASSWARMINGSTATION");

				// Token: 0x0400CDE8 RID: 52712
				public static LocString DESCRIPTION = "Raises the ambient temperature";

				// Token: 0x0400CDE9 RID: 52713
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.WARMINGSTATION.NAME;
			}

			// Token: 0x02002F98 RID: 12184
			public class GENERATORTYPE
			{
				// Token: 0x0400CDEA RID: 52714
				public static LocString NAME = UI.FormatAsLink("Generator", "REQUIREMENTCLASSGENERATORTYPE");

				// Token: 0x0400CDEB RID: 52715
				public static LocString DESCRIPTION = "Generates electrical power";

				// Token: 0x0400CDEC RID: 52716
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.GENERATORTYPE.NAME;
			}

			// Token: 0x02002F99 RID: 12185
			public class HEAVYDUTYGENERATORTYPE
			{
				// Token: 0x0400CDED RID: 52717
				public static LocString NAME = UI.FormatAsLink("Heavy-duty generator", "REQUIREMENTCLASSGENERATORTYPE");

				// Token: 0x0400CDEE RID: 52718
				public static LocString DESCRIPTION = "For big power needs";

				// Token: 0x0400CDEF RID: 52719
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.HEAVYDUTYGENERATORTYPE.NAME;
			}

			// Token: 0x02002F9A RID: 12186
			public class LIGHTDUTYGENERATORTYPE
			{
				// Token: 0x0400CDF0 RID: 52720
				public static LocString NAME = UI.FormatAsLink("Basic generator", "REQUIREMENTCLASSGENERATORTYPE");

				// Token: 0x0400CDF1 RID: 52721
				public static LocString DESCRIPTION = "For basic power needs";

				// Token: 0x0400CDF2 RID: 52722
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.LIGHTDUTYGENERATORTYPE.NAME;
			}

			// Token: 0x02002F9B RID: 12187
			public class POWERBUILDING
			{
				// Token: 0x0400CDF3 RID: 52723
				public static LocString NAME = UI.FormatAsLink("Power building", "REQUIREMENTCLASSPOWERBUILDING");

				// Token: 0x0400CDF4 RID: 52724
				public static LocString DESCRIPTION = "Buildings that generate, store, or manage power";

				// Token: 0x0400CDF5 RID: 52725
				public static LocString CONFLICT_DESCRIPTION = ROOMS.CRITERIA.POWERBUILDING.NAME;
			}
		}

		// Token: 0x020023FD RID: 9213
		public class DETAILS
		{
			// Token: 0x0400A30B RID: 41739
			public static LocString HEADER = "Room Details";

			// Token: 0x02002F9C RID: 12188
			public class ASSIGNED_TO
			{
				// Token: 0x0400CDF6 RID: 52726
				public static LocString NAME = "<b>Assignments:</b>\n{0}";

				// Token: 0x0400CDF7 RID: 52727
				public static LocString UNASSIGNED = "Unassigned";
			}

			// Token: 0x02002F9D RID: 12189
			public class AVERAGE_TEMPERATURE
			{
				// Token: 0x0400CDF8 RID: 52728
				public static LocString NAME = "Average temperature: {0}";
			}

			// Token: 0x02002F9E RID: 12190
			public class AVERAGE_ATMO_MASS
			{
				// Token: 0x0400CDF9 RID: 52729
				public static LocString NAME = "Average air pressure: {0}";
			}

			// Token: 0x02002F9F RID: 12191
			public class SIZE
			{
				// Token: 0x0400CDFA RID: 52730
				public static LocString NAME = "Room size: {0} Tiles";
			}

			// Token: 0x02002FA0 RID: 12192
			public class BUILDING_COUNT
			{
				// Token: 0x0400CDFB RID: 52731
				public static LocString NAME = "Buildings: {0}";
			}

			// Token: 0x02002FA1 RID: 12193
			public class CREATURE_COUNT
			{
				// Token: 0x0400CDFC RID: 52732
				public static LocString NAME = "Critters: {0}";
			}

			// Token: 0x02002FA2 RID: 12194
			public class PLANT_COUNT
			{
				// Token: 0x0400CDFD RID: 52733
				public static LocString NAME = "Plants: {0}";
			}
		}

		// Token: 0x020023FE RID: 9214
		public class EFFECTS
		{
			// Token: 0x0400A30C RID: 41740
			public static LocString HEADER = "<b>Effects:</b>";
		}
	}
}
