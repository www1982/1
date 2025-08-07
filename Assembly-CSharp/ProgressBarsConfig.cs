using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000DA2 RID: 3490
public class ProgressBarsConfig : ScriptableObject
{
	// Token: 0x06006D58 RID: 27992 RVA: 0x002964D6 File Offset: 0x002946D6
	public static void DestroyInstance()
	{
		ProgressBarsConfig.instance = null;
	}

	// Token: 0x170007A2 RID: 1954
	// (get) Token: 0x06006D59 RID: 27993 RVA: 0x002964DE File Offset: 0x002946DE
	public static ProgressBarsConfig Instance
	{
		get
		{
			if (ProgressBarsConfig.instance == null)
			{
				ProgressBarsConfig.instance = Resources.Load<ProgressBarsConfig>("ProgressBarsConfig");
				ProgressBarsConfig.instance.Initialize();
			}
			return ProgressBarsConfig.instance;
		}
	}

	// Token: 0x06006D5A RID: 27994 RVA: 0x0029650C File Offset: 0x0029470C
	public void Initialize()
	{
		foreach (ProgressBarsConfig.BarData barData in this.barColorDataList)
		{
			this.barColorMap.Add(barData.barName, barData);
		}
	}

	// Token: 0x06006D5B RID: 27995 RVA: 0x0029656C File Offset: 0x0029476C
	public string GetBarDescription(string barName)
	{
		string text = "";
		if (this.IsBarNameValid(barName))
		{
			text = Strings.Get(this.barColorMap[barName].barDescriptionKey);
		}
		return text;
	}

	// Token: 0x06006D5C RID: 27996 RVA: 0x002965A8 File Offset: 0x002947A8
	public Color GetBarColor(string barName)
	{
		Color color = Color.clear;
		if (this.IsBarNameValid(barName))
		{
			color = this.barColorMap[barName].barColor;
		}
		return color;
	}

	// Token: 0x06006D5D RID: 27997 RVA: 0x002965D7 File Offset: 0x002947D7
	public bool IsBarNameValid(string barName)
	{
		if (string.IsNullOrEmpty(barName))
		{
			global::Debug.LogError("The barName provided was null or empty. Don't do that.");
			return false;
		}
		if (!this.barColorMap.ContainsKey(barName))
		{
			global::Debug.LogError(string.Format("No BarData found for the entry [ {0} ]", barName));
			return false;
		}
		return true;
	}

	// Token: 0x04004AA9 RID: 19113
	public GameObject progressBarPrefab;

	// Token: 0x04004AAA RID: 19114
	public GameObject progressBarUIPrefab;

	// Token: 0x04004AAB RID: 19115
	public GameObject healthBarPrefab;

	// Token: 0x04004AAC RID: 19116
	public List<ProgressBarsConfig.BarData> barColorDataList = new List<ProgressBarsConfig.BarData>();

	// Token: 0x04004AAD RID: 19117
	public Dictionary<string, ProgressBarsConfig.BarData> barColorMap = new Dictionary<string, ProgressBarsConfig.BarData>();

	// Token: 0x04004AAE RID: 19118
	private static ProgressBarsConfig instance;

	// Token: 0x02001FA3 RID: 8099
	[Serializable]
	public struct BarData
	{
		// Token: 0x0400919E RID: 37278
		public string barName;

		// Token: 0x0400919F RID: 37279
		public Color barColor;

		// Token: 0x040091A0 RID: 37280
		public string barDescriptionKey;
	}
}
