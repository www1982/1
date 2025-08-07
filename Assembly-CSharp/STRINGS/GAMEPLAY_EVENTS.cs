using System;

namespace STRINGS
{
	// Token: 0x02000FA5 RID: 4005
	public class GAMEPLAY_EVENTS
	{
		// Token: 0x04005D9B RID: 23963
		public static LocString CANCELED = "{0} Canceled";

		// Token: 0x04005D9C RID: 23964
		public static LocString CANCELED_TOOLTIP = "The {0} event was canceled";

		// Token: 0x04005D9D RID: 23965
		public static LocString DEFAULT_OPTION_NAME = "OK";

		// Token: 0x04005D9E RID: 23966
		public static LocString DEFAULT_OPTION_CONSIDER_NAME = "Let me think about it";

		// Token: 0x04005D9F RID: 23967
		public static LocString CHAIN_EVENT_TOOLTIP = "This event is a chain event";

		// Token: 0x04005DA0 RID: 23968
		public static LocString BONUS_EVENT_DESCRIPTION = "{effects} for {duration}";

		// Token: 0x020023F6 RID: 9206
		public class LOCATIONS
		{
			// Token: 0x0400A2FD RID: 41725
			public static LocString NONE_AVAILABLE = "No location currently available";

			// Token: 0x0400A2FE RID: 41726
			public static LocString SUN = "The Sun";

			// Token: 0x0400A2FF RID: 41727
			public static LocString SURFACE = "Planetary Surface";

			// Token: 0x0400A300 RID: 41728
			public static LocString PRINTING_POD = BUILDINGS.PREFABS.HEADQUARTERS.NAME;

			// Token: 0x0400A301 RID: 41729
			public static LocString COLONY_WIDE = "Colonywide";
		}

		// Token: 0x020023F7 RID: 9207
		public class TIMES
		{
			// Token: 0x0400A302 RID: 41730
			public static LocString NOW = "Right now";

			// Token: 0x0400A303 RID: 41731
			public static LocString IN_CYCLES = "In {0} cycles";

			// Token: 0x0400A304 RID: 41732
			public static LocString UNKNOWN = "Sometime";
		}

		// Token: 0x020023F8 RID: 9208
		public class EVENT_TYPES
		{
			// Token: 0x02002F24 RID: 12068
			public class PARTY
			{
				// Token: 0x0400CC99 RID: 52377
				public static LocString NAME = "Party";

				// Token: 0x0400CC9A RID: 52378
				public static LocString DESCRIPTION = "THIS EVENT IS NOT WORKING\n{host} is throwing a birthday party for {dupe}. Make sure there is an available " + ROOMS.TYPES.REC_ROOM.NAME + " for the party.\n\nSocial events are good for Duplicant morale. Rejecting this party will hurt {host} and {dupe}'s fragile ego.";

				// Token: 0x0400CC9B RID: 52379
				public static LocString CANCELED_NO_ROOM_TITLE = "Party Canceled";

				// Token: 0x0400CC9C RID: 52380
				public static LocString CANCELED_NO_ROOM_DESCRIPTION = "The party was canceled because no " + ROOMS.TYPES.REC_ROOM.NAME + " was available.";

				// Token: 0x0400CC9D RID: 52381
				public static LocString UNDERWAY = "Party Happening";

				// Token: 0x0400CC9E RID: 52382
				public static LocString UNDERWAY_TOOLTIP = "There's a party going on";

				// Token: 0x0400CC9F RID: 52383
				public static LocString ACCEPT_OPTION_NAME = "Allow the party to happen";

				// Token: 0x0400CCA0 RID: 52384
				public static LocString ACCEPT_OPTION_DESC = "Party goers will get {goodEffect}";

				// Token: 0x0400CCA1 RID: 52385
				public static LocString ACCEPT_OPTION_INVALID_TOOLTIP = "A cake must be built for this event to take place.";

				// Token: 0x0400CCA2 RID: 52386
				public static LocString REJECT_OPTION_NAME = "Cancel the party";

				// Token: 0x0400CCA3 RID: 52387
				public static LocString REJECT_OPTION_DESC = "{host} and {dupe} gain {badEffect}";
			}

			// Token: 0x02002F25 RID: 12069
			public class ECLIPSE
			{
				// Token: 0x0400CCA4 RID: 52388
				public static LocString NAME = "Eclipse";

