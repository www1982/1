using System;
using UnityEngine;

// Token: 0x020004F3 RID: 1267
public struct SchedulerEntry
{
	// Token: 0x170000B0 RID: 176
	// (get) Token: 0x06001B2A RID: 6954 RVA: 0x0009576C File Offset: 0x0009396C
	// (set) Token: 0x06001B2B RID: 6955 RVA: 0x00095774 File Offset: 0x00093974
	public SchedulerEntry.Details details { readonly get; private set; }

	// Token: 0x06001B2C RID: 6956 RVA: 0x0009577D File Offset: 0x0009397D
	public SchedulerEntry(string name, float time, float time_interval, Action<object> callback, object callback_data, GameObject profiler_obj)
	{
		this.time = time;
		this.details = new SchedulerEntry.Details(name, callback, callback_data, time_interval, profiler_obj);
	}

	// Token: 0x06001B2D RID: 6957 RVA: 0x00095799 File Offset: 0x00093999
	public void FreeResources()
	{
		this.details = null;
	}

	// Token: 0x170000B1 RID: 177
	// (get) Token: 0x06001B2E RID: 6958 RVA: 0x000957A2 File Offset: 0x000939A2
	public Action<object> callback
	{
		get
		{
			return this.details.callback;
		}
	}

	// Token: 0x170000B2 RID: 178
	// (get) Token: 0x06001B2F RID: 6959 RVA: 0x000957AF File Offset: 0x000939AF
	public object callbackData
	{
		get
		{
			return this.details.callbackData;
		}
	}

	// Token: 0x170000B3 RID: 179
	// (get) Token: 0x06001B30 RID: 6960 RVA: 0x000957BC File Offset: 0x000939BC
	public float timeInterval
	{
		get
		{
			return this.details.timeInterval;
		}
	}

	// Token: 0x06001B31 RID: 6961 RVA: 0x000957C9 File Offset: 0x000939C9
	public override string ToString()
	{
		return this.time.ToString();
	}

	// Token: 0x06001B32 RID: 6962 RVA: 0x000957D6 File Offset: 0x000939D6
	public void Clear()
	{
		this.details.callback = null;
	}

	// Token: 0x04001001 RID: 4097
	public float time;

	// Token: 0x02001340 RID: 4928
	public class Details
	{
		// Token: 0x0600892A RID: 35114 RVA: 0x0034B063 File Offset: 0x00349263
		public Details(string name, Action<object> callback, object callback_data, float time_interval, GameObject profiler_obj)
		{
			this.timeInterval = time_interval;
			this.callback = callback;
			this.callbackData = callback_data;
		}

		// Token: 0x040068EF RID: 26863
		public Action<object> callback;

		// Token: 0x040068F0 RID: 26864
		public object callbackData;

		// Token: 0x040068F1 RID: 26865
		public float timeInterval;
	}
}
