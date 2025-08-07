using System;
using System.Collections.Generic;

// Token: 0x020008A6 RID: 2214
public abstract class BionicColonyDiagnostic : ColonyDiagnostic
{
	// Token: 0x06003D88 RID: 15752 RVA: 0x00157A18 File Offset: 0x00155C18
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.DLC3;
	}

	// Token: 0x06003D89 RID: 15753 RVA: 0x00157A1F File Offset: 0x00155C1F
	public BionicColonyDiagnostic(int worldID, string name)
		: base(worldID, name)
	{
		this.RefreshData();
	}

	// Token: 0x06003D8A RID: 15754 RVA: 0x00157A38 File Offset: 0x00155C38
	protected void RefreshData()
	{
		Components.Cmps<MinionIdentity> cmps;
		if (Components.LiveMinionIdentitiesByModel.TryGetValue(BionicMinionConfig.MODEL, out cmps))
		{
			this.bionics = cmps.GetWorldItems(base.worldID, true, new Func<MinionIdentity, bool>(this.MinionFilter));
			return;
		}
		this.bionics = new List<MinionIdentity>();
	}

	// Token: 0x06003D8B RID: 15755 RVA: 0x00157A84 File Offset: 0x00155C84
	protected virtual bool MinionFilter(MinionIdentity minion)
	{
		return true;
	}

	// Token: 0x06003D8C RID: 15756 RVA: 0x00157A88 File Offset: 0x00155C88
	public override ColonyDiagnostic.DiagnosticResult Evaluate()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult;
		if (this.ignoreInIdleRockets && ColonyDiagnosticUtility.IgnoreRocketsWithNoCrewRequested(base.worldID, out diagnosticResult))
		{
			return diagnosticResult;
		}
		this.RefreshData();
		diagnosticResult = base.Evaluate();
		if (diagnosticResult.opinion == ColonyDiagnostic.DiagnosticResult.Opinion.Normal)
		{
			diagnosticResult.Message = this.GetDefaultResultMessage();
		}
		return diagnosticResult;
	}

	// Token: 0x06003D8D RID: 15757
	protected abstract string GetDefaultResultMessage();

	// Token: 0x04002603 RID: 9731
	protected const bool INCLUDE_CHILD_WORLDS = true;

	// Token: 0x04002604 RID: 9732
	protected List<MinionIdentity> bionics;

	// Token: 0x04002605 RID: 9733
	protected bool ignoreInIdleRockets = true;
}
