using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000B7C RID: 2940
public class CargoBayIsEmpty : ProcessCondition
{
	// Token: 0x060057FD RID: 22525 RVA: 0x001FDF74 File Offset: 0x001FC174
	public CargoBayIsEmpty(CommandModule module)
	{
		this.commandModule = module;
	}

	// Token: 0x060057FE RID: 22526 RVA: 0x001FDF84 File Offset: 0x001FC184
	public override ProcessCondition.Status EvaluateCondition()
	{
		foreach (GameObject gameObject in AttachableBuilding.GetAttachedNetwork(this.commandModule.GetComponent<AttachableBuilding>()))
		{
			CargoBay component = gameObject.GetComponent<CargoBay>();
			if (component != null && component.storage.MassStored() != 0f)
			{
				return ProcessCondition.Status.Failure;
			}
		}
		return ProcessCondition.Status.Ready;
	}

	// Token: 0x060057FF RID: 22527 RVA: 0x001FE004 File Offset: 0x001FC204
	public override string GetStatusMessage(ProcessCondition.Status status)
	{
		return UI.STARMAP.CARGOEMPTY.NAME;
	}

	// Token: 0x06005800 RID: 22528 RVA: 0x001FE010 File Offset: 0x001FC210
	public override string GetStatusTooltip(ProcessCondition.Status status)
	{
		return UI.STARMAP.CARGOEMPTY.TOOLTIP;
	}

	// Token: 0x06005801 RID: 22529 RVA: 0x001FE01C File Offset: 0x001FC21C
	public override bool ShowInUI()
	{
		return true;
	}

	// Token: 0x04003AAE RID: 15022
	private CommandModule commandModule;
}
