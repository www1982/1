using System;
using System.Collections.Generic;
using System.Text;
using Database;
using STRINGS;

// Token: 0x02000DC3 RID: 3523
public static class SearchUtil
{
	// Token: 0x06006F27 RID: 28455 RVA: 0x002A517C File Offset: 0x002A337C
	private static void CacheMeaninglessWords()
	{
		if (SearchUtil.MeaninglessWords.Count != 0)
		{
			return;
		}
		ListPool<string, SearchUtil.MatchCache>.PooledList pooledList = ListPool<string, SearchUtil.MatchCache>.Allocate();
		SearchUtil.AddCommaDelimitedSearchTerms(SEARCH_TERMS.SUPPRESSED, pooledList);
		foreach (string text in pooledList)
		{
			SearchUtil.MeaninglessWords.Add(text);
		}
		pooledList.Recycle();
	}

	// Token: 0x06006F28 RID: 28456 RVA: 0x002A51F8 File Offset: 0x002A33F8
	public static bool IsPassingScore(int score)
	{
		return score >= 79;
	}

	// Token: 0x06006F29 RID: 28457 RVA: 0x002A5202 File Offset: 0x002A3402
	public static string Canonicalize(string s)
	{
		return FuzzySearch.Canonicalize(s).ToUpper();
	}

	// Token: 0x06006F2A RID: 28458 RVA: 0x002A5210 File Offset: 0x002A3410
	public static string CanonicalizePhrase(string s)
	{
		string text = FuzzySearch.Canonicalize(s).ToUpper();
		FuzzySearch.Features features = FuzzySearch.GetFeatures();
		if ((features & (FuzzySearch.Features.Suppress1And2LetterWords | FuzzySearch.Features.SuppressMeaninglessWords)) == (FuzzySearch.Features)0)
		{
			return text;
		}
		string[] array = text.Split(FuzzySearch.TOKEN_SEPARATORS);
		StringBuilder stringBuilder = new StringBuilder();
		bool flag = (features & FuzzySearch.Features.Suppress1And2LetterWords) > (FuzzySearch.Features)0;
		bool flag2 = (features & FuzzySearch.Features.SuppressMeaninglessWords) > (FuzzySearch.Features)0;
		if (flag2)
		{
			SearchUtil.CacheMeaninglessWords();
		}
		foreach (string text2 in array)
		{
			if ((!flag || text2.Length > 2) && (!flag2 || !SearchUtil.MeaninglessWords.Contains(text2)))
			{
				if (stringBuilder.Length != 0)
				{
					stringBuilder.AppendFormat(" {0}", text2);
				}
				else
				{
					stringBuilder.Append(text2);
				}
			}
		}
		return stringBuilder.ToString();
	}

	// Token: 0x06006F2B RID: 28459 RVA: 0x002A52D0 File Offset: 0x002A34D0
	public static void AddCommaDelimitedSearchTerms(string commaDelimitedSearchTerms, List<string> searchTerms)
	{
		foreach (string text in commaDelimitedSearchTerms.ToUpper().Split(SearchUtil.COMMA_DELIMETERS, StringSplitOptions.RemoveEmptyEntries))
		{
			searchTerms.Add(text);
		}
	}

