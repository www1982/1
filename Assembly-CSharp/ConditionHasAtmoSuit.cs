using System;
using STRINGS;

// Token: 0x02000B81 RID: 2945
public class ConditionHasAtmoSuit : ProcessCondition
{
	// Token: 0x06005820 RID: 22560 RVA: 0x001FE9E0 File Offset: 0x001FCBE0
	public ConditionHasAtmoSuit(CommandModule module)
	{
		this.module = module;
		ManualDeliveryKG manualDeliveryKG = this.module.FindOrAdd<ManualDeliveryKG>();
		manualDeliveryKG.choreTypeIDHash = Db.Get().ChoreTypes.MachineFetch.IdHash;
		manualDeliveryKG.SetStorage(module.storage);
		manualDeliveryKG.RequestedItemTag = GameTags.AtmoSuit;
		manualDeliveryKG.MinimumMass = 1f;
		manualDeliveryKG.refillMass = 0.1f;
		manualDeliveryKG.capacity = 1f;
	}

	// Token: 0x06005821 RID: 22561 RVA: 0x001FEA56 File Offset: 0x001FCC56
	public override ProcessCondition.Status EvaluateCondition()
	{
		if (this.module.storage.GetAmountAvailable(GameTags.AtmoSuit) < 1f)
		{
			return ProcessCondition.Status.Failure;
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x06005822 RID: 22562 RVA: 0x001FEA7C File Offset: 0x001FCC7C
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.HASSUIT.NAME;
		}
		return UI.STARMAP.NOSUIT.NAME;
	}

	// Token: 0x06005823 RID: 22563 RVA: 0x001FEA97 File Offset: 0x001FCC97
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		if (status == ProcessCondition.Status.Ready)
		{
			return UI.STARMAP.HASSUIT.TOOLTIP;
		}
		return UI.STARMAP.NOSUIT.TOOLTIP;
	}

	// Token: 0x06005824 RID: 22564 RVA: 0x001FEAB2 File Offset: 0x001FCCB2
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04003ABA RID: 15034
	private CommandModule module;
}
