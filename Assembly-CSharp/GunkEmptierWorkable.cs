using System;
using System.Collections.Generic;
using Klei.AI;
using UnityEngine;

// Token: 0x0200023F RID: 575
public class GunkEmptierWorkable : Workable
{
	// Token: 0x06000B99 RID: 2969 RVA: 0x000465A3 File Offset: 0x000447A3
	private GunkEmptierWorkable()
	{
		base.SetReportType(ReportManager.ReportType.PersonalTime);
	}

	// Token: 0x06000B9A RID: 2970 RVA: 0x000465B4 File Offset: 0x000447B4
	protected override void OnPrefabInit()
	{
		base.OnPrefabInit();
		this.showProgressBar = true;
		this.resetProgressOnStop = true;
		this.overrideAnims = new KAnimFile[] { Assets.GetAnim("anim_interacts_gunkdump_kanim") };
		this.attributeConverter = Db.Get().AttributeConverters.ToiletSpeed;
		this.storage = base.GetComponent<Storage>();
		base.SetWorkTime(8.5f);
	}

	// Token: 0x06000B9B RID: 2971 RVA: 0x00046620 File Offset: 0x00044820
	protected override bool OnWorkTick(WorkerBase worker, float dt)
	{
		float num = Mathf.Min(new float[]
		{
			dt / this.workTime * GunkMonitor.GUNK_CAPACITY,
			this.gunkMonitor.CurrentGunkMass,
			this.storage.RemainingCapacity()
		});
		this.gunkMonitor.ExpellGunk(num, this.storage);
		return base.OnWorkTick(worker, dt);
	}

	// Token: 0x06000B9C RID: 2972 RVA: 0x00046680 File Offset: 0x00044880
	protected override void OnStartWork(WorkerBase worker)
	{
		base.OnStartWork(worker);
		this.gunkMonitor = worker.GetSMI<GunkMonitor.Instance>();
		if (Sim.IsRadiationEnabled() && worker.GetAmounts().Get(Db.Get().Amounts.RadiationBalance).value > 0f)
		{
			worker.gameObject.GetComponent<KSelectable>().AddStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, null);
		}
		this.TriggerRoomEffects();
	}

	// Token: 0x06000B9D RID: 2973 RVA: 0x000466F4 File Offset: 0x000448F4
	private void TriggerRoomEffects()
	{
		Room roomOfGameObject = Game.Instance.roomProber.GetRoomOfGameObject(base.gameObject);
		if (roomOfGameObject != null)
		{
			RoomType roomType = roomOfGameObject.roomType;
			List<EffectInstance> list = null;
			roomType.TriggerRoomEffects(base.GetComponent<KPrefabID>(), base.worker.GetComponent<Effects>(), out list);
			if (list != null)
			{
				foreach (EffectInstance effectInstance in list)
				{
					effectInstance.timeRemaining = 1800f;
				}
			}
		}
	}

	// Token: 0x06000B9E RID: 2974 RVA: 0x00046780 File Offset: 0x00044980
	protected override void OnCompleteWork(WorkerBase worker)
	{
		if (this.gunkMonitor != null)
		{
			this.gunkMonitor.ExpellAllGunk(this.storage);
		}
		this.gunkMonitor = null;
		base.OnCompleteWork(worker);
	}

	// Token: 0x06000B9F RID: 2975 RVA: 0x000467A9 File Offset: 0x000449A9
	protected override void OnStopWork(WorkerBase worker)
	{
		this.RemoveExpellingRadStatusItem();
		base.OnStopWork(worker);
	}

	// Token: 0x06000BA0 RID: 2976 RVA: 0x000467B8 File Offset: 0x000449B8
	protected override void OnAbortWork(WorkerBase worker)
	{
		this.RemoveExpellingRadStatusItem();
		base.OnAbortWork(worker);
		this.gunkMonitor = null;
	}

	// Token: 0x06000BA1 RID: 2977 RVA: 0x000467CE File Offset: 0x000449CE
	private void RemoveExpellingRadStatusItem()
	{
		if (Sim.IsRadiationEnabled())
		{
			base.worker.gameObject.GetComponent<KSelectable>().RemoveStatusItem(Db.Get().DuplicantStatusItems.ExpellingRads, false);
		}
	}

	// Token: 0x040007F9 RID: 2041
	private const float BATHROOM_EFFECTS_DURATION_OVERRIDE = 1800f;

	// Token: 0x040007FA RID: 2042
	private Storage storage;

	// Token: 0x040007FB RID: 2043
	private GunkMonitor.Instance gunkMonitor;
}