	// Token: 0x06006F2C RID: 28460 RVA: 0x002A5308 File Offset: 0x002A3508
	public static Dictionary<string, SearchUtil.TechCache> CacheTechs()
	{
		Dictionary<string, SearchUtil.TechCache> dictionary = new Dictionary<string, SearchUtil.TechCache>();
		ListPool<ComplexRecipe, SearchUtil.TechCache>.PooledList pooledList = ListPool<ComplexRecipe, SearchUtil.TechCache>.Allocate();
		Techs techs = Db.Get().Techs;
		for (int num = 0; num != techs.Count; num++)
		{
			Tech tech = (Tech)techs.GetResource(num);
			Dictionary<string, SearchUtil.TechItemCache> dictionary2 = new Dictionary<string, SearchUtil.TechItemCache>();
			foreach (TechItem techItem in tech.unlockedItems)
			{
				pooledList.Clear();
				BuildingDef.CollectFabricationRecipes(techItem.Id, pooledList);
				List<SearchUtil.NameDescCache> list = new List<SearchUtil.NameDescCache>();
				foreach (ComplexRecipe complexRecipe in pooledList)
				{
					list.Add(new SearchUtil.NameDescCache
					{
						name = new SearchUtil.MatchCache
						{
							text = SearchUtil.Canonicalize(complexRecipe.GetUIName(false))
						},
						desc = new SearchUtil.MatchCache
						{
							text = SearchUtil.CanonicalizePhrase(complexRecipe.description)
						}
					});
				}
				TechItem techItem2 = Db.Get().TechItems.Get(techItem.Id);
				SearchUtil.TechItemCache techItemCache = new SearchUtil.TechItemCache
				{
					nameDescSearchTerms = new SearchUtil.NameDescSearchTermsCache
					{
						nameDesc = new SearchUtil.NameDescCache
						{
							name = new SearchUtil.MatchCache
							{
								text = SearchUtil.Canonicalize(techItem2.Name)
							},
							desc = new SearchUtil.MatchCache
							{
								text = SearchUtil.CanonicalizePhrase(techItem2.description)
							}
						},
						searchTerms = techItem2.searchTerms
					},
					recipes = list,
					tier = tech.tier
				};
				dictionary2[techItem.Id] = techItemCache;
			}
			SearchUtil.TechCache techCache = new SearchUtil.TechCache
			{
				tech = new SearchUtil.NameDescSearchTermsCache
				{
					nameDesc = new SearchUtil.NameDescCache
					{
						name = new SearchUtil.MatchCache
						{
							text = SearchUtil.Canonicalize(tech.Name)
						},
						desc = new SearchUtil.MatchCache
						{
							text = SearchUtil.CanonicalizePhrase(tech.desc)
						}
					},
					searchTerms = tech.searchTerms
				},
				techItems = dictionary2,
				tier = tech.tier
			};
			dictionary[tech.Id] = techCache;
		}
		pooledList.Recycle();
		return dictionary;
	}

	// Token: 0x06006F2D RID: 28461 RVA: 0x002A5588 File Offset: 0x002A3788
	public static SearchUtil.BuildingDefCache MakeBuildingDefCache(BuildingDef def)
	{
		SearchUtil.NameDescSearchTermsCache nameDescSearchTermsCache = new SearchUtil.NameDescSearchTermsCache
		{
			nameDesc = new SearchUtil.NameDescCache
			{
				name = new SearchUtil.MatchCache
				{
					text = SearchUtil.Canonicalize(def.Name)
				},
				desc = new SearchUtil.MatchCache
				{
					text = SearchUtil.CanonicalizePhrase(def.Desc)
				}
			},
			searchTerms = def.SearchTerms
		};
		SearchUtil.MatchCache matchCache = new SearchUtil.MatchCache
		{
			text = SearchUtil.CanonicalizePhrase(def.Effect)
		};
		List<SearchUtil.NameDescCache> list = new List<SearchUtil.NameDescCache>();
		ListPool<ComplexRecipe, PlanBuildingToggle>.PooledList pooledList = ListPool<ComplexRecipe, PlanBuildingToggle>.Allocate();
		BuildingDef.CollectFabricationRecipes(def.PrefabID, pooledList);
		foreach (ComplexRecipe complexRecipe in pooledList)
		{
			list.Add(new SearchUtil.NameDescCache
			{
				name = new SearchUtil.MatchCache
				{
					text = SearchUtil.Canonicalize(complexRecipe.GetUIName(false))
				},
				desc = new SearchUtil.MatchCache
				{
					text = SearchUtil.CanonicalizePhrase(complexRecipe.description)
				}
			});
		}
		pooledList.Recycle();
		return new SearchUtil.BuildingDefCache
		{
			nameDescSearchTerms = nameDescSearchTermsCache,
			effect = matchCache,
			recipes = list,
			techTier = Db.Get().TechItems.GetTechTierForItem(def.PrefabID)
		};
	}

