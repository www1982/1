using System;
using System.Collections.Generic;
using STRINGS;

// Token: 0x020008BB RID: 2235
public class ToiletDiagnostic : ColonyDiagnostic
{
	// Token: 0x06003DE8 RID: 15848 RVA: 0x0015A014 File Offset: 0x00158214
	public ToiletDiagnostic(int worldID)
		: base(worldID, UI.COLONY_DIAGNOSTICS.TOILETDIAGNOSTIC.ALL_NAME)
	{
		this.icon = "icon_action_region_toilet";
		this.tracker = TrackerTool.Instance.GetWorldTracker<WorkingToiletTracker>(worldID);
		this.NO_MINIONS_WITH_BLADDER = (base.IsWorldModuleInterior ? UI.COLONY_DIAGNOSTICS.TOILETDIAGNOSTIC.NO_MINIONS_ROCKET : UI.COLONY_DIAGNOSTICS.TOILETDIAGNOSTIC.NO_MINIONS_PLANETOID);
		base.AddCriterion("CheckHasAnyToilets", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.TOILETDIAGNOSTIC.CRITERIA.CHECKHASANYTOILETS, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckHasAnyToilets)));
		base.AddCriterion("CheckEnoughToilets", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.TOILETDIAGNOSTIC.CRITERIA.CHECKENOUGHTOILETS, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckEnoughToilets)));
		base.AddCriterion("CheckBladders", new DiagnosticCriterion(UI.COLONY_DIAGNOSTICS.TOILETDIAGNOSTIC.CRITERIA.CHECKBLADDERS, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckBladders)));
	}

	// Token: 0x06003DE9 RID: 15849 RVA: 0x0015A0E0 File Offset: 0x001582E0
	private ColonyDiagnostic.DiagnosticResult CheckHasAnyToilets()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		if (this.minionsWithBladders.Count == 0)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
			diagnosticResult.Message = this.NO_MINIONS_WITH_BLADDER;
		}
		else if (this.toilets.Count == 0)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.TOILETDIAGNOSTIC.NO_TOILETS;
		}
		return diagnosticResult;
	}

	// Token: 0x06003DEA RID: 15850 RVA: 0x0015A14C File Offset: 0x0015834C
	private ColonyDiagnostic.DiagnosticResult CheckEnoughToilets()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		if (this.minionsWithBladders.Count == 0)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
			diagnosticResult.Message = this.NO_MINIONS_WITH_BLADDER;
		}
		else
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.TOILETDIAGNOSTIC.NORMAL;
			if (this.tracker.GetDataTimeLength() > 10f && this.tracker.GetAverageValue(this.trackerSampleCountSeconds) <= 0f)
			{
				diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
				diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.TOILETDIAGNOSTIC.NO_WORKING_TOILETS;
			}
		}
		return diagnosticResult;
	}

	// Token: 0x06003DEB RID: 15851 RVA: 0x0015A1F0 File Offset: 0x001583F0
	private ColonyDiagnostic.DiagnosticResult CheckBladders()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.COLONY_DIAGNOSTICS.GENERIC_CRITERIA_PASS, null);
		if (this.minionsWithBladders.Count == 0)
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
			diagnosticResult.Message = this.NO_MINIONS_WITH_BLADDER;
		}
		else
		{
			diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Normal;
			diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.TOILETDIAGNOSTIC.NORMAL;
			WorldContainer world = ClusterManager.Instance.GetWorld(base.worldID);
			foreach (PeeChoreMonitor.Instance instance in Components.CriticalBladders.Items)
			{
				int myWorldId = instance.master.gameObject.GetMyWorldId();
				if (myWorldId == base.worldID || world.GetChildWorldIds().Contains(myWorldId))
				{
					diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Warning;
					diagnosticResult.Message = UI.COLONY_DIAGNOSTICS.TOILETDIAGNOSTIC.TOILET_URGENT;
					break;
				}
			}
		}
		return diagnosticResult;
	}

	// Token: 0x06003DEC RID: 15852 RVA: 0x0015A2E8 File Offset: 0x001584E8
	private bool MinionFilter(MinionIdentity minion)
	{
		return minion.modifiers.amounts.Has(Db.Get().Amounts.Bladder);
	}

	// Token: 0x06003DED RID: 15853 RVA: 0x0015A30C File Offset: 0x0015850C
	public override ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, this.NO_MINIONS_WITH_BLADDER, null);
		if (ColonyDiagnosticUtility.IgnoreRocketsWithNoCrewRequested(base.worldID, out diagnosticResult))
		{
			return diagnosticResult;
		}
		this.RefreshData();
		return base.Evaluate();
	}

	// Token: 0x06003DEE RID: 15854 RVA: 0x0015A345 File Offset: 0x00158545
	private void RefreshData()
	{
		this.minionsWithBladders = Components.LiveMinionIdentities.GetWorldItems(base.worldID, true, new Func<MinionIdentity, bool>(this.MinionFilter));
		this.toilets = Components.Toilets.GetWorldItems(base.worldID, true);
	}

	// Token: 0x06003DEF RID: 15855 RVA: 0x0015A384 File Offset: 0x00158584
	public override string GetAverageValueString()
	{
		if (this.minionsWithBladders == null || this.minionsWithBladders.Count == 0)
		{
			this.RefreshData();
		}
		int num = this.toilets.Count;
		for (int i = 0; i < this.toilets.Count; i++)
		{
			if (!this.toilets[i].IsNullOrDestroyed() && !this.toilets[i].IsUsable())
			{
				num--;
			}
		}
		return num.ToString() + ":" + this.minionsWithBladders.Count.ToString();
	}

	// Token: 0x0400261F RID: 9759
	private const bool INCLUDE_CHILD_WORLDS = true;

	// Token: 0x04002620 RID: 9760
	private List<MinionIdentity> minionsWithBladders;

	// Token: 0x04002621 RID: 9761
	private List<IUsable> toilets;

	// Token: 0x04002622 RID: 9762
	private readonly string NO_MINIONS_WITH_BLADDER;
}
