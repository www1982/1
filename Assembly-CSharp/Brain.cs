using System;
using UnityEngine;

// Token: 0x02000464 RID: 1124
[AddComponentMenu("KMonoBehaviour/scripts/Brain")]
public class Brain : KMonoBehaviour
{
	// Token: 0x0600179C RID: 6044 RVA: 0x000831CF File Offset: 0x000813CF
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
	}

	// Token: 0x0600179D RID: 6045 RVA: 0x000831D7 File Offset: 0x000813D7
	protected override void OnSpawn()
	{
		this.prefabId = base.GetComponent<KPrefabID>();
		this.choreConsumer = base.GetComponent<ChoreConsumer>();
		this.running = true;
		Components.Brains.Add(this);
	}

	// Token: 0x14000004 RID: 4
	// (add) Token: 0x0600179E RID: 6046 RVA: 0x00083204 File Offset: 0x00081404
	// (remove) Token: 0x0600179F RID: 6047 RVA: 0x0008323C File Offset: 0x0008143C
	public event global::System.Action onPreUpdate;

	// Token: 0x060017A0 RID: 6048 RVA: 0x00083271 File Offset: 0x00081471
	public virtual void UpdateBrain()
	{
		SuperluminalPerf.BeginEvent("UpdateBrain", base.name);
		if (this.onPreUpdate != null)
		{
			this.onPreUpdate();
		}
		if (this.IsRunning())
		{
			this.UpdateChores();
		}
		SuperluminalPerf.EndEvent();
	}

	// Token: 0x060017A1 RID: 6049 RVA: 0x000832AA File Offset: 0x000814AA
	private bool FindBetterChore(ref Chore.Precondition.Context context)
	{
		return this.choreConsumer.FindNextChore(ref context);
	}

	// Token: 0x060017A2 RID: 6050 RVA: 0x000832B8 File Offset: 0x000814B8
	private void UpdateChores()
	{
		if (this.prefabId.HasTag(GameTags.PreventChoreInterruption))
		{
			return;
		}
		Chore.Precondition.Context context = default(Chore.Precondition.Context);
		if (this.FindBetterChore(ref context))
		{
			if (this.prefabId.HasTag(GameTags.PerformingWorkRequest))
			{
				base.Trigger(1485595942, null);
				return;
			}
			this.choreConsumer.choreDriver.SetChore(context);
		}
	}

	// Token: 0x060017A3 RID: 6051 RVA: 0x0008331A File Offset: 0x0008151A
	public bool IsRunning()
	{
		return this.running && !this.suspend;
	}

	// Token: 0x060017A4 RID: 6052 RVA: 0x0008332F File Offset: 0x0008152F
	public void Reset(string reason)
	{
		this.Stop("Reset");
		this.running = true;
	}

	// Token: 0x060017A5 RID: 6053 RVA: 0x00083343 File Offset: 0x00081543
	public void Stop(string reason)
	{
		base.GetComponent<ChoreDriver>().StopChore();
		this.running = false;
	}

	// Token: 0x060017A6 RID: 6054 RVA: 0x00083357 File Offset: 0x00081557
	public void Resume(string caller)
	{
		this.suspend = false;
	}

	// Token: 0x060017A7 RID: 6055 RVA: 0x00083360 File Offset: 0x00081560
	public void Suspend(string caller)
	{
		this.suspend = true;
	}

	// Token: 0x060017A8 RID: 6056 RVA: 0x00083369 File Offset: 0x00081569
	protected override void OnCmpDisable()
	{
		base.OnCmpDisable();
		this.Stop("OnCmpDisable");
	}

	// Token: 0x060017A9 RID: 6057 RVA: 0x0008337C File Offset: 0x0008157C
	protected override void OnCleanUp()
	{
		this.Stop("OnCleanUp");
		Components.Brains.Remove(this);
	}

	// Token: 0x04000DAB RID: 3499
	private bool running;

	// Token: 0x04000DAC RID: 3500
	private bool suspend;

	// Token: 0x04000DAD RID: 3501
	protected KPrefabID prefabId;

	// Token: 0x04000DAE RID: 3502
	protected ChoreConsumer choreConsumer;
}
