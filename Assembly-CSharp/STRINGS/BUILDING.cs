using System;

namespace STRINGS
{
	// Token: 0x02000FA7 RID: 4007
	public class BUILDING
	{
		// Token: 0x020023FF RID: 9215
		public class STATUSITEMS
		{
			// Token: 0x02002FA3 RID: 12195
			public class GUNKEMPTIERFULL
			{
				// Token: 0x0400CDFE RID: 52734
				public static LocString NAME = "Storage Full";

				// Token: 0x0400CDFF RID: 52735
				public static LocString TOOLTIP = "This building's internal storage is at maximum capacity\n\nIt must be emptied before its next use";
			}

			// Token: 0x02002FA4 RID: 12196
			public class MERCURYLIGHT_CHARGING
			{
				// Token: 0x0400CE00 RID: 52736
				public static LocString NAME = "Powering Up: {0}";

				// Token: 0x0400CE01 RID: 52737
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building's ",
					UI.PRE_KEYWORD,
					"Light",
					UI.PST_KEYWORD,
					" levels are gradually increasing\n\nIf its ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" and ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" requirements continue to be met, it will reach maximum brightness in {0}"
				});
			}

			// Token: 0x02002FA5 RID: 12197
			public class MERCURYLIGHT_DEPLEATING
			{
				// Token: 0x0400CE02 RID: 52738
				public static LocString NAME = "Brightness: {0}";

