using System;
using System.Collections.Generic;
using STRINGS;

// Token: 0x02000B80 RID: 2944
public class ConditionHasAstronaut : ProcessCondition
{
	// Token: 0x0600581B RID: 22555 RVA: 0x001FE95B File Offset: 0x001FCB5B
	public ConditionHasAstronaut(CommandModule module)
	{
		this.module = module;
	}

	// Token: 0x0600581C RID: 22556 RVA: 0x001FE96C File Offset: 0x001FCB6C
	public override ProcessCondition.Status EvaluateCondition()
	{
		List<MinionStorage.Info> storedMinionInfo = this.module.GetComponent<MinionStorage>().GetStoredMinionInfo();
		if (storedMinionInfo.Count > 0 && storedMinionInfo[0].serializedMinion != null)
		{
			return ProcessCondition.Status.Ready;
		}
		return ProcessCondition.Status.Failure;
	}

	// Token: 0x0600581D RID: 22557 RVA: 0x001FE9A4 File Offset: 0x001FCBA4
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.ASTRONAUT_TITLE;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.ASTRONAUGHT;
	}

	// Token: 0x0600581E RID: 22558 RVA: 0x001FE9BF File Offset: 0x001FCBBF
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.LAUNCHCHECKLIST.HASASTRONAUT;
		}
		return UI.STARMAP.LAUNCHCHECKLIST.ASTRONAUGHT;
	}

	// Token: 0x0600581F RID: 22559 RVA: 0x001FE9DA File Offset: 0x001FCBDA
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04003AB9 RID: 15033
	private CommandModule module;
}
