using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x02000BD7 RID: 3031
[AddComponentMenu("KMonoBehaviour/Workable/VerticalWindTunnelWorkable")]
public class VerticalWindTunnelWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x06005AE4 RID: 23268 RVA: 0x0020D85B File Offset: 0x0020BA5B
	private VerticalWindTunnelWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x06005AE5 RID: 23269 RVA: 0x0020D86C File Offset: 0x0020BA6C
	public override Workable.AnimInfo GetAnim(WorkerBase worker)
	{
		Workable.AnimInfo anim = base.GetAnim(worker);
		anim.smi = new WindTunnelWorkerStateMachine.StatesInstance(worker, this);
		return anim;
	}

	// Token: 0x06005AE6 RID: 23270 RVA: 0x0020D890 File Offset: 0x0020BA90
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.synchronizeAnims = false;
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		base.SetWorkTime(90f);
	}

	// Token: 0x06005AE7 RID: 23271 RVA: 0x0020D8B8 File Offset: 0x0020BAB8
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		worker.GetComponent<Effects>().Add("VerticalWindTunnelFlying", false);
	}

	// Token: 0x06005AE8 RID: 23272 RVA: 0x0020D8D3 File Offset: 0x0020BAD3
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		worker.GetComponent<Effects>().Remove("VerticalWindTunnelFlying");
	}

	// Token: 0x06005AE9 RID: 23273 RVA: 0x0020D8EC File Offset: 0x0020BAEC
	protected override void OnCompleteWork(WorkerBase worker)
	{
		Effects component = worker.GetComponent<Effects>();
		component.Add(this.windTunnel.trackingEffect, true);
		component.Add(this.windTunnel.specificEffect, true);
	}

	// Token: 0x06005AEA RID: 23274 RVA: 0x0020D91C File Offset: 0x0020BB1C
	public bool GetWorkerPriority(WorkerBase worker, out int priority)
	{
		priority = this.windTunnel.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (component.HasEffect(this.windTunnel.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (component.HasEffect(this.windTunnel.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x04003C47 RID: 15431
	public VerticalWindTunnel windTunnel;

	// Token: 0x04003C48 RID: 15432
	public HashedString overrideAnim;

	// Token: 0x04003C49 RID: 15433
	public string[] preAnims;

	// Token: 0x04003C4A RID: 15434
	public string loopAnim;

	// Token: 0x04003C4B RID: 15435
	public string[] pstAnims;
}
