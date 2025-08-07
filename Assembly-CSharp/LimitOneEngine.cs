using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000AFF RID: 2815
public class LimitOneEngine : SelectModuleCondition
{
	// Token: 0x060052C5 RID: 21189 RVA: 0x001E234C File Offset: 0x001E054C
	public override bool EvaluateCondition(GameObject existingModule, BuildingDef selectedPart, SelectModuleCondition.SelectionContext selectionContext)
	{
		if (existingModule == null)
		{
			return true;
		}
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(existingModule.GetComponent<AttachableBuilding>()))
		{
			if (selectionContext != SelectModuleCondition.SelectionContext.ReplaceModule || !(gameObject == existingModule.gameObject))
			{
				if (gameObject.GetComponent<RocketEngineCluster>() != null)
				{
					return false;
				}
				if (gameObject.GetComponent<BuildingUnderConstruction>() != null && gameObject.GetComponent<BuildingUnderConstruction>().Def.BuildingComplete.GetComponent<RocketEngineCluster>() != null)
				{
					return false;
				}
			}
		}
		return true;
	}

	// Token: 0x060052C6 RID: 21190 RVA: 0x001E2400 File Offset: 0x001E0600
	public override string GetStatusTooltip(bool ready, GameObject moduleBase, BuildingDef selectedPart)
	{
		if (ready)
		{
			return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.ONE_ENGINE_PER_ROCKET.COMPLETE;
		}
		return UI.UISIDESCREENS.SELECTMODULESIDESCREEN.CONSTRAINTS.ONE_ENGINE_PER_ROCKET.FAILED;
	}
}
