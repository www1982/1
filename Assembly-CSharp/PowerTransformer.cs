using System;
using System.Diagnostics;

// Token: 0x020007AB RID: 1963
[DebuggerDisplay("{name}")]
public class PowerTransformer : Generator
{
	// Token: 0x06003402 RID: 13314 RVA: 0x00123F58 File Offset: 0x00122158
	protected override void OnSpawn()
	{
		base.OnSpawn();
		this.battery = base.GetComponent<Battery>();
		base.Subscribe<PowerTransformer>(-592767678, PowerTransformer.OnOperationalChangedDelegate);
		this.UpdateJoulesLostPerSecond();
	}

	// Token: 0x06003403 RID: 13315 RVA: 0x00123F83 File Offset: 0x00122183
	public override void ApplyDeltaJoules(float joules_delta, bool can_over_power = false)
	{
		this.battery.ConsumeEnergy(-joules_delta);
		base.ApplyDeltaJoules(joules_delta, can_over_power);
	}

	// Token: 0x06003404 RID: 13316 RVA: 0x00123F9A File Offset: 0x0012219A
	public override void ConsumeEnergy(float joules)
	{
		this.battery.ConsumeEnergy(joules);
		base.ConsumeEnergy(joules);
	}

	// Token: 0x06003405 RID: 13317 RVA: 0x00123FAF File Offset: 0x001221AF
	private void OnOperationalChanged(object data)
	{
		this.UpdateJoulesLostPerSecond();
	}

	// Token: 0x06003406 RID: 13318 RVA: 0x00123FB7 File Offset: 0x001221B7
	private void UpdateJoulesLostPerSecond()
	{
		if (this.operational.IsOperational)
		{
			this.battery.joulesLostPerSecond = 0f;
			return;
		}
		this.battery.joulesLostPerSecond = 3.3333333f;
	}

	// Token: 0x06003407 RID: 13319 RVA: 0x00123FE8 File Offset: 0x001221E8
	public override void EnergySim200ms(float dt)
	{
		base.EnergySim200ms(dt);
		float num = (this.operational.IsOperational ? Math.Min(this.battery.JoulesAvailable, base.WattageRating * dt) : 0f);
		base.AssignJoulesAvailable(num);
		ushort circuitID = this.battery.CircuitID;
		ushort circuitID2 = base.CircuitID;
		bool flag = circuitID == circuitID2 && circuitID != ushort.MaxValue;
		if (this.mLoopDetected != flag)
		{
			this.mLoopDetected = flag;
			this.selectable.ToggleStatusItem(Db.Get().BuildingStatusItems.PowerLoopDetected, this.mLoopDetected, this);
		}
	}

	// Token: 0x04001F54 RID: 8020
	private Battery battery;

	// Token: 0x04001F55 RID: 8021
	private bool mLoopDetected;

	// Token: 0x04001F56 RID: 8022
	private static readonly EventSystem.IntraObjectHandler<PowerTransformer> OnOperationalChangedDelegate = new EventSystem.IntraObjectHandler<PowerTransformer>(delegate(PowerTransformer component, object data)
	{
		component.OnOperationalChanged(data);
	});
}
