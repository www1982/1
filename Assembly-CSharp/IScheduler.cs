using System;

// Token: 0x020004F0 RID: 1264
public interface IScheduler
{
	// Token: 0x06001B1E RID: 6942
	SchedulerHandle Schedule(string name, float time, Action<object> callback, object callback_data = null, SchedulerGroup group = null);
}
