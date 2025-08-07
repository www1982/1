using System;
using System.Collections.Generic;
using STRINGS;

// Token: 0x02000B82 RID: 2946
public class ConditionHasCargoBayForNoseconeHarvest : ProcessCondition
{
	// Token: 0x06005825 RID: 22565 RVA: 0x001FEAB5 File Offset: 0x001FCCB5
	public ConditionHasCargoBayForNoseconeHarvest(LaunchableRocketCluster launchable)
	{
		this.launchable = launchable;
	}

	// Token: 0x06005826 RID: 22566 RVA: 0x001FEAC4 File Offset: 0x001FCCC4
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (!this.HasHarvestNosecone())
		{
			return ProcessCondition.Status.Ready;
		}
		using (IEnumerator<Ref<RocketModuleCluster>> enumerator = this.launchable.parts.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Get().GetComponent<CargoBayCluster>())
				{
					return ProcessCondition.Status.Ready;
				}
			}
		}
		return ProcessCondition.Status.Warning;
	}

	// Token: 0x06005827 RID: 22567 RVA: 0x001FEB30 File Offset: 0x001FCD30
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		string text = "";
		switch (status)
		{
		case ProcessCondition.Status.Failure:
			text = UI.STARMAP.LAUNCHCHECKLIST.HAS_CARGO_BAY_FOR_NOSECONE_HARVEST.STATUS.FAILURE;
			break;
		case ProcessCondition.Status.Warning:
			text = UI.STARMAP.LAUNCHCHECKLIST.HAS_CARGO_BAY_FOR_NOSECONE_HARVEST.STATUS.WARNING;
			break;
		case ProcessCondition.Status.Ready:
			text = UI.STARMAP.LAUNCHCHECKLIST.HAS_CARGO_BAY_FOR_NOSECONE_HARVEST.STATUS.READY;
			break;
		}
		return text;
	}

	// Token: 0x06005828 RID: 22568 RVA: 0x001FEB80 File Offset: 0x001FCD80
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		string text = "";
		switch (status)
		{
		case ProcessCondition.Status.Failure:
			text = UI.STARMAP.LAUNCHCHECKLIST.HAS_CARGO_BAY_FOR_NOSECONE_HARVEST.TOOLTIP.FAILURE;
			break;
		case ProcessCondition.Status.Warning:
			text = UI.STARMAP.LAUNCHCHECKLIST.HAS_CARGO_BAY_FOR_NOSECONE_HARVEST.TOOLTIP.WARNING;
			break;
		case ProcessCondition.Status.Ready:
			text = UI.STARMAP.LAUNCHCHECKLIST.HAS_CARGO_BAY_FOR_NOSECONE_HARVEST.TOOLTIP.READY;
			break;
		}
		return text;
	}

	// Token: 0x06005829 RID: 22569 RVA: 0x001FEBCD File Offset: 0x001FCDCD
	public override bool ShowInUI()
	{
		return this.HasHarvestNosecone();
	}

	// Token: 0x0600582A RID: 22570 RVA: 0x001FEBD8 File Offset: 0x001FCDD8
	private bool HasHarvestNosecone()
	{
		using (IEnumerator<Ref<RocketModuleCluster>> enumerator = this.launchable.parts.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Get().HasTag("NoseconeHarvest"))
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x04003ABB RID: 15035
	private LaunchableRocketCluster launchable;
}
