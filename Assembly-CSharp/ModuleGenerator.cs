using System;

// Token: 0x02000A23 RID: 2595
public class ModuleGenerator : Generator
{
	// Token: 0x06004B52 RID: 19282 RVA: 0x001B5110 File Offset: 0x001B3310
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.connectedTags = new Tag[0];
		base.IsVirtual = true;
	}

	// Token: 0x06004B53 RID: 19283 RVA: 0x001B512C File Offset: 0x001B332C
	protected override void OnSpawn()
	{
		CraftModuleInterface craftInterface = base.GetComponent<RocketModuleCluster>().CraftInterface;
		base.VirtualCircuitKey = craftInterface;
		this.clustercraft = craftInterface.GetComponent<Clustercraft>();
		Game.Instance.electricalConduitSystem.AddToVirtualNetworks(base.VirtualCircuitKey, this, true);
		base.OnSpawn();
	}

	// Token: 0x06004B54 RID: 19284 RVA: 0x001B5175 File Offset: 0x001B3375
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		Game.Instance.electricalConduitSystem.RemoveFromVirtualNetworks(base.VirtualCircuitKey, this, true);
	}

	// Token: 0x06004B55 RID: 19285 RVA: 0x001B5194 File Offset: 0x001B3394
	public override bool IsProducingPower()
	{
		return this.clustercraft.IsFlightInProgress();
	}

	// Token: 0x06004B56 RID: 19286 RVA: 0x001B51A4 File Offset: 0x001B33A4
	public override void EnergySim200ms(float dt)
	{
		base.EnergySim200ms(dt);
		if (this.IsProducingPower())
		{
			base.GenerateJoules(base.WattageRating * dt, false);
			if (this.poweringStatusItemHandle == Guid.Empty)
			{
				this.poweringStatusItemHandle = this.selectable.ReplaceStatusItem(this.notPoweringStatusItemHandle, Db.Get().BuildingStatusItems.ModuleGeneratorPowered, this);
				this.notPoweringStatusItemHandle = Guid.Empty;
				return;
			}
		}
		else if (this.notPoweringStatusItemHandle == Guid.Empty)
		{
			this.notPoweringStatusItemHandle = this.selectable.ReplaceStatusItem(this.poweringStatusItemHandle, Db.Get().BuildingStatusItems.ModuleGeneratorNotPowered, this);
			this.poweringStatusItemHandle = Guid.Empty;
		}
	}

	// Token: 0x0400320A RID: 12810
	private Clustercraft clustercraft;

	// Token: 0x0400320B RID: 12811
	private Guid poweringStatusItemHandle;

	// Token: 0x0400320C RID: 12812
	private Guid notPoweringStatusItemHandle;
}
