using System;
using System.Collections.Generic;
using System.Linq;
using Klei.AI;
using STRINGS;

namespace TUNING
{
	// Token: 0x02000F90 RID: 3984
	public class CREATURES
	{
		// Token: 0x04005C48 RID: 23624
		public const float WILD_GROWTH_RATE_MODIFIER = 0.25f;

		// Token: 0x04005C49 RID: 23625
		public const int DEFAULT_PROBING_RADIUS = 32;

		// Token: 0x04005C4A RID: 23626
		public const float CREATURES_BASE_GENERATION_KILOWATTS = 10f;

		// Token: 0x04005C4B RID: 23627
		public const float FERTILITY_TIME_BY_LIFESPAN = 0.6f;

		// Token: 0x04005C4C RID: 23628
		public const float INCUBATION_TIME_BY_LIFESPAN = 0.2f;

		// Token: 0x04005C4D RID: 23629
		public const float INCUBATOR_INCUBATION_MULTIPLIER = 4f;

		// Token: 0x04005C4E RID: 23630
		public const float WILD_CALORIE_BURN_RATIO = 0.25f;

		// Token: 0x04005C4F RID: 23631
		public const float HUG_INCUBATION_MULTIPLIER = 1f;

		// Token: 0x04005C50 RID: 23632
		public const float VIABILITY_LOSS_RATE = -0.016666668f;

		// Token: 0x04005C51 RID: 23633
		public const float STATERPILLAR_POWER_CHARGE_LOSS_RATE = -0.055555556f;

		// Token: 0x04005C52 RID: 23634
		public const float HUNT_FAILED_DURATION = 45f;

		// Token: 0x04005C53 RID: 23635
		public const float EVADED_HUNT_DURATION = 10f;

		// Token: 0x02002168 RID: 8552
		public class HITPOINTS
		{
			// Token: 0x04009952 RID: 39250
			public const float TIER0 = 5f;

			// Token: 0x04009953 RID: 39251
			public const float TIER1 = 25f;

			// Token: 0x04009954 RID: 39252
			public const float TIER2 = 50f;

			// Token: 0x04009955 RID: 39253
			public const float TIER3 = 100f;

			// Token: 0x04009956 RID: 39254
			public const float TIER4 = 150f;

			// Token: 0x04009957 RID: 39255
			public const float TIER5 = 200f;

			// Token: 0x04009958 RID: 39256
			public const float TIER6 = 400f;
		}

		// Token: 0x02002169 RID: 8553
		public class MASS_KG
		{
			// Token: 0x04009959 RID: 39257
			public const float TIER0 = 5f;

			// Token: 0x0400995A RID: 39258
			public const float TIER1 = 25f;

			// Token: 0x0400995B RID: 39259
			public const float TIER2 = 50f;

			// Token: 0x0400995C RID: 39260
			public const float TIER3 = 100f;

			// Token: 0x0400995D RID: 39261
			public const float TIER4 = 200f;

			// Token: 0x0400995E RID: 39262
			public const float TIER5 = 400f;
		}

		// Token: 0x0200216A RID: 8554
		public class TEMPERATURE
		{
			// Token: 0x0400995F RID: 39263
			public const float SKIN_THICKNESS = 0.025f;

			// Token: 0x04009960 RID: 39264
			public const float SURFACE_AREA = 17.5f;

			// Token: 0x04009961 RID: 39265
			public const float GROUND_TRANSFER_SCALE = 0f;

			// Token: 0x04009962 RID: 39266
			public static float FREEZING_10 = 173f;

			// Token: 0x04009963 RID: 39267
			public static float FREEZING_9 = 183f;

			// Token: 0x04009964 RID: 39268
			public static float FREEZING_3 = 243f;

			// Token: 0x04009965 RID: 39269
			public static float FREEZING_2 = 253f;

			// Token: 0x04009966 RID: 39270
			public static float FREEZING_1 = 263f;

			// Token: 0x04009967 RID: 39271
			public static float FREEZING = 273f;

