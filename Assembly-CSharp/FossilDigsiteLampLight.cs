using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000AA4 RID: 2724
public class FossilDigsiteLampLight : Light2D
{
	// Token: 0x17000576 RID: 1398
	// (get) Token: 0x06004F03 RID: 20227 RVA: 0x001C9048 File Offset: 0x001C7248
	// (set) Token: 0x06004F02 RID: 20226 RVA: 0x001C903F File Offset: 0x001C723F
	public bool independent { get; private set; }

	// Token: 0x06004F04 RID: 20228 RVA: 0x001C9050 File Offset: 0x001C7250
	protected override void OnPrefabInit()
	{
		base.Subscribe<FossilDigsiteLampLight>(-592767678, FossilDigsiteLampLight.OnOperationalChangedDelegate);
		base.IntensityAnimation = 1f;
	}

	// Token: 0x06004F05 RID: 20229 RVA: 0x001C9070 File Offset: 0x001C7270
	public void SetIndependentState(bool isIndependent, bool checkOperational = true)
	{
		this.independent = isIndependent;
		Operational component = base.GetComponent<Operational>();
		if (component != null && this.independent && checkOperational && base.enabled != component.IsOperational)
		{
			base.enabled = component.IsOperational;
		}
	}

	// Token: 0x06004F06 RID: 20230 RVA: 0x001C90BB File Offset: 0x001C72BB
	public override List<Descriptor> GetDescriptors(GameObject go)
	{
		if (this.independent || base.enabled)
		{
			return base.GetDescriptors(go);
		}
		return new List<Descriptor>();
	}

	// Token: 0x04003472 RID: 13426
	private static readonly EventSystem.IntraObjectHandler<FossilDigsiteLampLight> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<FossilDigsiteLampLight>(delegate(FossilDigsiteLampLight light, object data)
	{
		if (light.independent)
		{
			light.enabled = (bool)data;
		}
	});
}
