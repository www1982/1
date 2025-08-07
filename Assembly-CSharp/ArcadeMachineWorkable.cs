using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x020006A4 RID: 1700
[AddComponentMenu("KMonoBehaviour/Workable/ArcadeMachineWorkable")]
public class ArcadeMachineWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x0600296A RID: 10602 RVA: 0x000F0E02 File Offset: 0x000EF002
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetReportType(ReportManager.ReportType.PersonalTime);
		this.synchronizeAnims = false;
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		base.SetWorkTime(15f);
	}

	// Token: 0x0600296B RID: 10603 RVA: 0x000F0E32 File Offset: 0x000EF032
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		worker.GetComponent<Effects>().Add("ArcadePlaying", false);
	}

	// Token: 0x0600296C RID: 10604 RVA: 0x000F0E4D File Offset: 0x000EF04D
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		worker.GetComponent<Effects>().Remove("ArcadePlaying");
	}

	// Token: 0x0600296D RID: 10605 RVA: 0x000F0E68 File Offset: 0x000EF068
	protected override void OnCompleteWork(WorkerBase worker)
	{
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(ArcadeMachineWorkable.trackingEffect))
		{
			component.Add(ArcadeMachineWorkable.trackingEffect, true);
		}
		if (!string.IsNullOrEmpty(ArcadeMachineWorkable.specificEffect))
		{
			component.Add(ArcadeMachineWorkable.specificEffect, true);
		}
	}

	// Token: 0x0600296E RID: 10606 RVA: 0x000F0EB0 File Offset: 0x000EF0B0
	public bool GetWorkerPriority(WorkerBase worker, out int priority)
	{
		priority = this.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(ArcadeMachineWorkable.trackingEffect) && component.HasEffect(ArcadeMachineWorkable.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (!string.IsNullOrEmpty(ArcadeMachineWorkable.specificEffect) && component.HasEffect(ArcadeMachineWorkable.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x0400188D RID: 6285
	public ArcadeMachine owner;

	// Token: 0x0400188E RID: 6286
	public int basePriority = RELAXATION.PRIORITY.TIER3;

	// Token: 0x0400188F RID: 6287
	private static string specificEffect = "PlayedArcade";

	// Token: 0x04001890 RID: 6288
	private static string trackingEffect = "RecentlyPlayedArcade";
}
