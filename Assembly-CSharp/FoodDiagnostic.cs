using System;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

// Token: 0x020008AF RID: 2223
public class FoodDiagnostic : ColonyDiagnostic
{
	// Token: 0x06003DC0 RID: 15808 RVA: 0x00158AF8 File Offset: 0x00156CF8
	public FoodDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.FOODDIAGNOSTIC.ALL_NAME)
	{
		this.tracker = TrackerTool.Instance.GetWorldTracker<KCalTracker>(worldID);
		this.icon = "icon_category_food";
		this.trackerSampleCountSeconds = 150f;
		this.presentationSetting = ColonyDiagnostic.PresentationSetting.CurrentValue;
		base.AddCriterion("CheckEnoughFood", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.FOODDIAGNOSTIC.CRITERIA.CHECKENOUGHFOOD, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckEnoughFood)));
		base.AddCriterion("CheckStarvation", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.FOODDIAGNOSTIC.CRITERIA.CHECKSTARVATION, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckStarvation)));
		this.multiplier = MinionIdentity.GetCalorieBurnMultiplier();
		this.recommendedKCalPerDuplicant = 3000f * this.multiplier;
	}

	// Token: 0x06003DC1 RID: 15809 RVA: 0x00158BB8 File Offset: 0x00156DB8
	private ColonyDiagnostic.DiagnosticResult CheckAnyFood()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.FOODDIAGNOSTIC.CRITERIA_HAS_FOOD.PASS, null);
		if (Components.LiveMinionIdentities.GetWorldItems(base.worldID, false).Count != 0)
		{
			if (this.tracker.GetDataTimeLength() < 10f)
			{
				diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
				diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.NO_DATA;
			}
			else if (this.tracker.GetAverageValue(this.trackerSampleCountSeconds) == 0f)
			{
				diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Bad;
				diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.FOODDIAGNOSTIC.CRITERIA_HAS_FOOD.FAIL;
			}
		}
		return diagnosticResult;
	}

	// Token: 0x06003DC2 RID: 15810 RVA: 0x00158C50 File Offset: 0x00156E50
	private ColonyDiagnostic.DiagnosticResult CheckEnoughFood()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		List<MinionIdentity> list = Components.LiveMinionIdentities.GetWorldItems(base.worldID, false).FindAll((MinionIdentity MID) => Db.Get().Amounts.Calories.Lookup(MID) != null);
		if (this.tracker.GetDataTimeLength() < 10f)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.NO_DATA;
		}
		else if ((float)list.Count * (1000f * this.recommendedKCalPerDuplicant) > this.tracker.GetAverageValue(this.trackerSampleCountSeconds))
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
			float currentValue = this.tracker.GetCurrentValue();
			float num = (float)list.Count * DUPLICANTSTATS.STANDARD.BaseStats.CALORIES_BURNED_PER_CYCLE * this.multiplier;
			string formattedCalories = GameUtil.GetFormattedCalories(currentValue, GameUtil.TimeSlice.None, true);
			string formattedCalories2 = GameUtil.GetFormattedCalories(Mathf.Abs(num), GameUtil.TimeSlice.None, true);
			string text = MISC.NOTIFICATIONS.FOODLOW.TOOLTIP;
			text = text.Replace("{0}", formattedCalories);
			text = text.Replace("{1}", formattedCalories2);
			diagnosticResult.Message = text;
		}
		return diagnosticResult;
	}

	// Token: 0x06003DC3 RID: 15811 RVA: 0x00158D7C File Offset: 0x00156F7C
	private ColonyDiagnostic.DiagnosticResult CheckStarvation()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		foreach (MinionIdentity minionIdentity in Components.LiveMinionIdentities.GetWorldItems(base.worldID, false))
		{
			if (!minionIdentity.IsNull())
			{
				CalorieMonitor.Instance smi = minionIdentity.GetSMI<CalorieMonitor.Instance>();
				if (!smi.IsNullOrStopped() && smi.IsInsideState(smi.sm.hungry.starving))
				{
					diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Bad;
					diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.FOODDIAGNOSTIC.HUNGRY;
					diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(smi.gameObject.transform.position, smi.gameObject);
				}
			}
		}
		return diagnosticResult;
	}

	// Token: 0x06003DC4 RID: 15812 RVA: 0x00158E54 File Offset: 0x00157054
	public override string GetCurrentValueString()
	{
		return GameUtil.GetFormattedCalories(this.tracker.GetCurrentValue(), GameUtil.TimeSlice.None, true);
	}

	// Token: 0x06003DC5 RID: 15813 RVA: 0x00158E68 File Offset: 0x00157068
	public override ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult;
		if (ColonyDiagnosticUtility.IgnoreRocketsWithNoCrewRequested(base.worldID, out diagnosticResult))
		{
			return diagnosticResult;
		}
		diagnosticResult = base.Evaluate();
		if (diagnosticResult.opinion == ColonyDiagnostic.DiagnosticResult.Opinion.Normal)
		{
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.FOODDIAGNOSTIC.NORMAL;
		}
		return diagnosticResult;
	}

	// Token: 0x04002619 RID: 9753
	private const int CYCLES_OF_FOOD = 3;

	// Token: 0x0400261A RID: 9754
	private const float BASE_KCAL_PER_CYCLE = 1000f;

	// Token: 0x0400261B RID: 9755
	private float multiplier = 1f;

	// Token: 0x0400261C RID: 9756
	private float recommendedKCalPerDuplicant;
}
