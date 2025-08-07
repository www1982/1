using System;

// Token: 0x020007DE RID: 2014
public class TeleporterWorkableUse : Workable
{
	// Token: 0x0600366B RID: 13931 RVA: 0x0012EFAA File Offset: 0x0012D1AA
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x0600366C RID: 13932 RVA: 0x0012EFB2 File Offset: 0x0012D1B2
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.SetWorkTime(5f);
		this.resetProgressOnStop = true;
	}

	// Token: 0x0600366D RID: 13933 RVA: 0x0012EFCC File Offset: 0x0012D1CC
	protected override void OnStartWork(WorkerBase worker)
	{
		Teleporter component = base.GetComponent<Teleporter>();
		Teleporter teleporter = component.FindTeleportTarget();
		component.SetTeleportTarget(teleporter);
		TeleportalPad.StatesInstance smi = teleporter.GetSMI<TeleportalPad.StatesInstance>();
		smi.sm.targetTeleporter.Trigger(smi);
	}

	// Token: 0x0600366E RID: 13934 RVA: 0x0012F004 File Offset: 0x0012D204
	protected override void OnStopWork(WorkerBase worker)
	{
		TeleportalPad.StatesInstance smi = this.GetSMI<TeleportalPad.StatesInstance>();
		smi.sm.doTeleport.Trigger(smi);
	}
}
