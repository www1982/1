using System;
using UnityEngine;

// Token: 0x02000756 RID: 1878
[AddComponentMenu("KMonoBehaviour/Workable/LiquidCooledFanWorkable")]
public class LiquidCooledFanWorkable : Workable
{
	// Token: 0x06002FCE RID: 12238 RVA: 0x00111AD7 File Offset: 0x0010FCD7
	private LiquidCooledFanWorkable()
	{
		this.showProgressBar = false;
	}

	// Token: 0x06002FCF RID: 12239 RVA: 0x00111AE6 File Offset: 0x0010FCE6
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.workerStatusItem = null;
	}

	// Token: 0x06002FD0 RID: 12240 RVA: 0x00111AF5 File Offset: 0x0010FCF5
	protected override void OnSpawn()
	{
		GameScheduler.Instance.Schedule("InsulationTutorial", 2f, delegate(object obj)
		{
			Tutorial.Instance.TutorialMessage(Tutorial.TutorialMessages.TM_Insulation, true);
		}, null, null);
		base.OnSpawn();
	}

	// Token: 0x06002FD1 RID: 12241 RVA: 0x00111B33 File Offset: 0x0010FD33
	protected override void OnStartWork(WorkerBase worker)
	{
		this.operational.SetActive(true, false);
	}

	// Token: 0x06002FD2 RID: 12242 RVA: 0x00111B42 File Offset: 0x0010FD42
	protected override void OnStopWork(WorkerBase worker)
	{
		this.operational.SetActive(false, false);
	}

	// Token: 0x06002FD3 RID: 12243 RVA: 0x00111B51 File Offset: 0x0010FD51
	protected override void OnCompleteWork(WorkerBase worker)
	{
		this.operational.SetActive(false, false);
	}

	// Token: 0x04001C7C RID: 7292
	[MyCmpGet]
	private Operational operational;
}