			// Token: 0x04009968 RID: 39272
			public static float COOL = 283f;

			// Token: 0x04009969 RID: 39273
			public static float MODERATE = 293f;

			// Token: 0x0400996A RID: 39274
			public static float HOT = 303f;

			// Token: 0x0400996B RID: 39275
			public static float HOT_1 = 313f;

			// Token: 0x0400996C RID: 39276
			public static float HOT_2 = 323f;

			// Token: 0x0400996D RID: 39277
			public static float HOT_3 = 333f;

			// Token: 0x0400996E RID: 39278
			public static float HOT_7 = 373f;
		}

		// Token: 0x0200216B RID: 8555
		public class LIFESPAN
		{
			// Token: 0x0400996F RID: 39279
			public const float TIER0 = 5f;

			// Token: 0x04009970 RID: 39280
			public const float TIER1 = 25f;

			// Token: 0x04009971 RID: 39281
			public const float TIER2 = 75f;

			// Token: 0x04009972 RID: 39282
			public const float TIER3 = 100f;

			// Token: 0x04009973 RID: 39283
			public const float TIER4 = 150f;

			// Token: 0x04009974 RID: 39284
			public const float TIER5 = 200f;

			// Token: 0x04009975 RID: 39285
			public const float TIER6 = 400f;
		}

		// Token: 0x0200216C RID: 8556
		public class CONVERSION_EFFICIENCY
		{
			// Token: 0x04009976 RID: 39286
			public static float BAD_2 = 0.1f;

			// Token: 0x04009977 RID: 39287
			public static float BAD_1 = 0.25f;

			// Token: 0x04009978 RID: 39288
			public static float NORMAL = 0.5f;

			// Token: 0x04009979 RID: 39289
			public static float GOOD_1 = 0.75f;

			// Token: 0x0400997A RID: 39290
			public static float GOOD_2 = 0.95f;

			// Token: 0x0400997B RID: 39291
			public static float GOOD_3 = 1f;
		}

		// Token: 0x0200216D RID: 8557
		public class SPACE_REQUIREMENTS
		{
			// Token: 0x0400997C RID: 39292
			public static int TIER1 = 4;

			// Token: 0x0400997D RID: 39293
			public static int TIER2 = 8;

			// Token: 0x0400997E RID: 39294
			public static int TIER3 = 12;

			// Token: 0x0400997F RID: 39295
			public static int TIER4 = 16;
		}

		// Token: 0x0200216E RID: 8558
		public class EGG_CHANCE_MODIFIERS
		{
			// Token: 0x0600B998 RID: 47512 RVA: 0x003ED209 File Offset: 0x003EB409
			private static global::System.Action CreateDietaryModifier(string id, Tag eggTag, HashSet<Tag> foodTags, float modifierPerCal)
			{
				Func<string, string> <>9__1;
				FertilityModifier.FertilityModFn <>9__2;
				return delegate
				{
					string text = CREATURES.FERTILITY_MODIFIERS.DIET.NAME;
					string text2 = CREATURES.FERTILITY_MODIFIERS.DIET.DESC;
					ModifierSet modifierSet = Db.Get();
					string id2 = id;
					Tag eggTag2 = eggTag;
					string text3 = text;
					string text4 = text2;
					Func<string, string> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = delegate(string descStr)
						{
							string text5 = string.Join(", ", foodTags.Select((Tag t) => t.ProperName()).ToArray<string>());
							descStr = string.Format(descStr, text5);
							return descStr;
						});
					}
					FertilityModifier.FertilityModFn fertilityModFn;
					if ((fertilityModFn = <>9__2) == null)
					{
						fertilityModFn = (<>9__2 = delegate(FertilityMonitor.Instance inst, Tag eggType)
						{
							inst.gameObject.Subscribe(-2038961714, delegate(object data)
							{
								CreatureCalorieMonitor.CaloriesConsumedEvent caloriesConsumedEvent = (CreatureCalorieMonitor.CaloriesConsumedEvent)data;
								if (foodTags.Contains(caloriesConsumedEvent.tag))
								{
									inst.AddBreedingChance(eggType, caloriesConsumedEvent.calories * modifierPerCal);
								}
							});
						});
					}
					modifierSet.CreateFertilityModifier(id2, eggTag2, text3, text4, func, fertilityModFn);
				};
			}

