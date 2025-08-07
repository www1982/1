using System;

// Token: 0x02000794 RID: 1940
public class ModuleBattery : Battery
{
	// Token: 0x0600333A RID: 13114 RVA: 0x00120A04 File Offset: 0x0011EC04
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.connectedTags = new Tag[0];
		base.IsVirtual = true;
	}

	// Token: 0x0600333B RID: 13115 RVA: 0x00120A20 File Offset: 0x0011EC20
	protected override void OnSpawn()
	{
		CraftModuleInterface craftInterface = base.GetComponent<RocketModuleCluster>().CraftInterface;
		base.VirtualCircuitKey = craftInterface;
		base.OnSpawn();
		this.meter.gameObject.GetComponent<KBatchedAnimTracker>().matchParentOffset = true;
	}
}
