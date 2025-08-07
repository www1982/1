using System;
using System.Collections.Generic;
using FuzzySharp;
using STRINGS;

// Token: 0x0200044F RID: 1103
public class FuzzySearch
{
	// Token: 0x060016E4 RID: 5860 RVA: 0x00081888 File Offset: 0x0007FA88
	public static FuzzySearch.Features GetFeatures()
	{
		FuzzySearch.Features features = FuzzySearch.Features.Initialism;
		if (Localization.GetLocale() == null)
		{
			features |= FuzzySearch.Features.Suppress1And2LetterWords;
			features |= FuzzySearch.Features.SuppressMeaninglessWords;
		}
		return features;
	}

	// Token: 0x060016E5 RID: 5861 RVA: 0x000818A7 File Offset: 0x0007FAA7
	public static string Canonicalize(string s)
	{
		return UI.StripLinkFormatting(UI.StripStyleFormatting(s));
	}

	// Token: 0x060016E6 RID: 5862 RVA: 0x000818B4 File Offset: 0x0007FAB4
	private static int ScoreImpl_Unchecked(string searchString, string candidate)
	{
		return Fuzz.Ratio(searchString, candidate);
	}

	// Token: 0x060016E7 RID: 5863 RVA: 0x000818BD File Offset: 0x0007FABD
	private static int ScoreImpl(string searchString, string candidate)
	{
		return FuzzySearch.ScoreImpl_Unchecked(searchString, candidate);
	}

	// Token: 0x060016E8 RID: 5864 RVA: 0x000818C8 File Offset: 0x0007FAC8
	private static bool IsUpper(string s)
	{
		foreach (char c in s)
		{
			if (char.IsLetter(c) && !char.IsUpper(c))
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x060016E9 RID: 5865 RVA: 0x00081904 File Offset: 0x0007FB04
	private static FuzzySearch.Match ScoreTokens_Unchecked(string searchStringUpper, string[] tokens)
	{
		if (tokens.Length == 0)
		{
			return FuzzySearch.Match.NONE;
		}
		int? num = null;
		string text = null;
		int i = 0;
		while (i < tokens.Length)
		{
			string text2 = tokens[i];
			int num2 = FuzzySearch.ScoreImpl_Unchecked(searchStringUpper, text2);
			if (num == null)
			{
				goto IL_004A;
			}
			int num3 = num2;
			int? num4 = num;
			if ((num3 > num4.GetValueOrDefault()) & (num4 != null))
			{
				goto IL_004A;
			}
			IL_0056:
			i++;
			continue;
			IL_004A:
			num = new int?(num2);
			text = text2;
			goto IL_0056;
		}
		return new FuzzySearch.Match
		{
			score = num.Value,
			token = text
		};
	}

	// Token: 0x060016EA RID: 5866 RVA: 0x00081994 File Offset: 0x0007FB94
	private static FuzzySearch.Match ScoreTokens_Unchecked(string searchStringUpper, IReadOnlyList<string> tokens)
	{
		if (tokens.Count == 0)
		{
			return FuzzySearch.Match.NONE;
		}
		int? num = null;
		string text = null;
		foreach (string text2 in tokens)
		{
			int num2 = FuzzySearch.ScoreImpl_Unchecked(searchStringUpper, text2);
			if (num != null)
			{
				int num3 = num2;
				int? num4 = num;
				if (!((num3 > num4.GetValueOrDefault()) & (num4 != null)))
				{
					continue;
				}
			}
			num = new int?(num2);
			text = text2;
		}
		return new FuzzySearch.Match
		{
			score = num.Value,
			token = text
		};
	}

	// Token: 0x060016EB RID: 5867 RVA: 0x00081A44 File Offset: 0x0007FC44
	public static FuzzySearch.Match ScoreTokens(string searchStringUpper, string[] tokens)
	{
		return FuzzySearch.ScoreTokens_Unchecked(searchStringUpper, tokens);
	}

	// Token: 0x060016EC RID: 5868 RVA: 0x00081A4D File Offset: 0x0007FC4D
	public static FuzzySearch.Match ScoreTokens(string searchStringUpper, IReadOnlyList<string> tokens)
	{
		return FuzzySearch.ScoreTokens_Unchecked(searchStringUpper, tokens);
	}

	// Token: 0x060016ED RID: 5869 RVA: 0x00081A58 File Offset: 0x0007FC58
	public static FuzzySearch.Match ScoreCanonicalCandidate(string searchStringUpper, string canonicalCandidate, string candidate = null)
	{
		FuzzySearch.Match match = new FuzzySearch.Match
		{
			score = Fuzz.WeightedRatio(searchStringUpper, canonicalCandidate),
			token = (candidate ?? canonicalCandidate)
		};
		if ((FuzzySearch.GetFeatures() & FuzzySearch.Features.Initialism) != (FuzzySearch.Features)0)
		{
			int num = Fuzz.TokenInitialismRatio(searchStringUpper, canonicalCandidate);
			if (num > match.score)
			{
				match.score = num;
			}
		}
		string[] array = canonicalCandidate.Split(FuzzySearch.TOKEN_SEPARATORS, StringSplitOptions.RemoveEmptyEntries);
		FuzzySearch.Match match2 = FuzzySearch.ScoreTokens_Unchecked(searchStringUpper, array);
		if (match2.score <= match.score)
		{
			return match;
		}
		return match2;
	}

	// Token: 0x060016EE RID: 5870 RVA: 0x00081AD5 File Offset: 0x0007FCD5
	public static FuzzySearch.Match CanonicalizeAndScore(string searchStringUpper, string candidate)
	{
		return FuzzySearch.ScoreCanonicalCandidate(searchStringUpper, FuzzySearch.Canonicalize(candidate).ToUpper(), candidate);
	}

	// Token: 0x04000D85 RID: 3461
	public const FuzzySearch.Features PHRASE_MUTATION_FEATURES = FuzzySearch.Features.Suppress1And2LetterWords | FuzzySearch.Features.SuppressMeaninglessWords;

	// Token: 0x04000D86 RID: 3462
	public static readonly char[] TOKEN_SEPARATORS = new char[]
	{
		' ', '.', '\n', ',', ';', ':', '?', '!', '-', '(',
		')', '[', ']', '{', '}'
	};

	// Token: 0x0200122A RID: 4650
	[Flags]
	public enum Features
	{
		// Token: 0x0400654D RID: 25933
		Suppress1And2LetterWords = 1,
		// Token: 0x0400654E RID: 25934
		SuppressMeaninglessWords = 2,
		// Token: 0x0400654F RID: 25935
		Initialism = 4
	}

	// Token: 0x0200122B RID: 4651
	public struct Match
	{
		// Token: 0x04006550 RID: 25936
		public int score;

		// Token: 0x04006551 RID: 25937
		public string token;

		// Token: 0x04006552 RID: 25938
		public static readonly FuzzySearch.Match NONE = new FuzzySearch.Match
		{
			score = 0,
			token = string.Empty
		};
	}
}
