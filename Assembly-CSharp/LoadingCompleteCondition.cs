using System;
using STRINGS;

// Token: 0x02000B92 RID: 2962
public class LoadingCompleteCondition : ProcessCondition
{
	// Token: 0x06005879 RID: 22649 RVA: 0x002000F6 File Offset: 0x001FE2F6
	public LoadingCompleteCondition(Storage target)
	{
		this.target = target;
		this.userControlledTarget = target.GetComponent<IUserControlledCapacity>();
	}

	// Token: 0x0600587A RID: 22650 RVA: 0x00200111 File Offset: 0x001FE311
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (this.userControlledTarget != null)
		{
			if (this.userControlledTarget.AmountStored < this.userControlledTarget.UserMaxCapacity)
			{
				return ProcessCondition.Status.Warning;
			}
			return ProcessCondition.Status.Ready;
		}
		else
		{
			if (!this.target.IsFull())
			{
				return ProcessCondition.Status.Warning;
			}
			return ProcessCondition.Status.Ready;
		}
	}

	// Token: 0x0600587B RID: 22651 RVA: 0x00200147 File Offset: 0x001FE347
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		return (status == ProcessCondition.Status.Ready) ? UI.STARMAP.LAUNCHCHECKLIST.LOADING_COMPLETE.STATUS.READY : UI.STARMAP.LAUNCHCHECKLIST.LOADING_COMPLETE.STATUS.WARNING;
	}

	// Token: 0x0600587C RID: 22652 RVA: 0x0020015E File Offset: 0x001FE35E
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		return (status == ProcessCondition.Status.Ready) ? UI.STARMAP.LAUNCHCHECKLIST.LOADING_COMPLETE.TOOLTIP.READY : UI.STARMAP.LAUNCHCHECKLIST.LOADING_COMPLETE.TOOLTIP.WARNING;
	}

	// Token: 0x0600587D RID: 22653 RVA: 0x00200175 File Offset: 0x001FE375
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04003AD0 RID: 15056
	private Storage target;

	// Token: 0x04003AD1 RID: 15057
	private IUserControlledCapacity userControlledTarget;
}
