using System;
using System.Collections.Generic;
using System.Text;
using ImGuiNET;
using UnityEngine;

// Token: 0x0200066F RID: 1647
public class DevToolFuzzy : DevTool
{
	// Token: 0x0600285D RID: 10333 RVA: 0x000E626B File Offset: 0x000E446B
	public DevToolFuzzy()
	{
		this.mostRecentEditTime = Time.unscaledTime;
	}

	// Token: 0x0600285E RID: 10334 RVA: 0x000E62A8 File Offset: 0x000E44A8
	private void RecipesUi(StringBuilder sb, string id, List<SearchUtil.NameDescCache> recipes)
	{
		int num = 0;
		foreach (SearchUtil.NameDescCache nameDescCache in recipes)
		{
			if (nameDescCache.Score > num)
			{
				num = nameDescCache.Score;
			}
		}
		if (!this.IsPassingScore(num))
		{
			return;
		}
		sb.Clear();
		sb.AppendFormat("[{0}] Recipes##{1}", num, id);
		if (ImGui.CollapsingHeader(sb.ToString()))
		{
			ImGui.Indent();
			foreach (SearchUtil.NameDescCache nameDescCache2 in recipes)
			{
				if (this.IsPassingScore(nameDescCache2.Score))
				{
					sb.Clear();
					sb.AppendFormat("{0}##{1}", DevToolFuzzy.FormatScoreDisplay(nameDescCache2.Score, nameDescCache2.name.text), id);
					if (ImGui.CollapsingHeader(sb.ToString()))
					{
						this.DisplayIfScorePasses(nameDescCache2);
					}
				}
			}
			ImGui.Unindent();
		}
	}

	// Token: 0x0600285F RID: 10335 RVA: 0x000E63C4 File Offset: 0x000E45C4
	private void TechItemUi(StringBuilder sb, string id, SearchUtil.TechItemCache techItem, SearchUtil.TechCache parentTech = null)
	{
		if (!this.IsPassingScore(techItem.Score))
		{
			return;
		}
		sb.Clear();
		sb.AppendFormat("{0}##TechItem{1}", DevToolFuzzy.FormatScoreDisplay(techItem.Score, techItem.nameDescSearchTerms.nameDesc.name.text), id);
		string text = sb.ToString();
		if (ImGui.CollapsingHeader(text))
		{
			ImGui.Indent();
			if (parentTech != null)
			{
				ImGui.LabelText("Parent Tech", parentTech.tech.nameDesc.name.text);
			}
			this.DisplayIfScorePasses(techItem.nameDescSearchTerms);
			this.RecipesUi(sb, text, techItem.recipes);
			ImGui.Unindent();
		}
	}

	// Token: 0x06002860 RID: 10336 RVA: 0x000E646C File Offset: 0x000E466C
	protected override void RenderTo(DevPanel panel)
	{
		if (ImGui.InputText("Search Text", ref this.searchText, 30U))
		{
			this.refresh = true;
			this.mostRecentEditTime = Time.unscaledTime;
		}
		if (this.refresh && Time.unscaledTime - this.mostRecentEditTime > 0.4f)
		{
			this.Refresh();
			this.refresh = false;
		}
		ImGui.DragInt("Score Threshold", ref this.scoreThreshold, 0.5f, 0, 100);
		StringBuilder stringBuilder = new StringBuilder();
		if (ImGui.CollapsingHeader("Techs"))
		{
			ImGui.Indent();
			foreach (SearchUtil.TechCache techCache in this.techs)
			{
				if (this.IsPassingScore(techCache.Score))
				{
					stringBuilder.Clear();
					stringBuilder.AppendFormat("{0}##Tech", DevToolFuzzy.FormatScoreDisplay(techCache.Score, techCache.tech.nameDesc.name.text));
					string text = stringBuilder.ToString();
					if (ImGui.CollapsingHeader(text))
					{
						ImGui.Indent();
						this.DisplayIfScorePasses(techCache.tech);
						foreach (KeyValuePair<string, SearchUtil.TechItemCache> keyValuePair in techCache.techItems)
						{
							this.TechItemUi(stringBuilder, text, keyValuePair.Value, null);
						}
						ImGui.Unindent();
					}
				}
			}
			ImGui.Unindent();
		}
		if (ImGui.CollapsingHeader("TechItems"))
		{
			ImGui.Indent();
			foreach (SearchUtil.TechCache techCache2 in this.techs)
			{
				foreach (KeyValuePair<string, SearchUtil.TechItemCache> keyValuePair2 in techCache2.techItems)
				{
					this.TechItemUi(stringBuilder, "TechItem", keyValuePair2.Value, techCache2);
				}
			}
			ImGui.Unindent();
		}
		if (ImGui.CollapsingHeader("BuildingDefs"))
		{
			ImGui.Indent();
			foreach (SearchUtil.BuildingDefCache buildingDefCache in this.buildingDefs)
			{
				if (this.IsPassingScore(buildingDefCache.Score))
				{
					stringBuilder.Clear();
					stringBuilder.AppendFormat("{0}##BuildingDef", DevToolFuzzy.FormatScoreDisplay(buildingDefCache.Score, buildingDefCache.nameDescSearchTerms.nameDesc.name.text));
					string text2 = stringBuilder.ToString();
					if (ImGui.CollapsingHeader(text2))
					{
						ImGui.Indent();
						this.DisplayIfScorePasses(buildingDefCache.nameDescSearchTerms);
						this.DisplayIfScorePasses("Effect", buildingDefCache.effect);
						this.RecipesUi(stringBuilder, text2, buildingDefCache.recipes);
						ImGui.Unindent();
					}
				}
			}
			ImGui.Unindent();
		}
	}

