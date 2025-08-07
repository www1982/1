using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000702 RID: 1794
[SerializationConfig(MemberSerialization.OptIn)]
public abstract class ConduitThresholdSensor : ConduitSensor
{
	// Token: 0x17000254 RID: 596
	// (get) Token: 0x06002CE1 RID: 11489
	public abstract float CurrentValue { get; }

	// Token: 0x06002CE2 RID: 11490 RVA: 0x0010239B File Offset: 0x0010059B
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.Subscribe<ConduitThresholdSensor>(-905833192, ConduitThresholdSensor.OnCopySettingsDelegate);
	}

	// Token: 0x06002CE3 RID: 11491 RVA: 0x001023B4 File Offset: 0x001005B4
	private void OnCopySettings(object data)
	{
		ConduitThresholdSensor component = ((GameObject)data).GetComponent<ConduitThresholdSensor>();
		if (component != null)
		{
			this.Threshold = component.Threshold;
			this.ActivateAboveThreshold = component.ActivateAboveThreshold;
		}
	}

	// Token: 0x06002CE4 RID: 11492 RVA: 0x001023F0 File Offset: 0x001005F0
	protected override void ConduitUpdate(float dt)
	{
		if (this.GetContainedMass() <= 0f && !this.dirty)
		{
			return;
		}
		float currentValue = this.CurrentValue;
		this.dirty = false;
		if (this.activateAboveThreshold)
		{
			if ((currentValue > this.threshold && !base.IsSwitchedOn) || (currentValue <= this.threshold && base.IsSwitchedOn))
			{
				this.Toggle();
				return;
			}
		}
		else if ((currentValue > this.threshold && base.IsSwitchedOn) || (currentValue <= this.threshold && !base.IsSwitchedOn))
		{
			this.Toggle();
		}
	}

	// Token: 0x06002CE5 RID: 11493 RVA: 0x0010247C File Offset: 0x0010067C
	private float GetContainedMass()
	{
		int num = Grid.PosToCell(this);
		if (this.conduitType == ConduitType.Liquid || this.conduitType == ConduitType.Gas)
		{
			return Conduit.GetFlowManager(this.conduitType).GetContents(num).mass;
		}
		SolidConduitFlow flowManager = SolidConduit.GetFlowManager();
		SolidConduitFlow.ConduitContents contents = flowManager.GetContents(num);
		Pickupable pickupable = flowManager.GetPickupable(contents.pickupableHandle);
		if (pickupable != null)
		{
			return pickupable.PrimaryElement.Mass;
		}
		return 0f;
	}

	// Token: 0x17000255 RID: 597
	// (get) Token: 0x06002CE6 RID: 11494 RVA: 0x001024EF File Offset: 0x001006EF
	// (set) Token: 0x06002CE7 RID: 11495 RVA: 0x001024F7 File Offset: 0x001006F7
	public float Threshold
	{
		get
		{
			return this.threshold;
		}
		set
		{
			this.threshold = value;
			this.dirty = true;
		}
	}

	// Token: 0x17000256 RID: 598
	// (get) Token: 0x06002CE8 RID: 11496 RVA: 0x00102507 File Offset: 0x00100707
	// (set) Token: 0x06002CE9 RID: 11497 RVA: 0x0010250F File Offset: 0x0010070F
	public bool ActivateAboveThreshold
	{
		get
		{
			return this.activateAboveThreshold;
		}
		set
		{
			this.activateAboveThreshold = value;
			this.dirty = true;
		}
	}

	// Token: 0x04001A7B RID: 6779
	[SerializeField]
	[Serialize]
	protected float threshold;

	// Token: 0x04001A7C RID: 6780
	[SerializeField]
	[Serialize]
	protected bool activateAboveThreshold = true;

	// Token: 0x04001A7D RID: 6781
	[Serialize]
	private bool dirty = true;

	// Token: 0x04001A7E RID: 6782
	[MyCmpAdd]
	private CopyBuildingSettings copyBuildingSettings;

	// Token: 0x04001A7F RID: 6783
	private static readonly EventSystem.IntraObjectHandler<ConduitThresholdSensor> OnCopySettingsDelegate = new EventSystem.IntraObjectHandler<ConduitThresholdSensor>(delegate(ConduitThresholdSensor component, object data)
	{
		component.OnCopySettings(data);
	});
}
