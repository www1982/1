using System;
using STRINGS;

// Token: 0x02000B8E RID: 2958
public class ConditionRocketHeight : ProcessCondition
{
	// Token: 0x06005865 RID: 22629 RVA: 0x001FFE3C File Offset: 0x001FE03C
	public ConditionRocketHeight(RocketEngineCluster engine)
	{
		this.engine = engine;
	}

	// Token: 0x06005866 RID: 22630 RVA: 0x001FFE4B File Offset: 0x001FE04B
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (this.engine.maxHeight < this.engine.GetComponent<RocketModuleCluster>().CraftInterface.RocketHeight)
		{
			return ProcessCondition.Status.Failure;
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x06005867 RID: 22631 RVA: 0x001FFE74 File Offset: 0x001FE074
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.MAX_HEIGHT.STATUS.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.MAX_HEIGHT.STATUS.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.MAX_HEIGHT.STATUS.FAILURE;
		}
		return text;
	}

	// Token: 0x06005868 RID: 22632 RVA: 0x001FFEB4 File Offset: 0x001FE0B4
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.MAX_HEIGHT.TOOLTIP.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.MAX_HEIGHT.TOOLTIP.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.MAX_HEIGHT.TOOLTIP.FAILURE;
		}
		return text;
	}

	// Token: 0x06005869 RID: 22633 RVA: 0x001FFEF4 File Offset: 0x001FE0F4
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04003ACC RID: 15052
	private RocketEngineCluster engine;
}