				// Token: 0x0400CCA5 RID: 52389
				public static LocString DESCRIPTION = "A celestial object has obscured the sunlight";
			}

			// Token: 0x02002F26 RID: 12070
			public class SOLAR_FLARE
			{
				// Token: 0x0400CCA6 RID: 52390
				public static LocString NAME = "Solar Storm";

				// Token: 0x0400CCA7 RID: 52391
				public static LocString DESCRIPTION = "A solar flare is headed this way";
			}

			// Token: 0x02002F27 RID: 12071
			public class CREATURE_SPAWN
			{
				// Token: 0x0400CCA8 RID: 52392
				public static LocString NAME = "Critter Infestation";

				// Token: 0x0400CCA9 RID: 52393
				public static LocString DESCRIPTION = "There was a massive influx of destructive critters";
			}

			// Token: 0x02002F28 RID: 12072
			public class SATELLITE_CRASH
			{
				// Token: 0x0400CCAA RID: 52394
				public static LocString NAME = "Satellite Crash";

				// Token: 0x0400CCAB RID: 52395
				public static LocString DESCRIPTION = "Mysterious space junk has crashed into the surface.\n\nIt may contain useful resources or information, but it may also be dangerous. Approach with caution.";
			}

			// Token: 0x02002F29 RID: 12073
			public class FOOD_FIGHT
			{
				// Token: 0x0400CCAC RID: 52396
				public static LocString NAME = "Food Fight";

				// Token: 0x0400CCAD RID: 52397
				public static LocString DESCRIPTION = "Duplicants will throw food at each other for recreation\n\nIt may be wasteful, but everyone who participates will benefit from a major stress reduction.";

				// Token: 0x0400CCAE RID: 52398
				public static LocString UNDERWAY = "Food Fight";

				// Token: 0x0400CCAF RID: 52399
				public static LocString UNDERWAY_TOOLTIP = "There is a food fight happening now";

				// Token: 0x0400CCB0 RID: 52400
				public static LocString ACCEPT_OPTION_NAME = "Duplicants start preparing to fight.";

				// Token: 0x0400CCB1 RID: 52401
				public static LocString ACCEPT_OPTION_DETAILS = "(Plus morale)";

				// Token: 0x0400CCB2 RID: 52402
				public static LocString REJECT_OPTION_NAME = "No food fight today";

				// Token: 0x0400CCB3 RID: 52403
				public static LocString REJECT_OPTION_DETAILS = "Sadface";
			}

			// Token: 0x02002F2A RID: 12074
			public class PLANT_BLIGHT
			{
				// Token: 0x0400CCB4 RID: 52404
				public static LocString NAME = "Plant Blight: {plant}";

				// Token: 0x0400CCB5 RID: 52405
				public static LocString DESCRIPTION = "Our {plant} crops have been afflicted by a fungal sickness!\n\nI must get the Duplicants to uproot and compost the sick plants to save our farms.";

				// Token: 0x0400CCB6 RID: 52406
				public static LocString SUCCESS = "Blight Managed: {plant}";

				// Token: 0x0400CCB7 RID: 52407
				public static LocString SUCCESS_TOOLTIP = "All the blighted {plant} plants have been dealt with, halting the infection.";
			}

			// Token: 0x02002F2B RID: 12075
			public class CRYOFRIEND
			{
				// Token: 0x0400CCB8 RID: 52408
				public static LocString NAME = "New Event: A Frozen Friend";

				// Token: 0x0400CCB9 RID: 52409
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"{dupe} has made an amazing discovery! A barely working ",
					BUILDINGS.PREFABS.CRYOTANK.NAME,
					" has been uncovered containing a {friend} inside in a frozen state.\n\n{dupe} was successful in thawing {friend} and this encounter has filled both Duplicants with a sense of hope, something they will desperately need to keep their ",
					UI.FormatAsLink("Morale", "MORALE"),
					" up when facing the dangers ahead."
				});

				// Token: 0x0400CCBA RID: 52410
				public static LocString BUTTON = "{friend} is thawed!";
			}

			// Token: 0x02002F2C RID: 12076
			public class WARPWORLDREVEAL
			{
				// Token: 0x0400CCBB RID: 52411
				public static LocString NAME = "New Event: Personnel Teleporter";

