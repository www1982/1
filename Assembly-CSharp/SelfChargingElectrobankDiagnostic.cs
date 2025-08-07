using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x020008B9 RID: 2233
public class SelfChargingElectrobankDiagnostic : ColonyDiagnostic
{
	// Token: 0x06003DE3 RID: 15843 RVA: 0x00159D7C File Offset: 0x00157F7C
	public SelfChargingElectrobankDiagnostic(int worldID)
		: base(worldID, UI.SELFCHARGINGBATTERYDIAGNOSTIC.ALL_NAME)
	{
		this.icon = "overlay_radiation";
		base.AddCriterion("CheckLifetime", new DiagnosticCriterion(UI.SELFCHARGINGBATTERYDIAGNOSTIC.CRITERIA.CHECKSELFCHARGINGBATTERYLIFE, new Func<ColonyDiagnostic.DiagnosticResult>(this.CheckLifetime)));
	}

	// Token: 0x06003DE4 RID: 15844 RVA: 0x00159DD6 File Offset: 0x00157FD6
	public override string[] GetRequiredDlcIds()
	{
		return DlcManager.EXPANSION1.Concat(DlcManager.DLC3);
	}

	// Token: 0x06003DE5 RID: 15845 RVA: 0x00159DE8 File Offset: 0x00157FE8
	private ColonyDiagnostic.DiagnosticResult CheckLifetime()
	{
		ColonyDiagnostic.DiagnosticResult diagnosticResult = new ColonyDiagnostic.DiagnosticResult(ColonyDiagnostic.DiagnosticResult.Opinion.Normal, UI.SELFCHARGINGBATTERYDIAGNOSTIC.NORMAL, null);
		foreach (SelfChargingElectrobank selfChargingElectrobank in Components.SelfChargingElectrobanks.GetItems(base.worldID))
		{
			if (selfChargingElectrobank.LifetimeRemaining <= this.WARNING_LIFETIME)
			{
				diagnosticResult.opinion = ColonyDiagnostic.DiagnosticResult.Opinion.Concern;
				if (diagnosticResult.clickThroughObjects == null)
				{
					diagnosticResult.clickThroughObjects = new List<GameObject>();
				}
				diagnosticResult.clickThroughObjects.Add(selfChargingElectrobank.gameObject);
				diagnosticResult.Message = UI.SELFCHARGINGBATTERYDIAGNOSTIC.CRITERIA_BATTERYLIFE_WARNING;
			}
		}
		return diagnosticResult;
	}

	// Token: 0x0400261E RID: 9758
	private float WARNING_LIFETIME = 600f;
}
