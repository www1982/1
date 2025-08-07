using System;
using UnityEngine;

// Token: 0x020004EF RID: 1263
[AddComponentMenu("KMonoBehaviour/scripts/GameScheduler")]
public class GameScheduler : KMonoBehaviour, IScheduler
{
	// Token: 0x06001B15 RID: 6933 RVA: 0x000954DC File Offset: 0x000936DC
	public static void DestroyInstance()
	{
		GameScheduler.Instance = null;
	}

	// Token: 0x06001B16 RID: 6934 RVA: 0x000954E4 File Offset: 0x000936E4
	protected override void OnPrefabInit()
	{
		GameScheduler.Instance = this;
		Singleton<StateMachineManager>.Instance.RegisterScheduler(this.scheduler);
	}

	// Token: 0x06001B17 RID: 6935 RVA: 0x000954FC File Offset: 0x000936FC
	public SchedulerHandle Schedule(string name, float time, Action<object> callback, object callback_data = null, SchedulerGroup group = null)
	{
		return this.scheduler.Schedule(name, time, callback, callback_data, group);
	}

	// Token: 0x06001B18 RID: 6936 RVA: 0x00095510 File Offset: 0x00093710
	public SchedulerHandle ScheduleNextFrame(string name, Action<object> callback, object callback_data = null, SchedulerGroup group = null)
	{
		return this.scheduler.Schedule(name, 0f, callback, callback_data, group);
	}

	// Token: 0x06001B19 RID: 6937 RVA: 0x00095527 File Offset: 0x00093727
	private void Update()
	{
		this.scheduler.Update();
	}

	// Token: 0x06001B1A RID: 6938 RVA: 0x00095534 File Offset: 0x00093734
	protected override void OnLoadLevel()
	{
		this.scheduler.FreeResources();
		this.scheduler = null;
	}

	// Token: 0x06001B1B RID: 6939 RVA: 0x00095548 File Offset: 0x00093748
	public SchedulerGroup CreateGroup()
	{
		return new SchedulerGroup(this.scheduler);
	}

	// Token: 0x06001B1C RID: 6940 RVA: 0x00095555 File Offset: 0x00093755
	public Scheduler GetScheduler()
	{
		return this.scheduler;
	}

	// Token: 0x04000FFC RID: 4092
	private Scheduler scheduler = new Scheduler(new GameScheduler.GameSchedulerClock());

	// Token: 0x04000FFD RID: 4093
	public static GameScheduler Instance;

	// Token: 0x0200133F RID: 4927
	public class GameSchedulerClock : SchedulerClock
	{
		// Token: 0x06008928 RID: 35112 RVA: 0x0034B04F File Offset: 0x0034924F
		public override float GetTime()
		{
			return GameClock.Instance.GetTime();
		}
	}
}
