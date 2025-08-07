using System;

namespace STRINGS
{
	// Token: 0x02000FAD RID: 4013
	public class ITEMS
	{
		// Token: 0x02002428 RID: 9256
		public class PILLS
		{
			// Token: 0x020036F2 RID: 14066
			public class PLACEBO
			{
				// Token: 0x0400DFB9 RID: 57273
				public static LocString NAME = "Placebo";

				// Token: 0x0400DFBA RID: 57274
				public static LocString DESC = "A general, all-purpose " + UI.FormatAsLink("Medicine", "MEDICINE") + ".\n\nThe less one knows about it, the better it works.";

				// Token: 0x0400DFBB RID: 57275
				public static LocString RECIPEDESC = "All-purpose " + UI.FormatAsLink("Medicine", "MEDICINE") + ".";
			}

			// Token: 0x020036F3 RID: 14067
			public class BASICBOOSTER
			{
				// Token: 0x0400DFBC RID: 57276
				public static LocString NAME = UI.FormatAsLink("Vitamin Chews", "BASICBOOSTER");

				// Token: 0x0400DFBD RID: 57277
				public static LocString DESC = "Minorly reduces the chance of becoming sick.";

				// Token: 0x0400DFBE RID: 57278
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"A supplement that minorly reduces the chance of contracting a ",
					UI.PRE_KEYWORD,
					"Germ",
					UI.PST_KEYWORD,
					"-based ",
					UI.FormatAsLink("Disease", "DISEASE"),
					".\n\nMust be taken daily."
				});
			}

			// Token: 0x020036F4 RID: 14068
			public class INTERMEDIATEBOOSTER
			{
				// Token: 0x0400DFBF RID: 57279
				public static LocString NAME = UI.FormatAsLink("Immuno Booster", "INTERMEDIATEBOOSTER");

				// Token: 0x0400DFC0 RID: 57280
				public static LocString DESC = "Significantly reduces the chance of becoming sick.";

