using System;
using UnityEngine;

// Token: 0x02000AFB RID: 2811
public abstract class SelectModuleCondition
{
	// Token: 0x060052B6 RID: 21174
	public abstract bool EvaluateCondition(GameObject existingModule, BuildingDef selectedPart, SelectModuleCondition.SelectionContext selectionContext);

	// Token: 0x060052B7 RID: 21175
	public abstract string GetStatusTooltip(bool ready, GameObject moduleBase, BuildingDef selectedPart);

	// Token: 0x060052B8 RID: 21176 RVA: 0x001E2125 File Offset: 0x001E0325
	public virtual bool IgnoreInSanboxMode()
	{
		return false;
	}

	// Token: 0x02001C06 RID: 7174
	public enum SelectionContext
	{
		// Token: 0x040084D6 RID: 34006
		AddModuleAbove,
		// Token: 0x040084D7 RID: 34007
		AddModuleBelow,
		// Token: 0x040084D8 RID: 34008
		ReplaceModule
	}
}
