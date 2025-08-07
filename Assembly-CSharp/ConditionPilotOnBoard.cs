using System;
using STRINGS;

// Token: 0x02000B8B RID: 2955
public class ConditionPilotOnBoard : ProcessCondition
{
	// Token: 0x06005854 RID: 22612 RVA: 0x001FF6FB File Offset: 0x001FD8FB
	public ConditionPilotOnBoard(PassengerRocketModule module)
	{
		this.module = module;
		this.rocketModule = module.GetComponent<RocketModuleCluster>();
	}

	// Token: 0x06005855 RID: 22613 RVA: 0x001FF716 File Offset: 0x001FD916
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (this.module.CheckPilotBoarded())
		{
			return ProcessCondition.Status.Ready;
		}
		if (this.rocketModule.CraftInterface.GetRobotPilotModule() != null)
		{
			return ProcessCondition.Status.Warning;
		}
		return ProcessCondition.Status.Failure;
	}

	// Token: 0x06005856 RID: 22614 RVA: 0x001FF744 File Offset: 0x001FD944
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.PILOT_BOARDED.READY;
		}
		if (status == ProcessCondition.Status.Warning && this.rocketModule.CraftInterface.GetRobotPilotModule() != null)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.PILOT_BOARDED.ROBO_PILOT_WARNING;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.PILOT_BOARDED.FAILURE;
	}

	// Token: 0x06005857 RID: 22615 RVA: 0x001FF794 File Offset: 0x001FD994
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.PILOT_BOARDED.TOOLTIP.READY;
		}
		if (status == ProcessCondition.Status.Warning && this.rocketModule.CraftInterface.GetRobotPilotModule() != null)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.PILOT_BOARDED.TOOLTIP.ROBO_PILOT_WARNING;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.PILOT_BOARDED.TOOLTIP.FAILURE;
	}

	// Token: 0x06005858 RID: 22616 RVA: 0x001FF7E1 File Offset: 0x001FD9E1
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04003AC6 RID: 15046
	private PassengerRocketModule module;

	// Token: 0x04003AC7 RID: 15047
	private RocketModuleCluster rocketModule;
}
