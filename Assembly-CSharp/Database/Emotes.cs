using System;
using Klei.AI;

namespace Database
{
	// Token: 0x02000EEE RID: 3822
	public class Emotes : ResourceSet<Resource>
	{
		// Token: 0x0600797E RID: 31102 RVA: 0x002FB9DB File Offset: 0x002F9BDB
		public Emotes(ResourceSet parent)
			: base("Emotes", parent)
		{
			this.Minion = new Emotes.MinionEmotes(this);
			this.Critter = new Emotes.CritterEmotes(this);
		}

		// Token: 0x0600797F RID: 31103 RVA: 0x002FBA04 File Offset: 0x002F9C04
		public void ResetProblematicReferences()
		{
			for (int i = 0; i < this.Minion.resources.Count; i++)
			{
				Emote emote = this.Minion.resources[i];
				for (int j = 0; j < emote.StepCount; j++)
				{
					emote[j].UnregisterAllCallbacks();
				}
			}
			for (int k = 0; k < this.Critter.resources.Count; k++)
			{
				Emote emote2 = this.Critter.resources[k];
				for (int l = 0; l < emote2.StepCount; l++)
				{
					emote2[l].UnregisterAllCallbacks();
				}
			}
		}

		// Token: 0x04005781 RID: 22401
		public Emotes.MinionEmotes Minion;

		// Token: 0x04005782 RID: 22402
		public Emotes.CritterEmotes Critter;

		// Token: 0x020020E3 RID: 8419
		public class MinionEmotes : ResourceSet<Emote>
		{
			// Token: 0x0600B85D RID: 47197 RVA: 0x003E935D File Offset: 0x003E755D
			public MinionEmotes(ResourceSet parent)
				: base("Minion", parent)
			{
				this.InitializeCelebrations();
				this.InitializePhysicalStatus();
				this.InitializeEmotionalStatus();
				this.InitializeGreetings();
			}

