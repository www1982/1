using System;
using STRINGS;

// Token: 0x02000B89 RID: 2953
public class ConditionOnLaunchPad : ProcessCondition
{
	// Token: 0x0600584A RID: 22602 RVA: 0x001FF531 File Offset: 0x001FD731
	public ConditionOnLaunchPad(CraftModuleInterface craftInterface)
	{
		this.craftInterface = craftInterface;
	}

	// Token: 0x0600584B RID: 22603 RVA: 0x001FF540 File Offset: 0x001FD740
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (!(this.craftInterface.CurrentPad != null))
		{
			return ProcessCondition.Status.Failure;
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x0600584C RID: 22604 RVA: 0x001FF558 File Offset: 0x001FD758
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.ON_LAUNCHPAD.STATUS.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.ON_LAUNCHPAD.STATUS.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.ON_LAUNCHPAD.STATUS.FAILURE;
		}
		return text;
	}

	// Token: 0x0600584D RID: 22605 RVA: 0x001FF598 File Offset: 0x001FD798
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.ON_LAUNCHPAD.TOOLTIP.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.ON_LAUNCHPAD.TOOLTIP.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.ON_LAUNCHPAD.TOOLTIP.FAILURE;
		}
		return text;
	}

	// Token: 0x0600584E RID: 22606 RVA: 0x001FF5D8 File Offset: 0x001FD7D8
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04003AC4 RID: 15044
	private CraftModuleInterface craftInterface;
}
