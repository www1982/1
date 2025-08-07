using System;
using System.Collections.Generic;

// Token: 0x020004F4 RID: 1268
public class SchedulerGroup
{
	// Token: 0x170000B4 RID: 180
	// (get) Token: 0x06001B33 RID: 6963 RVA: 0x000957E4 File Offset: 0x000939E4
	// (set) Token: 0x06001B34 RID: 6964 RVA: 0x000957EC File Offset: 0x000939EC
	public Scheduler scheduler { get; private set; }

	// Token: 0x06001B35 RID: 6965 RVA: 0x000957F5 File Offset: 0x000939F5
	public SchedulerGroup(Scheduler scheduler)
	{
		this.scheduler = scheduler;
		this.Reset();
	}

	// Token: 0x06001B36 RID: 6966 RVA: 0x00095815 File Offset: 0x00093A15
	public void FreeResources()
	{
		if (this.scheduler != null)
		{
			this.scheduler.FreeResources();
		}
		this.scheduler = null;
		if (this.handles != null)
		{
			this.handles.Clear();
		}
		this.handles = null;
	}

	// Token: 0x06001B37 RID: 6967 RVA: 0x0009584C File Offset: 0x00093A4C
	public void Reset()
	{
		foreach (SchedulerHandle schedulerHandle in this.handles)
		{
			schedulerHandle.ClearScheduler();
		}
		this.handles.Clear();
	}

	// Token: 0x06001B38 RID: 6968 RVA: 0x000958AC File Offset: 0x00093AAC
	public void Add(SchedulerHandle handle)
	{
		this.handles.Add(handle);
	}

	// Token: 0x04001004 RID: 4100
	private List<SchedulerHandle> handles = new List<SchedulerHandle>();
}
