using System;
using System.Collections.Generic;
using UnityEngine;

namespace TUNING
{
	// Token: 0x02000F88 RID: 3976
	public class DUPLICANTSTATS
	{
		// Token: 0x06007C35 RID: 31797 RVA: 0x0031B0D0 File Offset: 0x003192D0
		public static DUPLICANTSTATS.TraitVal GetTraitVal(string id)
		{
			foreach (DUPLICANTSTATS.TraitVal traitVal in DUPLICANTSTATS.SPECIALTRAITS)
			{
				if (id == traitVal.id)
				{
					return traitVal;
				}
			}
			foreach (DUPLICANTSTATS.TraitVal traitVal2 in DUPLICANTSTATS.GOODTRAITS)
			{
				if (id == traitVal2.id)
				{
					return traitVal2;
				}
			}
			foreach (DUPLICANTSTATS.TraitVal traitVal3 in DUPLICANTSTATS.BADTRAITS)
			{
				if (id == traitVal3.id)
				{
					return traitVal3;
				}
			}
			foreach (DUPLICANTSTATS.TraitVal traitVal4 in DUPLICANTSTATS.CONGENITALTRAITS)
			{
				if (id == traitVal4.id)
				{
					return traitVal4;
				}
			}
			DebugUtil.Assert(true, "Could not find TraitVal with ID: " + id);
			return DUPLICANTSTATS.INVALID_TRAIT_VAL;
		}

		// Token: 0x06007C36 RID: 31798 RVA: 0x0031B238 File Offset: 0x00319438
		public static DUPLICANTSTATS GetStatsFor(GameObject gameObject)
		{
			KPrefabID component = gameObject.GetComponent<KPrefabID>();
			if (component != null)
			{
				return DUPLICANTSTATS.GetStatsFor(component);
			}
			return null;
		}

		// Token: 0x06007C37 RID: 31799 RVA: 0x0031B260 File Offset: 0x00319460
		public static DUPLICANTSTATS GetStatsFor(KPrefabID prefabID)
		{
			if (!prefabID.HasTag(GameTags.BaseMinion))
			{
				return null;
			}
			foreach (Tag tag in GameTags.Minions.Models.AllModels)
			{
				if (prefabID.HasTag(tag))
				{
					return DUPLICANTSTATS.GetStatsFor(tag);
				}
			}
			return null;
		}

		// Token: 0x06007C38 RID: 31800 RVA: 0x0031B2A9 File Offset: 0x003194A9
		public static DUPLICANTSTATS GetStatsFor(Tag type)
		{
			if (DUPLICANTSTATS.DUPLICANT_TYPES.ContainsKey(type))
			{
				return DUPLICANTSTATS.DUPLICANT_TYPES[type];
			}
			return null;
		}

		// Token: 0x04005BBC RID: 23484
		public const float RANCHING_DURATION_MULTIPLIER_BONUS_PER_POINT = 0.1f;

		// Token: 0x04005BBD RID: 23485
		public const float FARMING_DURATION_MULTIPLIER_BONUS_PER_POINT = 0.1f;

		// Token: 0x04005BBE RID: 23486
		public const float POWER_DURATION_MULTIPLIER_BONUS_PER_POINT = 0.025f;

		// Token: 0x04005BBF RID: 23487
		public const float RANCHING_CAPTURABLE_MULTIPLIER_BONUS_PER_POINT = 0.05f;

		// Token: 0x04005BC0 RID: 23488
		public const float STANDARD_STRESS_PENALTY = 0.016666668f;

		// Token: 0x04005BC1 RID: 23489
		public const float STANDARD_STRESS_BONUS = -0.033333335f;

		// Token: 0x04005BC2 RID: 23490
		public const float STRESS_BELOW_EXPECTATIONS_FOOD = 0.25f;

		// Token: 0x04005BC3 RID: 23491
		public const float STRESS_ABOVE_EXPECTATIONS_FOOD = -0.5f;

		// Token: 0x04005BC4 RID: 23492
		public const float STANDARD_STRESS_PENALTY_SECOND = 0.25f;

		// Token: 0x04005BC5 RID: 23493
		public const float STANDARD_STRESS_BONUS_SECOND = -0.5f;

		// Token: 0x04005BC6 RID: 23494
		public const float TRAVEL_TIME_WARNING_THRESHOLD = 0.4f;

		// Token: 0x04005BC7 RID: 23495
		public static string[] ALL_ATTRIBUTES = new string[]
		{
			"Strength", "Caring", "Construction", "Digging", "Machinery", "Learning", "Cooking", "Botanist", "Art", "Ranching",
			"Athletics", "SpaceNavigation"
		};

		// Token: 0x04005BC8 RID: 23496
		public static string[] DISTRIBUTED_ATTRIBUTES = new string[] { "Strength", "Caring", "Construction", "Digging", "Machinery", "Learning", "Cooking", "Botanist", "Art", "Ranching" };

		// Token: 0x04005BC9 RID: 23497
		public static string[] ROLLED_ATTRIBUTES = new string[] { "Athletics" };

		// Token: 0x04005BCA RID: 23498
		public static int[] APTITUDE_ATTRIBUTE_BONUSES = new int[] { 7, 3, 1 };

		// Token: 0x04005BCB RID: 23499
		public static int ROLLED_ATTRIBUTE_MAX = 5;

		// Token: 0x04005BCC RID: 23500
		public static float ROLLED_ATTRIBUTE_POWER = 4f;

		// Token: 0x04005BCD RID: 23501
		public static Dictionary<string, List<string>> ARCHETYPE_TRAIT_EXCLUSIONS = new Dictionary<string, List<string>>
		{
			{
				"Mining",
				new List<string> { "Anemic", "DiggingDown", "Narcolepsy" }
			},
			{
				"Building",
				new List<string> { "Anemic", "NoodleArms", "ConstructionDown", "DiggingDown", "Narcolepsy" }
			},
			{
				"Farming",
				new List<string> { "Anemic", "NoodleArms", "BotanistDown", "RanchingDown", "Narcolepsy" }
			},
			{
				"Ranching",
				new List<string> { "RanchingDown", "BotanistDown", "Narcolepsy" }
			},
			{
				"Cooking",
				new List<string> { "NoodleArms", "CookingDown" }
			},
			{
				"Art",
				new List<string> { "ArtDown", "DecorDown" }
			},
			{
				"Research",
				new List<string> { "SlowLearner" }
			},
			{
				"Suits",
				new List<string> { "Anemic", "NoodleArms" }
			},
			{
				"Hauling",
				new List<string> { "Anemic", "NoodleArms", "Narcolepsy" }
			},
			{
				"Technicals",
				new List<string> { "MachineryDown" }
			},
			{
				"MedicalAid",
				new List<string> { "CaringDown", "WeakImmuneSystem" }
			},
			{
				"Basekeeping",
				new List<string> { "Anemic", "NoodleArms" }
			},
			{
				"Rocketry",
				new List<string>()
			}
		};