	// Token: 0x06002861 RID: 10337 RVA: 0x000E678C File Offset: 0x000E498C
	private void Refresh()
	{
		string text = this.searchText.ToUpper().Trim();
		if (string.IsNullOrEmpty(text))
		{
			return;
		}
		if (this.techs.Count == 0)
		{
			foreach (KeyValuePair<string, SearchUtil.TechCache> keyValuePair in SearchUtil.CacheTechs())
			{
				this.techs.Add(keyValuePair.Value);
			}
		}
		foreach (SearchUtil.TechCache techCache in this.techs)
		{
			techCache.Bind(text);
		}
		this.techs.Sort();
		if (this.buildingDefs.Count == 0)
		{
			foreach (BuildingDef buildingDef in Assets.BuildingDefs)
			{
				this.buildingDefs.Add(SearchUtil.MakeBuildingDefCache(buildingDef));
			}
		}
		foreach (SearchUtil.BuildingDefCache buildingDefCache in this.buildingDefs)
		{
			buildingDefCache.Bind(text);
		}
		this.buildingDefs.Sort();
	}

	// Token: 0x06002862 RID: 10338 RVA: 0x000E6904 File Offset: 0x000E4B04
	private bool IsPassingScore(int score)
	{
		return score >= this.scoreThreshold;
	}

	// Token: 0x06002863 RID: 10339 RVA: 0x000E6912 File Offset: 0x000E4B12
	private static string FormatScoreDisplay(int score, string text)
	{
		return string.Format("[{0}] {1}", score, FuzzySearch.Canonicalize(text));
	}

	// Token: 0x06002864 RID: 10340 RVA: 0x000E692C File Offset: 0x000E4B2C
	private static void DisplayScore(int score, string label, string token, string text)
	{
		ImGui.Text(string.Format("[{0}]", score));
		ImGui.SameLine();
		ImGui.Text(label);
		ImGui.SameLine();
		ImGui.Text(string.Format("({0})", token));
		ImGui.SameLine();
		ImGui.TextWrapped(text);
	}

	// Token: 0x06002865 RID: 10341 RVA: 0x000E6979 File Offset: 0x000E4B79
	private static void DisplayScore(string label, SearchUtil.MatchCache match)
	{
		DevToolFuzzy.DisplayScore(match.Score, label, match.FuzzyMatch.token, match.text);
	}

	// Token: 0x06002866 RID: 10342 RVA: 0x000E6998 File Offset: 0x000E4B98
	private void DisplayIfScorePasses(string label, SearchUtil.MatchCache match)
	{
		if (this.IsPassingScore(match.Score))
		{
			DevToolFuzzy.DisplayScore(label, match);
		}
	}

	// Token: 0x06002867 RID: 10343 RVA: 0x000E69AF File Offset: 0x000E4BAF
	private void DisplayIfScorePasses(SearchUtil.NameDescCache nameDesc)
	{
		this.DisplayIfScorePasses("Name", nameDesc.name);
		this.DisplayIfScorePasses("Desc", nameDesc.desc);
	}

	// Token: 0x06002868 RID: 10344 RVA: 0x000E69D4 File Offset: 0x000E4BD4
	private void DisplayIfScorePasses(SearchUtil.NameDescSearchTermsCache nameDescSearchTerms)
	{
		this.DisplayIfScorePasses(nameDescSearchTerms.nameDesc);
		if (this.IsPassingScore(nameDescSearchTerms.SearchTermsScore.score))
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendJoin<string>(", ", nameDescSearchTerms.searchTerms);
			DevToolFuzzy.DisplayScore(nameDescSearchTerms.SearchTermsScore.score, "SearchTerms", nameDescSearchTerms.SearchTermsScore.token, stringBuilder.ToString());
		}
	}

	// Token: 0x040017A7 RID: 6055
	private string searchText = "";

	// Token: 0x040017A8 RID: 6056
	private float mostRecentEditTime;

	// Token: 0x040017A9 RID: 6057
	private bool refresh;

	// Token: 0x040017AA RID: 6058
	private const float REFRESH_DELAY = 0.4f;

	// Token: 0x040017AB RID: 6059
	private int scoreThreshold = 79;

	// Token: 0x040017AC RID: 6060
	private readonly List<SearchUtil.TechCache> techs = new List<SearchUtil.TechCache>();

	// Token: 0x040017AD RID: 6061
	private readonly List<SearchUtil.BuildingDefCache> buildingDefs = new List<SearchUtil.BuildingDefCache>();
}
