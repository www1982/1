using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x02000B15 RID: 2837
[AddComponentMenu("KMonoBehaviour/Workable/Sleepable")]
public class Sleepable : Workable
{
	// Token: 0x06005397 RID: 21399 RVA: 0x001E6EEC File Offset: 0x001E50EC
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		base.SetReportType(ReportManager.ReportType.PersonalTime);
		this.showProgressBar = false;
		this.workerStatusItem = null;
		this.synchronizeAnims = false;
		this.triggerWorkReactions = false;
		this.lightEfficiencyBonus = false;
		this.approachable = base.GetComponent<IApproachable>();
	}

	// Token: 0x06005398 RID: 21400 RVA: 0x001E6F2B File Offset: 0x001E512B
	protected override void OnSpawn()
	{
		if (this.isNormalBed)
		{
			Components.NormalBeds.Add(base.gameObject.GetMyWorldId(), this);
		}
		base.SetWorkTime(float.PositiveInfinity);
	}

	// Token: 0x06005399 RID: 21401 RVA: 0x001E6F58 File Offset: 0x001E5158
	public override HashedString[] GetWorkAnims(WorkerBase worker)
	{
		MinionResume component = worker.GetComponent<MinionResume>();
		if (base.GetComponent<Building>() != null && component != null && component.CurrentHat != null)
		{
			return Sleepable.hatWorkAnims;
		}
		return Sleepable.normalWorkAnims;
	}

	// Token: 0x0600539A RID: 21402 RVA: 0x001E6F98 File Offset: 0x001E5198
	public override HashedString[] GetWorkPstAnims(WorkerBase worker, bool successfully_completed)
	{
		MinionResume component = worker.GetComponent<MinionResume>();
		if (base.GetComponent<Building>() != null && component != null && component.CurrentHat != null)
		{
			return Sleepable.hatWorkPstAnim;
		}
		return Sleepable.normalWorkPstAnim;
	}

	// Token: 0x0600539B RID: 21403 RVA: 0x001E6FD8 File Offset: 0x001E51D8
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		KAnimControllerBase animController = this.GetAnimController();
		if (animController != null)
		{
			animController.Play("working_pre", KAnim.PlayMode.Once, 1f, 0f);
			animController.Queue("working_loop", KAnim.PlayMode.Loop, 1f, 0f);
		}
		base.Subscribe(worker.gameObject, -1142962013, new Action<object>(this.PlayPstAnim));
		if (this.operational != null)
		{
			this.operational.SetActive(true, false);
		}
		worker.Trigger(-1283701846, this);
		worker.GetComponent<Effects>().Add(this.effectName, false);
		this.isDoneSleeping = false;
	}

	// Token: 0x0600539C RID: 21404 RVA: 0x001E7094 File Offset: 0x001E5294
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		if (this.isDoneSleeping)
		{
			return Time.time > this.wakeTime;
		}
		if (this.Dreamable != null && !this.Dreamable.DreamIsDisturbed)
		{
			this.Dreamable.WorkTick(worker, dt);
		}
		if (worker.GetSMI<StaminaMonitor.Instance>().ShouldExitSleep())
		{
			this.isDoneSleeping = true;
			this.wakeTime = Time.time + global::UnityEngine.Random.value * 3f;
		}
		return false;
	}

	// Token: 0x0600539D RID: 21405 RVA: 0x001E710C File Offset: 0x001E530C
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		if (this.operational != null)
		{
			this.operational.SetActive(false, false);
		}
		base.Unsubscribe(worker.gameObject, -1142962013, new Action<object>(this.PlayPstAnim));
		if (worker != null)
		{
			Effects component = worker.GetComponent<Effects>();
			component.Remove(this.effectName);
			if (this.wakeEffects != null)
			{
				foreach (string text in this.wakeEffects)
				{
					component.Add(text, true);
				}
			}
			if (this.stretchOnWake && global::UnityEngine.Random.value < 0.33f)
			{
				new EmoteChore(worker.GetComponent<ChoreProvider>(), Db.Get().ChoreTypes.EmoteHighPriority, Db.Get().Emotes.Minion.MorningStretch, 1, null);
			}
			if (worker.GetAmounts().Get(Db.Get().Amounts.Stamina).value < worker.GetAmounts().Get(Db.Get().Amounts.Stamina).GetMax())
			{
				worker.Trigger(1338475637, this);
			}
		}
	}

	// Token: 0x0600539E RID: 21406 RVA: 0x001E7258 File Offset: 0x001E5458
	public override bool InstantlyFinish(WorkerBase worker)
	{
		return false;
	}

	// Token: 0x0600539F RID: 21407 RVA: 0x001E725B File Offset: 0x001E545B
	protected override void OnCleanUp()
	{
		base.OnCleanUp();
		if (this.isNormalBed)
		{
			Components.NormalBeds.Remove(base.gameObject.GetMyWorldId(), this);
		}
	}

	// Token: 0x060053A0 RID: 21408 RVA: 0x001E7284 File Offset: 0x001E5484
	private void PlayPstAnim(object data)
	{
		WorkerBase workerBase = (WorkerBase)data;
		if (workerBase != null && workerBase.GetWorkable() != null)
		{
			KAnimControllerBase component = workerBase.GetWorkable().gameObject.GetComponent<KAnimControllerBase>();
			if (component != null)
			{
				component.Play("working_pst", KAnim.PlayMode.Once, 1f, 0f);
			}
		}
	}

	// Token: 0x0400382A RID: 14378
	private const float STRECH_CHANCE = 0.33f;

	// Token: 0x0400382B RID: 14379
	[MyCmpGet]
	public Assignable assignable;

	// Token: 0x0400382C RID: 14380
	public IApproachable approachable;

	// Token: 0x0400382D RID: 14381
	[MyCmpGet]
	private Operational operational;

	// Token: 0x0400382E RID: 14382
	public string effectName = "Sleep";

	// Token: 0x0400382F RID: 14383
	public List<string> wakeEffects;

	// Token: 0x04003830 RID: 14384
	public bool stretchOnWake = true;

	// Token: 0x04003831 RID: 14385
	private float wakeTime;

	// Token: 0x04003832 RID: 14386
	private bool isDoneSleeping;

	// Token: 0x04003833 RID: 14387
	public bool isNormalBed = true;

	// Token: 0x04003834 RID: 14388
	public ClinicDreamable Dreamable;

	// Token: 0x04003835 RID: 14389
	private static readonly HashedString[] normalWorkAnims = new HashedString[] { "working_pre", "working_loop" };

	// Token: 0x04003836 RID: 14390
	private static readonly HashedString[] hatWorkAnims = new HashedString[] { "hat_pre", "working_loop" };

	// Token: 0x04003837 RID: 14391
	private static readonly HashedString[] normalWorkPstAnim = new HashedString[] { "working_pst" };

	// Token: 0x04003838 RID: 14392
	private static readonly HashedString[] hatWorkPstAnim = new HashedString[] { "hat_pst" };
}