		// Token: 0x04005BCE RID: 23502
		public static Dictionary<string, List<string>> ARCHETYPE_BIONIC_TRAIT_COMPATIBILITY = new Dictionary<string, List<string>>
		{
			{
				"Mining",
				new List<string> { "Booster_Dig1", "Booster_Dig2" }
			},
			{
				"Building",
				new List<string> { "Booster_Construct1" }
			},
			{
				"Farming",
				new List<string> { "Booster_Farm1" }
			},
			{
				"Ranching",
				new List<string> { "Booster_Ranch1" }
			},
			{
				"Cooking",
				new List<string> { "Booster_Cook1" }
			},
			{
				"Art",
				new List<string> { "Booster_Art1" }
			},
			{
				"Research",
				new List<string> { "Booster_Research1", "Booster_Research2", "Booster_Research3" }
			},
			{
				"Suits",
				new List<string> { "Booster_Suits1" }
			},
			{
				"Hauling",
				new List<string> { "Booster_Tidy1", "Booster_Carry1" }
			},
			{
				"Technicals",
				new List<string> { "Booster_Op1", "Booster_Op2" }
			},
			{
				"MedicalAid",
				new List<string> { "Booster_Medicine1" }
			},
			{
				"Basekeeping",
				new List<string> { "Booster_Tidy1", "Booster_Carry1" }
			},
			{
				"Rocketry",
				new List<string> { "Booster_PilotVanilla1", "Booster_Pilot1" }
			}
		};

		// Token: 0x04005BCF RID: 23503
		public static int RARITY_LEGENDARY = 5;

		// Token: 0x04005BD0 RID: 23504
		public static int RARITY_EPIC = 4;

		// Token: 0x04005BD1 RID: 23505
		public static int RARITY_RARE = 3;

		// Token: 0x04005BD2 RID: 23506
		public static int RARITY_UNCOMMON = 2;

		// Token: 0x04005BD3 RID: 23507
		public static int RARITY_COMMON = 1;

		// Token: 0x04005BD4 RID: 23508
		public static int NO_STATPOINT_BONUS = 0;

		// Token: 0x04005BD5 RID: 23509
		public static int TINY_STATPOINT_BONUS = 1;

		// Token: 0x04005BD6 RID: 23510
		public static int SMALL_STATPOINT_BONUS = 2;

		// Token: 0x04005BD7 RID: 23511
		public static int MEDIUM_STATPOINT_BONUS = 3;

		// Token: 0x04005BD8 RID: 23512
		public static int LARGE_STATPOINT_BONUS = 4;

		// Token: 0x04005BD9 RID: 23513
		public static int HUGE_STATPOINT_BONUS = 5;

		// Token: 0x04005BDA RID: 23514
		public static int COMMON = 1;

		// Token: 0x04005BDB RID: 23515
		public static int UNCOMMON = 2;

		// Token: 0x04005BDC RID: 23516
		public static int RARE = 3;

		// Token: 0x04005BDD RID: 23517
		public static int EPIC = 4;

		// Token: 0x04005BDE RID: 23518
		public static int LEGENDARY = 5;

		// Token: 0x04005BDF RID: 23519
		public static global::Tuple<int, int> TRAITS_ONE_POSITIVE_ONE_NEGATIVE = new global::Tuple<int, int>(1, 1);

		// Token: 0x04005BE0 RID: 23520
		public static global::Tuple<int, int> TRAITS_TWO_POSITIVE_ONE_NEGATIVE = new global::Tuple<int, int>(2, 1);

		// Token: 0x04005BE1 RID: 23521
		public static global::Tuple<int, int> TRAITS_ONE_POSITIVE_TWO_NEGATIVE = new global::Tuple<int, int>(1, 2);

		// Token: 0x04005BE2 RID: 23522
		public static global::Tuple<int, int> TRAITS_TWO_POSITIVE_TWO_NEGATIVE = new global::Tuple<int, int>(2, 2);

		// Token: 0x04005BE3 RID: 23523
		public static global::Tuple<int, int> TRAITS_THREE_POSITIVE_ONE_NEGATIVE = new global::Tuple<int, int>(3, 1);

		// Token: 0x04005BE4 RID: 23524
		public static global::Tuple<int, int> TRAITS_ONE_POSITIVE_THREE_NEGATIVE = new global::Tuple<int, int>(1, 3);

		// Token: 0x04005BE5 RID: 23525
		public static int MIN_STAT_POINTS = 0;

		// Token: 0x04005BE6 RID: 23526
		public static int MAX_STAT_POINTS = 0;

		// Token: 0x04005BE7 RID: 23527
		public static int MAX_TRAITS = 4;

		// Token: 0x04005BE8 RID: 23528
		public static int APTITUDE_BONUS = 1;

		// Token: 0x04005BE9 RID: 23529
		public static List<int> RARITY_DECK = new List<int>
		{
			DUPLICANTSTATS.RARITY_COMMON,
			DUPLICANTSTATS.RARITY_COMMON,
			DUPLICANTSTATS.RARITY_COMMON,
			DUPLICANTSTATS.RARITY_COMMON,
			DUPLICANTSTATS.RARITY_COMMON,
			DUPLICANTSTATS.RARITY_COMMON,
			DUPLICANTSTATS.RARITY_COMMON,
			DUPLICANTSTATS.RARITY_UNCOMMON,
			DUPLICANTSTATS.RARITY_UNCOMMON,
			DUPLICANTSTATS.RARITY_UNCOMMON,
			DUPLICANTSTATS.RARITY_UNCOMMON,
			DUPLICANTSTATS.RARITY_UNCOMMON,
			DUPLICANTSTATS.RARITY_UNCOMMON,
			DUPLICANTSTATS.RARITY_RARE,
			DUPLICANTSTATS.RARITY_RARE,
			DUPLICANTSTATS.RARITY_RARE,
			DUPLICANTSTATS.RARITY_RARE,
			DUPLICANTSTATS.RARITY_EPIC,
			DUPLICANTSTATS.RARITY_EPIC,
			DUPLICANTSTATS.RARITY_LEGENDARY
		};

		// Token: 0x04005BEA RID: 23530
		public static List<int> rarityDeckActive = new List<int>(DUPLICANTSTATS.RARITY_DECK);

