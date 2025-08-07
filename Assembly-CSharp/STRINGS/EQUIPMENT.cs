using System;

namespace STRINGS
{
	// Token: 0x02000FA0 RID: 4000
	public class EQUIPMENT
	{
		// Token: 0x0200227F RID: 8831
		public class PREFABS
		{
			// Token: 0x02002C6F RID: 11375
			public class OXYGEN_MASK
			{
				// Token: 0x0400C056 RID: 49238
				public static LocString NAME = UI.FormatAsLink("Oxygen Mask", "OXYGEN_MASK");

				// Token: 0x0400C057 RID: 49239
				public static LocString DESC = "Ensures my Duplicants can breathe easy... for a little while, anyways.";

				// Token: 0x0400C058 RID: 49240
				public static LocString EFFECT = "Supplies Duplicants with <style=\"oxygen\">Oxygen</style> in toxic and low breathability environments.\n\nMust be refilled with oxygen at an " + UI.FormatAsLink("Oxygen Mask Dock", "OXYGENMASKLOCKER") + " when depleted.";

				// Token: 0x0400C059 RID: 49241
				public static LocString RECIPE_DESC = "Supplies Duplicants with <style=\"oxygen\">Oxygen</style> in toxic and low breathability environments.";

				// Token: 0x0400C05A RID: 49242
				public static LocString GENERICNAME = "Suit";

				// Token: 0x0400C05B RID: 49243
				public static LocString WORN_NAME = UI.FormatAsLink("Worn Oxygen Mask", "OXYGEN_MASK");

				// Token: 0x0400C05C RID: 49244
				public static LocString WORN_DESC = string.Concat(new string[]
				{
					"A worn out ",
					UI.FormatAsLink("Oxygen Mask", "OXYGEN_MASK"),
					".\n\nMasks can be repaired at a ",
					UI.FormatAsLink("Crafting Station", "CRAFTINGTABLE"),
					"."
				});
			}

			// Token: 0x02002C70 RID: 11376
			public class ATMO_SUIT
			{
				// Token: 0x0400C05D RID: 49245
				public static LocString NAME = UI.FormatAsLink("Atmo Suit", "ATMO_SUIT");

				// Token: 0x0400C05E RID: 49246
				public static LocString DESC = "Ensures my Duplicants can breathe easy, anytime, anywhere.";

				// Token: 0x0400C05F RID: 49247
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Supplies Duplicants with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" in toxic and low breathability environments, and protects against extreme temperatures.\n\nMust be refilled with oxygen at an ",
					UI.FormatAsLink("Atmo Suit Dock", "SUITLOCKER"),
					" when depleted."
				});

				// Token: 0x0400C060 RID: 49248
				public static LocString RECIPE_DESC = "Supplies Duplicants with " + UI.FormatAsLink("Oxygen", "OXYGEN") + " in toxic and low breathability environments.";

				// Token: 0x0400C061 RID: 49249
				public static LocString GENERICNAME = "Suit";

				// Token: 0x0400C062 RID: 49250
				public static LocString WORN_NAME = UI.FormatAsLink("Worn Atmo Suit", "ATMO_SUIT");

				// Token: 0x0400C063 RID: 49251
				public static LocString WORN_DESC = string.Concat(new string[]
				{
					"A worn out ",
					UI.FormatAsLink("Atmo Suit", "ATMO_SUIT"),
					".\n\nSuits can be repaired at an ",
					UI.FormatAsLink("Exosuit Forge", "SUITFABRICATOR"),
					"."
				});

				// Token: 0x0400C064 RID: 49252
				public static LocString REPAIR_WORN_RECIPE_NAME = "Repair " + EQUIPMENT.PREFABS.ATMO_SUIT.NAME;

				// Token: 0x0400C065 RID: 49253
				public static LocString REPAIR_WORN_DESC = "Restore a " + UI.FormatAsLink("Worn Atmo Suit", "ATMO_SUIT") + " to working order.";
			}

			// Token: 0x02002C71 RID: 11377
			public class ATMO_SUIT_SET
			{
				// Token: 0x0200393A RID: 14650
				public class PUFT
				{
					// Token: 0x0400E5FA RID: 58874
					public static LocString NAME = "Puft Atmo Suit";

					// Token: 0x0400E5FB RID: 58875
					public static LocString DESC = "Critter-forward protective gear for the intrepid explorer!\nReleased for Klei Fest 2023.";
				}
			}

			// Token: 0x02002C72 RID: 11378
			public class HOLIDAY_2023_CRATE
			{
				// Token: 0x0400C066 RID: 49254
				public static LocString NAME = "Holiday Gift Crate";

				// Token: 0x0400C067 RID: 49255
				public static LocString DESC = "An unaddressed package has been discovered near the Printing Pod. It exudes seasonal cheer, and trace amounts of Neutronium have been detected.";
			}

			// Token: 0x02002C73 RID: 11379
			public class ATMO_SUIT_HELMET
			{
				// Token: 0x0400C068 RID: 49256
				public static LocString NAME = "Default Atmo Helmet";

				// Token: 0x0400C069 RID: 49257
				public static LocString DESC = "Default helmet for atmo suits.";

				// Token: 0x0200393B RID: 14651
				public class FACADES
				{
					// Token: 0x02003D75 RID: 15733
					public class SPARKLE_RED
					{
						// Token: 0x0400F024 RID: 61476
						public static LocString NAME = "Red Glitter Atmo Helmet";

						// Token: 0x0400F025 RID: 61477
						public static LocString DESC = "Protective gear at its sparkliest.";
					}

					// Token: 0x02003D76 RID: 15734
					public class SPARKLE_GREEN
					{
						// Token: 0x0400F026 RID: 61478
						public static LocString NAME = "Green Glitter Atmo Helmet";

						// Token: 0x0400F027 RID: 61479
						public static LocString DESC = "Protective gear at its sparkliest.";
					}

					// Token: 0x02003D77 RID: 15735
					public class SPARKLE_BLUE
					{
						// Token: 0x0400F028 RID: 61480
						public static LocString NAME = "Blue Glitter Atmo Helmet";

						// Token: 0x0400F029 RID: 61481
						public static LocString DESC = "Protective gear at its sparkliest.";
					}

					// Token: 0x02003D78 RID: 15736
					public class SPARKLE_PURPLE
					{
						// Token: 0x0400F02A RID: 61482
						public static LocString NAME = "Violet Glitter Atmo Helmet";

						// Token: 0x0400F02B RID: 61483
						public static LocString DESC = "Protective gear at its sparkliest.";
					}

					// Token: 0x02003D79 RID: 15737
					public class LIMONE
					{
						// Token: 0x0400F02C RID: 61484
						public static LocString NAME = "Citrus Atmo Helmet";

						// Token: 0x0400F02D RID: 61485
						public static LocString DESC = "Fresh, fruity and full of breathable air.";
					}

					// Token: 0x02003D7A RID: 15738
					public class PUFT
					{
						// Token: 0x0400F02E RID: 61486
						public static LocString NAME = "Puft Atmo Helmet";

						// Token: 0x0400F02F RID: 61487
						public static LocString DESC = "Convincing enough to fool most Pufts and even a few Duplicants.\nReleased for Klei Fest 2023.";
					}

					// Token: 0x02003D7B RID: 15739
					public class CLUBSHIRT_PURPLE
					{
						// Token: 0x0400F030 RID: 61488
						public static LocString NAME = "Eggplant Atmo Helmet";

						// Token: 0x0400F031 RID: 61489
						public static LocString DESC = "It is neither an egg, nor a plant. But it <i>is</i> a functional helmet.";
					}

					// Token: 0x02003D7C RID: 15740
					public class TRIANGLES_TURQ
					{
						// Token: 0x0400F032 RID: 61490
						public static LocString NAME = "Confetti Atmo Helmet";

						// Token: 0x0400F033 RID: 61491
						public static LocString DESC = "Doubles as a party hat.";
					}

					// Token: 0x02003D7D RID: 15741
					public class CUMMERBUND_RED
					{
						// Token: 0x0400F034 RID: 61492
						public static LocString NAME = "Blastoff Atmo Helmet";

						// Token: 0x0400F035 RID: 61493
						public static LocString DESC = "Red means go!";
					}

					// Token: 0x02003D7E RID: 15742
					public class WORKOUT_LAVENDER
					{
						// Token: 0x0400F036 RID: 61494
						public static LocString NAME = "Pink Punch Atmo Helmet";

						// Token: 0x0400F037 RID: 61495
						public static LocString DESC = "Unapologetically ostentatious.";
					}

					// Token: 0x02003D7F RID: 15743
					public class CANTALOUPE
					{
						// Token: 0x0400F038 RID: 61496
						public static LocString NAME = "Rocketmelon Atmo Helmet";

						// Token: 0x0400F039 RID: 61497
						public static LocString DESC = "A melon for your melon.";
					}

					// Token: 0x02003D80 RID: 15744
					public class MONDRIAN_BLUE_RED_YELLOW
					{
						// Token: 0x0400F03A RID: 61498
						public static LocString NAME = "Cubist Atmo Helmet";

						// Token: 0x0400F03B RID: 61499
						public static LocString DESC = "Abstract geometrics are both hip <i>and</i> square.";
					}

					// Token: 0x02003D81 RID: 15745
					public class OVERALLS_RED
					{
						// Token: 0x0400F03C RID: 61500
						public static LocString NAME = "Spiffy Atmo Helmet";

						// Token: 0x0400F03D RID: 61501
						public static LocString DESC = "The twin antennae serve as an early warning system for low ceilings.";
					}
				}
			}

			// Token: 0x02002C74 RID: 11380
			public class ATMO_SUIT_BODY
			{
				// Token: 0x0400C06A RID: 49258
				public static LocString NAME = "Default Atmo Uniform";

				// Token: 0x0400C06B RID: 49259
				public static LocString DESC = "Default top and bottom of an atmo suit.";

				// Token: 0x0200393C RID: 14652
				public class FACADES
				{
					// Token: 0x02003D82 RID: 15746
					public class SPARKLE_RED
					{
						// Token: 0x0400F03E RID: 61502
						public static LocString NAME = "Red Glitter Atmo Suit";

						// Token: 0x0400F03F RID: 61503
						public static LocString DESC = "Protects the wearer from hostile environments <i>and</i> drab fashion.";
					}

					// Token: 0x02003D83 RID: 15747
					public class SPARKLE_GREEN
					{
						// Token: 0x0400F040 RID: 61504
						public static LocString NAME = "Green Glitter Atmo Suit";

						// Token: 0x0400F041 RID: 61505
						public static LocString DESC = "Protects the wearer from hostile environments <i>and</i> drab fashion.";
					}

					// Token: 0x02003D84 RID: 15748
					public class SPARKLE_BLUE
					{
						// Token: 0x0400F042 RID: 61506
						public static LocString NAME = "Blue Glitter Atmo Suit";

						// Token: 0x0400F043 RID: 61507
						public static LocString DESC = "Protects the wearer from hostile environments <i>and</i> drab fashion.";
					}

					// Token: 0x02003D85 RID: 15749
					public class SPARKLE_LAVENDER
					{
						// Token: 0x0400F044 RID: 61508
						public static LocString NAME = "Violet Glitter Atmo Suit";

						// Token: 0x0400F045 RID: 61509
						public static LocString DESC = "Protects the wearer from hostile environments <i>and</i> drab fashion.";
					}

					// Token: 0x02003D86 RID: 15750
					public class LIMONE
					{
						// Token: 0x0400F046 RID: 61510
						public static LocString NAME = "Citrus Atmo Suit";

						// Token: 0x0400F047 RID: 61511
						public static LocString DESC = "Perfect for summery, atmospheric excursions.";
					}

					// Token: 0x02003D87 RID: 15751
					public class PUFT
					{
						// Token: 0x0400F048 RID: 61512
						public static LocString NAME = "Puft Atmo Suit";

						// Token: 0x0400F049 RID: 61513
						public static LocString DESC = "Warning: prolonged wear may result in feelings of Puft-up pride.\nReleased for Klei Fest 2023.";
					}

					// Token: 0x02003D88 RID: 15752
					public class BASIC_PURPLE
					{
						// Token: 0x0400F04A RID: 61514
						public static LocString NAME = "Crisp Eggplant Atmo Suit";

						// Token: 0x0400F04B RID: 61515
						public static LocString DESC = "It really emphasizes wide shoulders.";
					}

					// Token: 0x02003D89 RID: 15753
					public class PRINT_TRIANGLES_TURQ
					{
						// Token: 0x0400F04C RID: 61516
						public static LocString NAME = "Confetti Atmo Suit";

						// Token: 0x0400F04D RID: 61517
						public static LocString DESC = "It puts the \"fun\" in \"perfunctory nods to personnel individuality\"!";
					}

					// Token: 0x02003D8A RID: 15754
					public class BASIC_NEON_PINK
					{
						// Token: 0x0400F04E RID: 61518
						public static LocString NAME = "Crisp Neon Pink Atmo Suit";

						// Token: 0x0400F04F RID: 61519
						public static LocString DESC = "The neck is a little snug.";
					}

					// Token: 0x02003D8B RID: 15755
					public class MULTI_RED_BLACK
					{
						// Token: 0x0400F050 RID: 61520
						public static LocString NAME = "Red-bellied Atmo Suit";

						// Token: 0x0400F051 RID: 61521
						public static LocString DESC = "It really highlights the midsection.";
					}

					// Token: 0x02003D8C RID: 15756
					public class CANTALOUPE
					{
						// Token: 0x0400F052 RID: 61522
						public static LocString NAME = "Rocketmelon Atmo Suit";

						// Token: 0x0400F053 RID: 61523
						public static LocString DESC = "It starts to smell ripe pretty quickly.";
					}

					// Token: 0x02003D8D RID: 15757
					public class MULTI_BLUE_GREY_BLACK
					{
						// Token: 0x0400F054 RID: 61524
						public static LocString NAME = "Swagger Atmo Suit";

						// Token: 0x0400F055 RID: 61525
						public static LocString DESC = "Engineered to resemble stonewashed denim and black leather.";
					}

					// Token: 0x02003D8E RID: 15758
					public class MULTI_BLUE_YELLOW_RED
					{
						// Token: 0x0400F056 RID: 61526
						public static LocString NAME = "Fundamental Stripe Atmo Suit";

						// Token: 0x0400F057 RID: 61527
						public static LocString DESC = "Designed by the Primary Colors Appreciation Society.";
					}
				}
			}

			// Token: 0x02002C75 RID: 11381
			public class ATMO_SUIT_GLOVES
			{
				// Token: 0x0400C06C RID: 49260
				public static LocString NAME = "Default Atmo Gloves";

				// Token: 0x0400C06D RID: 49261
				public static LocString DESC = "Default atmo suit gloves.";

				// Token: 0x0200393D RID: 14653
				public class FACADES
				{
					// Token: 0x02003D8F RID: 15759
					public class SPARKLE_RED
					{
						// Token: 0x0400F058 RID: 61528
						public static LocString NAME = "Red Glitter Atmo Gloves";

						// Token: 0x0400F059 RID: 61529
						public static LocString DESC = "Sparkly red gloves for hostile environments.";
					}

					// Token: 0x02003D90 RID: 15760
					public class SPARKLE_GREEN
					{
						// Token: 0x0400F05A RID: 61530
						public static LocString NAME = "Green Glitter Atmo Gloves";

						// Token: 0x0400F05B RID: 61531
						public static LocString DESC = "Sparkly green gloves for hostile environments.";
					}

					// Token: 0x02003D91 RID: 15761
					public class SPARKLE_BLUE
					{
						// Token: 0x0400F05C RID: 61532
						public static LocString NAME = "Blue Glitter Atmo Gloves";

						// Token: 0x0400F05D RID: 61533
						public static LocString DESC = "Sparkly blue gloves for hostile environments.";
					}

					// Token: 0x02003D92 RID: 15762
					public class SPARKLE_LAVENDER
					{
						// Token: 0x0400F05E RID: 61534
						public static LocString NAME = "Violet Glitter Atmo Gloves";

						// Token: 0x0400F05F RID: 61535
						public static LocString DESC = "Sparkly violet gloves for hostile environments.";
					}

