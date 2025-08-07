using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000DF3 RID: 3571
public class DualSliderSideScreen : SideScreenContent
{
	// Token: 0x060070C9 RID: 28873 RVA: 0x002AE5BC File Offset: 0x002AC7BC
	protected override void OnSpawn()
	{
		base.OnSpawn();
		for (int i = 0; i < this.sliderSets.Count; i++)
		{
			this.sliderSets[i].SetupSlider(i);
		}
	}

	// Token: 0x060070CA RID: 28874 RVA: 0x002AE5F7 File Offset: 0x002AC7F7
	public override bool IsValidForTarget(GameObject target)
	{
		return target.GetComponent<IDualSliderControl>() != null;
	}

	// Token: 0x060070CB RID: 28875 RVA: 0x002AE604 File Offset: 0x002AC804
	public override void SetTarget(GameObject new_target)
	{
		if (new_target == null)
		{
			global::Debug.LogError("Invalid gameObject received");
			return;
		}
		this.target = new_target.GetComponent<IDualSliderControl>();
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

	// Token: 0x04004D9A RID: 19866
	private IDualSliderControl target;

	// Token: 0x04004D9B RID: 19867
	public List<SliderSet> sliderSets;
}
