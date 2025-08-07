using System;

namespace STRINGS
{
	// Token: 0x02000FAB RID: 4011
	public class RESEARCH
	{
		// Token: 0x02002412 RID: 9234
		public class MESSAGING
		{
			// Token: 0x0400A3C6 RID: 41926
			public static LocString NORESEARCHSELECTED = "No research selected";

			// Token: 0x0400A3C7 RID: 41927
			public static LocString RESEARCHTYPEREQUIRED = "{0} required";

			// Token: 0x0400A3C8 RID: 41928
			public static LocString RESEARCHTYPEALSOREQUIRED = "{0} also required";

			// Token: 0x0400A3C9 RID: 41929
			public static LocString NO_RESEARCHER_SKILL = "No Researchers assigned";

			// Token: 0x0400A3CA RID: 41930
			public static LocString NO_RESEARCHER_SKILL_TOOLTIP = "The selected research focus requires {ResearchType} to complete\n\nOpen the " + UI.FormatAsManagementMenu("Skills Panel", global::Action.ManageSkills) + " and teach a Duplicant the {ResearchType} Skill to use this building";

			// Token: 0x0400A3CB RID: 41931
			public static LocString MISSING_RESEARCH_STATION = "Missing Research Station";

			// Token: 0x0400A3CC RID: 41932
			public static LocString MISSING_RESEARCH_STATION_TOOLTIP = "The selected research focus requires a {0} to perform\n\nOpen the " + UI.FormatAsBuildMenuTab("Stations Tab", global::Action.Plan10) + " of the Build Menu to construct one";

			// Token: 0x02003271 RID: 12913
			public static class DLC
			{
				// Token: 0x0400D495 RID: 54421
				public static LocString EXPANSION1 = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"\n\n<i>",
					UI.DLC1.NAME,
					"</i>",
					UI.PST_KEYWORD,
					" DLC Content"
				});

