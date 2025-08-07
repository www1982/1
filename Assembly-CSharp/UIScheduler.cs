using System;
using UnityEngine;

// Token: 0x02000E74 RID: 3700
[AddComponentMenu("KMonoBehaviour/scripts/UIScheduler")]
public class UIScheduler : KMonoBehaviour, IScheduler
{
	// Token: 0x06007603 RID: 30211 RVA: 0x002D2D55 File Offset: 0x002D0F55
	public static void DestroyInstance()
	{
		UIScheduler.Instance = null;
	}

	// Token: 0x06007604 RID: 30212 RVA: 0x002D2D5D File Offset: 0x002D0F5D
	protected override void OnPrefabInit()
	{
		UIScheduler.Instance = this;
	}

	// Token: 0x06007605 RID: 30213 RVA: 0x002D2D65 File Offset: 0x002D0F65
	public SchedulerHandle Schedule(string name, float time, Action<object> callback, object callback_data = null, SchedulerGroup group = null)
	{
		return this.scheduler.Schedule(name, time, callback, callback_data, group);
	}

	// Token: 0x06007606 RID: 30214 RVA: 0x002D2D79 File Offset: 0x002D0F79
	public SchedulerHandle ScheduleNextFrame(string name, Action<object> callback, object callback_data = null, SchedulerGroup group = null)
	{
		return this.scheduler.Schedule(name, 0f, callback, callback_data, group);
	}

	// Token: 0x06007607 RID: 30215 RVA: 0x002D2D90 File Offset: 0x002D0F90
	private void Update()
	{
		this.scheduler.Update();
	}

	// Token: 0x06007608 RID: 30216 RVA: 0x002D2D9D File Offset: 0x002D0F9D
	protected override void OnLoadLevel()
	{
		this.scheduler.FreeResources();
		this.scheduler = null;
	}

	// Token: 0x06007609 RID: 30217 RVA: 0x002D2DB1 File Offset: 0x002D0FB1
	public SchedulerGroup CreateGroup()
	{
		return new SchedulerGroup(this.scheduler);
	}

	// Token: 0x0600760A RID: 30218 RVA: 0x002D2DBE File Offset: 0x002D0FBE
	public Scheduler GetScheduler()
	{
		return this.scheduler;
	}

	// Token: 0x040051D9 RID: 20953
	private Scheduler scheduler = new Scheduler(new UIScheduler.UISchedulerClock());

	// Token: 0x040051DA RID: 20954
	public static UIScheduler Instance;

	// Token: 0x0200206A RID: 8298
	public class UISchedulerClock : SchedulerClock
	{
		// Token: 0x0600B655 RID: 46677 RVA: 0x003E16D1 File Offset: 0x003DF8D1
		public override float GetTime()
		{
			return Time.unscaledTime;
		}
	}
}