			// Token: 0x0600B85E RID: 47198 RVA: 0x003E9384 File Offset: 0x003E7584
			public void InitializeCelebrations()
			{
				this.ClapCheer = new Emote(this, "ClapCheer", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "clapcheer_pre"
					},
					new EmoteStep
					{
						anim = "clapcheer_loop"
					},
					new EmoteStep
					{
						anim = "clapcheer_pst"
					}
				}, "anim_clapcheer_kanim");
				this.Cheer = new Emote(this, "Cheer", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "cheer_pre"
					},
					new EmoteStep
					{
						anim = "cheer_loop"
					},
					new EmoteStep
					{
						anim = "cheer_pst"
					}
				}, "anim_cheer_kanim");
				this.ProductiveCheer = new Emote(this, "Productive Cheer", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "productive"
					}
				}, "anim_productive_kanim");
				this.ResearchComplete = new Emote(this, "ResearchComplete", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_research_complete_kanim");
				this.ThumbsUp = new Emote(this, "ThumbsUp", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_thumbsup_kanim");
			}

			// Token: 0x0600B85F RID: 47199 RVA: 0x003E94C4 File Offset: 0x003E76C4
			private void InitializePhysicalStatus()
			{
				this.CloseCall_Fall = new Emote(this, "Near Fall", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_floor_missing_kanim");
				this.Cold = new Emote(this, "Cold", Emotes.MinionEmotes.DEFAULT_IDLE_STEPS, "anim_idle_cold_kanim");
				this.Cough = new Emote(this, "Cough", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_slimelungcough_kanim");
				this.Cough_Small = new Emote(this, "Small Cough", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react_small"
					}
				}, "anim_slimelungcough_kanim");
				this.FoodPoisoning = new Emote(this, "Food Poisoning", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_contaminated_food_kanim");
				this.Hot = new Emote(this, "Hot", Emotes.MinionEmotes.DEFAULT_IDLE_STEPS, "anim_idle_hot_kanim");
				this.IritatedEyes = new Emote(this, "Irritated Eyes", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "irritated_eyes"
					}
				}, "anim_irritated_eyes_kanim");
				this.MorningStretch = new Emote(this, "Morning Stretch", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_morning_stretch_kanim");
				this.Radiation_Glare = new Emote(this, "Radiation Glare", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react_radiation_glare"
					}
				}, "anim_react_radiation_kanim");
				this.Radiation_Itch = new Emote(this, "Radiation Itch", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react_radiation_itch"
					}
				}, "anim_react_radiation_kanim");
				this.Sick = new Emote(this, "Sick", Emotes.MinionEmotes.DEFAULT_IDLE_STEPS, "anim_idle_sick_kanim");
				this.Sneeze = new Emote(this, "Sneeze", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "sneeze"
					},
					new EmoteStep
					{
						anim = "sneeze_pst"
					}
				}, "anim_sneeze_kanim");
				this.WaterDamage = new Emote(this, "WaterDamage", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "zapped"
					}
				}, "anim_bionic_kanim");
				this.GrindingGears = new Emote(this, "GrindingGears", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react"
					}
				}, "anim_bionic_react_grinding_gears_kanim");
				this.Sneeze_Short = new Emote(this, "Short Sneeze", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "sneeze_short"
					},
					new EmoteStep
					{
						anim = "sneeze_short_pst"
					}
				}, "anim_sneeze_kanim");
			}

			// Token: 0x0600B860 RID: 47200 RVA: 0x003E9760 File Offset: 0x003E7960
			private void InitializeEmotionalStatus()
			{
				this.Concern = new Emote(this, "Concern", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_concern_kanim");
				this.Cringe = new Emote(this, "Cringe", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "cringe_pre"
					},
					new EmoteStep
					{
						anim = "cringe_loop"
					},
					new EmoteStep
					{
						anim = "cringe_pst"
					}
				}, "anim_cringe_kanim");
				this.Disappointed = new Emote(this, "Disappointed", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_disappointed_kanim");
				this.Shock = new Emote(this, "Shock", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_shock_kanim");
				this.Sing = new Emote(this, "Sing", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_singer_kanim");
			}

			// Token: 0x0600B861 RID: 47201 RVA: 0x003E9840 File Offset: 0x003E7A40
			private void InitializeGreetings()
			{
				this.FingerGuns = new Emote(this, "Finger Guns", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_fingerguns_kanim");
				this.Wave = new Emote(this, "Wave", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_wave_kanim");
				this.Wave_Shy = new Emote(this, "Shy Wave", Emotes.MinionEmotes.DEFAULT_STEPS, "anim_react_wave_shy_kanim");
			}

			// Token: 0x04009666 RID: 38502
			private static EmoteStep[] DEFAULT_STEPS = new EmoteStep[]
			{
				new EmoteStep
				{
					anim = "react"
				}
			};

			// Token: 0x04009667 RID: 38503
			private static EmoteStep[] DEFAULT_IDLE_STEPS = new EmoteStep[]
			{
				new EmoteStep
				{
					anim = "idle_pre"
				},
				new EmoteStep
				{
					anim = "idle_default"
				},
				new EmoteStep
				{
					anim = "idle_pst"
				}
			};

			// Token: 0x04009668 RID: 38504
			public Emote ClapCheer;

			// Token: 0x04009669 RID: 38505
			public Emote Cheer;

			// Token: 0x0400966A RID: 38506
			public Emote ProductiveCheer;

			// Token: 0x0400966B RID: 38507
			public Emote ResearchComplete;

			// Token: 0x0400966C RID: 38508
			public Emote ThumbsUp;

			// Token: 0x0400966D RID: 38509
			public Emote CloseCall_Fall;

			// Token: 0x0400966E RID: 38510
			public Emote Cold;

			// Token: 0x0400966F RID: 38511
			public Emote Cough;

			// Token: 0x04009670 RID: 38512
			public Emote Cough_Small;

			// Token: 0x04009671 RID: 38513
			public Emote FoodPoisoning;

			// Token: 0x04009672 RID: 38514
			public Emote Hot;

			// Token: 0x04009673 RID: 38515
			public Emote IritatedEyes;

			// Token: 0x04009674 RID: 38516
			public Emote MorningStretch;

			// Token: 0x04009675 RID: 38517
			public Emote Radiation_Glare;

			// Token: 0x04009676 RID: 38518
			public Emote Radiation_Itch;

			// Token: 0x04009677 RID: 38519
			public Emote Sick;

			// Token: 0x04009678 RID: 38520
			public Emote Sneeze;

			// Token: 0x04009679 RID: 38521
			public Emote WaterDamage;

			// Token: 0x0400967A RID: 38522
			public Emote Sneeze_Short;

			// Token: 0x0400967B RID: 38523
			public Emote GrindingGears;

			// Token: 0x0400967C RID: 38524
			public Emote Concern;

			// Token: 0x0400967D RID: 38525
			public Emote Cringe;

			// Token: 0x0400967E RID: 38526
			public Emote Disappointed;

			// Token: 0x0400967F RID: 38527
			public Emote Shock;

			// Token: 0x04009680 RID: 38528
			public Emote Sing;

			// Token: 0x04009681 RID: 38529
			public Emote FingerGuns;

			// Token: 0x04009682 RID: 38530
			public Emote Wave;

			// Token: 0x04009683 RID: 38531
			public Emote Wave_Shy;
		}

		// Token: 0x020020E4 RID: 8420
		public class CritterEmotes : ResourceSet<Emote>
		{
			// Token: 0x0600B863 RID: 47203 RVA: 0x003E9923 File Offset: 0x003E7B23
			public CritterEmotes(ResourceSet parent)
				: base("Critter", parent)
			{
				this.InitializePhysicalState();
				this.InitializeEmotionalState();
			}

			// Token: 0x0600B864 RID: 47204 RVA: 0x003E9940 File Offset: 0x003E7B40
			private void InitializePhysicalState()
			{
				this.Hungry = new Emote(this, "Hungry", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react_hungry"
					}
				}, null);
			}

			// Token: 0x0600B865 RID: 47205 RVA: 0x003E9980 File Offset: 0x003E7B80
			private void InitializeEmotionalState()
			{
				this.Angry = new Emote(this, "Angry", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react_angry"
					}
				}, null);
				this.Happy = new Emote(this, "Happy", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react_happy"
					}
				}, null);
				this.Idle = new Emote(this, "Idle", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react_idle"
					}
				}, null);
				this.Sad = new Emote(this, "Sad", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "react_sad"
					}
				}, null);
				this.Roar = new Emote(this, "Roar", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "roar"
					}
				}, null);
				this.RaptorSignal = new Emote(this, "Signal", new EmoteStep[]
				{
					new EmoteStep
					{
						anim = "signal"
					}
				}, null);
			}

			// Token: 0x04009684 RID: 38532
			public Emote Hungry;

			// Token: 0x04009685 RID: 38533
			public Emote Angry;

			// Token: 0x04009686 RID: 38534
			public Emote Happy;

			// Token: 0x04009687 RID: 38535
			public Emote Idle;

			// Token: 0x04009688 RID: 38536
			public Emote Sad;

			// Token: 0x04009689 RID: 38537
			public Emote Roar;

			// Token: 0x0400968A RID: 38538
			public Emote RaptorSignal;
		}
	}
}
