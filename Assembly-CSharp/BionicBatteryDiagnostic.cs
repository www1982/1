using System;
using STRINGS;
using UnityEngine;

// Token: 0x020008A5 RID: 2213
public class BionicBatteryDiagnostic : BionicColonyDiagnostic
{
	// Token: 0x06003D83 RID: 15747 RVA: 0x001576A4 File Offset: 0x001558A4
	public BionicBatteryDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.BIONICBATTERYDIAGNOSTIC.ALL_NAME)
	{
		this.tracker = TrackerTool.Instance.GetWorldTracker<ElectrobankJoulesTracker>(worldID);
		this.icon = "BionicPower";
		this.trackerSampleCountSeconds = 150f;
		this.presentationSetting = ColonyDiagnostic.PresentationSetting.CurrentValue;
		base.AddCriterion("CheckEnoughBatteries", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.BIONICBATTERYDIAGNOSTIC.CRITERIA.CHECKENOUGHBATTERIES, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckEnoughBatteries)));
		base.AddCriterion("CheckPowerLevel", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.BIONICBATTERYDIAGNOSTIC.CRITERIA.CHECKPOWERLEVEL, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckPowerLevel)));
		BionicBatteryMonitor.WattageModifier difficultyModifier = BionicBatteryMonitor.GetDifficultyModifier();
		this.multiplier = (difficultyModifier.value + 200f) / 200f;
		this.recommendedJoulesPerBionic = 480000f * this.multiplier;
		this.bionicJoulesPerCycle = 120000f * this.multiplier;
	}

	// Token: 0x06003D84 RID: 15748 RVA: 0x0015778C File Offset: 0x0015598C
	private ColonyDiagnostic.DiagnosticResult CheckEnoughBatteries()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		if (this.tracker.GetDataTimeLength() < 10f)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.NO_DATA;
		}
		else if (this.bionics.Count != 0)
		{
			if (this.tracker.GetAverageValue(this.trackerSampleCountSeconds) == 0f)
			{
				diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.BIONICBATTERYDIAGNOSTIC.CRITERIA_BATTERIES.NO_POWERBANKS;
				diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Bad;
			}
			else if ((float)this.bionics.Count * this.recommendedJoulesPerBionic > this.tracker.GetAverageValue(this.trackerSampleCountSeconds))
			{
				diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
				float currentValue = this.tracker.GetCurrentValue();
				float num = this.bionicJoulesPerCycle * (float)this.bionics.Count;
				string formattedJoules = GameUtil.GetFormattedJoules(currentValue, "F1", GameUtil.TimeSlice.None);
				string formattedJoules2 = GameUtil.GetFormattedJoules(Mathf.Abs(num), "F1", GameUtil.TimeSlice.None);
				string text = UI.COLONY_DIAGNOSTICS.BIONICBATTERYDIAGNOSTIC.CRITERIA_BATTERIES.LOW_POWERBANKS;
				text = text.Replace("{0}", formattedJoules);
				text = text.Replace("{1}", formattedJoules2);
				diagnosticResult.Message = text;
			}
		}
		return diagnosticResult;
	}

	// Token: 0x06003D85 RID: 15749 RVA: 0x001578C4 File Offset: 0x00155AC4
	private ColonyDiagnostic.DiagnosticResult CheckPowerLevel()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		foreach (MinionIdentity minionIdentity in this.bionics)
		{
			if (!minionIdentity.isNull)
			{
				BionicBatteryMonitor.Instance smi = minionIdentity.GetSMI<BionicBatteryMonitor.Instance>();
				if (!smi.IsNullOrStopped())
				{
					if (smi.IsInsideState(smi.sm.online.critical) && diagnosticResult.opinion != ColonyDiagnostic.DiagnosticResult.Opinion.Bad)
					{
						diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
						diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.BIONICBATTERYDIAGNOSTIC.CRITERIA_POWERLEVEL.CRITICAL_MODE;
						diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(smi.gameObject.transform.position, smi.gameObject);
					}
					if (smi.IsInsideState(smi.sm.offline))
					{
						diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Bad;
						diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.BIONICBATTERYDIAGNOSTIC.CRITERIA_POWERLEVEL.POWERLESS;
						diagnosticResult.clickThroughTarget = new global::Tuple<Vector3, GameObject>(smi.gameObject.transform.position, smi.gameObject);
					}
				}
			}
		}
		return diagnosticResult;
	}

	// Token: 0x06003D86 RID: 15750 RVA: 0x001579F4 File Offset: 0x00155BF4
	public override string GetCurrentValueString()
	{
		return GameUtil.GetFormattedJoules(this.tracker.GetCurrentValue(), "F1", GameUtil.TimeSlice.None);
	}

	// Token: 0x06003D87 RID: 15751 RVA: 0x00157A0C File Offset: 0x00155C0C
	protected override string GetDefaultResultMessage()
	{
		return UI.COLONY_DIAGNOSTICS.BIONICBATTERYDIAGNOSTIC.CRITERIA_BATTERIES.PASS;
	}

	// Token: 0x04002600 RID: 9728
	private float bionicJoulesPerCycle;

	// Token: 0x04002601 RID: 9729
	private float recommendedJoulesPerBionic;

	// Token: 0x04002602 RID: 9730
	private float multiplier = 1f;
}