				// Token: 0x0400DFC1 RID: 57281
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"A supplement that significantly reduces the chance of contracting a ",
					UI.PRE_KEYWORD,
					"Germ",
					UI.PST_KEYWORD,
					"-based ",
					UI.FormatAsLink("Disease", "DISEASE"),
					".\n\nMust be taken daily."
				});
			}

			// Token: 0x020036F5 RID: 14069
			public class ANTIHISTAMINE
			{
				// Token: 0x0400DFC2 RID: 57282
				public static LocString NAME = UI.FormatAsLink("Allergy Medication", "ANTIHISTAMINE");

				// Token: 0x0400DFC3 RID: 57283
				public static LocString DESC = "Suppresses and prevents allergic reactions.";

				// Token: 0x0400DFC4 RID: 57284
				public static LocString RECIPEDESC = "A strong antihistamine Duplicants can take to halt an allergic reaction. " + ITEMS.PILLS.ANTIHISTAMINE.NAME + " will also prevent further reactions from occurring for a short time after ingestion.";
			}

			// Token: 0x020036F6 RID: 14070
			public class BASICCURE
			{
				// Token: 0x0400DFC5 RID: 57285
				public static LocString NAME = UI.FormatAsLink("Curative Tablet", "BASICCURE");

				// Token: 0x0400DFC6 RID: 57286
				public static LocString DESC = "A simple, easy-to-take remedy for minor germ-based diseases.";

				// Token: 0x0400DFC7 RID: 57287
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"Duplicants can take this to cure themselves of minor ",
					UI.PRE_KEYWORD,
					"Germ",
					UI.PST_KEYWORD,
					"-based ",
					UI.FormatAsLink("Diseases", "DISEASE"),
					".\n\nCurative Tablets are very effective against ",
					UI.FormatAsLink("Food Poisoning", "FOODSICKNESS"),
					"."
				});
			}

			// Token: 0x020036F7 RID: 14071
			public class INTERMEDIATECURE
			{
				// Token: 0x0400DFC8 RID: 57288
				public static LocString NAME = UI.FormatAsLink("Medical Pack", "INTERMEDIATECURE");

				// Token: 0x0400DFC9 RID: 57289
				public static LocString DESC = "A doctor-administered cure for moderate ailments.";

				// Token: 0x0400DFCA RID: 57290
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"A doctor-administered cure for moderate ",
					UI.FormatAsLink("Diseases", "DISEASE"),
					". ",
					ITEMS.PILLS.INTERMEDIATECURE.NAME,
					"s are very effective against ",
					UI.FormatAsLink("Slimelung", "SLIMESICKNESS"),
					".\n\nMust be administered by a Duplicant with the ",
					DUPLICANTS.ROLES.MEDIC.NAME,
					" Skill."
				});
			}

			// Token: 0x020036F8 RID: 14072
			public class ADVANCEDCURE
			{
				// Token: 0x0400DFCB RID: 57291
				public static LocString NAME = UI.FormatAsLink("Serum Vial", "ADVANCEDCURE");

				// Token: 0x0400DFCC RID: 57292
				public static LocString DESC = "A doctor-administered cure for severe ailments.";

				// Token: 0x0400DFCD RID: 57293
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"An extremely powerful medication created to treat severe ",
					UI.FormatAsLink("Diseases", "DISEASE"),
					". ",
					ITEMS.PILLS.ADVANCEDCURE.NAME,
					" is very effective against ",
					UI.FormatAsLink("Zombie Spores", "ZOMBIESPORES"),
					".\n\nMust be administered by a Duplicant with the ",
					DUPLICANTS.ROLES.SENIOR_MEDIC.NAME,
					" Skill."
				});
			}

			// Token: 0x020036F9 RID: 14073
			public class BASICRADPILL
			{
				// Token: 0x0400DFCE RID: 57294
				public static LocString NAME = UI.FormatAsLink("Basic Rad Pill", "BASICRADPILL");

				// Token: 0x0400DFCF RID: 57295
				public static LocString DESC = "Increases a Duplicant's natural radiation absorption rate.";

				// Token: 0x0400DFD0 RID: 57296
				public static LocString RECIPEDESC = "A supplement that speeds up the rate at which a Duplicant body absorbs radiation, allowing them to manage increased radiation exposure.\n\nMust be taken daily.";
			}

			// Token: 0x020036FA RID: 14074
			public class INTERMEDIATERADPILL
			{
				// Token: 0x0400DFD1 RID: 57297
				public static LocString NAME = UI.FormatAsLink("Intermediate Rad Pill", "INTERMEDIATERADPILL");

				// Token: 0x0400DFD2 RID: 57298
				public static LocString DESC = "Increases a Duplicant's natural radiation absorption rate.";

				// Token: 0x0400DFD3 RID: 57299
				public static LocString RECIPEDESC = "A supplement that speeds up the rate at which a Duplicant body absorbs radiation, allowing them to manage increased radiation exposure.\n\nMust be taken daily.";
			}
		}

		// Token: 0x02002429 RID: 9257
		public class LUBRICATIONSTICK
		{
			// Token: 0x0400A40A RID: 41994
			public static LocString NAME = UI.FormatAsLink("Gear Balm", "LUBRICATIONSTICK");

			// Token: 0x0400A40B RID: 41995
			public static LocString SUBHEADER = "Mechanical Lubricant";

			// Token: 0x0400A40C RID: 41996
			public static LocString DESC = string.Concat(new string[]
			{
				"Provides a small amount of lubricating ",
				UI.FormatAsLink("Gear Oil", "LUBRICATINGOIL"),
				".\n\nCan be produced at the ",
				BUILDINGS.PREFABS.APOTHECARY.NAME,
				"."
			});

			// Token: 0x0400A40D RID: 41997
			public static LocString RECIPEDESC = "A self-administered mechanical lubricant for Duplicants with bionic parts.";
		}

		// Token: 0x0200242A RID: 9258
		public class TALLOWLUBRICATIONSTICK
		{
			// Token: 0x0400A40E RID: 41998
			public static LocString NAME = UI.FormatAsLink("Tallow Gear Balm", "TALLOWLUBRICATIONSTICK");

			// Token: 0x0400A40F RID: 41999
			public static LocString SUBHEADER = "Mechanical Lubricant";

			// Token: 0x0400A410 RID: 42000
			public static LocString DESC = string.Concat(new string[]
			{
				"Provides a small amount of extra-silky lubricating ",
				UI.FormatAsLink("Gear Oil", "LUBRICATINGOIL"),
				".\n\nCan be produced at the ",
				BUILDINGS.PREFABS.APOTHECARY.NAME,
				"."
			});

			// Token: 0x0400A411 RID: 42001
			public static LocString RECIPEDESC = "An advanced self-administered mechanical lubricant for Duplicants with bionic parts.";
		}

		// Token: 0x0200242B RID: 9259
		public class BIONIC_BOOSTERS
		{
			// Token: 0x0400A412 RID: 42002
			public static LocString FABRICATION_SOURCE = "This booster can be manufactured at the {0}.";

			// Token: 0x020036FB RID: 14075
			public class BOOSTER_DIG1
			{
				// Token: 0x0400DFD4 RID: 57300
				public static LocString NAME = UI.FormatAsLink("Digging Booster", "BOOSTER_DIG1");

				// Token: 0x0400DFD5 RID: 57301
				public static LocString DESC = "Grants a Bionic Duplicant the skill required to dig hard things.";
			}

			// Token: 0x020036FC RID: 14076
			public class BOOSTER_DIG2
			{
				// Token: 0x0400DFD6 RID: 57302
				public static LocString NAME = UI.FormatAsLink("Extreme Digging Booster", "BOOSTER_DIG2");

				// Token: 0x0400DFD7 RID: 57303
				public static LocString DESC = "Grants a Bionic Duplicant the digging skill required to get through anything.";
			}

			// Token: 0x020036FD RID: 14077
			public class BOOSTER_CONSTRUCT1
			{
				// Token: 0x0400DFD8 RID: 57304
				public static LocString NAME = UI.FormatAsLink("Construction Booster", "BOOSTER_CONSTRUCT1");

				// Token: 0x0400DFD9 RID: 57305
				public static LocString DESC = "Grants a Bionic Duplicant the ability to build fast, and demolish buildings that others cannot.";
			}

			// Token: 0x020036FE RID: 14078
			public class BOOSTER_FARM1
			{
				// Token: 0x0400DFDA RID: 57306
				public static LocString NAME = UI.FormatAsLink("Crop Tending Booster", "BOOSTER_FARM1");

				// Token: 0x0400DFDB RID: 57307
				public static LocString DESC = "Grants a Bionic Duplicant unparalleled farming and botanical analysis skills.";
			}

			// Token: 0x020036FF RID: 14079
			public class BOOSTER_RANCH1
			{
				// Token: 0x0400DFDC RID: 57308
				public static LocString NAME = UI.FormatAsLink("Ranching Booster", "BOOSTER_RANCH1");

				// Token: 0x0400DFDD RID: 57309
				public static LocString DESC = "Grants a Bionic Duplicant the skills required to care for " + UI.FormatAsLink("Critters", "CREATURES") + " in every way.";
			}

			// Token: 0x02003700 RID: 14080
			public class BOOSTER_COOK1
			{
				// Token: 0x0400DFDE RID: 57310
				public static LocString NAME = UI.FormatAsLink("Grilling Booster", "BOOSTER_COOK1");

				// Token: 0x0400DFDF RID: 57311
				public static LocString DESC = "Grants a Bionic Duplicant deliciously professional culinary skills.";
			}

			// Token: 0x02003701 RID: 14081
			public class BOOSTER_ART1
			{
				// Token: 0x0400DFE0 RID: 57312
				public static LocString NAME = UI.FormatAsLink("Masterworks Art Booster", "BOOSTER_ART1");

				// Token: 0x0400DFE1 RID: 57313
				public static LocString DESC = "Grants a Bionic Duplicant flawless decorating skills.";
			}

			// Token: 0x02003702 RID: 14082
			public class BOOSTER_RESEARCH1
			{
				// Token: 0x0400DFE2 RID: 57314
				public static LocString NAME = UI.FormatAsLink("Researching Booster", "BOOSTER_RESEARCH1");

				// Token: 0x0400DFE3 RID: 57315
				public static LocString DESC = "Grants a Bionic Duplicant the expertise required to study " + UI.FormatAsLink("geysers", "GEYSERS") + " and other advanced topics.";
			}

			// Token: 0x02003703 RID: 14083
			public class BOOSTER_RESEARCH2
			{
				// Token: 0x0400DFE4 RID: 57316
				public static LocString NAME = UI.FormatAsLink("Astronomy Booster", "BOOSTER_RESEARCH2");

				// Token: 0x0400DFE5 RID: 57317
				public static LocString DESC = "Grants a Bionic Duplicant a keen grasp of science and usage of space-research buildings.";
			}

			// Token: 0x02003704 RID: 14084
			public class BOOSTER_RESEARCH3
			{
				// Token: 0x0400DFE6 RID: 57318
				public static LocString NAME = UI.FormatAsLink("Applied Sciences Booster", "BOOSTER_RESEARCH3");

				// Token: 0x0400DFE7 RID: 57319
				public static LocString DESC = "Grants a Bionic Duplicant a deeply pragmatic approach to scientific research.";
			}

			// Token: 0x02003705 RID: 14085
			public class BOOSTER_PILOT1
			{
				// Token: 0x0400DFE8 RID: 57320
				public static LocString NAME = UI.FormatAsLink("Piloting Booster", "BOOSTER_PILOT1");

				// Token: 0x0400DFE9 RID: 57321
				public static LocString DESC = "Grants a Bionic Duplicant the expertise required to explore the skies in person.";
			}

			// Token: 0x02003706 RID: 14086
			public class BOOSTER_PILOTVANILLA1
			{
				// Token: 0x0400DFEA RID: 57322
				public static LocString NAME = UI.FormatAsLink("Rocketry Booster", "BOOSTER_PILOTVANILLA1");

				// Token: 0x0400DFEB RID: 57323
				public static LocString DESC = "Grants a Bionic Duplicant the expertise required to command a rocket.";
			}

			// Token: 0x02003707 RID: 14087
			public class BOOSTER_SUITS1
			{
				// Token: 0x0400DFEC RID: 57324
				public static LocString NAME = UI.FormatAsLink("Suit Training Booster", "BOOSTER_SUITS1");

				// Token: 0x0400DFED RID: 57325
				public static LocString DESC = "Enables a Bionic Duplicant to maximize durability of equipped " + UI.FormatAsLink("Exosuits", "EQUIPMENT") + " and maintain their runspeed.";
			}

			// Token: 0x02003708 RID: 14088
			public class BOOSTER_CARRY1
			{
				// Token: 0x0400DFEE RID: 57326
				public static LocString NAME = UI.FormatAsLink("Strength Booster", "BOOSTER_CARRY1");

				// Token: 0x0400DFEF RID: 57327
				public static LocString DESC = "Grants a Bionic Duplicant increased carrying capacity and athletic prowess.";
			}

			// Token: 0x02003709 RID: 14089
			public class BOOSTER_OP1
			{
				// Token: 0x0400DFF0 RID: 57328
				public static LocString NAME = UI.FormatAsLink("Electrical Engineering Booster", "BOOSTER_OP1");

				// Token: 0x0400DFF1 RID: 57329
				public static LocString DESC = "Grants a Bionic Duplicant the skills requried to tinker and solder to their heart's content.";
			}

			// Token: 0x0200370A RID: 14090
			public class BOOSTER_OP2
			{
				// Token: 0x0400DFF2 RID: 57330
				public static LocString NAME = UI.FormatAsLink("Mechatronics Engineering Booster", "BOOSTER_OP2");

				// Token: 0x0400DFF3 RID: 57331
				public static LocString DESC = "Grants a Bionic Duplicant complete mastery of engineering skills.";
			}

			// Token: 0x0200370B RID: 14091
			public class BOOSTER_MEDICINE1
			{
				// Token: 0x0400DFF4 RID: 57332
				public static LocString NAME = UI.FormatAsLink("Advanced Medical Booster", "BOOSTER_MEDICINE1");

				// Token: 0x0400DFF5 RID: 57333
				public static LocString DESC = "Grants a Bionic Duplicant the ability to perform all doctoring errands.";
			}

			// Token: 0x0200370C RID: 14092
			public class BOOSTER_TIDY1
			{
				// Token: 0x0400DFF6 RID: 57334
				public static LocString NAME = UI.FormatAsLink("Tidying Booster", "BOOSTER_TIDY1");

				// Token: 0x0400DFF7 RID: 57335
				public static LocString DESC = "Grants a Bionic Duplicant the full range of tidying skills, including blasting unwanted meteors out of the sky.";
			}
		}

		// Token: 0x0200242C RID: 9260
		public class FOOD
		{
			// Token: 0x0400A413 RID: 42003
			public static LocString COMPOST = "Compost";

			// Token: 0x0200370D RID: 14093
			public class FOODSPLAT
			{
				// Token: 0x0400DFF8 RID: 57336
				public static LocString NAME = "Food Splatter";

				// Token: 0x0400DFF9 RID: 57337
				public static LocString DESC = "Food smeared on the wall from a recent Food Fight";
			}

			// Token: 0x0200370E RID: 14094
			public class BURGER
			{
				// Token: 0x0400DFFA RID: 57338
				public static LocString NAME = UI.FormatAsLink("Frost Burger", "BURGER");

				// Token: 0x0400DFFB RID: 57339
				public static LocString DESC = string.Concat(new string[]
				{
					UI.FormatAsLink("Meat", "MEAT"),
					" and ",
					UI.FormatAsLink("Lettuce", "LETTUCE"),
					" on a chilled ",
					UI.FormatAsLink("Frost Bun", "COLDWHEATBREAD"),
					".\n\nIt's the only burger best served cold."
				});

				// Token: 0x0400DFFC RID: 57340
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					UI.FormatAsLink("Meat", "MEAT"),
					" and ",
					UI.FormatAsLink("Lettuce", "LETTUCE"),
					" on a chilled ",
					UI.FormatAsLink("Frost Bun", "COLDWHEATBREAD"),
					"."
				});

				// Token: 0x02003BBA RID: 15290
				public class DEHYDRATED
				{
					// Token: 0x0400EBCC RID: 60364
					public static LocString NAME = "Dried Frost Burger";

					// Token: 0x0400EBCD RID: 60365
					public static LocString DESC = string.Concat(new string[]
					{
						"A dehydrated ",
						UI.FormatAsLink("Frost Burger", "BURGER"),
						" ration. It must be rehydrated in order to be considered ",
						UI.FormatAsLink("Food", "FOOD"),
						".\n\nDry rations have no expiry date."
					});
				}
			}

			// Token: 0x0200370F RID: 14095
			public class FIELDRATION
			{
				// Token: 0x0400DFFD RID: 57341
				public static LocString NAME = UI.FormatAsLink("Nutrient Bar", "FIELDRATION");

				// Token: 0x0400DFFE RID: 57342
				public static LocString DESC = "A nourishing nutrient paste, sandwiched between thin wafer layers.";
			}

			// Token: 0x02003710 RID: 14096
			public class MUSHBAR
			{
				// Token: 0x0400DFFF RID: 57343
				public static LocString NAME = UI.FormatAsLink("Mush Bar", "MUSHBAR");

				// Token: 0x0400E000 RID: 57344
				public static LocString DESC = "An edible, putrefied mudslop.\n\nMush Bars are preferable to starvation, but only just barely.";

				// Token: 0x0400E001 RID: 57345
				public static LocString RECIPEDESC = "An edible, putrefied mudslop.\n\n" + ITEMS.FOOD.MUSHBAR.NAME + "s are preferable to starvation, but only just barely.";
			}

			// Token: 0x02003711 RID: 14097
			public class MUSHROOMWRAP
			{
				// Token: 0x0400E002 RID: 57346
				public static LocString NAME = UI.FormatAsLink("Mushroom Wrap", "MUSHROOMWRAP");

				// Token: 0x0400E003 RID: 57347
				public static LocString DESC = string.Concat(new string[]
				{
					"Flavorful ",
					UI.FormatAsLink("Mushrooms", "MUSHROOM"),
					" wrapped in ",
					UI.FormatAsLink("Lettuce", "LETTUCE"),
					".\n\nIt has an earthy flavor punctuated by a refreshing crunch."
				});

				// Token: 0x0400E004 RID: 57348
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"Flavorful ",
					UI.FormatAsLink("Mushrooms", "MUSHROOM"),
					" wrapped in ",
					UI.FormatAsLink("Lettuce", "LETTUCE"),
					"."
				});

				// Token: 0x02003BBB RID: 15291
				public class DEHYDRATED
				{
					// Token: 0x0400EBCE RID: 60366
					public static LocString NAME = "Dried Mushroom Wrap";

					// Token: 0x0400EBCF RID: 60367
					public static LocString DESC = string.Concat(new string[]
					{
						"A dehydrated ",
						UI.FormatAsLink("Mushroom Wrap", "MUSHROOMWRAP"),
						" ration. It must be rehydrated in order to be considered ",
						UI.FormatAsLink("Food", "FOOD"),
						".\n\nDry rations have no expiry date."
					});
				}
			}

			// Token: 0x02003712 RID: 14098
			public class MICROWAVEDLETTUCE
			{
				// Token: 0x0400E005 RID: 57349
				public static LocString NAME = UI.FormatAsLink("Microwaved Lettuce", "MICROWAVEDLETTUCE");

				// Token: 0x0400E006 RID: 57350
				public static LocString DESC = UI.FormatAsLink("Lettuce", "LETTUCE") + " scrumptiously wilted in the " + BUILDINGS.PREFABS.GAMMARAYOVEN.NAME + ".";

				// Token: 0x0400E007 RID: 57351
				public static LocString RECIPEDESC = UI.FormatAsLink("Lettuce", "LETTUCE") + " scrumptiously wilted in the " + BUILDINGS.PREFABS.GAMMARAYOVEN.NAME + ".";
			}

			// Token: 0x02003713 RID: 14099
			public class GAMMAMUSH
			{
				// Token: 0x0400E008 RID: 57352
				public static LocString NAME = UI.FormatAsLink("Gamma Mush", "GAMMAMUSH");

				// Token: 0x0400E009 RID: 57353
				public static LocString DESC = "A disturbingly delicious mixture of irradiated dirt and water.";

				// Token: 0x0400E00A RID: 57354
				public static LocString RECIPEDESC = UI.FormatAsLink("Mush Fry", "FRIEDMUSHBAR") + " reheated in a " + BUILDINGS.PREFABS.GAMMARAYOVEN.NAME + ".";
			}

			// Token: 0x02003714 RID: 14100
			public class FRUITCAKE
			{
				// Token: 0x0400E00B RID: 57355
				public static LocString NAME = UI.FormatAsLink("Berry Sludge", "FRUITCAKE");

				// Token: 0x0400E00C RID: 57356
				public static LocString DESC = "A mashed up " + UI.FormatAsLink("Bristle Berry", "PRICKLEFRUIT") + " sludge with an exceptionally long shelf life.\n\nIts aggressive, overbearing sweetness can leave the tongue feeling temporarily numb.";

				// Token: 0x0400E00D RID: 57357
				public static LocString RECIPEDESC = "A mashed up " + UI.FormatAsLink("Bristle Berry", "PRICKLEFRUIT") + " sludge with an exceptionally long shelf life.";
			}

			// Token: 0x02003715 RID: 14101
			public class POPCORN
			{
				// Token: 0x0400E00E RID: 57358
				public static LocString NAME = UI.FormatAsLink("Popcorn", "POPCORN");

				// Token: 0x0400E00F RID: 57359
				public static LocString DESC = UI.FormatAsLink("Sleet Wheat Grain", "COLDWHEATSEED") + " popped in a " + BUILDINGS.PREFABS.GAMMARAYOVEN.NAME + ".\n\nCompletely devoid of any fancy flavorings.";

				// Token: 0x0400E010 RID: 57360
				public static LocString RECIPEDESC = "Gamma-radiated " + UI.FormatAsLink("Sleet Wheat Grain", "COLDWHEATSEED") + ".";
			}

			// Token: 0x02003716 RID: 14102
			public class SUSHI
			{
				// Token: 0x0400E011 RID: 57361
				public static LocString NAME = UI.FormatAsLink("Sushi", "SUSHI");

				// Token: 0x0400E012 RID: 57362
				public static LocString DESC = string.Concat(new string[]
				{
					"Raw ",
					UI.FormatAsLink("Pacu Fillet", "FISHMEAT"),
					" wrapped with fresh ",
					UI.FormatAsLink("Lettuce", "LETTUCE"),
					".\n\nWhile the salt of the lettuce may initially overpower the flavor, a keen palate can discern the subtle sweetness of the fillet beneath."
				});

				// Token: 0x0400E013 RID: 57363
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"Raw ",
					UI.FormatAsLink("Pacu Fillet", "FISHMEAT"),
					" wrapped with fresh ",
					UI.FormatAsLink("Lettuce", "LETTUCE"),
					"."
				});
			}

			// Token: 0x02003717 RID: 14103
			public class HATCHEGG
			{
				// Token: 0x0400E014 RID: 57364
				public static LocString NAME = CREATURES.SPECIES.HATCH.EGG_NAME;

				// Token: 0x0400E015 RID: 57365
				public static LocString DESC = string.Concat(new string[]
				{
					"An egg laid by a ",
					UI.FormatAsLink("Hatch", "HATCH"),
					".\n\nIf incubated, it will hatch into a ",
					UI.FormatAsLink("Hatchling", "HATCH"),
					"."
				});

				// Token: 0x0400E016 RID: 57366
				public static LocString RECIPEDESC = "An egg laid by a " + UI.FormatAsLink("Hatch", "HATCH") + ".";
			}

			// Token: 0x02003718 RID: 14104
			public class DRECKOEGG
			{
				// Token: 0x0400E017 RID: 57367
				public static LocString NAME = CREATURES.SPECIES.DRECKO.EGG_NAME;

				// Token: 0x0400E018 RID: 57368
				public static LocString DESC = string.Concat(new string[]
				{
					"An egg laid by a ",
					UI.FormatAsLink("Drecko", "DRECKO"),
					".\n\nIf incubated, it will hatch into a new ",
					UI.FormatAsLink("Drecklet", "DRECKO"),
					"."
				});

				// Token: 0x0400E019 RID: 57369
				public static LocString RECIPEDESC = "An egg laid by a " + UI.FormatAsLink("Drecko", "DRECKO") + ".";
			}

			// Token: 0x02003719 RID: 14105
			public class LIGHTBUGEGG
			{
				// Token: 0x0400E01A RID: 57370
				public static LocString NAME = CREATURES.SPECIES.LIGHTBUG.EGG_NAME;

				// Token: 0x0400E01B RID: 57371
				public static LocString DESC = string.Concat(new string[]
				{
					"An egg laid by a ",
					UI.FormatAsLink("Shine Bug", "LIGHTBUG"),
					".\n\nIf incubated, it will hatch into a ",
					UI.FormatAsLink("Shine Nymph", "LIGHTBUG"),
					"."
				});

				// Token: 0x0400E01C RID: 57372
				public static LocString RECIPEDESC = "An egg laid by a " + UI.FormatAsLink("Shine Bug", "LIGHTBUG") + ".";
			}

			// Token: 0x0200371A RID: 14106
			public class LETTUCE
			{
				// Token: 0x0400E01D RID: 57373
				public static LocString NAME = UI.FormatAsLink("Lettuce", "LETTUCE");

				// Token: 0x0400E01E RID: 57374
				public static LocString DESC = "Crunchy, slightly salty leaves from a " + UI.FormatAsLink("Waterweed", "SEALETTUCE") + " plant.";

				// Token: 0x0400E01F RID: 57375
				public static LocString RECIPEDESC = "Edible roughage from a " + UI.FormatAsLink("Waterweed", "SEALETTUCE") + ".";
			}

			// Token: 0x0200371B RID: 14107
			public class PASTA
			{
				// Token: 0x0400E020 RID: 57376
				public static LocString NAME = UI.FormatAsLink("Pasta", "PASTA");

				// Token: 0x0400E021 RID: 57377
				public static LocString DESC = "pasta made from egg and wheat";

				// Token: 0x0400E022 RID: 57378
				public static LocString RECIPEDESC = "pasta made from egg and wheat";
			}

			// Token: 0x0200371C RID: 14108
			public class PANCAKES
			{
				// Token: 0x0400E023 RID: 57379
				public static LocString NAME = UI.FormatAsLink("Soufflé Pancakes", "PANCAKES");

				// Token: 0x0400E024 RID: 57380
				public static LocString DESC = string.Concat(new string[]
				{
					"Sweet discs made from ",
					UI.FormatAsLink("Raw Egg", "RAWEGG"),
					" and ",
					UI.FormatAsLink("Sleet Wheat Grain", "COLDWHEATSEED"),
					".\n\nThey're so thick!"
				});

				// Token: 0x0400E025 RID: 57381
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"Sweet discs made from ",
					UI.FormatAsLink("Raw Egg", "RAWEGG"),
					" and ",
					UI.FormatAsLink("Sleet Wheat Grain", "COLDWHEATSEED"),
					"."
				});
			}

			// Token: 0x0200371D RID: 14109
			public class OILFLOATEREGG
			{
				// Token: 0x0400E026 RID: 57382
				public static LocString NAME = CREATURES.SPECIES.OILFLOATER.EGG_NAME;

				// Token: 0x0400E027 RID: 57383
				public static LocString DESC = string.Concat(new string[]
				{
					"An egg laid by a ",
					UI.FormatAsLink("Slickster", "OILFLOATER"),
					".\n\nIf incubated, it will hatch into a ",
					UI.FormatAsLink("Slickster Larva", "OILFLOATER"),
					"."
				});

				// Token: 0x0400E028 RID: 57384
				public static LocString RECIPEDESC = "An egg laid by a " + UI.FormatAsLink("Slickster", "OILFLOATER") + ".";
			}

			// Token: 0x0200371E RID: 14110
			public class PUFTEGG
			{
				// Token: 0x0400E029 RID: 57385
				public static LocString NAME = CREATURES.SPECIES.PUFT.EGG_NAME;

				// Token: 0x0400E02A RID: 57386
				public static LocString DESC = string.Concat(new string[]
				{
					"An egg laid by a ",
					UI.FormatAsLink("Puft", "PUFT"),
					".\n\nIf incubated, it will hatch into a ",
					UI.FormatAsLink("Puftlet", "PUFT"),
					"."
				});

				// Token: 0x0400E02B RID: 57387
				public static LocString RECIPEDESC = "An egg laid by a " + CREATURES.SPECIES.PUFT.NAME + ".";
			}

			// Token: 0x0200371F RID: 14111
			public class PREHISTORICPACUFILLET
			{
				// Token: 0x0400E02C RID: 57388
				public static LocString NAME = UI.FormatAsLink("Jawbo Fillet", "PREHISTORICPACUFILLET");

				// Token: 0x0400E02D RID: 57389
				public static LocString DESC = "An uncooked fillet from a very dead " + CREATURES.SPECIES.PREHISTORICPACU.NAME + ". It has a silky texture.";
			}

			// Token: 0x02003720 RID: 14112
			public class FISHMEAT
			{
				// Token: 0x0400E02E RID: 57390
				public static LocString NAME = UI.FormatAsLink("Pacu Fillet", "FISHMEAT");

				// Token: 0x0400E02F RID: 57391
				public static LocString DESC = "An uncooked fillet from a very dead " + CREATURES.SPECIES.PACU.NAME + ". Yum!";
			}

			// Token: 0x02003721 RID: 14113
			public class MEAT
			{
				// Token: 0x0400E030 RID: 57392
				public static LocString NAME = UI.FormatAsLink("Meat", "MEAT");

				// Token: 0x0400E031 RID: 57393
				public static LocString DESC = "Uncooked meat from a very dead critter. Yum!";
			}

			// Token: 0x02003722 RID: 14114
			public class DINOSAURMEAT
			{
				// Token: 0x0400E032 RID: 57394
				public static LocString NAME = UI.FormatAsLink("Tough Meat", "DINOSAURMEAT");

				// Token: 0x0400E033 RID: 57395
				public static LocString DESC = "Uncooked meat from a very dead critter.\n\nIt's inedible until cooked in the " + BUILDINGS.PREFABS.SMOKER.NAME + ".";
			}

			// Token: 0x02003723 RID: 14115
			public class SMOKEDDINOSAURMEAT
			{
				// Token: 0x0400E034 RID: 57396
				public static LocString NAME = UI.FormatAsLink("Tender Brisket", "SMOKEDDINOSAURMEAT");

				// Token: 0x0400E035 RID: 57397
				public static LocString DESC = "A cooked stack of tough meat that's been marinated and slow-smoked to tender perfection.";

				// Token: 0x0400E036 RID: 57398
				public static LocString RECIPEDESC = "A stack of tender, slow-smoked meat.";
			}

			// Token: 0x02003724 RID: 14116
			public class SMOKEDFISH
			{
				// Token: 0x0400E037 RID: 57399
				public static LocString NAME = UI.FormatAsLink("Smoked Fish", "SMOKEDFISH");

				// Token: 0x0400E038 RID: 57400
				public static LocString DESC = "A buttery smoked fish fillet.\n\nIt flakes nicely when pulled apart with a fork.";

				// Token: 0x0400E039 RID: 57401
				public static LocString RECIPEDESC = "A buttery smoked fish fillet.";
			}

			// Token: 0x02003725 RID: 14117
			public class SMOKEDVEGETABLES
			{
				// Token: 0x0400E03A RID: 57402
				public static LocString NAME = UI.FormatAsLink("Veggie Poppers", "SMOKEDVEGETABLES");

				// Token: 0x0400E03B RID: 57403
				public static LocString DESC = "Crisp vegetables stuffed with herbs and smoked for hours.";

				// Token: 0x0400E03C RID: 57404
				public static LocString RECIPEDESC = "Crisp vegetables stuffed with herbs.";
			}

			// Token: 0x02003726 RID: 14118
			public class PLANTMEAT
			{
				// Token: 0x0400E03D RID: 57405
				public static LocString NAME = UI.FormatAsLink("Plant Meat", "PLANTMEAT");

				// Token: 0x0400E03E RID: 57406
				public static LocString DESC = "Planty plant meat from a plant. How nice!";
			}

			// Token: 0x02003727 RID: 14119
			public class SHELLFISHMEAT
			{
				// Token: 0x0400E03F RID: 57407
				public static LocString NAME = UI.FormatAsLink("Raw Shellfish", "SHELLFISHMEAT");

				// Token: 0x0400E040 RID: 57408
				public static LocString DESC = "An uncooked chunk of very dead " + CREATURES.SPECIES.CRAB.VARIANT_FRESH_WATER.NAME + ". Yum!";
			}

			// Token: 0x02003728 RID: 14120
			public class MUSHROOM
			{
				// Token: 0x0400E041 RID: 57409
				public static LocString NAME = UI.FormatAsLink("Mushroom", "MUSHROOM");

				// Token: 0x0400E042 RID: 57410
				public static LocString DESC = "An edible, flavorless fungus that grew in the dark.";
			}

			// Token: 0x02003729 RID: 14121
			public class COOKEDFISH
			{
				// Token: 0x0400E043 RID: 57411
				public static LocString NAME = UI.FormatAsLink("Cooked Seafood", "COOKEDFISH");

				// Token: 0x0400E044 RID: 57412
				public static LocString DESC = "A cooked piece of freshly caught aquatic critter.\n\nUnsurprisingly, it tastes a bit fishy.";

				// Token: 0x0400E045 RID: 57413
				public static LocString RECIPEDESC = "A cooked piece of freshly caught aquatic critter.";
			}

			// Token: 0x0200372A RID: 14122
			public class COOKEDMEAT
			{
				// Token: 0x0400E046 RID: 57414
				public static LocString NAME = UI.FormatAsLink("Barbeque", "COOKEDMEAT");

				// Token: 0x0400E047 RID: 57415
				public static LocString DESC = "The cooked meat of a defeated critter.\n\nIt has a delightful smoky aftertaste.";

				// Token: 0x0400E048 RID: 57416
				public static LocString RECIPEDESC = "The cooked meat of a defeated critter.";
			}

			// Token: 0x0200372B RID: 14123
			public class FRIESCARROT
			{
				// Token: 0x0400E049 RID: 57417
				public static LocString NAME = UI.FormatAsLink("Squash Fries", "FRIESCARROT");

				// Token: 0x0400E04A RID: 57418
				public static LocString DESC = "Irresistibly crunchy.\n\nBest eaten hot.";

				// Token: 0x0400E04B RID: 57419
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"Crunchy sticks of ",
					UI.FormatAsLink("Plume Squash", "CARROT"),
					" deep-fried in ",
					UI.FormatAsLink("Tallow", "TALLOW"),
					"."
				});
			}

			// Token: 0x0200372C RID: 14124
			public class DEEPFRIEDFISH
			{
				// Token: 0x0400E04C RID: 57420
				public static LocString NAME = UI.FormatAsLink("Fish Taco", "DEEPFRIEDFISH");

				// Token: 0x0400E04D RID: 57421
				public static LocString DESC = "Deep-fried fish cradled in a crunchy fin.";

				// Token: 0x0400E04E RID: 57422
				public static LocString RECIPEDESC = UI.FormatAsLink("Pacu Fillet", "FISHMEAT") + " lightly battered and deep-fried in " + UI.FormatAsLink("Tallow", "TALLOW") + ".";
			}

			// Token: 0x0200372D RID: 14125
			public class DEEPFRIEDSHELLFISH
			{
				// Token: 0x0400E04F RID: 57423
				public static LocString NAME = UI.FormatAsLink("Shellfish Tempura", "DEEPFRIEDSHELLFISH");

				// Token: 0x0400E050 RID: 57424
				public static LocString DESC = "A crispy deep-fried critter claw.";

				// Token: 0x0400E051 RID: 57425
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"A tender chunk of battered ",
					UI.FormatAsLink("Raw Shellfish", "SHELLFISHMEAT"),
					" deep-fried in ",
					UI.FormatAsLink("Tallow", "TALLOW"),
					"."
				});
			}

			// Token: 0x0200372E RID: 14126
			public class DEEPFRIEDMEAT
			{
				// Token: 0x0400E052 RID: 57426
				public static LocString NAME = UI.FormatAsLink("Deep Fried Steak", "DEEPFRIEDMEAT");

				// Token: 0x0400E053 RID: 57427
				public static LocString DESC = "A juicy slab of meat with a crunchy deep-fried upper layer.";

				// Token: 0x0400E054 RID: 57428
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"A juicy slab of ",
					UI.FormatAsLink("Raw Meat", "MEAT"),
					" deep-fried in ",
					UI.FormatAsLink("Tallow", "TALLOW"),
					"."
				});
			}

			// Token: 0x0200372F RID: 14127
			public class DEEPFRIEDNOSH
			{
				// Token: 0x0400E055 RID: 57429
				public static LocString NAME = UI.FormatAsLink("Nosh Noms", "DEEPFRIEDNOSH");

				// Token: 0x0400E056 RID: 57430
				public static LocString DESC = "A snackable handful of crunchy beans.";

				// Token: 0x0400E057 RID: 57431
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"A crunchy stack of ",
					UI.FormatAsLink("Nosh Beans", "BEANPLANTSEED"),
					" deep-fried in ",
					UI.FormatAsLink("Tallow", "TALLOW"),
					"."
				});
			}

			// Token: 0x02003730 RID: 14128
			public class PICKLEDMEAL
			{
				// Token: 0x0400E058 RID: 57432
				public static LocString NAME = UI.FormatAsLink("Pickled Meal", "PICKLEDMEAL");

				// Token: 0x0400E059 RID: 57433
				public static LocString DESC = "Meal Lice preserved in vinegar.\n\nIt's a rarely acquired taste.";

				// Token: 0x0400E05A RID: 57434
				public static LocString RECIPEDESC = ITEMS.FOOD.BASICPLANTFOOD.NAME + " regrettably preserved in vinegar.";
			}

			// Token: 0x02003731 RID: 14129
			public class FRIEDMUSHBAR
			{
				// Token: 0x0400E05B RID: 57435
				public static LocString NAME = UI.FormatAsLink("Mush Fry", "FRIEDMUSHBAR");

				// Token: 0x0400E05C RID: 57436
				public static LocString DESC = "Pan-fried, solidified mudslop.\n\nThe inside is almost completely uncooked, despite the crunch on the outside.";

				// Token: 0x0400E05D RID: 57437
				public static LocString RECIPEDESC = "Pan-fried, solidified mudslop.";
			}

			// Token: 0x02003732 RID: 14130
			public class RAWEGG
			{
				// Token: 0x0400E05E RID: 57438
				public static LocString NAME = UI.FormatAsLink("Raw Egg", "RAWEGG");

				// Token: 0x0400E05F RID: 57439
				public static LocString DESC = "A raw Egg that has been cracked open for use in " + UI.FormatAsLink("Food", "FOOD") + " preparation.\n\nIt will never hatch.";

				// Token: 0x0400E060 RID: 57440
				public static LocString RECIPEDESC = "A raw egg that has been cracked open for use in " + UI.FormatAsLink("Food", "FOOD") + " preparation.";
			}

			// Token: 0x02003733 RID: 14131
			public class COOKEDEGG
			{
				// Token: 0x0400E061 RID: 57441
				public static LocString NAME = UI.FormatAsLink("Omelette", "COOKEDEGG");

				// Token: 0x0400E062 RID: 57442
				public static LocString DESC = "Fluffed and folded Egg innards.\n\nIt turns out you do, in fact, have to break a few eggs to make it.";

				// Token: 0x0400E063 RID: 57443
				public static LocString RECIPEDESC = "Fluffed and folded egg innards.";
			}

			// Token: 0x02003734 RID: 14132
			public class FRIEDMUSHROOM
			{
				// Token: 0x0400E064 RID: 57444
				public static LocString NAME = UI.FormatAsLink("Fried Mushroom", "FRIEDMUSHROOM");

				// Token: 0x0400E065 RID: 57445
				public static LocString DESC = "A pan-fried dish made with a fruiting " + UI.FormatAsLink("Dusk Cap", "MUSHROOM") + ".\n\nIt has a thick, savory flavor with subtle earthy undertones.";

				// Token: 0x0400E066 RID: 57446
				public static LocString RECIPEDESC = "A pan-fried dish made with a fruiting " + UI.FormatAsLink("Dusk Cap", "MUSHROOM") + ".";
			}

			// Token: 0x02003735 RID: 14133
			public class COOKEDPIKEAPPLE
			{
				// Token: 0x0400E067 RID: 57447
				public static LocString NAME = UI.FormatAsLink("Pikeapple Skewer", "COOKEDPIKEAPPLE");

				// Token: 0x0400E068 RID: 57448
				public static LocString DESC = "Grilling a " + UI.FormatAsLink("Pikeapple", "HARDSKINBERRY") + " softens its spikes, making it slighly less awkward to eat.\n\nIt does not diminish the smell.";

				// Token: 0x0400E069 RID: 57449
				public static LocString RECIPEDESC = "A grilled dish made with a fruiting " + UI.FormatAsLink("Pikeapple", "HARDSKINBERRY") + ".";
			}

			// Token: 0x02003736 RID: 14134
			public class PRICKLEFRUIT
			{
				// Token: 0x0400E06A RID: 57450
				public static LocString NAME = UI.FormatAsLink("Bristle Berry", "PRICKLEFRUIT");

				// Token: 0x0400E06B RID: 57451
				public static LocString DESC = "A sweet, mostly pleasant-tasting fruit covered in prickly barbs.";
			}

			// Token: 0x02003737 RID: 14135
			public class GRILLEDPRICKLEFRUIT
			{
				// Token: 0x0400E06C RID: 57452
				public static LocString NAME = UI.FormatAsLink("Gristle Berry", "GRILLEDPRICKLEFRUIT");

				// Token: 0x0400E06D RID: 57453
				public static LocString DESC = "The grilled bud of a " + UI.FormatAsLink("Bristle Berry", "PRICKLEFRUIT") + ".\n\nHeat unlocked an exquisite taste in the fruit, though the burnt spines leave something to be desired.";

				// Token: 0x0400E06E RID: 57454
				public static LocString RECIPEDESC = "The grilled bud of a " + UI.FormatAsLink("Bristle Berry", "PRICKLEFRUIT") + ".";
			}

			// Token: 0x02003738 RID: 14136
			public class SWAMPFRUIT
			{
				// Token: 0x0400E06F RID: 57455
				public static LocString NAME = UI.FormatAsLink("Bog Jelly", "SWAMPFRUIT");

				// Token: 0x0400E070 RID: 57456
				public static LocString DESC = "A fruit with an outer film that contains chewy gelatinous cubes.";
			}

			// Token: 0x02003739 RID: 14137
			public class SWAMPDELIGHTS
			{
				// Token: 0x0400E071 RID: 57457
				public static LocString NAME = UI.FormatAsLink("Swampy Delights", "SWAMPDELIGHTS");

				// Token: 0x0400E072 RID: 57458
				public static LocString DESC = "Dried gelatinous cubes from a " + UI.FormatAsLink("Bog Jelly", "SWAMPFRUIT") + ".\n\nEach cube has a wonderfully chewy texture and is lightly coated in a delicate powder.";

				// Token: 0x0400E073 RID: 57459
				public static LocString RECIPEDESC = "Dried gelatinous cubes from a " + UI.FormatAsLink("Bog Jelly", "SWAMPFRUIT") + ".";
			}

			// Token: 0x0200373A RID: 14138
			public class WORMBASICFRUIT
			{
				// Token: 0x0400E074 RID: 57460
				public static LocString NAME = UI.FormatAsLink("Spindly Grubfruit", "WORMBASICFRUIT");

				// Token: 0x0400E075 RID: 57461
				public static LocString DESC = "A " + UI.FormatAsLink("Grubfruit", "WORMSUPERFRUIT") + " that failed to develop properly.\n\nIt is nonetheless edible, and vaguely tasty.";
			}

			// Token: 0x0200373B RID: 14139
			public class WORMBASICFOOD
			{
				// Token: 0x0400E076 RID: 57462
				public static LocString NAME = UI.FormatAsLink("Roast Grubfruit Nut", "WORMBASICFOOD");

				// Token: 0x0400E077 RID: 57463
				public static LocString DESC = "Slow roasted " + UI.FormatAsLink("Spindly Grubfruit", "WORMBASICFRUIT") + ".\n\nIt has a smoky aroma and tastes of coziness.";

				// Token: 0x0400E078 RID: 57464
				public static LocString RECIPEDESC = "Slow roasted " + UI.FormatAsLink("Spindly Grubfruit", "WORMBASICFRUIT") + ".";
			}

			// Token: 0x0200373C RID: 14140
			public class WORMSUPERFRUIT
			{
				// Token: 0x0400E079 RID: 57465
				public static LocString NAME = UI.FormatAsLink("Grubfruit", "WORMSUPERFRUIT");

				// Token: 0x0400E07A RID: 57466
				public static LocString DESC = "A plump, healthy fruit with a honey-like taste.";
			}

			// Token: 0x0200373D RID: 14141
			public class WORMSUPERFOOD
			{
				// Token: 0x0400E07B RID: 57467
				public static LocString NAME = UI.FormatAsLink("Grubfruit Preserve", "WORMSUPERFOOD");

				// Token: 0x0400E07C RID: 57468
				public static LocString DESC = string.Concat(new string[]
				{
					"A long lasting ",
					UI.FormatAsLink("Grubfruit", "WORMSUPERFRUIT"),
					" jam preserved in ",
					UI.FormatAsLink("Sucrose", "SUCROSE"),
					".\n\nThe thick, goopy jam retains the shape of the jar when poured out, but the sweet taste can't be matched."
				});

				// Token: 0x0400E07D RID: 57469
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"A long lasting ",
					UI.FormatAsLink("Grubfruit", "WORMSUPERFRUIT"),
					" jam preserved in ",
					UI.FormatAsLink("Sucrose", "SUCROSE"),
					"."
				});
			}

			// Token: 0x0200373E RID: 14142
			public class VINEFRUITJAM
			{
				// Token: 0x0400E07E RID: 57470
				public static LocString NAME = UI.FormatAsLink("", "VINEFRUITJAM");

				// Token: 0x0400E07F RID: 57471
				public static LocString DESC = "";

				// Token: 0x0400E080 RID: 57472
				public static LocString RECIPEDESC = "";
			}

			// Token: 0x0200373F RID: 14143
			public class BERRYPIE
			{
				// Token: 0x0400E081 RID: 57473
				public static LocString NAME = UI.FormatAsLink("Mixed Berry Pie", "BERRYPIE");

				// Token: 0x0400E082 RID: 57474
				public static LocString DESC = string.Concat(new string[]
				{
					"A pie made primarily of ",
					UI.FormatAsLink("Grubfruit", "WORMSUPERFRUIT"),
					" and ",
					UI.FormatAsLink("Gristle Berries", "PRICKLEFRUIT"),
					".\n\nThe mixture of berries creates a fragrant, colorful filling that packs a sweet punch."
				});

				// Token: 0x0400E083 RID: 57475
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"A pie made primarily of ",
					UI.FormatAsLink("Grubfruit", "WORMSUPERFRUIT"),
					" and ",
					UI.FormatAsLink("Gristle Berries", "PRICKLEFRUIT"),
					"."
				});

				// Token: 0x02003BBC RID: 15292
				public class DEHYDRATED
				{
					// Token: 0x0400EBD0 RID: 60368
					public static LocString NAME = "Dried Berry Pie";

					// Token: 0x0400EBD1 RID: 60369
					public static LocString DESC = string.Concat(new string[]
					{
						"A dehydrated ",
						UI.FormatAsLink("Mixed Berry Pie", "BERRYPIE"),
						" ration. It must be rehydrated in order to be considered ",
						UI.FormatAsLink("Food", "FOOD"),
						".\n\nDry rations have no expiry date."
					});
				}
			}

			// Token: 0x02003740 RID: 14144
			public class COLDWHEATBREAD
			{
				// Token: 0x0400E084 RID: 57476
				public static LocString NAME = UI.FormatAsLink("Frost Bun", "COLDWHEATBREAD");

				// Token: 0x0400E085 RID: 57477
				public static LocString DESC = "A simple bun baked from " + UI.FormatAsLink("Sleet Wheat Grain", "COLDWHEATSEED") + ".\n\nEach bite leaves a mild cooling sensation in one's mouth, even when the bun itself is warm.";

				// Token: 0x0400E086 RID: 57478
				public static LocString RECIPEDESC = "A simple bun baked from " + UI.FormatAsLink("Sleet Wheat Grain", "COLDWHEATSEED") + " grain.";
			}

			// Token: 0x02003741 RID: 14145
			public class BEAN
			{
				// Token: 0x0400E087 RID: 57479
				public static LocString NAME = UI.FormatAsLink("Nosh Bean", "BEAN");

				// Token: 0x0400E088 RID: 57480
				public static LocString DESC = "The crisp bean of a " + UI.FormatAsLink("Nosh Sprout", "BEAN_PLANT") + ".\n\nEach bite tastes refreshingly natural and wholesome.";
			}

			// Token: 0x02003742 RID: 14146
			public class SPICENUT
			{
				// Token: 0x0400E089 RID: 57481
				public static LocString NAME = UI.FormatAsLink("Pincha Peppernut", "SPICENUT");

				// Token: 0x0400E08A RID: 57482
				public static LocString DESC = "The flavorful nut of a " + UI.FormatAsLink("Pincha Pepperplant", "SPICE_VINE") + ".\n\nThe bitter outer rind hides a rich, peppery core that is useful in cooking.";
			}

			// Token: 0x02003743 RID: 14147
			public class VINEFRUIT
			{
				// Token: 0x0400E08B RID: 57483
				public static LocString NAME = UI.FormatAsLink("Ovagro Fig", "VINEFRUIT");

				// Token: 0x0400E08C RID: 57484
				public static LocString DESC = "These fruit from an " + UI.FormatAsLink("Ovagro Vine", "VINEMOTHER") + ".\n\nIt's fun to squeeze as many as possible in a single mouthful.";
			}

			// Token: 0x02003744 RID: 14148
			public class SPICEBREAD
			{
				// Token: 0x0400E08D RID: 57485
				public static LocString NAME = UI.FormatAsLink("Pepper Bread", "SPICEBREAD");

				// Token: 0x0400E08E RID: 57486
				public static LocString DESC = "A loaf of bread, lightly spiced with " + UI.FormatAsLink("Pincha Peppernut", "SPICENUT") + " for a mild bite.\n\nThere's a simple joy to be had in pulling it apart in one's fingers.";

				// Token: 0x0400E08F RID: 57487
				public static LocString RECIPEDESC = "A loaf of bread, lightly spiced with " + UI.FormatAsLink("Pincha Peppernut", "SPICENUT") + " for a mild bite.";

				// Token: 0x02003BBD RID: 15293
				public class DEHYDRATED
				{
					// Token: 0x0400EBD2 RID: 60370
					public static LocString NAME = "Dried Pepper Bread";

					// Token: 0x0400EBD3 RID: 60371
					public static LocString DESC = string.Concat(new string[]
					{
						"A dehydrated ",
						UI.FormatAsLink("Pepper Bread", "SPICEBREAD"),
						" ration. It must be rehydrated in order to be considered ",
						UI.FormatAsLink("Food", "FOOD"),
						".\n\nDry rations have no expiry date."
					});
				}
			}

			// Token: 0x02003745 RID: 14149
			public class SURFANDTURF
			{
				// Token: 0x0400E090 RID: 57488
				public static LocString NAME = UI.FormatAsLink("Surf'n'Turf", "SURFANDTURF");

				// Token: 0x0400E091 RID: 57489
				public static LocString DESC = string.Concat(new string[]
				{
					"A bit of ",
					UI.FormatAsLink("Meat", "MEAT"),
					" from the land and ",
					UI.FormatAsLink("Cooked Seafood", "COOKEDFISH"),
					" from the sea.\n\nIt's hearty and satisfying."
				});

				// Token: 0x0400E092 RID: 57490
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"A bit of ",
					UI.FormatAsLink("Meat", "MEAT"),
					" from the land and ",
					UI.FormatAsLink("Cooked Seafood", "COOKEDFISH"),
					" from the sea."
				});

				// Token: 0x02003BBE RID: 15294
				public class DEHYDRATED
				{
					// Token: 0x0400EBD4 RID: 60372
					public static LocString NAME = "Dried Surf'n'Turf";

					// Token: 0x0400EBD5 RID: 60373
					public static LocString DESC = string.Concat(new string[]
					{
						"A dehydrated ",
						UI.FormatAsLink("Surf'n'Turf", "SURFANDTURF"),
						" ration. It must be rehydrated in order to be considered ",
						UI.FormatAsLink("Food", "FOOD"),
						".\n\nDry rations have no expiry date."
					});
				}
			}

			// Token: 0x02003746 RID: 14150
			public class TOFU
			{
				// Token: 0x0400E093 RID: 57491
				public static LocString NAME = UI.FormatAsLink("Tofu", "TOFU");

				// Token: 0x0400E094 RID: 57492
				public static LocString DESC = "A bland curd made from " + UI.FormatAsLink("Nosh Beans", "BEANPLANTSEED") + ".\n\nIt has an unusual but pleasant consistency.";

				// Token: 0x0400E095 RID: 57493
				public static LocString RECIPEDESC = "A bland curd made from " + UI.FormatAsLink("Nosh Beans", "BEANPLANTSEED") + ".";
			}

			// Token: 0x02003747 RID: 14151
			public class SPICYTOFU
			{
				// Token: 0x0400E096 RID: 57494
				public static LocString NAME = UI.FormatAsLink("Spicy Tofu", "SPICYTOFU");

				// Token: 0x0400E097 RID: 57495
				public static LocString DESC = ITEMS.FOOD.TOFU.NAME + " marinated in a flavorful " + UI.FormatAsLink("Pincha Peppernut", "SPICENUT") + " sauce.\n\nIt packs a delightful punch.";

				// Token: 0x0400E098 RID: 57496
				public static LocString RECIPEDESC = ITEMS.FOOD.TOFU.NAME + " marinated in a flavorful " + UI.FormatAsLink("Pincha Peppernut", "SPICENUT") + " sauce.";

				// Token: 0x02003BBF RID: 15295
				public class DEHYDRATED
				{
					// Token: 0x0400EBD6 RID: 60374
					public static LocString NAME = "Dried Spicy Tofu";

					// Token: 0x0400EBD7 RID: 60375
					public static LocString DESC = string.Concat(new string[]
					{
						"A dehydrated ",
						UI.FormatAsLink("Spicy Tofu", "SPICYTOFU"),
						" ration. It must be rehydrated in order to be considered ",
						UI.FormatAsLink("Food", "FOOD"),
						".\n\nDry rations have no expiry date."
					});
				}
			}

			// Token: 0x02003748 RID: 14152
			public class CURRY
			{
				// Token: 0x0400E099 RID: 57497
				public static LocString NAME = UI.FormatAsLink("Curried Beans", "CURRY");

				// Token: 0x0400E09A RID: 57498
				public static LocString DESC = string.Concat(new string[]
				{
					"Chewy ",
					UI.FormatAsLink("Nosh Beans", "BEANPLANTSEED"),
					" simmered with chunks of ",
					ITEMS.INGREDIENTS.GINGER.NAME,
					".\n\nIt's so spicy!"
				});

				// Token: 0x0400E09B RID: 57499
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					"Chewy ",
					UI.FormatAsLink("Nosh Beans", "BEANPLANTSEED"),
					" simmered with chunks of ",
					ITEMS.INGREDIENTS.GINGER.NAME,
					"."
				});

				// Token: 0x02003BC0 RID: 15296
				public class DEHYDRATED
				{
					// Token: 0x0400EBD8 RID: 60376
					public static LocString NAME = "Dried Curried Beans";

					// Token: 0x0400EBD9 RID: 60377
					public static LocString DESC = string.Concat(new string[]
					{
						"A dehydrated ",
						UI.FormatAsLink("Curried Beans", "CURRY"),
						" ration. It must be rehydrated in order to be considered ",
						UI.FormatAsLink("Food", "FOOD"),
						".\n\nDry rations have no expiry date."
					});
				}
			}

			// Token: 0x02003749 RID: 14153
			public class SALSA
			{
				// Token: 0x0400E09C RID: 57500
				public static LocString NAME = UI.FormatAsLink("Stuffed Berry", "SALSA");

				// Token: 0x0400E09D RID: 57501
				public static LocString DESC = "A baked " + UI.FormatAsLink("Bristle Berry", "PRICKLEFRUIT") + " stuffed with delectable spices and vibrantly flavored.";

				// Token: 0x0400E09E RID: 57502
				public static LocString RECIPEDESC = "A baked " + UI.FormatAsLink("Bristle Berry", "PRICKLEFRUIT") + " stuffed with delectable spices and vibrantly flavored.";

				// Token: 0x02003BC1 RID: 15297
				public class DEHYDRATED
				{
					// Token: 0x0400EBDA RID: 60378
					public static LocString NAME = "Dried Stuffed Berry";

					// Token: 0x0400EBDB RID: 60379
					public static LocString DESC = string.Concat(new string[]
					{
						"A dehydrated ",
						UI.FormatAsLink("Stuffed Berry", "SALSA"),
						" ration. It must be rehydrated in order to be considered ",
						UI.FormatAsLink("Food", "FOOD"),
						".\n\nDry rations have no expiry date."
					});
				}
			}

			// Token: 0x0200374A RID: 14154
			public class HARDSKINBERRY
			{
				// Token: 0x0400E09F RID: 57503
				public static LocString NAME = UI.FormatAsLink("Pikeapple", "HARDSKINBERRY");

				// Token: 0x0400E0A0 RID: 57504
				public static LocString DESC = "An edible fruit encased in a thorny husk.";
			}

			// Token: 0x0200374B RID: 14155
			public class CARROT
			{
				// Token: 0x0400E0A1 RID: 57505
				public static LocString NAME = UI.FormatAsLink("Plume Squash", "CARROT");

				// Token: 0x0400E0A2 RID: 57506
				public static LocString DESC = "An edible tuber with an earthy, elegant flavor.";
			}

			// Token: 0x0200374C RID: 14156
			public class FERNFOOD
			{
				// Token: 0x0400E0A3 RID: 57507
				public static LocString NAME = UI.FormatAsLink("Megafrond Grain", "FERNFOOD");

				// Token: 0x0400E0A4 RID: 57508
				public static LocString DESC = "An ancient grain that can be processed into " + UI.FormatAsLink("Food", "FOOD") + ".";
			}

			// Token: 0x0200374D RID: 14157
			public class PEMMICAN
			{
				// Token: 0x0400E0A5 RID: 57509
				public static LocString NAME = UI.FormatAsLink("Pemmican", "PEMMICAN");

				// Token: 0x0400E0A6 RID: 57510
				public static LocString DESC = UI.FormatAsLink("Meat", "MEAT") + " and " + UI.FormatAsLink("Tallow", "TALLOW") + " pounded into a calorie-dense brick with an exceptionally long shelf life.\n\nSurvival never tasted so good.";

				// Token: 0x0400E0A7 RID: 57511
				public static LocString RECIPEDESC = UI.FormatAsLink("Meat", "MEAT") + " and " + UI.FormatAsLink("Tallow", "TALLOW") + " pounded into a nutrient-dense brick with an exceptionally long shelf life.";
			}

			// Token: 0x0200374E RID: 14158
			public class BASICPLANTFOOD
			{
				// Token: 0x0400E0A8 RID: 57512
				public static LocString NAME = UI.FormatAsLink("Meal Lice", "BASICPLANTFOOD");

				// Token: 0x0400E0A9 RID: 57513
				public static LocString DESC = "A flavorless grain that almost never wiggles on its own.";
			}

			// Token: 0x0200374F RID: 14159
			public class BASICPLANTBAR
			{
				// Token: 0x0400E0AA RID: 57514
				public static LocString NAME = UI.FormatAsLink("Liceloaf", "BASICPLANTBAR");

				// Token: 0x0400E0AB RID: 57515
				public static LocString DESC = UI.FormatAsLink("Meal Lice", "BASICPLANTFOOD") + " compacted into a dense, immobile loaf.";

				// Token: 0x0400E0AC RID: 57516
				public static LocString RECIPEDESC = UI.FormatAsLink("Meal Lice", "BASICPLANTFOOD") + " compacted into a dense, immobile loaf.";
			}

			// Token: 0x02003750 RID: 14160
			public class BASICFORAGEPLANT
			{
				// Token: 0x0400E0AD RID: 57517
				public static LocString NAME = UI.FormatAsLink("Muckroot", "BASICFORAGEPLANT");

				// Token: 0x0400E0AE RID: 57518
				public static LocString DESC = "A seedless fruit with an upsettingly bland aftertaste.\n\nIt cannot be replanted.\n\nDigging up Buried Objects may uncover a " + ITEMS.FOOD.BASICFORAGEPLANT.NAME + ".";
			}

			// Token: 0x02003751 RID: 14161
			public class FORESTFORAGEPLANT
			{
				// Token: 0x0400E0AF RID: 57519
				public static LocString NAME = UI.FormatAsLink("Hexalent Fruit", "FORESTFORAGEPLANT");

				// Token: 0x0400E0B0 RID: 57520
				public static LocString DESC = "A seedless fruit with an unusual rubbery texture.\n\nIt cannot be replanted.\n\nHexalent fruit is much more calorie dense than Muckroot fruit.";
			}

			// Token: 0x02003752 RID: 14162
			public class SWAMPFORAGEPLANT
			{
				// Token: 0x0400E0B1 RID: 57521
				public static LocString NAME = UI.FormatAsLink("Swamp Chard Heart", "SWAMPFORAGEPLANT");

				// Token: 0x0400E0B2 RID: 57522
				public static LocString DESC = "A seedless plant with a squishy, juicy center and an awful smell.\n\nIt cannot be replanted.";
			}

			// Token: 0x02003753 RID: 14163
			public class ICECAVESFORAGEPLANT
			{
				// Token: 0x0400E0B3 RID: 57523
				public static LocString NAME = UI.FormatAsLink("Sherberry", "ICECAVESFORAGEPLANT");

				// Token: 0x0400E0B4 RID: 57524
				public static LocString DESC = "A cold seedless fruit that triggers mild brain freeze.\n\nIt cannot be replanted.";
			}

			// Token: 0x02003754 RID: 14164
			public class ROTPILE
			{
				// Token: 0x0400E0B5 RID: 57525
				public static LocString NAME = UI.FormatAsLink("Rot Pile", "COMPOST");

				// Token: 0x0400E0B6 RID: 57526
				public static LocString DESC = string.Concat(new string[]
				{
					"An inedible glop of former foodstuff.\n\n",
					ITEMS.FOOD.ROTPILE.NAME,
					"s break down into ",
					UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
					" over time."
				});
			}

			// Token: 0x02003755 RID: 14165
			public class COLDWHEATSEED
			{
				// Token: 0x0400E0B7 RID: 57527
				public static LocString NAME = UI.FormatAsLink("Sleet Wheat Grain", "COLDWHEATSEED");

				// Token: 0x0400E0B8 RID: 57528
				public static LocString DESC = "An edible grain that leaves a cool taste on the tongue.";
			}

			// Token: 0x02003756 RID: 14166
			public class BEANPLANTSEED
			{
				// Token: 0x0400E0B9 RID: 57529
				public static LocString NAME = UI.FormatAsLink("Nosh Bean", "BEANPLANTSEED");

				// Token: 0x0400E0BA RID: 57530
				public static LocString DESC = "An inedible bean that can be processed into delicious foods.";
			}

			// Token: 0x02003757 RID: 14167
			public class QUICHE
			{
				// Token: 0x0400E0BB RID: 57531
				public static LocString NAME = UI.FormatAsLink("Mushroom Quiche", "QUICHE");

				// Token: 0x0400E0BC RID: 57532
				public static LocString DESC = string.Concat(new string[]
				{
					UI.FormatAsLink("Omelette", "COOKEDEGG"),
					", ",
					UI.FormatAsLink("Fried Mushroom", "FRIEDMUSHROOM"),
					" and ",
					UI.FormatAsLink("Lettuce", "LETTUCE"),
					" piled onto a yummy crust.\n\nSomehow, it's both soggy <i>and</i> crispy."
				});

				// Token: 0x0400E0BD RID: 57533
				public static LocString RECIPEDESC = string.Concat(new string[]
				{
					UI.FormatAsLink("Omelette", "COOKEDEGG"),
					", ",
					UI.FormatAsLink("Fried Mushroom", "FRIEDMUSHROOM"),
					" and ",
					UI.FormatAsLink("Lettuce", "LETTUCE"),
					" piled onto a yummy crust."
				});

				// Token: 0x02003BC2 RID: 15298
				public class DEHYDRATED
				{
					// Token: 0x0400EBDC RID: 60380
					public static LocString NAME = "Dried Mushroom Quiche";

					// Token: 0x0400EBDD RID: 60381
					public static LocString DESC = string.Concat(new string[]
					{
						"A dehydrated ",
						UI.FormatAsLink("Mushroom Quiche", "QUICHE"),
						" ration. It must be rehydrated in order to be considered ",
						UI.FormatAsLink("Food", "FOOD"),
						".\n\nDry rations have no expiry date."
					});
				}
			}

			// Token: 0x02003758 RID: 14168
			public class GARDENFOODPLANTFOOD
			{
				// Token: 0x0400E0BE RID: 57534
				public static LocString NAME = UI.FormatAsLink("Sweatcorn", "GARDENFOODPLANTFOOD");

				// Token: 0x0400E0BF RID: 57535
				public static LocString DESC = string.Concat(new string[]
				{
					"The sugary fruit of a ",
					UI.FormatAsLink("Sweatcorn Stalk", "GARDENFOODPLANT"),
					"\n\nSweatcorn is more calorie-dense than ",
					UI.FormatAsLink("Snac Fruit", "GARDENFORAGEPLANT"),
					"."
				});
			}

			// Token: 0x02003759 RID: 14169
			public class GARDENFORAGEPLANT
			{
				// Token: 0x0400E0C0 RID: 57536
				public static LocString NAME = UI.FormatAsLink("Snac Fruit", "GARDENFORAGEPLANT");

				// Token: 0x0400E0C1 RID: 57537
				public static LocString DESC = "A seedless fruit that loses its flavor long before it is fully chewed.\n\nIt cannot be replanted.\n\nDigging up Buried Objects may uncover a " + ITEMS.FOOD.GARDENFORAGEPLANT.NAME + ".";
			}

			// Token: 0x0200375A RID: 14170
			public class BUTTERFLYPLANTSEED
			{
				// Token: 0x0400E0C2 RID: 57538
				public static LocString NAME = UI.FormatAsLink("Mimillet", "BUTTERFLYPLANT");

				// Token: 0x0400E0C3 RID: 57539
				public static LocString DESC = string.Concat(new string[]
				{
					"An inedible seed from a ",
					UI.FormatAsLink("Mimika Bud", "BUTTERFLYPLANT"),
					".\n\nIt can be sown to cultivate more plants, or processed into ",
					UI.FormatAsLink("Food", "FOOD"),
					".\n\nDigging up Buried Objects may uncover a Mimillet Seed."
				});

				// Token: 0x0400E0C4 RID: 57540
				public static LocString RECIPEDESC = "An inedible " + UI.FormatAsLink("Mimillet", "BUTTERFLYPLANT") + " seed.";
			}

			// Token: 0x0200375B RID: 14171
			public class BUTTERFLYFOOD
			{
				// Token: 0x0400E0C5 RID: 57541
				public static LocString NAME = UI.FormatAsLink("Toasted Mimillet", "BUTTERFLYFOOD");

				// Token: 0x0400E0C6 RID: 57542
				public static LocString DESC = "A lightly toasted " + UI.FormatAsLink("Mimillet", "BUTTERFLYPLANT") + ".\n\nIt makes the tummy feel a bit fluttery.";

				// Token: 0x0400E0C7 RID: 57543
				public static LocString RECIPEDESC = "A lightly toasted " + UI.FormatAsLink("Mimillet", "BUTTERFLYPLANT") + ".";
			}
		}

		// Token: 0x0200242D RID: 9261
		public class INGREDIENTS
		{
			// Token: 0x0200375C RID: 14172
			public class SWAMPLILYFLOWER
			{
				// Token: 0x0400E0C8 RID: 57544
				public static LocString NAME = UI.FormatAsLink("Balm Lily Flower", "SWAMPLILYFLOWER");

				// Token: 0x0400E0C9 RID: 57545
				public static LocString DESC = "A medicinal flower that soothes most minor maladies.\n\nIt is exceptionally fragrant.";
			}

			// Token: 0x0200375D RID: 14173
			public class GINGER
			{
				// Token: 0x0400E0CA RID: 57546
				public static LocString NAME = UI.FormatAsLink("Tonic Root", "GINGERCONFIG");

				// Token: 0x0400E0CB RID: 57547
				public static LocString DESC = "A chewy, fibrous rhizome with a fiery aftertaste.";
			}

			// Token: 0x0200375E RID: 14174
			public class KELP
			{
				// Token: 0x0400E0CC RID: 57548
				public static LocString NAME = UI.FormatAsLink("Seakomb Leaf", "KELP");

				// Token: 0x0400E0CD RID: 57549
				public static LocString DESC = string.Concat(new string[]
				{
					"The leaf of a ",
					UI.FormatAsLink("Seakomb", "KELPPLANT"),
					".\n\nIt can be processed into ",
					UI.FormatAsLink("Phyto Oil", "PHYTOOIL"),
					" or used as an ingredient in ",
					UI.FormatAsLink("Allergy Medication", "ANTIHISTAMINE "),
					"."
				});
			}
		}

		// Token: 0x0200242E RID: 9262
		public class INDUSTRIAL_PRODUCTS
		{
			// Token: 0x0200375F RID: 14175
			public class ELECTROBANK_URANIUM_ORE
			{
				// Token: 0x0400E0CE RID: 57550
				public static LocString NAME = UI.FormatAsLink("Uranium Ore Power Bank", "ELECTROBANK_URANIUM_ORE");

				// Token: 0x0400E0CF RID: 57551
				public static LocString DESC = string.Concat(new string[]
				{
					"A disposable ",
					UI.FormatAsLink("Power Bank", "ELECTROBANK"),
					" made with ",
					UI.FormatAsLink("Uranium Ore", "URANIUMORE"),
					".\n\nIt can power buildings via ",
					UI.FormatAsLink("Large Dischargers", "LARGEELECTROBANKDISCHARGER"),
					" or ",
					UI.FormatAsLink("Compact Dischargers", "SMALLELECTROBANKDISCHARGER"),
					".\n\nDuplicants can produce new ",
					UI.FormatAsLink("Uranium Ore Power Banks", "ELECTROBANK"),
					" at the ",
					UI.FormatAsLink("Crafting Station", "CRAFTINGTABLE"),
					".\n\nMust be kept dry."
				});
			}

			// Token: 0x02003760 RID: 14176
			public class ELECTROBANK_METAL_ORE
			{
				// Token: 0x0400E0D0 RID: 57552
				public static LocString NAME = UI.FormatAsLink("Metal Power Bank", "ELECTROBANK_METAL_ORE");

				// Token: 0x0400E0D1 RID: 57553
				public static LocString DESC = string.Concat(new string[]
				{
					"A disposable ",
					UI.FormatAsLink("Power Bank", "ELECTROBANK"),
					" made with ",
					UI.FormatAsLink("Metal Ore", "METAL"),
					".\n\nIt can power buildings via ",
					UI.FormatAsLink("Large Dischargers", "LARGEELECTROBANKDISCHARGER"),
					" or ",
					UI.FormatAsLink("Compact Dischargers", "SMALLELECTROBANKDISCHARGER"),
					".\n\nDuplicants can produce new ",
					UI.FormatAsLink("Metal Power Banks", "ELECTROBANK"),
					" at the ",
					UI.FormatAsLink("Crafting Station", "CRAFTINGTABLE"),
					".\n\nMust be kept dry."
				});
			}

			// Token: 0x02003761 RID: 14177
			public class ELECTROBANK_SELFCHARGING
			{
				// Token: 0x0400E0D2 RID: 57554
				public static LocString NAME = UI.FormatAsLink("Atomic Power Bank", "ELECTROBANK_SELFCHARGING");

				// Token: 0x0400E0D3 RID: 57555
				public static LocString DESC = string.Concat(new string[]
				{
					"A self-charging ",
					UI.FormatAsLink("Power Bank", "ELECTROBANK"),
					" made with ",
					ELEMENTS.ENRICHEDURANIUM.NAME,
					".\n\nIt can power buildings via ",
					UI.FormatAsLink("Large Dischargers", "LARGEELECTROBANKDISCHARGER"),
					" or ",
					UI.FormatAsLink("Compact Dischargers", "SMALLELECTROBANKDISCHARGER"),
					".\n\nIts low ",
					UI.FormatAsLink("wattage", "POWER"),
					" and high ",
					UI.FormatAsLink("Radioactivity", "RADIATION"),
					" make it unsuitable for Bionic Duplicant use."
				});
			}

			// Token: 0x02003762 RID: 14178
			public class ELECTROBANK
			{
				// Token: 0x0400E0D4 RID: 57556
				public static LocString NAME = UI.FormatAsLink("Eco Power Bank", "ELECTROBANK");

				// Token: 0x0400E0D5 RID: 57557
				public static LocString DESC = string.Concat(new string[]
				{
					"A rechargeable ",
					UI.FormatAsLink("Power Bank", "ELECTROBANK"),
					".\n\nIt can power buildings via ",
					UI.FormatAsLink("Large Dischargers", "LARGEELECTROBANKDISCHARGER"),
					" or ",
					UI.FormatAsLink("Compact Dischargers", "SMALLELECTROBANKDISCHARGER"),
					".\n\nDuplicants can produce new ",
					UI.FormatAsLink("Eco Power Banks", "ELECTROBANK"),
					" at the ",
					UI.FormatAsLink("Soldering Station", "ADVANCEDCRAFTINGTABLE"),
					".\n\nMust be kept dry."
				});
			}

			// Token: 0x02003763 RID: 14179
			public class ELECTROBANK_EMPTY
			{
				// Token: 0x0400E0D6 RID: 57558
				public static LocString NAME = UI.FormatAsLink("Empty Eco Power Bank", "ELECTROBANK");

				// Token: 0x0400E0D7 RID: 57559
				public static LocString DESC = string.Concat(new string[]
				{
					"A depleted ",
					UI.FormatAsLink("Power Bank", "ELECTROBANK"),
					".\n\nIt must be recharged at a ",
					UI.FormatAsLink("Power Bank Charger", "ELECTROBANKCHARGER"),
					" before it can be reused."
				});
			}

			// Token: 0x02003764 RID: 14180
			public class ELECTROBANK_GARBAGE
			{
				// Token: 0x0400E0D8 RID: 57560
				public static LocString NAME = UI.FormatAsLink("Power Bank Scrap", "ELECTROBANK");

				// Token: 0x0400E0D9 RID: 57561
				public static LocString DESC = string.Concat(new string[]
				{
					"A ",
					UI.FormatAsLink("Power Bank", "ELECTROBANK"),
					" that has reached the end of its lifetime.\n\nIt can be salvaged for ",
					UI.FormatAsLink("Abyssalite", "KATAIRITE"),
					" at the ",
					UI.FormatAsLink("Rock Crusher", "ROCKCRUSHER"),
					"."
				});
			}

			// Token: 0x02003765 RID: 14181
			public class FUEL_BRICK
			{
				// Token: 0x0400E0DA RID: 57562
				public static LocString NAME = "Fuel Brick";

				// Token: 0x0400E0DB RID: 57563
				public static LocString DESC = "A densely compressed brick of combustible material.\n\nIt can be burned to produce a one-time burst of " + UI.FormatAsLink("Power", "POWER") + ".";
			}

			// Token: 0x02003766 RID: 14182
			public class BASIC_FABRIC
			{
				// Token: 0x0400E0DC RID: 57564
				public static LocString NAME = UI.FormatAsLink("Reed Fiber", "BASIC_FABRIC");

				// Token: 0x0400E0DD RID: 57565
				public static LocString DESC = "A ball of raw cellulose used in the production of " + UI.FormatAsLink("Clothing", "EQUIPMENT") + " and textiles.";
			}

			// Token: 0x02003767 RID: 14183
			public class FEATHER_FABRIC
			{
				// Token: 0x0400E0DE RID: 57566
				public static LocString NAME = UI.FormatAsLink("Feather Fiber", "FEATHER_FABRIC");

				// Token: 0x0400E0DF RID: 57567
				public static LocString DESC = "A stalk of raw keratin used in the production of " + UI.FormatAsLink("Clothing", "EQUIPMENT") + " and textiles.";
			}

			// Token: 0x02003768 RID: 14184
			public class DEWDRIP
			{
				// Token: 0x0400E0E0 RID: 57568
				public static LocString NAME = UI.FormatAsLink("Dewdrip", "DEWDRIP");

				// Token: 0x0400E0E1 RID: 57569
				public static LocString DESC = string.Concat(new string[]
				{
					"A crystallized blob of ",
					UI.FormatAsLink("Brackene", "MILK"),
					" from the ",
					UI.FormatAsLink("Dew Dripper", "DEWDRIPPERPLANT"),
					"."
				});
			}

			// Token: 0x02003769 RID: 14185
			public class TRAP_PARTS
			{
				// Token: 0x0400E0E2 RID: 57570
				public static LocString NAME = "Trap Components";

				// Token: 0x0400E0E3 RID: 57571
				public static LocString DESC = string.Concat(new string[]
				{
					"These components can be assembled into a ",
					BUILDINGS.PREFABS.CREATURETRAP.NAME,
					" and used to catch ",
					UI.FormatAsLink("Critters", "CREATURES"),
					"."
				});
			}

			// Token: 0x0200376A RID: 14186
			public class POWER_STATION_TOOLS
			{
				// Token: 0x0400E0E4 RID: 57572
				public static LocString NAME = UI.FormatAsLink("Microchip", "POWER_STATION_TOOLS");

				// Token: 0x0400E0E5 RID: 57573
				public static LocString DESC = string.Concat(new string[]
				{
					"A specialized ",
					ITEMS.INDUSTRIAL_PRODUCTS.POWER_STATION_TOOLS.NAME,
					" created by a professional engineer.\n\nTunes up ",
					UI.FormatAsLink("Generators", "REQUIREMENTCLASSGENERATORTYPE"),
					" to increase their ",
					UI.FormatAsLink("Power", "POWER"),
					" output.\n\nAlso used in the production of ",
					UI.FormatAsLink("Boosters", "BOOSTER"),
					" for Bionic Duplicants."
				});

				// Token: 0x0400E0E6 RID: 57574
				public static LocString TINKER_REQUIREMENT_NAME = "Skill: " + DUPLICANTS.ROLES.POWER_TECHNICIAN.NAME;

				// Token: 0x0400E0E7 RID: 57575
				public static LocString TINKER_REQUIREMENT_TOOLTIP = string.Concat(new string[]
				{
					"Can only be used by a Duplicant with ",
					DUPLICANTS.ROLES.POWER_TECHNICIAN.NAME,
					" to apply a ",
					UI.PRE_KEYWORD,
					"Tune Up",
					UI.PST_KEYWORD,
					"."
				});

				// Token: 0x0400E0E8 RID: 57576
				public static LocString TINKER_EFFECT_NAME = "Engie's Tune-Up: {0} {1}";

				// Token: 0x0400E0E9 RID: 57577
				public static LocString TINKER_EFFECT_TOOLTIP = string.Concat(new string[]
				{
					"Can be used to ",
					UI.PRE_KEYWORD,
					"Tune Up",
					UI.PST_KEYWORD,
					" a generator, increasing its {0} by <b>{1}</b>."
				});

				// Token: 0x0400E0EA RID: 57578
				public static LocString RECIPE_DESCRIPTION = "Make " + ITEMS.INDUSTRIAL_PRODUCTS.POWER_STATION_TOOLS.NAME + " from {0}";
			}

			// Token: 0x0200376B RID: 14187
			public class FARM_STATION_TOOLS
			{
				// Token: 0x0400E0EB RID: 57579
				public static LocString NAME = UI.FormatAsLink("Micronutrient Fertilizer", "FARM_STATION_TOOLS");

				// Token: 0x0400E0EC RID: 57580
				public static LocString DESC = string.Concat(new string[]
				{
					"Specialized ",
					UI.FormatAsLink("Fertilizer", "FERTILIZER"),
					" mixed by a Duplicant with the ",
					DUPLICANTS.ROLES.FARMER.NAME,
					" Skill.\n\nIncreases the ",
					UI.PRE_KEYWORD,
					"Growth Rate",
					UI.PST_KEYWORD,
					" of one ",
					UI.FormatAsLink("Plant", "PLANTS"),
					"."
				});
			}

			// Token: 0x0200376C RID: 14188
			public class MACHINE_PARTS
			{
				// Token: 0x0400E0ED RID: 57581
				public static LocString NAME = "Custom Parts";

				// Token: 0x0400E0EE RID: 57582
				public static LocString DESC = string.Concat(new string[]
				{
					"Specialized Parts crafted by a professional engineer.\n\n",
					UI.PRE_KEYWORD,
					"Jerry Rig",
					UI.PST_KEYWORD,
					" machine buildings to increase their efficiency."
				});

				// Token: 0x0400E0EF RID: 57583
				public static LocString TINKER_REQUIREMENT_NAME = "Job: " + DUPLICANTS.ROLES.MECHATRONIC_ENGINEER.NAME;

				// Token: 0x0400E0F0 RID: 57584
				public static LocString TINKER_REQUIREMENT_TOOLTIP = string.Concat(new string[]
				{
					"Can only be used by a Duplicant with ",
					DUPLICANTS.ROLES.MECHATRONIC_ENGINEER.NAME,
					" to apply a ",
					UI.PRE_KEYWORD,
					"Jerry Rig",
					UI.PST_KEYWORD,
					"."
				});

				// Token: 0x0400E0F1 RID: 57585
				public static LocString TINKER_EFFECT_NAME = "Engineer's Jerry Rig: {0} {1}";

				// Token: 0x0400E0F2 RID: 57586
				public static LocString TINKER_EFFECT_TOOLTIP = string.Concat(new string[]
				{
					"Can be used to ",
					UI.PRE_KEYWORD,
					"Jerry Rig",
					UI.PST_KEYWORD,
					" upgrades to a machine building, increasing its {0} by <b>{1}</b>."
				});
			}

			// Token: 0x0200376D RID: 14189
			public class RESEARCH_DATABANK
			{
				// Token: 0x0400E0F3 RID: 57587
				public static LocString NAME = UI.FormatAsLink("Data Bank", "DATABANK");

				// Token: 0x0400E0F4 RID: 57588
				public static LocString NAME_PLURAL = UI.FormatAsLink("Data Banks", "DATABANK");

				// Token: 0x0400E0F5 RID: 57589
				public static LocString DESC = "Raw data that can be processed into " + UI.FormatAsLink("Interstellar Research", "RESEARCH") + " points.";
			}

			// Token: 0x0200376E RID: 14190
			public class ORBITAL_RESEARCH_DATABANK
			{
				// Token: 0x0400E0F6 RID: 57590
				public static LocString NAME = UI.FormatAsLink("Data Bank", "DATABANK");

				// Token: 0x0400E0F7 RID: 57591
				public static LocString NAME_PLURAL = UI.FormatAsLink("Data Banks", "DATABANK");

				// Token: 0x0400E0F8 RID: 57592
				public static LocString DESC = "Raw Data that can be processed into " + UI.FormatAsLink("Data Analysis Research", "RESEARCHDLC1") + " points.";

				// Token: 0x0400E0F9 RID: 57593
				public static LocString RECIPE_DESC = string.Concat(new string[]
				{
					"Data Banks of raw data generated from exploring, either by exploring new areas with Duplicants, or by using an ",
					UI.FormatAsLink("Orbital Data Collection Lab", "ORBITALRESEARCHCENTER"),
					".\n\nUsed by the ",
					UI.FormatAsLink("Virtual Planetarium", "DLC1COSMICRESEARCHCENTER"),
					" to conduct research."
				});
			}

			// Token: 0x0200376F RID: 14191
			public class EGG_SHELL
			{
				// Token: 0x0400E0FA RID: 57594
				public static LocString NAME = UI.FormatAsLink("Egg Shell", "EGG_SHELL");

				// Token: 0x0400E0FB RID: 57595
				public static LocString DESC = "Can be crushed to produce " + UI.FormatAsLink("Lime", "LIME") + ".";
			}

			// Token: 0x02003770 RID: 14192
			public class GOLD_BELLY_CROWN
			{
				// Token: 0x0400E0FC RID: 57596
				public static LocString NAME = UI.FormatAsLink("Regal Bammoth Crest", "GOLD_BELLY_CROWN");

				// Token: 0x0400E0FD RID: 57597
				public static LocString DESC = "Can be crushed to produce " + ELEMENTS.GOLDAMALGAM.NAME + ".";
			}

			// Token: 0x02003771 RID: 14193
			public class CRAB_SHELL
			{
				// Token: 0x0400E0FE RID: 57598
				public static LocString NAME = UI.FormatAsLink("Pokeshell Molt", "CRAB_SHELL");

				// Token: 0x0400E0FF RID: 57599
				public static LocString DESC = "Can be crushed to produce " + UI.FormatAsLink("Lime", "LIME") + ".";

				// Token: 0x02003BC3 RID: 15299
				public class VARIANT_WOOD
				{
					// Token: 0x0400EBDE RID: 60382
					public static LocString NAME = UI.FormatAsLink("Oakshell Molt", "CRABWOODSHELL");

					// Token: 0x0400EBDF RID: 60383
					public static LocString DESC = "Can be crushed to produce " + UI.FormatAsLink("Wood", "WOOD") + ".";
				}
			}

			// Token: 0x02003772 RID: 14194
			public class BABY_CRAB_SHELL
			{
				// Token: 0x0400E100 RID: 57600
				public static LocString NAME = UI.FormatAsLink("Small Pokeshell Molt", "CRAB_SHELL");

				// Token: 0x0400E101 RID: 57601
				public static LocString DESC = "Can be crushed to produce " + UI.FormatAsLink("Lime", "LIME") + ".";

				// Token: 0x02003BC4 RID: 15300
				public class VARIANT_WOOD
				{
					// Token: 0x0400EBE0 RID: 60384
					public static LocString NAME = UI.FormatAsLink("Small Oakshell Molt", "CRABWOODSHELL");

					// Token: 0x0400EBE1 RID: 60385
					public static LocString DESC = "Can be crushed to produce " + UI.FormatAsLink("Wood", "WOOD") + ".";
				}
			}

			// Token: 0x02003773 RID: 14195
			public class WOOD
			{
				// Token: 0x0400E102 RID: 57602
				public static LocString NAME = UI.FormatAsLink("Wood", "WOOD");

				// Token: 0x0400E103 RID: 57603
				public static LocString DESC = string.Concat(new string[]
				{
					"Natural resource harvested from certain ",
					UI.FormatAsLink("Critters", "CREATURES"),
					" and ",
					UI.FormatAsLink("Plants", "PLANTS"),
					".\n\nUsed in construction or ",
					UI.FormatAsLink("Heat", "HEAT"),
					" production."
				});
			}

			// Token: 0x02003774 RID: 14196
			public class GENE_SHUFFLER_RECHARGE
			{
				// Token: 0x0400E104 RID: 57604
				public static LocString NAME = "Vacillator Recharge";

				// Token: 0x0400E105 RID: 57605
				public static LocString DESC = "Replenishes one charge to a depleted " + BUILDINGS.PREFABS.GENESHUFFLER.NAME + ".";
			}

			// Token: 0x02003775 RID: 14197
			public class TABLE_SALT
			{
				// Token: 0x0400E106 RID: 57606
				public static LocString NAME = "Table Salt";

				// Token: 0x0400E107 RID: 57607
				public static LocString DESC = string.Concat(new string[]
				{
					"A seasoning that Duplicants can add to their ",
					UI.FormatAsLink("Food", "FOOD"),
					" to boost ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nDuplicants will automatically use Table Salt while sitting at a ",
					BUILDINGS.PREFABS.DININGTABLE.NAME,
					" during mealtime.\n\n<i>Only the finest grains are chosen.</i>"
				});
			}

			// Token: 0x02003776 RID: 14198
			public class REFINED_SUGAR
			{
				// Token: 0x0400E108 RID: 57608
				public static LocString NAME = "Refined Sugar";

				// Token: 0x0400E109 RID: 57609
				public static LocString DESC = string.Concat(new string[]
				{
					"A seasoning that Duplicants can add to their ",
					UI.FormatAsLink("Food", "FOOD"),
					" to boost ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nDuplicants will automatically use Refined Sugar while sitting at a ",
					BUILDINGS.PREFABS.DININGTABLE.NAME,
					" during mealtime.\n\n<i>Only the finest grains are chosen.</i>"
				});
			}

			// Token: 0x02003777 RID: 14199
			public class ICE_BELLY_POOP
			{
				// Token: 0x0400E10A RID: 57610
				public static LocString NAME = UI.FormatAsLink("Bammoth Patty", "ICE_BELLY_POOP");

				// Token: 0x0400E10B RID: 57611
				public static LocString DESC = string.Concat(new string[]
				{
					"A little treat left behind by a very large critter.\n\nIt can be crushed to extract ",
					UI.FormatAsLink("Phosphorite", "PHOSPHORITE"),
					" and ",
					UI.FormatAsLink("Clay", "CLAY"),
					"."
				});
			}
		}

		// Token: 0x0200242F RID: 9263
		public class CARGO_CAPSULE
		{
			// Token: 0x0400A414 RID: 42004
			public static LocString NAME = "Care Package";

			// Token: 0x0400A415 RID: 42005
			public static LocString DESC = "A delivery system for recently printed resources.\n\nIt will dematerialize shortly.";
		}

		// Token: 0x02002430 RID: 9264
		public class RAILGUNPAYLOAD
		{
			// Token: 0x0400A416 RID: 42006
			public static LocString NAME = UI.FormatAsLink("Interplanetary Payload", "RAILGUNPAYLOAD");

			// Token: 0x0400A417 RID: 42007
			public static LocString DESC = string.Concat(new string[]
			{
				"Contains resources packed for interstellar shipping.\n\nCan be launched by a ",
				BUILDINGS.PREFABS.RAILGUN.NAME,
				" or unpacked with a ",
				BUILDINGS.PREFABS.RAILGUNPAYLOADOPENER.NAME,
				"."
			});
		}

		// Token: 0x02002431 RID: 9265
		public class MISSILE_BASIC
		{
			// Token: 0x0400A418 RID: 42008
			public static LocString NAME = UI.FormatAsLink("Blastshot", "MISSILELAUNCHER");

			// Token: 0x0400A419 RID: 42009
			public static LocString DESC = "An explosive projectile designed to defend against meteor showers.\n\nMust be launched by a " + UI.FormatAsLink("Meteor Blaster", "MISSILELAUNCHER") + ".";
		}

		// Token: 0x02002432 RID: 9266
		public class MISSILE_LONGRANGE_VANILLADLC4
		{
			// Token: 0x0400A41A RID: 42010
			public static LocString NAME = UI.FormatAsLink("Intracosmic Blastshot", "MISSILELAUNCHER");

			// Token: 0x0400A41B RID: 42011
			public static LocString DESC = "A long-range explosive projectile that defends against distant space objects.\n\nMust be launched by " + UI.FormatAsLink("Meteor Blaster", "MISSILELAUNCHER") + ".";
		}

		// Token: 0x02002433 RID: 9267
		public class MISSILE_LONGRANGE
		{
			// Token: 0x0400A41C RID: 42012
			public static LocString NAME = UI.FormatAsLink("Intracosmic Blastshot", "MISSILELAUNCHER");

			// Token: 0x0400A41D RID: 42013
			public static LocString DESC = "A long-range explosive projectile that defends against distant space objects.\n\nMust be launched by " + UI.FormatAsLink("Meteor Blaster", "MISSILELAUNCHER") + ".";
		}

		// Token: 0x02002434 RID: 9268
		public class DEBRISPAYLOAD
		{
			// Token: 0x0400A41E RID: 42014
			public static LocString NAME = "Rocket Debris";

			// Token: 0x0400A41F RID: 42015
			public static LocString DESC = "Whatever is left over from a Rocket Self-Destruct can be recovered once it has crash-landed.";
		}

		// Token: 0x02002435 RID: 9269
		public class RADIATION
		{
			// Token: 0x02003778 RID: 14200
			public class HIGHENERGYPARITCLE
			{
				// Token: 0x0400E10C RID: 57612
				public static LocString NAME = "Radbolts";

				// Token: 0x0400E10D RID: 57613
				public static LocString DESC = string.Concat(new string[]
				{
					"A concentrated field of ",
					UI.FormatAsKeyWord("Radbolts"),
					" that can be largely redirected using a ",
					UI.FormatAsLink("Radbolt Reflector", "HIGHENERGYPARTICLEREDIRECTOR"),
					"."
				});
			}
		}

		// Token: 0x02002436 RID: 9270
		public class DREAMJOURNAL
		{
			// Token: 0x0400A420 RID: 42016
			public static LocString NAME = "Dream Journal";

			// Token: 0x0400A421 RID: 42017
			public static LocString DESC = string.Concat(new string[]
			{
				"A hand-scrawled account of ",
				UI.FormatAsLink("Pajama", "SLEEP_CLINIC_PAJAMAS"),
				"-induced dreams.\n\nCan be analyzed using a ",
				UI.FormatAsLink("Somnium Synthesizer", "MEGABRAINTANK"),
				"."
			});
		}

		// Token: 0x02002437 RID: 9271
		public class DEHYDRATEDFOODPACKAGE
		{
			// Token: 0x0400A422 RID: 42018
			public static LocString NAME = "Dry Ration";

			// Token: 0x0400A423 RID: 42019
			public static LocString DESC = "A package of non-perishable dehydrated food.\n\nIt requires no refrigeration, but must be rehydrated before consumption.";

			// Token: 0x0400A424 RID: 42020
			public static LocString CONSUMED = "Ate Rehydrated Food";

			// Token: 0x0400A425 RID: 42021
			public static LocString CONTENTS = "Dried {0}";
		}

		// Token: 0x02002438 RID: 9272
		public class SPICES
		{
			// Token: 0x02003779 RID: 14201
			public class MACHINERY_SPICE
			{
				// Token: 0x0400E10E RID: 57614
				public static LocString NAME = UI.FormatAsLink("Machinist Spice", "MACHINERY_SPICE");

				// Token: 0x0400E10F RID: 57615
				public static LocString DESC = "Improves operating skills when ingested.";
			}

			// Token: 0x0200377A RID: 14202
			public class PILOTING_SPICE
			{
				// Token: 0x0400E110 RID: 57616
				public static LocString NAME = UI.FormatAsLink("Rocketeer Spice", "PILOTING_SPICE");

				// Token: 0x0400E111 RID: 57617
				public static LocString DESC = "Provides a boost to piloting abilities.";
			}

			// Token: 0x0200377B RID: 14203
			public class PRESERVING_SPICE
			{
				// Token: 0x0400E112 RID: 57618
				public static LocString NAME = UI.FormatAsLink("Freshener Spice", "PRESERVING_SPICE");

				// Token: 0x0400E113 RID: 57619
				public static LocString DESC = "Slows the decomposition of perishable foods.";
			}

			// Token: 0x0200377C RID: 14204
			public class STRENGTH_SPICE
			{
				// Token: 0x0400E114 RID: 57620
				public static LocString NAME = UI.FormatAsLink("Brawny Spice", "STRENGTH_SPICE");

				// Token: 0x0400E115 RID: 57621
				public static LocString DESC = "Strengthens even the weakest of muscles.";
			}
		}
	}
}