	// Token: 0x04004C6F RID: 19567
	public const int MATCH_SCORE_MIN = 0;

	// Token: 0x04004C70 RID: 19568
	public const int MATCH_SCORE_MAX = 100;

	// Token: 0x04004C71 RID: 19569
	public const int MATCH_SCORE_THRESHOLD = 79;

	// Token: 0x04004C72 RID: 19570
	private static readonly HashSet<string> MeaninglessWords = new HashSet<string>();

	// Token: 0x04004C73 RID: 19571
	private static readonly char[] COMMA_DELIMETERS = new char[] { ' ', ',' };

	// Token: 0x04004C74 RID: 19572
	private const int LHS_GT_RHS = -1;

	// Token: 0x04004C75 RID: 19573
	private const int RHS_GT_LHS = 1;

	// Token: 0x02001FDE RID: 8158
	private interface IScore
	{
		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x0600B491 RID: 46225
		int Score { get; }
	}

	// Token: 0x02001FDF RID: 8159
	private struct TieBreaker
	{
		// Token: 0x0600B492 RID: 46226 RVA: 0x003DD623 File Offset: 0x003DB823
		public TieBreaker(int _globalMax)
		{
			this.globalMax = _globalMax;
			this.globalMaxCmp = 0;
			this.localMaxScore = -1;
			this.localMaxCmp = 0;
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x0600B493 RID: 46227 RVA: 0x003DD641 File Offset: 0x003DB841
		public readonly bool IsTieBroken
		{
			get
			{
				return this.globalMaxCmp != 0;
			}
		}

		// Token: 0x0600B494 RID: 46228 RVA: 0x003DD64C File Offset: 0x003DB84C
		private int CacheLocalScore(int score, int cmp)
		{
			if (this.localMaxScore == -1 || this.localMaxScore < score)
			{
				this.localMaxScore = score;
				this.localMaxCmp = cmp;
			}
			return this.localMaxCmp;
		}

		// Token: 0x0600B495 RID: 46229 RVA: 0x003DD674 File Offset: 0x003DB874
		private int CacheScore(int score, int cmp)
		{
			if (score == this.globalMax)
			{
				this.globalMaxCmp = cmp;
				return this.globalMaxCmp;
			}
			return this.CacheLocalScore(score, cmp);
		}

		// Token: 0x0600B496 RID: 46230 RVA: 0x003DD698 File Offset: 0x003DB898
		public int Consider(int lhs, int rhs)
		{
			if (this.IsTieBroken)
			{
				return this.globalMaxCmp;
			}
			switch (-lhs.CompareTo(rhs))
			{
			case -1:
				return this.CacheScore(lhs, -1);
			case 0:
				if (this.localMaxScore != -1)
				{
					return this.localMaxCmp;
				}
				return 0;
			case 1:
				return this.CacheScore(rhs, 1);
			default:
				Debug.Assert(false);
				return 0;
			}
		}

		// Token: 0x0600B497 RID: 46231 RVA: 0x003DD700 File Offset: 0x003DB900
		public int Consider<T>(T lhs, T rhs) where T : IComparable, SearchUtil.IScore
		{
			if (this.IsTieBroken)
			{
				return this.globalMaxCmp;
			}
			if (lhs == null)
			{
				if (rhs != null)
				{
					return this.CacheScore(rhs.Score, 1);
				}
				if (this.localMaxScore != -1)
				{
					return this.localMaxCmp;
				}
				return 0;
			}
			else
			{
				if (rhs == null)
				{
					return this.CacheScore(lhs.Score, -1);
				}
				switch (lhs.CompareTo(rhs))
				{
				case -1:
					return this.CacheScore(lhs.Score, -1);
				case 0:
					if (this.localMaxScore != -1)
					{
						return this.localMaxCmp;
					}
					return 0;
				case 1:
					return this.CacheScore(rhs.Score, 1);
				default:
					Debug.Assert(false);
					return 0;
				}
			}
		}

		// Token: 0x0400926F RID: 37487
		private readonly int globalMax;

		// Token: 0x04009270 RID: 37488
		private int globalMaxCmp;

		// Token: 0x04009271 RID: 37489
		private int localMaxScore;

		// Token: 0x04009272 RID: 37490
		private int localMaxCmp;
	}

