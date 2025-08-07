using System;
using System.Collections.Generic;
using System.Linq;
using STRINGS;
using TUNING;

// Token: 0x02000D64 RID: 3428
public class MeterScreen_Rations : MeterScreen_ValueTrackerDisplayer
{
	// Token: 0x06006A4C RID: 27212 RVA: 0x002813BC File Offset: 0x0027F5BC
	protected override string OnTooltip()
	{
		this.rationsDict.Clear();
		float num = WorldResourceAmountTracker<RationTracker>.Get().CountAmount(this.rationsDict, ClusterManager.Instance.activeWorld.worldInventory, true);
		this.Label.text = GameUtil.GetFormattedCalories(num, GameUtil.TimeSlice.None, true);
		this.Tooltip.ClearMultiStringTooltip();
		this.Tooltip.AddMultiStringTooltip(string.Format(UI.TOOLTIPS.METERSCREEN_MEALHISTORY, GameUtil.GetFormattedCalories(num, GameUtil.TimeSlice.None, true), GameUtil.GetFormattedCalories(-MinionIdentity.GetCalorieBurnMultiplier() * DUPLICANTSTATS.STANDARD.BaseStats.CALORIES_BURNED_PER_CYCLE, GameUtil.TimeSlice.None, true)), this.ToolTipStyle_Header);
		this.Tooltip.AddMultiStringTooltip("", this.ToolTipStyle_Property);
		foreach (KeyValuePair<string, float> keyValuePair in this.rationsDict.OrderByDescending(delegate(KeyValuePair<string, float> x)
		{
			EdiblesManager.FoodInfo foodInfo2 = EdiblesManager.GetFoodInfo(x.Key);
			return x.Value * ((foodInfo2 != null) ? foodInfo2.CaloriesPerUnit : (-1f));
		}).ToDictionary((KeyValuePair<string, float> t) => t.Key, (KeyValuePair<string, float> t) => t.Value))
		{
			EdiblesManager.FoodInfo foodInfo = EdiblesManager.GetFoodInfo(keyValuePair.Key);
			this.Tooltip.AddMultiStringTooltip((foodInfo != null) ? string.Format("{0}: {1}", foodInfo.Name, GameUtil.GetFormattedCalories(keyValuePair.Value * foodInfo.CaloriesPerUnit, GameUtil.TimeSlice.None, true)) : string.Format(UI.TOOLTIPS.METERSCREEN_INVALID_FOOD_TYPE, keyValuePair.Key), this.ToolTipStyle_Property);
		}
		return "";
	}

	// Token: 0x06006A4D RID: 27213 RVA: 0x00281578 File Offset: 0x0027F778
	protected override void InternalRefresh()
	{
		if (this.Label != null && WorldResourceAmountTracker<RationTracker>.Get() != null)
		{
			long num = (long)WorldResourceAmountTracker<RationTracker>.Get().CountAmount(null, ClusterManager.Instance.activeWorld.worldInventory, true);
			if (this.cachedCalories != num)
			{
				this.Label.text = GameUtil.GetFormattedCalories((float)num, GameUtil.TimeSlice.None, true);
				this.cachedCalories = num;
			}
		}
		this.diagnosticGraph.GetComponentInChildren<SparkLayer>().SetColor(((float)this.cachedCalories > (float)this.GetWorldMinionIdentities().Count * FOOD.FOOD_CALORIES_PER_CYCLE) ? Constants.NEUTRAL_COLOR : Constants.NEGATIVE_COLOR);
		this.diagnosticGraph.GetComponentInChildren<LineLayer>().RefreshLine(TrackerTool.Instance.GetWorldTracker<KCalTracker>(ClusterManager.Instance.activeWorldId).ChartableData(600f), "kcal");
	}

	// Token: 0x0400487F RID: 18559
	private long cachedCalories = -1L;

	// Token: 0x04004880 RID: 18560
	private Dictionary<string, float> rationsDict = new Dictionary<string, float>();
}