				// Token: 0x0400CE03 RID: 52739
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building's ",
					UI.PRE_KEYWORD,
					"Light",
					UI.PST_KEYWORD,
					" output is decreasing because its ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" and ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" requirements are not being met\n\nIt will power off once its stores are depleted"
				});
			}

			// Token: 0x02002FA6 RID: 12198
			public class MERCURYLIGHT_DEPLEATED
			{
				// Token: 0x0400CE04 RID: 52740
				public static LocString NAME = "Powered Off";

				// Token: 0x0400CE05 RID: 52741
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building is non-operational due to a lack of resources\n\nIt will begin to power up when its ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" and ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" requirements are met"
				});
			}

			// Token: 0x02002FA7 RID: 12199
			public class MERCURYLIGHT_CHARGED
			{
				// Token: 0x0400CE06 RID: 52742
				public static LocString NAME = "Fully Charged";

				// Token: 0x0400CE07 RID: 52743
				public static LocString TOOLTIP = "This building is functioning at maximum capacity";
			}

			// Token: 0x02002FA8 RID: 12200
			public class SPECIALCARGOBAYCLUSTERCRITTERSTORED
			{
				// Token: 0x0400CE08 RID: 52744
				public static LocString NAME = "Contents: {0}";

				// Token: 0x0400CE09 RID: 52745
				public static LocString TOOLTIP = "";
			}

			// Token: 0x02002FA9 RID: 12201
			public class GEOTUNER_NEEDGEYSER
			{
				// Token: 0x0400CE0A RID: 52746
				public static LocString NAME = "No Geyser Selected";

				// Token: 0x0400CE0B RID: 52747
				public static LocString TOOLTIP = "Select an analyzed geyser to increase its output";
			}

			// Token: 0x02002FAA RID: 12202
			public class GEOTUNER_CHARGE_REQUIRED
			{
				// Token: 0x0400CE0C RID: 52748
				public static LocString NAME = "Experimentation Needed";

				// Token: 0x0400CE0D RID: 52749
				public static LocString TOOLTIP = "This building requires a Duplicant to produce amplification data through experimentation";
			}

			// Token: 0x02002FAB RID: 12203
			public class GEOTUNER_CHARGING
			{
				// Token: 0x0400CE0E RID: 52750
				public static LocString NAME = "Compiling Data";

				// Token: 0x0400CE0F RID: 52751
				public static LocString TOOLTIP = "Compiling amplification data through experimentation";
			}

			// Token: 0x02002FAC RID: 12204
			public class GEOTUNER_CHARGED
			{
				// Token: 0x0400CE10 RID: 52752
				public static LocString NAME = "Data Remaining: {0}";

				// Token: 0x0400CE11 RID: 52753
				public static LocString TOOLTIP = "This building consumes amplification data while boosting a geyser\n\nTime remaining: {0} ({1} data per second)";
			}

			// Token: 0x02002FAD RID: 12205
			public class GEOTUNER_GEYSER_STATUS
			{
				// Token: 0x0400CE12 RID: 52754
				public static LocString NAME = "";

				// Token: 0x0400CE13 RID: 52755
				public static LocString NAME_ERUPTING = "Target is Erupting";

				// Token: 0x0400CE14 RID: 52756
				public static LocString NAME_DORMANT = "Target is Not Erupting";

				// Token: 0x0400CE15 RID: 52757
				public static LocString NAME_IDLE = "Target is Not Erupting";

				// Token: 0x0400CE16 RID: 52758
				public static LocString TOOLTIP = "";

				// Token: 0x0400CE17 RID: 52759
				public static LocString TOOLTIP_ERUPTING = "The selected geyser is erupting and will receive stored amplification data";

				// Token: 0x0400CE18 RID: 52760
				public static LocString TOOLTIP_DORMANT = "The selected geyser is not erupting\n\nIt will not receive stored amplification data in this state";

				// Token: 0x0400CE19 RID: 52761
				public static LocString TOOLTIP_IDLE = "The selected geyser is not erupting\n\nIt will not receive stored amplification data in this state";
			}

			// Token: 0x02002FAE RID: 12206
			public class GEYSER_GEOTUNED
			{
				// Token: 0x0400CE1A RID: 52762
				public static LocString NAME = "Geotuned ({0}/{1})";

				// Token: 0x0400CE1B RID: 52763
				public static LocString TOOLTIP = "This geyser is being boosted by {0} out {1} of " + UI.PRE_KEYWORD + "Geotuners" + UI.PST_KEYWORD;
			}

			// Token: 0x02002FAF RID: 12207
			public class RADIATOR_ENERGY_CURRENT_EMISSION_RATE
			{
				// Token: 0x0400CE1C RID: 52764
				public static LocString NAME = "Currently Emitting: {ENERGY_RATE}";

				// Token: 0x0400CE1D RID: 52765
				public static LocString TOOLTIP = "Currently Emitting: {ENERGY_RATE}";
			}

			// Token: 0x02002FB0 RID: 12208
			public class NOTLINKEDTOHEAD
			{
				// Token: 0x0400CE1E RID: 52766
				public static LocString NAME = "Not Linked";

				// Token: 0x0400CE1F RID: 52767
				public static LocString TOOLTIP = "This building must be built adjacent to a {headBuilding} or another {linkBuilding} in order to function";
			}

			// Token: 0x02002FB1 RID: 12209
			public class BAITED
			{
				// Token: 0x0400CE20 RID: 52768
				public static LocString NAME = "{0} Bait";

				// Token: 0x0400CE21 RID: 52769
				public static LocString TOOLTIP = "This lure is baited with {0}\n\nBait material is set during the construction of the building";
			}

			// Token: 0x02002FB2 RID: 12210
			public class NOCOOLANT
			{
				// Token: 0x0400CE22 RID: 52770
				public static LocString NAME = "No Coolant";

				// Token: 0x0400CE23 RID: 52771
				public static LocString TOOLTIP = "This building needs coolant";
			}

			// Token: 0x02002FB3 RID: 12211
			public class ANGERDAMAGE
			{
				// Token: 0x0400CE24 RID: 52772
				public static LocString NAME = "Damage: Duplicant Tantrum";

				// Token: 0x0400CE25 RID: 52773
				public static LocString TOOLTIP = "A stressed Duplicant is damaging this building";

				// Token: 0x0400CE26 RID: 52774
				public static LocString NOTIFICATION = "Building Damage: Duplicant Tantrum";

				// Token: 0x0400CE27 RID: 52775
				public static LocString NOTIFICATION_TOOLTIP = "Stressed Duplicants are damaging these buildings:\n\n{0}";
			}

			// Token: 0x02002FB4 RID: 12212
			public class PIPECONTENTS
			{
				// Token: 0x0400CE28 RID: 52776
				public static LocString EMPTY = "Empty";

				// Token: 0x0400CE29 RID: 52777
				public static LocString CONTENTS = "{0} of {1} at {2}";

				// Token: 0x0400CE2A RID: 52778
				public static LocString CONTENTS_WITH_DISEASE = "\n  {0}";
			}

			// Token: 0x02002FB5 RID: 12213
			public class CONVEYOR_CONTENTS
			{
				// Token: 0x0400CE2B RID: 52779
				public static LocString EMPTY = "Empty";

				// Token: 0x0400CE2C RID: 52780
				public static LocString CONTENTS = "{0} of {1} at {2}";

				// Token: 0x0400CE2D RID: 52781
				public static LocString CONTENTS_WITH_DISEASE = "\n  {0}";
			}

			// Token: 0x02002FB6 RID: 12214
			public class ASSIGNEDTO
			{
				// Token: 0x0400CE2E RID: 52782
				public static LocString NAME = "Assigned to: {Assignee}";

				// Token: 0x0400CE2F RID: 52783
				public static LocString TOOLTIP = "Only {Assignee} can use this amenity";
			}

			// Token: 0x02002FB7 RID: 12215
			public class ASSIGNEDPUBLIC
			{
				// Token: 0x0400CE30 RID: 52784
				public static LocString NAME = "Assigned to: Public";

				// Token: 0x0400CE31 RID: 52785
				public static LocString TOOLTIP = "Any Duplicant can use this amenity";
			}

			// Token: 0x02002FB8 RID: 12216
			public class ASSIGNEDTOROOM
			{
				// Token: 0x0400CE32 RID: 52786
				public static LocString NAME = "Assigned to: {0}";

				// Token: 0x0400CE33 RID: 52787
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Any Duplicant assigned to this ",
					UI.PRE_KEYWORD,
					"Room",
					UI.PST_KEYWORD,
					" can use this amenity"
				});
			}

			// Token: 0x02002FB9 RID: 12217
			public class AWAITINGSEEDDELIVERY
			{
				// Token: 0x0400CE34 RID: 52788
				public static LocString NAME = "Awaiting Delivery";

				// Token: 0x0400CE35 RID: 52789
				public static LocString TOOLTIP = "Awaiting delivery of selected " + UI.PRE_KEYWORD + "Seed" + UI.PST_KEYWORD;
			}

			// Token: 0x02002FBA RID: 12218
			public class AWAITINGBAITDELIVERY
			{
				// Token: 0x0400CE36 RID: 52790
				public static LocString NAME = "Awaiting Bait";

				// Token: 0x0400CE37 RID: 52791
				public static LocString TOOLTIP = "Awaiting delivery of selected " + UI.PRE_KEYWORD + "Bait" + UI.PST_KEYWORD;
			}

			// Token: 0x02002FBB RID: 12219
			public class CLINICOUTSIDEHOSPITAL
			{
				// Token: 0x0400CE38 RID: 52792
				public static LocString NAME = "Medical building outside Hospital";

				// Token: 0x0400CE39 RID: 52793
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Rebuild this medical equipment in a ",
					UI.PRE_KEYWORD,
					"Hospital",
					UI.PST_KEYWORD,
					" to more effectively quarantine sick Duplicants"
				});
			}

			// Token: 0x02002FBC RID: 12220
			public class BOTTLE_EMPTIER
			{
				// Token: 0x02003AF1 RID: 15089
				public static class ALLOWED
				{
					// Token: 0x0400E9D8 RID: 59864
					public static LocString NAME = "Auto-Bottle: On";

					// Token: 0x0400E9D9 RID: 59865
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"Duplicants may specifically fetch ",
						UI.PRE_KEYWORD,
						"Liquid",
						UI.PST_KEYWORD,
						" from a bottling station to bring to this location"
					});
				}

				// Token: 0x02003AF2 RID: 15090
				public static class DENIED
				{
					// Token: 0x0400E9DA RID: 59866
					public static LocString NAME = "Auto-Bottle: Off";

					// Token: 0x0400E9DB RID: 59867
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"Duplicants may not specifically fetch ",
						UI.PRE_KEYWORD,
						"Liquid",
						UI.PST_KEYWORD,
						" from a bottling station to bring to this location"
					});
				}
			}

			// Token: 0x02002FBD RID: 12221
			public class CANISTER_EMPTIER
			{
				// Token: 0x02003AF3 RID: 15091
				public static class ALLOWED
				{
					// Token: 0x0400E9DC RID: 59868
					public static LocString NAME = "Auto-Bottle: On";

					// Token: 0x0400E9DD RID: 59869
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"Duplicants may specifically fetch ",
						UI.PRE_KEYWORD,
						"Gas",
						UI.PST_KEYWORD,
						" from a canister filling station to bring to this location"
					});
				}

				// Token: 0x02003AF4 RID: 15092
				public static class DENIED
				{
					// Token: 0x0400E9DE RID: 59870
					public static LocString NAME = "Auto-Bottle: Off";

					// Token: 0x0400E9DF RID: 59871
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"Duplicants may not specifically fetch ",
						UI.PRE_KEYWORD,
						"Gas",
						UI.PST_KEYWORD,
						" from a canister filling station to bring to this location"
					});
				}
			}

			// Token: 0x02002FBE RID: 12222
			public class BROKEN
			{
				// Token: 0x0400CE3A RID: 52794
				public static LocString NAME = "Broken";

				// Token: 0x0400CE3B RID: 52795
				public static LocString TOOLTIP = "This building received damage from <b>{DamageInfo}</b>\n\nIt will not function until it receives repairs";
			}

			// Token: 0x02002FBF RID: 12223
			public class CHANGESTORAGETILETARGET
			{
				// Token: 0x0400CE3C RID: 52796
				public static LocString NAME = "Set Storage: {TargetName}";

				// Token: 0x0400CE3D RID: 52797
				public static LocString TOOLTIP = "Waiting for a Duplicant to reassign this storage to {TargetName}";

				// Token: 0x0400CE3E RID: 52798
				public static LocString EMPTY = "Empty";
			}

			// Token: 0x02002FC0 RID: 12224
			public class CHANGEDOORCONTROLSTATE
			{
				// Token: 0x0400CE3F RID: 52799
				public static LocString NAME = "Pending Door State Change: {ControlState}";

				// Token: 0x0400CE40 RID: 52800
				public static LocString TOOLTIP = "Waiting for a Duplicant to change control state";
			}

			// Token: 0x02002FC1 RID: 12225
			public class DISPENSEREQUESTED
			{
				// Token: 0x0400CE41 RID: 52801
				public static LocString NAME = "Dispense Requested";

				// Token: 0x0400CE42 RID: 52802
				public static LocString TOOLTIP = "Waiting for a Duplicant to dispense the item";
			}

			// Token: 0x02002FC2 RID: 12226
			public class SUIT_LOCKER
			{
				// Token: 0x02003AF5 RID: 15093
				public class NEED_CONFIGURATION
				{
					// Token: 0x0400E9E0 RID: 59872
					public static LocString NAME = "Current Status: Needs Configuration";

					// Token: 0x0400E9E1 RID: 59873
					public static LocString TOOLTIP = "Set this dock to store a suit or leave it empty";
				}

				// Token: 0x02003AF6 RID: 15094
				public class READY
				{
					// Token: 0x0400E9E2 RID: 59874
					public static LocString NAME = "Current Status: Empty";

					// Token: 0x0400E9E3 RID: 59875
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"This dock is ready to receive a ",
						UI.PRE_KEYWORD,
						"Suit",
						UI.PST_KEYWORD,
						", either by manual delivery or from a Duplicant returning the suit they're wearing"
					});
				}

				// Token: 0x02003AF7 RID: 15095
				public class SUIT_REQUESTED
				{
					// Token: 0x0400E9E4 RID: 59876
					public static LocString NAME = "Current Status: Awaiting Delivery";

					// Token: 0x0400E9E5 RID: 59877
					public static LocString TOOLTIP = "Waiting for a Duplicant to deliver a " + UI.PRE_KEYWORD + "Suit" + UI.PST_KEYWORD;
				}

				// Token: 0x02003AF8 RID: 15096
				public class CHARGING
				{
					// Token: 0x0400E9E6 RID: 59878
					public static LocString NAME = "Current Status: Charging Suit";

					// Token: 0x0400E9E7 RID: 59879
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"This ",
						UI.PRE_KEYWORD,
						"Suit",
						UI.PST_KEYWORD,
						" is docked and refueling"
					});
				}

				// Token: 0x02003AF9 RID: 15097
				public class NO_OXYGEN
				{
					// Token: 0x0400E9E8 RID: 59880
					public static LocString NAME = "Current Status: No Oxygen";

					// Token: 0x0400E9E9 RID: 59881
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"This dock does not contain enough ",
						ELEMENTS.OXYGEN.NAME,
						" to refill a ",
						UI.PRE_KEYWORD,
						"Suit",
						UI.PST_KEYWORD
					});
				}

				// Token: 0x02003AFA RID: 15098
				public class NO_FUEL
				{
					// Token: 0x0400E9EA RID: 59882
					public static LocString NAME = "Current Status: No Fuel";

					// Token: 0x0400E9EB RID: 59883
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"This dock does not contain enough ",
						ELEMENTS.PETROLEUM.NAME,
						" to refill a ",
						UI.PRE_KEYWORD,
						"Suit",
						UI.PST_KEYWORD
					});
				}

				// Token: 0x02003AFB RID: 15099
				public class NO_COOLANT
				{
					// Token: 0x0400E9EC RID: 59884
					public static LocString NAME = "Current Status: No Coolant";

					// Token: 0x0400E9ED RID: 59885
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"This dock does not contain enough ",
						ELEMENTS.WATER.NAME,
						" to refill a ",
						UI.PRE_KEYWORD,
						"Suit",
						UI.PST_KEYWORD
					});
				}

				// Token: 0x02003AFC RID: 15100
				public class NOT_OPERATIONAL
				{
					// Token: 0x0400E9EE RID: 59886
					public static LocString NAME = "Current Status: Offline";

					// Token: 0x0400E9EF RID: 59887
					public static LocString TOOLTIP = "This dock requires " + UI.PRE_KEYWORD + "Power" + UI.PST_KEYWORD;
				}

				// Token: 0x02003AFD RID: 15101
				public class FULLY_CHARGED
				{
					// Token: 0x0400E9F0 RID: 59888
					public static LocString NAME = "Current Status: Full Fueled";

					// Token: 0x0400E9F1 RID: 59889
					public static LocString TOOLTIP = "This suit is fully refueled and ready for use";
				}
			}

			// Token: 0x02002FC3 RID: 12227
			public class SUITMARKERTRAVERSALONLYWHENROOMAVAILABLE
			{
				// Token: 0x0400CE43 RID: 52803
				public static LocString NAME = "Clearance: Vacancy Only";

				// Token: 0x0400CE44 RID: 52804
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Suited Duplicants may pass only if there is room in a ",
					UI.PRE_KEYWORD,
					"Dock",
					UI.PST_KEYWORD,
					" to store their ",
					UI.PRE_KEYWORD,
					"Suit",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002FC4 RID: 12228
			public class SUITMARKERTRAVERSALANYTIME
			{
				// Token: 0x0400CE45 RID: 52805
				public static LocString NAME = "Clearance: Always Permitted";

				// Token: 0x0400CE46 RID: 52806
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Suited Duplicants may pass even if there is no room to store their ",
					UI.PRE_KEYWORD,
					"Suits",
					UI.PST_KEYWORD,
					"\n\nWhen all available docks are full, Duplicants will unequip their ",
					UI.PRE_KEYWORD,
					"Suits",
					UI.PST_KEYWORD,
					" and drop them on the floor"
				});
			}

			// Token: 0x02002FC5 RID: 12229
			public class SUIT_LOCKER_NEEDS_CONFIGURATION
			{
				// Token: 0x0400CE47 RID: 52807
				public static LocString NAME = "Not Configured";

				// Token: 0x0400CE48 RID: 52808
				public static LocString TOOLTIP = "Dock settings not configured";
			}

			// Token: 0x02002FC6 RID: 12230
			public class CURRENTDOORCONTROLSTATE
			{
				// Token: 0x0400CE49 RID: 52809
				public static LocString NAME = "Current State: {ControlState}";

				// Token: 0x0400CE4A RID: 52810
				public static LocString TOOLTIP = "Current State: {ControlState}\n\nAuto: Duplicants open and close this door as needed\nLocked: Nothing may pass through\nOpen: This door will remain open";

				// Token: 0x0400CE4B RID: 52811
				public static LocString OPENED = "Opened";

				// Token: 0x0400CE4C RID: 52812
				public static LocString AUTO = "Auto";

				// Token: 0x0400CE4D RID: 52813
				public static LocString LOCKED = "Locked";
			}

			// Token: 0x02002FC7 RID: 12231
			public class CONDUITBLOCKED
			{
				// Token: 0x0400CE4E RID: 52814
				public static LocString NAME = "Pipe Blocked";

				// Token: 0x0400CE4F RID: 52815
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Output ",
					UI.PRE_KEYWORD,
					"Pipe",
					UI.PST_KEYWORD,
					" is blocked"
				});
			}

			// Token: 0x02002FC8 RID: 12232
			public class OUTPUTTILEBLOCKED
			{
				// Token: 0x0400CE50 RID: 52816
				public static LocString NAME = "Output Blocked";

				// Token: 0x0400CE51 RID: 52817
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Output ",
					UI.PRE_KEYWORD,
					"Pipe",
					UI.PST_KEYWORD,
					" is blocked"
				});
			}

			// Token: 0x02002FC9 RID: 12233
			public class CONDUITBLOCKEDMULTIPLES
			{
				// Token: 0x0400CE52 RID: 52818
				public static LocString NAME = "Pipe Blocked";

				// Token: 0x0400CE53 RID: 52819
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Output ",
					UI.PRE_KEYWORD,
					"Pipe",
					UI.PST_KEYWORD,
					" is blocked"
				});
			}

			// Token: 0x02002FCA RID: 12234
			public class SOLIDCONDUITBLOCKEDMULTIPLES
			{
				// Token: 0x0400CE54 RID: 52820
				public static LocString NAME = "Conveyor Rail Blocked";

				// Token: 0x0400CE55 RID: 52821
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Output ",
					UI.PRE_KEYWORD,
					"Conveyor Rail",
					UI.PST_KEYWORD,
					" is blocked"
				});
			}

			// Token: 0x02002FCB RID: 12235
			public class OUTPUTPIPEFULL
			{
				// Token: 0x0400CE56 RID: 52822
				public static LocString NAME = "Output Pipe Full";

				// Token: 0x0400CE57 RID: 52823
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Unable to flush contents, output ",
					UI.PRE_KEYWORD,
					"Pipe",
					UI.PST_KEYWORD,
					" is blocked"
				});
			}

			// Token: 0x02002FCC RID: 12236
			public class CONSTRUCTIONUNREACHABLE
			{
				// Token: 0x0400CE58 RID: 52824
				public static LocString NAME = "Unreachable Build";

				// Token: 0x0400CE59 RID: 52825
				public static LocString TOOLTIP = "Duplicants cannot reach this construction site";
			}

			// Token: 0x02002FCD RID: 12237
			public class MOPUNREACHABLE
			{
				// Token: 0x0400CE5A RID: 52826
				public static LocString NAME = "Unreachable Mop";

				// Token: 0x0400CE5B RID: 52827
				public static LocString TOOLTIP = "Duplicants cannot reach this area";
			}

			// Token: 0x02002FCE RID: 12238
			public class DEADREACTORCOOLINGOFF
			{
				// Token: 0x0400CE5C RID: 52828
				public static LocString NAME = "Cooling ({CyclesRemaining} cycles remaining)";

				// Token: 0x0400CE5D RID: 52829
				public static LocString TOOLTIP = "The radiation coming from this reactor is diminishing";
			}

			// Token: 0x02002FCF RID: 12239
			public class DIGUNREACHABLE
			{
				// Token: 0x0400CE5E RID: 52830
				public static LocString NAME = "Unreachable Dig";

				// Token: 0x0400CE5F RID: 52831
				public static LocString TOOLTIP = "Duplicants cannot reach this area";
			}

			// Token: 0x02002FD0 RID: 12240
			public class STORAGEUNREACHABLE
			{
				// Token: 0x0400CE60 RID: 52832
				public static LocString NAME = "Unreachable Storage";

				// Token: 0x0400CE61 RID: 52833
				public static LocString TOOLTIP = "Duplicants cannot reach this storage unit";
			}

			// Token: 0x02002FD1 RID: 12241
			public class PASSENGERMODULEUNREACHABLE
			{
				// Token: 0x0400CE62 RID: 52834
				public static LocString NAME = "Unreachable Module";

				// Token: 0x0400CE63 RID: 52835
				public static LocString TOOLTIP = "Duplicants cannot reach this rocket module";
			}

			// Token: 0x02002FD2 RID: 12242
			public class POWERBANKCHARGERINPROGRESS
			{
				// Token: 0x0400CE64 RID: 52836
				public static LocString NAME = "Recharging Power Bank: {0}";

				// Token: 0x0400CE65 RID: 52837
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building is currently charging a ",
					UI.PRE_KEYWORD,
					"Power Bank",
					UI.PST_KEYWORD,
					" at {0}\n\nThe ",
					UI.PRE_KEYWORD,
					"Power Bank",
					UI.PST_KEYWORD,
					" will be dropped once charging is complete"
				});
			}

			// Token: 0x02002FD3 RID: 12243
			public class CONSTRUCTABLEDIGUNREACHABLE
			{
				// Token: 0x0400CE66 RID: 52838
				public static LocString NAME = "Unreachable Dig";

				// Token: 0x0400CE67 RID: 52839
				public static LocString TOOLTIP = "This construction site contains cells that cannot be dug out";
			}

			// Token: 0x02002FD4 RID: 12244
			public class EMPTYPUMPINGSTATION
			{
				// Token: 0x0400CE68 RID: 52840
				public static LocString NAME = "Empty";

				// Token: 0x0400CE69 RID: 52841
				public static LocString TOOLTIP = "This pumping station cannot access any " + UI.PRE_KEYWORD + "Liquid" + UI.PST_KEYWORD;
			}

			// Token: 0x02002FD5 RID: 12245
			public class ENTOMBED
			{
				// Token: 0x0400CE6A RID: 52842
				public static LocString NAME = "Entombed";

				// Token: 0x0400CE6B RID: 52843
				public static LocString TOOLTIP = "Must be dug out by a Duplicant";

				// Token: 0x0400CE6C RID: 52844
				public static LocString NOTIFICATION_NAME = "Building entombment";

				// Token: 0x0400CE6D RID: 52845
				public static LocString NOTIFICATION_TOOLTIP = "These buildings are entombed and need to be dug out:";
			}

			// Token: 0x02002FD6 RID: 12246
			public class ELECTROBANKJOULESAVAILABLE
			{
				// Token: 0x0400CE6E RID: 52846
				public static LocString NAME = "Power Remaining: {JoulesAvailable} / {JoulesCapacity}";

				// Token: 0x0400CE6F RID: 52847
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"<b>{JoulesAvailable}</b> of stored ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" available for use\n\nMaximum capacity: {JoulesCapacity}"
				});
			}

			// Token: 0x02002FD7 RID: 12247
			public class FABRICATORACCEPTSMUTANTSEEDS
			{
				// Token: 0x0400CE70 RID: 52848
				public static LocString NAME = "Fabricator accepts mutant seeds";

				// Token: 0x0400CE71 RID: 52849
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This fabricator is allowed to use ",
					UI.PRE_KEYWORD,
					"Mutant Seeds",
					UI.PST_KEYWORD,
					" as recipe ingredients"
				});
			}

			// Token: 0x02002FD8 RID: 12248
			public class FISHFEEDERACCEPTSMUTANTSEEDS
			{
				// Token: 0x0400CE72 RID: 52850
				public static LocString NAME = "Fish Feeder accepts mutant seeds";

				// Token: 0x0400CE73 RID: 52851
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This fish feeder is allowed to use ",
					UI.PRE_KEYWORD,
					"Mutant Seeds",
					UI.PST_KEYWORD,
					" as fish food"
				});
			}

			// Token: 0x02002FD9 RID: 12249
			public class INVALIDPORTOVERLAP
			{
				// Token: 0x0400CE74 RID: 52852
				public static LocString NAME = "Invalid Port Overlap";

				// Token: 0x0400CE75 RID: 52853
				public static LocString TOOLTIP = "Ports on this building overlap those on another building\n\nThis building must be rebuilt in a valid location";

				// Token: 0x0400CE76 RID: 52854
				public static LocString NOTIFICATION_NAME = "Building has overlapping ports";

				// Token: 0x0400CE77 RID: 52855
				public static LocString NOTIFICATION_TOOLTIP = "These buildings must be rebuilt with non-overlapping ports:";
			}

			// Token: 0x02002FDA RID: 12250
			public class GENESHUFFLECOMPLETED
			{
				// Token: 0x0400CE78 RID: 52856
				public static LocString NAME = "Vacillation Complete";

				// Token: 0x0400CE79 RID: 52857
				public static LocString TOOLTIP = "The Duplicant has completed the neural vacillation process and is ready to be released";
			}

			// Token: 0x02002FDB RID: 12251
			public class OVERHEATED
			{
				// Token: 0x0400CE7A RID: 52858
				public static LocString NAME = "Damage: Overheating";

				// Token: 0x0400CE7B RID: 52859
				public static LocString TOOLTIP = "This building is taking damage and will break down if not cooled";
			}

			// Token: 0x02002FDC RID: 12252
			public class OVERLOADED
			{
				// Token: 0x0400CE7C RID: 52860
				public static LocString NAME = "Damage: Overloading";

				// Token: 0x0400CE7D RID: 52861
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Wire",
					UI.PST_KEYWORD,
					" is taking damage because there are too many buildings pulling ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" from this circuit\n\nSplit this ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" circuit into multiple circuits, or use higher quality ",
					UI.PRE_KEYWORD,
					"Wires",
					UI.PST_KEYWORD,
					" to prevent overloading"
				});
			}

			// Token: 0x02002FDD RID: 12253
			public class LOGICOVERLOADED
			{
				// Token: 0x0400CE7E RID: 52862
				public static LocString NAME = "Damage: Overloading";

				// Token: 0x0400CE7F RID: 52863
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Logic Wire",
					UI.PST_KEYWORD,
					" is taking damage\n\nLimit the output to one Bit, or replace it with ",
					UI.PRE_KEYWORD,
					"Logic Ribbon",
					UI.PST_KEYWORD,
					" to prevent further damage"
				});
			}

			// Token: 0x02002FDE RID: 12254
			public class OPERATINGENERGY
			{
				// Token: 0x0400CE80 RID: 52864
				public static LocString NAME = "Heat Production: {0}/s";

				// Token: 0x0400CE81 RID: 52865
				public static LocString TOOLTIP = "This building is producing <b>{0}</b> per second\n\nSources:\n{1}";

				// Token: 0x0400CE82 RID: 52866
				public static LocString LINEITEM = "    • {0}: {1}\n";

				// Token: 0x0400CE83 RID: 52867
				public static LocString OPERATING = "Normal operation";

				// Token: 0x0400CE84 RID: 52868
				public static LocString EXHAUSTING = "Excess produced";

				// Token: 0x0400CE85 RID: 52869
				public static LocString PIPECONTENTS_TRANSFER = "Transferred from pipes";

				// Token: 0x0400CE86 RID: 52870
				public static LocString FOOD_TRANSFER = "Internal Cooling";
			}

			// Token: 0x02002FDF RID: 12255
			public class FLOODED
			{
				// Token: 0x0400CE87 RID: 52871
				public static LocString NAME = "Building Flooded";

				// Token: 0x0400CE88 RID: 52872
				public static LocString TOOLTIP = "Building cannot function at current saturation";

				// Token: 0x0400CE89 RID: 52873
				public static LocString NOTIFICATION_NAME = "Flooding";

				// Token: 0x0400CE8A RID: 52874
				public static LocString NOTIFICATION_TOOLTIP = "These buildings are flooded:";
			}

			// Token: 0x02002FE0 RID: 12256
			public class NOTSUBMERGED
			{
				// Token: 0x0400CE8B RID: 52875
				public static LocString NAME = "Building Not Submerged";

				// Token: 0x0400CE8C RID: 52876
				public static LocString TOOLTIP = "Building cannot function unless submerged in liquid";
			}

			// Token: 0x02002FE1 RID: 12257
			public class GASVENTOBSTRUCTED
			{
				// Token: 0x0400CE8D RID: 52877
				public static LocString NAME = "Gas Vent Obstructed";

				// Token: 0x0400CE8E RID: 52878
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A ",
					UI.PRE_KEYWORD,
					"Pipe",
					UI.PST_KEYWORD,
					" has been obstructed and is preventing ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" flow to this vent"
				});
			}

			// Token: 0x02002FE2 RID: 12258
			public class GASVENTOVERPRESSURE
			{
				// Token: 0x0400CE8F RID: 52879
				public static LocString NAME = "Gas Vent Overpressure";

				// Token: 0x0400CE90 RID: 52880
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"High ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" or ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" pressure in this area is preventing further ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" emission\nReduce pressure by pumping ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" away or clearing more space"
				});
			}

			// Token: 0x02002FE3 RID: 12259
			public class DIRECTION_CONTROL
			{
				// Token: 0x0400CE91 RID: 52881
				public static LocString NAME = "Use Direction: {Direction}";

				// Token: 0x0400CE92 RID: 52882
				public static LocString TOOLTIP = "Duplicants will only use this building when walking by it\n\nCurrently allowed direction: <b>{Direction}</b>";

				// Token: 0x02003AFE RID: 15102
				public static class DIRECTIONS
				{
					// Token: 0x0400E9F2 RID: 59890
					public static LocString LEFT = "Left";

					// Token: 0x0400E9F3 RID: 59891
					public static LocString RIGHT = "Right";

					// Token: 0x0400E9F4 RID: 59892
					public static LocString BOTH = "Both";
				}
			}

			// Token: 0x02002FE4 RID: 12260
			public class WATTSONGAMEOVER
			{
				// Token: 0x0400CE93 RID: 52883
				public static LocString NAME = "Colony Lost";

				// Token: 0x0400CE94 RID: 52884
				public static LocString TOOLTIP = "All Duplicants are dead or incapacitated";
			}

			// Token: 0x02002FE5 RID: 12261
			public class INVALIDBUILDINGLOCATION
			{
				// Token: 0x0400CE95 RID: 52885
				public static LocString NAME = "Invalid Building Location";

				// Token: 0x0400CE96 RID: 52886
				public static LocString TOOLTIP = "Cannot construct a building in this location";
			}

			// Token: 0x02002FE6 RID: 12262
			public class LIQUIDVENTOBSTRUCTED
			{
				// Token: 0x0400CE97 RID: 52887
				public static LocString NAME = "Liquid Vent Obstructed";

				// Token: 0x0400CE98 RID: 52888
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A ",
					UI.PRE_KEYWORD,
					"Pipe",
					UI.PST_KEYWORD,
					" has been obstructed and is preventing ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" flow to this vent"
				});
			}

			// Token: 0x02002FE7 RID: 12263
			public class LIQUIDVENTOVERPRESSURE
			{
				// Token: 0x0400CE99 RID: 52889
				public static LocString NAME = "Liquid Vent Overpressure";

				// Token: 0x0400CE9A RID: 52890
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"High ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" or ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" pressure in this area is preventing further ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" emission\nReduce pressure by pumping ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" away or clearing more space"
				});
			}

			// Token: 0x02002FE8 RID: 12264
			public class MANUALLYCONTROLLED
			{
				// Token: 0x0400CE9B RID: 52891
				public static LocString NAME = "Manually Controlled";

				// Token: 0x0400CE9C RID: 52892
				public static LocString TOOLTIP = "This Duplicant is under my control";
			}

			// Token: 0x02002FE9 RID: 12265
			public class LIMITVALVELIMITREACHED
			{
				// Token: 0x0400CE9D RID: 52893
				public static LocString NAME = "Limit Reached";

				// Token: 0x0400CE9E RID: 52894
				public static LocString TOOLTIP = "No more Mass can be transferred";
			}

			// Token: 0x02002FEA RID: 12266
			public class LIMITVALVELIMITNOTREACHED
			{
				// Token: 0x0400CE9F RID: 52895
				public static LocString NAME = "Amount remaining: {0}";

				// Token: 0x0400CEA0 RID: 52896
				public static LocString TOOLTIP = "This building will stop transferring Mass when the amount remaining reaches 0";
			}

			// Token: 0x02002FEB RID: 12267
			public class MATERIALSUNAVAILABLE
			{
				// Token: 0x0400CEA1 RID: 52897
				public static LocString NAME = "Insufficient Resources\n{ItemsRemaining}";

				// Token: 0x0400CEA2 RID: 52898
				public static LocString TOOLTIP = "Crucial materials for this building are beyond reach or unavailable";

				// Token: 0x0400CEA3 RID: 52899
				public static LocString NOTIFICATION_NAME = "Building lacks resources";

				// Token: 0x0400CEA4 RID: 52900
				public static LocString NOTIFICATION_TOOLTIP = "Crucial materials are unavailable or beyond reach for these buildings:";

				// Token: 0x0400CEA5 RID: 52901
				public static LocString LINE_ITEM_MASS = "• {0}: {1}";

				// Token: 0x0400CEA6 RID: 52902
				public static LocString LINE_ITEM_UNITS = "• {0}";
			}

			// Token: 0x02002FEC RID: 12268
			public class MATERIALSUNAVAILABLEFORREFILL
			{
				// Token: 0x0400CEA7 RID: 52903
				public static LocString NAME = "Resources Low\n{ItemsRemaining}";

				// Token: 0x0400CEA8 RID: 52904
				public static LocString TOOLTIP = "This building will soon require materials that are unavailable";

				// Token: 0x0400CEA9 RID: 52905
				public static LocString LINE_ITEM = "• {0}";
			}

			// Token: 0x02002FED RID: 12269
			public class MELTINGDOWN
			{
				// Token: 0x0400CEAA RID: 52906
				public static LocString NAME = "Breaking Down";

				// Token: 0x0400CEAB RID: 52907
				public static LocString TOOLTIP = "This building is collapsing";

				// Token: 0x0400CEAC RID: 52908
				public static LocString NOTIFICATION_NAME = "Building breakdown";

				// Token: 0x0400CEAD RID: 52909
				public static LocString NOTIFICATION_TOOLTIP = "These buildings are collapsing:";
			}

			// Token: 0x02002FEE RID: 12270
			public class MISSINGFOUNDATION
			{
				// Token: 0x0400CEAE RID: 52910
				public static LocString NAME = "Missing Tile";

				// Token: 0x0400CEAF RID: 52911
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Build ",
					UI.PRE_KEYWORD,
					"Tile",
					UI.PST_KEYWORD,
					" beneath this building to regain function\n\nTile can be found in the ",
					UI.FormatAsBuildMenuTab("Base Tab", global::Action.Plan1),
					" of the Build Menu"
				});
			}

			// Token: 0x02002FEF RID: 12271
			public class NEUTRONIUMUNMINABLE
			{
				// Token: 0x0400CEB0 RID: 52912
				public static LocString NAME = "Cannot Mine";

				// Token: 0x0400CEB1 RID: 52913
				public static LocString TOOLTIP = "This resource cannot be mined by Duplicant tools";
			}

			// Token: 0x02002FF0 RID: 12272
			public class NEEDGASIN
			{
				// Token: 0x0400CEB2 RID: 52914
				public static LocString NAME = "No Gas Intake\n{GasRequired}";

				// Token: 0x0400CEB3 RID: 52915
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building's ",
					UI.PRE_KEYWORD,
					"Gas Intake",
					UI.PST_KEYWORD,
					" does not have a ",
					BUILDINGS.PREFABS.GASCONDUIT.NAME,
					" connected"
				});

				// Token: 0x0400CEB4 RID: 52916
				public static LocString LINE_ITEM = "• {0}";
			}

			// Token: 0x02002FF1 RID: 12273
			public class NEEDGASOUT
			{
				// Token: 0x0400CEB5 RID: 52917
				public static LocString NAME = "No Gas Output";

				// Token: 0x0400CEB6 RID: 52918
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building's ",
					UI.PRE_KEYWORD,
					"Gas Output",
					UI.PST_KEYWORD,
					" does not have a ",
					BUILDINGS.PREFABS.GASCONDUIT.NAME,
					" connected"
				});
			}

			// Token: 0x02002FF2 RID: 12274
			public class NEEDLIQUIDIN
			{
				// Token: 0x0400CEB7 RID: 52919
				public static LocString NAME = "No Liquid Intake\n{LiquidRequired}";

				// Token: 0x0400CEB8 RID: 52920
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building's ",
					UI.PRE_KEYWORD,
					"Liquid Intake",
					UI.PST_KEYWORD,
					" does not have a ",
					BUILDINGS.PREFABS.LIQUIDCONDUIT.NAME,
					" connected"
				});

				// Token: 0x0400CEB9 RID: 52921
				public static LocString LINE_ITEM = "• {0}";
			}

			// Token: 0x02002FF3 RID: 12275
			public class NEEDLIQUIDOUT
			{
				// Token: 0x0400CEBA RID: 52922
				public static LocString NAME = "No Liquid Output";

				// Token: 0x0400CEBB RID: 52923
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building's ",
					UI.PRE_KEYWORD,
					"Liquid Output",
					UI.PST_KEYWORD,
					" does not have a ",
					BUILDINGS.PREFABS.LIQUIDCONDUIT.NAME,
					" connected"
				});
			}

			// Token: 0x02002FF4 RID: 12276
			public class LIQUIDPIPEEMPTY
			{
				// Token: 0x0400CEBC RID: 52924
				public static LocString NAME = "Empty Pipe";

				// Token: 0x0400CEBD RID: 52925
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"There is no ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" in this pipe"
				});
			}

			// Token: 0x02002FF5 RID: 12277
			public class LIQUIDPIPEOBSTRUCTED
			{
				// Token: 0x0400CEBE RID: 52926
				public static LocString NAME = "Not Pumping";

				// Token: 0x0400CEBF RID: 52927
				public static LocString TOOLTIP = "This pump is not active";
			}

			// Token: 0x02002FF6 RID: 12278
			public class GASPIPEEMPTY
			{
				// Token: 0x0400CEC0 RID: 52928
				public static LocString NAME = "Empty Pipe";

				// Token: 0x0400CEC1 RID: 52929
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"There is no ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" in this pipe"
				});
			}

			// Token: 0x02002FF7 RID: 12279
			public class GASPIPEOBSTRUCTED
			{
				// Token: 0x0400CEC2 RID: 52930
				public static LocString NAME = "Not Pumping";

				// Token: 0x0400CEC3 RID: 52931
				public static LocString TOOLTIP = "This pump is not active";
			}

			// Token: 0x02002FF8 RID: 12280
			public class NEEDSOLIDIN
			{
				// Token: 0x0400CEC4 RID: 52932
				public static LocString NAME = "No Conveyor Loader";

				// Token: 0x0400CEC5 RID: 52933
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Material cannot be fed onto this Conveyor system for transport\n\nEnter the ",
					UI.FormatAsBuildMenuTab("Shipping Tab", global::Action.Plan13),
					" of the Build Menu to build and connect a ",
					UI.PRE_KEYWORD,
					"Conveyor Loader",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002FF9 RID: 12281
			public class NEEDSOLIDOUT
			{
				// Token: 0x0400CEC6 RID: 52934
				public static LocString NAME = "No Conveyor Receptacle";

				// Token: 0x0400CEC7 RID: 52935
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Material cannot be offloaded from this Conveyor system and will backup the rails\n\nEnter the ",
					UI.FormatAsBuildMenuTab("Shipping Tab", global::Action.Plan13),
					" of the Build Menu to build and connect a ",
					UI.PRE_KEYWORD,
					"Conveyor Receptacle",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02002FFA RID: 12282
			public class SOLIDPIPEOBSTRUCTED
			{
				// Token: 0x0400CEC8 RID: 52936
				public static LocString NAME = "Conveyor Rail Backup";

				// Token: 0x0400CEC9 RID: 52937
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Conveyor Rail",
					UI.PST_KEYWORD,
					" cannot carry anymore material\n\nRemove material from the ",
					UI.PRE_KEYWORD,
					"Conveyor Receptacle",
					UI.PST_KEYWORD,
					" to free space for more objects"
				});
			}

			// Token: 0x02002FFB RID: 12283
			public class NEEDPLANT
			{
				// Token: 0x0400CECA RID: 52938
				public static LocString NAME = "No Seeds";

				// Token: 0x0400CECB RID: 52939
				public static LocString TOOLTIP = "Uproot wild plants to obtain seeds";
			}

			// Token: 0x02002FFC RID: 12284
			public class NEEDSEED
			{
				// Token: 0x0400CECC RID: 52940
				public static LocString NAME = "No Seed Selected";

				// Token: 0x0400CECD RID: 52941
				public static LocString TOOLTIP = "Uproot wild plants to obtain seeds";
			}

			// Token: 0x02002FFD RID: 12285
			public class NEEDPOWER
			{
				// Token: 0x0400CECE RID: 52942
				public static LocString NAME = "No Power";

				// Token: 0x0400CECF RID: 52943
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"All connected ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" sources have lost charge"
				});
			}

			// Token: 0x02002FFE RID: 12286
			public class NOTENOUGHPOWER
			{
				// Token: 0x0400CED0 RID: 52944
				public static LocString NAME = "Insufficient Power";

				// Token: 0x0400CED1 RID: 52945
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building does not have enough stored ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" to run"
				});
			}

			// Token: 0x02002FFF RID: 12287
			public class POWERLOOPDETECTED
			{
				// Token: 0x0400CED2 RID: 52946
				public static LocString NAME = "Power Loop Detected";

				// Token: 0x0400CED3 RID: 52947
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A ",
					UI.PRE_KEYWORD,
					"Transformer's",
					UI.PST_KEYWORD,
					" ",
					UI.PRE_KEYWORD,
					"Power Output",
					UI.PST_KEYWORD,
					" has been connected back to its own ",
					UI.PRE_KEYWORD,
					"Input",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02003000 RID: 12288
			public class NEEDRESOURCE
			{
				// Token: 0x0400CED4 RID: 52948
				public static LocString NAME = "Resource Required";

				// Token: 0x0400CED5 RID: 52949
				public static LocString TOOLTIP = "This building is missing required materials";
			}

			// Token: 0x02003001 RID: 12289
			public class NEWDUPLICANTSAVAILABLE
			{
				// Token: 0x0400CED6 RID: 52950
				public static LocString NAME = "Printables Available";

				// Token: 0x0400CED7 RID: 52951
				public static LocString TOOLTIP = "I am ready to print a new colony member or care package";

				// Token: 0x0400CED8 RID: 52952
				public static LocString NOTIFICATION_NAME = "New Printables are available";

				// Token: 0x0400CED9 RID: 52953
				public static LocString NOTIFICATION_TOOLTIP = "The Printing Pod " + UI.FormatAsHotKey(global::Action.Plan1) + " is ready to print a new Duplicant or care package.\nI'll need to select a blueprint:";
			}

			// Token: 0x02003002 RID: 12290
			public class NOAPPLICABLERESEARCHSELECTED
			{
				// Token: 0x0400CEDA RID: 52954
				public static LocString NAME = "Inapplicable Research";

				// Token: 0x0400CEDB RID: 52955
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building cannot produce the correct ",
					UI.PRE_KEYWORD,
					"Research Type",
					UI.PST_KEYWORD,
					" for the current ",
					UI.FormatAsLink("Research Focus", "TECH")
				});

				// Token: 0x0400CEDC RID: 52956
				public static LocString NOTIFICATION_NAME = UI.FormatAsLink("Research Center", "ADVANCEDRESEARCHCENTER") + " idle";

				// Token: 0x0400CEDD RID: 52957
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"These buildings cannot produce the correct ",
					UI.PRE_KEYWORD,
					"Research Type",
					UI.PST_KEYWORD,
					" for the selected ",
					UI.FormatAsLink("Research Focus", "TECH"),
					":"
				});
			}

			// Token: 0x02003003 RID: 12291
			public class NOAPPLICABLEANALYSISSELECTED
			{
				// Token: 0x0400CEDE RID: 52958
				public static LocString NAME = "No Analysis Focus Selected";

				// Token: 0x0400CEDF RID: 52959
				public static LocString TOOLTIP = "Select an unknown destination from the " + UI.FormatAsManagementMenu("Starmap", global::Action.ManageStarmap) + " to begin analysis";

				// Token: 0x0400CEE0 RID: 52960
				public static LocString NOTIFICATION_NAME = UI.FormatAsLink("Telescope", "TELESCOPE") + " idle";

				// Token: 0x0400CEE1 RID: 52961
				public static LocString NOTIFICATION_TOOLTIP = "These buildings require an analysis focus:";
			}

			// Token: 0x02003004 RID: 12292
			public class NOAVAILABLESEED
			{
				// Token: 0x0400CEE2 RID: 52962
				public static LocString NAME = "No Seed Available";

				// Token: 0x0400CEE3 RID: 52963
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The selected ",
					UI.PRE_KEYWORD,
					"Seed",
					UI.PST_KEYWORD,
					" is not available"
				});
			}

			// Token: 0x02003005 RID: 12293
			public class NOSTORAGEFILTERSET
			{
				// Token: 0x0400CEE4 RID: 52964
				public static LocString NAME = "Filters Not Designated";

				// Token: 0x0400CEE5 RID: 52965
				public static LocString TOOLTIP = "No resources types are marked for storage in this building";
			}

			// Token: 0x02003006 RID: 12294
			public class NOSUITMARKER
			{
				// Token: 0x0400CEE6 RID: 52966
				public static LocString NAME = "No Checkpoint";

				// Token: 0x0400CEE7 RID: 52967
				public static LocString TOOLTIP = "Docks must be placed beside a " + BUILDINGS.PREFABS.CHECKPOINT.NAME + ", opposite the side the checkpoint faces";
			}

			// Token: 0x02003007 RID: 12295
			public class SUITMARKERWRONGSIDE
			{
				// Token: 0x0400CEE8 RID: 52968
				public static LocString NAME = "Invalid Checkpoint";

				// Token: 0x0400CEE9 RID: 52969
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building has been built on the wrong side of a ",
					BUILDINGS.PREFABS.CHECKPOINT.NAME,
					"\n\nDocks must be placed beside a ",
					BUILDINGS.PREFABS.CHECKPOINT.NAME,
					", opposite the side the checkpoint faces"
				});
			}

			// Token: 0x02003008 RID: 12296
			public class NOFILTERELEMENTSELECTED
			{
				// Token: 0x0400CEEA RID: 52970
				public static LocString NAME = "No Filter Selected";

				// Token: 0x0400CEEB RID: 52971
				public static LocString TOOLTIP = "Select a resource to filter";
			}

			// Token: 0x02003009 RID: 12297
			public class NOLUREELEMENTSELECTED
			{
				// Token: 0x0400CEEC RID: 52972
				public static LocString NAME = "No Bait Selected";

				// Token: 0x0400CEED RID: 52973
				public static LocString TOOLTIP = "Select a resource to use as bait";
			}

			// Token: 0x0200300A RID: 12298
			public class NOFISHABLEWATERBELOW
			{
				// Token: 0x0400CEEE RID: 52974
				public static LocString NAME = "No Fishable Water";

				// Token: 0x0400CEEF RID: 52975
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"There are no edible ",
					UI.PRE_KEYWORD,
					"Fish",
					UI.PST_KEYWORD,
					" beneath this structure"
				});
			}

			// Token: 0x0200300B RID: 12299
			public class NOPOWERCONSUMERS
			{
				// Token: 0x0400CEF0 RID: 52976
				public static LocString NAME = "No Power Consumers";

				// Token: 0x0400CEF1 RID: 52977
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"No buildings are connected to this ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" source"
				});
			}

			// Token: 0x0200300C RID: 12300
			public class NOWIRECONNECTED
			{
				// Token: 0x0400CEF2 RID: 52978
				public static LocString NAME = "No Power Wire Connected";

				// Token: 0x0400CEF3 RID: 52979
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building has not been connected to a ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" grid"
				});
			}

			// Token: 0x0200300D RID: 12301
			public class PENDINGDECONSTRUCTION
			{
				// Token: 0x0400CEF4 RID: 52980
				public static LocString NAME = "Deconstruction Errand";

				// Token: 0x0400CEF5 RID: 52981
				public static LocString TOOLTIP = "Building will be deconstructed once a Duplicant is available";
			}

			// Token: 0x0200300E RID: 12302
			public class PENDINGDEMOLITION
			{
				// Token: 0x0400CEF6 RID: 52982
				public static LocString NAME = "Demolition Errand";

				// Token: 0x0400CEF7 RID: 52983
				public static LocString TOOLTIP = "Object will be permanently demolished once a Duplicant is available";
			}

			// Token: 0x0200300F RID: 12303
			public class PENDINGFISH
			{
				// Token: 0x0400CEF8 RID: 52984
				public static LocString NAME = "Fishing Errand";

				// Token: 0x0400CEF9 RID: 52985
				public static LocString TOOLTIP = "Spot will be fished once a Duplicant is available";
			}

			// Token: 0x02003010 RID: 12304
			public class PENDINGHARVEST
			{
				// Token: 0x0400CEFA RID: 52986
				public static LocString NAME = "Harvest Errand";

				// Token: 0x0400CEFB RID: 52987
				public static LocString TOOLTIP = "Plant will be harvested once a Duplicant is available";
			}

			// Token: 0x02003011 RID: 12305
			public class PENDINGUPROOT
			{
				// Token: 0x0400CEFC RID: 52988
				public static LocString NAME = "Uproot Errand";

				// Token: 0x0400CEFD RID: 52989
				public static LocString TOOLTIP = "Plant will be uprooted once a Duplicant is available";
			}

			// Token: 0x02003012 RID: 12306
			public class PENDINGREPAIR
			{
				// Token: 0x0400CEFE RID: 52990
				public static LocString NAME = "Repair Errand";

				// Token: 0x0400CEFF RID: 52991
				public static LocString TOOLTIP = "Building will be repaired once a Duplicant is available\nReceived damage from {DamageInfo}";
			}

			// Token: 0x02003013 RID: 12307
			public class PENDINGSWITCHTOGGLE
			{
				// Token: 0x0400CF00 RID: 52992
				public static LocString NAME = "Settings Errand";

				// Token: 0x0400CF01 RID: 52993
				public static LocString TOOLTIP = "Settings will be changed once a Duplicant is available";
			}

			// Token: 0x02003014 RID: 12308
			public class PENDINGWORK
			{
				// Token: 0x0400CF02 RID: 52994
				public static LocString NAME = "Work Errand";

				// Token: 0x0400CF03 RID: 52995
				public static LocString TOOLTIP = "Building will be operated once a Duplicant is available";
			}

			// Token: 0x02003015 RID: 12309
			public class POWERBUTTONOFF
			{
				// Token: 0x0400CF04 RID: 52996
				public static LocString NAME = "Function Suspended";

				// Token: 0x0400CF05 RID: 52997
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building has been toggled off\nPress ",
					UI.PRE_KEYWORD,
					"Enable Building",
					UI.PST_KEYWORD,
					" ",
					UI.FormatAsHotKey(global::Action.ToggleEnabled),
					" to resume its use"
				});
			}

			// Token: 0x02003016 RID: 12310
			public class PUMPINGSTATION
			{
				// Token: 0x0400CF06 RID: 52998
				public static LocString NAME = "Liquid Available: {Liquids}";

				// Token: 0x0400CF07 RID: 52999
				public static LocString TOOLTIP = "This pumping station has access to: {Liquids}";
			}

			// Token: 0x02003017 RID: 12311
			public class PRESSUREOK
			{
				// Token: 0x0400CF08 RID: 53000
				public static LocString NAME = "Max Gas Pressure";

				// Token: 0x0400CF09 RID: 53001
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"High ambient ",
					UI.PRE_KEYWORD,
					"Gas Pressure",
					UI.PST_KEYWORD,
					" is preventing this building from emitting gas\n\nReduce pressure by pumping ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" away or clearing more space"
				});
			}

			// Token: 0x02003018 RID: 12312
			public class UNDERPRESSURE
			{
				// Token: 0x0400CF0A RID: 53002
				public static LocString NAME = "Low Air Pressure";

				// Token: 0x0400CF0B RID: 53003
				public static LocString TOOLTIP = "A minimum atmospheric pressure of <b>{TargetPressure}</b> is needed for this building to operate";
			}

			// Token: 0x02003019 RID: 12313
			public class STORAGELOCKER
			{
				// Token: 0x0400CF0C RID: 53004
				public static LocString NAME = "Storing: {Stored} / {Capacity} {Units}";

				// Token: 0x0400CF0D RID: 53005
				public static LocString TOOLTIP = "This container is storing <b>{Stored}{Units}</b> of a maximum <b>{Capacity}{Units}</b>";
			}

			// Token: 0x0200301A RID: 12314
			public class CRITTERCAPACITY
			{
				// Token: 0x0400CF0E RID: 53006
				public static LocString NAME = "Storing: {Stored} / {Capacity} Critters";

				// Token: 0x0400CF0F RID: 53007
				public static LocString TOOLTIP = "This container is storing <b>{Stored} {StoredUnits}</b> of a maximum <b>{Capacity} {CapacityUnits}</b>";

				// Token: 0x0400CF10 RID: 53008
				public static LocString UNITS = "Critters";

				// Token: 0x0400CF11 RID: 53009
				public static LocString UNIT = "Critter";
			}

			// Token: 0x0200301B RID: 12315
			public class SKILL_POINTS_AVAILABLE
			{
				// Token: 0x0400CF12 RID: 53010
				public static LocString NAME = "Skill Points Available";

				// Token: 0x0400CF13 RID: 53011
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A Duplicant has ",
					UI.PRE_KEYWORD,
					"Skill Points",
					UI.PST_KEYWORD,
					" available"
				});
			}

			// Token: 0x0200301C RID: 12316
			public class TANNINGLIGHTSUFFICIENT
			{
				// Token: 0x0400CF14 RID: 53012
				public static LocString NAME = "Tanning Light Available";

				// Token: 0x0400CF15 RID: 53013
				public static LocString TOOLTIP = "There is sufficient " + UI.FormatAsLink("Light", "LIGHT") + " here to create pleasing skin crisping";
			}

			// Token: 0x0200301D RID: 12317
			public class TANNINGLIGHTINSUFFICIENT
			{
				// Token: 0x0400CF16 RID: 53014
				public static LocString NAME = "Insufficient Tanning Light";

				// Token: 0x0400CF17 RID: 53015
				public static LocString TOOLTIP = "The " + UI.FormatAsLink("Light", "LIGHT") + " here is not bright enough for that Sunny Day feeling";
			}

			// Token: 0x0200301E RID: 12318
			public class UNASSIGNED
			{
				// Token: 0x0400CF18 RID: 53016
				public static LocString NAME = "Unassigned";

				// Token: 0x0400CF19 RID: 53017
				public static LocString TOOLTIP = "Assign a Duplicant to use this amenity";
			}

			// Token: 0x0200301F RID: 12319
			public class UNDERCONSTRUCTION
			{
				// Token: 0x0400CF1A RID: 53018
				public static LocString NAME = "Under Construction";

				// Token: 0x0400CF1B RID: 53019
				public static LocString TOOLTIP = "This building is currently being built";
			}

			// Token: 0x02003020 RID: 12320
			public class UNDERCONSTRUCTIONNOWORKER
			{
				// Token: 0x0400CF1C RID: 53020
				public static LocString NAME = "Construction Errand";

				// Token: 0x0400CF1D RID: 53021
				public static LocString TOOLTIP = "Building will be constructed once a Duplicant is available";
			}

			// Token: 0x02003021 RID: 12321
			public class WAITINGFORMATERIALS
			{
				// Token: 0x0400CF1E RID: 53022
				public static LocString NAME = "Awaiting Delivery\n{ItemsRemaining}";

				// Token: 0x0400CF1F RID: 53023
				public static LocString TOOLTIP = "These materials will be delivered once a Duplicant is available";

				// Token: 0x0400CF20 RID: 53024
				public static LocString LINE_ITEM_MASS = "• {0}: {1}";

				// Token: 0x0400CF21 RID: 53025
				public static LocString LINE_ITEM_UNITS = "• {0}";
			}

			// Token: 0x02003022 RID: 12322
			public class WAITINGFORRADIATION
			{
				// Token: 0x0400CF22 RID: 53026
				public static LocString NAME = "Awaiting Radbolts";

				// Token: 0x0400CF23 RID: 53027
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building requires Radbolts to function\n\nOpen the ",
					UI.FormatAsOverlay("Radiation Overlay"),
					" ",
					UI.FormatAsHotKey(global::Action.Overlay15),
					" to view this building's radiation port"
				});
			}

			// Token: 0x02003023 RID: 12323
			public class WAITINGFORREPAIRMATERIALS
			{
				// Token: 0x0400CF24 RID: 53028
				public static LocString NAME = "Awaiting Repair Delivery\n{ItemsRemaining}\n";

				// Token: 0x0400CF25 RID: 53029
				public static LocString TOOLTIP = "These materials must be delivered before this building can be repaired";

				// Token: 0x0400CF26 RID: 53030
				public static LocString LINE_ITEM = string.Concat(new string[]
				{
					"• ",
					UI.PRE_KEYWORD,
					"{0}",
					UI.PST_KEYWORD,
					": <b>{1}</b>"
				});
			}

			// Token: 0x02003024 RID: 12324
			public class MISSINGGANTRY
			{
				// Token: 0x0400CF27 RID: 53031
				public static LocString NAME = "Missing Gantry";

				// Token: 0x0400CF28 RID: 53032
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A ",
					UI.FormatAsLink("Gantry", "GANTRY"),
					" must be built below ",
					UI.FormatAsLink("Command Capsules", "COMMANDMODULE"),
					" and ",
					UI.FormatAsLink("Sight-Seeing Modules", "TOURISTMODULE"),
					" for Duplicant access"
				});
			}

			// Token: 0x02003025 RID: 12325
			public class DISEMBARKINGDUPLICANT
			{
				// Token: 0x0400CF29 RID: 53033
				public static LocString NAME = "Waiting To Disembark";

				// Token: 0x0400CF2A RID: 53034
				public static LocString TOOLTIP = "The Duplicant inside this rocket can't come out until the " + UI.FormatAsLink("Gantry", "GANTRY") + " is extended";
			}

			// Token: 0x02003026 RID: 12326
			public class REACTORMELTDOWN
			{
				// Token: 0x0400CF2B RID: 53035
				public static LocString NAME = "Reactor Meltdown";

				// Token: 0x0400CF2C RID: 53036
				public static LocString TOOLTIP = "This reactor is spilling dangerous radioactive waste and cannot be stopped";
			}

			// Token: 0x02003027 RID: 12327
			public class ROCKETNAME
			{
				// Token: 0x0400CF2D RID: 53037
				public static LocString NAME = "Parent Rocket: {0}";

				// Token: 0x0400CF2E RID: 53038
				public static LocString TOOLTIP = "This module belongs to the rocket: " + UI.PRE_KEYWORD + "{0}" + UI.PST_KEYWORD;
			}

			// Token: 0x02003028 RID: 12328
			public class HASGANTRY
			{
				// Token: 0x0400CF2F RID: 53039
				public static LocString NAME = "Has Gantry";

				// Token: 0x0400CF30 RID: 53040
				public static LocString TOOLTIP = "Duplicants may now enter this section of the rocket";
			}

			// Token: 0x02003029 RID: 12329
			public class NORMAL
			{
				// Token: 0x0400CF31 RID: 53041
				public static LocString NAME = "Normal";

				// Token: 0x0400CF32 RID: 53042
				public static LocString TOOLTIP = "Nothing out of the ordinary here";
			}

			// Token: 0x0200302A RID: 12330
			public class MANUALGENERATORCHARGINGUP
			{
				// Token: 0x0400CF33 RID: 53043
				public static LocString NAME = "Charging Up";

				// Token: 0x0400CF34 RID: 53044
				public static LocString TOOLTIP = "This power source is being charged";
			}

			// Token: 0x0200302B RID: 12331
			public class MANUALGENERATORRELEASINGENERGY
			{
				// Token: 0x0400CF35 RID: 53045
				public static LocString NAME = "Powering";

				// Token: 0x0400CF36 RID: 53046
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This generator is supplying energy to ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" consumers"
				});
			}

			// Token: 0x0200302C RID: 12332
			public class GENERATOROFFLINE
			{
				// Token: 0x0400CF37 RID: 53047
				public static LocString NAME = "Generator Idle";

				// Token: 0x0400CF38 RID: 53048
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" source is idle"
				});
			}

			// Token: 0x0200302D RID: 12333
			public class PIPE
			{
				// Token: 0x0400CF39 RID: 53049
				public static LocString NAME = "Contents: {Contents}";

				// Token: 0x0400CF3A RID: 53050
				public static LocString TOOLTIP = "This pipe is delivering {Contents}";
			}

			// Token: 0x0200302E RID: 12334
			public class CONVEYOR
			{
				// Token: 0x0400CF3B RID: 53051
				public static LocString NAME = "Contents: {Contents}";

				// Token: 0x0400CF3C RID: 53052
				public static LocString TOOLTIP = "This conveyor is delivering {Contents}";
			}

			// Token: 0x0200302F RID: 12335
			public class FABRICATORIDLE
			{
				// Token: 0x0400CF3D RID: 53053
				public static LocString NAME = "No Fabrications Queued";

				// Token: 0x0400CF3E RID: 53054
				public static LocString TOOLTIP = "Select a recipe to begin fabrication";
			}

			// Token: 0x02003030 RID: 12336
			public class FABRICATOREMPTY
			{
				// Token: 0x0400CF3F RID: 53055
				public static LocString NAME = "Waiting For Materials";

				// Token: 0x0400CF40 RID: 53056
				public static LocString TOOLTIP = "Fabrication will begin once materials have been delivered";
			}

			// Token: 0x02003031 RID: 12337
			public class FABRICATORLACKSHEP
			{
				// Token: 0x0400CF41 RID: 53057
				public static LocString NAME = "Waiting For Radbolts ({CurrentHEP}/{HEPRequired})";

				// Token: 0x0400CF42 RID: 53058
				public static LocString TOOLTIP = "A queued recipe requires more Radbolts than are currently stored.\n\nCurrently stored: {CurrentHEP}\nRequired for recipe: {HEPRequired}";
			}

			// Token: 0x02003032 RID: 12338
			public class TOILET
			{
				// Token: 0x0400CF43 RID: 53059
				public static LocString NAME = "{FlushesRemaining} \"Visits\" Remaining";

				// Token: 0x0400CF44 RID: 53060
				public static LocString TOOLTIP = "{FlushesRemaining} more Duplicants can use this amenity before it requires maintenance";
			}

			// Token: 0x02003033 RID: 12339
			public class TOILETNEEDSEMPTYING
			{
				// Token: 0x0400CF45 RID: 53061
				public static LocString NAME = "Requires Emptying";

				// Token: 0x0400CF46 RID: 53062
				public static LocString TOOLTIP = "This amenity cannot be used while full\n\nEmptying it will produce " + UI.FormatAsLink("Polluted Dirt", "TOXICSAND");
			}

			// Token: 0x02003034 RID: 12340
			public class DESALINATORNEEDSEMPTYING
			{
				// Token: 0x0400CF47 RID: 53063
				public static LocString NAME = "Requires Emptying";

				// Token: 0x0400CF48 RID: 53064
				public static LocString TOOLTIP = "This building needs to be emptied of " + UI.FormatAsLink("Salt", "SALT") + " to resume function";
			}

			// Token: 0x02003035 RID: 12341
			public class MILKSEPARATORNEEDSEMPTYING
			{
				// Token: 0x0400CF49 RID: 53065
				public static LocString NAME = "Requires Emptying";

				// Token: 0x0400CF4A RID: 53066
				public static LocString TOOLTIP = "This building needs to be emptied of " + UI.FormatAsLink("Brackwax", "MILKFAT") + " to resume function";
			}

			// Token: 0x02003036 RID: 12342
			public class HABITATNEEDSEMPTYING
			{
				// Token: 0x0400CF4B RID: 53067
				public static LocString NAME = "Requires Emptying";

				// Token: 0x0400CF4C RID: 53068
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.FormatAsLink("Algae Terrarium", "ALGAEHABITAT"),
					" needs to be emptied of ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					"\n\n",
					UI.FormatAsLink("Bottle Emptiers", "BOTTLEEMPTIER"),
					" can be used to transport and dispose of ",
					UI.FormatAsLink("Polluted Water", "DIRTYWATER"),
					" in designated areas"
				});
			}

			// Token: 0x02003037 RID: 12343
			public class UNUSABLE
			{
				// Token: 0x0400CF4D RID: 53069
				public static LocString NAME = "Out of Order";

				// Token: 0x0400CF4E RID: 53070
				public static LocString TOOLTIP = "This amenity requires maintenance";
			}

			// Token: 0x02003038 RID: 12344
			public class UNUSABLEGUNKED
			{
				// Token: 0x0400CF4F RID: 53071
				public static LocString NAME = "Out of Order: Gunk";

				// Token: 0x0400CF50 RID: 53072
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Someone dumped ",
					UI.FormatAsLink("Gunk", "LIQUIDGUNK"),
					" here instead of in a ",
					UI.FormatAsLink("Gunk Extractor", "GUNKEMPTIER"),
					"\n\nThis amenity requires maintenance"
				});
			}

			// Token: 0x02003039 RID: 12345
			public class NORESEARCHSELECTED
			{
				// Token: 0x0400CF51 RID: 53073
				public static LocString NAME = "No Research Focus Selected";

				// Token: 0x0400CF52 RID: 53074
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Open the ",
					UI.FormatAsManagementMenu("Research Tree", global::Action.ManageResearch),
					" to select a new ",
					UI.FormatAsLink("Research", "TECH"),
					" project"
				});

				// Token: 0x0400CF53 RID: 53075
				public static LocString NOTIFICATION_NAME = "No " + UI.FormatAsLink("Research Focus", "TECH") + " selected";

				// Token: 0x0400CF54 RID: 53076
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"Open the ",
					UI.FormatAsManagementMenu("Research Tree", global::Action.ManageResearch),
					" to select a new ",
					UI.FormatAsLink("Research", "TECH"),
					" project"
				});
			}

			// Token: 0x0200303A RID: 12346
			public class NORESEARCHORDESTINATIONSELECTED
			{
				// Token: 0x0400CF55 RID: 53077
				public static LocString NAME = "No Research Focus or Starmap Destination Selected";

				// Token: 0x0400CF56 RID: 53078
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Select a ",
					UI.FormatAsLink("Research", "TECH"),
					" project in the ",
					UI.FormatAsManagementMenu("Research Tree", global::Action.ManageResearch),
					" or a Destination in the ",
					UI.FormatAsManagementMenu("Starmap", global::Action.ManageStarmap)
				});

				// Token: 0x0400CF57 RID: 53079
				public static LocString NOTIFICATION_NAME = "No " + UI.FormatAsLink("Research Focus", "TECH") + " or Starmap destination selected";

				// Token: 0x0400CF58 RID: 53080
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"Select a ",
					UI.FormatAsLink("Research", "TECH"),
					" project in the ",
					UI.FormatAsManagementMenu("Research Tree", "[R]"),
					" or a Destination in the ",
					UI.FormatAsManagementMenu("Starmap", "[Z]")
				});
			}

			// Token: 0x0200303B RID: 12347
			public class RESEARCHING
			{
				// Token: 0x0400CF59 RID: 53081
				public static LocString NAME = "Current " + UI.FormatAsLink("Research", "TECH") + ": {Tech}";

				// Token: 0x0400CF5A RID: 53082
				public static LocString TOOLTIP = "Research produced at this station will be invested in {Tech}";
			}

			// Token: 0x0200303C RID: 12348
			public class TINKERING
			{
				// Token: 0x0400CF5B RID: 53083
				public static LocString NAME = "Tinkering: {0}";

				// Token: 0x0400CF5C RID: 53084
				public static LocString TOOLTIP = "This Duplicant is creating {0} to use somewhere else";
			}

			// Token: 0x0200303D RID: 12349
			public class VALVE
			{
				// Token: 0x0400CF5D RID: 53085
				public static LocString NAME = "Max Flow Rate: {MaxFlow}";

				// Token: 0x0400CF5E RID: 53086
				public static LocString TOOLTIP = "This valve is allowing flow at a volume of <b>{MaxFlow}</b>";
			}

			// Token: 0x0200303E RID: 12350
			public class VALVEREQUEST
			{
				// Token: 0x0400CF5F RID: 53087
				public static LocString NAME = "Requested Flow Rate: {QueuedMaxFlow}";

				// Token: 0x0400CF60 RID: 53088
				public static LocString TOOLTIP = "Waiting for a Duplicant to adjust flow rate";
			}

			// Token: 0x0200303F RID: 12351
			public class EMITTINGLIGHT
			{
				// Token: 0x0400CF61 RID: 53089
				public static LocString NAME = "Emitting Light";

				// Token: 0x0400CF62 RID: 53090
				public static LocString TOOLTIP = "Open the " + UI.FormatAsOverlay("Light Overlay", global::Action.Overlay5) + " to view this light's visibility radius";
			}

			// Token: 0x02003040 RID: 12352
			public class KETTLEINSUFICIENTSOLIDS
			{
				// Token: 0x0400CF63 RID: 53091
				public static LocString NAME = "Insufficient " + UI.FormatAsLink("Ice", "ICE");

				// Token: 0x0400CF64 RID: 53092
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building requires a minimum of {0} ",
					UI.FormatAsLink("Ice", "ICE"),
					" in order to function\n\nDeliver more ",
					UI.FormatAsLink("Ice", "ICE"),
					" to operate this building"
				});
			}

			// Token: 0x02003041 RID: 12353
			public class KETTLEINSUFICIENTFUEL
			{
				// Token: 0x0400CF65 RID: 53093
				public static LocString NAME = "Insufficient " + UI.FormatAsLink("Wood", "WOODLOG");

				// Token: 0x0400CF66 RID: 53094
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Colder ",
					UI.FormatAsLink("Ice", "ICE"),
					" increases the amount of ",
					UI.FormatAsLink("Wood", "WOODLOG"),
					" required for melting\n\nCurrent requirement: minimum {0} ",
					UI.FormatAsLink("Wood", "WOODLOG")
				});
			}

			// Token: 0x02003042 RID: 12354
			public class KETTLEINSUFICIENTLIQUIDSPACE
			{
				// Token: 0x0400CF67 RID: 53095
				public static LocString NAME = "Requires Emptying";

				// Token: 0x0400CF68 RID: 53096
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.FormatAsLink("Ice Liquefier", "ICEKETTLE"),
					" needs to be emptied of ",
					UI.FormatAsLink("Water", "WATER"),
					" in order to resume function\n\nIt requires at least {2} of storage space in order to function properly\n\nCurrently storing {0} of a maximum {1} ",
					UI.FormatAsLink("Water", "WATER")
				});
			}

			// Token: 0x02003043 RID: 12355
			public class KETTLEMELTING
			{
				// Token: 0x0400CF69 RID: 53097
				public static LocString NAME = "Melting Ice";

				// Token: 0x0400CF6A RID: 53098
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building is currently melting stored ",
					UI.FormatAsLink("Ice", "ICE"),
					" to produce ",
					UI.FormatAsLink("Water", "WATER"),
					"\n\n",
					UI.FormatAsLink("Water", "WATER"),
					" output temperature: {0}"
				});
			}

			// Token: 0x02003044 RID: 12356
			public class RATIONBOXCONTENTS
			{
				// Token: 0x0400CF6B RID: 53099
				public static LocString NAME = "Storing: {Stored}";

				// Token: 0x0400CF6C RID: 53100
				public static LocString TOOLTIP = "This box contains <b>{Stored}</b> of " + UI.PRE_KEYWORD + "Food" + UI.PST_KEYWORD;
			}

			// Token: 0x02003045 RID: 12357
			public class EMITTINGELEMENT
			{
				// Token: 0x0400CF6D RID: 53101
				public static LocString NAME = "Emitting {ElementType}: {FlowRate}";

				// Token: 0x0400CF6E RID: 53102
				public static LocString TOOLTIP = "Producing {ElementType} at " + UI.FormatAsPositiveRate("{FlowRate}");
			}

			// Token: 0x02003046 RID: 12358
			public class EMITTINGCO2
			{
				// Token: 0x0400CF6F RID: 53103
				public static LocString NAME = "Emitting CO<sub>2</sub>: {FlowRate}";

				// Token: 0x0400CF70 RID: 53104
				public static LocString TOOLTIP = "Producing " + ELEMENTS.CARBONDIOXIDE.NAME + " at " + UI.FormatAsPositiveRate("{FlowRate}");
			}

			// Token: 0x02003047 RID: 12359
			public class EMITTINGOXYGENAVG
			{
				// Token: 0x0400CF71 RID: 53105
				public static LocString NAME = "Emitting " + UI.FormatAsLink("Oxygen", "OXYGEN") + ": {FlowRate}";

				// Token: 0x0400CF72 RID: 53106
				public static LocString TOOLTIP = "Producing " + ELEMENTS.OXYGEN.NAME + " at a rate of " + UI.FormatAsPositiveRate("{FlowRate}");
			}

			// Token: 0x02003048 RID: 12360
			public class EMITTINGGASAVG
			{
				// Token: 0x0400CF73 RID: 53107
				public static LocString NAME = "Emitting {Element}: {FlowRate}";

				// Token: 0x0400CF74 RID: 53108
				public static LocString TOOLTIP = "Producing {Element} at a rate of " + UI.FormatAsPositiveRate("{FlowRate}");
			}

			// Token: 0x02003049 RID: 12361
			public class EMITTINGBLOCKEDHIGHPRESSURE
			{
				// Token: 0x0400CF75 RID: 53109
				public static LocString NAME = "Not Emitting: Overpressure";

				// Token: 0x0400CF76 RID: 53110
				public static LocString TOOLTIP = "Ambient pressure is too high for {Element} to be released";
			}

			// Token: 0x0200304A RID: 12362
			public class EMITTINGBLOCKEDLOWTEMPERATURE
			{
				// Token: 0x0400CF77 RID: 53111
				public static LocString NAME = "Not Emitting: Too Cold";

				// Token: 0x0400CF78 RID: 53112
				public static LocString TOOLTIP = "Temperature is too low for {Element} to be released";
			}

			// Token: 0x0200304B RID: 12363
			public class PUMPINGLIQUIDORGAS
			{
				// Token: 0x0400CF79 RID: 53113
				public static LocString NAME = "Average Flow Rate: {FlowRate}";

				// Token: 0x0400CF7A RID: 53114
				public static LocString TOOLTIP = "This building is pumping an average volume of " + UI.FormatAsPositiveRate("{FlowRate}");
			}

			// Token: 0x0200304C RID: 12364
			public class WIRECIRCUITSTATUS
			{
				// Token: 0x0400CF7B RID: 53115
				public static LocString NAME = "Current Load: {CurrentLoadAndColor} / {MaxLoad}";

				// Token: 0x0400CF7C RID: 53116
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The current ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" load on this wire\n\nOverloading a wire will cause damage to the wire over time and cause it to break"
				});
			}

			// Token: 0x0200304D RID: 12365
			public class WIREMAXWATTAGESTATUS
			{
				// Token: 0x0400CF7D RID: 53117
				public static LocString NAME = "Potential Load: {TotalPotentialLoadAndColor} / {MaxLoad}";

				// Token: 0x0400CF7E RID: 53118
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"How much wattage this network will draw if all ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" consumers on the network become active at once"
				});
			}

			// Token: 0x0200304E RID: 12366
			public class NOLIQUIDELEMENTTOPUMP
			{
				// Token: 0x0400CF7F RID: 53119
				public static LocString NAME = "Pump Not In Liquid";

				// Token: 0x0400CF80 RID: 53120
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This pump must be submerged in ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" to work"
				});
			}

			// Token: 0x0200304F RID: 12367
			public class NOGASELEMENTTOPUMP
			{
				// Token: 0x0400CF81 RID: 53121
				public static LocString NAME = "Pump Not In Gas";

				// Token: 0x0400CF82 RID: 53122
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This pump must be submerged in ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" to work"
				});
			}

			// Token: 0x02003050 RID: 12368
			public class INVALIDMASKSTATIONCONSUMPTIONSTATE
			{
				// Token: 0x0400CF83 RID: 53123
				public static LocString NAME = "Station Not In Oxygen";

				// Token: 0x0400CF84 RID: 53124
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This station must be submerged in ",
					UI.PRE_KEYWORD,
					"Oxygen",
					UI.PST_KEYWORD,
					" to work"
				});
			}

			// Token: 0x02003051 RID: 12369
			public class PIPEMAYMELT
			{
				// Token: 0x0400CF85 RID: 53125
				public static LocString NAME = "High Melt Risk";

				// Token: 0x0400CF86 RID: 53126
				public static LocString TOOLTIP = "This pipe is in danger of melting at the current " + UI.PRE_KEYWORD + "Temperature" + UI.PST_KEYWORD;
			}

			// Token: 0x02003052 RID: 12370
			public class ELEMENTEMITTEROUTPUT
			{
				// Token: 0x0400CF87 RID: 53127
				public static LocString NAME = "Emitting {ElementTypes}: {FlowRate}";

				// Token: 0x0400CF88 RID: 53128
				public static LocString TOOLTIP = "This object is releasing {ElementTypes} at a rate of " + UI.FormatAsPositiveRate("{FlowRate}");
			}

			// Token: 0x02003053 RID: 12371
			public class ELEMENTCONSUMER
			{
				// Token: 0x0400CF89 RID: 53129
				public static LocString NAME = "Consuming {ElementTypes}: {FlowRate}";

				// Token: 0x0400CF8A RID: 53130
				public static LocString TOOLTIP = "This object is utilizing ambient {ElementTypes} from the environment";
			}

			// Token: 0x02003054 RID: 12372
			public class SPACECRAFTREADYTOLAND
			{
				// Token: 0x0400CF8B RID: 53131
				public static LocString NAME = "Spacecraft ready to land";

				// Token: 0x0400CF8C RID: 53132
				public static LocString TOOLTIP = "A spacecraft is ready to land";

				// Token: 0x0400CF8D RID: 53133
				public static LocString NOTIFICATION = "Space mission complete";

				// Token: 0x0400CF8E RID: 53134
				public static LocString NOTIFICATION_TOOLTIP = "Spacecrafts have completed their missions";
			}

			// Token: 0x02003055 RID: 12373
			public class CONSUMINGFROMSTORAGE
			{
				// Token: 0x0400CF8F RID: 53135
				public static LocString NAME = "Consuming {ElementTypes}: {FlowRate}";

				// Token: 0x0400CF90 RID: 53136
				public static LocString TOOLTIP = "This building is consuming {ElementTypes} from storage";
			}

			// Token: 0x02003056 RID: 12374
			public class ELEMENTCONVERTEROUTPUT
			{
				// Token: 0x0400CF91 RID: 53137
				public static LocString NAME = "Emitting {ElementTypes}: {FlowRate}";

				// Token: 0x0400CF92 RID: 53138
				public static LocString TOOLTIP = "This building is releasing {ElementTypes} at a rate of " + UI.FormatAsPositiveRate("{FlowRate}");
			}

			// Token: 0x02003057 RID: 12375
			public class ELEMENTCONVERTERINPUT
			{
				// Token: 0x0400CF93 RID: 53139
				public static LocString NAME = "Using {ElementTypes}: {FlowRate}";

				// Token: 0x0400CF94 RID: 53140
				public static LocString TOOLTIP = "This building is using {ElementTypes} from storage at a rate of " + UI.FormatAsNegativeRate("{FlowRate}");
			}

			// Token: 0x02003058 RID: 12376
			public class AWAITINGCOMPOSTFLIP
			{
				// Token: 0x0400CF95 RID: 53141
				public static LocString NAME = "Requires Flipping";

				// Token: 0x0400CF96 RID: 53142
				public static LocString TOOLTIP = "Compost must be flipped periodically to produce " + UI.FormatAsLink("Dirt", "DIRT");
			}

			// Token: 0x02003059 RID: 12377
			public class AWAITINGWASTE
			{
				// Token: 0x0400CF97 RID: 53143
				public static LocString NAME = "Awaiting Compostables";

				// Token: 0x0400CF98 RID: 53144
				public static LocString TOOLTIP = "More waste material is required to begin the composting process";
			}

			// Token: 0x0200305A RID: 12378
			public class BATTERIESSUFFICIENTLYFULL
			{
				// Token: 0x0400CF99 RID: 53145
				public static LocString NAME = "Batteries Sufficiently Full";

				// Token: 0x0400CF9A RID: 53146
				public static LocString TOOLTIP = "All batteries are above the refill threshold";
			}

			// Token: 0x0200305B RID: 12379
			public class NEEDRESOURCEMASS
			{
				// Token: 0x0400CF9B RID: 53147
				public static LocString NAME = "Insufficient Resources\n{ResourcesRequired}";

				// Token: 0x0400CF9C RID: 53148
				public static LocString TOOLTIP = "The mass of material that was delivered to this building was too low\n\nDeliver more material to run this building";

				// Token: 0x0400CF9D RID: 53149
				public static LocString LINE_ITEM = "• <b>{0}</b>";
			}

			// Token: 0x0200305C RID: 12380
			public class JOULESAVAILABLE
			{
				// Token: 0x0400CF9E RID: 53150
				public static LocString NAME = "Power Available: {JoulesAvailable} / {JoulesCapacity}";

				// Token: 0x0400CF9F RID: 53151
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"<b>{JoulesAvailable}</b> of stored ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" available for use"
				});
			}

			// Token: 0x0200305D RID: 12381
			public class WATTAGE
			{
				// Token: 0x0400CFA0 RID: 53152
				public static LocString NAME = "Wattage: {Wattage}";

				// Token: 0x0400CFA1 RID: 53153
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building is generating ",
					UI.FormatAsPositiveRate("{Wattage}"),
					" of ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x0200305E RID: 12382
			public class SOLARPANELWATTAGE
			{
				// Token: 0x0400CFA2 RID: 53154
				public static LocString NAME = "Current Wattage: {Wattage}";

				// Token: 0x0400CFA3 RID: 53155
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This panel is generating ",
					UI.FormatAsPositiveRate("{Wattage}"),
					" of ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x0200305F RID: 12383
			public class MODULESOLARPANELWATTAGE
			{
				// Token: 0x0400CFA4 RID: 53156
				public static LocString NAME = "Current Wattage: {Wattage}";

				// Token: 0x0400CFA5 RID: 53157
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This panel is generating ",
					UI.FormatAsPositiveRate("{Wattage}"),
					" of ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x02003060 RID: 12384
			public class WATTSON
			{
				// Token: 0x0400CFA6 RID: 53158
				public static LocString NAME = "Next Print: {TimeRemaining}";

				// Token: 0x0400CFA7 RID: 53159
				public static LocString TOOLTIP = "The Printing Pod can print out new Duplicants and useful resources over time.\nThe next print will be ready in <b>{TimeRemaining}</b>";

				// Token: 0x0400CFA8 RID: 53160
				public static LocString UNAVAILABLE = "UNAVAILABLE";
			}

			// Token: 0x02003061 RID: 12385
			public class FLUSHTOILET
			{
				// Token: 0x0400CFA9 RID: 53161
				public static LocString NAME = "{toilet} Ready";

				// Token: 0x0400CFAA RID: 53162
				public static LocString TOOLTIP = "This bathroom is ready to receive visitors";
			}

			// Token: 0x02003062 RID: 12386
			public class FLUSHTOILETINUSE
			{
				// Token: 0x0400CFAB RID: 53163
				public static LocString NAME = "{toilet} In Use";

				// Token: 0x0400CFAC RID: 53164
				public static LocString TOOLTIP = "This bathroom is occupied";
			}

			// Token: 0x02003063 RID: 12387
			public class WIRECONNECTED
			{
				// Token: 0x0400CFAD RID: 53165
				public static LocString NAME = "Wire Connected";

				// Token: 0x0400CFAE RID: 53166
				public static LocString TOOLTIP = "This wire is connected to a network";
			}

			// Token: 0x02003064 RID: 12388
			public class WIRENOMINAL
			{
				// Token: 0x0400CFAF RID: 53167
				public static LocString NAME = "Wire Nominal";

				// Token: 0x0400CFB0 RID: 53168
				public static LocString TOOLTIP = "This wire is able to handle the wattage it is receiving";
			}

			// Token: 0x02003065 RID: 12389
			public class WIREDISCONNECTED
			{
				// Token: 0x0400CFB1 RID: 53169
				public static LocString NAME = "Wire Disconnected";

				// Token: 0x0400CFB2 RID: 53170
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This wire is not connecting a ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" consumer to a ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" generator"
				});
			}

			// Token: 0x02003066 RID: 12390
			public class COOLING
			{
				// Token: 0x0400CFB3 RID: 53171
				public static LocString NAME = "Cooling";

				// Token: 0x0400CFB4 RID: 53172
				public static LocString TOOLTIP = "This building is cooling the surrounding area";
			}

			// Token: 0x02003067 RID: 12391
			public class COOLINGSTALLEDHOTENV
			{
				// Token: 0x0400CFB5 RID: 53173
				public static LocString NAME = "Gas Too Hot";

				// Token: 0x0400CFB6 RID: 53174
				public static LocString TOOLTIP = "Incoming pipe contents cannot be cooled more than <b>{2}</b> below the surrounding environment\n\nEnvironment: {0}\nCurrent Pipe Contents: {1}";
			}

			// Token: 0x02003068 RID: 12392
			public class COOLINGSTALLEDCOLDGAS
			{
				// Token: 0x0400CFB7 RID: 53175
				public static LocString NAME = "Gas Too Cold";

				// Token: 0x0400CFB8 RID: 53176
				public static LocString TOOLTIP = "This building cannot cool incoming pipe contents below <b>{0}</b>\n\nCurrent Pipe Contents: {0}";
			}

			// Token: 0x02003069 RID: 12393
			public class COOLINGSTALLEDHOTLIQUID
			{
				// Token: 0x0400CFB9 RID: 53177
				public static LocString NAME = "Liquid Too Hot";

				// Token: 0x0400CFBA RID: 53178
				public static LocString TOOLTIP = "Incoming pipe contents cannot be cooled more than <b>{2}</b> below the surrounding environment\n\nEnvironment: {0}\nCurrent Pipe Contents: {1}";
			}

			// Token: 0x0200306A RID: 12394
			public class COOLINGSTALLEDCOLDLIQUID
			{
				// Token: 0x0400CFBB RID: 53179
				public static LocString NAME = "Liquid Too Cold";

				// Token: 0x0400CFBC RID: 53180
				public static LocString TOOLTIP = "This building cannot cool incoming pipe contents below <b>{0}</b>\n\nCurrent Pipe Contents: {0}";
			}

			// Token: 0x0200306B RID: 12395
			public class CANNOTCOOLFURTHER
			{
				// Token: 0x0400CFBD RID: 53181
				public static LocString NAME = "Minimum Temperature Reached";

				// Token: 0x0400CFBE RID: 53182
				public static LocString TOOLTIP = "This building cannot cool the surrounding environment below <b>{0}</b>";
			}

			// Token: 0x0200306C RID: 12396
			public class HEATINGSTALLEDHOTENV
			{
				// Token: 0x0400CFBF RID: 53183
				public static LocString NAME = "Target Temperature Reached";

				// Token: 0x0400CFC0 RID: 53184
				public static LocString TOOLTIP = "This building cannot heat the surrounding environment beyond <b>{0}</b>";
			}

			// Token: 0x0200306D RID: 12397
			public class HEATINGSTALLEDLOWMASS_GAS
			{
				// Token: 0x0400CFC1 RID: 53185
				public static LocString NAME = "Insufficient Atmosphere";

				// Token: 0x0400CFC2 RID: 53186
				public static LocString TOOLTIP = "This building cannot operate in a vacuum";
			}

			// Token: 0x0200306E RID: 12398
			public class HEATINGSTALLEDLOWMASS_LIQUID
			{
				// Token: 0x0400CFC3 RID: 53187
				public static LocString NAME = "Not Submerged In Liquid";

				// Token: 0x0400CFC4 RID: 53188
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building must be submerged in ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" to function"
				});
			}

			// Token: 0x0200306F RID: 12399
			public class BUILDINGDISABLED
			{
				// Token: 0x0400CFC5 RID: 53189
				public static LocString NAME = "Building Disabled";

				// Token: 0x0400CFC6 RID: 53190
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Press ",
					UI.PRE_KEYWORD,
					"Enable Building",
					UI.PST_KEYWORD,
					" ",
					UI.FormatAsHotKey(global::Action.ToggleEnabled),
					" to resume use"
				});
			}

			// Token: 0x02003070 RID: 12400
			public class MISSINGREQUIREMENTS
			{
				// Token: 0x0400CFC7 RID: 53191
				public static LocString NAME = "Missing Requirements";

				// Token: 0x0400CFC8 RID: 53192
				public static LocString TOOLTIP = "There are some problems that need to be fixed before this building is operational";
			}

			// Token: 0x02003071 RID: 12401
			public class GETTINGREADY
			{
				// Token: 0x0400CFC9 RID: 53193
				public static LocString NAME = "Getting Ready";

				// Token: 0x0400CFCA RID: 53194
				public static LocString TOOLTIP = "This building will soon be ready to use";
			}

			// Token: 0x02003072 RID: 12402
			public class WORKING
			{
				// Token: 0x0400CFCB RID: 53195
				public static LocString NAME = "Nominal";

				// Token: 0x0400CFCC RID: 53196
				public static LocString TOOLTIP = "This building is working as intended";
			}

			// Token: 0x02003073 RID: 12403
			public class GRAVEEMPTY
			{
				// Token: 0x0400CFCD RID: 53197
				public static LocString NAME = "Empty";

				// Token: 0x0400CFCE RID: 53198
				public static LocString TOOLTIP = "This memorial honors no one.";
			}

			// Token: 0x02003074 RID: 12404
			public class GRAVE
			{
				// Token: 0x0400CFCF RID: 53199
				public static LocString NAME = "RIP {DeadDupe}";

				// Token: 0x0400CFD0 RID: 53200
				public static LocString TOOLTIP = "{Epitaph}";
			}

			// Token: 0x02003075 RID: 12405
			public class AWAITINGARTING
			{
				// Token: 0x0400CFD1 RID: 53201
				public static LocString NAME = "Incomplete Artwork";

				// Token: 0x0400CFD2 RID: 53202
				public static LocString TOOLTIP = "This building requires a Duplicant's artistic touch";
			}

			// Token: 0x02003076 RID: 12406
			public class LOOKINGUGLY
			{
				// Token: 0x0400CFD3 RID: 53203
				public static LocString NAME = "Crude";

				// Token: 0x0400CFD4 RID: 53204
				public static LocString TOOLTIP = "Honestly, Morbs could've done better";
			}

			// Token: 0x02003077 RID: 12407
			public class LOOKINGOKAY
			{
				// Token: 0x0400CFD5 RID: 53205
				public static LocString NAME = "Quaint";

				// Token: 0x0400CFD6 RID: 53206
				public static LocString TOOLTIP = "Duplicants find this art piece quite charming";
			}

			// Token: 0x02003078 RID: 12408
			public class LOOKINGGREAT
			{
				// Token: 0x0400CFD7 RID: 53207
				public static LocString NAME = "Masterpiece";

				// Token: 0x0400CFD8 RID: 53208
				public static LocString TOOLTIP = "This poignant piece stirs something deep within each Duplicant's soul";
			}

			// Token: 0x02003079 RID: 12409
			public class EXPIRED
			{
				// Token: 0x0400CFD9 RID: 53209
				public static LocString NAME = "Depleted";

				// Token: 0x0400CFDA RID: 53210
				public static LocString TOOLTIP = "This building has no more use";
			}

			// Token: 0x0200307A RID: 12410
			public class COOLINGWATER
			{
				// Token: 0x0400CFDB RID: 53211
				public static LocString NAME = "Cooling Water";

				// Token: 0x0400CFDC RID: 53212
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building is cooling ",
					UI.PRE_KEYWORD,
					"Water",
					UI.PST_KEYWORD,
					" down to its freezing point"
				});
			}

			// Token: 0x0200307B RID: 12411
			public class EXCAVATOR_BOMB
			{
				// Token: 0x02003AFF RID: 15103
				public class UNARMED
				{
					// Token: 0x0400E9F5 RID: 59893
					public static LocString NAME = "Unarmed";

					// Token: 0x0400E9F6 RID: 59894
					public static LocString TOOLTIP = "This explosive is currently inactive";
				}

				// Token: 0x02003B00 RID: 15104
				public class ARMED
				{
					// Token: 0x0400E9F7 RID: 59895
					public static LocString NAME = "Armed";

					// Token: 0x0400E9F8 RID: 59896
					public static LocString TOOLTIP = "Stand back, this baby's ready to blow!";
				}

				// Token: 0x02003B01 RID: 15105
				public class COUNTDOWN
				{
					// Token: 0x0400E9F9 RID: 59897
					public static LocString NAME = "Countdown: {0}";

					// Token: 0x0400E9FA RID: 59898
					public static LocString TOOLTIP = "<b>{0}</b> seconds until detonation";
				}

				// Token: 0x02003B02 RID: 15106
				public class DUPE_DANGER
				{
					// Token: 0x0400E9FB RID: 59899
					public static LocString NAME = "Duplicant Preservation Override";

					// Token: 0x0400E9FC RID: 59900
					public static LocString TOOLTIP = "Explosive disabled due to close Duplicant proximity";
				}

				// Token: 0x02003B03 RID: 15107
				public class EXPLODING
				{
					// Token: 0x0400E9FD RID: 59901
					public static LocString NAME = "Exploding";

					// Token: 0x0400E9FE RID: 59902
					public static LocString TOOLTIP = "Kaboom!";
				}
			}

			// Token: 0x0200307C RID: 12412
			public class BURNER
			{
				// Token: 0x02003B04 RID: 15108
				public class BURNING_FUEL
				{
					// Token: 0x0400E9FF RID: 59903
					public static LocString NAME = "Consuming Fuel: {0}";

					// Token: 0x0400EA00 RID: 59904
					public static LocString TOOLTIP = "<b>{0}</b> fuel remaining";
				}

				// Token: 0x02003B05 RID: 15109
				public class HAS_FUEL
				{
					// Token: 0x0400EA01 RID: 59905
					public static LocString NAME = "Fueled: {0}";

					// Token: 0x0400EA02 RID: 59906
					public static LocString TOOLTIP = "<b>{0}</b> fuel remaining";
				}
			}

			// Token: 0x0200307D RID: 12413
			public class CREATURE_REUSABLE_TRAP
			{
				// Token: 0x02003B06 RID: 15110
				public class NEEDS_ARMING
				{
					// Token: 0x0400EA03 RID: 59907
					public static LocString NAME = "Waiting to be Armed";

					// Token: 0x0400EA04 RID: 59908
					public static LocString TOOLTIP = "Waiting for a Duplicant to arm this trap\n\nOnly Duplicants with the " + DUPLICANTS.ROLES.RANCHER.NAME + " skill can arm traps";
				}

				// Token: 0x02003B07 RID: 15111
				public class READY
				{
					// Token: 0x0400EA05 RID: 59909
					public static LocString NAME = "Armed";

					// Token: 0x0400EA06 RID: 59910
					public static LocString TOOLTIP = "This trap has been armed and is ready to catch a " + UI.PRE_KEYWORD + "Critter" + UI.PST_KEYWORD;
				}

				// Token: 0x02003B08 RID: 15112
				public class SPRUNG
				{
					// Token: 0x0400EA07 RID: 59911
					public static LocString NAME = "Sprung";

					// Token: 0x0400EA08 RID: 59912
					public static LocString TOOLTIP = "This trap has caught a {0}!";
				}
			}

			// Token: 0x0200307E RID: 12414
			public class CREATURE_TRAP
			{
				// Token: 0x02003B09 RID: 15113
				public class NEEDSBAIT
				{
					// Token: 0x0400EA09 RID: 59913
					public static LocString NAME = "Needs Bait";

					// Token: 0x0400EA0A RID: 59914
					public static LocString TOOLTIP = "This trap needs to be baited before it can be set";
				}

				// Token: 0x02003B0A RID: 15114
				public class READY
				{
					// Token: 0x0400EA0B RID: 59915
					public static LocString NAME = "Set";

					// Token: 0x0400EA0C RID: 59916
					public static LocString TOOLTIP = "This trap has been set and is ready to catch a " + UI.PRE_KEYWORD + "Critter" + UI.PST_KEYWORD;
				}

				// Token: 0x02003B0B RID: 15115
				public class SPRUNG
				{
					// Token: 0x0400EA0D RID: 59917
					public static LocString NAME = "Sprung";

					// Token: 0x0400EA0E RID: 59918
					public static LocString TOOLTIP = "This trap has caught a {0}!";
				}
			}

			// Token: 0x0200307F RID: 12415
			public class ACCESS_CONTROL
			{
				// Token: 0x02003B0C RID: 15116
				public class ACTIVE
				{
					// Token: 0x0400EA0F RID: 59919
					public static LocString NAME = "Access Restrictions";

					// Token: 0x0400EA10 RID: 59920
					public static LocString TOOLTIP = "Some Duplicants are prohibited from passing through this door by the current " + UI.PRE_KEYWORD + "Access Permissions" + UI.PST_KEYWORD;
				}

				// Token: 0x02003B0D RID: 15117
				public class OFFLINE
				{
					// Token: 0x0400EA11 RID: 59921
					public static LocString NAME = "Access Control Offline";

					// Token: 0x0400EA12 RID: 59922
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"This door has granted Emergency ",
						UI.PRE_KEYWORD,
						"Access Permissions",
						UI.PST_KEYWORD,
						"\n\nAll Duplicants are permitted to pass through it until ",
						UI.PRE_KEYWORD,
						"Power",
						UI.PST_KEYWORD,
						" is restored"
					});
				}
			}

			// Token: 0x02003080 RID: 12416
			public class REQUIRESSKILLPERK
			{
				// Token: 0x0400CFDD RID: 53213
				public static LocString NAME = "Skill-Required Operation";

				// Token: 0x0400CFDE RID: 53214
				public static LocString TOOLTIP = "Only Duplicants with the {Skills} Skill can operate this building";

				// Token: 0x0400CFDF RID: 53215
				public static LocString TOOLTIP_DLC3 = "Only Duplicants with the {Skills} Skill or {Boosters} can operate this building";
			}

			// Token: 0x02003081 RID: 12417
			public class DIGREQUIRESSKILLPERK
			{
				// Token: 0x0400CFE0 RID: 53216
				public static LocString NAME = "Skill-Required Dig";

				// Token: 0x0400CFE1 RID: 53217
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Only Duplicants with one of the following ",
					UI.PRE_KEYWORD,
					"Skills",
					UI.PST_KEYWORD,
					" can mine this material:\n{Skills}"
				});

				// Token: 0x0400CFE2 RID: 53218
				public static LocString TOOLTIP_DLC3 = "Only Duplicants with the {Skills} Skill or {Boosters} can mine this material";
			}

			// Token: 0x02003082 RID: 12418
			public class COLONYLACKSREQUIREDSKILLPERK
			{
				// Token: 0x0400CFE3 RID: 53219
				public static LocString NAME = "Colony Lacks {Skills} Skill";

				// Token: 0x0400CFE4 RID: 53220
				public static LocString TOOLTIP = "{Skills} Skill required to operate\n\nOpen the " + UI.FormatAsManagementMenu("Skills Panel", global::Action.ManageSkills) + " to teach {Skills} to a Duplicant";

				// Token: 0x0400CFE5 RID: 53221
				public static LocString TOOLTIP_DLC3 = "{Skills} Skill or {Boosters} required to operate\n\nOpen the " + UI.FormatAsManagementMenu("Skills Panel", global::Action.ManageSkills) + " to teach {Skills} to a Duplicant";
			}

			// Token: 0x02003083 RID: 12419
			public class CLUSTERCOLONYLACKSREQUIREDSKILLPERK
			{
				// Token: 0x0400CFE6 RID: 53222
				public static LocString NAME = "Local Colony Lacks {Skills} Skill";

				// Token: 0x0400CFE7 RID: 53223
				public static LocString TOOLTIP = BUILDING.STATUSITEMS.COLONYLACKSREQUIREDSKILLPERK.TOOLTIP + ", or bring a Duplicant with the skill from another " + UI.CLUSTERMAP.PLANETOID;

				// Token: 0x0400CFE8 RID: 53224
				public static LocString TOOLTIP_DLC3 = BUILDING.STATUSITEMS.COLONYLACKSREQUIREDSKILLPERK.TOOLTIP_DLC3 + ", or bring a Duplicant with this skill or booster from another " + UI.CLUSTERMAP.PLANETOID;
			}

			// Token: 0x02003084 RID: 12420
			public class WORKREQUIRESMINION
			{
				// Token: 0x0400CFE9 RID: 53225
				public static LocString NAME = "Duplicant Operation Required";

				// Token: 0x0400CFEA RID: 53226
				public static LocString TOOLTIP = "A Duplicant must be present to complete this operation";
			}

			// Token: 0x02003085 RID: 12421
			public class SWITCHSTATUSACTIVE
			{
				// Token: 0x0400CFEB RID: 53227
				public static LocString NAME = "Active";

				// Token: 0x0400CFEC RID: 53228
				public static LocString TOOLTIP = "This switch is currently toggled <b>On</b>";
			}

			// Token: 0x02003086 RID: 12422
			public class SWITCHSTATUSINACTIVE
			{
				// Token: 0x0400CFED RID: 53229
				public static LocString NAME = "Inactive";

				// Token: 0x0400CFEE RID: 53230
				public static LocString TOOLTIP = "This switch is currently toggled <b>Off</b>";
			}

			// Token: 0x02003087 RID: 12423
			public class LOGICSWITCHSTATUSACTIVE
			{
				// Token: 0x0400CFEF RID: 53231
				public static LocString NAME = "Sending a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);

				// Token: 0x0400CFF0 RID: 53232
				public static LocString TOOLTIP = "This switch is currently sending a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);
			}

			// Token: 0x02003088 RID: 12424
			public class LOGICSWITCHSTATUSINACTIVE
			{
				// Token: 0x0400CFF1 RID: 53233
				public static LocString NAME = "Sending a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x0400CFF2 RID: 53234
				public static LocString TOOLTIP = "This switch is currently sending a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x02003089 RID: 12425
			public class LOGICSENSORSTATUSACTIVE
			{
				// Token: 0x0400CFF3 RID: 53235
				public static LocString NAME = "Sending a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);

				// Token: 0x0400CFF4 RID: 53236
				public static LocString TOOLTIP = "This sensor is currently sending a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active);
			}

			// Token: 0x0200308A RID: 12426
			public class LOGICSENSORSTATUSINACTIVE
			{
				// Token: 0x0400CFF5 RID: 53237
				public static LocString NAME = "Sending a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);

				// Token: 0x0400CFF6 RID: 53238
				public static LocString TOOLTIP = "This sensor is currently sending " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
			}

			// Token: 0x0200308B RID: 12427
			public class PLAYERCONTROLLEDTOGGLESIDESCREEN
			{
				// Token: 0x0400CFF7 RID: 53239
				public static LocString NAME = "Pending Toggle on Unpause";

				// Token: 0x0400CFF8 RID: 53240
				public static LocString TOOLTIP = "This will be toggled when time is unpaused";
			}

			// Token: 0x0200308C RID: 12428
			public class FOOD_CONTAINERS_OUTSIDE_RANGE
			{
				// Token: 0x0400CFF9 RID: 53241
				public static LocString NAME = "Unreachable food";

				// Token: 0x0400CFFA RID: 53242
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Recuperating Duplicants must have ",
					UI.PRE_KEYWORD,
					"Food",
					UI.PST_KEYWORD,
					" available within <b>{0}</b> cells"
				});
			}

			// Token: 0x0200308D RID: 12429
			public class TOILETS_OUTSIDE_RANGE
			{
				// Token: 0x0400CFFB RID: 53243
				public static LocString NAME = "Unreachable restroom";

				// Token: 0x0400CFFC RID: 53244
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Recuperating Duplicants must have ",
					UI.PRE_KEYWORD,
					"Toilets",
					UI.PST_KEYWORD,
					" available within <b>{0}</b> cells"
				});
			}

			// Token: 0x0200308E RID: 12430
			public class BUILDING_DEPRECATED
			{
				// Token: 0x0400CFFD RID: 53245
				public static LocString NAME = "Building Deprecated";

				// Token: 0x0400CFFE RID: 53246
				public static LocString TOOLTIP = "This building is from an older version of the game and its use is not intended";
			}

			// Token: 0x0200308F RID: 12431
			public class TURBINE_BLOCKED_INPUT
			{
				// Token: 0x0400CFFF RID: 53247
				public static LocString NAME = "All Inputs Blocked";

				// Token: 0x0400D000 RID: 53248
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This turbine's ",
					UI.PRE_KEYWORD,
					"Input Vents",
					UI.PST_KEYWORD,
					" are blocked, so it can't intake any ",
					ELEMENTS.STEAM.NAME,
					".\n\nThe ",
					UI.PRE_KEYWORD,
					"Input Vents",
					UI.PST_KEYWORD,
					" are located directly below the foundation ",
					UI.PRE_KEYWORD,
					"Tile",
					UI.PST_KEYWORD,
					" this building is resting on."
				});
			}

			// Token: 0x02003090 RID: 12432
			public class TURBINE_PARTIALLY_BLOCKED_INPUT
			{
				// Token: 0x0400D001 RID: 53249
				public static LocString NAME = "{Blocked}/{Total} Inputs Blocked";

				// Token: 0x0400D002 RID: 53250
				public static LocString TOOLTIP = "<b>{Blocked}</b> of this turbine's <b>{Total}</b> inputs have been blocked, resulting in reduced throughput";
			}

			// Token: 0x02003091 RID: 12433
			public class TURBINE_TOO_HOT
			{
				// Token: 0x0400D003 RID: 53251
				public static LocString NAME = "Turbine Too Hot";

				// Token: 0x0400D004 RID: 53252
				public static LocString TOOLTIP = "This turbine must be below <b>{Overheat_Temperature}</b> to properly process {Src_Element} into {Dest_Element}";
			}

			// Token: 0x02003092 RID: 12434
			public class TURBINE_BLOCKED_OUTPUT
			{
				// Token: 0x0400D005 RID: 53253
				public static LocString NAME = "Output Blocked";

				// Token: 0x0400D006 RID: 53254
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A blocked ",
					UI.PRE_KEYWORD,
					"Output",
					UI.PST_KEYWORD,
					" has stopped this turbine from functioning"
				});
			}

			// Token: 0x02003093 RID: 12435
			public class TURBINE_INSUFFICIENT_MASS
			{
				// Token: 0x0400D007 RID: 53255
				public static LocString NAME = "Not Enough {Src_Element}";

				// Token: 0x0400D008 RID: 53256
				public static LocString TOOLTIP = "The {Src_Element} present below this turbine must be at least <b>{Min_Mass}</b> in order to turn the turbine";
			}

			// Token: 0x02003094 RID: 12436
			public class TURBINE_INSUFFICIENT_TEMPERATURE
			{
				// Token: 0x0400D009 RID: 53257
				public static LocString NAME = "{Src_Element} Temperature Below {Active_Temperature}";

				// Token: 0x0400D00A RID: 53258
				public static LocString TOOLTIP = "This turbine requires {Src_Element} that is a minimum of <b>{Active_Temperature}</b> in order to produce power";
			}

			// Token: 0x02003095 RID: 12437
			public class TURBINE_ACTIVE_WATTAGE
			{
				// Token: 0x0400D00B RID: 53259
				public static LocString NAME = "Current Wattage: {Wattage}";

				// Token: 0x0400D00C RID: 53260
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This turbine is generating ",
					UI.FormatAsPositiveRate("{Wattage}"),
					" of ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					"\n\nIt is running at <b>{Efficiency}</b> of full capacity\n\nIncrease {Src_Element} ",
					UI.PRE_KEYWORD,
					"Mass",
					UI.PST_KEYWORD,
					" and ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" to improve output"
				});
			}

			// Token: 0x02003096 RID: 12438
			public class TURBINE_SPINNING_UP
			{
				// Token: 0x0400D00D RID: 53261
				public static LocString NAME = "Spinning Up";

				// Token: 0x0400D00E RID: 53262
				public static LocString TOOLTIP = "This turbine is currently spinning up\n\nSpinning up allows a turbine to continue running for a short period if the pressure it needs to run becomes unavailable";
			}

			// Token: 0x02003097 RID: 12439
			public class TURBINE_ACTIVE
			{
				// Token: 0x0400D00F RID: 53263
				public static LocString NAME = "Active";

				// Token: 0x0400D010 RID: 53264
				public static LocString TOOLTIP = "This turbine is running at <b>{0}RPM</b>";
			}

			// Token: 0x02003098 RID: 12440
			public class WELL_PRESSURIZING
			{
				// Token: 0x0400D011 RID: 53265
				public static LocString NAME = "Backpressure: {0}";

				// Token: 0x0400D012 RID: 53266
				public static LocString TOOLTIP = "Well pressure increases with each use and must be periodically relieved to prevent shutdown";
			}

			// Token: 0x02003099 RID: 12441
			public class WELL_OVERPRESSURE
			{
				// Token: 0x0400D013 RID: 53267
				public static LocString NAME = "Overpressure";

				// Token: 0x0400D014 RID: 53268
				public static LocString TOOLTIP = "This well can no longer function due to excessive backpressure";
			}

			// Token: 0x0200309A RID: 12442
			public class NOTINANYROOM
			{
				// Token: 0x0400D015 RID: 53269
				public static LocString NAME = "Outside of room";

				// Token: 0x0400D016 RID: 53270
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building must be built inside a ",
					UI.PRE_KEYWORD,
					"Room",
					UI.PST_KEYWORD,
					" for full functionality\n\nOpen the ",
					UI.FormatAsOverlay("Room Overlay", global::Action.Overlay11),
					" to view full ",
					UI.PRE_KEYWORD,
					"Room",
					UI.PST_KEYWORD,
					" status"
				});
			}

			// Token: 0x0200309B RID: 12443
			public class NOTINREQUIREDROOM
			{
				// Token: 0x0400D017 RID: 53271
				public static LocString NAME = "Outside of {0}";

				// Token: 0x0400D018 RID: 53272
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building must be built inside a {0} for full functionality\n\nOpen the ",
					UI.FormatAsOverlay("Room Overlay", global::Action.Overlay11),
					" to view full ",
					UI.PRE_KEYWORD,
					"Room",
					UI.PST_KEYWORD,
					" status"
				});
			}

			// Token: 0x0200309C RID: 12444
			public class NOTINRECOMMENDEDROOM
			{
				// Token: 0x0400D019 RID: 53273
				public static LocString NAME = "Outside of {0}";

				// Token: 0x0400D01A RID: 53274
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"It is recommended to build this building inside a {0}\n\nOpen the ",
					UI.FormatAsOverlay("Room Overlay", global::Action.Overlay11),
					" to view full ",
					UI.PRE_KEYWORD,
					"Room",
					UI.PST_KEYWORD,
					" status"
				});
			}

			// Token: 0x0200309D RID: 12445
			public class RELEASING_PRESSURE
			{
				// Token: 0x0400D01B RID: 53275
				public static LocString NAME = "Releasing Pressure";

				// Token: 0x0400D01C RID: 53276
				public static LocString TOOLTIP = "Pressure buildup is being safely released";
			}

			// Token: 0x0200309E RID: 12446
			public class LOGIC_FEEDBACK_LOOP
			{
				// Token: 0x0400D01D RID: 53277
				public static LocString NAME = "Feedback Loop";

				// Token: 0x0400D01E RID: 53278
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Feedback loops prevent automation grids from functioning\n\nFeedback loops occur when the ",
					UI.PRE_KEYWORD,
					"Output",
					UI.PST_KEYWORD,
					" of an automated building connects back to its own ",
					UI.PRE_KEYWORD,
					"Input",
					UI.PST_KEYWORD,
					" through the Automation grid"
				});
			}

			// Token: 0x0200309F RID: 12447
			public class ENOUGH_COOLANT
			{
				// Token: 0x0400D01F RID: 53279
				public static LocString NAME = "Awaiting Coolant";

				// Token: 0x0400D020 RID: 53280
				public static LocString TOOLTIP = "<b>{1}</b> of {0} must be present in storage to begin production";
			}

			// Token: 0x020030A0 RID: 12448
			public class ENOUGH_FUEL
			{
				// Token: 0x0400D021 RID: 53281
				public static LocString NAME = "Awaiting Fuel";

				// Token: 0x0400D022 RID: 53282
				public static LocString TOOLTIP = "<b>{1}</b> of {0} must be present in storage to begin production";
			}

			// Token: 0x020030A1 RID: 12449
			public class LOGIC
			{
				// Token: 0x0400D023 RID: 53283
				public static LocString LOGIC_CONTROLLED_ENABLED = "Enabled by Automation Grid";

				// Token: 0x0400D024 RID: 53284
				public static LocString LOGIC_CONTROLLED_DISABLED = "Disabled by Automation Grid";
			}

			// Token: 0x020030A2 RID: 12450
			public class GANTRY
			{
				// Token: 0x0400D025 RID: 53285
				public static LocString AUTOMATION_CONTROL = "Automation Control: {0}";

				// Token: 0x0400D026 RID: 53286
				public static LocString MANUAL_CONTROL = "Manual Control: {0}";

				// Token: 0x0400D027 RID: 53287
				public static LocString EXTENDED = "Extended";

				// Token: 0x0400D028 RID: 53288
				public static LocString RETRACTED = "Retracted";
			}

			// Token: 0x020030A3 RID: 12451
			public class OBJECTDISPENSER
			{
				// Token: 0x0400D029 RID: 53289
				public static LocString AUTOMATION_CONTROL = "Automation Control: {0}";

				// Token: 0x0400D02A RID: 53290
				public static LocString MANUAL_CONTROL = "Manual Control: {0}";

				// Token: 0x0400D02B RID: 53291
				public static LocString OPENED = "Opened";

				// Token: 0x0400D02C RID: 53292
				public static LocString CLOSED = "Closed";
			}

			// Token: 0x020030A4 RID: 12452
			public class TOO_COLD
			{
				// Token: 0x0400D02D RID: 53293
				public static LocString NAME = "Too Cold";

				// Token: 0x0400D02E RID: 53294
				public static LocString TOOLTIP = "Either this building or its surrounding environment is too cold to operate";
			}

			// Token: 0x020030A5 RID: 12453
			public class CHECKPOINT
			{
				// Token: 0x0400D02F RID: 53295
				public static LocString LOGIC_CONTROLLED_OPEN = "Clearance: Permitted";

				// Token: 0x0400D030 RID: 53296
				public static LocString LOGIC_CONTROLLED_CLOSED = "Clearance: Not Permitted";

				// Token: 0x0400D031 RID: 53297
				public static LocString LOGIC_CONTROLLED_DISCONNECTED = "No Automation";

				// Token: 0x02003B0E RID: 15118
				public class TOOLTIPS
				{
					// Token: 0x0400EA13 RID: 59923
					public static LocString LOGIC_CONTROLLED_OPEN = "Automated Checkpoint is receiving a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby) + ", preventing Duplicants from passing";

					// Token: 0x0400EA14 RID: 59924
					public static LocString LOGIC_CONTROLLED_CLOSED = "Automated Checkpoint is receiving a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + ", allowing Duplicants to pass";

					// Token: 0x0400EA15 RID: 59925
					public static LocString LOGIC_CONTROLLED_DISCONNECTED = string.Concat(new string[]
					{
						"This Checkpoint has not been connected to an ",
						UI.PRE_KEYWORD,
						"Automation",
						UI.PST_KEYWORD,
						" grid"
					});
				}
			}

			// Token: 0x020030A6 RID: 12454
			public class HIGHENERGYPARTICLEREDIRECTOR
			{
				// Token: 0x0400D032 RID: 53298
				public static LocString LOGIC_CONTROLLED_STANDBY = "Incoming Radbolts: Ignore";

				// Token: 0x0400D033 RID: 53299
				public static LocString LOGIC_CONTROLLED_ACTIVE = "Incoming Radbolts: Redirect";

				// Token: 0x0400D034 RID: 53300
				public static LocString NORMAL = "Normal";

				// Token: 0x02003B0F RID: 15119
				public class TOOLTIPS
				{
					// Token: 0x0400EA16 RID: 59926
					public static LocString LOGIC_CONTROLLED_STANDBY = string.Concat(new string[]
					{
						UI.FormatAsKeyWord("Radbolt Reflector"),
						" is receiving a ",
						UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
						", ignoring incoming ",
						UI.PRE_KEYWORD,
						"Radbolts",
						UI.PST_KEYWORD
					});

					// Token: 0x0400EA17 RID: 59927
					public static LocString LOGIC_CONTROLLED_ACTIVE = string.Concat(new string[]
					{
						UI.FormatAsKeyWord("Radbolt Reflector"),
						" is receiving a ",
						UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
						", accepting incoming ",
						UI.PRE_KEYWORD,
						"Radbolts",
						UI.PST_KEYWORD
					});

					// Token: 0x0400EA18 RID: 59928
					public static LocString NORMAL = "Incoming Radbolts will be accepted and redirected";
				}
			}

			// Token: 0x020030A7 RID: 12455
			public class HIGHENERGYPARTICLESPAWNER
			{
				// Token: 0x0400D035 RID: 53301
				public static LocString LOGIC_CONTROLLED_STANDBY = "Launch Radbolt: Off";

				// Token: 0x0400D036 RID: 53302
				public static LocString LOGIC_CONTROLLED_ACTIVE = "Launch Radbolt: On";

				// Token: 0x0400D037 RID: 53303
				public static LocString NORMAL = "Normal";

				// Token: 0x02003B10 RID: 15120
				public class TOOLTIPS
				{
					// Token: 0x0400EA19 RID: 59929
					public static LocString LOGIC_CONTROLLED_STANDBY = string.Concat(new string[]
					{
						UI.FormatAsKeyWord("Radbolt Generator"),
						" is receiving a ",
						UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby),
						", ignoring incoming ",
						UI.PRE_KEYWORD,
						"Radbolts",
						UI.PST_KEYWORD
					});

					// Token: 0x0400EA1A RID: 59930
					public static LocString LOGIC_CONTROLLED_ACTIVE = string.Concat(new string[]
					{
						UI.FormatAsKeyWord("Radbolt Generator"),
						" is receiving a ",
						UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active),
						", accepting incoming ",
						UI.PRE_KEYWORD,
						"Radbolts",
						UI.PST_KEYWORD
					});

					// Token: 0x0400EA1B RID: 59931
					public static LocString NORMAL = string.Concat(new string[]
					{
						"Incoming ",
						UI.PRE_KEYWORD,
						"Radbolts",
						UI.PST_KEYWORD,
						" will be accepted and redirected"
					});
				}
			}

			// Token: 0x020030A8 RID: 12456
			public class AWAITINGFUEL
			{
				// Token: 0x0400D038 RID: 53304
				public static LocString NAME = "Awaiting Fuel: {0}";

				// Token: 0x0400D039 RID: 53305
				public static LocString TOOLTIP = "This building requires <b>{1}</b> of {0} to operate";
			}

			// Token: 0x020030A9 RID: 12457
			public class FOSSILHUNT
			{
				// Token: 0x02003B11 RID: 15121
				public class PENDING_EXCAVATION
				{
					// Token: 0x0400EA1C RID: 59932
					public static LocString NAME = "Awaiting Excavation";

					// Token: 0x0400EA1D RID: 59933
					public static LocString TOOLTIP = "Currently awaiting excavation by a Duplicant";
				}

				// Token: 0x02003B12 RID: 15122
				public class EXCAVATING
				{
					// Token: 0x0400EA1E RID: 59934
					public static LocString NAME = "Excavation In Progress";

					// Token: 0x0400EA1F RID: 59935
					public static LocString TOOLTIP = "Currently being excavated by a Duplicant";
				}
			}

			// Token: 0x020030AA RID: 12458
			public class MEGABRAINTANK
			{
				// Token: 0x02003B13 RID: 15123
				public class PROGRESS
				{
					// Token: 0x02003F0D RID: 16141
					public class PROGRESSIONRATE
					{
						// Token: 0x0400F389 RID: 62345
						public static LocString NAME = "Dream Journals: {ActivationProgress}";

						// Token: 0x0400F38A RID: 62346
						public static LocString TOOLTIP = "Currently awaiting the Dream Journals necessary to restore this building to full functionality";
					}

					// Token: 0x02003F0E RID: 16142
					public class DREAMANALYSIS
					{
						// Token: 0x0400F38B RID: 62347
						public static LocString NAME = "Analyzing Dreams: {TimeToComplete}s";

						// Token: 0x0400F38C RID: 62348
						public static LocString TOOLTIP = "Maximum Aptitude effect sustained while dream analysis continues";
					}
				}

				// Token: 0x02003B14 RID: 15124
				public class COMPLETE
				{
					// Token: 0x0400EA20 RID: 59936
					public static LocString NAME = "Fully Restored";

					// Token: 0x0400EA21 RID: 59937
					public static LocString TOOLTIP = "This building is functioning at full capacity";
				}
			}

			// Token: 0x020030AB RID: 12459
			public class MEGABRAINNOTENOUGHOXYGEN
			{
				// Token: 0x0400D03A RID: 53306
				public static LocString NAME = "Lacks Oxygen";

				// Token: 0x0400D03B RID: 53307
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building needs ",
					UI.PRE_KEYWORD,
					"Oxygen",
					UI.PST_KEYWORD,
					" in order to function"
				});
			}

			// Token: 0x020030AC RID: 12460
			public class NOLOGICWIRECONNECTED
			{
				// Token: 0x0400D03C RID: 53308
				public static LocString NAME = "No Automation Wire Connected";

				// Token: 0x0400D03D RID: 53309
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building has not been connected to an ",
					UI.PRE_KEYWORD,
					"Automation",
					UI.PST_KEYWORD,
					" grid"
				});
			}

			// Token: 0x020030AD RID: 12461
			public class NOTUBECONNECTED
			{
				// Token: 0x0400D03E RID: 53310
				public static LocString NAME = "No Tube Connected";

				// Token: 0x0400D03F RID: 53311
				public static LocString TOOLTIP = "The first section of tube extending from a " + BUILDINGS.PREFABS.TRAVELTUBEENTRANCE.NAME + " must connect directly upward";
			}

			// Token: 0x020030AE RID: 12462
			public class NOTUBEEXITS
			{
				// Token: 0x0400D040 RID: 53312
				public static LocString NAME = "No Landing Available";

				// Token: 0x0400D041 RID: 53313
				public static LocString TOOLTIP = "Duplicants can only exit a tube when there is somewhere for them to land within <b>two tiles</b>";
			}

			// Token: 0x020030AF RID: 12463
			public class STOREDCHARGE
			{
				// Token: 0x0400D042 RID: 53314
				public static LocString NAME = "Charge Available: {0}/{1}";

				// Token: 0x0400D043 RID: 53315
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building has <b>{0}</b> of stored ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					"\n\nIt consumes ",
					UI.FormatAsNegativeRate("{2}"),
					" per use"
				});
			}

			// Token: 0x020030B0 RID: 12464
			public class NEEDEGG
			{
				// Token: 0x0400D044 RID: 53316
				public static LocString NAME = "No Egg Selected";

				// Token: 0x0400D045 RID: 53317
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Collect ",
					UI.PRE_KEYWORD,
					"Eggs",
					UI.PST_KEYWORD,
					" from ",
					UI.FormatAsLink("Critters", "CREATURES"),
					" to incubate"
				});
			}

			// Token: 0x020030B1 RID: 12465
			public class NOAVAILABLEEGG
			{
				// Token: 0x0400D046 RID: 53318
				public static LocString NAME = "No Egg Available";

				// Token: 0x0400D047 RID: 53319
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"The selected ",
					UI.PRE_KEYWORD,
					"Egg",
					UI.PST_KEYWORD,
					" is not currently available"
				});
			}

			// Token: 0x020030B2 RID: 12466
			public class AWAITINGEGGDELIVERY
			{
				// Token: 0x0400D048 RID: 53320
				public static LocString NAME = "Awaiting Delivery";

				// Token: 0x0400D049 RID: 53321
				public static LocString TOOLTIP = "Awaiting delivery of selected " + UI.PRE_KEYWORD + "Egg" + UI.PST_KEYWORD;
			}

			// Token: 0x020030B3 RID: 12467
			public class INCUBATORPROGRESS
			{
				// Token: 0x0400D04A RID: 53322
				public static LocString NAME = "Incubating: {Percent}";

				// Token: 0x0400D04B RID: 53323
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Egg",
					UI.PST_KEYWORD,
					" incubating cozily\n\nIt will hatch when ",
					UI.PRE_KEYWORD,
					"Incubation",
					UI.PST_KEYWORD,
					" reaches <b>100%</b>"
				});
			}

			// Token: 0x020030B4 RID: 12468
			public class NETWORKQUALITY
			{
				// Token: 0x0400D04C RID: 53324
				public static LocString NAME = "Scan Network Quality: {TotalQuality}";

				// Token: 0x0400D04D RID: 53325
				public static LocString TOOLTIP = "This scanner network is scanning at <b>{TotalQuality}</b> effectiveness\n\nIt will detect incoming objects <b>{WorstTime}</b> to <b>{BestTime}</b> before they arrive\n\nBuild multiple " + BUILDINGS.PREFABS.COMETDETECTOR.NAME + "s to increase surface coverage and improve network quality\n\n    • Surface Coverage: <b>{Coverage}</b>";
			}

			// Token: 0x020030B5 RID: 12469
			public class DETECTORSCANNING
			{
				// Token: 0x0400D04E RID: 53326
				public static LocString NAME = "Scanning";

				// Token: 0x0400D04F RID: 53327
				public static LocString TOOLTIP = "This scanner is currently scouring space for anything of interest";
			}

			// Token: 0x020030B6 RID: 12470
			public class INCOMINGMETEORS
			{
				// Token: 0x0400D050 RID: 53328
				public static LocString NAME = "Incoming Object Detected";

				// Token: 0x0400D051 RID: 53329
				public static LocString TOOLTIP = "Warning!\n\nHigh velocity objects on approach!";
			}

			// Token: 0x020030B7 RID: 12471
			public class SPACE_VISIBILITY_NONE
			{
				// Token: 0x0400D052 RID: 53330
				public static LocString NAME = "No Line of Sight";

				// Token: 0x0400D053 RID: 53331
				public static LocString TOOLTIP = "This building has no view of space\n\nTo properly function, this building requires an unblocked view of space\n    • Efficiency: <b>{VISIBILITY}</b>";
			}

			// Token: 0x020030B8 RID: 12472
			public class SPACE_VISIBILITY_REDUCED
			{
				// Token: 0x0400D054 RID: 53332
				public static LocString NAME = "Reduced Visibility";

				// Token: 0x0400D055 RID: 53333
				public static LocString TOOLTIP = "This building has a partially obstructed view of space\n\nTo operate at maximum speed, this building requires an unblocked view of space\n    • Efficiency: <b>{VISIBILITY}</b>";
			}

			// Token: 0x020030B9 RID: 12473
			public class LANDEDROCKETLACKSPASSENGERMODULE
			{
				// Token: 0x0400D056 RID: 53334
				public static LocString NAME = "Rocket lacks spacefarer module";

				// Token: 0x0400D057 RID: 53335
				public static LocString TOOLTIP = "A rocket must have a spacefarer module";
			}

			// Token: 0x020030BA RID: 12474
			public class PATH_NOT_CLEAR
			{
				// Token: 0x0400D058 RID: 53336
				public static LocString NAME = "Launch Path Blocked";

				// Token: 0x0400D059 RID: 53337
				public static LocString TOOLTIP = "There are obstructions in the launch trajectory of this rocket:\n    • {0}\n\nThis rocket requires a clear flight path for launch";

				// Token: 0x0400D05A RID: 53338
				public static LocString TILE_FORMAT = "Solid {0}";
			}

			// Token: 0x020030BB RID: 12475
			public class RAILGUN_PATH_NOT_CLEAR
			{
				// Token: 0x0400D05B RID: 53339
				public static LocString NAME = "Launch Path Blocked";

				// Token: 0x0400D05C RID: 53340
				public static LocString TOOLTIP = "There are obstructions in the launch trajectory of this " + UI.FormatAsLink("Interplanetary Launcher", "RAILGUN") + "\n\nThis launcher requires a clear path to launch payloads";
			}

			// Token: 0x020030BC RID: 12476
			public class RAILGUN_NO_DESTINATION
			{
				// Token: 0x0400D05D RID: 53341
				public static LocString NAME = "No Delivery Destination";

				// Token: 0x0400D05E RID: 53342
				public static LocString TOOLTIP = "A delivery destination has not been set";
			}

			// Token: 0x020030BD RID: 12477
			public class NOSURFACESIGHT
			{
				// Token: 0x0400D05F RID: 53343
				public static LocString NAME = "No Line of Sight";

				// Token: 0x0400D060 RID: 53344
				public static LocString TOOLTIP = "This building has no view of space\n\nTo properly function, this building requires an unblocked view of space";
			}

			// Token: 0x020030BE RID: 12478
			public class ROCKETRESTRICTIONACTIVE
			{
				// Token: 0x0400D061 RID: 53345
				public static LocString NAME = "Access: Restricted";

				// Token: 0x0400D062 RID: 53346
				public static LocString TOOLTIP = "This building cannot be operated while restricted, though it can be filled\n\nControlled by a " + BUILDINGS.PREFABS.ROCKETCONTROLSTATION.NAME;
			}

			// Token: 0x020030BF RID: 12479
			public class ROCKETRESTRICTIONINACTIVE
			{
				// Token: 0x0400D063 RID: 53347
				public static LocString NAME = "Access: Not Restricted";

				// Token: 0x0400D064 RID: 53348
				public static LocString TOOLTIP = "This building's operation is not restricted\n\nControlled by a " + BUILDINGS.PREFABS.ROCKETCONTROLSTATION.NAME;
			}

			// Token: 0x020030C0 RID: 12480
			public class NOROCKETRESTRICTION
			{
				// Token: 0x0400D065 RID: 53349
				public static LocString NAME = "Not Controlled";

				// Token: 0x0400D066 RID: 53350
				public static LocString TOOLTIP = "This building is not controlled by a " + BUILDINGS.PREFABS.ROCKETCONTROLSTATION.NAME;
			}

			// Token: 0x020030C1 RID: 12481
			public class BROADCASTEROUTOFRANGE
			{
				// Token: 0x0400D067 RID: 53351
				public static LocString NAME = "Broadcaster Out of Range";

				// Token: 0x0400D068 RID: 53352
				public static LocString TOOLTIP = "This receiver is too far from the selected broadcaster to get signal updates";
			}

			// Token: 0x020030C2 RID: 12482
			public class LOSINGRADBOLTS
			{
				// Token: 0x0400D069 RID: 53353
				public static LocString NAME = "Radbolt Decay";

				// Token: 0x0400D06A RID: 53354
				public static LocString TOOLTIP = "This building is unable to maintain the integrity of the radbolts it is storing";
			}

			// Token: 0x020030C3 RID: 12483
			public class TOP_PRIORITY_CHORE
			{
				// Token: 0x0400D06B RID: 53355
				public static LocString NAME = "Top Priority";

				// Token: 0x0400D06C RID: 53356
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This errand has been set to ",
					UI.PRE_KEYWORD,
					"Top Priority",
					UI.PST_KEYWORD,
					"\n\nThe colony will be in ",
					UI.PRE_KEYWORD,
					"Yellow Alert",
					UI.PST_KEYWORD,
					" until this task is completed"
				});

				// Token: 0x0400D06D RID: 53357
				public static LocString NOTIFICATION_NAME = "Yellow Alert";

				// Token: 0x0400D06E RID: 53358
				public static LocString NOTIFICATION_TOOLTIP = string.Concat(new string[]
				{
					"The following errands have been set to ",
					UI.PRE_KEYWORD,
					"Top Priority",
					UI.PST_KEYWORD,
					":"
				});
			}

			// Token: 0x020030C4 RID: 12484
			public class HOTTUBWATERTOOCOLD
			{
				// Token: 0x0400D06F RID: 53359
				public static LocString NAME = "Water Too Cold";

				// Token: 0x0400D070 RID: 53360
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This tub's ",
					UI.PRE_KEYWORD,
					"Water",
					UI.PST_KEYWORD,
					" is below <b>{temperature}</b>\n\nIt is draining so it can be refilled with warmer ",
					UI.PRE_KEYWORD,
					"Water",
					UI.PST_KEYWORD
				});
			}

			// Token: 0x020030C5 RID: 12485
			public class HOTTUBTOOHOT
			{
				// Token: 0x0400D071 RID: 53361
				public static LocString NAME = "Building Too Hot";

				// Token: 0x0400D072 RID: 53362
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This tub's ",
					UI.PRE_KEYWORD,
					"Temperature",
					UI.PST_KEYWORD,
					" is above <b>{temperature}</b>\n\nIt needs to cool before it can safely be used"
				});
			}

			// Token: 0x020030C6 RID: 12486
			public class HOTTUBFILLING
			{
				// Token: 0x0400D073 RID: 53363
				public static LocString NAME = "Filling Up: ({fullness})";

				// Token: 0x0400D074 RID: 53364
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This tub is currently filling with ",
					UI.PRE_KEYWORD,
					"Water",
					UI.PST_KEYWORD,
					"\n\nIt will be available to use when the ",
					UI.PRE_KEYWORD,
					"Water",
					UI.PST_KEYWORD,
					" level reaches <b>100%</b>"
				});
			}

			// Token: 0x020030C7 RID: 12487
			public class WINDTUNNELINTAKE
			{
				// Token: 0x0400D075 RID: 53365
				public static LocString NAME = "Intake Requires Gas";

				// Token: 0x0400D076 RID: 53366
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"A wind tunnel requires ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					" at the top and bottom intakes in order to operate\n\nThe intakes for this wind tunnel don't have enough gas to operate"
				});
			}

			// Token: 0x020030C8 RID: 12488
			public class TEMPORAL_TEAR_OPENER_NO_TARGET
			{
				// Token: 0x0400D077 RID: 53367
				public static LocString NAME = "Temporal Tear not revealed";

				// Token: 0x0400D078 RID: 53368
				public static LocString TOOLTIP = "This machine is meant to target something in space, but the target has not yet been revealed";
			}

			// Token: 0x020030C9 RID: 12489
			public class TEMPORAL_TEAR_OPENER_NO_LOS
			{
				// Token: 0x0400D079 RID: 53369
				public static LocString NAME = "Line of Sight: Obstructed";

				// Token: 0x0400D07A RID: 53370
				public static LocString TOOLTIP = "This device needs a clear view of space to operate";
			}

			// Token: 0x020030CA RID: 12490
			public class TEMPORAL_TEAR_OPENER_INSUFFICIENT_COLONIES
			{
				// Token: 0x0400D07B RID: 53371
				public static LocString NAME = "Too few Printing Pods {progress}";

				// Token: 0x0400D07C RID: 53372
				public static LocString TOOLTIP = "To open the Temporal Tear, this device relies on a network of activated Printing Pods {progress}";
			}

			// Token: 0x020030CB RID: 12491
			public class TEMPORAL_TEAR_OPENER_PROGRESS
			{
				// Token: 0x0400D07D RID: 53373
				public static LocString NAME = "Charging Progress: {progress}";

				// Token: 0x0400D07E RID: 53374
				public static LocString TOOLTIP = "This device must be charged with a high number of Radbolts\n\nOperation can commence once this device is fully charged";
			}

			// Token: 0x020030CC RID: 12492
			public class TEMPORAL_TEAR_OPENER_READY
			{
				// Token: 0x0400D07F RID: 53375
				public static LocString NOTIFICATION = "Temporal Tear Opener fully charged";

				// Token: 0x0400D080 RID: 53376
				public static LocString NOTIFICATION_TOOLTIP = "Push the red button to activate";
			}

			// Token: 0x020030CD RID: 12493
			public class WARPPORTALCHARGING
			{
				// Token: 0x0400D081 RID: 53377
				public static LocString NAME = "Recharging: {charge}";

				// Token: 0x0400D082 RID: 53378
				public static LocString TOOLTIP = "This teleporter will be ready for use in {cycles} cycles";
			}

			// Token: 0x020030CE RID: 12494
			public class WARPCONDUITPARTNERDISABLED
			{
				// Token: 0x0400D083 RID: 53379
				public static LocString NAME = "Teleporter Disabled ({x}/2)";

				// Token: 0x0400D084 RID: 53380
				public static LocString TOOLTIP = "This teleporter cannot be used until both the transmitting and receiving sides have been activated";
			}

			// Token: 0x020030CF RID: 12495
			public class COLLECTINGHEP
			{
				// Token: 0x0400D085 RID: 53381
				public static LocString NAME = "Collecting Radbolts ({x}/cycle)";

				// Token: 0x0400D086 RID: 53382
				public static LocString TOOLTIP = "Collecting Radbolts from ambient radiation";
			}

			// Token: 0x020030D0 RID: 12496
			public class INORBIT
			{
				// Token: 0x0400D087 RID: 53383
				public static LocString NAME = "In Orbit: {Destination}";

				// Token: 0x0400D088 RID: 53384
				public static LocString TOOLTIP = "This rocket is currently in orbit around {Destination}";
			}

			// Token: 0x020030D1 RID: 12497
			public class WAITINGTOLAND
			{
				// Token: 0x0400D089 RID: 53385
				public static LocString NAME = "Waiting to land on {Destination}";

				// Token: 0x0400D08A RID: 53386
				public static LocString TOOLTIP = "This rocket is waiting for an available Rcoket Platform on {Destination}";
			}

			// Token: 0x020030D2 RID: 12498
			public class INFLIGHT
			{
				// Token: 0x0400D08B RID: 53387
				public static LocString NAME = "In Flight To {Destination_Asteroid}: {ETA}";

				// Token: 0x0400D08C RID: 53388
				public static LocString TOOLTIP = "This rocket is currently traveling to {Destination_Pad} on {Destination_Asteroid}\n\nIt will arrive in {ETA}";

				// Token: 0x0400D08D RID: 53389
				public static LocString TOOLTIP_NO_PAD = "This rocket is currently traveling to {Destination_Asteroid}\n\nIt will arrive in {ETA}";
			}

			// Token: 0x020030D3 RID: 12499
			public class DESTINATIONOUTOFRANGE
			{
				// Token: 0x0400D08E RID: 53390
				public static LocString NAME = "Destination Out Of Range";

				// Token: 0x0400D08F RID: 53391
				public static LocString TOOLTIP = "This rocket lacks the range to reach its destination\n\nRocket Range: {Range}\nDestination Distance: {Distance}";
			}

			// Token: 0x020030D4 RID: 12500
			public class ROCKETSTRANDED
			{
				// Token: 0x0400D090 RID: 53392
				public static LocString NAME = "Stranded";

				// Token: 0x0400D091 RID: 53393
				public static LocString TOOLTIP = "This rocket has run out of fuel and cannot move";
			}

			// Token: 0x020030D5 RID: 12501
			public class SPACEPOIHARVESTING
			{
				// Token: 0x0400D092 RID: 53394
				public static LocString NAME = "Extracting Resources: {0}";

				// Token: 0x0400D093 RID: 53395
				public static LocString TOOLTIP = "Resources are being mined from this space debris";
			}

			// Token: 0x020030D6 RID: 12502
			public class SPACEPOIWASTING
			{
				// Token: 0x0400D094 RID: 53396
				public static LocString NAME = "Cannot store resources: {0}";

				// Token: 0x0400D095 RID: 53397
				public static LocString TOOLTIP = "Some resources being mined from this space debris cannot be stored in this rocket";
			}

			// Token: 0x020030D7 RID: 12503
			public class RAILGUNPAYLOADNEEDSEMPTYING
			{
				// Token: 0x0400D096 RID: 53398
				public static LocString NAME = "Ready To Unpack";

				// Token: 0x0400D097 RID: 53399
				public static LocString TOOLTIP = "This payload has reached its destination and is ready to be unloaded\n\nIt can be marked for unpacking manually, or automatically unpacked on arrival using a " + BUILDINGS.PREFABS.RAILGUNPAYLOADOPENER.NAME;
			}

			// Token: 0x020030D8 RID: 12504
			public class MISSIONCONTROLASSISTINGROCKET
			{
				// Token: 0x0400D098 RID: 53400
				public static LocString NAME = "Guidance Signal: {0}";

				// Token: 0x0400D099 RID: 53401
				public static LocString TOOLTIP = "Once transmission is complete, Mission Control will boost targeted rocket's speed";
			}

			// Token: 0x020030D9 RID: 12505
			public class MISSIONCONTROLBOOSTED
			{
				// Token: 0x0400D09A RID: 53402
				public static LocString NAME = "Mission Control Speed Boost: {0}";

				// Token: 0x0400D09B RID: 53403
				public static LocString TOOLTIP = "Mission Control has given this rocket a {0} speed boost\n\n{1} remaining";
			}

			// Token: 0x020030DA RID: 12506
			public class TRANSITTUBEENTRANCEWAXREADY
			{
				// Token: 0x0400D09C RID: 53404
				public static LocString NAME = "Smooth Ride Ready";

				// Token: 0x0400D09D RID: 53405
				public static LocString TOOLTIP = "This building is stocked with speed-boosting " + ELEMENTS.MILKFAT.NAME + "\n\n{0} per use ({1} remaining)";
			}

			// Token: 0x020030DB RID: 12507
			public class NOROCKETSTOMISSIONCONTROLBOOST
			{
				// Token: 0x0400D09E RID: 53406
				public static LocString NAME = "No Eligible Rockets in Range";

				// Token: 0x0400D09F RID: 53407
				public static LocString TOOLTIP = "Rockets must be mid-flight and not targeted by another Mission Control Station, or already boosted";
			}

			// Token: 0x020030DC RID: 12508
			public class NOROCKETSTOMISSIONCONTROLCLUSTERBOOST
			{
				// Token: 0x0400D0A0 RID: 53408
				public static LocString NAME = "No Eligible Rockets in Range";

				// Token: 0x0400D0A1 RID: 53409
				public static LocString TOOLTIP = "Rockets must be mid-flight, within {0} tiles, and not targeted by another Mission Control Station or already boosted";
			}

			// Token: 0x020030DD RID: 12509
			public class AWAITINGEMPTYBUILDING
			{
				// Token: 0x0400D0A2 RID: 53410
				public static LocString NAME = "Empty Errand";

				// Token: 0x0400D0A3 RID: 53411
				public static LocString TOOLTIP = "Building will be emptied once a Duplicant is available";
			}

			// Token: 0x020030DE RID: 12510
			public class DUPLICANTACTIVATIONREQUIRED
			{
				// Token: 0x0400D0A4 RID: 53412
				public static LocString NAME = "Activation Required";

				// Token: 0x0400D0A5 RID: 53413
				public static LocString TOOLTIP = "A Duplicant is required to bring this building online";
			}

			// Token: 0x020030DF RID: 12511
			public class PILOTNEEDED
			{
				// Token: 0x0400D0A6 RID: 53414
				public static LocString NAME = "Switching to Autopilot";

				// Token: 0x0400D0A7 RID: 53415
				public static LocString TOOLTIP = "Autopilot will engage in {timeRemaining} if a Duplicant pilot does not assume control";
			}

			// Token: 0x020030E0 RID: 12512
			public class AUTOPILOTACTIVE
			{
				// Token: 0x0400D0A8 RID: 53416
				public static LocString NAME = "Autopilot Engaged";

				// Token: 0x0400D0A9 RID: 53417
				public static LocString TOOLTIP = "This rocket has entered autopilot mode and will fly at reduced speed\n\nIt can resume full speed once a Duplicant pilot takes over";
			}

			// Token: 0x020030E1 RID: 12513
			public class INFLIGHTPILOTED
			{
				// Token: 0x0400D0AA RID: 53418
				public static LocString NAME = "Piloted";

				// Token: 0x0400D0AB RID: 53419
				public static LocString DUPE_TOOLTIP = "Duplicant pilot's <b>Skill</b>: +{0} speed boost";

				// Token: 0x0400D0AC RID: 53420
				public static LocString ROBO_TOOLTIP = "Piloted by a " + UI.PRE_KEYWORD + "Robo-Pilot" + UI.PST_KEYWORD;
			}

			// Token: 0x020030E2 RID: 12514
			public class INFLIGHTUNPILOTED
			{
				// Token: 0x0400D0AD RID: 53421
				public static LocString NAME = "Unpiloted";

				// Token: 0x0400D0AE RID: 53422
				public static LocString TOOLTIP = "Inactive rocket module: -{penalty} speed {modules}";

				// Token: 0x0400D0AF RID: 53423
				public static LocString ROBO_PILOT_ONLY_TOOLTIP = string.Concat(new string[]
				{
					UI.PRE_KEYWORD,
					"Robo-Pilot",
					UI.PST_KEYWORD,
					" has run out of ",
					UI.PRE_KEYWORD,
					"Data Banks",
					UI.PST_KEYWORD,
					"\n\nThis rocket is stranded"
				});
			}

			// Token: 0x020030E3 RID: 12515
			public class INFLIGHTAUTOPILOTED
			{
				// Token: 0x0400D0B0 RID: 53424
				public static LocString NAME = "Autopilot Engaged";

				// Token: 0x0400D0B1 RID: 53425
				public static LocString TOOLTIP = "This rocket's {modules} is inactive\n\nThis rocket has entered autopilot mode and will fly at reduced speed\n    •  -{penalty} speed";
			}

			// Token: 0x020030E4 RID: 12516
			public class INFLIGHTSUPERPILOT
			{
				// Token: 0x0400D0B2 RID: 53426
				public static LocString NAME = "Multi-Piloted";

				// Token: 0x0400D0B3 RID: 53427
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This rocket is being piloted by a Duplicant and a ",
					UI.PRE_KEYWORD,
					"Robo-Pilot",
					UI.PST_KEYWORD,
					"\n    • Multi-Piloted: +{1} speed boost\n    • Duplicant pilot skill: +{0} speed boost"
				});
			}

			// Token: 0x020030E5 RID: 12517
			public class ROCKETCHECKLISTINCOMPLETE
			{
				// Token: 0x0400D0B4 RID: 53428
				public static LocString NAME = "Launch Checklist Incomplete";

				// Token: 0x0400D0B5 RID: 53429
				public static LocString TOOLTIP = "Critical launch tasks uncompleted\n\nRefer to the Launch Checklist in the status panel";
			}

			// Token: 0x020030E6 RID: 12518
			public class ROCKETCARGOEMPTYING
			{
				// Token: 0x0400D0B6 RID: 53430
				public static LocString NAME = "Unloading Cargo";

				// Token: 0x0400D0B7 RID: 53431
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Rocket cargo is being unloaded into the ",
					UI.PRE_KEYWORD,
					"Rocket Platform",
					UI.PST_KEYWORD,
					"\n\nLoading of new cargo will begin once unloading is complete"
				});
			}

			// Token: 0x020030E7 RID: 12519
			public class ROCKETCARGOFILLING
			{
				// Token: 0x0400D0B8 RID: 53432
				public static LocString NAME = "Loading Cargo";

				// Token: 0x0400D0B9 RID: 53433
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Cargo is being loaded onto the rocket from the ",
					UI.PRE_KEYWORD,
					"Rocket Platform",
					UI.PST_KEYWORD,
					"\n\nRocket cargo will be ready for launch once loading is complete"
				});
			}

			// Token: 0x020030E8 RID: 12520
			public class ROCKETCARGOFULL
			{
				// Token: 0x0400D0BA RID: 53434
				public static LocString NAME = "Platform Ready";

				// Token: 0x0400D0BB RID: 53435
				public static LocString TOOLTIP = "All cargo operations are complete";
			}

			// Token: 0x020030E9 RID: 12521
			public class FLIGHTALLCARGOFULL
			{
				// Token: 0x0400D0BC RID: 53436
				public static LocString NAME = "All cargo bays are full";

				// Token: 0x0400D0BD RID: 53437
				public static LocString TOOLTIP = "Rocket cannot store any more materials";
			}

			// Token: 0x020030EA RID: 12522
			public class FLIGHTCARGOREMAINING
			{
				// Token: 0x0400D0BE RID: 53438
				public static LocString NAME = "Cargo capacity remaining: {0}";

				// Token: 0x0400D0BF RID: 53439
				public static LocString TOOLTIP = "Rocket can store up to {0} more materials";
			}

			// Token: 0x020030EB RID: 12523
			public class ROCKET_PORT_IDLE
			{
				// Token: 0x0400D0C0 RID: 53440
				public static LocString NAME = "Idle";

				// Token: 0x0400D0C1 RID: 53441
				public static LocString TOOLTIP = "This port is idle because there is no rocket on the connected " + UI.PRE_KEYWORD + "Rocket Platform" + UI.PST_KEYWORD;
			}

			// Token: 0x020030EC RID: 12524
			public class ROCKET_PORT_UNLOADING
			{
				// Token: 0x0400D0C2 RID: 53442
				public static LocString NAME = "Unloading Rocket";

				// Token: 0x0400D0C3 RID: 53443
				public static LocString TOOLTIP = "Resources are being unloaded from the rocket into the local network";
			}

			// Token: 0x020030ED RID: 12525
			public class ROCKET_PORT_LOADING
			{
				// Token: 0x0400D0C4 RID: 53444
				public static LocString NAME = "Loading Rocket";

				// Token: 0x0400D0C5 RID: 53445
				public static LocString TOOLTIP = "Resources are being loaded from the local network into the rocket's storage";
			}

			// Token: 0x020030EE RID: 12526
			public class ROCKET_PORT_LOADED
			{
				// Token: 0x0400D0C6 RID: 53446
				public static LocString NAME = "Cargo Transfer Complete";

				// Token: 0x0400D0C7 RID: 53447
				public static LocString TOOLTIP = "The connected rocket has either reached max capacity for this resource type, or lacks appropriate storage modules";
			}

			// Token: 0x020030EF RID: 12527
			public class CONNECTED_ROCKET_PORT
			{
				// Token: 0x0400D0C8 RID: 53448
				public static LocString NAME = "Port Network Attached";

				// Token: 0x0400D0C9 RID: 53449
				public static LocString TOOLTIP = "This module has been connected to a " + BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME + " and can now load and unload cargo";
			}

			// Token: 0x020030F0 RID: 12528
			public class CONNECTED_ROCKET_WRONG_PORT
			{
				// Token: 0x0400D0CA RID: 53450
				public static LocString NAME = "Incorrect Port Network";

				// Token: 0x0400D0CB RID: 53451
				public static LocString TOOLTIP = "The attached " + BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME + " is not the correct type for this cargo module";
			}

			// Token: 0x020030F1 RID: 12529
			public class CONNECTED_ROCKET_NO_PORT
			{
				// Token: 0x0400D0CC RID: 53452
				public static LocString NAME = "No Rocket Ports";

				// Token: 0x0400D0CD RID: 53453
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Rocket Platform",
					UI.PST_KEYWORD,
					" has no ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME,
					" attached\n\n",
					UI.PRE_KEYWORD,
					"Solid",
					UI.PST_KEYWORD,
					", ",
					UI.PRE_KEYWORD,
					"Gas",
					UI.PST_KEYWORD,
					", and ",
					UI.PRE_KEYWORD,
					"Liquid",
					UI.PST_KEYWORD,
					" ",
					BUILDINGS.PREFABS.MODULARLAUNCHPADPORT.NAME_PLURAL,
					" can be attached to load and unload cargo from a landed rocket's modules"
				});
			}

			// Token: 0x020030F2 RID: 12530
			public class CLUSTERTELESCOPEALLWORKCOMPLETE
			{
				// Token: 0x0400D0CE RID: 53454
				public static LocString NAME = "Area Complete";

				// Token: 0x0400D0CF RID: 53455
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This ",
					UI.PRE_KEYWORD,
					"Telescope",
					UI.PST_KEYWORD,
					" has analyzed all the space visible from its current location"
				});
			}

			// Token: 0x020030F3 RID: 12531
			public class ROCKETPLATFORMCLOSETOCEILING
			{
				// Token: 0x0400D0D0 RID: 53456
				public static LocString NAME = "Low Clearance: {distance} Tiles";

				// Token: 0x0400D0D1 RID: 53457
				public static LocString TOOLTIP = "Tall rockets may not be able to land on this " + UI.PRE_KEYWORD + "Rocket Platform" + UI.PST_KEYWORD;
			}

			// Token: 0x020030F4 RID: 12532
			public class MODULEGENERATORNOTPOWERED
			{
				// Token: 0x0400D0D2 RID: 53458
				public static LocString NAME = "Thrust Generation: {ActiveWattage}/{MaxWattage}";

				// Token: 0x0400D0D3 RID: 53459
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Engine will generate ",
					UI.FormatAsPositiveRate("{MaxWattage}"),
					" of ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" once traveling through space\n\nRight now, it's not doing much of anything"
				});
			}

			// Token: 0x020030F5 RID: 12533
			public class MODULEGENERATORPOWERED
			{
				// Token: 0x0400D0D4 RID: 53460
				public static LocString NAME = "Thrust Generation: {ActiveWattage}/{MaxWattage}";

				// Token: 0x0400D0D5 RID: 53461
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"Engine is extracting ",
					UI.FormatAsPositiveRate("{MaxWattage}"),
					" of ",
					UI.PRE_KEYWORD,
					"Power",
					UI.PST_KEYWORD,
					" from the thruster\n\nIt will continue generating power as long as it travels through space"
				});
			}

			// Token: 0x020030F6 RID: 12534
			public class INORBITREQUIRED
			{
				// Token: 0x0400D0D6 RID: 53462
				public static LocString NAME = "Grounded";

				// Token: 0x0400D0D7 RID: 53463
				public static LocString TOOLTIP = "This building cannot operate from the surface of a " + UI.CLUSTERMAP.PLANETOID_KEYWORD + " and must be in space to function";
			}

			// Token: 0x020030F7 RID: 12535
			public class REACTORREFUELDISABLED
			{
				// Token: 0x0400D0D8 RID: 53464
				public static LocString NAME = "Refuel Disabled";

				// Token: 0x0400D0D9 RID: 53465
				public static LocString TOOLTIP = "This building will not be refueled once its active fuel has been consumed";
			}

			// Token: 0x020030F8 RID: 12536
			public class RAILGUNCOOLDOWN
			{
				// Token: 0x0400D0DA RID: 53466
				public static LocString NAME = "Cleaning Rails: {timeleft}";

				// Token: 0x0400D0DB RID: 53467
				public static LocString TOOLTIP = "This building automatically performs routine maintenance every {x} launches";
			}

			// Token: 0x020030F9 RID: 12537
			public class FRIDGECOOLING
			{
				// Token: 0x0400D0DC RID: 53468
				public static LocString NAME = "Cooling Contents: {UsedPower}";

				// Token: 0x0400D0DD RID: 53469
				public static LocString TOOLTIP = "{UsedPower} of {MaxPower} are being used to cool the contents of this food storage";
			}

			// Token: 0x020030FA RID: 12538
			public class FRIDGESTEADY
			{
				// Token: 0x0400D0DE RID: 53470
				public static LocString NAME = "Energy Saver: {UsedPower}";

				// Token: 0x0400D0DF RID: 53471
				public static LocString TOOLTIP = "The contents of this food storage are at refrigeration temperatures\n\nEnergy Saver mode has been automatically activated using only {UsedPower} of {MaxPower}";
			}

			// Token: 0x020030FB RID: 12539
			public class TELEPHONE
			{
				// Token: 0x02003B15 RID: 15125
				public class BABBLE
				{
					// Token: 0x0400EA22 RID: 59938
					public static LocString NAME = "Babbling to no one.";

					// Token: 0x0400EA23 RID: 59939
					public static LocString TOOLTIP = "{Duplicant} just needed to vent to into the void.";
				}

				// Token: 0x02003B16 RID: 15126
				public class CONVERSATION
				{
					// Token: 0x0400EA24 RID: 59940
					public static LocString TALKING_TO = "Talking to {Duplicant} on {Asteroid}";

					// Token: 0x0400EA25 RID: 59941
					public static LocString TALKING_TO_NUM = "Talking to {0} friends.";
				}
			}

			// Token: 0x020030FC RID: 12540
			public class CREATUREMANIPULATORPROGRESS
			{
				// Token: 0x0400D0E0 RID: 53472
				public static LocString NAME = "Collected Species Data {0}/{1}";

				// Token: 0x0400D0E1 RID: 53473
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building requires data from multiple ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					" species to unlock its genetic manipulator\n\nSpecies scanned:"
				});

				// Token: 0x0400D0E2 RID: 53474
				public static LocString NO_DATA = "No species scanned";
			}

			// Token: 0x020030FD RID: 12541
			public class CREATUREMANIPULATORMORPHMODELOCKED
			{
				// Token: 0x0400D0E3 RID: 53475
				public static LocString NAME = "Current Status: Offline";

				// Token: 0x0400D0E4 RID: 53476
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building cannot operate until it collects more ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					" DNA"
				});
			}

			// Token: 0x020030FE RID: 12542
			public class CREATUREMANIPULATORMORPHMODE
			{
				// Token: 0x0400D0E5 RID: 53477
				public static LocString NAME = "Current Status: Online";

				// Token: 0x0400D0E6 RID: 53478
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building is ready to manipulate ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					" DNA"
				});
			}

			// Token: 0x020030FF RID: 12543
			public class CREATUREMANIPULATORWAITING
			{
				// Token: 0x0400D0E7 RID: 53479
				public static LocString NAME = "Waiting for a Critter";

				// Token: 0x0400D0E8 RID: 53480
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building is waiting for a ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					" to get sucked into its scanning area"
				});
			}

			// Token: 0x02003100 RID: 12544
			public class CREATUREMANIPULATORWORKING
			{
				// Token: 0x0400D0E9 RID: 53481
				public static LocString NAME = "Poking and Prodding Critter";

				// Token: 0x0400D0EA RID: 53482
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building is extracting genetic information from a ",
					UI.PRE_KEYWORD,
					"Critter",
					UI.PST_KEYWORD,
					" "
				});
			}

			// Token: 0x02003101 RID: 12545
			public class SPICEGRINDERNOSPICE
			{
				// Token: 0x0400D0EB RID: 53483
				public static LocString NAME = "No Spice Selected";

				// Token: 0x0400D0EC RID: 53484
				public static LocString TOOLTIP = "Select a recipe to begin fabrication";
			}

			// Token: 0x02003102 RID: 12546
			public class SPICEGRINDERACCEPTSMUTANTSEEDS
			{
				// Token: 0x0400D0ED RID: 53485
				public static LocString NAME = "Spice Grinder accepts mutant seeds";

				// Token: 0x0400D0EE RID: 53486
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This spice grinder is allowed to use ",
					UI.PRE_KEYWORD,
					"Mutant Seeds",
					UI.PST_KEYWORD,
					" as recipe ingredients"
				});
			}

			// Token: 0x02003103 RID: 12547
			public class MISSILELAUNCHER_NOSURFACESIGHT
			{
				// Token: 0x0400D0EF RID: 53487
				public static LocString NAME = "No Line of Sight";

				// Token: 0x0400D0F0 RID: 53488
				public static LocString TOOLTIP = "This building has no view of space\n\nTo properly function, this building requires an unblocked view of space";
			}

			// Token: 0x02003104 RID: 12548
			public class MISSILELAUNCHER_PARTIALLYBLOCKED
			{
				// Token: 0x0400D0F1 RID: 53489
				public static LocString NAME = "Limited Line of Sight";

				// Token: 0x0400D0F2 RID: 53490
				public static LocString TOOLTIP = "This building has a partially obstructed view of space\n\nTo properly function, this building requires an unblocked view of space";
			}

			// Token: 0x02003105 RID: 12549
			public class MISSILELAUNCHER_LONGRANGECOOLDOWN
			{
				// Token: 0x0400D0F3 RID: 53491
				public static LocString NAME = "Reloading";

				// Token: 0x0400D0F4 RID: 53492
				public static LocString TOOLTIP = string.Concat(new string[]
				{
					"This building recently fired an ",
					UI.PRE_KEYWORD,
					"Intracosmic Blastshot",
					UI.PST_KEYWORD,
					" and needs a moment to reload"
				});
			}

			// Token: 0x02003106 RID: 12550
			public class COMPLEXFABRICATOR
			{
				// Token: 0x02003B17 RID: 15127
				public class COOKING
				{
					// Token: 0x0400EA26 RID: 59942
					public static LocString NAME = "Cooking {Item}";

					// Token: 0x0400EA27 RID: 59943
					public static LocString TOOLTIP = "This building is currently whipping up a batch of {Item}";
				}

				// Token: 0x02003B18 RID: 15128
				public class PRODUCING
				{
					// Token: 0x0400EA28 RID: 59944
					public static LocString NAME = "Producing {Item}";

					// Token: 0x0400EA29 RID: 59945
					public static LocString TOOLTIP = "This building is carrying out its current production orders";
				}

				// Token: 0x02003B19 RID: 15129
				public class RESEARCHING
				{
					// Token: 0x0400EA2A RID: 59946
					public static LocString NAME = "Researching {Item}";

					// Token: 0x0400EA2B RID: 59947
					public static LocString TOOLTIP = "This building is currently conducting important research";
				}

				// Token: 0x02003B1A RID: 15130
				public class ANALYZING
				{
					// Token: 0x0400EA2C RID: 59948
					public static LocString NAME = "Analyzing {Item}";

					// Token: 0x0400EA2D RID: 59949
					public static LocString TOOLTIP = "This building is currently analyzing a fascinating artifact";
				}

				// Token: 0x02003B1B RID: 15131
				public class UNTRAINING
				{
					// Token: 0x0400EA2E RID: 59950
					public static LocString NAME = "Untraining {Duplicant}";

					// Token: 0x0400EA2F RID: 59951
					public static LocString TOOLTIP = "Restoring {Duplicant} to a blissfully ignorant state";
				}

				// Token: 0x02003B1C RID: 15132
				public class TELESCOPE
				{
					// Token: 0x0400EA30 RID: 59952
					public static LocString NAME = "Studying Space";

					// Token: 0x0400EA31 RID: 59953
					public static LocString TOOLTIP = "This building is currently investigating the mysteries of space";
				}

				// Token: 0x02003B1D RID: 15133
				public class CLUSTERTELESCOPEMETEOR
				{
					// Token: 0x0400EA32 RID: 59954
					public static LocString NAME = "Studying Meteor";

					// Token: 0x0400EA33 RID: 59955
					public static LocString TOOLTIP = "This building is currently studying a meteor";
				}
			}

			// Token: 0x02003107 RID: 12551
			public class REMOTEWORKERDEPOT
			{
				// Token: 0x02003B1E RID: 15134
				public class MAKINGWORKER
				{
					// Token: 0x0400EA34 RID: 59956
					public static LocString NAME = "Assembling Remote Worker";

					// Token: 0x0400EA35 RID: 59957
					public static LocString TOOLTIP = "This building is currently assembling a remote worker drone";
				}
			}

			// Token: 0x02003108 RID: 12552
			public class REMOTEWORKTERMINAL
			{
				// Token: 0x02003B1F RID: 15135
				public class NODOCK
				{
					// Token: 0x0400EA36 RID: 59958
					public static LocString NAME = "No Dock Assigned";

					// Token: 0x0400EA37 RID: 59959
					public static LocString TOOLTIP = string.Concat(new string[]
					{
						"This building must be assigned a ",
						UI.PRE_KEYWORD,
						"Remote Worker Dock",
						UI.PST_KEYWORD,
						" in order to function"
					});
				}
			}

			// Token: 0x02003109 RID: 12553
			public class DATAMINER
			{
				// Token: 0x02003B20 RID: 15136
				public class PRODUCTIONRATE
				{
					// Token: 0x0400EA38 RID: 59960
					public static LocString NAME = "Production Rate: {RATE}";

					// Token: 0x0400EA39 RID: 59961
					public static LocString TOOLTIP = "This building is operating at {RATE} of its maximum speed\n\nProduction rate decreases at higher temperatures\n\nCurrent ambient temperature: {TEMP}";
				}
			}
		}

		// Token: 0x02002400 RID: 9216
		public class DETAILS
		{
			// Token: 0x0400A30D RID: 41741
			public static LocString USE_COUNT = "Uses: {0}";

			// Token: 0x0400A30E RID: 41742
			public static LocString USE_COUNT_TOOLTIP = "This building has been used {0} times";
		}
	}
}
