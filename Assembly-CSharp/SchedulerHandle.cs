using System;

// Token: 0x020004F5 RID: 1269
public struct SchedulerHandle
{
	// Token: 0x06001B39 RID: 6969 RVA: 0x000958BA File Offset: 0x00093ABA
	public SchedulerHandle(Scheduler scheduler, SchedulerEntry entry)
	{
		this.entry = entry;
		this.scheduler = scheduler;
	}

	// Token: 0x170000B5 RID: 181
	// (get) Token: 0x06001B3A RID: 6970 RVA: 0x000958CA File Offset: 0x00093ACA
	public float TimeRemaining
	{
		get
		{
			if (!this.IsValid)
			{
				return -1f;
			}
			return this.entry.time - this.scheduler.GetTime();
		}
	}

	// Token: 0x06001B3B RID: 6971 RVA: 0x000958F1 File Offset: 0x00093AF1
	public void FreeResources()
	{
		this.entry.FreeResources();
		this.scheduler = null;
	}

	// Token: 0x06001B3C RID: 6972 RVA: 0x00095905 File Offset: 0x00093B05
	public void ClearScheduler()
	{
		if (this.scheduler == null)
		{
			return;
		}
		this.scheduler.Clear(this);
		this.scheduler = null;
	}

	// Token: 0x170000B6 RID: 182
	// (get) Token: 0x06001B3D RID: 6973 RVA: 0x00095928 File Offset: 0x00093B28
	public bool IsValid
	{
		get
		{
			return this.scheduler != null;
		}
	}

	// Token: 0x04001005 RID: 4101
	public SchedulerEntry entry;

	// Token: 0x04001006 RID: 4102
	private Scheduler scheduler;
}
