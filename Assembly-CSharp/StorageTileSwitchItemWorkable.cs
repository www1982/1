using System;

// Token: 0x020007D4 RID: 2004
public class StorageTileSwitchItemWorkable : Workable
{
	// Token: 0x17000386 RID: 902
	// (get) Token: 0x060035DF RID: 13791 RVA: 0x0012CB56 File Offset: 0x0012AD56
	// (set) Token: 0x060035DE RID: 13790 RVA: 0x0012CB4D File Offset: 0x0012AD4D
	public int LastCellWorkerUsed { get; private set; } = -1;

	// Token: 0x060035E0 RID: 13792 RVA: 0x0012CB5E File Offset: 0x0012AD5E
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_use_remote_kanim") };
		base.SetOffsetTable(OffsetGroups.InvertedStandardTable);
		this.faceTargetWhenWorking = true;
		this.synchronizeAnims = false;
	}

	// Token: 0x060035E1 RID: 13793 RVA: 0x0012CB9D File Offset: 0x0012AD9D
	protected override void OnSpawn()
	{
		base.OnSpawn();
		base.SetWorkTime(3f);
	}

	// Token: 0x060035E2 RID: 13794 RVA: 0x0012CBB0 File Offset: 0x0012ADB0
	protected override void OnCompleteWork(WorkerBase worker)
	{
		if (worker != null)
		{
			this.LastCellWorkerUsed = Grid.PosToCell(worker.transform.GetPosition());
		}
		base.OnCompleteWork(worker);
	}

	// Token: 0x040020A1 RID: 8353
	private const string animName = "anim_use_remote_kanim";
}
