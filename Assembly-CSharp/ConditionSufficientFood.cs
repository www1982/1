using System;
using STRINGS;

// Token: 0x02000B8F RID: 2959
public class ConditionSufficientFood : ProcessCondition
{
	// Token: 0x0600586A RID: 22634 RVA: 0x001FFEF7 File Offset: 0x001FE0F7
	public ConditionSufficientFood(CommandModule module)
	{
		this.module = module;
	}

	// Token: 0x0600586B RID: 22635 RVA: 0x001FFF06 File Offset: 0x001FE106
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (this.module.storage.GetAmountAvailable(GameTags.Edible) <= 1f)
		{
			return ProcessCondition.Status.Failure;
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x0600586C RID: 22636 RVA: 0x001FFF29 File Offset: 0x001FE129
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.HASFOOD.NAME;
		}
		return UI.STARMAP.NOFOOD.NAME;
	}

	// Token: 0x0600586D RID: 22637 RVA: 0x001FFF44 File Offset: 0x001FE144
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.HASFOOD.TOOLTIP;
		}
		return UI.STARMAP.NOFOOD.TOOLTIP;
	}

	// Token: 0x0600586E RID: 22638 RVA: 0x001FFF5F File Offset: 0x001FE15F
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04003ACD RID: 15053
	private CommandModule module;
}
