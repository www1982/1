using System;
using System.Collections.Generic;
using STRINGS;
using UnityEngine;

// Token: 0x02000856 RID: 2134
public class CaloriesConsumedElementProducer : KMonoBehaviour, IGameObjectEffectDescriptor
{
	// Token: 0x06003A96 RID: 14998 RVA: 0x00145B97 File Offset: 0x00143D97
	protected override void OnSpawn()
	{
		base.OnSpawn();
		new CaloriesConsumedSecondaryExcretionMonitor.Instance(base.gameObject.GetComponent<StateMachineController>())
		{
			sm = 
			{
				producedElement = this.producedElement
			},
			sm = 
			{
				kgProducedPerKcalConsumed = this.kgProducedPerKcalConsumed
			}
		}.StartSM();
	}

	// Token: 0x06003A97 RID: 14999 RVA: 0x00145BD8 File Offset: 0x00143DD8
	public List<Descriptor> GetDescriptors(GameObject go)
	{
		return new List<Descriptor>
		{
			new Descriptor(UI.BUILDINGEFFECTS.DIET_ADDITIONAL_PRODUCED.Replace("{Items}", ElementLoader.GetElement(this.producedElement.CreateTag()).name), UI.BUILDINGEFFECTS.TOOLTIPS.DIET_ADDITIONAL_PRODUCED.Replace("{Items}", ElementLoader.GetElement(this.producedElement.CreateTag()).name), Descriptor.DescriptorType.Effect, false)
		};
	}

	// Token: 0x040023EA RID: 9194
	public SimHashes producedElement;

	// Token: 0x040023EB RID: 9195
	public float kgProducedPerKcalConsumed = 1f;
}
