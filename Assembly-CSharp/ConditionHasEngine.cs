using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000B84 RID: 2948
public class ConditionHasEngine : ProcessCondition
{
	// Token: 0x06005830 RID: 22576 RVA: 0x001FED08 File Offset: 0x001FCF08
	public ConditionHasEngine(ILaunchableRocket launchable)
	{
		this.launchable = launchable;
	}

	// Token: 0x06005831 RID: 22577 RVA: 0x001FED18 File Offset: 0x001FCF18
	public override ProcessCondition.Status EvaluateCondition()
	{
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.launchable.LaunchableGameObject.GetComponent<AttachableBuilding>()))
		{
			if (gameObject.GetComponent<RocketEngine>() != null || gameObject.GetComponent<RocketEngineCluster>())
			{
				return ProcessCondition.Status.Ready;
			}
		}
		return ProcessCondition.Status.Failure;
	}

	// Token: 0x06005832 RID: 22578 RVA: 0x001FED98 File Offset: 0x001FCF98
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.HAS_ENGINE.STATUS.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.HAS_ENGINE.STATUS.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.HAS_ENGINE.STATUS.FAILURE;
		}
		return text;
	}

	// Token: 0x06005833 RID: 22579 RVA: 0x001FEDD8 File Offset: 0x001FCFD8
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.HAS_ENGINE.TOOLTIP.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.HAS_ENGINE.TOOLTIP.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.HAS_ENGINE.TOOLTIP.FAILURE;
		}
		return text;
	}

	// Token: 0x06005834 RID: 22580 RVA: 0x001FEE18 File Offset: 0x001FD018
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04003ABD RID: 15037
	private ILaunchableRocket launchable;
}
