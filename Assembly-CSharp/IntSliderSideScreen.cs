using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000E01 RID: 3585
public class IntSliderSideScreen : SideScreenContent
{
	// Token: 0x0600711A RID: 28954 RVA: 0x002B0118 File Offset: 0x002AE318
	protected override void OnSpawn()
	{
		base.OnSpawn();
		for (int i = 0; i < this.sliderSets.Count; i++)
		{
			this.sliderSets[i].SetupSlider(i);
			this.sliderSets[i].valueSlider.wholeNumbers = true;
		}
	}

	// Token: 0x0600711B RID: 28955 RVA: 0x002B016A File Offset: 0x002AE36A
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<IIntSliderControl>() != null || target.GetSMI<IIntSliderControl>() != null;
	}

	// Token: 0x0600711C RID: 28956 RVA: 0x002B0180 File Offset: 0x002AE380
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.target = new_target.GetComponent<IIntSliderControl>();
		if (this.target == null)
		{
			this.target = new_target.GetSMI<IIntSliderControl>();
		}
		if (this.target == null)
		{
			global::Debug.LogError("The gameObject received does not contain a Manual Generator component");
			return;
		}
		this.titleKey = this.target.SliderTitleKey;
		for (int i = 0; i < this.sliderSets.Count; i++)
		{
			this.sliderSets[i].SetTarget(this.target, i);
		}
	}

	// Token: 0x04004DD2 RID: 19922
	private IIntSliderControl target;

	// Token: 0x04004DD3 RID: 19923
	public List<SliderSet> sliderSets;
}
