using System;
using System.Collections.Generic;
using STRINGS;

// Token: 0x02000B86 RID: 2950
public class ConditionHasNosecone : ProcessCondition
{
	// Token: 0x0600583B RID: 22587 RVA: 0x001FF213 File Offset: 0x001FD413
	public ConditionHasNosecone(LaunchableRocketCluster launchable)
	{
		this.launchable = launchable;
	}

	// Token: 0x0600583C RID: 22588 RVA: 0x001FF224 File Offset: 0x001FD424
	public override ProcessCondition.Status EvaluateCondition()
	{
		using (IEnumerator<Ref<RocketModuleCluster>> enumerator = this.launchable.parts.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Get().HasTag(GameTags.NoseRocketModule))
				{
					return ProcessCondition.Status.Ready;
				}
			}
		}
		return ProcessCondition.Status.Failure;
	}

	// Token: 0x0600583D RID: 22589 RVA: 0x001FF288 File Offset: 0x001FD488
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.HAS_NOSECONE.STATUS.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.HAS_NOSECONE.STATUS.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.HAS_NOSECONE.STATUS.FAILURE;
		}
		return text;
	}

	// Token: 0x0600583E RID: 22590 RVA: 0x001FF2C8 File Offset: 0x001FD4C8
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		string text;
		if (status != ProcessCondition.Status.Failure)
		{
			if (status == ProcessCondition.Status.Ready)
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.HAS_NOSECONE.TOOLTIP.READY;
			}
			else
			{
				text = UI.STARMAP.LAUNCHCHECKLIST.HAS_NOSECONE.TOOLTIP.WARNING;
			}
		}
		else
		{
			text = UI.STARMAP.LAUNCHCHECKLIST.HAS_NOSECONE.TOOLTIP.FAILURE;
		}
		return text;
	}

	// Token: 0x0600583F RID: 22591 RVA: 0x001FF308 File Offset: 0x001FD508
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04003ABF RID: 15039
	private LaunchableRocketCluster launchable;
}