				// Token: 0x0400D496 RID: 54422
				public static LocString DLC_CONTENT = "\n<i>{0}</i> DLC Content";
			}
		}

		// Token: 0x02002413 RID: 9235
		public class TYPES
		{
			// Token: 0x0400A3CD RID: 41933
			public static LocString MISSINGRECIPEDESC = "Missing Recipe Description";

			// Token: 0x02003272 RID: 12914
			public class ALPHA
			{
				// Token: 0x0400D497 RID: 54423
				public static LocString NAME = "Novice Research";

				// Token: 0x0400D498 RID: 54424
				public static LocString DESC = UI.FormatAsLink("Novice Research", "RESEARCH") + " is required to unlock basic technologies.\nIt can be conducted at a " + UI.FormatAsLink("Research Station", "RESEARCHCENTER") + ".";

				// Token: 0x0400D499 RID: 54425
				public static LocString RECIPEDESC = "Unlocks rudimentary technologies.";
			}

			// Token: 0x02003273 RID: 12915
			public class BETA
			{
				// Token: 0x0400D49A RID: 54426
				public static LocString NAME = "Advanced Research";

				// Token: 0x0400D49B RID: 54427
				public static LocString DESC = UI.FormatAsLink("Advanced Research", "RESEARCH") + " is required to unlock improved technologies.\nIt can be conducted at a " + UI.FormatAsLink("Super Computer", "ADVANCEDRESEARCHCENTER") + ".";

				// Token: 0x0400D49C RID: 54428
				public static LocString RECIPEDESC = "Unlocks improved technologies.";
			}

			// Token: 0x02003274 RID: 12916
			public class GAMMA
			{
				// Token: 0x0400D49D RID: 54429
				public static LocString NAME = "Interstellar Research";

				// Token: 0x0400D49E RID: 54430
				public static LocString DESC = UI.FormatAsLink("Interstellar Research", "RESEARCH") + " is required to unlock space technologies.\nIt can be conducted at a " + UI.FormatAsLink("Virtual Planetarium", "COSMICRESEARCHCENTER") + ".";

				// Token: 0x0400D49F RID: 54431
				public static LocString RECIPEDESC = "Unlocks cutting-edge technologies.";
			}

			// Token: 0x02003275 RID: 12917
			public class DELTA
			{
				// Token: 0x0400D4A0 RID: 54432
				public static LocString NAME = "Applied Sciences Research";

				// Token: 0x0400D4A1 RID: 54433
				public static LocString DESC = UI.FormatAsLink("Applied Sciences Research", "RESEARCH") + " is required to unlock materials science technologies.\nIt can be conducted at a " + UI.FormatAsLink("Materials Study Terminal", "NUCLEARRESEARCHCENTER") + ".";

				// Token: 0x0400D4A2 RID: 54434
				public static LocString RECIPEDESC = "Unlocks next wave technologies.";
			}

			// Token: 0x02003276 RID: 12918
			public class ORBITAL
			{
				// Token: 0x0400D4A3 RID: 54435
				public static LocString NAME = "Data Analysis Research";

				// Token: 0x0400D4A4 RID: 54436
				public static LocString DESC = UI.FormatAsLink("Data Analysis Research", "RESEARCH") + " is required to unlock Data Analysis technologies.\nIt can be conducted at a " + UI.FormatAsLink("Orbital Data Collection Lab", "ORBITALRESEARCHCENTER") + ".";

				// Token: 0x0400D4A5 RID: 54437
				public static LocString RECIPEDESC = "Unlocks out-of-this-world technologies.";
			}
		}

		// Token: 0x02002414 RID: 9236
		public class OTHER_TECH_ITEMS
		{
			// Token: 0x02003277 RID: 12919
			public class AUTOMATION_OVERLAY
			{
				// Token: 0x0400D4A6 RID: 54438
				public static LocString NAME = UI.FormatAsOverlay("Automation Overlay");

				// Token: 0x0400D4A7 RID: 54439
				public static LocString DESC = "Enables access to the " + UI.FormatAsOverlay("Automation Overlay") + ".";
			}

			// Token: 0x02003278 RID: 12920
			public class SUITS_OVERLAY
			{
				// Token: 0x0400D4A8 RID: 54440
				public static LocString NAME = UI.FormatAsOverlay("Exosuit Overlay");

				// Token: 0x0400D4A9 RID: 54441
				public static LocString DESC = "Enables access to the " + UI.FormatAsOverlay("Exosuit Overlay") + ".";
			}

			// Token: 0x02003279 RID: 12921
			public class JET_SUIT
			{
				// Token: 0x0400D4AA RID: 54442
				public static LocString NAME = UI.PRE_KEYWORD + "Jet Suit" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4AB RID: 54443
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Jet Suits",
					UI.PST_KEYWORD,
					" at the ",
					BUILDINGS.PREFABS.SUITFABRICATOR.NAME
				});
			}

			// Token: 0x0200327A RID: 12922
			public class OXYGEN_MASK
			{
				// Token: 0x0400D4AC RID: 54444
				public static LocString NAME = UI.PRE_KEYWORD + "Oxygen Mask" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4AD RID: 54445
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Oxygen Masks",
					UI.PST_KEYWORD,
					" at the ",
					BUILDINGS.PREFABS.CRAFTINGTABLE.NAME
				});
			}

			// Token: 0x0200327B RID: 12923
			public class LEAD_SUIT
			{
				// Token: 0x0400D4AE RID: 54446
				public static LocString NAME = UI.PRE_KEYWORD + "Lead Suit" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4AF RID: 54447
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Lead Suits",
					UI.PST_KEYWORD,
					" at the ",
					BUILDINGS.PREFABS.SUITFABRICATOR.NAME
				});
			}

			// Token: 0x0200327C RID: 12924
			public class ATMO_SUIT
			{
				// Token: 0x0400D4B0 RID: 54448
				public static LocString NAME = UI.PRE_KEYWORD + "Atmo Suit" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4B1 RID: 54449
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Atmo Suits",
					UI.PST_KEYWORD,
					" at the ",
					BUILDINGS.PREFABS.SUITFABRICATOR.NAME
				});
			}

			// Token: 0x0200327D RID: 12925
			public class BETA_RESEARCH_POINT
			{
				// Token: 0x0400D4B2 RID: 54450
				public static LocString NAME = UI.PRE_KEYWORD + "Advanced Research" + UI.PST_KEYWORD + " Capability";

				// Token: 0x0400D4B3 RID: 54451
				public static LocString DESC = string.Concat(new string[]
				{
					"Allows ",
					UI.PRE_KEYWORD,
					"Advanced Research",
					UI.PST_KEYWORD,
					" points to be accumulated, unlocking higher technology tiers."
				});
			}

			// Token: 0x0200327E RID: 12926
			public class GAMMA_RESEARCH_POINT
			{
				// Token: 0x0400D4B4 RID: 54452
				public static LocString NAME = UI.PRE_KEYWORD + "Interstellar Research" + UI.PST_KEYWORD + " Capability";

				// Token: 0x0400D4B5 RID: 54453
				public static LocString DESC = string.Concat(new string[]
				{
					"Allows ",
					UI.PRE_KEYWORD,
					"Interstellar Research",
					UI.PST_KEYWORD,
					" points to be accumulated, unlocking higher technology tiers."
				});
			}

			// Token: 0x0200327F RID: 12927
			public class DELTA_RESEARCH_POINT
			{
				// Token: 0x0400D4B6 RID: 54454
				public static LocString NAME = UI.PRE_KEYWORD + "Materials Science Research" + UI.PST_KEYWORD + " Capability";

				// Token: 0x0400D4B7 RID: 54455
				public static LocString DESC = string.Concat(new string[]
				{
					"Allows ",
					UI.PRE_KEYWORD,
					"Materials Science Research",
					UI.PST_KEYWORD,
					" points to be accumulated, unlocking higher technology tiers."
				});
			}

			// Token: 0x02003280 RID: 12928
			public class ORBITAL_RESEARCH_POINT
			{
				// Token: 0x0400D4B8 RID: 54456
				public static LocString NAME = UI.PRE_KEYWORD + "Data Analysis Research" + UI.PST_KEYWORD + " Capability";

				// Token: 0x0400D4B9 RID: 54457
				public static LocString DESC = string.Concat(new string[]
				{
					"Allows ",
					UI.PRE_KEYWORD,
					"Data Analysis Research",
					UI.PST_KEYWORD,
					" points to be accumulated, unlocking higher technology tiers."
				});
			}

			// Token: 0x02003281 RID: 12929
			public class CONVEYOR_OVERLAY
			{
				// Token: 0x0400D4BA RID: 54458
				public static LocString NAME = UI.FormatAsOverlay("Conveyor Overlay");

				// Token: 0x0400D4BB RID: 54459
				public static LocString DESC = "Enables access to the " + UI.FormatAsOverlay("Conveyor Overlay") + ".";
			}

			// Token: 0x02003282 RID: 12930
			public class SUPER_LIQUIDS
			{
				// Token: 0x0400D4BC RID: 54460
				public static LocString NAME = UI.PRE_KEYWORD + "Advanced Chemical Production" + UI.PST_KEYWORD;

				// Token: 0x0400D4BD RID: 54461
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables production of ",
					ELEMENTS.VISCOGEL.NAME,
					" and ",
					ELEMENTS.SUPERCOOLANT.NAME,
					" at the ",
					BUILDINGS.PREFABS.CHEMICALREFINERY.NAME,
					"."
				});
			}

			// Token: 0x02003283 RID: 12931
			public class LUBRICATION_STICK
			{
				// Token: 0x0400D4BE RID: 54462
				public static LocString NAME = UI.PRE_KEYWORD + "Gear Balm" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4BF RID: 54463
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Gear Balm",
					UI.PST_KEYWORD,
					" at the ",
					BUILDINGS.PREFABS.APOTHECARY.NAME
				});
			}

			// Token: 0x02003284 RID: 12932
			public class DISPOSABLE_ELECTROBANK_METAL_ORE
			{
				// Token: 0x0400D4C0 RID: 54464
				public static LocString NAME = UI.PRE_KEYWORD + "Metal Power Bank" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4C1 RID: 54465
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Metal Power Banks",
					UI.PST_KEYWORD,
					" at the ",
					BUILDINGS.PREFABS.CRAFTINGTABLE.NAME
				});
			}

			// Token: 0x02003285 RID: 12933
			public class DISPOSABLE_ELECTROBANK_URANIUM_ORE
			{
				// Token: 0x0400D4C2 RID: 54466
				public static LocString NAME = UI.PRE_KEYWORD + "Uranium Ore Power Bank" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4C3 RID: 54467
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Uranium Ore Power Banks",
					UI.PST_KEYWORD,
					" at the ",
					BUILDINGS.PREFABS.CRAFTINGTABLE.NAME
				});
			}

			// Token: 0x02003286 RID: 12934
			public class ELECTROBANK
			{
				// Token: 0x0400D4C4 RID: 54468
				public static LocString NAME = UI.PRE_KEYWORD + "Eco Power Bank" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4C5 RID: 54469
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Eco Power Banks",
					UI.PST_KEYWORD,
					" at the ",
					BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME
				});
			}

			// Token: 0x02003287 RID: 12935
			public class SELFCHARGINGELECTROBANK
			{
				// Token: 0x0400D4C6 RID: 54470
				public static LocString NAME = UI.PRE_KEYWORD + "Atomic Power Bank" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4C7 RID: 54471
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Atomic Power Bank",
					UI.PST_KEYWORD,
					" at the ",
					BUILDINGS.PREFABS.SUPERMATERIALREFINERY.NAME
				});
			}

			// Token: 0x02003288 RID: 12936
			public class FETCHDRONE
			{
				// Token: 0x0400D4C8 RID: 54472
				public static LocString NAME = UI.PRE_KEYWORD + "Flydo" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4C9 RID: 54473
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Flydo",
					UI.PST_KEYWORD,
					" at the ",
					BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME
				});
			}

			// Token: 0x02003289 RID: 12937
			public class PILOTINGBOOSTER
			{
				// Token: 0x0400D4CA RID: 54474
				public static LocString NAME = UI.PRE_KEYWORD + "Rocketry Booster" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4CB RID: 54475
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Rocketry Boosters",
					UI.PST_KEYWORD,
					" for Bionic Duplicants at the ",
					BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME
				});
			}

			// Token: 0x0200328A RID: 12938
			public class CONSTRUCTIONBOOSTER
			{
				// Token: 0x0400D4CC RID: 54476
				public static LocString NAME = UI.PRE_KEYWORD + "Building Booster" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4CD RID: 54477
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Building Boosters",
					UI.PST_KEYWORD,
					" for Bionic Duplicants at the ",
					BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME
				});
			}

			// Token: 0x0200328B RID: 12939
			public class EXCAVATIONBOOSTER
			{
				// Token: 0x0400D4CE RID: 54478
				public static LocString NAME = UI.PRE_KEYWORD + "Digging Booster" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4CF RID: 54479
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Digging Boosters",
					UI.PST_KEYWORD,
					" for Bionic Duplicants at the ",
					BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME
				});
			}

			// Token: 0x0200328C RID: 12940
			public class EXPLORERBOOSTER
			{
				// Token: 0x0400D4D0 RID: 54480
				public static LocString NAME = UI.PRE_KEYWORD + "Dowsing Booster" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4D1 RID: 54481
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Dowsing Boosters",
					UI.PST_KEYWORD,
					" for Bionic Duplicants at the ",
					BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME
				});
			}

			// Token: 0x0200328D RID: 12941
			public class MACHINERYBOOSTER
			{
				// Token: 0x0400D4D2 RID: 54482
				public static LocString NAME = UI.PRE_KEYWORD + "Operating Booster" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4D3 RID: 54483
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Operating Boosters",
					UI.PST_KEYWORD,
					" for Bionic Duplicants at the ",
					BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME
				});
			}

			// Token: 0x0200328E RID: 12942
			public class ATHLETICSBOOSTER
			{
				// Token: 0x0400D4D4 RID: 54484
				public static LocString NAME = UI.PRE_KEYWORD + "Athletics Booster" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4D5 RID: 54485
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Athletics Boosters",
					UI.PST_KEYWORD,
					" for Bionic Duplicants at the ",
					BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME
				});
			}

			// Token: 0x0200328F RID: 12943
			public class SCIENCEBOOSTER
			{
				// Token: 0x0400D4D6 RID: 54486
				public static LocString NAME = UI.PRE_KEYWORD + "Researching Booster" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4D7 RID: 54487
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Researching Boosters",
					UI.PST_KEYWORD,
					" for Bionic Duplicants at the ",
					BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME
				});
			}

			// Token: 0x02003290 RID: 12944
			public class COOKINGBOOSTER
			{
				// Token: 0x0400D4D8 RID: 54488
				public static LocString NAME = UI.PRE_KEYWORD + "Cooking Booster" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4D9 RID: 54489
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Cooking Boosters",
					UI.PST_KEYWORD,
					" for Bionic Duplicants at the ",
					BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME
				});
			}

			// Token: 0x02003291 RID: 12945
			public class MEDICINEBOOSTER
			{
				// Token: 0x0400D4DA RID: 54490
				public static LocString NAME = UI.PRE_KEYWORD + "Doctoring Booster" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4DB RID: 54491
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Doctoring Boosters",
					UI.PST_KEYWORD,
					" for Bionic Duplicants at the ",
					BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME
				});
			}

			// Token: 0x02003292 RID: 12946
			public class STRENGTHBOOSTER
			{
				// Token: 0x0400D4DC RID: 54492
				public static LocString NAME = UI.PRE_KEYWORD + "Strength Booster" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4DD RID: 54493
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Strength Boosters",
					UI.PST_KEYWORD,
					" for Bionic Duplicants at the ",
					BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME
				});
			}

			// Token: 0x02003293 RID: 12947
			public class CREATIVITYBOOSTER
			{
				// Token: 0x0400D4DE RID: 54494
				public static LocString NAME = UI.PRE_KEYWORD + "Decorating Booster" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4DF RID: 54495
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Decorating Boosters",
					UI.PST_KEYWORD,
					" for Bionic Duplicants at the ",
					BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME
				});
			}

			// Token: 0x02003294 RID: 12948
			public class AGRICULTUREBOOSTER
			{
				// Token: 0x0400D4E0 RID: 54496
				public static LocString NAME = UI.PRE_KEYWORD + "Farming Booster" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4E1 RID: 54497
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Farming Boosters",
					UI.PST_KEYWORD,
					" for Bionic Duplicants at the ",
					BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME
				});
			}

			// Token: 0x02003295 RID: 12949
			public class HUSBANDRYBOOSTER
			{
				// Token: 0x0400D4E2 RID: 54498
				public static LocString NAME = UI.PRE_KEYWORD + "Ranching Booster" + UI.PST_KEYWORD + " Pattern";

				// Token: 0x0400D4E3 RID: 54499
				public static LocString DESC = string.Concat(new string[]
				{
					"Enables fabrication of ",
					UI.PRE_KEYWORD,
					"Ranching Boosters",
					UI.PST_KEYWORD,
					" for Bionic Duplicants at the ",
					BUILDINGS.PREFABS.ADVANCEDCRAFTINGTABLE.NAME
				});
			}
		}

		// Token: 0x02002415 RID: 9237
		public class TREES
		{
			// Token: 0x0400A3CE RID: 41934
			public static LocString TITLE_FOOD = "Food";

			// Token: 0x0400A3CF RID: 41935
			public static LocString TITLE_POWER = "Power";

			// Token: 0x0400A3D0 RID: 41936
			public static LocString TITLE_SOLIDS = "Solid Material";

			// Token: 0x0400A3D1 RID: 41937
			public static LocString TITLE_COLONYDEVELOPMENT = "Colony Development";

			// Token: 0x0400A3D2 RID: 41938
			public static LocString TITLE_RADIATIONTECH = "Radiation Technologies";

			// Token: 0x0400A3D3 RID: 41939
			public static LocString TITLE_MEDICINE = "Medicine";

			// Token: 0x0400A3D4 RID: 41940
			public static LocString TITLE_LIQUIDS = "Liquids";

			// Token: 0x0400A3D5 RID: 41941
			public static LocString TITLE_GASES = "Gases";

			// Token: 0x0400A3D6 RID: 41942
			public static LocString TITLE_SUITS = "Exosuits";

			// Token: 0x0400A3D7 RID: 41943
			public static LocString TITLE_DECOR = "Decor";

			// Token: 0x0400A3D8 RID: 41944
			public static LocString TITLE_COMPUTERS = "Computers";

			// Token: 0x0400A3D9 RID: 41945
			public static LocString TITLE_ROCKETS = "Rocketry";
		}

		// Token: 0x02002416 RID: 9238
		public class TECHS
		{
			// Token: 0x02003296 RID: 12950
			public class JOBS
			{
				// Token: 0x0400D4E4 RID: 54500
				public static LocString NAME = UI.FormatAsLink("Employment", "JOBS");

				// Token: 0x0400D4E5 RID: 54501
				public static LocString DESC = "Exchange the skill points earned by Duplicants for new traits and abilities.";
			}

			// Token: 0x02003297 RID: 12951
			public class IMPROVEDOXYGEN
			{
				// Token: 0x0400D4E6 RID: 54502
				public static LocString NAME = UI.FormatAsLink("Air Systems", "IMPROVEDOXYGEN");

				// Token: 0x0400D4E7 RID: 54503
				public static LocString DESC = "Maintain clean, breathable air in the colony.";
			}

			// Token: 0x02003298 RID: 12952
			public class FARMINGTECH
			{
				// Token: 0x0400D4E8 RID: 54504
				public static LocString NAME = UI.FormatAsLink("Basic Farming", "FARMINGTECH");

				// Token: 0x0400D4E9 RID: 54505
				public static LocString DESC = "Learn the introductory principles of " + UI.FormatAsLink("Plant", "PLANTS") + " domestication.";
			}

			// Token: 0x02003299 RID: 12953
			public class AGRICULTURE
			{
				// Token: 0x0400D4EA RID: 54506
				public static LocString NAME = UI.FormatAsLink("Agriculture", "AGRICULTURE");

				// Token: 0x0400D4EB RID: 54507
				public static LocString DESC = "Master the agricultural art of crop raising.";
			}

			// Token: 0x0200329A RID: 12954
			public class RANCHING
			{
				// Token: 0x0400D4EC RID: 54508
				public static LocString NAME = UI.FormatAsLink("Ranching", "RANCHING");

				// Token: 0x0400D4ED RID: 54509
				public static LocString DESC = "Tame and care for wild critters.";
			}

			// Token: 0x0200329B RID: 12955
			public class ANIMALCONTROL
			{
				// Token: 0x0400D4EE RID: 54510
				public static LocString NAME = UI.FormatAsLink("Animal Control", "ANIMALCONTROL");

				// Token: 0x0400D4EF RID: 54511
				public static LocString DESC = "Useful techniques to manage critter populations in the colony.";
			}

			// Token: 0x0200329C RID: 12956
			public class ANIMALCOMFORT
			{
				// Token: 0x0400D4F0 RID: 54512
				public static LocString NAME = UI.FormatAsLink("Creature Comforts", "ANIMALCOMFORT");

				// Token: 0x0400D4F1 RID: 54513
				public static LocString DESC = "Strategies for maximizing critters' quality of life.";
			}

			// Token: 0x0200329D RID: 12957
			public class DAIRYOPERATION
			{
				// Token: 0x0400D4F2 RID: 54514
				public static LocString NAME = UI.FormatAsLink("Brackene Flow", "DAIRYOPERATION");

				// Token: 0x0400D4F3 RID: 54515
				public static LocString DESC = "Advanced production, processing and distribution of this fluid resource.";
			}

			// Token: 0x0200329E RID: 12958
			public class FOODREPURPOSING
			{
				// Token: 0x0400D4F4 RID: 54516
				public static LocString NAME = UI.FormatAsLink("Food Repurposing", "FOODREPURPOSING");

				// Token: 0x0400D4F5 RID: 54517
				public static LocString DESC = string.Concat(new string[]
				{
					"Blend that leftover ",
					UI.FormatAsLink("Food", "FOOD"),
					" into a ",
					UI.FormatAsLink("Morale", "MORALE"),
					"-boosting slurry."
				});
			}

			// Token: 0x0200329F RID: 12959
			public class FINEDINING
			{
				// Token: 0x0400D4F6 RID: 54518
				public static LocString NAME = UI.FormatAsLink("Meal Preparation", "FINEDINING");

				// Token: 0x0400D4F7 RID: 54519
				public static LocString DESC = "Prepare more nutritious " + UI.FormatAsLink("Food", "FOOD") + " and store it longer before spoiling.";
			}

			// Token: 0x020032A0 RID: 12960
			public class FINERDINING
			{
				// Token: 0x0400D4F8 RID: 54520
				public static LocString NAME = UI.FormatAsLink("Gourmet Meal Preparation", "FINERDINING");

				// Token: 0x0400D4F9 RID: 54521
				public static LocString DESC = "Raise colony Morale by cooking the most delicious, high-quality " + UI.FormatAsLink("Foods", "FOOD") + ".";
			}

			// Token: 0x020032A1 RID: 12961
			public class GASPIPING
			{
				// Token: 0x0400D4FA RID: 54522
				public static LocString NAME = UI.FormatAsLink("Ventilation", "GASPIPING");

				// Token: 0x0400D4FB RID: 54523
				public static LocString DESC = "Rudimentary technologies for installing " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " infrastructure.";
			}

			// Token: 0x020032A2 RID: 12962
			public class IMPROVEDGASPIPING
			{
				// Token: 0x0400D4FC RID: 54524
				public static LocString NAME = UI.FormatAsLink("Improved Ventilation", "IMPROVEDGASPIPING");

				// Token: 0x0400D4FD RID: 54525
				public static LocString DESC = UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " infrastructure capable of withstanding more intense conditions, such as " + UI.FormatAsLink("Heat", "Heat") + " and pressure.";
			}

			// Token: 0x020032A3 RID: 12963
			public class FLOWREDIRECTION
			{
				// Token: 0x0400D4FE RID: 54526
				public static LocString NAME = UI.FormatAsLink("Flow Redirection", "FLOWREDIRECTION");

				// Token: 0x0400D4FF RID: 54527
				public static LocString DESC = UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " management for " + UI.FormatAsLink("Morale", "MORALE") + " and industry.";
			}

			// Token: 0x020032A4 RID: 12964
			public class LIQUIDDISTRIBUTION
			{
				// Token: 0x0400D500 RID: 54528
				public static LocString NAME = UI.FormatAsLink("Liquid Distribution", "LIQUIDDISTRIBUTION");

				// Token: 0x0400D501 RID: 54529
				public static LocString DESC = "Advanced fittings ensure that " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " resources get where they need to go.";
			}

			// Token: 0x020032A5 RID: 12965
			public class TEMPERATUREMODULATION
			{
				// Token: 0x0400D502 RID: 54530
				public static LocString NAME = UI.FormatAsLink("Temperature Modulation", "TEMPERATUREMODULATION");

				// Token: 0x0400D503 RID: 54531
				public static LocString DESC = "Precise " + UI.FormatAsLink("Temperature", "HEAT") + " altering technologies to keep my colony at the perfect Kelvin.";
			}

			// Token: 0x020032A6 RID: 12966
			public class HVAC
			{
				// Token: 0x0400D504 RID: 54532
				public static LocString NAME = UI.FormatAsLink("HVAC", "HVAC");

				// Token: 0x0400D505 RID: 54533
				public static LocString DESC = string.Concat(new string[]
				{
					"Regulate ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" in the colony for ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" cultivation and Duplicant comfort."
				});
			}

			// Token: 0x020032A7 RID: 12967
			public class GASDISTRIBUTION
			{
				// Token: 0x0400D506 RID: 54534
				public static LocString NAME = UI.FormatAsLink("Gas Distribution", "GASDISTRIBUTION");

				// Token: 0x0400D507 RID: 54535
				public static LocString DESC = "Design building hookups to get " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " resources circulating properly.";
			}

			// Token: 0x020032A8 RID: 12968
			public class LIQUIDTEMPERATURE
			{
				// Token: 0x0400D508 RID: 54536
				public static LocString NAME = UI.FormatAsLink("Liquid Tuning", "LIQUIDTEMPERATURE");

				// Token: 0x0400D509 RID: 54537
				public static LocString DESC = string.Concat(new string[]
				{
					"Easily manipulate ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" ",
					UI.FormatAsLink("Heat", "Temperatures"),
					" with these temperature regulating technologies."
				});
			}

			// Token: 0x020032A9 RID: 12969
			public class INSULATION
			{
				// Token: 0x0400D50A RID: 54538
				public static LocString NAME = UI.FormatAsLink("Insulation", "INSULATION");

				// Token: 0x0400D50B RID: 54539
				public static LocString DESC = "Improve " + UI.FormatAsLink("Heat", "Heat") + " distribution within the colony and guard buildings from extreme temperatures.";
			}

			// Token: 0x020032AA RID: 12970
			public class PRESSUREMANAGEMENT
			{
				// Token: 0x0400D50C RID: 54540
				public static LocString NAME = UI.FormatAsLink("Pressure Management", "PRESSUREMANAGEMENT");

				// Token: 0x0400D50D RID: 54541
				public static LocString DESC = "Unlock technologies to manage colony pressure and atmosphere.";
			}

			// Token: 0x020032AB RID: 12971
			public class PORTABLEGASSES
			{
				// Token: 0x0400D50E RID: 54542
				public static LocString NAME = UI.FormatAsLink("Portable Gases", "PORTABLEGASSES");

				// Token: 0x0400D50F RID: 54543
				public static LocString DESC = "Unlock technologies to easily move gases around your colony.";
			}

			// Token: 0x020032AC RID: 12972
			public class DIRECTEDAIRSTREAMS
			{
				// Token: 0x0400D510 RID: 54544
				public static LocString NAME = UI.FormatAsLink("Decontamination", "DIRECTEDAIRSTREAMS");

				// Token: 0x0400D511 RID: 54545
				public static LocString DESC = "Instruments to help reduce " + UI.FormatAsLink("Germ", "DISEASE") + " spread within the base.";
			}

			// Token: 0x020032AD RID: 12973
			public class LIQUIDFILTERING
			{
				// Token: 0x0400D512 RID: 54546
				public static LocString NAME = UI.FormatAsLink("Liquid-Based Refinement Processes", "LIQUIDFILTERING");

				// Token: 0x0400D513 RID: 54547
				public static LocString DESC = "Use pumped " + UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID") + " to filter out unwanted elements.";
			}

			// Token: 0x020032AE RID: 12974
			public class LIQUIDPIPING
			{
				// Token: 0x0400D514 RID: 54548
				public static LocString NAME = UI.FormatAsLink("Plumbing", "LIQUIDPIPING");

				// Token: 0x0400D515 RID: 54549
				public static LocString DESC = "Rudimentary technologies for installing " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " infrastructure.";
			}

			// Token: 0x020032AF RID: 12975
			public class IMPROVEDLIQUIDPIPING
			{
				// Token: 0x0400D516 RID: 54550
				public static LocString NAME = UI.FormatAsLink("Improved Plumbing", "IMPROVEDLIQUIDPIPING");

				// Token: 0x0400D517 RID: 54551
				public static LocString DESC = UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " infrastructure capable of withstanding more intense conditions, such as " + UI.FormatAsLink("Heat", "Heat") + " and pressure.";
			}

			// Token: 0x020032B0 RID: 12976
			public class PRECISIONPLUMBING
			{
				// Token: 0x0400D518 RID: 54552
				public static LocString NAME = UI.FormatAsLink("Advanced Caffeination", "PRECISIONPLUMBING");

				// Token: 0x0400D519 RID: 54553
				public static LocString DESC = "Let Duplicants relax after a long day of subterranean digging with a shot of warm beanjuice.";
			}

			// Token: 0x020032B1 RID: 12977
			public class SANITATIONSCIENCES
			{
				// Token: 0x0400D51A RID: 54554
				public static LocString NAME = UI.FormatAsLink("Sanitation", "SANITATIONSCIENCES");

				// Token: 0x0400D51B RID: 54555
				public static LocString DESC = "Make daily ablutions less of a hassle.";
			}

			// Token: 0x020032B2 RID: 12978
			public class ADVANCEDSANITATION
			{
				// Token: 0x0400D51C RID: 54556
				public static LocString NAME = UI.FormatAsLink("Advanced Sanitation", "ADVANCEDSANITATION");

				// Token: 0x0400D51D RID: 54557
				public static LocString DESC = "Clean up dirty Duplicants.";
			}

			// Token: 0x020032B3 RID: 12979
			public class MEDICINEI
			{
				// Token: 0x0400D51E RID: 54558
				public static LocString NAME = UI.FormatAsLink("Pharmacology", "MEDICINEI");

				// Token: 0x0400D51F RID: 54559
				public static LocString DESC = "Compound natural cures to fight the most common " + UI.FormatAsLink("Sicknesses", "SICKNESSES") + " that plague Duplicants.";
			}

			// Token: 0x020032B4 RID: 12980
			public class MEDICINEII
			{
				// Token: 0x0400D520 RID: 54560
				public static LocString NAME = UI.FormatAsLink("Medical Equipment", "MEDICINEII");

				// Token: 0x0400D521 RID: 54561
				public static LocString DESC = "The basic necessities doctors need to facilitate patient care.";
			}

			// Token: 0x020032B5 RID: 12981
			public class MEDICINEIII
			{
				// Token: 0x0400D522 RID: 54562
				public static LocString NAME = UI.FormatAsLink("Pathogen Diagnostics", "MEDICINEIII");

				// Token: 0x0400D523 RID: 54563
				public static LocString DESC = "Stop Germs at the source using special medical automation technology.";
			}

			// Token: 0x020032B6 RID: 12982
			public class MEDICINEIV
			{
				// Token: 0x0400D524 RID: 54564
				public static LocString NAME = UI.FormatAsLink("Micro-Targeted Medicine", "MEDICINEIV");

				// Token: 0x0400D525 RID: 54565
				public static LocString DESC = "State of the art equipment to conquer the most stubborn of illnesses.";
			}

			// Token: 0x020032B7 RID: 12983
			public class ADVANCEDFILTRATION
			{
				// Token: 0x0400D526 RID: 54566
				public static LocString NAME = UI.FormatAsLink("Filtration", "ADVANCEDFILTRATION");

				// Token: 0x0400D527 RID: 54567
				public static LocString DESC = string.Concat(new string[]
				{
					"Basic technologies for filtering ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					" and ",
					UI.FormatAsLink("Gases", "ELEMENTS_GAS"),
					"."
				});
			}

			// Token: 0x020032B8 RID: 12984
			public class POWERREGULATION
			{
				// Token: 0x0400D528 RID: 54568
				public static LocString NAME = UI.FormatAsLink("Power Regulation", "POWERREGULATION");

				// Token: 0x0400D529 RID: 54569
				public static LocString DESC = "Prevent wasted " + UI.FormatAsLink("Power", "POWER") + " with improved electrical tools.";
			}

			// Token: 0x020032B9 RID: 12985
			public class COMBUSTION
			{
				// Token: 0x0400D52A RID: 54570
				public static LocString NAME = UI.FormatAsLink("Internal Combustion", "COMBUSTION");

				// Token: 0x0400D52B RID: 54571
				public static LocString DESC = "Fuel-powered generators for crude yet powerful " + UI.FormatAsLink("Power", "POWER") + " production.";
			}

			// Token: 0x020032BA RID: 12986
			public class IMPROVEDCOMBUSTION
			{
				// Token: 0x0400D52C RID: 54572
				public static LocString NAME = UI.FormatAsLink("Fossil Fuels", "IMPROVEDCOMBUSTION");

				// Token: 0x0400D52D RID: 54573
				public static LocString DESC = "Burn dirty fuels for exceptional " + UI.FormatAsLink("Power", "POWER") + " production.";
			}

			// Token: 0x020032BB RID: 12987
			public class INTERIORDECOR
			{
				// Token: 0x0400D52E RID: 54574
				public static LocString NAME = UI.FormatAsLink("Interior Decor", "INTERIORDECOR");

				// Token: 0x0400D52F RID: 54575
				public static LocString DESC = UI.FormatAsLink("Decor", "DECOR") + " boosting items to counteract the gloom of underground living.";
			}

			// Token: 0x020032BC RID: 12988
			public class ARTISTRY
			{
				// Token: 0x0400D530 RID: 54576
				public static LocString NAME = UI.FormatAsLink("Artistic Expression", "ARTISTRY");

				// Token: 0x0400D531 RID: 54577
				public static LocString DESC = "Majorly improve " + UI.FormatAsLink("Decor", "DECOR") + " by giving Duplicants the tools of artistic and emotional expression.";
			}

			// Token: 0x020032BD RID: 12989
			public class CLOTHING
			{
				// Token: 0x0400D532 RID: 54578
				public static LocString NAME = UI.FormatAsLink("Textile Production", "CLOTHING");

				// Token: 0x0400D533 RID: 54579
				public static LocString DESC = "Bring Duplicants the " + UI.FormatAsLink("Morale", "MORALE") + " boosting benefits of soft, cushy fabrics.";
			}

			// Token: 0x020032BE RID: 12990
			public class ACOUSTICS
			{
				// Token: 0x0400D534 RID: 54580
				public static LocString NAME = UI.FormatAsLink("Sound Amplifiers", "ACOUSTICS");

				// Token: 0x0400D535 RID: 54581
				public static LocString DESC = "Precise control of the audio spectrum allows Duplicants to get funky.";
			}

			// Token: 0x020032BF RID: 12991
			public class SPACEPOWER
			{
				// Token: 0x0400D536 RID: 54582
				public static LocString NAME = UI.FormatAsLink("Space Power", "SPACEPOWER");

				// Token: 0x0400D537 RID: 54583
				public static LocString DESC = "It's like power... in space!";
			}

			// Token: 0x020032C0 RID: 12992
			public class AMPLIFIERS
			{
				// Token: 0x0400D538 RID: 54584
				public static LocString NAME = UI.FormatAsLink("Power Amplifiers", "AMPLIFIERS");

				// Token: 0x0400D539 RID: 54585
				public static LocString DESC = "Further increased efficacy of " + UI.FormatAsLink("Power", "POWER") + " management to prevent those wasted joules.";
			}

			// Token: 0x020032C1 RID: 12993
			public class LUXURY
			{
				// Token: 0x0400D53A RID: 54586
				public static LocString NAME = UI.FormatAsLink("Home Luxuries", "LUXURY");

				// Token: 0x0400D53B RID: 54587
				public static LocString DESC = "Luxury amenities for advanced " + UI.FormatAsLink("Stress", "STRESS") + " reduction.";
			}

			// Token: 0x020032C2 RID: 12994
			public class ENVIRONMENTALAPPRECIATION
			{
				// Token: 0x0400D53C RID: 54588
				public static LocString NAME = UI.FormatAsLink("Environmental Appreciation", "ENVIRONMENTALAPPRECIATION");

				// Token: 0x0400D53D RID: 54589
				public static LocString DESC = string.Concat(new string[]
				{
					"Improve ",
					UI.FormatAsLink("Morale", "MORALE"),
					" by lazing around in ",
					UI.FormatAsLink("Light", "LIGHT"),
					" with a high Lux value."
				});
			}

			// Token: 0x020032C3 RID: 12995
			public class FINEART
			{
				// Token: 0x0400D53E RID: 54590
				public static LocString NAME = UI.FormatAsLink("Fine Art", "FINEART");

				// Token: 0x0400D53F RID: 54591
				public static LocString DESC = "Broader options for artistic " + UI.FormatAsLink("Decor", "DECOR") + " improvements.";
			}

			// Token: 0x020032C4 RID: 12996
			public class REFRACTIVEDECOR
			{
				// Token: 0x0400D540 RID: 54592
				public static LocString NAME = UI.FormatAsLink("High Culture", "REFRACTIVEDECOR");

				// Token: 0x0400D541 RID: 54593
				public static LocString DESC = "New methods for working with extremely high quality art materials.";
			}

			// Token: 0x020032C5 RID: 12997
			public class RENAISSANCEART
			{
				// Token: 0x0400D542 RID: 54594
				public static LocString NAME = UI.FormatAsLink("Renaissance Art", "RENAISSANCEART");

				// Token: 0x0400D543 RID: 54595
				public static LocString DESC = "The kind of art that culture legacies are made of.";
			}

			// Token: 0x020032C6 RID: 12998
			public class GLASSFURNISHINGS
			{
				// Token: 0x0400D544 RID: 54596
				public static LocString NAME = UI.FormatAsLink("Glass Blowing", "GLASSFURNISHINGS");

				// Token: 0x0400D545 RID: 54597
				public static LocString DESC = "The decorative benefits of glass are both apparent and transparent.";
			}

			// Token: 0x020032C7 RID: 12999
			public class SCREENS
			{
				// Token: 0x0400D546 RID: 54598
				public static LocString NAME = UI.FormatAsLink("New Media", "SCREENS");

				// Token: 0x0400D547 RID: 54599
				public static LocString DESC = "High tech displays with lots of pretty colors.";
			}

			// Token: 0x020032C8 RID: 13000
			public class ADVANCEDPOWERREGULATION
			{
				// Token: 0x0400D548 RID: 54600
				public static LocString NAME = UI.FormatAsLink("Advanced Power Regulation", "ADVANCEDPOWERREGULATION");

				// Token: 0x0400D549 RID: 54601
				public static LocString DESC = "Circuit components required for large scale " + UI.FormatAsLink("Power", "POWER") + " management.";
			}

			// Token: 0x020032C9 RID: 13001
			public class PLASTICS
			{
				// Token: 0x0400D54A RID: 54602
				public static LocString NAME = UI.FormatAsLink("Plastic Manufacturing", "PLASTICS");

				// Token: 0x0400D54B RID: 54603
				public static LocString DESC = "Stable, lightweight, durable. Plastics are useful for a wide array of applications.";
			}

			// Token: 0x020032CA RID: 13002
			public class SUITS
			{
				// Token: 0x0400D54C RID: 54604
				public static LocString NAME = UI.FormatAsLink("Hazard Protection", "SUITS");

				// Token: 0x0400D54D RID: 54605
				public static LocString DESC = "Vital gear for surviving in extreme conditions and environments.";
			}

			// Token: 0x020032CB RID: 13003
			public class DISTILLATION
			{
				// Token: 0x0400D54E RID: 54606
				public static LocString NAME = UI.FormatAsLink("Distillation", "DISTILLATION");

				// Token: 0x0400D54F RID: 54607
				public static LocString DESC = "Distill difficult mixtures down to their most useful parts.";
			}

			// Token: 0x020032CC RID: 13004
			public class ADVANCEDDISTILLATION
			{
				// Token: 0x0400D550 RID: 54608
				public static LocString NAME = UI.FormatAsLink("Emulsification", "ADVANCEDDISTILLATION");

				// Token: 0x0400D551 RID: 54609
				public static LocString DESC = "Specialized production of " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " compounds.";
			}

			// Token: 0x020032CD RID: 13005
			public class CATALYTICS
			{
				// Token: 0x0400D552 RID: 54610
				public static LocString NAME = UI.FormatAsLink("Catalytics", "CATALYTICS");

				// Token: 0x0400D553 RID: 54611
				public static LocString DESC = "Advanced gas manipulation using unique catalysts.";
			}

			// Token: 0x020032CE RID: 13006
			public class ADVANCEDRESEARCH
			{
				// Token: 0x0400D554 RID: 54612
				public static LocString NAME = UI.FormatAsLink("Advanced Research", "ADVANCEDRESEARCH");

				// Token: 0x0400D555 RID: 54613
				public static LocString DESC = "The tools my colony needs to conduct more advanced, in-depth research.";
			}

			// Token: 0x020032CF RID: 13007
			public class SPACEPROGRAM
			{
				// Token: 0x0400D556 RID: 54614
				public static LocString NAME = UI.FormatAsLink("Space Program", "SPACEPROGRAM");

				// Token: 0x0400D557 RID: 54615
				public static LocString DESC = "The first steps in getting a Duplicant to space.";
			}

			// Token: 0x020032D0 RID: 13008
			public class CRASHPLAN
			{
				// Token: 0x0400D558 RID: 54616
				public static LocString NAME = UI.FormatAsLink("Crash Plan", "CRASHPLAN");

				// Token: 0x0400D559 RID: 54617
				public static LocString DESC = "What goes up, must come down.";
			}

			// Token: 0x020032D1 RID: 13009
			public class DURABLELIFESUPPORT
			{
				// Token: 0x0400D55A RID: 54618
				public static LocString NAME = UI.FormatAsLink("Durable Life Support", "DURABLELIFESUPPORT");

				// Token: 0x0400D55B RID: 54619
				public static LocString DESC = "Improved devices for extended missions into space.";
			}

			// Token: 0x020032D2 RID: 13010
			public class ARTIFICIALFRIENDS
			{
				// Token: 0x0400D55C RID: 54620
				public static LocString NAME = UI.FormatAsLink("Artificial Friends", "ARTIFICIALFRIENDS");

				// Token: 0x0400D55D RID: 54621
				public static LocString DESC = "Sweeping advances in companion technology.";
			}

			// Token: 0x020032D3 RID: 13011
			public class ROBOTICTOOLS
			{
				// Token: 0x0400D55E RID: 54622
				public static LocString NAME = UI.FormatAsLink("Robotic Tools", "ROBOTICTOOLS");

				// Token: 0x0400D55F RID: 54623
				public static LocString DESC = "The goal of every great civilization is to one day make itself obsolete.";
			}

			// Token: 0x020032D4 RID: 13012
			public class LOGICCONTROL
			{
				// Token: 0x0400D560 RID: 54624
				public static LocString NAME = UI.FormatAsLink("Smart Home", "LOGICCONTROL");

				// Token: 0x0400D561 RID: 54625
				public static LocString DESC = "Switches that grant full control of building operations within the colony.";
			}

			// Token: 0x020032D5 RID: 13013
			public class LOGICCIRCUITS
			{
				// Token: 0x0400D562 RID: 54626
				public static LocString NAME = UI.FormatAsLink("Advanced Automation", "LOGICCIRCUITS");

				// Token: 0x0400D563 RID: 54627
				public static LocString DESC = "The only limit to colony automation is my own imagination.";
			}

			// Token: 0x020032D6 RID: 13014
			public class PARALLELAUTOMATION
			{
				// Token: 0x0400D564 RID: 54628
				public static LocString NAME = UI.FormatAsLink("Parallel Automation", "PARALLELAUTOMATION");

				// Token: 0x0400D565 RID: 54629
				public static LocString DESC = "Multi-wire automation at a fraction of the space.";
			}

			// Token: 0x020032D7 RID: 13015
			public class MULTIPLEXING
			{
				// Token: 0x0400D566 RID: 54630
				public static LocString NAME = UI.FormatAsLink("Multiplexing", "MULTIPLEXING");

				// Token: 0x0400D567 RID: 54631
				public static LocString DESC = "More choices for Automation signal distribution.";
			}

			// Token: 0x020032D8 RID: 13016
			public class VALVEMINIATURIZATION
			{
				// Token: 0x0400D568 RID: 54632
				public static LocString NAME = UI.FormatAsLink("Valve Miniaturization", "VALVEMINIATURIZATION");

				// Token: 0x0400D569 RID: 54633
				public static LocString DESC = "Smaller, more efficient pumps for those low-throughput situations.";
			}

			// Token: 0x020032D9 RID: 13017
			public class HYDROCARBONPROPULSION
			{
				// Token: 0x0400D56A RID: 54634
				public static LocString NAME = UI.FormatAsLink("Hydrocarbon Propulsion", "HYDROCARBONPROPULSION");

				// Token: 0x0400D56B RID: 54635
				public static LocString DESC = "Low-range rocket engines with lots of smoke.";
			}

			// Token: 0x020032DA RID: 13018
			public class BETTERHYDROCARBONPROPULSION
			{
				// Token: 0x0400D56C RID: 54636
				public static LocString NAME = UI.FormatAsLink("Improved Hydrocarbon Propulsion", "BETTERHYDROCARBONPROPULSION");

				// Token: 0x0400D56D RID: 54637
				public static LocString DESC = "Mid-range rocket engines with lots of smoke.";
			}

			// Token: 0x020032DB RID: 13019
			public class PRETTYGOODCONDUCTORS
			{
				// Token: 0x0400D56E RID: 54638
				public static LocString NAME = UI.FormatAsLink("Low-Resistance Conductors", "PRETTYGOODCONDUCTORS");

				// Token: 0x0400D56F RID: 54639
				public static LocString DESC = "Pure-core wires that can handle more " + UI.FormatAsLink("Electrical", "POWER") + " current without overloading.";
			}

			// Token: 0x020032DC RID: 13020
			public class RENEWABLEENERGY
			{
				// Token: 0x0400D570 RID: 54640
				public static LocString NAME = UI.FormatAsLink("Renewable Energy", "RENEWABLEENERGY");

				// Token: 0x0400D571 RID: 54641
				public static LocString DESC = "Clean, sustainable " + UI.FormatAsLink("Power", "POWER") + " production that produces little to no waste.";
			}

			// Token: 0x020032DD RID: 13021
			public class BASICREFINEMENT
			{
				// Token: 0x0400D572 RID: 54642
				public static LocString NAME = UI.FormatAsLink("Brute-Force Refinement", "BASICREFINEMENT");

				// Token: 0x0400D573 RID: 54643
				public static LocString DESC = "Low-tech refinement methods for producing clay and renewable sources of sand.";
			}

			// Token: 0x020032DE RID: 13022
			public class REFINEDOBJECTS
			{
				// Token: 0x0400D574 RID: 54644
				public static LocString NAME = UI.FormatAsLink("Refined Renovations", "REFINEDOBJECTS");

				// Token: 0x0400D575 RID: 54645
				public static LocString DESC = "Improve base infrastructure with new objects crafted from " + UI.FormatAsLink("Refined Metals", "REFINEDMETAL") + ".";
			}

			// Token: 0x020032DF RID: 13023
			public class GENERICSENSORS
			{
				// Token: 0x0400D576 RID: 54646
				public static LocString NAME = UI.FormatAsLink("Generic Sensors", "GENERICSENSORS");

				// Token: 0x0400D577 RID: 54647
				public static LocString DESC = "Drive automation in a variety of new, inventive ways.";
			}

			// Token: 0x020032E0 RID: 13024
			public class DUPETRAFFICCONTROL
			{
				// Token: 0x0400D578 RID: 54648
				public static LocString NAME = UI.FormatAsLink("Computing", "DUPETRAFFICCONTROL");

				// Token: 0x0400D579 RID: 54649
				public static LocString DESC = "Virtually extend the boundaries of Duplicant imagination.";
			}

			// Token: 0x020032E1 RID: 13025
			public class ADVANCEDSCANNERS
			{
				// Token: 0x0400D57A RID: 54650
				public static LocString NAME = UI.FormatAsLink("Sensitive Microimaging", "ADVANCEDSCANNERS");

				// Token: 0x0400D57B RID: 54651
				public static LocString DESC = "Computerized systems do the looking, so Duplicants don't have to.";
			}

			// Token: 0x020032E2 RID: 13026
			public class SMELTING
			{
				// Token: 0x0400D57C RID: 54652
				public static LocString NAME = UI.FormatAsLink("Smelting", "SMELTING");

				// Token: 0x0400D57D RID: 54653
				public static LocString DESC = "High temperatures facilitate the production of purer, special use metal resources.";
			}

			// Token: 0x020032E3 RID: 13027
			public class TRAVELTUBES
			{
				// Token: 0x0400D57E RID: 54654
				public static LocString NAME = UI.FormatAsLink("Transit Tubes", "TRAVELTUBES");

				// Token: 0x0400D57F RID: 54655
				public static LocString DESC = "A wholly futuristic way to move Duplicants around the base.";
			}

			// Token: 0x020032E4 RID: 13028
			public class SMARTSTORAGE
			{
				// Token: 0x0400D580 RID: 54656
				public static LocString NAME = UI.FormatAsLink("Smart Storage", "SMARTSTORAGE");

				// Token: 0x0400D581 RID: 54657
				public static LocString DESC = "Completely automate the storage of solid resources.";
			}

			// Token: 0x020032E5 RID: 13029
			public class SOLIDTRANSPORT
			{
				// Token: 0x0400D582 RID: 54658
				public static LocString NAME = UI.FormatAsLink("Solid Transport", "SOLIDTRANSPORT");

				// Token: 0x0400D583 RID: 54659
				public static LocString DESC = "Free Duplicants from the drudgery of day-to-day material deliveries with new methods of automation.";
			}

			// Token: 0x020032E6 RID: 13030
			public class SOLIDMANAGEMENT
			{
				// Token: 0x0400D584 RID: 54660
				public static LocString NAME = UI.FormatAsLink("Solid Management", "SOLIDMANAGEMENT");

				// Token: 0x0400D585 RID: 54661
				public static LocString DESC = "Make solid decisions in " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " sorting.";
			}

			// Token: 0x020032E7 RID: 13031
			public class SOLIDDISTRIBUTION
			{
				// Token: 0x0400D586 RID: 54662
				public static LocString NAME = UI.FormatAsLink("Solid Distribution", "SOLIDDISTRIBUTION");

				// Token: 0x0400D587 RID: 54663
				public static LocString DESC = "Internal rocket hookups for " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " resources.";
			}

			// Token: 0x020032E8 RID: 13032
			public class HIGHTEMPFORGING
			{
				// Token: 0x0400D588 RID: 54664
				public static LocString NAME = UI.FormatAsLink("Superheated Forging", "HIGHTEMPFORGING");

				// Token: 0x0400D589 RID: 54665
				public static LocString DESC = "Craft entirely new materials by harnessing the most extreme temperatures.";
			}

			// Token: 0x020032E9 RID: 13033
			public class HIGHPRESSUREFORGING
			{
				// Token: 0x0400D58A RID: 54666
				public static LocString NAME = UI.FormatAsLink("Pressurized Forging", "HIGHPRESSUREFORGING");

				// Token: 0x0400D58B RID: 54667
				public static LocString DESC = "High pressure diamond forging.";
			}

			// Token: 0x020032EA RID: 13034
			public class RADIATIONPROTECTION
			{
				// Token: 0x0400D58C RID: 54668
				public static LocString NAME = UI.FormatAsLink("Radiation Protection", "RADIATIONPROTECTION");

				// Token: 0x0400D58D RID: 54669
				public static LocString DESC = "Shield Duplicants from dangerous amounts of radiation.";
			}

			// Token: 0x020032EB RID: 13035
			public class SKYDETECTORS
			{
				// Token: 0x0400D58E RID: 54670
				public static LocString NAME = UI.FormatAsLink("Celestial Detection", "SKYDETECTORS");

				// Token: 0x0400D58F RID: 54671
				public static LocString DESC = "Turn Duplicants' eyes to the skies and discover what undiscovered wonders await out there.";
			}

			// Token: 0x020032EC RID: 13036
			public class JETPACKS
			{
				// Token: 0x0400D590 RID: 54672
				public static LocString NAME = UI.FormatAsLink("Projectiles", "JETPACKS");

				// Token: 0x0400D591 RID: 54673
				public static LocString DESC = "Things that get Duplicants and explosives off the ground.";
			}

			// Token: 0x020032ED RID: 13037
			public class BASICROCKETRY
			{
				// Token: 0x0400D592 RID: 54674
				public static LocString NAME = UI.FormatAsLink("Introductory Rocketry", "BASICROCKETRY");

				// Token: 0x0400D593 RID: 54675
				public static LocString DESC = "Everything required for launching the colony's very first space program.";
			}

			// Token: 0x020032EE RID: 13038
			public class ENGINESI
			{
				// Token: 0x0400D594 RID: 54676
				public static LocString NAME = UI.FormatAsLink("Solid Fuel Combustion", "ENGINESI");

				// Token: 0x0400D595 RID: 54677
				public static LocString DESC = "Rockets that fly further, longer.";
			}

			// Token: 0x020032EF RID: 13039
			public class ENGINESII
			{
				// Token: 0x0400D596 RID: 54678
				public static LocString NAME = UI.FormatAsLink("Hydrocarbon Combustion", "ENGINESII");

				// Token: 0x0400D597 RID: 54679
				public static LocString DESC = "Delve deeper into the vastness of space than ever before.";
			}

			// Token: 0x020032F0 RID: 13040
			public class ENGINESIII
			{
				// Token: 0x0400D598 RID: 54680
				public static LocString NAME = UI.FormatAsLink("Cryofuel Combustion", "ENGINESIII");

				// Token: 0x0400D599 RID: 54681
				public static LocString DESC = "With this technology, the sky is your oyster. Go exploring!";
			}

			// Token: 0x020032F1 RID: 13041
			public class CRYOFUELPROPULSION
			{
				// Token: 0x0400D59A RID: 54682
				public static LocString NAME = UI.FormatAsLink("Cryofuel Propulsion", "CRYOFUELPROPULSION");

				// Token: 0x0400D59B RID: 54683
				public static LocString DESC = "A semi-powerful engine to propel you further into the galaxy.";
			}

			// Token: 0x020032F2 RID: 13042
			public class NUCLEARPROPULSION
			{
				// Token: 0x0400D59C RID: 54684
				public static LocString NAME = UI.FormatAsLink("Radbolt Propulsion", "NUCLEARPROPULSION");

				// Token: 0x0400D59D RID: 54685
				public static LocString DESC = "Radical technology to get you to the stars.";
			}

			// Token: 0x020032F3 RID: 13043
			public class ADVANCEDRESOURCEEXTRACTION
			{
				// Token: 0x0400D59E RID: 54686
				public static LocString NAME = UI.FormatAsLink("Advanced Resource Extraction", "ADVANCEDRESOURCEEXTRACTION");

				// Token: 0x0400D59F RID: 54687
				public static LocString DESC = "Bring back souvieners from the stars.";
			}

			// Token: 0x020032F4 RID: 13044
			public class CARGOI
			{
				// Token: 0x0400D5A0 RID: 54688
				public static LocString NAME = UI.FormatAsLink("Solid Cargo", "CARGOI");

				// Token: 0x0400D5A1 RID: 54689
				public static LocString DESC = "Make extra use of journeys into space by mining and storing useful resources.";
			}

			// Token: 0x020032F5 RID: 13045
			public class CARGOII
			{
				// Token: 0x0400D5A2 RID: 54690
				public static LocString NAME = UI.FormatAsLink("Liquid and Gas Cargo", "CARGOII");

				// Token: 0x0400D5A3 RID: 54691
				public static LocString DESC = "Extract precious liquids and gases from the far reaches of space, and return with them to the colony.";
			}

			// Token: 0x020032F6 RID: 13046
			public class CARGOIII
			{
				// Token: 0x0400D5A4 RID: 54692
				public static LocString NAME = UI.FormatAsLink("Unique Cargo", "CARGOIII");

				// Token: 0x0400D5A5 RID: 54693
				public static LocString DESC = "Allow Duplicants to take their friends to see the stars... or simply bring souvenirs back from their travels.";
			}

			// Token: 0x020032F7 RID: 13047
			public class NOTIFICATIONSYSTEMS
			{
				// Token: 0x0400D5A6 RID: 54694
				public static LocString NAME = UI.FormatAsLink("Notification Systems", "NOTIFICATIONSYSTEMS");

				// Token: 0x0400D5A7 RID: 54695
				public static LocString DESC = "Get all the news you need to know about your complex colony.";
			}

			// Token: 0x020032F8 RID: 13048
			public class NUCLEARREFINEMENT
			{
				// Token: 0x0400D5A8 RID: 54696
				public static LocString NAME = UI.FormatAsLink("Radiation Refinement", "NUCLEAR");

				// Token: 0x0400D5A9 RID: 54697
				public static LocString DESC = "Refine uranium and generate radiation.";
			}

			// Token: 0x020032F9 RID: 13049
			public class NUCLEARRESEARCH
			{
				// Token: 0x0400D5AA RID: 54698
				public static LocString NAME = UI.FormatAsLink("Materials Science Research", "NUCLEARRESEARCH");

				// Token: 0x0400D5AB RID: 54699
				public static LocString DESC = "Harness sub-atomic particles to study the properties of matter.";
			}

			// Token: 0x020032FA RID: 13050
			public class ADVANCEDNUCLEARRESEARCH
			{
				// Token: 0x0400D5AC RID: 54700
				public static LocString NAME = UI.FormatAsLink("More Materials Science Research", "ADVANCEDNUCLEARRESEARCH");

				// Token: 0x0400D5AD RID: 54701
				public static LocString DESC = "Harness sub-atomic particles to study the properties of matter even more.";
			}

			// Token: 0x020032FB RID: 13051
			public class NUCLEARSTORAGE
			{
				// Token: 0x0400D5AE RID: 54702
				public static LocString NAME = UI.FormatAsLink("Radbolt Containment", "NUCLEARSTORAGE");

				// Token: 0x0400D5AF RID: 54703
				public static LocString DESC = "Build a quality cache of radbolts.";
			}

			// Token: 0x020032FC RID: 13052
			public class SOLIDSPACE
			{
				// Token: 0x0400D5B0 RID: 54704
				public static LocString NAME = UI.FormatAsLink("Solid Control", "SOLIDSPACE");

				// Token: 0x0400D5B1 RID: 54705
				public static LocString DESC = "Transport and sort " + UI.FormatAsLink("Solid", "ELEMENTS_SOLID") + " resources.";
			}

			// Token: 0x020032FD RID: 13053
			public class HIGHVELOCITYTRANSPORT
			{
				// Token: 0x0400D5B2 RID: 54706
				public static LocString NAME = UI.FormatAsLink("High Velocity Transport", "HIGHVELOCITY");

				// Token: 0x0400D5B3 RID: 54707
				public static LocString DESC = "Hurl things through space.";
			}

			// Token: 0x020032FE RID: 13054
			public class MONUMENTS
			{
				// Token: 0x0400D5B4 RID: 54708
				public static LocString NAME = UI.FormatAsLink("Monuments", "MONUMENTS");

				// Token: 0x0400D5B5 RID: 54709
				public static LocString DESC = "Monumental art projects.";
			}

			// Token: 0x020032FF RID: 13055
			public class BIOENGINEERING
			{
				// Token: 0x0400D5B6 RID: 54710
				public static LocString NAME = UI.FormatAsLink("Bioengineering", "BIOENGINEERING");

				// Token: 0x0400D5B7 RID: 54711
				public static LocString DESC = "Mutation station.";
			}

			// Token: 0x02003300 RID: 13056
			public class SPACECOMBUSTION
			{
				// Token: 0x0400D5B8 RID: 54712
				public static LocString NAME = UI.FormatAsLink("Advanced Combustion", "SPACECOMBUSTION");

				// Token: 0x0400D5B9 RID: 54713
				public static LocString DESC = "Sweet advancements in rocket engines.";
			}

			// Token: 0x02003301 RID: 13057
			public class HIGHVELOCITYDESTRUCTION
			{
				// Token: 0x0400D5BA RID: 54714
				public static LocString NAME = UI.FormatAsLink("High Velocity Destruction", "HIGHVELOCITYDESTRUCTION");

				// Token: 0x0400D5BB RID: 54715
				public static LocString DESC = "Mine the skies.";
			}

			// Token: 0x02003302 RID: 13058
			public class SPACEGAS
			{
				// Token: 0x0400D5BC RID: 54716
				public static LocString NAME = UI.FormatAsLink("Advanced Gas Flow", "SPACEGAS");

				// Token: 0x0400D5BD RID: 54717
				public static LocString DESC = UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " engines and transportation for rockets.";
			}

			// Token: 0x02003303 RID: 13059
			public class DATASCIENCE
			{
				// Token: 0x0400D5BE RID: 54718
				public static LocString NAME = UI.FormatAsLink("Data Science", "DATASCIENCE");

				// Token: 0x0400D5BF RID: 54719
				public static LocString DESC = "The science of making the data work for my Duplicants, instead of the other way around.";
			}

			// Token: 0x02003304 RID: 13060
			public class DATASCIENCEBASEGAME
			{
				// Token: 0x0400D5C0 RID: 54720
				public static LocString NAME = UI.FormatAsLink("Data Science", "DATASCIENCEBASEGAME");

				// Token: 0x0400D5C1 RID: 54721
				public static LocString DESC = "The science of making the data work for my Duplicants, instead of the other way around.";
			}
		}
	}
}
