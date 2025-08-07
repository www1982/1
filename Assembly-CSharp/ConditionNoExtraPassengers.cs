using System;
using STRINGS;

// Token: 0x02000B88 RID: 2952
public class ConditionNoExtraPassengers : ProcessCondition
{
	// Token: 0x06005845 RID: 22597 RVA: 0x001FF4D7 File Offset: 0x001FD6D7
	public ConditionNoExtraPassengers(PassengerRocketModule module)
	{
		this.module = module;
	}

	// Token: 0x06005846 RID: 22598 RVA: 0x001FF4E6 File Offset: 0x001FD6E6
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (!this.module.CheckExtraPassengers())
		{
			return ProcessCondition.Status.Ready;
		}
		return ProcessCondition.Status.Failure;
	}

	// Token: 0x06005847 RID: 22599 RVA: 0x001FF4F8 File Offset: 0x001FD6F8
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.NO_EXTRA_PASSENGERS.READY;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.NO_EXTRA_PASSENGERS.FAILURE;
	}

	// Token: 0x06005848 RID: 22600 RVA: 0x001FF513 File Offset: 0x001FD713
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.NO_EXTRA_PASSENGERS.TOOLTIP.READY;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.NO_EXTRA_PASSENGERS.TOOLTIP.FAILURE;
	}

	// Token: 0x06005849 RID: 22601 RVA: 0x001FF52E File Offset: 0x001FD72E
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04003AC3 RID: 15043
	private PassengerRocketModule module;
}
