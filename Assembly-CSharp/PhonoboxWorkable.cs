using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x02000A57 RID: 2647
[AddComponentMenu("KMonoBehaviour/Workable/PhonoboxWorkable")]
public class PhonoboxWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x06004CBC RID: 19644 RVA: 0x001BD370 File Offset: 0x001BB570
	private PhonoboxWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x06004CBD RID: 19645 RVA: 0x001BD409 File Offset: 0x001BB609
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.synchronizeAnims = false;
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		base.SetWorkTime(15f);
	}

	// Token: 0x06004CBE RID: 19646 RVA: 0x001BD434 File Offset: 0x001BB634
	protected override void OnCompleteWork(WorkerBase worker)
	{
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.trackingEffect))
		{
			component.Add(this.trackingEffect, true);
		}
		if (!string.IsNullOrEmpty(this.specificEffect))
		{
			component.Add(this.specificEffect, true);
		}
	}

	// Token: 0x06004CBF RID: 19647 RVA: 0x001BD480 File Offset: 0x001BB680
	public bool GetWorkerPriority(WorkerBase worker, out int priority)
	{
		priority = this.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.trackingEffect) && component.HasEffect(this.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (!string.IsNullOrEmpty(this.specificEffect) && component.HasEffect(this.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x06004CC0 RID: 19648 RVA: 0x001BD4DF File Offset: 0x001BB6DF
	protected override void OnStartWork(WorkerBase worker)
	{
		this.owner.AddWorker(worker);
		worker.GetComponent<Effects>().Add("Dancing", false);
	}

	// Token: 0x06004CC1 RID: 19649 RVA: 0x001BD4FF File Offset: 0x001BB6FF
	protected override void OnStopWork(WorkerBase worker)
	{
		this.owner.RemoveWorker(worker);
		worker.GetComponent<Effects>().Remove("Dancing");
	}

	// Token: 0x06004CC2 RID: 19650 RVA: 0x001BD520 File Offset: 0x001BB720
	public override Workable.AnimInfo GetAnim(WorkerBase worker)
	{
		int num = global::UnityEngine.Random.Range(0, this.workerOverrideAnims.Length);
		this.overrideAnims = this.workerOverrideAnims[num];
		return base.GetAnim(worker);
	}

	// Token: 0x040032E6 RID: 13030
	public Phonobox owner;

	// Token: 0x040032E7 RID: 13031
	public int basePriority = RELAXATION.PRIORITY.TIER3;

	// Token: 0x040032E8 RID: 13032
	public string specificEffect = "Danced";

	// Token: 0x040032E9 RID: 13033
	public string trackingEffect = "RecentlyDanced";

	// Token: 0x040032EA RID: 13034
	public KAnimFile[][] workerOverrideAnims = new KAnimFile[][]
	{
		new KAnimFile[] { Assets.GetAnim("anim_interacts_phonobox_danceone_kanim") },
		new KAnimFile[] { Assets.GetAnim("anim_interacts_phonobox_dancetwo_kanim") },
		new KAnimFile[] { Assets.GetAnim("anim_interacts_phonobox_dancethree_kanim") }
	};
}