		// Token: 0x04005BEB RID: 23531
		public static List<global::Tuple<int, int>> POD_TRAIT_CONFIGURATIONS_DECK = new List<global::Tuple<int, int>>
		{
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_TWO_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_TWO_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_TWO_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_TWO_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_TWO_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_TWO_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_TWO_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_TWO_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_TWO_NEGATIVE,
			DUPLICANTSTATS.TRAITS_TWO_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_TWO_POSITIVE_TWO_NEGATIVE,
			DUPLICANTSTATS.TRAITS_TWO_POSITIVE_TWO_NEGATIVE,
			DUPLICANTSTATS.TRAITS_THREE_POSITIVE_ONE_NEGATIVE,
			DUPLICANTSTATS.TRAITS_ONE_POSITIVE_THREE_NEGATIVE
		};

		// Token: 0x04005BEC RID: 23532
		public static List<global::Tuple<int, int>> podTraitConfigurationsActive = new List<global::Tuple<int, int>>(DUPLICANTSTATS.POD_TRAIT_CONFIGURATIONS_DECK);

		// Token: 0x04005BED RID: 23533
		public static List<string> CONTRACTEDTRAITS_HEALING = new List<string> { "IrritableBowel", "Aggressive", "SlowLearner", "WeakImmuneSystem", "Snorer", "CantDig" };