	// Token: 0x02001FE0 RID: 8160
	public class MatchCache : IComparable, SearchUtil.IScore
	{
		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x0600B498 RID: 46232 RVA: 0x003DD7DD File Offset: 0x003DB9DD
		public int Score
		{
			get
			{
				return this.FuzzyMatch.score;
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x0600B499 RID: 46233 RVA: 0x003DD7EA File Offset: 0x003DB9EA
		// (set) Token: 0x0600B49A RID: 46234 RVA: 0x003DD7F2 File Offset: 0x003DB9F2
		public FuzzySearch.Match FuzzyMatch { get; private set; }

		// Token: 0x0600B49B RID: 46235 RVA: 0x003DD7FC File Offset: 0x003DB9FC
		public void Bind(string searchStringUpper)
		{
			try
			{
				this.FuzzyMatch = FuzzySearch.ScoreCanonicalCandidate(searchStringUpper, this.text, null);
			}
			catch (Exception ex)
			{
				throw new Exception("searchStringUpper: " + searchStringUpper + ", text: " + this.text, ex);
			}
		}

		// Token: 0x0600B49C RID: 46236 RVA: 0x003DD84C File Offset: 0x003DBA4C
		public void Reset()
		{
			this.FuzzyMatch = FuzzySearch.Match.NONE;
		}

		// Token: 0x0600B49D RID: 46237 RVA: 0x003DD85C File Offset: 0x003DBA5C
		public int CompareTo(object obj)
		{
			SearchUtil.MatchCache matchCache = (SearchUtil.MatchCache)obj;
			return -this.Score.CompareTo(matchCache.Score);
		}

		// Token: 0x04009273 RID: 37491
		public string text;
	}

	// Token: 0x02001FE1 RID: 8161
	public class NameDescCache : IComparable, SearchUtil.IScore
	{
		// Token: 0x0600B49F RID: 46239 RVA: 0x003DD88D File Offset: 0x003DBA8D
		public void Bind(string searchStringUpper)
		{
			this.name.Bind(searchStringUpper);
			this.desc.Bind(searchStringUpper);
		}

		// Token: 0x0600B4A0 RID: 46240 RVA: 0x003DD8A7 File Offset: 0x003DBAA7
		public void Reset()
		{
			this.name.Reset();
			this.desc.Reset();
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x0600B4A1 RID: 46241 RVA: 0x003DD8BF File Offset: 0x003DBABF
		public int Score
		{
			get
			{
				return Math.Max(this.name.Score, this.desc.Score);
			}
		}

		// Token: 0x0600B4A2 RID: 46242 RVA: 0x003DD8DC File Offset: 0x003DBADC
		public int CompareTo(object obj)
		{
			SearchUtil.NameDescCache nameDescCache = (SearchUtil.NameDescCache)obj;
			int score = this.Score;
			int score2 = nameDescCache.Score;
			int num = -score.CompareTo(score2);
			if (num != 0)
			{
				return num;
			}
			SearchUtil.TieBreaker tieBreaker = new SearchUtil.TieBreaker(score);
			tieBreaker.Consider<SearchUtil.MatchCache>(this.name, nameDescCache.name);
			return tieBreaker.Consider<SearchUtil.MatchCache>(this.desc, nameDescCache.desc);
		}

		// Token: 0x04009275 RID: 37493
		public SearchUtil.MatchCache name;

		// Token: 0x04009276 RID: 37494
		public SearchUtil.MatchCache desc;
	}

	// Token: 0x02001FE2 RID: 8162
	public class NameDescSearchTermsCache : IComparable, SearchUtil.IScore
	{
		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x0600B4A4 RID: 46244 RVA: 0x003DD944 File Offset: 0x003DBB44
		// (set) Token: 0x0600B4A5 RID: 46245 RVA: 0x003DD94C File Offset: 0x003DBB4C
		public FuzzySearch.Match SearchTermsScore { get; private set; }

		// Token: 0x0600B4A6 RID: 46246 RVA: 0x003DD955 File Offset: 0x003DBB55
		public void Bind(string searchStringUpper)
		{
			this.nameDesc.Bind(searchStringUpper);
			this.SearchTermsScore = FuzzySearch.ScoreTokens(searchStringUpper, this.searchTerms);
		}

		// Token: 0x0600B4A7 RID: 46247 RVA: 0x003DD975 File Offset: 0x003DBB75
		public void Reset()
		{
			this.nameDesc.Reset();
			this.SearchTermsScore = FuzzySearch.Match.NONE;
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x0600B4A8 RID: 46248 RVA: 0x003DD98D File Offset: 0x003DBB8D
		public int Score
		{
			get
			{
				return Math.Max(this.nameDesc.Score, this.SearchTermsScore.score);
			}
		}

		// Token: 0x0600B4A9 RID: 46249 RVA: 0x003DD9AA File Offset: 0x003DBBAA
		public bool IsPassingScore()
		{
			return this.Score >= 79;
		}

		// Token: 0x0600B4AA RID: 46250 RVA: 0x003DD9BC File Offset: 0x003DBBBC
		public int CompareTo(object obj)
		{
			SearchUtil.NameDescSearchTermsCache nameDescSearchTermsCache = (SearchUtil.NameDescSearchTermsCache)obj;
			int score = this.Score;
			int score2 = nameDescSearchTermsCache.Score;
			int num = -score.CompareTo(score2);
			if (num != 0)
			{
				return num;
			}
			SearchUtil.TieBreaker tieBreaker = new SearchUtil.TieBreaker(score);
			tieBreaker.Consider<SearchUtil.MatchCache>(this.nameDesc.name, nameDescSearchTermsCache.nameDesc.name);
			tieBreaker.Consider(this.SearchTermsScore.score, nameDescSearchTermsCache.SearchTermsScore.score);
			return tieBreaker.Consider<SearchUtil.MatchCache>(this.nameDesc.desc, nameDescSearchTermsCache.nameDesc.desc);
		}

		// Token: 0x04009277 RID: 37495
		public SearchUtil.NameDescCache nameDesc;

		// Token: 0x04009278 RID: 37496
		public IReadOnlyList<string> searchTerms;
	}

	// Token: 0x02001FE3 RID: 8163
	public class BuildingDefCache : IComparable, SearchUtil.IScore
	{
		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x0600B4AC RID: 46252 RVA: 0x003DDA56 File Offset: 0x003DBC56
		// (set) Token: 0x0600B4AD RID: 46253 RVA: 0x003DDA5E File Offset: 0x003DBC5E
		public SearchUtil.NameDescCache BestRecipe { get; private set; }

		// Token: 0x0600B4AE RID: 46254 RVA: 0x003DDA68 File Offset: 0x003DBC68
		public void Bind(string searchStringUpper)
		{
			this.nameDescSearchTerms.Bind(searchStringUpper);
			this.effect.Bind(searchStringUpper);
			this.BestRecipe = null;
			foreach (SearchUtil.NameDescCache nameDescCache in this.recipes)
			{
				nameDescCache.Bind(searchStringUpper);
				if (this.BestRecipe == null || nameDescCache.CompareTo(this.BestRecipe) == -1)
				{
					this.BestRecipe = nameDescCache;
				}
			}
		}

		// Token: 0x0600B4AF RID: 46255 RVA: 0x003DDAF8 File Offset: 0x003DBCF8
		public void Reset()
		{
			this.nameDescSearchTerms.Reset();
			this.effect.Reset();
			foreach (SearchUtil.NameDescCache nameDescCache in this.recipes)
			{
				nameDescCache.Reset();
			}
			this.BestRecipe = null;
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x0600B4B0 RID: 46256 RVA: 0x003DDB68 File Offset: 0x003DBD68
		public int Score
		{
			get
			{
				return Math.Max(this.nameDescSearchTerms.Score, Math.Max(this.effect.Score, (this.BestRecipe == null) ? 0 : this.BestRecipe.Score));
			}
		}

		// Token: 0x0600B4B1 RID: 46257 RVA: 0x003DDBA0 File Offset: 0x003DBDA0
		public bool IsPassingScore()
		{
			return this.Score >= 79;
		}

		// Token: 0x0600B4B2 RID: 46258 RVA: 0x003DDBB0 File Offset: 0x003DBDB0
		public int CompareTo(object obj)
		{
			SearchUtil.BuildingDefCache buildingDefCache = (SearchUtil.BuildingDefCache)obj;
			int score = this.Score;
			int score2 = buildingDefCache.Score;
			int num = -score.CompareTo(score2);
			if (num != 0)
			{
				return num;
			}
			SearchUtil.TieBreaker tieBreaker = new SearchUtil.TieBreaker(score);
			tieBreaker.Consider<SearchUtil.MatchCache>(this.nameDescSearchTerms.nameDesc.name, buildingDefCache.nameDescSearchTerms.nameDesc.name);
			tieBreaker.Consider(this.nameDescSearchTerms.SearchTermsScore.score, buildingDefCache.nameDescSearchTerms.SearchTermsScore.score);
			if (!tieBreaker.IsTieBroken)
			{
				int num2 = this.techTier.CompareTo(buildingDefCache.techTier);
				if (num2 != 0)
				{
					return num2;
				}
			}
			tieBreaker.Consider<SearchUtil.MatchCache>(this.effect, buildingDefCache.effect);
			return tieBreaker.Consider<SearchUtil.MatchCache>(this.nameDescSearchTerms.nameDesc.desc, buildingDefCache.nameDescSearchTerms.nameDesc.desc);
		}

		// Token: 0x0400927A RID: 37498
		public SearchUtil.NameDescSearchTermsCache nameDescSearchTerms;

		// Token: 0x0400927B RID: 37499
		public SearchUtil.MatchCache effect;

		// Token: 0x0400927C RID: 37500
		public List<SearchUtil.NameDescCache> recipes;

		// Token: 0x0400927E RID: 37502
		public int techTier;
	}

	// Token: 0x02001FE4 RID: 8164
	public class TechItemCache : IComparable, SearchUtil.IScore
	{
		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x0600B4B4 RID: 46260 RVA: 0x003DDC9F File Offset: 0x003DBE9F
		// (set) Token: 0x0600B4B5 RID: 46261 RVA: 0x003DDCA7 File Offset: 0x003DBEA7
		public SearchUtil.NameDescCache BestRecipe { get; private set; }

		// Token: 0x0600B4B6 RID: 46262 RVA: 0x003DDCB0 File Offset: 0x003DBEB0
		public void Bind(string searchStringUpper)
		{
			this.nameDescSearchTerms.Bind(searchStringUpper);
			this.BestRecipe = null;
			foreach (SearchUtil.NameDescCache nameDescCache in this.recipes)
			{
				nameDescCache.Bind(searchStringUpper);
				if (this.BestRecipe == null || nameDescCache.CompareTo(this.BestRecipe) == -1)
				{
					this.BestRecipe = nameDescCache;
				}
			}
		}

		// Token: 0x0600B4B7 RID: 46263 RVA: 0x003DDD34 File Offset: 0x003DBF34
		public void Reset()
		{
			this.nameDescSearchTerms.Reset();
			foreach (SearchUtil.NameDescCache nameDescCache in this.recipes)
			{
				nameDescCache.Reset();
			}
			this.BestRecipe = null;
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x0600B4B8 RID: 46264 RVA: 0x003DDD98 File Offset: 0x003DBF98
		public int Score
		{
			get
			{
				return Math.Max(this.nameDescSearchTerms.Score, (this.BestRecipe == null) ? 0 : this.BestRecipe.Score);
			}
		}

		// Token: 0x0600B4B9 RID: 46265 RVA: 0x003DDDC0 File Offset: 0x003DBFC0
		public bool IsPassingScore()
		{
			return this.Score >= 79;
		}

		// Token: 0x0600B4BA RID: 46266 RVA: 0x003DDDD0 File Offset: 0x003DBFD0
		public int CompareTo(object obj)
		{
			SearchUtil.TechItemCache techItemCache = (SearchUtil.TechItemCache)obj;
			int score = this.Score;
			int score2 = techItemCache.Score;
			int num = -score.CompareTo(score2);
			if (num != 0)
			{
				return num;
			}
			SearchUtil.TieBreaker tieBreaker = new SearchUtil.TieBreaker(score);
			tieBreaker.Consider<SearchUtil.MatchCache>(this.nameDescSearchTerms.nameDesc.name, techItemCache.nameDescSearchTerms.nameDesc.name);
			tieBreaker.Consider(this.nameDescSearchTerms.SearchTermsScore.score, techItemCache.nameDescSearchTerms.SearchTermsScore.score);
			if (!tieBreaker.IsTieBroken)
			{
				int num2 = this.tier.CompareTo(techItemCache.tier);
				if (num2 != 0)
				{
					return num2;
				}
			}
			tieBreaker.Consider<SearchUtil.MatchCache>(this.nameDescSearchTerms.nameDesc.desc, techItemCache.nameDescSearchTerms.nameDesc.desc);
			return tieBreaker.Consider<SearchUtil.NameDescCache>(this.BestRecipe, techItemCache.BestRecipe);
		}

		// Token: 0x0400927F RID: 37503
		public SearchUtil.NameDescSearchTermsCache nameDescSearchTerms;

		// Token: 0x04009280 RID: 37504
		public List<SearchUtil.NameDescCache> recipes;

		// Token: 0x04009282 RID: 37506
		public int tier;
	}

	// Token: 0x02001FE5 RID: 8165
	public class TechCache : IComparable
	{
		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x0600B4BC RID: 46268 RVA: 0x003DDEBF File Offset: 0x003DC0BF
		// (set) Token: 0x0600B4BD RID: 46269 RVA: 0x003DDEC7 File Offset: 0x003DC0C7
		public SearchUtil.TechItemCache BestItem { get; private set; }

		// Token: 0x0600B4BE RID: 46270 RVA: 0x003DDED0 File Offset: 0x003DC0D0
		public void Bind(string searchStringUpper)
		{
			this.tech.Bind(searchStringUpper);
			this.BestItem = null;
			foreach (KeyValuePair<string, SearchUtil.TechItemCache> keyValuePair in this.techItems)
			{
				keyValuePair.Value.Bind(searchStringUpper);
				if (this.BestItem == null || keyValuePair.Value.CompareTo(this.BestItem) == -1)
				{
					this.BestItem = keyValuePair.Value;
				}
			}
		}

		// Token: 0x0600B4BF RID: 46271 RVA: 0x003DDF68 File Offset: 0x003DC168
		public void Reset()
		{
			this.tech.Reset();
			foreach (KeyValuePair<string, SearchUtil.TechItemCache> keyValuePair in this.techItems)
			{
				keyValuePair.Value.Reset();
			}
			this.BestItem = null;
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x0600B4C0 RID: 46272 RVA: 0x003DDFD4 File Offset: 0x003DC1D4
		public int Score
		{
			get
			{
				return Math.Max(this.tech.Score, (this.BestItem == null) ? 0 : this.BestItem.Score);
			}
		}

		// Token: 0x0600B4C1 RID: 46273 RVA: 0x003DDFFC File Offset: 0x003DC1FC
		public bool IsPassingScore()
		{
			return this.Score >= 79;
		}

		// Token: 0x0600B4C2 RID: 46274 RVA: 0x003DE00C File Offset: 0x003DC20C
		public int CompareTo(object obj)
		{
			SearchUtil.TechCache techCache = (SearchUtil.TechCache)obj;
			int score = this.Score;
			int score2 = techCache.Score;
			int num = -score.CompareTo(score2);
			if (num != 0)
			{
				return num;
			}
			SearchUtil.TieBreaker tieBreaker = new SearchUtil.TieBreaker(score);
			tieBreaker.Consider<SearchUtil.MatchCache>(this.tech.nameDesc.name, techCache.tech.nameDesc.name);
			tieBreaker.Consider(this.tech.SearchTermsScore.score, techCache.tech.SearchTermsScore.score);
			if (!tieBreaker.IsTieBroken)
			{
				int num2 = this.tier.CompareTo(techCache.tier);
				if (num2 != 0)
				{
					return num2;
				}
			}
			tieBreaker.Consider<SearchUtil.MatchCache>(this.tech.nameDesc.desc, techCache.tech.nameDesc.desc);
			return tieBreaker.Consider<SearchUtil.TechItemCache>(this.BestItem, techCache.BestItem);
		}

		// Token: 0x04009283 RID: 37507
		public SearchUtil.NameDescSearchTermsCache tech;

		// Token: 0x04009284 RID: 37508
		public Dictionary<string, SearchUtil.TechItemCache> techItems;

		// Token: 0x04009286 RID: 37510
		public int tier;
	}

	// Token: 0x02001FE6 RID: 8166
	public class SubcategoryCache : IComparable
	{
		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x0600B4C4 RID: 46276 RVA: 0x003DE0FB File Offset: 0x003DC2FB
		// (set) Token: 0x0600B4C5 RID: 46277 RVA: 0x003DE103 File Offset: 0x003DC303
		public SearchUtil.BuildingDefCache BestBuildingDef { get; private set; }

		// Token: 0x0600B4C6 RID: 46278 RVA: 0x003DE10C File Offset: 0x003DC30C
		public void Bind(string searchStringUpper)
		{
			this.subcategory.Bind(searchStringUpper);
			this.BestBuildingDef = null;
			foreach (SearchUtil.BuildingDefCache buildingDefCache in this.buildingDefs)
			{
				buildingDefCache.Bind(searchStringUpper);
				if (this.BestBuildingDef == null || buildingDefCache.CompareTo(this.BestBuildingDef) == -1)
				{
					this.BestBuildingDef = buildingDefCache;
				}
			}
		}

		// Token: 0x0600B4C7 RID: 46279 RVA: 0x003DE190 File Offset: 0x003DC390
		public void Reset()
		{
			this.subcategory.Reset();
			foreach (SearchUtil.BuildingDefCache buildingDefCache in this.buildingDefs)
			{
				buildingDefCache.Reset();
			}
			this.BestBuildingDef = null;
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x0600B4C8 RID: 46280 RVA: 0x003DE1F4 File Offset: 0x003DC3F4
		public int Score
		{
			get
			{
				return Math.Max(this.subcategory.Score, (this.BestBuildingDef == null) ? 0 : this.BestBuildingDef.Score);
			}
		}

		// Token: 0x0600B4C9 RID: 46281 RVA: 0x003DE21C File Offset: 0x003DC41C
		public bool IsPassingScore()
		{
			return this.Score >= 79;
		}

		// Token: 0x0600B4CA RID: 46282 RVA: 0x003DE22C File Offset: 0x003DC42C
		public int CompareTo(object obj)
		{
			SearchUtil.SubcategoryCache subcategoryCache = (SearchUtil.SubcategoryCache)obj;
			int score = this.Score;
			int score2 = subcategoryCache.Score;
			int num = -score.CompareTo(score2);
			if (num != 0)
			{
				return num;
			}
			SearchUtil.TieBreaker tieBreaker = new SearchUtil.TieBreaker(score);
			tieBreaker.Consider<SearchUtil.MatchCache>(this.subcategory, subcategoryCache.subcategory);
			return tieBreaker.Consider<SearchUtil.BuildingDefCache>(this.BestBuildingDef, subcategoryCache.BestBuildingDef);
		}

		// Token: 0x04009287 RID: 37511
		public SearchUtil.MatchCache subcategory;

		// Token: 0x04009288 RID: 37512
		public HashSet<SearchUtil.BuildingDefCache> buildingDefs;
	}
}
