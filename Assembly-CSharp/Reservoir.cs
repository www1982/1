using System;
using UnityEngine;

// Token: 0x02000ACC RID: 2764
[AddComponentMenu("KMonoBehaviour/scripts/Reservoir")]
public class Reservoir : KMonoBehaviour
{
	// Token: 0x06005056 RID: 20566 RVA: 0x001D117C File Offset: 0x001CF37C
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.meter = new MeterController(base.GetComponent<KBatchedAnimController>(), "meter_target", "meter", Meter.Offset.Infront, Grid.SceneLayer.NoLayer, new string[] { "meter_fill", "meter_OL" });
		base.Subscribe<Reservoir>(-1697596308, Reservoir.OnStorageChangeDelegate);
		this.OnStorageChange(null);
	}

	// Token: 0x06005057 RID: 20567 RVA: 0x001D11DB File Offset: 0x001CF3DB
	private void OnStorageChange(object data)
	{
		this.meter.SetPositionPercent(Mathf.Clamp01(this.storage.MassStored() / this.storage.capacityKg));
	}

	// Token: 0x04003610 RID: 13840
	private MeterController meter;

	// Token: 0x04003611 RID: 13841
	[MyCmpGet]
	private Storage storage;

	// Token: 0x04003612 RID: 13842
	private static readonly EventSystem.IntraObjectHandler<Reservoir> OnStorageChangeDelegate = new EventSystem.IntraObjectHandler<Reservoir>(delegate(Reservoir component, object data)
	{
		component.OnStorageChange(data);
	});
}