		// Token: 0x04005BEE RID: 23534
		public static List<DUPLICANTSTATS.TraitVal> CONGENITALTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "None"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Joshua",
				mutuallyExclusiveTraits = new List<string> { "ScaredyCat", "Aggressive" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Ellie",
				statBonus = DUPLICANTSTATS.TINY_STATPOINT_BONUS,
				mutuallyExclusiveTraits = new List<string> { "InteriorDecorator", "MouthBreather", "Uncultured" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Stinky",
				mutuallyExclusiveTraits = new List<string> { "Flatulence", "InteriorDecorator" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Liam",
				mutuallyExclusiveTraits = new List<string> { "Flatulence", "InteriorDecorator" }
			}
		};

		// Token: 0x04005BEF RID: 23535
		public static readonly DUPLICANTSTATS.TraitVal INVALID_TRAIT_VAL = new DUPLICANTSTATS.TraitVal
		{
			id = "INVALID"
		};

		// Token: 0x04005BF0 RID: 23536
		public static List<DUPLICANTSTATS.TraitVal> BADTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "CantResearch",
				statBonus = DUPLICANTSTATS.NO_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				mutuallyExclusiveAptitudes = new List<HashedString> { "Research" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "CantDig",
				statBonus = DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				mutuallyExclusiveAptitudes = new List<HashedString> { "Mining" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "CantCook",
				statBonus = DUPLICANTSTATS.NO_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				mutuallyExclusiveAptitudes = new List<HashedString> { "Cooking" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "CantBuild",
				statBonus = DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				mutuallyExclusiveAptitudes = new List<HashedString> { "Building" },
				mutuallyExclusiveTraits = new List<string> { "GrantSkill_Engineering1" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Hemophobia",
				statBonus = DUPLICANTSTATS.NO_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				mutuallyExclusiveAptitudes = new List<HashedString> { "MedicalAid" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "ScaredyCat",
				statBonus = DUPLICANTSTATS.NO_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				mutuallyExclusiveAptitudes = new List<HashedString> { "Mining" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "ConstructionDown",
				statBonus = DUPLICANTSTATS.MEDIUM_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				mutuallyExclusiveTraits = new List<string> { "ConstructionUp", "CantBuild" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "RanchingDown",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				mutuallyExclusiveTraits = new List<string> { "RanchingUp" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "CaringDown",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				mutuallyExclusiveTraits = new List<string> { "Hemophobia" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "BotanistDown",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_COMMON
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "ArtDown",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_COMMON
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "CookingDown",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				mutuallyExclusiveTraits = new List<string> { "Foodie", "CantCook" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "MachineryDown",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_COMMON
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "DiggingDown",
				statBonus = DUPLICANTSTATS.MEDIUM_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_RARE,
				mutuallyExclusiveTraits = new List<string> { "MoleHands", "CantDig" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "SlowLearner",
				statBonus = DUPLICANTSTATS.MEDIUM_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_RARE,
				mutuallyExclusiveTraits = new List<string> { "FastLearner", "CantResearch" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "NoodleArms",
				statBonus = DUPLICANTSTATS.MEDIUM_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_RARE
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "DecorDown",
				statBonus = DUPLICANTSTATS.TINY_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_COMMON
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Anemic",
				statBonus = DUPLICANTSTATS.HUGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_LEGENDARY
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Flatulence",
				statBonus = DUPLICANTSTATS.MEDIUM_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_RARE
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "IrritableBowel",
				statBonus = DUPLICANTSTATS.TINY_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Snorer",
				statBonus = DUPLICANTSTATS.TINY_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_RARE
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "MouthBreather",
				statBonus = DUPLICANTSTATS.HUGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_LEGENDARY
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "SmallBladder",
				statBonus = DUPLICANTSTATS.TINY_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "CalorieBurner",
				statBonus = DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "WeakImmuneSystem",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Allergies",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_RARE
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "NightLight",
				statBonus = DUPLICANTSTATS.SMALL_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_RARE
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Narcolepsy",
				statBonus = DUPLICANTSTATS.HUGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_RARE
			}
		};

		// Token: 0x04005BF1 RID: 23537
		public static List<DUPLICANTSTATS.TraitVal> STRESSTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "Aggressive"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "StressVomiter"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "UglyCrier"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "BingeEater"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Banshee"
			}
		};

		// Token: 0x04005BF2 RID: 23538
		public static List<DUPLICANTSTATS.TraitVal> JOYTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "BalloonArtist"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "SparkleStreaker"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "StickerBomber"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "SuperProductive"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "HappySinger"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "DataRainer",
				requiredDlcIds = DlcManager.DLC3
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "RoboDancer",
				requiredDlcIds = DlcManager.DLC3
			}
		};

		// Token: 0x04005BF3 RID: 23539
		public static List<DUPLICANTSTATS.TraitVal> GENESHUFFLERTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "Regeneration"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "DeeperDiversLungs"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "SunnyDisposition"
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "RockCrusher"
			}
		};

		// Token: 0x04005BF4 RID: 23540
		public static List<DUPLICANTSTATS.TraitVal> BIONICBUGTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "BionicBug1",
				requiredDlcIds = DlcManager.DLC3
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "BionicBug2",
				requiredDlcIds = DlcManager.DLC3
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "BionicBug3",
				requiredDlcIds = DlcManager.DLC3
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "BionicBug4",
				requiredDlcIds = DlcManager.DLC3
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "BionicBug5",
				requiredDlcIds = DlcManager.DLC3
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "BionicBug6",
				requiredDlcIds = DlcManager.DLC3
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "BionicBug7",
				requiredDlcIds = DlcManager.DLC3
			}
		};

		// Token: 0x04005BF5 RID: 23541
		public static readonly List<DUPLICANTSTATS.TraitVal> BIONICUPGRADETRAITS = new List<DUPLICANTSTATS.TraitVal>();

		// Token: 0x04005BF6 RID: 23542
		public static List<DUPLICANTSTATS.TraitVal> SPECIALTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "AncientKnowledge",
				rarity = DUPLICANTSTATS.RARITY_LEGENDARY,
				requiredDlcIds = DlcManager.EXPANSION1,
				doNotGenerateTrait = true,
				mutuallyExclusiveTraits = new List<string>
				{
					"CantResearch", "CantBuild", "CantCook", "CantDig", "Hemophobia", "ScaredyCat", "Anemic", "SlowLearner", "NoodleArms", "ConstructionDown",
					"RanchingDown", "DiggingDown", "MachineryDown", "CookingDown", "ArtDown", "CaringDown", "BotanistDown"
				}
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Chatty",
				rarity = DUPLICANTSTATS.RARITY_LEGENDARY,
				doNotGenerateTrait = true
			}
		};

		// Token: 0x04005BF7 RID: 23543
		public static List<DUPLICANTSTATS.TraitVal> GOODTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "Twinkletoes",
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				mutuallyExclusiveTraits = new List<string> { "Anemic" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "StrongArm",
				rarity = DUPLICANTSTATS.RARITY_RARE,
				mutuallyExclusiveTraits = new List<string> { "NoodleArms" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Greasemonkey",
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				mutuallyExclusiveTraits = new List<string> { "MachineryDown" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "DiversLung",
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				mutuallyExclusiveTraits = new List<string> { "MouthBreather" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "IronGut",
				rarity = DUPLICANTSTATS.RARITY_COMMON
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "StrongImmuneSystem",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				mutuallyExclusiveTraits = new List<string> { "WeakImmuneSystem" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "EarlyBird",
				rarity = DUPLICANTSTATS.RARITY_RARE,
				mutuallyExclusiveTraits = new List<string> { "NightOwl" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "NightOwl",
				rarity = DUPLICANTSTATS.RARITY_RARE,
				mutuallyExclusiveTraits = new List<string> { "EarlyBird" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Meteorphile",
				rarity = DUPLICANTSTATS.RARITY_RARE
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "MoleHands",
				rarity = DUPLICANTSTATS.RARITY_RARE,
				mutuallyExclusiveTraits = new List<string> { "CantDig", "DiggingDown" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "FastLearner",
				rarity = DUPLICANTSTATS.RARITY_RARE,
				mutuallyExclusiveTraits = new List<string> { "SlowLearner", "CantResearch" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "InteriorDecorator",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				mutuallyExclusiveTraits = new List<string> { "Uncultured", "ArtDown" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Uncultured",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				mutuallyExclusiveTraits = new List<string> { "InteriorDecorator" },
				mutuallyExclusiveAptitudes = new List<HashedString> { "Art" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "SimpleTastes",
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				mutuallyExclusiveTraits = new List<string> { "Foodie" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Foodie",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				mutuallyExclusiveTraits = new List<string> { "SimpleTastes", "CantCook", "CookingDown" },
				mutuallyExclusiveAptitudes = new List<HashedString> { "Cooking" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "BedsideManner",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				mutuallyExclusiveTraits = new List<string> { "Hemophobia", "CaringDown" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "DecorUp",
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				mutuallyExclusiveTraits = new List<string> { "DecorDown" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Thriver",
				rarity = DUPLICANTSTATS.RARITY_EPIC
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GreenThumb",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				mutuallyExclusiveTraits = new List<string> { "BotanistDown" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "ConstructionUp",
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				mutuallyExclusiveTraits = new List<string> { "ConstructionDown", "CantBuild" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "RanchingUp",
				rarity = DUPLICANTSTATS.RARITY_UNCOMMON,
				mutuallyExclusiveTraits = new List<string> { "RanchingDown" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Loner",
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				requiredDlcIds = DlcManager.EXPANSION1
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "StarryEyed",
				rarity = DUPLICANTSTATS.RARITY_RARE,
				requiredDlcIds = DlcManager.EXPANSION1
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GlowStick",
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				requiredDlcIds = DlcManager.EXPANSION1
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "RadiationEater",
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				requiredDlcIds = DlcManager.EXPANSION1
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "FrostProof",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				requiredDlcIds = DlcManager.DLC2
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Mining1",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_LEGENDARY,
				mutuallyExclusiveTraits = new List<string> { "CantDig" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Mining2",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_LEGENDARY,
				mutuallyExclusiveTraits = new List<string> { "CantDig" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Mining3",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_LEGENDARY,
				mutuallyExclusiveTraits = new List<string> { "CantDig" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Farming2",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Ranching1",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Cooking1",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				mutuallyExclusiveTraits = new List<string> { "CantCook" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Arting1",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				mutuallyExclusiveTraits = new List<string> { "Uncultured" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Arting2",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				mutuallyExclusiveTraits = new List<string> { "Uncultured" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Arting3",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				mutuallyExclusiveTraits = new List<string> { "Uncultured" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Suits1",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Technicals2",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Engineering1",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Basekeeping2",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				mutuallyExclusiveTraits = new List<string> { "Anemic" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "GrantSkill_Medicine2",
				statBonus = -DUPLICANTSTATS.LARGE_STATPOINT_BONUS,
				rarity = DUPLICANTSTATS.RARITY_EPIC,
				mutuallyExclusiveTraits = new List<string> { "Hemophobia" }
			}
		};

		// Token: 0x04005BF8 RID: 23544
		public static List<DUPLICANTSTATS.TraitVal> NEEDTRAITS = new List<DUPLICANTSTATS.TraitVal>
		{
			new DUPLICANTSTATS.TraitVal
			{
				id = "Claustrophobic",
				rarity = DUPLICANTSTATS.RARITY_COMMON
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "PrefersWarmer",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				mutuallyExclusiveTraits = new List<string> { "PrefersColder" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "PrefersColder",
				rarity = DUPLICANTSTATS.RARITY_COMMON,
				mutuallyExclusiveTraits = new List<string> { "PrefersWarmer" }
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "SensitiveFeet",
				rarity = DUPLICANTSTATS.RARITY_COMMON
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Fashionable",
				rarity = DUPLICANTSTATS.RARITY_COMMON
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "Climacophobic",
				rarity = DUPLICANTSTATS.RARITY_COMMON
			},
			new DUPLICANTSTATS.TraitVal
			{
				id = "SolitarySleeper",
				rarity = DUPLICANTSTATS.RARITY_COMMON
			}
		};

		// Token: 0x04005BF9 RID: 23545
		public static DUPLICANTSTATS STANDARD = new DUPLICANTSTATS();

		// Token: 0x04005BFA RID: 23546
		public static DUPLICANTSTATS BIONICS = new DUPLICANTSTATS
		{
			BaseStats = new DUPLICANTSTATS.BASESTATS
			{
				MAX_CALORIES = 0f
			},
			DiseaseImmunities = new DUPLICANTSTATS.DISEASEIMMUNITIES
			{
				IMMUNITIES = new string[] { "FoodSickness" }
			}
		};

		// Token: 0x04005BFB RID: 23547
		private static Dictionary<Tag, DUPLICANTSTATS> DUPLICANT_TYPES = new Dictionary<Tag, DUPLICANTSTATS>
		{
			{
				GameTags.Minions.Models.Standard,
				DUPLICANTSTATS.STANDARD
			},
			{
				GameTags.Minions.Models.Bionic,
				DUPLICANTSTATS.BIONICS
			}
		};

		// Token: 0x04005BFC RID: 23548
		public DUPLICANTSTATS.BASESTATS BaseStats = new DUPLICANTSTATS.BASESTATS();

		// Token: 0x04005BFD RID: 23549
		public DUPLICANTSTATS.DISEASEIMMUNITIES DiseaseImmunities = new DUPLICANTSTATS.DISEASEIMMUNITIES();

		// Token: 0x04005BFE RID: 23550
		public DUPLICANTSTATS.TEMPERATURE Temperature = new DUPLICANTSTATS.TEMPERATURE();

		// Token: 0x04005BFF RID: 23551
		public DUPLICANTSTATS.BREATH Breath = new DUPLICANTSTATS.BREATH();

		// Token: 0x04005C00 RID: 23552
		public DUPLICANTSTATS.LIGHT Light = new DUPLICANTSTATS.LIGHT();

		// Token: 0x04005C01 RID: 23553
		public DUPLICANTSTATS.COMBAT Combat = new DUPLICANTSTATS.COMBAT();

		// Token: 0x04005C02 RID: 23554
		public DUPLICANTSTATS.SECRETIONS Secretions = new DUPLICANTSTATS.SECRETIONS();

		// Token: 0x0200214A RID: 8522
		public static class RADIATION_DIFFICULTY_MODIFIERS
		{
			// Token: 0x04009862 RID: 39010
			public static float HARDEST = 0.33f;

			// Token: 0x04009863 RID: 39011
			public static float HARDER = 0.66f;

			// Token: 0x04009864 RID: 39012
			public static float DEFAULT = 1f;

			// Token: 0x04009865 RID: 39013
			public static float EASIER = 2f;

			// Token: 0x04009866 RID: 39014
			public static float EASIEST = 100f;
		}

		// Token: 0x0200214B RID: 8523
		public static class RADIATION_EXPOSURE_LEVELS
		{
			// Token: 0x04009867 RID: 39015
			public const float LOW = 100f;

			// Token: 0x04009868 RID: 39016
			public const float MODERATE = 300f;

			// Token: 0x04009869 RID: 39017
			public const float HIGH = 600f;

			// Token: 0x0400986A RID: 39018
			public const float DEADLY = 900f;
		}

		// Token: 0x0200214C RID: 8524
		public static class MOVEMENT_MODIFIERS
		{
			// Token: 0x0400986B RID: 39019
			public static float NEUTRAL = 1f;

			// Token: 0x0400986C RID: 39020
			public static float BONUS_1 = 1.1f;

			// Token: 0x0400986D RID: 39021
			public static float BONUS_2 = 1.25f;

			// Token: 0x0400986E RID: 39022
			public static float BONUS_3 = 1.5f;

			// Token: 0x0400986F RID: 39023
			public static float BONUS_4 = 1.75f;

			// Token: 0x04009870 RID: 39024
			public static float PENALTY_1 = 0.9f;

			// Token: 0x04009871 RID: 39025
			public static float PENALTY_2 = 0.75f;

			// Token: 0x04009872 RID: 39026
			public static float PENALTY_3 = 0.5f;

			// Token: 0x04009873 RID: 39027
			public static float PENALTY_4 = 0.25f;
		}

		// Token: 0x0200214D RID: 8525
		public static class QOL_STRESS
		{
			// Token: 0x04009874 RID: 39028
			public const float ABOVE_EXPECTATIONS = -0.016666668f;

			// Token: 0x04009875 RID: 39029
			public const float AT_EXPECTATIONS = -0.008333334f;

			// Token: 0x04009876 RID: 39030
			public const float MIN_STRESS = -0.033333335f;

			// Token: 0x02002922 RID: 10530
			public static class BELOW_EXPECTATIONS
			{
				// Token: 0x0400B5F7 RID: 46583
				public const float EASY = 0.0033333334f;

				// Token: 0x0400B5F8 RID: 46584
				public const float NEUTRAL = 0.004166667f;

				// Token: 0x0400B5F9 RID: 46585
				public const float HARD = 0.008333334f;

				// Token: 0x0400B5FA RID: 46586
				public const float VERYHARD = 0.016666668f;
			}

			// Token: 0x02002923 RID: 10531
			public static class MAX_STRESS
			{
				// Token: 0x0400B5FB RID: 46587
				public const float EASY = 0.016666668f;

				// Token: 0x0400B5FC RID: 46588
				public const float NEUTRAL = 0.041666668f;

				// Token: 0x0400B5FD RID: 46589
				public const float HARD = 0.05f;

				// Token: 0x0400B5FE RID: 46590
				public const float VERYHARD = 0.083333336f;
			}
		}

		// Token: 0x0200214E RID: 8526
		public static class CLOTHING
		{
			// Token: 0x02002924 RID: 10532
			public class DECOR_MODIFICATION
			{
				// Token: 0x0400B5FF RID: 46591
				public const int NEGATIVE_SIGNIFICANT = -30;

				// Token: 0x0400B600 RID: 46592
				public const int NEGATIVE_MILD = -10;

				// Token: 0x0400B601 RID: 46593
				public const int BASIC = -5;

				// Token: 0x0400B602 RID: 46594
				public const int POSITIVE_MILD = 10;

				// Token: 0x0400B603 RID: 46595
				public const int POSITIVE_SIGNIFICANT = 30;

				// Token: 0x0400B604 RID: 46596
				public const int POSITIVE_MAJOR = 40;
			}

			// Token: 0x02002925 RID: 10533
			public class CONDUCTIVITY_BARRIER_MODIFICATION
			{
				// Token: 0x0400B605 RID: 46597
				public const float THIN = 0.0005f;

				// Token: 0x0400B606 RID: 46598
				public const float BASIC = 0.0025f;

				// Token: 0x0400B607 RID: 46599
				public const float THICK = 0.008f;
			}

			// Token: 0x02002926 RID: 10534
			public class SWEAT_EFFICIENCY_MULTIPLIER
			{
				// Token: 0x0400B608 RID: 46600
				public const float DIMINISH_SIGNIFICANT = -2.5f;

				// Token: 0x0400B609 RID: 46601
				public const float DIMINISH_MILD = -1.25f;

				// Token: 0x0400B60A RID: 46602
				public const float NEUTRAL = 0f;

				// Token: 0x0400B60B RID: 46603
				public const float IMPROVE = 2f;
			}
		}

		// Token: 0x0200214F RID: 8527
		public static class NOISE
		{
			// Token: 0x04009877 RID: 39031
			public const int THRESHOLD_PEACEFUL = 0;

			// Token: 0x04009878 RID: 39032
			public const int THRESHOLD_QUIET = 36;

			// Token: 0x04009879 RID: 39033
			public const int THRESHOLD_TOSS_AND_TURN = 45;

			// Token: 0x0400987A RID: 39034
			public const int THRESHOLD_WAKE_UP = 60;

			// Token: 0x0400987B RID: 39035
			public const int THRESHOLD_MINOR_REACTION = 80;

			// Token: 0x0400987C RID: 39036
			public const int THRESHOLD_MAJOR_REACTION = 106;

			// Token: 0x0400987D RID: 39037
			public const int THRESHOLD_EXTREME_REACTION = 125;
		}

		// Token: 0x02002150 RID: 8528
		public static class ROOM
		{
			// Token: 0x0400987E RID: 39038
			public const float LABORATORY_RESEARCH_EFFICIENCY_BONUS = 0.1f;
		}

		// Token: 0x02002151 RID: 8529
		public class DISTRIBUTIONS
		{
			// Token: 0x0600B96B RID: 47467 RVA: 0x003EBF43 File Offset: 0x003EA143
			public static int[] GetRandomDistribution()
			{
				return DUPLICANTSTATS.DISTRIBUTIONS.TYPES[global::UnityEngine.Random.Range(0, DUPLICANTSTATS.DISTRIBUTIONS.TYPES.Count)];
			}

			// Token: 0x0400987F RID: 39039
			public static readonly List<int[]> TYPES = new List<int[]>
			{
				new int[] { 5, 4, 4, 3, 3, 2, 1 },
				new int[] { 5, 3, 2, 1 },
				new int[] { 5, 2, 2, 1 },
				new int[] { 5, 1 },
				new int[] { 5, 3, 1 },
				new int[] { 3, 3, 3, 3, 1 },
				new int[] { 4 },
				new int[] { 3 },
				new int[] { 2 },
				new int[] { 1 }
			};
		}

		// Token: 0x02002152 RID: 8530
		public struct TraitVal : IHasDlcRestrictions
		{
			// Token: 0x0600B96E RID: 47470 RVA: 0x003EC048 File Offset: 0x003EA248
			public string[] GetRequiredDlcIds()
			{
				return this.requiredDlcIds;
			}

			// Token: 0x0600B96F RID: 47471 RVA: 0x003EC050 File Offset: 0x003EA250
			public string[] GetForbiddenDlcIds()
			{
				return this.forbiddenDlcIds;
			}

			// Token: 0x04009880 RID: 39040
			public string id;

			// Token: 0x04009881 RID: 39041
			public int statBonus;

			// Token: 0x04009882 RID: 39042
			public int impact;

			// Token: 0x04009883 RID: 39043
			public int rarity;

			// Token: 0x04009884 RID: 39044
			public List<string> mutuallyExclusiveTraits;

			// Token: 0x04009885 RID: 39045
			public List<HashedString> mutuallyExclusiveAptitudes;

			// Token: 0x04009886 RID: 39046
			public bool doNotGenerateTrait;

			// Token: 0x04009887 RID: 39047
			public string[] requiredDlcIds;

			// Token: 0x04009888 RID: 39048
			public string[] forbiddenDlcIds;
		}

		// Token: 0x02002153 RID: 8531
		public class ATTRIBUTE_LEVELING
		{
			// Token: 0x04009889 RID: 39049
			public static int MAX_GAINED_ATTRIBUTE_LEVEL = 20;

			// Token: 0x0400988A RID: 39050
			public static int TARGET_MAX_LEVEL_CYCLE = 400;

			// Token: 0x0400988B RID: 39051
			public static float EXPERIENCE_LEVEL_POWER = 1.7f;

			// Token: 0x0400988C RID: 39052
			public static float FULL_EXPERIENCE = 1f;

			// Token: 0x0400988D RID: 39053
			public static float ALL_DAY_EXPERIENCE = DUPLICANTSTATS.ATTRIBUTE_LEVELING.FULL_EXPERIENCE / 0.8f;

			// Token: 0x0400988E RID: 39054
			public static float MOST_DAY_EXPERIENCE = DUPLICANTSTATS.ATTRIBUTE_LEVELING.FULL_EXPERIENCE / 0.5f;

			// Token: 0x0400988F RID: 39055
			public static float PART_DAY_EXPERIENCE = DUPLICANTSTATS.ATTRIBUTE_LEVELING.FULL_EXPERIENCE / 0.25f;

			// Token: 0x04009890 RID: 39056
			public static float BARELY_EVER_EXPERIENCE = DUPLICANTSTATS.ATTRIBUTE_LEVELING.FULL_EXPERIENCE / 0.1f;
		}

		// Token: 0x02002154 RID: 8532
		public class BASESTATS
		{
			// Token: 0x17000CBC RID: 3260
			// (get) Token: 0x0600B972 RID: 47474 RVA: 0x003EC0D2 File Offset: 0x003EA2D2
			public float CALORIES_BURNED_PER_SECOND
			{
				get
				{
					return this.CALORIES_BURNED_PER_CYCLE / 600f;
				}
			}

			// Token: 0x17000CBD RID: 3261
			// (get) Token: 0x0600B973 RID: 47475 RVA: 0x003EC0E0 File Offset: 0x003EA2E0
			public float HUNGRY_THRESHOLD
			{
				get
				{
					return this.SATISFIED_THRESHOLD - -this.CALORIES_BURNED_PER_CYCLE * 0.5f / this.MAX_CALORIES;
				}
			}

			// Token: 0x17000CBE RID: 3262
			// (get) Token: 0x0600B974 RID: 47476 RVA: 0x003EC0FD File Offset: 0x003EA2FD
			public float STARVING_THRESHOLD
			{
				get
				{
					return -this.CALORIES_BURNED_PER_CYCLE / this.MAX_CALORIES;
				}
			}

			// Token: 0x17000CBF RID: 3263
			// (get) Token: 0x0600B975 RID: 47477 RVA: 0x003EC10D File Offset: 0x003EA30D
			public float DUPLICANT_COOLING_KILOWATTS
			{
				get
				{
					return this.COOLING_EFFICIENCY * -this.CALORIES_BURNED_PER_SECOND * 0.001f * this.KCAL2JOULES / 1000f;
				}
			}

			// Token: 0x17000CC0 RID: 3264
			// (get) Token: 0x0600B976 RID: 47478 RVA: 0x003EC130 File Offset: 0x003EA330
			public float DUPLICANT_WARMING_KILOWATTS
			{
				get
				{
					return this.WARMING_EFFICIENCY * -this.CALORIES_BURNED_PER_SECOND * 0.001f * this.KCAL2JOULES / 1000f;
				}
			}

			// Token: 0x17000CC1 RID: 3265
			// (get) Token: 0x0600B977 RID: 47479 RVA: 0x003EC153 File Offset: 0x003EA353
			public float DUPLICANT_BASE_GENERATION_KILOWATTS
			{
				get
				{
					return this.HEAT_GENERATION_EFFICIENCY * -this.CALORIES_BURNED_PER_SECOND * 0.001f * this.KCAL2JOULES / 1000f;
				}
			}

			// Token: 0x17000CC2 RID: 3266
			// (get) Token: 0x0600B978 RID: 47480 RVA: 0x003EC176 File Offset: 0x003EA376
			public float GUESSTIMATE_CALORIES_BURNED_PER_SECOND
			{
				get
				{
					return this.CALORIES_BURNED_PER_CYCLE / 600f;
				}
			}

			// Token: 0x04009891 RID: 39057
			public float DEFAULT_MASS = 30f;

			// Token: 0x04009892 RID: 39058
			public float STAMINA_USED_PER_SECOND = -0.11666667f;

			// Token: 0x04009893 RID: 39059
			public float TRANSIT_TUBE_TRAVEL_SPEED = 18f;

			// Token: 0x04009894 RID: 39060
			public float OXYGEN_USED_PER_SECOND = 0.1f;

			// Token: 0x04009895 RID: 39061
			public float OXYGEN_TO_CO2_CONVERSION = 0.02f;

			// Token: 0x04009896 RID: 39062
			public float LOW_OXYGEN_THRESHOLD = 0.52f;

			// Token: 0x04009897 RID: 39063
			public float NO_OXYGEN_THRESHOLD = 0.05f;

			// Token: 0x04009898 RID: 39064
			public float RECOVER_BREATH_DELTA = 3f;

			// Token: 0x04009899 RID: 39065
			public float MIN_CO2_TO_EMIT = 0.02f;

			// Token: 0x0400989A RID: 39066
			public float BLADDER_INCREASE_PER_SECOND = 0.16666667f;

			// Token: 0x0400989B RID: 39067
			public float DECOR_EXPECTATION;

			// Token: 0x0400989C RID: 39068
			public float FOOD_QUALITY_EXPECTATION;

			// Token: 0x0400989D RID: 39069
			public float RECREATION_EXPECTATION = 2f;

			// Token: 0x0400989E RID: 39070
			public float MAX_PROFESSION_DECOR_EXPECTATION = 75f;

			// Token: 0x0400989F RID: 39071
			public float MAX_PROFESSION_FOOD_EXPECTATION;

			// Token: 0x040098A0 RID: 39072
			public int MAX_UNDERWATER_TRAVEL_COST = 8;

			// Token: 0x040098A1 RID: 39073
			public float TOILET_EFFICIENCY = 1f;

			// Token: 0x040098A2 RID: 39074
			public float ROOM_TEMPERATURE_PREFERENCE;

			// Token: 0x040098A3 RID: 39075
			public int BUILDING_DAMAGE_ACTING_OUT = 100;

			// Token: 0x040098A4 RID: 39076
			public float IMMUNE_LEVEL_MAX = 100f;

			// Token: 0x040098A5 RID: 39077
			public float IMMUNE_LEVEL_RECOVERY = 0.025f;

			// Token: 0x040098A6 RID: 39078
			public float CARRY_CAPACITY = 200f;

			// Token: 0x040098A7 RID: 39079
			public float HIT_POINTS = 100f;

			// Token: 0x040098A8 RID: 39080
			public float RADIATION_RESISTANCE;

			// Token: 0x040098A9 RID: 39081
			public string NAV_GRID_NAME = "MinionNavGrid";

			// Token: 0x040098AA RID: 39082
			public float KCAL2JOULES = 4184f;

			// Token: 0x040098AB RID: 39083
			public float MAX_CALORIES = 4000000f;

			// Token: 0x040098AC RID: 39084
			public float CALORIES_BURNED_PER_CYCLE = -1000000f;

			// Token: 0x040098AD RID: 39085
			public float SATISFIED_THRESHOLD = 0.95f;

			// Token: 0x040098AE RID: 39086
			public float COOLING_EFFICIENCY = 0.08f;

			// Token: 0x040098AF RID: 39087
			public float WARMING_EFFICIENCY = 0.08f;

			// Token: 0x040098B0 RID: 39088
			public float HEAT_GENERATION_EFFICIENCY = 0.012f;

			// Token: 0x040098B1 RID: 39089
			public float GUESSTIMATE_CALORIES_PER_CYCLE = -1600000f;
		}

		// Token: 0x02002155 RID: 8533
		public class DISEASEIMMUNITIES
		{
			// Token: 0x040098B2 RID: 39090
			public string[] IMMUNITIES;
		}

		// Token: 0x02002156 RID: 8534
		public class TEMPERATURE
		{
			// Token: 0x040098B3 RID: 39091
			public DUPLICANTSTATS.TEMPERATURE.EXTERNAL External = new DUPLICANTSTATS.TEMPERATURE.EXTERNAL();

			// Token: 0x040098B4 RID: 39092
			public DUPLICANTSTATS.TEMPERATURE.INTERNAL Internal = new DUPLICANTSTATS.TEMPERATURE.INTERNAL();

			// Token: 0x040098B5 RID: 39093
			public DUPLICANTSTATS.TEMPERATURE.CONDUCTIVITY_BARRIER_MODIFICATION Conductivity_Barrier_Modification = new DUPLICANTSTATS.TEMPERATURE.CONDUCTIVITY_BARRIER_MODIFICATION();

			// Token: 0x040098B6 RID: 39094
			public float SKIN_THICKNESS = 0.002f;

			// Token: 0x040098B7 RID: 39095
			public float SURFACE_AREA = 1f;

			// Token: 0x040098B8 RID: 39096
			public float GROUND_TRANSFER_SCALE;

			// Token: 0x02002927 RID: 10535
			public class EXTERNAL
			{
				// Token: 0x0400B60C RID: 46604
				public float THRESHOLD_COLD = 283.15f;

				// Token: 0x0400B60D RID: 46605
				public float THRESHOLD_HOT = 306.15f;

				// Token: 0x0400B60E RID: 46606
				public float THRESHOLD_SCALDING = 345f;
			}

			// Token: 0x02002928 RID: 10536
			public class INTERNAL
			{
				// Token: 0x0400B60F RID: 46607
				public float IDEAL = 310.15f;

				// Token: 0x0400B610 RID: 46608
				public float THRESHOLD_HYPOTHERMIA = 308.15f;

				// Token: 0x0400B611 RID: 46609
				public float THRESHOLD_HYPERTHERMIA = 312.15f;

				// Token: 0x0400B612 RID: 46610
				public float THRESHOLD_FATAL_HOT = 320.15f;

				// Token: 0x0400B613 RID: 46611
				public float THRESHOLD_FATAL_COLD = 300.15f;
			}

			// Token: 0x02002929 RID: 10537
			public class CONDUCTIVITY_BARRIER_MODIFICATION
			{
				// Token: 0x0400B614 RID: 46612
				public float SKINNY = -0.005f;

				// Token: 0x0400B615 RID: 46613
				public float PUDGY = 0.005f;
			}
		}

		// Token: 0x02002157 RID: 8535
		public class BREATH
		{
			// Token: 0x17000CC3 RID: 3267
			// (get) Token: 0x0600B97C RID: 47484 RVA: 0x003EC30B File Offset: 0x003EA50B
			public float RETREAT_AMOUNT
			{
				get
				{
					return this.RETREAT_AT_SECONDS / this.BREATH_BAR_TOTAL_SECONDS * this.BREATH_BAR_TOTAL_AMOUNT;
				}
			}

			// Token: 0x17000CC4 RID: 3268
			// (get) Token: 0x0600B97D RID: 47485 RVA: 0x003EC321 File Offset: 0x003EA521
			public float SUFFOCATE_AMOUNT
			{
				get
				{
					return this.SUFFOCATION_WARN_AT_SECONDS / this.BREATH_BAR_TOTAL_SECONDS * this.BREATH_BAR_TOTAL_AMOUNT;
				}
			}

			// Token: 0x17000CC5 RID: 3269
			// (get) Token: 0x0600B97E RID: 47486 RVA: 0x003EC337 File Offset: 0x003EA537
			public float BREATH_RATE
			{
				get
				{
					return this.BREATH_BAR_TOTAL_AMOUNT / this.BREATH_BAR_TOTAL_SECONDS;
				}
			}

			// Token: 0x040098B9 RID: 39097
			private float BREATH_BAR_TOTAL_SECONDS = 110f;

			// Token: 0x040098BA RID: 39098
			private float RETREAT_AT_SECONDS = 80f;

			// Token: 0x040098BB RID: 39099
			private float SUFFOCATION_WARN_AT_SECONDS = 50f;

			// Token: 0x040098BC RID: 39100
			public float BREATH_BAR_TOTAL_AMOUNT = 100f;
		}

		// Token: 0x02002158 RID: 8536
		public class LIGHT
		{
			// Token: 0x040098BD RID: 39101
			public int LUX_SUNBURN = 72000;

			// Token: 0x040098BE RID: 39102
			public float SUNBURN_DELAY_TIME = 120f;

			// Token: 0x040098BF RID: 39103
			public int LUX_PLEASANT_LIGHT = 40000;

			// Token: 0x040098C0 RID: 39104
			public float LIGHT_WORK_EFFICIENCY_BONUS = 0.15f;

			// Token: 0x040098C1 RID: 39105
			public int NO_LIGHT;

			// Token: 0x040098C2 RID: 39106
			public int VERY_LOW_LIGHT = 1;

			// Token: 0x040098C3 RID: 39107
			public int LOW_LIGHT = 500;

			// Token: 0x040098C4 RID: 39108
			public int MEDIUM_LIGHT = 1000;

			// Token: 0x040098C5 RID: 39109
			public int HIGH_LIGHT = 10000;

			// Token: 0x040098C6 RID: 39110
			public int VERY_HIGH_LIGHT = 50000;

			// Token: 0x040098C7 RID: 39111
			public int MAX_LIGHT = 100000;
		}

		// Token: 0x02002159 RID: 8537
		public class COMBAT
		{
			// Token: 0x040098C8 RID: 39112
			public DUPLICANTSTATS.COMBAT.BASICWEAPON BasicWeapon = new DUPLICANTSTATS.COMBAT.BASICWEAPON();

			// Token: 0x040098C9 RID: 39113
			public Health.HealthState FLEE_THRESHOLD = Health.HealthState.Critical;

			// Token: 0x0200292A RID: 10538
			public class BASICWEAPON
			{
				// Token: 0x0400B616 RID: 46614
				public float ATTACKS_PER_SECOND = 2f;

				// Token: 0x0400B617 RID: 46615
				public float MIN_DAMAGE_PER_HIT = 1f;

				// Token: 0x0400B618 RID: 46616
				public float MAX_DAMAGE_PER_HIT = 1f;

				// Token: 0x0400B619 RID: 46617
				public AttackProperties.TargetType TARGET_TYPE;

				// Token: 0x0400B61A RID: 46618
				public AttackProperties.DamageType DAMAGE_TYPE;

				// Token: 0x0400B61B RID: 46619
				public int MAX_HITS = 1;

				// Token: 0x0400B61C RID: 46620
				public float AREA_OF_EFFECT_RADIUS;
			}
		}

		// Token: 0x0200215A RID: 8538
		public class SECRETIONS
		{
			// Token: 0x040098CA RID: 39114
			public float PEE_FUSE_TIME = 120f;

			// Token: 0x040098CB RID: 39115
			public float PEE_PER_FLOOR_PEE = 2f;

			// Token: 0x040098CC RID: 39116
			public float PEE_PER_TOILET_PEE = 6.7f;

			// Token: 0x040098CD RID: 39117
			public string PEE_DISEASE = "FoodPoisoning";

			// Token: 0x040098CE RID: 39118
			public int DISEASE_PER_PEE = 100000;

			// Token: 0x040098CF RID: 39119
			public int DISEASE_PER_VOMIT = 100000;
		}
	}
}
