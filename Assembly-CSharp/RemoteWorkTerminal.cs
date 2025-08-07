using System;
using KSerialization;
using UnityEngine;

// Token: 0x02000A90 RID: 2704
[AddComponentMenu("KMonoBehaviour/Workable/RemoteWorkTerminal")]
public class RemoteWorkTerminal : Workable
{
	// Token: 0x06004E85 RID: 20101 RVA: 0x001C6F08 File Offset: 0x001C5108
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_remote_terminal_kanim") };
		this.InitializeWorkingInteracts();
		this.synchronizeAnims = true;
		this.showProgressBar = false;
		this.workLayer = Grid.SceneLayer.BuildingUse;
		this.surpressWorkerForceSync = true;
		this.kbac.onAnimComplete += this.PlayNextWorkingAnim;
	}

	// Token: 0x06004E86 RID: 20102 RVA: 0x001C6F74 File Offset: 0x001C5174
	private void InitializeWorkingInteracts()
	{
		if (RemoteWorkTerminal.NUM_WORKING_INTERACTS != -1)
		{
			return;
		}
		KAnimFileData data = this.overrideAnims[0].GetData();
		RemoteWorkTerminal.NUM_WORKING_INTERACTS = 0;
		for (;;)
		{
			string text = string.Format("working_loop_{0}", RemoteWorkTerminal.NUM_WORKING_INTERACTS + 1);
			if (data.GetAnim(text) == null)
			{
				break;
			}
			RemoteWorkTerminal.NUM_WORKING_INTERACTS++;
		}
	}

	// Token: 0x06004E87 RID: 20103 RVA: 0x001C6FCC File Offset: 0x001C51CC
	public override HashedString[] GetWorkAnims(WorkerBase worker)
	{
		MinionResume component = worker.GetComponent<MinionResume>();
		if (base.GetComponent<Building>() != null && component != null && component.CurrentHat != null)
		{
			return RemoteWorkTerminal.hatWorkAnims;
		}
		return RemoteWorkTerminal.normalWorkAnims;
	}

	// Token: 0x06004E88 RID: 20104 RVA: 0x001C700C File Offset: 0x001C520C
	public override HashedString[] GetWorkPstAnims(WorkerBase worker, bool successfully_completed)
	{
		MinionResume component = worker.GetComponent<MinionResume>();
		if (base.GetComponent<Building>() != null && component != null && component.CurrentHat != null)
		{
			return RemoteWorkTerminal.hatWorkPstAnim;
		}
		return RemoteWorkTerminal.normalWorkPstAnim;
	}

	// Token: 0x1700055E RID: 1374
	// (get) Token: 0x06004E89 RID: 20105 RVA: 0x001C704A File Offset: 0x001C524A
	// (set) Token: 0x06004E8A RID: 20106 RVA: 0x001C705D File Offset: 0x001C525D
	public RemoteWorkerDock CurrentDock
	{
		get
		{
			Ref<RemoteWorkerDock> @ref = this.dock;
			if (@ref == null)
			{
				return null;
			}
			return @ref.Get();
		}
		set
		{
			Ref<RemoteWorkerDock> @ref = this.dock;
			if (((@ref != null) ? @ref.Get() : null) != null)
			{
				this.dock.Get().StopWorking(this);
			}
			this.dock = new Ref<RemoteWorkerDock>(value);
		}
	}

	// Token: 0x1700055F RID: 1375
	// (get) Token: 0x06004E8B RID: 20107 RVA: 0x001C7096 File Offset: 0x001C5296
	// (set) Token: 0x06004E8C RID: 20108 RVA: 0x001C70A8 File Offset: 0x001C52A8
	public RemoteWorkerDock FutureDock
	{
		get
		{
			return this.future_dock ?? this.CurrentDock;
		}
		set
		{
			this.CurrentDock = value;
		}
	}

	// Token: 0x06004E8D RID: 20109 RVA: 0x001C70B1 File Offset: 0x001C52B1
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		this.kbac.Queue(this.GetWorkingLoop(), KAnim.PlayMode.Once, 1f, 0f);
		RemoteWorkerDock currentDock = this.CurrentDock;
		if (currentDock == null)
		{
			return;
		}
		currentDock.StartWorking(this);
	}

	// Token: 0x06004E8E RID: 20110 RVA: 0x001C70E8 File Offset: 0x001C52E8
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		RemoteWorkerDock currentDock = this.CurrentDock;
		if (currentDock == null)
		{
			return;
		}
		currentDock.StopWorking(this);
	}

	// Token: 0x06004E8F RID: 20111 RVA: 0x001C7102 File Offset: 0x001C5302
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		return this.CurrentDock == null || this.CurrentDock.OnRemoteWorkTick(dt);
	}

	// Token: 0x06004E90 RID: 20112 RVA: 0x001C7120 File Offset: 0x001C5320
	private HashedString GetWorkingLoop()
	{
		return string.Format("working_loop_{0}", global::UnityEngine.Random.Range(1, RemoteWorkTerminal.NUM_WORKING_INTERACTS + 1));
	}

	// Token: 0x06004E91 RID: 20113 RVA: 0x001C7143 File Offset: 0x001C5343
	private void PlayNextWorkingAnim(HashedString anim)
	{
		if (base.worker == null)
		{
			return;
		}
		if (base.worker.GetState() == WorkerBase.State.Working)
		{
			this.kbac.Play(this.GetWorkingLoop(), KAnim.PlayMode.Once, 1f, 0f);
		}
	}

	// Token: 0x04003426 RID: 13350
	[Serialize]
	private Ref<RemoteWorkerDock> dock;

	// Token: 0x04003427 RID: 13351
	private static int NUM_WORKING_INTERACTS = -1;

	// Token: 0x04003428 RID: 13352
	[MyCmpReq]
	private KBatchedAnimController kbac;

	// Token: 0x04003429 RID: 13353
	private static readonly HashedString[] normalWorkAnims = new HashedString[] { "working_pre" };

	// Token: 0x0400342A RID: 13354
	private static readonly HashedString[] hatWorkAnims = new HashedString[] { "hat_pre" };

	// Token: 0x0400342B RID: 13355
	private static readonly HashedString[] normalWorkPstAnim = new HashedString[] { "working_pst" };

	// Token: 0x0400342C RID: 13356
	private static readonly HashedString[] hatWorkPstAnim = new HashedString[] { "working_hat_pst" };

	// Token: 0x0400342D RID: 13357
	public RemoteWorkerDock future_dock;
}