					// Token: 0x02003D93 RID: 15763
					public class LIMONE
					{
						// Token: 0x0400F060 RID: 61536
						public static LocString NAME = "Citrus Atmo Gloves";

						// Token: 0x0400F061 RID: 61537
						public static LocString DESC = "Lime-inspired gloves brighten up hostile environments.";
					}

					// Token: 0x02003D94 RID: 15764
					public class PUFT
					{
						// Token: 0x0400F062 RID: 61538
						public static LocString NAME = "Puft Atmo Gloves";

						// Token: 0x0400F063 RID: 61539
						public static LocString DESC = "A little Puft-love for delicate extremities.\nReleased for Klei Fest 2023.";
					}

					// Token: 0x02003D95 RID: 15765
					public class GOLD
					{
						// Token: 0x0400F064 RID: 61540
						public static LocString NAME = "Gold Atmo Gloves";

						// Token: 0x0400F065 RID: 61541
						public static LocString DESC = "A golden touch! Without all the Midas-type baggage.";
					}

					// Token: 0x02003D96 RID: 15766
					public class PURPLE
					{
						// Token: 0x0400F066 RID: 61542
						public static LocString NAME = "Eggplant Atmo Gloves";

						// Token: 0x0400F067 RID: 61543
						public static LocString DESC = "Fab purple gloves for hostile environments.";
					}

					// Token: 0x02003D97 RID: 15767
					public class WHITE
					{
						// Token: 0x0400F068 RID: 61544
						public static LocString NAME = "White Atmo Gloves";

						// Token: 0x0400F069 RID: 61545
						public static LocString DESC = "For the Duplicant who never gets their hands dirty.";
					}

					// Token: 0x02003D98 RID: 15768
					public class STRIPES_LAVENDER
					{
						// Token: 0x0400F06A RID: 61546
						public static LocString NAME = "Wildberry Atmo Gloves";

						// Token: 0x0400F06B RID: 61547
						public static LocString DESC = "Functional finger-protectors with fruity flair.";
					}

					// Token: 0x02003D99 RID: 15769
					public class CANTALOUPE
					{
						// Token: 0x0400F06C RID: 61548
						public static LocString NAME = "Rocketmelon Atmo Gloves";

						// Token: 0x0400F06D RID: 61549
						public static LocString DESC = "It takes eighteen melon rinds to make a single glove.";
					}

					// Token: 0x02003D9A RID: 15770
					public class BROWN
					{
						// Token: 0x0400F06E RID: 61550
						public static LocString NAME = "Leather Atmo Gloves";

						// Token: 0x0400F06F RID: 61551
						public static LocString DESC = "They creak rather loudly during the break-in period.";
					}
				}
			}

			// Token: 0x02002C76 RID: 11382
			public class ATMO_SUIT_BELT
			{
				// Token: 0x0400C06E RID: 49262
				public static LocString NAME = "Default Atmo Belt";

				// Token: 0x0400C06F RID: 49263
				public static LocString DESC = "Default belt for atmo suits.";

				// Token: 0x0200393E RID: 14654
				public class FACADES
				{
					// Token: 0x02003D9B RID: 15771
					public class SPARKLE_RED
					{
						// Token: 0x0400F070 RID: 61552
						public static LocString NAME = "Red Glitter Atmo Belt";

						// Token: 0x0400F071 RID: 61553
						public static LocString DESC = "It's red! It's shiny! It keeps atmo suit pants on!";
					}

					// Token: 0x02003D9C RID: 15772
					public class SPARKLE_GREEN
					{
						// Token: 0x0400F072 RID: 61554
						public static LocString NAME = "Green Glitter Atmo Belt";

						// Token: 0x0400F073 RID: 61555
						public static LocString DESC = "It's green! It's shiny! It keeps atmo suit pants on!";
					}

					// Token: 0x02003D9D RID: 15773
					public class SPARKLE_BLUE
					{
						// Token: 0x0400F074 RID: 61556
						public static LocString NAME = "Blue Glitter Atmo Belt";

						// Token: 0x0400F075 RID: 61557
						public static LocString DESC = "It's blue! It's shiny! It keeps atmo suit pants on!";
					}

					// Token: 0x02003D9E RID: 15774
					public class SPARKLE_LAVENDER
					{
						// Token: 0x0400F076 RID: 61558
						public static LocString NAME = "Violet Glitter Atmo Belt";

						// Token: 0x0400F077 RID: 61559
						public static LocString DESC = "It's violet! It's shiny! It keeps atmo suit pants on!";
					}

					// Token: 0x02003D9F RID: 15775
					public class LIMONE
					{
						// Token: 0x0400F078 RID: 61560
						public static LocString NAME = "Citrus Atmo Belt";

						// Token: 0x0400F079 RID: 61561
						public static LocString DESC = "This lime-hued belt really pulls an atmo suit together.";
					}

					// Token: 0x02003DA0 RID: 15776
					public class PUFT
					{
						// Token: 0x0400F07A RID: 61562
						public static LocString NAME = "Puft Atmo Belt";

						// Token: 0x0400F07B RID: 61563
						public static LocString DESC = "If critters wore belts...\nReleased for Klei Fest 2023.";
					}

					// Token: 0x02003DA1 RID: 15777
					public class TWOTONE_PURPLE
					{
						// Token: 0x0400F07C RID: 61564
						public static LocString NAME = "Eggplant Atmo Belt";

						// Token: 0x0400F07D RID: 61565
						public static LocString DESC = "In the more pretentious space-fashion circles, it's known as \"aubergine.\"";
					}

					// Token: 0x02003DA2 RID: 15778
					public class BASIC_GOLD
					{
						// Token: 0x0400F07E RID: 61566
						public static LocString NAME = "Gold Atmo Belt";

						// Token: 0x0400F07F RID: 61567
						public static LocString DESC = "Better to be overdressed than underdressed.";
					}

					// Token: 0x02003DA3 RID: 15779
					public class BASIC_GREY
					{
						// Token: 0x0400F080 RID: 61568
						public static LocString NAME = "Slate Atmo Belt";

						// Token: 0x0400F081 RID: 61569
						public static LocString DESC = "Slick and understated space style.";
					}

					// Token: 0x02003DA4 RID: 15780
					public class BASIC_NEON_PINK
					{
						// Token: 0x0400F082 RID: 61570
						public static LocString NAME = "Neon Pink Atmo Belt";

						// Token: 0x0400F083 RID: 61571
						public static LocString DESC = "Visible from several planetoids away.";
					}

					// Token: 0x02003DA5 RID: 15781
					public class CANTALOUPE
					{
						// Token: 0x0400F084 RID: 61572
						public static LocString NAME = "Rocketmelon Atmo Belt";

						// Token: 0x0400F085 RID: 61573
						public static LocString DESC = "A tribute to the <i>cucumis melo cantalupensis</i>.";
					}

					// Token: 0x02003DA6 RID: 15782
					public class TWOTONE_BROWN
					{
						// Token: 0x0400F086 RID: 61574
						public static LocString NAME = "Leather Atmo Belt";

						// Token: 0x0400F087 RID: 61575
						public static LocString DESC = "Crafted from the tanned hide of a thick-skinned critter.";
					}
				}
			}

			// Token: 0x02002C77 RID: 11383
			public class ATMO_SUIT_SHOES
			{
				// Token: 0x0400C070 RID: 49264
				public static LocString NAME = "Default Atmo Boots";

				// Token: 0x0400C071 RID: 49265
				public static LocString DESC = "Default footwear for atmo suits.";

				// Token: 0x0200393F RID: 14655
				public class FACADES
				{
					// Token: 0x02003DA7 RID: 15783
					public class LIMONE
					{
						// Token: 0x0400F088 RID: 61576
						public static LocString NAME = "Citrus Atmo Boots";

						// Token: 0x0400F089 RID: 61577
						public static LocString DESC = "Cheery boots for stomping around in hostile environments.";
					}

					// Token: 0x02003DA8 RID: 15784
					public class PUFT
					{
						// Token: 0x0400F08A RID: 61578
						public static LocString NAME = "Puft Atmo Boots";

						// Token: 0x0400F08B RID: 61579
						public static LocString DESC = "These boots were made for puft-ing.\nReleased for Klei Fest 2023.";
					}

					// Token: 0x02003DA9 RID: 15785
					public class SPARKLE_BLACK
					{
						// Token: 0x0400F08C RID: 61580
						public static LocString NAME = "Black Glitter Atmo Boots";

						// Token: 0x0400F08D RID: 61581
						public static LocString DESC = "A timeless color, with a little pizzazz.";
					}

					// Token: 0x02003DAA RID: 15786
					public class BASIC_BLACK
					{
						// Token: 0x0400F08E RID: 61582
						public static LocString NAME = "Stealth Atmo Boots";

						// Token: 0x0400F08F RID: 61583
						public static LocString DESC = "They attract no attention at all.";
					}

					// Token: 0x02003DAB RID: 15787
					public class BASIC_PURPLE
					{
						// Token: 0x0400F090 RID: 61584
						public static LocString NAME = "Eggplant Atmo Boots";

						// Token: 0x0400F091 RID: 61585
						public static LocString DESC = "Purple boots for stomping around in hostile environments.";
					}

					// Token: 0x02003DAC RID: 15788
					public class BASIC_LAVENDER
					{
						// Token: 0x0400F092 RID: 61586
						public static LocString NAME = "Lavender Atmo Boots";

						// Token: 0x0400F093 RID: 61587
						public static LocString DESC = "Soothing space booties for tired feet.";
					}

					// Token: 0x02003DAD RID: 15789
					public class CANTALOUPE
					{
						// Token: 0x0400F094 RID: 61588
						public static LocString NAME = "Rocketmelon Atmo Boots";

						// Token: 0x0400F095 RID: 61589
						public static LocString DESC = "Keeps feet safe (and juicy) in hostile environments.";
					}
				}
			}

			// Token: 0x02002C78 RID: 11384
			public class AQUA_SUIT
			{
				// Token: 0x0400C072 RID: 49266
				public static LocString NAME = UI.FormatAsLink("Aqua Suit", "AQUA_SUIT");

				// Token: 0x0400C073 RID: 49267
				public static LocString DESC = "Because breathing underwater is better than... not.";

