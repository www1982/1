using System;
using STRINGS;

// Token: 0x02000B91 RID: 2961
public class InternalConstructionCompleteCondition : ProcessCondition
{
	// Token: 0x06005874 RID: 22644 RVA: 0x00200097 File Offset: 0x001FE297
	public InternalConstructionCompleteCondition(BuildingInternalConstructor.Instance target)
	{
		this.target = target;
	}

	// Token: 0x06005875 RID: 22645 RVA: 0x002000A6 File Offset: 0x001FE2A6
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (this.target.IsRequestingConstruction() && !this.target.HasOutputInStorage())
		{
			return ProcessCondition.Status.Warning;
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x06005876 RID: 22646 RVA: 0x002000C5 File Offset: 0x001FE2C5
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		return (status == ProcessCondition.Status.Ready) ? UI.STARMAP.LAUNCHCHECKLIST.INTERNAL_CONSTRUCTION_COMPLETE.STATUS.READY : UI.STARMAP.LAUNCHCHECKLIST.INTERNAL_CONSTRUCTION_COMPLETE.STATUS.FAILURE;
	}

	// Token: 0x06005877 RID: 22647 RVA: 0x002000DC File Offset: 0x001FE2DC
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		return (status == ProcessCondition.Status.Ready) ? UI.STARMAP.LAUNCHCHECKLIST.INTERNAL_CONSTRUCTION_COMPLETE.TOOLTIP.READY : UI.STARMAP.LAUNCHCHECKLIST.INTERNAL_CONSTRUCTION_COMPLETE.TOOLTIP.FAILURE;
	}

	// Token: 0x06005878 RID: 22648 RVA: 0x002000F3 File Offset: 0x001FE2F3
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04003ACF RID: 15055
	private BuildingInternalConstructor.Instance target;
}
