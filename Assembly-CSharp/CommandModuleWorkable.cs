using System;
using UnityEngine;

// Token: 0x020006F1 RID: 1777
[AddComponentMenu("KMonoBehaviour/Workable/CommandModuleWorkable")]
public class CommandModuleWorkable : Workable
{
	// Token: 0x06002C32 RID: 11314 RVA: 0x000FEC54 File Offset: 0x000FCE54
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetOffsets(CommandModuleWorkable.entryOffsets);
		this.synchronizeAnims = false;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_incubator_kanim") };
		base.SetWorkTime(float.PositiveInfinity);
		this.showProgressBar = false;
	}

	// Token: 0x06002C33 RID: 11315 RVA: 0x000FECA9 File Offset: 0x000FCEA9
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
	}

	// Token: 0x06002C34 RID: 11316 RVA: 0x000FECB4 File Offset: 0x000FCEB4
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		if (!(worker != null))
		{
			return base.OnWorkTick(worker, dt);
		}
		if (DlcManager.IsExpansion1Active())
		{
			GameObject gameObject = worker.gameObject;
			base.CompleteWork(worker);
			base.GetComponent<ClustercraftExteriorDoor>().FerryMinion(gameObject);
			return true;
		}
		GameObject gameObject2 = worker.gameObject;
		base.CompleteWork(worker);
		base.GetComponent<MinionStorage>().SerializeMinion(gameObject2);
		return true;
	}

	// Token: 0x06002C35 RID: 11317 RVA: 0x000FED11 File Offset: 0x000FCF11
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
	}

	// Token: 0x06002C36 RID: 11318 RVA: 0x000FED1A File Offset: 0x000FCF1A
	protected override void OnCompleteWork(WorkerBase worker)
	{
	}

	// Token: 0x04001A1A RID: 6682
	private static CellOffset[] entryOffsets = new CellOffset[]
	{
		new CellOffset(0, 0),
		new CellOffset(0, 1),
		new CellOffset(0, 2),
		new CellOffset(0, 3),
		new CellOffset(0, 4)
	};
}
