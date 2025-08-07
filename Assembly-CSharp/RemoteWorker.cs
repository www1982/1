using System;
using Klei.AI;
using UnityEngine;

// Token: 0x02000A9A RID: 2714
public class RemoteWorker : StandardWorker
{
	// Token: 0x06004EB6 RID: 20150 RVA: 0x001C73E4 File Offset: 0x001C55E4
	public override Attributes GetAttributes()
	{
		RemoteWorkerDock homeDepot = this.remoteWorkerSM.HomeDepot;
		WorkerBase workerBase = ((homeDepot != null) ? homeDepot.GetActiveTerminalWorker() : null) ?? null;
		if (workerBase != null)
		{
			return workerBase.GetAttributes();
		}
		return null;
	}

	// Token: 0x06004EB7 RID: 20151 RVA: 0x001C7420 File Offset: 0x001C5620
	public override AttributeConverterInstance GetAttributeConverter(string id)
	{
		RemoteWorkerDock homeDepot = this.remoteWorkerSM.HomeDepot;
		WorkerBase workerBase = ((homeDepot != null) ? homeDepot.GetActiveTerminalWorker() : null) ?? null;
		if (workerBase != null)
		{
			return workerBase.GetAttributeConverter(id);
		}
		return null;
	}

	// Token: 0x06004EB8 RID: 20152 RVA: 0x001C745C File Offset: 0x001C565C
	protected override void TryPlayingIdle()
	{
		if (this.remoteWorkerSM.Docked)
		{
			base.GetComponent<KAnimControllerBase>().Play("in_dock_idle", KAnim.PlayMode.Once, 1f, 0f);
			return;
		}
		base.TryPlayingIdle();
	}

	// Token: 0x06004EB9 RID: 20153 RVA: 0x001C7494 File Offset: 0x001C5694
	protected override void InternalStopWork(Workable target_workable, bool is_aborted)
	{
		base.InternalStopWork(target_workable, is_aborted);
		Vector3 position = base.transform.GetPosition();
		RemoteWorkerSM remoteWorkerSM = this.remoteWorkerSM;
		position.z = Grid.GetLayerZ((remoteWorkerSM != null && remoteWorkerSM.Docked) ? Grid.SceneLayer.BuildingUse : Grid.SceneLayer.Move);
		base.transform.SetPosition(position);
	}

	// Token: 0x0400343F RID: 13375
	[MyCmpGet]
	private RemoteWorkerSM remoteWorkerSM;
}
