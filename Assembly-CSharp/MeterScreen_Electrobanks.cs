using System;
using System.Collections.Generic;
using System.Linq;
using STRINGS;
using UnityEngine;

// Token: 0x02000D63 RID: 3427
public class MeterScreen_Electrobanks : MeterScreen_ValueTrackerDisplayer
{
	// Token: 0x06006A45 RID: 27205 RVA: 0x00280FF8 File Offset: 0x0027F1F8
	protected override void OnSpawn()
	{
		base.OnSpawn();
		Components.LiveMinionIdentities.OnAdd += this.OnNewMinionAdded;
		List<MinionIdentity> allMinionsFromAllWorlds = this.GetAllMinionsFromAllWorlds();
		bool flag;
		if (allMinionsFromAllWorlds != null)
		{
			flag = allMinionsFromAllWorlds.Find((MinionIdentity m) => m.model == BionicMinionConfig.MODEL) != null;
		}
		else
		{
			flag = false;
		}
		this.SetVisibility(flag);
		BionicBatteryMonitor.WattageModifier difficultyModifier = BionicBatteryMonitor.GetDifficultyModifier();
		this.bionicJoulesPerCycle = (difficultyModifier.value + 200f) * 600f;
	}

	// Token: 0x06006A46 RID: 27206 RVA: 0x0028107D File Offset: 0x0027F27D
	protected override void OnCleanUp()
	{
		Components.LiveMinionIdentities.OnAdd -= this.OnNewMinionAdded;
		base.OnCleanUp();
	}

	// Token: 0x06006A47 RID: 27207 RVA: 0x0028109B File Offset: 0x0027F29B
	private void OnNewMinionAdded(MinionIdentity id)
	{
		if (id.model == BionicMinionConfig.MODEL)
		{
			this.SetVisibility(true);
		}
	}

	// Token: 0x06006A48 RID: 27208 RVA: 0x002810B6 File Offset: 0x0027F2B6
	public void SetVisibility(bool isVisible)
	{
		base.gameObject.SetActive(isVisible);
	}

	// Token: 0x06006A49 RID: 27209 RVA: 0x002810C4 File Offset: 0x0027F2C4
	protected override string OnTooltip()
	{
		this.per_electrobankType_UnitCount_Dictionary.Clear();
		float num = 0f;
		string formattedJoules = GameUtil.GetFormattedJoules(WorldResourceAmountTracker<ElectrobankTracker>.Get().CountAmount(this.per_electrobankType_UnitCount_Dictionary, out num, ClusterManager.Instance.activeWorld.worldInventory, true), "F1", GameUtil.TimeSlice.None);
		this.Label.text = formattedJoules;
		this.Tooltip.ClearMultiStringTooltip();
		this.Tooltip.AddMultiStringTooltip(string.Format(UI.TOOLTIPS.METERSCREEN_ELECTROBANK_JOULES, formattedJoules, GameUtil.GetFormattedJoules(this.bionicJoulesPerCycle, "F1", GameUtil.TimeSlice.None), GameUtil.GetFormattedUnits((float)((int)num), GameUtil.TimeSlice.None, true, "")), this.ToolTipStyle_Header);
		this.Tooltip.AddMultiStringTooltip("", this.ToolTipStyle_Property);
		foreach (KeyValuePair<string, float> keyValuePair in this.per_electrobankType_UnitCount_Dictionary.OrderByDescending((KeyValuePair<string, float> x) => x.Value).ToDictionary((KeyValuePair<string, float> t) => t.Key, (KeyValuePair<string, float> t) => t.Value))
		{
			GameObject prefab = Assets.GetPrefab(keyValuePair.Key);
			this.Tooltip.AddMultiStringTooltip((prefab != null) ? string.Format("{0} ({1}): {2}", prefab.GetProperName(), GameUtil.GetFormattedUnits((float)((int)keyValuePair.Value), GameUtil.TimeSlice.None, true, ""), GameUtil.GetFormattedJoules(keyValuePair.Value * 120000f, "F1", GameUtil.TimeSlice.None)) : string.Format(UI.TOOLTIPS.METERSCREEN_INVALID_ELECTROBANK_TYPE, keyValuePair.Key), this.ToolTipStyle_Property);
		}
		return "";
	}

	// Token: 0x06006A4A RID: 27210 RVA: 0x002812B4 File Offset: 0x0027F4B4
	protected override void InternalRefresh()
	{
		if (!Game.IsDlcActiveForCurrentSave("DLC3_ID"))
		{
			return;
		}
		if (this.Label != null && WorldResourceAmountTracker<ElectrobankTracker>.Get() != null)
		{
			float num2;
			long num = (long)WorldResourceAmountTracker<ElectrobankTracker>.Get().CountAmount(null, out num2, ClusterManager.Instance.activeWorld.worldInventory, true);
			if (this.cachedJoules != num)
			{
				this.Label.text = GameUtil.GetFormattedJoules((float)num, "F1", GameUtil.TimeSlice.None);
				this.cachedJoules = num;
			}
		}
		this.diagnosticGraph.GetComponentInChildren<SparkLayer>().SetColor(((float)this.cachedJoules > (float)this.GetWorldMinionIdentities().Count * 120000f) ? Constants.NEUTRAL_COLOR : Constants.NEGATIVE_COLOR);
		WorldTracker worldTracker = TrackerTool.Instance.GetWorldTracker<ElectrobankJoulesTracker>(ClusterManager.Instance.activeWorldId);
		if (worldTracker != null)
		{
			this.diagnosticGraph.GetComponentInChildren<LineLayer>().RefreshLine(worldTracker.ChartableData(600f), "joules");
		}
	}

	// Token: 0x0400487C RID: 18556
	private long cachedJoules = -1L;

	// Token: 0x0400487D RID: 18557
	private Dictionary<string, float> per_electrobankType_UnitCount_Dictionary = new Dictionary<string, float>();

	// Token: 0x0400487E RID: 18558
	private float bionicJoulesPerCycle;
}
