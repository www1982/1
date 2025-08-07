using System;
using Klei.AI;
using TUNING;
using UnityEngine;

// Token: 0x02000A53 RID: 2643
public class PartyPointWorkable : Workable, IWorkerPrioritizable
{
	// Token: 0x06004CA3 RID: 19619 RVA: 0x001BCB52 File Offset: 0x001BAD52
	private PartyPointWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x06004CA4 RID: 19620 RVA: 0x001BCB64 File Offset: 0x001BAD64
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_generic_convo_kanim") };
		this.workAnimPlayMode = KAnim.PlayMode.Loop;
		this.faceTargetWhenWorking = true;
		this.workerStatusItem = Db.Get().DuplicantStatusItems.Socializing;
		this.synchronizeAnims = false;
		this.showProgressBar = false;
		this.resetProgressOnStop = true;
		this.lightEfficiencyBonus = false;
		if (global::UnityEngine.Random.Range(0f, 100f) > 80f)
		{
			this.activity = PartyPointWorkable.ActivityType.Dance;
		}
		else
		{
			this.activity = PartyPointWorkable.ActivityType.Talk;
		}
		PartyPointWorkable.ActivityType activityType = this.activity;
		if (activityType == PartyPointWorkable.ActivityType.Talk)
		{
			this.workAnims = new HashedString[] { "idle" };
			this.workerOverrideAnims = new KAnimFile[][] { new KAnimFile[] { Assets.GetAnim("anim_generic_convo_kanim") } };
			return;
		}
		if (activityType != PartyPointWorkable.ActivityType.Dance)
		{
			return;
		}
		this.workAnims = new HashedString[] { "working_loop" };
		this.workerOverrideAnims = new KAnimFile[][]
		{
			new KAnimFile[] { Assets.GetAnim("anim_interacts_phonobox_danceone_kanim") },
			new KAnimFile[] { Assets.GetAnim("anim_interacts_phonobox_dancetwo_kanim") },
			new KAnimFile[] { Assets.GetAnim("anim_interacts_phonobox_dancethree_kanim") }
		};
	}

	// Token: 0x06004CA5 RID: 19621 RVA: 0x001BCCC8 File Offset: 0x001BAEC8
	public override Workable.AnimInfo GetAnim(WorkerBase worker)
	{
		int num = global::UnityEngine.Random.Range(0, this.workerOverrideAnims.Length);
		this.overrideAnims = this.workerOverrideAnims[num];
		return base.GetAnim(worker);
	}

	// Token: 0x06004CA6 RID: 19622 RVA: 0x001BCCF9 File Offset: 0x001BAEF9
	public override Vector3 GetFacingTarget()
	{
		if (this.lastTalker != null)
		{
			return this.lastTalker.transform.GetPosition();
		}
		return base.GetFacingTarget();
	}

	// Token: 0x06004CA7 RID: 19623 RVA: 0x001BCD20 File Offset: 0x001BAF20
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		return false;
	}

	// Token: 0x06004CA8 RID: 19624 RVA: 0x001BCD24 File Offset: 0x001BAF24
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		worker.GetComponent<KPrefabID>().AddTag(GameTags.AlwaysConverse, false);
		worker.Subscribe(-594200555, new Action<object>(this.OnStartedTalking));
		worker.Subscribe(25860745, new Action<object>(this.OnStoppedTalking));
	}

	// Token: 0x06004CA9 RID: 19625 RVA: 0x001BCD7C File Offset: 0x001BAF7C
	protected override void OnStopWork(WorkerBase worker)
	{
		base.OnStopWork(worker);
		worker.GetComponent<KPrefabID>().RemoveTag(GameTags.AlwaysConverse);
		worker.Unsubscribe(-594200555, new Action<object>(this.OnStartedTalking));
		worker.Unsubscribe(25860745, new Action<object>(this.OnStoppedTalking));
	}

	// Token: 0x06004CAA RID: 19626 RVA: 0x001BCDD0 File Offset: 0x001BAFD0
	protected override void OnCompleteWork(WorkerBase worker)
	{
		Effects component = worker.GetComponent<Effects>();
		if (!string.IsNullOrEmpty(this.specificEffect))
		{
			component.Add(this.specificEffect, true);
		}
	}

	// Token: 0x06004CAB RID: 19627 RVA: 0x001BCE00 File Offset: 0x001BB000
	private void OnStartedTalking(object data)
	{
		ConversationManager.StartedTalkingEvent startedTalkingEvent = data as ConversationManager.StartedTalkingEvent;
		if (startedTalkingEvent == null)
		{
			return;
		}
		GameObject talker = startedTalkingEvent.talker;
		if (talker == base.worker.gameObject)
		{
			if (this.activity == PartyPointWorkable.ActivityType.Talk)
			{
				KBatchedAnimController component = base.worker.GetComponent<KBatchedAnimController>();
				string text = startedTalkingEvent.anim;
				text += global::UnityEngine.Random.Range(1, 9).ToString();
				component.Play(text, KAnim.PlayMode.Once, 1f, 0f);
				component.Queue("idle", KAnim.PlayMode.Loop, 1f, 0f);
				return;
			}
		}
		else
		{
			if (this.activity == PartyPointWorkable.ActivityType.Talk)
			{
				base.worker.GetComponent<Facing>().Face(talker.transform.GetPosition());
			}
			this.lastTalker = talker;
		}
	}

	// Token: 0x06004CAC RID: 19628 RVA: 0x001BCEC2 File Offset: 0x001BB0C2
	private void OnStoppedTalking(object data)
	{
	}

	// Token: 0x06004CAD RID: 19629 RVA: 0x001BCEC4 File Offset: 0x001BB0C4
	public bool GetWorkerPriority(WorkerBase worker, out int priority)
	{
		priority = this.basePriority;
		if (!string.IsNullOrEmpty(this.specificEffect) && worker.GetComponent<Effects>().HasEffect(this.specificEffect))
		{
			priority = RELAXATION.PRIORITY.RECENTLY_USED;
		}
		return true;
	}

	// Token: 0x040032D7 RID: 13015
	private GameObject lastTalker;

	// Token: 0x040032D8 RID: 13016
	public int basePriority;

	// Token: 0x040032D9 RID: 13017
	public string specificEffect;

	// Token: 0x040032DA RID: 13018
	public KAnimFile[][] workerOverrideAnims;

	// Token: 0x040032DB RID: 13019
	private PartyPointWorkable.ActivityType activity;

	// Token: 0x02001B0E RID: 6926
	private enum ActivityType
	{
		// Token: 0x0400818F RID: 33167
		Talk,
		// Token: 0x04008190 RID: 33168
		Dance,
		// Token: 0x04008191 RID: 33169
		LENGTH
	}
}