			// Token: 0x0600B999 RID: 47513 RVA: 0x003ED237 File Offset: 0x003EB437
			private static global::System.Action CreateDietaryModifier(string id, Tag eggTag, Tag foodTag, float modifierPerCal)
			{
				return CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier(id, eggTag, new HashSet<Tag> { foodTag }, modifierPerCal);
			}

			// Token: 0x0600B99A RID: 47514 RVA: 0x003ED24E File Offset: 0x003EB44E
			private static global::System.Action CreateNearbyCreatureModifier(string id, Tag eggTag, Tag nearbyCreatureBaby, Tag nearbyCreatureAdult, float modifierPerSecond, bool alsoInvert)
			{
				Func<string, string> <>9__1;
				FertilityModifier.FertilityModFn <>9__2;
				return delegate
				{
					string text = ((modifierPerSecond < 0f) ? CREATURES.FERTILITY_MODIFIERS.NEARBY_CREATURE_NEG.NAME : CREATURES.FERTILITY_MODIFIERS.NEARBY_CREATURE.NAME);
					string text2 = ((modifierPerSecond < 0f) ? CREATURES.FERTILITY_MODIFIERS.NEARBY_CREATURE_NEG.DESC : CREATURES.FERTILITY_MODIFIERS.NEARBY_CREATURE.DESC);
					ModifierSet modifierSet = Db.Get();
					string id2 = id;
					Tag eggTag2 = eggTag;
					string text3 = text;
					string text4 = text2;
					Func<string, string> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (string descStr) => string.Format(descStr, nearbyCreatureAdult.ProperName()));
					}
					FertilityModifier.FertilityModFn fertilityModFn;
					if ((fertilityModFn = <>9__2) == null)
					{
						fertilityModFn = (<>9__2 = delegate(FertilityMonitor.Instance inst, Tag eggType)
						{
							NearbyCreatureMonitor.Instance instance = inst.gameObject.GetSMI<NearbyCreatureMonitor.Instance>();
							if (instance == null)
							{
								instance = new NearbyCreatureMonitor.Instance(inst.master);
								instance.StartSM();
							}
							instance.OnUpdateNearbyCreatures += delegate(float dt, List<KPrefabID> creatures, List<KPrefabID> eggs)
							{
								bool flag = false;
								foreach (KPrefabID kprefabID in creatures)
								{
									if (kprefabID.PrefabTag == nearbyCreatureBaby || kprefabID.PrefabTag == nearbyCreatureAdult)
									{
										flag = true;
										break;
									}
								}
								if (flag)
								{
									inst.AddBreedingChance(eggType, dt * modifierPerSecond);
									return;
								}
								if (alsoInvert)
								{
									inst.AddBreedingChance(eggType, dt * -modifierPerSecond);
								}
							};
						});
					}
					modifierSet.CreateFertilityModifier(id2, eggTag2, text3, text4, func, fertilityModFn);
				};
			}

			// Token: 0x0600B99B RID: 47515 RVA: 0x003ED28C File Offset: 0x003EB48C
			private static global::System.Action CreateElementCreatureModifier(string id, Tag eggTag, Tag element, float modifierPerSecond, bool alsoInvert, bool checkSubstantialLiquid, string tooltipOverride = null)
			{
				Func<string, string> <>9__1;
				FertilityModifier.FertilityModFn <>9__2;
				return delegate
				{
					string text = CREATURES.FERTILITY_MODIFIERS.LIVING_IN_ELEMENT.NAME;
					string text2 = CREATURES.FERTILITY_MODIFIERS.LIVING_IN_ELEMENT.DESC;
					ModifierSet modifierSet = Db.Get();
					string id2 = id;
					Tag eggTag2 = eggTag;
					string text3 = text;
					string text4 = text2;
					Func<string, string> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = delegate(string descStr)
						{
							if (tooltipOverride == null)
							{
								return string.Format(descStr, ElementLoader.GetElement(element).name);
							}
							return tooltipOverride;
						});
					}
					FertilityModifier.FertilityModFn fertilityModFn;
					if ((fertilityModFn = <>9__2) == null)
					{
						fertilityModFn = (<>9__2 = delegate(FertilityMonitor.Instance inst, Tag eggType)
						{
							CritterElementMonitor.Instance instance = inst.gameObject.GetSMI<CritterElementMonitor.Instance>();
							if (instance == null)
							{
								instance = new CritterElementMonitor.Instance(inst.master);
								instance.StartSM();
							}
							instance.OnUpdateEggChances += delegate(float dt)
							{
								int num = Grid.PosToCell(inst);
								if (!Grid.IsValidCell(num))
								{
									return;
								}
								if (Grid.Element[num].HasTag(element) && (!checkSubstantialLiquid || Grid.IsSubstantialLiquid(num, 0.35f)))
								{
									inst.AddBreedingChance(eggType, dt * modifierPerSecond);
									return;
								}
								if (alsoInvert)
								{
									inst.AddBreedingChance(eggType, dt * -modifierPerSecond);
								}
							};
						});
					}
					modifierSet.CreateFertilityModifier(id2, eggTag2, text3, text4, func, fertilityModFn);
				};
			}

			// Token: 0x0600B99C RID: 47516 RVA: 0x003ED2DD File Offset: 0x003EB4DD
			private static global::System.Action CreateCropTendedModifier(string id, Tag eggTag, HashSet<Tag> cropTags, float modifierPerEvent)
			{
				Func<string, string> <>9__1;
				FertilityModifier.FertilityModFn <>9__2;
				return delegate
				{
					string text = CREATURES.FERTILITY_MODIFIERS.CROPTENDING.NAME;
					string text2 = CREATURES.FERTILITY_MODIFIERS.CROPTENDING.DESC;
					ModifierSet modifierSet = Db.Get();
					string id2 = id;
					Tag eggTag2 = eggTag;
					string text3 = text;
					string text4 = text2;
					Func<string, string> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = delegate(string descStr)
						{
							string text5 = string.Join(", ", cropTags.Select((Tag t) => t.ProperName()).ToArray<string>());
							descStr = string.Format(descStr, text5);
							return descStr;
						});
					}
					FertilityModifier.FertilityModFn fertilityModFn;
					if ((fertilityModFn = <>9__2) == null)
					{
						fertilityModFn = (<>9__2 = delegate(FertilityMonitor.Instance inst, Tag eggType)
						{
							inst.gameObject.Subscribe(90606262, delegate(object data)
							{
								CropTendingStates.CropTendingEventData cropTendingEventData = (CropTendingStates.CropTendingEventData)data;
								if (cropTags.Contains(cropTendingEventData.cropId))
								{
									inst.AddBreedingChance(eggType, modifierPerEvent);
								}
							});
						});
					}
					modifierSet.CreateFertilityModifier(id2, eggTag2, text3, text4, func, fertilityModFn);
				};
			}

			// Token: 0x0600B99D RID: 47517 RVA: 0x003ED30B File Offset: 0x003EB50B
			private static global::System.Action CreateTemperatureModifier(string id, Tag eggTag, float minTemp, float maxTemp, float modifierPerSecond, bool alsoInvert)
			{
				Func<string, string> <>9__1;
				FertilityModifier.FertilityModFn <>9__2;
				return delegate
				{
					string text = CREATURES.FERTILITY_MODIFIERS.TEMPERATURE.NAME;
					ModifierSet modifierSet = Db.Get();
					string id2 = id;
					Tag eggTag2 = eggTag;
					string text2 = text;
					string text3 = null;
					Func<string, string> func;
					if ((func = <>9__1) == null)
					{
						func = (<>9__1 = (string src) => string.Format(CREATURES.FERTILITY_MODIFIERS.TEMPERATURE.DESC, GameUtil.GetFormattedTemperature(minTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false), GameUtil.GetFormattedTemperature(maxTemp, GameUtil.TimeSlice.None, GameUtil.TemperatureInterpretation.Absolute, true, false)));
					}
					FertilityModifier.FertilityModFn fertilityModFn;
					if ((fertilityModFn = <>9__2) == null)
					{
						fertilityModFn = (<>9__2 = delegate(FertilityMonitor.Instance inst, Tag eggType)
						{
							CritterTemperatureMonitor.Instance smi = inst.gameObject.GetSMI<CritterTemperatureMonitor.Instance>();
							if (smi != null)
							{
								CritterTemperatureMonitor.Instance instance = smi;
								instance.OnUpdate_GetTemperatureInternal = (Action<float, float>)Delegate.Combine(instance.OnUpdate_GetTemperatureInternal, new Action<float, float>(delegate(float dt, float newTemp)
								{
									if (newTemp > minTemp && newTemp < maxTemp)
									{
										inst.AddBreedingChance(eggType, dt * modifierPerSecond);
										return;
									}
									if (alsoInvert)
									{
										inst.AddBreedingChance(eggType, dt * -modifierPerSecond);
									}
								}));
								return;
							}
							DebugUtil.LogErrorArgs(new object[]
							{
								"Ack! Trying to add temperature modifier",
								id,
								"to",
								inst.master.name,
								"but it doesn't have a CritterTemperatureMonitor.Instance"
							});
						});
					}
					modifierSet.CreateFertilityModifier(id2, eggTag2, text2, text3, func, fertilityModFn);
				};
			}

			// Token: 0x04009980 RID: 39296
			public static List<global::System.Action> MODIFIER_CREATORS = new List<global::System.Action>
			{
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("HatchHard", "HatchHardEgg".ToTag(), SimHashes.SedimentaryRock.CreateTag(), 0.05f / HatchTuning.STANDARD_CALORIES_PER_CYCLE),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("HatchVeggie", "HatchVeggieEgg".ToTag(), SimHashes.Dirt.CreateTag(), 0.05f / HatchTuning.STANDARD_CALORIES_PER_CYCLE),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("HatchMetal", "HatchMetalEgg".ToTag(), HatchMetalConfig.METAL_ORE_TAGS, 0.05f / HatchTuning.STANDARD_CALORIES_PER_CYCLE),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateNearbyCreatureModifier("PuftAlphaBalance", "PuftAlphaEgg".ToTag(), "PuftAlphaBaby".ToTag(), "PuftAlpha".ToTag(), -0.00025f, true),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateNearbyCreatureModifier("PuftAlphaNearbyOxylite", "PuftOxyliteEgg".ToTag(), "PuftAlphaBaby".ToTag(), "PuftAlpha".ToTag(), 8.333333E-05f, false),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateNearbyCreatureModifier("PuftAlphaNearbyBleachstone", "PuftBleachstoneEgg".ToTag(), "PuftAlphaBaby".ToTag(), "PuftAlpha".ToTag(), 8.333333E-05f, false),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateTemperatureModifier("OilFloaterHighTemp", "OilfloaterHighTempEgg".ToTag(), 373.15f, 523.15f, 8.333333E-05f, false),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateTemperatureModifier("OilFloaterDecor", "OilfloaterDecorEgg".ToTag(), 293.15f, 333.15f, 8.333333E-05f, false),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("LightBugOrange", "LightBugOrangeEgg".ToTag(), "GrilledPrickleFruit".ToTag(), 0.00125f),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("LightBugPurple", "LightBugPurpleEgg".ToTag(), "FriedMushroom".ToTag(), 0.00125f),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("LightBugPink", "LightBugPinkEgg".ToTag(), "SpiceBread".ToTag(), 0.00125f),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("LightBugBlue", "LightBugBlueEgg".ToTag(), "Salsa".ToTag(), 0.00125f),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("LightBugBlack", "LightBugBlackEgg".ToTag(), SimHashes.Phosphorus.CreateTag(), 0.00125f),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("LightBugCrystal", "LightBugCrystalEgg".ToTag(), "CookedMeat".ToTag(), 0.00125f),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateTemperatureModifier("PacuTropical", "PacuTropicalEgg".ToTag(), 308.15f, 353.15f, 8.333333E-05f, false),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateTemperatureModifier("PacuCleaner", "PacuCleanerEgg".ToTag(), 243.15f, 278.15f, 8.333333E-05f, false),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("DreckoPlastic", "DreckoPlasticEgg".ToTag(), "BasicSingleHarvestPlant".ToTag(), 0.025f / DreckoTuning.STANDARD_CALORIES_PER_CYCLE),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("SquirrelHug", "SquirrelHugEgg".ToTag(), BasicFabricMaterialPlantConfig.ID.ToTag(), 0.025f / SquirrelTuning.STANDARD_CALORIES_PER_CYCLE),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateCropTendedModifier("DivergentWorm", "DivergentWormEgg".ToTag(), new HashSet<Tag>
				{
					"WormPlant".ToTag(),
					"SuperWormPlant".ToTag()
				}, 0.05f / (float)DivergentTuning.TIMES_TENDED_PER_CYCLE_FOR_EVOLUTION),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateElementCreatureModifier("PokeLumber", "CrabWoodEgg".ToTag(), SimHashes.Ethanol.CreateTag(), 0.00025f, true, true, null),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateElementCreatureModifier("PokeFreshWater", "CrabFreshWaterEgg".ToTag(), SimHashes.Water.CreateTag(), 0.00025f, true, true, null),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateTemperatureModifier("MoleDelicacy", "MoleDelicacyEgg".ToTag(), MoleDelicacyConfig.EGG_CHANCES_TEMPERATURE_MIN, MoleDelicacyConfig.EGG_CHANCES_TEMPERATURE_MAX, 8.333333E-05f, false),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateElementCreatureModifier("StaterpillarGas", "StaterpillarGasEgg".ToTag(), GameTags.Unbreathable, 0.00025f, true, false, CREATURES.FERTILITY_MODIFIERS.LIVING_IN_ELEMENT.UNBREATHABLE),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateElementCreatureModifier("StaterpillarLiquid", "StaterpillarLiquidEgg".ToTag(), GameTags.Liquid, 0.00025f, true, false, CREATURES.FERTILITY_MODIFIERS.LIVING_IN_ELEMENT.LIQUID),
				CREATURES.EGG_CHANCE_MODIFIERS.CreateDietaryModifier("BellyGold", "GoldBellyEgg".ToTag(), "FriesCarrot".ToTag(), 0.05f / BellyTuning.STANDARD_CALORIES_PER_CYCLE)
			};
		}

		// Token: 0x0200216F RID: 8559
		public class SORTING
		{
			// Token: 0x04009981 RID: 39297
			public static Dictionary<string, int> CRITTER_ORDER = new Dictionary<string, int>
			{
				{ "Hatch", 10 },
				{ "Puft", 20 },
				{ "Drecko", 30 },
				{ "Squirrel", 40 },
				{ "Pacu", 50 },
				{ "Oilfloater", 60 },
				{ "LightBug", 70 },
				{ "Crab", 80 },
				{ "DivergentBeetle", 90 },
				{ "Staterpillar", 100 },
				{ "Mole", 110 },
				{ "Bee", 120 },
				{ "Moo", 130 },
				{ "Glom", 140 },
				{ "WoodDeer", 150 },
				{ "Seal", 160 },
				{ "IceBelly", 170 },
				{ "Stego", 180 },
				{ "Butterfly", 190 },
				{ "Mosquito", 200 },
				{ "Chameleon", 210 },
				{ "PrehistoricPacu", 220 }
			};
		}
	}
}