				// Token: 0x0400C074 RID: 49268
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Supplies Duplicants with <style=\"oxygen\">Oxygen</style> in underwater environments.\n\nMust be refilled with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" at an ",
					UI.FormatAsLink("Atmo Suit Dock", "SUITLOCKER"),
					" when depleted."
				});

				// Token: 0x0400C075 RID: 49269
				public static LocString RECIPE_DESC = "Supplies Duplicants with <style=\"oxygen\">Oxygen</style> in underwater environments.";

				// Token: 0x0400C076 RID: 49270
				public static LocString WORN_NAME = UI.FormatAsLink("Worn Lead Suit", "AQUA_SUIT");

				// Token: 0x0400C077 RID: 49271
				public static LocString WORN_DESC = string.Concat(new string[]
				{
					"A worn out ",
					UI.FormatAsLink("Aqua Suit", "AQUA_SUIT"),
					".\n\nSuits can be repaired at a ",
					UI.FormatAsLink("Crafting Station", "CRAFTINGTABLE"),
					"."
				});
			}

			// Token: 0x02002C79 RID: 11385
			public class TEMPERATURE_SUIT
			{
				// Token: 0x0400C078 RID: 49272
				public static LocString NAME = UI.FormatAsLink("Thermo Suit", "TEMPERATURE_SUIT");

				// Token: 0x0400C079 RID: 49273
				public static LocString DESC = "Keeps my Duplicants cool in case things heat up.";

				// Token: 0x0400C07A RID: 49274
				public static LocString EFFECT = "Provides insulation in regions with extreme <style=\"heat\">Temperatures</style>.\n\nMust be powered at a Thermo Suit Dock when depleted.";

				// Token: 0x0400C07B RID: 49275
				public static LocString RECIPE_DESC = "Provides insulation in regions with extreme <style=\"heat\">Temperatures</style>.";

				// Token: 0x0400C07C RID: 49276
				public static LocString WORN_NAME = UI.FormatAsLink("Worn Lead Suit", "TEMPERATURE_SUIT");

				// Token: 0x0400C07D RID: 49277
				public static LocString WORN_DESC = string.Concat(new string[]
				{
					"A worn out ",
					UI.FormatAsLink("Thermo Suit", "TEMPERATURE_SUIT"),
					".\n\nSuits can be repaired at a ",
					UI.FormatAsLink("Crafting Station", "CRAFTINGTABLE"),
					"."
				});
			}

			// Token: 0x02002C7A RID: 11386
			public class JET_SUIT
			{
				// Token: 0x0400C07E RID: 49278
				public static LocString NAME = UI.FormatAsLink("Jet Suit", "JET_SUIT");

				// Token: 0x0400C07F RID: 49279
				public static LocString DESC = "Allows my Duplicants to take to the skies, for a time.";

				// Token: 0x0400C080 RID: 49280
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Supplies Duplicants with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" in toxic and low breathability environments.\n\nMust be refilled with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" and ",
					UI.FormatAsLink("Petroleum", "PETROLEUM"),
					" at a ",
					UI.FormatAsLink("Jet Suit Dock", "JETSUITLOCKER"),
					" when depleted."
				});

				// Token: 0x0400C081 RID: 49281
				public static LocString RECIPE_DESC = "Supplies Duplicants with " + UI.FormatAsLink("Oxygen", "OXYGEN") + " in toxic and low breathability environments.\n\nAllows Duplicant flight.";

				// Token: 0x0400C082 RID: 49282
				public static LocString GENERICNAME = "Jet Suit";

				// Token: 0x0400C083 RID: 49283
				public static LocString TANK_EFFECT_NAME = "Fuel Tank";

				// Token: 0x0400C084 RID: 49284
				public static LocString WORN_NAME = UI.FormatAsLink("Worn Jet Suit", "JET_SUIT");

				// Token: 0x0400C085 RID: 49285
				public static LocString WORN_DESC = string.Concat(new string[]
				{
					"A worn out ",
					UI.FormatAsLink("Jet Suit", "JET_SUIT"),
					".\n\nSuits can be repaired at an ",
					UI.FormatAsLink("Exosuit Forge", "SUITFABRICATOR"),
					"."
				});
			}

			// Token: 0x02002C7B RID: 11387
			public class LEAD_SUIT
			{
				// Token: 0x0400C086 RID: 49286
				public static LocString NAME = UI.FormatAsLink("Lead Suit", "LEAD_SUIT");

				// Token: 0x0400C087 RID: 49287
				public static LocString DESC = "Because exposure to radiation doesn't grant Duplicants superpowers.";

				// Token: 0x0400C088 RID: 49288
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Supplies Duplicants with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" and protection in areas with ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					".\n\nMust be refilled with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" at a ",
					UI.FormatAsLink("Lead Suit Dock", "LEADSUITLOCKER"),
					" when depleted."
				});

				// Token: 0x0400C089 RID: 49289
				public static LocString RECIPE_DESC = string.Concat(new string[]
				{
					"Supplies Duplicants with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" in toxic and low breathability environments.\n\nProtects Duplicants from ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					"."
				});

				// Token: 0x0400C08A RID: 49290
				public static LocString GENERICNAME = "Lead Suit";

				// Token: 0x0400C08B RID: 49291
				public static LocString BATTERY_EFFECT_NAME = "Suit Battery";

				// Token: 0x0400C08C RID: 49292
				public static LocString SUIT_OUT_OF_BATTERIES = "Suit Batteries Empty";

				// Token: 0x0400C08D RID: 49293
				public static LocString WORN_NAME = UI.FormatAsLink("Worn Lead Suit", "LEAD_SUIT");

				// Token: 0x0400C08E RID: 49294
				public static LocString WORN_DESC = string.Concat(new string[]
				{
					"A worn out ",
					UI.FormatAsLink("Lead Suit", "LEAD_SUIT"),
					".\n\nSuits can be repaired at an ",
					UI.FormatAsLink("Exosuit Forge", "SUITFABRICATOR"),
					"."
				});
			}

			// Token: 0x02002C7C RID: 11388
			public class COOL_VEST
			{
				// Token: 0x0400C08F RID: 49295
				public static LocString NAME = UI.FormatAsLink("Cool Vest", "COOL_VEST");

				// Token: 0x0400C090 RID: 49296
				public static LocString GENERICNAME = "Clothing";

				// Token: 0x0400C091 RID: 49297
				public static LocString DESC = "Don't sweat it!";

				// Token: 0x0400C092 RID: 49298
				public static LocString EFFECT = "Protects the wearer from <style=\"heat\">Heat</style> by decreasing insulation.";

				// Token: 0x0400C093 RID: 49299
				public static LocString RECIPE_DESC = "Protects the wearer from <style=\"heat\">Heat</style> by decreasing insulation";
			}

			// Token: 0x02002C7D RID: 11389
			public class WARM_VEST
			{
				// Token: 0x0400C094 RID: 49300
				public static LocString NAME = UI.FormatAsLink("Warm Coat", "WARM_VEST");

				// Token: 0x0400C095 RID: 49301
				public static LocString GENERICNAME = "Clothing";

				// Token: 0x0400C096 RID: 49302
				public static LocString DESC = "Happiness is a warm Duplicant.";

				// Token: 0x0400C097 RID: 49303
				public static LocString EFFECT = "Protects the wearer from <style=\"heat\">Cold</style> by increasing insulation.";

				// Token: 0x0400C098 RID: 49304
				public static LocString RECIPE_DESC = "Protects the wearer from <style=\"heat\">Cold</style> by increasing insulation";
			}

			// Token: 0x02002C7E RID: 11390
			public class FUNKY_VEST
			{
				// Token: 0x0400C099 RID: 49305
				public static LocString NAME = UI.FormatAsLink("Snazzy Suit", "FUNKY_VEST");

				// Token: 0x0400C09A RID: 49306
				public static LocString GENERICNAME = "Clothing";

				// Token: 0x0400C09B RID: 49307
				public static LocString DESC = "This transforms my Duplicant into a walking beacon of charm and style.";

				// Token: 0x0400C09C RID: 49308
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Increases Decor in a small area effect around the wearer. Can be upgraded to ",
					UI.FormatAsLink("Primo Garb", "CUSTOMCLOTHING"),
					" at the ",
					UI.FormatAsLink("Clothing Refashionator", "CLOTHINGALTERATIONSTATION"),
					"."
				});

				// Token: 0x0400C09D RID: 49309
				public static LocString RECIPE_DESC = "Increases Decor in a small area effect around the wearer. Can be upgraded to " + UI.FormatAsLink("Primo Garb", "CUSTOMCLOTHING") + " at the " + UI.FormatAsLink("Clothing Refashionator", "CLOTHINGALTERATIONSTATION");
			}

			// Token: 0x02002C7F RID: 11391
			public class CUSTOMCLOTHING
			{
				// Token: 0x0400C09E RID: 49310
				public static LocString NAME = UI.FormatAsLink("Primo Garb", "CUSTOMCLOTHING");

				// Token: 0x0400C09F RID: 49311
				public static LocString GENERICNAME = "Clothing";

				// Token: 0x0400C0A0 RID: 49312
				public static LocString DESC = "This transforms my Duplicant into a colony-inspiring fashion icon.";

				// Token: 0x0400C0A1 RID: 49313
				public static LocString EFFECT = "Increases Decor in a small area effect around the wearer.";

				// Token: 0x0400C0A2 RID: 49314
				public static LocString RECIPE_DESC = "Increases Decor in a small area effect around the wearer";

				// Token: 0x02003940 RID: 14656
				public class FACADES
				{
					// Token: 0x0400E5FC RID: 58876
					public static LocString CLUBSHIRT = UI.FormatAsLink("Purple Polyester Suit", "CUSTOMCLOTHING");

					// Token: 0x0400E5FD RID: 58877
					public static LocString CUMMERBUND = UI.FormatAsLink("Classic Cummerbund", "CUSTOMCLOTHING");

					// Token: 0x0400E5FE RID: 58878
					public static LocString DECOR_02 = UI.FormatAsLink("Snazzier Red Suit", "CUSTOMCLOTHING");

					// Token: 0x0400E5FF RID: 58879
					public static LocString DECOR_03 = UI.FormatAsLink("Snazzier Blue Suit", "CUSTOMCLOTHING");

					// Token: 0x0400E600 RID: 58880
					public static LocString DECOR_04 = UI.FormatAsLink("Snazzier Green Suit", "CUSTOMCLOTHING");

					// Token: 0x0400E601 RID: 58881
					public static LocString DECOR_05 = UI.FormatAsLink("Snazzier Violet Suit", "CUSTOMCLOTHING");

					// Token: 0x0400E602 RID: 58882
					public static LocString GAUDYSWEATER = UI.FormatAsLink("Pompom Knit Suit", "CUSTOMCLOTHING");

					// Token: 0x0400E603 RID: 58883
					public static LocString LIMONE = UI.FormatAsLink("Citrus Spandex Suit", "CUSTOMCLOTHING");

					// Token: 0x0400E604 RID: 58884
					public static LocString MONDRIAN = UI.FormatAsLink("Cubist Knit Suit", "CUSTOMCLOTHING");

					// Token: 0x0400E605 RID: 58885
					public static LocString OVERALLS = UI.FormatAsLink("Spiffy Overalls", "CUSTOMCLOTHING");

					// Token: 0x0400E606 RID: 58886
					public static LocString TRIANGLES = UI.FormatAsLink("Confetti Suit", "CUSTOMCLOTHING");

					// Token: 0x0400E607 RID: 58887
					public static LocString WORKOUT = UI.FormatAsLink("Pink Unitard", "CUSTOMCLOTHING");
				}
			}

			// Token: 0x02002C80 RID: 11392
			public class CLOTHING_GLOVES
			{
				// Token: 0x0400C0A3 RID: 49315
				public static LocString NAME = "Default Gloves";

				// Token: 0x0400C0A4 RID: 49316
				public static LocString DESC = "The default gloves.";

				// Token: 0x02003941 RID: 14657
				public class FACADES
				{
					// Token: 0x02003DAE RID: 15790
					public class BASIC_BLUE_MIDDLE
					{
						// Token: 0x0400F096 RID: 61590
						public static LocString NAME = "Basic Aqua Gloves";

						// Token: 0x0400F097 RID: 61591
						public static LocString DESC = "A good, solid pair of aqua-blue gloves that go with everything.";
					}

					// Token: 0x02003DAF RID: 15791
					public class BASIC_YELLOW
					{
						// Token: 0x0400F098 RID: 61592
						public static LocString NAME = "Basic Yellow Gloves";

						// Token: 0x0400F099 RID: 61593
						public static LocString DESC = "A good, solid pair of yellow gloves that go with everything.";
					}

					// Token: 0x02003DB0 RID: 15792
					public class BASIC_BLACK
					{
						// Token: 0x0400F09A RID: 61594
						public static LocString NAME = "Basic Black Gloves";

						// Token: 0x0400F09B RID: 61595
						public static LocString DESC = "A good, solid pair of black gloves that go with everything.";
					}

					// Token: 0x02003DB1 RID: 15793
					public class BASIC_PINK_ORCHID
					{
						// Token: 0x0400F09C RID: 61596
						public static LocString NAME = "Basic Bubblegum Gloves";

						// Token: 0x0400F09D RID: 61597
						public static LocString DESC = "A good, solid pair of bubblegum-pink gloves that go with everything.";
					}

					// Token: 0x02003DB2 RID: 15794
					public class BASIC_GREEN
					{
						// Token: 0x0400F09E RID: 61598
						public static LocString NAME = "Basic Green Gloves";

						// Token: 0x0400F09F RID: 61599
						public static LocString DESC = "A good, solid pair of green gloves that go with everything.";
					}

					// Token: 0x02003DB3 RID: 15795
					public class BASIC_ORANGE
					{
						// Token: 0x0400F0A0 RID: 61600
						public static LocString NAME = "Basic Orange Gloves";

						// Token: 0x0400F0A1 RID: 61601
						public static LocString DESC = "A good, solid pair of orange gloves that go with everything.";
					}

					// Token: 0x02003DB4 RID: 15796
					public class BASIC_PURPLE
					{
						// Token: 0x0400F0A2 RID: 61602
						public static LocString NAME = "Basic Purple Gloves";

						// Token: 0x0400F0A3 RID: 61603
						public static LocString DESC = "A good, solid pair of purple gloves that go with everything.";
					}

					// Token: 0x02003DB5 RID: 15797
					public class BASIC_RED
					{
						// Token: 0x0400F0A4 RID: 61604
						public static LocString NAME = "Basic Red Gloves";

						// Token: 0x0400F0A5 RID: 61605
						public static LocString DESC = "A good, solid pair of red gloves that go with everything.";
					}

					// Token: 0x02003DB6 RID: 15798
					public class BASIC_WHITE
					{
						// Token: 0x0400F0A6 RID: 61606
						public static LocString NAME = "Basic White Gloves";

						// Token: 0x0400F0A7 RID: 61607
						public static LocString DESC = "A good, solid pair of white gloves that go with everything.";
					}

					// Token: 0x02003DB7 RID: 15799
					public class GLOVES_ATHLETIC_DEEPRED
					{
						// Token: 0x0400F0A8 RID: 61608
						public static LocString NAME = "Team Captain Sports Gloves";

						// Token: 0x0400F0A9 RID: 61609
						public static LocString DESC = "Red-striped gloves for winning at any activity.";
					}

					// Token: 0x02003DB8 RID: 15800
					public class GLOVES_ATHLETIC_SATSUMA
					{
						// Token: 0x0400F0AA RID: 61610
						public static LocString NAME = "Superfan Sports Gloves";

						// Token: 0x0400F0AB RID: 61611
						public static LocString DESC = "Orange-striped gloves for enthusiastic athletes.";
					}

					// Token: 0x02003DB9 RID: 15801
					public class GLOVES_ATHLETIC_LEMON
					{
						// Token: 0x0400F0AC RID: 61612
						public static LocString NAME = "Hype Sports Gloves";

						// Token: 0x0400F0AD RID: 61613
						public static LocString DESC = "Yellow-striped gloves for athletes who seek to raise the bar.";
					}

					// Token: 0x02003DBA RID: 15802
					public class GLOVES_ATHLETIC_KELLYGREEN
					{
						// Token: 0x0400F0AE RID: 61614
						public static LocString NAME = "Go Team Sports Gloves";

						// Token: 0x0400F0AF RID: 61615
						public static LocString DESC = "Green-striped gloves for the perenially good sport.";
					}

					// Token: 0x02003DBB RID: 15803
					public class GLOVES_ATHLETIC_COBALT
					{
						// Token: 0x0400F0B0 RID: 61616
						public static LocString NAME = "True Blue Sports Gloves";

						// Token: 0x0400F0B1 RID: 61617
						public static LocString DESC = "Blue-striped gloves perfect for shaking hands after the game.";
					}

					// Token: 0x02003DBC RID: 15804
					public class GLOVES_ATHLETIC_FLAMINGO
					{
						// Token: 0x0400F0B2 RID: 61618
						public static LocString NAME = "Pep Rally Sports Gloves";

						// Token: 0x0400F0B3 RID: 61619
						public static LocString DESC = "Pink-striped glove designed to withstand countless high-fives.";
					}

					// Token: 0x02003DBD RID: 15805
					public class GLOVES_ATHLETIC_CHARCOAL
					{
						// Token: 0x0400F0B4 RID: 61620
						public static LocString NAME = "Underdog Sports Gloves";

						// Token: 0x0400F0B5 RID: 61621
						public static LocString DESC = "The muted stripe minimizes distractions so its wearer can focus on trying very, very hard.";
					}

					// Token: 0x02003DBE RID: 15806
					public class CUFFLESS_BLUEBERRY
					{
						// Token: 0x0400F0B6 RID: 61622
						public static LocString NAME = "Blueberry Glovelets";

						// Token: 0x0400F0B7 RID: 61623
						public static LocString DESC = "Wrist coverage is <i>so</i> overrated.";
					}

					// Token: 0x02003DBF RID: 15807
					public class CUFFLESS_GRAPE
					{
						// Token: 0x0400F0B8 RID: 61624
						public static LocString NAME = "Grape Glovelets";

						// Token: 0x0400F0B9 RID: 61625
						public static LocString DESC = "Wrist coverage is <i>so</i> overrated.";
					}

					// Token: 0x02003DC0 RID: 15808
					public class CUFFLESS_LEMON
					{
						// Token: 0x0400F0BA RID: 61626
						public static LocString NAME = "Lemon Glovelets";

						// Token: 0x0400F0BB RID: 61627
						public static LocString DESC = "Wrist coverage is <i>so</i> overrated.";
					}

					// Token: 0x02003DC1 RID: 15809
					public class CUFFLESS_LIME
					{
						// Token: 0x0400F0BC RID: 61628
						public static LocString NAME = "Lime Glovelets";

						// Token: 0x0400F0BD RID: 61629
						public static LocString DESC = "Wrist coverage is <i>so</i> overrated.";
					}

					// Token: 0x02003DC2 RID: 15810
					public class CUFFLESS_SATSUMA
					{
						// Token: 0x0400F0BE RID: 61630
						public static LocString NAME = "Satsuma Glovelets";

						// Token: 0x0400F0BF RID: 61631
						public static LocString DESC = "Wrist coverage is <i>so</i> overrated.";
					}

					// Token: 0x02003DC3 RID: 15811
					public class CUFFLESS_STRAWBERRY
					{
						// Token: 0x0400F0C0 RID: 61632
						public static LocString NAME = "Strawberry Glovelets";

						// Token: 0x0400F0C1 RID: 61633
						public static LocString DESC = "Wrist coverage is <i>so</i> overrated.";
					}

					// Token: 0x02003DC4 RID: 15812
					public class CUFFLESS_WATERMELON
					{
						// Token: 0x0400F0C2 RID: 61634
						public static LocString NAME = "Watermelon Glovelets";

						// Token: 0x0400F0C3 RID: 61635
						public static LocString DESC = "Wrist coverage is <i>so</i> overrated.";
					}

					// Token: 0x02003DC5 RID: 15813
					public class CIRCUIT_GREEN
					{
						// Token: 0x0400F0C4 RID: 61636
						public static LocString NAME = "LED Gloves";

						// Token: 0x0400F0C5 RID: 61637
						public static LocString DESC = "Great for gesticulating at parties.";
					}

					// Token: 0x02003DC6 RID: 15814
					public class ATHLETE
					{
						// Token: 0x0400F0C6 RID: 61638
						public static LocString NAME = "Racing Gloves";

						// Token: 0x0400F0C7 RID: 61639
						public static LocString DESC = "Crafted for high-speed handshakes.";
					}

					// Token: 0x02003DC7 RID: 15815
					public class BASIC_BROWN_KHAKI
					{
						// Token: 0x0400F0C8 RID: 61640
						public static LocString NAME = "Basic Khaki Gloves";

						// Token: 0x0400F0C9 RID: 61641
						public static LocString DESC = "They don't show dirt.";
					}

					// Token: 0x02003DC8 RID: 15816
					public class BASIC_BLUEGREY
					{
						// Token: 0x0400F0CA RID: 61642
						public static LocString NAME = "Basic Gunmetal Gloves";

						// Token: 0x0400F0CB RID: 61643
						public static LocString DESC = "A tough name for soft gloves.";
					}

					// Token: 0x02003DC9 RID: 15817
					public class CUFFLESS_BLACK
					{
						// Token: 0x0400F0CC RID: 61644
						public static LocString NAME = "Stealth Glovelets";

						// Token: 0x0400F0CD RID: 61645
						public static LocString DESC = "It's easy to forget they're even on.";
					}

					// Token: 0x02003DCA RID: 15818
					public class DENIM_BLUE
					{
						// Token: 0x0400F0CE RID: 61646
						public static LocString NAME = "Denim Gloves";

						// Token: 0x0400F0CF RID: 61647
						public static LocString DESC = "They're not great for dexterity.";
					}

					// Token: 0x02003DCB RID: 15819
					public class BASIC_GREY
					{
						// Token: 0x0400F0D0 RID: 61648
						public static LocString NAME = "Basic Gray Gloves";

						// Token: 0x0400F0D1 RID: 61649
						public static LocString DESC = "A good, solid pair of gray gloves that go with everything.";
					}

					// Token: 0x02003DCC RID: 15820
					public class BASIC_PINKSALMON
					{
						// Token: 0x0400F0D2 RID: 61650
						public static LocString NAME = "Basic Coral Gloves";

						// Token: 0x0400F0D3 RID: 61651
						public static LocString DESC = "A good, solid pair of bright pink gloves that go with everything.";
					}

					// Token: 0x02003DCD RID: 15821
					public class BASIC_TAN
					{
						// Token: 0x0400F0D4 RID: 61652
						public static LocString NAME = "Basic Tan Gloves";

						// Token: 0x0400F0D5 RID: 61653
						public static LocString DESC = "A good, solid pair of tan gloves that go with everything.";
					}

					// Token: 0x02003DCE RID: 15822
					public class BALLERINA_PINK
					{
						// Token: 0x0400F0D6 RID: 61654
						public static LocString NAME = "Ballet Gloves";

						// Token: 0x0400F0D7 RID: 61655
						public static LocString DESC = "Wrist ruffles highlight the poetic movements of the phalanges.";
					}

					// Token: 0x02003DCF RID: 15823
					public class FORMAL_WHITE
					{
						// Token: 0x0400F0D8 RID: 61656
						public static LocString NAME = "White Silk Gloves";

						// Token: 0x0400F0D9 RID: 61657
						public static LocString DESC = "They're as soft as...well, silk.";
					}

					// Token: 0x02003DD0 RID: 15824
					public class LONG_WHITE
					{
						// Token: 0x0400F0DA RID: 61658
						public static LocString NAME = "White Evening Gloves";

						// Token: 0x0400F0DB RID: 61659
						public static LocString DESC = "Super-long gloves for super-formal occasions.";
					}

					// Token: 0x02003DD1 RID: 15825
					public class TWOTONE_CREAM_CHARCOAL
					{
						// Token: 0x0400F0DC RID: 61660
						public static LocString NAME = "Contrast Cuff Gloves";

						// Token: 0x0400F0DD RID: 61661
						public static LocString DESC = "For elegance so understated, it may go completely unnoticed.";
					}

					// Token: 0x02003DD2 RID: 15826
					public class SOCKSUIT_BEIGE
					{
						// Token: 0x0400F0DE RID: 61662
						public static LocString NAME = "Vintage Handsock";

						// Token: 0x0400F0DF RID: 61663
						public static LocString DESC = "Designed by someone with cold hands and an excess of old socks.";
					}

					// Token: 0x02003DD3 RID: 15827
					public class BASIC_SLATE
					{
						// Token: 0x0400F0E0 RID: 61664
						public static LocString NAME = "Basic Slate Gloves";

						// Token: 0x0400F0E1 RID: 61665
						public static LocString DESC = "A good, solid pair of slate gloves that go with everything.";
					}

					// Token: 0x02003DD4 RID: 15828
					public class KNIT_GOLD
					{
						// Token: 0x0400F0E2 RID: 61666
						public static LocString NAME = "Gold Knit Gloves";

						// Token: 0x0400F0E3 RID: 61667
						public static LocString DESC = "Produces a pleasantly muffled \"whump\" when high-fiving.";
					}

					// Token: 0x02003DD5 RID: 15829
					public class KNIT_MAGENTA
					{
						// Token: 0x0400F0E4 RID: 61668
						public static LocString NAME = "Magenta Knit Gloves";

						// Token: 0x0400F0E5 RID: 61669
						public static LocString DESC = "Produces a pleasantly muffled \"whump\" when high-fiving.";
					}

					// Token: 0x02003DD6 RID: 15830
					public class SPARKLE_WHITE
					{
						// Token: 0x0400F0E6 RID: 61670
						public static LocString NAME = "White Glitter Gloves";

						// Token: 0x0400F0E7 RID: 61671
						public static LocString DESC = "Each sequin was attached using sealant borrowed from the rocketry department.";
					}

					// Token: 0x02003DD7 RID: 15831
					public class GINCH_PINK_SALTROCK
					{
						// Token: 0x0400F0E8 RID: 61672
						public static LocString NAME = "Frilly Saltrock Gloves";

						// Token: 0x0400F0E9 RID: 61673
						public static LocString DESC = "Thick, soft pink gloves with added flounce.";
					}

					// Token: 0x02003DD8 RID: 15832
					public class GINCH_PURPLE_DUSKY
					{
						// Token: 0x0400F0EA RID: 61674
						public static LocString NAME = "Frilly Dusk Gloves";

						// Token: 0x0400F0EB RID: 61675
						public static LocString DESC = "Thick, soft purple gloves with added flounce.";
					}

					// Token: 0x02003DD9 RID: 15833
					public class GINCH_BLUE_BASIN
					{
						// Token: 0x0400F0EC RID: 61676
						public static LocString NAME = "Frilly Basin Gloves";

						// Token: 0x0400F0ED RID: 61677
						public static LocString DESC = "Thick, soft blue gloves with added flounce.";
					}

					// Token: 0x02003DDA RID: 15834
					public class GINCH_TEAL_BALMY
					{
						// Token: 0x0400F0EE RID: 61678
						public static LocString NAME = "Frilly Balm Gloves";

						// Token: 0x0400F0EF RID: 61679
						public static LocString DESC = "The soft teal fabric soothes hard-working hands.";
					}

					// Token: 0x02003DDB RID: 15835
					public class GINCH_GREEN_LIME
					{
						// Token: 0x0400F0F0 RID: 61680
						public static LocString NAME = "Frilly Leach Gloves";

						// Token: 0x0400F0F1 RID: 61681
						public static LocString DESC = "Thick, soft green gloves with added flounce.";
					}

					// Token: 0x02003DDC RID: 15836
					public class GINCH_YELLOW_YELLOWCAKE
					{
						// Token: 0x0400F0F2 RID: 61682
						public static LocString NAME = "Frilly Yellowcake Gloves";

						// Token: 0x0400F0F3 RID: 61683
						public static LocString DESC = "Thick, soft yellow gloves with added flounce.";
					}

					// Token: 0x02003DDD RID: 15837
					public class GINCH_ORANGE_ATOMIC
					{
						// Token: 0x0400F0F4 RID: 61684
						public static LocString NAME = "Frilly Atomic Gloves";

						// Token: 0x0400F0F5 RID: 61685
						public static LocString DESC = "Thick, bright orange gloves with added flounce.";
					}

					// Token: 0x02003DDE RID: 15838
					public class GINCH_RED_MAGMA
					{
						// Token: 0x0400F0F6 RID: 61686
						public static LocString NAME = "Frilly Magma Gloves";

						// Token: 0x0400F0F7 RID: 61687
						public static LocString DESC = "Thick, soft red gloves with added flounce.";
					}

					// Token: 0x02003DDF RID: 15839
					public class GINCH_GREY_GREY
					{
						// Token: 0x0400F0F8 RID: 61688
						public static LocString NAME = "Frilly Slate Gloves";

						// Token: 0x0400F0F9 RID: 61689
						public static LocString DESC = "Thick, soft grey gloves with added flounce.";
					}

					// Token: 0x02003DE0 RID: 15840
					public class GINCH_GREY_CHARCOAL
					{
						// Token: 0x0400F0FA RID: 61690
						public static LocString NAME = "Frilly Charcoal Gloves";

						// Token: 0x0400F0FB RID: 61691
						public static LocString DESC = "Thick, soft dark grey gloves with added flounce.";
					}
				}
			}

			// Token: 0x02002C81 RID: 11393
			public class CLOTHING_TOPS
			{
				// Token: 0x0400C0A5 RID: 49317
				public static LocString NAME = "Default Top";

				// Token: 0x0400C0A6 RID: 49318
				public static LocString DESC = "The default shirt.";

				// Token: 0x02003942 RID: 14658
				public class FACADES
				{
					// Token: 0x02003DE1 RID: 15841
					public class BASIC_BLUE_MIDDLE
					{
						// Token: 0x0400F0FC RID: 61692
						public static LocString NAME = "Basic Aqua Shirt";

						// Token: 0x0400F0FD RID: 61693
						public static LocString DESC = "A nice aqua-blue shirt that goes with everything.";
					}

					// Token: 0x02003DE2 RID: 15842
					public class BASIC_BLACK
					{
						// Token: 0x0400F0FE RID: 61694
						public static LocString NAME = "Basic Black Shirt";

						// Token: 0x0400F0FF RID: 61695
						public static LocString DESC = "A nice black shirt that goes with everything.";
					}

					// Token: 0x02003DE3 RID: 15843
					public class BASIC_PINK_ORCHID
					{
						// Token: 0x0400F100 RID: 61696
						public static LocString NAME = "Basic Bubblegum Shirt";

						// Token: 0x0400F101 RID: 61697
						public static LocString DESC = "A nice bubblegum-pink shirt that goes with everything.";
					}

					// Token: 0x02003DE4 RID: 15844
					public class BASIC_GREEN
					{
						// Token: 0x0400F102 RID: 61698
						public static LocString NAME = "Basic Green Shirt";

						// Token: 0x0400F103 RID: 61699
						public static LocString DESC = "A nice green shirt that goes with everything.";
					}

					// Token: 0x02003DE5 RID: 15845
					public class BASIC_ORANGE
					{
						// Token: 0x0400F104 RID: 61700
						public static LocString NAME = "Basic Orange Shirt";

						// Token: 0x0400F105 RID: 61701
						public static LocString DESC = "A nice orange shirt that goes with everything.";
					}

					// Token: 0x02003DE6 RID: 15846
					public class BASIC_PURPLE
					{
						// Token: 0x0400F106 RID: 61702
						public static LocString NAME = "Basic Purple Shirt";

						// Token: 0x0400F107 RID: 61703
						public static LocString DESC = "A nice purple shirt that goes with everything.";
					}

					// Token: 0x02003DE7 RID: 15847
					public class BASIC_RED_BURNT
					{
						// Token: 0x0400F108 RID: 61704
						public static LocString NAME = "Basic Red Shirt";

						// Token: 0x0400F109 RID: 61705
						public static LocString DESC = "A nice red shirt that goes with everything.";
					}

					// Token: 0x02003DE8 RID: 15848
					public class BASIC_WHITE
					{
						// Token: 0x0400F10A RID: 61706
						public static LocString NAME = "Basic White Shirt";

						// Token: 0x0400F10B RID: 61707
						public static LocString DESC = "A nice white shirt that goes with everything.";
					}

					// Token: 0x02003DE9 RID: 15849
					public class BASIC_YELLOW
					{
						// Token: 0x0400F10C RID: 61708
						public static LocString NAME = "Basic Yellow Shirt";

						// Token: 0x0400F10D RID: 61709
						public static LocString DESC = "A nice yellow shirt that goes with everything.";
					}

					// Token: 0x02003DEA RID: 15850
					public class RAGLANTOP_DEEPRED
					{
						// Token: 0x0400F10E RID: 61710
						public static LocString NAME = "Team Captain T-shirt";

						// Token: 0x0400F10F RID: 61711
						public static LocString DESC = "A slightly sweat-stained tee for natural leaders.";
					}

					// Token: 0x02003DEB RID: 15851
					public class RAGLANTOP_COBALT
					{
						// Token: 0x0400F110 RID: 61712
						public static LocString NAME = "True Blue T-shirt";

						// Token: 0x0400F111 RID: 61713
						public static LocString DESC = "A slightly sweat-stained tee for the real team players.";
					}

					// Token: 0x02003DEC RID: 15852
					public class RAGLANTOP_FLAMINGO
					{
						// Token: 0x0400F112 RID: 61714
						public static LocString NAME = "Pep Rally T-shirt";

						// Token: 0x0400F113 RID: 61715
						public static LocString DESC = "A slightly sweat-stained tee to boost team spirits.";
					}

					// Token: 0x02003DED RID: 15853
					public class RAGLANTOP_KELLYGREEN
					{
						// Token: 0x0400F114 RID: 61716
						public static LocString NAME = "Go Team T-shirt";

						// Token: 0x0400F115 RID: 61717
						public static LocString DESC = "A slightly sweat-stained tee for cheering from the sidelines.";
					}

					// Token: 0x02003DEE RID: 15854
					public class RAGLANTOP_CHARCOAL
					{
						// Token: 0x0400F116 RID: 61718
						public static LocString NAME = "Underdog T-shirt";

						// Token: 0x0400F117 RID: 61719
						public static LocString DESC = "For those who don't win a lot.";
					}

					// Token: 0x02003DEF RID: 15855
					public class RAGLANTOP_LEMON
					{
						// Token: 0x0400F118 RID: 61720
						public static LocString NAME = "Hype T-shirt";

						// Token: 0x0400F119 RID: 61721
						public static LocString DESC = "A slightly sweat-stained tee to wear when talking a big game.";
					}

					// Token: 0x02003DF0 RID: 15856
					public class RAGLANTOP_SATSUMA
					{
						// Token: 0x0400F11A RID: 61722
						public static LocString NAME = "Superfan T-shirt";

						// Token: 0x0400F11B RID: 61723
						public static LocString DESC = "A slightly sweat-stained tee for the long-time supporter.";
					}

					// Token: 0x02003DF1 RID: 15857
					public class JELLYPUFFJACKET_BLUEBERRY
					{
						// Token: 0x0400F11C RID: 61724
						public static LocString NAME = "Blueberry Jelly Jacket";

						// Token: 0x0400F11D RID: 61725
						public static LocString DESC = "It's best to keep jelly-filled puffer jackets away from sharp corners.";
					}

					// Token: 0x02003DF2 RID: 15858
					public class JELLYPUFFJACKET_GRAPE
					{
						// Token: 0x0400F11E RID: 61726
						public static LocString NAME = "Grape Jelly Jacket";

						// Token: 0x0400F11F RID: 61727
						public static LocString DESC = "It's best to keep jelly-filled puffer jackets away from sharp corners.";
					}

					// Token: 0x02003DF3 RID: 15859
					public class JELLYPUFFJACKET_LEMON
					{
						// Token: 0x0400F120 RID: 61728
						public static LocString NAME = "Lemon Jelly Jacket";

						// Token: 0x0400F121 RID: 61729
						public static LocString DESC = "It's best to keep jelly-filled puffer jackets away from sharp corners.";
					}

					// Token: 0x02003DF4 RID: 15860
					public class JELLYPUFFJACKET_LIME
					{
						// Token: 0x0400F122 RID: 61730
						public static LocString NAME = "Lime Jelly Jacket";

						// Token: 0x0400F123 RID: 61731
						public static LocString DESC = "It's best to keep jelly-filled puffer jackets away from sharp corners.";
					}

					// Token: 0x02003DF5 RID: 15861
					public class JELLYPUFFJACKET_SATSUMA
					{
						// Token: 0x0400F124 RID: 61732
						public static LocString NAME = "Satsuma Jelly Jacket";

						// Token: 0x0400F125 RID: 61733
						public static LocString DESC = "It's best to keep jelly-filled puffer jackets away from sharp corners.";
					}

					// Token: 0x02003DF6 RID: 15862
					public class JELLYPUFFJACKET_STRAWBERRY
					{
						// Token: 0x0400F126 RID: 61734
						public static LocString NAME = "Strawberry Jelly Jacket";

						// Token: 0x0400F127 RID: 61735
						public static LocString DESC = "It's best to keep jelly-filled puffer jackets away from sharp corners.";
					}

					// Token: 0x02003DF7 RID: 15863
					public class JELLYPUFFJACKET_WATERMELON
					{
						// Token: 0x0400F128 RID: 61736
						public static LocString NAME = "Watermelon Jelly Jacket";

						// Token: 0x0400F129 RID: 61737
						public static LocString DESC = "It's best to keep jelly-filled puffer jackets away from sharp corners.";
					}

					// Token: 0x02003DF8 RID: 15864
					public class CIRCUIT_GREEN
					{
						// Token: 0x0400F12A RID: 61738
						public static LocString NAME = "LED Jacket";

						// Token: 0x0400F12B RID: 61739
						public static LocString DESC = "For dancing in the dark.";
					}

					// Token: 0x02003DF9 RID: 15865
					public class TSHIRT_WHITE
					{
						// Token: 0x0400F12C RID: 61740
						public static LocString NAME = "Classic White Tee";

						// Token: 0x0400F12D RID: 61741
						public static LocString DESC = "It's practically begging for a big Bog Jelly stain down the front.";
					}

					// Token: 0x02003DFA RID: 15866
					public class TSHIRT_MAGENTA
					{
						// Token: 0x0400F12E RID: 61742
						public static LocString NAME = "Classic Magenta Tee";

						// Token: 0x0400F12F RID: 61743
						public static LocString DESC = "It will never chafe against delicate inner-elbow skin.";
					}

					// Token: 0x02003DFB RID: 15867
					public class ATHLETE
					{
						// Token: 0x0400F130 RID: 61744
						public static LocString NAME = "Racing Jacket";

						// Token: 0x0400F131 RID: 61745
						public static LocString DESC = "The epitome of fast fashion.";
					}

					// Token: 0x02003DFC RID: 15868
					public class DENIM_BLUE
					{
						// Token: 0x0400F132 RID: 61746
						public static LocString NAME = "Denim Jacket";

						// Token: 0x0400F133 RID: 61747
						public static LocString DESC = "The top half of a Canadian tuxedo.";
					}

					// Token: 0x02003DFD RID: 15869
					public class GONCH_STRAWBERRY
					{
						// Token: 0x0400F134 RID: 61748
						public static LocString NAME = "Executive Undershirt";

						// Token: 0x0400F135 RID: 61749
						public static LocString DESC = "The breathable base layer every power suit needs.";
					}

					// Token: 0x02003DFE RID: 15870
					public class GONCH_SATSUMA
					{
						// Token: 0x0400F136 RID: 61750
						public static LocString NAME = "Underling Undershirt";

						// Token: 0x0400F137 RID: 61751
						public static LocString DESC = "Extra-absorbent fabric in the underarms to mop up nervous sweat.";
					}

					// Token: 0x02003DFF RID: 15871
					public class GONCH_LEMON
					{
						// Token: 0x0400F138 RID: 61752
						public static LocString NAME = "Groupthink Undershirt";

						// Token: 0x0400F139 RID: 61753
						public static LocString DESC = "Because the most popular choice is always the right choice.";
					}

					// Token: 0x02003E00 RID: 15872
					public class GONCH_LIME
					{
						// Token: 0x0400F13A RID: 61754
						public static LocString NAME = "Stakeholder Undershirt";

						// Token: 0x0400F13B RID: 61755
						public static LocString DESC = "Soft against the skin, for those who have skin in the game.";
					}

					// Token: 0x02003E01 RID: 15873
					public class GONCH_BLUEBERRY
					{
						// Token: 0x0400F13C RID: 61756
						public static LocString NAME = "Admin Undershirt";

						// Token: 0x0400F13D RID: 61757
						public static LocString DESC = "Criminally underappreciated.";
					}

					// Token: 0x02003E02 RID: 15874
					public class GONCH_GRAPE
					{
						// Token: 0x0400F13E RID: 61758
						public static LocString NAME = "Buzzword Undershirt";

						// Token: 0x0400F13F RID: 61759
						public static LocString DESC = "A value-added vest for touching base and thinking outside the box using best practices ASAP.";
					}

					// Token: 0x02003E03 RID: 15875
					public class GONCH_WATERMELON
					{
						// Token: 0x0400F140 RID: 61760
						public static LocString NAME = "Synergy Undershirt";

						// Token: 0x0400F141 RID: 61761
						public static LocString DESC = "Asking for it by name often triggers dramatic eye-rolls from bystanders.";
					}

					// Token: 0x02003E04 RID: 15876
					public class NERD_BROWN
					{
						// Token: 0x0400F142 RID: 61762
						public static LocString NAME = "Research Shirt";

						// Token: 0x0400F143 RID: 61763
						public static LocString DESC = "Comes with a thoughtfully chewed-up ballpoint pen.";
					}

					// Token: 0x02003E05 RID: 15877
					public class GI_WHITE
					{
						// Token: 0x0400F144 RID: 61764
						public static LocString NAME = "Rebel Gi Jacket";

						// Token: 0x0400F145 RID: 61765
						public static LocString DESC = "The contrasting trim hides stains from messy post-sparring snacks.";
					}

					// Token: 0x02003E06 RID: 15878
					public class JACKET_SMOKING_BURGUNDY
					{
						// Token: 0x0400F146 RID: 61766
						public static LocString NAME = "Donor Jacket";

						// Token: 0x0400F147 RID: 61767
						public static LocString DESC = "Crafted from the softest, most philanthropic fibers.";
					}

					// Token: 0x02003E07 RID: 15879
					public class MECHANIC
					{
						// Token: 0x0400F148 RID: 61768
						public static LocString NAME = "Engineer Jacket";

						// Token: 0x0400F149 RID: 61769
						public static LocString DESC = "Designed to withstand the rigors of applied science.";
					}

					// Token: 0x02003E08 RID: 15880
					public class VELOUR_BLACK
					{
						// Token: 0x0400F14A RID: 61770
						public static LocString NAME = "PhD Velour Jacket";

						// Token: 0x0400F14B RID: 61771
						public static LocString DESC = "A formal jacket for those who are \"not that kind of doctor.\"";
					}

					// Token: 0x02003E09 RID: 15881
					public class VELOUR_BLUE
					{
						// Token: 0x0400F14C RID: 61772
						public static LocString NAME = "Shortwave Velour Jacket";

						// Token: 0x0400F14D RID: 61773
						public static LocString DESC = "A luxe, pettable jacket paired with a clip-on tie.";
					}

					// Token: 0x02003E0A RID: 15882
					public class VELOUR_PINK
					{
						// Token: 0x0400F14E RID: 61774
						public static LocString NAME = "Gamma Velour Jacket";

						// Token: 0x0400F14F RID: 61775
						public static LocString DESC = "Some scientists are less shy than others.";
					}

					// Token: 0x02003E0B RID: 15883
					public class WAISTCOAT_PINSTRIPE_SLATE
					{
						// Token: 0x0400F150 RID: 61776
						public static LocString NAME = "Nobel Pinstripe Waistcoat";

						// Token: 0x0400F151 RID: 61777
						public static LocString DESC = "One must dress for the prize that one wishes to win.";
					}

					// Token: 0x02003E0C RID: 15884
					public class WATER
					{
						// Token: 0x0400F152 RID: 61778
						public static LocString NAME = "HVAC Khaki Shirt";

						// Token: 0x0400F153 RID: 61779
						public static LocString DESC = "Designed to regulate temperature and humidity.";
					}

					// Token: 0x02003E0D RID: 15885
					public class TWEED_PINK_ORCHID
					{
						// Token: 0x0400F154 RID: 61780
						public static LocString NAME = "Power Brunch Blazer";

						// Token: 0x0400F155 RID: 61781
						public static LocString DESC = "Winners never quit, quitters never win.";
					}

					// Token: 0x02003E0E RID: 15886
					public class DRESS_SLEEVELESS_BOW_BW
					{
						// Token: 0x0400F156 RID: 61782
						public static LocString NAME = "PhD Dress";

						// Token: 0x0400F157 RID: 61783
						public static LocString DESC = "Ready for a post-thesis-defense party.";
					}

					// Token: 0x02003E0F RID: 15887
					public class BODYSUIT_BALLERINA_PINK
					{
						// Token: 0x0400F158 RID: 61784
						public static LocString NAME = "Ballet Leotard";

						// Token: 0x0400F159 RID: 61785
						public static LocString DESC = "Lab-crafted fabric with a level of stretchiness that defies the laws of physics.";
					}

					// Token: 0x02003E10 RID: 15888
					public class SOCKSUIT_BEIGE
					{
						// Token: 0x0400F15A RID: 61786
						public static LocString NAME = "Vintage Sockshirt";

						// Token: 0x0400F15B RID: 61787
						public static LocString DESC = "Like a sock for the torso. With sleeves.";
					}

					// Token: 0x02003E11 RID: 15889
					public class X_SPORCHID
					{
						// Token: 0x0400F15C RID: 61788
						public static LocString NAME = "Sporefest Sweater";

						// Token: 0x0400F15D RID: 61789
						public static LocString DESC = "This soft knit can be worn anytime, not just during Zombie Spore season.";
					}

					// Token: 0x02003E12 RID: 15890
					public class X1_PINCHAPEPPERNUTBELLS
					{
						// Token: 0x0400F15E RID: 61790
						public static LocString NAME = "Pinchabell Jacket";

						// Token: 0x0400F15F RID: 61791
						public static LocString DESC = "The peppernuts jingle just loudly enough to be distracting.";
					}

					// Token: 0x02003E13 RID: 15891
					public class POMPOM_SHINEBUGS_PINK_PEPPERNUT
					{
						// Token: 0x0400F160 RID: 61792
						public static LocString NAME = "Pom Bug Sweater";

						// Token: 0x0400F161 RID: 61793
						public static LocString DESC = "No Shine Bugs were harmed in the making of this sweater.";
					}

					// Token: 0x02003E14 RID: 15892
					public class SNOWFLAKE_BLUE
					{
						// Token: 0x0400F162 RID: 61794
						public static LocString NAME = "Crystal-Iced Sweater";

						// Token: 0x0400F163 RID: 61795
						public static LocString DESC = "Tiny imperfections in the front pattern ensure that no two are truly identical.";
					}

					// Token: 0x02003E15 RID: 15893
					public class PJ_CLOVERS_GLITCH_KELLY
					{
						// Token: 0x0400F164 RID: 61796
						public static LocString NAME = "Lucky Jammies";

						// Token: 0x0400F165 RID: 61797
						public static LocString DESC = "Even the most brilliant minds need a little extra luck sometimes.";
					}

					// Token: 0x02003E16 RID: 15894
					public class PJ_HEARTS_CHILLI_STRAWBERRY
					{
						// Token: 0x0400F166 RID: 61798
						public static LocString NAME = "Sweetheart Jammies";

						// Token: 0x0400F167 RID: 61799
						public static LocString DESC = "Plush chenille fabric and a drool-absorbent collar? This sleepsuit really <i>is</i> \"The One.\"";
					}

					// Token: 0x02003E17 RID: 15895
					public class BUILDER
					{
						// Token: 0x0400F168 RID: 61800
						public static LocString NAME = "Hi-Vis Jacket";

						// Token: 0x0400F169 RID: 61801
						public static LocString DESC = "Unmissable style for the safety-minded.";
					}

					// Token: 0x02003E18 RID: 15896
					public class FLORAL_PINK
					{
						// Token: 0x0400F16A RID: 61802
						public static LocString NAME = "Downtime Shirt";

						// Token: 0x0400F16B RID: 61803
						public static LocString DESC = "For maxing and relaxing when errands are too taxing.";
					}

					// Token: 0x02003E19 RID: 15897
					public class GINCH_PINK_SALTROCK
					{
						// Token: 0x0400F16C RID: 61804
						public static LocString NAME = "Frilly Saltrock Undershirt";

						// Token: 0x0400F16D RID: 61805
						public static LocString DESC = "A seamless pink undershirt with laser-cut ruffles.";
					}

					// Token: 0x02003E1A RID: 15898
					public class GINCH_PURPLE_DUSKY
					{
						// Token: 0x0400F16E RID: 61806
						public static LocString NAME = "Frilly Dusk Undershirt";

						// Token: 0x0400F16F RID: 61807
						public static LocString DESC = "A seamless purple undershirt with laser-cut ruffles.";
					}

					// Token: 0x02003E1B RID: 15899
					public class GINCH_BLUE_BASIN
					{
						// Token: 0x0400F170 RID: 61808
						public static LocString NAME = "Frilly Basin Undershirt";

						// Token: 0x0400F171 RID: 61809
						public static LocString DESC = "A seamless blue undershirt with laser-cut ruffles.";
					}

					// Token: 0x02003E1C RID: 15900
					public class GINCH_TEAL_BALMY
					{
						// Token: 0x0400F172 RID: 61810
						public static LocString NAME = "Frilly Balm Undershirt";

						// Token: 0x0400F173 RID: 61811
						public static LocString DESC = "A seamless teal undershirt with laser-cut ruffles.";
					}

					// Token: 0x02003E1D RID: 15901
					public class GINCH_GREEN_LIME
					{
						// Token: 0x0400F174 RID: 61812
						public static LocString NAME = "Frilly Leach Undershirt";

						// Token: 0x0400F175 RID: 61813
						public static LocString DESC = "A seamless green undershirt with laser-cut ruffles.";
					}

					// Token: 0x02003E1E RID: 15902
					public class GINCH_YELLOW_YELLOWCAKE
					{
						// Token: 0x0400F176 RID: 61814
						public static LocString NAME = "Frilly Yellowcake Undershirt";

						// Token: 0x0400F177 RID: 61815
						public static LocString DESC = "A seamless yellow undershirt with laser-cut ruffles.";
					}

					// Token: 0x02003E1F RID: 15903
					public class GINCH_ORANGE_ATOMIC
					{
						// Token: 0x0400F178 RID: 61816
						public static LocString NAME = "Frilly Atomic Undershirt";

						// Token: 0x0400F179 RID: 61817
						public static LocString DESC = "A seamless orange undershirt with laser-cut ruffles.";
					}

					// Token: 0x02003E20 RID: 15904
					public class GINCH_RED_MAGMA
					{
						// Token: 0x0400F17A RID: 61818
						public static LocString NAME = "Frilly Magma Undershirt";

						// Token: 0x0400F17B RID: 61819
						public static LocString DESC = "A seamless red undershirt with laser-cut ruffles.";
					}

					// Token: 0x02003E21 RID: 15905
					public class GINCH_GREY_GREY
					{
						// Token: 0x0400F17C RID: 61820
						public static LocString NAME = "Frilly Slate Undershirt";

						// Token: 0x0400F17D RID: 61821
						public static LocString DESC = "A seamless grey undershirt with laser-cut ruffles.";
					}

					// Token: 0x02003E22 RID: 15906
					public class GINCH_GREY_CHARCOAL
					{
						// Token: 0x0400F17E RID: 61822
						public static LocString NAME = "Frilly Charcoal Undershirt";

						// Token: 0x0400F17F RID: 61823
						public static LocString DESC = "A seamless dark grey undershirt with laser-cut ruffles.";
					}

					// Token: 0x02003E23 RID: 15907
					public class KNIT_POLKADOT_TURQ
					{
						// Token: 0x0400F180 RID: 61824
						public static LocString NAME = "Polka Dot Track Jacket";

						// Token: 0x0400F181 RID: 61825
						public static LocString DESC = "The dots are infused with odor-neutralizing enzymes!";
					}

					// Token: 0x02003E24 RID: 15908
					public class FLASHY
					{
						// Token: 0x0400F182 RID: 61826
						public static LocString NAME = "Superstar Jacket";

						// Token: 0x0400F183 RID: 61827
						public static LocString DESC = "Some of us were not made to be subtle.";
					}
				}
			}

			// Token: 0x02002C82 RID: 11394
			public class CLOTHING_BOTTOMS
			{
				// Token: 0x0400C0A7 RID: 49319
				public static LocString NAME = "Default Bottom";

				// Token: 0x0400C0A8 RID: 49320
				public static LocString DESC = "The default bottoms.";

				// Token: 0x02003943 RID: 14659
				public class FACADES
				{
					// Token: 0x02003E25 RID: 15909
					public class BASIC_BLUE_MIDDLE
					{
						// Token: 0x0400F184 RID: 61828
						public static LocString NAME = "Basic Aqua Pants";

						// Token: 0x0400F185 RID: 61829
						public static LocString DESC = "A clean pair of aqua-blue pants that go with everything.";
					}

					// Token: 0x02003E26 RID: 15910
					public class BASIC_PINK_ORCHID
					{
						// Token: 0x0400F186 RID: 61830
						public static LocString NAME = "Basic Bubblegum Pants";

						// Token: 0x0400F187 RID: 61831
						public static LocString DESC = "A clean pair of bubblegum-pink pants that go with everything.";
					}

					// Token: 0x02003E27 RID: 15911
					public class BASIC_GREEN
					{
						// Token: 0x0400F188 RID: 61832
						public static LocString NAME = "Basic Green Pants";

						// Token: 0x0400F189 RID: 61833
						public static LocString DESC = "A clean pair of green pants that go with everything.";
					}

					// Token: 0x02003E28 RID: 15912
					public class BASIC_ORANGE
					{
						// Token: 0x0400F18A RID: 61834
						public static LocString NAME = "Basic Orange Pants";

						// Token: 0x0400F18B RID: 61835
						public static LocString DESC = "A clean pair of orange pants that go with everything.";
					}

					// Token: 0x02003E29 RID: 15913
					public class BASIC_PURPLE
					{
						// Token: 0x0400F18C RID: 61836
						public static LocString NAME = "Basic Purple Pants";

						// Token: 0x0400F18D RID: 61837
						public static LocString DESC = "A clean pair of purple pants that go with everything.";
					}

					// Token: 0x02003E2A RID: 15914
					public class BASIC_RED
					{
						// Token: 0x0400F18E RID: 61838
						public static LocString NAME = "Basic Red Pants";

						// Token: 0x0400F18F RID: 61839
						public static LocString DESC = "A clean pair of red pants that go with everything.";
					}

					// Token: 0x02003E2B RID: 15915
					public class BASIC_WHITE
					{
						// Token: 0x0400F190 RID: 61840
						public static LocString NAME = "Basic White Pants";

						// Token: 0x0400F191 RID: 61841
						public static LocString DESC = "A clean pair of white pants that go with everything.";
					}

					// Token: 0x02003E2C RID: 15916
					public class BASIC_YELLOW
					{
						// Token: 0x0400F192 RID: 61842
						public static LocString NAME = "Basic Yellow Pants";

						// Token: 0x0400F193 RID: 61843
						public static LocString DESC = "A clean pair of yellow pants that go with everything.";
					}

					// Token: 0x02003E2D RID: 15917
					public class BASIC_BLACK
					{
						// Token: 0x0400F194 RID: 61844
						public static LocString NAME = "Basic Black Pants";

						// Token: 0x0400F195 RID: 61845
						public static LocString DESC = "A clean pair of black pants that go with everything.";
					}

					// Token: 0x02003E2E RID: 15918
					public class SHORTS_BASIC_DEEPRED
					{
						// Token: 0x0400F196 RID: 61846
						public static LocString NAME = "Team Captain Shorts";

						// Token: 0x0400F197 RID: 61847
						public static LocString DESC = "A fresh pair of shorts for natural leaders.";
					}

					// Token: 0x02003E2F RID: 15919
					public class SHORTS_BASIC_SATSUMA
					{
						// Token: 0x0400F198 RID: 61848
						public static LocString NAME = "Superfan Shorts";

						// Token: 0x0400F199 RID: 61849
						public static LocString DESC = "A fresh pair of shorts for long-time supporters of...shorts.";
					}

					// Token: 0x02003E30 RID: 15920
					public class SHORTS_BASIC_YELLOWCAKE
					{
						// Token: 0x0400F19A RID: 61850
						public static LocString NAME = "Yellowcake Shorts";

						// Token: 0x0400F19B RID: 61851
						public static LocString DESC = "A fresh pair of uranium-powder-colored shorts that are definitely not radioactive. Probably.";
					}

					// Token: 0x02003E31 RID: 15921
					public class SHORTS_BASIC_KELLYGREEN
					{
						// Token: 0x0400F19C RID: 61852
						public static LocString NAME = "Go Team Shorts";

						// Token: 0x0400F19D RID: 61853
						public static LocString DESC = "A fresh pair of shorts for cheering from the sidelines.";
					}

					// Token: 0x02003E32 RID: 15922
					public class SHORTS_BASIC_BLUE_COBALT
					{
						// Token: 0x0400F19E RID: 61854
						public static LocString NAME = "True Blue Shorts";

						// Token: 0x0400F19F RID: 61855
						public static LocString DESC = "A fresh pair of shorts for the real team players.";
					}

					// Token: 0x02003E33 RID: 15923
					public class SHORTS_BASIC_PINK_FLAMINGO
					{
						// Token: 0x0400F1A0 RID: 61856
						public static LocString NAME = "Pep Rally Shorts";

						// Token: 0x0400F1A1 RID: 61857
						public static LocString DESC = "The peppiest pair of shorts this side of the asteroid.";
					}

					// Token: 0x02003E34 RID: 15924
					public class SHORTS_BASIC_CHARCOAL
					{
						// Token: 0x0400F1A2 RID: 61858
						public static LocString NAME = "Underdog Shorts";

						// Token: 0x0400F1A3 RID: 61859
						public static LocString DESC = "A fresh pair of shorts. They're cleaner than they look.";
					}

					// Token: 0x02003E35 RID: 15925
					public class CIRCUIT_GREEN
					{
						// Token: 0x0400F1A4 RID: 61860
						public static LocString NAME = "LED Pants";

						// Token: 0x0400F1A5 RID: 61861
						public static LocString DESC = "These legs are lit.";
					}

					// Token: 0x02003E36 RID: 15926
					public class ATHLETE
					{
						// Token: 0x0400F1A6 RID: 61862
						public static LocString NAME = "Racing Pants";

						// Token: 0x0400F1A7 RID: 61863
						public static LocString DESC = "Fast, furious fashion.";
					}

					// Token: 0x02003E37 RID: 15927
					public class BASIC_LIGHTBROWN
					{
						// Token: 0x0400F1A8 RID: 61864
						public static LocString NAME = "Basic Khaki Pants";

						// Token: 0x0400F1A9 RID: 61865
						public static LocString DESC = "Transition effortlessly from subterranean day to subterranean night.";
					}

					// Token: 0x02003E38 RID: 15928
					public class BASIC_REDORANGE
					{
						// Token: 0x0400F1AA RID: 61866
						public static LocString NAME = "Basic Crimson Pants";

						// Token: 0x0400F1AB RID: 61867
						public static LocString DESC = "Like red pants, but slightly fancier-sounding.";
					}

					// Token: 0x02003E39 RID: 15929
					public class GONCH_STRAWBERRY
					{
						// Token: 0x0400F1AC RID: 61868
						public static LocString NAME = "Executive Briefs";

						// Token: 0x0400F1AD RID: 61869
						public static LocString DESC = "Bossy (under)pants.";
					}

					// Token: 0x02003E3A RID: 15930
					public class GONCH_SATSUMA
					{
						// Token: 0x0400F1AE RID: 61870
						public static LocString NAME = "Underling Briefs";

						// Token: 0x0400F1AF RID: 61871
						public static LocString DESC = "The seams are already unraveling.";
					}

					// Token: 0x02003E3B RID: 15931
					public class GONCH_LEMON
					{
						// Token: 0x0400F1B0 RID: 61872
						public static LocString NAME = "Groupthink Briefs";

						// Token: 0x0400F1B1 RID: 61873
						public static LocString DESC = "All the cool people are wearing them.";
					}

					// Token: 0x02003E3C RID: 15932
					public class GONCH_LIME
					{
						// Token: 0x0400F1B2 RID: 61874
						public static LocString NAME = "Stakeholder Briefs";

						// Token: 0x0400F1B3 RID: 61875
						public static LocString DESC = "They're really invested in keeping the wearer comfortable.";
					}

					// Token: 0x02003E3D RID: 15933
					public class GONCH_BLUEBERRY
					{
						// Token: 0x0400F1B4 RID: 61876
						public static LocString NAME = "Admin Briefs";

						// Token: 0x0400F1B5 RID: 61877
						public static LocString DESC = "The workhorse of the underwear world.";
					}

					// Token: 0x02003E3E RID: 15934
					public class GONCH_GRAPE
					{
						// Token: 0x0400F1B6 RID: 61878
						public static LocString NAME = "Buzzword Briefs";

						// Token: 0x0400F1B7 RID: 61879
						public static LocString DESC = "Underwear that works hard, plays hard, and gives 110% to maximize the \"bottom\" line.";
					}

					// Token: 0x02003E3F RID: 15935
					public class GONCH_WATERMELON
					{
						// Token: 0x0400F1B8 RID: 61880
						public static LocString NAME = "Synergy Briefs";

						// Token: 0x0400F1B9 RID: 61881
						public static LocString DESC = "Teamwork makes the dream work.";
					}

					// Token: 0x02003E40 RID: 15936
					public class DENIM_BLUE
					{
						// Token: 0x0400F1BA RID: 61882
						public static LocString NAME = "Jeans";

						// Token: 0x0400F1BB RID: 61883
						public static LocString DESC = "The bottom half of a Canadian tuxedo.";
					}

					// Token: 0x02003E41 RID: 15937
					public class GI_WHITE
					{
						// Token: 0x0400F1BC RID: 61884
						public static LocString NAME = "White Capris";

						// Token: 0x0400F1BD RID: 61885
						public static LocString DESC = "The cropped length is ideal for wading through flooded hallways.";
					}

					// Token: 0x02003E42 RID: 15938
					public class NERD_BROWN
					{
						// Token: 0x0400F1BE RID: 61886
						public static LocString NAME = "Research Pants";

						// Token: 0x0400F1BF RID: 61887
						public static LocString DESC = "The pockets are full of illegible notes that didn't quite survive the wash.";
					}

					// Token: 0x02003E43 RID: 15939
					public class SKIRT_BASIC_BLUE_MIDDLE
					{
						// Token: 0x0400F1C0 RID: 61888
						public static LocString NAME = "Aqua Rayon Skirt";

						// Token: 0x0400F1C1 RID: 61889
						public static LocString DESC = "The tag says \"Dry Clean Only.\" There are no dry cleaners in space.";
					}

					// Token: 0x02003E44 RID: 15940
					public class SKIRT_BASIC_PURPLE
					{
						// Token: 0x0400F1C2 RID: 61890
						public static LocString NAME = "Purple Rayon Skirt";

						// Token: 0x0400F1C3 RID: 61891
						public static LocString DESC = "It's not the most breathable fabric, but it <i>is</i> a lovely shade of purple.";
					}

					// Token: 0x02003E45 RID: 15941
					public class SKIRT_BASIC_GREEN
					{
						// Token: 0x0400F1C4 RID: 61892
						public static LocString NAME = "Olive Rayon Skirt";

						// Token: 0x0400F1C5 RID: 61893
						public static LocString DESC = "Designed not to get snagged on ladders.";
					}

					// Token: 0x02003E46 RID: 15942
					public class SKIRT_BASIC_ORANGE
					{
						// Token: 0x0400F1C6 RID: 61894
						public static LocString NAME = "Apricot Rayon Skirt";

						// Token: 0x0400F1C7 RID: 61895
						public static LocString DESC = "Ready for spontaneous workplace twirling.";
					}

					// Token: 0x02003E47 RID: 15943
					public class SKIRT_BASIC_PINK_ORCHID
					{
						// Token: 0x0400F1C8 RID: 61896
						public static LocString NAME = "Bubblegum Rayon Skirt";

						// Token: 0x0400F1C9 RID: 61897
						public static LocString DESC = "The bubblegum scent lasts 100 washes!";
					}

					// Token: 0x02003E48 RID: 15944
					public class SKIRT_BASIC_RED
					{
						// Token: 0x0400F1CA RID: 61898
						public static LocString NAME = "Garnet Rayon Skirt";

						// Token: 0x0400F1CB RID: 61899
						public static LocString DESC = "It's business time.";
					}

					// Token: 0x02003E49 RID: 15945
					public class SKIRT_BASIC_YELLOW
					{
						// Token: 0x0400F1CC RID: 61900
						public static LocString NAME = "Yellow Rayon Skirt";

						// Token: 0x0400F1CD RID: 61901
						public static LocString DESC = "A formerly white skirt that has not aged well.";
					}

					// Token: 0x02003E4A RID: 15946
					public class SKIRT_BASIC_POLKADOT
					{
						// Token: 0x0400F1CE RID: 61902
						public static LocString NAME = "Polka Dot Skirt";

						// Token: 0x0400F1CF RID: 61903
						public static LocString DESC = "Polka dots are a way to infinity.";
					}

					// Token: 0x02003E4B RID: 15947
					public class SKIRT_BASIC_WATERMELON
					{
						// Token: 0x0400F1D0 RID: 61904
						public static LocString NAME = "Picnic Skirt";

						// Token: 0x0400F1D1 RID: 61905
						public static LocString DESC = "The seeds are spittable, but will bear no fruit.";
					}

					// Token: 0x02003E4C RID: 15948
					public class SKIRT_DENIM_BLUE
					{
						// Token: 0x0400F1D2 RID: 61906
						public static LocString NAME = "Denim Tux Skirt";

						// Token: 0x0400F1D3 RID: 61907
						public static LocString DESC = "Designed for the casual red carpet.";
					}

					// Token: 0x02003E4D RID: 15949
					public class SKIRT_LEOPARD_PRINT_BLUE_PINK
					{
						// Token: 0x0400F1D4 RID: 61908
						public static LocString NAME = "Disco Leopard Skirt";

						// Token: 0x0400F1D5 RID: 61909
						public static LocString DESC = "A faux-fur party staple.";
					}

					// Token: 0x02003E4E RID: 15950
					public class SKIRT_SPARKLE_BLUE
					{
						// Token: 0x0400F1D6 RID: 61910
						public static LocString NAME = "Blue Tinsel Skirt";

						// Token: 0x0400F1D7 RID: 61911
						public static LocString DESC = "The tinsel is scratchy, but look how shiny!";
					}

					// Token: 0x02003E4F RID: 15951
					public class BASIC_ORANGE_SATSUMA
					{
						// Token: 0x0400F1D8 RID: 61912
						public static LocString NAME = "Hi-Vis Pants";

						// Token: 0x0400F1D9 RID: 61913
						public static LocString DESC = "They make the wearer feel truly seen.";
					}

					// Token: 0x02003E50 RID: 15952
					public class PINSTRIPE_SLATE
					{
						// Token: 0x0400F1DA RID: 61914
						public static LocString NAME = "Nobel Pinstripe Trousers";

						// Token: 0x0400F1DB RID: 61915
						public static LocString DESC = "There's a waterproof pocket to keep acceptance speeches smudge-free.";
					}

					// Token: 0x02003E51 RID: 15953
					public class VELOUR_BLACK
					{
						// Token: 0x0400F1DC RID: 61916
						public static LocString NAME = "Black Velour Trousers";

						// Token: 0x0400F1DD RID: 61917
						public static LocString DESC = "Fuzzy, formal and finely cut.";
					}

					// Token: 0x02003E52 RID: 15954
					public class VELOUR_BLUE
					{
						// Token: 0x0400F1DE RID: 61918
						public static LocString NAME = "Shortwave Velour Pants";

						// Token: 0x0400F1DF RID: 61919
						public static LocString DESC = "Formal wear with a sensory side.";
					}

					// Token: 0x02003E53 RID: 15955
					public class VELOUR_PINK
					{
						// Token: 0x0400F1E0 RID: 61920
						public static LocString NAME = "Gamma Velour Pants";

						// Token: 0x0400F1E1 RID: 61921
						public static LocString DESC = "They're stretchy <i>and</i> flame retardant.";
					}

					// Token: 0x02003E54 RID: 15956
					public class SKIRT_BALLERINA_PINK
					{
						// Token: 0x0400F1E2 RID: 61922
						public static LocString NAME = "Ballet Tutu";

						// Token: 0x0400F1E3 RID: 61923
						public static LocString DESC = "A tulle skirt spun and assembled by an army of patent-pending nanobots.";
					}

					// Token: 0x02003E55 RID: 15957
					public class SKIRT_TWEED_PINK_ORCHID
					{
						// Token: 0x0400F1E4 RID: 61924
						public static LocString NAME = "Power Brunch Skirt";

						// Token: 0x0400F1E5 RID: 61925
						public static LocString DESC = "It has pockets!";
					}

					// Token: 0x02003E56 RID: 15958
					public class GINCH_PINK_GLUON
					{
						// Token: 0x0400F1E6 RID: 61926
						public static LocString NAME = "Gluon Shorties";

						// Token: 0x0400F1E7 RID: 61927
						public static LocString DESC = "Comfy pink short-shorts with a ruffled hem.";
					}

					// Token: 0x02003E57 RID: 15959
					public class GINCH_PURPLE_CORTEX
					{
						// Token: 0x0400F1E8 RID: 61928
						public static LocString NAME = "Cortex Shorties";

						// Token: 0x0400F1E9 RID: 61929
						public static LocString DESC = "Comfy purple short-shorts with a ruffled hem.";
					}

					// Token: 0x02003E58 RID: 15960
					public class GINCH_BLUE_FROSTY
					{
						// Token: 0x0400F1EA RID: 61930
						public static LocString NAME = "Frosty Shorties";

						// Token: 0x0400F1EB RID: 61931
						public static LocString DESC = "Icy blue short-shorts with a ruffled hem.";
					}

					// Token: 0x02003E59 RID: 15961
					public class GINCH_TEAL_LOCUS
					{
						// Token: 0x0400F1EC RID: 61932
						public static LocString NAME = "Locus Shorties";

						// Token: 0x0400F1ED RID: 61933
						public static LocString DESC = "Comfy teal short-shorts with a ruffled hem.";
					}

					// Token: 0x02003E5A RID: 15962
					public class GINCH_GREEN_GOOP
					{
						// Token: 0x0400F1EE RID: 61934
						public static LocString NAME = "Goop Shorties";

						// Token: 0x0400F1EF RID: 61935
						public static LocString DESC = "Short-shorts with a ruffled hem and one pocket full of melted snacks.";
					}

					// Token: 0x02003E5B RID: 15963
					public class GINCH_YELLOW_BILE
					{
						// Token: 0x0400F1F0 RID: 61936
						public static LocString NAME = "Bile Shorties";

						// Token: 0x0400F1F1 RID: 61937
						public static LocString DESC = "Ruffled short-shorts in a stomach-turning shade of yellow.";
					}

					// Token: 0x02003E5C RID: 15964
					public class GINCH_ORANGE_NYBBLE
					{
						// Token: 0x0400F1F2 RID: 61938
						public static LocString NAME = "Nybble Shorties";

						// Token: 0x0400F1F3 RID: 61939
						public static LocString DESC = "Comfy orange ruffled short-shorts for computer scientists.";
					}

					// Token: 0x02003E5D RID: 15965
					public class GINCH_RED_IRONBOW
					{
						// Token: 0x0400F1F4 RID: 61940
						public static LocString NAME = "Ironbow Shorties";

						// Token: 0x0400F1F5 RID: 61941
						public static LocString DESC = "Comfy red short-shorts with a ruffled hem.";
					}

					// Token: 0x02003E5E RID: 15966
					public class GINCH_GREY_PHLEGM
					{
						// Token: 0x0400F1F6 RID: 61942
						public static LocString NAME = "Phlegmy Shorties";

						// Token: 0x0400F1F7 RID: 61943
						public static LocString DESC = "Ruffled short-shorts in a rather sticky shade of light grey.";
					}

					// Token: 0x02003E5F RID: 15967
					public class GINCH_GREY_OBELUS
					{
						// Token: 0x0400F1F8 RID: 61944
						public static LocString NAME = "Obelus Shorties";

						// Token: 0x0400F1F9 RID: 61945
						public static LocString DESC = "Comfy grey short-shorts with a ruffled hem.";
					}

					// Token: 0x02003E60 RID: 15968
					public class KNIT_POLKADOT_TURQ
					{
						// Token: 0x0400F1FA RID: 61946
						public static LocString NAME = "Polka Dot Track Pants";

						// Token: 0x0400F1FB RID: 61947
						public static LocString DESC = "For clowning around during mandatory physical fitness week.";
					}

					// Token: 0x02003E61 RID: 15969
					public class GI_BELT_WHITE_BLACK
					{
						// Token: 0x0400F1FC RID: 61948
						public static LocString NAME = "Rebel Gi Pants";

						// Token: 0x0400F1FD RID: 61949
						public static LocString DESC = "Relaxed-fit pants designed for roundhouse kicks.";
					}

					// Token: 0x02003E62 RID: 15970
					public class BELT_KHAKI_TAN
					{
						// Token: 0x0400F1FE RID: 61950
						public static LocString NAME = "HVAC Khaki Pants";

						// Token: 0x0400F1FF RID: 61951
						public static LocString DESC = "Rip-resistant fabric makes crawling through ducts a breeze.";
					}
				}
			}

			// Token: 0x02002C83 RID: 11395
			public class CLOTHING_SHOES
			{
				// Token: 0x0400C0A9 RID: 49321
				public static LocString NAME = "Default Footwear";

				// Token: 0x0400C0AA RID: 49322
				public static LocString DESC = "The default style of footwear.";

				// Token: 0x02003944 RID: 14660
				public class FACADES
				{
					// Token: 0x02003E63 RID: 15971
					public class BASIC_BLUE_MIDDLE
					{
						// Token: 0x0400F200 RID: 61952
						public static LocString NAME = "Basic Aqua Shoes";

						// Token: 0x0400F201 RID: 61953
						public static LocString DESC = "A fresh pair of aqua-blue shoes that go with everything.";
					}

					// Token: 0x02003E64 RID: 15972
					public class BASIC_PINK_ORCHID
					{
						// Token: 0x0400F202 RID: 61954
						public static LocString NAME = "Basic Bubblegum Shoes";

						// Token: 0x0400F203 RID: 61955
						public static LocString DESC = "A fresh pair of bubblegum-pink shoes that go with everything.";
					}

					// Token: 0x02003E65 RID: 15973
					public class BASIC_GREEN
					{
						// Token: 0x0400F204 RID: 61956
						public static LocString NAME = "Basic Green Shoes";

						// Token: 0x0400F205 RID: 61957
						public static LocString DESC = "A fresh pair of green shoes that go with everything.";
					}

					// Token: 0x02003E66 RID: 15974
					public class BASIC_ORANGE
					{
						// Token: 0x0400F206 RID: 61958
						public static LocString NAME = "Basic Orange Shoes";

						// Token: 0x0400F207 RID: 61959
						public static LocString DESC = "A fresh pair of orange shoes that go with everything.";
					}

					// Token: 0x02003E67 RID: 15975
					public class BASIC_PURPLE
					{
						// Token: 0x0400F208 RID: 61960
						public static LocString NAME = "Basic Purple Shoes";

						// Token: 0x0400F209 RID: 61961
						public static LocString DESC = "A fresh pair of purple shoes that go with everything.";
					}

					// Token: 0x02003E68 RID: 15976
					public class BASIC_RED
					{
						// Token: 0x0400F20A RID: 61962
						public static LocString NAME = "Basic Red Shoes";

						// Token: 0x0400F20B RID: 61963
						public static LocString DESC = "A fresh pair of red shoes that go with everything.";
					}

					// Token: 0x02003E69 RID: 15977
					public class BASIC_WHITE
					{
						// Token: 0x0400F20C RID: 61964
						public static LocString NAME = "Basic White Shoes";

						// Token: 0x0400F20D RID: 61965
						public static LocString DESC = "A fresh pair of white shoes that go with everything.";
					}

					// Token: 0x02003E6A RID: 15978
					public class BASIC_YELLOW
					{
						// Token: 0x0400F20E RID: 61966
						public static LocString NAME = "Basic Yellow Shoes";

						// Token: 0x0400F20F RID: 61967
						public static LocString DESC = "A fresh pair of yellow shoes that go with everything.";
					}

					// Token: 0x02003E6B RID: 15979
					public class BASIC_BLACK
					{
						// Token: 0x0400F210 RID: 61968
						public static LocString NAME = "Basic Black Shoes";

						// Token: 0x0400F211 RID: 61969
						public static LocString DESC = "A fresh pair of black shoes that go with everything.";
					}

					// Token: 0x02003E6C RID: 15980
					public class BASIC_BLUEGREY
					{
						// Token: 0x0400F212 RID: 61970
						public static LocString NAME = "Basic Gunmetal Shoes";

						// Token: 0x0400F213 RID: 61971
						public static LocString DESC = "A fresh pair of pastel shoes that go with everything.";
					}

					// Token: 0x02003E6D RID: 15981
					public class BASIC_TAN
					{
						// Token: 0x0400F214 RID: 61972
						public static LocString NAME = "Basic Tan Shoes";

						// Token: 0x0400F215 RID: 61973
						public static LocString DESC = "They're remarkably unremarkable.";
					}

					// Token: 0x02003E6E RID: 15982
					public class SOCKS_ATHLETIC_DEEPRED
					{
						// Token: 0x0400F216 RID: 61974
						public static LocString NAME = "Team Captain Gym Socks";

						// Token: 0x0400F217 RID: 61975
						public static LocString DESC = "Breathable socks with sporty red stripes.";
					}

					// Token: 0x02003E6F RID: 15983
					public class SOCKS_ATHLETIC_SATSUMA
					{
						// Token: 0x0400F218 RID: 61976
						public static LocString NAME = "Superfan Gym Socks";

						// Token: 0x0400F219 RID: 61977
						public static LocString DESC = "Breathable socks with sporty orange stripes.";
					}

					// Token: 0x02003E70 RID: 15984
					public class SOCKS_ATHLETIC_LEMON
					{
						// Token: 0x0400F21A RID: 61978
						public static LocString NAME = "Hype Gym Socks";

						// Token: 0x0400F21B RID: 61979
						public static LocString DESC = "Breathable socks with sporty yellow stripes.";
					}

					// Token: 0x02003E71 RID: 15985
					public class SOCKS_ATHLETIC_KELLYGREEN
					{
						// Token: 0x0400F21C RID: 61980
						public static LocString NAME = "Go Team Gym Socks";

						// Token: 0x0400F21D RID: 61981
						public static LocString DESC = "Breathable socks with sporty green stripes.";
					}

					// Token: 0x02003E72 RID: 15986
					public class SOCKS_ATHLETIC_COBALT
					{
						// Token: 0x0400F21E RID: 61982
						public static LocString NAME = "True Blue Gym Socks";

						// Token: 0x0400F21F RID: 61983
						public static LocString DESC = "Breathable socks with sporty blue stripes.";
					}

					// Token: 0x02003E73 RID: 15987
					public class SOCKS_ATHLETIC_FLAMINGO
					{
						// Token: 0x0400F220 RID: 61984
						public static LocString NAME = "Pep Rally Gym Socks";

						// Token: 0x0400F221 RID: 61985
						public static LocString DESC = "Breathable socks with sporty pink stripes.";
					}

					// Token: 0x02003E74 RID: 15988
					public class SOCKS_ATHLETIC_CHARCOAL
					{
						// Token: 0x0400F222 RID: 61986
						public static LocString NAME = "Underdog Gym Socks";

						// Token: 0x0400F223 RID: 61987
						public static LocString DESC = "Breathable socks that do nothing whatsoever to eliminate foot odor.";
					}

					// Token: 0x02003E75 RID: 15989
					public class BASIC_GREY
					{
						// Token: 0x0400F224 RID: 61988
						public static LocString NAME = "Basic Gray Shoes";

						// Token: 0x0400F225 RID: 61989
						public static LocString DESC = "A fresh pair of gray shoes that go with everything.";
					}

					// Token: 0x02003E76 RID: 15990
					public class DENIM_BLUE
					{
						// Token: 0x0400F226 RID: 61990
						public static LocString NAME = "Denim Shoes";

						// Token: 0x0400F227 RID: 61991
						public static LocString DESC = "Not technically essential for a Canadian tuxedo, but why not?";
					}

					// Token: 0x02003E77 RID: 15991
					public class LEGWARMERS_STRAWBERRY
					{
						// Token: 0x0400F228 RID: 61992
						public static LocString NAME = "Slouchy Strawberry Socks";

						// Token: 0x0400F229 RID: 61993
						public static LocString DESC = "Freckly knitted socks that don't stay up.";
					}

					// Token: 0x02003E78 RID: 15992
					public class LEGWARMERS_SATSUMA
					{
						// Token: 0x0400F22A RID: 61994
						public static LocString NAME = "Slouchy Satsuma Socks";

						// Token: 0x0400F22B RID: 61995
						public static LocString DESC = "Sweet knitted socks for spontaneous dance segments.";
					}

					// Token: 0x02003E79 RID: 15993
					public class LEGWARMERS_LEMON
					{
						// Token: 0x0400F22C RID: 61996
						public static LocString NAME = "Slouchy Lemon Socks";

						// Token: 0x0400F22D RID: 61997
						public static LocString DESC = "Zesty knitted socks that don't stay up.";
					}

					// Token: 0x02003E7A RID: 15994
					public class LEGWARMERS_LIME
					{
						// Token: 0x0400F22E RID: 61998
						public static LocString NAME = "Slouchy Lime Socks";

						// Token: 0x0400F22F RID: 61999
						public static LocString DESC = "Juicy knitted socks that don't stay up.";
					}

					// Token: 0x02003E7B RID: 15995
					public class LEGWARMERS_BLUEBERRY
					{
						// Token: 0x0400F230 RID: 62000
						public static LocString NAME = "Slouchy Blueberry Socks";

						// Token: 0x0400F231 RID: 62001
						public static LocString DESC = "Knitted socks with a fun bobble-stitch texture.";
					}

					// Token: 0x02003E7C RID: 15996
					public class LEGWARMERS_GRAPE
					{
						// Token: 0x0400F232 RID: 62002
						public static LocString NAME = "Slouchy Grape Socks";

						// Token: 0x0400F233 RID: 62003
						public static LocString DESC = "These fabulous knitted socks that don't stay up are really raisin the bar.";
					}

					// Token: 0x02003E7D RID: 15997
					public class LEGWARMERS_WATERMELON
					{
						// Token: 0x0400F234 RID: 62004
						public static LocString NAME = "Slouchy Watermelon Socks";

						// Token: 0x0400F235 RID: 62005
						public static LocString DESC = "Summery knitted socks that don't stay up.";
					}

					// Token: 0x02003E7E RID: 15998
					public class BALLERINA_PINK
					{
						// Token: 0x0400F236 RID: 62006
						public static LocString NAME = "Ballet Shoes";

						// Token: 0x0400F237 RID: 62007
						public static LocString DESC = "There's no \"pointe\" in aiming for anything less than perfection.";
					}

					// Token: 0x02003E7F RID: 15999
					public class MARYJANE_SOCKS_BW
					{
						// Token: 0x0400F238 RID: 62008
						public static LocString NAME = "Frilly Sock Shoes";

						// Token: 0x0400F239 RID: 62009
						public static LocString DESC = "They add a little <i>je ne sais quoi</i> to everyday lab wear.";
					}

					// Token: 0x02003E80 RID: 16000
					public class CLASSICFLATS_CREAM_CHARCOAL
					{
						// Token: 0x0400F23A RID: 62010
						public static LocString NAME = "Dressy Shoes";

						// Token: 0x0400F23B RID: 62011
						public static LocString DESC = "An enduring style, for enduring endless small talk.";
					}

					// Token: 0x02003E81 RID: 16001
					public class VELOUR_BLUE
					{
						// Token: 0x0400F23C RID: 62012
						public static LocString NAME = "Shortwave Velour Shoes";

						// Token: 0x0400F23D RID: 62013
						public static LocString DESC = "Not the easiest to keep clean.";
					}

					// Token: 0x02003E82 RID: 16002
					public class VELOUR_PINK
					{
						// Token: 0x0400F23E RID: 62014
						public static LocString NAME = "Gamma Velour Shoes";

						// Token: 0x0400F23F RID: 62015
						public static LocString DESC = "Finally, a pair of work-appropriate fuzzy shoes.";
					}

					// Token: 0x02003E83 RID: 16003
					public class VELOUR_BLACK
					{
						// Token: 0x0400F240 RID: 62016
						public static LocString NAME = "Black Velour Shoes";

						// Token: 0x0400F241 RID: 62017
						public static LocString DESC = "Matching velour lining gently tickles feet with every step.";
					}

					// Token: 0x02003E84 RID: 16004
					public class FLASHY
					{
						// Token: 0x0400F242 RID: 62018
						public static LocString NAME = "Superstar Shoes";

						// Token: 0x0400F243 RID: 62019
						public static LocString DESC = "Why walk when you can <i>moon</i>walk?";
					}

					// Token: 0x02003E85 RID: 16005
					public class GINCH_PINK_SALTROCK
					{
						// Token: 0x0400F244 RID: 62020
						public static LocString NAME = "Frilly Saltrock Socks";

						// Token: 0x0400F245 RID: 62021
						public static LocString DESC = "Thick, soft pink socks with extra flounce.";
					}

					// Token: 0x02003E86 RID: 16006
					public class GINCH_PURPLE_DUSKY
					{
						// Token: 0x0400F246 RID: 62022
						public static LocString NAME = "Frilly Dusk Socks";

						// Token: 0x0400F247 RID: 62023
						public static LocString DESC = "Thick, soft purple socks with extra flounce.";
					}

					// Token: 0x02003E87 RID: 16007
					public class GINCH_BLUE_BASIN
					{
						// Token: 0x0400F248 RID: 62024
						public static LocString NAME = "Frilly Basin Socks";

						// Token: 0x0400F249 RID: 62025
						public static LocString DESC = "Thick, soft blue socks with extra flounce.";
					}

					// Token: 0x02003E88 RID: 16008
					public class GINCH_TEAL_BALMY
					{
						// Token: 0x0400F24A RID: 62026
						public static LocString NAME = "Frilly Balm Socks";

						// Token: 0x0400F24B RID: 62027
						public static LocString DESC = "Thick, soothing teal socks with extra flounce.";
					}

					// Token: 0x02003E89 RID: 16009
					public class GINCH_GREEN_LIME
					{
						// Token: 0x0400F24C RID: 62028
						public static LocString NAME = "Frilly Leach Socks";

						// Token: 0x0400F24D RID: 62029
						public static LocString DESC = "Thick, soft green socks with extra flounce.";
					}

					// Token: 0x02003E8A RID: 16010
					public class GINCH_YELLOW_YELLOWCAKE
					{
						// Token: 0x0400F24E RID: 62030
						public static LocString NAME = "Frilly Yellowcake Socks";

						// Token: 0x0400F24F RID: 62031
						public static LocString DESC = "Dangerously soft yellow socks with extra flounce.";
					}

					// Token: 0x02003E8B RID: 16011
					public class GINCH_ORANGE_ATOMIC
					{
						// Token: 0x0400F250 RID: 62032
						public static LocString NAME = "Frilly Atomic Socks";

						// Token: 0x0400F251 RID: 62033
						public static LocString DESC = "Thick, soft orange socks with extra flounce.";
					}

					// Token: 0x02003E8C RID: 16012
					public class GINCH_RED_MAGMA
					{
						// Token: 0x0400F252 RID: 62034
						public static LocString NAME = "Frilly Magma Socks";

						// Token: 0x0400F253 RID: 62035
						public static LocString DESC = "Thick, toasty red socks with extra flounce.";
					}

					// Token: 0x02003E8D RID: 16013
					public class GINCH_GREY_GREY
					{
						// Token: 0x0400F254 RID: 62036
						public static LocString NAME = "Frilly Slate Socks";

						// Token: 0x0400F255 RID: 62037
						public static LocString DESC = "Thick, soft grey socks with extra flounce.";
					}

					// Token: 0x02003E8E RID: 16014
					public class GINCH_GREY_CHARCOAL
					{
						// Token: 0x0400F256 RID: 62038
						public static LocString NAME = "Frilly Charcoal Socks";

						// Token: 0x0400F257 RID: 62039
						public static LocString DESC = "Thick, soft dark grey socks with extra flounce.";
					}
				}
			}

			// Token: 0x02002C84 RID: 11396
			public class CLOTHING_HATS
			{
				// Token: 0x0400C0AB RID: 49323
				public static LocString NAME = "Default Headgear";

				// Token: 0x0400C0AC RID: 49324
				public static LocString DESC = "<DESC>";

				// Token: 0x02003945 RID: 14661
				public class FACADES
				{
				}
			}

			// Token: 0x02002C85 RID: 11397
			public class CLOTHING_ACCESORIES
			{
				// Token: 0x0400C0AD RID: 49325
				public static LocString NAME = "Default Accessory";

				// Token: 0x0400C0AE RID: 49326
				public static LocString DESC = "<DESC>";

				// Token: 0x02003946 RID: 14662
				public class FACADES
				{
				}
			}

			// Token: 0x02002C86 RID: 11398
			public class OXYGEN_TANK
			{
				// Token: 0x0400C0AF RID: 49327
				public static LocString NAME = UI.FormatAsLink("Oxygen Tank", "OXYGEN_TANK");

				// Token: 0x0400C0B0 RID: 49328
				public static LocString GENERICNAME = "Equipment";

				// Token: 0x0400C0B1 RID: 49329
				public static LocString DESC = "It's like a to-go bag for your lungs.";

				// Token: 0x0400C0B2 RID: 49330
				public static LocString EFFECT = "Allows Duplicants to breathe in hazardous environments.\n\nDoes not work when submerged in <style=\"liquid\">Liquid</style>.";

				// Token: 0x0400C0B3 RID: 49331
				public static LocString RECIPE_DESC = "Allows Duplicants to breathe in hazardous environments.\n\nDoes not work when submerged in <style=\"liquid\">Liquid</style>";
			}

			// Token: 0x02002C87 RID: 11399
			public class OXYGEN_TANK_UNDERWATER
			{
				// Token: 0x0400C0B4 RID: 49332
				public static LocString NAME = "Oxygen Rebreather";

				// Token: 0x0400C0B5 RID: 49333
				public static LocString GENERICNAME = "Equipment";

				// Token: 0x0400C0B6 RID: 49334
				public static LocString DESC = "";

				// Token: 0x0400C0B7 RID: 49335
				public static LocString EFFECT = "Allows Duplicants to breathe while submerged in <style=\"liquid\">Liquid</style>.\n\nDoes not work outside of liquid.";

				// Token: 0x0400C0B8 RID: 49336
				public static LocString RECIPE_DESC = "Allows Duplicants to breathe while submerged in <style=\"liquid\">Liquid</style>.\n\nDoes not work outside of liquid";
			}

			// Token: 0x02002C88 RID: 11400
			public class EQUIPPABLEBALLOON
			{
				// Token: 0x0400C0B9 RID: 49337
				public static LocString NAME = UI.FormatAsLink("Balloon Friend", "EQUIPPABLEBALLOON");

				// Token: 0x0400C0BA RID: 49338
				public static LocString DESC = "A floating friend to reassure my Duplicants they are so very, very clever.";

				// Token: 0x0400C0BB RID: 49339
				public static LocString EFFECT = "Gives Duplicants a boost in brain function.\n\nSupplied by Duplicants with the Balloon Artist " + UI.FormatAsLink("Overjoyed", "MORALE") + " response.";

				// Token: 0x0400C0BC RID: 49340
				public static LocString RECIPE_DESC = "Gives Duplicants a boost in brain function.\n\nSupplied by Duplicants with the Balloon Artist " + UI.FormatAsLink("Overjoyed", "MORALE") + " response";

				// Token: 0x0400C0BD RID: 49341
				public static LocString GENERICNAME = "Balloon Friend";

				// Token: 0x02003947 RID: 14663
				public class FACADES
				{
					// Token: 0x02003E8F RID: 16015
					public class DEFAULT_BALLOON
					{
						// Token: 0x0400F258 RID: 62040
						public static LocString NAME = UI.FormatAsLink("Balloon Friend", "EQUIPPABLEBALLOON");

						// Token: 0x0400F259 RID: 62041
						public static LocString DESC = "A floating friend to reassure my Duplicants that they are so very, very clever.";
					}

					// Token: 0x02003E90 RID: 16016
					public class BALLOON_FIREENGINE_LONG_SPARKLES
					{
						// Token: 0x0400F25A RID: 62042
						public static LocString NAME = UI.FormatAsLink("Magma Glitter", "EQUIPPABLEBALLOON");

						// Token: 0x0400F25B RID: 62043
						public static LocString DESC = "They float <i>and</i> sparkle!";
					}

					// Token: 0x02003E91 RID: 16017
					public class BALLOON_YELLOW_LONG_SPARKLES
					{
						// Token: 0x0400F25C RID: 62044
						public static LocString NAME = UI.FormatAsLink("Lavatory Glitter", "EQUIPPABLEBALLOON");

						// Token: 0x0400F25D RID: 62045
						public static LocString DESC = "Sparkly balloons in an all-too-familiar hue.";
					}

					// Token: 0x02003E92 RID: 16018
					public class BALLOON_BLUE_LONG_SPARKLES
					{
						// Token: 0x0400F25E RID: 62046
						public static LocString NAME = UI.FormatAsLink("Wheezewort Glitter", "EQUIPPABLEBALLOON");

						// Token: 0x0400F25F RID: 62047
						public static LocString DESC = "They float <i>and</i> sparkle!";
					}

					// Token: 0x02003E93 RID: 16019
					public class BALLOON_GREEN_LONG_SPARKLES
					{
						// Token: 0x0400F260 RID: 62048
						public static LocString NAME = UI.FormatAsLink("Mush Bar Glitter", "EQUIPPABLEBALLOON");

						// Token: 0x0400F261 RID: 62049
						public static LocString DESC = "They float <i>and</i> sparkle!";
					}

					// Token: 0x02003E94 RID: 16020
					public class BALLOON_PINK_LONG_SPARKLES
					{
						// Token: 0x0400F262 RID: 62050
						public static LocString NAME = UI.FormatAsLink("Petal Glitter", "EQUIPPABLEBALLOON");

						// Token: 0x0400F263 RID: 62051
						public static LocString DESC = "They float <i>and</i> sparkle!";
					}

					// Token: 0x02003E95 RID: 16021
					public class BALLOON_PURPLE_LONG_SPARKLES
					{
						// Token: 0x0400F264 RID: 62052
						public static LocString NAME = UI.FormatAsLink("Dusky Glitter", "EQUIPPABLEBALLOON");

						// Token: 0x0400F265 RID: 62053
						public static LocString DESC = "They float <i>and</i> sparkle!";
					}

					// Token: 0x02003E96 RID: 16022
					public class BALLOON_BABY_PACU_EGG
					{
						// Token: 0x0400F266 RID: 62054
						public static LocString NAME = UI.FormatAsLink("Floatie Fish", "EQUIPPABLEBALLOON");

						// Token: 0x0400F267 RID: 62055
						public static LocString DESC = "They do not taste as good as the real thing.";
					}

					// Token: 0x02003E97 RID: 16023
					public class BALLOON_BABY_GLOSSY_DRECKO_EGG
					{
						// Token: 0x0400F268 RID: 62056
						public static LocString NAME = UI.FormatAsLink("Glossy Glee", "EQUIPPABLEBALLOON");

						// Token: 0x0400F269 RID: 62057
						public static LocString DESC = "A happy little trio of inflatable critters.";
					}

					// Token: 0x02003E98 RID: 16024
					public class BALLOON_BABY_HATCH_EGG
					{
						// Token: 0x0400F26A RID: 62058
						public static LocString NAME = UI.FormatAsLink("Helium Hatches", "EQUIPPABLEBALLOON");

						// Token: 0x0400F26B RID: 62059
						public static LocString DESC = "A happy little trio of inflatable critters.";
					}

					// Token: 0x02003E99 RID: 16025
					public class BALLOON_BABY_POKESHELL_EGG
					{
						// Token: 0x0400F26C RID: 62060
						public static LocString NAME = UI.FormatAsLink("Peppy Pokeshells", "EQUIPPABLEBALLOON");

						// Token: 0x0400F26D RID: 62061
						public static LocString DESC = "A happy little trio of inflatable critters.";
					}

					// Token: 0x02003E9A RID: 16026
					public class BALLOON_BABY_PUFT_EGG
					{
						// Token: 0x0400F26E RID: 62062
						public static LocString NAME = UI.FormatAsLink("Puffed-Up Pufts", "EQUIPPABLEBALLOON");

						// Token: 0x0400F26F RID: 62063
						public static LocString DESC = "A happy little trio of inflatable critters.";
					}

					// Token: 0x02003E9B RID: 16027
					public class BALLOON_BABY_SHOVOLE_EGG
					{
						// Token: 0x0400F270 RID: 62064
						public static LocString NAME = UI.FormatAsLink("Voley Voley Voles", "EQUIPPABLEBALLOON");

						// Token: 0x0400F271 RID: 62065
						public static LocString DESC = "A happy little trio of inflatable critters.";
					}

					// Token: 0x02003E9C RID: 16028
					public class BALLOON_BABY_PIP_EGG
					{
						// Token: 0x0400F272 RID: 62066
						public static LocString NAME = UI.FormatAsLink("Pip Pip Hooray", "EQUIPPABLEBALLOON");

						// Token: 0x0400F273 RID: 62067
						public static LocString DESC = "A happy little trio of inflatable critters.";
					}

					// Token: 0x02003E9D RID: 16029
					public class CANDY_BLUEBERRY
					{
						// Token: 0x0400F274 RID: 62068
						public static LocString NAME = UI.FormatAsLink("Candied Blueberry", "EQUIPPABLEBALLOON");

						// Token: 0x0400F275 RID: 62069
						public static LocString DESC = "A juicy bunch of blueberry-scented balloons.";
					}

					// Token: 0x02003E9E RID: 16030
					public class CANDY_GRAPE
					{
						// Token: 0x0400F276 RID: 62070
						public static LocString NAME = UI.FormatAsLink("Candied Grape", "EQUIPPABLEBALLOON");

						// Token: 0x0400F277 RID: 62071
						public static LocString DESC = "A juicy bunch of grape-scented balloons.";
					}

					// Token: 0x02003E9F RID: 16031
					public class CANDY_LEMON
					{
						// Token: 0x0400F278 RID: 62072
						public static LocString NAME = UI.FormatAsLink("Candied Lemon", "EQUIPPABLEBALLOON");

						// Token: 0x0400F279 RID: 62073
						public static LocString DESC = "A juicy lemon-scented bunch of balloons.";
					}

					// Token: 0x02003EA0 RID: 16032
					public class CANDY_LIME
					{
						// Token: 0x0400F27A RID: 62074
						public static LocString NAME = UI.FormatAsLink("Candied Lime", "EQUIPPABLEBALLOON");

						// Token: 0x0400F27B RID: 62075
						public static LocString DESC = "A juicy lime-scented bunch of balloons.";
					}

					// Token: 0x02003EA1 RID: 16033
					public class CANDY_ORANGE
					{
						// Token: 0x0400F27C RID: 62076
						public static LocString NAME = UI.FormatAsLink("Candied Satsuma", "EQUIPPABLEBALLOON");

						// Token: 0x0400F27D RID: 62077
						public static LocString DESC = "A juicy satsuma-scented bunch of balloons.";
					}

					// Token: 0x02003EA2 RID: 16034
					public class CANDY_STRAWBERRY
					{
						// Token: 0x0400F27E RID: 62078
						public static LocString NAME = UI.FormatAsLink("Candied Strawberry", "EQUIPPABLEBALLOON");

						// Token: 0x0400F27F RID: 62079
						public static LocString DESC = "A juicy strawberry-scented bunch of balloons.";
					}

					// Token: 0x02003EA3 RID: 16035
					public class CANDY_WATERMELON
					{
						// Token: 0x0400F280 RID: 62080
						public static LocString NAME = UI.FormatAsLink("Candied Watermelon", "EQUIPPABLEBALLOON");

						// Token: 0x0400F281 RID: 62081
						public static LocString DESC = "A juicy watermelon-scented bunch of balloons.";
					}

					// Token: 0x02003EA4 RID: 16036
					public class HAND_GOLD
					{
						// Token: 0x0400F282 RID: 62082
						public static LocString NAME = UI.FormatAsLink("Gold Fingers", "EQUIPPABLEBALLOON");

						// Token: 0x0400F283 RID: 62083
						public static LocString DESC = "Inflatable gestures of encouragement.";
					}
				}
			}

			// Token: 0x02002C89 RID: 11401
			public class SLEEPCLINICPAJAMAS
			{
				// Token: 0x0400C0BE RID: 49342
				public static LocString NAME = UI.FormatAsLink("Pajamas", "SLEEP_CLINIC_PAJAMAS");

				// Token: 0x0400C0BF RID: 49343
				public static LocString GENERICNAME = "Clothing";

				// Token: 0x0400C0C0 RID: 49344
				public static LocString DESC = "A soft, fleecy ticket to dreamland.";

				// Token: 0x0400C0C1 RID: 49345
				public static LocString EFFECT = "Helps Duplicants fall asleep by reducing " + UI.FormatAsLink("Stamina", "HEALTH") + ".\n\nEnables the wearer to dream and produce Dream Journals.";

				// Token: 0x0400C0C2 RID: 49346
				public static LocString DESTROY_TOAST = "Ripped Pajamas";
			}
		}
	}
}
