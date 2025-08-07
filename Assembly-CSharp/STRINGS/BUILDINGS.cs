using System;
using TUNING;

namespace STRINGS
{
	// Token: 0x02000F9E RID: 3998
	public class BUILDINGS
	{
		// Token: 0x02002194 RID: 8596
		public class PREFABS
		{
			// Token: 0x0200293D RID: 10557
			public class FOSSILSCULPTURE
			{
				// Token: 0x0400B66D RID: 46701
				public static LocString NAME = UI.FormatAsLink("Fossil Block", "FOSSILSCULPTURE");

				// Token: 0x0400B66E RID: 46702
				public static LocString DESC = "Duplicants who have learned art skills can produce more decorative sculptures.";

				// Token: 0x0400B66F RID: 46703
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Majorly increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be sculpted by a Duplicant."
				});
			}

			// Token: 0x0200293E RID: 10558
			public class CEILINGFOSSILSCULPTURE
			{
				// Token: 0x0400B670 RID: 46704
				public static LocString NAME = UI.FormatAsLink("Hanging Fossil Block", "CEILINGFOSSILSCULPTURE");

				// Token: 0x0400B671 RID: 46705
				public static LocString DESC = "Duplicants who have learned art skills can produce more decorative ceiling sculptures.";

				// Token: 0x0400B672 RID: 46706
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Majorly increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be sculpted by a Duplicant."
				});
			}

			// Token: 0x0200293F RID: 10559
			public class HEADQUARTERSCOMPLETE
			{
				// Token: 0x0400B673 RID: 46707
				public static LocString NAME = UI.FormatAsLink("Printing Pod", "HEADQUARTERS");

				// Token: 0x0400B674 RID: 46708
				public static LocString UNIQUE_POPTEXT = "A clone of the cloning machine? What a novel thought.\n\nAlas, it won't work.";
			}

			// Token: 0x02002940 RID: 10560
			public class EXOBASEHEADQUARTERS
			{
				// Token: 0x0400B675 RID: 46709
				public static LocString NAME = UI.FormatAsLink("Mini-Pod", "EXOBASEHEADQUARTERS");

				// Token: 0x0400B676 RID: 46710
				public static LocString DESC = "A quick and easy substitute, though it'll never live up to the original.";

				// Token: 0x0400B677 RID: 46711
				public static LocString EFFECT = "A portable bioprinter that produces new Duplicants or care packages containing resources.\n\nOnly one Printing Pod or Mini-Pod is permitted per Planetoid.";
			}

			// Token: 0x02002941 RID: 10561
			public class AIRCONDITIONER
			{
				// Token: 0x0400B678 RID: 46712
				public static LocString NAME = UI.FormatAsLink("Thermo Regulator", "AIRCONDITIONER");

				// Token: 0x0400B679 RID: 46713
				public static LocString DESC = "A thermo regulator doesn't remove heat, but relocates it to a new area.";

				// Token: 0x0400B67A RID: 46714
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Cools the ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" piped through it, but outputs ",
					UI.FormatAsLink("Heat", "HEAT"),
					" in its immediate vicinity."
				});
			}

			// Token: 0x02002942 RID: 10562
			public class STATERPILLAREGG
			{
				// Token: 0x0400B67B RID: 46715
				public static LocString NAME = UI.FormatAsLink("Slug Egg", "STATERPILLAREGG");

				// Token: 0x0400B67C RID: 46716
				public static LocString DESC = "The electrifying egg of the " + UI.FormatAsLink("Plug Slug", "STATERPILLAR") + ".";

				// Token: 0x0400B67D RID: 46717
				public static LocString EFFECT = "Slug Eggs can be connected to a " + UI.FormatAsLink("Power", "POWER") + " circuit as an energy source.";
			}

			// Token: 0x02002943 RID: 10563
			public class STATERPILLARGENERATOR
			{
				// Token: 0x0400B67E RID: 46718
				public static LocString NAME = UI.FormatAsLink("Plug Slug", "STATERPILLAR");

				// Token: 0x02003894 RID: 14484
				public class MODIFIERS
				{
					// Token: 0x0400E4A0 RID: 58528
					public static LocString WILD = "Wild!";

					// Token: 0x0400E4A1 RID: 58529
					public static LocString HUNGRY = "Hungry!";
				}
			}

			// Token: 0x02002944 RID: 10564
			public class BEEHIVE
			{
				// Token: 0x0400B67F RID: 46719
				public static LocString NAME = UI.FormatAsLink("Beeta Hive", "BEEHIVE");

				// Token: 0x0400B680 RID: 46720
				public static LocString DESC = string.Concat(new string[]
				{
					"A moderately ",
					UI.FormatAsLink("Radioactive", "RADIATION"),
					" nest made by ",
					UI.FormatAsLink("Beetas", "BEE"),
					".\n\nConverts ",
					UI.FormatAsLink("Uranium Ore", "URANIUMORE"),
					" into ",
					UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM"),
					" when worked by a Beeta.\nWill not function if ground below has been destroyed."
				});

				// Token: 0x0400B681 RID: 46721
				public static LocString EFFECT = "The cozy home of a Beeta.";
			}

			// Token: 0x02002945 RID: 10565
			public class ETHANOLDISTILLERY
			{
				// Token: 0x0400B682 RID: 46722
				public static LocString NAME = UI.FormatAsLink("Ethanol Distiller", "ETHANOLDISTILLERY");

				// Token: 0x0400B683 RID: 46723
				public static LocString DESC = string.Concat(new string[]
				{
					"Ethanol distillers convert ",
					ITEMS.INDUSTRIAL_PRODUCTS.WOOD.NAME,
					" into burnable ",
					ELEMENTS.ETHANOL.NAME,
					" fuel."
				});

				// Token: 0x0400B684 RID: 46724
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Refines ",
					UI.FormatAsLink("Wood", "WOOD"),
					" into ",
					UI.FormatAsLink("Ethanol", "ETHANOL"),
					"."
				});
			}

			// Token: 0x02002946 RID: 10566
			public class ALGAEDISTILLERY
			{
				// Token: 0x0400B685 RID: 46725
				public static LocString NAME = UI.FormatAsLink("Algae Distiller", "ALGAEDISTILLERY");

				// Token: 0x0400B686 RID: 46726
				public static LocString DESC = "Algae distillers convert disease-causing slime into algae for oxygen production.";

				// Token: 0x0400B687 RID: 46727
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Refines ",
					UI.FormatAsLink("Slime", "SLIMEMOLD"),
					" into ",
					UI.FormatAsLink("Algae", "ALGAE"),
					"."
				});
			}

			// Token: 0x02002947 RID: 10567
			public class GUNKEMPTIER
			{
				// Token: 0x0400B688 RID: 46728
				public static LocString NAME = UI.FormatAsLink("Gunk Extractor", "GUNKEMPTIER");

				// Token: 0x0400B689 RID: 46729
				public static LocString DESC = "Bionic Duplicants are much more relaxed after a visit to the gunk extractor.";

				// Token: 0x0400B68A RID: 46730
				public static LocString EFFECT = "Cleanses stale " + UI.FormatAsLink("Gunk", "LIQUIDGUNK") + " build-up from Duplicants' bionic parts.";
			}

			// Token: 0x02002948 RID: 10568
			public class OILCHANGER
			{
				// Token: 0x0400B68B RID: 46731
				public static LocString NAME = UI.FormatAsLink("Lubrication Station", "OILCHANGER");

				// Token: 0x0400B68C RID: 46732
				public static LocString DESC = "A fresh supply of oil keeps the ol' joints from getting too creaky.";

				// Token: 0x0400B68D RID: 46733
				public static LocString EFFECT = "Uses " + UI.FormatAsLink("Gear Oil", "LUBRICATINGOIL") + " to keep Duplicants' bionic parts running smoothly.";
			}

			// Token: 0x02002949 RID: 10569
			public class OXYLITEREFINERY
			{
				// Token: 0x0400B68E RID: 46734
				public static LocString NAME = UI.FormatAsLink("Oxylite Refinery", "OXYLITEREFINERY");

				// Token: 0x0400B68F RID: 46735
				public static LocString DESC = "Oxylite is a solid and easily transportable source of consumable oxygen.";

				// Token: 0x0400B690 RID: 46736
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Synthesizes ",
					UI.FormatAsLink("Oxylite", "OXYROCK"),
					" using ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" and a small amount of ",
					UI.FormatAsLink("Gold", "GOLD"),
					"."
				});
			}

			// Token: 0x0200294A RID: 10570
			public class OXYSCONCE
			{
				// Token: 0x0400B691 RID: 46737
				public static LocString NAME = UI.FormatAsLink("Oxylite Sconce", "OXYSCONCE");

				// Token: 0x0400B692 RID: 46738
				public static LocString DESC = "Sconces prevent diffused oxygen from being wasted inside storage bins.";

				// Token: 0x0400B693 RID: 46739
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores a small chunk of ",
					UI.FormatAsLink("Oxylite", "OXYROCK"),
					" which gradually releases ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" into the environment."
				});
			}

			// Token: 0x0200294B RID: 10571
			public class FERTILIZERMAKER
			{
				// Token: 0x0400B694 RID: 46740
				public static LocString NAME = UI.FormatAsLink("Fertilizer Synthesizer", "FERTILIZERMAKER");

				// Token: 0x0400B695 RID: 46741
				public static LocString DESC = "Fertilizer synthesizers convert polluted dirt into fertilizer for domestic plants.";

				// Token: 0x0400B696 RID: 46742
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					" and ",
					UI.FormatAsLink("Phosphorite", "PHOSPHORITE"),
					" to produce ",
					UI.FormatAsLink("Fertilizer", "FERTILIZER"),
					"."
				});
			}

			// Token: 0x0200294C RID: 10572
			public class ALGAEHABITAT
			{
				// Token: 0x0400B697 RID: 46743
				public static LocString NAME = UI.FormatAsLink("Algae Terrarium", "ALGAEHABITAT");

				// Token: 0x0400B698 RID: 46744
				public static LocString DESC = "Algae colony, Duplicant colony... we're more alike than we are different.";

				// Token: 0x0400B699 RID: 46745
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Consumes ",
					UI.FormatAsLink("Algae", "ALGAE"),
					" to produce ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" and remove some ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					".\n\nGains a 10% efficiency boost in direct ",
					UI.FormatAsLink("Light", "LIGHT"),
					"."
				});

				// Token: 0x0400B69A RID: 46746
				public static LocString SIDESCREEN_TITLE = "Empty " + UI.FormatAsLink("Polluted Water", "DIRTYWATER") + " Threshold";
			}

			// Token: 0x0200294D RID: 10573
			public class BATTERY
			{
				// Token: 0x0400B69B RID: 46747
				public static LocString NAME = UI.FormatAsLink("Battery", "BATTERY");

				// Token: 0x0400B69C RID: 46748
				public static LocString DESC = "Batteries allow power from generators to be stored for later.";

				// Token: 0x0400B69D RID: 46749
				public static LocString EFFECT = "Stores " + UI.FormatAsLink("Power", "POWER") + " from generators, then provides that power to buildings.\n\nLoses charge over time.";

				// Token: 0x0400B69E RID: 46750
				public static LocString CHARGE_LOSS = "{Battery} charge loss";
			}

			// Token: 0x0200294E RID: 10574
			public class FLYINGCREATUREBAIT
			{
				// Token: 0x0400B69F RID: 46751
				public static LocString NAME = UI.FormatAsLink("Airborne Critter Bait", "FLYINGCREATUREBAIT");

				// Token: 0x0400B6A0 RID: 46752
				public static LocString DESC = "The type of critter attracted by critter bait depends on the construction material.";

				// Token: 0x0400B6A1 RID: 46753
				public static LocString EFFECT = "Attracts one type of airborne critter.\n\nSingle use.";
			}

			// Token: 0x0200294F RID: 10575
			public class WATERTRAP
			{
				// Token: 0x0400B6A2 RID: 46754
				public static LocString NAME = UI.FormatAsLink("Fish Trap", "WATERTRAP");

				// Token: 0x0400B6A3 RID: 46755
				public static LocString DESC = "Trapped fish will automatically be bagged for transport.";

				// Token: 0x0400B6A4 RID: 46756
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Attracts and traps swimming ",
					UI.FormatAsLink("Pacu", "PACU"),
					".\n\nOnly Duplicants with the ",
					UI.FormatAsLink("Critter Ranching I", "RANCHING1"),
					" skill can arm this trap. It's reusable!"
				});
			}

			// Token: 0x02002950 RID: 10576
			public class REUSABLETRAP
			{
				// Token: 0x0400B6A5 RID: 46757
				public static LocString LOGIC_PORT = "Trap Occupied";

				// Token: 0x0400B6A6 RID: 46758
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when a critter has been trapped";

				// Token: 0x0400B6A7 RID: 46759
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x0400B6A8 RID: 46760
				public static LocString INPUT_LOGIC_PORT = "Trap Setter";

				// Token: 0x0400B6A9 RID: 46761
				public static LocString INPUT_LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Set trap";

				// Token: 0x0400B6AA RID: 46762
				public static LocString INPUT_LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Disarm and empty trap";
			}

			// Token: 0x02002951 RID: 10577
			public class CREATUREAIRTRAP
			{
				// Token: 0x0400B6AB RID: 46763
				public static LocString NAME = UI.FormatAsLink("Airborne Critter Trap", "FLYINGCREATUREBAIT");

				// Token: 0x0400B6AC RID: 46764
				public static LocString DESC = "It needs to be armed prior to use.";

				// Token: 0x0400B6AD RID: 46765
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Attracts and captures airborne ",
					UI.FormatAsLink("Critters", "CREATURES"),
					".\n\nOnly Duplicants with the ",
					UI.FormatAsLink("Critter Ranching I", "RANCHING1"),
					" skill can arm this trap. It's reusable!"
				});
			}

			// Token: 0x02002952 RID: 10578
			public class AIRBORNECREATURELURE
			{
				// Token: 0x0400B6AE RID: 46766
				public static LocString NAME = UI.FormatAsLink("Airborne Critter Lure", "AIRBORNECREATURELURE");

				// Token: 0x0400B6AF RID: 46767
				public static LocString DESC = "Lures can relocate Pufts or Shine Bugs to specific locations in my colony.";

				// Token: 0x0400B6B0 RID: 46768
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Attracts one type of airborne critter at a time.\n\nMust be baited with ",
					UI.FormatAsLink("Slime", "SLIMEMOLD"),
					" or ",
					UI.FormatAsLink("Phosphorite", "PHOSPHORITE"),
					"."
				});
			}

			// Token: 0x02002953 RID: 10579
			public class BATTERYMEDIUM
			{
				// Token: 0x0400B6B1 RID: 46769
				public static LocString NAME = UI.FormatAsLink("Jumbo Battery", "BATTERYMEDIUM");

				// Token: 0x0400B6B2 RID: 46770
				public static LocString DESC = "Larger batteries hold more power and keep systems running longer before recharging.";

				// Token: 0x0400B6B3 RID: 46771
				public static LocString EFFECT = "Stores " + UI.FormatAsLink("Power", "POWER") + " from generators, then provides that power to buildings.\n\nSlightly loses charge over time.";
			}

			// Token: 0x02002954 RID: 10580
			public class BATTERYSMART
			{
				// Token: 0x0400B6B4 RID: 46772
				public static LocString NAME = UI.FormatAsLink("Smart Battery", "BATTERYSMART");

				// Token: 0x0400B6B5 RID: 46773
				public static LocString DESC = "Smart batteries send a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when they require charging.";

				// Token: 0x0400B6B6 RID: 46774
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Power", "POWER"),
					" from generators, then provides that power to buildings.\n\nSends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" based on the configuration of the Logic Activation Parameters.\n\nVery slightly loses charge over time."
				});

				// Token: 0x0400B6B7 RID: 46775
				public static LocString LOGIC_PORT = "Charge Parameters";

				// Token: 0x0400B6B8 RID: 46776
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when battery is less than <b>Low Threshold</b> charged, until <b>High Threshold</b> is reached again";

				// Token: 0x0400B6B9 RID: 46777
				public static LocString LOGIC_PORT_INACTIVE = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " when the battery is more than <b>High Threshold</b> charged, until <b>Low Threshold</b> is reached again";

				// Token: 0x0400B6BA RID: 46778
				public static LocString ACTIVATE_TOOLTIP = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when battery is less than <b>{0}%</b> charged, until it is <b>{1}% (High Threshold)</b> charged";

				// Token: 0x0400B6BB RID: 46779
				public static LocString DEACTIVATE_TOOLTIP = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " when battery is <b>{0}%</b> charged, until it is less than <b>{1}% (Low Threshold)</b> charged";

				// Token: 0x0400B6BC RID: 46780
				public static LocString SIDESCREEN_TITLE = "Logic Activation Parameters";

				// Token: 0x0400B6BD RID: 46781
				public static LocString SIDESCREEN_ACTIVATE = "Low Threshold:";

				// Token: 0x0400B6BE RID: 46782
				public static LocString SIDESCREEN_DEACTIVATE = "High Threshold:";
			}

			// Token: 0x02002955 RID: 10581
			public class BED
			{
				// Token: 0x0400B6BF RID: 46783
				public static LocString NAME = UI.FormatAsLink("Cot", "BED");

				// Token: 0x0400B6C0 RID: 46784
				public static LocString DESC = "Duplicants without a bed will develop sore backs from sleeping on the floor.";

				// Token: 0x0400B6C1 RID: 46785
				public static LocString EFFECT = "Gives one Duplicant a place to sleep.\n\nDuplicants will automatically return to their cots to sleep at night.";

				// Token: 0x02003895 RID: 14485
				public class FACADES
				{
					// Token: 0x02003BD9 RID: 15321
					public class DEFAULT_BED
					{
						// Token: 0x0400ECEA RID: 60650
						public static LocString NAME = UI.FormatAsLink("Cot", "BED");

						// Token: 0x0400ECEB RID: 60651
						public static LocString DESC = "A safe place to sleep.";
					}

					// Token: 0x02003BDA RID: 15322
					public class STARCURTAIN
					{
						// Token: 0x0400ECEC RID: 60652
						public static LocString NAME = UI.FormatAsLink("Stargazer Cot", "BED");

						// Token: 0x0400ECED RID: 60653
						public static LocString DESC = "Now Duplicants can sleep beneath the stars without wearing an Atmo Suit to bed.";
					}

					// Token: 0x02003BDB RID: 15323
					public class SCIENCELAB
					{
						// Token: 0x0400ECEE RID: 60654
						public static LocString NAME = UI.FormatAsLink("Lab Cot", "BED");

						// Token: 0x0400ECEF RID: 60655
						public static LocString DESC = "For the Duplicant who dreams of scientific discoveries.";
					}

					// Token: 0x02003BDC RID: 15324
					public class STAYCATION
					{
						// Token: 0x0400ECF0 RID: 60656
						public static LocString NAME = UI.FormatAsLink("Staycation Cot", "BED");

						// Token: 0x0400ECF1 RID: 60657
						public static LocString DESC = "Like a weekend away, except... not.";
					}

					// Token: 0x02003BDD RID: 15325
					public class CREAKY
					{
						// Token: 0x0400ECF2 RID: 60658
						public static LocString NAME = UI.FormatAsLink("Camping Cot", "BED");

						// Token: 0x0400ECF3 RID: 60659
						public static LocString DESC = "It's sturdier than it looks.";
					}

					// Token: 0x02003BDE RID: 15326
					public class STRINGLIGHTS
					{
						// Token: 0x0400ECF4 RID: 60660
						public static LocString NAME = "Good Job Cot";

						// Token: 0x0400ECF5 RID: 60661
						public static LocString DESC = "Wrapped in shiny gold stars, to help sleepy Duplicants feel accomplished.";
					}
				}
			}

			// Token: 0x02002956 RID: 10582
			public class BOTTLEEMPTIER
			{
				// Token: 0x0400B6C2 RID: 46786
				public static LocString NAME = UI.FormatAsLink("Bottle Emptier", "BOTTLEEMPTIER");

				// Token: 0x0400B6C3 RID: 46787
				public static LocString DESC = "A bottle emptier's Element Filter can be used to designate areas for specific liquid storage.";

				// Token: 0x0400B6C4 RID: 46788
				public static LocString EFFECT = "Empties bottled " + UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID") + " back into the world.";
			}

			// Token: 0x02002957 RID: 10583
			public class BOTTLEEMPTIERGAS
			{
				// Token: 0x0400B6C5 RID: 46789
				public static LocString NAME = UI.FormatAsLink("Canister Emptier", "BOTTLEEMPTIERGAS");

				// Token: 0x0400B6C6 RID: 46790
				public static LocString DESC = "A canister emptier's Element Filter can designate areas for specific gas storage.";

				// Token: 0x0400B6C7 RID: 46791
				public static LocString EFFECT = "Empties " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " canisters back into the world.";
			}

			// Token: 0x02002958 RID: 10584
			public class BOTTLEEMPTIERCONDUITLIQUID
			{
				// Token: 0x0400B6C8 RID: 46792
				public static LocString NAME = UI.FormatAsLink("Bottle Drainer", "BOTTLEEMPTIERCONDUITLIQUID");

				// Token: 0x0400B6C9 RID: 46793
				public static LocString DESC = "A bottle drainer's Element Filter can be used to designate areas for specific liquid storage.";

				// Token: 0x0400B6CA RID: 46794
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Drains bottled ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					" into ",
					UI.FormatAsLink("Liquid Pipes", "LIQUIDCONDUIT"),
					"."
				});
			}

			// Token: 0x02002959 RID: 10585
			public class BOTTLEEMPTIERCONDUITGAS
			{
				// Token: 0x0400B6CB RID: 46795
				public static LocString NAME = UI.FormatAsLink("Canister Drainer", "BOTTLEEMPTIERCONDUITGAS");

				// Token: 0x0400B6CC RID: 46796
				public static LocString DESC = "A canister drainer's Element Filter can designate areas for specific gas storage.";

				// Token: 0x0400B6CD RID: 46797
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Drains ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" canisters into ",
					UI.FormatAsLink("Gas Pipes", "GASCONDUIT"),
					"."
				});
			}

			// Token: 0x0200295A RID: 10586
			public class ARTIFACTCARGOBAY
			{
				// Token: 0x0400B6CE RID: 46798
				public static LocString NAME = UI.FormatAsLink("Artifact Transport Module", "ARTIFACTCARGOBAY");

				// Token: 0x0400B6CF RID: 46799
				public static LocString DESC = "Holds artifacts found in space.";

				// Token: 0x0400B6D0 RID: 46800
				public static LocString EFFECT = "Allows Duplicants to store any artifacts they uncover during space missions.\n\nArtifacts become available to the colony upon the rocket's return. \n\nMust be built via " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + ".";
			}

			// Token: 0x0200295B RID: 10587
			public class CARGOBAY
			{
				// Token: 0x0400B6D1 RID: 46801
				public static LocString NAME = UI.FormatAsLink("Cargo Bay", "CARGOBAY");

				// Token: 0x0400B6D2 RID: 46802
				public static LocString DESC = "Duplicants will fill cargo bays with any resources they find during space missions.";

				// Token: 0x0400B6D3 RID: 46803
				public static LocString EFFECT = "Allows Duplicants to store any " + UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID") + " found during space missions.\n\nStored resources become available to the colony upon the rocket's return.";
			}

			// Token: 0x0200295C RID: 10588
			public class CARGOBAYCLUSTER
			{
				// Token: 0x0400B6D4 RID: 46804
				public static LocString NAME = UI.FormatAsLink("Large Cargo Bay", "CARGOBAY");

				// Token: 0x0400B6D5 RID: 46805
				public static LocString DESC = "Holds more than a regular cargo bay.";

				// Token: 0x0400B6D6 RID: 46806
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to store most of the ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" found during space missions.\n\nStored resources become available to the colony upon the rocket's return. \n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x0200295D RID: 10589
			public class SOLIDCARGOBAYSMALL
			{
				// Token: 0x0400B6D7 RID: 46807
				public static LocString NAME = UI.FormatAsLink("Cargo Bay", "SOLIDCARGOBAYSMALL");

				// Token: 0x0400B6D8 RID: 46808
				public static LocString DESC = "Duplicants will fill cargo bays with any resources they find during space missions.";

				// Token: 0x0400B6D9 RID: 46809
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to store some of the ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" found during space missions.\n\nStored resources become available to the colony upon the rocket's return. \n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x0200295E RID: 10590
			public class SPECIALCARGOBAY
			{
				// Token: 0x0400B6DA RID: 46810
				public static LocString NAME = UI.FormatAsLink("Biological Cargo Bay", "SPECIALCARGOBAY");

				// Token: 0x0400B6DB RID: 46811
				public static LocString DESC = "Biological cargo bays allow Duplicants to retrieve alien plants and wildlife from space.";

				// Token: 0x0400B6DC RID: 46812
				public static LocString EFFECT = "Allows Duplicants to store unusual or organic resources found during space missions.\n\nStored resources become available to the colony upon the rocket's return.";
			}

			// Token: 0x0200295F RID: 10591
			public class SPECIALCARGOBAYCLUSTER
			{
				// Token: 0x0400B6DD RID: 46813
				public static LocString NAME = UI.FormatAsLink("Critter Cargo Bay", "SPECIALCARGOBAY");

				// Token: 0x0400B6DE RID: 46814
				public static LocString DESC = "Critters do not require feeding during transit.";

				// Token: 0x0400B6DF RID: 46815
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to transport ",
					UI.CODEX.CATEGORYNAMES.CREATURES,
					" through space.\n\nSpecimens can be released into the colony upon the rocket's return.\n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});

				// Token: 0x0400B6E0 RID: 46816
				public static LocString RELEASE_BTN = "Release Critter";

				// Token: 0x0400B6E1 RID: 46817
				public static LocString RELEASE_BTN_TOOLTIP = "Release the critter stored inside";
			}

			// Token: 0x02002960 RID: 10592
			public class COMMANDMODULE
			{
				// Token: 0x0400B6E2 RID: 46818
				public static LocString NAME = UI.FormatAsLink("Command Capsule", "COMMANDMODULE");

				// Token: 0x0400B6E3 RID: 46819
				public static LocString DESC = "At least one astronaut must be assigned to the command module to pilot a rocket.";

				// Token: 0x0400B6E4 RID: 46820
				public static LocString EFFECT = "Contains passenger seating for Duplicant " + UI.FormatAsLink("Astronauts", "ASTRONAUTING1") + ".\n\nA Command Capsule must be the last module installed at the top of a rocket.";

				// Token: 0x0400B6E5 RID: 46821
				public static LocString LOGIC_PORT_READY = "Rocket Checklist";

				// Token: 0x0400B6E6 RID: 46822
				public static LocString LOGIC_PORT_READY_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when its rocket launch checklist is complete";

				// Token: 0x0400B6E7 RID: 46823
				public static LocString LOGIC_PORT_READY_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x0400B6E8 RID: 46824
				public static LocString LOGIC_PORT_LAUNCH = "Launch Rocket";

				// Token: 0x0400B6E9 RID: 46825
				public static LocString LOGIC_PORT_LAUNCH_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Launch rocket";

				// Token: 0x0400B6EA RID: 46826
				public static LocString LOGIC_PORT_LAUNCH_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Awaits launch command";
			}

			// Token: 0x02002961 RID: 10593
			public class CLUSTERCOMMANDMODULE
			{
				// Token: 0x0400B6EB RID: 46827
				public static LocString NAME = UI.FormatAsLink("Command Capsule", "CLUSTERCOMMANDMODULE");

				// Token: 0x0400B6EC RID: 46828
				public static LocString DESC = "";

				// Token: 0x0400B6ED RID: 46829
				public static LocString EFFECT = "";

				// Token: 0x0400B6EE RID: 46830
				public static LocString LOGIC_PORT_READY = "Rocket Checklist";

				// Token: 0x0400B6EF RID: 46831
				public static LocString LOGIC_PORT_READY_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when its rocket launch checklist is complete";

				// Token: 0x0400B6F0 RID: 46832
				public static LocString LOGIC_PORT_READY_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x0400B6F1 RID: 46833
				public static LocString LOGIC_PORT_LAUNCH = "Launch Rocket";

				// Token: 0x0400B6F2 RID: 46834
				public static LocString LOGIC_PORT_LAUNCH_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Launch rocket";

				// Token: 0x0400B6F3 RID: 46835
				public static LocString LOGIC_PORT_LAUNCH_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Awaits launch command";
			}

			// Token: 0x02002962 RID: 10594
			public class CLUSTERCRAFTINTERIORDOOR
			{
				// Token: 0x0400B6F4 RID: 46836
				public static LocString NAME = UI.FormatAsLink("Interior Hatch", "CLUSTERCRAFTINTERIORDOOR");

				// Token: 0x0400B6F5 RID: 46837
				public static LocString DESC = "A hatch for getting in and out of the rocket.";

				// Token: 0x0400B6F6 RID: 46838
				public static LocString EFFECT = "Warning: Do not open mid-flight.";
			}

			// Token: 0x02002963 RID: 10595
			public class ROBOPILOTMODULE
			{
				// Token: 0x0400B6F7 RID: 46839
				public static LocString NAME = UI.FormatAsLink("Robo-Pilot Module", "ROBOPILOTMODULE");

				// Token: 0x0400B6F8 RID: 46840
				public static LocString DESC = "Robo-pilot modules do not require a Duplicant astronaut.";

				// Token: 0x0400B6F9 RID: 46841
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Enables rockets to travel swfitly without a ",
					UI.FormatAsLink("Rocket Control Station", "ROCKETCONTROLSTATION"),
					".\n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x02002964 RID: 10596
			public class ROBOPILOTCOMMANDMODULE
			{
				// Token: 0x0400B6FA RID: 46842
				public static LocString NAME = UI.FormatAsLink("Robo-Pilot Capsule", "ROBOPILOTCOMMANDMODULE");

				// Token: 0x0400B6FB RID: 46843
				public static LocString DESC = "Robo-pilot modules do not require a Duplicant astronaut.";

				// Token: 0x0400B6FC RID: 46844
				public static LocString EFFECT = "Enables rockets to travel swiftly and safely without a " + UI.FormatAsLink("Command Capsule", "COMMANDMODULE") + ".\n\nA Robo-Pilot Capsule must be the last module installed at the top of a rocket.";
			}

			// Token: 0x02002965 RID: 10597
			public class ROCKETCONTROLSTATION
			{
				// Token: 0x0400B6FD RID: 46845
				public static LocString NAME = UI.FormatAsLink("Rocket Control Station", "ROCKETCONTROLSTATION");

				// Token: 0x0400B6FE RID: 46846
				public static LocString DESC = "Someone needs to be around to jiggle the controls when the screensaver comes on.";

				// Token: 0x0400B6FF RID: 46847
				public static LocString EFFECT = "Allows Duplicants to use pilot-operated rockets and control access to interior buildings.\n\nAssigned Duplicants must have the " + UI.FormatAsLink("Rocket Piloting", "ROCKETPILOTING1") + " skill.";

				// Token: 0x0400B700 RID: 46848
				public static LocString LOGIC_PORT = "Restrict Building Usage";

				// Token: 0x0400B701 RID: 46849
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Restrict access to interior buildings";

				// Token: 0x0400B702 RID: 46850
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Unrestrict access to interior buildings";
			}

			// Token: 0x02002966 RID: 10598
			public class RESEARCHMODULE
			{
				// Token: 0x0400B703 RID: 46851
				public static LocString NAME = UI.FormatAsLink("Research Module", "RESEARCHMODULE");

				// Token: 0x0400B704 RID: 46852
				public static LocString DESC = "Data banks can be used at virtual planetariums to produce additional research.";

				// Token: 0x0400B705 RID: 46853
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Completes one ",
					UI.FormatAsLink("Research Task", "RESEARCH"),
					" per space mission.\n\nProduces a small Data Bank regardless of mission destination.\n\nGenerated ",
					UI.FormatAsLink("Research Points", "RESEARCH"),
					" become available upon the rocket's return."
				});
			}

			// Token: 0x02002967 RID: 10599
			public class TOURISTMODULE
			{
				// Token: 0x0400B706 RID: 46854
				public static LocString NAME = UI.FormatAsLink("Sight-Seeing Module", "TOURISTMODULE");

				// Token: 0x0400B707 RID: 46855
				public static LocString DESC = "An astronaut must accompany sight seeing Duplicants on rocket flights.";

				// Token: 0x0400B708 RID: 46856
				public static LocString EFFECT = "Allows one non-Astronaut Duplicant to visit space.\n\nSight-Seeing Rocket flights decrease " + UI.FormatAsLink("Stress", "STRESS") + ".";
			}

			// Token: 0x02002968 RID: 10600
			public class SCANNERMODULE
			{
				// Token: 0x0400B709 RID: 46857
				public static LocString NAME = UI.FormatAsLink("Cartographic Module", "SCANNERMODULE");

				// Token: 0x0400B70A RID: 46858
				public static LocString DESC = "Allows Duplicants to boldly go where other Duplicants haven't been yet.";

				// Token: 0x0400B70B RID: 46859
				public static LocString EFFECT = "Automatically analyzes adjacent space while on a voyage. \n\nMust be built via " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + ".";
			}

			// Token: 0x02002969 RID: 10601
			public class HABITATMODULESMALL
			{
				// Token: 0x0400B70C RID: 46860
				public static LocString NAME = UI.FormatAsLink("Solo Spacefarer Nosecone", "HABITATMODULESMALL");

				// Token: 0x0400B70D RID: 46861
				public static LocString DESC = "One lucky Duplicant gets the best view from the whole rocket.";

				// Token: 0x0400B70E RID: 46862
				public static LocString EFFECT = "Functions as a Command Module and a Nosecone.\n\nHolds one Duplicant traveller.\n\nOne Command Module may be installed per rocket.\n\nMust be built via " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + ". \n\nMust be built at the top of a rocket.";
			}

			// Token: 0x0200296A RID: 10602
			public class HABITATMODULEMEDIUM
			{
				// Token: 0x0400B70F RID: 46863
				public static LocString NAME = UI.FormatAsLink("Spacefarer Module", "HABITATMODULEMEDIUM");

				// Token: 0x0400B710 RID: 46864
				public static LocString DESC = "Allows Duplicants to survive space travel... Hopefully.";

				// Token: 0x0400B711 RID: 46865
				public static LocString EFFECT = "Functions as a Command Module.\n\nHolds up to ten Duplicant travellers.\n\nOne Command Module may be installed per rocket. \n\nEngine must be built via " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + ".";
			}

			// Token: 0x0200296B RID: 10603
			public class NOSECONEBASIC
			{
				// Token: 0x0400B712 RID: 46866
				public static LocString NAME = UI.FormatAsLink("Basic Nosecone", "NOSECONEBASIC");

				// Token: 0x0400B713 RID: 46867
				public static LocString DESC = "Every rocket requires a nosecone to fly.";

				// Token: 0x0400B714 RID: 46868
				public static LocString EFFECT = "Protects a rocket during takeoff and entry, enabling space travel.\n\nEngine must be built via " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + ". \n\nMust be built at the top of a rocket.";
			}

			// Token: 0x0200296C RID: 10604
			public class NOSECONEHARVEST
			{
				// Token: 0x0400B715 RID: 46869
				public static LocString NAME = UI.FormatAsLink("Drillcone", "NOSECONEHARVEST");

				// Token: 0x0400B716 RID: 46870
				public static LocString DESC = "Harvests resources from the universe.";

				// Token: 0x0400B717 RID: 46871
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Enables a rocket to drill into interstellar debris and collect ",
					UI.FormatAsLink("gas", "ELEMENTS_GAS"),
					", ",
					UI.FormatAsLink("liquid", "ELEMENTS_LIQUID"),
					" and ",
					UI.FormatAsLink("solid", "ELEMENTS_SOLID"),
					" resources from space.\n\nEngine must be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					". \n\nMust be built at the top of a rocket with ",
					UI.FormatAsLink("gas", "ELEMENTS_GAS"),
					", ",
					UI.FormatAsLink("liquid", "ELEMENTS_LIQUID"),
					" or ",
					UI.FormatAsLink("solid", "ELEMENTS_SOLID"),
					" Cargo Module attached to store the appropriate resources."
				});
			}

			// Token: 0x0200296D RID: 10605
			public class CO2ENGINE
			{
				// Token: 0x0400B718 RID: 46872
				public static LocString NAME = UI.FormatAsLink("Carbon Dioxide Engine", "CO2ENGINE");

				// Token: 0x0400B719 RID: 46873
				public static LocString DESC = "Rockets can be used to send Duplicants into space and retrieve rare resources.";

				// Token: 0x0400B71A RID: 46874
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses pressurized ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					" to propel rockets for short range space exploration.\n\nCarbon Dioxide Engines are relatively fast engine for their size but with limited height restrictions.\n\nEngine must be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					". \n\nOnce the engine has been built, more rocket modules can be added."
				});
			}

			// Token: 0x0200296E RID: 10606
			public class KEROSENEENGINE
			{
				// Token: 0x0400B71B RID: 46875
				public static LocString NAME = UI.FormatAsLink("Petroleum Engine", "KEROSENEENGINE");

				// Token: 0x0400B71C RID: 46876
				public static LocString DESC = "Rockets can be used to send Duplicants into space and retrieve rare resources.";

				// Token: 0x0400B71D RID: 46877
				public static LocString EFFECT = "Burns " + UI.FormatAsLink("Petroleum", "PETROLEUM") + " to propel rockets for mid-range space exploration.\n\nPetroleum Engines have generous height restrictions, ideal for hauling many modules.\n\nThe engine must be built first before more rocket modules can be added.";
			}

			// Token: 0x0200296F RID: 10607
			public class KEROSENEENGINECLUSTER
			{
				// Token: 0x0400B71E RID: 46878
				public static LocString NAME = UI.FormatAsLink("Petroleum Engine", "KEROSENEENGINECLUSTER");

				// Token: 0x0400B71F RID: 46879
				public static LocString DESC = "More powerful rocket engines can propel heavier burdens.";

				// Token: 0x0400B720 RID: 46880
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Burns ",
					UI.FormatAsLink("Petroleum", "PETROLEUM"),
					" to propel rockets for mid-range space exploration.\n\nPetroleum Engines have generous height restrictions, ideal for hauling many modules.\n\nEngine must be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					". \n\nOnce the engine has been built, more rocket modules can be added."
				});
			}

			// Token: 0x02002970 RID: 10608
			public class KEROSENEENGINECLUSTERSMALL
			{
				// Token: 0x0400B721 RID: 46881
				public static LocString NAME = UI.FormatAsLink("Small Petroleum Engine", "KEROSENEENGINECLUSTERSMALL");

				// Token: 0x0400B722 RID: 46882
				public static LocString DESC = "Rockets can be used to send Duplicants into space and retrieve rare resources.";

				// Token: 0x0400B723 RID: 46883
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Burns ",
					UI.FormatAsLink("Petroleum", "PETROLEUM"),
					" to propel rockets for mid-range space exploration.\n\nSmall Petroleum Engines possess the same speed as a ",
					UI.FormatAsLink("Petroleum Engines", "KEROSENEENGINE"),
					" but have smaller height restrictions.\n\nEngine must be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					". \n\nOnce the engine has been built, more rocket modules can be added."
				});
			}

			// Token: 0x02002971 RID: 10609
			public class HYDROGENENGINE
			{
				// Token: 0x0400B724 RID: 46884
				public static LocString NAME = UI.FormatAsLink("Hydrogen Engine", "HYDROGENENGINE");

				// Token: 0x0400B725 RID: 46885
				public static LocString DESC = "Hydrogen engines can propel rockets further than steam or petroleum engines.";

				// Token: 0x0400B726 RID: 46886
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Burns ",
					UI.FormatAsLink("Liquid Hydrogen", "LIQUIDHYDROGEN"),
					" to propel rockets for long-range space exploration.\n\nHydrogen Engines have the same generous height restrictions as ",
					UI.FormatAsLink("Petroleum Engines", "KEROSENEENGINE"),
					" but are slightly faster.\n\nThe engine must be built first before more rocket modules can be added."
				});
			}

			// Token: 0x02002972 RID: 10610
			public class HYDROGENENGINECLUSTER
			{
				// Token: 0x0400B727 RID: 46887
				public static LocString NAME = UI.FormatAsLink("Hydrogen Engine", "HYDROGENENGINECLUSTER");

				// Token: 0x0400B728 RID: 46888
				public static LocString DESC = "Hydrogen engines can propel rockets further than steam or petroleum engines.";

				// Token: 0x0400B729 RID: 46889
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Burns ",
					UI.FormatAsLink("Liquid Hydrogen", "LIQUIDHYDROGEN"),
					" to propel rockets for long-range space exploration.\n\nHydrogen Engines have the same generous height restrictions as ",
					UI.FormatAsLink("Petroleum Engines", "KEROSENEENGINE"),
					" but are slightly faster.\n\nEngine must be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					".\n\nOnce the engine has been built, more rocket modules can be added."
				});
			}

			// Token: 0x02002973 RID: 10611
			public class SUGARENGINE
			{
				// Token: 0x0400B72A RID: 46890
				public static LocString NAME = UI.FormatAsLink("Sugar Engine", "SUGARENGINE");

				// Token: 0x0400B72B RID: 46891
				public static LocString DESC = "Not the most stylish way to travel space, but certainly the tastiest.";

				// Token: 0x0400B72C RID: 46892
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Burns ",
					UI.FormatAsLink("Sucrose", "SUCROSE"),
					" to propel rockets for short range space exploration.\n\nSugar Engines have higher height restrictions than ",
					UI.FormatAsLink("Carbon Dioxide Engines", "CO2ENGINE"),
					", but move slower.\n\nEngine must be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					". \n\nOnce the engine has been built, more rocket modules can be added."
				});
			}

			// Token: 0x02002974 RID: 10612
			public class HEPENGINE
			{
				// Token: 0x0400B72D RID: 46893
				public static LocString NAME = UI.FormatAsLink("Radbolt Engine", "HEPENGINE");

				// Token: 0x0400B72E RID: 46894
				public static LocString DESC = "Radbolt-fueled rockets support few modules, but travel exceptionally far.";

				// Token: 0x0400B72F RID: 46895
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Injects ",
					UI.FormatAsLink("Radbolts", "RADIATION"),
					" into a reaction chamber to propel rockets for long-range space exploration.\n\nRadbolt Engines are faster than ",
					UI.FormatAsLink("Hydrogen Engines", "HYDROGENENGINE"),
					" but with a more restrictive height allowance.\n\nEngine must be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					". \n\nOnce the engine has been built, more rocket modules can be added."
				});

				// Token: 0x0400B730 RID: 46896
				public static LocString LOGIC_PORT_STORAGE = "Radbolt Storage";

				// Token: 0x0400B731 RID: 46897
				public static LocString LOGIC_PORT_STORAGE_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when its Radbolt Storage is full";

				// Token: 0x0400B732 RID: 46898
				public static LocString LOGIC_PORT_STORAGE_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002975 RID: 10613
			public class ORBITALCARGOMODULE
			{
				// Token: 0x0400B733 RID: 46899
				public static LocString NAME = UI.FormatAsLink("Orbital Cargo Module", "ORBITALCARGOMODULE");

				// Token: 0x0400B734 RID: 46900
				public static LocString DESC = "It's a generally good idea to pack some supplies when exploring unknown worlds.";

				// Token: 0x0400B735 RID: 46901
				public static LocString EFFECT = "Delivers cargo to the surface of Planetoids that do not yet have a " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + ". \n\nMust be built via Rocket Platform.";
			}

			// Token: 0x02002976 RID: 10614
			public class BATTERYMODULE
			{
				// Token: 0x0400B736 RID: 46902
				public static LocString NAME = UI.FormatAsLink("Battery Module", "BATTERYMODULE");

				// Token: 0x0400B737 RID: 46903
				public static LocString DESC = "Charging a battery module before takeoff makes it easier to power buildings during flight.";

				// Token: 0x0400B738 RID: 46904
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores the excess ",
					UI.FormatAsLink("Power", "POWER"),
					" generated by a Rocket Engine or ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					".\n\nProvides stored power to ",
					UI.FormatAsLink("Interior Rocket Outlets", "ROCKETINTERIORPOWERPLUG"),
					".\n\nLoses charge over time. \n\nMust be built via Rocket Platform."
				});
			}

			// Token: 0x02002977 RID: 10615
			public class PIONEERMODULE
			{
				// Token: 0x0400B739 RID: 46905
				public static LocString NAME = UI.FormatAsLink("Trailblazer Module", "PIONEERMODULE");

				// Token: 0x0400B73A RID: 46906
				public static LocString DESC = "That's one small step for Dupekind.";

				// Token: 0x0400B73B RID: 46907
				public static LocString EFFECT = "Enables travel to Planetoids that do not yet have a " + BUILDINGS.PREFABS.LAUNCHPAD.NAME + ".\n\nCan hold one Duplicant traveller.\n\nDeployment is available while in a Starmap hex adjacent to a Planetoid. \n\nMust be built via Rocket Platform.";
			}

			// Token: 0x02002978 RID: 10616
			public class SOLARPANELMODULE
			{
				// Token: 0x0400B73C RID: 46908
				public static LocString NAME = UI.FormatAsLink("Solar Panel Module", "SOLARPANELMODULE");

				// Token: 0x0400B73D RID: 46909
				public static LocString DESC = "Collect solar energy before takeoff and during flight.";

				// Token: 0x0400B73E RID: 46910
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Sunlight", "LIGHT"),
					" into electrical ",
					UI.FormatAsLink("Power", "POWER"),
					" for use on rockets.\n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					". \n\nMust be exposed to space."
				});
			}

			// Token: 0x02002979 RID: 10617
			public class SCOUTMODULE
			{
				// Token: 0x0400B73F RID: 46911
				public static LocString NAME = UI.FormatAsLink("Rover's Module", "SCOUTMODULE");

				// Token: 0x0400B740 RID: 46912
				public static LocString DESC = "Rover can conduct explorations of planetoids that don't have rocket platforms built.";

				// Token: 0x0400B741 RID: 46913
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Deploys one ",
					UI.FormatAsLink("Rover Bot", "SCOUT"),
					" for remote Planetoid exploration.\n\nDeployment is available while in a Starmap hex adjacent to a Planetoid. \n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x0200297A RID: 10618
			public class PIONEERLANDER
			{
				// Token: 0x0400B742 RID: 46914
				public static LocString NAME = UI.FormatAsLink("Trailblazer Lander", "PIONEERLANDER");

				// Token: 0x0400B743 RID: 46915
				public static LocString DESC = "Lands a Duplicant on a Planetoid from an orbiting " + BUILDINGS.PREFABS.PIONEERMODULE.NAME + ".";
			}

			// Token: 0x0200297B RID: 10619
			public class SCOUTLANDER
			{
				// Token: 0x0400B744 RID: 46916
				public static LocString NAME = UI.FormatAsLink("Rover's Lander", "SCOUTLANDER");

				// Token: 0x0400B745 RID: 46917
				public static LocString DESC = string.Concat(new string[]
				{
					"Lands ",
					UI.FormatAsLink("Rover", "SCOUT"),
					" on a Planetoid when ",
					BUILDINGS.PREFABS.SCOUTMODULE.NAME,
					" is in orbit."
				});
			}

			// Token: 0x0200297C RID: 10620
			public class GANTRY
			{
				// Token: 0x0400B746 RID: 46918
				public static LocString NAME = UI.FormatAsLink("Gantry", "GANTRY");

				// Token: 0x0400B747 RID: 46919
				public static LocString DESC = "A gantry can be built over rocket pieces where ladders and tile cannot.";

				// Token: 0x0400B748 RID: 46920
				public static LocString EFFECT = "Provides scaffolding across rocket modules to allow Duplicant access.";

				// Token: 0x0400B749 RID: 46921
				public static LocString LOGIC_PORT = "Extend/Retract";

				// Token: 0x0400B74A RID: 46922
				public static LocString LOGIC_PORT_ACTIVE = "<b>Extends gantry</b> when a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " signal is received";

				// Token: 0x0400B74B RID: 46923
				public static LocString LOGIC_PORT_INACTIVE = "<b>Retracts gantry</b> when a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " signal is received";
			}

			// Token: 0x0200297D RID: 10621
			public class ROCKETINTERIORPOWERPLUG
			{
				// Token: 0x0400B74C RID: 46924
				public static LocString NAME = UI.FormatAsLink("Power Outlet Fitting", "ROCKETINTERIORPOWERPLUG");

				// Token: 0x0400B74D RID: 46925
				public static LocString DESC = "Outlets conveniently power buildings inside a cockpit using their rocket's power stores.";

				// Token: 0x0400B74E RID: 46926
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Provides ",
					UI.FormatAsLink("Power", "POWER"),
					" to connected buildings.\n\nPulls power from ",
					UI.FormatAsLink("Battery Modules", "BATTERYMODULE"),
					" and Rocket Engines.\n\nMust be built within the interior of a Rocket Module."
				});
			}

			// Token: 0x0200297E RID: 10622
			public class ROCKETINTERIORLIQUIDINPUT
			{
				// Token: 0x0400B74F RID: 46927
				public static LocString NAME = UI.FormatAsLink("Liquid Intake Fitting", "ROCKETINTERIORLIQUIDINPUT");

				// Token: 0x0400B750 RID: 46928
				public static LocString DESC = "Begone, foul waters!";

				// Token: 0x0400B751 RID: 46929
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					" to be pumped into rocket storage via ",
					UI.FormatAsLink("Pipes", "LIQUIDCONDUIT"),
					".\n\nSends liquid to the first Rocket Module with available space.\n\nMust be built within the interior of a Rocket Module."
				});
			}

			// Token: 0x0200297F RID: 10623
			public class ROCKETINTERIORLIQUIDOUTPUT
			{
				// Token: 0x0400B752 RID: 46930
				public static LocString NAME = UI.FormatAsLink("Liquid Output Fitting", "ROCKETINTERIORLIQUIDOUTPUT");

				// Token: 0x0400B753 RID: 46931
				public static LocString DESC = "Now if only we had some water balloons...";

				// Token: 0x0400B754 RID: 46932
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					" to be drawn from rocket storage via ",
					UI.FormatAsLink("Pipes", "LIQUIDCONDUIT"),
					".\n\nDraws liquid from the first Rocket Module with the requested material.\n\nMust be built within the interior of a Rocket Module."
				});
			}

			// Token: 0x02002980 RID: 10624
			public class ROCKETINTERIORGASINPUT
			{
				// Token: 0x0400B755 RID: 46933
				public static LocString NAME = UI.FormatAsLink("Gas Intake Fitting", "ROCKETINTERIORGASINPUT");

				// Token: 0x0400B756 RID: 46934
				public static LocString DESC = "It's basically central-vac.";

				// Token: 0x0400B757 RID: 46935
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows ",
					UI.FormatAsLink("Gases", "ELEMENTS_GAS"),
					" to be pumped into rocket storage via ",
					UI.FormatAsLink("Pipes", "GASCONDUIT"),
					".\n\nSends gas to the first Rocket Module with available space.\n\nMust be built within the interior of a Rocket Module."
				});
			}

			// Token: 0x02002981 RID: 10625
			public class ROCKETINTERIORGASOUTPUT
			{
				// Token: 0x0400B758 RID: 46936
				public static LocString NAME = UI.FormatAsLink("Gas Output Fitting", "ROCKETINTERIORGASOUTPUT");

				// Token: 0x0400B759 RID: 46937
				public static LocString DESC = "Refreshing breezes, on-demand.";

				// Token: 0x0400B75A RID: 46938
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows ",
					UI.FormatAsLink("Gases", "ELEMENTS_GAS"),
					" to be drawn from rocket storage via ",
					UI.FormatAsLink("Pipes", "GASCONDUIT"),
					".\n\nDraws gas from the first Rocket Module with the requested material.\n\nMust be built within the interior of a Rocket Module."
				});
			}

			// Token: 0x02002982 RID: 10626
			public class ROCKETINTERIORSOLIDINPUT
			{
				// Token: 0x0400B75B RID: 46939
				public static LocString NAME = UI.FormatAsLink("Conveyor Receptacle Fitting", "ROCKETINTERIORSOLIDINPUT");

				// Token: 0x0400B75C RID: 46940
				public static LocString DESC = "Why organize your shelves when you can just shove everything in here?";

				// Token: 0x0400B75D RID: 46941
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" to be moved into rocket storage via ",
					UI.FormatAsLink("Conveyor Rails", "SOLIDCONDUIT"),
					".\n\nSends solid material to the first Rocket Module with available space.\n\nMust be built within the interior of a Rocket Module."
				});
			}

			// Token: 0x02002983 RID: 10627
			public class ROCKETINTERIORSOLIDOUTPUT
			{
				// Token: 0x0400B75E RID: 46942
				public static LocString NAME = UI.FormatAsLink("Conveyor Loader Fitting", "ROCKETINTERIORSOLIDOUTPUT");

				// Token: 0x0400B75F RID: 46943
				public static LocString DESC = "For accessing your stored luggage mid-flight.";

				// Token: 0x0400B760 RID: 46944
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" to be moved out of rocket storage via ",
					UI.FormatAsLink("Conveyor Rails", "SOLIDCONDUIT"),
					".\n\nDraws solid material from the first Rocket Module with the requested material.\n\nMust be built within the interior of a Rocket Module."
				});
			}

			// Token: 0x02002984 RID: 10628
			public class WATERCOOLER
			{
				// Token: 0x0400B761 RID: 46945
				public static LocString NAME = UI.FormatAsLink("Water Cooler", "WATERCOOLER");

				// Token: 0x0400B762 RID: 46946
				public static LocString DESC = "Chatting with friends improves Duplicants' moods and reduces their stress.";

				// Token: 0x0400B763 RID: 46947
				public static LocString EFFECT = "Provides a gathering place for Duplicants during Downtime.\n\nImproves Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";

				// Token: 0x02003896 RID: 14486
				public class OPTION_TOOLTIPS
				{
					// Token: 0x0400E4A2 RID: 58530
					public static LocString WATER = ELEMENTS.WATER.NAME + "\nPlain potable water";

					// Token: 0x0400E4A3 RID: 58531
					public static LocString MILK = ELEMENTS.MILK.NAME + "\nA salty, green-hued beverage";
				}

				// Token: 0x02003897 RID: 14487
				public class FACADES
				{
					// Token: 0x02003BDF RID: 15327
					public class DEFAULT_WATERCOOLER
					{
						// Token: 0x0400ECF6 RID: 60662
						public static LocString NAME = UI.FormatAsLink("Water Cooler", "WATERCOOLER");

						// Token: 0x0400ECF7 RID: 60663
						public static LocString DESC = "Where Duplicants sip and socialize.";
					}

					// Token: 0x02003BE0 RID: 15328
					public class ROUND_BODY
					{
						// Token: 0x0400ECF8 RID: 60664
						public static LocString NAME = UI.FormatAsLink("Elegant Water Cooler", "WATERCOOLER");

						// Token: 0x0400ECF9 RID: 60665
						public static LocString DESC = "It really classes up a breakroom.";
					}

					// Token: 0x02003BE1 RID: 15329
					public class BALLOON
					{
						// Token: 0x0400ECFA RID: 60666
						public static LocString NAME = UI.FormatAsLink("Inflatable Water Cooler", "WATERCOOLER");

						// Token: 0x0400ECFB RID: 60667
						public static LocString DESC = "There's a funny aftertaste.";
					}

					// Token: 0x02003BE2 RID: 15330
					public class YELLOW_TARTAR
					{
						// Token: 0x0400ECFC RID: 60668
						public static LocString NAME = UI.FormatAsLink("Ick Yellow Water Cooler", "WATERCOOLER");

						// Token: 0x0400ECFD RID: 60669
						public static LocString DESC = "Did someone boil eggs in this water?";
					}

					// Token: 0x02003BE3 RID: 15331
					public class RED_ROSE
					{
						// Token: 0x0400ECFE RID: 60670
						public static LocString NAME = UI.FormatAsLink("Puce Pink Water Cooler", "WATERCOOLER");

						// Token: 0x0400ECFF RID: 60671
						public static LocString DESC = "Rose-colored paper cups: the shatter-proof alternative to rose-colored glasses.";
					}

					// Token: 0x02003BE4 RID: 15332
					public class GREEN_MUSH
					{
						// Token: 0x0400ED00 RID: 60672
						public static LocString NAME = UI.FormatAsLink("Mush Green Water Cooler", "WATERCOOLER");

						// Token: 0x0400ED01 RID: 60673
						public static LocString DESC = "Ideal for post-Mush Bar palate cleansing.";
					}

					// Token: 0x02003BE5 RID: 15333
					public class PURPLE_BRAINFAT
					{
						// Token: 0x0400ED02 RID: 60674
						public static LocString NAME = UI.FormatAsLink("Faint Purple Water Cooler", "WATERCOOLER");

						// Token: 0x0400ED03 RID: 60675
						public static LocString DESC = "Most Duplicants agree that it really should dispense juice.";
					}

					// Token: 0x02003BE6 RID: 15334
					public class BLUE_BABYTEARS
					{
						// Token: 0x0400ED04 RID: 60676
						public static LocString NAME = UI.FormatAsLink("Weepy Blue Water Cooler", "WATERCOOLER");

						// Token: 0x0400ED05 RID: 60677
						public static LocString DESC = "Lightly salted with Duplicants' tears.";
					}
				}
			}

			// Token: 0x02002985 RID: 10629
			public class ARCADEMACHINE
			{
				// Token: 0x0400B764 RID: 46948
				public static LocString NAME = UI.FormatAsLink("Arcade Cabinet", "ARCADEMACHINE");

				// Token: 0x0400B765 RID: 46949
				public static LocString DESC = "Komet Kablam-O!\nFor up to two players.";

				// Token: 0x0400B766 RID: 46950
				public static LocString EFFECT = "Allows Duplicants to play video games on their breaks.\n\nIncreases Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";
			}

			// Token: 0x02002986 RID: 10630
			public class SINGLEPLAYERARCADE
			{
				// Token: 0x0400B767 RID: 46951
				public static LocString NAME = UI.FormatAsLink("Single Player Arcade", "SINGLEPLAYERARCADE");

				// Token: 0x0400B768 RID: 46952
				public static LocString DESC = "Space Brawler IV! For one player.";

				// Token: 0x0400B769 RID: 46953
				public static LocString EFFECT = "Allows a Duplicant to play video games solo on their breaks.\n\nIncreases Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";
			}

			// Token: 0x02002987 RID: 10631
			public class PHONOBOX
			{
				// Token: 0x0400B76A RID: 46954
				public static LocString NAME = UI.FormatAsLink("Jukebot", "PHONOBOX");

				// Token: 0x0400B76B RID: 46955
				public static LocString DESC = "Dancing helps Duplicants get their innermost feelings out.";

				// Token: 0x0400B76C RID: 46956
				public static LocString EFFECT = "Plays music for Duplicants to dance to on their breaks.\n\nIncreases Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";
			}

			// Token: 0x02002988 RID: 10632
			public class JUICER
			{
				// Token: 0x0400B76D RID: 46957
				public static LocString NAME = UI.FormatAsLink("Juicer", "JUICER");

				// Token: 0x0400B76E RID: 46958
				public static LocString DESC = "Fruity juice can really brighten a Duplicant's breaktime";

				// Token: 0x0400B76F RID: 46959
				public static LocString EFFECT = "Provides refreshment for Duplicants on their breaks.\n\nDrinking juice increases Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";
			}

			// Token: 0x02002989 RID: 10633
			public class ESPRESSOMACHINE
			{
				// Token: 0x0400B770 RID: 46960
				public static LocString NAME = UI.FormatAsLink("Espresso Machine", "ESPRESSOMACHINE");

				// Token: 0x0400B771 RID: 46961
				public static LocString DESC = "A shot of espresso helps Duplicants relax after a long day.";

				// Token: 0x0400B772 RID: 46962
				public static LocString EFFECT = "Provides refreshment for Duplicants on their breaks.\n\nIncreases Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";
			}

			// Token: 0x0200298A RID: 10634
			public class TELEPHONE
			{
				// Token: 0x0400B773 RID: 46963
				public static LocString NAME = UI.FormatAsLink("Party Line Phone", "TELEPHONE");

				// Token: 0x0400B774 RID: 46964
				public static LocString DESC = "You never know who you'll meet on the other line.";

				// Token: 0x0400B775 RID: 46965
				public static LocString EFFECT = "Can be used by one Duplicant to chat with themselves or with other Duplicants in different locations.\n\nChatting increases Duplicant " + UI.FormatAsLink("Morale", "MORALE") + ".";

				// Token: 0x0400B776 RID: 46966
				public static LocString EFFECT_BABBLE = "{attrib}: {amount} (No One)";

				// Token: 0x0400B777 RID: 46967
				public static LocString EFFECT_BABBLE_TOOLTIP = "Duplicants will gain {amount} {attrib} if they chat only with themselves.";

				// Token: 0x0400B778 RID: 46968
				public static LocString EFFECT_CHAT = "{attrib}: {amount} (At least one Duplicant)";

				// Token: 0x0400B779 RID: 46969
				public static LocString EFFECT_CHAT_TOOLTIP = "Duplicants will gain {amount} {attrib} if they chat with at least one other Duplicant.";

				// Token: 0x0400B77A RID: 46970
				public static LocString EFFECT_LONG_DISTANCE = "{attrib}: {amount} (At least one Duplicant across space)";

				// Token: 0x0400B77B RID: 46971
				public static LocString EFFECT_LONG_DISTANCE_TOOLTIP = "Duplicants will gain {amount} {attrib} if they chat with at least one other Duplicant across space.";
			}

			// Token: 0x0200298B RID: 10635
			public class MODULARLIQUIDINPUT
			{
				// Token: 0x0400B77C RID: 46972
				public static LocString NAME = UI.FormatAsLink("Liquid Input Hub", "MODULARLIQUIDINPUT");

				// Token: 0x0400B77D RID: 46973
				public static LocString DESC = "A hub from which to input " + UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID") + ".";
			}

			// Token: 0x0200298C RID: 10636
			public class MODULARSOLIDINPUT
			{
				// Token: 0x0400B77E RID: 46974
				public static LocString NAME = UI.FormatAsLink("Solid Input Hub", "MODULARSOLIDINPUT");

				// Token: 0x0400B77F RID: 46975
				public static LocString DESC = "A hub from which to input " + UI.FormatAsLink("Solids", "ELEMENTS_SOLID") + ".";
			}

			// Token: 0x0200298D RID: 10637
			public class MODULARGASINPUT
			{
				// Token: 0x0400B780 RID: 46976
				public static LocString NAME = UI.FormatAsLink("Gas Input Hub", "MODULARGASINPUT");

				// Token: 0x0400B781 RID: 46977
				public static LocString DESC = "A hub from which to input " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + ".";
			}

			// Token: 0x0200298E RID: 10638
			public class MECHANICALSURFBOARD
			{
				// Token: 0x0400B782 RID: 46978
				public static LocString NAME = UI.FormatAsLink("Mechanical Surfboard", "MECHANICALSURFBOARD");

				// Token: 0x0400B783 RID: 46979
				public static LocString DESC = "Mechanical waves make for radical relaxation time.";

				// Token: 0x0400B784 RID: 46980
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Increases Duplicant ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nSome ",
					UI.FormatAsLink("Water", "WATER"),
					" gets splashed on the floor during use."
				});

				// Token: 0x0400B785 RID: 46981
				public static LocString WATER_REQUIREMENT = "{element}: {amount}";

				// Token: 0x0400B786 RID: 46982
				public static LocString WATER_REQUIREMENT_TOOLTIP = "This building must be filled with {amount} {element} in order to function.";

				// Token: 0x0400B787 RID: 46983
				public static LocString LEAK_REQUIREMENT = "Spillage: {amount}";

				// Token: 0x0400B788 RID: 46984
				public static LocString LEAK_REQUIREMENT_TOOLTIP = "This building will spill {amount} of its contents on to the floor during use, which must be replenished.";
			}

			// Token: 0x0200298F RID: 10639
			public class SAUNA
			{
				// Token: 0x0400B789 RID: 46985
				public static LocString NAME = UI.FormatAsLink("Sauna", "SAUNA");

				// Token: 0x0400B78A RID: 46986
				public static LocString DESC = "A steamy sauna soothes away all the aches and pains.";

				// Token: 0x0400B78B RID: 46987
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Steam", "STEAM"),
					" to create a relaxing atmosphere.\n\nIncreases Duplicant ",
					UI.FormatAsLink("Morale", "MORALE"),
					" and provides a lingering sense of warmth."
				});
			}

			// Token: 0x02002990 RID: 10640
			public class BEACHCHAIR
			{
				// Token: 0x0400B78C RID: 46988
				public static LocString NAME = UI.FormatAsLink("Beach Chair", "BEACHCHAIR");

				// Token: 0x0400B78D RID: 46989
				public static LocString DESC = "Soak up some relaxing sun rays.";

				// Token: 0x0400B78E RID: 46990
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Duplicants can relax by lounging in ",
					UI.FormatAsLink("Sunlight", "LIGHT"),
					".\n\nIncreases Duplicant ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});

				// Token: 0x0400B78F RID: 46991
				public static LocString LIGHTEFFECT_LOW = "{attrib}: {amount} (Dim Light)";

				// Token: 0x0400B790 RID: 46992
				public static LocString LIGHTEFFECT_LOW_TOOLTIP = "Duplicants will gain {amount} {attrib} if this building is in light dimmer than {lux}.";

				// Token: 0x0400B791 RID: 46993
				public static LocString LIGHTEFFECT_HIGH = "{attrib}: {amount} (Bright Light)";

				// Token: 0x0400B792 RID: 46994
				public static LocString LIGHTEFFECT_HIGH_TOOLTIP = "Duplicants will gain {amount} {attrib} if this building is in at least {lux} light.";
			}

			// Token: 0x02002991 RID: 10641
			public class SUNLAMP
			{
				// Token: 0x0400B793 RID: 46995
				public static LocString NAME = UI.FormatAsLink("Sun Lamp", "SUNLAMP");

				// Token: 0x0400B794 RID: 46996
				public static LocString DESC = "An artificial ray of sunshine.";

				// Token: 0x0400B795 RID: 46997
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Gives off ",
					UI.FormatAsLink("Sunlight", "LIGHT"),
					" level Lux.\n\nCan be paired with ",
					UI.FormatAsLink("Beach Chairs", "BEACHCHAIR"),
					"."
				});
			}

			// Token: 0x02002992 RID: 10642
			public class VERTICALWINDTUNNEL
			{
				// Token: 0x0400B796 RID: 46998
				public static LocString NAME = UI.FormatAsLink("Vertical Wind Tunnel", "VERTICALWINDTUNNEL");

				// Token: 0x0400B797 RID: 46999
				public static LocString DESC = "Duplicants love the feeling of high-powered wind through their hair.";

				// Token: 0x0400B798 RID: 47000
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Must be connected to a ",
					UI.FormatAsLink("Power Source", "POWER"),
					". To properly function, the area under this building must be left vacant.\n\nIncreases Duplicants ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});

				// Token: 0x0400B799 RID: 47001
				public static LocString DISPLACEMENTEFFECT = "Gas Displacement: {amount}";

				// Token: 0x0400B79A RID: 47002
				public static LocString DISPLACEMENTEFFECT_TOOLTIP = "This building will displace {amount} Gas while in use.";
			}

			// Token: 0x02002993 RID: 10643
			public class TELEPORTALPAD
			{
				// Token: 0x0400B79B RID: 47003
				public static LocString NAME = "Teleporter Pad";

				// Token: 0x0400B79C RID: 47004
				public static LocString DESC = "Duplicants are just atoms as far as the pad's concerned.";

				// Token: 0x0400B79D RID: 47005
				public static LocString EFFECT = "Instantly transports Duplicants and items to another portal with the same portal code.";

				// Token: 0x0400B79E RID: 47006
				public static LocString LOGIC_PORT = "Portal Code Input";

				// Token: 0x0400B79F RID: 47007
				public static LocString LOGIC_PORT_ACTIVE = "1";

				// Token: 0x0400B7A0 RID: 47008
				public static LocString LOGIC_PORT_INACTIVE = "0";
			}

			// Token: 0x02002994 RID: 10644
			public class CHECKPOINT
			{
				// Token: 0x0400B7A1 RID: 47009
				public static LocString NAME = UI.FormatAsLink("Duplicant Checkpoint", "CHECKPOINT");

				// Token: 0x0400B7A2 RID: 47010
				public static LocString DESC = "Checkpoints can be connected to automated sensors to determine when it's safe to enter.";

				// Token: 0x0400B7A3 RID: 47011
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to pass when receiving a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					".\n\nPrevents Duplicants from passing when receiving a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					"."
				});

				// Token: 0x0400B7A4 RID: 47012
				public static LocString LOGIC_PORT = "Duplicant Stop/Go";

				// Token: 0x0400B7A5 RID: 47013
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Allow Duplicant passage";

				// Token: 0x0400B7A6 RID: 47014
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Prevent Duplicant passage";
			}

			// Token: 0x02002995 RID: 10645
			public class FIREPOLE
			{
				// Token: 0x0400B7A7 RID: 47015
				public static LocString NAME = UI.FormatAsLink("Fire Pole", "FIREPOLE");

				// Token: 0x0400B7A8 RID: 47016
				public static LocString DESC = "Build these in addition to ladders for efficient upward and downward movement.";

				// Token: 0x0400B7A9 RID: 47017
				public static LocString EFFECT = "Allows rapid Duplicant descent.\n\nSignificantly slows upward climbing.";
			}

			// Token: 0x02002996 RID: 10646
			public class FLOORSWITCH
			{
				// Token: 0x0400B7AA RID: 47018
				public static LocString NAME = UI.FormatAsLink("Weight Plate", "FLOORSWITCH");

				// Token: 0x0400B7AB RID: 47019
				public static LocString DESC = "Weight plates can be used to turn on amenities only when Duplicants pass by.";

				// Token: 0x0400B7AC RID: 47020
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" when an object or Duplicant is placed atop of it.\n\nCannot be triggered by ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" or ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					"."
				});

				// Token: 0x0400B7AD RID: 47021
				public static LocString LOGIC_PORT_DESC = UI.FormatAsLink("Active", "LOGIC") + "/" + UI.FormatAsLink("Inactive", "LOGIC");
			}

			// Token: 0x02002997 RID: 10647
			public class KILN
			{
				// Token: 0x0400B7AE RID: 47022
				public static LocString NAME = UI.FormatAsLink("Kiln", "KILN");

				// Token: 0x0400B7AF RID: 47023
				public static LocString DESC = "It gets quite hot.";

				// Token: 0x0400B7B0 RID: 47024
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Fires ",
					UI.FormatAsLink("Clay", "CLAY"),
					" to produce ",
					UI.FormatAsLink("Ceramic", "CERAMIC"),
					", and ",
					UI.FormatAsLink("Coal", "CARBON"),
					" or ",
					UI.FormatAsLink("Wood", "WOOD"),
					" to produce ",
					UI.FormatAsLink("Refined Carbon", "REFINEDCARBON"),
					".\n\nDuplicants will not fabricate items unless recipes are queued."
				});
			}

			// Token: 0x02002998 RID: 10648
			public class LIQUIDFUELTANK
			{
				// Token: 0x0400B7B1 RID: 47025
				public static LocString NAME = UI.FormatAsLink("Liquid Fuel Tank", "LIQUIDFUELTANK");

				// Token: 0x0400B7B2 RID: 47026
				public static LocString DESC = "Storing additional fuel increases the distance a rocket can travel before returning.";

				// Token: 0x0400B7B3 RID: 47027
				public static LocString EFFECT = "Stores the " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " fuel piped into it to supply rocket engines.\n\nThe stored fuel type is determined by the rocket engine it is built upon.";
			}

			// Token: 0x02002999 RID: 10649
			public class LIQUIDFUELTANKCLUSTER
			{
				// Token: 0x0400B7B4 RID: 47028
				public static LocString NAME = UI.FormatAsLink("Large Liquid Fuel Tank", "LIQUIDFUELTANKCLUSTER");

				// Token: 0x0400B7B5 RID: 47029
				public static LocString DESC = "Storing additional fuel increases the distance a rocket can travel before returning.";

				// Token: 0x0400B7B6 RID: 47030
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores the ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" fuel piped into it to supply rocket engines.\n\nThe stored fuel type is determined by the rocket engine it is built upon. \n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x0200299A RID: 10650
			public class LANDING_POD
			{
				// Token: 0x0400B7B7 RID: 47031
				public static LocString NAME = "Spacefarer Deploy Pod";

				// Token: 0x0400B7B8 RID: 47032
				public static LocString DESC = "Geronimo!";

				// Token: 0x0400B7B9 RID: 47033
				public static LocString EFFECT = "Contains a Duplicant deployed from orbit.\n\nPod will disintegrate on arrival.";
			}

			// Token: 0x0200299B RID: 10651
			public class ROCKETPOD
			{
				// Token: 0x0400B7BA RID: 47034
				public static LocString NAME = UI.FormatAsLink("Trailblazer Deploy Pod", "ROCKETPOD");

				// Token: 0x0400B7BB RID: 47035
				public static LocString DESC = "The Duplicant inside is equal parts nervous and excited.";

				// Token: 0x0400B7BC RID: 47036
				public static LocString EFFECT = "Contains a Duplicant deployed from orbit by a " + BUILDINGS.PREFABS.PIONEERMODULE.NAME + ".\n\nPod will disintegrate on arrival.";
			}

			// Token: 0x0200299C RID: 10652
			public class SCOUTROCKETPOD
			{
				// Token: 0x0400B7BD RID: 47037
				public static LocString NAME = UI.FormatAsLink("Rover's Doghouse", "SCOUTROCKETPOD");

				// Token: 0x0400B7BE RID: 47038
				public static LocString DESC = "Good luck out there, boy!";

				// Token: 0x0400B7BF RID: 47039
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Contains a ",
					UI.FormatAsLink("Rover", "SCOUT"),
					" deployed from an orbiting ",
					BUILDINGS.PREFABS.SCOUTMODULE.NAME,
					".\n\nPod will disintegrate on arrival."
				});
			}

			// Token: 0x0200299D RID: 10653
			public class ROCKETCOMMANDCONSOLE
			{
				// Token: 0x0400B7C0 RID: 47040
				public static LocString NAME = UI.FormatAsLink("Rocket Cockpit", "ROCKETCOMMANDCONSOLE");

				// Token: 0x0400B7C1 RID: 47041
				public static LocString DESC = "Looks kinda fun.";

				// Token: 0x0400B7C2 RID: 47042
				public static LocString EFFECT = "Allows a Duplicant to pilot a rocket.\n\nCargo rockets must possess a Rocket Cockpit in order to function.";
			}

			// Token: 0x0200299E RID: 10654
			public class ROCKETENVELOPETILE
			{
				// Token: 0x0400B7C3 RID: 47043
				public static LocString NAME = UI.FormatAsLink("Rocket", "ROCKETENVELOPETILE");

				// Token: 0x0400B7C4 RID: 47044
				public static LocString DESC = "Keeps the space out.";

				// Token: 0x0400B7C5 RID: 47045
				public static LocString EFFECT = "The walls of a rocket.";
			}

			// Token: 0x0200299F RID: 10655
			public class ROCKETENVELOPEWINDOWTILE
			{
				// Token: 0x0400B7C6 RID: 47046
				public static LocString NAME = UI.FormatAsLink("Rocket Window", "ROCKETENVELOPEWINDOWTILE");

				// Token: 0x0400B7C7 RID: 47047
				public static LocString DESC = "I can see my asteroid from here!";

				// Token: 0x0400B7C8 RID: 47048
				public static LocString EFFECT = "The window of a rocket.";
			}

			// Token: 0x020029A0 RID: 10656
			public class ROCKETWALLTILE
			{
				// Token: 0x0400B7C9 RID: 47049
				public static LocString NAME = UI.FormatAsLink("Rocket Wall", "ROCKETENVELOPETILE");

				// Token: 0x0400B7CA RID: 47050
				public static LocString DESC = "Keeps the space out.";

				// Token: 0x0400B7CB RID: 47051
				public static LocString EFFECT = "The walls of a rocket.";
			}

			// Token: 0x020029A1 RID: 10657
			public class SMALLOXIDIZERTANK
			{
				// Token: 0x0400B7CC RID: 47052
				public static LocString NAME = UI.FormatAsLink("Small Solid Oxidizer Tank", "SMALLOXIDIZERTANK");

				// Token: 0x0400B7CD RID: 47053
				public static LocString DESC = "Solid oxidizers allows rocket fuel to be efficiently burned in the vacuum of space.";

				// Token: 0x0400B7CE RID: 47054
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Fertilizer", "Fertilizer"),
					" and ",
					UI.FormatAsLink("Oxylite", "OXYROCK"),
					" for burning rocket fuels. \n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});

				// Token: 0x0400B7CF RID: 47055
				public static LocString UI_FILTER_CATEGORY = "Accepted Oxidizers";
			}

			// Token: 0x020029A2 RID: 10658
			public class OXIDIZERTANK
			{
				// Token: 0x0400B7D0 RID: 47056
				public static LocString NAME = UI.FormatAsLink("Solid Oxidizer Tank", "OXIDIZERTANK");

				// Token: 0x0400B7D1 RID: 47057
				public static LocString DESC = "Solid oxidizers allows rocket fuel to be efficiently burned in the vacuum of space.";

				// Token: 0x0400B7D2 RID: 47058
				public static LocString EFFECT = "Stores " + UI.FormatAsLink("Oxylite", "OXYROCK") + " and other oxidizers for burning rocket fuels.";

				// Token: 0x0400B7D3 RID: 47059
				public static LocString UI_FILTER_CATEGORY = "Accepted Oxidizers";
			}

			// Token: 0x020029A3 RID: 10659
			public class OXIDIZERTANKCLUSTER
			{
				// Token: 0x0400B7D4 RID: 47060
				public static LocString NAME = UI.FormatAsLink("Large Solid Oxidizer Tank", "OXIDIZERTANKCLUSTER");

				// Token: 0x0400B7D5 RID: 47061
				public static LocString DESC = "Solid oxidizers allows rocket fuel to be efficiently burned in the vacuum of space.";

				// Token: 0x0400B7D6 RID: 47062
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Oxylite", "OXYROCK"),
					" and other oxidizers for burning rocket fuels.\n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});

				// Token: 0x0400B7D7 RID: 47063
				public static LocString UI_FILTER_CATEGORY = "Accepted Oxidizers";
			}

			// Token: 0x020029A4 RID: 10660
			public class OXIDIZERTANKLIQUID
			{
				// Token: 0x0400B7D8 RID: 47064
				public static LocString NAME = UI.FormatAsLink("Liquid Oxidizer Tank", "OXIDIZERTANKLIQUID");

				// Token: 0x0400B7D9 RID: 47065
				public static LocString DESC = "Liquid oxygen improves the thrust-to-mass ratio of rocket fuels.";

				// Token: 0x0400B7DA RID: 47066
				public static LocString EFFECT = "Stores " + UI.FormatAsLink("Liquid Oxygen", "LIQUIDOXYGEN") + " for burning rocket fuels.";
			}

			// Token: 0x020029A5 RID: 10661
			public class OXIDIZERTANKLIQUIDCLUSTER
			{
				// Token: 0x0400B7DB RID: 47067
				public static LocString NAME = UI.FormatAsLink("Liquid Oxidizer Tank", "OXIDIZERTANKLIQUIDCLUSTER");

				// Token: 0x0400B7DC RID: 47068
				public static LocString DESC = "Liquid oxygen improves the thrust-to-mass ratio of rocket fuels.";

				// Token: 0x0400B7DD RID: 47069
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Liquid Oxygen", "LIQUIDOXYGEN"),
					" for burning rocket fuels. \n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x020029A6 RID: 10662
			public class LIQUIDCONDITIONER
			{
				// Token: 0x0400B7DE RID: 47070
				public static LocString NAME = UI.FormatAsLink("Thermo Aquatuner", "LIQUIDCONDITIONER");

				// Token: 0x0400B7DF RID: 47071
				public static LocString DESC = "A thermo aquatuner cools liquid and outputs the heat elsewhere.";

				// Token: 0x0400B7E0 RID: 47072
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Cools the ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" piped through it, but outputs ",
					UI.FormatAsLink("Heat", "HEAT"),
					" in its immediate vicinity."
				});
			}

			// Token: 0x020029A7 RID: 10663
			public class LIQUIDCARGOBAY
			{
				// Token: 0x0400B7E1 RID: 47073
				public static LocString NAME = UI.FormatAsLink("Liquid Cargo Tank", "LIQUIDCARGOBAY");

				// Token: 0x0400B7E2 RID: 47074
				public static LocString DESC = "Duplicants will fill cargo bays with any resources they find during space missions.";

				// Token: 0x0400B7E3 RID: 47075
				public static LocString EFFECT = "Allows Duplicants to store any " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " resources found during space missions.\n\nStored resources become available to the colony upon the rocket's return.";
			}

			// Token: 0x020029A8 RID: 10664
			public class LIQUIDCARGOBAYCLUSTER
			{
				// Token: 0x0400B7E4 RID: 47076
				public static LocString NAME = UI.FormatAsLink("Large Liquid Cargo Tank", "LIQUIDCARGOBAY");

				// Token: 0x0400B7E5 RID: 47077
				public static LocString DESC = "Holds more than a regular cargo tank.";

				// Token: 0x0400B7E6 RID: 47078
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to store most of the ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" resources found during space missions.\n\nStored resources become available to the colony upon the rocket's return.\n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x020029A9 RID: 10665
			public class LIQUIDCARGOBAYSMALL
			{
				// Token: 0x0400B7E7 RID: 47079
				public static LocString NAME = UI.FormatAsLink("Liquid Cargo Tank", "LIQUIDCARGOBAYSMALL");

				// Token: 0x0400B7E8 RID: 47080
				public static LocString DESC = "Duplicants will fill cargo tanks with whatever resources they find during space missions.";

				// Token: 0x0400B7E9 RID: 47081
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to store some of the ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" resources found during space missions.\n\nStored resources become available to the colony upon the rocket's return. \n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x020029AA RID: 10666
			public class LUXURYBED
			{
				// Token: 0x0400B7EA RID: 47082
				public static LocString NAME = UI.FormatAsLink("Comfy Bed", "LUXURYBED");

				// Token: 0x0400B7EB RID: 47083
				public static LocString DESC = "Duplicants prefer comfy beds to cots and wake up more rested after sleeping in them.";

				// Token: 0x0400B7EC RID: 47084
				public static LocString EFFECT = "Provides a sleeping area for one Duplicant and restores additional stamina.\n\nDuplicants will automatically sleep in their assigned beds at night.";

				// Token: 0x02003898 RID: 14488
				public class FACADES
				{
					// Token: 0x02003BE7 RID: 15335
					public class DEFAULT_LUXURYBED
					{
						// Token: 0x0400ED06 RID: 60678
						public static LocString NAME = UI.FormatAsLink("Comfy Bed", "LUXURYBED");

						// Token: 0x0400ED07 RID: 60679
						public static LocString DESC = "Much comfier than a cot.";
					}

					// Token: 0x02003BE8 RID: 15336
					public class GRANDPRIX
					{
						// Token: 0x0400ED08 RID: 60680
						public static LocString NAME = UI.FormatAsLink("Grand Prix Bed", "LUXURYBED");

						// Token: 0x0400ED09 RID: 60681
						public static LocString DESC = "Where every Duplicant wakes up a winner.";
					}

					// Token: 0x02003BE9 RID: 15337
					public class BOAT
					{
						// Token: 0x0400ED0A RID: 60682
						public static LocString NAME = UI.FormatAsLink("Dreamboat Bed", "LUXURYBED");

						// Token: 0x0400ED0B RID: 60683
						public static LocString DESC = "Ahoy! Set sail for zzzzz's.";
					}

					// Token: 0x02003BEA RID: 15338
					public class ROCKET_BED
					{
						// Token: 0x0400ED0C RID: 60684
						public static LocString NAME = UI.FormatAsLink("S.S. Napmaster Bed", "LUXURYBED");

						// Token: 0x0400ED0D RID: 60685
						public static LocString DESC = "Launches sleepy Duplicants into a deep-space slumber.";
					}

					// Token: 0x02003BEB RID: 15339
					public class BOUNCY_BED
					{
						// Token: 0x0400ED0E RID: 60686
						public static LocString NAME = UI.FormatAsLink("Bouncy Castle Bed", "LUXURYBED");

						// Token: 0x0400ED0F RID: 60687
						public static LocString DESC = "An inflatable party prop makes a surprisingly good bed.";
					}

					// Token: 0x02003BEC RID: 15340
					public class PUFT_BED
					{
						// Token: 0x0400ED10 RID: 60688
						public static LocString NAME = UI.FormatAsLink("Puft Bed", "LUXURYBED");

						// Token: 0x0400ED11 RID: 60689
						public static LocString DESC = "A comfy, if somewhat 'fragrant', place to sleep.";
					}

					// Token: 0x02003BED RID: 15341
					public class HAND
					{
						// Token: 0x0400ED12 RID: 60690
						public static LocString NAME = UI.FormatAsLink("Cradled Bed", "LUXURYBED");

						// Token: 0x0400ED13 RID: 60691
						public static LocString DESC = "It's so nice to be held.";
					}

					// Token: 0x02003BEE RID: 15342
					public class RUBIKS
					{
						// Token: 0x0400ED14 RID: 60692
						public static LocString NAME = UI.FormatAsLink("Puzzle Cube Bed", "LUXURYBED");

						// Token: 0x0400ED15 RID: 60693
						public static LocString DESC = "A little pattern recognition at bedtime soothes the mind.";
					}

					// Token: 0x02003BEF RID: 15343
					public class RED_ROSE
					{
						// Token: 0x0400ED16 RID: 60694
						public static LocString NAME = UI.FormatAsLink("Comfy Puce Bed", "LUXURYBED");

						// Token: 0x0400ED17 RID: 60695
						public static LocString DESC = "A pink-hued bed for rosy dreams.";
					}

					// Token: 0x02003BF0 RID: 15344
					public class GREEN_MUSH
					{
						// Token: 0x0400ED18 RID: 60696
						public static LocString NAME = UI.FormatAsLink("Comfy Mush Bed", "LUXURYBED");

						// Token: 0x0400ED19 RID: 60697
						public static LocString DESC = "The mattress is so soft, it's almost impossible to climb out of.";
					}

					// Token: 0x02003BF1 RID: 15345
					public class YELLOW_TARTAR
					{
						// Token: 0x0400ED1A RID: 60698
						public static LocString NAME = UI.FormatAsLink("Comfy Ick Bed", "LUXURYBED");

						// Token: 0x0400ED1B RID: 60699
						public static LocString DESC = "When life is icky, bed rest is the only answer.";
					}

					// Token: 0x02003BF2 RID: 15346
					public class PURPLE_BRAINFAT
					{
						// Token: 0x0400ED1C RID: 60700
						public static LocString NAME = UI.FormatAsLink("Comfy Fainting Bed", "LUXURYBED");

						// Token: 0x0400ED1D RID: 60701
						public static LocString DESC = "A soft landing spot for swooners.";
					}
				}
			}

			// Token: 0x020029AB RID: 10667
			public class LADDERBED
			{
				// Token: 0x0400B7ED RID: 47085
				public static LocString NAME = UI.FormatAsLink("Ladder Bed", "LADDERBED");

				// Token: 0x0400B7EE RID: 47086
				public static LocString DESC = "Duplicant's sleep will be interrupted if another Duplicant uses the ladder.";

				// Token: 0x0400B7EF RID: 47087
				public static LocString EFFECT = "Provides a sleeping area for one Duplicant and also functions as a ladder.\n\nDuplicants will automatically sleep in their assigned beds at night.";
			}

			// Token: 0x020029AC RID: 10668
			public class MEDICALCOT
			{
				// Token: 0x0400B7F0 RID: 47088
				public static LocString NAME = UI.FormatAsLink("Triage Cot", "MEDICALCOT");

				// Token: 0x0400B7F1 RID: 47089
				public static LocString DESC = "Duplicants use triage cots to recover from physical injuries and receive aid from peers.";

				// Token: 0x0400B7F2 RID: 47090
				public static LocString EFFECT = "Accelerates " + UI.FormatAsLink("Health", "HEALTH") + " restoration and the healing of physical injuries.\n\nRevives incapacitated Duplicants.";
			}

			// Token: 0x020029AD RID: 10669
			public class DOCTORSTATION
			{
				// Token: 0x0400B7F3 RID: 47091
				public static LocString NAME = UI.FormatAsLink("Sick Bay", "DOCTORSTATION");

				// Token: 0x0400B7F4 RID: 47092
				public static LocString DESC = "Sick bays can be placed in hospital rooms to decrease the likelihood of disease spreading.";

				// Token: 0x0400B7F5 RID: 47093
				public static LocString EFFECT = "Allows Duplicants to administer basic treatments to sick Duplicants.\n\nDuplicants must possess the Bedside Manner " + UI.FormatAsLink("Skill", "ROLES") + " to treat peers.";
			}

			// Token: 0x020029AE RID: 10670
			public class ADVANCEDDOCTORSTATION
			{
				// Token: 0x0400B7F6 RID: 47094
				public static LocString NAME = UI.FormatAsLink("Disease Clinic", "ADVANCEDDOCTORSTATION");

				// Token: 0x0400B7F7 RID: 47095
				public static LocString DESC = "Disease clinics require power, but treat more serious illnesses than sick bays alone.";

				// Token: 0x0400B7F8 RID: 47096
				public static LocString EFFECT = "Allows Duplicants to administer powerful treatments to sick Duplicants.\n\nDuplicants must possess the Advanced Medical Care " + UI.FormatAsLink("Skill", "ROLES") + " to treat peers.";
			}

			// Token: 0x020029AF RID: 10671
			public class MASSAGETABLE
			{
				// Token: 0x0400B7F9 RID: 47097
				public static LocString NAME = UI.FormatAsLink("Massage Table", "MASSAGETABLE");

				// Token: 0x0400B7FA RID: 47098
				public static LocString DESC = "Massage tables quickly reduce extreme stress, at the cost of power production.";

				// Token: 0x0400B7FB RID: 47099
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Rapidly reduces ",
					UI.FormatAsLink("Stress", "STRESS"),
					" for the Duplicant user.\n\nDuplicants will automatically seek a massage table when ",
					UI.FormatAsLink("Stress", "STRESS"),
					" exceeds breaktime range."
				});

				// Token: 0x0400B7FC RID: 47100
				public static LocString ACTIVATE_TOOLTIP = "Duplicants must take a massage break when their " + UI.FormatAsKeyWord("Stress") + " reaches {0}%";

				// Token: 0x0400B7FD RID: 47101
				public static LocString DEACTIVATE_TOOLTIP = "Breaktime ends when " + UI.FormatAsKeyWord("Stress") + " is reduced to {0}%";

				// Token: 0x02003899 RID: 14489
				public class FACADES
				{
					// Token: 0x02003BF3 RID: 15347
					public class DEFAULT_MASSAGETABLE
					{
						// Token: 0x0400ED1E RID: 60702
						public static LocString NAME = UI.FormatAsLink("Massage Table", "MASSAGETABLE");

						// Token: 0x0400ED1F RID: 60703
						public static LocString DESC = "Massage tables quickly reduce extreme stress, at the cost of power production.";
					}

					// Token: 0x02003BF4 RID: 15348
					public class SHIATSU
					{
						// Token: 0x0400ED20 RID: 60704
						public static LocString NAME = UI.FormatAsLink("Shiatsu Table", "MASSAGETABLE");

						// Token: 0x0400ED21 RID: 60705
						public static LocString DESC = "Deep pressure for deep-seated stress.";
					}

					// Token: 0x02003BF5 RID: 15349
					public class MASSEUR_BALLOON
					{
						// Token: 0x0400ED22 RID: 60706
						public static LocString NAME = UI.FormatAsLink("Inflatable Massage Table", "MASSAGETABLE");

						// Token: 0x0400ED23 RID: 60707
						public static LocString DESC = "Inflates well-being, deflates stress.";
					}
				}
			}

			// Token: 0x020029B0 RID: 10672
			public class CEILINGLIGHT
			{
				// Token: 0x0400B7FE RID: 47102
				public static LocString NAME = UI.FormatAsLink("Ceiling Light", "CEILINGLIGHT");

				// Token: 0x0400B7FF RID: 47103
				public static LocString DESC = "Light reduces Duplicant stress and is required to grow certain plants.";

				// Token: 0x0400B800 RID: 47104
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Provides ",
					UI.FormatAsLink("Light", "LIGHT"),
					" when ",
					UI.FormatAsLink("Powered", "POWER"),
					".\n\nIncreases Duplicant workspeed within light radius."
				});

				// Token: 0x0200389A RID: 14490
				public class FACADES
				{
					// Token: 0x02003BF6 RID: 15350
					public class DEFAULT_CEILINGLIGHT
					{
						// Token: 0x0400ED24 RID: 60708
						public static LocString NAME = UI.FormatAsLink("Ceiling Light", "CEILINGLIGHT");

						// Token: 0x0400ED25 RID: 60709
						public static LocString DESC = "It does not go on the floor.";
					}

					// Token: 0x02003BF7 RID: 15351
					public class LABFLASK
					{
						// Token: 0x0400ED26 RID: 60710
						public static LocString NAME = UI.FormatAsLink("Lab Flask Ceiling Light", "CEILINGLIGHT");

						// Token: 0x0400ED27 RID: 60711
						public static LocString DESC = "For best results, do not fill with liquids.";
					}

					// Token: 0x02003BF8 RID: 15352
					public class FAUXPIPE
					{
						// Token: 0x0400ED28 RID: 60712
						public static LocString NAME = UI.FormatAsLink("Faux Pipe Ceiling Light", "CEILINGLIGHT");

						// Token: 0x0400ED29 RID: 60713
						public static LocString DESC = "The height of plumbing-inspired interior design.";
					}

					// Token: 0x02003BF9 RID: 15353
					public class MINING
					{
						// Token: 0x0400ED2A RID: 60714
						public static LocString NAME = UI.FormatAsLink("Mining Ceiling Light", "CEILINGLIGHT");

						// Token: 0x0400ED2B RID: 60715
						public static LocString DESC = "The protective cage makes it the safest choice for underground parties.";
					}

					// Token: 0x02003BFA RID: 15354
					public class BLOSSOM
					{
						// Token: 0x0400ED2C RID: 60716
						public static LocString NAME = UI.FormatAsLink("Blossom Ceiling Light", "CEILINGLIGHT");

						// Token: 0x0400ED2D RID: 60717
						public static LocString DESC = "For Duplicants who can't keep real plants alive.";
					}

					// Token: 0x02003BFB RID: 15355
					public class POLKADOT
					{
						// Token: 0x0400ED2E RID: 60718
						public static LocString NAME = UI.FormatAsLink("Polka Dot Ceiling Light", "CEILINGLIGHT");

						// Token: 0x0400ED2F RID: 60719
						public static LocString DESC = "A fun lampshade for fun spaces.";
					}

					// Token: 0x02003BFC RID: 15356
					public class RUBIKS
					{
						// Token: 0x0400ED30 RID: 60720
						public static LocString NAME = UI.FormatAsLink("Puzzle Cube Ceiling Light", "CEILINGLIGHT");

						// Token: 0x0400ED31 RID: 60721
						public static LocString DESC = "The initials E.R. are sewn into the lampshade.";
					}
				}
			}

			// Token: 0x020029B1 RID: 10673
			public class MERCURYCEILINGLIGHT
			{
				// Token: 0x0400B801 RID: 47105
				public static LocString NAME = UI.FormatAsLink("Mercury Ceiling Light", "MERCURYCEILINGLIGHT");

				// Token: 0x0400B802 RID: 47106
				public static LocString DESC = "Mercury ceiling lights take a while to reach full brightness, but once they do...zowie!";

				// Token: 0x0400B803 RID: 47107
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Mercury", "MERCURY"),
					" and ",
					UI.FormatAsLink("Power", "POWER"),
					" to produce ",
					UI.FormatAsLink("Light", "LIGHT"),
					".\n\nLight reduces Duplicant stress and is required to grow certain plants."
				});
			}

			// Token: 0x020029B2 RID: 10674
			public class AIRFILTER
			{
				// Token: 0x0400B804 RID: 47108
				public static LocString NAME = UI.FormatAsLink("Deodorizer", "AIRFILTER");

				// Token: 0x0400B805 RID: 47109
				public static LocString DESC = "Oh! Citrus scented!";

				// Token: 0x0400B806 RID: 47110
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Sand", "SAND"),
					" to filter ",
					UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN"),
					" from the air, reducing ",
					UI.FormatAsLink("Disease", "DISEASE"),
					" spread."
				});
			}

			// Token: 0x020029B3 RID: 10675
			public class ARTIFACTANALYSISSTATION
			{
				// Token: 0x0400B807 RID: 47111
				public static LocString NAME = UI.FormatAsLink("Artifact Analysis Station", "ARTIFACTANALYSISSTATION");

				// Token: 0x0400B808 RID: 47112
				public static LocString DESC = "Discover the mysteries of the past.";

				// Token: 0x0400B809 RID: 47113
				public static LocString EFFECT = "Analyses and extracts " + UI.FormatAsLink("Neutronium", "UNOBTANIUM") + " from artifacts of interest.";

				// Token: 0x0400B80A RID: 47114
				public static LocString PAYLOAD_DROP_RATE = ITEMS.INDUSTRIAL_PRODUCTS.GENE_SHUFFLER_RECHARGE.NAME + " drop chance: {chance}";

				// Token: 0x0400B80B RID: 47115
				public static LocString PAYLOAD_DROP_RATE_TOOLTIP = "This artifact has a {chance} to drop a " + ITEMS.INDUSTRIAL_PRODUCTS.GENE_SHUFFLER_RECHARGE.NAME + " when analyzed at the " + BUILDINGS.PREFABS.ARTIFACTANALYSISSTATION.NAME;
			}

			// Token: 0x020029B4 RID: 10676
			public class CANVAS
			{
				// Token: 0x0400B80C RID: 47116
				public static LocString NAME = UI.FormatAsLink("Blank Canvas", "CANVAS");

				// Token: 0x0400B80D RID: 47117
				public static LocString DESC = "Once built, a Duplicant can paint a blank canvas to produce a decorative painting.";

				// Token: 0x0400B80E RID: 47118
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be painted by a Duplicant."
				});

				// Token: 0x0400B80F RID: 47119
				public static LocString POORQUALITYNAME = "Crude Painting";

				// Token: 0x0400B810 RID: 47120
				public static LocString AVERAGEQUALITYNAME = "Mediocre Painting";

				// Token: 0x0400B811 RID: 47121
				public static LocString EXCELLENTQUALITYNAME = "Masterpiece";

				// Token: 0x0200389B RID: 14491
				public class FACADES
				{
					// Token: 0x02003BFD RID: 15357
					public class ART_A
					{
						// Token: 0x0400ED32 RID: 60722
						public static LocString NAME = UI.FormatAsLink("Doodle Dee Duplicant", "ART_A");

						// Token: 0x0400ED33 RID: 60723
						public static LocString DESC = "A sweet, amateurish interpretation of the Duplicant form.";
					}

					// Token: 0x02003BFE RID: 15358
					public class ART_B
					{
						// Token: 0x0400ED34 RID: 60724
						public static LocString NAME = UI.FormatAsLink("Midnight Meal", "ART_B");

						// Token: 0x0400ED35 RID: 60725
						public static LocString DESC = "The fast-food equivalent of high art.";
					}

					// Token: 0x02003BFF RID: 15359
					public class ART_C
					{
						// Token: 0x0400ED36 RID: 60726
						public static LocString NAME = UI.FormatAsLink("Dupa Leesa", "ART_C");

						// Token: 0x0400ED37 RID: 60727
						public static LocString DESC = "Some viewers swear they've seen it blink.";
					}

					// Token: 0x02003C00 RID: 15360
					public class ART_D
					{
						// Token: 0x0400ED38 RID: 60728
						public static LocString NAME = UI.FormatAsLink("The Screech", "ART_D");

						// Token: 0x0400ED39 RID: 60729
						public static LocString DESC = "If art could speak, this piece would be far less popular.";
					}

					// Token: 0x02003C01 RID: 15361
					public class ART_E
					{
						// Token: 0x0400ED3A RID: 60730
						public static LocString NAME = UI.FormatAsLink("Fridup Kallo", "ART_E");

						// Token: 0x0400ED3B RID: 60731
						public static LocString DESC = "Scratching and sniffing the flower yields no scent.";
					}

					// Token: 0x02003C02 RID: 15362
					public class ART_F
					{
						// Token: 0x0400ED3C RID: 60732
						public static LocString NAME = UI.FormatAsLink("Moopoleon Bonafarte", "ART_F");

						// Token: 0x0400ED3D RID: 60733
						public static LocString DESC = "Portrait of a leader astride their mighty steed.";
					}

					// Token: 0x02003C03 RID: 15363
					public class ART_G
					{
						// Token: 0x0400ED3E RID: 60734
						public static LocString NAME = UI.FormatAsLink("Expressive Genius", "ART_G");

						// Token: 0x0400ED3F RID: 60735
						public static LocString DESC = "The raw emotion conveyed here often renders viewers speechless.";
					}

					// Token: 0x02003C04 RID: 15364
					public class ART_H
					{
						// Token: 0x0400ED40 RID: 60736
						public static LocString NAME = UI.FormatAsLink("The Smooch", "ART_H");

						// Token: 0x0400ED41 RID: 60737
						public static LocString DESC = "A candid moment of affection between two organisms.";
					}

					// Token: 0x02003C05 RID: 15365
					public class ART_I
					{
						// Token: 0x0400ED42 RID: 60738
						public static LocString NAME = UI.FormatAsLink("Self-Self-Self Portrait", "ART_I");

						// Token: 0x0400ED43 RID: 60739
						public static LocString DESC = "A multi-layered exploration of the artist as a subject.";
					}

					// Token: 0x02003C06 RID: 15366
					public class ART_J
					{
						// Token: 0x0400ED44 RID: 60740
						public static LocString NAME = UI.FormatAsLink("Nikola Devouring His Mush Bar", "ART_J");

						// Token: 0x0400ED45 RID: 60741
						public static LocString DESC = "A painting that captures the true nature of hunger.";
					}

					// Token: 0x02003C07 RID: 15367
					public class ART_K
					{
						// Token: 0x0400ED46 RID: 60742
						public static LocString NAME = UI.FormatAsLink("Sketchy Fungi", "ART_K");

						// Token: 0x0400ED47 RID: 60743
						public static LocString DESC = "The perfect painting for dark, dank spaces.";
					}

					// Token: 0x02003C08 RID: 15368
					public class ART_L
					{
						// Token: 0x0400ED48 RID: 60744
						public static LocString NAME = UI.FormatAsLink("Post-Ear Era", "ART_L");

						// Token: 0x0400ED49 RID: 60745
						public static LocString DESC = "The furry hat helped keep the artist's bandage on.";
					}

					// Token: 0x02003C09 RID: 15369
					public class ART_M
					{
						// Token: 0x0400ED4A RID: 60746
						public static LocString NAME = UI.FormatAsLink("Maternal Gaze", "ART_M");

						// Token: 0x0400ED4B RID: 60747
						public static LocString DESC = "She's not angry, just disappointed.";
					}

					// Token: 0x02003C0A RID: 15370
					public class ART_O
					{
						// Token: 0x0400ED4C RID: 60748
						public static LocString NAME = UI.FormatAsLink("Hands-On", "ART_O");

						// Token: 0x0400ED4D RID: 60749
						public static LocString DESC = "It's all about cooperation, really.";
					}

					// Token: 0x02003C0B RID: 15371
					public class ART_N
					{
						// Token: 0x0400ED4E RID: 60750
						public static LocString NAME = UI.FormatAsLink("Always Hope", "ART_N");

						// Token: 0x0400ED4F RID: 60751
						public static LocString DESC = "Most Duplicants believe that the balloon in this image is about to be caught.";
					}

					// Token: 0x02003C0C RID: 15372
					public class ART_P
					{
						// Token: 0x0400ED50 RID: 60752
						public static LocString NAME = UI.FormatAsLink("Pour Soul", "ART_P");

						// Token: 0x0400ED51 RID: 60753
						public static LocString DESC = "It is a cruel guest who does not RSVP.";
					}

					// Token: 0x02003C0D RID: 15373
					public class ART_Q
					{
						// Token: 0x0400ED52 RID: 60754
						public static LocString NAME = UI.FormatAsLink("Ore Else", "ART_Q");

						// Token: 0x0400ED53 RID: 60755
						public static LocString DESC = "The only kind of gift that poorly behaved Duplicants can expect to receive.";
					}

					// Token: 0x02003C0E RID: 15374
					public class ART_R
					{
						// Token: 0x0400ED54 RID: 60756
						public static LocString NAME = UI.FormatAsLink("Lazer Pipz", "ART_R");

						// Token: 0x0400ED55 RID: 60757
						public static LocString DESC = "It combines two things that everyone loves: pips and lasers.";
					}
				}
			}

			// Token: 0x020029B5 RID: 10677
			public class CANVASWIDE
			{
				// Token: 0x0400B812 RID: 47122
				public static LocString NAME = UI.FormatAsLink("Landscape Canvas", "CANVASWIDE");

				// Token: 0x0400B813 RID: 47123
				public static LocString DESC = "Once built, a Duplicant can paint a blank canvas to produce a decorative painting.";

				// Token: 0x0400B814 RID: 47124
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Moderately increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be painted by a Duplicant."
				});

				// Token: 0x0400B815 RID: 47125
				public static LocString POORQUALITYNAME = "Crude Painting";

				// Token: 0x0400B816 RID: 47126
				public static LocString AVERAGEQUALITYNAME = "Mediocre Painting";

				// Token: 0x0400B817 RID: 47127
				public static LocString EXCELLENTQUALITYNAME = "Masterpiece";

				// Token: 0x0200389C RID: 14492
				public class FACADES
				{
					// Token: 0x02003C0F RID: 15375
					public class ART_WIDE_A
					{
						// Token: 0x0400ED56 RID: 60758
						public static LocString NAME = UI.FormatAsLink("The Twins", "ART_WIDE_A");

						// Token: 0x0400ED57 RID: 60759
						public static LocString DESC = "The effort is admirable, though the execution is not.";
					}

					// Token: 0x02003C10 RID: 15376
					public class ART_WIDE_B
					{
						// Token: 0x0400ED58 RID: 60760
						public static LocString NAME = UI.FormatAsLink("Ground Zero", "ART_WIDE_B");

						// Token: 0x0400ED59 RID: 60761
						public static LocString DESC = "Every story has its origin.";
					}

					// Token: 0x02003C11 RID: 15377
					public class ART_WIDE_C
					{
						// Token: 0x0400ED5A RID: 60762
						public static LocString NAME = UI.FormatAsLink("Still Life with Barbeque and Frost Bun", "ART_WIDE_C");

						// Token: 0x0400ED5B RID: 60763
						public static LocString DESC = "Food this good deserves to be immortalized.";
					}

					// Token: 0x02003C12 RID: 15378
					public class ART_WIDE_D
					{
						// Token: 0x0400ED5C RID: 60764
						public static LocString NAME = UI.FormatAsLink("Composition with Three Colors", "ART_WIDE_D");

						// Token: 0x0400ED5D RID: 60765
						public static LocString DESC = "All the other colors in the artist's palette had dried up.";
					}

					// Token: 0x02003C13 RID: 15379
					public class ART_WIDE_E
					{
						// Token: 0x0400ED5E RID: 60766
						public static LocString NAME = UI.FormatAsLink("Behold, A Fork", "ART_WIDE_E");

						// Token: 0x0400ED5F RID: 60767
						public static LocString DESC = "Each tine represents a branch of science.";
					}

					// Token: 0x02003C14 RID: 15380
					public class ART_WIDE_F
					{
						// Token: 0x0400ED60 RID: 60768
						public static LocString NAME = UI.FormatAsLink("The Astronomer at Home", "ART_WIDE_F");

						// Token: 0x0400ED61 RID: 60769
						public static LocString DESC = "Its companion piece, \"The Astronomer at Work\" was lost in a meteor shower.";
					}

					// Token: 0x02003C15 RID: 15381
					public class ART_WIDE_G
					{
						// Token: 0x0400ED62 RID: 60770
						public static LocString NAME = UI.FormatAsLink("Iconic Iteration", "ART_WIDE_G");

						// Token: 0x0400ED63 RID: 60771
						public static LocString DESC = "For the art collector who doesn't mind a bit of repetition.";
					}

					// Token: 0x02003C16 RID: 15382
					public class ART_WIDE_H
					{
						// Token: 0x0400ED64 RID: 60772
						public static LocString NAME = UI.FormatAsLink("La Belle Meep", "ART_WIDE_H");

						// Token: 0x0400ED65 RID: 60773
						public static LocString DESC = "A daring piece, guaranteed to cause a stir.";
					}

					// Token: 0x02003C17 RID: 15383
					public class ART_WIDE_I
					{
						// Token: 0x0400ED66 RID: 60774
						public static LocString NAME = UI.FormatAsLink("Glorious Vole", "ART_WIDE_I");

						// Token: 0x0400ED67 RID: 60775
						public static LocString DESC = "A moody study of the renowned tunneler.";
					}

					// Token: 0x02003C18 RID: 15384
					public class ART_WIDE_J
					{
						// Token: 0x0400ED68 RID: 60776
						public static LocString NAME = UI.FormatAsLink("The Swell Swell", "ART_WIDE_J");

						// Token: 0x0400ED69 RID: 60777
						public static LocString DESC = "As far as wave-themed art goes, it's great.";
					}

					// Token: 0x02003C19 RID: 15385
					public class ART_WIDE_K
					{
						// Token: 0x0400ED6A RID: 60778
						public static LocString NAME = UI.FormatAsLink("Flight of the Slicksters", "ART_WIDE_K");

						// Token: 0x0400ED6B RID: 60779
						public static LocString DESC = "The delight on the subjects' faces is contagious.";
					}

					// Token: 0x02003C1A RID: 15386
					public class ART_WIDE_L
					{
						// Token: 0x0400ED6C RID: 60780
						public static LocString NAME = UI.FormatAsLink("The Shiny Night", "ART_WIDE_L");

						// Token: 0x0400ED6D RID: 60781
						public static LocString DESC = "A dreamy abundance of swirls, whirls and whorls.";
					}

					// Token: 0x02003C1B RID: 15387
					public class ART_WIDE_M
					{
						// Token: 0x0400ED6E RID: 60782
						public static LocString NAME = UI.FormatAsLink("Hot Afternoon", "ART_WIDE_M");

						// Token: 0x0400ED6F RID: 60783
						public static LocString DESC = "Things get a bit melty if they're forgotten in the sun.";
					}

					// Token: 0x02003C1C RID: 15388
					public class ART_WIDE_O
					{
						// Token: 0x0400ED70 RID: 60784
						public static LocString NAME = UI.FormatAsLink("Super Old Mural", "ART_WIDE_O");

						// Token: 0x0400ED71 RID: 60785
						public static LocString DESC = "Even just exhaling nearby could damage this historical work.";
					}
				}
			}

			// Token: 0x020029B6 RID: 10678
			public class CANVASTALL
			{
				// Token: 0x0400B818 RID: 47128
				public static LocString NAME = UI.FormatAsLink("Portrait Canvas", "CANVASTALL");

				// Token: 0x0400B819 RID: 47129
				public static LocString DESC = "Once built, a Duplicant can paint a blank canvas to produce a decorative painting.";

				// Token: 0x0400B81A RID: 47130
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Moderately increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be painted by a Duplicant."
				});

				// Token: 0x0400B81B RID: 47131
				public static LocString POORQUALITYNAME = "Crude Painting";

				// Token: 0x0400B81C RID: 47132
				public static LocString AVERAGEQUALITYNAME = "Mediocre Painting";

				// Token: 0x0400B81D RID: 47133
				public static LocString EXCELLENTQUALITYNAME = "Masterpiece";

				// Token: 0x0200389D RID: 14493
				public class FACADES
				{
					// Token: 0x02003C1D RID: 15389
					public class ART_TALL_A
					{
						// Token: 0x0400ED72 RID: 60786
						public static LocString NAME = UI.FormatAsLink("Ode to O2", "ART_TALL_A");

						// Token: 0x0400ED73 RID: 60787
						public static LocString DESC = "Even amateur art is essential to life.";
					}

					// Token: 0x02003C1E RID: 15390
					public class ART_TALL_B
					{
						// Token: 0x0400ED74 RID: 60788
						public static LocString NAME = UI.FormatAsLink("A Cool Wheeze", "ART_TALL_B");

						// Token: 0x0400ED75 RID: 60789
						public static LocString DESC = "It certainly is colorful.";
					}

					// Token: 0x02003C1F RID: 15391
					public class ART_TALL_C
					{
						// Token: 0x0400ED76 RID: 60790
						public static LocString NAME = UI.FormatAsLink("Luxe Splatter", "ART_TALL_C");

						// Token: 0x0400ED77 RID: 60791
						public static LocString DESC = "Chaotic, yet compelling.";
					}

					// Token: 0x02003C20 RID: 15392
					public class ART_TALL_D
					{
						// Token: 0x0400ED78 RID: 60792
						public static LocString NAME = UI.FormatAsLink("Pickled Meal Lice II", "ART_TALL_D");

						// Token: 0x0400ED79 RID: 60793
						public static LocString DESC = "It doesn't have to taste good, it's art.";
					}

					// Token: 0x02003C21 RID: 15393
					public class ART_TALL_E
					{
						// Token: 0x0400ED7A RID: 60794
						public static LocString NAME = UI.FormatAsLink("Fruit Face", "ART_TALL_E");

						// Token: 0x0400ED7B RID: 60795
						public static LocString DESC = "Rumor has it that the model was self-conscious about their uneven eyebrows.";
					}

					// Token: 0x02003C22 RID: 15394
					public class ART_TALL_F
					{
						// Token: 0x0400ED7C RID: 60796
						public static LocString NAME = UI.FormatAsLink("Girl with the Blue Scarf", "ART_TALL_F");

						// Token: 0x0400ED7D RID: 60797
						public static LocString DESC = "The earring is nice too.";
					}

					// Token: 0x02003C23 RID: 15395
					public class ART_TALL_G
					{
						// Token: 0x0400ED7E RID: 60798
						public static LocString NAME = UI.FormatAsLink("A Farewell at Sunrise", "ART_TALL_G");

						// Token: 0x0400ED7F RID: 60799
						public static LocString DESC = "A poetic ink painting depicting the beginning of an end.";
					}

					// Token: 0x02003C24 RID: 15396
					public class ART_TALL_H
					{
						// Token: 0x0400ED80 RID: 60800
						public static LocString NAME = UI.FormatAsLink("Conqueror of Clusters", "ART_TALL_H");

						// Token: 0x0400ED81 RID: 60801
						public static LocString DESC = "The type of painting that ambitious Duplicants gravitate to.";
					}

					// Token: 0x02003C25 RID: 15397
					public class ART_TALL_I
					{
						// Token: 0x0400ED82 RID: 60802
						public static LocString NAME = UI.FormatAsLink("Pei Phone", "ART_TALL_I");

						// Token: 0x0400ED83 RID: 60803
						public static LocString DESC = "When the future calls, Duplicants answer.";
					}

					// Token: 0x02003C26 RID: 15398
					public class ART_TALL_J
					{
						// Token: 0x0400ED84 RID: 60804
						public static LocString NAME = UI.FormatAsLink("Duplicants of the Galaxy", "ART_TALL_J");

						// Token: 0x0400ED85 RID: 60805
						public static LocString DESC = "A poster for a blockbuster film that was never made.";
					}

					// Token: 0x02003C27 RID: 15399
					public class ART_TALL_K
					{
						// Token: 0x0400ED86 RID: 60806
						public static LocString NAME = UI.FormatAsLink("Cubist Loo", "ART_TALL_K");

						// Token: 0x0400ED87 RID: 60807
						public static LocString DESC = "The glass and frame are hydrophobic, for easy cleaning.";
					}

					// Token: 0x02003C28 RID: 15400
					public class ART_TALL_M
					{
						// Token: 0x0400ED88 RID: 60808
						public static LocString NAME = UI.FormatAsLink("Do Not Disturb", "ART_TALL_M");

						// Token: 0x0400ED89 RID: 60809
						public static LocString DESC = "No one likes being interrupted when they're waiting for inspiration to strike.";
					}

					// Token: 0x02003C29 RID: 15401
					public class ART_TALL_L
					{
						// Token: 0x0400ED8A RID: 60810
						public static LocString NAME = UI.FormatAsLink("Mirror Ball", "ART_TALL_L");

						// Token: 0x0400ED8B RID: 60811
						public static LocString DESC = "Nearby, a companion animal waited for the object to be thrown.";
					}

					// Token: 0x02003C2A RID: 15402
					public class ART_TALL_P
					{
						// Token: 0x0400ED8C RID: 60812
						public static LocString NAME = "The Feast";

						// Token: 0x0400ED8D RID: 60813
						public static LocString DESC = "There were greasy fingerprints on the canvas even before the paint had dried.";
					}
				}
			}

			// Token: 0x020029B7 RID: 10679
			public class CO2SCRUBBER
			{
				// Token: 0x0400B81E RID: 47134
				public static LocString NAME = UI.FormatAsLink("Carbon Skimmer", "CO2SCRUBBER");

				// Token: 0x0400B81F RID: 47135
				public static LocString DESC = "Skimmers remove large amounts of carbon dioxide, but produce no breathable air.";

				// Token: 0x0400B820 RID: 47136
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Water", "WATER"),
					" to filter ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					" from the air."
				});
			}

			// Token: 0x020029B8 RID: 10680
			public class COMPOST
			{
				// Token: 0x0400B821 RID: 47137
				public static LocString NAME = UI.FormatAsLink("Compost", "COMPOST");

				// Token: 0x0400B822 RID: 47138
				public static LocString DESC = "Composts safely deal with biological waste, producing fresh dirt.";

				// Token: 0x0400B823 RID: 47139
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Reduces ",
					UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
					", rotting ",
					UI.FormatAsLink("Foods", "FOOD"),
					", and discarded organics down into ",
					UI.FormatAsLink("Dirt", "DIRT"),
					"."
				});
			}

			// Token: 0x020029B9 RID: 10681
			public class COOKINGSTATION
			{
				// Token: 0x0400B824 RID: 47140
				public static LocString NAME = UI.FormatAsLink("Electric Grill", "COOKINGSTATION");

				// Token: 0x0400B825 RID: 47141
				public static LocString DESC = "Proper cooking eliminates foodborne disease and produces tasty, stress-relieving meals.";

				// Token: 0x0400B826 RID: 47142
				public static LocString EFFECT = "Cooks a wide variety of improved " + UI.FormatAsLink("Foods", "FOOD") + ".\n\nDuplicants will not fabricate items unless recipes are queued.";
			}

			// Token: 0x020029BA RID: 10682
			public class CRYOTANK
			{
				// Token: 0x0400B827 RID: 47143
				public static LocString NAME = UI.FormatAsLink("Cryotank 3000", "CRYOTANK");

				// Token: 0x0400B828 RID: 47144
				public static LocString DESC = "The tank appears impossibly old, but smells crisp and brand new.\n\nA silhouette just barely visible through the frost of the glass.";

				// Token: 0x0400B829 RID: 47145
				public static LocString DEFROSTBUTTON = "Defrost Friend";

				// Token: 0x0400B82A RID: 47146
				public static LocString DEFROSTBUTTONTOOLTIP = "A new pal is just an icebreaker away";
			}

			// Token: 0x020029BB RID: 10683
			public class GOURMETCOOKINGSTATION
			{
				// Token: 0x0400B82B RID: 47147
				public static LocString NAME = UI.FormatAsLink("Gas Range", "GOURMETCOOKINGSTATION");

				// Token: 0x0400B82C RID: 47148
				public static LocString DESC = "Luxury meals increase Duplicants' morale and prevents them from becoming stressed.";

				// Token: 0x0400B82D RID: 47149
				public static LocString EFFECT = "Cooks a wide variety of quality " + UI.FormatAsLink("Foods", "FOOD") + ".\n\nDuplicants will not fabricate items unless recipes are queued.";
			}

			// Token: 0x020029BC RID: 10684
			public class DININGTABLE
			{
				// Token: 0x0400B82E RID: 47150
				public static LocString NAME = UI.FormatAsLink("Mess Table", "DININGTABLE");

				// Token: 0x0400B82F RID: 47151
				public static LocString DESC = "Duplicants prefer to dine at a table, rather than eat off the floor.";

				// Token: 0x0400B830 RID: 47152
				public static LocString EFFECT = "Gives one Duplicant a place to eat.\n\nDuplicants will automatically eat at their assigned table when hungry.";
			}

			// Token: 0x020029BD RID: 10685
			public class DOOR
			{
				// Token: 0x0400B831 RID: 47153
				public static LocString NAME = UI.FormatAsLink("Pneumatic Door", "DOOR");

				// Token: 0x0400B832 RID: 47154
				public static LocString DESC = "Door controls can be used to prevent Duplicants from entering restricted areas.";

				// Token: 0x0400B833 RID: 47155
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Encloses areas without blocking ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" or ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" flow.\n\nWild ",
					UI.FormatAsLink("Critters", "CREATURES"),
					" cannot pass through doors."
				});

				// Token: 0x0400B834 RID: 47156
				public static LocString PRESSURE_SUIT_REQUIRED = UI.FormatAsLink("Atmo Suit", "ATMO_SUIT") + " required {0}";

				// Token: 0x0400B835 RID: 47157
				public static LocString PRESSURE_SUIT_NOT_REQUIRED = UI.FormatAsLink("Atmo Suit", "ATMO_SUIT") + " not required {0}";

				// Token: 0x0400B836 RID: 47158
				public static LocString ABOVE = "above";

				// Token: 0x0400B837 RID: 47159
				public static LocString BELOW = "below";

				// Token: 0x0400B838 RID: 47160
				public static LocString LEFT = "on left";

				// Token: 0x0400B839 RID: 47161
				public static LocString RIGHT = "on right";

				// Token: 0x0400B83A RID: 47162
				public static LocString LOGIC_OPEN = "Open/Close";

				// Token: 0x0400B83B RID: 47163
				public static LocString LOGIC_OPEN_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Open";

				// Token: 0x0400B83C RID: 47164
				public static LocString LOGIC_OPEN_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Close and lock";

				// Token: 0x0200389E RID: 14494
				public static class CONTROL_STATE
				{
					// Token: 0x02003C2B RID: 15403
					public class OPEN
					{
						// Token: 0x0400ED8E RID: 60814
						public static LocString NAME = "Open";

						// Token: 0x0400ED8F RID: 60815
						public static LocString TOOLTIP = "This door will remain open";
					}

					// Token: 0x02003C2C RID: 15404
					public class CLOSE
					{
						// Token: 0x0400ED90 RID: 60816
						public static LocString NAME = "Lock";

						// Token: 0x0400ED91 RID: 60817
						public static LocString TOOLTIP = "Nothing may pass through";
					}

					// Token: 0x02003C2D RID: 15405
					public class AUTO
					{
						// Token: 0x0400ED92 RID: 60818
						public static LocString NAME = "Auto";

						// Token: 0x0400ED93 RID: 60819
						public static LocString TOOLTIP = "Duplicants open and close this door as needed";
					}
				}
			}

			// Token: 0x020029BE RID: 10686
			public class ELECTROBANKCHARGER
			{
				// Token: 0x0400B83D RID: 47165
				public static LocString NAME = UI.FormatAsLink("Power Bank Charger", "ELECTROBANKCHARGER");

				// Token: 0x0400B83E RID: 47166
				public static LocString DESC = "Bionic Duplicants rely on a steady supply of power to function.";

				// Token: 0x0400B83F RID: 47167
				public static LocString EFFECT = "Converts empty " + UI.FormatAsLink("Eco Power Banks", "ELECTROBANK") + " into fully charged units ready for reuse.";
			}

			// Token: 0x020029BF RID: 10687
			public class SMALLELECTROBANKDISCHARGER
			{
				// Token: 0x0400B840 RID: 47168
				public static LocString NAME = UI.FormatAsLink("Compact Discharger", "SMALLELECTROBANKDISCHARGER");

				// Token: 0x0400B841 RID: 47169
				public static LocString DESC = "A small standalone power center that can be mounted on the floor or wall.";

				// Token: 0x0400B842 RID: 47170
				public static LocString EFFECT = "Converts stored energy from " + UI.FormatAsLink("Power Banks", "ELECTROBANK") + " into power for connected buildings.";
			}

			// Token: 0x020029C0 RID: 10688
			public class LARGEELECTROBANKDISCHARGER
			{
				// Token: 0x0400B843 RID: 47171
				public static LocString NAME = UI.FormatAsLink("Large Discharger", "LARGEELECTROBANKDISCHARGER");

				// Token: 0x0400B844 RID: 47172
				public static LocString DESC = "It's basically its own power grid.";

				// Token: 0x0400B845 RID: 47173
				public static LocString EFFECT = "Efficiently converts stored energy from " + UI.FormatAsLink("Power Banks", "ELECTROBANK") + " into power for connected buildings.";
			}

			// Token: 0x020029C1 RID: 10689
			public class ELECTROLYZER
			{
				// Token: 0x0400B846 RID: 47174
				public static LocString NAME = UI.FormatAsLink("Electrolyzer", "ELECTROLYZER");

				// Token: 0x0400B847 RID: 47175
				public static LocString DESC = "Water goes in one end, life sustaining oxygen comes out the other.";

				// Token: 0x0400B848 RID: 47176
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Water", "WATER"),
					" into ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" and ",
					UI.FormatAsLink("Hydrogen Gas", "HYDROGEN"),
					".\n\nBecomes idle when the area reaches maximum pressure capacity."
				});
			}

			// Token: 0x020029C2 RID: 10690
			public class RUSTDEOXIDIZER
			{
				// Token: 0x0400B849 RID: 47177
				public static LocString NAME = UI.FormatAsLink("Rust Deoxidizer", "RUSTDEOXIDIZER");

				// Token: 0x0400B84A RID: 47178
				public static LocString DESC = "Rust and salt goes in, oxygen comes out.";

				// Token: 0x0400B84B RID: 47179
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Rust", "RUST"),
					" into ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" and ",
					UI.FormatAsLink("Chlorine Gas", "CHLORINE"),
					".\n\nBecomes idle when the area reaches maximum pressure capacity."
				});
			}

			// Token: 0x020029C3 RID: 10691
			public class DESALINATOR
			{
				// Token: 0x0400B84C RID: 47180
				public static LocString NAME = UI.FormatAsLink("Desalinator", "DESALINATOR");

				// Token: 0x0400B84D RID: 47181
				public static LocString DESC = "Salt can be refined into table salt for a mealtime morale boost.";

				// Token: 0x0400B84E RID: 47182
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Removes ",
					UI.FormatAsLink("Salt", "SALT"),
					" from ",
					UI.FormatAsLink("Brine", "BRINE"),
					" or ",
					UI.FormatAsLink("Salt Water", "SALTWATER"),
					", producing ",
					UI.FormatAsLink("Water", "WATER"),
					"."
				});
			}

			// Token: 0x020029C4 RID: 10692
			public class POWERTRANSFORMERSMALL
			{
				// Token: 0x0400B84F RID: 47183
				public static LocString NAME = UI.FormatAsLink("Power Transformer", "POWERTRANSFORMERSMALL");

				// Token: 0x0400B850 RID: 47184
				public static LocString DESC = "Limiting the power drawn by wires prevents them from incurring overload damage.";

				// Token: 0x0400B851 RID: 47185
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Limits ",
					UI.FormatAsLink("Power", "POWER"),
					" flowing through the Transformer to 1000 W.\n\nConnect ",
					UI.FormatAsLink("Batteries", "BATTERY"),
					" on the large side to act as a valve and prevent ",
					UI.FormatAsLink("Wires", "WIRE"),
					" from drawing more than 1000 W.\n\nCan be rotated before construction."
				});
			}

			// Token: 0x020029C5 RID: 10693
			public class POWERTRANSFORMER
			{
				// Token: 0x0400B852 RID: 47186
				public static LocString NAME = UI.FormatAsLink("Large Power Transformer", "POWERTRANSFORMER");

				// Token: 0x0400B853 RID: 47187
				public static LocString DESC = "It's a power transformer, but larger.";

				// Token: 0x0400B854 RID: 47188
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Limits ",
					UI.FormatAsLink("Power", "POWER"),
					" flowing through the Transformer to 4 kW.\n\nConnect ",
					UI.FormatAsLink("Batteries", "BATTERY"),
					" on the large side to act as a valve and prevent ",
					UI.FormatAsLink("Wires", "WIRE"),
					" from drawing more than 4 kW.\n\nCan be rotated before construction."
				});
			}

			// Token: 0x020029C6 RID: 10694
			public class FLOORLAMP
			{
				// Token: 0x0400B855 RID: 47189
				public static LocString NAME = UI.FormatAsLink("Lamp", "FLOORLAMP");

				// Token: 0x0400B856 RID: 47190
				public static LocString DESC = "Any building's light emitting radius can be viewed in the light overlay.";

				// Token: 0x0400B857 RID: 47191
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Provides ",
					UI.FormatAsLink("Light", "LIGHT"),
					" when ",
					UI.FormatAsLink("Powered", "POWER"),
					".\n\nIncreases Duplicant workspeed within light radius."
				});

				// Token: 0x0200389F RID: 14495
				public class FACADES
				{
					// Token: 0x02003C2E RID: 15406
					public class DEFAULT_FLOORLAMP
					{
						// Token: 0x0400ED94 RID: 60820
						public static LocString NAME = UI.FormatAsLink("Lamp", "FLOORLAMP");

						// Token: 0x0400ED95 RID: 60821
						public static LocString DESC = "Any building's light emitting radius can be viewed in the light overlay.";
					}

					// Token: 0x02003C2F RID: 15407
					public class LEG
					{
						// Token: 0x0400ED96 RID: 60822
						public static LocString NAME = UI.FormatAsLink("Fragile Leg Lamp", "FLOORLAMP");

						// Token: 0x0400ED97 RID: 60823
						public static LocString DESC = "This lamp blazes forth in unparalleled glory.";
					}

					// Token: 0x02003C30 RID: 15408
					public class BRISTLEBLOSSOM
					{
						// Token: 0x0400ED98 RID: 60824
						public static LocString NAME = UI.FormatAsLink("Holiday Lamp", "FLOORLAMP");

						// Token: 0x0400ED99 RID: 60825
						public static LocString DESC = "It's a bit prickly, but it casts a festive glow.";
					}
				}
			}

			// Token: 0x020029C7 RID: 10695
			public class FLOWERVASE
			{
				// Token: 0x0400B858 RID: 47192
				public static LocString NAME = UI.FormatAsLink("Flower Pot", "FLOWERVASE");

				// Token: 0x0400B859 RID: 47193
				public static LocString DESC = "Flower pots allow decorative plants to be moved to new locations.";

				// Token: 0x0400B85A RID: 47194
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Houses a single ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" when sown with a ",
					UI.FormatAsLink("Seed", "PLANTS"),
					".\n\nIncreases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});

				// Token: 0x020038A0 RID: 14496
				public class FACADES
				{
					// Token: 0x02003C31 RID: 15409
					public class DEFAULT_FLOWERVASE
					{
						// Token: 0x0400ED9A RID: 60826
						public static LocString NAME = UI.FormatAsLink("Flower Pot", "FLOWERVASE");

						// Token: 0x0400ED9B RID: 60827
						public static LocString DESC = "The original container for plants on the move.";
					}

					// Token: 0x02003C32 RID: 15410
					public class RETRO_SUNNY
					{
						// Token: 0x0400ED9C RID: 60828
						public static LocString NAME = UI.FormatAsLink("Sunny Retro Flower Pot", "FLOWERVASE");

						// Token: 0x0400ED9D RID: 60829
						public static LocString DESC = "A funky yellow flower pot for plants on the move.";
					}

					// Token: 0x02003C33 RID: 15411
					public class RETRO_BOLD
					{
						// Token: 0x0400ED9E RID: 60830
						public static LocString NAME = UI.FormatAsLink("Bold Retro Flower Pot", "FLOWERVASE");

						// Token: 0x0400ED9F RID: 60831
						public static LocString DESC = "A funky red flower pot for plants on the move.";
					}

					// Token: 0x02003C34 RID: 15412
					public class RETRO_BRIGHT
					{
						// Token: 0x0400EDA0 RID: 60832
						public static LocString NAME = UI.FormatAsLink("Bright Retro Flower Pot", "FLOWERVASE");

						// Token: 0x0400EDA1 RID: 60833
						public static LocString DESC = "A funky green flower pot for plants on the move.";
					}

					// Token: 0x02003C35 RID: 15413
					public class RETRO_DREAMY
					{
						// Token: 0x0400EDA2 RID: 60834
						public static LocString NAME = UI.FormatAsLink("Dreamy Retro Flower Pot", "FLOWERVASE");

						// Token: 0x0400EDA3 RID: 60835
						public static LocString DESC = "A funky blue flower pot for plants on the move.";
					}

					// Token: 0x02003C36 RID: 15414
					public class RETRO_ELEGANT
					{
						// Token: 0x0400EDA4 RID: 60836
						public static LocString NAME = UI.FormatAsLink("Elegant Retro Flower Pot", "FLOWERVASE");

						// Token: 0x0400EDA5 RID: 60837
						public static LocString DESC = "A funky white flower pot for plants on the move.";
					}
				}
			}

			// Token: 0x020029C8 RID: 10696
			public class FLOWERVASEWALL
			{
				// Token: 0x0400B85B RID: 47195
				public static LocString NAME = UI.FormatAsLink("Wall Pot", "FLOWERVASEWALL");

				// Token: 0x0400B85C RID: 47196
				public static LocString DESC = "Placing a plant in a wall pot can add a spot of Decor to otherwise bare walls.";

				// Token: 0x0400B85D RID: 47197
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Houses a single ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" when sown with a ",
					UI.FormatAsLink("Seed", "PLANTS"),
					".\n\nIncreases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be hung from a wall."
				});

				// Token: 0x020038A1 RID: 14497
				public class FACADES
				{
					// Token: 0x02003C37 RID: 15415
					public class DEFAULT_FLOWERVASEWALL
					{
						// Token: 0x0400EDA6 RID: 60838
						public static LocString NAME = UI.FormatAsLink("Wall Pot", "FLOWERVASEWALL");

						// Token: 0x0400EDA7 RID: 60839
						public static LocString DESC = "Facilitates vertical plant displays.";
					}

					// Token: 0x02003C38 RID: 15416
					public class RETRO_GREEN
					{
						// Token: 0x0400EDA8 RID: 60840
						public static LocString NAME = UI.FormatAsLink("Bright Retro Wall Pot", "FLOWERVASEWALL");

						// Token: 0x0400EDA9 RID: 60841
						public static LocString DESC = "Vertical gardens are pretty nifty.";
					}

					// Token: 0x02003C39 RID: 15417
					public class RETRO_YELLOW
					{
						// Token: 0x0400EDAA RID: 60842
						public static LocString NAME = UI.FormatAsLink("Sunny Retro Wall Pot", "FLOWERVASEWALL");

						// Token: 0x0400EDAB RID: 60843
						public static LocString DESC = "Vertical gardens are pretty nifty.";
					}

					// Token: 0x02003C3A RID: 15418
					public class RETRO_RED
					{
						// Token: 0x0400EDAC RID: 60844
						public static LocString NAME = UI.FormatAsLink("Bold Retro Wall Pot", "FLOWERVASEWALL");

						// Token: 0x0400EDAD RID: 60845
						public static LocString DESC = "Vertical gardens are pretty nifty.";
					}

					// Token: 0x02003C3B RID: 15419
					public class RETRO_BLUE
					{
						// Token: 0x0400EDAE RID: 60846
						public static LocString NAME = UI.FormatAsLink("Dreamy Retro Wall Pot", "FLOWERVASEWALL");

						// Token: 0x0400EDAF RID: 60847
						public static LocString DESC = "Vertical gardens are pretty nifty.";
					}

					// Token: 0x02003C3C RID: 15420
					public class RETRO_WHITE
					{
						// Token: 0x0400EDB0 RID: 60848
						public static LocString NAME = UI.FormatAsLink("Elegant Retro Wall Pot", "FLOWERVASEWALL");

						// Token: 0x0400EDB1 RID: 60849
						public static LocString DESC = "Vertical gardens are pretty nifty.";
					}
				}
			}

			// Token: 0x020029C9 RID: 10697
			public class FLOWERVASEHANGING
			{
				// Token: 0x0400B85E RID: 47198
				public static LocString NAME = UI.FormatAsLink("Hanging Pot", "FLOWERVASEHANGING");

				// Token: 0x0400B85F RID: 47199
				public static LocString DESC = "Hanging pots can add some Decor to a room, without blocking buildings on the floor.";

				// Token: 0x0400B860 RID: 47200
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Houses a single ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" when sown with a ",
					UI.FormatAsLink("Seed", "PLANTS"),
					".\n\nIncreases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be hung from a ceiling."
				});

				// Token: 0x020038A2 RID: 14498
				public class FACADES
				{
					// Token: 0x02003C3D RID: 15421
					public class RETRO_RED
					{
						// Token: 0x0400EDB2 RID: 60850
						public static LocString NAME = UI.FormatAsLink("Bold Hanging Pot", "FLOWERVASEHANGING");

						// Token: 0x0400EDB3 RID: 60851
						public static LocString DESC = "Suspended vessels really elevate a plant display.";
					}

					// Token: 0x02003C3E RID: 15422
					public class RETRO_GREEN
					{
						// Token: 0x0400EDB4 RID: 60852
						public static LocString NAME = UI.FormatAsLink("Bright Hanging Pot", "FLOWERVASEHANGING");

						// Token: 0x0400EDB5 RID: 60853
						public static LocString DESC = "Suspended vessels really elevate a plant display.";
					}

					// Token: 0x02003C3F RID: 15423
					public class RETRO_BLUE
					{
						// Token: 0x0400EDB6 RID: 60854
						public static LocString NAME = UI.FormatAsLink("Dreamy Hanging Pot", "FLOWERVASEHANGING");

						// Token: 0x0400EDB7 RID: 60855
						public static LocString DESC = "Suspended vessels really elevate a plant display.";
					}

					// Token: 0x02003C40 RID: 15424
					public class RETRO_YELLOW
					{
						// Token: 0x0400EDB8 RID: 60856
						public static LocString NAME = UI.FormatAsLink("Sunny Hanging Pot", "FLOWERVASEHANGING");

						// Token: 0x0400EDB9 RID: 60857
						public static LocString DESC = "Suspended vessels really elevate a plant display.";
					}

					// Token: 0x02003C41 RID: 15425
					public class RETRO_WHITE
					{
						// Token: 0x0400EDBA RID: 60858
						public static LocString NAME = UI.FormatAsLink("Elegant Hanging Pot", "FLOWERVASEHANGING");

						// Token: 0x0400EDBB RID: 60859
						public static LocString DESC = "Suspended vessels really elevate a plant display.";
					}

					// Token: 0x02003C42 RID: 15426
					public class BEAKER
					{
						// Token: 0x0400EDBC RID: 60860
						public static LocString NAME = UI.FormatAsLink("Beaker Hanging Pot", "FLOWERVASEHANGING");

						// Token: 0x0400EDBD RID: 60861
						public static LocString DESC = "A measured approach to indoor plant decor.";
					}

					// Token: 0x02003C43 RID: 15427
					public class RUBIKS
					{
						// Token: 0x0400EDBE RID: 60862
						public static LocString NAME = UI.FormatAsLink("Puzzle Cube Hanging Pot", "FLOWERVASEHANGING");

						// Token: 0x0400EDBF RID: 60863
						public static LocString DESC = "The real puzzle is how to keep indoor plants alive.";
					}
				}
			}

			// Token: 0x020029CA RID: 10698
			public class FLOWERVASEHANGINGFANCY
			{
				// Token: 0x0400B861 RID: 47201
				public static LocString NAME = UI.FormatAsLink("Aero Pot", "FLOWERVASEHANGINGFANCY");

				// Token: 0x0400B862 RID: 47202
				public static LocString DESC = "Aero pots can be hung from the ceiling and have extremely high Decor.";

				// Token: 0x0400B863 RID: 47203
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Houses a single ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" when sown with a ",
					UI.FormatAsLink("Seed", "PLANTS"),
					".\n\nIncreases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be hung from a ceiling."
				});

				// Token: 0x020038A3 RID: 14499
				public class FACADES
				{
				}
			}

			// Token: 0x020029CB RID: 10699
			public class FLUSHTOILET
			{
				// Token: 0x0400B864 RID: 47204
				public static LocString NAME = UI.FormatAsLink("Lavatory", "FLUSHTOILET");

				// Token: 0x0400B865 RID: 47205
				public static LocString DESC = "Lavatories transmit fewer germs to Duplicants' skin and require no emptying.";

				// Token: 0x0400B866 RID: 47206
				public static LocString EFFECT = "Gives Duplicants a place to relieve themselves.\n\nSpreads very few " + UI.FormatAsLink("Germs", "DISEASE") + ".";

				// Token: 0x020038A4 RID: 14500
				public class FACADES
				{
					// Token: 0x02003C44 RID: 15428
					public class DEFAULT_FLUSHTOILET
					{
						// Token: 0x0400EDC0 RID: 60864
						public static LocString NAME = UI.FormatAsLink("Lavatory", "FLUSHTOILET");

						// Token: 0x0400EDC1 RID: 60865
						public static LocString DESC = "Lavatories transmit fewer germs to Duplicants' skin and require no emptying.";
					}

					// Token: 0x02003C45 RID: 15429
					public class POLKA_DARKPURPLERESIN
					{
						// Token: 0x0400EDC2 RID: 60866
						public static LocString NAME = UI.FormatAsLink("Mod Dot Lavatory", "FLUSHTOILET");

						// Token: 0x0400EDC3 RID: 60867
						public static LocString DESC = "For those who've really got to a-go-go.";
					}

					// Token: 0x02003C46 RID: 15430
					public class POLKA_DARKNAVYNOOKGREEN
					{
						// Token: 0x0400EDC4 RID: 60868
						public static LocString NAME = UI.FormatAsLink("Party Dot Lavatory", "FLUSHTOILET");

						// Token: 0x0400EDC5 RID: 60869
						public static LocString DESC = "Smooth moves happen here.";
					}

					// Token: 0x02003C47 RID: 15431
					public class PURPLE_BRAINFAT
					{
						// Token: 0x0400EDC6 RID: 60870
						public static LocString NAME = UI.FormatAsLink("Faint Purple Lavatory", "FLUSHTOILET");

						// Token: 0x0400EDC7 RID: 60871
						public static LocString DESC = "It's like pooping inside Hexalent fruit!";
					}

					// Token: 0x02003C48 RID: 15432
					public class YELLOW_TARTAR
					{
						// Token: 0x0400EDC8 RID: 60872
						public static LocString NAME = UI.FormatAsLink("Ick Yellow Lavatory", "FLUSHTOILET");

						// Token: 0x0400EDC9 RID: 60873
						public static LocString DESC = "Someone thought it'd be a good idea to have the outside match the inside.";
					}

					// Token: 0x02003C49 RID: 15433
					public class RED_ROSE
					{
						// Token: 0x0400EDCA RID: 60874
						public static LocString NAME = UI.FormatAsLink("Puce Pink Lavatory", "FLUSHTOILET");

						// Token: 0x0400EDCB RID: 60875
						public static LocString DESC = "The scented pink toilet paper smells like a rosebush in a sewage plant.";
					}

					// Token: 0x02003C4A RID: 15434
					public class GREEN_MUSH
					{
						// Token: 0x0400EDCC RID: 60876
						public static LocString NAME = UI.FormatAsLink("Mush Green Lavatory", "FLUSHTOILET");

						// Token: 0x0400EDCD RID: 60877
						public static LocString DESC = "Mush in, mush out.";
					}

					// Token: 0x02003C4B RID: 15435
					public class BLUE_BABYTEARS
					{
						// Token: 0x0400EDCE RID: 60878
						public static LocString NAME = UI.FormatAsLink("Weepy Lavatory", "FLUSHTOILET");

						// Token: 0x0400EDCF RID: 60879
						public static LocString DESC = "A private place to feel big feelings.";
					}
				}
			}

			// Token: 0x020029CC RID: 10700
			public class SHOWER
			{
				// Token: 0x0400B867 RID: 47207
				public static LocString NAME = UI.FormatAsLink("Shower", "SHOWER");

				// Token: 0x0400B868 RID: 47208
				public static LocString DESC = "Regularly showering will prevent Duplicants spreading germs to the things they touch.";

				// Token: 0x0400B869 RID: 47209
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Improves Duplicant ",
					UI.FormatAsLink("Morale", "MORALE"),
					" and removes surface ",
					UI.FormatAsLink("Germs", "DISEASE"),
					"."
				});
			}

			// Token: 0x020029CD RID: 10701
			public class CONDUIT
			{
				// Token: 0x020038A5 RID: 14501
				public class STATUS_ITEM
				{
					// Token: 0x0400E4A4 RID: 58532
					public static LocString NAME = "Marked for Emptying";

					// Token: 0x0400E4A5 RID: 58533
					public static LocString TOOLTIP = "Awaiting a " + UI.FormatAsLink("Plumber", "PLUMBER") + " to clear this pipe";
				}
			}

			// Token: 0x020029CE RID: 10702
			public class MORBROVERMAKER
			{
				// Token: 0x0400B86A RID: 47210
				public static LocString NAME = UI.FormatAsLink("Biobot Builder", "STORYTRAITMORBROVER");

				// Token: 0x0400B86B RID: 47211
				public static LocString DESC = "Allows a skilled Duplicant to manufacture a steady supply of icky yet effective bots.";

				// Token: 0x0400B86C RID: 47212
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Zombie Spores", "ZOMBIESPORES"),
					" and ",
					UI.FormatAsLink("Steel", "STEEL"),
					" to craft biofueled machines that can be sent into hostile environments.\n\nDefunct ",
					UI.FormatAsLink("Biobots", "STORYTRAITMORBROVER"),
					" drop harvestable ",
					UI.FormatAsLink("Steel", "STEEL"),
					"."
				});
			}

			// Token: 0x020029CF RID: 10703
			public class FOSSILDIG
			{
				// Token: 0x0400B86D RID: 47213
				public static LocString NAME = "Ancient Specimen";

				// Token: 0x0400B86E RID: 47214
				public static LocString DESC = "It's not from around here.";

				// Token: 0x0400B86F RID: 47215
				public static LocString EFFECT = "Contains a partial " + UI.FormatAsLink("Fossil", "FOSSIL") + " left behind by a giant critter.\n\nStudying the full skeleton could yield the information required to access a valuable new resource.";
			}

			// Token: 0x020029D0 RID: 10704
			public class FOSSILDIG_COMPLETED
			{
				// Token: 0x0400B870 RID: 47216
				public static LocString NAME = "Fossil Quarry";

				// Token: 0x0400B871 RID: 47217
				public static LocString DESC = "There sure are a lot of old bones in this area.";

				// Token: 0x0400B872 RID: 47218
				public static LocString EFFECT = "Contains a deep cache of harvestable " + UI.FormatAsLink("Fossils", "FOSSIL") + ".";
			}

			// Token: 0x020029D1 RID: 10705
			public class GAMMARAYOVEN
			{
				// Token: 0x0400B873 RID: 47219
				public static LocString NAME = UI.FormatAsLink("Gamma Ray Oven", "GAMMARAYOVEN");

				// Token: 0x0400B874 RID: 47220
				public static LocString DESC = "Nuke your food.";

				// Token: 0x0400B875 RID: 47221
				public static LocString EFFECT = "Cooks a variety of " + UI.FormatAsLink("Foods", "FOOD") + ".\n\nDuplicants will not fabricate items unless recipes are queued.";
			}

			// Token: 0x020029D2 RID: 10706
			public class GASCARGOBAY
			{
				// Token: 0x0400B876 RID: 47222
				public static LocString NAME = UI.FormatAsLink("Gas Cargo Canister", "GASCARGOBAY");

				// Token: 0x0400B877 RID: 47223
				public static LocString DESC = "Duplicants will fill cargo bays with any resources they find during space missions.";

				// Token: 0x0400B878 RID: 47224
				public static LocString EFFECT = "Allows Duplicants to store any " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " resources found during space missions.\n\nStored resources become available to the colony upon the rocket's return.";
			}

			// Token: 0x020029D3 RID: 10707
			public class GASCARGOBAYCLUSTER
			{
				// Token: 0x0400B879 RID: 47225
				public static LocString NAME = UI.FormatAsLink("Large Gas Cargo Canister", "GASCARGOBAY");

				// Token: 0x0400B87A RID: 47226
				public static LocString DESC = "Holds more than a typical gas cargo canister.";

				// Token: 0x0400B87B RID: 47227
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to store most of the ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" resources found during space missions.\n\nStored resources become available to the colony upon the rocket's return.\n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x020029D4 RID: 10708
			public class GASCARGOBAYSMALL
			{
				// Token: 0x0400B87C RID: 47228
				public static LocString NAME = UI.FormatAsLink("Gas Cargo Canister", "GASCARGOBAYSMALL");

				// Token: 0x0400B87D RID: 47229
				public static LocString DESC = "Duplicants fill cargo canisters with any resources they find during space missions.";

				// Token: 0x0400B87E RID: 47230
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to store some of the ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" resources found during space missions.\n\nStored resources become available to the colony upon the rocket's return. \n\nMust be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					"."
				});
			}

			// Token: 0x020029D5 RID: 10709
			public class GASCONDUIT
			{
				// Token: 0x0400B87F RID: 47231
				public static LocString NAME = UI.FormatAsLink("Gas Pipe", "GASCONDUIT");

				// Token: 0x0400B880 RID: 47232
				public static LocString DESC = "Gas pipes are used to connect the inputs and outputs of ventilated buildings.";

				// Token: 0x0400B881 RID: 47233
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" between ",
					UI.FormatAsLink("Outputs", "GASPIPING"),
					" and ",
					UI.FormatAsLink("Intakes", "GASPIPING"),
					".\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x020029D6 RID: 10710
			public class GASCONDUITBRIDGE
			{
				// Token: 0x0400B882 RID: 47234
				public static LocString NAME = UI.FormatAsLink("Gas Bridge", "GASCONDUITBRIDGE");

				// Token: 0x0400B883 RID: 47235
				public static LocString DESC = "Separate pipe systems prevent mingled contents from causing building damage.";

				// Token: 0x0400B884 RID: 47236
				public static LocString EFFECT = "Runs one " + UI.FormatAsLink("Gas Pipe", "GASPIPING") + " section over another without joining them.\n\nCan be run through wall and floor tile.";
			}

			// Token: 0x020029D7 RID: 10711
			public class GASCONDUITPREFERENTIALFLOW
			{
				// Token: 0x0400B885 RID: 47237
				public static LocString NAME = UI.FormatAsLink("Priority Gas Flow", "GASCONDUITPREFERENTIALFLOW");

				// Token: 0x0400B886 RID: 47238
				public static LocString DESC = "Priority flows ensure important buildings are filled first when on a system with other buildings.";

				// Token: 0x0400B887 RID: 47239
				public static LocString EFFECT = "Diverts " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " to a secondary input when its primary input overflows.";
			}

			// Token: 0x020029D8 RID: 10712
			public class LIQUIDCONDUITPREFERENTIALFLOW
			{
				// Token: 0x0400B888 RID: 47240
				public static LocString NAME = UI.FormatAsLink("Priority Liquid Flow", "LIQUIDCONDUITPREFERENTIALFLOW");

				// Token: 0x0400B889 RID: 47241
				public static LocString DESC = "Priority flows ensure important buildings are filled first when on a system with other buildings.";

				// Token: 0x0400B88A RID: 47242
				public static LocString EFFECT = "Diverts " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " to a secondary input when its primary input overflows.";
			}

			// Token: 0x020029D9 RID: 10713
			public class GASCONDUITOVERFLOW
			{
				// Token: 0x0400B88B RID: 47243
				public static LocString NAME = UI.FormatAsLink("Gas Overflow Valve", "GASCONDUITOVERFLOW");

				// Token: 0x0400B88C RID: 47244
				public static LocString DESC = "Overflow valves can be used to prioritize which buildings should receive precious resources first.";

				// Token: 0x0400B88D RID: 47245
				public static LocString EFFECT = "Fills a secondary" + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " output only when its primary output is blocked.";
			}

			// Token: 0x020029DA RID: 10714
			public class LIQUIDCONDUITOVERFLOW
			{
				// Token: 0x0400B88E RID: 47246
				public static LocString NAME = UI.FormatAsLink("Liquid Overflow Valve", "LIQUIDCONDUITOVERFLOW");

				// Token: 0x0400B88F RID: 47247
				public static LocString DESC = "Overflow valves can be used to prioritize which buildings should receive precious resources first.";

				// Token: 0x0400B890 RID: 47248
				public static LocString EFFECT = "Fills a secondary" + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " output only when its primary output is blocked.";
			}

			// Token: 0x020029DB RID: 10715
			public class LAUNCHPAD
			{
				// Token: 0x0400B891 RID: 47249
				public static LocString NAME = UI.FormatAsLink("Rocket Platform", "LAUNCHPAD");

				// Token: 0x0400B892 RID: 47250
				public static LocString DESC = "A platform from which rockets can be launched and on which they can land.";

				// Token: 0x0400B893 RID: 47251
				public static LocString EFFECT = "Precursor to construction of all other Rocket modules.\n\nAllows Rockets to launch from or land on the host Planetoid.\n\nAutomatically links up to " + BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME + UI.FormatAsLink("s", "MODULARLAUNCHPADPORTSOLID") + " built to either side of the platform.";

				// Token: 0x0400B894 RID: 47252
				public static LocString LOGIC_PORT_READY = "Rocket Checklist";

				// Token: 0x0400B895 RID: 47253
				public static LocString LOGIC_PORT_READY_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when its rocket is ready for flight";

				// Token: 0x0400B896 RID: 47254
				public static LocString LOGIC_PORT_READY_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x0400B897 RID: 47255
				public static LocString LOGIC_PORT_LANDED_ROCKET = "Landed Rocket";

				// Token: 0x0400B898 RID: 47256
				public static LocString LOGIC_PORT_LANDED_ROCKET_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when its rocket is on the " + BUILDINGS.PREFABS.LAUNCHPAD.NAME;

				// Token: 0x0400B899 RID: 47257
				public static LocString LOGIC_PORT_LANDED_ROCKET_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x0400B89A RID: 47258
				public static LocString LOGIC_PORT_LAUNCH = "Launch Rocket";

				// Token: 0x0400B89B RID: 47259
				public static LocString LOGIC_PORT_LAUNCH_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Launch rocket";

				// Token: 0x0400B89C RID: 47260
				public static LocString LOGIC_PORT_LAUNCH_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Cancel launch";
			}

			// Token: 0x020029DC RID: 10716
			public class GASFILTER
			{
				// Token: 0x0400B89D RID: 47261
				public static LocString NAME = UI.FormatAsLink("Gas Filter", "GASFILTER");

				// Token: 0x0400B89E RID: 47262
				public static LocString DESC = "All gases are sent into the building's output pipe, except the gas chosen for filtering.";

				// Token: 0x0400B89F RID: 47263
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sieves one ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" from the air, sending it into a dedicated ",
					UI.FormatAsLink("Pipe", "GASPIPING"),
					"."
				});

				// Token: 0x0400B8A0 RID: 47264
				public static LocString STATUS_ITEM = "Filters: {0}";

				// Token: 0x0400B8A1 RID: 47265
				public static LocString ELEMENT_NOT_SPECIFIED = "Not Specified";
			}

			// Token: 0x020029DD RID: 10717
			public class SOLIDFILTER
			{
				// Token: 0x0400B8A2 RID: 47266
				public static LocString NAME = UI.FormatAsLink("Solid Filter", "SOLIDFILTER");

				// Token: 0x0400B8A3 RID: 47267
				public static LocString DESC = "All solids are sent into the building's output conveyor, except the solid chosen for filtering.";

				// Token: 0x0400B8A4 RID: 47268
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Separates one ",
					UI.FormatAsLink("Solid Material", "ELEMENTS_SOLID"),
					" from the conveyor, sending it into a dedicated ",
					BUILDINGS.PREFABS.SOLIDCONDUIT.NAME,
					"."
				});

				// Token: 0x0400B8A5 RID: 47269
				public static LocString STATUS_ITEM = "Filters: {0}";

				// Token: 0x0400B8A6 RID: 47270
				public static LocString ELEMENT_NOT_SPECIFIED = "Not Specified";
			}

			// Token: 0x020029DE RID: 10718
			public class GASPERMEABLEMEMBRANE
			{
				// Token: 0x0400B8A7 RID: 47271
				public static LocString NAME = UI.FormatAsLink("Airflow Tile", "GASPERMEABLEMEMBRANE");

				// Token: 0x0400B8A8 RID: 47272
				public static LocString DESC = "Building with airflow tile promotes better gas circulation within a colony.";

				// Token: 0x0400B8A9 RID: 47273
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Used to build the walls and floors of rooms.\n\nBlocks ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" flow without obstructing ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					"."
				});
			}

			// Token: 0x020029DF RID: 10719
			public class DEVPUMPGAS
			{
				// Token: 0x0400B8AA RID: 47274
				public static LocString NAME = "Dev Pump Gas";

				// Token: 0x0400B8AB RID: 47275
				public static LocString DESC = "Piping a pump's output to a building's intake will send gas to that building.";

				// Token: 0x0400B8AC RID: 47276
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Draws in ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" and runs it through ",
					UI.FormatAsLink("Pipes", "GASPIPING"),
					".\n\nMust be immersed in ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					"."
				});
			}

			// Token: 0x020029E0 RID: 10720
			public class GASPUMP
			{
				// Token: 0x0400B8AD RID: 47277
				public static LocString NAME = UI.FormatAsLink("Gas Pump", "GASPUMP");

				// Token: 0x0400B8AE RID: 47278
				public static LocString DESC = "Piping a pump's output to a building's intake will send gas to that building.";

				// Token: 0x0400B8AF RID: 47279
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Draws in ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" and runs it through ",
					UI.FormatAsLink("Pipes", "GASPIPING"),
					".\n\nMust be immersed in ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					"."
				});
			}

			// Token: 0x020029E1 RID: 10721
			public class GASMINIPUMP
			{
				// Token: 0x0400B8B0 RID: 47280
				public static LocString NAME = UI.FormatAsLink("Mini Gas Pump", "GASMINIPUMP");

				// Token: 0x0400B8B1 RID: 47281
				public static LocString DESC = "Mini pumps are useful for moving small quantities of gas with minimum power.";

				// Token: 0x0400B8B2 RID: 47282
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Draws in a small amount of ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" and runs it through ",
					UI.FormatAsLink("Pipes", "GASPIPING"),
					".\n\nMust be immersed in ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					"."
				});
			}

			// Token: 0x020029E2 RID: 10722
			public class GASVALVE
			{
				// Token: 0x0400B8B3 RID: 47283
				public static LocString NAME = UI.FormatAsLink("Gas Valve", "GASVALVE");

				// Token: 0x0400B8B4 RID: 47284
				public static LocString DESC = "Valves control the amount of gas that moves through pipes, preventing waste.";

				// Token: 0x0400B8B5 RID: 47285
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Controls the ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" volume permitted through ",
					UI.FormatAsLink("Pipes", "GASPIPING"),
					"."
				});
			}

			// Token: 0x020029E3 RID: 10723
			public class GASLOGICVALVE
			{
				// Token: 0x0400B8B6 RID: 47286
				public static LocString NAME = UI.FormatAsLink("Gas Shutoff", "GASLOGICVALVE");

				// Token: 0x0400B8B7 RID: 47287
				public static LocString DESC = "Automated piping saves power and time by removing the need for Duplicant input.";

				// Token: 0x0400B8B8 RID: 47288
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Connects to an ",
					UI.FormatAsLink("Automation", "LOGIC"),
					" grid to automatically turn ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" flow on or off."
				});

				// Token: 0x0400B8B9 RID: 47289
				public static LocString LOGIC_PORT = "Open/Close";

				// Token: 0x0400B8BA RID: 47290
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Allow gas flow";

				// Token: 0x0400B8BB RID: 47291
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Prevent gas flow";
			}

			// Token: 0x020029E4 RID: 10724
			public class GASLIMITVALVE
			{
				// Token: 0x0400B8BC RID: 47292
				public static LocString NAME = UI.FormatAsLink("Gas Meter Valve", "GASLIMITVALVE");

				// Token: 0x0400B8BD RID: 47293
				public static LocString DESC = "Meter Valves let an exact amount of gas pass through before shutting off.";

				// Token: 0x0400B8BE RID: 47294
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Connects to an ",
					UI.FormatAsLink("Automation", "LOGIC"),
					" grid to automatically turn ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" flow off when the specified amount has passed through it."
				});

				// Token: 0x0400B8BF RID: 47295
				public static LocString LOGIC_PORT_OUTPUT = "Limit Reached";

				// Token: 0x0400B8C0 RID: 47296
				public static LocString OUTPUT_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if limit has been reached";

				// Token: 0x0400B8C1 RID: 47297
				public static LocString OUTPUT_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x0400B8C2 RID: 47298
				public static LocString LOGIC_PORT_RESET = "Reset Meter";

				// Token: 0x0400B8C3 RID: 47299
				public static LocString RESET_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Reset the amount";

				// Token: 0x0400B8C4 RID: 47300
				public static LocString RESET_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Nothing";
			}

			// Token: 0x020029E5 RID: 10725
			public class GASVENT
			{
				// Token: 0x0400B8C5 RID: 47301
				public static LocString NAME = UI.FormatAsLink("Gas Vent", "GASVENT");

				// Token: 0x0400B8C6 RID: 47302
				public static LocString DESC = "Vents are an exit point for gases from ventilation systems.";

				// Token: 0x0400B8C7 RID: 47303
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Releases ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" from ",
					UI.FormatAsLink("Gas Pipes", "GASPIPING"),
					"."
				});
			}

			// Token: 0x020029E6 RID: 10726
			public class GASVENTHIGHPRESSURE
			{
				// Token: 0x0400B8C8 RID: 47304
				public static LocString NAME = UI.FormatAsLink("High Pressure Gas Vent", "GASVENTHIGHPRESSURE");

				// Token: 0x0400B8C9 RID: 47305
				public static LocString DESC = "High pressure vents can expel gas into more highly pressurized environments.";

				// Token: 0x0400B8CA RID: 47306
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Releases ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" from ",
					UI.FormatAsLink("Gas Pipes", "GASPIPING"),
					" into high pressure locations."
				});
			}

			// Token: 0x020029E7 RID: 10727
			public class GASBOTTLER
			{
				// Token: 0x0400B8CB RID: 47307
				public static LocString NAME = UI.FormatAsLink("Canister Filler", "GASBOTTLER");

				// Token: 0x0400B8CC RID: 47308
				public static LocString DESC = "Canisters allow Duplicants to manually deliver gases from place to place.";

				// Token: 0x0400B8CD RID: 47309
				public static LocString EFFECT = "Automatically stores piped " + UI.FormatAsLink("Gases", "ELEMENTS_GAS") + " into canisters for manual transport.";
			}

			// Token: 0x020029E8 RID: 10728
			public class LIQUIDBOTTLER
			{
				// Token: 0x0400B8CE RID: 47310
				public static LocString NAME = UI.FormatAsLink("Bottle Filler", "LIQUIDBOTTLER");

				// Token: 0x0400B8CF RID: 47311
				public static LocString DESC = "Bottle fillers allow Duplicants to manually deliver liquids from place to place.";

				// Token: 0x0400B8D0 RID: 47312
				public static LocString EFFECT = "Automatically stores piped " + UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID") + " into bottles for manual transport.";
			}

			// Token: 0x020029E9 RID: 10729
			public class GENERATOR
			{
				// Token: 0x0400B8D1 RID: 47313
				public static LocString NAME = UI.FormatAsLink("Coal Generator", "GENERATOR");

				// Token: 0x0400B8D2 RID: 47314
				public static LocString DESC = "Burning coal produces more energy than manual power, but emits heat and exhaust.";

				// Token: 0x0400B8D3 RID: 47315
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Coal", "CARBON"),
					" into electrical ",
					UI.FormatAsLink("Power", "POWER"),
					".\n\nProduces ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					"."
				});

				// Token: 0x0400B8D4 RID: 47316
				public static LocString OVERPRODUCTION = "{Generator} overproduction";
			}

			// Token: 0x020029EA RID: 10730
			public class GENETICANALYSISSTATION
			{
				// Token: 0x0400B8D5 RID: 47317
				public static LocString NAME = UI.FormatAsLink("Botanical Analyzer", "GENETICANALYSISSTATION");

				// Token: 0x0400B8D6 RID: 47318
				public static LocString DESC = "Would a mutated rose still smell as sweet?";

				// Token: 0x0400B8D7 RID: 47319
				public static LocString EFFECT = "Identifies new " + UI.FormatAsLink("Seed", "PLANTS") + " subspecies.";
			}

			// Token: 0x020029EB RID: 10731
			public class DEVGENERATOR
			{
				// Token: 0x0400B8D8 RID: 47320
				public static LocString NAME = "Dev Generator";

				// Token: 0x0400B8D9 RID: 47321
				public static LocString DESC = "Runs on coffee.";

				// Token: 0x0400B8DA RID: 47322
				public static LocString EFFECT = "Generates testing power for late nights.";
			}

			// Token: 0x020029EC RID: 10732
			public class DEVLIFESUPPORT
			{
				// Token: 0x0400B8DB RID: 47323
				public static LocString NAME = "Dev Life Support";

				// Token: 0x0400B8DC RID: 47324
				public static LocString DESC = "Keeps Duplicants cozy and breathing.";

				// Token: 0x0400B8DD RID: 47325
				public static LocString EFFECT = "Generates warm, oxygen-rich air.";
			}

			// Token: 0x020029ED RID: 10733
			public class DEVLIGHTGENERATOR
			{
				// Token: 0x0400B8DE RID: 47326
				public static LocString NAME = "Dev Light Source";

				// Token: 0x0400B8DF RID: 47327
				public static LocString DESC = "Brightens up a dev's darkest hours.";

				// Token: 0x0400B8E0 RID: 47328
				public static LocString EFFECT = "Generates dimmable light on demand.";

				// Token: 0x0400B8E1 RID: 47329
				public static LocString FALLOFF_LABEL = "Falloff Rate";

				// Token: 0x0400B8E2 RID: 47330
				public static LocString BRIGHTNESS_LABEL = "Brightness";

				// Token: 0x0400B8E3 RID: 47331
				public static LocString RANGE_LABEL = "Range";
			}

			// Token: 0x020029EE RID: 10734
			public class DEVRADIATIONGENERATOR
			{
				// Token: 0x0400B8E4 RID: 47332
				public static LocString NAME = "Dev Radiation Emitter";

				// Token: 0x0400B8E5 RID: 47333
				public static LocString DESC = "That's some <i>strong</i> coffee.";

				// Token: 0x0400B8E6 RID: 47334
				public static LocString EFFECT = "Generates on-demand radiation to keep things clear. <i>Nu-</i>clear.";
			}

			// Token: 0x020029EF RID: 10735
			public class DEVHEATER
			{
				// Token: 0x0400B8E7 RID: 47335
				public static LocString NAME = "Dev Heater";

				// Token: 0x0400B8E8 RID: 47336
				public static LocString DESC = "Did someone touch the thermostat?";

				// Token: 0x0400B8E9 RID: 47337
				public static LocString EFFECT = "Generates on-demand heat for testing toastiness.";
			}

			// Token: 0x020029F0 RID: 10736
			public class GENERICFABRICATOR
			{
				// Token: 0x0400B8EA RID: 47338
				public static LocString NAME = UI.FormatAsLink("Omniprinter", "GENERICFABRICATOR");

				// Token: 0x0400B8EB RID: 47339
				public static LocString DESC = "Omniprinters are incapable of printing organic matter.";

				// Token: 0x0400B8EC RID: 47340
				public static LocString EFFECT = "Converts " + UI.FormatAsLink("Raw Mineral", "RAWMINERAL") + " into unique materials and objects.";
			}

			// Token: 0x020029F1 RID: 10737
			public class GEOTUNER
			{
				// Token: 0x0400B8ED RID: 47341
				public static LocString NAME = UI.FormatAsLink("Geotuner", "GEOTUNER");

				// Token: 0x0400B8EE RID: 47342
				public static LocString DESC = "The targeted geyser receives stored amplification data when it is erupting.";

				// Token: 0x0400B8EF RID: 47343
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Increases the ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" and output of an analyzed ",
					UI.FormatAsLink("Geyser", "GEYSERS"),
					".\n\nMultiple Geotuners can be directed at a single ",
					UI.FormatAsLink("Geyser", "GEYSERS"),
					" anywhere on an asteroid."
				});

				// Token: 0x0400B8F0 RID: 47344
				public static LocString LOGIC_PORT = "Geyser Eruption Monitor";

				// Token: 0x0400B8F1 RID: 47345
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when geyser is erupting";

				// Token: 0x0400B8F2 RID: 47346
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x020029F2 RID: 10738
			public class GRAVE
			{
				// Token: 0x0400B8F3 RID: 47347
				public static LocString NAME = UI.FormatAsLink("Tasteful Memorial", "GRAVE");

				// Token: 0x0400B8F4 RID: 47348
				public static LocString DESC = "Burying dead Duplicants reduces health hazards and stress on the colony.";

				// Token: 0x0400B8F5 RID: 47349
				public static LocString EFFECT = "Provides a final resting place for deceased Duplicants.\n\nLiving Duplicants will automatically place an unburied corpse inside.";
			}

			// Token: 0x020029F3 RID: 10739
			public class HEADQUARTERS
			{
				// Token: 0x0400B8F6 RID: 47350
				public static LocString NAME = UI.FormatAsLink("Printing Pod", "HEADQUARTERS");

				// Token: 0x0400B8F7 RID: 47351
				public static LocString DESC = "New Duplicants come out here, but thank goodness, they never go back in.";

				// Token: 0x0400B8F8 RID: 47352
				public static LocString EFFECT = "An exceptionally advanced bioprinter of unknown origin.\n\nIt periodically produces new Duplicants or care packages containing resources.";
			}

			// Token: 0x020029F4 RID: 10740
			public class HYDROGENGENERATOR
			{
				// Token: 0x0400B8F9 RID: 47353
				public static LocString NAME = UI.FormatAsLink("Hydrogen Generator", "HYDROGENGENERATOR");

				// Token: 0x0400B8FA RID: 47354
				public static LocString DESC = "Hydrogen generators are extremely efficient, emitting next to no waste.";

				// Token: 0x0400B8FB RID: 47355
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Hydrogen Gas", "HYDROGEN"),
					" into electrical ",
					UI.FormatAsLink("Power", "POWER"),
					"."
				});
			}

			// Token: 0x020029F5 RID: 10741
			public class METHANEGENERATOR
			{
				// Token: 0x0400B8FC RID: 47356
				public static LocString NAME = UI.FormatAsLink("Natural Gas Generator", "METHANEGENERATOR");

				// Token: 0x0400B8FD RID: 47357
				public static LocString DESC = "Natural gas generators leak polluted water and are best built above a waste reservoir.";

				// Token: 0x0400B8FE RID: 47358
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Natural Gas", "METHANE"),
					" into electrical ",
					UI.FormatAsLink("Power", "POWER"),
					".\n\nProduces ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					" and ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					"."
				});
			}

			// Token: 0x020029F6 RID: 10742
			public class NUCLEARREACTOR
			{
				// Token: 0x0400B8FF RID: 47359
				public static LocString NAME = UI.FormatAsLink("Research Reactor", "NUCLEARREACTOR");

				// Token: 0x0400B900 RID: 47360
				public static LocString DESC = "Radbolt generators and reflectors make radiation useable by other buildings.";

				// Token: 0x0400B901 RID: 47361
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM"),
					" to produce ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					" for Radbolt production.\n\nGenerates a massive amount of ",
					UI.FormatAsLink("Heat", "HEAT"),
					". Overheating will result in an explosive meltdown."
				});

				// Token: 0x0400B902 RID: 47362
				public static LocString LOGIC_PORT = "Fuel Delivery Control";

				// Token: 0x0400B903 RID: 47363
				public static LocString INPUT_PORT_ACTIVE = "Fuel Delivery Enabled";

				// Token: 0x0400B904 RID: 47364
				public static LocString INPUT_PORT_INACTIVE = "Fuel Delivery Disabled";
			}

			// Token: 0x020029F7 RID: 10743
			public class WOODGASGENERATOR
			{
				// Token: 0x0400B905 RID: 47365
				public static LocString NAME = UI.FormatAsLink("Wood Burner", "WOODGASGENERATOR");

				// Token: 0x0400B906 RID: 47366
				public static LocString DESC = "Wood burners are small and easy to maintain, but produce a fair amount of heat.";

				// Token: 0x0400B907 RID: 47367
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Burns ",
					UI.FormatAsLink("Wood", "WOOD"),
					" to produce electrical ",
					UI.FormatAsLink("Power", "POWER"),
					".\n\nProduces ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					" and ",
					UI.FormatAsLink("Heat", "HEAT"),
					"."
				});
			}

			// Token: 0x020029F8 RID: 10744
			public class PEATGENERATOR
			{
				// Token: 0x0400B908 RID: 47368
				public static LocString NAME = UI.FormatAsLink("Peat Burner", "PEATGENERATOR");

				// Token: 0x0400B909 RID: 47369
				public static LocString DESC = "It gives off an aroma that some Duplicants find inexplicably nostalgic.";

				// Token: 0x0400B90A RID: 47370
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Burns ",
					UI.FormatAsLink("Peat", "PEAT"),
					" to produce electrical ",
					UI.FormatAsLink("Power", "POWER"),
					".\n\nProduces a small amount of ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					" and ",
					ELEMENTS.DIRTYWATER.NAME,
					"."
				});
			}

			// Token: 0x020029F9 RID: 10745
			public class PETROLEUMGENERATOR
			{
				// Token: 0x0400B90B RID: 47371
				public static LocString NAME = UI.FormatAsLink("Petroleum Generator", "PETROLEUMGENERATOR");

				// Token: 0x0400B90C RID: 47372
				public static LocString DESC = "Petroleum generators have a high energy output but produce a great deal of waste.";

				// Token: 0x0400B90D RID: 47373
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Petroleum", "PETROLEUM"),
					", ",
					UI.FormatAsLink("Ethanol", "ETHANOL"),
					" or ",
					UI.FormatAsLink("Biodiesel", "REFINEDLIPID"),
					" into electrical ",
					UI.FormatAsLink("Power", "POWER"),
					".\n\nProduces ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					" and ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					"."
				});
			}

			// Token: 0x020029FA RID: 10746
			public class HYDROPONICFARM
			{
				// Token: 0x0400B90E RID: 47374
				public static LocString NAME = UI.FormatAsLink("Hydroponic Farm", "HYDROPONICFARM");

				// Token: 0x0400B90F RID: 47375
				public static LocString DESC = "Hydroponic farms reduce Duplicant traffic by automating irrigating crops.";

				// Token: 0x0400B910 RID: 47376
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Grows one ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" from a ",
					UI.FormatAsLink("Seed", "PLANTS"),
					".\n\nCan be used as floor tile and rotated before construction.\n\nMust be irrigated through ",
					UI.FormatAsLink("Liquid Piping", "LIQUIDPIPING"),
					"."
				});
			}

			// Token: 0x020029FB RID: 10747
			public class INSULATEDGASCONDUIT
			{
				// Token: 0x0400B911 RID: 47377
				public static LocString NAME = UI.FormatAsLink("Insulated Gas Pipe", "INSULATEDGASCONDUIT");

				// Token: 0x0400B912 RID: 47378
				public static LocString DESC = "Pipe insulation prevents gas contents from significantly changing temperature in transit.";

				// Token: 0x0400B913 RID: 47379
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" with minimal change in ",
					UI.FormatAsLink("Temperature", "HEAT"),
					".\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x020029FC RID: 10748
			public class GASCONDUITRADIANT
			{
				// Token: 0x0400B914 RID: 47380
				public static LocString NAME = UI.FormatAsLink("Radiant Gas Pipe", "GASCONDUITRADIANT");

				// Token: 0x0400B915 RID: 47381
				public static LocString DESC = "Radiant pipes pumping cold gas can be run through hot areas to help cool them down.";

				// Token: 0x0400B916 RID: 47382
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					", allowing extreme ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" exchange with the surrounding environment.\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x020029FD RID: 10749
			public class INSULATEDLIQUIDCONDUIT
			{
				// Token: 0x0400B917 RID: 47383
				public static LocString NAME = UI.FormatAsLink("Insulated Liquid Pipe", "INSULATEDLIQUIDCONDUIT");

				// Token: 0x0400B918 RID: 47384
				public static LocString DESC = "Pipe insulation prevents liquid contents from significantly changing temperature in transit.";

				// Token: 0x0400B919 RID: 47385
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" with minimal change in ",
					UI.FormatAsLink("Temperature", "HEAT"),
					".\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x020029FE RID: 10750
			public class LIQUIDCONDUITRADIANT
			{
				// Token: 0x0400B91A RID: 47386
				public static LocString NAME = UI.FormatAsLink("Radiant Liquid Pipe", "LIQUIDCONDUITRADIANT");

				// Token: 0x0400B91B RID: 47387
				public static LocString DESC = "Radiant pipes pumping cold liquid can be run through hot areas to help cool them down.";

				// Token: 0x0400B91C RID: 47388
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					", allowing extreme ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" exchange with the surrounding environment.\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x020029FF RID: 10751
			public class CONTACTCONDUCTIVEPIPEBRIDGE
			{
				// Token: 0x0400B91D RID: 47389
				public static LocString NAME = UI.FormatAsLink("Conduction Panel", "CONTACTCONDUCTIVEPIPEBRIDGE");

				// Token: 0x0400B91E RID: 47390
				public static LocString DESC = "It can transfer heat effectively even if no liquid is passing through.";

				// Token: 0x0400B91F RID: 47391
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					", allowing extreme ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" exchange with overlapping buildings.\n\nCan function in a vacuum.\n\nCan be run through wall and floor tiles."
				});
			}

			// Token: 0x02002A00 RID: 10752
			public class INSULATEDWIRE
			{
				// Token: 0x0400B920 RID: 47392
				public static LocString NAME = UI.FormatAsLink("Insulated Wire", "INSULATEDWIRE");

				// Token: 0x0400B921 RID: 47393
				public static LocString DESC = "This stuff won't go melting if things get heated.";

				// Token: 0x0400B922 RID: 47394
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Connects buildings to ",
					UI.FormatAsLink("Power", "POWER"),
					" sources in extreme ",
					UI.FormatAsLink("Heat", "HEAT"),
					".\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x02002A01 RID: 10753
			public class INSULATIONTILE
			{
				// Token: 0x0400B923 RID: 47395
				public static LocString NAME = UI.FormatAsLink("Insulated Tile", "INSULATIONTILE");

				// Token: 0x0400B924 RID: 47396
				public static LocString DESC = "The low thermal conductivity of insulated tiles slows any heat passing through them.";

				// Token: 0x0400B925 RID: 47397
				public static LocString EFFECT = "Used to build the walls and floors of rooms.\n\nReduces " + UI.FormatAsLink("Heat", "HEAT") + " transfer between walls, retaining ambient heat in an area.";
			}

			// Token: 0x02002A02 RID: 10754
			public class EXTERIORWALL
			{
				// Token: 0x0400B926 RID: 47398
				public static LocString NAME = UI.FormatAsLink("Drywall", "EXTERIORWALL");

				// Token: 0x0400B927 RID: 47399
				public static LocString DESC = "Drywall can be used in conjunction with tiles to build airtight rooms on the surface.";

				// Token: 0x0400B928 RID: 47400
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Prevents ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" and ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" loss in space.\n\nBuilds an insulating backwall behind buildings."
				});

				// Token: 0x020038A6 RID: 14502
				public class FACADES
				{
					// Token: 0x02003C4C RID: 15436
					public class DEFAULT_EXTERIORWALL
					{
						// Token: 0x0400EDD0 RID: 60880
						public static LocString NAME = UI.FormatAsLink("Drywall", "EXTERIORWALL");

						// Token: 0x0400EDD1 RID: 60881
						public static LocString DESC = "It gets the job done.";
					}

					// Token: 0x02003C4D RID: 15437
					public class BALM_LILY
					{
						// Token: 0x0400EDD2 RID: 60882
						public static LocString NAME = UI.FormatAsLink("Balm Lily Print", "EXTERIORWALL");

						// Token: 0x0400EDD3 RID: 60883
						public static LocString DESC = "A mellow floral wallpaper.";
					}

					// Token: 0x02003C4E RID: 15438
					public class CLOUDS
					{
						// Token: 0x0400EDD4 RID: 60884
						public static LocString NAME = UI.FormatAsLink("Cloud Print", "EXTERIORWALL");

						// Token: 0x0400EDD5 RID: 60885
						public static LocString DESC = "A soft, fluffy wallpaper.";
					}

					// Token: 0x02003C4F RID: 15439
					public class MUSHBAR
					{
						// Token: 0x0400EDD6 RID: 60886
						public static LocString NAME = UI.FormatAsLink("Mush Bar Print", "EXTERIORWALL");

						// Token: 0x0400EDD7 RID: 60887
						public static LocString DESC = "A gag-inducing wallpaper.";
					}

					// Token: 0x02003C50 RID: 15440
					public class PLAID
					{
						// Token: 0x0400EDD8 RID: 60888
						public static LocString NAME = UI.FormatAsLink("Aqua Plaid Print", "EXTERIORWALL");

						// Token: 0x0400EDD9 RID: 60889
						public static LocString DESC = "A cozy flannel wallpaper.";
					}

					// Token: 0x02003C51 RID: 15441
					public class RAIN
					{
						// Token: 0x0400EDDA RID: 60890
						public static LocString NAME = UI.FormatAsLink("Rainy Print", "EXTERIORWALL");

						// Token: 0x0400EDDB RID: 60891
						public static LocString DESC = "A precipitation-themed wallpaper.";
					}

					// Token: 0x02003C52 RID: 15442
					public class AQUATICMOSAIC
					{
						// Token: 0x0400EDDC RID: 60892
						public static LocString NAME = UI.FormatAsLink("Aquatic Mosaic", "EXTERIORWALL");

						// Token: 0x0400EDDD RID: 60893
						public static LocString DESC = "A multi-hued blue wallpaper.";
					}

					// Token: 0x02003C53 RID: 15443
					public class RAINBOW
					{
						// Token: 0x0400EDDE RID: 60894
						public static LocString NAME = UI.FormatAsLink("Rainbow Stripe", "EXTERIORWALL");

						// Token: 0x0400EDDF RID: 60895
						public static LocString DESC = "A wallpaper with <i>all</i> the colors.";
					}

					// Token: 0x02003C54 RID: 15444
					public class SNOW
					{
						// Token: 0x0400EDE0 RID: 60896
						public static LocString NAME = UI.FormatAsLink("Snowflake Print", "EXTERIORWALL");

						// Token: 0x0400EDE1 RID: 60897
						public static LocString DESC = "A wallpaper as unique as my colony.";
					}

					// Token: 0x02003C55 RID: 15445
					public class SUN
					{
						// Token: 0x0400EDE2 RID: 60898
						public static LocString NAME = UI.FormatAsLink("Sunshine Print", "EXTERIORWALL");

						// Token: 0x0400EDE3 RID: 60899
						public static LocString DESC = "A UV-free wallpaper.";
					}

					// Token: 0x02003C56 RID: 15446
					public class COFFEE
					{
						// Token: 0x0400EDE4 RID: 60900
						public static LocString NAME = UI.FormatAsLink("Cafe Print", "EXTERIORWALL");

						// Token: 0x0400EDE5 RID: 60901
						public static LocString DESC = "A caffeine-themed wallpaper.";
					}

					// Token: 0x02003C57 RID: 15447
					public class PASTELPOLKA
					{
						// Token: 0x0400EDE6 RID: 60902
						public static LocString NAME = UI.FormatAsLink("Pastel Polka Print", "EXTERIORWALL");

						// Token: 0x0400EDE7 RID: 60903
						public static LocString DESC = "A soothing, dotted wallpaper.";
					}

					// Token: 0x02003C58 RID: 15448
					public class PASTELBLUE
					{
						// Token: 0x0400EDE8 RID: 60904
						public static LocString NAME = UI.FormatAsLink("Pastel Blue", "EXTERIORWALL");

						// Token: 0x0400EDE9 RID: 60905
						public static LocString DESC = "A soothing blue wallpaper.";
					}

					// Token: 0x02003C59 RID: 15449
					public class PASTELGREEN
					{
						// Token: 0x0400EDEA RID: 60906
						public static LocString NAME = UI.FormatAsLink("Pastel Green", "EXTERIORWALL");

						// Token: 0x0400EDEB RID: 60907
						public static LocString DESC = "A soothing green wallpaper.";
					}

					// Token: 0x02003C5A RID: 15450
					public class PASTELPINK
					{
						// Token: 0x0400EDEC RID: 60908
						public static LocString NAME = UI.FormatAsLink("Pastel Pink", "EXTERIORWALL");

						// Token: 0x0400EDED RID: 60909
						public static LocString DESC = "A soothing pink wallpaper.";
					}

					// Token: 0x02003C5B RID: 15451
					public class PASTELPURPLE
					{
						// Token: 0x0400EDEE RID: 60910
						public static LocString NAME = UI.FormatAsLink("Pastel Purple", "EXTERIORWALL");

						// Token: 0x0400EDEF RID: 60911
						public static LocString DESC = "A soothing purple wallpaper.";
					}

					// Token: 0x02003C5C RID: 15452
					public class PASTELYELLOW
					{
						// Token: 0x0400EDF0 RID: 60912
						public static LocString NAME = UI.FormatAsLink("Pastel Yellow", "EXTERIORWALL");

						// Token: 0x0400EDF1 RID: 60913
						public static LocString DESC = "A soothing yellow wallpaper.";
					}

					// Token: 0x02003C5D RID: 15453
					public class BASIC_WHITE
					{
						// Token: 0x0400EDF2 RID: 60914
						public static LocString NAME = UI.FormatAsLink("Fresh White", "EXTERIORWALL");

						// Token: 0x0400EDF3 RID: 60915
						public static LocString DESC = "It's just so fresh and so clean.";
					}

					// Token: 0x02003C5E RID: 15454
					public class DIAGONAL_RED_DEEP_WHITE
					{
						// Token: 0x0400EDF4 RID: 60916
						public static LocString NAME = UI.FormatAsLink("Magma Diagonal", "EXTERIORWALL");

						// Token: 0x0400EDF5 RID: 60917
						public static LocString DESC = "A red wallpaper with a diagonal stripe.";
					}

					// Token: 0x02003C5F RID: 15455
					public class DIAGONAL_ORANGE_SATSUMA_WHITE
					{
						// Token: 0x0400EDF6 RID: 60918
						public static LocString NAME = UI.FormatAsLink("Bright Diagonal", "EXTERIORWALL");

						// Token: 0x0400EDF7 RID: 60919
						public static LocString DESC = "An orange wallpaper with a diagonal stripe.";
					}

					// Token: 0x02003C60 RID: 15456
					public class DIAGONAL_YELLOW_LEMON_WHITE
					{
						// Token: 0x0400EDF8 RID: 60920
						public static LocString NAME = UI.FormatAsLink("Yellowcake Diagonal", "EXTERIORWALL");

						// Token: 0x0400EDF9 RID: 60921
						public static LocString DESC = "A radiation-free wallpaper with a diagonal stripe.";
					}

					// Token: 0x02003C61 RID: 15457
					public class DIAGONAL_GREEN_KELLY_WHITE
					{
						// Token: 0x0400EDFA RID: 60922
						public static LocString NAME = UI.FormatAsLink("Algae Diagonal", "EXTERIORWALL");

						// Token: 0x0400EDFB RID: 60923
						public static LocString DESC = "A slippery wallpaper with a diagonal stripe.";
					}

					// Token: 0x02003C62 RID: 15458
					public class DIAGONAL_BLUE_COBALT_WHITE
					{
						// Token: 0x0400EDFC RID: 60924
						public static LocString NAME = UI.FormatAsLink("H2O Diagonal", "EXTERIORWALL");

						// Token: 0x0400EDFD RID: 60925
						public static LocString DESC = "A damp wallpaper with a diagonal stripe.";
					}

					// Token: 0x02003C63 RID: 15459
					public class DIAGONAL_PINK_FLAMINGO_WHITE
					{
						// Token: 0x0400EDFE RID: 60926
						public static LocString NAME = UI.FormatAsLink("Petal Diagonal", "EXTERIORWALL");

						// Token: 0x0400EDFF RID: 60927
						public static LocString DESC = "A pink wallpaper with a diagonal stripe.";
					}

					// Token: 0x02003C64 RID: 15460
					public class DIAGONAL_GREY_CHARCOAL_WHITE
					{
						// Token: 0x0400EE00 RID: 60928
						public static LocString NAME = UI.FormatAsLink("Charcoal Diagonal", "EXTERIORWALL");

						// Token: 0x0400EE01 RID: 60929
						public static LocString DESC = "A sleek wallpaper with a diagonal stripe.";
					}

					// Token: 0x02003C65 RID: 15461
					public class CIRCLE_RED_DEEP_WHITE
					{
						// Token: 0x0400EE02 RID: 60930
						public static LocString NAME = UI.FormatAsLink("Magma Wedge", "EXTERIORWALL");

						// Token: 0x0400EE03 RID: 60931
						public static LocString DESC = "It can be arranged into giant red polka dots.";
					}

					// Token: 0x02003C66 RID: 15462
					public class CIRCLE_ORANGE_SATSUMA_WHITE
					{
						// Token: 0x0400EE04 RID: 60932
						public static LocString NAME = UI.FormatAsLink("Bright Wedge", "EXTERIORWALL");

						// Token: 0x0400EE05 RID: 60933
						public static LocString DESC = "It can be arranged into giant orange polka dots.";
					}

					// Token: 0x02003C67 RID: 15463
					public class CIRCLE_YELLOW_LEMON_WHITE
					{
						// Token: 0x0400EE06 RID: 60934
						public static LocString NAME = UI.FormatAsLink("Yellowcake Wedge", "EXTERIORWALL");

						// Token: 0x0400EE07 RID: 60935
						public static LocString DESC = "A radiation-free pattern that can be arranged into giant polka dots.";
					}

					// Token: 0x02003C68 RID: 15464
					public class CIRCLE_GREEN_KELLY_WHITE
					{
						// Token: 0x0400EE08 RID: 60936
						public static LocString NAME = UI.FormatAsLink("Algae Wedge", "EXTERIORWALL");

						// Token: 0x0400EE09 RID: 60937
						public static LocString DESC = "It can be arranged into giant green polka dots.";
					}

					// Token: 0x02003C69 RID: 15465
					public class CIRCLE_BLUE_COBALT_WHITE
					{
						// Token: 0x0400EE0A RID: 60938
						public static LocString NAME = UI.FormatAsLink("H2O Wedge", "EXTERIORWALL");

						// Token: 0x0400EE0B RID: 60939
						public static LocString DESC = "It can be arranged into giant blue polka dots.";
					}

					// Token: 0x02003C6A RID: 15466
					public class CIRCLE_PINK_FLAMINGO_WHITE
					{
						// Token: 0x0400EE0C RID: 60940
						public static LocString NAME = UI.FormatAsLink("Petal Wedge", "EXTERIORWALL");

						// Token: 0x0400EE0D RID: 60941
						public static LocString DESC = "It can be arranged into giant pink polka dots.";
					}

					// Token: 0x02003C6B RID: 15467
					public class CIRCLE_GREY_CHARCOAL_WHITE
					{
						// Token: 0x0400EE0E RID: 60942
						public static LocString NAME = UI.FormatAsLink("Charcoal Wedge", "EXTERIORWALL");

						// Token: 0x0400EE0F RID: 60943
						public static LocString DESC = "It can be arranged into giant shadowy polka dots.";
					}

					// Token: 0x02003C6C RID: 15468
					public class BASIC_BLUE_COBALT
					{
						// Token: 0x0400EE10 RID: 60944
						public static LocString NAME = UI.FormatAsLink("Solid Cobalt", "EXTERIORWALL");

						// Token: 0x0400EE11 RID: 60945
						public static LocString DESC = "It doesn't cure the blues, so much as emphasize them.";
					}

					// Token: 0x02003C6D RID: 15469
					public class BASIC_GREEN_KELLY
					{
						// Token: 0x0400EE12 RID: 60946
						public static LocString NAME = UI.FormatAsLink("Spring Green", "EXTERIORWALL");

						// Token: 0x0400EE13 RID: 60947
						public static LocString DESC = "It's cheaper than having a garden.";
					}

					// Token: 0x02003C6E RID: 15470
					public class BASIC_GREY_CHARCOAL
					{
						// Token: 0x0400EE14 RID: 60948
						public static LocString NAME = UI.FormatAsLink("Solid Charcoal", "EXTERIORWALL");

						// Token: 0x0400EE15 RID: 60949
						public static LocString DESC = "An elevated take on \"gray\".";
					}

					// Token: 0x02003C6F RID: 15471
					public class BASIC_ORANGE_SATSUMA
					{
						// Token: 0x0400EE16 RID: 60950
						public static LocString NAME = UI.FormatAsLink("Solid Satsuma", "EXTERIORWALL");

						// Token: 0x0400EE17 RID: 60951
						public static LocString DESC = "Less fruit-forward, but just as fresh.";
					}

					// Token: 0x02003C70 RID: 15472
					public class BASIC_PINK_FLAMINGO
					{
						// Token: 0x0400EE18 RID: 60952
						public static LocString NAME = UI.FormatAsLink("Solid Pink", "EXTERIORWALL");

						// Token: 0x0400EE19 RID: 60953
						public static LocString DESC = "A bold statement, for bold Duplicants.";
					}

					// Token: 0x02003C71 RID: 15473
					public class BASIC_RED_DEEP
					{
						// Token: 0x0400EE1A RID: 60954
						public static LocString NAME = UI.FormatAsLink("Chili Red", "EXTERIORWALL");

						// Token: 0x0400EE1B RID: 60955
						public static LocString DESC = "It really spices up dull walls.";
					}

					// Token: 0x02003C72 RID: 15474
					public class BASIC_YELLOW_LEMON
					{
						// Token: 0x0400EE1C RID: 60956
						public static LocString NAME = UI.FormatAsLink("Canary Yellow", "EXTERIORWALL");

						// Token: 0x0400EE1D RID: 60957
						public static LocString DESC = "The original coal-mine chic.";
					}

					// Token: 0x02003C73 RID: 15475
					public class BLUEBERRIES
					{
						// Token: 0x0400EE1E RID: 60958
						public static LocString NAME = UI.FormatAsLink("Juicy Blueberry", "EXTERIORWALL");

						// Token: 0x0400EE1F RID: 60959
						public static LocString DESC = "It stains the fingers.";
					}

					// Token: 0x02003C74 RID: 15476
					public class GRAPES
					{
						// Token: 0x0400EE20 RID: 60960
						public static LocString NAME = UI.FormatAsLink("Grape Escape", "EXTERIORWALL");

						// Token: 0x0400EE21 RID: 60961
						public static LocString DESC = "It's seedless, if that matters.";
					}

					// Token: 0x02003C75 RID: 15477
					public class LEMON
					{
						// Token: 0x0400EE22 RID: 60962
						public static LocString NAME = UI.FormatAsLink("Sour Lemon", "EXTERIORWALL");

						// Token: 0x0400EE23 RID: 60963
						public static LocString DESC = "A bitter yet refreshing style.";
					}

					// Token: 0x02003C76 RID: 15478
					public class LIME
					{
						// Token: 0x0400EE24 RID: 60964
						public static LocString NAME = UI.FormatAsLink("Juicy Lime", "EXTERIORWALL");

						// Token: 0x0400EE25 RID: 60965
						public static LocString DESC = "Contains no actual vitamin C.";
					}

					// Token: 0x02003C77 RID: 15479
					public class SATSUMA
					{
						// Token: 0x0400EE26 RID: 60966
						public static LocString NAME = UI.FormatAsLink("Satsuma Slice", "EXTERIORWALL");

						// Token: 0x0400EE27 RID: 60967
						public static LocString DESC = "Adds some much-needed zest to the room.";
					}

					// Token: 0x02003C78 RID: 15480
					public class STRAWBERRY
					{
						// Token: 0x0400EE28 RID: 60968
						public static LocString NAME = UI.FormatAsLink("Strawberry Speckle", "EXTERIORWALL");

						// Token: 0x0400EE29 RID: 60969
						public static LocString DESC = "Fruity freckles for naturally sweet spaces.";
					}

					// Token: 0x02003C79 RID: 15481
					public class WATERMELON
					{
						// Token: 0x0400EE2A RID: 60970
						public static LocString NAME = UI.FormatAsLink("Juicy Watermelon", "EXTERIORWALL");

						// Token: 0x0400EE2B RID: 60971
						public static LocString DESC = "Far more practical than gluing real fruit on a wall.";
					}

					// Token: 0x02003C7A RID: 15482
					public class TROPICAL
					{
						// Token: 0x0400EE2C RID: 60972
						public static LocString NAME = UI.FormatAsLink("Sporechid Print", "EXTERIORWALL");

						// Token: 0x0400EE2D RID: 60973
						public static LocString DESC = "The original scratch-and-sniff version was immediately recalled.";
					}

					// Token: 0x02003C7B RID: 15483
					public class TOILETPAPER
					{
						// Token: 0x0400EE2E RID: 60974
						public static LocString NAME = UI.FormatAsLink("De-loo-xe", "EXTERIORWALL");

						// Token: 0x0400EE2F RID: 60975
						public static LocString DESC = "Softly undulating lines create an undeniable air of loo-xury.";
					}

					// Token: 0x02003C7C RID: 15484
					public class PLUNGER
					{
						// Token: 0x0400EE30 RID: 60976
						public static LocString NAME = UI.FormatAsLink("Plunger Print", "EXTERIORWALL");

						// Token: 0x0400EE31 RID: 60977
						public static LocString DESC = "Unclogs one's creative impulses.";
					}

					// Token: 0x02003C7D RID: 15485
					public class STRIPES_BLUE
					{
						// Token: 0x0400EE32 RID: 60978
						public static LocString NAME = UI.FormatAsLink("Blue Awning Stripe", "EXTERIORWALL");

						// Token: 0x0400EE33 RID: 60979
						public static LocString DESC = "Thick stripes in alternating shades of blue.";
					}

					// Token: 0x02003C7E RID: 15486
					public class STRIPES_DIAGONAL_BLUE
					{
						// Token: 0x0400EE34 RID: 60980
						public static LocString NAME = UI.FormatAsLink("Blue Regimental Stripe", "EXTERIORWALL");

						// Token: 0x0400EE35 RID: 60981
						public static LocString DESC = "Inspired by the ties worn during intraoffice sports.";
					}

					// Token: 0x02003C7F RID: 15487
					public class STRIPES_CIRCLE_BLUE
					{
						// Token: 0x0400EE36 RID: 60982
						public static LocString NAME = UI.FormatAsLink("Blue Circle Stripe", "EXTERIORWALL");

						// Token: 0x0400EE37 RID: 60983
						public static LocString DESC = "A stripe that curves to the right.";
					}

					// Token: 0x02003C80 RID: 15488
					public class SQUARES_RED_DEEP_WHITE
					{
						// Token: 0x0400EE38 RID: 60984
						public static LocString NAME = UI.FormatAsLink("Magma Checkers", "EXTERIORWALL");

						// Token: 0x0400EE39 RID: 60985
						public static LocString DESC = "They're so hot right now!";
					}

					// Token: 0x02003C81 RID: 15489
					public class SQUARES_ORANGE_SATSUMA_WHITE
					{
						// Token: 0x0400EE3A RID: 60986
						public static LocString NAME = UI.FormatAsLink("Bright Checkers", "EXTERIORWALL");

						// Token: 0x0400EE3B RID: 60987
						public static LocString DESC = "Every tile feels like four tiles!";
					}

					// Token: 0x02003C82 RID: 15490
					public class SQUARES_YELLOW_LEMON_WHITE
					{
						// Token: 0x0400EE3C RID: 60988
						public static LocString NAME = UI.FormatAsLink("Yellowcake Checkers", "EXTERIORWALL");

						// Token: 0x0400EE3D RID: 60989
						public static LocString DESC = "Any brighter, and they'd be radioactive!";
					}

					// Token: 0x02003C83 RID: 15491
					public class SQUARES_GREEN_KELLY_WHITE
					{
						// Token: 0x0400EE3E RID: 60990
						public static LocString NAME = UI.FormatAsLink("Algae Checkers", "EXTERIORWALL");

						// Token: 0x0400EE3F RID: 60991
						public static LocString DESC = "Now with real simulated algae color!";
					}

					// Token: 0x02003C84 RID: 15492
					public class SQUARES_BLUE_COBALT_WHITE
					{
						// Token: 0x0400EE40 RID: 60992
						public static LocString NAME = UI.FormatAsLink("H2O Checkers", "EXTERIORWALL");

						// Token: 0x0400EE41 RID: 60993
						public static LocString DESC = "Drink it all in!";
					}

					// Token: 0x02003C85 RID: 15493
					public class SQUARES_PINK_FLAMINGO_WHITE
					{
						// Token: 0x0400EE42 RID: 60994
						public static LocString NAME = UI.FormatAsLink("Petal Checkers", "EXTERIORWALL");

						// Token: 0x0400EE43 RID: 60995
						public static LocString DESC = "Fiercely fluorescent floral-inspired pink!";
					}

					// Token: 0x02003C86 RID: 15494
					public class SQUARES_GREY_CHARCOAL_WHITE
					{
						// Token: 0x0400EE44 RID: 60996
						public static LocString NAME = UI.FormatAsLink("Charcoal Checkers", "EXTERIORWALL");

						// Token: 0x0400EE45 RID: 60997
						public static LocString DESC = "So retro!";
					}

					// Token: 0x02003C87 RID: 15495
					public class KITCHEN_RETRO1
					{
						// Token: 0x0400EE46 RID: 60998
						public static LocString NAME = UI.FormatAsLink("Cafeteria Kitsch", "EXTERIORWALL");

						// Token: 0x0400EE47 RID: 60999
						public static LocString DESC = "Some diners find it nostalgic.";
					}

					// Token: 0x02003C88 RID: 15496
					public class PLUS_RED_DEEP_WHITE
					{
						// Token: 0x0400EE48 RID: 61000
						public static LocString NAME = UI.FormatAsLink("Digital Chili", "EXTERIORWALL");

						// Token: 0x0400EE49 RID: 61001
						public static LocString DESC = "A pixelated red-and-white print.";
					}

					// Token: 0x02003C89 RID: 15497
					public class PLUS_ORANGE_SATSUMA_WHITE
					{
						// Token: 0x0400EE4A RID: 61002
						public static LocString NAME = UI.FormatAsLink("Digital Satsuma", "EXTERIORWALL");

						// Token: 0x0400EE4B RID: 61003
						public static LocString DESC = "A pixelated orange-and-white print.";
					}

					// Token: 0x02003C8A RID: 15498
					public class PLUS_YELLOW_LEMON_WHITE
					{
						// Token: 0x0400EE4C RID: 61004
						public static LocString NAME = UI.FormatAsLink("Digital Lemon", "EXTERIORWALL");

						// Token: 0x0400EE4D RID: 61005
						public static LocString DESC = "A pixelated yellow-and-white print.";
					}

					// Token: 0x02003C8B RID: 15499
					public class PLUS_GREEN_KELLY_WHITE
					{
						// Token: 0x0400EE4E RID: 61006
						public static LocString NAME = UI.FormatAsLink("Digital Lawn", "EXTERIORWALL");

						// Token: 0x0400EE4F RID: 61007
						public static LocString DESC = "A pixelated green-and-white print.";
					}

					// Token: 0x02003C8C RID: 15500
					public class PLUS_BLUE_COBALT_WHITE
					{
						// Token: 0x0400EE50 RID: 61008
						public static LocString NAME = UI.FormatAsLink("Digital Cobalt", "EXTERIORWALL");

						// Token: 0x0400EE51 RID: 61009
						public static LocString DESC = "A pixelated blue-and-white print.";
					}

					// Token: 0x02003C8D RID: 15501
					public class PLUS_PINK_FLAMINGO_WHITE
					{
						// Token: 0x0400EE52 RID: 61010
						public static LocString NAME = UI.FormatAsLink("Digital Pink", "EXTERIORWALL");

						// Token: 0x0400EE53 RID: 61011
						public static LocString DESC = "A pixelated pink-and-white print.";
					}

					// Token: 0x02003C8E RID: 15502
					public class PLUS_GREY_CHARCOAL_WHITE
					{
						// Token: 0x0400EE54 RID: 61012
						public static LocString NAME = UI.FormatAsLink("Digital Charcoal", "EXTERIORWALL");

						// Token: 0x0400EE55 RID: 61013
						public static LocString DESC = "It's futuristic, so it must be good.";
					}

					// Token: 0x02003C8F RID: 15503
					public class STRIPES_ROSE
					{
						// Token: 0x0400EE56 RID: 61014
						public static LocString NAME = UI.FormatAsLink("Puce Stripe", "EXTERIORWALL");

						// Token: 0x0400EE57 RID: 61015
						public static LocString DESC = "Vertical stripes make it quite obvious when nearby objects are askew.";
					}

					// Token: 0x02003C90 RID: 15504
					public class STRIPES_DIAGONAL_ROSE
					{
						// Token: 0x0400EE58 RID: 61016
						public static LocString NAME = UI.FormatAsLink("Puce Diagonal", "EXTERIORWALL");

						// Token: 0x0400EE59 RID: 61017
						public static LocString DESC = "Some describe this color as \"squashed bug.\"";
					}

					// Token: 0x02003C91 RID: 15505
					public class STRIPES_CIRCLE_ROSE
					{
						// Token: 0x0400EE5A RID: 61018
						public static LocString NAME = UI.FormatAsLink("Puce Curves", "EXTERIORWALL");

						// Token: 0x0400EE5B RID: 61019
						public static LocString DESC = "It's pronounced \"peeyoo-ss,\" a sound that Duplicants just can't seem to reproduce.";
					}

					// Token: 0x02003C92 RID: 15506
					public class STRIPES_MUSH
					{
						// Token: 0x0400EE5C RID: 61020
						public static LocString NAME = UI.FormatAsLink("Mush Stripe", "EXTERIORWALL");

						// Token: 0x0400EE5D RID: 61021
						public static LocString DESC = "The kind of green that makes one feel slightly nauseated.";
					}

					// Token: 0x02003C93 RID: 15507
					public class STRIPES_DIAGONAL_MUSH
					{
						// Token: 0x0400EE5E RID: 61022
						public static LocString NAME = UI.FormatAsLink("Mush Diagonal", "EXTERIORWALL");

						// Token: 0x0400EE5F RID: 61023
						public static LocString DESC = "Diagonal stripes in alternating shades of mush bar.";
					}

					// Token: 0x02003C94 RID: 15508
					public class STRIPES_CIRCLE_MUSH
					{
						// Token: 0x0400EE60 RID: 61024
						public static LocString NAME = UI.FormatAsLink("Mush Curves", "EXTERIORWALL");

						// Token: 0x0400EE61 RID: 61025
						public static LocString DESC = "This wallpaper, like this colony's journey, is full of twists and turns.";
					}

					// Token: 0x02003C95 RID: 15509
					public class STRIPES_YELLOW_TARTAR
					{
						// Token: 0x0400EE62 RID: 61026
						public static LocString NAME = UI.FormatAsLink("Ick Stripe", "EXTERIORWALL");

						// Token: 0x0400EE63 RID: 61027
						public static LocString DESC = "Vertical stripes make it quite obvious when nearby objects are askew.";
					}

					// Token: 0x02003C96 RID: 15510
					public class STRIPES_DIAGONAL_YELLOW_TARTAR
					{
						// Token: 0x0400EE64 RID: 61028
						public static LocString NAME = UI.FormatAsLink("Ick Diagonal", "EXTERIORWALL");

						// Token: 0x0400EE65 RID: 61029
						public static LocString DESC = "Diagonal stripes in alternating shades of yellow.";
					}

					// Token: 0x02003C97 RID: 15511
					public class STRIPES_CIRCLE_YELLOW_TARTAR
					{
						// Token: 0x0400EE66 RID: 61030
						public static LocString NAME = UI.FormatAsLink("Ick Curves", "EXTERIORWALL");

						// Token: 0x0400EE67 RID: 61031
						public static LocString DESC = "This wallpaper, like this colony's journey, is full of twists and turns.";
					}

					// Token: 0x02003C98 RID: 15512
					public class STRIPES_PURPLE_BRAINFAT
					{
						// Token: 0x0400EE68 RID: 61032
						public static LocString NAME = UI.FormatAsLink("Fainting Stripe", "EXTERIORWALL");

						// Token: 0x0400EE69 RID: 61033
						public static LocString DESC = "Vertical stripes make it quite obvious when nearby objects are askew.";
					}

					// Token: 0x02003C99 RID: 15513
					public class STRIPES_DIAGONAL_PURPLE_BRAINFAT
					{
						// Token: 0x0400EE6A RID: 61034
						public static LocString NAME = UI.FormatAsLink("Fainting Diagonal", "EXTERIORWALL");

						// Token: 0x0400EE6B RID: 61035
						public static LocString DESC = "Diagonal stripes in alternating shades of purple.";
					}

					// Token: 0x02003C9A RID: 15514
					public class STRIPES_CIRCLE_PURPLE_BRAINFAT
					{
						// Token: 0x0400EE6C RID: 61036
						public static LocString NAME = UI.FormatAsLink("Fainting Curves", "EXTERIORWALL");

						// Token: 0x0400EE6D RID: 61037
						public static LocString DESC = "This wallpaper, like this colony's journey, is full of twists and turns.";
					}

					// Token: 0x02003C9B RID: 15515
					public class FLOPPY_AZULENE_VITRO
					{
						// Token: 0x0400EE6E RID: 61038
						public static LocString NAME = UI.FormatAsLink("Waterlogged Databank", "EXTERIORWALL");

						// Token: 0x0400EE6F RID: 61039
						public static LocString DESC = "A fun blue print in honor of information storage.";
					}

					// Token: 0x02003C9C RID: 15516
					public class FLOPPY_BLACK_WHITE
					{
						// Token: 0x0400EE70 RID: 61040
						public static LocString NAME = UI.FormatAsLink("Monochrome Databank", "EXTERIORWALL");

						// Token: 0x0400EE71 RID: 61041
						public static LocString DESC = "A chic black-and-white print in honor of information storage.";
					}

					// Token: 0x02003C9D RID: 15517
					public class FLOPPY_PEAGREEN_BALMY
					{
						// Token: 0x0400EE72 RID: 61042
						public static LocString NAME = UI.FormatAsLink("Lush Databank", "EXTERIORWALL");

						// Token: 0x0400EE73 RID: 61043
						public static LocString DESC = "A fun green print in honor of information storage.";
					}

					// Token: 0x02003C9E RID: 15518
					public class FLOPPY_SATSUMA_YELLOWCAKE
					{
						// Token: 0x0400EE74 RID: 61044
						public static LocString NAME = UI.FormatAsLink("Hi-Vis Databank", "EXTERIORWALL");

						// Token: 0x0400EE75 RID: 61045
						public static LocString DESC = "A fun orange print in honor of information storage.";
					}

					// Token: 0x02003C9F RID: 15519
					public class FLOPPY_MAGMA_AMINO
					{
						// Token: 0x0400EE76 RID: 61046
						public static LocString NAME = UI.FormatAsLink("Flashy Databank", "EXTERIORWALL");

						// Token: 0x0400EE77 RID: 61047
						public static LocString DESC = "A fun red print in honor of information storage.";
					}

					// Token: 0x02003CA0 RID: 15520
					public class ORANGE_JUICE
					{
						// Token: 0x0400EE78 RID: 61048
						public static LocString NAME = UI.FormatAsLink("Infinite Spill", "EXTERIORWALL");

						// Token: 0x0400EE79 RID: 61049
						public static LocString DESC = "If the liquids never hit the floor, is it really a spill?";
					}

					// Token: 0x02003CA1 RID: 15521
					public class PAINT_BLOTS
					{
						// Token: 0x0400EE7A RID: 61050
						public static LocString NAME = UI.FormatAsLink("Happy Accidents", "EXTERIORWALL");

						// Token: 0x0400EE7B RID: 61051
						public static LocString DESC = "There are no mistakes, only cheerful little splotches.";
					}

					// Token: 0x02003CA2 RID: 15522
					public class TELESCOPE
					{
						// Token: 0x0400EE7C RID: 61052
						public static LocString NAME = UI.FormatAsLink("Telescope Print", "EXTERIORWALL");

						// Token: 0x0400EE7D RID: 61053
						public static LocString DESC = "The perfect wallpaper for skygazers.";
					}

					// Token: 0x02003CA3 RID: 15523
					public class TICTACTOE_O
					{
						// Token: 0x0400EE7E RID: 61054
						public static LocString NAME = UI.FormatAsLink("TicTacToe O", "EXTERIORWALL");

						// Token: 0x0400EE7F RID: 61055
						public static LocString DESC = "A crisp black 'O' on a clean white background. Ideal for monochromatic games rooms.";
					}

					// Token: 0x02003CA4 RID: 15524
					public class TICTACTOE_X
					{
						// Token: 0x0400EE80 RID: 61056
						public static LocString NAME = UI.FormatAsLink("TicTacToe X", "EXTERIORWALL");

						// Token: 0x0400EE81 RID: 61057
						public static LocString DESC = "A crisp black 'X' on a clean white background. Ideal for monochromatic games rooms.";
					}

					// Token: 0x02003CA5 RID: 15525
					public class DICE_1
					{
						// Token: 0x0400EE82 RID: 61058
						public static LocString NAME = UI.FormatAsLink("Roll One", "EXTERIORWALL");

						// Token: 0x0400EE83 RID: 61059
						public static LocString DESC = "Inspired by classic dice.";
					}

					// Token: 0x02003CA6 RID: 15526
					public class DICE_2
					{
						// Token: 0x0400EE84 RID: 61060
						public static LocString NAME = UI.FormatAsLink("Roll Two", "EXTERIORWALL");

						// Token: 0x0400EE85 RID: 61061
						public static LocString DESC = "Inspired by classic dice.";
					}

					// Token: 0x02003CA7 RID: 15527
					public class DICE_3
					{
						// Token: 0x0400EE86 RID: 61062
						public static LocString NAME = UI.FormatAsLink("Roll Three", "EXTERIORWALL");

						// Token: 0x0400EE87 RID: 61063
						public static LocString DESC = "Inspired by classic dice.";
					}

					// Token: 0x02003CA8 RID: 15528
					public class DICE_4
					{
						// Token: 0x0400EE88 RID: 61064
						public static LocString NAME = UI.FormatAsLink("Roll Four", "EXTERIORWALL");

						// Token: 0x0400EE89 RID: 61065
						public static LocString DESC = "Inspired by classic dice.";
					}

					// Token: 0x02003CA9 RID: 15529
					public class DICE_5
					{
						// Token: 0x0400EE8A RID: 61066
						public static LocString NAME = UI.FormatAsLink("Roll Five", "EXTERIORWALL");

						// Token: 0x0400EE8B RID: 61067
						public static LocString DESC = "Inspired by classic dice.";
					}

					// Token: 0x02003CAA RID: 15530
					public class DICE_6
					{
						// Token: 0x0400EE8C RID: 61068
						public static LocString NAME = UI.FormatAsLink("High Roller", "EXTERIORWALL");

						// Token: 0x0400EE8D RID: 61069
						public static LocString DESC = "Inspired by classic dice.";
					}
				}
			}

			// Token: 0x02002A03 RID: 10755
			public class FARMTILE
			{
				// Token: 0x0400B929 RID: 47401
				public static LocString NAME = UI.FormatAsLink("Farm Tile", "FARMTILE");

				// Token: 0x0400B92A RID: 47402
				public static LocString DESC = "Duplicants can deliver fertilizer and liquids to farm tiles, accelerating plant growth.";

				// Token: 0x0400B92B RID: 47403
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Grows one ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" from a ",
					UI.FormatAsLink("Seed", "PLANTS"),
					".\n\nCan be used as floor tile and rotated before construction."
				});
			}

			// Token: 0x02002A04 RID: 10756
			public class LADDER
			{
				// Token: 0x0400B92C RID: 47404
				public static LocString NAME = UI.FormatAsLink("Ladder", "LADDER");

				// Token: 0x0400B92D RID: 47405
				public static LocString DESC = "(That means they climb it.)";

				// Token: 0x0400B92E RID: 47406
				public static LocString EFFECT = "Enables vertical mobility for Duplicants.";
			}

			// Token: 0x02002A05 RID: 10757
			public class LADDERFAST
			{
				// Token: 0x0400B92F RID: 47407
				public static LocString NAME = UI.FormatAsLink("Plastic Ladder", "LADDERFAST");

				// Token: 0x0400B930 RID: 47408
				public static LocString DESC = "Plastic ladders are mildly antiseptic and can help limit the spread of germs in a colony.";

				// Token: 0x0400B931 RID: 47409
				public static LocString EFFECT = "Increases Duplicant climbing speed.";
			}

			// Token: 0x02002A06 RID: 10758
			public class LIQUIDCONDUIT
			{
				// Token: 0x0400B932 RID: 47410
				public static LocString NAME = UI.FormatAsLink("Liquid Pipe", "LIQUIDCONDUIT");

				// Token: 0x0400B933 RID: 47411
				public static LocString DESC = "Liquid pipes are used to connect the inputs and outputs of plumbed buildings.";

				// Token: 0x0400B934 RID: 47412
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" between ",
					UI.FormatAsLink("Outputs", "LIQUIDPIPING"),
					" and ",
					UI.FormatAsLink("Intakes", "LIQUIDPIPING"),
					".\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x02002A07 RID: 10759
			public class LIQUIDCONDUITBRIDGE
			{
				// Token: 0x0400B935 RID: 47413
				public static LocString NAME = UI.FormatAsLink("Liquid Bridge", "LIQUIDCONDUITBRIDGE");

				// Token: 0x0400B936 RID: 47414
				public static LocString DESC = "Separate pipe systems help prevent building damage caused by mingled pipe contents.";

				// Token: 0x0400B937 RID: 47415
				public static LocString EFFECT = "Runs one " + UI.FormatAsLink("Liquid Pipe", "LIQUIDPIPING") + " section over another without joining them.\n\nCan be run through wall and floor tile.";
			}

			// Token: 0x02002A08 RID: 10760
			public class ICECOOLEDFAN
			{
				// Token: 0x0400B938 RID: 47416
				public static LocString NAME = UI.FormatAsLink("Ice-E Fan", "ICECOOLEDFAN");

				// Token: 0x0400B939 RID: 47417
				public static LocString DESC = "A Duplicant can work an Ice-E fan to temporarily cool small areas as needed.";

				// Token: 0x0400B93A RID: 47418
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Ice", "ICEORE"),
					" to dissipate a small amount of the ",
					UI.FormatAsLink("Heat", "HEAT"),
					"."
				});
			}

			// Token: 0x02002A09 RID: 10761
			public class ICEMACHINE
			{
				// Token: 0x0400B93B RID: 47419
				public static LocString NAME = UI.FormatAsLink("Ice Maker", "ICEMACHINE");

				// Token: 0x0400B93C RID: 47420
				public static LocString DESC = "Ice makers can be used as a small renewable source of ice and snow.";

				// Token: 0x0400B93D RID: 47421
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Water", "WATER"),
					" into ",
					UI.FormatAsLink("Ice", "ICE"),
					" or ",
					UI.FormatAsLink("Snow", "SNOW"),
					"."
				});

				// Token: 0x020038A7 RID: 14503
				public class OPTION_TOOLTIPS
				{
					// Token: 0x0400E4A6 RID: 58534
					public static LocString ICE = "Convert " + UI.FormatAsLink("Water", "WATER") + " into " + UI.FormatAsLink("Ice", "ICE");

					// Token: 0x0400E4A7 RID: 58535
					public static LocString SNOW = "Convert " + UI.FormatAsLink("Water", "WATER") + " into " + UI.FormatAsLink("Snow", "SNOW");
				}
			}

			// Token: 0x02002A0A RID: 10762
			public class LIQUIDCOOLEDFAN
			{
				// Token: 0x0400B93E RID: 47422
				public static LocString NAME = UI.FormatAsLink("Hydrofan", "LIQUIDCOOLEDFAN");

				// Token: 0x0400B93F RID: 47423
				public static LocString DESC = "A Duplicant can work a hydrofan to temporarily cool small areas as needed.";

				// Token: 0x0400B940 RID: 47424
				public static LocString EFFECT = "Dissipates a small amount of the " + UI.FormatAsLink("Heat", "HEAT") + ".";
			}

			// Token: 0x02002A0B RID: 10763
			public class CREATURETRAP
			{
				// Token: 0x0400B941 RID: 47425
				public static LocString NAME = UI.FormatAsLink("Critter Trap", "CREATURETRAP");

				// Token: 0x0400B942 RID: 47426
				public static LocString DESC = "Critter traps cannot catch swimming or flying critters.";

				// Token: 0x0400B943 RID: 47427
				public static LocString EFFECT = "Captures a living " + UI.FormatAsLink("Critter", "CREATURES") + " for transport.\n\nSingle use.";
			}

			// Token: 0x02002A0C RID: 10764
			public class CREATUREGROUNDTRAP
			{
				// Token: 0x0400B944 RID: 47428
				public static LocString NAME = UI.FormatAsLink("Critter Trap", "CREATUREGROUNDTRAP");

				// Token: 0x0400B945 RID: 47429
				public static LocString DESC = "It's designed for land critters, but flopping fish sometimes find their way in too.";

				// Token: 0x0400B946 RID: 47430
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Captures a living ",
					UI.FormatAsLink("Critter", "CREATURES"),
					" for transport.\n\nOnly Duplicants with the ",
					UI.FormatAsLink("Critter Ranching I", "RANCHING1"),
					" skill can arm this trap. It's reusable!"
				});
			}

			// Token: 0x02002A0D RID: 10765
			public class CREATUREDELIVERYPOINT
			{
				// Token: 0x0400B947 RID: 47431
				public static LocString NAME = UI.FormatAsLink("Critter Drop-Off", "CREATUREDELIVERYPOINT");

				// Token: 0x0400B948 RID: 47432
				public static LocString DESC = "Duplicants automatically bring captured critters to these relocation points for release.";

				// Token: 0x0400B949 RID: 47433
				public static LocString EFFECT = "Releases trapped " + UI.FormatAsLink("Critters", "CREATURES") + " back into the world.\n\nCan be used multiple times.";
			}

			// Token: 0x02002A0E RID: 10766
			public class CRITTERPICKUP
			{
				// Token: 0x0400B94A RID: 47434
				public static LocString NAME = UI.FormatAsLink("Critter Pick-Up", "CRITTERPICKUP");

				// Token: 0x0400B94B RID: 47435
				public static LocString DESC = "Duplicants will automatically wrangle excess critters.";

				// Token: 0x0400B94C RID: 47436
				public static LocString EFFECT = "Ensures the prompt relocation of " + UI.FormatAsLink("Critters", "CREATURES") + " that exceed the maximum amount set.\n\nMonitoring and pick-up are limited to the specified species.";

				// Token: 0x020038A8 RID: 14504
				public class LOGIC_INPUT
				{
					// Token: 0x0400E4A8 RID: 58536
					public static LocString DESC = "Enable/Disable";

					// Token: 0x0400E4A9 RID: 58537
					public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Wrangle excess critters";

					// Token: 0x0400E4AA RID: 58538
					public static LocString LOGIC_PORT_INACTIVE = (BUILDINGS.PREFABS.CRITTERPICKUP.LOGIC_INPUT.LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Ignore excess critters");
				}
			}

			// Token: 0x02002A0F RID: 10767
			public class CRITTERDROPOFF
			{
				// Token: 0x0400B94D RID: 47437
				public static LocString NAME = UI.FormatAsLink("Critter Drop-Off", "CRITTERDROPOFF");

				// Token: 0x0400B94E RID: 47438
				public static LocString DESC = "Duplicants automatically bring captured critters to these relocation points for release.";

				// Token: 0x0400B94F RID: 47439
				public static LocString EFFECT = "Releases trapped " + UI.FormatAsLink("Critters", "CREATURES") + " back into the world.\n\nMonitoring and drop-off are limited to the specified species.";

				// Token: 0x020038A9 RID: 14505
				public class LOGIC_INPUT
				{
					// Token: 0x0400E4AB RID: 58539
					public static LocString DESC = "Enable/Disable";

					// Token: 0x0400E4AC RID: 58540
					public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Enable critter drop-off";

					// Token: 0x0400E4AD RID: 58541
					public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Disable critter drop-off";
				}
			}

			// Token: 0x02002A10 RID: 10768
			public class LIQUIDFILTER
			{
				// Token: 0x0400B950 RID: 47440
				public static LocString NAME = UI.FormatAsLink("Liquid Filter", "LIQUIDFILTER");

				// Token: 0x0400B951 RID: 47441
				public static LocString DESC = "All liquids are sent into the building's output pipe, except the liquid chosen for filtering.";

				// Token: 0x0400B952 RID: 47442
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sieves one ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" out of a mix, sending it into a dedicated ",
					UI.FormatAsLink("Filtered Output Pipe", "LIQUIDPIPING"),
					".\n\nCan only filter one liquid type at a time."
				});
			}

			// Token: 0x02002A11 RID: 10769
			public class DEVPUMPLIQUID
			{
				// Token: 0x0400B953 RID: 47443
				public static LocString NAME = "Dev Pump Liquid";

				// Token: 0x0400B954 RID: 47444
				public static LocString DESC = "Piping a pump's output to a building's intake will send liquid to that building.";

				// Token: 0x0400B955 RID: 47445
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Draws in ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" and runs it through ",
					UI.FormatAsLink("Pipes", "LIQUIDPIPING"),
					".\n\nMust be submerged in ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					"."
				});
			}

			// Token: 0x02002A12 RID: 10770
			public class LIQUIDPUMP
			{
				// Token: 0x0400B956 RID: 47446
				public static LocString NAME = UI.FormatAsLink("Liquid Pump", "LIQUIDPUMP");

				// Token: 0x0400B957 RID: 47447
				public static LocString DESC = "Piping a pump's output to a building's intake will send liquid to that building.";

				// Token: 0x0400B958 RID: 47448
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Draws in ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" and runs it through ",
					UI.FormatAsLink("Pipes", "LIQUIDPIPING"),
					".\n\nMust be submerged in ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					"."
				});
			}

			// Token: 0x02002A13 RID: 10771
			public class LIQUIDMINIPUMP
			{
				// Token: 0x0400B959 RID: 47449
				public static LocString NAME = UI.FormatAsLink("Mini Liquid Pump", "LIQUIDMINIPUMP");

				// Token: 0x0400B95A RID: 47450
				public static LocString DESC = "Mini pumps are useful for moving small quantities of liquid with minimum power.";

				// Token: 0x0400B95B RID: 47451
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Draws in a small amount of ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" and runs it through ",
					UI.FormatAsLink("Pipes", "LIQUIDPIPING"),
					".\n\nMust be submerged in ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					"."
				});
			}

			// Token: 0x02002A14 RID: 10772
			public class LIQUIDPUMPINGSTATION
			{
				// Token: 0x0400B95C RID: 47452
				public static LocString NAME = UI.FormatAsLink("Pitcher Pump", "LIQUIDPUMPINGSTATION");

				// Token: 0x0400B95D RID: 47453
				public static LocString DESC = "Pitcher pumps allow Duplicants to bottle and deliver liquids from place to place.";

				// Token: 0x0400B95E RID: 47454
				public static LocString EFFECT = "Manually pumps " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " into bottles for transport.\n\nDuplicants can only carry liquids that are bottled.";
			}

			// Token: 0x02002A15 RID: 10773
			public class LIQUIDVALVE
			{
				// Token: 0x0400B95F RID: 47455
				public static LocString NAME = UI.FormatAsLink("Liquid Valve", "LIQUIDVALVE");

				// Token: 0x0400B960 RID: 47456
				public static LocString DESC = "Valves control the amount of liquid that moves through pipes, preventing waste.";

				// Token: 0x0400B961 RID: 47457
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Controls the ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" volume permitted through ",
					UI.FormatAsLink("Pipes", "LIQUIDPIPING"),
					"."
				});
			}

			// Token: 0x02002A16 RID: 10774
			public class LIQUIDLOGICVALVE
			{
				// Token: 0x0400B962 RID: 47458
				public static LocString NAME = UI.FormatAsLink("Liquid Shutoff", "LIQUIDLOGICVALVE");

				// Token: 0x0400B963 RID: 47459
				public static LocString DESC = "Automated piping saves power and time by removing the need for Duplicant input.";

				// Token: 0x0400B964 RID: 47460
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Connects to an ",
					UI.FormatAsLink("Automation", "LOGIC"),
					" grid to automatically turn ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" flow on or off."
				});

				// Token: 0x0400B965 RID: 47461
				public static LocString LOGIC_PORT = "Open/Close";

				// Token: 0x0400B966 RID: 47462
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Allow Liquid flow";

				// Token: 0x0400B967 RID: 47463
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Prevent Liquid flow";
			}

			// Token: 0x02002A17 RID: 10775
			public class LIQUIDLIMITVALVE
			{
				// Token: 0x0400B968 RID: 47464
				public static LocString NAME = UI.FormatAsLink("Liquid Meter Valve", "LIQUIDLIMITVALVE");

				// Token: 0x0400B969 RID: 47465
				public static LocString DESC = "Meter Valves let an exact amount of liquid pass through before shutting off.";

				// Token: 0x0400B96A RID: 47466
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Connects to an ",
					UI.FormatAsLink("Automation", "LOGIC"),
					" grid to automatically turn ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" flow off when the specified amount has passed through it."
				});

				// Token: 0x0400B96B RID: 47467
				public static LocString LOGIC_PORT_OUTPUT = "Limit Reached";

				// Token: 0x0400B96C RID: 47468
				public static LocString OUTPUT_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if limit has been reached";

				// Token: 0x0400B96D RID: 47469
				public static LocString OUTPUT_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x0400B96E RID: 47470
				public static LocString LOGIC_PORT_RESET = "Reset Meter";

				// Token: 0x0400B96F RID: 47471
				public static LocString RESET_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Reset the amount";

				// Token: 0x0400B970 RID: 47472
				public static LocString RESET_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Nothing";
			}

			// Token: 0x02002A18 RID: 10776
			public class LIQUIDVENT
			{
				// Token: 0x0400B971 RID: 47473
				public static LocString NAME = UI.FormatAsLink("Liquid Vent", "LIQUIDVENT");

				// Token: 0x0400B972 RID: 47474
				public static LocString DESC = "Vents are an exit point for liquids from plumbing systems.";

				// Token: 0x0400B973 RID: 47475
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Releases ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" from ",
					UI.FormatAsLink("Liquid Pipes", "LIQUIDPIPING"),
					"."
				});
			}

			// Token: 0x02002A19 RID: 10777
			public class MANUALGENERATOR
			{
				// Token: 0x0400B974 RID: 47476
				public static LocString NAME = UI.FormatAsLink("Manual Generator", "MANUALGENERATOR");

				// Token: 0x0400B975 RID: 47477
				public static LocString DESC = "Watching Duplicants run on it is adorable... the electrical power is just an added bonus.";

				// Token: 0x0400B976 RID: 47478
				public static LocString EFFECT = "Converts manual labor into electrical " + UI.FormatAsLink("Power", "POWER") + ".";
			}

			// Token: 0x02002A1A RID: 10778
			public class MANUALPRESSUREDOOR
			{
				// Token: 0x0400B977 RID: 47479
				public static LocString NAME = UI.FormatAsLink("Manual Airlock", "MANUALPRESSUREDOOR");

				// Token: 0x0400B978 RID: 47480
				public static LocString DESC = "Airlocks can quarter off dangerous areas and prevent gases from seeping into the colony.";

				// Token: 0x0400B979 RID: 47481
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Blocks ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" and ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" flow, maintaining pressure between areas.\n\nWild ",
					UI.FormatAsLink("Critters", "CREATURES"),
					" cannot pass through doors."
				});
			}

			// Token: 0x02002A1B RID: 10779
			public class MESHTILE
			{
				// Token: 0x0400B97A RID: 47482
				public static LocString NAME = UI.FormatAsLink("Mesh Tile", "MESHTILE");

				// Token: 0x0400B97B RID: 47483
				public static LocString DESC = "Mesh tile can be used to make Duplicant pathways in areas where liquid flows.";

				// Token: 0x0400B97C RID: 47484
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Used to build the walls and floors of rooms.\n\nDoes not obstruct ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" or ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" flow."
				});
			}

			// Token: 0x02002A1C RID: 10780
			public class PLASTICTILE
			{
				// Token: 0x0400B97D RID: 47485
				public static LocString NAME = UI.FormatAsLink("Plastic Tile", "PLASTICTILE");

				// Token: 0x0400B97E RID: 47486
				public static LocString DESC = "Plastic tile is mildly antiseptic and can help limit the spread of germs in a colony.";

				// Token: 0x0400B97F RID: 47487
				public static LocString EFFECT = "Used to build the walls and floors of rooms.\n\nSignificantly increases Duplicant runspeed.";
			}

			// Token: 0x02002A1D RID: 10781
			public class GLASSTILE
			{
				// Token: 0x0400B980 RID: 47488
				public static LocString NAME = UI.FormatAsLink("Window Tile", "GLASSTILE");

				// Token: 0x0400B981 RID: 47489
				public static LocString DESC = "Window tiles provide a barrier against liquid and gas and are completely transparent.";

				// Token: 0x0400B982 RID: 47490
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Used to build the walls and floors of rooms.\n\nAllows ",
					UI.FormatAsLink("Light", "LIGHT"),
					" and ",
					UI.FormatAsLink("Decor", "DECOR"),
					" to pass through."
				});
			}

			// Token: 0x02002A1E RID: 10782
			public class METALTILE
			{
				// Token: 0x0400B983 RID: 47491
				public static LocString NAME = UI.FormatAsLink("Metal Tile", "METALTILE");

				// Token: 0x0400B984 RID: 47492
				public static LocString DESC = "Heat travels much more quickly through metal tile than other types of flooring.";

				// Token: 0x0400B985 RID: 47493
				public static LocString EFFECT = "Used to build the walls and floors of rooms.\n\nSignificantly increases Duplicant runspeed.";
			}

			// Token: 0x02002A1F RID: 10783
			public class BUNKERTILE
			{
				// Token: 0x0400B986 RID: 47494
				public static LocString NAME = UI.FormatAsLink("Bunker Tile", "BUNKERTILE");

				// Token: 0x0400B987 RID: 47495
				public static LocString DESC = "Bunker tile can build strong shelters in otherwise dangerous environments.";

				// Token: 0x0400B988 RID: 47496
				public static LocString EFFECT = "Used to build the walls and floors of rooms.\n\nCan withstand extreme pressures and impacts.";
			}

			// Token: 0x02002A20 RID: 10784
			public class STORAGETILE
			{
				// Token: 0x0400B989 RID: 47497
				public static LocString NAME = UI.FormatAsLink("Storage Tile", "STORAGETILE");

				// Token: 0x0400B98A RID: 47498
				public static LocString DESC = "Storage tiles keep selected non-edible solids out of the way.";

				// Token: 0x0400B98B RID: 47499
				public static LocString EFFECT = "Used to build the walls and floors of rooms.\n\nProvides built-in storage for small spaces.";
			}

			// Token: 0x02002A21 RID: 10785
			public class CARPETTILE
			{
				// Token: 0x0400B98C RID: 47500
				public static LocString NAME = UI.FormatAsLink("Carpeted Tile", "CARPETTILE");

				// Token: 0x0400B98D RID: 47501
				public static LocString DESC = "Soft on little Duplicant toesies.";

				// Token: 0x0400B98E RID: 47502
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Used to build the walls and floors of rooms.\n\nIncreases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});
			}

			// Token: 0x02002A22 RID: 10786
			public class MOULDINGTILE
			{
				// Token: 0x0400B98F RID: 47503
				public static LocString NAME = UI.FormatAsLink("Trimming Tile", "MOUDLINGTILE");

				// Token: 0x0400B990 RID: 47504
				public static LocString DESC = "Trimming is used as purely decorative lining for walls and structures.";

				// Token: 0x0400B991 RID: 47505
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Used to build the walls and floors of rooms.\n\nIncreases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});
			}

			// Token: 0x02002A23 RID: 10787
			public class MONUMENTBOTTOM
			{
				// Token: 0x0400B992 RID: 47506
				public static LocString NAME = UI.FormatAsLink("Monument Base", "MONUMENTBOTTOM");

				// Token: 0x0400B993 RID: 47507
				public static LocString DESC = "The base of a monument must be constructed first.";

				// Token: 0x0400B994 RID: 47508
				public static LocString EFFECT = "Builds the bottom section of a Great Monument.\n\nCan be customized.\n\nA Great Monument must be built to achieve the Colonize Imperative.";

				// Token: 0x020038AA RID: 14506
				public class FACADES
				{
					// Token: 0x02003CAB RID: 15531
					public class OPTION_A
					{
						// Token: 0x0400EE8E RID: 61070
						public static LocString NAME = "On Asteroid I";

						// Token: 0x0400EE8F RID: 61071
						public static LocString DESC = "Standing tall.";
					}

					// Token: 0x02003CAC RID: 15532
					public class OPTION_B
					{
						// Token: 0x0400EE90 RID: 61072
						public static LocString NAME = "On Asteroid II";

						// Token: 0x0400EE91 RID: 61073
						public static LocString DESC = "Standing purposefully.";
					}

					// Token: 0x02003CAD RID: 15533
					public class OPTION_C
					{
						// Token: 0x0400EE92 RID: 61074
						public static LocString NAME = "On Asteroid III";

						// Token: 0x0400EE93 RID: 61075
						public static LocString DESC = "Their knees were knockin'.";
					}

					// Token: 0x02003CAE RID: 15534
					public class OPTION_D
					{
						// Token: 0x0400EE94 RID: 61076
						public static LocString NAME = "Scientific Seat";

						// Token: 0x0400EE95 RID: 61077
						public static LocString DESC = "In celebration of science!";
					}

					// Token: 0x02003CAF RID: 15535
					public class OPTION_E
					{
						// Token: 0x0400EE96 RID: 61078
						public static LocString NAME = "On Asteroid IV";

						// Token: 0x0400EE97 RID: 61079
						public static LocString DESC = "It's a confident stance.";
					}

					// Token: 0x02003CB0 RID: 15536
					public class OPTION_F
					{
						// Token: 0x0400EE98 RID: 61080
						public static LocString NAME = "On Asteroid V";

						// Token: 0x0400EE99 RID: 61081
						public static LocString DESC = "One knee tucked toward the other.";
					}

					// Token: 0x02003CB1 RID: 15537
					public class OPTION_G
					{
						// Token: 0x0400EE9A RID: 61082
						public static LocString NAME = "On Asteroid VI";

						// Token: 0x0400EE9B RID: 61083
						public static LocString DESC = "One small step for Duplicantkind...";
					}

					// Token: 0x02003CB2 RID: 15538
					public class OPTION_H
					{
						// Token: 0x0400EE9C RID: 61084
						public static LocString NAME = "Hatch Hunter";

						// Token: 0x0400EE9D RID: 61085
						public static LocString DESC = "Atop a pair of conquered critters.";
					}

					// Token: 0x02003CB3 RID: 15539
					public class OPTION_I
					{
						// Token: 0x0400EE9E RID: 61086
						public static LocString NAME = "Trash Tranquility";

						// Token: 0x0400EE9F RID: 61087
						public static LocString DESC = "Finding peace amid the debris.";
					}

					// Token: 0x02003CB4 RID: 15540
					public class OPTION_J
					{
						// Token: 0x0400EEA0 RID: 61088
						public static LocString NAME = "Fish Stomper";

						// Token: 0x0400EEA1 RID: 61089
						public static LocString DESC = "That can't be comfortable.";
					}

					// Token: 0x02003CB5 RID: 15541
					public class OPTION_K
					{
						// Token: 0x0400EEA2 RID: 61090
						public static LocString NAME = "Egg Equanimity";

						// Token: 0x0400EEA3 RID: 61091
						public static LocString DESC = "One must give the soul time to truly hatch.";
					}

					// Token: 0x02003CB6 RID: 15542
					public class OPTION_L
					{
						// Token: 0x0400EEA4 RID: 61092
						public static LocString NAME = "Tilted Nosecone";

						// Token: 0x0400EEA5 RID: 61093
						public static LocString DESC = "A slightly unbalanced base.";
					}

					// Token: 0x02003CB7 RID: 15543
					public class OPTION_M
					{
						// Token: 0x0400EEA6 RID: 61094
						public static LocString NAME = "Sweet Seat";

						// Token: 0x0400EEA7 RID: 61095
						public static LocString DESC = "In honor of the sugar engine.";
					}

					// Token: 0x02003CB8 RID: 15544
					public class OPTION_N
					{
						// Token: 0x0400EEA8 RID: 61096
						public static LocString NAME = "CO2 Straddle";

						// Token: 0x0400EEA9 RID: 61097
						public static LocString DESC = "Riding a carbon dioxide rocket engine to glory.";
					}

					// Token: 0x02003CB9 RID: 15545
					public class OPTION_O
					{
						// Token: 0x0400EEAA RID: 61098
						public static LocString NAME = "Petroleum Pose";

						// Token: 0x0400EEAB RID: 61099
						public static LocString DESC = "Atop a small petroleum rocket engine.";
					}

					// Token: 0x02003CBA RID: 15546
					public class OPTION_P
					{
						// Token: 0x0400EEAC RID: 61100
						public static LocString NAME = "Spacefarer Stance";

						// Token: 0x0400EEAD RID: 61101
						public static LocString DESC = "Atop a solo spacefarer rocket nosecone.";
					}

					// Token: 0x02003CBB RID: 15547
					public class OPTION_Q
					{
						// Token: 0x0400EEAE RID: 61102
						public static LocString NAME = "Seat of Power";

						// Token: 0x0400EEAF RID: 61103
						public static LocString DESC = "Atop a radbolt rocket engine.";
					}

					// Token: 0x02003CBC RID: 15548
					public class OPTION_R
					{
						// Token: 0x0400EEB0 RID: 61104
						public static LocString NAME = "Sweepy I";

						// Token: 0x0400EEB1 RID: 61105
						public static LocString DESC = "Atop a sleeping Sweepy bot.";
					}

					// Token: 0x02003CBD RID: 15549
					public class OPTION_S
					{
						// Token: 0x0400EEB2 RID: 61106
						public static LocString NAME = "Sweepy II";

						// Token: 0x0400EEB3 RID: 61107
						public static LocString DESC = "Atop a curious Sweepy bot.";
					}

					// Token: 0x02003CBE RID: 15550
					public class OPTION_T
					{
						// Token: 0x0400EEB4 RID: 61108
						public static LocString NAME = "Sweepy III";

						// Token: 0x0400EEB5 RID: 61109
						public static LocString DESC = "Atop a happy lil' Sweepy bot.";
					}
				}
			}

			// Token: 0x02002A24 RID: 10788
			public class MONUMENTMIDDLE
			{
				// Token: 0x0400B995 RID: 47509
				public static LocString NAME = UI.FormatAsLink("Monument Midsection", "MONUMENTMIDDLE");

				// Token: 0x0400B996 RID: 47510
				public static LocString DESC = "Customized sections of a Great Monument can be mixed and matched.";

				// Token: 0x0400B997 RID: 47511
				public static LocString EFFECT = "Builds the middle section of a Great Monument.\n\nCan be customized.\n\nA Great Monument must be built to achieve the Colonize Imperative.";

				// Token: 0x020038AB RID: 14507
				public class FACADES
				{
					// Token: 0x02003CBF RID: 15551
					public class OPTION_A
					{
						// Token: 0x0400EEB6 RID: 61110
						public static LocString NAME = "Thumbs Up";

						// Token: 0x0400EEB7 RID: 61111
						public static LocString DESC = "Good job, sculptor!";
					}

					// Token: 0x02003CC0 RID: 15552
					public class OPTION_B
					{
						// Token: 0x0400EEB8 RID: 61112
						public static LocString NAME = "Big Wrench";

						// Token: 0x0400EEB9 RID: 61113
						public static LocString DESC = "Lefty loose-y, righty tighty.";
					}

					// Token: 0x02003CC1 RID: 15553
					public class OPTION_C
					{
						// Token: 0x0400EEBA RID: 61114
						public static LocString NAME = "Um, Excuse Me";

						// Token: 0x0400EEBB RID: 61115
						public static LocString DESC = "Celebrates uncertainty.";
					}

					// Token: 0x02003CC2 RID: 15554
					public class OPTION_D
					{
						// Token: 0x0400EEBC RID: 61116
						public static LocString NAME = "Hands on Hips";

						// Token: 0x0400EEBD RID: 61117
						public static LocString DESC = "Makes the torso seem bigger and more intimidating than it is.";
					}

					// Token: 0x02003CC3 RID: 15555
					public class OPTION_E
					{
						// Token: 0x0400EEBE RID: 61118
						public static LocString NAME = "The Shrug";

						// Token: 0x0400EEBF RID: 61119
						public static LocString DESC = "Sometimes things are good enough just as they are.";
					}

					// Token: 0x02003CC4 RID: 15556
					public class OPTION_F
					{
						// Token: 0x0400EEC0 RID: 61120
						public static LocString NAME = "You Betcha";

						// Token: 0x0400EEC1 RID: 61121
						public static LocString DESC = "The finger gun of approval.";
					}

					// Token: 0x02003CC5 RID: 15557
					public class OPTION_G
					{
						// Token: 0x0400EEC2 RID: 61122
						public static LocString NAME = "Well Hello There";

						// Token: 0x0400EEC3 RID: 61123
						public static LocString DESC = "It's quite torso-forward.";
					}

					// Token: 0x02003CC6 RID: 15558
					public class OPTION_H
					{
						// Token: 0x0400EEC4 RID: 61124
						public static LocString NAME = "Fists of Fury";

						// Token: 0x0400EEC5 RID: 61125
						public static LocString DESC = "Let 'em fly!";
					}

					// Token: 0x02003CC7 RID: 15559
					public class OPTION_I
					{
						// Token: 0x0400EEC6 RID: 61126
						public static LocString NAME = "Hatch Hug";

						// Token: 0x0400EEC7 RID: 61127
						public static LocString DESC = "Cradling a cozy critter.";
					}

					// Token: 0x02003CC8 RID: 15560
					public class OPTION_J
					{
						// Token: 0x0400EEC8 RID: 61128
						public static LocString NAME = "Casual Elegance";

						// Token: 0x0400EEC9 RID: 61129
						public static LocString DESC = "Leaning casually, with grace.";
					}

					// Token: 0x02003CC9 RID: 15561
					public class OPTION_K
					{
						// Token: 0x0400EECA RID: 61130
						public static LocString NAME = "Arms Ajar";

						// Token: 0x0400EECB RID: 61131
						public static LocString DESC = "Hands hover slightly away from the body, as though raised in wonder.";
					}

					// Token: 0x02003CCA RID: 15562
					public class OPTION_L
					{
						// Token: 0x0400EECC RID: 61132
						public static LocString NAME = "Babes in Arms I";

						// Token: 0x0400EECD RID: 61133
						public static LocString DESC = "Cradling a couple of smooth lil' critter babies.";
					}

					// Token: 0x02003CCB RID: 15563
					public class OPTION_M
					{
						// Token: 0x0400EECE RID: 61134
						public static LocString NAME = "Model Rocket";

						// Token: 0x0400EECF RID: 61135
						public static LocString DESC = "Celebrates a cosmic undertaking.";
					}

					// Token: 0x02003CCC RID: 15564
					public class OPTION_N
					{
						// Token: 0x0400EED0 RID: 61136
						public static LocString NAME = "Babes in Arms II";

						// Token: 0x0400EED1 RID: 61137
						public static LocString DESC = "An armful of chonky lil' critter babies.";
					}

					// Token: 0x02003CCD RID: 15565
					public class OPTION_O
					{
						// Token: 0x0400EED2 RID: 61138
						public static LocString NAME = "Babes in Arms III";

						// Token: 0x0400EED3 RID: 61139
						public static LocString DESC = "Embracing buggy lil' critter babies.";
					}
				}
			}

			// Token: 0x02002A25 RID: 10789
			public class MONUMENTTOP
			{
				// Token: 0x0400B998 RID: 47512
				public static LocString NAME = UI.FormatAsLink("Monument Top", "MONUMENTTOP");

				// Token: 0x0400B999 RID: 47513
				public static LocString DESC = "Building a Great Monument will declare to the universe that this hunk of rock is your own.";

				// Token: 0x0400B99A RID: 47514
				public static LocString EFFECT = "Builds the top section of a Great Monument.\n\nCan be customized.\n\nA Great Monument must be built to achieve the Colonize Imperative.";

				// Token: 0x020038AC RID: 14508
				public class FACADES
				{
					// Token: 0x02003CCE RID: 15566
					public class OPTION_A
					{
						// Token: 0x0400EED4 RID: 61140
						public static LocString NAME = "Leira Noggin";

						// Token: 0x0400EED5 RID: 61141
						public static LocString DESC = "A massive replica of Leira's smiling face.";
					}

					// Token: 0x02003CCF RID: 15567
					public class OPTION_B
					{
						// Token: 0x0400EED6 RID: 61142
						public static LocString NAME = "Gossmann Noggin";

						// Token: 0x0400EED7 RID: 61143
						public static LocString DESC = "A massive replica of Gossmann's determined gaze.";
					}

					// Token: 0x02003CD0 RID: 15568
					public class OPTION_C
					{
						// Token: 0x0400EED8 RID: 61144
						public static LocString NAME = "Puft Top";

						// Token: 0x0400EED9 RID: 61145
						public static LocString DESC = "A great-monument-sized puft.";
					}

					// Token: 0x02003CD1 RID: 15569
					public class OPTION_D
					{
						// Token: 0x0400EEDA RID: 61146
						public static LocString NAME = "Nikola Noggin";

						// Token: 0x0400EEDB RID: 61147
						public static LocString DESC = "A massive replica of Nikola's post-explosion expression.";
					}

					// Token: 0x02003CD2 RID: 15570
					public class OPTION_E
					{
						// Token: 0x0400EEDC RID: 61148
						public static LocString NAME = "Burt Noggin";

						// Token: 0x0400EEDD RID: 61149
						public static LocString DESC = "A massive replica of Burt's critter-spotting expression.";
					}

					// Token: 0x02003CD3 RID: 15571
					public class OPTION_F
					{
						// Token: 0x0400EEDE RID: 61150
						public static LocString NAME = "Rowan Noggin";

						// Token: 0x0400EEDF RID: 61151
						public static LocString DESC = "A massive replica of Rowan's serene smile.";
					}

					// Token: 0x02003CD4 RID: 15572
					public class OPTION_G
					{
						// Token: 0x0400EEE0 RID: 61152
						public static LocString NAME = "Nisbet Noggin";

						// Token: 0x0400EEE1 RID: 61153
						public static LocString DESC = "A massive replica of Nisbet when she sees someone whose name she's forgotten.";
					}

					// Token: 0x02003CD5 RID: 15573
					public class OPTION_H
					{
						// Token: 0x0400EEE2 RID: 61154
						public static LocString NAME = "Ashkan Noggin";

						// Token: 0x0400EEE3 RID: 61155
						public static LocString DESC = "A massive replica of Ashkan's fossil-discovering expression.";
					}

					// Token: 0x02003CD6 RID: 15574
					public class OPTION_I
					{
						// Token: 0x0400EEE4 RID: 61156
						public static LocString NAME = "Ren Noggin";

						// Token: 0x0400EEE5 RID: 61157
						public static LocString DESC = "A massive replica of Ren's smoochy face.";
					}

					// Token: 0x02003CD7 RID: 15575
					public class OPTION_J
					{
						// Token: 0x0400EEE6 RID: 61158
						public static LocString NAME = "Hatch Top";

						// Token: 0x0400EEE7 RID: 61159
						public static LocString DESC = "A great-monument-sized Hatch.";
					}

					// Token: 0x02003CD8 RID: 15576
					public class OPTION_K
					{
						// Token: 0x0400EEE8 RID: 61160
						public static LocString NAME = "Glossy Drecko Top";

						// Token: 0x0400EEE9 RID: 61161
						public static LocString DESC = "A great-monument-sized Glossy Drecko.";
					}

					// Token: 0x02003CD9 RID: 15577
					public class OPTION_L
					{
						// Token: 0x0400EEEA RID: 61162
						public static LocString NAME = "Shove Vole Top";

						// Token: 0x0400EEEB RID: 61163
						public static LocString DESC = "A great-monument-sized Shove Vole.";
					}

					// Token: 0x02003CDA RID: 15578
					public class OPTION_M
					{
						// Token: 0x0400EEEC RID: 61164
						public static LocString NAME = "Gassy Moo Top";

						// Token: 0x0400EEED RID: 61165
						public static LocString DESC = "A great-monument-sized Gassy Moo. Gassier and moo-ier than ever.";
					}

					// Token: 0x02003CDB RID: 15579
					public class OPTION_N
					{
						// Token: 0x0400EEEE RID: 61166
						public static LocString NAME = "Morb Top";

						// Token: 0x0400EEEF RID: 61167
						public static LocString DESC = "A great-monument-sized Morb.";
					}

					// Token: 0x02003CDC RID: 15580
					public class OPTION_O
					{
						// Token: 0x0400EEF0 RID: 61168
						public static LocString NAME = "Shine Bug Top";

						// Token: 0x0400EEF1 RID: 61169
						public static LocString DESC = "A great-monument-sized Shine Bug.";
					}

					// Token: 0x02003CDD RID: 15581
					public class OPTION_P
					{
						// Token: 0x0400EEF2 RID: 61170
						public static LocString NAME = "Slickster Top";

						// Token: 0x0400EEF3 RID: 61171
						public static LocString DESC = "A great-monument-sized Slickster.";
					}

					// Token: 0x02003CDE RID: 15582
					public class OPTION_Q
					{
						// Token: 0x0400EEF4 RID: 61172
						public static LocString NAME = "Pacu Top";

						// Token: 0x0400EEF5 RID: 61173
						public static LocString DESC = "A great-monument-sized underbite.";
					}

					// Token: 0x02003CDF RID: 15583
					public class OPTION_R
					{
						// Token: 0x0400EEF6 RID: 61174
						public static LocString NAME = "Beeta Top";

						// Token: 0x0400EEF7 RID: 61175
						public static LocString DESC = "A great-monument-sized Beeta.";
					}

					// Token: 0x02003CE0 RID: 15584
					public class OPTION_S
					{
						// Token: 0x0400EEF8 RID: 61176
						public static LocString NAME = "Sweetle Top";

						// Token: 0x0400EEF9 RID: 61177
						public static LocString DESC = "A great-monument-sized Sweetle.";
					}

					// Token: 0x02003CE1 RID: 15585
					public class OPTION_T
					{
						// Token: 0x0400EEFA RID: 61178
						public static LocString NAME = "Plug Slug Top";

						// Token: 0x0400EEFB RID: 61179
						public static LocString DESC = "A great-monument-sized Plug Slug. Does not require a power source.";
					}

					// Token: 0x02003CE2 RID: 15586
					public class OPTION_U
					{
						// Token: 0x0400EEFC RID: 61180
						public static LocString NAME = "Grubgrub Top";

						// Token: 0x0400EEFD RID: 61181
						public static LocString DESC = "A great-monument-sized garden critter.";
					}

					// Token: 0x02003CE3 RID: 15587
					public class OPTION_V
					{
						// Token: 0x0400EEFE RID: 61182
						public static LocString NAME = "Rover Top";

						// Token: 0x0400EEFF RID: 61183
						public static LocString DESC = "It has no mouth, but still looks like it's smiling.";
					}

					// Token: 0x02003CE4 RID: 15588
					public class OPTION_W
					{
						// Token: 0x0400EF00 RID: 61184
						public static LocString NAME = "Radsick Top I";

						// Token: 0x0400EF01 RID: 61185
						public static LocString DESC = "A visual reminder about radiation safety.";
					}

					// Token: 0x02003CE5 RID: 15589
					public class OPTION_X
					{
						// Token: 0x0400EF02 RID: 61186
						public static LocString NAME = "Radsick Top II";

						// Token: 0x0400EF03 RID: 61187
						public static LocString DESC = "Progress comes at a price.";
					}

					// Token: 0x02003CE6 RID: 15590
					public class OPTION_Y
					{
						// Token: 0x0400EF04 RID: 61188
						public static LocString NAME = "Radsick Top III";

						// Token: 0x0400EF05 RID: 61189
						public static LocString DESC = "A cautionary tale for careless Duplicants.";
					}

					// Token: 0x02003CE7 RID: 15591
					public class OPTION_Z
					{
						// Token: 0x0400EF06 RID: 61190
						public static LocString NAME = "Radsick Top IV";

						// Token: 0x0400EF07 RID: 61191
						public static LocString DESC = "Excellent choice of decor for the entrance to highly radioactive site.";
					}
				}
			}

			// Token: 0x02002A26 RID: 10790
			public class MICROBEMUSHER
			{
				// Token: 0x0400B99B RID: 47515
				public static LocString NAME = UI.FormatAsLink("Microbe Musher", "MICROBEMUSHER");

				// Token: 0x0400B99C RID: 47516
				public static LocString DESC = "Musher recipes will keep Duplicants fed, but may impact health and morale over time.";

				// Token: 0x0400B99D RID: 47517
				public static LocString EFFECT = "Produces low quality " + UI.FormatAsLink("Food", "FOOD") + " using common ingredients.\n\nDuplicants will not fabricate items unless recipes are queued.";

				// Token: 0x020038AD RID: 14509
				public class FACADES
				{
					// Token: 0x02003CE8 RID: 15592
					public class DEFAULT_MICROBEMUSHER
					{
						// Token: 0x0400EF08 RID: 61192
						public static LocString NAME = UI.FormatAsLink("Microbe Musher", "MICROBEMUSHER");

						// Token: 0x0400EF09 RID: 61193
						public static LocString DESC = "Musher recipes will keep Duplicants fed, but may impact health and morale over time.";
					}

					// Token: 0x02003CE9 RID: 15593
					public class PURPLE_BRAINFAT
					{
						// Token: 0x0400EF0A RID: 61194
						public static LocString NAME = UI.FormatAsLink("Faint Purple Microbe Musher", "MICROBEMUSHER");

						// Token: 0x0400EF0B RID: 61195
						public static LocString DESC = "A colorful distraction from the actual quality of the food.";
					}

					// Token: 0x02003CEA RID: 15594
					public class YELLOW_TARTAR
					{
						// Token: 0x0400EF0C RID: 61196
						public static LocString NAME = UI.FormatAsLink("Ick Yellow Microbe Musher", "MICROBEMUSHER");

						// Token: 0x0400EF0D RID: 61197
						public static LocString DESC = "Makes meals that are memorable for all the wrong reasons.";
					}

					// Token: 0x02003CEB RID: 15595
					public class RED_ROSE
					{
						// Token: 0x0400EF0E RID: 61198
						public static LocString NAME = UI.FormatAsLink("Puce Pink Microbe Musher", "MICROBEMUSHER");

						// Token: 0x0400EF0F RID: 61199
						public static LocString DESC = "Hunger strikes are not an option, but color-coordination is.";
					}

					// Token: 0x02003CEC RID: 15596
					public class GREEN_MUSH
					{
						// Token: 0x0400EF10 RID: 61200
						public static LocString NAME = UI.FormatAsLink("Mush Green Microbe Musher", "MICROBEMUSHER");

						// Token: 0x0400EF11 RID: 61201
						public static LocString DESC = "Edible colloids for dinner <i>again</i>?";
					}

					// Token: 0x02003CED RID: 15597
					public class BLUE_BABYTEARS
					{
						// Token: 0x0400EF12 RID: 61202
						public static LocString NAME = UI.FormatAsLink("Weepy Blue Microbe Musher", "MICROBEMUSHER");

						// Token: 0x0400EF13 RID: 61203
						public static LocString DESC = "Prioritizes nutritional value over flavor.";
					}
				}
			}

			// Token: 0x02002A27 RID: 10791
			public class MINERALDEOXIDIZER
			{
				// Token: 0x0400B99E RID: 47518
				public static LocString NAME = UI.FormatAsLink("Oxygen Diffuser", "MINERALDEOXIDIZER");

				// Token: 0x0400B99F RID: 47519
				public static LocString DESC = "Oxygen diffusers are inefficient, but output enough oxygen to keep a colony breathing.";

				// Token: 0x0400B9A0 RID: 47520
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts large amounts of ",
					UI.FormatAsLink("Algae", "ALGAE"),
					" into ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					".\n\nBecomes idle when the area reaches maximum pressure capacity."
				});
			}

			// Token: 0x02002A28 RID: 10792
			public class SUBLIMATIONSTATION
			{
				// Token: 0x0400B9A1 RID: 47521
				public static LocString NAME = UI.FormatAsLink("Sublimation Station", "SUBLIMATIONSTATION");

				// Token: 0x0400B9A2 RID: 47522
				public static LocString DESC = "Sublimation is the sublime process by which solids convert directly into gas.";

				// Token: 0x0400B9A3 RID: 47523
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Speeds up the conversion of ",
					UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
					" into ",
					UI.FormatAsLink("Polluted Oxygen", "CONTAMINATEDOXYGEN"),
					".\n\nBecomes idle when the area reaches maximum pressure capacity."
				});
			}

			// Token: 0x02002A29 RID: 10793
			public class WOODTILE
			{
				// Token: 0x0400B9A4 RID: 47524
				public static LocString NAME = "Wood Tile";

				// Token: 0x0400B9A5 RID: 47525
				public static LocString DESC = "Rooms built with wood tile are cozy and pleasant.";

				// Token: 0x0400B9A6 RID: 47526
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Used to build the walls and floors of rooms.\n\nProvides good insulation and boosts ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});
			}

			// Token: 0x02002A2A RID: 10794
			public class SNOWTILE
			{
				// Token: 0x0400B9A7 RID: 47527
				public static LocString NAME = "Snow Tile";

				// Token: 0x0400B9A8 RID: 47528
				public static LocString DESC = "Snow tiles have low thermal conductivity, but will melt if temperatures get too high.";

				// Token: 0x0400B9A9 RID: 47529
				public static LocString EFFECT = "Used to build the walls and floors of rooms.\n\nInsulates rooms to reduce " + UI.FormatAsLink("Heat", "HEAT") + " loss in cold climates.";
			}

			// Token: 0x02002A2B RID: 10795
			public class CAMPFIRE
			{
				// Token: 0x0400B9AA RID: 47530
				public static LocString NAME = UI.FormatAsLink("Wood Heater", "CAMPFIRE");

				// Token: 0x0400B9AB RID: 47531
				public static LocString DESC = "Wood heaters dry out soggy feet and help Duplicants forget how cold they are.";

				// Token: 0x0400B9AC RID: 47532
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Consumes ",
					UI.FormatAsLink("Wood", "WOOD"),
					" in order to ",
					UI.FormatAsLink("Heat", "HEAT"),
					" chilly surroundings."
				});
			}

			// Token: 0x02002A2C RID: 10796
			public class ICEKETTLE
			{
				// Token: 0x0400B9AD RID: 47533
				public static LocString NAME = UI.FormatAsLink("Ice Liquefier", "ICEKETTLE");

				// Token: 0x0400B9AE RID: 47534
				public static LocString DESC = "The water never gets hot enough to burn the tongue.";

				// Token: 0x0400B9AF RID: 47535
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Consumes ",
					UI.FormatAsLink("Wood", "WOOD"),
					" to melt ",
					UI.FormatAsLink("Ice", "ICE"),
					" into ",
					UI.FormatAsLink("Water", "WATER"),
					", which can be bottled for transport."
				});
			}

			// Token: 0x02002A2D RID: 10797
			public class WOODSTORAGE
			{
				// Token: 0x0400B9B0 RID: 47536
				public static LocString NAME = "Wood Pile";

				// Token: 0x0400B9B1 RID: 47537
				public static LocString DESC = "Once it's empty, there's no use pining for more.";

				// Token: 0x0400B9B2 RID: 47538
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores a finite supply of ",
					UI.FormatAsLink("Wood", "WOOD"),
					", which can be used for construction or to produce ",
					UI.FormatAsLink("Heat", "HEAT"),
					"."
				});
			}

			// Token: 0x02002A2E RID: 10798
			public class DLC2POITECHUNLOCKS
			{
				// Token: 0x0400B9B3 RID: 47539
				public static LocString NAME = "Research Portal";

				// Token: 0x0400B9B4 RID: 47540
				public static LocString DESC = "A functional research decrypter with one transmission remaining.\n\nIt was designed to support colony survival.";
			}

			// Token: 0x02002A2F RID: 10799
			public class DLC4POITECHUNLOCKS
			{
				// Token: 0x0400B9B5 RID: 47541
				public static LocString NAME = "Research Portal";

				// Token: 0x0400B9B6 RID: 47542
				public static LocString DESC = "A functional research decrypter with one transmission remaining.\n\nIt was designed to support colony survival.";
			}

			// Token: 0x02002A30 RID: 10800
			public class DEEPFRYER
			{
				// Token: 0x0400B9B7 RID: 47543
				public static LocString NAME = UI.FormatAsLink("Deep Fryer", "DEEPFRYER");

				// Token: 0x0400B9B8 RID: 47544
				public static LocString DESC = "Everything tastes better when it's deep-fried.";

				// Token: 0x0400B9B9 RID: 47545
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Tallow", "TALLOW"),
					" to cook a wide variety of improved ",
					UI.FormatAsLink("Foods", "FOOD"),
					".\n\nDuplicants will not fabricate items unless recipes are queued."
				});

				// Token: 0x020038AE RID: 14510
				public class STATUSITEMS
				{
					// Token: 0x02003CEE RID: 15598
					public class OUTSIDE_KITCHEN
					{
						// Token: 0x0400EF14 RID: 61204
						public static LocString NAME = "Outside of Kitchen";

						// Token: 0x0400EF15 RID: 61205
						public static LocString TOOLTIP = "This building must be in a Kitchen before it can be used";
					}
				}
			}

			// Token: 0x02002A31 RID: 10801
			public class ORESCRUBBER
			{
				// Token: 0x0400B9BA RID: 47546
				public static LocString NAME = UI.FormatAsLink("Ore Scrubber", "ORESCRUBBER");

				// Token: 0x0400B9BB RID: 47547
				public static LocString DESC = "Scrubbers sanitize freshly mined materials before they're brought into the colony.";

				// Token: 0x0400B9BC RID: 47548
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Kills a significant amount of ",
					UI.FormatAsLink("Germs", "DISEASE"),
					" present on ",
					UI.FormatAsLink("Raw Ore", "RAWMINERAL"),
					"."
				});
			}

			// Token: 0x02002A32 RID: 10802
			public class OUTHOUSE
			{
				// Token: 0x0400B9BD RID: 47549
				public static LocString NAME = UI.FormatAsLink("Outhouse", "OUTHOUSE");

				// Token: 0x0400B9BE RID: 47550
				public static LocString DESC = "The colony that eats together, excretes together.";

				// Token: 0x0400B9BF RID: 47551
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Gives Duplicants a place to relieve themselves.\n\nRequires no ",
					UI.FormatAsLink("Piping", "LIQUIDPIPING"),
					".\n\nMust be periodically emptied of ",
					UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
					"."
				});
			}

			// Token: 0x02002A33 RID: 10803
			public class APOTHECARY
			{
				// Token: 0x0400B9C0 RID: 47552
				public static LocString NAME = UI.FormatAsLink("Apothecary", "APOTHECARY");

				// Token: 0x0400B9C1 RID: 47553
				public static LocString DESC = "Some medications help prevent diseases, while others aim to alleviate existing illness.";

				// Token: 0x0400B9C2 RID: 47554
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsLink("Medicine", "MEDICINE"),
					" to cure most basic ",
					UI.FormatAsLink("Diseases", "DISEASE"),
					".\n\nDuplicants must possess the Medicine Compounding ",
					UI.FormatAsLink("Skill", "ROLES"),
					" to fabricate medicines.\n\nDuplicants will not fabricate items unless recipes are queued."
				});
			}

			// Token: 0x02002A34 RID: 10804
			public class ADVANCEDAPOTHECARY
			{
				// Token: 0x0400B9C3 RID: 47555
				public static LocString NAME = UI.FormatAsLink("Nuclear Apothecary", "ADVANCEDAPOTHECARY");

				// Token: 0x0400B9C4 RID: 47556
				public static LocString DESC = "Some medications help prevent diseases, while others aim to alleviate existing illness.";

				// Token: 0x0400B9C5 RID: 47557
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsLink("Medicine", "MEDICINE"),
					" to cure most basic ",
					UI.FormatAsLink("Diseases", "DISEASE"),
					".\n\nDuplicants must possess the Medicine Compounding ",
					UI.FormatAsLink("Skill", "ROLES"),
					" to fabricate medicines.\n\nDuplicants will not fabricate items unless recipes are queued."
				});
			}

			// Token: 0x02002A35 RID: 10805
			public class PLANTERBOX
			{
				// Token: 0x0400B9C6 RID: 47558
				public static LocString NAME = UI.FormatAsLink("Planter Box", "PLANTERBOX");

				// Token: 0x0400B9C7 RID: 47559
				public static LocString DESC = "Domestically grown seeds mature more quickly than wild plants.";

				// Token: 0x0400B9C8 RID: 47560
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Grows one ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" from a ",
					UI.FormatAsLink("Seed", "PLANTS"),
					"."
				});

				// Token: 0x020038AF RID: 14511
				public class FACADES
				{
					// Token: 0x02003CEF RID: 15599
					public class DEFAULT_PLANTERBOX
					{
						// Token: 0x0400EF16 RID: 61206
						public static LocString NAME = UI.FormatAsLink("Planter Box", "PLANTERBOX");

						// Token: 0x0400EF17 RID: 61207
						public static LocString DESC = "Domestically grown seeds mature more quickly than wild plants.";
					}

					// Token: 0x02003CF0 RID: 15600
					public class MEALWOOD
					{
						// Token: 0x0400EF18 RID: 61208
						public static LocString NAME = UI.FormatAsLink("Mealy Teal Planter Box", "PLANTERBOX");

						// Token: 0x0400EF19 RID: 61209
						public static LocString DESC = "Inspired by genetically modified nature.";
					}

					// Token: 0x02003CF1 RID: 15601
					public class BRISTLEBLOSSOM
					{
						// Token: 0x0400EF1A RID: 61210
						public static LocString NAME = UI.FormatAsLink("Bristly Green Planter Box", "PLANTERBOX");

						// Token: 0x0400EF1B RID: 61211
						public static LocString DESC = "The interior is lined with tiny barbs.";
					}

					// Token: 0x02003CF2 RID: 15602
					public class WHEEZEWORT
					{
						// Token: 0x0400EF1C RID: 61212
						public static LocString NAME = UI.FormatAsLink("Wheezy Whorl Planter Box", "PLANTERBOX");

						// Token: 0x0400EF1D RID: 61213
						public static LocString DESC = "For the dreamy agriculturalist.";
					}

					// Token: 0x02003CF3 RID: 15603
					public class SLEETWHEAT
					{
						// Token: 0x0400EF1E RID: 61214
						public static LocString NAME = UI.FormatAsLink("Sleet Blue Planter Box", "PLANTERBOX");

						// Token: 0x0400EF1F RID: 61215
						public static LocString DESC = "The thick paint drips are invisible from a distance.";
					}

					// Token: 0x02003CF4 RID: 15604
					public class SALMON_PINK
					{
						// Token: 0x0400EF20 RID: 61216
						public static LocString NAME = UI.FormatAsLink("Flashy Planter Box", "PLANTERBOX");

						// Token: 0x0400EF21 RID: 61217
						public static LocString DESC = "It's not exactly a subtle color.";
					}
				}
			}

			// Token: 0x02002A36 RID: 10806
			public class PRESSUREDOOR
			{
				// Token: 0x0400B9C9 RID: 47561
				public static LocString NAME = UI.FormatAsLink("Mechanized Airlock", "PRESSUREDOOR");

				// Token: 0x0400B9CA RID: 47562
				public static LocString DESC = "Mechanized airlocks open and close more quickly than other types of door.";

				// Token: 0x0400B9CB RID: 47563
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Blocks ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" and ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" flow, maintaining pressure between areas.\n\nFunctions as a ",
					UI.FormatAsLink("Manual Airlock", "MANUALPRESSUREDOOR"),
					" when no ",
					UI.FormatAsLink("Power", "POWER"),
					" is available.\n\nWild ",
					UI.FormatAsLink("Critters", "CREATURES"),
					" cannot pass through doors."
				});
			}

			// Token: 0x02002A37 RID: 10807
			public class BUNKERDOOR
			{
				// Token: 0x0400B9CC RID: 47564
				public static LocString NAME = UI.FormatAsLink("Bunker Door", "BUNKERDOOR");

				// Token: 0x0400B9CD RID: 47565
				public static LocString DESC = "A massive, slow-moving door which is nearly indestructible.";

				// Token: 0x0400B9CE RID: 47566
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Blocks ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" and ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" flow, maintaining pressure between areas.\n\nCan withstand extremely high pressures and impacts."
				});
			}

			// Token: 0x02002A38 RID: 10808
			public class RATIONBOX
			{
				// Token: 0x0400B9CF RID: 47567
				public static LocString NAME = UI.FormatAsLink("Ration Box", "RATIONBOX");

				// Token: 0x0400B9D0 RID: 47568
				public static LocString DESC = "Ration boxes keep food safe from hungry critters, but don't slow food spoilage.";

				// Token: 0x0400B9D1 RID: 47569
				public static LocString EFFECT = "Stores a small amount of " + UI.FormatAsLink("Food", "FOOD") + ".\n\nFood must be delivered to boxes by Duplicants.";
			}

			// Token: 0x02002A39 RID: 10809
			public class PARKSIGN
			{
				// Token: 0x0400B9D2 RID: 47570
				public static LocString NAME = UI.FormatAsLink("Park Sign", "PARKSIGN");

				// Token: 0x0400B9D3 RID: 47571
				public static LocString DESC = "Passing through parks will increase Duplicant Morale.";

				// Token: 0x0400B9D4 RID: 47572
				public static LocString EFFECT = "Classifies an area as a Park or Nature Reserve.";
			}

			// Token: 0x02002A3A RID: 10810
			public class RADIATIONLIGHT
			{
				// Token: 0x0400B9D5 RID: 47573
				public static LocString NAME = UI.FormatAsLink("Radiation Lamp", "RADIATIONLIGHT");

				// Token: 0x0400B9D6 RID: 47574
				public static LocString DESC = "Duplicants can become sick if exposed to radiation without protection.";

				// Token: 0x0400B9D7 RID: 47575
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Emits ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					" when ",
					UI.FormatAsLink("Powered", "POWER"),
					" that can be collected by a ",
					UI.FormatAsLink("Radbolt Generator", "HIGHENERGYPARTICLESPAWNER"),
					"."
				});
			}

			// Token: 0x02002A3B RID: 10811
			public class REFRIGERATOR
			{
				// Token: 0x0400B9D8 RID: 47576
				public static LocString NAME = UI.FormatAsLink("Refrigerator", "REFRIGERATOR");

				// Token: 0x0400B9D9 RID: 47577
				public static LocString DESC = "Food spoilage can be slowed by ambient conditions as well as by refrigerators.";

				// Token: 0x0400B9DA RID: 47578
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Food", "FOOD"),
					" at an ideal ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" to prevent spoilage."
				});

				// Token: 0x0400B9DB RID: 47579
				public static LocString LOGIC_PORT = "Full/Not Full";

				// Token: 0x0400B9DC RID: 47580
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when full";

				// Token: 0x0400B9DD RID: 47581
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x020038B0 RID: 14512
				public class FACADES
				{
					// Token: 0x02003CF5 RID: 15605
					public class DEFAULT_REFRIGERATOR
					{
						// Token: 0x0400EF22 RID: 61218
						public static LocString NAME = UI.FormatAsLink("Refrigerator", "REFRIGERATOR");

						// Token: 0x0400EF23 RID: 61219
						public static LocString DESC = "Food spoilage can be slowed by ambient conditions as well as by refrigerators.";
					}

					// Token: 0x02003CF6 RID: 15606
					public class STRIPES_RED_WHITE
					{
						// Token: 0x0400EF24 RID: 61220
						public static LocString NAME = UI.FormatAsLink("Bold Stripe Refrigerator", "REFRIGERATOR");

						// Token: 0x0400EF25 RID: 61221
						public static LocString DESC = "Bold on the outside, cold on the inside!";
					}

					// Token: 0x02003CF7 RID: 15607
					public class BLUE_BABYTEARS
					{
						// Token: 0x0400EF26 RID: 61222
						public static LocString NAME = UI.FormatAsLink("Weepy Blue Refrigerator", "REFRIGERATOR");

						// Token: 0x0400EF27 RID: 61223
						public static LocString DESC = "For food so cold, it brings a tear to the eye.";
					}

					// Token: 0x02003CF8 RID: 15608
					public class GREEN_MUSH
					{
						// Token: 0x0400EF28 RID: 61224
						public static LocString NAME = UI.FormatAsLink("Mush Green Refrigerator", "REFRIGERATOR");

						// Token: 0x0400EF29 RID: 61225
						public static LocString DESC = "Honestly, this hue is particularly chilling.";
					}

					// Token: 0x02003CF9 RID: 15609
					public class RED_ROSE
					{
						// Token: 0x0400EF2A RID: 61226
						public static LocString NAME = UI.FormatAsLink("Puce Pink Refrigerator", "REFRIGERATOR");

						// Token: 0x0400EF2B RID: 61227
						public static LocString DESC = "Inspired by the Duplicant poem, \"Pretty in Puce.\"";
					}

					// Token: 0x02003CFA RID: 15610
					public class YELLOW_TARTAR
					{
						// Token: 0x0400EF2C RID: 61228
						public static LocString NAME = UI.FormatAsLink("Ick Yellow Refrigerator", "REFRIGERATOR");

						// Token: 0x0400EF2D RID: 61229
						public static LocString DESC = "Some Duplicants call it \"sunny\" yellow, but only because they've never seen the sun.";
					}

					// Token: 0x02003CFB RID: 15611
					public class PURPLE_BRAINFAT
					{
						// Token: 0x0400EF2E RID: 61230
						public static LocString NAME = UI.FormatAsLink("Faint Purple Refrigerator", "REFRIGERATOR");

						// Token: 0x0400EF2F RID: 61231
						public static LocString DESC = "This fridge makes color-coordination a (cold) snap.";
					}
				}
			}

			// Token: 0x02002A3C RID: 10812
			public class ROLESTATION
			{
				// Token: 0x0400B9DE RID: 47582
				public static LocString NAME = UI.FormatAsLink("Skills Board", "ROLESTATION");

				// Token: 0x0400B9DF RID: 47583
				public static LocString DESC = "A skills board can teach special skills to Duplicants they can't learn on their own.";

				// Token: 0x0400B9E0 RID: 47584
				public static LocString EFFECT = "Allows Duplicants to spend Skill Points to learn new " + UI.FormatAsLink("Skills", "JOBS") + ".";
			}

			// Token: 0x02002A3D RID: 10813
			public class RESETSKILLSSTATION
			{
				// Token: 0x0400B9E1 RID: 47585
				public static LocString NAME = UI.FormatAsLink("Skill Scrubber", "RESETSKILLSSTATION");

				// Token: 0x0400B9E2 RID: 47586
				public static LocString DESC = "Erase skills from a Duplicant's mind, returning them to their default abilities.";

				// Token: 0x0400B9E3 RID: 47587
				public static LocString EFFECT = "Refunds a Duplicant's Skill Points for reassignment.\n\nDuplicants will lose all assigned skills in the process.";
			}

			// Token: 0x02002A3E RID: 10814
			public class RESEARCHCENTER
			{
				// Token: 0x0400B9E4 RID: 47588
				public static LocString NAME = UI.FormatAsLink("Research Station", "RESEARCHCENTER");

				// Token: 0x0400B9E5 RID: 47589
				public static LocString DESC = "Research stations are necessary for unlocking all research tiers.";

				// Token: 0x0400B9E6 RID: 47590
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Conducts ",
					UI.FormatAsLink("Novice Research", "RESEARCH"),
					" to unlock new technologies.\n\nConsumes ",
					UI.FormatAsLink("Dirt", "DIRT"),
					"."
				});
			}

			// Token: 0x02002A3F RID: 10815
			public class ADVANCEDRESEARCHCENTER
			{
				// Token: 0x0400B9E7 RID: 47591
				public static LocString NAME = UI.FormatAsLink("Super Computer", "ADVANCEDRESEARCHCENTER");

				// Token: 0x0400B9E8 RID: 47592
				public static LocString DESC = "Super computers unlock higher technology tiers than research stations alone.";

				// Token: 0x0400B9E9 RID: 47593
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Conducts ",
					UI.FormatAsLink("Advanced Research", "RESEARCH"),
					" to unlock new technologies.\n\nConsumes ",
					UI.FormatAsLink("Water", "WATER"),
					".\n\nAssigned Duplicants must possess the ",
					UI.FormatAsLink("Advanced Research", "RESEARCHING1"),
					" skill."
				});
			}

			// Token: 0x02002A40 RID: 10816
			public class NUCLEARRESEARCHCENTER
			{
				// Token: 0x0400B9EA RID: 47594
				public static LocString NAME = UI.FormatAsLink("Materials Study Terminal", "NUCLEARRESEARCHCENTER");

				// Token: 0x0400B9EB RID: 47595
				public static LocString DESC = "Comes with a few ions thrown in, free of charge.";

				// Token: 0x0400B9EC RID: 47596
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Conducts ",
					UI.FormatAsLink("Materials Science Research", "RESEARCHDLC1"),
					" to unlock new technologies.\n\nConsumes Radbolts.\n\nAssigned Duplicants must possess the ",
					UI.FormatAsLink("Applied Sciences Research", "ATOMICRESEARCH"),
					" skill."
				});
			}

			// Token: 0x02002A41 RID: 10817
			public class ORBITALRESEARCHCENTER
			{
				// Token: 0x0400B9ED RID: 47597
				public static LocString NAME = UI.FormatAsLink("Orbital Data Collection Lab", "ORBITALRESEARCHCENTER");

				// Token: 0x0400B9EE RID: 47598
				public static LocString DESC = "Orbital Data Collection Labs record data while orbiting a Planetoid and write it to a " + UI.FormatAsLink("Data Bank", "ORBITALRESEARCHDATABANK") + ". ";

				// Token: 0x0400B9EF RID: 47599
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Creates ",
					UI.FormatAsLink("Data Banks", "ORBITALRESEARCHDATABANK"),
					" that can be consumed at a ",
					UI.FormatAsLink("Virtual Planetarium", "DLC1COSMICRESEARCHCENTER"),
					" to unlock new technologies.\n\nConsumes ",
					UI.FormatAsLink("Plastic", "POLYPROPYLENE"),
					" and ",
					UI.FormatAsLink("Power", "POWER"),
					"."
				});
			}

			// Token: 0x02002A42 RID: 10818
			public class COSMICRESEARCHCENTER
			{
				// Token: 0x0400B9F0 RID: 47600
				public static LocString NAME = UI.FormatAsLink("Virtual Planetarium", "COSMICRESEARCHCENTER");

				// Token: 0x0400B9F1 RID: 47601
				public static LocString DESC = "Planetariums allow the simulated exploration of locations discovered with a telescope.";

				// Token: 0x0400B9F2 RID: 47602
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Conducts ",
					UI.FormatAsLink("Interstellar Research", "RESEARCH"),
					" to unlock new technologies.\n\nConsumes data from ",
					UI.FormatAsLink("Research Modules", "RESEARCHMODULE"),
					".\n\nAssigned Duplicants must possess the ",
					UI.FormatAsLink("Astronomy", "ASTRONOMY"),
					" skill."
				});
			}

			// Token: 0x02002A43 RID: 10819
			public class DLC1COSMICRESEARCHCENTER
			{
				// Token: 0x0400B9F3 RID: 47603
				public static LocString NAME = UI.FormatAsLink("Virtual Planetarium", "DLC1COSMICRESEARCHCENTER");

				// Token: 0x0400B9F4 RID: 47604
				public static LocString DESC = "Planetariums allow the simulated exploration of locations recorded in " + UI.FormatAsLink("Data Banks", "ORBITALRESEARCHDATABANK") + ".";

				// Token: 0x0400B9F5 RID: 47605
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Conducts ",
					UI.FormatAsLink("Data Analysis Research", "RESEARCH"),
					" to unlock new technologies.\n\nConsumes ",
					UI.FormatAsLink("Data Banks", "ORBITALRESEARCHDATABANK"),
					" generated by exploration."
				});
			}

			// Token: 0x02002A44 RID: 10820
			public class TELESCOPE
			{
				// Token: 0x0400B9F6 RID: 47606
				public static LocString NAME = UI.FormatAsLink("Telescope", "TELESCOPE");

				// Token: 0x0400B9F7 RID: 47607
				public static LocString DESC = "Telescopes are necessary for learning starmaps and conducting rocket missions.";

				// Token: 0x0400B9F8 RID: 47608
				public static LocString EFFECT = "Maps Starmap destinations.\n\nAssigned Duplicants must possess the " + UI.FormatAsLink("Field Research", "RESEARCHING2") + " skill.\n\nBuilding must be exposed to space to function.";

				// Token: 0x0400B9F9 RID: 47609
				public static LocString REQUIREMENT_TOOLTIP = "A steady {0} supply is required to sustain working Duplicants.";
			}

			// Token: 0x02002A45 RID: 10821
			public class CLUSTERTELESCOPE
			{
				// Token: 0x0400B9FA RID: 47610
				public static LocString NAME = UI.FormatAsLink("Telescope", "CLUSTERTELESCOPE");

				// Token: 0x0400B9FB RID: 47611
				public static LocString DESC = "Telescopes are necessary for studying space, allowing rocket travel to other worlds.";

				// Token: 0x0400B9FC RID: 47612
				public static LocString EFFECT = "Reveals visitable Planetoids in space.\n\nAssigned Duplicants must possess the " + UI.FormatAsLink("Astronomy", "ASTRONOMY") + " skill.\n\nBuilding must be exposed to space to function.";

				// Token: 0x0400B9FD RID: 47613
				public static LocString REQUIREMENT_TOOLTIP = "A steady {0} supply is required to sustain working Duplicants.";
			}

			// Token: 0x02002A46 RID: 10822
			public class CLUSTERTELESCOPEENCLOSED
			{
				// Token: 0x0400B9FE RID: 47614
				public static LocString NAME = UI.FormatAsLink("Enclosed Telescope", "CLUSTERTELESCOPEENCLOSED");

				// Token: 0x0400B9FF RID: 47615
				public static LocString DESC = "Telescopes are necessary for studying space, allowing rocket travel to other worlds.";

				// Token: 0x0400BA00 RID: 47616
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Reveals visitable Planetoids in space... in comfort!\n\nAssigned Duplicants must possess the ",
					UI.FormatAsLink("Astronomy", "ASTRONOMY"),
					" skill.\n\nExcellent sunburn protection (100%), partial ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					" protection (",
					GameUtil.GetFormattedPercent(FIXEDTRAITS.COSMICRADIATION.TELESCOPE_RADIATION_SHIELDING * 100f, GameUtil.TimeSlice.None),
					") .\n\nBuilding must be exposed to space to function."
				});

				// Token: 0x0400BA01 RID: 47617
				public static LocString REQUIREMENT_TOOLTIP = "A steady {0} supply is required to sustain working Duplicants.";
			}

			// Token: 0x02002A47 RID: 10823
			public class MISSIONCONTROL
			{
				// Token: 0x0400BA02 RID: 47618
				public static LocString NAME = UI.FormatAsLink("Mission Control Station", "MISSIONCONTROL");

				// Token: 0x0400BA03 RID: 47619
				public static LocString DESC = "Like a backseat driver who actually does know better.";

				// Token: 0x0400BA04 RID: 47620
				public static LocString EFFECT = "Provides guidance data to rocket pilots, to improve rocket speed.\n\nMust be operated by a Duplicant with the " + UI.FormatAsLink("Astronomy", "ASTRONOMY") + " skill.\n\nRequires a clear line of sight to space in order to function.";
			}

			// Token: 0x02002A48 RID: 10824
			public class MISSIONCONTROLCLUSTER
			{
				// Token: 0x0400BA05 RID: 47621
				public static LocString NAME = UI.FormatAsLink("Mission Control Station", "MISSIONCONTROLCLUSTER");

				// Token: 0x0400BA06 RID: 47622
				public static LocString DESC = "Like a backseat driver who actually does know better.";

				// Token: 0x0400BA07 RID: 47623
				public static LocString EFFECT = "Provides guidance data to rocket pilots within range, to improve rocket speed.\n\nMust be operated by a Duplicant with the " + UI.FormatAsLink("Astronomy", "ASTRONOMY") + " skill.\n\nRequires a clear line of sight to space in order to function.";
			}

			// Token: 0x02002A49 RID: 10825
			public class SCULPTURE
			{
				// Token: 0x0400BA08 RID: 47624
				public static LocString NAME = UI.FormatAsLink("Large Sculpting Block", "SCULPTURE");

				// Token: 0x0400BA09 RID: 47625
				public static LocString DESC = "Duplicants who have learned art skills can produce more decorative sculptures.";

				// Token: 0x0400BA0A RID: 47626
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Moderately increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be sculpted by a Duplicant."
				});

				// Token: 0x0400BA0B RID: 47627
				public static LocString POORQUALITYNAME = "\"Abstract\" Sculpture";

				// Token: 0x0400BA0C RID: 47628
				public static LocString AVERAGEQUALITYNAME = "Mediocre Sculpture";

				// Token: 0x0400BA0D RID: 47629
				public static LocString EXCELLENTQUALITYNAME = "Genius Sculpture";

				// Token: 0x020038B1 RID: 14513
				public class FACADES
				{
					// Token: 0x02003CFC RID: 15612
					public class SCULPTURE_GOOD_1
					{
						// Token: 0x0400EF30 RID: 61232
						public static LocString NAME = UI.FormatAsLink("O Cupid, My Cupid", "SCULPTURE_GOOD_1");

						// Token: 0x0400EF31 RID: 61233
						public static LocString DESC = "Ode to the bow and arrow, love's equivalent to a mining gun...but for hearts.";
					}

					// Token: 0x02003CFD RID: 15613
					public class SCULPTURE_CRAP_1
					{
						// Token: 0x0400EF32 RID: 61234
						public static LocString NAME = UI.FormatAsLink("Inexplicable", "SCULPTURE_CRAP_1");

						// Token: 0x0400EF33 RID: 61235
						public static LocString DESC = "A valiant attempt at art.";
					}

					// Token: 0x02003CFE RID: 15614
					public class SCULPTURE_AMAZING_2
					{
						// Token: 0x0400EF34 RID: 61236
						public static LocString NAME = UI.FormatAsLink("Plate Chucker", "SCULPTURE_AMAZING_2");

						// Token: 0x0400EF35 RID: 61237
						public static LocString DESC = "A masterful portrayal of an athlete who's been banned from the communal kitchen.";
					}

					// Token: 0x02003CFF RID: 15615
					public class SCULPTURE_AMAZING_3
					{
						// Token: 0x0400EF36 RID: 61238
						public static LocString NAME = UI.FormatAsLink("Before Battle", "SCULPTURE_AMAZING_3");

						// Token: 0x0400EF37 RID: 61239
						public static LocString DESC = "A masterful portrayal of a slingshot-wielding hero.";
					}

					// Token: 0x02003D00 RID: 15616
					public class SCULPTURE_AMAZING_4
					{
						// Token: 0x0400EF38 RID: 61240
						public static LocString NAME = UI.FormatAsLink("Grandiose Grub-Grub", "SCULPTURE_AMAZING_4");

						// Token: 0x0400EF39 RID: 61241
						public static LocString DESC = "A masterful portrayal of a gentle, plant-tending critter.";
					}

					// Token: 0x02003D01 RID: 15617
					public class SCULPTURE_AMAZING_1
					{
						// Token: 0x0400EF3A RID: 61242
						public static LocString NAME = UI.FormatAsLink("The Hypothesizer", "SCULPTURE_AMAZING_1");

						// Token: 0x0400EF3B RID: 61243
						public static LocString DESC = "A masterful portrayal of a scientist lost in thought.";
					}

					// Token: 0x02003D02 RID: 15618
					public class SCULPTURE_AMAZING_5
					{
						// Token: 0x0400EF3C RID: 61244
						public static LocString NAME = UI.FormatAsLink("Vertical Cosmos", "SCULPTURE_AMAZING_5");

						// Token: 0x0400EF3D RID: 61245
						public static LocString DESC = "It contains multitudes.";
					}

					// Token: 0x02003D03 RID: 15619
					public class SCULPTURE_AMAZING_6
					{
						// Token: 0x0400EF3E RID: 61246
						public static LocString NAME = UI.FormatAsLink("Into the Voids", "SCULPTURE_AMAZING_6");

						// Token: 0x0400EF3F RID: 61247
						public static LocString DESC = "No amount of material success will ever fill the void within.";
					}
				}
			}

			// Token: 0x02002A4A RID: 10826
			public class ICESCULPTURE
			{
				// Token: 0x0400BA0E RID: 47630
				public static LocString NAME = UI.FormatAsLink("Ice Block", "ICESCULPTURE");

				// Token: 0x0400BA0F RID: 47631
				public static LocString DESC = "Prone to melting.";

				// Token: 0x0400BA10 RID: 47632
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Majorly increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be sculpted by a Duplicant."
				});

				// Token: 0x0400BA11 RID: 47633
				public static LocString POORQUALITYNAME = "\"Abstract\" Ice Sculpture";

				// Token: 0x0400BA12 RID: 47634
				public static LocString AVERAGEQUALITYNAME = "Mediocre Ice Sculpture";

				// Token: 0x0400BA13 RID: 47635
				public static LocString EXCELLENTQUALITYNAME = "Genius Ice Sculpture";

				// Token: 0x020038B2 RID: 14514
				public class FACADES
				{
					// Token: 0x02003D04 RID: 15620
					public class ICESCULPTURE_CRAP
					{
						// Token: 0x0400EF40 RID: 61248
						public static LocString NAME = UI.FormatAsLink("Cubi I", "ICESCULPTURE_CRAP");

						// Token: 0x0400EF41 RID: 61249
						public static LocString DESC = "It's structurally unsound, but otherwise not entirely terrible.";
					}

					// Token: 0x02003D05 RID: 15621
					public class ICESCULPTURE_AMAZING_1
					{
						// Token: 0x0400EF42 RID: 61250
						public static LocString NAME = UI.FormatAsLink("Exquisite Chompers", "ICESCULPTURE_AMAZING_1");

						// Token: 0x0400EF43 RID: 61251
						public static LocString DESC = "These incisors are the stuff of dental legend.";
					}

					// Token: 0x02003D06 RID: 15622
					public class ICESCULPTURE_AMAZING_2
					{
						// Token: 0x0400EF44 RID: 61252
						public static LocString NAME = UI.FormatAsLink("Frosty Crustacean", "ICESCULPTURE_AMAZING_2");

						// Token: 0x0400EF45 RID: 61253
						public static LocString DESC = "A charming depiction of the mighty Pokeshell in mid-rampage.";
					}

					// Token: 0x02003D07 RID: 15623
					public class ICESCULPTURE_AMAZING_3
					{
						// Token: 0x0400EF46 RID: 61254
						public static LocString NAME = UI.FormatAsLink("The Chase", "ICESCULPTURE_AMAZING_3");

						// Token: 0x0400EF47 RID: 61255
						public static LocString DESC = "Some aquarists posit that Pacus are the original creators of the game now known as \"Tag.\"";
					}
				}
			}

			// Token: 0x02002A4B RID: 10827
			public class MARBLESCULPTURE
			{
				// Token: 0x0400BA14 RID: 47636
				public static LocString NAME = UI.FormatAsLink("Marble Block", "MARBLESCULPTURE");

				// Token: 0x0400BA15 RID: 47637
				public static LocString DESC = "Duplicants who have learned art skills can produce more decorative sculptures.";

				// Token: 0x0400BA16 RID: 47638
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Majorly increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be sculpted by a Duplicant."
				});

				// Token: 0x0400BA17 RID: 47639
				public static LocString POORQUALITYNAME = "\"Abstract\" Marble Sculpture";

				// Token: 0x0400BA18 RID: 47640
				public static LocString AVERAGEQUALITYNAME = "Mediocre Marble Sculpture";

				// Token: 0x0400BA19 RID: 47641
				public static LocString EXCELLENTQUALITYNAME = "Genius Marble Sculpture";

				// Token: 0x020038B3 RID: 14515
				public class FACADES
				{
					// Token: 0x02003D08 RID: 15624
					public class SCULPTURE_MARBLE_CRAP_1
					{
						// Token: 0x0400EF48 RID: 61256
						public static LocString NAME = UI.FormatAsLink("Lumpy Fungus", "SCULPTURE_MARBLE_CRAP_1");

						// Token: 0x0400EF49 RID: 61257
						public static LocString DESC = "The artist was a very fungi.";
					}

					// Token: 0x02003D09 RID: 15625
					public class SCULPTURE_MARBLE_GOOD_1
					{
						// Token: 0x0400EF4A RID: 61258
						public static LocString NAME = UI.FormatAsLink("Unicorn Bust", "SCULPTURE_MARBLE_GOOD_1");

						// Token: 0x0400EF4B RID: 61259
						public static LocString DESC = "It has real \"mane\" character energy.";
					}

					// Token: 0x02003D0A RID: 15626
					public class SCULPTURE_MARBLE_AMAZING_1
					{
						// Token: 0x0400EF4C RID: 61260
						public static LocString NAME = UI.FormatAsLink("The Large-ish Mermaid", "SCULPTURE_MARBLE_AMAZING_1");

						// Token: 0x0400EF4D RID: 61261
						public static LocString DESC = "She's not afraid to take up space.";
					}

					// Token: 0x02003D0B RID: 15627
					public class SCULPTURE_MARBLE_AMAZING_2
					{
						// Token: 0x0400EF4E RID: 61262
						public static LocString NAME = UI.FormatAsLink("Grouchy Beast", "SCULPTURE_MARBLE_AMAZING_2");

						// Token: 0x0400EF4F RID: 61263
						public static LocString DESC = "The artist took great pleasure in conveying their displeasure.";
					}

					// Token: 0x02003D0C RID: 15628
					public class SCULPTURE_MARBLE_AMAZING_3
					{
						// Token: 0x0400EF50 RID: 61264
						public static LocString NAME = UI.FormatAsLink("The Guardian", "SCULPTURE_MARBLE_AMAZING_3");

						// Token: 0x0400EF51 RID: 61265
						public static LocString DESC = "Will not play fetch.";
					}

					// Token: 0x02003D0D RID: 15629
					public class SCULPTURE_MARBLE_AMAZING_4
					{
						// Token: 0x0400EF52 RID: 61266
						public static LocString NAME = UI.FormatAsLink("Truly A-Moo-Zing", "SCULPTURE_MARBLE_AMAZING_4");

						// Token: 0x0400EF53 RID: 61267
						public static LocString DESC = "A masterful celebration of one of the universe's most mysterious - and flatulent - organisms.";
					}

					// Token: 0x02003D0E RID: 15630
					public class SCULPTURE_MARBLE_AMAZING_5
					{
						// Token: 0x0400EF54 RID: 61268
						public static LocString NAME = UI.FormatAsLink("Green Goddess", "SCULPTURE_MARBLE_AMAZING_5");

						// Token: 0x0400EF55 RID: 61269
						public static LocString DESC = "A masterful celebration of the deep bond between a horticulturalist and her prize Bristle Blossom.";
					}
				}
			}

			// Token: 0x02002A4C RID: 10828
			public class METALSCULPTURE
			{
				// Token: 0x0400BA1A RID: 47642
				public static LocString NAME = UI.FormatAsLink("Metal Block", "METALSCULPTURE");

				// Token: 0x0400BA1B RID: 47643
				public static LocString DESC = "Duplicants who have learned art skills can produce more decorative sculptures.";

				// Token: 0x0400BA1C RID: 47644
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Majorly increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be sculpted by a Duplicant."
				});

				// Token: 0x0400BA1D RID: 47645
				public static LocString POORQUALITYNAME = "\"Abstract\" Metal Sculpture";

				// Token: 0x0400BA1E RID: 47646
				public static LocString AVERAGEQUALITYNAME = "Mediocre Metal Sculpture";

				// Token: 0x0400BA1F RID: 47647
				public static LocString EXCELLENTQUALITYNAME = "Genius Metal Sculpture";

				// Token: 0x020038B4 RID: 14516
				public class FACADES
				{
					// Token: 0x02003D0F RID: 15631
					public class SCULPTURE_METAL_CRAP_1
					{
						// Token: 0x0400EF56 RID: 61270
						public static LocString NAME = UI.FormatAsLink("Unnatural Beauty", "SCULPTURE_METAL_CRAP_1");

						// Token: 0x0400EF57 RID: 61271
						public static LocString DESC = "Actually, it's a very good likeness.";
					}

					// Token: 0x02003D10 RID: 15632
					public class SCULPTURE_METAL_GOOD_1
					{
						// Token: 0x0400EF58 RID: 61272
						public static LocString NAME = UI.FormatAsLink("Beautiful Biohazard", "SCULPTURE_METAL_GOOD_1");

						// Token: 0x0400EF59 RID: 61273
						public static LocString DESC = "The Morb's eye is mounted on a swivel that activates at random intervals.";
					}

					// Token: 0x02003D11 RID: 15633
					public class SCULPTURE_METAL_AMAZING_1
					{
						// Token: 0x0400EF5A RID: 61274
						public static LocString NAME = UI.FormatAsLink("Insatiable Appetite", "SCULPTURE_METAL_AMAZING_1");

						// Token: 0x0400EF5B RID: 61275
						public static LocString DESC = "It's quite lovely, until someone stubs their toe on it in the dark.";
					}

					// Token: 0x02003D12 RID: 15634
					public class SCULPTURE_METAL_AMAZING_2
					{
						// Token: 0x0400EF5C RID: 61276
						public static LocString NAME = UI.FormatAsLink("Agape", "SCULPTURE_METAL_AMAZING_2");

						// Token: 0x0400EF5D RID: 61277
						public static LocString DESC = "Not quite expressionist, but undeniably expressive.";
					}

					// Token: 0x02003D13 RID: 15635
					public class SCULPTURE_METAL_AMAZING_3
					{
						// Token: 0x0400EF5E RID: 61278
						public static LocString NAME = UI.FormatAsLink("Friendly Flier", "SCULPTURE_METAL_AMAZING_3");

						// Token: 0x0400EF5F RID: 61279
						public static LocString DESC = "It emits no light, but it sure does brighten up a room.";
					}

					// Token: 0x02003D14 RID: 15636
					public class SCULPTURE_METAL_AMAZING_4
					{
						// Token: 0x0400EF60 RID: 61280
						public static LocString NAME = UI.FormatAsLink("Whatta Pip", "SCULPTURE_METAL_AMAZING_4");

						// Token: 0x0400EF61 RID: 61281
						public static LocString DESC = "A masterful likeness of the mischievous critter that Duplicants love to love.";
					}

					// Token: 0x02003D15 RID: 15637
					public class SCULPTURE_METAL_AMAZING_5
					{
						// Token: 0x0400EF62 RID: 61282
						public static LocString NAME = UI.FormatAsLink("Phrenologist's Dream", "SCULPTURE_METAL_AMAZING_5");

						// Token: 0x0400EF63 RID: 61283
						public static LocString DESC = "What if the entire head is one big bump?";
					}
				}
			}

			// Token: 0x02002A4D RID: 10829
			public class SMALLSCULPTURE
			{
				// Token: 0x0400BA20 RID: 47648
				public static LocString NAME = UI.FormatAsLink("Sculpting Block", "SMALLSCULPTURE");

				// Token: 0x0400BA21 RID: 47649
				public static LocString DESC = "Duplicants who have learned art skills can produce more decorative sculptures.";

				// Token: 0x0400BA22 RID: 47650
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Minorly increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be sculpted by a Duplicant."
				});

				// Token: 0x0400BA23 RID: 47651
				public static LocString POORQUALITYNAME = "\"Abstract\" Sculpture";

				// Token: 0x0400BA24 RID: 47652
				public static LocString AVERAGEQUALITYNAME = "Mediocre Sculpture";

				// Token: 0x0400BA25 RID: 47653
				public static LocString EXCELLENTQUALITYNAME = "Genius Sculpture";

				// Token: 0x020038B5 RID: 14517
				public class FACADES
				{
					// Token: 0x02003D16 RID: 15638
					public class SCULPTURE_1x2_GOOD
					{
						// Token: 0x0400EF64 RID: 61284
						public static LocString NAME = UI.FormatAsLink("Lunar Slice", "SCULPTURE_1x2_GOOD");

						// Token: 0x0400EF65 RID: 61285
						public static LocString DESC = "It must be a moon, because there are no bananas in space.";
					}

					// Token: 0x02003D17 RID: 15639
					public class SCULPTURE_1x2_CRAP
					{
						// Token: 0x0400EF66 RID: 61286
						public static LocString NAME = UI.FormatAsLink("Unrequited", "SCULPTURE_1x2_CRAP");

						// Token: 0x0400EF67 RID: 61287
						public static LocString DESC = "It's a heavy heart.";
					}

					// Token: 0x02003D18 RID: 15640
					public class SCULPTURE_1x2_AMAZING_1
					{
						// Token: 0x0400EF68 RID: 61288
						public static LocString NAME = UI.FormatAsLink("Not a Funnel", "SCULPTURE_1x2_AMAZING_1");

						// Token: 0x0400EF69 RID: 61289
						public static LocString DESC = "<i>Ceci n'est pas un entonnoir.</i>";
					}

					// Token: 0x02003D19 RID: 15641
					public class SCULPTURE_1x2_AMAZING_2
					{
						// Token: 0x0400EF6A RID: 61290
						public static LocString NAME = UI.FormatAsLink("Equilibrium", "SCULPTURE_1x2_AMAZING_2");

						// Token: 0x0400EF6B RID: 61291
						public static LocString DESC = "Part of a well-balanced exhibit.";
					}

					// Token: 0x02003D1A RID: 15642
					public class SCULPTURE_1x2_AMAZING_3
					{
						// Token: 0x0400EF6C RID: 61292
						public static LocString NAME = UI.FormatAsLink("Opaque Orb", "SCULPTURE_1x2_AMAZING_3");

						// Token: 0x0400EF6D RID: 61293
						public static LocString DESC = "It lacks transparency.";
					}

					// Token: 0x02003D1B RID: 15643
					public class SCULPTURE_1x2_AMAZING_4
					{
						// Token: 0x0400EF6E RID: 61294
						public static LocString NAME = UI.FormatAsLink("Employee of the Month", "SCULPTURE_1x2_AMAZING_4");

						// Token: 0x0400EF6F RID: 61295
						public static LocString DESC = "A masterful celebration of the Sweepy's unbeatable work ethic and cheerful, can-clean attitude.";
					}

					// Token: 0x02003D1C RID: 15644
					public class SCULPTURE_1x2_AMAZING_5
					{
						// Token: 0x0400EF70 RID: 61296
						public static LocString NAME = UI.FormatAsLink("Pointy Impossibility", "SCULPTURE_1x2_AMAZING_5");

						// Token: 0x0400EF71 RID: 61297
						public static LocString DESC = "A three-dimensional rebellion against the rules of Euclidean space.";
					}

					// Token: 0x02003D1D RID: 15645
					public class SCULPTURE_1x2_AMAZING_6
					{
						// Token: 0x0400EF72 RID: 61298
						public static LocString NAME = UI.FormatAsLink("Fireball", "SCULPTURE_1x2_AMAZING_6");

						// Token: 0x0400EF73 RID: 61299
						public static LocString DESC = "Tribute to the artist's friend, who once attempted to catch a meteor with their bare hands.";
					}
				}
			}

			// Token: 0x02002A4E RID: 10830
			public class WOODSCULPTURE
			{
				// Token: 0x0400BA26 RID: 47654
				public static LocString NAME = UI.FormatAsLink("Wood Block", "WOODSCULPTURE");

				// Token: 0x0400BA27 RID: 47655
				public static LocString DESC = "A great fit for smaller spaces.";

				// Token: 0x0400BA28 RID: 47656
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Moderately increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nMust be sculpted by a Duplicant."
				});

				// Token: 0x0400BA29 RID: 47657
				public static LocString POORQUALITYNAME = "\"Abstract\" Wood Sculpture";

				// Token: 0x0400BA2A RID: 47658
				public static LocString AVERAGEQUALITYNAME = "Mediocre Wood Sculpture";

				// Token: 0x0400BA2B RID: 47659
				public static LocString EXCELLENTQUALITYNAME = "Genius Wood Sculpture";
			}

			// Token: 0x02002A4F RID: 10831
			public class SHEARINGSTATION
			{
				// Token: 0x0400BA2C RID: 47660
				public static LocString NAME = UI.FormatAsLink("Shearing Station", "SHEARINGSTATION");

				// Token: 0x0400BA2D RID: 47661
				public static LocString DESC = "Those critters aren't gonna shear themselves.";

				// Token: 0x0400BA2E RID: 47662
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Shearing stations allow eligible ",
					UI.FormatAsLink("Critters", "CREATURES"),
					" to be safely sheared for useful raw materials.\n\nVisiting this building restores ",
					UI.FormatAsLink("Critters'", "CREATURES"),
					" physical and emotional well-being."
				});
			}

			// Token: 0x02002A50 RID: 10832
			public class OXYGENMASKSTATION
			{
				// Token: 0x0400BA2F RID: 47663
				public static LocString NAME = UI.FormatAsLink("Oxygen Mask Station", "OXYGENMASKSTATION");

				// Token: 0x0400BA30 RID: 47664
				public static LocString DESC = "Duplicants can't pass by a station if it lacks enough oxygen to fill a mask.";

				// Token: 0x0400BA31 RID: 47665
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses designated ",
					UI.FormatAsLink("Metal Ores", "METAL"),
					" from filter settings to create ",
					UI.FormatAsLink("Oxygen Masks", "OXYGENMASK"),
					".\n\nAutomatically draws in ambient ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" to fill masks.\n\nMarks a threshold where Duplicants must put on or take off a mask.\n\nCan be rotated before construction."
				});
			}

			// Token: 0x02002A51 RID: 10833
			public class SWEEPBOTSTATION
			{
				// Token: 0x0400BA32 RID: 47666
				public static LocString NAME = UI.FormatAsLink("Sweepy's Dock", "SWEEPBOTSTATION");

				// Token: 0x0400BA33 RID: 47667
				public static LocString NAMEDSTATION = "{0}'s Dock";

				// Token: 0x0400BA34 RID: 47668
				public static LocString DESC = "The cute little face comes pre-installed.";

				// Token: 0x0400BA35 RID: 47669
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Deploys an automated ",
					UI.FormatAsLink("Sweepy Bot", "SWEEPBOT"),
					" to sweep up ",
					UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
					" debris and ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" spills.\n\nDock stores ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					" and ",
					UI.FormatAsLink("Solids", "ELEMENTS_SOLID"),
					" gathered by the Sweepy.\n\nUses ",
					UI.FormatAsLink("Power", "POWER"),
					" to recharge the Sweepy.\n\nDuplicants will empty Dock storage into available storage bins."
				});
			}

			// Token: 0x02002A52 RID: 10834
			public class OXYGENMASKMARKER
			{
				// Token: 0x0400BA36 RID: 47670
				public static LocString NAME = UI.FormatAsLink("Oxygen Mask Checkpoint", "OXYGENMASKMARKER");

				// Token: 0x0400BA37 RID: 47671
				public static LocString DESC = "A checkpoint must have a correlating dock built on the opposite side its arrow faces.";

				// Token: 0x0400BA38 RID: 47672
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Marks a threshold where Duplicants must put on or take off an ",
					UI.FormatAsLink("Oxygen Mask", "OXYGEN_MASK"),
					".\n\nMust be built next to an ",
					UI.FormatAsLink("Oxygen Mask Dock", "OXYGENMASKLOCKER"),
					".\n\nCan be rotated before construction."
				});
			}

			// Token: 0x02002A53 RID: 10835
			public class OXYGENMASKLOCKER
			{
				// Token: 0x0400BA39 RID: 47673
				public static LocString NAME = UI.FormatAsLink("Oxygen Mask Dock", "OXYGENMASKLOCKER");

				// Token: 0x0400BA3A RID: 47674
				public static LocString DESC = "An oxygen mask dock will store and refill masks while they're not in use.";

				// Token: 0x0400BA3B RID: 47675
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Oxygen Masks", "OXYGEN_MASK"),
					" and refuels them with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					".\n\nBuild next to an ",
					UI.FormatAsLink("Oxygen Mask Checkpoint", "OXYGENMASKMARKER"),
					" to make Duplicants put on masks when passing by."
				});
			}

			// Token: 0x02002A54 RID: 10836
			public class SUITMARKER
			{
				// Token: 0x0400BA3C RID: 47676
				public static LocString NAME = UI.FormatAsLink("Atmo Suit Checkpoint", "SUITMARKER");

				// Token: 0x0400BA3D RID: 47677
				public static LocString DESC = "A checkpoint must have a correlating dock built on the opposite side its arrow faces.";

				// Token: 0x0400BA3E RID: 47678
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Marks a threshold where Duplicants must change into or out of ",
					UI.FormatAsLink("Atmo Suits", "ATMO_SUIT"),
					".\n\nMust be built next to an ",
					UI.FormatAsLink("Atmo Suit Dock", "SUITLOCKER"),
					".\n\nCan be rotated before construction."
				});
			}

			// Token: 0x02002A55 RID: 10837
			public class SUITLOCKER
			{
				// Token: 0x0400BA3F RID: 47679
				public static LocString NAME = UI.FormatAsLink("Atmo Suit Dock", "SUITLOCKER");

				// Token: 0x0400BA40 RID: 47680
				public static LocString DESC = "An atmo suit dock will empty atmo suits of waste, but only one suit can charge at a time.";

				// Token: 0x0400BA41 RID: 47681
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Atmo Suits", "ATMO_SUIT"),
					" and refuels them with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					".\n\nEmpties suits of ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					".\n\nBuild next to an ",
					UI.FormatAsLink("Atmo Suit Checkpoint", "SUITMARKER"),
					" to make Duplicants change into suits when passing by."
				});
			}

			// Token: 0x02002A56 RID: 10838
			public class JETSUITMARKER
			{
				// Token: 0x0400BA42 RID: 47682
				public static LocString NAME = UI.FormatAsLink("Jet Suit Checkpoint", "JETSUITMARKER");

				// Token: 0x0400BA43 RID: 47683
				public static LocString DESC = "A checkpoint must have a correlating dock built on the opposite side its arrow faces.";

				// Token: 0x0400BA44 RID: 47684
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Marks a threshold where Duplicants must change into or out of ",
					UI.FormatAsLink("Jet Suits", "JET_SUIT"),
					".\n\nMust be built next to a ",
					UI.FormatAsLink("Jet Suit Dock", "JETSUITLOCKER"),
					".\n\nCan be rotated before construction."
				});
			}

			// Token: 0x02002A57 RID: 10839
			public class JETSUITLOCKER
			{
				// Token: 0x0400BA45 RID: 47685
				public static LocString NAME = UI.FormatAsLink("Jet Suit Dock", "JETSUITLOCKER");

				// Token: 0x0400BA46 RID: 47686
				public static LocString DESC = "Jet suit docks can refill jet suits with air and fuel, or empty them of waste.";

				// Token: 0x0400BA47 RID: 47687
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Jet Suits", "JET_SUIT"),
					" and refuels them with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					" and ",
					UI.FormatAsLink("Petroleum", "PETROLEUM"),
					".\n\nEmpties suits of ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					".\n\nBuild next to a ",
					UI.FormatAsLink("Jet Suit Checkpoint", "JETSUITMARKER"),
					" to make Duplicants change into suits when passing by."
				});
			}

			// Token: 0x02002A58 RID: 10840
			public class LEADSUITMARKER
			{
				// Token: 0x0400BA48 RID: 47688
				public static LocString NAME = UI.FormatAsLink("Lead Suit Checkpoint", "LEADSUITMARKER");

				// Token: 0x0400BA49 RID: 47689
				public static LocString DESC = "A checkpoint must have a correlating dock built on the opposite side its arrow faces.";

				// Token: 0x0400BA4A RID: 47690
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Marks a threshold where Duplicants must change into or out of ",
					UI.FormatAsLink("Lead Suits", "LEAD_SUIT"),
					".\n\nMust be built next to a ",
					UI.FormatAsLink("Lead Suit Dock", "LEADSUITLOCKER"),
					"\n\nCan be rotated before construction."
				});
			}

			// Token: 0x02002A59 RID: 10841
			public class LEADSUITLOCKER
			{
				// Token: 0x0400BA4B RID: 47691
				public static LocString NAME = UI.FormatAsLink("Lead Suit Dock", "LEADSUITLOCKER");

				// Token: 0x0400BA4C RID: 47692
				public static LocString DESC = "Lead suit docks can refill lead suits with air and empty them of waste.";

				// Token: 0x0400BA4D RID: 47693
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores ",
					UI.FormatAsLink("Lead Suits", "LEAD_SUIT"),
					" and refuels them with ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					".\n\nEmpties suits of ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					".\n\nBuild next to a ",
					UI.FormatAsLink("Lead Suit Checkpoint", "LEADSUITMARKER"),
					" to make Duplicants change into suits when passing by."
				});
			}

			// Token: 0x02002A5A RID: 10842
			public class CRAFTINGTABLE
			{
				// Token: 0x0400BA4E RID: 47694
				public static LocString NAME = UI.FormatAsLink("Crafting Station", "CRAFTINGTABLE");

				// Token: 0x0400BA4F RID: 47695
				public static LocString DESC = "Crafting stations allow Duplicants to make oxygen masks to wear in low breathability areas.";

				// Token: 0x0400BA50 RID: 47696
				public static LocString EFFECT = "Produces items and equipment for Duplicant use.\n\nDuplicants will not fabricate items unless recipes are queued.";

				// Token: 0x0400BA51 RID: 47697
				public static LocString RECIPE_DESCRIPTION = "Converts {0} to {1}";
			}

			// Token: 0x02002A5B RID: 10843
			public class ADVANCEDCRAFTINGTABLE
			{
				// Token: 0x0400BA52 RID: 47698
				public static LocString NAME = UI.FormatAsLink("Soldering Station", "ADVANCEDCRAFTINGTABLE");

				// Token: 0x0400BA53 RID: 47699
				public static LocString DESC = "Soldering stations allow Duplicants to build helpful Flydo retriever bots.";

				// Token: 0x0400BA54 RID: 47700
				public static LocString EFFECT = "Produces advanced electronics and bionic " + UI.FormatAsLink("Boosters", "BIONIC_UPGRADE") + ".\n\nDuplicants will not fabricate items unless recipes are queued.";

				// Token: 0x0400BA55 RID: 47701
				public static LocString BIONIC_COMPONENT_RECIPE_DESC = "Converts {0} to {1}";

				// Token: 0x0400BA56 RID: 47702
				public static LocString GENERIC_RECIPE_DESCRIPTION = "Converts {0} to {1}";

				// Token: 0x0400BA57 RID: 47703
				public static LocString COLONY_HAS_BOOSTER_ASSIGNED_NONE = "My colony has no Bionic Duplicants with this booster assigned";

				// Token: 0x0400BA58 RID: 47704
				public static LocString COLONY_HAS_BOOSTER_ASSIGNED_COUNT = "My colony has {0} Bionic Duplicant(s) with this booster assigned";
			}

			// Token: 0x02002A5C RID: 10844
			public class DATAMINER
			{
				// Token: 0x0400BA59 RID: 47705
				public static LocString NAME = UI.FormatAsLink("Data Miner", "DATAMINER");

				// Token: 0x0400BA5A RID: 47706
				public static LocString DESC = "Data banks can also be used to program robo-pilot rocket modules.";

				// Token: 0x0400BA5B RID: 47707
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Mass-produces ",
					UI.FormatAsLink(DatabankHelper.NAME_PLURAL, "Databank"),
					" that can be processed into ",
					UI.FormatAsLink(DatabankHelper.RESEARCH_NAME, DatabankHelper.RESEARCH_CODEXID),
					" points.\n\nDuplicants will not fabricate items unless recipes are queued."
				});

				// Token: 0x0400BA5C RID: 47708
				public static LocString RECIPE_DESCRIPTION = "Turns {0} into {1}.";
			}

			// Token: 0x02002A5D RID: 10845
			public class REMOTEWORKTERMINAL
			{
				// Token: 0x0400BA5D RID: 47709
				public static LocString NAME = UI.FormatAsLink("Remote Controller", "REMOTEWORKTERMINAL");

				// Token: 0x0400BA5E RID: 47710
				public static LocString DESC = "Remote controllers cut down on colony commute times.";

				// Token: 0x0400BA5F RID: 47711
				public static LocString EFFECT = "Enables Duplicants to operate machinery remotely via a connected " + UI.FormatAsLink("Remote Worker Dock", "REMOTEWORKERDOCK") + ".";
			}

			// Token: 0x02002A5E RID: 10846
			public class REMOTEWORKERDOCK
			{
				// Token: 0x0400BA60 RID: 47712
				public static LocString NAME = UI.FormatAsLink("Remote Worker Dock", "REMOTEWORKERDOCK");

				// Token: 0x0400BA61 RID: 47713
				public static LocString NAME_FMT = "Dock {ID}";

				// Token: 0x0400BA62 RID: 47714
				public static LocString DESC = "It's a Duplicant's duplicate's dock.";

				// Token: 0x0400BA63 RID: 47715
				public static LocString EFFECT = UI.FormatAsLink("Remote Worker Docks", "REMOTEWORKERDOCK") + " deploy automatons that operate machinery based on instructions received from a connected " + UI.FormatAsLink("Remote Controller", "REMOTEWORKTERMINAL") + ".\n\nMust be placed within range of its target building.";
			}

			// Token: 0x02002A5F RID: 10847
			public class SUITFABRICATOR
			{
				// Token: 0x0400BA64 RID: 47716
				public static LocString NAME = UI.FormatAsLink("Exosuit Forge", "SUITFABRICATOR");

				// Token: 0x0400BA65 RID: 47717
				public static LocString DESC = "Exosuits can be filled with oxygen to allow Duplicants to safely enter hazardous areas.";

				// Token: 0x0400BA66 RID: 47718
				public static LocString EFFECT = "Forges protective " + UI.FormatAsLink("Exosuits", "EQUIPMENT") + " for Duplicants to wear.\n\nDuplicants will not fabricate items unless recipes are queued.";
			}

			// Token: 0x02002A60 RID: 10848
			public class CLOTHINGALTERATIONSTATION
			{
				// Token: 0x0400BA67 RID: 47719
				public static LocString NAME = UI.FormatAsLink("Clothing Refashionator", "CLOTHINGALTERATIONSTATION");

				// Token: 0x0400BA68 RID: 47720
				public static LocString DESC = "Allows skilled Duplicants to add extra personal pizzazz to their wardrobe.";

				// Token: 0x0400BA69 RID: 47721
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Upgrades ",
					UI.FormatAsLink("Snazzy Suits", "FUNKY_VEST"),
					" into ",
					UI.FormatAsLink("Primo Garb", "CUSTOM_CLOTHING"),
					".\n\nDuplicants will not fabricate items unless recipes are queued."
				});
			}

			// Token: 0x02002A61 RID: 10849
			public class CLOTHINGFABRICATOR
			{
				// Token: 0x0400BA6A RID: 47722
				public static LocString NAME = UI.FormatAsLink("Textile Loom", "CLOTHINGFABRICATOR");

				// Token: 0x0400BA6B RID: 47723
				public static LocString DESC = "A textile loom can be used to spin Reed Fiber into wearable Duplicant clothing.";

				// Token: 0x0400BA6C RID: 47724
				public static LocString EFFECT = "Tailors Duplicant " + UI.FormatAsLink("Clothing", "EQUIPMENT") + " items.\n\nDuplicants will not fabricate items unless recipes are queued.";
			}

			// Token: 0x02002A62 RID: 10850
			public class SOLIDBOOSTER
			{
				// Token: 0x0400BA6D RID: 47725
				public static LocString NAME = UI.FormatAsLink("Solid Fuel Thruster", "SOLIDBOOSTER");

				// Token: 0x0400BA6E RID: 47726
				public static LocString DESC = "Additional thrusters allow rockets to reach far away space destinations.";

				// Token: 0x0400BA6F RID: 47727
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Burns ",
					UI.FormatAsLink("Iron", "IRON"),
					" and ",
					UI.FormatAsLink("Oxylite", "OXYROCK"),
					" to increase rocket exploration distance."
				});
			}

			// Token: 0x02002A63 RID: 10851
			public class SPACEHEATER
			{
				// Token: 0x0400BA70 RID: 47728
				public static LocString NAME = UI.FormatAsLink("Space Heater", "SPACEHEATER");

				// Token: 0x0400BA71 RID: 47729
				public static LocString DESC = "Space heaters are a welcome cure for cold, soggy feet.";

				// Token: 0x0400BA72 RID: 47730
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Radiates a moderate amount of ",
					UI.FormatAsLink("Heat", "HEAT"),
					".\n\nRequires ",
					UI.FormatAsLink("Power", "POWER"),
					" in order to function."
				});
			}

			// Token: 0x02002A64 RID: 10852
			public class SPICEGRINDER
			{
				// Token: 0x0400BA73 RID: 47731
				public static LocString NAME = UI.FormatAsLink("Spice Grinder", "SPICEGRINDER");

				// Token: 0x0400BA74 RID: 47732
				public static LocString DESC = "Crushed seeds and other edibles make excellent meal-enhancing additives.";

				// Token: 0x0400BA75 RID: 47733
				public static LocString EFFECT = "Produces ingredients that add benefits to " + UI.FormatAsLink("foods", "FOOD") + " prepared at skilled cooking stations.";

				// Token: 0x0400BA76 RID: 47734
				public static LocString INGREDIENTHEADER = "Ingredients per 1000kcal:";
			}

			// Token: 0x02002A65 RID: 10853
			public class SMOKER
			{
				// Token: 0x0400BA77 RID: 47735
				public static LocString NAME = UI.FormatAsLink("Smoker", "SMOKER");

				// Token: 0x0400BA78 RID: 47736
				public static LocString DESC = "With a little patience, even tough meat can become deliciously edible.";

				// Token: 0x0400BA79 RID: 47737
				public static LocString EFFECT = "Cooks improved " + UI.FormatAsLink("foods", "FOOD") + " over low, slow heat.\n\nDuplicants will not fabricate items unless recipes are queued.";
			}

			// Token: 0x02002A66 RID: 10854
			public class STORAGELOCKER
			{
				// Token: 0x0400BA7A RID: 47738
				public static LocString NAME = UI.FormatAsLink("Storage Bin", "STORAGELOCKER");

				// Token: 0x0400BA7B RID: 47739
				public static LocString DESC = "Resources left on the floor become \"debris\" and lower decor when not put away.";

				// Token: 0x0400BA7C RID: 47740
				public static LocString EFFECT = "Stores the " + UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID") + " of your choosing.";

				// Token: 0x020038B6 RID: 14518
				public class FACADES
				{
					// Token: 0x02003D1E RID: 15646
					public class DEFAULT_STORAGELOCKER
					{
						// Token: 0x0400EF74 RID: 61300
						public static LocString NAME = UI.FormatAsLink("Storage Bin", "STORAGELOCKER");

						// Token: 0x0400EF75 RID: 61301
						public static LocString DESC = "Resources left on the floor become \"debris\" and lower decor when not put away.";
					}

					// Token: 0x02003D1F RID: 15647
					public class GREEN_MUSH
					{
						// Token: 0x0400EF76 RID: 61302
						public static LocString NAME = UI.FormatAsLink("Mush Green Storage Bin", "STORAGELOCKER");

						// Token: 0x0400EF77 RID: 61303
						public static LocString DESC = "Color-coded storage makes things easier to find.";
					}

					// Token: 0x02003D20 RID: 15648
					public class RED_ROSE
					{
						// Token: 0x0400EF78 RID: 61304
						public static LocString NAME = UI.FormatAsLink("Puce Pink Storage Bin", "STORAGELOCKER");

						// Token: 0x0400EF79 RID: 61305
						public static LocString DESC = "Color-coded storage makes things easier to find.";
					}

					// Token: 0x02003D21 RID: 15649
					public class BLUE_BABYTEARS
					{
						// Token: 0x0400EF7A RID: 61306
						public static LocString NAME = UI.FormatAsLink("Weepy Blue Storage Bin", "STORAGELOCKER");

						// Token: 0x0400EF7B RID: 61307
						public static LocString DESC = "Color-coded storage makes things easier to find.";
					}

					// Token: 0x02003D22 RID: 15650
					public class PURPLE_BRAINFAT
					{
						// Token: 0x0400EF7C RID: 61308
						public static LocString NAME = UI.FormatAsLink("Faint Purple Storage Bin", "STORAGELOCKER");

						// Token: 0x0400EF7D RID: 61309
						public static LocString DESC = "Color-coded storage makes things easier to find.";
					}

					// Token: 0x02003D23 RID: 15651
					public class YELLOW_TARTAR
					{
						// Token: 0x0400EF7E RID: 61310
						public static LocString NAME = UI.FormatAsLink("Ick Yellow Storage Bin", "STORAGELOCKER");

						// Token: 0x0400EF7F RID: 61311
						public static LocString DESC = "Color-coded storage makes things easier to find.";
					}

					// Token: 0x02003D24 RID: 15652
					public class POLKA_DARKNAVYNOOKGREEN
					{
						// Token: 0x0400EF80 RID: 61312
						public static LocString NAME = UI.FormatAsLink("Party Dot Storage Bin", "STORAGELOCKER");

						// Token: 0x0400EF81 RID: 61313
						public static LocString DESC = "A fun storage solution for fun-damental materials.";
					}

					// Token: 0x02003D25 RID: 15653
					public class POLKA_DARKPURPLERESIN
					{
						// Token: 0x0400EF82 RID: 61314
						public static LocString NAME = UI.FormatAsLink("Mod Dot Storage Bin", "STORAGELOCKER");

						// Token: 0x0400EF83 RID: 61315
						public static LocString DESC = "Groovy storage, because messy colonies are such a drag.";
					}

					// Token: 0x02003D26 RID: 15654
					public class STRIPES_RED_WHITE
					{
						// Token: 0x0400EF84 RID: 61316
						public static LocString NAME = "Bold Stripe Storage Bin";

						// Token: 0x0400EF85 RID: 61317
						public static LocString DESC = "It's the merriest storage bin of all.";
					}
				}
			}

			// Token: 0x02002A67 RID: 10855
			public class STORAGELOCKERSMART
			{
				// Token: 0x0400BA7D RID: 47741
				public static LocString NAME = UI.FormatAsLink("Smart Storage Bin", "STORAGELOCKERSMART");

				// Token: 0x0400BA7E RID: 47742
				public static LocString DESC = "Smart storage bins can automate resource organization based on type and mass.";

				// Token: 0x0400BA7F RID: 47743
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores the ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" of your choosing.\n\nSends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" when bin is full."
				});

				// Token: 0x0400BA80 RID: 47744
				public static LocString LOGIC_PORT = "Full/Not Full";

				// Token: 0x0400BA81 RID: 47745
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when full";

				// Token: 0x0400BA82 RID: 47746
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002A68 RID: 10856
			public class OBJECTDISPENSER
			{
				// Token: 0x0400BA83 RID: 47747
				public static LocString NAME = UI.FormatAsLink("Automatic Dispenser", "OBJECTDISPENSER");

				// Token: 0x0400BA84 RID: 47748
				public static LocString DESC = "Automatic dispensers will store and drop resources in small quantities.";

				// Token: 0x0400BA85 RID: 47749
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Stores any ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" delivered to it by Duplicants.\n\nDumps stored materials back into the world when it receives a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					"."
				});

				// Token: 0x0400BA86 RID: 47750
				public static LocString LOGIC_PORT = "Dump Trigger";

				// Token: 0x0400BA87 RID: 47751
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Dump all stored materials";

				// Token: 0x0400BA88 RID: 47752
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Store materials";
			}

			// Token: 0x02002A69 RID: 10857
			public class LIQUIDRESERVOIR
			{
				// Token: 0x0400BA89 RID: 47753
				public static LocString NAME = UI.FormatAsLink("Liquid Reservoir", "LIQUIDRESERVOIR");

				// Token: 0x0400BA8A RID: 47754
				public static LocString DESC = "Reservoirs cannot receive manually delivered resources.";

				// Token: 0x0400BA8B RID: 47755
				public static LocString EFFECT = "Stores any " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " resources piped into it.";
			}

			// Token: 0x02002A6A RID: 10858
			public class GASRESERVOIR
			{
				// Token: 0x0400BA8C RID: 47756
				public static LocString NAME = UI.FormatAsLink("Gas Reservoir", "GASRESERVOIR");

				// Token: 0x0400BA8D RID: 47757
				public static LocString DESC = "Reservoirs cannot receive manually delivered resources.";

				// Token: 0x0400BA8E RID: 47758
				public static LocString EFFECT = "Stores any " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " resources piped into it.";

				// Token: 0x020038B7 RID: 14519
				public class FACADES
				{
					// Token: 0x02003D27 RID: 15655
					public class DEFAULT_GASRESERVOIR
					{
						// Token: 0x0400EF86 RID: 61318
						public static LocString NAME = UI.FormatAsLink("Gas Reservoir", "GASRESERVOIR");

						// Token: 0x0400EF87 RID: 61319
						public static LocString DESC = "Reservoirs cannot receive manually delivered resources.";
					}

					// Token: 0x02003D28 RID: 15656
					public class LIGHTGOLD
					{
						// Token: 0x0400EF88 RID: 61320
						public static LocString NAME = UI.FormatAsLink("Golden Gas Reservoir", "GASRESERVOIR");

						// Token: 0x0400EF89 RID: 61321
						public static LocString DESC = "A colorful reservoir keeps gases neatly organized.";
					}

					// Token: 0x02003D29 RID: 15657
					public class PEAGREEN
					{
						// Token: 0x0400EF8A RID: 61322
						public static LocString NAME = UI.FormatAsLink("Greenpea Gas Reservoir", "GASRESERVOIR");

						// Token: 0x0400EF8B RID: 61323
						public static LocString DESC = "A colorful reservoir keeps gases neatly organized.";
					}

					// Token: 0x02003D2A RID: 15658
					public class LIGHTCOBALT
					{
						// Token: 0x0400EF8C RID: 61324
						public static LocString NAME = UI.FormatAsLink("Bluemoon Gas Reservoir", "GASRESERVOIR");

						// Token: 0x0400EF8D RID: 61325
						public static LocString DESC = "A colorful reservoir keeps gases neatly organized.";
					}

					// Token: 0x02003D2B RID: 15659
					public class POLKA_DARKPURPLERESIN
					{
						// Token: 0x0400EF8E RID: 61326
						public static LocString NAME = UI.FormatAsLink("Mod Dot Gas Reservoir", "GASRESERVOIR");

						// Token: 0x0400EF8F RID: 61327
						public static LocString DESC = "It sports the cheeriest of paint jobs. What a gas!";
					}

					// Token: 0x02003D2C RID: 15660
					public class POLKA_DARKNAVYNOOKGREEN
					{
						// Token: 0x0400EF90 RID: 61328
						public static LocString NAME = UI.FormatAsLink("Party Dot Gas Reservoir", "GASRESERVOIR");

						// Token: 0x0400EF91 RID: 61329
						public static LocString DESC = "Safe gas storage doesn't have to be dull.";
					}

					// Token: 0x02003D2D RID: 15661
					public class BLUE_BABYTEARS
					{
						// Token: 0x0400EF92 RID: 61330
						public static LocString NAME = UI.FormatAsLink("Weepy Blue Gas Reservoir", "GASRESERVOIR");

						// Token: 0x0400EF93 RID: 61331
						public static LocString DESC = "A colorful reservoir keeps gases neatly organized.";
					}

					// Token: 0x02003D2E RID: 15662
					public class YELLOW_TARTAR
					{
						// Token: 0x0400EF94 RID: 61332
						public static LocString NAME = UI.FormatAsLink("Ick Yellow Gas Reservoir", "GASRESERVOIR");

						// Token: 0x0400EF95 RID: 61333
						public static LocString DESC = "A colorful reservoir keeps gases neatly organized.";
					}

					// Token: 0x02003D2F RID: 15663
					public class GREEN_MUSH
					{
						// Token: 0x0400EF96 RID: 61334
						public static LocString NAME = UI.FormatAsLink("Mush Green Gas Reservoir", "GASRESERVOIR");

						// Token: 0x0400EF97 RID: 61335
						public static LocString DESC = "A colorful reservoir keeps gases neatly organized.";
					}

					// Token: 0x02003D30 RID: 15664
					public class RED_ROSE
					{
						// Token: 0x0400EF98 RID: 61336
						public static LocString NAME = UI.FormatAsLink("Puce Pink Gas Reservoir", "GASRESERVOIR");

						// Token: 0x0400EF99 RID: 61337
						public static LocString DESC = "A colorful reservoir keeps gases neatly organized.";
					}

					// Token: 0x02003D31 RID: 15665
					public class PURPLE_BRAINFAT
					{
						// Token: 0x0400EF9A RID: 61338
						public static LocString NAME = UI.FormatAsLink("Faint Purple Gas Reservoir", "GASRESERVOIR");

						// Token: 0x0400EF9B RID: 61339
						public static LocString DESC = "A colorful reservoir keeps gases neatly organized.";
					}
				}
			}

			// Token: 0x02002A6B RID: 10859
			public class SMARTRESERVOIR
			{
				// Token: 0x0400BA8F RID: 47759
				public static LocString LOGIC_PORT = "Refill Parameters";

				// Token: 0x0400BA90 RID: 47760
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when reservoir is less than <b>Low Threshold</b> full, until <b>High Threshold</b> is reached again";

				// Token: 0x0400BA91 RID: 47761
				public static LocString LOGIC_PORT_INACTIVE = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " when reservoir is <b>High Threshold</b> full, until <b>Low Threshold</b> is reached again";

				// Token: 0x0400BA92 RID: 47762
				public static LocString ACTIVATE_TOOLTIP = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when reservoir is less than <b>{0}%</b> full, until it is <b>{1}% (High Threshold)</b> full";

				// Token: 0x0400BA93 RID: 47763
				public static LocString DEACTIVATE_TOOLTIP = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " when reservoir is <b>{0}%</b> full, until it is less than <b>{1}% (Low Threshold)</b> full";

				// Token: 0x0400BA94 RID: 47764
				public static LocString SIDESCREEN_TITLE = "Logic Activation Parameters";

				// Token: 0x0400BA95 RID: 47765
				public static LocString SIDESCREEN_ACTIVATE = "Low Threshold:";

				// Token: 0x0400BA96 RID: 47766
				public static LocString SIDESCREEN_DEACTIVATE = "High Threshold:";
			}

			// Token: 0x02002A6C RID: 10860
			public class LIQUIDHEATER
			{
				// Token: 0x0400BA97 RID: 47767
				public static LocString NAME = UI.FormatAsLink("Liquid Tepidizer", "LIQUIDHEATER");

				// Token: 0x0400BA98 RID: 47768
				public static LocString DESC = "Tepidizers heat liquid which can kill waterborne germs.";

				// Token: 0x0400BA99 RID: 47769
				public static LocString EFFECT = "Warms large bodies of " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + ".\n\nMust be fully submerged.";
			}

			// Token: 0x02002A6D RID: 10861
			public class SWITCH
			{
				// Token: 0x0400BA9A RID: 47770
				public static LocString NAME = UI.FormatAsLink("Switch", "SWITCH");

				// Token: 0x0400BA9B RID: 47771
				public static LocString DESC = "Switches can only affect buildings that come after them on a circuit.";

				// Token: 0x0400BA9C RID: 47772
				public static LocString EFFECT = "Turns " + UI.FormatAsLink("Power", "POWER") + " on or off.\n\nDoes not affect circuitry preceding the switch.";

				// Token: 0x0400BA9D RID: 47773
				public static LocString SIDESCREEN_TITLE = "Switch";

				// Token: 0x0400BA9E RID: 47774
				public static LocString TURN_ON = "Turn On";

				// Token: 0x0400BA9F RID: 47775
				public static LocString TURN_ON_TOOLTIP = "Turn On {Hotkey}";

				// Token: 0x0400BAA0 RID: 47776
				public static LocString TURN_OFF = "Turn Off";

				// Token: 0x0400BAA1 RID: 47777
				public static LocString TURN_OFF_TOOLTIP = "Turn Off {Hotkey}";
			}

			// Token: 0x02002A6E RID: 10862
			public class LOGICPOWERRELAY
			{
				// Token: 0x0400BAA2 RID: 47778
				public static LocString NAME = UI.FormatAsLink("Power Shutoff", "LOGICPOWERRELAY");

				// Token: 0x0400BAA3 RID: 47779
				public static LocString DESC = "Automated systems save power and time by removing the need for Duplicant input.";

				// Token: 0x0400BAA4 RID: 47780
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Connects to an ",
					UI.FormatAsLink("Automation", "LOGIC"),
					" grid to automatically turn ",
					UI.FormatAsLink("Power", "POWER"),
					" on or off.\n\nDoes not affect circuitry preceding the switch."
				});

				// Token: 0x0400BAA5 RID: 47781
				public static LocString LOGIC_PORT = "Kill Power";

				// Token: 0x0400BAA6 RID: 47782
				public static LocString LOGIC_PORT_ACTIVE = string.Concat(new string[]
				{
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					": Allow ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" through connected circuits"
				});

				// Token: 0x0400BAA7 RID: 47783
				public static LocString LOGIC_PORT_INACTIVE = string.Concat(new string[]
				{
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					": Prevent ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" from flowing through connected circuits"
				});
			}

			// Token: 0x02002A6F RID: 10863
			public class LOGICINTERASTEROIDSENDER
			{
				// Token: 0x0400BAA8 RID: 47784
				public static LocString NAME = UI.FormatAsLink("Automation Broadcaster", "LOGICINTERASTEROIDSENDER");

				// Token: 0x0400BAA9 RID: 47785
				public static LocString DESC = "Sends automation signals into space.";

				// Token: 0x0400BAAA RID: 47786
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" to an ",
					UI.FormatAsLink("Automation Receiver", "LOGICINTERASTEROIDRECEIVER"),
					" over vast distances in space.\n\nBoth the Automation Broadcaster and the Automation Receiver must be exposed to space to function."
				});

				// Token: 0x0400BAAB RID: 47787
				public static LocString DEFAULTNAME = "Unnamed Broadcaster";

				// Token: 0x0400BAAC RID: 47788
				public static LocString LOGIC_PORT = "Broadcasting Signal";

				// Token: 0x0400BAAD RID: 47789
				public static LocString LOGIC_PORT_ACTIVE = "Broadcasting: " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);

				// Token: 0x0400BAAE RID: 47790
				public static LocString LOGIC_PORT_INACTIVE = "Broadcasting: " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002A70 RID: 10864
			public class LOGICINTERASTEROIDRECEIVER
			{
				// Token: 0x0400BAAF RID: 47791
				public static LocString NAME = UI.FormatAsLink("Automation Receiver", "LOGICINTERASTEROIDRECEIVER");

				// Token: 0x0400BAB0 RID: 47792
				public static LocString DESC = "Receives automation signals from space.";

				// Token: 0x0400BAB1 RID: 47793
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Receives a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" from an ",
					UI.FormatAsLink("Automation Broadcaster", "LOGICINTERASTEROIDSENDER"),
					" over vast distances in space.\n\nBoth the Automation Receiver and the Automation Broadcaster must be exposed to space to function."
				});

				// Token: 0x0400BAB2 RID: 47794
				public static LocString LOGIC_PORT = "Receiving Signal";

				// Token: 0x0400BAB3 RID: 47795
				public static LocString LOGIC_PORT_ACTIVE = "Receiving: " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);

				// Token: 0x0400BAB4 RID: 47796
				public static LocString LOGIC_PORT_INACTIVE = "Receiving: " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002A71 RID: 10865
			public class TEMPERATURECONTROLLEDSWITCH
			{
				// Token: 0x0400BAB5 RID: 47797
				public static LocString NAME = UI.FormatAsLink("Thermo Switch", "TEMPERATURECONTROLLEDSWITCH");

				// Token: 0x0400BAB6 RID: 47798
				public static LocString DESC = "Automated switches can be used to manage circuits in areas where Duplicants cannot enter.";

				// Token: 0x0400BAB7 RID: 47799
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Automatically turns ",
					UI.FormatAsLink("Power", "POWER"),
					" on or off using ambient ",
					UI.FormatAsLink("Temperature", "HEAT"),
					".\n\nDoes not affect circuitry preceding the switch."
				});
			}

			// Token: 0x02002A72 RID: 10866
			public class PRESSURESWITCHLIQUID
			{
				// Token: 0x0400BAB8 RID: 47800
				public static LocString NAME = UI.FormatAsLink("Hydro Switch", "PRESSURESWITCHLIQUID");

				// Token: 0x0400BAB9 RID: 47801
				public static LocString DESC = "A hydro switch shuts off power when the liquid pressure surrounding it surpasses the set threshold.";

				// Token: 0x0400BABA RID: 47802
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Automatically turns ",
					UI.FormatAsLink("Power", "POWER"),
					" on or off using ambient ",
					UI.FormatAsLink("Liquid Pressure", "PRESSURE"),
					".\n\nDoes not affect circuitry preceding the switch.\n\nMust be submerged in ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					"."
				});
			}

			// Token: 0x02002A73 RID: 10867
			public class PRESSURESWITCHGAS
			{
				// Token: 0x0400BABB RID: 47803
				public static LocString NAME = UI.FormatAsLink("Atmo Switch", "PRESSURESWITCHGAS");

				// Token: 0x0400BABC RID: 47804
				public static LocString DESC = "An atmo switch shuts off power when the air pressure surrounding it surpasses the set threshold.";

				// Token: 0x0400BABD RID: 47805
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Automatically turns ",
					UI.FormatAsLink("Power", "POWER"),
					" on or off using ambient ",
					UI.FormatAsLink("Gas Pressure", "PRESSURE"),
					" .\n\nDoes not affect circuitry preceding the switch."
				});
			}

			// Token: 0x02002A74 RID: 10868
			public class TILE
			{
				// Token: 0x0400BABE RID: 47806
				public static LocString NAME = UI.FormatAsLink("Tile", "TILE");

				// Token: 0x0400BABF RID: 47807
				public static LocString DESC = "Tile can be used to bridge gaps and get to unreachable areas.";

				// Token: 0x0400BAC0 RID: 47808
				public static LocString EFFECT = "Used to build the walls and floors of rooms.\n\nIncreases Duplicant runspeed.";
			}

			// Token: 0x02002A75 RID: 10869
			public class WALLTOILET
			{
				// Token: 0x0400BAC1 RID: 47809
				public static LocString NAME = UI.FormatAsLink("Wall Toilet", "WALLTOILET");

				// Token: 0x0400BAC2 RID: 47810
				public static LocString DESC = "Wall Toilets transmit fewer germs to Duplicants and require no emptying.";

				// Token: 0x0400BAC3 RID: 47811
				public static LocString EFFECT = "Gives Duplicants a place to relieve themselves. Empties directly on the other side of the wall.\n\nSpreads very few " + UI.FormatAsLink("Germs", "DISEASE") + ".";
			}

			// Token: 0x02002A76 RID: 10870
			public class WATERPURIFIER
			{
				// Token: 0x0400BAC4 RID: 47812
				public static LocString NAME = UI.FormatAsLink("Water Sieve", "WATERPURIFIER");

				// Token: 0x0400BAC5 RID: 47813
				public static LocString DESC = "Sieves cannot kill germs and pass any they receive into their waste and water output.";

				// Token: 0x0400BAC6 RID: 47814
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Produces clean ",
					UI.FormatAsLink("Water", "WATER"),
					" from ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					" using ",
					UI.FormatAsLink("Sand", "SAND"),
					".\n\nProduces ",
					UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
					"."
				});
			}

			// Token: 0x02002A77 RID: 10871
			public class DISTILLATIONCOLUMN
			{
				// Token: 0x0400BAC7 RID: 47815
				public static LocString NAME = UI.FormatAsLink("Distillation Column", "DISTILLATIONCOLUMN");

				// Token: 0x0400BAC8 RID: 47816
				public static LocString DESC = "Gets hot and steamy.";

				// Token: 0x0400BAC9 RID: 47817
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Separates any ",
					UI.FormatAsLink("Contaminated Water", "DIRTYWATER"),
					" piped through it into ",
					UI.FormatAsLink("Steam", "STEAM"),
					" and ",
					UI.FormatAsLink("Polluted Dirt", "TOXICSAND"),
					"."
				});
			}

			// Token: 0x02002A78 RID: 10872
			public class WIRE
			{
				// Token: 0x0400BACA RID: 47818
				public static LocString NAME = UI.FormatAsLink("Wire", "WIRE");

				// Token: 0x0400BACB RID: 47819
				public static LocString DESC = "Electrical wire is used to connect generators, batteries, and buildings.";

				// Token: 0x0400BACC RID: 47820
				public static LocString EFFECT = "Connects buildings to " + UI.FormatAsLink("Power", "POWER") + " sources.\n\nCan be run through wall and floor tile.";
			}

			// Token: 0x02002A79 RID: 10873
			public class WIREBRIDGE
			{
				// Token: 0x0400BACD RID: 47821
				public static LocString NAME = UI.FormatAsLink("Wire Bridge", "WIREBRIDGE");

				// Token: 0x0400BACE RID: 47822
				public static LocString DESC = "Splitting generators onto separate grids can prevent overloads and wasted electricity.";

				// Token: 0x0400BACF RID: 47823
				public static LocString EFFECT = "Runs one wire section over another without joining them.\n\nCan be run through wall and floor tile.";
			}

			// Token: 0x02002A7A RID: 10874
			public class HIGHWATTAGEWIRE
			{
				// Token: 0x0400BAD0 RID: 47824
				public static LocString NAME = UI.FormatAsLink("Heavi-Watt Wire", "HIGHWATTAGEWIRE");

				// Token: 0x0400BAD1 RID: 47825
				public static LocString DESC = "Higher wattage wire is used to avoid power overloads, particularly for strong generators.";

				// Token: 0x0400BAD2 RID: 47826
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries more ",
					UI.FormatAsLink("Wattage", "POWER"),
					" than regular ",
					UI.FormatAsLink("Wire", "WIRE"),
					" without overloading.\n\nCannot be run through wall and floor tile."
				});
			}

			// Token: 0x02002A7B RID: 10875
			public class WIREBRIDGEHIGHWATTAGE
			{
				// Token: 0x0400BAD3 RID: 47827
				public static LocString NAME = UI.FormatAsLink("Heavi-Watt Joint Plate", "WIREBRIDGEHIGHWATTAGE");

				// Token: 0x0400BAD4 RID: 47828
				public static LocString DESC = "Joint plates can run Heavi-Watt wires through walls without leaking gas or liquid.";

				// Token: 0x0400BAD5 RID: 47829
				public static LocString EFFECT = "Allows " + UI.FormatAsLink("Heavi-Watt Wire", "HIGHWATTAGEWIRE") + " to be run through wall and floor tile.\n\nFunctions as regular tile.";
			}

			// Token: 0x02002A7C RID: 10876
			public class WIREREFINED
			{
				// Token: 0x0400BAD6 RID: 47830
				public static LocString NAME = UI.FormatAsLink("Conductive Wire", "WIREREFINED");

				// Token: 0x0400BAD7 RID: 47831
				public static LocString DESC = "My Duplicants prefer the look of conductive wire to the regular raggedy stuff.";

				// Token: 0x0400BAD8 RID: 47832
				public static LocString EFFECT = "Connects buildings to " + UI.FormatAsLink("Power", "POWER") + " sources.\n\nCan be run through wall and floor tile.";
			}

			// Token: 0x02002A7D RID: 10877
			public class WIREREFINEDBRIDGE
			{
				// Token: 0x0400BAD9 RID: 47833
				public static LocString NAME = UI.FormatAsLink("Conductive Wire Bridge", "WIREREFINEDBRIDGE");

				// Token: 0x0400BADA RID: 47834
				public static LocString DESC = "Splitting generators onto separate systems can prevent overloads and wasted electricity.";

				// Token: 0x0400BADB RID: 47835
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries more ",
					UI.FormatAsLink("Wattage", "POWER"),
					" than a regular ",
					UI.FormatAsLink("Wire Bridge", "WIREBRIDGE"),
					" without overloading.\n\nRuns one wire section over another without joining them.\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x02002A7E RID: 10878
			public class WIREREFINEDHIGHWATTAGE
			{
				// Token: 0x0400BADC RID: 47836
				public static LocString NAME = UI.FormatAsLink("Heavi-Watt Conductive Wire", "WIREREFINEDHIGHWATTAGE");

				// Token: 0x0400BADD RID: 47837
				public static LocString DESC = "Higher wattage wire is used to avoid power overloads, particularly for strong generators.";

				// Token: 0x0400BADE RID: 47838
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries more ",
					UI.FormatAsLink("Wattage", "POWER"),
					" than regular ",
					UI.FormatAsLink("Wire", "WIRE"),
					" without overloading.\n\nCannot be run through wall and floor tile."
				});
			}

			// Token: 0x02002A7F RID: 10879
			public class WIREREFINEDBRIDGEHIGHWATTAGE
			{
				// Token: 0x0400BADF RID: 47839
				public static LocString NAME = UI.FormatAsLink("Heavi-Watt Conductive Joint Plate", "WIREREFINEDBRIDGEHIGHWATTAGE");

				// Token: 0x0400BAE0 RID: 47840
				public static LocString DESC = "Joint plates can run Heavi-Watt wires through walls without leaking gas or liquid.";

				// Token: 0x0400BAE1 RID: 47841
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Carries more ",
					UI.FormatAsLink("Wattage", "POWER"),
					" than a regular ",
					UI.FormatAsLink("Heavi-Watt Joint Plate", "WIREBRIDGEHIGHWATTAGE"),
					" without overloading.\n\nAllows ",
					UI.FormatAsLink("Heavi-Watt Wire", "HIGHWATTAGEWIRE"),
					" to be run through wall and floor tile."
				});
			}

			// Token: 0x02002A80 RID: 10880
			public class HANDSANITIZER
			{
				// Token: 0x0400BAE2 RID: 47842
				public static LocString NAME = UI.FormatAsLink("Hand Sanitizer", "HANDSANITIZER");

				// Token: 0x0400BAE3 RID: 47843
				public static LocString DESC = "Hand sanitizers kill germs more effectively than wash basins.";

				// Token: 0x0400BAE4 RID: 47844
				public static LocString EFFECT = "Removes most " + UI.FormatAsLink("Germs", "DISEASE") + " from Duplicants.\n\nGerm-covered Duplicants use Hand Sanitizers when passing by in the selected direction.";
			}

			// Token: 0x02002A81 RID: 10881
			public class WASHBASIN
			{
				// Token: 0x0400BAE5 RID: 47845
				public static LocString NAME = UI.FormatAsLink("Wash Basin", "WASHBASIN");

				// Token: 0x0400BAE6 RID: 47846
				public static LocString DESC = "Germ spread can be reduced by building these where Duplicants often get dirty.";

				// Token: 0x0400BAE7 RID: 47847
				public static LocString EFFECT = "Removes some " + UI.FormatAsLink("Germs", "DISEASE") + " from Duplicants.\n\nGerm-covered Duplicants use Wash Basins when passing by in the selected direction.";
			}

			// Token: 0x02002A82 RID: 10882
			public class WASHSINK
			{
				// Token: 0x0400BAE8 RID: 47848
				public static LocString NAME = UI.FormatAsLink("Sink", "WASHSINK");

				// Token: 0x0400BAE9 RID: 47849
				public static LocString DESC = "Sinks are plumbed and do not need to be manually emptied or refilled.";

				// Token: 0x0400BAEA RID: 47850
				public static LocString EFFECT = "Removes " + UI.FormatAsLink("Germs", "DISEASE") + " from Duplicants.\n\nGerm-covered Duplicants use Sinks when passing by in the selected direction.";

				// Token: 0x020038B8 RID: 14520
				public class FACADES
				{
					// Token: 0x02003D32 RID: 15666
					public class DEFAULT_WASHSINK
					{
						// Token: 0x0400EF9C RID: 61340
						public static LocString NAME = UI.FormatAsLink("Sink", "WASHSINK");

						// Token: 0x0400EF9D RID: 61341
						public static LocString DESC = "Sinks are plumbed and do not need to be manually emptied or refilled.";
					}

					// Token: 0x02003D33 RID: 15667
					public class PURPLE_BRAINFAT
					{
						// Token: 0x0400EF9E RID: 61342
						public static LocString NAME = UI.FormatAsLink("Faint Purple Sink", "WASHSINK");

						// Token: 0x0400EF9F RID: 61343
						public static LocString DESC = "A refreshing splash of color for the light-headed.";
					}

					// Token: 0x02003D34 RID: 15668
					public class BLUE_BABYTEARS
					{
						// Token: 0x0400EFA0 RID: 61344
						public static LocString NAME = UI.FormatAsLink("Weepy Blue Sink", "WASHSINK");

						// Token: 0x0400EFA1 RID: 61345
						public static LocString DESC = "A calm, colorful sink for heavy-hearted Duplicants.";
					}

					// Token: 0x02003D35 RID: 15669
					public class GREEN_MUSH
					{
						// Token: 0x0400EFA2 RID: 61346
						public static LocString NAME = UI.FormatAsLink("Mush Green Sink", "WASHSINK");

						// Token: 0x0400EFA3 RID: 61347
						public static LocString DESC = "Even the soap is mush-colored.";
					}

					// Token: 0x02003D36 RID: 15670
					public class YELLOW_TARTAR
					{
						// Token: 0x0400EFA4 RID: 61348
						public static LocString NAME = UI.FormatAsLink("Ick Yellow Sink", "WASHSINK");

						// Token: 0x0400EFA5 RID: 61349
						public static LocString DESC = "The juxtaposition of 'ick' and 'clean' can be very satisfying.";
					}

					// Token: 0x02003D37 RID: 15671
					public class RED_ROSE
					{
						// Token: 0x0400EFA6 RID: 61350
						public static LocString NAME = UI.FormatAsLink("Puce Pink Sink", "WASHSINK");

						// Token: 0x0400EFA7 RID: 61351
						public static LocString DESC = "Some Duplicants say it looks like a germ-devouring mouth.";
					}
				}
			}

			// Token: 0x02002A83 RID: 10883
			public class DECONTAMINATIONSHOWER
			{
				// Token: 0x0400BAEB RID: 47851
				public static LocString NAME = UI.FormatAsLink("Decontamination Shower", "DECONTAMINATIONSHOWER");

				// Token: 0x0400BAEC RID: 47852
				public static LocString DESC = "Don't forget to decontaminate behind your ears.";

				// Token: 0x0400BAED RID: 47853
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Water", "WATER"),
					" to remove ",
					UI.FormatAsLink("Germs", "DISEASE"),
					" and ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					".\n\nDecontaminates both Duplicants and their ",
					UI.FormatAsLink("Clothing", "EQUIPMENT"),
					"."
				});
			}

			// Token: 0x02002A84 RID: 10884
			public class TILEPOI
			{
				// Token: 0x0400BAEE RID: 47854
				public static LocString NAME = UI.FormatAsLink("Tile", "TILEPOI");

				// Token: 0x0400BAEF RID: 47855
				public static LocString DESC = "";

				// Token: 0x0400BAF0 RID: 47856
				public static LocString EFFECT = "Used to build the walls and floor of rooms.";
			}

			// Token: 0x02002A85 RID: 10885
			public class POLYMERIZER
			{
				// Token: 0x0400BAF1 RID: 47857
				public static LocString NAME = UI.FormatAsLink("Polymer Press", "POLYMERIZER");

				// Token: 0x0400BAF2 RID: 47858
				public static LocString DESC = "Plastic can be used to craft unique buildings and goods.";

				// Token: 0x0400BAF3 RID: 47859
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Plastic Monomers", "PLASTIFIABLELIQUID"),
					" into raw ",
					UI.FormatAsLink("Plastic", "POLYPROPYLENE"),
					"."
				});
			}

			// Token: 0x02002A86 RID: 10886
			public class DIRECTIONALWORLDPUMPLIQUID
			{
				// Token: 0x0400BAF4 RID: 47860
				public static LocString NAME = UI.FormatAsLink("Liquid Channel", "DIRECTIONALWORLDPUMPLIQUID");

				// Token: 0x0400BAF5 RID: 47861
				public static LocString DESC = "Channels move more volume than pumps and require no power, but need sufficient pressure to function.";

				// Token: 0x0400BAF6 RID: 47862
				public static LocString EFFECT = "Directionally moves large volumes of " + UI.FormatAsLink("LIQUID", "ELEMENTS_LIQUID") + " through a channel.\n\nCan be used as floor tile and rotated before construction.";
			}

			// Token: 0x02002A87 RID: 10887
			public class STEAMTURBINE
			{
				// Token: 0x0400BAF7 RID: 47863
				public static LocString NAME = UI.FormatAsLink("[DEPRECATED] Steam Turbine", "STEAMTURBINE");

				// Token: 0x0400BAF8 RID: 47864
				public static LocString DESC = "Useful for converting the geothermal energy of magma into usable power.";

				// Token: 0x0400BAF9 RID: 47865
				public static LocString EFFECT = string.Concat(new string[]
				{
					"THIS BUILDING HAS BEEN DEPRECATED AND CANNOT BE BUILT.\n\nGenerates exceptional electrical ",
					UI.FormatAsLink("Power", "POWER"),
					" using pressurized, ",
					UI.FormatAsLink("Scalding", "HEAT"),
					" ",
					UI.FormatAsLink("Steam", "STEAM"),
					".\n\nOutputs significantly cooler ",
					UI.FormatAsLink("Steam", "STEAM"),
					" than it receives.\n\nAir pressure beneath this building must be higher than pressure above for air to flow."
				});
			}

			// Token: 0x02002A88 RID: 10888
			public class STEAMTURBINE2
			{
				// Token: 0x0400BAFA RID: 47866
				public static LocString NAME = UI.FormatAsLink("Steam Turbine", "STEAMTURBINE2");

				// Token: 0x0400BAFB RID: 47867
				public static LocString DESC = "Useful for converting the geothermal energy into usable power.";

				// Token: 0x0400BAFC RID: 47868
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Draws in ",
					UI.FormatAsLink("Steam", "STEAM"),
					" from the tiles directly below the machine's foundation and uses it to generate electrical ",
					UI.FormatAsLink("Power", "POWER"),
					".\n\nOutputs ",
					UI.FormatAsLink("Water", "WATER"),
					"."
				});

				// Token: 0x0400BAFD RID: 47869
				public static LocString HEAT_SOURCE = "Power Generation Waste";
			}

			// Token: 0x02002A89 RID: 10889
			public class STEAMENGINE
			{
				// Token: 0x0400BAFE RID: 47870
				public static LocString NAME = UI.FormatAsLink("Steam Engine", "STEAMENGINE");

				// Token: 0x0400BAFF RID: 47871
				public static LocString DESC = "Rockets can be used to send Duplicants into space and retrieve rare resources.";

				// Token: 0x0400BB00 RID: 47872
				public static LocString EFFECT = "Utilizes " + UI.FormatAsLink("Steam", "STEAM") + " to propel rockets for space exploration.\n\nThe engine of a rocket must be built first before more rocket modules may be added.";
			}

			// Token: 0x02002A8A RID: 10890
			public class STEAMENGINECLUSTER
			{
				// Token: 0x0400BB01 RID: 47873
				public static LocString NAME = UI.FormatAsLink("Steam Engine", "STEAMENGINECLUSTER");

				// Token: 0x0400BB02 RID: 47874
				public static LocString DESC = "Rockets can be used to send Duplicants into space and retrieve rare resources.";

				// Token: 0x0400BB03 RID: 47875
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Utilizes ",
					UI.FormatAsLink("Steam", "STEAM"),
					" to propel rockets for space exploration.\n\nEngine must be built via ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					". \n\nOnce the engine has been built, more rocket modules can be added."
				});
			}

			// Token: 0x02002A8B RID: 10891
			public class SOLARPANEL
			{
				// Token: 0x0400BB04 RID: 47876
				public static LocString NAME = UI.FormatAsLink("Solar Panel", "SOLARPANEL");

				// Token: 0x0400BB05 RID: 47877
				public static LocString DESC = "Solar panels convert high intensity sunlight into power and produce zero waste.";

				// Token: 0x0400BB06 RID: 47878
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Sunlight", "LIGHT"),
					" into electrical ",
					UI.FormatAsLink("Power", "POWER"),
					".\n\nMust be exposed to space."
				});
			}

			// Token: 0x02002A8C RID: 10892
			public class COMETDETECTOR
			{
				// Token: 0x0400BB07 RID: 47879
				public static LocString NAME = UI.FormatAsLink("Space Scanner", "COMETDETECTOR");

				// Token: 0x0400BB08 RID: 47880
				public static LocString DESC = "Networks of many scanners will scan more efficiently than one on its own.";

				// Token: 0x0400BB09 RID: 47881
				public static LocString EFFECT = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " to its automation circuit when it detects incoming objects from space.\n\nCan be configured to detect incoming meteor showers or returning space rockets.";
			}

			// Token: 0x02002A8D RID: 10893
			public class OILREFINERY
			{
				// Token: 0x0400BB0A RID: 47882
				public static LocString NAME = UI.FormatAsLink("Oil Refinery", "OILREFINERY");

				// Token: 0x0400BB0B RID: 47883
				public static LocString DESC = "Petroleum can only be produced from the refinement of crude oil.";

				// Token: 0x0400BB0C RID: 47884
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Converts ",
					UI.FormatAsLink("Crude Oil", "CRUDEOIL"),
					" into ",
					UI.FormatAsLink("Petroleum", "PETROLEUM"),
					" and ",
					UI.FormatAsLink("Natural Gas", "METHANE"),
					"."
				});
			}

			// Token: 0x02002A8E RID: 10894
			public class OILWELLCAP
			{
				// Token: 0x0400BB0D RID: 47885
				public static LocString NAME = UI.FormatAsLink("Oil Well", "OILWELLCAP");

				// Token: 0x0400BB0E RID: 47886
				public static LocString DESC = "Water pumped into an oil reservoir cannot be recovered.";

				// Token: 0x0400BB0F RID: 47887
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Extracts ",
					UI.FormatAsLink("Crude Oil", "CRUDEOIL"),
					" using clean ",
					UI.FormatAsLink("Water", "WATER"),
					".\n\nMust be built atop an ",
					UI.FormatAsLink("Oil Reservoir", "OIL_WELL"),
					"."
				});
			}

			// Token: 0x02002A8F RID: 10895
			public class METALREFINERY
			{
				// Token: 0x0400BB10 RID: 47888
				public static LocString NAME = UI.FormatAsLink("Metal Refinery", "METALREFINERY");

				// Token: 0x0400BB11 RID: 47889
				public static LocString DESC = "Refined metals are necessary to build advanced electronics and technologies.";

				// Token: 0x0400BB12 RID: 47890
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsLink("Refined Metals", "REFINEDMETAL"),
					" from raw ",
					UI.FormatAsLink("Metal Ore", "RAWMETAL"),
					".\n\nSignificantly ",
					UI.FormatAsLink("Heats", "HEAT"),
					" and outputs the ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" piped into it.\n\nDuplicants will not fabricate items unless recipes are queued."
				});

				// Token: 0x0400BB13 RID: 47891
				public static LocString RECIPE_DESCRIPTION = "Extracts pure {0} from {1}.";
			}

			// Token: 0x02002A90 RID: 10896
			public class MISSILEFABRICATOR
			{
				// Token: 0x0400BB14 RID: 47892
				public static LocString NAME = UI.FormatAsLink("Blastshot Maker", "MISSILEFABRICATOR");

				// Token: 0x0400BB15 RID: 47893
				public static LocString DESC = "Blastshot shells are an effective defense against incoming meteor showers.";

				// Token: 0x0400BB16 RID: 47894
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsLink("Blastshot", "MISSILELAUNCHER"),
					" from ",
					UI.FormatAsLink("Refined Metals", "REFINEDMETAL"),
					" combined with ",
					UI.FormatAsLink("Petroleum", "PETROLEUM"),
					".\n\nDuplicants will not fabricate items unless recipes are queued."
				});

				// Token: 0x0400BB17 RID: 47895
				public static LocString RECIPE_DESCRIPTION = "Produces {0} from {1} and {2}.";

				// Token: 0x0400BB18 RID: 47896
				public static LocString RECIPE_DESCRIPTION_LONGRANGE = "Produces {0} from {1}, {2}, and {3}.";
			}

			// Token: 0x02002A91 RID: 10897
			public class GLASSFORGE
			{
				// Token: 0x0400BB19 RID: 47897
				public static LocString NAME = UI.FormatAsLink("Glass Forge", "GLASSFORGE");

				// Token: 0x0400BB1A RID: 47898
				public static LocString DESC = "Glass can be used to construct window tile.";

				// Token: 0x0400BB1B RID: 47899
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsLink("Molten Glass", "MOLTENGLASS"),
					" from raw ",
					UI.FormatAsLink("Sand", "SAND"),
					".\n\nOutputs ",
					UI.FormatAsLink("High Temperature", "HEAT"),
					" ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					".\n\nDuplicants will not fabricate items unless recipes are queued."
				});

				// Token: 0x0400BB1C RID: 47900
				public static LocString RECIPE_DESCRIPTION = "Extracts pure {0} from {1}.";
			}

			// Token: 0x02002A92 RID: 10898
			public class ROCKCRUSHER
			{
				// Token: 0x0400BB1D RID: 47901
				public static LocString NAME = UI.FormatAsLink("Rock Crusher", "ROCKCRUSHER");

				// Token: 0x0400BB1E RID: 47902
				public static LocString DESC = "Rock Crushers loosen nuggets from raw ore and can process many different resources.";

				// Token: 0x0400BB1F RID: 47903
				public static LocString EFFECT = "Inefficiently produces refined materials from raw resources.\n\nDuplicants will not fabricate items unless recipes are queued.";

				// Token: 0x0400BB20 RID: 47904
				public static LocString RECIPE_DESCRIPTION = "Crushes {0} into {1}";

				// Token: 0x0400BB21 RID: 47905
				public static LocString RECIPE_DESCRIPTION_TWO_OUTPUT = "Crushes {0} into {1} and {2}";

				// Token: 0x0400BB22 RID: 47906
				public static LocString METAL_RECIPE_DESCRIPTION = "Crushes {1} into " + UI.FormatAsLink("Sand", "SAND") + " and pure {0}";

				// Token: 0x0400BB23 RID: 47907
				public static LocString LIME_RECIPE_DESCRIPTION = "Crushes {1} into {0}";

				// Token: 0x0400BB24 RID: 47908
				public static LocString LIME_FROM_LIMESTONE_RECIPE_DESCRIPTION = "Crushes {0} into {1} and a small amount of pure {2}";

				// Token: 0x0400BB25 RID: 47909
				public static LocString RESIN_FROM_AMBER_RECIPE_DESCRIPTION = "Crushes {0} into {1} and {2}, and a small amount of {3}";

				// Token: 0x0400BB26 RID: 47910
				public static LocString SAND_FROM_RAW_MINERAL_NAME = UI.FormatAsLink("Raw Mineral", "BUILDABLERAW") + " to " + UI.FormatAsLink("Sand", "SAND");

				// Token: 0x0400BB27 RID: 47911
				public static LocString SAND_FROM_RAW_MINERAL_DESCRIPTION = "Crushes " + UI.FormatAsLink("Raw Minerals", "BUILDABLERAW") + " into " + UI.FormatAsLink("Sand", "SAND");

				// Token: 0x020038B9 RID: 14521
				public class FACADES
				{
					// Token: 0x02003D38 RID: 15672
					public class DEFAULT_ROCKCRUSHER
					{
						// Token: 0x0400EFA8 RID: 61352
						public static LocString NAME = UI.FormatAsLink("Rock Crusher", "ROCKCRUSHER");

						// Token: 0x0400EFA9 RID: 61353
						public static LocString DESC = "Rock Crushers loosen nuggets from raw ore and can process many different resources.";
					}

					// Token: 0x02003D39 RID: 15673
					public class HANDS
					{
						// Token: 0x0400EFAA RID: 61354
						public static LocString NAME = UI.FormatAsLink("Punchy Rock Crusher", "ROCKCRUSHER");

						// Token: 0x0400EFAB RID: 61355
						public static LocString DESC = "Smashy smashy!";
					}

					// Token: 0x02003D3A RID: 15674
					public class TEETH
					{
						// Token: 0x0400EFAC RID: 61356
						public static LocString NAME = UI.FormatAsLink("Toothy Rock Crusher", "ROCKCRUSHER");

						// Token: 0x0400EFAD RID: 61357
						public static LocString DESC = "Not designed to handle overcooked food waste.";
					}

					// Token: 0x02003D3B RID: 15675
					public class ROUNDSTAMP
					{
						// Token: 0x0400EFAE RID: 61358
						public static LocString NAME = UI.FormatAsLink("Smooth Rock Crusher", "ROCKCRUSHER");

						// Token: 0x0400EFAF RID: 61359
						public static LocString DESC = "Inspired by the traditional mortar and pestle.";
					}

					// Token: 0x02003D3C RID: 15676
					public class SPIKEBEDS
					{
						// Token: 0x0400EFB0 RID: 61360
						public static LocString NAME = UI.FormatAsLink("Spiked Rock Crusher", "ROCKCRUSHER");

						// Token: 0x0400EFB1 RID: 61361
						public static LocString DESC = "Mashes rocks into oblivion.";
					}

					// Token: 0x02003D3D RID: 15677
					public class CHOMP
					{
						// Token: 0x0400EFB2 RID: 61362
						public static LocString NAME = UI.FormatAsLink("Mani Rock Crusher", "ROCKCRUSHER");

						// Token: 0x0400EFB3 RID: 61363
						public static LocString DESC = "Buffs rough ore into smooth little nuggets.";
					}

					// Token: 0x02003D3E RID: 15678
					public class GEARS
					{
						// Token: 0x0400EFB4 RID: 61364
						public static LocString NAME = UI.FormatAsLink("Super-Mech Rock Crusher", "ROCKCRUSHER");

						// Token: 0x0400EFB5 RID: 61365
						public static LocString DESC = "Uncrushed ore really grinds its gears.";
					}

					// Token: 0x02003D3F RID: 15679
					public class BALLOON
					{
						// Token: 0x0400EFB6 RID: 61366
						public static LocString NAME = UI.FormatAsLink("Pop-A-Rocks-E", "ROCKCRUSHER");

						// Token: 0x0400EFB7 RID: 61367
						public static LocString DESC = "Wherever there's raw ore, there's a rock crusher lurking nearby.";
					}
				}
			}

			// Token: 0x02002A93 RID: 10899
			public class SLUDGEPRESS
			{
				// Token: 0x0400BB28 RID: 47912
				public static LocString NAME = UI.FormatAsLink("Sludge Press", "SLUDGEPRESS");

				// Token: 0x0400BB29 RID: 47913
				public static LocString DESC = "What Duplicant doesn't love playing with mud?";

				// Token: 0x0400BB2A RID: 47914
				public static LocString EFFECT = "Separates " + UI.FormatAsLink("Mud", "MUD") + " and other sludges into their base elements.\n\nDuplicants will not fabricate items unless recipes are queued.";

				// Token: 0x0400BB2B RID: 47915
				public static LocString RECIPE_DESCRIPTION = "Separates {0} into its base elements.";
			}

			// Token: 0x02002A94 RID: 10900
			public class CHEMICALREFINERY
			{
				// Token: 0x0400BB2C RID: 47916
				public static LocString NAME = UI.FormatAsLink("Emulsifier", "CHEMICALREFINERY");

				// Token: 0x0400BB2D RID: 47917
				public static LocString DESC = "It's like a blender, but better.";

				// Token: 0x0400BB2E RID: 47918
				public static LocString EFFECT = "Combines " + UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID") + " and other inputs into fluid compounds.\n\nDuplicants will not fabricate emulsions unless recipes are queued.";

				// Token: 0x0400BB2F RID: 47919
				public static LocString REFINEDLIPID_RECIPE_DESCRIPTION = string.Concat(new string[]
				{
					"Biodiesel is a ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" used in ",
					UI.FormatAsLink("Power", "POWER"),
					" production."
				});

				// Token: 0x0400BB30 RID: 47920
				public static LocString SALTWATER_RECIPE_DESCRIPTION = "Salt Water is a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " with insulating and radiation-absorbing properties.";
			}

			// Token: 0x02002A95 RID: 10901
			public class SUPERMATERIALREFINERY
			{
				// Token: 0x0400BB31 RID: 47921
				public static LocString NAME = UI.FormatAsLink("Molecular Forge", "SUPERMATERIALREFINERY");

				// Token: 0x0400BB32 RID: 47922
				public static LocString DESC = "Rare materials can be procured through rocket missions into space.";

				// Token: 0x0400BB33 RID: 47923
				public static LocString EFFECT = "Processes " + UI.FormatAsLink("Rare Materials", "RAREMATERIALS") + " into advanced industrial goods.\n\nRare materials can be retrieved from space missions.\n\nDuplicants will not fabricate items unless recipes are queued.";

				// Token: 0x0400BB34 RID: 47924
				public static LocString SUPERCOOLANT_RECIPE_DESCRIPTION = "Super Coolant is an industrial-grade " + UI.FormatAsLink("Fullerene", "FULLERENE") + " coolant.";

				// Token: 0x0400BB35 RID: 47925
				public static LocString SUPERINSULATOR_RECIPE_DESCRIPTION = string.Concat(new string[]
				{
					"Insulite reduces ",
					UI.FormatAsLink("Heat Transfer", "HEAT"),
					" and is composed of recrystallized ",
					UI.FormatAsLink("Abyssalite", "KATAIRITE"),
					"."
				});

				// Token: 0x0400BB36 RID: 47926
				public static LocString TEMPCONDUCTORSOLID_RECIPE_DESCRIPTION = "Thermium is an industrial metal alloy formulated to maximize " + UI.FormatAsLink("Heat Transfer", "HEAT") + " and thermal dispersion.";

				// Token: 0x0400BB37 RID: 47927
				public static LocString VISCOGEL_RECIPE_DESCRIPTION = "Visco-Gel Fluid is a " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " polymer with high surface tension.";

				// Token: 0x0400BB38 RID: 47928
				public static LocString YELLOWCAKE_RECIPE_DESCRIPTION = "Yellowcake is a " + UI.FormatAsLink("Solid Material", "ELEMENTS_SOLID") + " used in uranium enrichment.";

				// Token: 0x0400BB39 RID: 47929
				public static LocString FULLERENE_RECIPE_DESCRIPTION = string.Concat(new string[]
				{
					"Fullerene is a ",
					UI.FormatAsLink("Solid Material", "ELEMENTS_SOLID"),
					" used in the production of ",
					UI.FormatAsLink("Super Coolant", "SUPERCOOLANT"),
					"."
				});

				// Token: 0x0400BB3A RID: 47930
				public static LocString HARDPLASTIC_RECIPE_DESCRIPTION = "Plastium is a highly heat-resistant, plastic-like " + UI.FormatAsLink("Solid Material", "ELEMENTS_SOLID") + " used for space buildings.";

				// Token: 0x0400BB3B RID: 47931
				public static LocString SELF_CHARGING_POWERBANK_RECIPE_DESCRIPTION = "Atomic Power Banks are portable, self-charging units used for isolated " + UI.FormatAsLink("Power", "POWER") + " grids.";
			}

			// Token: 0x02002A96 RID: 10902
			public class THERMALBLOCK
			{
				// Token: 0x0400BB3C RID: 47932
				public static LocString NAME = UI.FormatAsLink("Tempshift Plate", "THERMALBLOCK");

				// Token: 0x0400BB3D RID: 47933
				public static LocString DESC = "The thermal properties of construction materials determine their heat retention.";

				// Token: 0x0400BB3E RID: 47934
				public static LocString EFFECT = "Accelerates or buffers " + UI.FormatAsLink("Heat", "HEAT") + " dispersal based on the construction material used.\n\nHas a small area of effect.";
			}

			// Token: 0x02002A97 RID: 10903
			public class POWERCONTROLSTATION
			{
				// Token: 0x0400BB3F RID: 47935
				public static LocString NAME = UI.FormatAsLink("Power Control Station", "POWERCONTROLSTATION");

				// Token: 0x0400BB40 RID: 47936
				public static LocString DESC = "Only one Duplicant may be assigned to a station at a time.";

				// Token: 0x0400BB41 RID: 47937
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Produces ",
					ITEMS.INDUSTRIAL_PRODUCTS.POWER_STATION_TOOLS.NAME,
					" to increase the ",
					UI.FormatAsLink("Power", "POWER"),
					" output of generators.\n\nAssigned Duplicants must possess the ",
					UI.FormatAsLink("Tune Up", "TECHNICALS2"),
					" trait."
				});
			}

			// Token: 0x02002A98 RID: 10904
			public class FARMSTATION
			{
				// Token: 0x0400BB42 RID: 47938
				public static LocString NAME = UI.FormatAsLink("Farm Station", "FARMSTATION");

				// Token: 0x0400BB43 RID: 47939
				public static LocString DESC = "This station only has an effect on crops grown within the same room.";

				// Token: 0x0400BB44 RID: 47940
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Produces ",
					UI.FormatAsLink("Micronutrient Fertilizer", "FARM_STATION_TOOLS"),
					" to increase ",
					UI.FormatAsLink("Plant", "PLANTS"),
					" growth rates.\n\nAssigned Duplicants must possess the ",
					UI.FormatAsLink("Crop Tending", "FARMING2"),
					" trait.\n\nThis building is a necessary component of the Greenhouse room."
				});
			}

			// Token: 0x02002A99 RID: 10905
			public class FISHDELIVERYPOINT
			{
				// Token: 0x0400BB45 RID: 47941
				public static LocString NAME = UI.FormatAsLink("Fish Release", "FISHDELIVERYPOINT");

				// Token: 0x0400BB46 RID: 47942
				public static LocString DESC = "A fish release must be built in liquid to prevent released fish from suffocating.";

				// Token: 0x0400BB47 RID: 47943
				public static LocString EFFECT = "Releases trapped " + UI.FormatAsLink("Pacu", "PACU") + " back into the world.\n\nCan be used multiple times.";
			}

			// Token: 0x02002A9A RID: 10906
			public class FISHFEEDER
			{
				// Token: 0x0400BB48 RID: 47944
				public static LocString NAME = UI.FormatAsLink("Fish Feeder", "FISHFEEDER");

				// Token: 0x0400BB49 RID: 47945
				public static LocString DESC = "Build this feeder above a body of water to feed the fish within.";

				// Token: 0x0400BB4A RID: 47946
				public static LocString EFFECT = "Automatically dispenses stored " + UI.FormatAsLink("Critter", "CREATURES") + " food into the area below.\n\nDispenses continuously as food is consumed.";
			}

			// Token: 0x02002A9B RID: 10907
			public class FISHTRAP
			{
				// Token: 0x0400BB4B RID: 47947
				public static LocString NAME = UI.FormatAsLink("Fish Trap", "FISHTRAP");

				// Token: 0x0400BB4C RID: 47948
				public static LocString DESC = "Trapped fish will automatically be bagged for transport.";

				// Token: 0x0400BB4D RID: 47949
				public static LocString EFFECT = "Attracts and traps swimming " + UI.FormatAsLink("Pacu", "PACU") + ".\n\nSingle use.";
			}

			// Token: 0x02002A9C RID: 10908
			public class RANCHSTATION
			{
				// Token: 0x0400BB4E RID: 47950
				public static LocString NAME = UI.FormatAsLink("Grooming Station", "RANCHSTATION");

				// Token: 0x0400BB4F RID: 47951
				public static LocString DESC = "A groomed critter is a happy, healthy, productive critter.";

				// Token: 0x0400BB50 RID: 47952
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows the assigned ",
					UI.FormatAsLink("Rancher", "RANCHER"),
					" to care for ",
					UI.FormatAsLink("Critters", "CREATURES"),
					".\n\nAssigned Duplicants must possess the ",
					UI.FormatAsLink("Critter Ranching", "RANCHING1"),
					" skill."
				});
			}

			// Token: 0x02002A9D RID: 10909
			public class MACHINESHOP
			{
				// Token: 0x0400BB51 RID: 47953
				public static LocString NAME = UI.FormatAsLink("Mechanics Station", "MACHINESHOP");

				// Token: 0x0400BB52 RID: 47954
				public static LocString DESC = "Duplicants will only improve the efficiency of buildings in the same room as this station.";

				// Token: 0x0400BB53 RID: 47955
				public static LocString EFFECT = "Allows the assigned " + UI.FormatAsLink("Engineer", "MACHINE_TECHNICIAN") + " to improve building production efficiency.\n\nThis building is a necessary component of the Machine Shop room.";
			}

			// Token: 0x02002A9E RID: 10910
			public class LOGICWIRE
			{
				// Token: 0x0400BB54 RID: 47956
				public static LocString NAME = UI.FormatAsLink("Automation Wire", "LOGICWIRE");

				// Token: 0x0400BB55 RID: 47957
				public static LocString DESC = "Automation wire is used to connect building ports to automation gates.";

				// Token: 0x0400BB56 RID: 47958
				public static LocString EFFECT = "Connects buildings to " + UI.FormatAsLink("Sensors", "LOGIC") + ".\n\nCan be run through wall and floor tile.";
			}

			// Token: 0x02002A9F RID: 10911
			public class LOGICRIBBON
			{
				// Token: 0x0400BB57 RID: 47959
				public static LocString NAME = UI.FormatAsLink("Automation Ribbon", "LOGICRIBBON");

				// Token: 0x0400BB58 RID: 47960
				public static LocString DESC = "Logic ribbons use significantly less space to carry multiple automation signals.";

				// Token: 0x0400BB59 RID: 47961
				public static LocString EFFECT = string.Concat(new string[]
				{
					"A 4-Bit ",
					BUILDINGS.PREFABS.LOGICWIRE.NAME,
					" which can carry up to four automation signals.\n\nUse a ",
					UI.FormatAsLink("Ribbon Writer", "LOGICRIBBONWRITER"),
					" to output to multiple Bits, and a ",
					UI.FormatAsLink("Ribbon Reader", "LOGICRIBBONREADER"),
					" to input from multiple Bits."
				});
			}

			// Token: 0x02002AA0 RID: 10912
			public class LOGICWIREBRIDGE
			{
				// Token: 0x0400BB5A RID: 47962
				public static LocString NAME = UI.FormatAsLink("Automation Wire Bridge", "LOGICWIREBRIDGE");

				// Token: 0x0400BB5B RID: 47963
				public static LocString DESC = "Wire bridges allow multiple automation grids to exist in a small area without connecting.";

				// Token: 0x0400BB5C RID: 47964
				public static LocString EFFECT = "Runs one " + UI.FormatAsLink("Automation Wire", "LOGICWIRE") + " section over another without joining them.\n\nCan be run through wall and floor tile.";

				// Token: 0x0400BB5D RID: 47965
				public static LocString LOGIC_PORT = "Transmit Signal";

				// Token: 0x0400BB5E RID: 47966
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Pass through the " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);

				// Token: 0x0400BB5F RID: 47967
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Pass through the " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AA1 RID: 10913
			public class LOGICRIBBONBRIDGE
			{
				// Token: 0x0400BB60 RID: 47968
				public static LocString NAME = UI.FormatAsLink("Automation Ribbon Bridge", "LOGICRIBBONBRIDGE");

				// Token: 0x0400BB61 RID: 47969
				public static LocString DESC = "Wire bridges allow multiple automation grids to exist in a small area without connecting.";

				// Token: 0x0400BB62 RID: 47970
				public static LocString EFFECT = "Runs one " + UI.FormatAsLink("Automation Ribbon", "LOGICRIBBON") + " section over another without joining them.\n\nCan be run through wall and floor tile.";

				// Token: 0x0400BB63 RID: 47971
				public static LocString LOGIC_PORT = "Transmit Signal";

				// Token: 0x0400BB64 RID: 47972
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Pass through the " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);

				// Token: 0x0400BB65 RID: 47973
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Pass through the " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AA2 RID: 10914
			public class LOGICGATEAND
			{
				// Token: 0x0400BB66 RID: 47974
				public static LocString NAME = UI.FormatAsLink("AND Gate", "LOGICGATEAND");

				// Token: 0x0400BB67 RID: 47975
				public static LocString DESC = "This gate outputs a Green Signal when both its inputs are receiving Green Signals at the same time.";

				// Token: 0x0400BB68 RID: 47976
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Outputs a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" when both Input A <b>AND</b> Input B are receiving ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					".\n\nOutputs a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when even one Input is receiving ",
					UI.FormatAsAutomationState("Red", UI.AutomationState.Standby),
					"."
				});

				// Token: 0x0400BB69 RID: 47977
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x0400BB6A RID: 47978
				public static LocString OUTPUT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if both Inputs are receiving " + UI.FormatAsAutomationState("Green", UI.AutomationState.Active);

				// Token: 0x0400BB6B RID: 47979
				public static LocString OUTPUT_INACTIVE = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " if any Input is receiving " + UI.FormatAsAutomationState("Red", UI.AutomationState.Standby);
			}

			// Token: 0x02002AA3 RID: 10915
			public class LOGICGATEOR
			{
				// Token: 0x0400BB6C RID: 47980
				public static LocString NAME = UI.FormatAsLink("OR Gate", "LOGICGATEOR");

				// Token: 0x0400BB6D RID: 47981
				public static LocString DESC = "This gate outputs a Green Signal if receiving one or more Green Signals.";

				// Token: 0x0400BB6E RID: 47982
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Outputs a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if at least one of Input A <b>OR</b> Input B is receiving ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					".\n\nOutputs a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when neither Input A or Input B are receiving ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					"."
				});

				// Token: 0x0400BB6F RID: 47983
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x0400BB70 RID: 47984
				public static LocString OUTPUT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if any Input is receiving " + UI.FormatAsAutomationState("Green", UI.AutomationState.Active);

				// Token: 0x0400BB71 RID: 47985
				public static LocString OUTPUT_INACTIVE = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " if both Inputs are receiving " + UI.FormatAsAutomationState("Red", UI.AutomationState.Standby);
			}

			// Token: 0x02002AA4 RID: 10916
			public class LOGICGATENOT
			{
				// Token: 0x0400BB72 RID: 47986
				public static LocString NAME = UI.FormatAsLink("NOT Gate", "LOGICGATENOT");

				// Token: 0x0400BB73 RID: 47987
				public static LocString DESC = "This gate reverses automation signals, turning a Green Signal into a Red Signal and vice versa.";

				// Token: 0x0400BB74 RID: 47988
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Outputs a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the Input is receiving a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					".\n\nOutputs a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when its Input is receiving a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					"."
				});

				// Token: 0x0400BB75 RID: 47989
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x0400BB76 RID: 47990
				public static LocString OUTPUT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if receiving " + UI.FormatAsAutomationState("Red", UI.AutomationState.Standby);

				// Token: 0x0400BB77 RID: 47991
				public static LocString OUTPUT_INACTIVE = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " if receiving " + UI.FormatAsAutomationState("Green", UI.AutomationState.Active);
			}

			// Token: 0x02002AA5 RID: 10917
			public class LOGICGATEXOR
			{
				// Token: 0x0400BB78 RID: 47992
				public static LocString NAME = UI.FormatAsLink("XOR Gate", "LOGICGATEXOR");

				// Token: 0x0400BB79 RID: 47993
				public static LocString DESC = "This gate outputs a Green Signal if exactly one of its Inputs is receiving a Green Signal.";

				// Token: 0x0400BB7A RID: 47994
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Outputs a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if exactly one of its Inputs is receiving ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					".\n\nOutputs a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" if both or neither Inputs are receiving a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					"."
				});

				// Token: 0x0400BB7B RID: 47995
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x0400BB7C RID: 47996
				public static LocString OUTPUT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if exactly one of its Inputs is receiving " + UI.FormatAsAutomationState("Green", UI.AutomationState.Active);

				// Token: 0x0400BB7D RID: 47997
				public static LocString OUTPUT_INACTIVE = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " if both Input signals match (any color)";
			}

			// Token: 0x02002AA6 RID: 10918
			public class LOGICGATEBUFFER
			{
				// Token: 0x0400BB7E RID: 47998
				public static LocString NAME = UI.FormatAsLink("BUFFER Gate", "LOGICGATEBUFFER");

				// Token: 0x0400BB7F RID: 47999
				public static LocString DESC = "This gate continues outputting a Green Signal for a short time after the gate stops receiving a Green Signal.";

				// Token: 0x0400BB80 RID: 48000
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Outputs a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if the Input is receiving a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					".\n\nContinues sending a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" for an amount of buffer time after the Input receives a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					"."
				});

				// Token: 0x0400BB81 RID: 48001
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x0400BB82 RID: 48002
				public static LocString OUTPUT_ACTIVE = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" while receiving ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					". After receiving ",
					UI.FormatAsAutomationState("Red", UI.AutomationState.Standby),
					", will continue sending ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					" until the timer has expired"
				});

				// Token: 0x0400BB83 RID: 48003
				public static LocString OUTPUT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ".";
			}

			// Token: 0x02002AA7 RID: 10919
			public class LOGICGATEFILTER
			{
				// Token: 0x0400BB84 RID: 48004
				public static LocString NAME = UI.FormatAsLink("FILTER Gate", "LOGICGATEFILTER");

				// Token: 0x0400BB85 RID: 48005
				public static LocString DESC = "This gate only lets a Green Signal through if its Input has received a Green Signal that lasted longer than the selected filter time.";

				// Token: 0x0400BB86 RID: 48006
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Only lets a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" through if the Input has received a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" for longer than the selected filter time.\n\nWill continue outputting a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" if the ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" did not last long enough."
				});

				// Token: 0x0400BB87 RID: 48007
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x0400BB88 RID: 48008
				public static LocString OUTPUT_ACTIVE = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" after receiving ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					" for longer than the selected filter timer"
				});

				// Token: 0x0400BB89 RID: 48009
				public static LocString OUTPUT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ".";
			}

			// Token: 0x02002AA8 RID: 10920
			public class LOGICMEMORY
			{
				// Token: 0x0400BB8A RID: 48010
				public static LocString NAME = UI.FormatAsLink("Memory Toggle", "LOGICMEMORY");

				// Token: 0x0400BB8B RID: 48011
				public static LocString DESC = "A Memory stores a Green Signal received in the Set Port (S) until the Reset Port (R) receives a Green Signal.";

				// Token: 0x0400BB8C RID: 48012
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Contains an internal Memory, and will output whatever signal is stored in that Memory.\n\nSignals sent to the Inputs <i>only</i> affect the Memory, and do not pass through to the Output. \n\nSending a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" to the Set Port (S) will set the memory to ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					". \n\nSending a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" to the Reset Port (R) will reset the memory back to ",
					UI.FormatAsAutomationState("Red", UI.AutomationState.Standby),
					"."
				});

				// Token: 0x0400BB8D RID: 48013
				public static LocString STATUS_ITEM_VALUE = "Current Value: {0}";

				// Token: 0x0400BB8E RID: 48014
				public static LocString READ_PORT = "MEMORY OUTPUT";

				// Token: 0x0400BB8F RID: 48015
				public static LocString READ_PORT_ACTIVE = "Outputs a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the internal Memory is set to " + UI.FormatAsAutomationState("Green", UI.AutomationState.Active);

				// Token: 0x0400BB90 RID: 48016
				public static LocString READ_PORT_INACTIVE = "Outputs a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " if the internal Memory is set to " + UI.FormatAsAutomationState("Red", UI.AutomationState.Standby);

				// Token: 0x0400BB91 RID: 48017
				public static LocString SET_PORT = "SET PORT (S)";

				// Token: 0x0400BB92 RID: 48018
				public static LocString SET_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Set the internal Memory to " + UI.FormatAsAutomationState("Green", UI.AutomationState.Active);

				// Token: 0x0400BB93 RID: 48019
				public static LocString SET_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": No effect";

				// Token: 0x0400BB94 RID: 48020
				public static LocString RESET_PORT = "RESET PORT (R)";

				// Token: 0x0400BB95 RID: 48021
				public static LocString RESET_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Reset the internal Memory to " + UI.FormatAsAutomationState("Red", UI.AutomationState.Standby);

				// Token: 0x0400BB96 RID: 48022
				public static LocString RESET_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": No effect";
			}

			// Token: 0x02002AA9 RID: 10921
			public class LOGICGATEMULTIPLEXER
			{
				// Token: 0x0400BB97 RID: 48023
				public static LocString NAME = UI.FormatAsLink("Signal Selector", "LOGICGATEMULTIPLEXER");

				// Token: 0x0400BB98 RID: 48024
				public static LocString DESC = "Signal Selectors can be used to select which automation signal is relevant to pass through to a given circuit";

				// Token: 0x0400BB99 RID: 48025
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Select which one of four Input signals should be sent out the Output, using Control Inputs.\n\nSend a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" to the two Control Inputs to determine which Input is selected."
				});

				// Token: 0x0400BB9A RID: 48026
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x0400BB9B RID: 48027
				public static LocString OUTPUT_ACTIVE = string.Concat(new string[]
				{
					"Receives a ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					" or ",
					UI.FormatAsAutomationState("Red", UI.AutomationState.Standby),
					" signal from the selected input"
				});

				// Token: 0x0400BB9C RID: 48028
				public static LocString OUTPUT_INACTIVE = "Nothing";
			}

			// Token: 0x02002AAA RID: 10922
			public class LOGICGATEDEMULTIPLEXER
			{
				// Token: 0x0400BB9D RID: 48029
				public static LocString NAME = UI.FormatAsLink("Signal Distributor", "LOGICGATEDEMULTIPLEXER");

				// Token: 0x0400BB9E RID: 48030
				public static LocString DESC = "Signal Distributors can be used to choose which circuit should receive a given automation signal.";

				// Token: 0x0400BB9F RID: 48031
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Route a single Input signal out one of four possible Outputs, based on the selection made by the Control Inputs.\n\nSend a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" to the two Control Inputs to determine which Output is selected."
				});

				// Token: 0x0400BBA0 RID: 48032
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x0400BBA1 RID: 48033
				public static LocString OUTPUT_ACTIVE = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					" or ",
					UI.FormatAsAutomationState("Red", UI.AutomationState.Standby),
					" signal to the selected output"
				});

				// Token: 0x0400BBA2 RID: 48034
				public static LocString OUTPUT_INACTIVE = "Nothing";
			}

			// Token: 0x02002AAB RID: 10923
			public class LOGICSWITCH
			{
				// Token: 0x0400BBA3 RID: 48035
				public static LocString NAME = UI.FormatAsLink("Signal Switch", "LOGICSWITCH");

				// Token: 0x0400BBA4 RID: 48036
				public static LocString DESC = "Signal switches don't turn grids on and off like power switches, but add an extra signal.";

				// Token: 0x0400BBA5 RID: 48037
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" on an ",
					UI.FormatAsLink("Automation", "LOGIC"),
					" grid."
				});

				// Token: 0x0400BBA6 RID: 48038
				public static LocString SIDESCREEN_TITLE = "Signal Switch";

				// Token: 0x0400BBA7 RID: 48039
				public static LocString LOGIC_PORT = "Signal Toggle";

				// Token: 0x0400BBA8 RID: 48040
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if toggled ON";

				// Token: 0x0400BBA9 RID: 48041
				public static LocString LOGIC_PORT_INACTIVE = "Sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " if toggled OFF";
			}

			// Token: 0x02002AAC RID: 10924
			public class LOGICPRESSURESENSORGAS
			{
				// Token: 0x0400BBAA RID: 48042
				public static LocString NAME = UI.FormatAsLink("Atmo Sensor", "LOGICPRESSURESENSORGAS");

				// Token: 0x0400BBAB RID: 48043
				public static LocString DESC = "Atmo sensors can be used to prevent excess oxygen production and overpressurization.";

				// Token: 0x0400BBAC RID: 48044
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" pressure enters the chosen range."
				});

				// Token: 0x0400BBAD RID: 48045
				public static LocString LOGIC_PORT = UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " Pressure";

				// Token: 0x0400BBAE RID: 48046
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if Gas pressure is within the selected range";

				// Token: 0x0400BBAF RID: 48047
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AAD RID: 10925
			public class LOGICPRESSURESENSORLIQUID
			{
				// Token: 0x0400BBB0 RID: 48048
				public static LocString NAME = UI.FormatAsLink("Hydro Sensor", "LOGICPRESSURESENSORLIQUID");

				// Token: 0x0400BBB1 RID: 48049
				public static LocString DESC = "A hydro sensor can tell a pump to refill its basin as soon as it contains too little liquid.";

				// Token: 0x0400BBB2 RID: 48050
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" pressure enters the chosen range.\n\nMust be submerged in ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					"."
				});

				// Token: 0x0400BBB3 RID: 48051
				public static LocString LOGIC_PORT = UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " Pressure";

				// Token: 0x0400BBB4 RID: 48052
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if Liquid pressure is within the selected range";

				// Token: 0x0400BBB5 RID: 48053
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AAE RID: 10926
			public class LOGICTEMPERATURESENSOR
			{
				// Token: 0x0400BBB6 RID: 48054
				public static LocString NAME = UI.FormatAsLink("Thermo Sensor", "LOGICTEMPERATURESENSOR");

				// Token: 0x0400BBB7 RID: 48055
				public static LocString DESC = "Thermo sensors can disable buildings when they approach dangerous temperatures.";

				// Token: 0x0400BBB8 RID: 48056
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when ambient ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" enters the chosen range."
				});

				// Token: 0x0400BBB9 RID: 48057
				public static LocString LOGIC_PORT = "Ambient " + UI.FormatAsLink("Temperature", "HEAT");

				// Token: 0x0400BBBA RID: 48058
				public static LocString LOGIC_PORT_ACTIVE = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if ambient ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" is within the selected range"
				});

				// Token: 0x0400BBBB RID: 48059
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AAF RID: 10927
			public class LOGICLIGHTSENSOR
			{
				// Token: 0x0400BBBC RID: 48060
				public static LocString NAME = UI.FormatAsLink("Light Sensor", "LOGICLIGHTSENSOR");

				// Token: 0x0400BBBD RID: 48061
				public static LocString DESC = "Light sensors can tell surface bunker doors above solar panels to open or close based on solar light levels.";

				// Token: 0x0400BBBE RID: 48062
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when ambient ",
					UI.FormatAsLink("Brightness", "LIGHT"),
					" enters the chosen range."
				});

				// Token: 0x0400BBBF RID: 48063
				public static LocString LOGIC_PORT = "Ambient " + UI.FormatAsLink("Brightness", "LIGHT");

				// Token: 0x0400BBC0 RID: 48064
				public static LocString LOGIC_PORT_ACTIVE = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if ambient ",
					UI.FormatAsLink("Brightness", "LIGHT"),
					" is within the selected range"
				});

				// Token: 0x0400BBC1 RID: 48065
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AB0 RID: 10928
			public class LOGICWATTAGESENSOR
			{
				// Token: 0x0400BBC2 RID: 48066
				public static LocString NAME = UI.FormatAsLink("Wattage Sensor", "LOGICWATTSENSOR");

				// Token: 0x0400BBC3 RID: 48067
				public static LocString DESC = "Wattage sensors can send a signal when a building has switched on or off.";

				// Token: 0x0400BBC4 RID: 48068
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when ",
					UI.FormatAsLink("Wattage", "POWER"),
					" consumed enters the chosen range."
				});

				// Token: 0x0400BBC5 RID: 48069
				public static LocString LOGIC_PORT = "Consumed " + UI.FormatAsLink("Wattage", "POWER");

				// Token: 0x0400BBC6 RID: 48070
				public static LocString LOGIC_PORT_ACTIVE = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if current ",
					UI.FormatAsLink("Wattage", "POWER"),
					" is within the selected range"
				});

				// Token: 0x0400BBC7 RID: 48071
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AB1 RID: 10929
			public class LOGICHEPSENSOR
			{
				// Token: 0x0400BBC8 RID: 48072
				public static LocString NAME = UI.FormatAsLink("Radbolt Sensor", "LOGICHEPSENSOR");

				// Token: 0x0400BBC9 RID: 48073
				public static LocString DESC = "Radbolt sensors can send a signal when a Radbolt passes over them.";

				// Token: 0x0400BBCA RID: 48074
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when Radbolts detected enters the chosen range."
				});

				// Token: 0x0400BBCB RID: 48075
				public static LocString LOGIC_PORT = "Detected Radbolts";

				// Token: 0x0400BBCC RID: 48076
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if detected Radbolts are within the selected range";

				// Token: 0x0400BBCD RID: 48077
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AB2 RID: 10930
			public class LOGICTIMEOFDAYSENSOR
			{
				// Token: 0x0400BBCE RID: 48078
				public static LocString NAME = UI.FormatAsLink("Cycle Sensor", "LOGICTIMEOFDAYSENSOR");

				// Token: 0x0400BBCF RID: 48079
				public static LocString DESC = "Cycle sensors ensure systems always turn on at the same time, day or night, every cycle.";

				// Token: 0x0400BBD0 RID: 48080
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sets an automatic ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" and ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" schedule within one day-night cycle."
				});

				// Token: 0x0400BBD1 RID: 48081
				public static LocString LOGIC_PORT = "Cycle Time";

				// Token: 0x0400BBD2 RID: 48082
				public static LocString LOGIC_PORT_ACTIVE = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if current time is within the selected ",
					UI.FormatAsAutomationState("Green", UI.AutomationState.Active),
					" range"
				});

				// Token: 0x0400BBD3 RID: 48083
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AB3 RID: 10931
			public class LOGICTIMERSENSOR
			{
				// Token: 0x0400BBD4 RID: 48084
				public static LocString NAME = UI.FormatAsLink("Timer Sensor", "LOGICTIMERSENSOR");

				// Token: 0x0400BBD5 RID: 48085
				public static LocString DESC = "Timer sensors create automation schedules for very short or very long periods of time.";

				// Token: 0x0400BBD6 RID: 48086
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Creates a timer to send ",
					UI.FormatAsAutomationState("Green Signals", UI.AutomationState.Active),
					" and ",
					UI.FormatAsAutomationState("Red Signals", UI.AutomationState.Standby),
					" for specific amounts of time."
				});

				// Token: 0x0400BBD7 RID: 48087
				public static LocString LOGIC_PORT = "Timer Schedule";

				// Token: 0x0400BBD8 RID: 48088
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " for the selected amount of Green time";

				// Token: 0x0400BBD9 RID: 48089
				public static LocString LOGIC_PORT_INACTIVE = "Then, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " for the selected amount of Red time";
			}

			// Token: 0x02002AB4 RID: 10932
			public class LOGICCRITTERCOUNTSENSOR
			{
				// Token: 0x0400BBDA RID: 48090
				public static LocString NAME = UI.FormatAsLink("Critter Sensor", "LOGICCRITTERCOUNTSENSOR");

				// Token: 0x0400BBDB RID: 48091
				public static LocString DESC = "Detecting critter populations can help adjust their automated feeding and care regimens.";

				// Token: 0x0400BBDC RID: 48092
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" based on the number of eggs and critters in a room."
				});

				// Token: 0x0400BBDD RID: 48093
				public static LocString LOGIC_PORT = "Critter Count";

				// Token: 0x0400BBDE RID: 48094
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the number of Critters and Eggs in the Room is greater than the selected threshold.";

				// Token: 0x0400BBDF RID: 48095
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x0400BBE0 RID: 48096
				public static LocString SIDESCREEN_TITLE = "Critter Sensor";

				// Token: 0x0400BBE1 RID: 48097
				public static LocString COUNT_CRITTER_LABEL = "Count Critters";

				// Token: 0x0400BBE2 RID: 48098
				public static LocString COUNT_EGG_LABEL = "Count Eggs";
			}

			// Token: 0x02002AB5 RID: 10933
			public class LOGICCLUSTERLOCATIONSENSOR
			{
				// Token: 0x0400BBE3 RID: 48099
				public static LocString NAME = UI.FormatAsLink("Starmap Location Sensor", "LOGICCLUSTERLOCATIONSENSOR");

				// Token: 0x0400BBE4 RID: 48100
				public static LocString DESC = "Starmap Location sensors can signal when a spacecraft is at a certain location";

				// Token: 0x0400BBE5 RID: 48101
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Send ",
					UI.FormatAsAutomationState("Green Signals", UI.AutomationState.Active),
					" at the chosen Starmap locations and ",
					UI.FormatAsAutomationState("Red Signals", UI.AutomationState.Standby),
					" everywhere else."
				});

				// Token: 0x0400BBE6 RID: 48102
				public static LocString LOGIC_PORT = "Starmap Location Sensor";

				// Token: 0x0400BBE7 RID: 48103
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + "when a spacecraft is at the chosen Starmap locations";

				// Token: 0x0400BBE8 RID: 48104
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AB6 RID: 10934
			public class LOGICDUPLICANTSENSOR
			{
				// Token: 0x0400BBE9 RID: 48105
				public static LocString NAME = UI.FormatAsLink("Duplicant Motion Sensor", "DUPLICANTSENSOR");

				// Token: 0x0400BBEA RID: 48106
				public static LocString DESC = "Motion sensors save power by only enabling buildings when Duplicants are nearby.";

				// Token: 0x0400BBEB RID: 48107
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" based on whether a Duplicant is in the sensor's range."
				});

				// Token: 0x0400BBEC RID: 48108
				public static LocString LOGIC_PORT = "Duplicant Motion Sensor";

				// Token: 0x0400BBED RID: 48109
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " while a Duplicant is in the sensor's tile range";

				// Token: 0x0400BBEE RID: 48110
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AB7 RID: 10935
			public class LOGICDISEASESENSOR
			{
				// Token: 0x0400BBEF RID: 48111
				public static LocString NAME = UI.FormatAsLink("Germ Sensor", "LOGICDISEASESENSOR");

				// Token: 0x0400BBF0 RID: 48112
				public static LocString DESC = "Detecting germ populations can help block off or clean up dangerous areas.";

				// Token: 0x0400BBF1 RID: 48113
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" based on quantity of surrounding ",
					UI.FormatAsLink("Germs", "DISEASE"),
					"."
				});

				// Token: 0x0400BBF2 RID: 48114
				public static LocString LOGIC_PORT = UI.FormatAsLink("Germ", "DISEASE") + " Count";

				// Token: 0x0400BBF3 RID: 48115
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the number of Germs is within the selected range";

				// Token: 0x0400BBF4 RID: 48116
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AB8 RID: 10936
			public class LOGICELEMENTSENSORGAS
			{
				// Token: 0x0400BBF5 RID: 48117
				public static LocString NAME = UI.FormatAsLink("Gas Element Sensor", "LOGICELEMENTSENSORGAS");

				// Token: 0x0400BBF6 RID: 48118
				public static LocString DESC = "These sensors can detect the presence of a specific gas and alter systems accordingly.";

				// Token: 0x0400BBF7 RID: 48119
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" when the selected ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" is detected on this sensor's tile.\n\nSends a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when the selected ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" is not present."
				});

				// Token: 0x0400BBF8 RID: 48120
				public static LocString LOGIC_PORT = "Specific " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " Presence";

				// Token: 0x0400BBF9 RID: 48121
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the selected Gas is detected";

				// Token: 0x0400BBFA RID: 48122
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AB9 RID: 10937
			public class LOGICELEMENTSENSORLIQUID
			{
				// Token: 0x0400BBFB RID: 48123
				public static LocString NAME = UI.FormatAsLink("Liquid Element Sensor", "LOGICELEMENTSENSORLIQUID");

				// Token: 0x0400BBFC RID: 48124
				public static LocString DESC = "These sensors can detect the presence of a specific liquid and alter systems accordingly.";

				// Token: 0x0400BBFD RID: 48125
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" when the selected ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" is detected on this sensor's tile.\n\nSends a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when the selected ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" is not present."
				});

				// Token: 0x0400BBFE RID: 48126
				public static LocString LOGIC_PORT = "Specific " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " Presence";

				// Token: 0x0400BBFF RID: 48127
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the selected Liquid is detected";

				// Token: 0x0400BC00 RID: 48128
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002ABA RID: 10938
			public class LOGICRADIATIONSENSOR
			{
				// Token: 0x0400BC01 RID: 48129
				public static LocString NAME = UI.FormatAsLink("Radiation Sensor", "LOGICRADIATIONSENSOR");

				// Token: 0x0400BC02 RID: 48130
				public static LocString DESC = "Radiation sensors can disable buildings when they detect dangerous levels of radiation.";

				// Token: 0x0400BC03 RID: 48131
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when ambient ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					" enters the chosen range."
				});

				// Token: 0x0400BC04 RID: 48132
				public static LocString LOGIC_PORT = "Ambient " + UI.FormatAsLink("Radiation", "RADIATION");

				// Token: 0x0400BC05 RID: 48133
				public static LocString LOGIC_PORT_ACTIVE = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" if ambient ",
					UI.FormatAsLink("Radiation", "RADIATION"),
					" is within the selected range"
				});

				// Token: 0x0400BC06 RID: 48134
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002ABB RID: 10939
			public class GASCONDUITDISEASESENSOR
			{
				// Token: 0x0400BC07 RID: 48135
				public static LocString NAME = UI.FormatAsLink("Gas Pipe Germ Sensor", "GASCONDUITDISEASESENSOR");

				// Token: 0x0400BC08 RID: 48136
				public static LocString DESC = "Germ sensors can help control automation behavior in the presence of germs.";

				// Token: 0x0400BC09 RID: 48137
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" based on the internal ",
					UI.FormatAsLink("Germ", "DISEASE"),
					" count of the pipe."
				});

				// Token: 0x0400BC0A RID: 48138
				public static LocString LOGIC_PORT = "Internal " + UI.FormatAsLink("Germ", "DISEASE") + " Count";

				// Token: 0x0400BC0B RID: 48139
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the number of Germs in the pipe is within the selected range";

				// Token: 0x0400BC0C RID: 48140
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002ABC RID: 10940
			public class LIQUIDCONDUITDISEASESENSOR
			{
				// Token: 0x0400BC0D RID: 48141
				public static LocString NAME = UI.FormatAsLink("Liquid Pipe Germ Sensor", "LIQUIDCONDUITDISEASESENSOR");

				// Token: 0x0400BC0E RID: 48142
				public static LocString DESC = "Germ sensors can help control automation behavior in the presence of germs.";

				// Token: 0x0400BC0F RID: 48143
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" based on the internal ",
					UI.FormatAsLink("Germ", "DISEASE"),
					" count of the pipe."
				});

				// Token: 0x0400BC10 RID: 48144
				public static LocString LOGIC_PORT = "Internal " + UI.FormatAsLink("Germ", "DISEASE") + " Count";

				// Token: 0x0400BC11 RID: 48145
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the number of Germs in the pipe is within the selected range";

				// Token: 0x0400BC12 RID: 48146
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002ABD RID: 10941
			public class SOLIDCONDUITDISEASESENSOR
			{
				// Token: 0x0400BC13 RID: 48147
				public static LocString NAME = UI.FormatAsLink("Conveyor Rail Germ Sensor", "SOLIDCONDUITDISEASESENSOR");

				// Token: 0x0400BC14 RID: 48148
				public static LocString DESC = "Germ sensors can help control automation behavior in the presence of germs.";

				// Token: 0x0400BC15 RID: 48149
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" based on the internal ",
					UI.FormatAsLink("Germ", "DISEASE"),
					" count of the object on the rail."
				});

				// Token: 0x0400BC16 RID: 48150
				public static LocString LOGIC_PORT = "Internal " + UI.FormatAsLink("Germ", "DISEASE") + " Count";

				// Token: 0x0400BC17 RID: 48151
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the number of Germs on the object on the rail is within the selected range";

				// Token: 0x0400BC18 RID: 48152
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002ABE RID: 10942
			public class GASCONDUITELEMENTSENSOR
			{
				// Token: 0x0400BC19 RID: 48153
				public static LocString NAME = UI.FormatAsLink("Gas Pipe Element Sensor", "GASCONDUITELEMENTSENSOR");

				// Token: 0x0400BC1A RID: 48154
				public static LocString DESC = "Element sensors can be used to detect the presence of a specific gas in a pipe.";

				// Token: 0x0400BC1B RID: 48155
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" when the selected ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" is detected within a pipe."
				});

				// Token: 0x0400BC1C RID: 48156
				public static LocString LOGIC_PORT = "Internal " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " Presence";

				// Token: 0x0400BC1D RID: 48157
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the configured Gas is detected";

				// Token: 0x0400BC1E RID: 48158
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002ABF RID: 10943
			public class LIQUIDCONDUITELEMENTSENSOR
			{
				// Token: 0x0400BC1F RID: 48159
				public static LocString NAME = UI.FormatAsLink("Liquid Pipe Element Sensor", "LIQUIDCONDUITELEMENTSENSOR");

				// Token: 0x0400BC20 RID: 48160
				public static LocString DESC = "Element sensors can be used to detect the presence of a specific liquid in a pipe.";

				// Token: 0x0400BC21 RID: 48161
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" when the selected ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" is detected within a pipe."
				});

				// Token: 0x0400BC22 RID: 48162
				public static LocString LOGIC_PORT = "Internal " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " Presence";

				// Token: 0x0400BC23 RID: 48163
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the configured Liquid is detected within the pipe";

				// Token: 0x0400BC24 RID: 48164
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AC0 RID: 10944
			public class SOLIDCONDUITELEMENTSENSOR
			{
				// Token: 0x0400BC25 RID: 48165
				public static LocString NAME = UI.FormatAsLink("Conveyor Rail Element Sensor", "SOLIDCONDUITELEMENTSENSOR");

				// Token: 0x0400BC26 RID: 48166
				public static LocString DESC = "Element sensors can be used to detect the presence of a specific item on a rail.";

				// Token: 0x0400BC27 RID: 48167
				public static LocString EFFECT = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when the selected item is detected on a rail.";

				// Token: 0x0400BC28 RID: 48168
				public static LocString LOGIC_PORT = "Internal " + UI.FormatAsLink("Item", "ELEMENTS_LIQUID") + " Presence";

				// Token: 0x0400BC29 RID: 48169
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the configured item is detected on the rail";

				// Token: 0x0400BC2A RID: 48170
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AC1 RID: 10945
			public class GASCONDUITTEMPERATURESENSOR
			{
				// Token: 0x0400BC2B RID: 48171
				public static LocString NAME = UI.FormatAsLink("Gas Pipe Thermo Sensor", "GASCONDUITTEMPERATURESENSOR");

				// Token: 0x0400BC2C RID: 48172
				public static LocString DESC = "Thermo sensors disable buildings when their pipe contents reach a certain temperature.";

				// Token: 0x0400BC2D RID: 48173
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when pipe contents enter the chosen ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" range."
				});

				// Token: 0x0400BC2E RID: 48174
				public static LocString LOGIC_PORT = "Internal " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " " + UI.FormatAsLink("Temperature", "HEAT");

				// Token: 0x0400BC2F RID: 48175
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the contained Gas is within the selected Temperature range";

				// Token: 0x0400BC30 RID: 48176
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AC2 RID: 10946
			public class LIQUIDCONDUITTEMPERATURESENSOR
			{
				// Token: 0x0400BC31 RID: 48177
				public static LocString NAME = UI.FormatAsLink("Liquid Pipe Thermo Sensor", "LIQUIDCONDUITTEMPERATURESENSOR");

				// Token: 0x0400BC32 RID: 48178
				public static LocString DESC = "Thermo sensors disable buildings when their pipe contents reach a certain temperature.";

				// Token: 0x0400BC33 RID: 48179
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when pipe contents enter the chosen ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" range."
				});

				// Token: 0x0400BC34 RID: 48180
				public static LocString LOGIC_PORT = "Internal " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " " + UI.FormatAsLink("Temperature", "HEAT");

				// Token: 0x0400BC35 RID: 48181
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the contained Liquid is within the selected Temperature range";

				// Token: 0x0400BC36 RID: 48182
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AC3 RID: 10947
			public class SOLIDCONDUITTEMPERATURESENSOR
			{
				// Token: 0x0400BC37 RID: 48183
				public static LocString NAME = UI.FormatAsLink("Conveyor Rail Thermo Sensor", "SOLIDCONDUITTEMPERATURESENSOR");

				// Token: 0x0400BC38 RID: 48184
				public static LocString DESC = "Thermo sensors disable buildings when their rail contents reach a certain temperature.";

				// Token: 0x0400BC39 RID: 48185
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" when rail contents enter the chosen ",
					UI.FormatAsLink("Temperature", "HEAT"),
					" range."
				});

				// Token: 0x0400BC3A RID: 48186
				public static LocString LOGIC_PORT = "Internal Item " + UI.FormatAsLink("Temperature", "HEAT");

				// Token: 0x0400BC3B RID: 48187
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if the contained item is within the selected Temperature range";

				// Token: 0x0400BC3C RID: 48188
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AC4 RID: 10948
			public class LOGICCOUNTER
			{
				// Token: 0x0400BC3D RID: 48189
				public static LocString NAME = UI.FormatAsLink("Signal Counter", "LOGICCOUNTER");

				// Token: 0x0400BC3E RID: 48190
				public static LocString DESC = "For numbers higher than ten connect multiple counters together.";

				// Token: 0x0400BC3F RID: 48191
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Counts how many times a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" has been received up to a chosen number.\n\nWhen the chosen number is reached it sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" until it receives another ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					", when it resets automatically and begins counting again."
				});

				// Token: 0x0400BC40 RID: 48192
				public static LocString LOGIC_PORT = "Internal Counter Value";

				// Token: 0x0400BC41 RID: 48193
				public static LocString INPUT_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Increase counter by one";

				// Token: 0x0400BC42 RID: 48194
				public static LocString INPUT_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Nothing";

				// Token: 0x0400BC43 RID: 48195
				public static LocString LOGIC_PORT_RESET = "Reset Counter";

				// Token: 0x0400BC44 RID: 48196
				public static LocString RESET_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Reset counter";

				// Token: 0x0400BC45 RID: 48197
				public static LocString RESET_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Nothing";

				// Token: 0x0400BC46 RID: 48198
				public static LocString LOGIC_PORT_OUTPUT = "Number Reached";

				// Token: 0x0400BC47 RID: 48199
				public static LocString OUTPUT_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when the counter matches the selected value";

				// Token: 0x0400BC48 RID: 48200
				public static LocString OUTPUT_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AC5 RID: 10949
			public class LOGICALARM
			{
				// Token: 0x0400BC49 RID: 48201
				public static LocString NAME = UI.FormatAsLink("Automated Notifier", "LOGICALARM");

				// Token: 0x0400BC4A RID: 48202
				public static LocString DESC = "Sends a notification when it receives a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ".";

				// Token: 0x0400BC4B RID: 48203
				public static LocString EFFECT = "Attach to sensors to send a notification when certain conditions are met.\n\nNotifications can be customized.";

				// Token: 0x0400BC4C RID: 48204
				public static LocString LOGIC_PORT = "Notification";

				// Token: 0x0400BC4D RID: 48205
				public static LocString INPUT_NAME = "INPUT";

				// Token: 0x0400BC4E RID: 48206
				public static LocString INPUT_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Push notification";

				// Token: 0x0400BC4F RID: 48207
				public static LocString INPUT_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Nothing";
			}

			// Token: 0x02002AC6 RID: 10950
			public class PIXELPACK
			{
				// Token: 0x0400BC50 RID: 48208
				public static LocString NAME = UI.FormatAsLink("Pixel Pack", "PIXELPACK");

				// Token: 0x0400BC51 RID: 48209
				public static LocString DESC = "Four pixels which can be individually designated different colors.";

				// Token: 0x0400BC52 RID: 48210
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Pixels can be designated a color when it receives a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" and a different color when it receives a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					".\n\nInput from an ",
					UI.FormatAsLink("Automation Wire", "LOGICWIRE"),
					" controls the whole strip. Input from an ",
					UI.FormatAsLink("Automation Ribbon", "LOGICRIBBON"),
					" can control individual pixels on the strip."
				});

				// Token: 0x0400BC53 RID: 48211
				public static LocString LOGIC_PORT = "Color Selection";

				// Token: 0x0400BC54 RID: 48212
				public static LocString INPUT_NAME = "RIBBON INPUT";

				// Token: 0x0400BC55 RID: 48213
				public static LocString INPUT_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Display the configured " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " pixels";

				// Token: 0x0400BC56 RID: 48214
				public static LocString INPUT_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Display the configured " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " pixels";

				// Token: 0x0400BC57 RID: 48215
				public static LocString SIDESCREEN_TITLE = "Pixel Pack";
			}

			// Token: 0x02002AC7 RID: 10951
			public class LOGICHAMMER
			{
				// Token: 0x0400BC58 RID: 48216
				public static LocString NAME = UI.FormatAsLink("Hammer", "LOGICHAMMER");

				// Token: 0x0400BC59 RID: 48217
				public static LocString DESC = "The hammer makes neat sounds when it strikes buildings.";

				// Token: 0x0400BC5A RID: 48218
				public static LocString EFFECT = "In its default orientation, the hammer strikes the building to the left when it receives a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ".\n\nEach building has a unique sound when struck by the hammer.\n\nThe hammer does no damage when it strikes.";

				// Token: 0x0400BC5B RID: 48219
				public static LocString LOGIC_PORT = "Resonating Buildings";

				// Token: 0x0400BC5C RID: 48220
				public static LocString INPUT_NAME = "INPUT";

				// Token: 0x0400BC5D RID: 48221
				public static LocString INPUT_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Hammer strikes once";

				// Token: 0x0400BC5E RID: 48222
				public static LocString INPUT_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Nothing";
			}

			// Token: 0x02002AC8 RID: 10952
			public class LOGICRIBBONWRITER
			{
				// Token: 0x0400BC5F RID: 48223
				public static LocString NAME = UI.FormatAsLink("Ribbon Writer", "LOGICRIBBONWRITER");

				// Token: 0x0400BC60 RID: 48224
				public static LocString DESC = "Translates the signal from an " + UI.FormatAsLink("Automation Wire", "LOGICWIRE") + " to a single Bit in an " + UI.FormatAsLink("Automation Ribbon", "LOGICRIBBON");

				// Token: 0x0400BC61 RID: 48225
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Writes a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" to the specified Bit of an ",
					BUILDINGS.PREFABS.LOGICRIBBON.NAME,
					"\n\n",
					BUILDINGS.PREFABS.LOGICRIBBON.NAME,
					" must be used as the output wire to avoid overloading."
				});

				// Token: 0x0400BC62 RID: 48226
				public static LocString LOGIC_PORT = "1-Bit Input";

				// Token: 0x0400BC63 RID: 48227
				public static LocString INPUT_NAME = "INPUT";

				// Token: 0x0400BC64 RID: 48228
				public static LocString INPUT_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Receives " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " to be written to selected Bit";

				// Token: 0x0400BC65 RID: 48229
				public static LocString INPUT_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Receives " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " to to be written selected Bit";

				// Token: 0x0400BC66 RID: 48230
				public static LocString LOGIC_PORT_OUTPUT = "Bit Writing";

				// Token: 0x0400BC67 RID: 48231
				public static LocString OUTPUT_NAME = "RIBBON OUTPUT";

				// Token: 0x0400BC68 RID: 48232
				public static LocString OUTPUT_PORT_ACTIVE = string.Concat(new string[]
				{
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					": Writes a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" to selected Bit of an ",
					BUILDINGS.PREFABS.LOGICRIBBON.NAME
				});

				// Token: 0x0400BC69 RID: 48233
				public static LocString OUTPUT_PORT_INACTIVE = string.Concat(new string[]
				{
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					": Writes a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" to selected Bit of an ",
					BUILDINGS.PREFABS.LOGICRIBBON.NAME
				});
			}

			// Token: 0x02002AC9 RID: 10953
			public class LOGICRIBBONREADER
			{
				// Token: 0x0400BC6A RID: 48234
				public static LocString NAME = UI.FormatAsLink("Ribbon Reader", "LOGICRIBBONREADER");

				// Token: 0x0400BC6B RID: 48235
				public static LocString DESC = string.Concat(new string[]
				{
					"Inputs the signal from a single Bit in an ",
					UI.FormatAsLink("Automation Ribbon", "LOGICRIBBON"),
					" into an ",
					UI.FormatAsLink("Automation Wire", "LOGICWIRE"),
					"."
				});

				// Token: 0x0400BC6C RID: 48236
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Reads a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" or a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" from the specified Bit of an ",
					BUILDINGS.PREFABS.LOGICRIBBON.NAME,
					" onto an ",
					BUILDINGS.PREFABS.LOGICWIRE.NAME,
					"."
				});

				// Token: 0x0400BC6D RID: 48237
				public static LocString LOGIC_PORT = "4-Bit Input";

				// Token: 0x0400BC6E RID: 48238
				public static LocString INPUT_NAME = "RIBBON INPUT";

				// Token: 0x0400BC6F RID: 48239
				public static LocString INPUT_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Reads a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " from selected Bit";

				// Token: 0x0400BC70 RID: 48240
				public static LocString INPUT_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Reads a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + " from selected Bit";

				// Token: 0x0400BC71 RID: 48241
				public static LocString LOGIC_PORT_OUTPUT = "Bit Reading";

				// Token: 0x0400BC72 RID: 48242
				public static LocString OUTPUT_NAME = "OUTPUT";

				// Token: 0x0400BC73 RID: 48243
				public static LocString OUTPUT_PORT_ACTIVE = string.Concat(new string[]
				{
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					": Sends a ",
					UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
					" to attached ",
					UI.FormatAsLink("Automation Wire", "LOGICWIRE")
				});

				// Token: 0x0400BC74 RID: 48244
				public static LocString OUTPUT_PORT_INACTIVE = string.Concat(new string[]
				{
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					": Sends a ",
					UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
					" to attached ",
					UI.FormatAsLink("Automation Wire", "LOGICWIRE")
				});
			}

			// Token: 0x02002ACA RID: 10954
			public class TRAVELTUBEENTRANCE
			{
				// Token: 0x0400BC75 RID: 48245
				public static LocString NAME = UI.FormatAsLink("Transit Tube Access", "TRAVELTUBEENTRANCE");

				// Token: 0x0400BC76 RID: 48246
				public static LocString DESC = "Duplicants require access points to enter tubes, but not to exit them.";

				// Token: 0x0400BC77 RID: 48247
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants to enter the connected ",
					UI.FormatAsLink("Transit Tube", "TRAVELTUBE"),
					" system.\n\nStops drawing ",
					UI.FormatAsLink("Power", "POWER"),
					" once fully charged."
				});
			}

			// Token: 0x02002ACB RID: 10955
			public class TRAVELTUBE
			{
				// Token: 0x0400BC78 RID: 48248
				public static LocString NAME = UI.FormatAsLink("Transit Tube", "TRAVELTUBE");

				// Token: 0x0400BC79 RID: 48249
				public static LocString DESC = "Duplicants will only exit a transit tube when a safe landing area is available beneath it.";

				// Token: 0x0400BC7A RID: 48250
				public static LocString EFFECT = "Quickly transports Duplicants from a " + UI.FormatAsLink("Transit Tube Access", "TRAVELTUBEENTRANCE") + " to the tube's end.\n\nOnly transports Duplicants.";
			}

			// Token: 0x02002ACC RID: 10956
			public class TRAVELTUBEWALLBRIDGE
			{
				// Token: 0x0400BC7B RID: 48251
				public static LocString NAME = UI.FormatAsLink("Transit Tube Crossing", "TRAVELTUBEWALLBRIDGE");

				// Token: 0x0400BC7C RID: 48252
				public static LocString DESC = "Tube crossings can run transit tubes through walls without leaking gas or liquid.";

				// Token: 0x0400BC7D RID: 48253
				public static LocString EFFECT = "Allows " + UI.FormatAsLink("Transit Tubes", "TRAVELTUBE") + " to be run through wall and floor tile.\n\nFunctions as regular tile.";
			}

			// Token: 0x02002ACD RID: 10957
			public class SOLIDCONDUIT
			{
				// Token: 0x0400BC7E RID: 48254
				public static LocString NAME = UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT");

				// Token: 0x0400BC7F RID: 48255
				public static LocString DESC = "Rails move materials where they'll be needed most, saving Duplicants the walk.";

				// Token: 0x0400BC80 RID: 48256
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Transports ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" on a track between ",
					UI.FormatAsLink("Conveyor Loader", "SOLIDCONDUITINBOX"),
					" and ",
					UI.FormatAsLink("Conveyor Receptacle", "SOLIDCONDUITOUTBOX"),
					".\n\nCan be run through wall and floor tile."
				});
			}

			// Token: 0x02002ACE RID: 10958
			public class SOLIDCONDUITINBOX
			{
				// Token: 0x0400BC81 RID: 48257
				public static LocString NAME = UI.FormatAsLink("Conveyor Loader", "SOLIDCONDUITINBOX");

				// Token: 0x0400BC82 RID: 48258
				public static LocString DESC = "Material filters can be used to determine what resources are sent down the rail.";

				// Token: 0x0400BC83 RID: 48259
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Loads ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" onto ",
					UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT"),
					" for transport.\n\nOnly loads the resources of your choosing."
				});
			}

			// Token: 0x02002ACF RID: 10959
			public class SOLIDCONDUITOUTBOX
			{
				// Token: 0x0400BC84 RID: 48260
				public static LocString NAME = UI.FormatAsLink("Conveyor Receptacle", "SOLIDCONDUITOUTBOX");

				// Token: 0x0400BC85 RID: 48261
				public static LocString DESC = "When materials reach the end of a rail they enter a receptacle to be used by Duplicants.";

				// Token: 0x0400BC86 RID: 48262
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Unloads ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" from a ",
					UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT"),
					" into storage."
				});
			}

			// Token: 0x02002AD0 RID: 10960
			public class SOLIDTRANSFERARM
			{
				// Token: 0x0400BC87 RID: 48263
				public static LocString NAME = UI.FormatAsLink("Auto-Sweeper", "SOLIDTRANSFERARM");

				// Token: 0x0400BC88 RID: 48264
				public static LocString DESC = "An auto-sweeper's range can be viewed at any time by " + UI.CLICK(UI.ClickType.clicking) + " on the building.";

				// Token: 0x0400BC89 RID: 48265
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Automates ",
					UI.FormatAsLink("Sweeping", "CHORES"),
					" and ",
					UI.FormatAsLink("Supplying", "CHORES"),
					" errands by sucking up all nearby ",
					UI.FormatAsLink("Debris", "DECOR"),
					".\n\nMaterials are automatically delivered to any ",
					UI.FormatAsLink("Conveyor Loader", "SOLIDCONDUITINBOX"),
					", ",
					UI.FormatAsLink("Conveyor Receptacle", "SOLIDCONDUITOUTBOX"),
					", storage, or buildings within range."
				});
			}

			// Token: 0x02002AD1 RID: 10961
			public class SOLIDCONDUITBRIDGE
			{
				// Token: 0x0400BC8A RID: 48266
				public static LocString NAME = UI.FormatAsLink("Conveyor Bridge", "SOLIDCONDUITBRIDGE");

				// Token: 0x0400BC8B RID: 48267
				public static LocString DESC = "Separating rail systems helps ensure materials go to the intended destinations.";

				// Token: 0x0400BC8C RID: 48268
				public static LocString EFFECT = "Runs one " + UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT") + " section over another without joining them.\n\nCan be run through wall and floor tile.";
			}

			// Token: 0x02002AD2 RID: 10962
			public class SOLIDVENT
			{
				// Token: 0x0400BC8D RID: 48269
				public static LocString NAME = UI.FormatAsLink("Conveyor Chute", "SOLIDVENT");

				// Token: 0x0400BC8E RID: 48270
				public static LocString DESC = "When materials reach the end of a rail they are dropped back into the world.";

				// Token: 0x0400BC8F RID: 48271
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Unloads ",
					UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID"),
					" from a ",
					UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT"),
					" onto the floor."
				});
			}

			// Token: 0x02002AD3 RID: 10963
			public class SOLIDLOGICVALVE
			{
				// Token: 0x0400BC90 RID: 48272
				public static LocString NAME = UI.FormatAsLink("Conveyor Shutoff", "SOLIDLOGICVALVE");

				// Token: 0x0400BC91 RID: 48273
				public static LocString DESC = "Automated conveyors save power and time by removing the need for Duplicant input.";

				// Token: 0x0400BC92 RID: 48274
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Connects to an ",
					UI.FormatAsLink("Automation", "LOGIC"),
					" grid to automatically turn ",
					UI.FormatAsLink("Solid Material", "ELEMENTS_SOLID"),
					" transport on or off."
				});

				// Token: 0x0400BC93 RID: 48275
				public static LocString LOGIC_PORT = "Open/Close";

				// Token: 0x0400BC94 RID: 48276
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Allow material transport";

				// Token: 0x0400BC95 RID: 48277
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Prevent material transport";
			}

			// Token: 0x02002AD4 RID: 10964
			public class SOLIDLIMITVALVE
			{
				// Token: 0x0400BC96 RID: 48278
				public static LocString NAME = UI.FormatAsLink("Conveyor Meter", "SOLIDLIMITVALVE");

				// Token: 0x0400BC97 RID: 48279
				public static LocString DESC = "Conveyor Meters let an exact amount of materials pass through before shutting off.";

				// Token: 0x0400BC98 RID: 48280
				public static LocString EFFECT = "Connects to an " + UI.FormatAsLink("Automation", "LOGIC") + " grid to automatically turn material transfer off when the specified amount has passed through it.";

				// Token: 0x0400BC99 RID: 48281
				public static LocString LOGIC_PORT_OUTPUT = "Limit Reached";

				// Token: 0x0400BC9A RID: 48282
				public static LocString OUTPUT_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " if limit has been reached";

				// Token: 0x0400BC9B RID: 48283
				public static LocString OUTPUT_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x0400BC9C RID: 48284
				public static LocString LOGIC_PORT_RESET = "Reset Meter";

				// Token: 0x0400BC9D RID: 48285
				public static LocString RESET_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Reset the amount";

				// Token: 0x0400BC9E RID: 48286
				public static LocString RESET_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Nothing";
			}

			// Token: 0x02002AD5 RID: 10965
			public class DEVPUMPSOLID
			{
				// Token: 0x0400BC9F RID: 48287
				public static LocString NAME = "Dev Pump Solid";

				// Token: 0x0400BCA0 RID: 48288
				public static LocString DESC = "Piping a pump's output to a building's intake will send solids to that building.";

				// Token: 0x0400BCA1 RID: 48289
				public static LocString EFFECT = "Generates chosen " + UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID") + " and runs it through " + UI.FormatAsLink("Conveyor Rail", "SOLIDCONDUIT");
			}

			// Token: 0x02002AD6 RID: 10966
			public class AUTOMINER
			{
				// Token: 0x0400BCA2 RID: 48290
				public static LocString NAME = UI.FormatAsLink("Robo-Miner", "AUTOMINER");

				// Token: 0x0400BCA3 RID: 48291
				public static LocString DESC = "A robo-miner's range can be viewed at any time by selecting the building.";

				// Token: 0x0400BCA4 RID: 48292
				public static LocString EFFECT = "Automatically digs out all materials in a set range.";
			}

			// Token: 0x02002AD7 RID: 10967
			public class CREATUREFEEDER
			{
				// Token: 0x0400BCA5 RID: 48293
				public static LocString NAME = UI.FormatAsLink("Critter Feeder", "CREATUREFEEDER");

				// Token: 0x0400BCA6 RID: 48294
				public static LocString DESC = "Critters tend to stay close to their food source and wander less when given a feeder.";

				// Token: 0x0400BCA7 RID: 48295
				public static LocString EFFECT = "Automatically dispenses food for hungry " + UI.FormatAsLink("Critters", "CREATURES") + ".";
			}

			// Token: 0x02002AD8 RID: 10968
			public class GRAVITASPEDESTAL
			{
				// Token: 0x0400BCA8 RID: 48296
				public static LocString NAME = UI.FormatAsLink("Pedestal", "ITEMPEDESTAL");

				// Token: 0x0400BCA9 RID: 48297
				public static LocString DESC = "Perception can be drastically changed by a bit of thoughtful presentation.";

				// Token: 0x0400BCAA RID: 48298
				public static LocString EFFECT = "Displays a single object, doubling its " + UI.FormatAsLink("Decor", "DECOR") + " value.\n\nObjects with negative Decor will gain some positive Decor when displayed.";

				// Token: 0x0400BCAB RID: 48299
				public static LocString DISPLAYED_ITEM_FMT = "Displayed {0}";
			}

			// Token: 0x02002AD9 RID: 10969
			public class ITEMPEDESTAL
			{
				// Token: 0x0400BCAC RID: 48300
				public static LocString NAME = UI.FormatAsLink("Pedestal", "ITEMPEDESTAL");

				// Token: 0x0400BCAD RID: 48301
				public static LocString DESC = "Perception can be drastically changed by a bit of thoughtful presentation.";

				// Token: 0x0400BCAE RID: 48302
				public static LocString EFFECT = "Displays a single object, doubling its " + UI.FormatAsLink("Decor", "DECOR") + " value.\n\nObjects with negative Decor will gain some positive Decor when displayed.";

				// Token: 0x0400BCAF RID: 48303
				public static LocString DISPLAYED_ITEM_FMT = "Displayed {0}";

				// Token: 0x020038BA RID: 14522
				public class FACADES
				{
					// Token: 0x02003D40 RID: 15680
					public class DEFAULT_ITEMPEDESTAL
					{
						// Token: 0x0400EFB8 RID: 61368
						public static LocString NAME = UI.FormatAsLink("Pedestal", "ITEMPEDESTAL");

						// Token: 0x0400EFB9 RID: 61369
						public static LocString DESC = "Perception can be drastically changed by a bit of thoughtful presentation.";
					}

					// Token: 0x02003D41 RID: 15681
					public class HAND
					{
						// Token: 0x0400EFBA RID: 61370
						public static LocString NAME = UI.FormatAsLink("Hand of Dupe Pedestal", "ITEMPEDESTAL");

						// Token: 0x0400EFBB RID: 61371
						public static LocString DESC = "This pedestal cradles precious objects in the palm of its hand.";
					}
				}
			}

			// Token: 0x02002ADA RID: 10970
			public class CROWNMOULDING
			{
				// Token: 0x0400BCB0 RID: 48304
				public static LocString NAME = UI.FormatAsLink("Ceiling Trim", "CROWNMOULDING");

				// Token: 0x0400BCB1 RID: 48305
				public static LocString DESC = "Ceiling trim is a purely decorative addition to one's overhead area.";

				// Token: 0x0400BCB2 RID: 48306
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Used to decorate the ceilings of rooms.\n\nIncreases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});

				// Token: 0x020038BB RID: 14523
				public class FACADES
				{
					// Token: 0x02003D42 RID: 15682
					public class DEFAULT_CROWNMOULDING
					{
						// Token: 0x0400EFBC RID: 61372
						public static LocString NAME = UI.FormatAsLink("Ceiling Trim", "CROWNMOULDING");

						// Token: 0x0400EFBD RID: 61373
						public static LocString DESC = "Ceiling trim is a purely decorative addition to one's overhead area.";
					}

					// Token: 0x02003D43 RID: 15683
					public class SHINEORNAMENTS
					{
						// Token: 0x0400EFBE RID: 61374
						public static LocString NAME = UI.FormatAsLink("Fancy Bug Ceiling Garland", "CROWNMOULDING");

						// Token: 0x0400EFBF RID: 61375
						public static LocString DESC = "Someone spent their entire weekend gluing ribbons to paper Shine Bug cut-outs, and it shows.";
					}
				}
			}

			// Token: 0x02002ADB RID: 10971
			public class CORNERMOULDING
			{
				// Token: 0x0400BCB3 RID: 48307
				public static LocString NAME = UI.FormatAsLink("Corner Trim", "CORNERMOULDING");

				// Token: 0x0400BCB4 RID: 48308
				public static LocString DESC = "Corner trim is a purely decorative addition for ceiling corners.";

				// Token: 0x0400BCB5 RID: 48309
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Used to decorate the ceiling corners of rooms.\n\nIncreases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});

				// Token: 0x020038BC RID: 14524
				public class FACADES
				{
					// Token: 0x02003D44 RID: 15684
					public class DEFAULT_CORNERMOULDING
					{
						// Token: 0x0400EFC0 RID: 61376
						public static LocString NAME = UI.FormatAsLink("Corner Trim", "CORNERMOULDING");

						// Token: 0x0400EFC1 RID: 61377
						public static LocString DESC = "It really dresses up a ceiling corner.";
					}

					// Token: 0x02003D45 RID: 15685
					public class SHINEORNAMENTS
					{
						// Token: 0x0400EFC2 RID: 61378
						public static LocString NAME = UI.FormatAsLink("Fancy Bug Corner Garland", "CORNERMOULDING");

						// Token: 0x0400EFC3 RID: 61379
						public static LocString DESC = "Why deck the halls, when you could <i>festoon</i> them?";
					}
				}
			}

			// Token: 0x02002ADC RID: 10972
			public class EGGINCUBATOR
			{
				// Token: 0x0400BCB6 RID: 48310
				public static LocString NAME = UI.FormatAsLink("Incubator", "EGGINCUBATOR");

				// Token: 0x0400BCB7 RID: 48311
				public static LocString DESC = "Incubators can maintain the ideal internal conditions for several species of critter egg.";

				// Token: 0x0400BCB8 RID: 48312
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Incubates ",
					UI.FormatAsLink("Critter", "CREATURES"),
					" eggs until ready to hatch.\n\nAssigned Duplicants must possess the ",
					UI.FormatAsLink("Critter Ranching", "RANCHING1"),
					" skill."
				});
			}

			// Token: 0x02002ADD RID: 10973
			public class EGGCRACKER
			{
				// Token: 0x0400BCB9 RID: 48313
				public static LocString NAME = UI.FormatAsLink("Egg Cracker", "EGGCRACKER");

				// Token: 0x0400BCBA RID: 48314
				public static LocString DESC = "Raw eggs are an ingredient in certain high quality food recipes.";

				// Token: 0x0400BCBB RID: 48315
				public static LocString EFFECT = "Converts viable " + UI.FormatAsLink("Critter", "CREATURES") + " eggs into cooking ingredients.\n\nCracked Eggs cannot hatch.\n\nDuplicants will not crack eggs unless tasks are queued.";

				// Token: 0x0400BCBC RID: 48316
				public static LocString RECIPE_DESCRIPTION = "Turns {0} into {1}.";

				// Token: 0x0400BCBD RID: 48317
				public static LocString RESULT_DESCRIPTION = "Cracked {0}";

				// Token: 0x020038BD RID: 14525
				public class FACADES
				{
					// Token: 0x02003D46 RID: 15686
					public class DEFAULT_EGGCRACKER
					{
						// Token: 0x0400EFC4 RID: 61380
						public static LocString NAME = UI.FormatAsLink("Egg Cracker", "EGGCRACKER");

						// Token: 0x0400EFC5 RID: 61381
						public static LocString DESC = "It cracks eggs.";
					}

					// Token: 0x02003D47 RID: 15687
					public class BEAKER
					{
						// Token: 0x0400EFC6 RID: 61382
						public static LocString NAME = UI.FormatAsLink("Beaker Cracker", "EGGCRACKER");

						// Token: 0x0400EFC7 RID: 61383
						public static LocString DESC = "A practical exercise in physics.";
					}

					// Token: 0x02003D48 RID: 15688
					public class FLOWER
					{
						// Token: 0x0400EFC8 RID: 61384
						public static LocString NAME = UI.FormatAsLink("Blossom Cracker", "EGGCRACKER");

						// Token: 0x0400EFC9 RID: 61385
						public static LocString DESC = "Now with EZ-clean petals.";
					}

					// Token: 0x02003D49 RID: 15689
					public class HANDS
					{
						// Token: 0x0400EFCA RID: 61386
						public static LocString NAME = UI.FormatAsLink("Handy Cracker", "EGGCRACKER");

						// Token: 0x0400EFCB RID: 61387
						public static LocString DESC = "Just like Mi-Ma used to have.";
					}
				}
			}

			// Token: 0x02002ADE RID: 10974
			public class URANIUMCENTRIFUGE
			{
				// Token: 0x0400BCBE RID: 48318
				public static LocString NAME = UI.FormatAsLink("Uranium Centrifuge", "URANIUMCENTRIFUGE");

				// Token: 0x0400BCBF RID: 48319
				public static LocString DESC = "Enriched uranium is a specialized substance that can be used to fuel powerful research reactors.";

				// Token: 0x0400BCC0 RID: 48320
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Extracts ",
					UI.FormatAsLink("Enriched Uranium", "ENRICHEDURANIUM"),
					" from ",
					UI.FormatAsLink("Uranium Ore", "URANIUMORE"),
					".\n\nOutputs ",
					UI.FormatAsLink("Depleted Uranium", "DEPLETEDURANIUM"),
					" in molten form."
				});

				// Token: 0x0400BCC1 RID: 48321
				public static LocString RECIPE_DESCRIPTION = "Convert Uranium ore to Molten Uranium and Enriched Uranium";
			}

			// Token: 0x02002ADF RID: 10975
			public class HIGHENERGYPARTICLEREDIRECTOR
			{
				// Token: 0x0400BCC2 RID: 48322
				public static LocString NAME = UI.FormatAsLink("Radbolt Reflector", "HIGHENERGYPARTICLEREDIRECTOR");

				// Token: 0x0400BCC3 RID: 48323
				public static LocString DESC = "We were all out of mirrors.";

				// Token: 0x0400BCC4 RID: 48324
				public static LocString EFFECT = "Receives and redirects Radbolts from " + UI.FormatAsLink("Radbolt Generators", "HIGHENERGYPARTICLESPAWNER") + ".";

				// Token: 0x0400BCC5 RID: 48325
				public static LocString LOGIC_PORT = "Ignore incoming Radbolts";

				// Token: 0x0400BCC6 RID: 48326
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Allow incoming Radbolts";

				// Token: 0x0400BCC7 RID: 48327
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Ignore incoming Radbolts";
			}

			// Token: 0x02002AE0 RID: 10976
			public class MANUALHIGHENERGYPARTICLESPAWNER
			{
				// Token: 0x0400BCC8 RID: 48328
				public static LocString NAME = UI.FormatAsLink("Manual Radbolt Generator", "MANUALHIGHENERGYPARTICLESPAWNER");

				// Token: 0x0400BCC9 RID: 48329
				public static LocString DESC = "Radbolts are necessary for producing Materials Science research.";

				// Token: 0x0400BCCA RID: 48330
				public static LocString EFFECT = "Refines radioactive ores to generate Radbolts.\n\nEmits generated Radbolts in the direction of your choosing.";

				// Token: 0x0400BCCB RID: 48331
				public static LocString RECIPE_DESCRIPTION = "Creates " + UI.FormatAsLink("Radbolts", "RADIATION") + " by processing {0}. Also creates {1} as a byproduct.";
			}

			// Token: 0x02002AE1 RID: 10977
			public class HIGHENERGYPARTICLESPAWNER
			{
				// Token: 0x0400BCCC RID: 48332
				public static LocString NAME = UI.FormatAsLink("Radbolt Generator", "HIGHENERGYPARTICLESPAWNER");

				// Token: 0x0400BCCD RID: 48333
				public static LocString DESC = "Radbolts are necessary for producing Materials Science research.";

				// Token: 0x0400BCCE RID: 48334
				public static LocString EFFECT = "Attracts nearby " + UI.FormatAsLink("Radiation", "RADIATION") + " to generate Radbolts.\n\nEmits generated Radbolts in the direction of your choosing when the set Radbolt threshold is reached.\n\nRadbolts collected will rapidly decay while this building is disabled.";

				// Token: 0x0400BCCF RID: 48335
				public static LocString LOGIC_PORT = "Do not emit Radbolts";

				// Token: 0x0400BCD0 RID: 48336
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Emit Radbolts";

				// Token: 0x0400BCD1 RID: 48337
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Do not emit Radbolts";
			}

			// Token: 0x02002AE2 RID: 10978
			public class DEVHEPSPAWNER
			{
				// Token: 0x0400BCD2 RID: 48338
				public static LocString NAME = "Dev Radbolt Generator";

				// Token: 0x0400BCD3 RID: 48339
				public static LocString DESC = "Radbolts are necessary for producing Materials Science research.";

				// Token: 0x0400BCD4 RID: 48340
				public static LocString EFFECT = "Generates Radbolts.";

				// Token: 0x0400BCD5 RID: 48341
				public static LocString LOGIC_PORT = "Do not emit Radbolts";

				// Token: 0x0400BCD6 RID: 48342
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Emit Radbolts";

				// Token: 0x0400BCD7 RID: 48343
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Do not emit Radbolts";
			}

			// Token: 0x02002AE3 RID: 10979
			public class HEPBATTERY
			{
				// Token: 0x0400BCD8 RID: 48344
				public static LocString NAME = UI.FormatAsLink("Radbolt Chamber", "HEPBATTERY");

				// Token: 0x0400BCD9 RID: 48345
				public static LocString DESC = "Particles packed up and ready to go.";

				// Token: 0x0400BCDA RID: 48346
				public static LocString EFFECT = "Stores Radbolts in a high-energy state, ready for transport.\n\nRequires a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " to release radbolts from storage when the Radbolt threshold is reached.\n\nRadbolts in storage will rapidly decay while this building is disabled.";

				// Token: 0x0400BCDB RID: 48347
				public static LocString LOGIC_PORT = "Do not emit Radbolts";

				// Token: 0x0400BCDC RID: 48348
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Emit Radbolts";

				// Token: 0x0400BCDD RID: 48349
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Do not emit Radbolts";

				// Token: 0x0400BCDE RID: 48350
				public static LocString LOGIC_PORT_STORAGE = "Radbolt Storage";

				// Token: 0x0400BCDF RID: 48351
				public static LocString LOGIC_PORT_STORAGE_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when its Radbolt Storage is full";

				// Token: 0x0400BCE0 RID: 48352
				public static LocString LOGIC_PORT_STORAGE_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02002AE4 RID: 10980
			public class HEPBRIDGETILE
			{
				// Token: 0x0400BCE1 RID: 48353
				public static LocString NAME = UI.FormatAsLink("Radbolt Joint Plate", "HEPBRIDGETILE");

				// Token: 0x0400BCE2 RID: 48354
				public static LocString DESC = "Allows Radbolts to pass through walls.";

				// Token: 0x0400BCE3 RID: 48355
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Receives ",
					UI.FormatAsLink("Radbolts", "RADIATION"),
					" from ",
					UI.FormatAsLink("Radbolt Generators", "HIGHENERGYPARTICLESPAWNER"),
					" and directs them through walls. All other materials and elements will be blocked from passage."
				});
			}

			// Token: 0x02002AE5 RID: 10981
			public class ASTRONAUTTRAININGCENTER
			{
				// Token: 0x0400BCE4 RID: 48356
				public static LocString NAME = UI.FormatAsLink("Space Cadet Centrifuge", "ASTRONAUTTRAININGCENTER");

				// Token: 0x0400BCE5 RID: 48357
				public static LocString DESC = "Duplicants must complete astronaut training in order to pilot space rockets.";

				// Token: 0x0400BCE6 RID: 48358
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Trains Duplicants to become ",
					UI.FormatAsLink("Astronaut", "ROCKETPILOTING1"),
					".\n\nDuplicants must possess the ",
					UI.FormatAsLink("Astronaut", "ROCKETPILOTING1"),
					" trait to receive training."
				});
			}

			// Token: 0x02002AE6 RID: 10982
			public class HOTTUB
			{
				// Token: 0x0400BCE7 RID: 48359
				public static LocString NAME = UI.FormatAsLink("Hot Tub", "HOTTUB");

				// Token: 0x0400BCE8 RID: 48360
				public static LocString DESC = "Relaxes Duplicants with massaging jets of heated liquid.";

				// Token: 0x0400BCE9 RID: 48361
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Requires ",
					UI.FormatAsLink("Pipes", "LIQUIDPIPING"),
					" to and from tub and ",
					UI.FormatAsLink("Power", "POWER"),
					" to run the jets.\n\nWater must be a comfortable temperature and will cool rapidly.\n\nIncreases Duplicant ",
					UI.FormatAsLink("Morale", "MORALE"),
					" and leaves them feeling deliciously warm."
				});

				// Token: 0x0400BCEA RID: 48362
				public static LocString WATER_REQUIREMENT = "{element}: {amount}";

				// Token: 0x0400BCEB RID: 48363
				public static LocString WATER_REQUIREMENT_TOOLTIP = "This building must be filled with {amount} {element} in order to function.";

				// Token: 0x0400BCEC RID: 48364
				public static LocString TEMPERATURE_REQUIREMENT = "Minimum {element} Temperature: {temperature}";

				// Token: 0x0400BCED RID: 48365
				public static LocString TEMPERATURE_REQUIREMENT_TOOLTIP = "The Hot Tub will only be usable if supplied with {temperature} {element}. If the {element} gets too cold, the Hot Tub will drain and require refilling with {element}.";
			}

			// Token: 0x02002AE7 RID: 10983
			public class SODAFOUNTAIN
			{
				// Token: 0x0400BCEE RID: 48366
				public static LocString NAME = UI.FormatAsLink("Soda Fountain", "SODAFOUNTAIN");

				// Token: 0x0400BCEF RID: 48367
				public static LocString DESC = "Sparkling water puts a twinkle in a Duplicant's eye.";

				// Token: 0x0400BCF0 RID: 48368
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Creates soda from ",
					UI.FormatAsLink("Water", "WATER"),
					" and ",
					UI.FormatAsLink("Carbon Dioxide", "CARBONDIOXIDE"),
					".\n\nConsuming soda water increases Duplicant ",
					UI.FormatAsLink("Morale", "MORALE"),
					"."
				});
			}

			// Token: 0x02002AE8 RID: 10984
			public class UNCONSTRUCTEDROCKETMODULE
			{
				// Token: 0x0400BCF1 RID: 48369
				public static LocString NAME = "Empty Rocket Module";

				// Token: 0x0400BCF2 RID: 48370
				public static LocString DESC = "Something useful could be put here someday";

				// Token: 0x0400BCF3 RID: 48371
				public static LocString EFFECT = "Can be changed into a different rocket module";
			}

			// Token: 0x02002AE9 RID: 10985
			public class MILKFATSEPARATOR
			{
				// Token: 0x0400BCF4 RID: 48372
				public static LocString NAME = UI.FormatAsLink("Brackwax Gleaner", "MILKFATSEPARATOR");

				// Token: 0x0400BCF5 RID: 48373
				public static LocString DESC = "Duplicants can slather up with brackwax to increase their travel speed in transit tubes.";

				// Token: 0x0400BCF6 RID: 48374
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Refines ",
					ELEMENTS.MILK.NAME,
					" into ",
					ELEMENTS.BRINE.NAME,
					" and ",
					ELEMENTS.MILKFAT.NAME,
					", and emits ",
					ELEMENTS.CARBONDIOXIDE.NAME,
					"."
				});
			}

			// Token: 0x02002AEA RID: 10986
			public class MILKFEEDER
			{
				// Token: 0x0400BCF7 RID: 48375
				public static LocString NAME = UI.FormatAsLink("Critter Fountain", "MILKFEEDER");

				// Token: 0x0400BCF8 RID: 48376
				public static LocString DESC = "It's easier to tolerate overcrowding when you're all hopped up on brackene.";

				// Token: 0x0400BCF9 RID: 48377
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Dispenses ",
					ELEMENTS.MILK.NAME,
					" to a wide variety of ",
					UI.CODEX.CATEGORYNAMES.CREATURES,
					".\n\nAccessing the fountain significantly improves ",
					UI.CODEX.CATEGORYNAMES.CREATURES,
					"' moods."
				});
			}

			// Token: 0x02002AEB RID: 10987
			public class MILKINGSTATION
			{
				// Token: 0x0400BCFA RID: 48378
				public static LocString NAME = UI.FormatAsLink("Milking Station", "MILKINGSTATION");

				// Token: 0x0400BCFB RID: 48379
				public static LocString DESC = "The harvested liquid is basically the equivalent of soda for critters.";

				// Token: 0x0400BCFC RID: 48380
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows Duplicants with the ",
					UI.FormatAsLink("Critter Ranching II", "RANCHING2"),
					" skill to milk ",
					UI.FormatAsLink("Gassy Moos", "MOO"),
					" for ",
					ELEMENTS.MILK.NAME,
					".\n\n",
					ELEMENTS.MILK.NAME,
					" can be used to refill the ",
					BUILDINGS.PREFABS.MILKFEEDER.NAME,
					"."
				});
			}

			// Token: 0x02002AEC RID: 10988
			public class MODULARLAUNCHPADPORT
			{
				// Token: 0x0400BCFD RID: 48381
				public static LocString NAME = UI.FormatAsLink("Rocket Port", "MODULARLAUNCHPADPORTSOLID");

				// Token: 0x0400BCFE RID: 48382
				public static LocString NAME_PLURAL = UI.FormatAsLink("Rocket Ports", "MODULARLAUNCHPADPORTSOLID");
			}

			// Token: 0x02002AED RID: 10989
			public class MODULARLAUNCHPADPORTGAS
			{
				// Token: 0x0400BCFF RID: 48383
				public static LocString NAME = UI.FormatAsLink("Gas Rocket Port Loader", "MODULARLAUNCHPADPORTGAS");

				// Token: 0x0400BD00 RID: 48384
				public static LocString DESC = "Rockets must be landed to load or unload resources.";

				// Token: 0x0400BD01 RID: 48385
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Loads ",
					UI.FormatAsLink("Gases", "ELEMENTS_GAS"),
					" to the storage of a linked rocket.\n\nAutomatically links when built to the side of a ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					" or another ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME,
					".\n\nUses the gas filters set on the rocket's cargo bays."
				});
			}

			// Token: 0x02002AEE RID: 10990
			public class MODULARLAUNCHPADPORTBRIDGE
			{
				// Token: 0x0400BD02 RID: 48386
				public static LocString NAME = UI.FormatAsLink("Rocket Port Extension", "MODULARLAUNCHPADPORTBRIDGE");

				// Token: 0x0400BD03 RID: 48387
				public static LocString DESC = "Allows rocket platforms to be built farther apart.";

				// Token: 0x0400BD04 RID: 48388
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Automatically links when built to the side of a ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					" or any ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME,
					"."
				});
			}

			// Token: 0x02002AEF RID: 10991
			public class MODULARLAUNCHPADPORTLIQUID
			{
				// Token: 0x0400BD05 RID: 48389
				public static LocString NAME = UI.FormatAsLink("Liquid Rocket Port Loader", "MODULARLAUNCHPADPORTLIQUID");

				// Token: 0x0400BD06 RID: 48390
				public static LocString DESC = "Rockets must be landed to load or unload resources.";

				// Token: 0x0400BD07 RID: 48391
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Loads ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					" to the storage of a linked rocket.\n\nAutomatically links when built to the side of a ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					" or another ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME,
					".\n\nUses the liquid filters set on the rocket's cargo bays."
				});
			}

			// Token: 0x02002AF0 RID: 10992
			public class MODULARLAUNCHPADPORTSOLID
			{
				// Token: 0x0400BD08 RID: 48392
				public static LocString NAME = UI.FormatAsLink("Solid Rocket Port Loader", "MODULARLAUNCHPADPORTSOLID");

				// Token: 0x0400BD09 RID: 48393
				public static LocString DESC = "Rockets must be landed to load or unload resources.";

				// Token: 0x0400BD0A RID: 48394
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Loads ",
					UI.FormatAsLink("Solids", "ELEMENTS_SOLID"),
					" to the storage of a linked rocket.\n\nAutomatically links when built to the side of a ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					" or another ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME,
					".\n\nUses the solid material filters set on the rocket's cargo bays."
				});
			}

			// Token: 0x02002AF1 RID: 10993
			public class MODULARLAUNCHPADPORTGASUNLOADER
			{
				// Token: 0x0400BD0B RID: 48395
				public static LocString NAME = UI.FormatAsLink("Gas Rocket Port Unloader", "MODULARLAUNCHPADPORTGASUNLOADER");

				// Token: 0x0400BD0C RID: 48396
				public static LocString DESC = "Rockets must be landed to load or unload resources.";

				// Token: 0x0400BD0D RID: 48397
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Unloads ",
					UI.FormatAsLink("Gases", "ELEMENTS_GAS"),
					" from the storage of a linked rocket.\n\nAutomatically links when built to the side of a ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					" or another ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME,
					".\n\nUses the gas filters set on this unloader."
				});
			}

			// Token: 0x02002AF2 RID: 10994
			public class MODULARLAUNCHPADPORTLIQUIDUNLOADER
			{
				// Token: 0x0400BD0E RID: 48398
				public static LocString NAME = UI.FormatAsLink("Liquid Rocket Port Unloader", "MODULARLAUNCHPADPORTLIQUIDUNLOADER");

				// Token: 0x0400BD0F RID: 48399
				public static LocString DESC = "Rockets must be landed to load or unload resources.";

				// Token: 0x0400BD10 RID: 48400
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Unloads ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					" from the storage of a linked rocket.\n\nAutomatically links when built to the side of a ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					" or another ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME,
					".\n\nUses the liquid filters set on this unloader."
				});
			}

			// Token: 0x02002AF3 RID: 10995
			public class MODULARLAUNCHPADPORTSOLIDUNLOADER
			{
				// Token: 0x0400BD11 RID: 48401
				public static LocString NAME = UI.FormatAsLink("Solid Rocket Port Unloader", "MODULARLAUNCHPADPORTSOLIDUNLOADER");

				// Token: 0x0400BD12 RID: 48402
				public static LocString DESC = "Rockets must be landed to load or unload resources.";

				// Token: 0x0400BD13 RID: 48403
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Unloads ",
					UI.FormatAsLink("Solids", "ELEMENTS_SOLID"),
					" from the storage of a linked rocket.\n\nAutomatically links when built to the side of a ",
					BUILDINGS.PREFABS.LAUNCHPAD.NAME,
					" or another ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME,
					".\n\nUses the solid material filters set on this unloader."
				});
			}

			// Token: 0x02002AF4 RID: 10996
			public class STICKERBOMB
			{
				// Token: 0x0400BD14 RID: 48404
				public static LocString NAME = UI.FormatAsLink("Sticker Bomb", "STICKERBOMB");

				// Token: 0x0400BD15 RID: 48405
				public static LocString DESC = "Surprise decor sneak attacks a Duplicant's gloomy day.";
			}

			// Token: 0x02002AF5 RID: 10997
			public class HEATCOMPRESSOR
			{
				// Token: 0x0400BD16 RID: 48406
				public static LocString NAME = UI.FormatAsLink("Liquid Heatquilizer", "HEATCOMPRESSOR");

				// Token: 0x0400BD17 RID: 48407
				public static LocString DESC = "\"Room temperature\" is relative, really.";

				// Token: 0x0400BD18 RID: 48408
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Heats or cools ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" to match ambient ",
					UI.FormatAsLink("Air Temperature", "HEAT"),
					"."
				});
			}

			// Token: 0x02002AF6 RID: 10998
			public class PARTYCAKE
			{
				// Token: 0x0400BD19 RID: 48409
				public static LocString NAME = UI.FormatAsLink("Triple Decker Cake", "PARTYCAKE");

				// Token: 0x0400BD1A RID: 48410
				public static LocString DESC = "Any way you slice it, that's a good looking cake.";

				// Token: 0x0400BD1B RID: 48411
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Increases ",
					UI.FormatAsLink("Decor", "DECOR"),
					", contributing to ",
					UI.FormatAsLink("Morale", "MORALE"),
					".\n\nAdds a ",
					UI.FormatAsLink("Morale", "MORALE"),
					" bonus to Duplicants' parties."
				});
			}

			// Token: 0x02002AF7 RID: 10999
			public class RAILGUN
			{
				// Token: 0x0400BD1C RID: 48412
				public static LocString NAME = UI.FormatAsLink("Interplanetary Launcher", "RAILGUN");

				// Token: 0x0400BD1D RID: 48413
				public static LocString DESC = "It's tempting to climb inside but trust me... don't.";

				// Token: 0x0400BD1E RID: 48414
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Launches ",
					UI.FormatAsLink("Interplanetary Payloads", "RAILGUNPAYLOAD"),
					" between Planetoids.\n\nPayloads can contain ",
					UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
					", ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					", or ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" materials.\n\nCannot transport Duplicants."
				});

				// Token: 0x0400BD1F RID: 48415
				public static LocString SIDESCREEN_HEP_REQUIRED = "Launch cost: {current} / {required} radbolts";

				// Token: 0x0400BD20 RID: 48416
				public static LocString LOGIC_PORT = "Launch Toggle";

				// Token: 0x0400BD21 RID: 48417
				public static LocString LOGIC_PORT_ACTIVE = UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ": Enable payload launching.";

				// Token: 0x0400BD22 RID: 48418
				public static LocString LOGIC_PORT_INACTIVE = UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ": Disable payload launching.";
			}

			// Token: 0x02002AF8 RID: 11000
			public class RAILGUNPAYLOADOPENER
			{
				// Token: 0x0400BD23 RID: 48419
				public static LocString NAME = UI.FormatAsLink("Payload Opener", "RAILGUNPAYLOADOPENER");

				// Token: 0x0400BD24 RID: 48420
				public static LocString DESC = "Payload openers can be hooked up to conveyors, plumbing and ventilation for improved sorting.";

				// Token: 0x0400BD25 RID: 48421
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Unpacks ",
					UI.FormatAsLink("Interplanetary Payloads", "RAILGUNPAYLOAD"),
					" delivered by Duplicants.\n\nAutomatically separates ",
					UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
					", ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					", and ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" materials and distributes them to the appropriate systems."
				});
			}

			// Token: 0x02002AF9 RID: 11001
			public class LANDINGBEACON
			{
				// Token: 0x0400BD26 RID: 48422
				public static LocString NAME = UI.FormatAsLink("Targeting Beacon", "LANDINGBEACON");

				// Token: 0x0400BD27 RID: 48423
				public static LocString DESC = "Microtarget where your " + UI.FormatAsLink("Interplanetary Payload", "RAILGUNPAYLOAD") + " lands on a Planetoid surface.";

				// Token: 0x0400BD28 RID: 48424
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Guides ",
					UI.FormatAsLink("Interplanetary Payloads", "RAILGUNPAYLOAD"),
					" and ",
					UI.FormatAsLink("Orbital Cargo Modules", "ORBITALCARGOMODULE"),
					" to land nearby.\n\n",
					UI.FormatAsLink("Interplanetary Payloads", "RAILGUNPAYLOAD"),
					" must be launched from a ",
					UI.FormatAsLink("Interplanetary Launcher", "RAILGUN"),
					"."
				});
			}

			// Token: 0x02002AFA RID: 11002
			public class DIAMONDPRESS
			{
				// Token: 0x0400BD29 RID: 48425
				public static LocString NAME = UI.FormatAsLink("Diamond Press", "DIAMONDPRESS");

				// Token: 0x0400BD2A RID: 48426
				public static LocString DESC = "Crushes refined carbon into diamond.";

				// Token: 0x0400BD2B RID: 48427
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Power", "POWER"),
					" and ",
					UI.FormatAsLink("Radbolts", "RADIATION"),
					" to crush ",
					UI.FormatAsLink("Refined Carbon", "REFINEDCARBON"),
					" into ",
					UI.FormatAsLink("Diamond", "DIAMOND"),
					".\n\nDuplicants will not fabricate items unless recipes are queued and ",
					UI.FormatAsLink("Refined Carbon", "REFINEDCARBON"),
					" has been discovered."
				});

				// Token: 0x0400BD2C RID: 48428
				public static LocString REFINED_CARBON_RECIPE_DESCRIPTION = "Converts {1} to {0}";
			}

			// Token: 0x02002AFB RID: 11003
			public class ESCAPEPOD
			{
				// Token: 0x0400BD2D RID: 48429
				public static LocString NAME = UI.FormatAsLink("Escape Pod", "ESCAPEPOD");

				// Token: 0x0400BD2E RID: 48430
				public static LocString DESC = "Delivers a Duplicant from a stranded rocket to the nearest Planetoid.";
			}

			// Token: 0x02002AFC RID: 11004
			public class ROCKETINTERIORLIQUIDOUTPUTPORT
			{
				// Token: 0x0400BD2F RID: 48431
				public static LocString NAME = UI.FormatAsLink("Liquid Spacefarer Output Port", "ROCKETINTERIORLIQUIDOUTPUTPORT");

				// Token: 0x0400BD30 RID: 48432
				public static LocString DESC = "A direct attachment to the input port on the exterior of a rocket.";

				// Token: 0x0400BD31 RID: 48433
				public static LocString EFFECT = "Allows a direct conduit connection into the " + UI.FormatAsLink("Spacefarer Module", "HABITATMODULEMEDIUM") + " of a rocket.";
			}

			// Token: 0x02002AFD RID: 11005
			public class ROCKETINTERIORLIQUIDINPUTPORT
			{
				// Token: 0x0400BD32 RID: 48434
				public static LocString NAME = UI.FormatAsLink("Liquid Spacefarer Input Port", "ROCKETINTERIORLIQUIDINPUTPORT");

				// Token: 0x0400BD33 RID: 48435
				public static LocString DESC = "A direct attachment to the output port on the exterior of a rocket.";

				// Token: 0x0400BD34 RID: 48436
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows a direct conduit connection out of the ",
					UI.FormatAsLink("Spacefarer Module", "HABITATMODULEMEDIUM"),
					" of a rocket.\nCan be used to vent ",
					UI.FormatAsLink("Liquids", "ELEMENTS_LIQUID"),
					" to space during flight."
				});
			}

			// Token: 0x02002AFE RID: 11006
			public class ROCKETINTERIORGASOUTPUTPORT
			{
				// Token: 0x0400BD35 RID: 48437
				public static LocString NAME = UI.FormatAsLink("Gas Spacefarer Output Port", "ROCKETINTERIORGASOUTPUTPORT");

				// Token: 0x0400BD36 RID: 48438
				public static LocString DESC = "A direct attachment to the input port on the exterior of a rocket.";

				// Token: 0x0400BD37 RID: 48439
				public static LocString EFFECT = "Allows a direct conduit connection into the " + UI.FormatAsLink("Spacefarer Module", "HABITATMODULEMEDIUM") + " of a rocket.";
			}

			// Token: 0x02002AFF RID: 11007
			public class ROCKETINTERIORGASINPUTPORT
			{
				// Token: 0x0400BD38 RID: 48440
				public static LocString NAME = UI.FormatAsLink("Gas Spacefarer Input Port", "ROCKETINTERIORGASINPUTPORT");

				// Token: 0x0400BD39 RID: 48441
				public static LocString DESC = "A direct attachment leading to the output port on the exterior of the rocket.";

				// Token: 0x0400BD3A RID: 48442
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Allows a direct conduit connection out of the ",
					UI.FormatAsLink("Spacefarer Module", "HABITATMODULEMEDIUM"),
					" of the rocket.\nCan be used to vent ",
					UI.FormatAsLink("Gasses", "ELEMENTS_GAS"),
					" to space during flight."
				});
			}

			// Token: 0x02002B00 RID: 11008
			public class MISSILELAUNCHER
			{
				// Token: 0x0400BD3B RID: 48443
				public static LocString NAME = UI.FormatAsLink("Meteor Blaster", "MISSILELAUNCHER");

				// Token: 0x0400BD3C RID: 48444
				public static LocString DESC = "Some meteors drop harvestable resources when they're blown to smithereens.";

				// Token: 0x0400BD3D RID: 48445
				public static LocString EFFECT = "Fires explosive projectiles at incoming space objects to defend the colony from impact-related damage.\n\nProjectiles must be crafted at a " + UI.FormatAsLink("Blastshot Maker", "MISSILEFABRICATOR") + ".\n\nRange: 16 tiles horizontally, 32 tiles vertically.";

				// Token: 0x0400BD3E RID: 48446
				public static LocString TARGET_SELECTION_HEADER = "Short Range Target Selection";

				// Token: 0x020038BE RID: 14526
				public class BODY
				{
					// Token: 0x0400E4AE RID: 58542
					public static LocString CONTAINER1 = "Fires " + UI.FormatAsLink("Blastshot", "MISSILELAUNCHER") + " shells at meteor showers to defend the colony from impact-related damage.\n\nRange: 16 tiles horizontally, 32 tiles vertically.\n\nMeteors that have been blown to smithereens leave behind no harvestable resources.";
				}
			}

			// Token: 0x02002B01 RID: 11009
			public class CRITTERCONDO
			{
				// Token: 0x0400BD3F RID: 48447
				public static LocString NAME = UI.FormatAsLink("Critter Condo", "CRITTERCONDO");

				// Token: 0x0400BD40 RID: 48448
				public static LocString DESC = "It's nice to have nice things.";

				// Token: 0x0400BD41 RID: 48449
				public static LocString EFFECT = "Provides a comfortable lounge area that boosts " + UI.FormatAsLink("Critter", "CREATURES") + " happiness.";
			}

			// Token: 0x02002B02 RID: 11010
			public class UNDERWATERCRITTERCONDO
			{
				// Token: 0x0400BD42 RID: 48450
				public static LocString NAME = UI.FormatAsLink("Water Fort", "UNDERWATERCRITTERCONDO");

				// Token: 0x0400BD43 RID: 48451
				public static LocString DESC = "Even wild critters are happier after they've had a little R&R.";

				// Token: 0x0400BD44 RID: 48452
				public static LocString EFFECT = string.Concat(new string[]
				{
					"A fancy respite area for adult ",
					UI.FormatAsLink("Pokeshells", "CRABSPECIES"),
					" and ",
					UI.FormatAsLink("Pacu", "PACUSPECIES"),
					"."
				});
			}

			// Token: 0x02002B03 RID: 11011
			public class AIRBORNECRITTERCONDO
			{
				// Token: 0x0400BD45 RID: 48453
				public static LocString NAME = UI.FormatAsLink("Airborne Critter Condo", "AIRBORNECRITTERCONDO");

				// Token: 0x0400BD46 RID: 48454
				public static LocString DESC = "Triggers natural nesting instincts and improves critters' moods.";

				// Token: 0x0400BD47 RID: 48455
				public static LocString EFFECT = string.Concat(new string[]
				{
					"A hanging respite area for adult ",
					UI.FormatAsLink("Pufts", "PUFT"),
					", ",
					UI.FormatAsLink("Gassy Moos", "MOOSPECIES"),
					" and ",
					UI.FormatAsLink("Shine Bugs", "LIGHTBUG"),
					"."
				});
			}

			// Token: 0x02002B04 RID: 11012
			public class MASSIVEHEATSINK
			{
				// Token: 0x0400BD48 RID: 48456
				public static LocString NAME = UI.FormatAsLink("Anti Entropy Thermo-Nullifier", "MASSIVEHEATSINK");

				// Token: 0x0400BD49 RID: 48457
				public static LocString DESC = "";

				// Token: 0x0400BD4A RID: 48458
				public static LocString EFFECT = string.Concat(new string[]
				{
					"A self-sustaining machine powered by what appears to be refined ",
					UI.FormatAsLink("Neutronium", "UNOBTANIUM"),
					".\n\nAbsorbs and neutralizes ",
					UI.FormatAsLink("Heat", "HEAT"),
					" energy when provided with piped ",
					UI.FormatAsLink("Hydrogen Gas", "HYDROGEN"),
					"."
				});
			}

			// Token: 0x02002B05 RID: 11013
			public class MEGABRAINTANK
			{
				// Token: 0x0400BD4B RID: 48459
				public static LocString NAME = UI.FormatAsLink("Somnium Synthesizer", "MEGABRAINTANK");

				// Token: 0x0400BD4C RID: 48460
				public static LocString DESC = "";

				// Token: 0x0400BD4D RID: 48461
				public static LocString EFFECT = string.Concat(new string[]
				{
					"An organic multi-cortex repository and processing system fuelled by ",
					UI.FormatAsLink("Oxygen", "OXYGEN"),
					".\n\nAnalyzes ",
					UI.FormatAsLink("Dream Journals", "DREAMJOURNAL"),
					" produced by Duplicants wearing ",
					UI.FormatAsLink("Pajamas", "SLEEP_CLINIC_PAJAMAS"),
					".\n\nProvides a sustainable boost to Duplicant skills and abilities throughout the colony."
				});
			}

			// Token: 0x02002B06 RID: 11014
			public class GRAVITASCREATUREMANIPULATOR
			{
				// Token: 0x0400BD4E RID: 48462
				public static LocString NAME = UI.FormatAsLink("Critter Flux-O-Matic", "GRAVITASCREATUREMANIPULATOR");

				// Token: 0x0400BD4F RID: 48463
				public static LocString DESC = "";

				// Token: 0x0400BD50 RID: 48464
				public static LocString EFFECT = "An experimental DNA manipulator.\n\nAnalyzes " + UI.FormatAsLink("Critters", "CREATURES") + " to transform base morphs into random variants of their species.";
			}

			// Token: 0x02002B07 RID: 11015
			public class FACILITYBACKWALLWINDOW
			{
				// Token: 0x0400BD51 RID: 48465
				public static LocString NAME = "Window";

				// Token: 0x0400BD52 RID: 48466
				public static LocString DESC = "";

				// Token: 0x0400BD53 RID: 48467
				public static LocString EFFECT = "A tall, thin window.";
			}

			// Token: 0x02002B08 RID: 11016
			public class POIBUNKEREXTERIORDOOR
			{
				// Token: 0x0400BD54 RID: 48468
				public static LocString NAME = "Security Door";

				// Token: 0x0400BD55 RID: 48469
				public static LocString EFFECT = "A strong door with a sophisticated genetic lock.";

				// Token: 0x0400BD56 RID: 48470
				public static LocString DESC = "";
			}

			// Token: 0x02002B09 RID: 11017
			public class POIDOORINTERNAL
			{
				// Token: 0x0400BD57 RID: 48471
				public static LocString NAME = "Security Door";

				// Token: 0x0400BD58 RID: 48472
				public static LocString EFFECT = "A strong door with a sophisticated genetic lock.";

				// Token: 0x0400BD59 RID: 48473
				public static LocString DESC = "";
			}

			// Token: 0x02002B0A RID: 11018
			public class POIFACILITYDOOR
			{
				// Token: 0x0400BD5A RID: 48474
				public static LocString NAME = "Lobby Doors";

				// Token: 0x0400BD5B RID: 48475
				public static LocString EFFECT = "Large double doors that were once the main entrance to a large facility.";

				// Token: 0x0400BD5C RID: 48476
				public static LocString DESC = "";
			}

			// Token: 0x02002B0B RID: 11019
			public class POIDLC2SHOWROOMDOOR
			{
				// Token: 0x0400BD5D RID: 48477
				public static LocString NAME = "Showroom Doors";

				// Token: 0x0400BD5E RID: 48478
				public static LocString EFFECT = "Large double doors identical to those you might find at the main entrance to a large facility.";

				// Token: 0x0400BD5F RID: 48479
				public static LocString DESC = "";
			}

			// Token: 0x02002B0C RID: 11020
			public class VENDINGMACHINE
			{
				// Token: 0x0400BD60 RID: 48480
				public static LocString NAME = "Vending Machine";

				// Token: 0x0400BD61 RID: 48481
				public static LocString DESC = "A pristine " + UI.FormatAsLink("Nutrient Bar", "FIELDRATION") + " dispenser.";
			}

			// Token: 0x02002B0D RID: 11021
			public class GENESHUFFLER
			{
				// Token: 0x0400BD62 RID: 48482
				public static LocString NAME = "Neural Vacillator";

				// Token: 0x0400BD63 RID: 48483
				public static LocString DESC = "A massive synthetic brain, suspended in saline solution.\n\nThere is a chair attached to the device with room for one person.";
			}

			// Token: 0x02002B0E RID: 11022
			public class PROPTALLPLANT
			{
				// Token: 0x0400BD64 RID: 48484
				public static LocString NAME = "Potted Plant";

				// Token: 0x0400BD65 RID: 48485
				public static LocString DESC = "Looking closely, it appears to be fake.";
			}

			// Token: 0x02002B0F RID: 11023
			public class PROPTABLE
			{
				// Token: 0x0400BD66 RID: 48486
				public static LocString NAME = "Table";

				// Token: 0x0400BD67 RID: 48487
				public static LocString DESC = "A table and some chairs.";
			}

			// Token: 0x02002B10 RID: 11024
			public class PROPDESK
			{
				// Token: 0x0400BD68 RID: 48488
				public static LocString NAME = "Computer Desk";

				// Token: 0x0400BD69 RID: 48489
				public static LocString DESC = "An intact office desk, decorated with several personal belongings and a barely functioning computer.";
			}

			// Token: 0x02002B11 RID: 11025
			public class PROPFACILITYCHAIR
			{
				// Token: 0x0400BD6A RID: 48490
				public static LocString NAME = "Lobby Chair";

				// Token: 0x0400BD6B RID: 48491
				public static LocString DESC = "A chair where visitors can comfortably wait before their appointments.";
			}

			// Token: 0x02002B12 RID: 11026
			public class PROPFACILITYCOUCH
			{
				// Token: 0x0400BD6C RID: 48492
				public static LocString NAME = "Lobby Couch";

				// Token: 0x0400BD6D RID: 48493
				public static LocString DESC = "A couch where visitors can comfortably wait before their appointments.";
			}

			// Token: 0x02002B13 RID: 11027
			public class PROPFACILITYDESK
			{
				// Token: 0x0400BD6E RID: 48494
				public static LocString NAME = "Director's Desk";

				// Token: 0x0400BD6F RID: 48495
				public static LocString DESC = "A spotless desk filled with impeccably organized office supplies.\n\nA photo peeks out from beneath the desk pad, depicting two beaming young women in caps and gowns.\n\nThe photo is quite old.";
			}

			// Token: 0x02002B14 RID: 11028
			public class PROPFACILITYTABLE
			{
				// Token: 0x0400BD70 RID: 48496
				public static LocString NAME = "Coffee Table";

				// Token: 0x0400BD71 RID: 48497
				public static LocString DESC = "A low coffee table that may have once held old science magazines.";
			}

			// Token: 0x02002B15 RID: 11029
			public class PROPFACILITYSTATUE
			{
				// Token: 0x0400BD72 RID: 48498
				public static LocString NAME = "Gravitas Monument";

				// Token: 0x0400BD73 RID: 48499
				public static LocString DESC = "A large, modern sculpture that sits in the center of the lobby.\n\nIt's an artistic cross between an hourglass shape and a double helix.";
			}

			// Token: 0x02002B16 RID: 11030
			public class PROPFACILITYCHANDELIER
			{
				// Token: 0x0400BD74 RID: 48500
				public static LocString NAME = "Chandelier";

				// Token: 0x0400BD75 RID: 48501
				public static LocString DESC = "A large chandelier that hangs from the ceiling.\n\nIt does not appear to function.";
			}

			// Token: 0x02002B17 RID: 11031
			public class PROPFACILITYGLOBEDROORS
			{
				// Token: 0x0400BD76 RID: 48502
				public static LocString NAME = "Filing Cabinet";

				// Token: 0x0400BD77 RID: 48503
				public static LocString DESC = "A filing cabinet for storing hard copy employee records.\n\nThe contents have been shredded.";
			}

			// Token: 0x02002B18 RID: 11032
			public class PROPFACILITYDISPLAY1
			{
				// Token: 0x0400BD78 RID: 48504
				public static LocString NAME = "Electronic Display";

				// Token: 0x0400BD79 RID: 48505
				public static LocString DESC = "An electronic display projecting the blueprint of a familiar device.\n\nIt looks like a Printing Pod.";
			}

			// Token: 0x02002B19 RID: 11033
			public class PROPFACILITYDISPLAY2
			{
				// Token: 0x0400BD7A RID: 48506
				public static LocString NAME = "Electronic Display";

				// Token: 0x0400BD7B RID: 48507
				public static LocString DESC = "An electronic display projecting the blueprint of a familiar device.\n\nIt looks like a Mining Gun.";
			}

			// Token: 0x02002B1A RID: 11034
			public class PROPFACILITYDISPLAY3
			{
				// Token: 0x0400BD7C RID: 48508
				public static LocString NAME = "Electronic Display";

				// Token: 0x0400BD7D RID: 48509
				public static LocString DESC = "An electronic display projecting the blueprint of a strange device.\n\nPerhaps these displays were used to entice visitors.";
			}

			// Token: 0x02002B1B RID: 11035
			public class PROPFACILITYTALLPLANT
			{
				// Token: 0x0400BD7E RID: 48510
				public static LocString NAME = "Office Plant";

				// Token: 0x0400BD7F RID: 48511
				public static LocString DESC = "It's survived the vacuum of space by virtue of being plastic.";
			}

			// Token: 0x02002B1C RID: 11036
			public class PROPFACILITYLAMP
			{
				// Token: 0x0400BD80 RID: 48512
				public static LocString NAME = "Light Fixture";

				// Token: 0x0400BD81 RID: 48513
				public static LocString DESC = "A long light fixture that hangs from the ceiling.\n\nIt does not appear to function.";
			}

			// Token: 0x02002B1D RID: 11037
			public class PROPFACILITYWALLDEGREE
			{
				// Token: 0x0400BD82 RID: 48514
				public static LocString NAME = "Doctorate Degree";

				// Token: 0x0400BD83 RID: 48515
				public static LocString DESC = "Certification in Applied Physics, awarded in recognition of one \"Jacquelyn A. Stern\".";
			}

			// Token: 0x02002B1E RID: 11038
			public class PROPFACILITYPAINTING
			{
				// Token: 0x0400BD84 RID: 48516
				public static LocString NAME = "Landscape Portrait";

				// Token: 0x0400BD85 RID: 48517
				public static LocString DESC = "A painting featuring a copse of fir trees and a magnificent mountain range on the horizon.\n\nThe air in the room prickles with the sensation that I'm not meant to be here.";
			}

			// Token: 0x02002B1F RID: 11039
			public class PROPRECEPTIONDESK
			{
				// Token: 0x0400BD86 RID: 48518
				public static LocString NAME = "Reception Desk";

				// Token: 0x0400BD87 RID: 48519
				public static LocString DESC = "A full coffee cup and a note abandoned mid sentence sit behind the desk.\n\nIt gives me an eerie feeling, as if the receptionist has stepped out and will return any moment.";
			}

			// Token: 0x02002B20 RID: 11040
			public class PROPELEVATOR
			{
				// Token: 0x0400BD88 RID: 48520
				public static LocString NAME = "Broken Elevator";

				// Token: 0x0400BD89 RID: 48521
				public static LocString DESC = "Out of service.\n\nThe buttons inside indicate it went down more than a dozen floors at one point in time.";
			}

			// Token: 0x02002B21 RID: 11041
			public class SETLOCKER
			{
				// Token: 0x0400BD8A RID: 48522
				public static LocString NAME = "Locker";

				// Token: 0x0400BD8B RID: 48523
				public static LocString DESC = "A basic metal locker.\n\nIt contains an assortment of personal effects.";
			}

			// Token: 0x02002B22 RID: 11042
			public class PROPEXOSETLOCKER
			{
				// Token: 0x0400BD8C RID: 48524
				public static LocString NAME = "Off-site Locker";

				// Token: 0x0400BD8D RID: 48525
				public static LocString DESC = "A locker made with ultra-lightweight textiles.\n\nIt contains an assortment of personal effects.";
			}

			// Token: 0x02002B23 RID: 11043
			public class MISSILESETLOCKER
			{
				// Token: 0x0400BD8E RID: 48526
				public static LocString NAME = "Explosives Locker";

				// Token: 0x0400BD8F RID: 48527
				public static LocString DESC = "A locker that once belonged to an explosives engineer.\n\nIt holds one " + UI.FormatAsLink("Intracosmic Blastshot", "MISSILELAUNCHER") + ".";
			}

			// Token: 0x02002B24 RID: 11044
			public class PROPGRAVITASSMALLSEEDLOCKER
			{
				// Token: 0x0400BD90 RID: 48528
				public static LocString NAME = "Wall Cabinet";

				// Token: 0x0400BD91 RID: 48529
				public static LocString DESC = "A small glass cabinet.\n\nThere's a biohazard symbol on it.";
			}

			// Token: 0x02002B25 RID: 11045
			public class PROPLIGHT
			{
				// Token: 0x0400BD92 RID: 48530
				public static LocString NAME = "Light Fixture";

				// Token: 0x0400BD93 RID: 48531
				public static LocString DESC = "An elegant ceiling lamp, slightly worse for wear.";
			}

			// Token: 0x02002B26 RID: 11046
			public class PROPLADDER
			{
				// Token: 0x0400BD94 RID: 48532
				public static LocString NAME = "Ladder";

				// Token: 0x0400BD95 RID: 48533
				public static LocString DESC = "A hard plastic ladder.";
			}

			// Token: 0x02002B27 RID: 11047
			public class PROPSKELETON
			{
				// Token: 0x0400BD96 RID: 48534
				public static LocString NAME = "Model Skeleton";

				// Token: 0x0400BD97 RID: 48535
				public static LocString DESC = "A detailed anatomical model.\n\nIt appears to be made of resin.";
			}

			// Token: 0x02002B28 RID: 11048
			public class PROPSURFACESATELLITE1
			{
				// Token: 0x0400BD98 RID: 48536
				public static LocString NAME = "Crashed Satellite";

				// Token: 0x0400BD99 RID: 48537
				public static LocString DESC = "All that remains of a once peacefully orbiting satellite.";
			}

			// Token: 0x02002B29 RID: 11049
			public class PROPSURFACESATELLITE2
			{
				// Token: 0x0400BD9A RID: 48538
				public static LocString NAME = "Wrecked Satellite";

				// Token: 0x0400BD9B RID: 48539
				public static LocString DESC = "All that remains of a once peacefully orbiting satellite.";
			}

			// Token: 0x02002B2A RID: 11050
			public class PROPSURFACESATELLITE3
			{
				// Token: 0x0400BD9C RID: 48540
				public static LocString NAME = "Crushed Satellite";

				// Token: 0x0400BD9D RID: 48541
				public static LocString DESC = "All that remains of a once peacefully orbiting satellite.";
			}

			// Token: 0x02002B2B RID: 11051
			public class PROPCLOCK
			{
				// Token: 0x0400BD9E RID: 48542
				public static LocString NAME = "Clock";

				// Token: 0x0400BD9F RID: 48543
				public static LocString DESC = "A simple wall clock.\n\nIt is no longer ticking.";
			}

			// Token: 0x02002B2C RID: 11052
			public class PROPGRAVITASDECORATIVEWINDOW
			{
				// Token: 0x0400BDA0 RID: 48544
				public static LocString NAME = "Window";

				// Token: 0x0400BDA1 RID: 48545
				public static LocString DESC = "A tall, thin window which once pointed to a courtyard.";
			}

			// Token: 0x02002B2D RID: 11053
			public class PROPGRAVITASLABWINDOW
			{
				// Token: 0x0400BDA2 RID: 48546
				public static LocString NAME = "Lab Window";

				// Token: 0x0400BDA3 RID: 48547
				public static LocString DESC = "";

				// Token: 0x0400BDA4 RID: 48548
				public static LocString EFFECT = "A lab window. Formerly a portal to the outside world.";
			}

			// Token: 0x02002B2E RID: 11054
			public class PROPGRAVITASLABWINDOWHORIZONTAL
			{
				// Token: 0x0400BDA5 RID: 48549
				public static LocString NAME = "Lab Window";

				// Token: 0x0400BDA6 RID: 48550
				public static LocString DESC = "";

				// Token: 0x0400BDA7 RID: 48551
				public static LocString EFFECT = "A lab window.\n\nSomeone once stared out of this, contemplating the results of an experiment.";
			}

			// Token: 0x02002B2F RID: 11055
			public class PROPGRAVITASLABWALL
			{
				// Token: 0x0400BDA8 RID: 48552
				public static LocString NAME = "Lab Wall";

				// Token: 0x0400BDA9 RID: 48553
				public static LocString DESC = "";

				// Token: 0x0400BDAA RID: 48554
				public static LocString EFFECT = "A regular wall that once existed in a working lab.";
			}

			// Token: 0x02002B30 RID: 11056
			public class GRAVITASCONTAINER
			{
				// Token: 0x0400BDAB RID: 48555
				public static LocString NAME = "Pajama Cubby";

				// Token: 0x0400BDAC RID: 48556
				public static LocString DESC = "";

				// Token: 0x0400BDAD RID: 48557
				public static LocString EFFECT = "A clothing storage unit.\n\nIt contains ultra-soft sleepwear.";
			}

			// Token: 0x02002B31 RID: 11057
			public class GRAVITASLABLIGHT
			{
				// Token: 0x0400BDAE RID: 48558
				public static LocString NAME = "LED Light";

				// Token: 0x0400BDAF RID: 48559
				public static LocString DESC = "";

				// Token: 0x0400BDB0 RID: 48560
				public static LocString EFFECT = "An overhead light therapy lamp designed to soothe the minds.";
			}

			// Token: 0x02002B32 RID: 11058
			public class GRAVITASDOOR
			{
				// Token: 0x0400BDB1 RID: 48561
				public static LocString NAME = "Gravitas Door";

				// Token: 0x0400BDB2 RID: 48562
				public static LocString DESC = "";

				// Token: 0x0400BDB3 RID: 48563
				public static LocString EFFECT = "An office door to an office that no longer exists.";
			}

			// Token: 0x02002B33 RID: 11059
			public class PROPGRAVITASWALL
			{
				// Token: 0x0400BDB4 RID: 48564
				public static LocString NAME = "Wall";

				// Token: 0x0400BDB5 RID: 48565
				public static LocString DESC = "";

				// Token: 0x0400BDB6 RID: 48566
				public static LocString EFFECT = "The wall of a once-great scientific facility.";
			}

			// Token: 0x02002B34 RID: 11060
			public class PROPGRAVITASWALLPURPLE
			{
				// Token: 0x0400BDB7 RID: 48567
				public static LocString NAME = "Wall";

				// Token: 0x0400BDB8 RID: 48568
				public static LocString DESC = "";

				// Token: 0x0400BDB9 RID: 48569
				public static LocString EFFECT = "The wall of an ambitious research and development department.";
			}

			// Token: 0x02002B35 RID: 11061
			public class PROPGRAVITASWALLPURPLEWHITEDIAGONAL
			{
				// Token: 0x0400BDBA RID: 48570
				public static LocString NAME = "Wall";

				// Token: 0x0400BDBB RID: 48571
				public static LocString DESC = "";

				// Token: 0x0400BDBC RID: 48572
				public static LocString EFFECT = "The wall of an ambitious research and development department.";
			}

			// Token: 0x02002B36 RID: 11062
			public class PROPGRAVITASDISPLAY4
			{
				// Token: 0x0400BDBD RID: 48573
				public static LocString NAME = "Electronic Display";

				// Token: 0x0400BDBE RID: 48574
				public static LocString DESC = "An electronic display projecting the blueprint of a robotic device.\n\nIt looks like a ceiling robot.";
			}

			// Token: 0x02002B37 RID: 11063
			public class PROPDLC2DISPLAY1
			{
				// Token: 0x0400BDBF RID: 48575
				public static LocString NAME = "Electronic Display";

				// Token: 0x0400BDC0 RID: 48576
				public static LocString DESC = "An electronic display projecting the blueprint of an engineering project.\n\nIt looks like a pump of some kind.";
			}

			// Token: 0x02002B38 RID: 11064
			public class PROPGRAVITASCEILINGROBOT
			{
				// Token: 0x0400BDC1 RID: 48577
				public static LocString NAME = "Ceiling Robot";

				// Token: 0x0400BDC2 RID: 48578
				public static LocString DESC = "Non-functioning robotic arms that once assisted lab technicians.";
			}

			// Token: 0x02002B39 RID: 11065
			public class PROPGRAVITASFLOORROBOT
			{
				// Token: 0x0400BDC3 RID: 48579
				public static LocString NAME = "Robotic Arm";

				// Token: 0x0400BDC4 RID: 48580
				public static LocString DESC = "The grasping robotic claw designed to assist technicians in a lab.";
			}

			// Token: 0x02002B3A RID: 11066
			public class PROPGRAVITASJAR1
			{
				// Token: 0x0400BDC5 RID: 48581
				public static LocString NAME = "Big Brain Jar";

				// Token: 0x0400BDC6 RID: 48582
				public static LocString DESC = "An abnormally large brain floating in embalming liquid to prevent decomposition.";
			}

			// Token: 0x02002B3B RID: 11067
			public class PROPGRAVITASCREATUREPOSTER
			{
				// Token: 0x0400BDC7 RID: 48583
				public static LocString NAME = "Anatomy Poster";

				// Token: 0x0400BDC8 RID: 48584
				public static LocString DESC = "An anatomical illustration of the very first " + UI.FormatAsLink("Hatch", "HATCH") + " ever produced.\n\nWhile the ratio of egg sac to brain may appear outlandish, it is in fact to scale.";
			}

			// Token: 0x02002B3C RID: 11068
			public class PROPGRAVITASDESKPODIUM
			{
				// Token: 0x0400BDC9 RID: 48585
				public static LocString NAME = "Computer Podium";

				// Token: 0x0400BDCA RID: 48586
				public static LocString DESC = "A clutter-proof desk to minimize distractions.\n\nThere appears to be something stored in the computer.";
			}

			// Token: 0x02002B3D RID: 11069
			public class PROPGRAVITASFIRSTAIDKIT
			{
				// Token: 0x0400BDCB RID: 48587
				public static LocString NAME = "First Aid Kit";

				// Token: 0x0400BDCC RID: 48588
				public static LocString DESC = "It looks like it's been used a lot.";
			}

			// Token: 0x02002B3E RID: 11070
			public class PROPGRAVITASHANDSCANNER
			{
				// Token: 0x0400BDCD RID: 48589
				public static LocString NAME = "Hand Scanner";

				// Token: 0x0400BDCE RID: 48590
				public static LocString DESC = "A sophisticated security device.\n\nIt appears to use a method other than fingerprints to verify an individual's identity.";
			}

			// Token: 0x02002B3F RID: 11071
			public class PROPGRAVITASLABTABLE
			{
				// Token: 0x0400BDCF RID: 48591
				public static LocString NAME = "Lab Desk";

				// Token: 0x0400BDD0 RID: 48592
				public static LocString DESC = "The quaint research desk of a departed lab technician.\n\nPerhaps the computer stores something of interest.";
			}

			// Token: 0x02002B40 RID: 11072
			public class PROPGRAVITASROBTICTABLE
			{
				// Token: 0x0400BDD1 RID: 48593
				public static LocString NAME = "Robotics Research Desk";

				// Token: 0x0400BDD2 RID: 48594
				public static LocString DESC = "The work space of an extinct robotics technician who left behind some unfinished prototypes.";
			}

			// Token: 0x02002B41 RID: 11073
			public class PROPDLC2GEOTHERMALCART
			{
				// Token: 0x0400BDD3 RID: 48595
				public static LocString NAME = "Service Cart";

				// Token: 0x0400BDD4 RID: 48596
				public static LocString DESC = "Maintenance equipment that once flushed debris out of complex mechanisms.\n\nOne of the wheels is squeaky.";
			}

			// Token: 0x02002B42 RID: 11074
			public class PROPGRAVITASSHELF
			{
				// Token: 0x0400BDD5 RID: 48597
				public static LocString NAME = "Shelf";

				// Token: 0x0400BDD6 RID: 48598
				public static LocString DESC = "A shelf holding jars just out of reach for a short person.";
			}

			// Token: 0x02002B43 RID: 11075
			public class PROPGRAVITASTOOLSHELF
			{
				// Token: 0x0400BDD7 RID: 48599
				public static LocString NAME = "Tool Rack";

				// Token: 0x0400BDD8 RID: 48600
				public static LocString DESC = "A wall-mounted rack for storing and displaying useful tools at a not-so-useful height.";
			}

			// Token: 0x02002B44 RID: 11076
			public class PROPGRAVITASTOOLCRATE
			{
				// Token: 0x0400BDD9 RID: 48601
				public static LocString NAME = "Tool Crate";

				// Token: 0x0400BDDA RID: 48602
				public static LocString DESC = "A packing crate intended for safety equipment.\n\nIt has been repurposed for tool storage.";
			}

			// Token: 0x02002B45 RID: 11077
			public class PROPGRAVITASFIREEXTINGUISHER
			{
				// Token: 0x0400BDDB RID: 48603
				public static LocString NAME = "Broken Fire Extinguisher";

				// Token: 0x0400BDDC RID: 48604
				public static LocString DESC = "Essential lab equipment.\n\nThe inspection tag indicates it has long expired.";
			}

			// Token: 0x02002B46 RID: 11078
			public class PROPGRAVITASJAR2
			{
				// Token: 0x0400BDDD RID: 48605
				public static LocString NAME = "Sample Jar";

				// Token: 0x0400BDDE RID: 48606
				public static LocString DESC = "The corpse of a proto-hatch creature meticulously preserved in a jar.";
			}

			// Token: 0x02002B47 RID: 11079
			public class PROPEXOSHELFLONG
			{
				// Token: 0x0400BDDF RID: 48607
				public static LocString NAME = "Long Prefab Shelf";

				// Token: 0x0400BDE0 RID: 48608
				public static LocString DESC = "A shelf made out of flat-packed pieces that can be assembled in various ways.\n\nThis is the long way.";
			}

			// Token: 0x02002B48 RID: 11080
			public class PROPEXOSHELSHORT
			{
				// Token: 0x0400BDE1 RID: 48609
				public static LocString NAME = "Prefab Shelf";

				// Token: 0x0400BDE2 RID: 48610
				public static LocString DESC = "A shelf made out of flat-packed pieces that can be assembled in various ways.\n\nIt looks nice, actually.";
			}

			// Token: 0x02002B49 RID: 11081
			public class PROPHUMANMURPHYBED
			{
				// Token: 0x0400BDE3 RID: 48611
				public static LocString NAME = "Murphy Bed";

				// Token: 0x0400BDE4 RID: 48612
				public static LocString DESC = "A bed that folds into the wall, for small live/work spaces.\n\nThis is the display model.";
			}

			// Token: 0x02002B4A RID: 11082
			public class PROPHUMANCHESTERFIELDSOFA
			{
				// Token: 0x0400BDE5 RID: 48613
				public static LocString NAME = "Showroom Couch";

				// Token: 0x0400BDE6 RID: 48614
				public static LocString DESC = "A luxurious couch where potential residents can comfortably nap and dream of home.";
			}

			// Token: 0x02002B4B RID: 11083
			public class PROPHUMANCHESTERFIELDCHAIR
			{
				// Token: 0x0400BDE7 RID: 48615
				public static LocString NAME = "Showroom Chair";

				// Token: 0x0400BDE8 RID: 48616
				public static LocString DESC = "A luxurious chair where future generations can comfortably sit and dream of home.";
			}

			// Token: 0x02002B4C RID: 11084
			public class WARPCONDUITRECEIVER
			{
				// Token: 0x0400BDE9 RID: 48617
				public static LocString NAME = "Supply Teleporter Output";

				// Token: 0x0400BDEA RID: 48618
				public static LocString DESC = "The tubes at the back disappear into nowhere.";

				// Token: 0x0400BDEB RID: 48619
				public static LocString EFFECT = string.Concat(new string[]
				{
					"A machine capable of teleporting ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					", ",
					UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
					", and ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" resources to another asteroid.\n\nIt can be activated by a Duplicant with the ",
					UI.FormatAsLink("Field Research", "RESEARCHING2"),
					" skill.\n\nThis is the receiving side."
				});
			}

			// Token: 0x02002B4D RID: 11085
			public class WARPCONDUITSENDER
			{
				// Token: 0x0400BDEC RID: 48620
				public static LocString NAME = "Supply Teleporter Input";

				// Token: 0x0400BDED RID: 48621
				public static LocString DESC = "The tubes at the back disappear into nowhere.";

				// Token: 0x0400BDEE RID: 48622
				public static LocString EFFECT = string.Concat(new string[]
				{
					"A machine capable of teleporting ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					", ",
					UI.FormatAsLink("Solid", "ELEMENTS_SOLID"),
					", and ",
					UI.FormatAsLink("Gas", "ELEMENTS_GAS"),
					" resources to another asteroid.\n\nIt can be activated by a Duplicant with the ",
					UI.FormatAsLink("Field Research", "RESEARCHING2"),
					" skill.\n\nThis is the transmitting side."
				});
			}

			// Token: 0x02002B4E RID: 11086
			public class WARPPORTAL
			{
				// Token: 0x0400BDEF RID: 48623
				public static LocString NAME = "Teleporter Transmitter";

				// Token: 0x0400BDF0 RID: 48624
				public static LocString DESC = "The functional remnants of an intricate teleportation system.\n\nThis is the outgoing side, and has one pre-programmed destination.";
			}

			// Token: 0x02002B4F RID: 11087
			public class WARPRECEIVER
			{
				// Token: 0x0400BDF1 RID: 48625
				public static LocString NAME = "Teleporter Receiver";

				// Token: 0x0400BDF2 RID: 48626
				public static LocString DESC = "The functional remnants of an intricate teleportation system.\n\nThis is the incoming side.";
			}

			// Token: 0x02002B50 RID: 11088
			public class TEMPORALTEAROPENER
			{
				// Token: 0x0400BDF3 RID: 48627
				public static LocString NAME = "Temporal Tear Opener";

				// Token: 0x0400BDF4 RID: 48628
				public static LocString DESC = "Infinite possibilities, with a complimentary side of meteor showers.";

				// Token: 0x0400BDF5 RID: 48629
				public static LocString EFFECT = "A powerful mechanism capable of tearing through the fabric of reality.";

				// Token: 0x020038BF RID: 14527
				public class SIDESCREEN
				{
					// Token: 0x0400E4AF RID: 58543
					public static LocString TEXT = "Fire!";

					// Token: 0x0400E4B0 RID: 58544
					public static LocString TOOLTIP = "The big red button.";
				}
			}

			// Token: 0x02002B51 RID: 11089
			public class LONELYMINIONHOUSE
			{
				// Token: 0x0400BDF6 RID: 48630
				public static LocString NAME = UI.FormatAsLink("Gravitas Shipping Container", "LONELYMINIONHOUSE");

				// Token: 0x0400BDF7 RID: 48631
				public static LocString DESC = "Its occupant has been alone for so long, he's forgotten what friendship feels like.";

				// Token: 0x0400BDF8 RID: 48632
				public static LocString EFFECT = "A large transport unit from the facility's sub-sub-basement.\n\nIt has been modified into a crude yet functional temporary shelter.";
			}

			// Token: 0x02002B52 RID: 11090
			public class LONELYMINIONHOUSE_COMPLETE
			{
				// Token: 0x0400BDF9 RID: 48633
				public static LocString NAME = UI.FormatAsLink("Gravitas Shipping Container", "LONELYMINIONHOUSE_COMPLETE");

				// Token: 0x0400BDFA RID: 48634
				public static LocString DESC = "Someone lived inside it for a while.";

				// Token: 0x0400BDFB RID: 48635
				public static LocString EFFECT = "A super-spacious container for the " + UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID") + " of your choosing.";
			}

			// Token: 0x02002B53 RID: 11091
			public class LONELYMAILBOX
			{
				// Token: 0x0400BDFC RID: 48636
				public static LocString NAME = "Mailbox";

				// Token: 0x0400BDFD RID: 48637
				public static LocString DESC = "There's nothing quite like receiving homemade gifts in the mail.";

				// Token: 0x0400BDFE RID: 48638
				public static LocString EFFECT = "Displays a single edible object.";
			}

			// Token: 0x02002B54 RID: 11092
			public class PLASTICFLOWERS
			{
				// Token: 0x0400BDFF RID: 48639
				public static LocString NAME = "Plastic Flowers";

				// Token: 0x0400BE00 RID: 48640
				public static LocString DESCRIPTION = "Maintenance-free blooms that will outlive us all.";

				// Token: 0x0400BE01 RID: 48641
				public static LocString LORE_DLC2 = "Manufactured by Home Staging Heroes Ltd. as commissioned by the Gravitas Facility, to <i>\"Make Space Feel More Like Home.\"</i>\n\nThis bouquet is designed to smell like freshly baked cookies.";
			}

			// Token: 0x02002B55 RID: 11093
			public class FOUNTAINPEN
			{
				// Token: 0x0400BE02 RID: 48642
				public static LocString NAME = "Fountain Pen";

				// Token: 0x0400BE03 RID: 48643
				public static LocString DESCRIPTION = "Cuts through red tape better than a sword ever could.";

				// Token: 0x0400BE04 RID: 48644
				public static LocString LORE_DLC2 = "The handcrafted gold nib features a triangular logo with the letters V and I inside.\n\nIts owner was too proud to report it stolen, and would be shocked to learn of its whereabouts.";
			}

			// Token: 0x02002B56 RID: 11094
			public class PROPCLOTHESHANGER
			{
				// Token: 0x0400BE05 RID: 48645
				public static LocString NAME = "Coat Rack";

				// Token: 0x0400BE06 RID: 48646
				public static LocString DESC = "Holds one " + EQUIPMENT.PREFABS.WARM_VEST.NAME + ".\n\nIt'd be silly not to use it.";
			}

			// Token: 0x02002B57 RID: 11095
			public class PROPCERESPOSTERA
			{
				// Token: 0x0400BE07 RID: 48647
				public static LocString NAME = "Travel Poster";

				// Token: 0x0400BE08 RID: 48648
				public static LocString DESC = "A poster promoting a local tourist attraction.\n\nActual scenery may vary.";
			}

			// Token: 0x02002B58 RID: 11096
			public class PROPCERESPOSTERB
			{
				// Token: 0x0400BE09 RID: 48649
				public static LocString NAME = "Travel Poster";

				// Token: 0x0400BE0A RID: 48650
				public static LocString DESC = "A poster promoting local wildlife.\n\nThe first in an unfinished series.";
			}

			// Token: 0x02002B59 RID: 11097
			public class PROPCERESPOSTERLARGE
			{
				// Token: 0x0400BE0B RID: 48651
				public static LocString NAME = "Acoustic Art Panel";

				// Token: 0x0400BE0C RID: 48652
				public static LocString DESC = "A sound-absorbing panel that makes small-space living more bearable.\n\nThe artwork features a  power source.";
			}

			// Token: 0x02002B5A RID: 11098
			public class CHLORINATOR
			{
				// Token: 0x0400BE0D RID: 48653
				public static LocString NAME = UI.FormatAsLink("Bleach Stone Hopper", "CHLORINATOR");

				// Token: 0x0400BE0E RID: 48654
				public static LocString DESC = "Bleach stone is useful for sanitation and geotuning.";

				// Token: 0x0400BE0F RID: 48655
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					ELEMENTS.SALT.NAME,
					" and ",
					ELEMENTS.GOLD.NAME,
					" to produce ",
					ELEMENTS.BLEACHSTONE.NAME,
					"."
				});
			}

			// Token: 0x02002B5B RID: 11099
			public class MILKPRESS
			{
				// Token: 0x0400BE10 RID: 48656
				public static LocString NAME = UI.FormatAsLink("Plant Pulverizer", "MILKPRESS");

				// Token: 0x0400BE11 RID: 48657
				public static LocString DESC = "For Duplicants who are too squeamish to milk critters.";

				// Token: 0x0400BE12 RID: 48658
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Crushes organic materials to extract liquids such as ",
					ELEMENTS.MILK.NAME,
					" or ",
					ELEMENTS.PHYTOOIL.NAME,
					".\n\n",
					ELEMENTS.MILK.NAME,
					" can be used to refill the ",
					BUILDINGS.PREFABS.MILKFEEDER.NAME,
					"."
				});

				// Token: 0x0400BE13 RID: 48659
				public static LocString WHEAT_MILK_RECIPE_DESCRIPTION = "Converts {0} to {1}";

				// Token: 0x0400BE14 RID: 48660
				public static LocString VINEFRUIT_JAM_RECIPE_DESCRIPTION = "Converts {0} to {1}";

				// Token: 0x0400BE15 RID: 48661
				public static LocString NUT_MILK_RECIPE_DESCRIPTION = "Converts {0} to {1}";

				// Token: 0x0400BE16 RID: 48662
				public static LocString PHYTO_OIL_RECIPE_DESCRIPTION = "Converts {0} to {1} and {2}";

				// Token: 0x0400BE17 RID: 48663
				public static LocString KELP_TO_PHYTO_OIL_RECIPE_DESCRIPTION = "Converts {0} to {1}";

				// Token: 0x0400BE18 RID: 48664
				public static LocString DEWDRIPPER_MILK_RECIPE_DESCRIPTION = "Converts {0} to {1}";

				// Token: 0x0400BE19 RID: 48665
				public static LocString RESIN_FROM_AMBER_RECIPE_DESCRIPTION = "Converts {0} into {1}, {2}, and a small amount of {3}";
			}

			// Token: 0x02002B5C RID: 11100
			public class FOODDEHYDRATOR
			{
				// Token: 0x0400BE1A RID: 48666
				public static LocString NAME = UI.FormatAsLink("Dehydrator", "FOODDEHYDRATOR");

				// Token: 0x0400BE1B RID: 48667
				public static LocString DESC = "Some of the eliminated liquid inevitably ends up on the floor.";

				// Token: 0x0400BE1C RID: 48668
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses low, even heat to eliminate moisture from eligible ",
					UI.FormatAsLink("Foods", "FOOD"),
					" and render them shelf-stable.\n\nDehydrated meals must be processed at the ",
					UI.FormatAsLink("Rehydrator", "FOODREHYDRATOR"),
					" before they can be eaten."
				});

				// Token: 0x0400BE1D RID: 48669
				public static LocString RECIPE_NAME = "Dried {0}";

				// Token: 0x0400BE1E RID: 48670
				public static LocString RESULT_DESCRIPTION = "Dehydrated portions of {0} do not require refrigeration.";
			}

			// Token: 0x02002B5D RID: 11101
			public class FOODREHYDRATOR
			{
				// Token: 0x0400BE1F RID: 48671
				public static LocString NAME = UI.FormatAsLink("Rehydrator", "FOODREHYDRATOR");

				// Token: 0x0400BE20 RID: 48672
				public static LocString DESC = "Rehydrated food is nutritious and only slightly less delicious.";

				// Token: 0x0400BE21 RID: 48673
				public static LocString EFFECT = "Restores moisture to convert shelf-stable packaged meals into edible " + UI.FormatAsLink("Food", "FOOD") + ".";
			}

			// Token: 0x02002B5E RID: 11102
			public class GEOTHERMALCONTROLLER
			{
				// Token: 0x0400BE22 RID: 48674
				public static LocString NAME = UI.FormatAsLink("Geothermal Heat Pump", "GEOTHERMALCONTROLLER");

				// Token: 0x0400BE23 RID: 48675
				public static LocString DESC = "What comes out depends very much on the initial temperature of what goes in.";

				// Token: 0x0400BE24 RID: 48676
				public static LocString EFFECT = string.Concat(new string[]
				{
					"Uses ",
					UI.FormatAsLink("Heat", "HEAT"),
					" from the planet's core to dramatically increase the temperature of ",
					UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID"),
					" inputs.\n\nMaterials will be emitted at connected Geo Vents."
				});
			}

			// Token: 0x02002B5F RID: 11103
			public class GEOTHERMALVENT
			{
				// Token: 0x0400BE25 RID: 48677
				public static LocString NAME = UI.FormatAsLink("Geo Vent", "GEOTHERMALVENT");

				// Token: 0x0400BE26 RID: 48678
				public static LocString NAME_FMT = UI.FormatAsLink("Geo Vent C-{ID}", "GEOTHERMALVENT");

				// Token: 0x0400BE27 RID: 48679
				public static LocString DESC = "Geo vents must finish their current emission before accepting new materials.";

				// Token: 0x0400BE28 RID: 48680
				public static LocString EFFECT = "Emits high-" + UI.FormatAsLink("temperature", "HEAT") + " materials received from the Geothermal Heat Pump.";

				// Token: 0x0400BE29 RID: 48681
				public static LocString BLOCKED_DESC = string.Concat(new string[]
				{
					"Blocked geo vents can be cleared by pumping in ",
					UI.FormatAsLink("liquids", "ELEMENTS_LIQUID"),
					" that are hot enough to melt ",
					UI.FormatAsLink("Lead", "LEAD"),
					"."
				});

				// Token: 0x0400BE2A RID: 48682
				public static LocString LOGIC_PORT = "Material Content Monitor";

				// Token: 0x0400BE2B RID: 48683
				public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when geo vent has materials to emit";

				// Token: 0x0400BE2C RID: 48684
				public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}
		}

		// Token: 0x02002195 RID: 8597
		public static class DAMAGESOURCES
		{
			// Token: 0x04009A4D RID: 39501
			public static LocString NOTIFICATION_TOOLTIP = "A {0} sustained damage from {1}";

			// Token: 0x04009A4E RID: 39502
			public static LocString CONDUIT_CONTENTS_FROZE = "pipe contents becoming too cold";

			// Token: 0x04009A4F RID: 39503
			public static LocString CONDUIT_CONTENTS_BOILED = "pipe contents becoming too hot";

			// Token: 0x04009A50 RID: 39504
			public static LocString BUILDING_OVERHEATED = "overheating";

			// Token: 0x04009A51 RID: 39505
			public static LocString CORROSIVE_ELEMENT = "corrosive element";

			// Token: 0x04009A52 RID: 39506
			public static LocString BAD_INPUT_ELEMENT = "receiving an incorrect substance";

			// Token: 0x04009A53 RID: 39507
			public static LocString MINION_DESTRUCTION = "an angry Duplicant. Rude!";

			// Token: 0x04009A54 RID: 39508
			public static LocString LIQUID_PRESSURE = "neighboring liquid pressure";

			// Token: 0x04009A55 RID: 39509
			public static LocString CIRCUIT_OVERLOADED = "an overloaded circuit";

			// Token: 0x04009A56 RID: 39510
			public static LocString LOGIC_CIRCUIT_OVERLOADED = "an overloaded logic circuit";

			// Token: 0x04009A57 RID: 39511
			public static LocString MICROMETEORITE = "micrometeorite";

			// Token: 0x04009A58 RID: 39512
			public static LocString COMET = "falling space rocks";

			// Token: 0x04009A59 RID: 39513
			public static LocString ROCKET = "rocket engine";
		}

		// Token: 0x02002196 RID: 8598
		public static class AUTODISINFECTABLE
		{
			// Token: 0x02002B60 RID: 11104
			public static class ENABLE_AUTODISINFECT
			{
				// Token: 0x0400BE2D RID: 48685
				public static LocString NAME = "Enable Disinfect";

				// Token: 0x0400BE2E RID: 48686
				public static LocString TOOLTIP = "Automatically disinfect this building when it becomes contaminated";
			}

			// Token: 0x02002B61 RID: 11105
			public static class DISABLE_AUTODISINFECT
			{
				// Token: 0x0400BE2F RID: 48687
				public static LocString NAME = "Disable Disinfect";

				// Token: 0x0400BE30 RID: 48688
				public static LocString TOOLTIP = "Do not automatically disinfect this building";
			}

			// Token: 0x02002B62 RID: 11106
			public static class NO_DISEASE
			{
				// Token: 0x0400BE31 RID: 48689
				public static LocString TOOLTIP = "This building is clean";
			}
		}

		// Token: 0x02002197 RID: 8599
		public static class DISINFECTABLE
		{
			// Token: 0x02002B63 RID: 11107
			public static class ENABLE_DISINFECT
			{
				// Token: 0x0400BE32 RID: 48690
				public static LocString NAME = "Disinfect";

				// Token: 0x0400BE33 RID: 48691
				public static LocString TOOLTIP = "Mark this building for disinfection";
			}

			// Token: 0x02002B64 RID: 11108
			public static class DISABLE_DISINFECT
			{
				// Token: 0x0400BE34 RID: 48692
				public static LocString NAME = "Cancel Disinfect";

				// Token: 0x0400BE35 RID: 48693
				public static LocString TOOLTIP = "Cancel this disinfect order";
			}

			// Token: 0x02002B65 RID: 11109
			public static class NO_DISEASE
			{
				// Token: 0x0400BE36 RID: 48694
				public static LocString TOOLTIP = "This building is already clean";
			}
		}

		// Token: 0x02002198 RID: 8600
		public static class REPAIRABLE
		{
			// Token: 0x02002B66 RID: 11110
			public static class ENABLE_AUTOREPAIR
			{
				// Token: 0x0400BE37 RID: 48695
				public static LocString NAME = "Enable Autorepair";

				// Token: 0x0400BE38 RID: 48696
				public static LocString TOOLTIP = "Automatically repair this building when damaged";
			}

			// Token: 0x02002B67 RID: 11111
			public static class DISABLE_AUTOREPAIR
			{
				// Token: 0x0400BE39 RID: 48697
				public static LocString NAME = "Disable Autorepair";

				// Token: 0x0400BE3A RID: 48698
				public static LocString TOOLTIP = "Only repair this building when ordered";
			}
		}

		// Token: 0x02002199 RID: 8601
		public static class AUTOMATABLE
		{
			// Token: 0x02002B68 RID: 11112
			public static class ENABLE_AUTOMATIONONLY
			{
				// Token: 0x0400BE3B RID: 48699
				public static LocString NAME = "Disable Manual";

				// Token: 0x0400BE3C RID: 48700
				public static LocString TOOLTIP = "This building's storage may be accessed by Auto-Sweepers only\n\nDuplicants will not be permitted to add or remove materials from this building";
			}

			// Token: 0x02002B69 RID: 11113
			public static class DISABLE_AUTOMATIONONLY
			{
				// Token: 0x0400BE3D RID: 48701
				public static LocString NAME = "Enable Manual";

				// Token: 0x0400BE3E RID: 48702
				public static LocString TOOLTIP = "This building's storage may be accessed by both Duplicants and Auto-Sweeper buildings";
			}
		}
	}
}