				// Token: 0x0400CCBC RID: 52412
				public static LocString DESCRIPTION = "I've discovered a functioning teleportation device with a pre-programmed destination.\n\nIt appears to go to another " + UI.CLUSTERMAP.PLANETOID + ", and I'm fairly certain there's a return device on the other end.\n\nI could send a Duplicant through safely if I desired.";

				// Token: 0x0400CCBD RID: 52413
				public static LocString BUTTON = "See Destination";
			}

			// Token: 0x02002F2D RID: 12077
			public class ARTIFACT_REVEAL
			{
				// Token: 0x0400CCBE RID: 52414
				public static LocString NAME = "New Event: Artifact Analyzed";

				// Token: 0x0400CCBF RID: 52415
				public static LocString DESCRIPTION = "An artifact from a past civilization was analyzed.\n\n{desc}";

				// Token: 0x0400CCC0 RID: 52416
				public static LocString BUTTON = "Close";
			}
		}

		// Token: 0x020023F9 RID: 9209
		public class BONUS
		{
			// Token: 0x02002F2E RID: 12078
			public class BONUSDREAM1
			{
				// Token: 0x0400CCC1 RID: 52417
				public static LocString NAME = "Good Dream";

				// Token: 0x0400CCC2 RID: 52418
				public static LocString DESCRIPTION = "I've observed many improvements to {dupe}'s demeanor today. Analysis indicates unusually high amounts of dopamine in their system. There's a good chance this is due to an exceptionally good dream and analysis indicates that current sleeping conditions may have contributed to this occurrence.\n\nFurther improvements to sleeping conditions may have additional positive effects to the " + UI.FormatAsLink("Morale", "MORALE") + " of {dupe} and other Duplicants.";

				// Token: 0x0400CCC3 RID: 52419
				public static LocString CHAIN_TOOLTIP = "Improving the living conditions of {dupe} will lead to more good dreams.";
			}

			// Token: 0x02002F2F RID: 12079
			public class BONUSDREAM2
			{
				// Token: 0x0400CCC4 RID: 52420
				public static LocString NAME = "Really Good Dream";

				// Token: 0x0400CCC5 RID: 52421
				public static LocString DESCRIPTION = "{dupe} had another really good dream and the resulting release of dopamine has made this Duplicant energetic and full of possibilities! This is an encouraging byproduct of improving the living conditions of the colony.\n\nBased on these observations, building a better sleeping area for my Duplicants will have a similar effect on their " + UI.FormatAsLink("Morale", "MORALE") + ".";
			}

			// Token: 0x02002F30 RID: 12080
			public class BONUSDREAM3
			{
				// Token: 0x0400CCC6 RID: 52422
				public static LocString NAME = "Great Dream";

				// Token: 0x0400CCC7 RID: 52423
				public static LocString DESCRIPTION = "I have detected a distinct spring in {dupe}'s step today. There is a good chance that this Duplicant had another great dream last night. Such incidents are further indications that working on the care and comfort of the colony is not a waste of time.\n\nI do wonder though: What do Duplicants dream of?";
			}

			// Token: 0x02002F31 RID: 12081
			public class BONUSDREAM4
			{
				// Token: 0x0400CCC8 RID: 52424
				public static LocString NAME = "Amazing Dream";

				// Token: 0x0400CCC9 RID: 52425
				public static LocString DESCRIPTION = "{dupe}'s dream last night must have been simply amazing! Their dopamine levels are at an all time high. Based on these results, it can be safely assumed that improving the living conditions of my Duplicants will reduce " + UI.FormatAsLink("Stress", "STRESS") + " and have similar positive effects on their well-being.\n\nObservations such as this are an integral and enjoyable part of science. When I see my Duplicants happy, I can't help but share in some of their joy.";
			}

			// Token: 0x02002F32 RID: 12082
			public class BONUSTOILET1
			{
				// Token: 0x0400CCCA RID: 52426
				public static LocString NAME = "Small Comforts";

				// Token: 0x0400CCCB RID: 52427
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"{dupe} recently visited an Outhouse and appears to have appreciated the small comforts based on the marked increase to their ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nHigh ",
					UI.FormatAsLink("Morale", "MORALE"),
					" has been linked to a better work ethic and greater enthusiasm for complex jobs, which are essential in building a successful new colony."
				});
			}

			// Token: 0x02002F33 RID: 12083
			public class BONUSTOILET2
			{
				// Token: 0x0400CCCC RID: 52428
				public static LocString NAME = "Greater Comforts";

				// Token: 0x0400CCCD RID: 52429
				public static LocString DESCRIPTION = "{dupe} used a Lavatory and analysis shows a decided improvement to this Duplicant's " + UI.FormatAsLink("Morale", "MORALE") + ".\n\nAs my colony grows and expands, it's important not to ignore the benefits of giving my Duplicants a pleasant place to relieve themselves.";
			}

			// Token: 0x02002F34 RID: 12084
			public class BONUSTOILET3
			{
				// Token: 0x0400CCCE RID: 52430
				public static LocString NAME = "Small Luxury";

				// Token: 0x0400CCCF RID: 52431
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"{dupe} visited a ",
					ROOMS.TYPES.LATRINE.NAME,
					" and experienced luxury unlike they anything this Duplicant had previously experienced as analysis has revealed yet another boost to their ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nIt is unclear whether this development is a result of increased hygiene or whether there is something else inherently about working plumbing which would improve ",
					UI.FormatAsLink("Morale", "MORALE"),
					" in this way. Further analysis is needed."
				});
			}

			// Token: 0x02002F35 RID: 12085
			public class BONUSTOILET4
			{
				// Token: 0x0400CCD0 RID: 52432
				public static LocString NAME = "Greater Luxury";

				// Token: 0x0400CCD1 RID: 52433
				public static LocString DESCRIPTION = "{dupe} visited a Washroom and the experience has left this Duplicant with significantly improved " + UI.FormatAsLink("Morale", "MORALE") + ". Analysis indicates this improvement should continue for many cycles.\n\nThe relationship of my Duplicants and their surroundings is an interesting aspect of colony life. I should continue to watch future developments in this department closely.";
			}

			// Token: 0x02002F36 RID: 12086
			public class BONUSRESEARCH
			{
				// Token: 0x0400CCD2 RID: 52434
				public static LocString NAME = "Inspired Learner";

				// Token: 0x0400CCD3 RID: 52435
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"Analysis indicates that the appearance of a ",
					UI.PRE_KEYWORD,
					"Research Station",
					UI.PST_KEYWORD,
					" has inspired {dupe} and heightened their brain activity on a cellular level.\n\nBrain stimulation is important if my Duplicants are going to adapt and innovate in their increasingly harsh environment."
				});
			}

			// Token: 0x02002F37 RID: 12087
			public class BONUSDIGGING1
			{
				// Token: 0x0400CCD4 RID: 52436
				public static LocString NAME = "Hot Diggity!";

				// Token: 0x0400CCD5 RID: 52437
				public static LocString DESCRIPTION = "Some interesting data has revealed that {dupe} has had a marked increase in physical abilities, an increase that cannot entirely be attributed to the usual improvements that occur after regular physical activity.\n\nBased on previous observations this Duplicant's positive associations with digging appear to account for this additional physical boost.\n\nThis would mean the personal preferences of my Duplicants are directly correlated to how hard they work. How interesting...";
			}

			// Token: 0x02002F38 RID: 12088
			public class BONUSSTORAGE
			{
				// Token: 0x0400CCD6 RID: 52438
				public static LocString NAME = "Something in Store";

				// Token: 0x0400CCD7 RID: 52439
				public static LocString DESCRIPTION = "Data indicates that {dupe}'s activity in storing something in a Storage Bin has led to an increase in this Duplicant's physical strength as well as an overall improvement to their general demeanor.\n\nThere have been many studies connecting organization with an increase in well-being. It is possible this explains {dupe}'s " + UI.FormatAsLink("Morale", "MORALE") + " improvements.";
			}

			// Token: 0x02002F39 RID: 12089
			public class BONUSBUILDER
			{
				// Token: 0x0400CCD8 RID: 52440
				public static LocString NAME = "Accomplished Builder";

				// Token: 0x0400CCD9 RID: 52441
				public static LocString DESCRIPTION = "{dupe} has been hard at work building many structures crucial to the future of the colony. It seems this activity has improved this Duplicant's budding construction and mechanical skills beyond what my models predicted.\n\nWhether this increase in ability is due to them learning new skills or simply gaining self-confidence I cannot say, but this unexpected development is a welcome surprise development.";
			}

			// Token: 0x02002F3A RID: 12090
			public class BONUSOXYGEN
			{
				// Token: 0x0400CCDA RID: 52442
				public static LocString NAME = "Fresh Air";

				// Token: 0x0400CCDB RID: 52443
				public static LocString DESCRIPTION = "{dupe} is experiencing a sudden unexpected improvement to their physical prowess which appears to be a result of exposure to elevated levels of oxygen from passing by an Oxygen Diffuser.\n\nObservations such as this are important in documenting just how beneficial having access to oxygen is to my colony.";
			}

			// Token: 0x02002F3B RID: 12091
			public class BONUSALGAE
			{
				// Token: 0x0400CCDC RID: 52444
				public static LocString NAME = "Fresh Algae Smell";

				// Token: 0x0400CCDD RID: 52445
				public static LocString DESCRIPTION = "{dupe}'s recent proximity to an Algae Terrarium has left them feeling refreshed and exuberant and is correlated to an increase in their physical attributes. It is unclear whether these physical improvements came from the excess of oxygen or the invigorating smell of algae.\n\nIt's curious that I find myself nostalgic for the smell of algae growing in a lab. But how could this be...?";
			}

			// Token: 0x02002F3C RID: 12092
			public class BONUSGENERATOR
			{
				// Token: 0x0400CCDE RID: 52446
				public static LocString NAME = "Exercised";

				// Token: 0x0400CCDF RID: 52447
				public static LocString DESCRIPTION = "{dupe} ran in a Manual Generator and the physical activity appears to have given this Duplicant increased strength and sense of well-being.\n\nWhile not the primary reason for building Manual Generators, I am very pleased to see my Duplicants reaping the " + UI.FormatAsLink("Stress", "STRESS") + " relieving benefits to physical activity.";
			}

			// Token: 0x02002F3D RID: 12093
			public class BONUSDOOR
			{
				// Token: 0x0400CCE0 RID: 52448
				public static LocString NAME = "Open and Shut";

				// Token: 0x0400CCE1 RID: 52449
				public static LocString DESCRIPTION = string.Concat(new string[]
				{
					"The act of closing a door has apparently lead to a decrease in the ",
					UI.FormatAsLink("Stress", "STRESS"),
					" levels of {dupe}, as well as decreased the exposure of this Duplicant to harmful ",
					UI.FormatAsLink("Germs", "GERMS"),
					".\n\nWhile it may be more efficient to group all my Duplicants together in common sleeping quarters, it's important to remember the mental benefits to privacy and space to express their individuality."
				});
			}

			// Token: 0x02002F3E RID: 12094
			public class BONUSHITTHEBOOKS
			{
				// Token: 0x0400CCE2 RID: 52450
				public static LocString NAME = "Hit the Books";

				// Token: 0x0400CCE3 RID: 52451
				public static LocString DESCRIPTION = "{dupe}'s recent Research errand has resulted in a significant increase to this Duplicant's brain activity. The discovery of newly found knowledge has given {dupe} an invigorating jolt of excitement.\n\nI am all too familiar with this feeling.";
			}

			// Token: 0x02002F3F RID: 12095
			public class BONUSLITWORKSPACE
			{
				// Token: 0x0400CCE4 RID: 52452
				public static LocString NAME = "Lit-erally Great";

				// Token: 0x0400CCE5 RID: 52453
				public static LocString DESCRIPTION = "{dupe}'s recent time in a well-lit area has greatly improved this Duplicant's ability to work with, and on, machinery.\n\nThis supports the prevailing theory that a well-lit workspace has many benefits beyond just improving my Duplicant's ability to see.";
			}

			// Token: 0x02002F40 RID: 12096
			public class BONUSTALKER
			{
				// Token: 0x0400CCE6 RID: 52454
				public static LocString NAME = "Big Small Talker";

				// Token: 0x0400CCE7 RID: 52455
				public static LocString DESCRIPTION = "{dupe}'s recent conversation with another Duplicant shows a correlation to improved serotonin and " + UI.FormatAsLink("Morale", "MORALE") + " levels in this Duplicant. It is very possible that small talk with a co-worker, however short and seemingly insignificant, will make my Duplicant's feel connected to the colony as a whole.\n\nAs the colony gets bigger and more sophisticated, I must ensure that the opportunity for such connections continue, for the good of my Duplicants' mental well being.";
			}
		}
	}
}
