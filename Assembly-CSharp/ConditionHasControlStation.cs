using System;
using STRINGS;

// Token: 0x02000B83 RID: 2947
public class ConditionHasControlStation : ProcessCondition
{
	// Token: 0x0600582B RID: 22571 RVA: 0x001FEC40 File Offset: 0x001FCE40
	public ConditionHasControlStation(RocketModuleCluster module)
	{
		this.module = module;
	}

	// Token: 0x0600582C RID: 22572 RVA: 0x001FEC50 File Offset: 0x001FCE50
	public override ProcessCondition.Status EvaluateCondition()
	{
		ProcessCondition.Status status = ProcessCondition.Status.Failure;
		if (Components.RocketControlStations.GetWorldItems(this.module.CraftInterface.GetComponent<WorldContainer>().id, false).Count > 0)
		{
			status = ProcessCondition.Status.Ready;
		}
		else if (this.module.CraftInterface.GetRobotPilotModule() != null)
		{
			status = ProcessCondition.Status.Warning;
		}
		return status;
	}

	// Token: 0x0600582D RID: 22573 RVA: 0x001FECA6 File Offset: 0x001FCEA6
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.HAS_CONTROLSTATION.STATUS.READY;
		}
		if (status == ProcessCondition.Status.Warning)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.HAS_CONTROLSTATION.STATUS.WARNING;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.HAS_CONTROLSTATION.STATUS.FAILURE;
	}

	// Token: 0x0600582E RID: 22574 RVA: 0x001FECD0 File Offset: 0x001FCED0
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.HAS_CONTROLSTATION.TOOLTIP.READY;
		}
		if (status == ProcessCondition.Status.Warning)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.HAS_CONTROLSTATION.TOOLTIP.WARNING_ROBO_PILOT;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.HAS_CONTROLSTATION.TOOLTIP.FAILURE;
	}

	// Token: 0x0600582F RID: 22575 RVA: 0x001FECFA File Offset: 0x001FCEFA
	public override bool ShowInUI()
	{
		return this.EvaluateCondition() != ProcessCondition.Status.Ready;
	}

	// Token: 0x04003ABC RID: 15036
	private RocketModuleCluster module;
}
