using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000B01 RID: 2817
public class TopOnly : SelectModuleCondition
{
	// Token: 0x060052CB RID: 21195 RVA: 0x001E24A4 File Offset: 0x001E06A4
	public override bool EvaluateCondition(GameObject existingModule, BuildingDef selectedPart, SelectModuleCondition.SelectionContext selectionContext)
	{
		global::Debug.Assert(existingModule != null, "Existing module is null in top only condition");
		if (selectionContext == SelectModuleCondition.SelectionContext.ReplaceModule)
		{
			global::Debug.Assert(existingModule.GetComponent<LaunchPad>() == null, "Trying to replace launch pad with rocket module");
			return existingModule.GetComponent<BuildingAttachPoint>() == null || existingModule.GetComponent<BuildingAttachPoint>().points[0].attachedBuilding == null;
		}
		return existingModule.GetComponent<LaunchPad>() != null || (existingModule.GetComponent<BuildingAttachPoint>() != null && existingModule.GetComponent<BuildingAttachPoint>().points[0].attachedBuilding == null);
	}

	// Token: 0x060052CC RID: 21196 RVA: 0x001E2545 File Offset: 0x001E0745
	public override string GetStatusTooltip(bool ready, GameObject moduleBase, BuildingDef selectedPart)
	{
		if (ready)
		{
			return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.TOP_ONLY.COMPLETE;
		}
		return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.TOP_ONLY.FAILED;
	}
}
