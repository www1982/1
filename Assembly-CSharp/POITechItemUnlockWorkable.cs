using System;

// Token: 0x02000A51 RID: 2641
public class POITechItemUnlockWorkable : Workable
{
	// Token: 0x06004C92 RID: 19602 RVA: 0x001BC648 File Offset: 0x001BA848
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = Db.Get().DuplicantStatusItems.ResearchingFromPOI;
		this.alwaysShowProgressBar = true;
		this.resetProgressOnStop = false;
		this.synchronizeAnims = true;
	}

	// Token: 0x06004C93 RID: 19603 RVA: 0x001BC67C File Offset: 0x001BA87C
	protected override void OnCompleteWork(WorkerBase worker)
	{
		base.OnCompleteWork(worker);
		POITechItemUnlocks.Instance smi = this.GetSMI<POITechItemUnlocks.Instance>();
		smi.UnlockTechItems();
		smi.sm.pendingChore.Set(false, smi, false);
		base.gameObject.Trigger(1980521255, null);
		Prioritizable.RemoveRef(base.gameObject);
	}
}
