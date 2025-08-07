using System;

// Token: 0x020005D0 RID: 1488
public class KnockKnock : Activatable
{
	// Token: 0x06002265 RID: 8805 RVA: 0x000C5787 File Offset: 0x000C3987
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.showProgressBar = false;
	}

	// Token: 0x06002266 RID: 8806 RVA: 0x000C5796 File Offset: 0x000C3996
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		if (!this.doorAnswered)
		{
			this.workTimeRemaining += dt;
		}
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x06002267 RID: 8807 RVA: 0x000C57B6 File Offset: 0x000C39B6
	public void AnswerDoor()
	{
		this.doorAnswered = true;
		this.workTimeRemaining = 1f;
	}

	// Token: 0x040013FB RID: 5115
	private bool doorAnswered;
}
