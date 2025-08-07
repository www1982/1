using System;
using STRINGS;
using UnityEngine;

// Token: 0x02000DD6 RID: 3542
public class AutomatableSideScreen : SideScreenContent
{
	// Token: 0x06006FCE RID: 28622 RVA: 0x002A8DD1 File Offset: 0x002A6FD1
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06006FCF RID: 28623 RVA: 0x002A8DDC File Offset: 0x002A6FDC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.allowManualToggle.transform.parent.GetComponent<ToolTip>().SetSimpleTooltip(UI.UISIDESCREENS.AUTOMATABLE_SIDE_SCREEN.ALLOWMANUALBUTTONTOOLTIP);
		this.allowManualToggle.onValueChanged += this.OnAllowManualChanged;
	}

	// Token: 0x06006FD0 RID: 28624 RVA: 0x002A8E2A File Offset: 0x002A702A
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<Automatable>() != null;
	}

	// Token: 0x06006FD1 RID: 28625 RVA: 0x002A8E38 File Offset: 0x002A7038
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		if (target == null)
		{
			global::Debug.LogError("The target object provided was null");
			return;
		}
		this.targetAutomatable = target.GetComponent<Automatable>();
		if (this.targetAutomatable == null)
		{
			global::Debug.LogError("The target provided does not have an Automatable component");
			return;
		}
		this.allowManualToggle.isOn = !this.targetAutomatable.GetAutomationOnly();
		this.allowManualToggleCheckMark.enabled = this.allowManualToggle.isOn;
	}

	// Token: 0x06006FD2 RID: 28626 RVA: 0x002A8EB4 File Offset: 0x002A70B4
	private void OnAllowManualChanged(bool value)
	{
		this.targetAutomatable.SetAutomationOnly(!value);
		this.allowManualToggleCheckMark.enabled = value;
	}

	// Token: 0x04004CE5 RID: 19685
	public KToggle allowManualToggle;

	// Token: 0x04004CE6 RID: 19686
	public KImage allowManualToggleCheckMark;

	// Token: 0x04004CE7 RID: 19687
	public GameObject content;

	// Token: 0x04004CE8 RID: 19688
	private GameObject target;

	// Token: 0x04004CE9 RID: 19689
	public LocText DescriptionText;

	// Token: 0x04004CEA RID: 19690
	private Automatable targetAutomatable;
}
