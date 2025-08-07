using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x020006B7 RID: 1719
[AddComponentMenu("KMonoBehaviour/Workable/BeachChairWorkable")]
public class BeachChairWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x06002A35 RID: 10805 RVA: 0x000F4968 File Offset: 0x000F2B68
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetReportType(ReportManager.ReportType.PersonalTime);
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_beach_chair_kanim") };
		this.workAnims = null;
		this.workingPstComplete = null;
		this.workingPstFailed = null;
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		this.synchronizeAnims = false;
		this.lightEfficiencyBonus = false;
		base.SetWorkTime(150f);
		this.beachChair = base.GetComponent<BeachChair>();
	}

	// Token: 0x06002A36 RID: 10806 RVA: 0x000F49E9 File Offset: 0x000F2BE9
	protected override void OnStartWork(WorkerBase worker)
	{
		this.timeLit = 0f;
		this.beachChair.SetWorker(worker);
		this.operational.SetActive(true, false);
		worker.GetComponent<Effects>().Add("BeachChairRelaxing", false);
	}

	// Token: 0x06002A37 RID: 10807 RVA: 0x000F4A24 File Offset: 0x000F2C24
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		int num = Grid.PosToCell(base.gameObject);
		bool flag = (float)Grid.LightIntensity[num] >= (float)BeachChairConfig.TAN_LUX - 1f;
		this.beachChair.SetLit(flag);
		if (flag)
		{
			base.GetComponent<LoopingSounds>().SetParameter(this.soundPath, this.BEACH_CHAIR_LIT_PARAMETER, 1f);
			this.timeLit += dt;
		}
		else
		{
			base.GetComponent<LoopingSounds>().SetParameter(this.soundPath, this.BEACH_CHAIR_LIT_PARAMETER, 0f);
		}
		return false;
	}

	// Token: 0x06002A38 RID: 10808 RVA: 0x000F4AB4 File Offset: 0x000F2CB4
	protected override void OnCompleteWork(WorkerBase worker)
	{
		Effects component = worker.GetComponent<Effects>();
		if (this.timeLit / this.workTime >= 0.75f)
		{
			component.Add(this.beachChair.specificEffectLit, true);
			component.Remove(this.beachChair.specificEffectUnlit);
		}
		else
		{
			component.Add(this.beachChair.specificEffectUnlit, true);
			component.Remove(this.beachChair.specificEffectLit);
		}
		component.Add(this.beachChair.trackingEffect, true);
	}

	// Token: 0x06002A39 RID: 10809 RVA: 0x000F4B39 File Offset: 0x000F2D39
	protected override void OnStopWork(WorkerBase worker)
	{
		this.operational.SetActive(false, false);
		worker.GetComponent<Effects>().Remove("BeachChairRelaxing");
	}

	// Token: 0x06002A3A RID: 10810 RVA: 0x000F4B58 File Offset: 0x000F2D58
	public bool GetWorkerPriority(WorkerBase worker, out int priority)
	{
		priority = this.basePriority;
		Effects component = worker.GetComponent<Effects>();
		if (component.HasEffect(this.beachChair.trackingEffect))
		{
			priority = 0;
			return false;
		}
		if (component.HasEffect(this.beachChair.specificEffectLit) || component.HasEffect(this.beachChair.specificEffectUnlit))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x040018FF RID: 6399
	[MyCmpReq]
	private Operational operational;

	// Token: 0x04001900 RID: 6400
	private float timeLit;

	// Token: 0x04001901 RID: 6401
	public string soundPath = GlobalAssets.GetSound("BeachChair_music_lp", false);

	// Token: 0x04001902 RID: 6402
	public HashedString BEACH_CHAIR_LIT_PARAMETER = "beachChair_lit";

	// Token: 0x04001903 RID: 6403
	public int basePriority;

	// Token: 0x04001904 RID: 6404
	private BeachChair beachChair;
}
