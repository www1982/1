using System;
using UnityEngine;

// Token: 0x02000E32 RID: 3634
public class SingleCheckboxSideScreen : SideScreenContent
{
	// Token: 0x06007316 RID: 29462 RVA: 0x002BC8B7 File Offset: 0x002BAAB7
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x06007317 RID: 29463 RVA: 0x002BC8BF File Offset: 0x002BAABF
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.toggle.onValueChanged += this.OnValueChanged;
	}

	// Token: 0x06007318 RID: 29464 RVA: 0x002BC8DE File Offset: 0x002BAADE
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<ICheckboxControl>() != null || target.GetSMI<ICheckboxControl>() != null;
	}

	// Token: 0x06007319 RID: 29465 RVA: 0x002BC8F4 File Offset: 0x002BAAF4
	public override void SetTarget(GameObject target)
	{
		base.SetTarget(target);
		if (target == null)
		{
			global::Debug.LogError("The target object provided was null");
			return;
		}
		this.target = target.GetComponent<ICheckboxControl>();
		if (this.target == null)
		{
			this.target = target.GetSMI<ICheckboxControl>();
		}
		if (this.target == null)
		{
			global::Debug.LogError("The target provided does not have an ICheckboxControl component");
			return;
		}
		this.label.text = this.target.CheckboxLabel;
		this.toggle.transform.parent.GetComponent<ToolTip>().SetSimpleTooltip(this.target.CheckboxTooltip);
		this.titleKey = this.target.CheckboxTitleKey;
		this.toggle.isOn = this.target.GetCheckboxValue();
		this.toggleCheckMark.enabled = this.toggle.isOn;
	}

	// Token: 0x0600731A RID: 29466 RVA: 0x002BC9C7 File Offset: 0x002BABC7
	public override void ClearTarget()
	{
		base.ClearTarget();
		this.target = null;
	}

	// Token: 0x0600731B RID: 29467 RVA: 0x002BC9D6 File Offset: 0x002BABD6
	private void OnValueChanged(bool value)
	{
		this.target.SetCheckboxValue(value);
		this.toggleCheckMark.enabled = value;
	}

	// Token: 0x04004F46 RID: 20294
	public KToggle toggle;

	// Token: 0x04004F47 RID: 20295
	public KImage toggleCheckMark;

	// Token: 0x04004F48 RID: 20296
	public LocText label;

	// Token: 0x04004F49 RID: 20297
	private ICheckboxControl target;
}
