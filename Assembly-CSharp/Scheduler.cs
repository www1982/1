using System;
using UnityEngine;

// Token: 0x020004F1 RID: 1265
public class Scheduler : IScheduler
{
	// Token: 0x170000AF RID: 175
	// (get) Token: 0x06001B1F RID: 6943 RVA: 0x00095575 File Offset: 0x00093775
	public int Count
	{
		get
		{
			return this.entries.Count;
		}
	}

	// Token: 0x06001B20 RID: 6944 RVA: 0x00095582 File Offset: 0x00093782
	public Scheduler(SchedulerClock clock)
	{
		this.clock = clock;
	}

	// Token: 0x06001B21 RID: 6945 RVA: 0x000955A7 File Offset: 0x000937A7
	public float GetTime()
	{
		return this.clock.GetTime();
	}

	// Token: 0x06001B22 RID: 6946 RVA: 0x000955B4 File Offset: 0x000937B4
	private SchedulerHandle Schedule(SchedulerEntry entry)
	{
		this.entries.Enqueue(entry.time, entry);
		return new SchedulerHandle(this, entry);
	}

	// Token: 0x06001B23 RID: 6947 RVA: 0x000955D0 File Offset: 0x000937D0
	private SchedulerHandle Schedule(string name, float time, float time_interval, Action<object> callback, object callback_data, GameObject profiler_obj)
	{
		SchedulerEntry schedulerEntry = new SchedulerEntry(name, time + this.clock.GetTime(), time_interval, callback, callback_data, profiler_obj);
		return this.Schedule(schedulerEntry);
	}

	// Token: 0x06001B24 RID: 6948 RVA: 0x00095600 File Offset: 0x00093800
	public void FreeResources()
	{
		this.clock = null;
		if (this.entries != null)
		{
			while (this.entries.Count > 0)
			{
				this.entries.Dequeue().Value.FreeResources();
			}
		}
		this.entries = null;
	}

	// Token: 0x06001B25 RID: 6949 RVA: 0x00095650 File Offset: 0x00093850
	public SchedulerHandle Schedule(string name, float time, Action<object> callback, object callback_data = null, SchedulerGroup group = null)
	{
		if (group != null && group.scheduler != this)
		{
			global::Debug.LogError("Scheduler group mismatch!");
		}
		SchedulerHandle schedulerHandle = this.Schedule(name, time, -1f, callback, callback_data, null);
		if (group != null)
		{
			group.Add(schedulerHandle);
		}
		return schedulerHandle;
	}

	// Token: 0x06001B26 RID: 6950 RVA: 0x00095694 File Offset: 0x00093894
	public void Clear(SchedulerHandle handle)
	{
		handle.entry.Clear();
	}

	// Token: 0x06001B27 RID: 6951 RVA: 0x000956A4 File Offset: 0x000938A4
	public void Update()
	{
		if (this.Count == 0)
		{
			return;
		}
		int count = this.Count;
		int num = 0;
		using (new KProfiler.Region("Scheduler.Update", null))
		{
			float time = this.clock.GetTime();
			if (this.previousTime != time)
			{
				this.previousTime = time;
				while (num < count && time >= this.entries.Peek().Key)
				{
					SchedulerEntry value = this.entries.Dequeue().Value;
					if (value.callback != null)
					{
						value.callback(value.callbackData);
					}
					num++;
				}
			}
		}
	}

	// Token: 0x04000FFE RID: 4094
	public FloatHOTQueue<SchedulerEntry> entries = new FloatHOTQueue<SchedulerEntry>();

	// Token: 0x04000FFF RID: 4095
	private SchedulerClock clock;

	// Token: 0x04001000 RID: 4096
	private float previousTime = float.NegativeInfinity;
}
