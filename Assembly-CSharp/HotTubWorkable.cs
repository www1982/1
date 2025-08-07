using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x02000957 RID: 2391
[AddComponentMenu("KMonoBehaviour/Workable/HotTubWorkable")]
public class HotTubWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x060044B7 RID: 17591 RVA: 0x0018AB31 File Offset: 0x00188D31
	private HotTubWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x060044B8 RID: 17592 RVA: 0x0018AB41 File Offset: 0x00188D41
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.synchronizeAnims = false;
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		this.faceTargetWhenWorking = true;
		base.SetWorkTime(90f);
	}

	// Token: 0x060044B9 RID: 17593 RVA: 0x0018AB70 File Offset: 0x00188D70
	public override Workable.AnimInfo GetAnim(WorkerBase worker)
	{
		Workable.AnimInfo anim = base.GetAnim(worker);
		anim.smi = new HotTubWorkerStateMachine.StatesInstance(worker);
		return anim;
	}

	// Token: 0x060044BA RID: 17594 RVA: 0x0018AB93 File Offset: 0x00188D93
	protected override void OnStartWork(WorkerBase worker)
	{
		this.faceLeft = global::UnityEngine.Random.value > 0.5f;
		worker.GetComponent<Effects>().Add("HotTubRelaxing", false);
	}

	// Token: 0x060044BB RID: 17595 RVA: 0x0018ABBD File Offset: 0x00188DBD
	protected override void OnStopWork(WorkerBase worker)
	{
		worker.GetComponent<Effects>().Remove("HotTubRelaxing");
	}

	// Token: 0x060044BC RID: 17596 RVA: 0x0018ABCF File Offset: 0x00188DCF
	public override Vector3 GetFacingTarget()
	{
		return base.transform.GetPosition() + (this.faceLeft ? Vector3.left : Vector3.right);
	}

	// Token: 0x060044BD RID: 17597 RVA: 0x0018ABF8 File Offset: 0x00188DF8
	protected override void OnCompleteWork(WorkerBase worker)
	{
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.hotTub.trackingEffect))
		{
			component.Add(this.hotTub.trackingEffect, true);
		}
		if (!string.IsNullOrEmpty(this.hotTub.specificEffect))
		{
			component.Add(this.hotTub.specificEffect, true);
		}
		component.Add("WarmTouch", true).timeRemaining = 1800f;
	}

	// Token: 0x060044BE RID: 17598 RVA: 0x0018AC6C File Offset: 0x00188E6C
	public bool GetWorkerPriority(WorkerBase worker, out int priority)
	{
		priority = this.hotTub.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.hotTub.trackingEffect) && component.HasEffect(this.hotTub.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (!string.IsNullOrEmpty(this.hotTub.specificEffect) && component.HasEffect(this.hotTub.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x04002E02 RID: 11778
	public HotTub hotTub;

	// Token: 0x04002E03 RID: 11779
	private bool faceLeft;
}
