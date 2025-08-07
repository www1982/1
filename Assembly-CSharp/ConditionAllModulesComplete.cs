using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000B7D RID: 2941
public class ConditionAllModulesComplete : ProcessCondition
{
	// Token: 0x06005802 RID: 22530 RVA: 0x001FE01F File Offset: 0x001FC21F
	public ConditionAllModulesComplete(ILaunchableRocket launchable)
	{
		this.launchable = launchable;
	}

	// Token: 0x06005803 RID: 22531 RVA: 0x001FE030 File Offset: 0x001FC230
	public override ProcessCondition.Status EvaluateCondition()
	{
		using (List<GameObject>.Enumerator enumerator = AttachableBuilding.GetAttachedNetwork(this.launchable.LaunchableGameObject.GetComponent<AttachableBuilding>()).GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.GetComponent<Constructable>() != null)
				{
					return ProcessCondition.Status.Failure;
				}
			}
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x06005804 RID: 22532 RVA: 0x001FE0A0 File Offset: 0x001FC2A0
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.CONSTRUCTION_COMPLETE.STATUS.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.CONSTRUCTION_COMPLETE.STATUS.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.CONSTRUCTION_COMPLETE.STATUS.FAILURE;
		}
		return text;
	}

	// Token: 0x06005805 RID: 22533 RVA: 0x001FE0E0 File Offset: 0x001FC2E0
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.CONSTRUCTION_COMPLETE.TOOLTIP.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.CONSTRUCTION_COMPLETE.TOOLTIP.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.CONSTRUCTION_COMPLETE.TOOLTIP.FAILURE;
		}
		return text;
	}

	// Token: 0x06005806 RID: 22534 RVA: 0x001FE120 File Offset: 0x001FC320
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04003AAF RID: 15023
	private ILaunchableRocket launchable;
}
