using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x020007F5 RID: 2037
public class WatchRoboDancerWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x06003772 RID: 14194 RVA: 0x00134050 File Offset: 0x00132250
	private WatchRoboDancerWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x06003773 RID: 14195 RVA: 0x001340B8 File Offset: 0x001322B8
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.synchronizeAnims = false;
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.WatchRoboDancerWorkable;
		base.SetWorkTime(30f);
		this.showProgressBar = false;
	}

	// Token: 0x06003774 RID: 14196 RVA: 0x00134108 File Offset: 0x00132308
	protected override void OnCompleteWork(WorkerBase worker)
	{
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(WatchRoboDancerWorkable.TRACKING_EFFECT))
		{
			component.Add(WatchRoboDancerWorkable.TRACKING_EFFECT, true);
		}
		if (!string.IsNullOrEmpty(WatchRoboDancerWorkable.SPECIFIC_EFFECT))
		{
			component.Add(WatchRoboDancerWorkable.SPECIFIC_EFFECT, true);
		}
	}

	// Token: 0x06003775 RID: 14197 RVA: 0x00134150 File Offset: 0x00132350
	public bool GetWorkerPriority(WorkerBase worker, out int priority)
	{
		priority = this.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(WatchRoboDancerWorkable.TRACKING_EFFECT) && component.HasEffect(WatchRoboDancerWorkable.TRACKING_EFFECT))
		{
			priority = 0;
			return false;
		}
		if (!string.IsNullOrEmpty(WatchRoboDancerWorkable.SPECIFIC_EFFECT) && component.HasEffect(WatchRoboDancerWorkable.SPECIFIC_EFFECT))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x06003776 RID: 14198 RVA: 0x001341AB File Offset: 0x001323AB
	protected override void OnStartWork(WorkerBase worker)
	{
		worker.GetComponent<Effects>().Add("Dancing", false);
	}

	// Token: 0x06003777 RID: 14199 RVA: 0x001341BF File Offset: 0x001323BF
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		worker.GetComponent<Facing>().Face(this.owner.transform.position.x);
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x06003778 RID: 14200 RVA: 0x001341E9 File Offset: 0x001323E9
	protected override void OnStopWork(WorkerBase worker)
	{
		worker.GetComponent<Effects>().Remove("Dancing");
		ChoreHelpers.DestroyLocator(base.gameObject);
	}

	// Token: 0x06003779 RID: 14201 RVA: 0x00134208 File Offset: 0x00132408
	public override Workable.AnimInfo GetAnim(WorkerBase worker)
	{
		int num = global::UnityEngine.Random.Range(0, this.workerOverrideAnims.Length);
		this.overrideAnims = this.workerOverrideAnims[num];
		return base.GetAnim(worker);
	}

	// Token: 0x040021A8 RID: 8616
	public GameObject owner;

	// Token: 0x040021A9 RID: 8617
	public int basePriority = RELAXATION.PRIORITY.TIER3;

	// Token: 0x040021AA RID: 8618
	public static string SPECIFIC_EFFECT = "SawRoboDancer";

	// Token: 0x040021AB RID: 8619
	public static string TRACKING_EFFECT = "RecentlySawRoboDancer";

	// Token: 0x040021AC RID: 8620
	public KAnimFile[][] workerOverrideAnims = new KAnimFile[][]
	{
		new KAnimFile[] { Assets.GetAnim("anim_interacts_robotdance_kanim") },
		new KAnimFile[] { Assets.GetAnim("anim_interacts_robotdance1_kanim") }
	};
}
