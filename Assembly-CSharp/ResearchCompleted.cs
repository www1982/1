using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000AFC RID: 2812
public class ResearchCompleted : SelectModuleCondition
{
	// Token: 0x060052BA RID: 21178 RVA: 0x001E2130 File Offset: 0x001E0330
	public override bool IgnoreInSanboxMode()
	{
		return true;
	}

	// Token: 0x060052BB RID: 21179 RVA: 0x001E2134 File Offset: 0x001E0334
	public override bool EvaluateCondition(GameObject existingModule, BuildingDef selectedPart, SelectModuleCondition.SelectionContext selectionContext)
	{
		if (existingModule == null)
		{
			return true;
		}
		TechItem techItem = Db.Get().TechItems.TryGet(selectedPart.PrefabID);
		return DebugHandler.InstantBuildMode || Game.Instance.SandboxModeActive || techItem == null || techItem.IsComplete();
	}

	// Token: 0x060052BC RID: 21180 RVA: 0x001E2180 File Offset: 0x001E0380
	public override string GetStatusTooltip(bool ready, GameObject moduleBase, BuildingDef selectedPart)
	{
		if (ready)
		{
			return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.RESEARCHED.COMPLETE;
		}
		return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.RESEARCHED.FAILED;
	}
}
